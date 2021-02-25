using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Pinger
{
    public partial class Pinger : Form
    {
        public Pinger()
        {
            InitializeComponent();

            m_configVals = new ConfigVals();
            m_data = new Data();

            m_pingURL = m_configVals.PingURL;
            m_URLField.Text = m_pingURL;

            m_stayOnTopToggle.Checked = m_configVals.StayOnTop;
            m_saveCheckBox.Checked = m_configVals.SaveOnStop;

            m_avgList = new RingBus(m_configVals.BufferSize);

            if (m_configVals.StartOnLaunch)
            {
                startButton_Click(null, null);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if(m_pingProcess == null)
            {
                if(string.IsNullOrWhiteSpace(m_pingURL))
                {
                    return;
                }

                m_URLField.Enabled = false;
                m_pingProcess = SpawnProcess("ping", $"{m_pingURL} -t", OnPingData, OnPingError);

                m_startButton.Text = "Stop";
            }
            else
            {
                m_URLField.Enabled = true;

                StopPinger();
                Reset();

                m_startButton.Text = "Start";
            }
        }

        private void stayOnTopToggle_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = m_stayOnTopToggle.Checked;
        }

        private void onFormClosing(object sender, EventArgs e)
        {
            StopPinger();
        }

        private void URLFieldTextChanged(object sender, EventArgs e)
        {
            m_pingURL = m_URLField.Text;
        }

        private void OnPingData(string data)
        {
            SafeWriteControl(m_richTextLabel, () =>
            {
                UpdatePingView(data);
            });
        }
        private void OnPingError(string data)
        {
            Debug.WriteLine(data);
        }

        private void StopPinger()
        {
            if (m_pingProcess != null && !m_pingProcess.HasExited)
            {
                m_pingProcess.Kill();

                if (m_saveCheckBox.Checked)
                {
                    m_data.Save(m_configVals);
                }

                m_pingProcess = null;
            }
        }

        private void UpdatePingView(string pingData)
        {
            if(string.IsNullOrWhiteSpace(pingData))
            {
                return;
            }

            if(m_configVals.PingCountLimit != 0 && m_data.PingCount >= m_configVals.PingCountLimit)
            {
                SafeWriteControl(m_startButton, ()=> { startButton_Click(null, null); });
                return;
            }

            int startidx = pingData.IndexOf(m_pingStartSearchTerm) + m_pingStartSearchTerm.Length;
            if (pingData.IndexOf("Pinging") != -1)
            {
                return;
            }
            else if(startidx == -1)
            {
                ++m_data.PingCount;
                ++m_data.PacketLoss;
            }
            else
            {
                int endidx = pingData.IndexOf(m_pingEndSearchTerm);

                string subStr = pingData.Substring(startidx, endidx - startidx);
                // Update our values
                if (Int16.TryParse(subStr, out short val))
                {
                    m_data.CurPing = val;
                    m_avgList.Add(m_data.CurPing);
                    m_data.AvgPing = m_avgList.GetAverage();
                    ++m_data.PingCount;
                }
            }

            // Display them
            m_richTextLabel.Text = "";
            m_richTextLabel.AppendColourText("Ping: ", Color.Black);
            m_richTextLabel.AppendColourText(m_data.CurPing.ToString(), GetColour(m_data.CurPing, m_configVals.CurrentPingLowerBounds, m_configVals.CurrentPingUpperBounds));

            m_richTextLabel.AppendColourText("\nAverage: ", Color.Black);
            m_richTextLabel.AppendColourText(m_data.AvgPing.ToString(), GetColour(m_data.AvgPing, m_configVals.CurrentPingAvgLowerBounds, m_configVals.CurrentPingAvgUpperBounds));

            m_richTextLabel.AppendColourText("\nLost: ", Color.Black);
            m_richTextLabel.AppendColourText(m_data.PacketLoss.ToString(), GetColour(m_data.PacketLoss, m_configVals.PacketLossLowerBounds, m_configVals.PacketLossUpperBounds));

            string pingCount = m_configVals.PingCountLimit == 0 ? $"\nPing Count: {m_data.PingCount}" : $"\nPing Count: {m_data.PingCount}/{m_configVals.PingCountLimit}";
            m_richTextLabel.AppendColourText(pingCount, Color.Black);
        }

        private void Reset()
        {
            m_avgList = new RingBus(m_configVals.BufferSize);
            m_data = new Data();
        }

        private Color GetColour(int val, int low, int mid)
        {
            if(val < low)
            {
                return Color.Green;
            }
            else if(val < mid)
            {
                return Color.Orange;
            }
            else
            {
                return Color.Red;
            }
        }

        private Process SpawnProcess(string command, string args, Action<string> onData, Action<string> onError)
        {
            StringBuilder finalOutput = new StringBuilder();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            Process newProcess = new Process();

            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            startInfo.UseShellExecute = false;
            startInfo.Arguments = args;
            startInfo.FileName = command;

            newProcess.StartInfo = startInfo;

            newProcess.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate (object sender, DataReceivedEventArgs e)
                {
                    finalOutput.Append(e.Data);
                    onData(e.Data);
                }
            );

            newProcess.ErrorDataReceived += new DataReceivedEventHandler
            (
                delegate (object sender, DataReceivedEventArgs e)
                {
                    finalOutput.Append(e.Data);
                    onError(e.Data);
                }
            );

            newProcess.Start();
            newProcess.BeginOutputReadLine();
            newProcess.BeginErrorReadLine();

            return newProcess;
        }

        private void SafeWriteControl(Control control, Action setData)
        {
            if(control.InvokeRequired)
            {
                control.Invoke(setData);
            }
            else
            {
                setData.Invoke();
            }
        }


        private Process m_pingProcess = null;
        private string m_pingURL = "google.co.uk";

        private Data m_data;

        private RingBus m_avgList;

        private ConfigVals m_configVals;

        private const string m_pingStartSearchTerm = "time=";
        private const string m_pingEndSearchTerm = "ms";

        public class RingBus
        {
            public RingBus(int bufferSize)
            {
                m_buffer = Enumerable.Repeat(-1, bufferSize).ToArray();
                m_bufferSize = bufferSize;
            }

            public void Add(int newVal)
            {
                m_placeIdx = (m_placeIdx + 1) % m_bufferSize;
                m_buffer[m_placeIdx] = newVal;
            }

            public int GetAverage()
            {
                int avg = -1;
                int count = m_bufferSize;
                for(int i = 0; i < m_bufferSize; ++i)
                {
                    int val = m_buffer[i];
                    if(val == -1)
                    {
                        --count;
                        continue;
                    }
                    avg += val;
                }
                avg /= count;

                return avg;
            }

            private readonly int m_bufferSize;
            private readonly int[] m_buffer;
            private int m_placeIdx = -1;
        }

        private class ConfigVals
        {
            public ConfigVals()
            {
                BufferSize = GetInt("BufferSize", 200, (val) => val >= 1);
                PingCountLimit = GetInt("PingCountLimit", 0);
                PingURL = ConfigurationManager.AppSettings["PingURL"];
                PingURL = string.IsNullOrWhiteSpace(PingURL) ? "google.co.uk" : PingURL;
                StayOnTop = ConfigurationManager.AppSettings["AlwaysOnTop"] == "true";
                StartOnLaunch = ConfigurationManager.AppSettings["StartOnLaunch"] == "true";
                SaveOnStop = ConfigurationManager.AppSettings["SaveOnStop"] == "true";
                CurrentPingLowerBounds = GetInt("CurrentPingLowerBounds", 60);
                CurrentPingUpperBounds = GetInt("CurrentPingUpperBounds", 90);
                CurrentPingAvgLowerBounds = GetInt("CurrentPingAverageLowerBounds", 60);
                CurrentPingAvgUpperBounds = GetInt("CurrentPingAverageUpperBounds", 90);
                PacketLossLowerBounds = GetInt("PacketLossLowerBounds", 1);
                PacketLossUpperBounds = GetInt("PacketLossUpperBounds", 2);
            }

            public string GetString()
            {
                return
                    $"\tConfiguration\n" +
                    $"Buffer size: {BufferSize}\n" +
                    $"Ping count limit: {PingCountLimit}\n" +
                    $"Ping url: {PingURL}\n" +
                    $"Stay on top: {StayOnTop}\n" +
                    $"Start on launch: {StartOnLaunch}\n" +
                    $"Save on stop: {SaveOnStop}";
            }

            private int GetInt(string key, int defaultVal, Func<int, bool> validate = null)
            {
                int outVal = defaultVal;
                if (Int32.TryParse(ConfigurationManager.AppSettings[key], out int bufferVal))
                {
                    outVal = Math.Abs(bufferVal);
                }

                if(validate != null && !validate.Invoke(outVal))
                {
                    return defaultVal;
                }

                return outVal;
            }

            public readonly int BufferSize;
            public readonly int PingCountLimit;
            public readonly string PingURL;
            public readonly bool StayOnTop;
            public readonly bool StartOnLaunch;
            public readonly bool SaveOnStop;
            public readonly int CurrentPingLowerBounds;
            public readonly int CurrentPingUpperBounds;
            public readonly int CurrentPingAvgLowerBounds;
            public readonly int CurrentPingAvgUpperBounds;
            public readonly int PacketLossLowerBounds;
            public readonly int PacketLossUpperBounds;
        }

        [System.Serializable]
        private class Data
        {
            public int CurPing = -1;
            public int AvgPing = -1;
            public int PacketLoss = 0;
            public int PingCount = 0;

            public void Save(ConfigVals configVals)
            {
                DateTime dateTime = DateTime.Now;
                string fileName = $"output-{dateTime.Day}_{dateTime.Month}_{dateTime.Year}-{dateTime.Hour}_{dateTime.Minute}_{dateTime.Second}.txt";

                string finalDir = $"{Directory.GetCurrentDirectory()}/{fileName}";
                string data =
                    $"\tPing Data\n" +
                    $"Last ping: {CurPing}\n" +
                    $"Average ping: {AvgPing}\n" +
                    $"Packets lost: {PacketLoss}\n" +
                    $"Ping count: {PingCount}\n\n" +
                    configVals.GetString();
                File.WriteAllText(finalDir, data);
            }
        }
    }
}

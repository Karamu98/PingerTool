
namespace Pinger
{
    partial class Pinger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pinger));
            this.m_startButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_stayOnTopToggle = new System.Windows.Forms.CheckBox();
            this.m_richTextLabel = new RichTextLabel();
            this.m_URLField = new System.Windows.Forms.TextBox();
            this.URLLabel = new System.Windows.Forms.Label();
            this.m_saveCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_startButton
            // 
            this.m_startButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_startButton.Location = new System.Drawing.Point(169, 22);
            this.m_startButton.Name = "m_startButton";
            this.m_startButton.Size = new System.Drawing.Size(75, 23);
            this.m_startButton.TabIndex = 0;
            this.m_startButton.Text = "Start";
            this.m_startButton.UseVisualStyleBackColor = true;
            this.m_startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.m_saveCheckBox);
            this.panel1.Controls.Add(this.m_stayOnTopToggle);
            this.panel1.Controls.Add(this.m_richTextLabel);
            this.panel1.Controls.Add(this.m_URLField);
            this.panel1.Controls.Add(this.URLLabel);
            this.panel1.Controls.Add(this.m_startButton);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 111);
            this.panel1.TabIndex = 1;
            // 
            // m_stayOnTopToggle
            // 
            this.m_stayOnTopToggle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.m_stayOnTopToggle.AutoSize = true;
            this.m_stayOnTopToggle.Location = new System.Drawing.Point(99, 26);
            this.m_stayOnTopToggle.Name = "m_stayOnTopToggle";
            this.m_stayOnTopToggle.Size = new System.Drawing.Size(62, 17);
            this.m_stayOnTopToggle.TabIndex = 5;
            this.m_stayOnTopToggle.Text = "On Top";
            this.m_stayOnTopToggle.UseVisualStyleBackColor = true;
            this.m_stayOnTopToggle.CheckedChanged += new System.EventHandler(this.stayOnTopToggle_CheckedChanged);
            // 
            // m_richTextLabel
            // 
            this.m_richTextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_richTextLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_richTextLabel.Location = new System.Drawing.Point(0, 55);
            this.m_richTextLabel.Name = "m_richTextLabel";
            this.m_richTextLabel.ReadOnly = true;
            this.m_richTextLabel.Size = new System.Drawing.Size(244, 56);
            this.m_richTextLabel.TabIndex = 4;
            this.m_richTextLabel.TabStop = false;
            this.m_richTextLabel.Text = "";
            // 
            // m_URLField
            // 
            this.m_URLField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.m_URLField.Location = new System.Drawing.Point(39, 0);
            this.m_URLField.Name = "m_URLField";
            this.m_URLField.Size = new System.Drawing.Size(205, 20);
            this.m_URLField.TabIndex = 2;
            this.m_URLField.TextChanged += new System.EventHandler(this.URLFieldTextChanged);
            // 
            // URLLabel
            // 
            this.URLLabel.AutoSize = true;
            this.URLLabel.Location = new System.Drawing.Point(0, 3);
            this.URLLabel.Name = "URLLabel";
            this.URLLabel.Size = new System.Drawing.Size(32, 13);
            this.URLLabel.TabIndex = 1;
            this.URLLabel.Text = "URL:";
            this.URLLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveCheckBox
            // 
            this.m_saveCheckBox.AutoSize = true;
            this.m_saveCheckBox.Location = new System.Drawing.Point(39, 26);
            this.m_saveCheckBox.Name = "saveCheckBox";
            this.m_saveCheckBox.Size = new System.Drawing.Size(51, 17);
            this.m_saveCheckBox.TabIndex = 6;
            this.m_saveCheckBox.Text = "Save";
            this.m_saveCheckBox.UseVisualStyleBackColor = true;
            // 
            // Pinger
            // 
            this.AcceptButton = this.m_startButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 135);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Pinger";
            this.Text = "Pinger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onFormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button m_startButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label URLLabel;
        private System.Windows.Forms.TextBox m_URLField;
        private RichTextLabel m_richTextLabel;
        private System.Windows.Forms.CheckBox m_stayOnTopToggle;
        private System.Windows.Forms.CheckBox m_saveCheckBox;
    }
}


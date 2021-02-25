using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pinger
{
    public partial class RichTextLabel : RichTextBox
    {
        public RichTextLabel()
        {
            base.ReadOnly = true;
            base.BorderStyle = BorderStyle.None;
            base.TabStop = false;
            base.SetStyle(ControlStyles.Selectable, false);
            base.SetStyle(ControlStyles.UserMouse, true);
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            base.MouseEnter += delegate (object sender, EventArgs e)
            {
                this.Cursor = Cursors.Default;
            };
        }

        public void AppendColourText(string text, Color color)
        {
            this.SelectionStart = this.TextLength;
            this.SelectionLength = 0;

            this.SelectionColor = color;
            this.AppendText(text);
            this.SelectionColor = this.ForeColor;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x204) return; // WM_RBUTTONDOWN
            if (m.Msg == 0x205) return; // WM_RBUTTONUP
            base.WndProc(ref m);
        }
    }
}

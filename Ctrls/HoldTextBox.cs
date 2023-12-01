using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace AutoBrowser.Ctrls
{
    [TypeDescriptionProvider("code")]
    public class HoldTextBox : TextBox
    {
        [Description("获取和设置 文本框 提示文本(PleaseHolder)")]
        public string PlaceHolderText { get; set; }

        private int m_tab_width = 3;
        public int TabWidth { get { return m_tab_width; } set { if (m_tab_width != value) { m_tab_width = value; this.setTabWidth(); } } }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x0F || m.Msg == 0x133)
            {
                WmPaint(ref m);
            }
            else if (m.Msg == 0x01) //| WM_CREATE
            {
                setTabWidth();
            }
            //| Console.WriteLine("" + m);
        }



        private void WmPaint(ref Message m)
        {
            if (!string.IsNullOrWhiteSpace(this.PlaceHolderText) && string.IsNullOrEmpty(this.Text))
            {
                using (Graphics g = Graphics.FromHwnd(base.Handle))
                {
                    g.DrawString(this.PlaceHolderText, this.Font, Brushes.LightGray, 2, 2);
                }
            }
        }

        protected void setTabWidth()
        {
            const uint EM_SETTABSTOPS = 0x00CB;

            using (Graphics graphics = this.CreateGraphics())
            {
                float characterWidth = (int)graphics.MeasureString("M", this.Font).Width;
                IMKCode.Api.SendMessage(base.Handle, EM_SETTABSTOPS, 1, new int[] { Convert.ToInt32(TabWidth * characterWidth) });
            }
        }

        public HoldTextBox()
        {
        }

    }
}

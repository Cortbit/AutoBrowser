using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoBrowser
{
    public partial class FrmPrompt : Form
    {
        public string Content { get { return txtContent.Text; } set { txtContent.Text = value; } }
        public FrmPrompt()
        {
            InitializeComponent();
        }

        public static bool Prompt(string title, int defValue, string description, out int content)
        {
            string _str = defValue.ToString();
            if (Prompt(title, _str, description, out _str, 0))
            {
                return int.TryParse(_str, out content);
            }
            content = defValue;
            return false;
        }
        public static bool Prompt(string title, string defValue, string description, out string content, int timeout)
        {
            FrmPrompt _prompt = new FrmPrompt();
            if (!string.IsNullOrEmpty(title)) { _prompt.lblTitle.Text = title; }
            if (!string.IsNullOrEmpty(defValue)) { _prompt.txtContent.Text = defValue; }
            if (!string.IsNullOrEmpty(description)) { _prompt.lblDescription.Text = description; }
            if (timeout > 0)
            {
                Timer tmr = new Timer();
                string _btxt = _prompt.Text;
                int _tlife = timeout;
                tmr.Tick += (s, e) => {
                    if (_tlife > 0)
                    {
                        _tlife -= 100;
                        _prompt.Text = _btxt + "("+ ((_tlife / 1000F).ToString("0.0")) +"s)";
                    }
                    else
                    {
                        tmr.Dispose();
                        _prompt.Close();
                    }
                };
                tmr.Interval = 100;
                tmr.Enabled = true;
            }
            DialogResult _res = _prompt.ShowDialog();
            content = _prompt.Content;
            return _res == DialogResult.OK;
        }

        public static bool Prompt(out string content)
        {
            return Prompt(null, null, null, out content, 0);
        }

    }
}

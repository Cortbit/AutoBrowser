using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AutoBrowser
{
    static class Program
    {
        private static string[] m_startArgs;
        /// <summary>
        /// 获取 程序启动时调用的参数
        /// </summary>
        public static string[] StartArguments
        {
            get
            {
                return m_startArgs;
            }
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            m_startArgs = args;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmBrowser());
        }
    }
}

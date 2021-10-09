using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OmronPLCTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //TestSystem.Command.PLC.Omron.HostLink.Fins.Omron_Binary binary = new TestSystem.Command.PLC.Omron.HostLink.Fins.Omron_Binary();
            //binary.BinaryConvert("00", 10);
            Application.Run(new Form1());
        }
    }
}

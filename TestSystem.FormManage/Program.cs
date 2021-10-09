﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace TestSystem.FormManage
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

            bool bCreatedNew;
            Mutex m = new Mutex(false, "TestSystem.FormManage.exe", out bCreatedNew);
            if (bCreatedNew)
            {
                frm_Loading fl = new frm_Loading();
                fl.TopLevel = true;
                fl.Show();
                fl.Refresh();
                System.Threading.Thread.Sleep(3000);
                fl.Close();

                Application.Run(new MDIMainTest());
            }
            else
            {
                MessageBox.Show("该程序已经运行!");
                return;
            }
        }
    }
}

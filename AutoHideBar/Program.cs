﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace AutoHideBar
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if(!new ProcessManager().StartProcess("AutoHideBar"))
            {
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1() { Visible = false});

        }
        
    }
}

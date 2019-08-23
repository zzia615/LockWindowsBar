using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoHideBar
{
    public sealed class ProcessManager
    {
        public const uint WM_SYSCOMMAND = 0x0112;
        public const uint SC_MAXIMIZE = 0xF030;
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        public bool StartProcess(string processName)
        {
            //获取所有进程
            var temp = Process.GetProcesses();
            //获取当前程序运行的进程实例
            var currentProcess = Process.GetCurrentProcess();
            //根据当前进程的名称查找是否已经有相同的进程运行
            Process process = null;
            foreach(var a in temp)
            {
                if (a.ProcessName == currentProcess.ProcessName && a.Id != currentProcess.Id)
                {
                    process = a;
                    break;
                }
            }
            //如果找到同名的进程，则最大化窗口并激活显示
            if (process != null)
            {
                IntPtr handle = process.MainWindowHandle;
                SendMessage(handle, WM_SYSCOMMAND, new IntPtr(SC_MAXIMIZE), IntPtr.Zero); // 最大化  
                SwitchToThisWindow(handle, true);   // 激活  
                return false;
            }
            return true;
        }

        public void RunExe(string exeFile,string[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo(exeFile, args[0]);
                    Process.Start(processStartInfo);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}

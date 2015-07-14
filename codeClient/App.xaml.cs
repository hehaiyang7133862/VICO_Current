using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;
using System.IO;
using System.Diagnostics;
using nsVicoClient.ctrls;
using log4net;

namespace nsVicoClient
{
    public partial class App : Application
    {
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected override void OnStartup(StartupEventArgs e)
        {
            //if (Environment.CurrentDirectory != "D:\\ValmoEngineering")
            //{
            //    MessageBox.Show("Path Error!");
            //    Environment.Exit(0);
            //}

            Process thisProc = Process.GetCurrentProcess();
            if(Process.GetProcessesByName(thisProc.ProcessName).Length >1)
            {
                MessageBox.Show("程序已运行!");
                Environment.Exit(0);

            }

            base.OnStartup(e);

            log4net.Config.XmlConfigurator.Configure();

            log.Info("程序启动");
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            string delFile = @"C:\Users\Valmo\AppData\Local\ValmoEngineering";

            if (Directory.Exists(delFile))
            {
                Directory.Delete(delFile, true);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            valmoWin.execMsg(new WinMsg(WinMsgType.mwMsgSave));

            base.OnExit(e);
        }

        //public bool checkUpgrade()
        //{
        //    foreach (DriveInfo drive in DriveInfo.GetDrives())
        //    {
        //        if (drive.DriveType == DriveType.Removable)
        //        {
        //            if (File.Exists("upGrade.exe"))
        //            {
        //                //FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(drive.Name + "upgrade\\upGrade.exe");
        //                //vm.fprintLn(myFileVersionInfo.FileName + " : " + myFileVersionInfo.FileVersion);
        //                //vm.fflush();
        //                System.Diagnostics.Process.Start("upGrade.exe");
        //                return true;
        //            }
        //            else
        //            {
        //                if (File.Exists(drive.Name + "upgrade\\upGrade.exe"))
        //                {
        //                    File.Copy(drive.Name + "upgrade\\upGrade.exe", "upGrade.exe");
        //                    System.Diagnostics.Process.Start("upGrade.exe");
        //                }
        //            }
                   
        //        }
               
        //    }
        //    return false;
        //}
    }
}

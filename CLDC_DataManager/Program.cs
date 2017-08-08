using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CLDC_DataManager
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

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            if (!System.IO.File.Exists(Application.StartupPath + "\\Plugins\\ManageInfo.ini"))
            {
                System.IO.FileStream Item = System.IO.File.Create(Application.StartupPath + "\\Plugins\\ManageInfo.ini");
                Item.Close();
            }

            if (!System.IO.File.Exists(Application.StartupPath + "\\Plugins\\ReportInfo.ini"))
            {
                System.IO.FileStream Item = System.IO.File.Create(Application.StartupPath + "\\Plugins\\ReportInfo.ini");
                Item.Close();
            }

            if (!System.IO.Directory.Exists(Application.StartupPath + "\\Res"))
            {
                System.IO.Directory.CreateDirectory(Application.StartupPath + "\\Res");
            }

            if (!System.IO.File.Exists(Application.StartupPath + "\\Res\\Templet.ini"))
            {
                System.IO.FileStream Item = System.IO.File.Create(Application.StartupPath + "\\Res\\Templet.ini");
                Item.Close();
            }
            clsMain.DefaultSystem();
            Application.Run(new Frm_Main());

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log("未处理的异常");
            Log(e.ExceptionObject.ToString());
            Log(e.IsTerminating.ToString());
            MessageBox.Show("检测到未处理异常\r\n请查看文件:" + LogFile, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        static string LogFile = Application.StartupPath + "\\cldatamanager.txt";
        public static void Log(string message)
        {
            System.IO.File.AppendAllText(LogFile, message + Environment.NewLine);
        }
    }
}
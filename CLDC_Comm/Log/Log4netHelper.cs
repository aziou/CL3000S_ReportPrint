using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CLDC_Comm.Log
{
    /// <summary>
    /// 日志 操作类
    /// </summary>
    public class Log4netHelper
    {
        /// <summary>
        /// 日志文件名称
        /// </summary>
        public static string LOGFILENAME = "\\Log\\log4netlog.txt";
        /// <summary>
        /// 日志文件配置名称
        /// </summary>
        public static string LOG4NETCONFIGFILENAME = "log4net.config";

        /// <summary>
        /// 日志记录文件最大值
        /// </summary>
        public static int MAXFILESIZE = 10 * 1024 * 1024;

        /// <summary>
        /// 初始化日志组件
        /// </summary>
        public static bool InitLogCommpent()
        {
            try
            {
                // 检查当前已有的日志文件数据量，如果大于 10MB则删除
                string logDir = new FileInfo(typeof(Log4netHelper).Assembly.Location).DirectoryName;
                FileInfo logFileInfo = new FileInfo(logDir + LOGFILENAME);
                if (Directory.Exists(logDir) == false)
                {
                    Directory.CreateDirectory(logDir);
                }
                if (logFileInfo.Exists && logFileInfo.Length > MAXFILESIZE)
                {
                    logFileInfo.Delete();
                }

                FileInfo configFileInfo = new FileInfo(new FileInfo(typeof(Log4netHelper).Assembly.Location).DirectoryName + "\\Appconfigs\\" + LOG4NETCONFIGFILENAME);
                log4net.Config.XmlConfigurator.Configure(configFileInfo);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "初始化日志记录组件失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 卸载日志记录组件
        /// </summary>
        /// <returns></returns>
        public static bool UnLoadLogCommpent()
        {
            try
            {
                log4net.LogManager.Shutdown();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CLDC_Comm
{
    /// <summary>
    /// 控制台，管理 类
    /// </summary>
    public class ConsoleHelper
    {
        /// <summary>
        /// 启动控制台
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        /// <summary>
        /// 释放控制台
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();
    }
}

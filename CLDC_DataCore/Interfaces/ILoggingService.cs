using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Interfaces
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Debug(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void DebugFormatted(string format, params object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Info(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void InfoFormatted(string format, params object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Warn(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Warn(object message, Exception exception);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void WarnFormatted(string format, params object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Error(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Error(object message, Exception exception);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void ErrorFormatted(string format, params object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void Fatal(object message);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Fatal(object message, Exception exception);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void FatalFormatted(string format, params object[] args);
        /// <summary>
        /// 
        /// </summary>
        bool IsDebugEnabled { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsInfoEnabled { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsWarnEnabled { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsErrorEnabled { get; }
        /// <summary>
        /// 
        /// </summary>
        bool IsFatalEnabled { get; }
    }
}

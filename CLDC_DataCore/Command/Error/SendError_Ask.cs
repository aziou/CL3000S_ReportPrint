using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Error
{
    /// <summary>
    /// 发送错误提示信息、通常是一端向另一端通过网络数据发送
    /// </summary>
    [Serializable]
    public class SendError_Ask : Command_Ask 
    {
        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorDescription = string.Empty;

        /// <summary>
        /// 其他数据信息
        /// </summary>
        public object ErrorOther = null;
        /// 错误提示信息
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public SendError_Ask()
        {
            AskMessage = "错误提示信息";
        }
    }
}

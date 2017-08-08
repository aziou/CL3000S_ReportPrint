using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Controlling
{
    /// <summary>
    /// 服务器主动控制客户端的主动、被动模式
    /// </summary>
    [Serializable]
    public class SendControlling_Ask:Command_Ask 
    {
        /// <summary>
        /// true 跳到主控，false 跳转到被控
        /// </summary>
        public bool ControllingOrRelease = true;
        /// <summary>
        /// 
        /// </summary>
        public SendControlling_Ask()
        {
            AskMessage = "控制权切换";
        }


    }
}

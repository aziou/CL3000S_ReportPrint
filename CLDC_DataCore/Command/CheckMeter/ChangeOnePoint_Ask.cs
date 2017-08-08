using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// 单步检定
    /// </summary>
    [Serializable]
    public class ChangeOnePoint_Ask : Command_Ask
    {
         /// <summary>
        /// 要跳转到的点
        /// </summary>
        public int ActiveID;
        /// <summary>
        /// 开始单步检定
        /// </summary>
        public ChangeOnePoint_Ask()
        {
            AskMessage="开始单步检定";
        }
    }
}

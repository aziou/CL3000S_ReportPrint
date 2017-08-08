using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// 请求跳点
    /// </summary>
    [Serializable]
    public class ChangePoint_Ask:Command_Ask 
    {
        /// <summary>
        /// 要跳转到的点
        /// </summary>
        public int ActiveID;

        /// <summary>
        /// 
        /// </summary>
        public ChangePoint_Ask()
        {
            AskMessage = "检定点切换";
        }
    }


}

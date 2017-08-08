using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Controlling
{
    /// <summary>
    /// 申请控制
    /// </summary>
    [Serializable]
    public class RequestControlling_Ask:Command_Ask 
    {
        /// <summary>
        /// true 申请控制权，false 释放控制权
        /// </summary>
        public bool ControllingOrRelease = true;

        /// <summary>
        /// 
        /// </summary>
        public RequestControlling_Ask()
        {
            AskMessage = "申请控制";
        }
    }


}

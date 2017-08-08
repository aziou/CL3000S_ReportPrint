using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Controlling
{
    /// <summary>
    /// 对申请控制请求的返回
    /// </summary>
    [Serializable]
    public class RequestControlling_Answer:Command_Answer 
    {
        /// <summary>
        /// 是否同意控制
        /// </summary>
        public bool bAgree = false;

        /// <summary>
        /// 
        /// </summary>
        public RequestControlling_Answer()
        {
        }
    }
}

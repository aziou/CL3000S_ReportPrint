using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.ErrorCounters
{

    /// <summary>
    /// 误差板 信息
    /// </summary>
    [Serializable]
    public class ErrorCounterInfo
    {
        /// <summary>
        /// 误差板上的值
        /// </summary>
        public string ErrorValue
        {
            get;
            set;
        }

        /// <summary>
        /// 485 通信是否正常
        /// </summary>
        public bool IsRs485OK
        {
            get;
            set;
        }

        /// <summary>
        /// 脉冲通信是否正常
        /// </summary>
        public bool IsPluseOK
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ErrorCounterInfo()
        {
            this.ErrorValue = "";
        }
    }
}

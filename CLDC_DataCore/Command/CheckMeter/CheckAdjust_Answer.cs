using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// 请求调表、或者结束调表
    /// </summary>
    [Serializable]
    public class CheckAdjust_Answer:Command_Answer 
    {
        /// <summary>
        /// true调表、false结束调表
        /// </summary>
        public bool  bAgree;



    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.MessageArgs
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public class DgnArgs:EventMessageArgs
    {
        /// <summary>
        /// 表位号
        /// </summary>
        public int BW;             
        //public CLDC_DataCore.Model.DnbModel.DnbInfo.MeterDgn DngResult;//多功能结论
    }
}

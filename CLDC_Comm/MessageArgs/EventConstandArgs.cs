using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.MessageArgs
{
    /// <summary>
    /// 走字试验数据
    /// </summary>
    [Serializable()]
    public class EventConstandArgs:EventMessageArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public string ItemKey;
        //public CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError ZZError;
    }
}

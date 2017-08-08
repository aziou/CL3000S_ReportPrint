using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /*
     电能表检定结果基类
     
     */
    [Serializable()]
    public class MeterErrorBase
    {
        /// <summary>
        /// 表唯一ID号
        /// </summary>
        public long _intMyId = 0;
        /// <summary>
        /// 台体编号
        /// </summary>
        public string _intTaiNo = "";
        /// <summary>
        /// 表位号		在表架上所挂位置
        /// </summary>
        public int _intBno = 0;

        /// <summary>
        /// 26.方案编号
        /// </summary>
        public long FK_LNG_SCHEME_ID { get; set; }

        /// <summary>
        /// 27.不合格原因
        /// </summary>
        public string AVR_DIS_REASON { get; set; }
    }
}

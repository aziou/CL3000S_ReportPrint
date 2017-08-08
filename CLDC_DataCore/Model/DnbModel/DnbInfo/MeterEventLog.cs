using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 事件记录检定数据
    /// </summary>
    [Serializable()]
    public class MeterEventLog : MeterErrorBase 
    {
        /// <summary>
        /// 表唯一ID号	
        /// </summary>
        public long Mel_lngMyID;
        /// <summary>
        /// 事件记录项目ID	
        /// </summary>
        public string Mel_PrjID;
        /// <summary>
        /// 项目名称		描述
        /// </summary>
        public string Mel_PrjName;
	    /// <summary>
	    /// 项目值
	    /// </summary>
        public string Mel_chrValue;
        /// <summary>
        /// 结论
        /// </summary>
        public string Mel_Result;
    }
}

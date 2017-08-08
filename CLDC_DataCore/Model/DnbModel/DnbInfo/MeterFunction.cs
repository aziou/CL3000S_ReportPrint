using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 智能表功能检定数据
    /// </summary>
    [Serializable()]
    public class MeterFunction : MeterErrorBase 
    {
        /// <summary>
        /// 表唯一ID号	
        /// </summary>
        public long Mf_lngMyID;
        /// <summary>
        /// 智能表功能项目ID	
        /// </summary>
        public string Mf_PrjID;
        /// <summary>
        /// 项目名称		描述
        /// </summary>
        public string Mf_PrjName;
	    /// <summary>
	    /// 项目值
	    /// </summary>
        public string Mf_chrValue;

        /// <summary>
        /// 结论
        /// </summary>
        public string Mf_Result;
    }
}

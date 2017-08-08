using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 费控功能检定数据
    /// </summary>
    [Serializable()]
    public class MeterCostControl : MeterErrorBase 
    {
        /// <summary>
        /// 表唯一ID号	
        /// </summary>
        public long Mcc_lngMyID;
        /// <summary>
        /// 费控功能项目ID	
        /// </summary>
        public string Mcc_PrjID;
        /// <summary>
        /// 项目名称		描述
        /// </summary>
        public string Mcc_PrjName;
	    /// <summary>
	    /// 项目值
	    /// </summary>
        public string Mcc_chrValue;
        /// <summary>
        /// 结论
        /// </summary>
        public string Mcc_Result;
    }
}

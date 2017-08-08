using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 多功能检定数据
    /// </summary>
    [Serializable()]
    public class MeterDgn:MeterErrorBase 
    {
        /// <summary>
        /// 多功能项目ID	
        /// </summary>
        public string Md_PrjID = "";
        /// <summary>
        /// 项目名称		描述
        /// </summary>
        public string Md_PrjName = "";
	    /// <summary>
	    /// 项目值
	    /// </summary>
        public string Md_chrValue = "";

        /// <summary>
        /// 6结论Y/N
        /// </summary>
        public string AVR_CONCLUSION { get; set; }

        
    }
}

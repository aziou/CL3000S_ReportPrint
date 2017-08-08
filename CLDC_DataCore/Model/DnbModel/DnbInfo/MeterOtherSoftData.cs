using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 南网费控软件多功能检定数据
    /// </summary>
    [Serializable()]
    public class MeterOtherSoftData:MeterErrorBase 
    {
        /// <summary>
        /// 多功能项目ID	
        /// </summary>
        public string Mosd_PrjID = "";
        /// <summary>
        /// 项目名称		描述
        /// </summary>
        public string Mosd_PrjName = "";
	    /// <summary>
	    /// 项目值
	    /// </summary>
        public string Mosd_chrValue = "";

        /// <summary>
        /// 6结论Y/N
        /// </summary>
        public string AVR_CONCLUSION { get; set; }

        
    }
}

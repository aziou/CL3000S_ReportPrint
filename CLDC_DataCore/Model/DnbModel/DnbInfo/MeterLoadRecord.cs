using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 负荷记录检定数据
    /// </summary>
    [Serializable()]
    public class MeterLoadRecord : MeterErrorBase 
    {        
        /// <summary>
        /// 负荷记录项目ID	
        /// </summary>
        public string Ml_PrjID;
        /// <summary>
        /// 项目名称		描述
        /// </summary>
        public string Ml_PrjName;
        /// <summary>
        /// 子项试验名称
        /// </summary>
        public string Ml_SubItemName;
	    /// <summary>
	    /// 项目值
	    /// </summary>
        public string Ml_chrValue;

        /// <summary>
        /// 结论
        /// </summary>
        public string Ml_Result;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 费控数据
    /// </summary>
    [Serializable()]
    public class MeterFK : MeterErrorBase
    {
        
        /// <summary>
        /// 4组别
        /// </summary>
        public string Mfk_chrGrpType = "";
        /// <summary>
        /// 5项目类型
        /// </summary>
        public string Mfk_chrItemType = "";
        /// <summary>
        /// 6结论
        /// </summary>
        public string Mfk_chrJL = "";
        /// <summary>
        /// 7数据
        /// </summary>
        public string Mfk_chrData = "";

        /// <summary>
        /// 项目名称		描述
        /// </summary>
        public string Mcc_PrjName;
    }
}

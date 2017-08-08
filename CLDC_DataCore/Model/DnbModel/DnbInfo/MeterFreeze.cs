using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 冻结检定数据
    /// </summary>
    [Serializable()]
    public class MeterFreeze : MeterErrorBase
    {
        /// <summary>
        /// 表唯一ID号	
        /// </summary>
        public long Md_lngMyID;
        /// <summary>
        /// 冻结项目ID	
        /// </summary>
        public string Md_PrjID;
        /// <summary>
        /// 项目名称		描述
        /// </summary>
        public string Md_PrjName;
        /// <summary>
        /// 项目值
        /// </summary>
        public string Md_chrValue;
    }
}

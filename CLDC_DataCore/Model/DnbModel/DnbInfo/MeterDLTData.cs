using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 规约一致性数据
    /// </summary>
    [Serializable()]
    public class MeterDLTData : MeterErrorBase
    {
        
        /// <summary>
        /// 项目ID  关联方案表Scheme_DLTData索引
        /// </summary>
        public string Mdlt_intItemID = "";
        /// <summary>
        /// 读写值  当读时保存值，当写时填“成功”、“失败”
        /// </summary>
        public string Mdlt_chrValue = "";
        /// <summary>
        /// 5数据标识
        /// </summary>
        public string AVR_DI0_DI3 { get; set; }

        /// <summary>
        /// 6标识意义
        /// </summary>
        public string AVR_DI_MSG { get; set; }

        /// <summary>
        /// 7长度
        /// </summary>
        public string AVR_DI_LEN { get; set; }

        /// <summary>
        /// 8格式
        /// </summary>
        public string AVR_DI_FORMAT { get; set; }


        /// <summary>
        /// 10对比值
        /// </summary>
        public string AVR_COMPARISON_VALUE { get; set; }

        /// <summary>
        /// 11对比条件
        /// </summary>
        public string AVR_CONDITION { get; set; }
        /// <summary>
        /// 13结论Y/N
        /// </summary>
        public string AVR_CONC { get; set; }
        /// <summary>
        /// 进度
        /// </summary>
        public string _str_progress = "";
    }
}

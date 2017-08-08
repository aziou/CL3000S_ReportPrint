using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 一致性试验数据
    /// </summary>
    [Serializable()]
    public class MeterConsistency : MeterErrorBase
    {
        
        /// <summary>
        /// 组别	
        /// </summary>
        public string Mc_chrGrpType = "";
        /// <summary>
        /// 项目类型
        /// </summary>
        public string Mc_chrItemType = "";
        /// <summary>
        /// 项目负载点编号
        /// </summary>
        public int Mc_intItemNo = 0;
        /// <summary>
        /// 采样类型
        /// </summary>
        public int Mc_intSamplingType = 0;
        /// <summary>
        /// 结论	
        /// </summary>
        public string Mc_chrJL = "";
        /// <summary>
        /// 采样化整值	
        /// </summary>
        public string Mc_chrDataInt = "";
        /// <summary>
        /// 采样平均值	
        /// </summary>
        public string Mc_chrDataAvg = "";
        /// <summary>
        /// 采样原始值	以“|”分隔
        /// </summary>
        public string Mc_chrData = "";


        /// <summary>
        /// 7方案的该负载点参数
        /// </summary>
        public string AVR_PARAMETER { get; set; }


        /// <summary>
        /// 9一次采样化整值
        /// </summary>
        public string AVR_DATA_1_ROUNDING { get; set; }

        /// <summary>
        /// 10一次采样平均值
        /// </summary>
        public string AVR_DATA_1_AVG { get; set; }

        /// <summary>
        /// 11一次采样原始值，以“|”分隔
        /// </summary>
        public string AVR_DATAS_1 { get; set; }

        /// <summary>
        /// 12二次采样化整值
        /// </summary>
        public string AVR_DATA_2_ROUNDING { get; set; }
        /// <summary>
        /// 13二次采样平均值
        /// </summary>
        public string AVR_DATA_2_AVG { get; set; }
        /// <summary>
        /// 14二次采样原始值，以“|”分隔
        /// </summary>
        public string AVR_DATAS_2 { get; set; }

        /// <summary>
        /// 15变差化整值
        /// </summary>
        public string AVR_DIF_DATA_ROUNDING { get; set; }

        /// <summary>
        /// 16变差平均值
        /// </summary>
        public string AVR_DIF_DATA_AVG { get; set; }


        /// <summary>
        /// 17变差限
        /// </summary>
        public string AVR_DIF_ERROR_LIMIT { get; set; }
    }
}

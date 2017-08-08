using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 潜动启动数据
    /// </summary>
    [Serializable()]
    public class MeterQdQid : MeterErrorBase
    {

        /// <summary>
        /// 4检定项目号
        /// </summary>
        public string Mqd_chrProjectNo = "";
        /// <summary>
        /// 5检定方向
        /// </summary>
        public string Mqd_chrJdfx = "";
        /// <summary>
        /// 6项目名称
        /// </summary>
        public string Mqd_chrProjectName = "";
        /// <summary>
        /// 7结论
        /// </summary>
        public string Mqd_chrJL = "";
        /// <summary>
        /// 8时间
        /// </summary>
        public string Mqd_chrTime = "";
        /// <summary>
        /// 12电流
        /// </summary>
        public string Mqd_chrDL = "";

        /// <summary>
        /// 9开始时间
        /// </summary>
        public string DTM_BEGIN_TIME { get; set; }

        /// <summary>
        /// 10结束时间
        /// </summary>
        public string DTM_OVER_TIME { get; set; }

        /// <summary>
        /// 11实际起动时间（秒）
        /// </summary>
        public string AVR_ACTUAL_TIME { get; set; }

        /// <summary>
        /// 12标准时间(秒)
        /// </summary>
        public string AVR_STANDARD_TIME { get; set; }
        /// 13电压倍数（%换成小数，例如1.15））
        /// </summary>
        public string AVR_VOLTAGE { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 耐压检定数据
    /// </summary>
    [Serializable()]
    public class MeterInsulation : MeterErrorBase
    {
        /// <summary>
        /// 耐压试验类型
        /// </summary>
        public string InsulationType;
        /// <summary>
        /// 耐压值
        /// </summary>
        public int Voltage;
        /// <summary>
        /// 耐压时间
        /// </summary>
        public int Time;
        /// <summary>
        /// 已测试时间
        /// </summary>
        public int TestTime;
        /// <summary>
        /// 表位漏电流（mA）
        /// </summary>
        public string stringCurrent;
        /// <summary>
        /// 检定结论 合格/不合格
        /// </summary>
        public string Result;
        /// <summary>
        /// 方案编号
        /// </summary>
        public string planID;
        /// <summary>
        /// 不合格原因
        /// </summary>
        public string Description;

    }
}

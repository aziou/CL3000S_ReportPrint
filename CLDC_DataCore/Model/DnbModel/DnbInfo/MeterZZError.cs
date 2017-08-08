using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    [Serializable()]
    public class MeterZZError:MeterErrorBase 
    {

        /// <summary>
        /// 4项目ID
        /// </summary>
        public string Me_chrProjectNo = "";
	    /// <summary>
	    /// 10起码
	    /// </summary>
        public float Mz_chrQiMa = -1F;
		/// <summary>
		/// 11止码	
		/// </summary>
        public float Mz_chrZiMa = -1F;
	    
		/// <summary>
		/// 17误差
		/// </summary>
        public string Mz_chrWc = "";
		/// <summary>
		/// 19结论		合格/不合格
		/// </summary>
        public string Mz_chrJL = "";
	    
        /// <summary>
        /// 5检定方向
        /// </summary>
        public string Mz_chrJdfx = "";
        /// <summary>
        /// 6费率
        /// </summary>
        public string Mz_chrFl = "";
        /// <summary>
        /// 7走字起始时间
        /// </summary>
        public string Mz_chrStartTime = "";
        /// <summary>
        /// 8走字需要时间
        /// </summary>
        public string Mz_chrNeedTime = "";
        /// <summary>
        /// 12起码总
        /// </summary>
        public string Mz_chrQiMaZ = "";
        /// <summary>
        /// 13止码总
        /// </summary>
        public string Mz_chrZiMaZ = "";
        /// <summary>
        /// 14起止码差
        /// </summary>
        public string Mz_chrQiZiMaC = "";
        /// <summary>
        /// 15所走脉冲数
        /// </summary>
        public string Mz_chrPules = "";
        /// <summary>
        /// 功率因数
        /// </summary>
        public string Mz_chrGlys = "";
        /// <summary>
        /// 电流倍数
        /// </summary>
        public string Mz_chrxIb = "";
        /// <summary>
        /// 电流倍数字符串
        /// </summary>
        public string Mz_chrxIbString = "";
        /// <summary>
        /// 元件
        /// </summary>
        public string Mz_chrYj = "";
        /// <summary>
        /// 9对应需要走字电量，或按照时间走字计算的电量
        /// </summary>
        public string AVR_NEED_ENERGY { get; set; }

        /// <summary>
        /// 16标准表累计电量
        /// </summary>
        public string AVR_STANDARD_METER_ENERGY { get; set; }

        /// <summary>
        /// 18试验方式，标准表法，计读脉冲法，走字试验法
        /// </summary>
        public string AVR_TEST_WAY { get; set; }
    }
}

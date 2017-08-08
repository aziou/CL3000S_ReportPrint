using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 预热结构体
    /// </summary>
    [Serializable()]
    public struct StPlan_YuRe
    {
        /// <summary>
        /// 功率方向
        /// </summary>
        public CLDC_Comm.Enum.Cus_PowerFangXiang PowerFangXiang;
        /// <summary>
        /// 电流倍数
        /// </summary>
        public string xIb;
        /// <summary>
        /// 预热时间
        /// </summary>
        public float Times;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}预热：电流倍数{1} 时间{2}分", PowerFangXiang.ToString(), xIb, Times);
        }
    }
}

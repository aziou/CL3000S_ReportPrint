using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 负荷记录
    /// </summary>
    [Serializable]
    public struct StPlan_LoadRecord
    {
        public string PrjID;

        public int OverTime;

        public string danWei;

        public int MarginTime;

        public string ModeByte;

        public List<StRunningE> RunningEPrj;

        public override string ToString()
        {
            return "负荷记录【运行" + OverTime + danWei + "】";
        }        

    }
    [Serializable]
    public struct StRunningE
    {
        /// <summary>
        /// 功率方向有无功
        /// </summary>
        public CLDC_Comm.Enum.Cus_PowerFangXiang PowerFX;
        /// <summary>
        /// 电流倍数
        /// </summary>
        public string xIB;
        /// <summary>
        /// 功率因数
        /// </summary>
        public string Glys;
        /// <summary>
        /// 运行时间
        /// </summary>
        public string RunningTime;

    }
}

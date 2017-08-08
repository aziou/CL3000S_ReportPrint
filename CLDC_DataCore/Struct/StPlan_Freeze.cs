using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 冻结方案项目
    /// </summary>
    [Serializable()]
    public class StPlan_Freeze
    {
        /// <summary>
        /// 冻结项目ID
        /// </summary>
        public string FreezePrjID;
        /// <summary>
        /// 冻结项目名称  
        /// </summary>
        public string FreezePrjName;
        /// <summary>
        /// 源输出参数
        /// </summary>
        public CLDC_DataCore.Struct.StPowerPramerter OutPramerter;
        /// <summary>
        /// 项目检定参数
        /// </summary>
        public string PrjParm;

        public override string ToString()
        {
            return FreezePrjName;
        }
    }
}

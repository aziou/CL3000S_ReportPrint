using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 费控功能方案项目
    /// </summary>
    [Serializable()]
    public class StPlan_CostControl
    {
        /// <summary>
        /// 费控功能项目ID
        /// </summary>
        public string CostControlPrjID;
        /// <summary>
        /// 智能表功能项目名称  
        /// </summary>
        public string CostControlPrjName;
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
            return CostControlPrjName;
        }
    }
}

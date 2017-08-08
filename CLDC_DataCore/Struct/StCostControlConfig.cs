using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 费控功能项目配置信息
    /// </summary>
    public struct StCostControlConfig
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
        public StPowerPramerter OutPramerter;
    }
}

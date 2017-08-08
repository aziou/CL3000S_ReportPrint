using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 多功能方案项目
    /// </summary>
    [Serializable()]
    public struct StPlan_Dgn
    {
        /// <summary>
        /// 多功能项目ID
        /// </summary>
        public string DgnPrjID;
        /// <summary>
        /// 多功能项目名称  
        /// </summary>
        public string DgnPrjName;
        /// <summary>
        /// 源输出参数
        /// </summary>
        public CLDC_DataCore.Struct.StPowerPramerter OutPramerter;
        /// <summary>
        /// 多功能检测项目检定全部参数
        /// </summary>
        public string PrjParm;

        public override string ToString()
        {
            return DgnPrjName;
        }

    }
}

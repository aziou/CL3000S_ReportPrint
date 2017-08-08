using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 预先调试项目
    /// </summary>
    [Serializable]
    public struct StPlan_PrePareTest
    {
        /// <summary>
        /// 预先调试项目ID
        /// </summary>
        public string PrePrjID;
        /// <summary>
        /// 预先调试项目名称  
        /// </summary>
        public string PrePrjName;
        /// <summary>
        /// 源输出参数
        /// </summary>
        public CLDC_DataCore.Struct.StPowerPramerter OutPramerter;
        /// <summary>
        /// 预先调试检测项目检定全部参数
        /// </summary>
        public string PrjParm;

        public override string ToString()
        {
            return PrePrjName;
        }
    }
}

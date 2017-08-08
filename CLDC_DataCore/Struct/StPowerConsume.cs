using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 功耗试验项目结构
    /// </summary>
    [Serializable()]
    public struct StPowerConsume
    {
        /// <summary>
        /// 功耗项目ID
        /// </summary>
        public string PowerConsumePrjID;
        /// <summary>
        /// 功耗项目名称  
        /// </summary>
        public string PowerConsumePrjName;
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
            return PowerConsumePrjName;
        }
    }
}

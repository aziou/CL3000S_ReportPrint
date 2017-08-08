using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 外观检查试验结构体
    /// </summary>
    [Serializable()]
    public struct StPlan_WGJC
    {
        /// <summary>
        /// ID
        /// </summary>
        public string WGJCPrjID;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "外观检查";
        }
    }
}

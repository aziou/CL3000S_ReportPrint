using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.Enum;
namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public struct StFAGroup
    {
        /// <summary>
        /// 方案类型
        /// </summary>
        public Cus_FAGroup FAType;
        /// <summary>
        /// 方案名称
        /// </summary>
        public string FAName;
        /// <summary>
        /// 要保存的序号从0起 2011/6/3 by netteans@163.com
        /// </summary>
        public int index;
    }
}

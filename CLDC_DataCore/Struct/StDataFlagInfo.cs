using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 功能描述：数据标识信息
    /// 作    者：zzg
    /// 编写日期：2013-04-26
    /// 修改记录：
    /// 修改日期		     修改人	            修改内容
    ///
    /// </summary>
    [Serializable()]
    public struct StDataFlagInfo
    {
        /// <summary>
        /// 数据标识名称
        /// </summary>
        public string DataFlagName;
        /// <summary>
        /// 数据标识
        /// </summary>
        public string DataFlag;
        /// <summary>
        /// 数据长度
        /// </summary>
        public string DataLength;
        /// <summary>
        /// 小数位
        /// </summary>
        public string DataSmallNumber;        
        /// <summary>
        /// 数据格式
        /// </summary>
        public string DataFormat;        
    }
}

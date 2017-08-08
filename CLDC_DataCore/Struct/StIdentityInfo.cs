using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 身份认证信息
    /// </summary>
    [Serializable()]
    public struct StIdentityInfo
    {        
        /// <summary>
        /// 随机数2
        /// </summary>
        public byte[] Rand;

        /// <summary>
        /// Esam序列号
        /// </summary>
        public byte[] EsamNo;
    }
}

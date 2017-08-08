using System;
using System.Collections.Generic;
using System.Text;
using CLDC_CTNProtocol;
using CLDC_Comm.Enum;

namespace CLDC_Comm.Command
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public class CheckNow : CTNPCommand
    {
        /// <summary>
        /// 下面是数据字段
        /// </summary>
        public int ItemID = 0;
        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckNow()
        {
        }
    }

}

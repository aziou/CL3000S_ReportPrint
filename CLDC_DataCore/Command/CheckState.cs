using System;
using System.Collections.Generic;
using System.Text;
using CLDC_CTNProtocol;
using CLDC_Comm.Enum;

namespace CLDC_Comm.Command
{
    /// <summary>
    /// 状态报告,已经预置MAINid:客户商---->服务器;SUBID：报告状态，如果需要作其它用途请修改
    /// </summary>
    [Serializable()]
    public class CheckState : CTNPCommand
    {
        /// <summary>
        /// 下面是数据字段
        /// </summary>
        public MessageArgs.EventMessageArgs MessageArgs = null;
        /// <summary>
        /// 报告当前检定状态
        /// </summary>
        public Cus_CheckStaute checkState = Cus_CheckStaute.未赋值的;

    }

}

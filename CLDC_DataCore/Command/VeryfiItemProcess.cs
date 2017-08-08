using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Command
{
    /// <summary>
    /// 检定项目进度报告
    /// </summary>
    [Serializable()]
    public class VeryfiItemProcess:CLDC_CTNProtocol.CTNPCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public CLDC_Comm.MessageArgs.EventProcessArgs ProcessArg = null;

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command
{ 
    /// <summary>
    /// 网络命令(回答)基类
    /// </summary>
    [Serializable()]
    public class Command_Answer:CLDC_CTNProtocol.CTNPCommand
    {
        /// <summary>
        /// 返回的信息提示
        /// </summary>
        public string AnswerMessage = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public Command_Answer()
        {
            AnswerMessage = string.Empty;
        }
    }


}

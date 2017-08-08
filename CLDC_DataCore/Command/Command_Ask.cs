using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command
{
    /// <summary>
    /// 网络命令 (问) 基类
    /// </summary>   
    [Serializable()]
    public class Command_Ask:CLDC_CTNProtocol.CTNPCommand 
    {
        /// <summary>
        /// 相关提示文字信息
        /// </summary>
        public string AskMessage = string.Empty;

        /// 台体编号
        /// <summary>
        /// 台体编号
        /// </summary>
        public int taiID = -1;

        /// <summary>
        /// 
        /// </summary>
        public Command_Ask()
        {
            AskMessage = string.Empty;
        }
    }


}

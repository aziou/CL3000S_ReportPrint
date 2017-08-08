using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.ReadPara
{
    /// <summary>
    /// 参数读取申请
    /// </summary>
    [Serializable]
    public class ReadPara_Ask : Command_Ask
    {
        public ReadPara_Ask()
        {
            AskMessage = "参数读取申请";
        }

    }
}

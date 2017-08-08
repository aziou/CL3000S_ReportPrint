using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Txm
{
    /// <summary>
    /// 客户端每录一个条码都需要通过本结构发送到客户端
    /// </summary>
    [Serializable]
    public class InputTxm_Update_Ask : Command_Ask
    {
        /// <summary>
        /// 条码号输入的条码号
        /// </summary>
        public string Txm = "";		

        /// <summary>
        /// 表位号.下标从0开始
        /// </summary>
        public int Bwh = 0;			

        /// <summary>
        /// 构造函数
        /// </summary>
        public InputTxm_Update_Ask()
        {
            AskMessage = "更新条码号";
        }

    }
}

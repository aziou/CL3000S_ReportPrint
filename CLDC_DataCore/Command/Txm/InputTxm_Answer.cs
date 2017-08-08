using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Txm
{
    /// <summary>
    /// 请求扫条码的回复包
    /// </summary>
    [Serializable]
    public class InputTxm_Answer : Command_Answer
    {

        /// <summary>
        /// 是否同意
        /// </summary>
        public bool bAgree = false;

        /// <summary>
        /// 
        /// </summary>
        public InputTxm_Answer()
        {
        }

    }


}

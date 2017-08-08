using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Login2
{
    /// <summary>
    /// 回复登陆
    /// </summary>
    [Serializable]
    public class Login_Answer:Command_Answer 
    {
        /// <summary>    
        /// 是否得到允许
        /// </summary>
        public bool bAgree = false;

    }


}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Update
{
    /// <summary>
    /// ActiveId被改变
    /// </summary>
    [Serializable ]
    public class UpdateActiveId_Ask:Command_Ask 
    {
        /// <summary>
        /// 新的、改变以后的ActiveId的值
        /// </summary>
        public int ActiveId;

        public UpdateActiveId_Ask()
        {
            AskMessage = "条码号变更";
        }
    }
}

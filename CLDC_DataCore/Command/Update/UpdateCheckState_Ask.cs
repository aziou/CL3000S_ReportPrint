using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Update
{
    /// <summary>
    /// ����춨״̬
    /// </summary>
    [Serializable()]
    public class UpdateCheckState_Ask:Command_Ask 
    {
        /// <summary>
        /// 
        /// </summary>
        public CLDC_Comm.Enum.Cus_CheckStaute CheckState;
        /// <summary>
        /// 
        /// </summary>
        public UpdateCheckState_Ask()
        {
            AskMessage = "�춨״̬���";
        }
    }


}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Model
{
    /// <summary>
    /// ��������һֻ�������
    /// </summary>
    [Serializable ]
    public class SendMeterData_Ask:Command_Ask 
    {
        /// <summary>
        /// һֻ��������
        /// </summary>
        //public CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo  MeterData = null;

        public SendMeterData_Ask()
        {
            AskMessage = "һֻ��������";
        }
    }


}

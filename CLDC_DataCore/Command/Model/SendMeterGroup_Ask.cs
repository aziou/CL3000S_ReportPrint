using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Model
{
    /// <summary>
    /// ������ģ��
    /// </summary>
    [Serializable]
    public class SendMeterGroup_Ask : Command_Ask 
    {
        /// <summary>
        /// ��ģ������
        /// </summary>
        //public CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup = null;

        public SendMeterGroup_Ask()
        {
            AskMessage = "���б������";
        }
    }
}

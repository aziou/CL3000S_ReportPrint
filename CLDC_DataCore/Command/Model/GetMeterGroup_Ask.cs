using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Model
{
    /// <summary>
    /// ������ MeterGroup����ģ��
    /// </summary>
    [Serializable]
    public class GetMeterGroup_Ask : Command_Ask 
    {
        /// <summary>
        /// 
        /// </summary>
        public GetMeterGroup_Ask()
        {
            AskMessage = "���б������";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Controlling
{
    /// <summary>
    /// �������������ƿͻ��˵�����������ģʽ
    /// </summary>
    [Serializable]
    public class SendControlling_Ask:Command_Ask 
    {
        /// <summary>
        /// true �������أ�false ��ת������
        /// </summary>
        public bool ControllingOrRelease = true;
        /// <summary>
        /// 
        /// </summary>
        public SendControlling_Ask()
        {
            AskMessage = "����Ȩ�л�";
        }


    }
}

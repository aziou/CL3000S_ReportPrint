using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Controlling
{
    /// <summary>
    /// �������
    /// </summary>
    [Serializable]
    public class RequestControlling_Ask:Command_Ask 
    {
        /// <summary>
        /// true �������Ȩ��false �ͷſ���Ȩ
        /// </summary>
        public bool ControllingOrRelease = true;

        /// <summary>
        /// 
        /// </summary>
        public RequestControlling_Ask()
        {
            AskMessage = "�������";
        }
    }


}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.SaveData
{
    /// <summary>
    /// ��������
    /// </summary>
    [Serializable]
    public class SaveData_Ask:Command_Ask 
    {

        ///// <summary>
        ///// ��ģ�����ݡ�ʹ������Ҫ���ǵ�ǰ�������ı�Ϊ׼��
        ///// </summary>
        //public CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup = null;
        /// <summary>
        /// 
        /// </summary>
        public SaveData_Ask()
        {
            AskMessage = "��������";
        }

    }

}

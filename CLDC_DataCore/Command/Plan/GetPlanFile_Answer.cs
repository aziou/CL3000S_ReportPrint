using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Plan
{

    /// <summary>
    /// ���ͷ������ݵķ���
    /// </summary>
    [Serializable]
    public class GetPlanFile_Answer:Command_Answer 
    {
        /// <summary>
        /// �Ƿ��Ѿ�װ�������ݵ�
        /// </summary>
        public bool IsLoadFile = false;

        /// <summary>
        /// ���������ļ���Key =�ļ���(���·��) �� Value = ��Ӧ�ļ�������
        /// ���������ļ���Const�ļ��е��ļ�
        /// </summary>
        public Dictionary<string, byte[]> LstFileData = new Dictionary<string, byte[]>();

        /// <summary>
        /// 
        /// </summary>
        public GetPlanFile_Answer()
        {
        }


    }


}

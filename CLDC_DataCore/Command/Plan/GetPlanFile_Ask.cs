using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Plan
{

    /// <summary>
    /// ���ͷ�����Ϣ
    /// </summary>
    [Serializable] 
    public class GetPlanFile_Ask:Command_Ask 
    {
        /// �춨��������
        /// <summary>
        /// �춨��������
        /// </summary>
        public string ProjectName = string.Empty;
        /// ��CTNP��������ʱ�����춨��������������
        /// <summary>
        /// ��CTNP��������ʱ�����춨��������������
        /// </summary>
        public List<object> CheckProject = new List<object>();
        /// <summary>
        /// 
        /// </summary>
        public GetPlanFile_Ask()
        {
            AskMessage = "���ͼ춨����";
        }

    }



}

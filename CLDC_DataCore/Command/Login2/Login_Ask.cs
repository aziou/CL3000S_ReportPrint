using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.Enum;
using CLDC_DataCore.Const;
namespace CLDC_DataCore.Command.Login2
{

    /// <summary>
    /// �����½
    /// </summary>
    [Serializable()]
    public class Login_Ask:Command_Ask 
    {
        //�����������ֶ�
        /// <summary>
        /// ̨�ӱ��
        /// </summary>
        public byte DeskNo = 0;		    
        /// <summary>
        /// ��λ��
        /// </summary>
        public int	PosCount = 0;
        /// <summary>
        /// ̨�����ͣ�0-����̨��1-����̨��
        /// </summary>
        public byte	DeskType  =0;		
        /// <summary>
        /// �Ƿ��Ǳ���
        /// </summary>
        public bool BeControl = false; 
        /// <summary>
        /// ̨������
        /// </summary>
        public string DeskName = "δ����";
        /// <summary>
        /// 
        /// </summary>
        public Login_Ask()
        {
            AskMessage = "�����½";
        }
    }
}

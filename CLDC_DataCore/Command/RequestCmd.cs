using System;
using System.Collections.Generic;
using System.Text;
using CLDC_CTNProtocol;
using CLDC_Comm.Enum;
using CLDC_DataCore.Const;
namespace CLDC_Comm.Command
{
    /// <summary>
    /// �������
    /// </summary>
    [Serializable()]
   public class RequestCmd:CTNPCommand
	{
			/// <summary>
        /// �����������ֶ�
			/// </summary>
			public bool RequestOK=false;		//�ɽ��նˣ����ﲻһ���ǿͻ��ˣ�������Ҳ�п��ܻᷢ�����󣩷���
	

	}

}

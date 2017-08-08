using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{


    /// <summary>
    /// ��Ϣ�������
    /// </summary>
    public enum  Cus_CommandType
    {
        /// <summary>
        /// ��������_�춨����
        /// </summary>0x10
        Cmd_Plan_DownPlan = 0x10 ,

        /// <summary>
        ///  Cmd_DownPlan ��Ϣ�Ļظ�
        /// </summary>
        Cmd_Plan_DownPlan_Answer = 0x11,

        /// <summary>
		/// �����½
		/// </summary>
		Login = 0x01 ,	

        /// <summary>
        /// �ļ�����
        /// </summary>
		DownFile	= 0x02 ,
		
	    /// <summary>
        /// ����¼������(����)
        /// </summary>
		Txm	= 0x03 ,

	    /// <summary>
        /// �ϴ������
        /// </summary>
		InPutTxm	= 0x04 ,

        /// <summary>
        /// ¼�����루���������
        /// </summary>
	     TxmOk 	= 0x05 , 

        /// <summary>
        /// ��ʼ�춨
        /// </summary>
        Start = 0x10,

	    /// <summary>
        /// ����ֹͣ�춨
        /// </summary>
        Stop = 0x11,

         /// <summary>
        /// �춨״̬
        /// </summary>
	   CheckState=0x12,

	   /// <summary>
        /// �춨���
        /// </summary>
	   UpdateCheck=0x13,

	   /// <summary>
        /// �ͻ���������ƣ�����״̬��
        /// </summary>
        Control = 0x30,

	    /// <summary>
        /// ��Ե��������
        /// </summary>
	    ServerControl=0x40,
	
	    /// <summary>
        /// ��Ե����
        /// </summary>
	   PtoPControl=0x41


    }

    

}

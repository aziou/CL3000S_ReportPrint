using System;
using System.Collections.Generic;
using System.Text;
using CLDC_CTNProtocol;
using CLDC_Comm.Enum;
using CLDC_DataCore.Const;
namespace CLDC_Comm.Command
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public class InPutTxm:CTNPCommand
    {
        //�����������ֶ�

        /// <summary>
        /// �����
        /// </summary>
        public string Txm = "";		//����������
        /// <summary>
        /// ��λ��.�±��0��ʼ
        /// </summary>
        public int Bwh = 0;			//��λ��,�±��0��ʼ
        /// <summary>
        /// �Ƿ������
        /// </summary>
        public bool TxmOk = false;	//�ɷ��������أ�ȷ�����������Ƿ�ɹ�
        /// <summary>
        /// ���캯��
        /// </summary>
        public InPutTxm()
        {
            //this.SubID = MessageID.CUS_SUB_REQUESTINPUTTXM;
            this.TxmOk = false;
            this.Txm = "";
        }
    }

}

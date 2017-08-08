using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.MessageArgs
{

    /// <summary>
    /// �춨��������Ϣ
    /// </summary>
    [Serializable()]
    public class EventMessageArgs:EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public bool RefreshData;        //�Ƿ���Ҫˢ������
        private string _Message;        //��ʾ��Ϣ
        private bool _VerifyOver;       //�춨���
        private CLDC_Comm.Enum.Cus_MessageType _MessageType;//��Ϣ����
        /// <summary>
        /// 
        /// </summary>
        public int ActiveItemID;        //���浱ǰActiveID
        /// <summary>
        /// 
        /// </summary>
        public EventMessageArgs()
            : base()
        {
            RefreshData = true;         //Ĭ��������Ҫˢ��
        }
        
        /// <summary>
        /// ����ʱ��Ϣ
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }

        }
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public CLDC_Comm.Enum.Cus_MessageType MessageType
        {
            get { return _MessageType; }
            set { _MessageType = (CLDC_Comm.Enum.Cus_MessageType)value; }
        }

        /// <summary>
        /// �춨�Ƿ����
        /// </summary>
        public virtual bool VerifyOver
        {
            set { _VerifyOver = value; }
            get { return _VerifyOver; }
        }
    }
}

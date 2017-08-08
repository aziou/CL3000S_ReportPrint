using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.MessageArgs
{
    /// <summary>
    /// Ԥ��
    /// </summary>
    /// 
    [Serializable()]
    public class EventProcessArgs:EventMessageArgs
    {
        private float _TotalMinute;//�ܹ���Ҫ����
        private float _PastMinute;//��ǰ�Ѿ�PASSʱ��
        private int _CurPos;//��ǰ����
        private string _OtherMessage;//������Ϣ
        /// <summary>
        /// 
        /// </summary>
        public EventProcessArgs()
            : base()
        {

        }


        /// <summary>
        /// ��Ҫ������ʱ��(��)
        /// </summary>
        public float TotalTime
        {
            set { _TotalMinute = value; }
            get { return _TotalMinute; }
        }
        /// <summary>
        /// ��ǰ�Ѿ�����ʱ��
        /// </summary>
        public float PastTime
        {
            set
            {
                _PastMinute = value;
                //���ý���
                if(_TotalMinute == 0)
                    _CurPos = 0;
                else
                {
                    _CurPos = (int)(_PastMinute / _TotalMinute) * 100;
                }
            }
            get { return _PastMinute; }
        }
        /// <summary>
        /// ��ǰ���Ȱٷ���
        /// </summary>
        public int Process
        {
            get
            {
                return _CurPos;
            }

        }
        /// <summary>
        /// ������Ϣ����Ҫ��Ӧ�õļ춨��Ŀȷ�����ݹ���.ĿǰԼ�����£�
        /// Ԥ�����飺������
        /// �����飺���������Ѿ��յ����壬����ֶμ�¼��һ���������ʱ��
        /// Ǳ�����飺ͬ������
        /// �������飺��׼�����/�����������
        /// �๦�����飺����
        /// </summary>
        public string OtherMessage
        {
            set { _OtherMessage = value; }
            get { return _OtherMessage; }
        }

        /// <summary>
        /// �춨���
        /// </summary>
        public override bool VerifyOver
        {
            get
            {
                return base.VerifyOver;
            }
            set
            {
                base.VerifyOver = value;
                if(value == true)
                {
                    PastTime = 0;
                }
            }
        }

    }
}

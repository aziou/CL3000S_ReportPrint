using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
    {
    /// <summary>
    /// ���ع���״̬ö��
    /// </summary>
    public enum Cus_SetStatus
        {
        /// <summary>
        /// ����״̬
        /// </summary>
            CUSINVALID=0,

        /// <summary>
        /// ����״̬
        /// </summary>
            CUSFREE=1,

        /// <summary>
        /// �Ѿ�����
        /// </summary>
        CUSCONNECT=2,

        /// <summary>
        /// ������
        /// </summary>
        CUSWORKING=3,

        /// <summary>
        /// ��Ŀ�춨���
        /// </summary>
        CUSCHECKOVER=4,

        /// <summary>
        /// �ѻ�����
        /// </summary>
        CUSCONTROL=5,

        /// <summary>
        /// �Ͽ�
        /// </summary>
        CUSCLOSED=6
        }
    }

using System;
using System.Collections.Generic;
using System.Text;
using CLDC_CTNProtocol;
using CLDC_Comm.Enum;

namespace CLDC_Comm.Command
{
    /// <summary>
    /// ״̬����,�Ѿ�Ԥ��MAINid:�ͻ���---->������;SUBID������״̬�������Ҫ��������;���޸�
    /// </summary>
    [Serializable()]
    public class CheckState : CTNPCommand
    {
        /// <summary>
        /// �����������ֶ�
        /// </summary>
        public MessageArgs.EventMessageArgs MessageArgs = null;
        /// <summary>
        /// ���浱ǰ�춨״̬
        /// </summary>
        public Cus_CheckStaute checkState = Cus_CheckStaute.δ��ֵ��;

    }

}

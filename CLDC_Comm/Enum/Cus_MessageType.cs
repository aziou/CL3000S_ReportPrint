using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// �춨����Ϣ��ʾ����
    /// ʹ��Bit���㣬һ����Ϣ�������ڶ�������
    /// </summary>
    [Flags]
    public enum Cus_MessageType : uint
    {
        /// <summary>
        /// ��ҪUI����Ϣ����ʾ���û�[����Ҫ����]
        /// </summary>
        ��ʾ��Ϣ = 1,
        /// <summary>
        /// �춨�������ǲ����г��ִ�����ʾ����Ϣ�����м춨ֹͣ
        /// </summary>
        ������Ϣ = 2,
        /// <summary>
        /// ����ʱ����ʾ��Ϣ���ṩ�������߲ο�
        /// </summary>
        ����ʱ��Ϣ = 4,
        /// <summary>
        /// ��Ŀ�춨��ı�,�����������
        /// </summary>
        �춨���� = 8,
        /// <summary>
        /// ������Ŀ�춨���
        /// </summary>
        �춨��� = 16,
        /// <summary>
        /// �ڲ���Ϣ:�����Ϣ����
        /// </summary>
        �����Ϣ���� = 32,
        /// <summary>
        /// �ڲ���Ϣ:������ݶ���
        /// </summary>
        ������ݶ��� = 64,
        /// <summary>
        /// �ֹ�¼�����
        /// </summary>
        ¼��������� = 128,
        /// <summary>
        /// �ֹ�¼�����ֹ��
        /// </summary>
        ¼�����ֹ�� = 256,
        /// <summary>
        /// ����Ϣ���Ͳ��ϱ���������
        /// </summary>
        �ͻ�����Ϣ = 512,
        /// <summary>
        /// ����Ϣֻ�ϱ���������
        /// </summary>
        ��������Ϣ = 1024,
        /// <summary>
        /// ���ر���Կ�忨
        /// </summary>
        ���ر���Կ�忨 = 2048,
        /// <summary>
        /// ����Ϣ��Ҫͨ�������ʶ�
        /// </summary>
        ������Ϣ = 0x40000000
    }
}

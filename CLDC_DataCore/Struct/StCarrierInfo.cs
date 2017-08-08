using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// �����������ز�Э����Ϣ
    /// ��    �ߣ�vs
    /// ��д���ڣ�2010-09-06
    /// �޸ļ�¼��
    ///         �޸����ڣ�		     
    ///         �޸�  �ˣ�������           
    ///         �޸����ݣ����·�ɱ�ʶ     
    ///
    /// </summary>
    [Serializable()]
    public struct StCarrierInfo
    {
        /// <summary>
        /// �ز�����
        /// </summary>
        public string CarrierName;
        /// <summary>
        /// ͨѶ����
        /// </summary>
        public string CarrierType;
        /// <summary>
        /// ����������
        /// </summary>
        public string RdType;
        /// <summary>
        /// ͨѶ��ʽ
        /// </summary>
        public string CommuType;        
        /// <summary>
        /// ������
        /// </summary>
        public string BaudRate;
        /// <summary>
        /// ͨѶ�˿�
        /// </summary>
        public string Comm;
        /// <summary>
        /// ������ʱ(ms)
        /// </summary>
        public string CmdTime;
        /// <summary>
        /// �ֽ���ʱ(ms)
        /// </summary>
        public string ByteTime;
        /// <summary>
        /// ·�ɱ�ʶ
        /// <para>0��ʾͨ��ģ���·�ɻ�����·��ģʽ��1��ʾͨ��ģ�鲻��·�ɻ�������·ģʽ��</para>
        /// </summary>
        public byte RouterID;

        /// <summary>
        /// �����ز��豸��Ϣ
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("�ز�����:{0} ͨѶ����:{1} ����������:{2} ͨѶ��ʽ:{3} ������:{4} ͨѶ�˿�:{5} ������ʱ(ms):{6} �ֽ���ʱ(ms):{7} ·�ɱ�ʶ:{8} ", CarrierName, CarrierType, RdType, CommuType, BaudRate, Comm, CmdTime, ByteTime, RouterID);
        }
    }
}

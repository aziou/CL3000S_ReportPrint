using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    ///   �ز��Ǳ�ĸ������
    /// </summary>
    public struct StCarrierMaster
    {
        /// <summary>
        /// ͨ��һ������[1-1200 2-2400 3-4800 4-9600 5-19200 6-34800 7-43000 8-56000 9-57600]
        /// </summary>
        public byte Chal1_BaudRate;
        /// <summary>
        ///  ͨ��һУ��λ[0-��У��λ 1-��У�� 2-żУ��]
        /// </summary>
        public byte Chal1_Parity;
        /// <summary>
        /// ͨ����������
        /// </summary>
        public byte Chal2_BaudRate;
        /// <summary>
        ///  ͨ����У��λ
        /// </summary>
        public byte Chal2_Parity;
        /// <summary>
        /// ͨ����������
        /// </summary>
        public byte Chal3_BaudRate;
        /// <summary>
        /// ͨ����У��λ
        /// </summary>
        public byte Chal3_Parity;
        /// <summary>
        /// ͨ���Ĳ�����
        /// </summary>
        public byte Chal4_BaudRate;
        /// <summary>
        /// ͨ����У��λ
        /// </summary>
        public byte Chal4_Parity;
        /// <summary>
        ///  ģ����
        /// </summary>
        public byte ModuleID;
        /// <summary>
        /// ��λ[0-�� 1-A�� 2-B�� 3-C��]
        /// </summary>
        public byte Phasic;
    }
}

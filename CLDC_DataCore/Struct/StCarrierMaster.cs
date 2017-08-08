using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    ///   载波仪表母板数据
    /// </summary>
    public struct StCarrierMaster
    {
        /// <summary>
        /// 通道一波特率[1-1200 2-2400 3-4800 4-9600 5-19200 6-34800 7-43000 8-56000 9-57600]
        /// </summary>
        public byte Chal1_BaudRate;
        /// <summary>
        ///  通道一校验位[0-无校验位 1-奇校验 2-偶校验]
        /// </summary>
        public byte Chal1_Parity;
        /// <summary>
        /// 通道二波特率
        /// </summary>
        public byte Chal2_BaudRate;
        /// <summary>
        ///  通道二校验位
        /// </summary>
        public byte Chal2_Parity;
        /// <summary>
        /// 通道三波特率
        /// </summary>
        public byte Chal3_BaudRate;
        /// <summary>
        /// 通道三校验位
        /// </summary>
        public byte Chal3_Parity;
        /// <summary>
        /// 通道四波特率
        /// </summary>
        public byte Chal4_BaudRate;
        /// <summary>
        /// 通道四校验位
        /// </summary>
        public byte Chal4_Parity;
        /// <summary>
        ///  模块编号
        /// </summary>
        public byte ModuleID;
        /// <summary>
        /// 相位[0-无 1-A相 2-B相 3-C相]
        /// </summary>
        public byte Phasic;
    }
}

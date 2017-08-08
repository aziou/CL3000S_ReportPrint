using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum EmEquipmentType
    {
        /// <summary>
        /// 标准表
        /// </summary>
        StdMeter = 1,
        /// <summary>
        /// 交流源
        /// </summary>
        Power = 2,
        /// <summary>
        /// 载波仪表
        /// </summary>
        Carrier = 3,
        /// <summary>
        /// PLC
        /// </summary>
        PLC = 4,
        /// <summary>
        /// 耐压仪
        /// </summary>
        NaiYa = 5,
        /// <summary>
        /// 时序板
        /// </summary>
        TimePanel = 6,
        /// <summary>
        /// 误差板
        /// </summary>
        ErrPanel = 7,
        /// <summary>
        /// 时基源
        /// </summary>
        Clock = 8,
        /// <summary>
        /// 电流采样板
        /// </summary>
        Sample = 9,
        /// <summary>
        /// 续流板
        /// </summary>
        SequelFlow = 10,
        /// <summary>
        /// 电压源
        /// </summary>
        VoltageSc = 11,
    }
}

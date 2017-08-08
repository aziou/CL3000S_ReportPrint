using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 电流的输出回路
    /// </summary>
    public enum Cus_BothIRoadType
    {
        /// <summary>
        /// 第一个电流回路
        /// </summary>
        第一个电流回路 = 0,
        /// <summary>
        /// 第二个电流回路
        /// </summary>
        第二个电流回路 = 1,
    }
    /// <summary>
    /// 电表电压回路选择
    /// </summary>
    public enum Cus_BothVRoadType
    {
        /// <summary>
        /// 直接接入式
        /// </summary>
        直接接入式 = 0,
        /// <summary>
        /// 互感器接入式
        /// </summary>
        互感器接入式 = 1,
        /// <summary>
        /// 本表位无电表接入
        /// </summary>
        本表位无电表接入=2
    }
}

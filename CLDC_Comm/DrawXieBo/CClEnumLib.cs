using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.DrawXieBo
{
    /// <summary>
    /// 测量方式
    /// </summary>
    public enum ClfsType
    {
        /// <summary>
        /// 
        /// </summary>
        三相四线有功 = 0,
        /// <summary>
        /// 
        /// </summary>
        三相四线无功 = 1,
        /// <summary>
        /// 
        /// </summary>
        三相三线有功 = 2,
        /// <summary>
        /// 
        /// </summary>
        三相三线无功 = 3,
        /// <summary>
        /// 
        /// </summary>
        二元件跨相90 = 4,
        /// <summary>
        /// 
        /// </summary>
        二元件跨相60 = 5,
        /// <summary>
        /// 
        /// </summary>
        三元件跨相90 = 6,
        /// <summary>
        /// 
        /// </summary>
        单相=7
    }

    /// <summary>
    /// 元件类型
    /// </summary>
    public enum ElementType
    {
        /// <summary>
        /// 
        /// </summary>
        H = 0,
        /// <summary>
        /// 
        /// </summary>
        A = 1,
        /// <summary>
        /// 
        /// </summary>
        B = 2,
        /// <summary>
        /// 
        /// </summary>
        C = 3
    }

    /// <summary>
    /// 坐标轴的位置类型
    /// </summary>
    public enum AxisType
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        LeftTop,
        /// <summary>
        /// 
        /// </summary>
        LeftMiddle,
        /// <summary>
        /// 
        /// </summary>
        LeftBottom,
        /// <summary>
        /// 
        /// </summary>
        MiddleTop,
        /// <summary>
        /// 
        /// </summary>
        Middle,
        /// <summary>
        /// 
        /// </summary>
        MiddleBottom,
        /// <summary>
        /// 
        /// </summary>
        RightTop,
        /// <summary>
        /// 
        /// </summary>
        RightMiddle,
        /// <summary>
        /// 
        /// </summary>
        RightBottom
    }

    /// <summary>
    /// 坐标方向
    /// </summary>
    public enum CoordinateType
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        Top,
        /// <summary>
        /// 
        /// </summary>
        Middle,
        /// <summary>
        /// 
        /// </summary>
        Bottom,
        /// <summary>
        /// 
        /// </summary>
        Left,
        /// <summary>
        /// 
        /// </summary>
        Right
    }
}

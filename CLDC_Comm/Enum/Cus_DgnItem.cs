using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 多功能检定项目枚举，注意！！！顺序不能变，只能往后续加.
    /// </summary>
    public enum Cus_DgnItem
    {
        /// <summary>
        /// 无效的项目
        /// </summary>
        无效的项目 = 000,
        /// <summary>
        /// 通信测试
        /// </summary>
        通信测试 = 001,
        /// <summary>
        /// 日计时误差
        /// </summary>
        日计时误差 = 002,
        /// <summary>
        /// 费率时段检查
        /// </summary>
        费率时段检查 = 003,
        /// <summary>
        /// 时段投切(默认正向有功)
        /// </summary>
        时段投切 = 004,
        /// <summary>
        /// 计度器示值组合误差
        /// </summary>
        计度器示值组合误差 = 005,
        /// <summary>
        /// 费率时段示值误差
        /// </summary>
        费率时段示值误差 = 006,
        /// <summary>
        /// GPS对时
        /// </summary>
        GPS对时 = 007,
        /// <summary>
        /// 闰年判断功能
        /// </summary>
        闰年判断功能 = 008,
        /// <summary>
        /// 事件记录检查
        /// </summary>
        事件记录检查 = 009,
        /// <summary>
        /// 需量清空
        /// </summary>
        需量清空 = 010,
        /// <summary>
        /// 电压逐渐变化
        /// </summary>
        电压逐渐变化 = 011,
        /// <summary>
        /// 电压跌落
        /// </summary>
        电压跌落 = 012,
        /// <summary>
        /// 时钟示值误差
        /// </summary>
        时钟示值误差 = 013,
        /// <summary>
        /// 最大需量01Ib
        /// </summary>
        最大需量01Ib = 014,
        /// <summary>
        /// 最大需量10Ib
        /// </summary>
        最大需量10Ib = 015,
        /// <summary>
        /// 最大需量Imax
        /// </summary>
        最大需量Imax = 016,
        /// <summary>
        /// 读取电量
        /// </summary>
        读取电量 = 017,
        /// <summary>
        /// 电量清零
        /// </summary>   
        电量清零 = 018,
        /// <summary>
        /// 电量寄存器检查
        /// </summary>
        电量寄存器检查 = 019,
        /// <summary>
        /// 需量寄存器检查
        /// </summary>
        需量寄存器检查 = 020,
        /// <summary>
        /// 瞬时寄存器检查
        /// </summary>
        瞬时寄存器检查 = 021,
        /// <summary>
        /// 状态寄存器检查
        /// </summary>
        状态寄存器检查 = 022,
        /// <summary>
        /// 失压寄存器检查
        /// </summary>
        失压寄存器检查 = 023,
        
        /// <summary>
        /// 校对电量
        /// </summary>
        校对电量 = 024,
        /// <summary>
        /// 校对需量
        /// </summary>
        校对需量 = 025,
        /// <summary>
        /// 检查电表运行状态
        /// </summary>
        检查电表运行状态 = 026,
        /// <summary>
        /// 预付费检测
        /// </summary>
        预付费检测 = 027,
        /// <summary>
        /// 反向有功时段投切
        /// </summary>
        反向有功时段投切 = 028,
        /// <summary>
        /// 正向无功时段投切
        /// </summary>
        正向无功时段投切 = 029,
        /// <summary>
        /// 反向无功时段投切
        /// </summary>
        反向无功时段投切 = 030,
        /// <summary>
        /// 反向有功计度器示值组合误差
        /// </summary>
        反向有功计度器示值组合误差 = 031,
        /// <summary>
        /// 正向无功计度器示值组合误差
        /// </summary>
        正向无功计度器示值组合误差 = 032,
        /// <summary>
        /// 反向无功计度器示值组合误差
        /// </summary>
        反向无功计度器示值组合误差 = 033,
        /// <summary>
        /// 反向有功费率时段示值误差
        /// </summary>
        反向有功费率时段示值误差 = 034,
        /// <summary>
        /// 正向无功费率时段示值误差
        /// </summary>
        正向无功费率时段示值误差 = 035,
        /// <summary>
        /// 反向无功费率时段示值误差
        /// </summary>
        反向无功费率时段示值误差 = 036,
        /// <summary>
        /// 电压短时终端试验
        /// </summary>
        电压短时中断 = 037,
        /// <summary>
        /// 修改密码
        /// </summary>
        修改密码 = 038,
    }
}

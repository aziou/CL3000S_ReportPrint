using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 费控功能试验检定项目枚举
    /// </summary>
    public enum Cus_CostControlItem
    {
        /// <summary>
        /// 无效的项目
        /// </summary>
        无效的项目=000,
        /// <summary>
        /// 身份认证
        /// </summary>
        身份认证 = 001,
        /// <summary>
        /// 远程控制
        /// </summary>
        远程控制 = 002,
        /// <summary>
        /// 报警功能
        /// </summary>
        报警功能 = 003,
        /// <summary>
        /// 保电功能
        /// </summary>
        保电功能 = 004,
        /// <summary>
        /// 保电解除
        /// </summary>
        保电解除 = 005,
        /// <summary>
        /// 远程保电
        /// </summary>
        远程保电 = 006,
        /// <summary>
        /// 数据回抄
        /// </summary>  
        数据回抄 = 007,
        /// <summary>
        /// 参数设置
        /// </summary>
        参数设置 = 008,
        /// <summary>
        /// 剩余电量递减准确度
        /// </summary>
        剩余电量递减准确度 = 009,
        /// <summary>
        /// 电价切换
        /// </summary>
        电价切换 = 010,
        /// <summary>
        /// 负荷开关
        /// </summary>
        负荷开关 = 011,
        /// <summary>
        /// 电量清零
        /// </summary>
        电量清零 = 012,
        /// <summary>
        /// 密钥更新
        /// </summary>
        密钥更新 = 013,
        /// <summary>
        /// 密钥恢复,特殊权限
        /// </summary>
        密钥恢复 = 014,
        /// <summary>
        /// 控制功能
        /// </summary>
        控制功能=015,
        /// <summary>
        /// 阶梯电价
        /// </summary>
        阶梯电价检测 = 016,
        /// <summary>
        /// 费率电价
        /// </summary>
        费率电价检测 = 017,
        /// <summary>
        /// 远程拉闸后发送直接合闸
        /// </summary>
        远程控制直接合闸 = 018,
        /// <summary>
        /// 钱包初始化
        /// </summary>
        钱包初始化 = 019,
        /// <summary>
        /// 预置内容检查 @C_B
        /// </summary>
        预置内容检查 = 021,
        /// <summary>
        /// 预置内容设置 @C_B
        /// </summary>
        预置内容设置 = 022,
        /// <summary>
        /// 本地——远程
        /// </summary>
        本地模式切换远程模式 =023,
        /// <summary>
        /// 远程——本地
        /// </summary>
        远程模式切换本地模式 =024,
        /// <summary>
        /// 用户卡开户
        /// </summary>
        用户卡开户 = 025,
        /// <summary>
        /// 透支
        /// </summary>
        透支功能 = 026

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// <summary>
    /// 检定器消息提示类型
    /// 使用Bit运算，一个消息可以属于多种类型
    /// </summary>
    [Flags]
    public enum Cus_MessageType : uint
    {
        /// <summary>
        /// 需要UI用消息框提示给用户[不需要交互]
        /// </summary>
        提示消息 = 1,
        /// <summary>
        /// 检定方案或是操作中出现错误。提示此消息后所有检定停止
        /// </summary>
        错误消息 = 2,
        /// <summary>
        /// 运行时的提示信息。提供给操作者参考
        /// </summary>
        运行时消息 = 4,
        /// <summary>
        /// 项目检定点改变,报告给服务器
        /// </summary>
        检定跳点 = 8,
        /// <summary>
        /// 所有项目检定完毕
        /// </summary>
        检定完毕 = 16,
        /// <summary>
        /// 内部消息:清空消息队列
        /// </summary>
        清空消息队列 = 32,
        /// <summary>
        /// 内部消息:清空数据队列
        /// </summary>
        清空数据队列 = 64,
        /// <summary>
        /// 手工录入电量
        /// </summary>
        录入电量起码 = 128,
        /// <summary>
        /// 手工录入电量止码
        /// </summary>
        录入电量止码 = 256,
        /// <summary>
        /// 此消息类型不上报到服务器
        /// </summary>
        客户端消息 = 512,
        /// <summary>
        /// 此消息只上报到服务器
        /// </summary>
        服务器消息 = 1024,
        /// <summary>
        /// 本地表密钥插卡
        /// </summary>
        本地表密钥插卡 = 2048,
        /// <summary>
        /// 本消息需要通过语音朗读
        /// </summary>
        语音消息 = 0x40000000
    }
}

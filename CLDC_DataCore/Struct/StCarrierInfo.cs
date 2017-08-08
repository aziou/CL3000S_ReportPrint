using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 功能描述：载波协议信息
    /// 作    者：vs
    /// 编写日期：2010-09-06
    /// 修改记录：
    ///         修改日期：		     
    ///         修改  人：樊江凯           
    ///         修改内容：添加路由标识     
    ///
    /// </summary>
    [Serializable()]
    public struct StCarrierInfo
    {
        /// <summary>
        /// 载波名称
        /// </summary>
        public string CarrierName;
        /// <summary>
        /// 通讯介质
        /// </summary>
        public string CarrierType;
        /// <summary>
        /// 抄表器类型
        /// </summary>
        public string RdType;
        /// <summary>
        /// 通讯方式
        /// </summary>
        public string CommuType;        
        /// <summary>
        /// 波特率
        /// </summary>
        public string BaudRate;
        /// <summary>
        /// 通讯端口
        /// </summary>
        public string Comm;
        /// <summary>
        /// 命令延时(ms)
        /// </summary>
        public string CmdTime;
        /// <summary>
        /// 字节延时(ms)
        /// </summary>
        public string ByteTime;
        /// <summary>
        /// 路由标识
        /// <para>0表示通信模块带路由或工作在路由模式，1表示通信模块不带路由或工作在旁路模式。</para>
        /// </summary>
        public byte RouterID;

        /// <summary>
        /// 返回载波设备信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("载波名称:{0} 通讯介质:{1} 抄表器类型:{2} 通讯方式:{3} 波特率:{4} 通讯端口:{5} 命令延时(ms):{6} 字节延时(ms):{7} 路由标识:{8} ", CarrierName, CarrierType, RdType, CommuType, BaudRate, Comm, CmdTime, ByteTime, RouterID);
        }
    }
}

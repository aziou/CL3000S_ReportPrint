using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.SocketModule.Packet
{
    /// <summary>
    /// 发送数据包基类
    /// </summary>
    public abstract class SendPacket : Packet
    {
        /// <summary>
        /// 是否需要回复
        /// </summary>
        public bool IsNeedReturn { get; set; }

        /// <summary>
        /// 获取数据包二进制内容
        /// </summary>
        /// <returns></returns>
        public abstract byte[] GetPacketData();

        
    }
}

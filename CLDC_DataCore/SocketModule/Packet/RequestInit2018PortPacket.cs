using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.SocketModule.Packet;

namespace CLDC_Comm.SocketModule.Packet
{
    /// <summary>
    /// 初始化2018数据包
    /// </summary>
    public class RequestInit2018PortPacket : SendPacket
    {
        private string m_strSetting = "";
        public RequestInit2018PortPacket(string strSetting)
        {
            m_strSetting = strSetting;
        }

        public override byte[] GetPacketData()
        {
            ByteBuffer buf = new ByteBuffer();
            buf.Initialize();
            string str_Data = "init " + m_strSetting.Replace(',', ' ');
            byte[] byt_Data = ASCIIEncoding.ASCII.GetBytes(str_Data);
            buf.Put(byt_Data);
            return buf.ToByteArray();
        }

    }
}

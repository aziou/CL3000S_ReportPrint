using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// 
    /// </summary>
    public class Net
    {

        /// <summary>
        /// 根据主机名字获取对应的IP、获取失败返回 string.Empty
        /// </summary>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public static string GetIpByHostName(string hostname)
        {
            IPHostEntry entry = Dns.GetHostEntry(hostname);
            if (entry.AddressList.Length > 0 )
                return entry.AddressList[entry.AddressList.Length - 1].ToString();
            return string.Empty;
        }


        /// <summary>
        /// 验证是否是IP
        /// </summary>
        /// <param name="strIP">要验证的IP地址</param>
        /// <returns>是Y否N</returns>
        public static bool IsIP(string strIP)
        {
            try
            {
                string strRegPattern = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
                bool _IsIp = System.Text.RegularExpressions.Regex.IsMatch(strIP, strRegPattern);
                return _IsIp;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 获取本机MAC地址
        /// </summary>
        /// <param name="MacString">MAC字符串</param>
        /// <returns>MAC值</returns>
        public static long GetMac(out string MacString)
        {
            string macAddress = "";
            long mac = 0;
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (!adapter.GetPhysicalAddress().ToString().Equals(""))
                    {
                        macAddress = adapter.GetPhysicalAddress().ToString();
                        mac=Convert.ToInt64(macAddress, 16);
                        for (int i = 1; i < 6; i++)
                        {
                            macAddress = macAddress.Insert(3 * i - 1, ":");
                        }
                        break;
                    }
                }

            }
            catch
            {
            }
            MacString = macAddress;
            return mac;
        }

    }
}

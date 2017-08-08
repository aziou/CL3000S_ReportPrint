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
        /// �����������ֻ�ȡ��Ӧ��IP����ȡʧ�ܷ��� string.Empty
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
        /// ��֤�Ƿ���IP
        /// </summary>
        /// <param name="strIP">Ҫ��֤��IP��ַ</param>
        /// <returns>��Y��N</returns>
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
        /// ��ȡ����MAC��ַ
        /// </summary>
        /// <param name="MacString">MAC�ַ���</param>
        /// <returns>MACֵ</returns>
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

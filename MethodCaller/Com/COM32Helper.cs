using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO.Ports;

namespace MethodCaller
{
    class COM32Helper
    {
        public static string[] GetVirtualPorts()
        {
            string[] ss = SerialPort.GetPortNames();

            //List<string> ports = new List<string>();
            //RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM", false);
            //foreach (string keyName in rk.GetValueNames())
            //{
            //    if (keyName.StartsWith(@"\Device\com0com"))
            //    {
            //        ports.Add(rk.GetValue(keyName).ToString());
            //    }
            //}
            //ports.Sort();
            //return ports.ToArray();
            return ss;
        }
    }
}

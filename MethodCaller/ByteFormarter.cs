using System;
using System.Collections.Generic;
using System.Text;

namespace MethodCaller
{
    class ByteFormarter
    {
        public static string Formart(byte[] value, int offset, int len)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < len; i++)
            {
                sb.Append(value[i + offset].ToString("X2"));
                sb.Append(" ");
            }
            sb.Append("[len:"+value.Length+"]");
            sb.AppendLine();
            return sb.ToString();
        }

        public static string Formart(byte[] value)
        {
            return Formart(value, 0, value.Length);
        }
    }
}

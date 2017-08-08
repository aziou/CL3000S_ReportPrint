using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Exceptions
{
    /// <summary>
    /// 设备控制异常
    /// 系统在控制设备失败时会抛出此异常。
    /// </summary>
    public class EquipmentControlException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public EquipmentControlException(string message)
            : base(message)
        {
            //TODO DoSomeThne
        }
        /// <summary>
        /// 获取控制失败消息
        /// </summary>
        public override string Message
        {
            get
            {
                return string.Format("控制设备失败:{0}", base.Message);
            }
        }
    }
}

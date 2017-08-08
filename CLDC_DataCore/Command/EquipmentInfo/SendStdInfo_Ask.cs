using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
namespace CLDC_DataCore.Command.EquipmentInfo
{
    /// <summary>
    /// 台体设置信息
    /// </summary>
    [Serializable()]
    public class SendStdInfo_Ask : Command_Ask
    {
        /// <summary>
        /// 理论值
        /// </summary>
        public StPower TheoryPower;             //源信息

        /// <summary>
        /// 实际值
        /// </summary>
        public StPower RealPower;

        /// <summary>
        /// 是否是升源，
        /// true 升源,
        /// false 降源
        /// </summary>
        public bool isPowerOn;

        /// <summary>
        /// 升源或降源时间
        /// </summary>
        public DateTime lastPowerOnTime;

        /// <summary>
        /// 
        /// </summary>
        public SendStdInfo_Ask()
        {
            AskMessage = "电源信息";
        }
    }
}

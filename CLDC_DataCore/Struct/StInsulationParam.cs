using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 电能表耐压测试相关参数的数据模型
    /// </summary>
    [Serializable()]
    public class StInsulationParam
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public StInsulationParam()
        {
            Voltage = 2000;
            Time = 60;
            Current = 1;
            CurrentDevice = 1;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="voltage">耐压值</param>
        /// <param name="time">耐压时间</param>
        /// <param name="current">漏电流</param>
        public StInsulationParam(int voltage,int time,int current)
        {
            Voltage = voltage;
            Time = time;
            Current = current;
        }

        /// <summary>
        /// 功耗项目ID
        /// </summary>
        public string InsulationPrjID
    {
        get
        {
            return String.Format("{0}{1}"                                          //Key:参见数据结构设计附2
                    , ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.工频耐压试验).ToString()
                    , (int)InsulationType);
        }
    }
        /// <summary>
        /// 耐压试验类型：
        /// </summary>
        public EnumInsulationType InsulationType { get; set; }

        /// <summary>
        /// 耐压测试时间
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// 耐压测试电压
        /// </summary>
        public int Voltage { get; set; }

        /// <summary>
        /// 表位漏电流
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// 耐压机总漏电流
        /// </summary>
        public int CurrentDevice { get; set; }

        /// <summary>
        /// 耐压类型枚举
        /// </summary>
        public enum EnumInsulationType
        {
            /// <summary>
            /// 辅助端子对地耐压  (更改电压电流对地)
            /// </summary>
            DigitalEarth,
            /// <summary>
            /// 模拟量端子对地耐压  (更改电压对电流)
            /// </summary>
            AnalogEarth,
            /// <summary>
            /// 辅助端子对模拟量端子耐压
            /// </summary>
            DigitalAnalog
        }

        /// <summary>
        /// 重写方法，获取耐压试验名
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (InsulationType == EnumInsulationType.DigitalEarth)
                return "电压电流对地耐压";//"辅助端子对外壳耐压";
            else if (InsulationType == EnumInsulationType.AnalogEarth)
                return "电压对电流耐压";//"模拟量端子对外壳耐压";
            else
                return "端子间耐压";
        }
    }
}

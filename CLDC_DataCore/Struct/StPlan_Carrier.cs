using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 功能描述：载波项目结构
    /// 作    者：vs
    /// 编写日期：2010-09-06
    /// 修改记录：
    ///         修改日期		     修改人	            修改内容
    ///
    /// </summary>
    [Serializable()]
    public struct StPlan_Carrier
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public string str_PrjID
        {
            get
            {
                return String.Format("{0}{1}{2}"                                          //Key:参见数据结构设计附2
                          , (int)Cus_MeterResultPrjID.载波试验
                          , str_Code
                          , str_Type);
            }
            set { }
        }
        /// <summary>
        /// 载波试验类型
        /// </summary>
        public string str_Type;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string str_Name;
        /// <summary>
        /// 标识符
        /// </summary>
        public string str_Code;
        /// <summary>
        /// 发送次数
        /// </summary>
        public int int_Times;
        /// <summary>
        /// 成功率
        /// </summary>
        public float flt_Success;
        /// <summary>
        /// 间隔时间(分)
        /// </summary>
        public int int_Interval;
        /// <summary>
        /// 载波模块互换
        /// </summary>
        public bool b_ModuleSwaps;
        /// <summary>
        /// 源输出参数
        /// </summary>
        public StPowerPramerter OutPramerter;

        /// <summary>
        /// 重写ToString（）方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("载波|{0}|{1}",  str_Name, str_Code);
        }
    }
}

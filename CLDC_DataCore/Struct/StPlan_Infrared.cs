using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 功能描述：红外数据比对项目结构
    /// 作    者：zzg
    /// 编写日期：2014-05-07
    /// 修改记录：
    ///         修改日期		     修改人	            修改内容
    ///
    /// </summary>
    [Serializable()]
    public struct StPlan_Infrared
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public string str_PrjID
        {
            get
            {
                return String.Format("{0}{1}"                                          //Key:参见数据结构设计附2
                        , (int)Cus_MeterResultPrjID.红外数据比对试验
                        , str_Code);
            }
            set { }
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string str_Name;
        /// <summary>
        /// 标识符
        /// </summary>
        public string str_Code;        
        
        /// <summary>
        /// 重写ToString（）方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("红外|{0}|{1}",  str_Name, str_Code);
        }
    }
}

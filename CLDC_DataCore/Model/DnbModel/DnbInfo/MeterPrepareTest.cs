using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    ///<summary>
    /// FileName:MeterPrepareTest.cs
    /// machinename:2014-0325-1259
    /// Author:kaury
    /// DateTime:2014/6/24 11:14:44
    /// Corporation:
    /// Description:
    ///</summary>
    [Serializable()]
    public class MeterPrepareTest : MeterErrorBase
    {
        /// <summary>
        /// 项目ID	
        /// </summary>
        public string Md_PrjID = "";
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Md_PrjName = "";
        /// <summary>
        /// 项目值		描述
        /// </summary>
        public string Md_chrValue = "";

        /// <summary>
        /// 6结论Y/N
        /// </summary>
        public string AVR_CONCLUSION { get; set; }


    }
}

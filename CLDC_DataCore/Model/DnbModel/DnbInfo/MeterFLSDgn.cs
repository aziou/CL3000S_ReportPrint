using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 费率时段功能
    /// </summary>
    [Serializable()]
    public class MeterFLSDgn : MeterErrorBase
    {
        
        /// <summary>
        /// 项目名称	
        /// </summary>
        public string Mfl_chrProjectName = "";
        /// <summary>
        /// 组别
        /// </summary>
        public string Mfl_chrGrpType = "";
        /// <summary>
        /// 项目号
        /// </summary>
        public string Mfl_chrListNo = "";
        /// <summary>
        /// 小项目号	
        /// </summary>
        public string Mfl_intItemType = "";
        /// <summary>
        /// 分项结论
        /// </summary>
        public string Mfl_chrItemJL = "";
        /// <summary>
        /// 时区状态字结论
        /// </summary>
        public string Mfl_chrSqZtzJL = "";
        /// <summary>
        /// 时区状态字数据	
        /// </summary>
        public string Mfl_chrSqZtzDat = "";
        /// <summary>
        /// 时区数据结论
        /// </summary>
        public string Mfl_chrSqDatJL = "";
        /// <summary>
        /// 写入时区
        /// </summary>
        public string Mfl_chrSqWdat = "";
        /// <summary>
        /// 读取时区
        /// </summary>
        public string Mfl_chrSqRdat = "";
        /// <summary>
        /// 时区编程结论
        /// </summary>
        public string Mfl_chrSqBcJL = "";
        /// <summary>
        /// 时区编程数据	
        /// </summary>
        public string Mfl_chrSqBcDat = "";
        /// <summary>
        /// 时区约定冻结结论
        /// </summary>
        public string Mfl_chrSqYddjJL = "";
        /// <summary>
        /// 时区约定冻结数据
        /// </summary>
        public string Mfl_chrSqYddjDat = "";
        /// <summary>
        /// 时段状态字结论
        /// </summary>
        public string Mfl_chrSdZtzJL = "";
        /// <summary>
        /// 时段状态字数据
        /// </summary>
        public string Mfl_chrSdZtzDat = "";
        /// <summary>
        /// 时段数据结论	
        /// </summary>
        public string Mfl_chrSdDatJL = "";
        /// <summary>
        /// 写入时段
        /// </summary>
        public string Mfl_chrSdWdat = "";
        /// <summary>
        /// 读取时段
        /// </summary>
        public string Mfl_chrSdRdat = "";
        /// <summary>
        /// 时段编程结论
        /// </summary>
        public string Mfl_chrSdBcJL = "";
        /// <summary>
        /// 时段编程数据
        /// </summary>
        public string Mfl_chrSdBcDat = "";
        /// <summary>
        /// 时段约定冻结结论	
        /// </summary>
        public string Mfl_chrSdYddjJL = "";
        /// <summary>
        /// 时段约定冻结数据
        /// </summary>
        public string Mfl_chrSdYddjDat = "";
        /// <summary>
        /// 周休日编程结论
        /// </summary>
        public string Mfl_chrZxrBcJL = "";        
        /// <summary>
        /// 周休日编程数据
        /// </summary>
        public string Mfl_chrZxrDat = "";
        /// <summary>
        /// 节假日编程结论
        /// </summary>
        public string Mfl_chrJjrBcJL = "";
        /// <summary>
        /// 节假日编程数据
        /// </summary>
        public string Mfl_chrJjrDat = "";
        /// <summary>
        /// 时段切换电量结论
        /// </summary>
        public string Mfl_chrSdDlQhJL = "";
        /// <summary>
        /// 尖时段电量数据	
        /// </summary>
        public string Mfl_chrJdlDat = "";
        /// <summary>
        /// 峰时段电量数据
        /// </summary>
        public string Mfl_chrFdlDat = "";
        /// <summary>
        /// 平时段电量数据
        /// </summary>
        public string Mfl_chrPdlDat = "";

        /// <summary>
        /// 谷时段电量数据
        /// </summary>
        public string Mfl_chrGdlDat = "";
        /// <summary>
        /// 时段切换脉冲结论
        /// </summary>
        public string Mfl_chrSdMCQhJL = "";
        /// <summary>
        /// 尖时段脉冲数据
        /// </summary>
        public string Mfl_chrJmcDat = "";
        /// <summary>
        /// 峰时段脉冲数据
        /// </summary>
        public string Mfl_chrFmcDat = "";
        /// <summary>
        /// 平时段脉冲数据	
        /// </summary>
        public string Mfl_chrPmcDat = "";
        /// <summary>
        /// 谷时段脉冲数据
        /// </summary>
        public string Mfl_chrGmcDat = "";
        /// <summary>
        /// 备份1
        /// </summary>
        public string Mfl_chrOther1 = "";
        /// <summary>
        /// 备份2
        /// </summary>
        public string Mfl_chrOther2 = "";
        /// <summary>
        /// 备份3
        /// </summary>
        public string Mfl_chrOther3 = "";
        /// <summary>
        /// 备份4
        /// </summary>
        public string Mfl_chrOther4 = "";
        /// <summary>
        /// 备份5
        /// </summary>
        public string Mfl_chrOther5 = "";
        /// <summary>
        /// 其他数据
        /// </summary>
        public string Mfl_chrRecordOther = "";
    }
}

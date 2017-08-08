using System;
using System.Collections.Generic;
using System.Text;
namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 事件记录
    /// </summary>
    [Serializable()]
    public class MeterSjJLgn : MeterErrorBase
    {
        /// <summary>
        /// 4.组类别
        /// </summary>
        public string GroupType;
        /// <summary>
        /// 5.排序号
        /// </summary>
        public string ListNo;
        /// <summary>
        /// 6.项目名：失压，断相
        /// </summary>
        public string ItemName;
        /// <summary>
        /// 7.工况点编号
        /// </summary>
        public string StatusNo;
        /// <summary>
        /// 8.分项目名称
        /// </summary>
        public string SubItemName;
        /// <summary>
        /// 9.开始运行工况时间
        /// </summary>
        public string TestStartTime;
        /// <summary>
        /// 10.工况结束时间
        /// </summary>
        public string TestEndTime;
        /// <summary>
        /// 11.发生总次数
        /// </summary>
        public string SumTimes;
        /// <summary>
        /// 12.总累计时间
        /// </summary>
        public string UseTime;
        /// <summary>
        /// 13.发生时刻（仅有发生时刻的事件或最近一次发生时刻）
        /// </summary>
        public string RecordStartTime;
        /// <summary>
        /// 14.结束时刻
        /// </summary>
        public string RecordEndTime;
        /// <summary>
        /// 15.此工况点结论
        /// </summary>
        public string SubItemConc;
        /// <summary>
        /// 16.结论Y/N
        /// </summary>
        public string ItemConc;
        /// <summary>
        /// 17.A相总次数
        /// </summary>
        public string ASumTimes;
        /// <summary>
        /// 18.A相总累计时间
        /// </summary>
        public string AUseTime;
        /// <summary>
        /// 19.B相总次数
        /// </summary>
        public string BSumTimes;
        /// <summary>
        /// 20.B相总累计时间
        /// </summary>
        public string BUseTime;
        /// <summary>
        /// 21.C相总次数
        /// </summary>
        public string CSumTimes;
        /// <summary>
        /// 22.C相总累计时间
        /// </summary>
        public string CUseTime;
        /// <summary>
        /// 23.上N次记录【1-N】
        /// </summary>
        public string RecordNo;
         /// <summary>
        /// 24.A相发生时刻
        /// </summary>
        public string ARecordStartTime;
        /// <summary>
        /// 25.A相结束时间
        /// </summary>
        public string ARecordEndTime;
        /// <summary>
        /// 26.A相发生时刻数据（不包括发生时刻）
        /// </summary>
        public string ARecordStartData;
        /// <summary>
        /// 27.A相结束时刻数据（不包括结束时刻）
        /// </summary>
        public string ARecordEndData;
        /// <summary>
        /// 28.A相事件期间数据（645-2007 中数据（增量）跟备案文件（发生、结束）不同）
        /// </summary>
        public string ARecordingData;
        /// <summary>
        /// 29.B相发生时刻
        /// </summary>
        public string BRecordStartTime;
        /// <summary>
        /// 30.B相结束时间
        /// </summary>
        public string BRecordEndTime;
        /// <summary>
        /// 31.B相发生时刻数据（不包括发生时刻）
        /// </summary>
        public string BRecordStartData;
        /// <summary>
        /// 32.B相结束时刻数据（不包括结束时刻）
        /// </summary>
        public string BRecordEndData;
        /// <summary>
        /// 33.B相事件期间数据（645-2007 中数据（增量）跟备案文件（发生、结束）不同）
        /// </summary>
        public string BRecordingData;
        /// <summary>
        /// 34.C相发生时刻
        /// </summary>
        public string CRecordStartTime;
        /// <summary>
        /// 35.C相结束时间
        /// </summary>
        public string CRecordEndTime;
        /// <summary>
        /// 36.C相发生时刻数据（不包括发生时刻）
        /// </summary>
        public string CRecordStartData;
        /// <summary>
        /// 37.C相结束时刻数据（不包括结束时刻）
        /// </summary>
        public string CRecordEndData;
        /// <summary>
        /// 38.C相事件期间数据（645-2007 中数据（增量）跟备案文件（发生、结束）不同）
        /// </summary>
        public string CRecordingData;
        /// <summary>
        /// 39.操作者代码
        /// </summary>
        public string UserCode;
        /// <summary>
        /// 40.操作标识
        /// </summary>
        public string DICode;
        /// <summary>
        /// 41.其他记录数据
        /// </summary>
        public string RecordOther;
        /// <summary>
        /// 42.标准最大不平衡率
        /// </summary>
        public string ImbalanceRatio;
        /// <summary>
        /// 43.方案编号
        /// </summary>
        public long SchemeID;
        /// <summary>
        /// 44.不合格原因
        /// </summary>
        public string DisReasion;
        /// <summary>
        /// 报表打印数据--结论|总次数|总累计时间|最近一次发生时间
        /// </summary>
        public string PrintData
        {
            get
            {
                string value = "";
                if (GroupType.Length == 3)
                {
                    value += ItemConc;
                    value += "|" + SumTimes;
                    value += "|" + UseTime;
                    value += "|" + RecordStartTime;
                }
                return value;
            }
        }
    }
}
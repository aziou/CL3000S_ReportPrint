using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 数据操作类型
    /// </summary>
    [Serializable()]
    public enum StMeterOperType
    {
        /// <summary>
        /// 
        /// </summary>
        读=0,
        /// <summary>
        /// 
        /// </summary>
        写=1
    }

    /// <summary>
    /// 通讯协议检查试验
    /// </summary>
    [Serializable()]
    public struct StPlan_ConnProtocol
    {

        private string _PrjID;
        /// <summary>
        /// 项目编号
        /// </summary>
        public string PrjID
        {
            get
            {
                return _PrjID;
            }
            set
            {
                _PrjID = value;
            }
        }

        /// <summary>
        /// 数据项名称
        /// </summary>
        public string ConnProtocolItem;

        /// <summary>
        /// 标识编码
        /// </summary>
        public string ItemCode;

        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataLen;

        /// <summary>
        /// 小数位索引
        /// </summary>
        public int PointIndex;

        /// <summary>
        /// 数据格式
        /// </summary>
        public string StrDataType;

        /// <summary>
        /// 操作类型,读/写
        /// </summary>
        public StMeterOperType OperType;

        /// <summary>
        /// 写入内容
        /// </summary>
        public string WriteContent;


        /// <summary>
        /// 通讯协议检查试验项目描述
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (ConnProtocolItem == null) ConnProtocolItem = "";
            return string.Format("通讯协议检查试验：({0}){1}", OperType == StMeterOperType.读 ? "读" : "写", ConnProtocolItem.ToString());
        }
    }
}

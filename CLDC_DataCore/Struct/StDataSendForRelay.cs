using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    [Serializable()]
    public class StDataSendForRelay
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
        /// 表位号
        /// </summary>
        public string MeterPosition;

        /// <summary>
        /// 条形码
        /// </summary>
        public string BarCode;


        /// <summary>
        /// 数据项名称
        /// </summary>
        public string ConnProtocolItem;

        /// <summary>
        /// 标识编码
        /// </summary>
        public string ItemCode;

        /// <summary>
        /// 参数值
        /// </summary>
        public string PARAMS_LIST;

        /// <summary>
        /// 通讯规约
        /// </summary>
        public string PROTOCOL;

     


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
            return string.Format("数据转发({0})：{1}", PARAMS_LIST, ConnProtocolItem.ToString());
        }
    }
}




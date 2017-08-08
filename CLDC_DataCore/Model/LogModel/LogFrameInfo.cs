using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.LogModel
{
    /// <summary>
    /// 帧日志，参见《数据库结构设计文档_20130615.doc》
    /// </summary>
    [Serializable()]
    public class LogFrameInfo
    {
        /// <summary>
        /// 端口号，格式：port_comX_2018-1/5_BTL
        /// </summary>
        public string strPortNo = "";
        /// <summary>
        /// 设备名、表位等
        /// </summary>
        public string strEquipName = "";
        /// <summary>
        /// 项目名称
        /// </summary>
        public string strItemName = "";
        /// <summary>
        /// 操作消息
        /// </summary>
        public string strMessage = "";
        /// <summary>
        /// 发帧结构
        /// </summary>
        public Struct.StSRFrame sendFrm = new CLDC_DataCore.Struct.StSRFrame();
        /// <summary>
        /// 收帧结构
        /// </summary>
        public Struct.StSRFrame ResvFrm = new CLDC_DataCore.Struct.StSRFrame();
        /// <summary>
        /// 备用
        /// </summary>
        public string strOther = "";
        /// <summary>
        /// 以英文逗号分隔的串,带单引号
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("'{0}','{1}','{2}','{3}',{4},{5},'{6}'", strPortNo, strEquipName, strItemName, strMessage, sendFrm.ToString(), ResvFrm.ToString(), strOther);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getStrSQL()
        {
            return string.Format("insert into FrameLog values({0})", this.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        public string chrPortNo { get; set; } //端口号

        /// <summary>
        /// 
        /// </summary>
        public string chrEquipName { get; set; } //设备名

        /// <summary>
        /// 
        /// </summary>
        public string chrItemName { get; set; }  //项目名称

        /// <summary>
        /// 
        /// </summary>
        public string chrMessage { get; set; }  //操作消息

        /// <summary>
        /// 
        /// </summary>
        public string chrSFrame { get; set; }   //发帧字符串

        /// <summary>
        /// 
        /// </summary>
        public string chrSMeaning { get; set; } //发解析

        /// <summary>
        /// 
        /// </summary>
        public string chrSTime { get; set; }    //发时间

        /// <summary>
        /// 
        /// </summary>
        public string chrRFrame { get; set; }   //收帧字符串

        /// <summary>
        /// 
        /// </summary>
        public string chrRMeaning { get; set; } //收解析

        /// <summary>
        /// 
        /// </summary>
        public string chrRTime { get; set; }    //收时间

        /// <summary>
        /// 
        /// </summary>
        public string chrOther { get; set; }    //备用

    }
}

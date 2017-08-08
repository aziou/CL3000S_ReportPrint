using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace CLDC_DataCore.Model.CarrierProtocol
{
    /// <summary>
    /// 功能描述：载波通信协议配置模型
    /// 作    者：vs
    /// 编写日期：2010-09-03
    /// 修改记录：
    ///         修改日期		     修改人	            修改内容
    ///
    /// </summary>
    [Serializable()]
    public class CarrierProtocolInfo
    {
        #region--------------私有变量-----------------
        /// <summary>
        /// 协议名称
        /// </summary>
        private string m_str_ProtocolName = "";

        ///// <summary>
        ///// 通讯介质
        ///// </summary>
        private string m_str_CarrierType = "";

        /// <summary>
        /// 抄表器类型
        /// </summary>
        private string m_str_ReadType = "";


        /// <summary>
        /// 通讯方式
        /// </summary>
        private string m_str_CommuType = "";

        /// <summary>
        /// 波特率
        /// </summary>
        private string m_str_BaudRate = "";

        /// <summary>
        /// 通讯端口
        /// </summary>
        private string m_str_ComPort = "";

        /// <summary>
        /// 命令延时(ms)
        /// </summary>
        private int m_int_CmdTime = 0;

        /// <summary>
        /// 字节延时(ms)
        /// </summary>
        private int m_int_ByteTime = 0;

        /// <summary>
        /// 配置文件
        /// </summary>
        private string m_str_ConfigFile = "";

        /// <summary>
        /// 标志检查
        /// </summary>
        private bool m_bln_Loading = false;
        /// <summary>
        /// 路由标识
        /// </summary>
        private byte m_RouterID;

        #endregion------------------------------------

        #region--------------公共属性-----------------
        /// <summary>
        /// 协议名称
        /// </summary>
        public string ProtocolName
        {
            get
            {
                return this.m_str_ProtocolName; 
            }
            set
            {
                this.m_str_ProtocolName = value;
            }
        }

        /// <summary>
        /// 协议类型
        /// </summary>
        public string CarrierType
        {
            get
            {
                return this.m_str_CarrierType;
            }
            set
            {
                this.m_str_CarrierType = value;
            }
        }

        /// <summary>
        /// 抄表器类型
        /// </summary>
        public string ReadType
        {
            get
            {
                return this.m_str_ReadType;
            }
            set
            {
                this.m_str_ReadType = value;
            }
        }


        /// <summary>
        /// 通讯方式
        /// </summary>
        public string CommuType
        {
            get
            {
                return this.m_str_CommuType;
            }
            set
            {
                this.m_str_CommuType = value;
            }
        }

        /// <summary>
        /// 波特率
        /// </summary>
        public string BaudRate
        {
            get
            {
                return this.m_str_BaudRate;
            }
            set
            {
                this.m_str_BaudRate = value;
            }
        }

        /// <summary>
        /// 通讯端口
        /// </summary>
        public string ComPort
        {
            get
            {
                return this.m_str_ComPort;
            }
            set
            {
                this.m_str_ComPort = value;
            }
        }

        /// <summary>
        /// 命令延时(ms)
        /// </summary>
        public int CmdTime 
        {
            get
            {
                return this.m_int_CmdTime;
            }
            set
            {
                this.m_int_CmdTime = value;
            }
        }

        /// <summary>
        /// 字节延时(ms)
        /// </summary>
        public int ByteTime
        {
            get
            {
                return this.m_int_ByteTime;
            }
            set
            {
                this.m_int_ByteTime = value;
            }
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        public string ConfigFile
        {
            get
            {
                return this.m_str_ConfigFile;
            }
            set
            {
                this.m_str_ConfigFile = value;
            }
        }

        /// <summary>
        /// 标志检查（只读），如果loading为假表示加载协议失败！
        /// </summary>
        public bool Loading
        {
            get
            {
                return m_bln_Loading;
            }
        }
        /// <summary>
        /// 路由标识
        /// </summary>
        public byte RouterID
        {
            get
            {
                return m_RouterID;
            }
            set
            {
                this.m_RouterID = value;
            }
        }

        #endregion------------------------------------

        #region--------------构造函数-----------------
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CarrierProtocolInfo()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_str_ProtocolName">协议名称</param>
        public CarrierProtocolInfo(string p_str_ProtocolName)
        {
            this.m_str_ProtocolName = p_str_ProtocolName;
        }
        #endregion------------------------------------

        #region--------------公共函数-----------------
        /// <summary>
        /// 根据协议名称加载协议信息
        /// </summary>
        /// <param name="ProtocolName">协议名称</param>
        public void Load(string p_str_ProtocolName)
        {
            this.LoadXmlData(p_str_ProtocolName);
        }

        /// <summary>
        /// 获取载波协议名称列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetProtocolNameList()
        {
            CLDC_DataCore.DataBase.clsXmlControl xmlControl = new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_CARRIER);

            if (xmlControl.Count() == 0)
            {
                return new List<string>();
            }

            List<string> protocolNames = new List<string>();

            System.Xml.XmlNode xmlNode = xmlControl.toXmlNode();

            for (int i = 0; i < xmlNode.ChildNodes.Count; i++)
            {
                protocolNames.Add(xmlNode.ChildNodes[i].Attributes["CarrierName"].Value);
            }

            xmlNode = null;
            xmlControl = null;

            return protocolNames;
        }

        /// <summary>
        /// 获取载波协议
        /// </summary>
        /// <param name="p_str_ProtocolName">载波协议名</param>
        /// <returns></returns>
        public static CarrierProtocolInfo GetCarrierProtocolInfo(string p_str_ProtocolName)
        {
            CarrierProtocolInfo carrierInfo = new CarrierProtocolInfo();
            string str_XmlFile = Directory.GetCurrentDirectory() + CLDC_DataCore.Const.Variable.CONST_CARRIER;

            XmlDocument doc = new XmlDocument();
            doc.Load(str_XmlFile);


            foreach (XmlNode node_item in doc.DocumentElement.ChildNodes)
            {
                if (node_item.Attributes["CarrierName"].Value == p_str_ProtocolName)
                {
                    carrierInfo.ProtocolName = p_str_ProtocolName;                                                       //协议名  
                    carrierInfo.CarrierType = node_item.Attributes["CarrierType"].Value;                                 //波特率   
                    carrierInfo.BaudRate = node_item.Attributes["BaudRate"].Value;                                       //字节延时
                    carrierInfo.ByteTime = int.Parse(node_item.Attributes["ByteTime"].Value);                            //通道号
                    carrierInfo.m_str_CommuType = node_item.Attributes["CommuType"].Value;                               //命令延时
                    carrierInfo.CmdTime = int.Parse(node_item.Attributes["CmdTime"].Value);                              //通讯端口号
                    carrierInfo.ComPort = node_item.Attributes["Comm"].Value;                                            //抄表器类型
                    carrierInfo.ReadType = node_item.Attributes["RdType"].Value;                                         //路由标识
                    if (node_item.Attributes.Count > 8)
                    {
                        carrierInfo.RouterID = byte.Parse(node_item.Attributes["RouterID"].Value);
                    }
                    return carrierInfo;
                }
            }

            return carrierInfo;
        }

        /// <summary>
        /// 获取协议唯一标志组合名称列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetProtocolDictionary()
        {
            CLDC_DataCore.DataBase.clsXmlControl _XmlNode = new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_CARRIER);

            if (_XmlNode.Count() == 0) return new Dictionary<string, string>();

            Dictionary<string, string> _ReturnNames = new Dictionary<string, string>();

            return _ReturnNames;
        }

        /// <summary>
        /// 删除当前打开的协议
        /// </summary>
        /// <returns></returns>
        public bool RemoveProtocol()
        {
            return Remove(this.m_str_ProtocolName);
        }
        #endregion------------------------------------

        #region--------------私有函数-----------------
        /// <summary>
        /// 加载XML文档
        /// </summary>
        /// <param name="p_str_ProtocolName">协议名称</param>
        private void LoadXmlData(string p_str_ProtocolName)
        {
            if (p_str_ProtocolName == "")
            {
                return;
            }
            CLDC_DataCore.DataBase.clsXmlControl xmlNode = new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_CARRIER);

            if (xmlNode.Count() == 0)
            {
                return;
            }

            System.Xml.XmlNode xnd_FindXmlNode = null;

            if (p_str_ProtocolName != "")
            {
                xnd_FindXmlNode = CLDC_DataCore.DataBase.clsXmlControl.FindSencetion(xmlNode.toXmlNode(),
                    CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("R,CarrierName,{0}", p_str_ProtocolName)));
            }
            if (xnd_FindXmlNode == null)
            {
                return;
            }

            #region----------------------------加载协议文件信息----------------------------------------------------------------------

            this.m_str_ProtocolName = xnd_FindXmlNode.Attributes["CarrierName"].Value;                                                           //协议名称 

            this.m_str_CarrierType = CLDC_DataCore.DataBase.clsXmlControl.getNodeAttributeValue(xnd_FindXmlNode, "CarrierType");                                //协议类型

            this.m_str_ReadType = CLDC_DataCore.DataBase.clsXmlControl.getNodeAttributeValue(xnd_FindXmlNode, "RdType");                                  //抄表器类型
            
            this.m_str_CommuType = CLDC_DataCore.DataBase.clsXmlControl.getNodeAttributeValue(xnd_FindXmlNode, "CommuType");                                       //通讯方式

            this.m_str_BaudRate = CLDC_DataCore.DataBase.clsXmlControl.getNodeAttributeValue(xnd_FindXmlNode, "BaudRate");                                //波特率

            this.m_str_ComPort = CLDC_DataCore.DataBase.clsXmlControl.getNodeAttributeValue(xnd_FindXmlNode, "Comm");                                     //端口号

            this.m_int_CmdTime = int.Parse(CLDC_DataCore.DataBase.clsXmlControl.getNodeAttributeValue(xnd_FindXmlNode, "CmdTime"));                       //命令延时(ms)

            this.m_int_ByteTime = int.Parse(CLDC_DataCore.DataBase.clsXmlControl.getNodeAttributeValue(xnd_FindXmlNode, "ByteTime"));                     //字节延时(ms)

            if (xnd_FindXmlNode.Attributes.Count > 8)
            {
                this.RouterID = byte.Parse(CLDC_DataCore.DataBase.clsXmlControl.getNodeAttributeValue(xnd_FindXmlNode, "RouterID"));
            }

            xnd_FindXmlNode = CLDC_DataCore.DataBase.clsXmlControl.FindSencetion(xnd_FindXmlNode, CLDC_DataCore.DataBase.clsXmlControl.XPath("Prjs"));             //转到项目数据节点

            this.m_bln_Loading = true;                //改写加载标志，表示加载协议成功

            #endregion----------------------------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// 移除一个协议节点
        /// </summary>
        /// <param name="p_str_ProtocolName">协议名称</param>
        /// <returns></returns>
        private static bool Remove(string p_str_ProtocolName)
        {
            if (p_str_ProtocolName == "")
                return false;
            CLDC_DataCore.DataBase.clsXmlControl xmlNode = new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_CARRIER);

            if (xmlNode.Count() == 0) return false;

            if (p_str_ProtocolName != "")
                xmlNode.RemoveChild(CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("R,CarrierName,{0}", p_str_ProtocolName)));

            xmlNode.SaveXml();
            return true;

        }
        #endregion------------------------------------
    }
}

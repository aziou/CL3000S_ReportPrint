using System;
using System.Collections.Generic;
using System.Text;
using CommDataBase = CLDC_DataCore.DataBase;
namespace CLDC_DataCore.Model.DgnProtocol
{
    [Serializable()]
    /// <summary>
    /// 多功能通信协议配置模型
    /// </summary>
    public class DgnProtocolInfo
    {
        #region---------------------------------------------协议模型结构部分-----------------------------------------
        /// <summary>
        /// 协议名称
        /// </summary>
        public string ProtocolName = "";

        /// <summary>
        /// 协议库名称
        /// </summary>
        public string DllFile = "";

        /// <summary>
        /// 协议类
        /// </summary>
        public string ClassName = "";

        /// <summary>
        /// 通信参数
        /// </summary>
        public string Setting = "";

        /// <summary>
        /// 用户代码
        /// </summary>
        public string UserID = "";

        /// <summary>
        /// 验证密码类型
        /// </summary>
        public int VerifyPasswordType = 0;

        /// <summary>
        /// 写操作密码
        /// </summary>
        public string WritePassword = "";

        /// <summary>
        /// 写操作密码等级
        /// </summary>
        public string WriteClass = "";

        private string clearDemandPassword = "";

        /// <summary>
        /// 清需量密码
        /// </summary>
        public string ClearDemandPassword
        {
            get { return this.clearDemandPassword; }
            set
            {
                if (value.Length != 8)
                {
                    //int i = 2332;
                }
                this.clearDemandPassword = value;

            }

        }

        /// <summary>
        /// 清需量密码等级
        /// </summary>
        public string ClearDemandClass = "";

        /// <summary>
        /// 清电量密码
        /// </summary>
        public string ClearDLPassword = "";

        /// <summary>
        /// 清电量密码等级
        /// </summary>
        public string ClearDLClass = "";
        /// <summary>
        /// 费率排序（峰平谷尖2341）
        /// </summary>
        public string TariffOrderType = "2341";
        /// <summary>
        /// 日期时间格式
        /// </summary>
        public string DateTimeFormat = "";
        /// <summary>
        /// 星期天序号
        /// </summary>
        public int SundayIndex = 0;
        /// <summary>
        /// 下发帧的唤醒符个数
        /// </summary>
        public int FECount = 0;

        /// <summary>
        /// 时钟频率
        /// </summary>
        public float ClockPL = 1;

        /// <summary>
        /// 数据域是否包含密码
        /// </summary>
        public bool DataFieldPassword = false;

        /// <summary>
        /// 写块操作是否加AA
        /// </summary>
        public bool BlockAddAA = false;
        /// <summary>
        /// 配置文件
        /// </summary>
        public string ConfigFile = "";

        /// <summary>
        /// 协议参数列表，KEY值为协议测试项目ID，并非多功能试验项目ID
        /// </summary>
        public Dictionary<string, string> DgnPras;

        /// <summary>
        /// 区别有无编程键，false：无，true：有
        /// </summary>
        public bool HaveProgrammingkey = false;


        /// <summary>
        /// 是否是南网13加密机，false：不是，true：是
        /// </summary>
        public bool IsSouthEncryption = false;

        private bool _Loading = false;
        /// <summary>
        /// 标志检查（只读），如果loading为假表示加载协议失败！
        /// </summary>
        public bool Loading
        {
            get
            {
                return _Loading;
            }
        }

        #endregion

        #region ---------------------------------下面部分为协议文件操作配置部分--------------------------------------


        /// <summary>
        /// 电能表制造厂家
        /// </summary>
        private string _DnbFactroy = "";
        /// <summary>
        /// 电能表型号
        /// </summary>
        private string _DnbSize = "";

        /// <summary>
        /// 电能表制造厂家
        /// </summary>
        public string DnbFactroy
        {
            get
            {
                return _DnbFactroy;
            }
            set
            {
                _DnbFactroy = value;
            }
        }
        /// <summary>
        /// 电能表型号
        /// </summary>
        public string DnbSize
        {
            get
            {
                return _DnbSize;
            }
            set
            {
                _DnbSize = value;
            }
        }


        public DgnProtocolInfo()
        {

        }
        /// <summary>
        /// 构造函数，根据协议名称获取通信协议
        /// </summary>
        /// <param name="ProtocolName">协议名称</param>
        public DgnProtocolInfo(string ProtocolName)
        {
            this.ProtocolName = ProtocolName;
            this.Load(ProtocolName);
        }
        /// <summary>
        /// 构造函数，根据生产厂家和表型号获取通信协议
        /// </summary>
        /// <param name="Factroy">生产厂家</param>
        /// <param name="Size">表型号</param>
        public DgnProtocolInfo(string Factroy, string Size)
        {
            this._DnbSize = Size;
            this._DnbFactroy = Factroy;
            this.Load(Factroy, Size);
        }


        /// <summary>
        /// 加载协议信息，调用该函数的前提是要么协议名称有值，要么制造厂家和表型号有值
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            if (this.ProtocolName == "" || (this._DnbFactroy == "" && this._DnbSize == ""))
            {
                return false;
            }

            this.LoadXmlData(this.ProtocolName, this._DnbFactroy, this._DnbSize);

            return true;
        }
        /// <summary>
        /// 根据协议名称加载协议信息
        /// </summary>
        /// <param name="ProtocolName"></param>
        public void Load(string ProtocolName)
        {
            this.LoadXmlData(ProtocolName, "", "");
        }
        /// <summary>
        /// 根据制造厂家和表型号加载协议信息
        /// </summary>
        /// <param name="Factroy"></param>
        /// <param name="Size"></param>
        public void Load(string Factroy, string Size)
        {
            this.LoadXmlData("", Factroy, Size);
        }


        /// <summary>
        /// 获取协议名称列表
        /// </summary>
        /// <returns></returns>
        public static List<string> getProtocolNames()
        {
            CLDC_DataCore.DataBase.clsXmlControl _XmlNode = new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_DGNPROTOCOL);

            if (_XmlNode.Count() == 0) return new List<string>();

            List<string> _ReturnNames = new List<string>();

            System.Xml.XmlNode _Xml = _XmlNode.toXmlNode();

            for (int i = 0; i < _Xml.ChildNodes.Count; i++)
            {
                _ReturnNames.Add(_Xml.ChildNodes[i].Attributes["Name"].Value);
            }

            _Xml = null;
            _XmlNode = null;

            return _ReturnNames;
        }

        /// <summary>
        /// 根据制造厂家和表型号获取协议名称
        /// </summary>
        /// <param name="Factroy">制造厂家</param>
        /// <param name="Size">表型号</param>
        /// <returns></returns>
        public static string getProtocolName(string Factroy, string Size)
        {
            if (Factroy == "" || Size == "")
            {
                return "";
            }
            CLDC_DataCore.DataBase.clsXmlControl _XmlNode = new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_DGNPROTOCOL);

            if (_XmlNode.Count() == 0) return "";

            System.Xml.XmlNode Xml_Child = null;

            Xml_Child = CLDC_DataCore.DataBase.clsXmlControl.FindSencetion(_XmlNode.toXmlNode()
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("R,ZZCJ,{0},BXH,{1}", Factroy, Size)));
            if (Xml_Child == null) return "";

            return CLDC_DataCore.DataBase.clsXmlControl.getNodeAttributeValue(Xml_Child, "Name");

        }

        /// <summary>
        /// 获取协议唯一标志组合名称列表
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> getProtocolString()
        {
            CLDC_DataCore.DataBase.clsXmlControl _XmlNode = new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_DGNPROTOCOL);

            if (_XmlNode.Count() == 0) return new Dictionary<string, string>();

            Dictionary<string, string> _ReturnNames = new Dictionary<string, string>();

            System.Xml.XmlNode _Xml = _XmlNode.toXmlNode();

            for (int i = 0; i < _Xml.ChildNodes.Count; i++)
            {
                _ReturnNames.Add(_Xml.ChildNodes[i].Attributes["Name"].Value, string.Format("协议名称：{0}；制造厂家：{1}；表型号：{2}", _Xml.ChildNodes[i].Attributes["Name"].Value
                                                                                          , _Xml.ChildNodes[i].Attributes["ZZCJ"].Value
                                                                                          , _Xml.ChildNodes[i].Attributes["BXH"].Value));
            }

            _Xml = null;
            _XmlNode = null;

            return _ReturnNames;
        }

        /// <summary>
        /// 删除当前打开的协议
        /// </summary>
        /// <returns></returns>
        public bool RemoveProtocol()
        {
            return Remove(this.ProtocolName, "", "");
        }

        /// <summary>
        /// 根据协议名称删除协议
        /// </summary>
        /// <param name="protocolname"></param>
        /// <returns></returns>
        public static bool RemoveProtocol(string protocolname)
        {

            return Remove(protocolname, "", "");
        }

        /// <summary>
        /// 根据制造厂家和表型号删除协议
        /// </summary>
        /// <param name="factroy"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static bool RemoveProtocol(string factory, string size)
        {
            return Remove("", factory, size);
        }

        /// <summary>
        /// 移除一个协议节点
        /// </summary>
        /// <param name="protocolname">协议名称</param>
        /// <param name="factory">制造厂家</param>
        /// <param name="size">表型号</param>
        /// <returns></returns>
        private static bool Remove(string protocolname, string factory, string size)
        {
            if (protocolname == "" && (factory == "" || size == ""))
                return false;
            CLDC_DataCore.DataBase.clsXmlControl _XmlNode = new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_DGNPROTOCOL);

            if (_XmlNode.Count() == 0) return false;

            if (protocolname != "")
                _XmlNode.RemoveChild(CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("R,Name,{0}", protocolname)));
            if (factory != "" && size != "")
                _XmlNode.RemoveChild(CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("R,ZZCJ,{0},BXH,{1}", factory, size)));

            _XmlNode.SaveXml();
            return true;

        }
        /// <summary>
        /// 添加一个新的协议！
        /// </summary>
        /// <returns></returns>
        public bool AddNewProtocol()
        {
            if (ProtocolName == "" || _DnbFactroy == "" || _DnbSize == "")
                return false;
            CLDC_DataCore.DataBase.clsXmlControl _XmlNode = new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_DGNPROTOCOL);
            if (ContainsNode(_XmlNode, ProtocolName, "", ""))
                Remove(ProtocolName, "", "");
            if (ContainsNode(_XmlNode, "", _DnbFactroy, _DnbSize))
                Remove("", _DnbFactroy, _DnbSize);
            if (_XmlNode.toXmlNode() == null)
            {
                _XmlNode.appendchild("", "DgnProtocol");
            }
            _XmlNode.appendchild("", "R", "Name", ProtocolName, "ZZCJ", _DnbFactroy, "BXH", _DnbSize);

            #region -----------------------------------------插入XML节点---------------------------------------------

            string _Xpath = CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("R,Name,{0}", ProtocolName));

            _XmlNode.appendchild(_Xpath, "C", "Name", "DllFile", this.DllFile);       //协议库名称
            _XmlNode.appendchild(_Xpath, "C", "Name", "ClassName", this.ClassName);       //所使用的协议名称
            _XmlNode.appendchild(_Xpath, "C", "Name", "Setting", this.Setting);       //通信参数
            _XmlNode.appendchild(_Xpath, "C", "Name", "UserID", this.UserID);       //用户名
            _XmlNode.appendchild(_Xpath, "C", "Name", "VerifyPasswordType", this.VerifyPasswordType.ToString());       //验证类型
            _XmlNode.appendchild(_Xpath, "C", "Name", "WritePassword", this.WritePassword);       //写密码
            _XmlNode.appendchild(_Xpath, "C", "Name", "WriteClass", this.WriteClass);       //写等级
            _XmlNode.appendchild(_Xpath, "C", "Name", "ClearDemandPassword", this.ClearDemandPassword);       //清需量密码
            _XmlNode.appendchild(_Xpath, "C", "Name", "ClearDemandClass", this.ClearDemandClass);       //请需量密码等级
            _XmlNode.appendchild(_Xpath, "C", "Name", "ClearDLPassword", this.ClearDLPassword);       //清电量密码
            _XmlNode.appendchild(_Xpath, "C", "Name", "ClearDLClass", this.ClearDLClass);       //清电量等级
            _XmlNode.appendchild(_Xpath, "C", "Name", "TariffOrderType", this.TariffOrderType);       //费率类型
            _XmlNode.appendchild(_Xpath, "C", "Name", "DateTimeFormat", this.DateTimeFormat);       //日期格式
            _XmlNode.appendchild(_Xpath, "C", "Name", "SundayIndex", this.SundayIndex.ToString());       //星期天表示
            _XmlNode.appendchild(_Xpath, "C", "Name", "ClockPL", this.ClockPL.ToString());              //时钟频率
            _XmlNode.appendchild(_Xpath, "C", "Name", "FECount", this.FECount.ToString());       //唤醒FE个数
            _XmlNode.appendchild(_Xpath, "C", "Name", "DataFieldPassword", this.DataFieldPassword ? "1" : "0");       //数据域是否包含密码
            _XmlNode.appendchild(_Xpath, "C", "Name", "BlockAddAA", this.BlockAddAA ? "1" : "0");       //写数据块是否加AA
            _XmlNode.appendchild(_Xpath, "C", "Name", "ConfigFile", this.ConfigFile);       //配置文件
            _XmlNode.appendchild(_Xpath, "C", "Name", "HaveProgrammingkey", this.HaveProgrammingkey ? "1" : "0");//有无编程键
            _XmlNode.appendchild(_Xpath, "C", "Name", "IsSouthEn", this.IsSouthEncryption ? "1" : "0");//是否是南网13加密机

            if (this.DgnPras.Count > 0)
                _XmlNode.appendchild(_Xpath, "Prjs");               //插入项目节点
            else
            {
                _XmlNode.SaveXml();
                return true;
            }
            _Xpath = CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("R,Name,{0}|Prjs", ProtocolName));

            foreach (string _Key in this.DgnPras.Keys)
            {
                _XmlNode.appendchild(_Xpath, "D", "ID", _Key, DgnPras[_Key]);
            }
            _XmlNode.SaveXml();
            #endregion

            return true;
        }


        /// <summary>
        /// 查找是否有存在该关键字的协议节点
        /// </summary>
        /// <param name="protocolname">协议名称</param>
        /// <returns>存在返回真，不存在返回假</returns>
        public static bool ContainsNodeBypName(string protocolname)
        {
            return ContainsNode(null, protocolname, "", "");
        }


        /// <summary>
        /// 查找是否有存在该关键字的协议节点
        /// </summary>
        /// <param name="factroy">制造厂家</param>
        /// <param name="size">表型号</param>
        /// <returns></returns>
        public static bool ContainsNodeByFactroy(string factroy, string size)
        {
            return ContainsNode(null, "", factroy, size);
        }

        /// <summary>
        /// 根据条件查找是否存在包含关键字的协议节点
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="protocolname"></param>
        /// <param name="factroy"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private static bool ContainsNode(CLDC_DataCore.DataBase.clsXmlControl Node, string protocolname, string factroy, string size)
        {
            if (Node == null)
            {
                Node = new CLDC_DataCore.DataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_DGNPROTOCOL);

            }
            if (Node == null) return false;

            if (protocolname != "")
                if (CommDataBase.clsXmlControl.FindSencetion(Node.toXmlNode(), CommDataBase.clsXmlControl.XPath(string.Format("R,Name,{0}", protocolname))) != null)
                    return true;
                else
                    return false;
            if (factroy != "" && size != "")
            {
                if (CommDataBase.clsXmlControl.FindSencetion(Node.toXmlNode(), CommDataBase.clsXmlControl.XPath(string.Format("R,ZZCJ,{0},BXH,{1}", factroy, size))) != null)
                    return true;
                else
                    return false;
            }
            return false;
        }

        /// <summary>
        /// 加载XML文档
        /// </summary>
        /// <param name="protocolname"></param>
        /// <param name="factroy"></param>
        /// <param name="size"></param>
        private void LoadXmlData(string protocolname, string factory, string size)
        {
            if (protocolname == "" && (factory == "" || size == ""))
                return;

            CommDataBase.clsXmlControl _XmlNode = new CommDataBase.clsXmlControl(System.Windows.Forms.Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_DGNPROTOCOL);

            if (_XmlNode.Count() == 0) return;

            System.Xml.XmlNode _FindXmlNode = null;

            if (protocolname != "")
                _FindXmlNode = CLDC_DataCore.DataBase.clsXmlControl.FindSencetion(_XmlNode.toXmlNode()
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("R,Name,{0}", protocolname)));
            else if (factory != "" && size != "")
                _FindXmlNode = CLDC_DataCore.DataBase.clsXmlControl.FindSencetion(_XmlNode.toXmlNode()
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath(string.Format("R,ZZCJ,{0},BXH,{1}", factory, size)));
            if (_FindXmlNode == null) return;

            #region----------------------------加载协议文件信息----------------------------------------------------------------------

            this.ProtocolName = _FindXmlNode.Attributes["Name"].Value;          //协议名称 

            this.DnbFactroy = CLDC_DataCore.DataBase.clsXmlControl.getNodeAttributeValue(_FindXmlNode, "ZZCJ");                  //制造厂家

            this.DnbSize = CLDC_DataCore.DataBase.clsXmlControl.getNodeAttributeValue(_FindXmlNode, "BXH");                      //表型号

            this.DllFile = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,DllFile"));             //协议库名称
            this.ClassName = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,ClassName"));           //说使用协议类名称
            this.Setting = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,Setting"));             //通信参数
            this.UserID = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,UserID"));              //用户名
            this.VerifyPasswordType = int.Parse(CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,VerifyPasswordType"))); //验证类型
            this.WritePassword = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,WritePassword"));       //写密码
            this.WriteClass = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,WriteClass"));          //写密码等级
            this.ClearDemandPassword = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,ClearDemandPassword")); //清需量密码
            this.ClearDemandClass = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,ClearDemandClass"));    //请需量密码等级    
            this.ClearDLPassword = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,ClearDLPassword"));     //清电量密码
            this.ClearDLClass = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,ClearDLClass"));        //清电量等级    
            this.TariffOrderType = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,TariffOrderType"));     //费率类型
            this.DateTimeFormat = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,DateTimeFormat"));      //日期格式
            this.SundayIndex = int.Parse(CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,SundayIndex")));        //星期天表示
            this.ClockPL = float.Parse(CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode, CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,ClockPL")) == "" ? "1"
                                                                : CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode, CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,ClockPL")));        //时钟频率
            this.FECount = int.Parse(CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,FECount")));            //唤醒FE个数
            this.DataFieldPassword = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,DataFieldPassword")) == "0" ? false : true;   //数据域是否包含密码
            this.BlockAddAA = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,BlockAddAA")) == "0" ? false : true;    //写数据块是否加AA    
            this.ConfigFile = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode
                                                                , CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,ConfigFile"));                          //配置文件    
            this.HaveProgrammingkey = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode, CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,HaveProgrammingkey")) == "0" ? false : true; //有无编程键
            this.IsSouthEncryption = CLDC_DataCore.DataBase.clsXmlControl.getNodeValue(_FindXmlNode, CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Name,IsSouthEn")) == "0" ? false : true; //有无编程键

            _FindXmlNode = CLDC_DataCore.DataBase.clsXmlControl.FindSencetion(_FindXmlNode, CLDC_DataCore.DataBase.clsXmlControl.XPath("Prjs"));          //转到项目数据节点


            this._Loading = true;                //改写加载标志，表示加载协议成功

            this.DgnPras = new Dictionary<string, string>();

            if (_FindXmlNode == null) return;

            for (int i = 0; i < _FindXmlNode.ChildNodes.Count; i++)
            {
                this.DgnPras.Add(_FindXmlNode.ChildNodes[i].Attributes["ID"].Value, _FindXmlNode.ChildNodes[i].ChildNodes[0].Value);        //加入ID，值
            }

            if (this.DgnPras.Count == 0) return;



            #endregion

        }

        #endregion


        public override string ToString()
        {
            return "HashCode:" + this.GetHashCode();
        }

        #region ---------------------------------------------协议库模型---------------------------------------
        /// <summary>
        /// 协议编号
        /// </summary>
        public int Pro_ProtocolID = 0;
        /// <summary>
        /// 通讯协议归属
        /// </summary>
        public int Pro_proNameNo = 0;
        /// <summary>
        /// 信息序号
        /// </summary>
        public int Pro_intInfoNo = 0;
        /// <summary>
        /// 协议名称
        /// </summary>
        public string Pro_chrPname = "";
        /// <summary>
        /// 对应值
        /// </summary>
        public string Pro_chrValue = "";
        #endregion

        #region ---------------------------------------------协议标识字典模型---------------------------------------
        /// <summary>
        /// 编号
        /// </summary>
        public int Pd_dltID = 0;
        /// <summary>
        /// 国标协议代号
        /// </summary>
        public int Pd_proNameNo = 0;
        /// <summary>
        /// 数据标识编码类型
        /// </summary>
        public int Pd_intIdentType = 0;
        /// <summary>
        /// 权限
        /// </summary>
        public int Pd_intClass = 0;
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Pd_chrItemName = "";
        /// <summary>
        /// 数据标识
        /// </summary>
        public string Pd_chrID = "";
        /// <summary>
        /// 长度
        /// </summary>
        public int Pd_intLength = 0;
        /// <summary>
        /// 小数位
        /// </summary>
        public int Pd_intDot = 0;
        /// <summary>
        /// 操作方式
        /// </summary>
        public int Pd_intType = 0;
        /// <summary>
        /// 格式串
        /// </summary>
        public string Pd_chrFormat = "";
        /// <summary>
        /// 定义值
        /// </summary>
        public string Pd_chrDefValue = "";

        CLDC_DataCore.DataBase.DataControl _Data;

        #endregion 
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="AccessPath">Access数据库路径</param>
        /// <param name="Ip">服务器数据库Ip地址</param>
        /// <param name="UserName">服务器登录名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        private bool ConnectDataBase(string AccessPath, string Ip, string UserName, string pwd)
        {
            if (Ip != "")    //构造连接SQL服务器
                _Data = new CLDC_DataCore.DataBase.DataControl(Ip, UserName, pwd);
            else if (AccessPath != "")  //构造连接本地ACCESS数据库
                _Data = new CLDC_DataCore.DataBase.DataControl(AccessPath,true);
            else
                _Data = new CLDC_DataCore.DataBase.DataControl();        //构造连接本地默认ACCESS数据库

            if (_Data.Connection)
                return true;
            else
                return false;
        }
        #region ---------------------------------------------通讯协议和协议标识字典查询---------------------------------------
        /// <summary>
        /// 通讯协议查询
        /// AccessPath、Ip、UserName、Pwd均为""时为本地默认路径数据库
        /// </summary>
        /// <param name="ProtocolID">要查询的协议编号</param>
        /// <param name="AccessPath">Access数据库路径，填写此项则Ip、UserName、pwd均填""</param>
        /// <param name="Ip"></param>
        /// <param name="UserName"></param>
        /// <param name="Pwd"></param>
        public DgnProtocolInfo SelectProtocol(int ProtocolID, string AccessPath, string Ip, string UserName, string Pwd)
        {
            string Sql = string.Format("Select * from ProtocolInfo where ProtocolID={0}", ProtocolID);
            if (Ip != "")
                return this.GetProtocolInfo(Sql, "", Ip, UserName, Pwd);
            else if (AccessPath != "")
                return this.GetProtocolInfo(Sql, AccessPath, "", "", "");
            else
                return this.GetProtocolInfo(Sql, "", "", "", "");
        }
        /// <summary>
        /// 协议标识字典查询
        /// AccessPath、Ip、UserName、Pwd均为""时为本地默认路径数据库
        /// </summary>
        /// <param name="dltID">编号</param>
        /// <param name="AccessPath">Access数据库路径，填写此项则Ip、UserName、pwd均填""</param>
        /// <param name="Ip"></param>
        /// <param name="UserName"></param>
        /// <param name="Pwd"></param>
        public DgnProtocolInfo SelectDict(int dltID, string AccessPath, string Ip, string UserName, string Pwd)
        {
            string Sql = string.Format("Select * from ProDLT645Dict where dltID={0}", dltID);
            if (Ip != "")
                return this.GetProDLT645Dict(Sql, "", Ip, UserName, Pwd);
            else if (AccessPath != "")
                return this.GetProDLT645Dict(Sql, AccessPath, "", "", "");
            else
                return this.GetProDLT645Dict(Sql, "", "", "", "");
        }
        /// <summary>
        /// 查询通讯协议
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="AccessPath">Access数据库路径</param>
        /// <param name="Ip">服务器数据库Ip地址</param>
        /// <param name="UserName">服务器登录名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        private DgnProtocolInfo GetProtocolInfo(string sql, string AccessPath, string Ip, string UserName, string pwd)
        {
            DgnProtocolInfo Items = new DgnProtocolInfo();
            bool flag = false;
            if (Ip != "")
                flag = ConnectDataBase("", Ip, UserName, pwd);
            else if (AccessPath != "")
                flag = ConnectDataBase(AccessPath, "", "", "");
            else
                flag = ConnectDataBase("","","","");

            try
            {
                if (!flag)
                    return Items;
                System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(sql, _Data.Con);
                System.Data.OleDb.OleDbDataReader Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    Items.Pro_ProtocolID = Convert.ToInt32(Reader["ProtocolID"].ToString()); //协议编号
                    Items.Pro_proNameNo = Convert.ToInt32(Reader["proNameNo"].ToString());//通讯协议归属
                    Items.Pro_chrPname = Reader["chrPname"].ToString();//协议名称
                    Items.Pro_intInfoNo = Convert.ToInt32(Reader["intInfoNo"].ToString());//信息序号
                    Items.Pro_chrValue = Reader["chrValue"].ToString();//对应值
                }
                Reader.Close();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "数据库操作错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                _Data.CloseDB();                
            }
            return Items;
        }
        /// <summary>
        /// 查询协议标识字典
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="AccessPath">Access数据库路径</param>
        /// <param name="Ip">服务器数据库Ip地址</param>
        /// <param name="UserName">服务器登录名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        private DgnProtocolInfo GetProDLT645Dict(string sql, string AccessPath, string Ip, string UserName, string pwd)
        {
            DgnProtocolInfo Items = new DgnProtocolInfo();
            bool flag = false;
            if (Ip != "")
                flag = ConnectDataBase("", Ip, UserName, pwd);
            else if (AccessPath != "")
                flag = ConnectDataBase(AccessPath, "", "", "");
            else
                flag = ConnectDataBase("", "", "", "");

            try
            {
                if (!flag)
                    return Items;
                System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(sql, _Data.Con);
                System.Data.OleDb.OleDbDataReader Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    Items.Pd_dltID = Convert.ToInt32(Reader["dltID"].ToString());//编号
                    Items.Pd_proNameNo = Convert.ToInt32(Reader["proNameNo"].ToString());//国标协议代号
                    Items.Pd_intIdentType = Convert.ToInt32(Reader["intIdentType"].ToString());//数据标识编码类型
                    Items.Pd_intClass = Convert.ToInt32(Reader["intClass"].ToString());//权限
                    Items.Pd_chrItemName = Reader["chrItemName"].ToString();//项目名称
                    Items.Pd_chrID = Reader["chrID"].ToString();//数据标识
                    Items.Pd_intLength = Convert.ToInt32(Reader["intLength"].ToString());//长度
                    Items.Pd_intDot = Convert.ToInt32(Reader["intDot"].ToString());//小数位
                    Items.Pd_chrFormat = Reader["chrFormat"].ToString();//格式串
                    Items.Pd_intType = Convert.ToInt32(Reader["intType"].ToString());//操作方式
                    Items.Pd_chrDefValue = Reader["chrDefValue"].ToString();//定义值
                }
                Reader.Close();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "数据库操作错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                _Data.CloseDB();
            }
            return Items;
        }
        #endregion 

        #region ---------------------------------------------通讯协议和协议标识字典删除---------------------------------------
        /// <summary>
        /// 通讯协议删除
        /// AccessPath、Ip、UserName、Pwd均为""时为本地默认路径数据库
        /// </summary>
        /// <param name="ProtocolID">要删除的协议编号</param>
        /// <param name="AccessPath">Access数据库路径，填写此项则Ip、UserName、pwd均填""</param>
        /// <param name="Ip"></param>
        /// <param name="UserName"></param>
        /// <param name="Pwd"></param>
        public void DeleteProtocol(int ProtocolID, string AccessPath, string Ip, string UserName, string Pwd)
        {
            string Sql = string.Format("Delete from ProtocolInfo where ProtocolID={0}",ProtocolID);
            if (Ip != "")
                DeleteProtocolInfo(Sql, "", Ip, UserName, Pwd);
            else if (AccessPath != "")
                DeleteProtocolInfo(Sql, AccessPath, "", "", "");
            else
                DeleteProtocolInfo(Sql, "", "", "", "");
        }
        /// <summary>
        /// 协议标识字典删除
        /// AccessPath、Ip、UserName、Pwd均为""时为本地默认路径数据库
        /// </summary>
        /// <param name="dltID">编号</param>
        /// <param name="AccessPath">Access数据库路径，填写此项则Ip、UserName、pwd均填""</param>
        /// <param name="Ip"></param>
        /// <param name="UserName"></param>
        /// <param name="Pwd"></param>
        public void DeleteDict(int dltID, string AccessPath, string Ip, string UserName, string Pwd)
        {
            string Sql = string.Format("Delete from ProDLT645Dict where dltID={0}", dltID);
            if (Ip != "")
                DeleteProtocolInfo(Sql, "", Ip, UserName, Pwd);
            else if (AccessPath != "")
                DeleteProtocolInfo(Sql, AccessPath, "", "", "");
            else
                DeleteProtocolInfo(Sql, "", "", "", "");
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="AccessPath"></param>
        /// <param name="Ip"></param>
        /// <param name="UserName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private bool DeleteProtocolInfo(string sql, string AccessPath, string Ip, string UserName, string pwd)
        {
            bool flag = false;
            int i = 0;
            if (Ip != "")
                flag = ConnectDataBase("", Ip, UserName, pwd);
            else if (AccessPath != "")
                flag = ConnectDataBase(AccessPath, "", "", "");
            else
                flag = ConnectDataBase("", "", "", "");

            if (flag)
            {
                System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(sql, _Data.Con);
                i = Cmd.ExecuteNonQuery();
                Cmd = null;
            }
            if (i == 1)
                return true;
            else
                return false;
        }

        #endregion

        #region ---------------------------------------------通讯协议和协议标识字典修改---------------------------------------
        /// <summary>
        /// 通讯协议修改
        /// AccessPath、Ip、UserName、Pwd均为""时为本地默认路径数据库
        /// </summary>
        /// <param name="Item">要修改的协议库数据</param>
        /// <param name="AccessPath">Access数据库路径，填写此项则Ip、UserName、pwd均填""</param>
        /// <param name="Ip">服务器数据库Ip地址</</param>
        /// <param name="UserName">服务器登录名</</param>
        /// <param name="Pwd">密码</param>
        public void UpdateProtocol(DgnProtocolInfo Item, string AccessPath, string Ip, string UserName, string Pwd)
        {
            string Sql = string.Format("Update ProtocolInfo Set ProtocolID={0},proNameNo={1},chrPname='{2}',intInfoNo={3},chrValue='{4}' where ProtocolID={5} AND proNameNo={6}",
                Item.Pro_ProtocolID, Item.Pro_proNameNo, Item.Pro_chrPname, Item.Pro_intInfoNo, Item.Pro_chrValue, Item.Pro_ProtocolID,Item.Pro_proNameNo);
            if (Ip != "")
                UpdateProtocolInfo(Sql, "", Ip, UserName, Pwd);
            else if (AccessPath != "")
                UpdateProtocolInfo(Sql, AccessPath, "", "", "");
            else
                UpdateProtocolInfo(Sql, "", "", "", "");
        }
        /// <summary>
        /// 协议标识字典修改
        /// AccessPath、Ip、UserName、Pwd均为""时为本地默认路径数据库
        /// </summary>
        /// <param name="Item">要修改的标识字典数据</param>
        /// <param name="AccessPath">Access数据库路径，填写此项则Ip、UserName、pwd均填""</param>
        /// <param name="Ip">服务器数据库Ip地址</param>
        /// <param name="UserName">服务器登录名</param>
        /// <param name="Pwd">密码</param>
        public void UpdateDict(DgnProtocolInfo Item, string AccessPath, string Ip, string UserName, string Pwd)
        {
            string Sql = string.Format("Update ProDLT645Dict Set dltID={0},proNameNo={1},intIdentType={2},intClass={3},chrItemName='{4}',chrID='{5}',intLength={6},intDot={7},chrFormat='{8}',intType={9},chrDefValue='{10}' where dltID={11} AND proNameNo={12}",
               Item.Pd_dltID, Item.Pd_proNameNo, Item.Pd_intIdentType, Item.Pd_intClass, Item.Pd_chrItemName, Item.Pd_chrID,
               Item.Pd_intLength, Item.Pd_intDot, Item.Pd_chrFormat, Item.Pd_intType, Item.Pd_chrDefValue, Item.Pd_dltID,Item.Pd_proNameNo);
            if (Ip != "")
                UpdateProtocolInfo(Sql, "", Ip, UserName, Pwd);
            else if (AccessPath != "")
                UpdateProtocolInfo(Sql, AccessPath, "", "", "");
            else
                UpdateProtocolInfo(Sql, "", "", "", "");
        }
        /// <summary>
        /// 更新协议库
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="AccessPath"></param>
        /// <param name="Ip"></param>
        /// <param name="UserName"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        private void UpdateProtocolInfo(string Sql, string AccessPath, string Ip, string UserName, string Pwd)
        {
            bool flag = false;
            if (Ip != "")
                flag = ConnectDataBase("", Ip, UserName, Pwd);
            else if (AccessPath != "")
                flag = ConnectDataBase(AccessPath, "", "", "");
            else
                flag = ConnectDataBase("", "", "", "");

            if (!flag)
                return;
                System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(Sql, _Data.Con);
                Cmd.ExecuteNonQuery();
                Cmd = null;        
        }
        #endregion

        #region ---------------------------------------------通讯协议和协议标识字典增加---------------------------------------  
        /// <summary>
        /// 增加通讯协议
        /// AccessPath、Ip、UserName、Pwd均为""时为本地默认路径数据库
        /// </summary>
        /// <param name="Items"></param>
        /// <param name="AccessPath">本地数据库路径，填写此项则Ip、UserName、Pwd均填""</param>
        /// <param name="Ip">服务器Ip</param>
        /// <param name="UserName">登陆名</param>
        /// <param name="Pwd">密码</param>
        public void InsertProtoco(List<DgnProtocolInfo> Items, string AccessPath, string Ip, string UserName, string Pwd)
        {
            List<string> _InsertDataBaseSQL = new List<string>();
            string Sql = "";
            if (Items.Count < 1) return;
            for (int i = 0; i < Items.Count; i++)
            {
                DgnProtocolInfo Item = Items[i];
                Sql = string.Format("Insert into ProtocolInfo (ProtocolID,proNameNo,chrPname,intInfoNo,chrValue) Values "
                    + "({0},{1},'{2}',{3},'{4}')", Item.Pro_ProtocolID,Item.Pro_proNameNo,Item.Pro_chrPname,Item.Pro_intInfoNo,Item.Pro_chrValue);
                _InsertDataBaseSQL.Add(Sql);
            }
            if (Ip != "")
                InsertProDLT(_InsertDataBaseSQL, "", Ip, UserName, Pwd);
            else if (AccessPath != "")
                InsertProDLT(_InsertDataBaseSQL, AccessPath, "", "", "");
            else
                InsertProDLT(_InsertDataBaseSQL, "", "", "", "");   
        }
        /// <summary>
        /// 增加协议标识字典
        /// AccessPath、Ip、UserName、Pwd均为""时为本地默认路径数据库
        /// </summary>
        /// <param name="Items">增加的数据</param>
        /// <param name="AccessPath">本地数据库路径，填写此项则Ip、UserName、Pwd均填""</param>
        /// <param name="Ip">服务器Ip</param>
        /// <param name="UserName">登录名</param>
        /// <param name="Pwd">密码</param>
        public void InsertDict(List<DgnProtocolInfo> Items, string AccessPath, string Ip, string UserName, string Pwd)
        {
            List<string> _InsertDataBaseSQL = new List<string>();
            string Sql = "";
            if (Items.Count < 1) return;
            for (int i = 0; i < Items.Count; i++)
            {
                DgnProtocolInfo Item = Items[i];
                Sql = string.Format("Insert into ProDLT645Dict (dltID,proNameNo,intIdentType,intClass,chrItemName,chrID,intLength,intDot,chrFormat,intType,chrDefValue) Values "
                    + "({0},{1},{2},{3},'{4}','{5}',{6},{7},'{8}',{9},'{10}')", Item.Pd_dltID, Item.Pd_proNameNo, Item.Pd_intIdentType, Item.Pd_intClass, Item.Pd_chrItemName,
                    Item.Pd_chrID, Item.Pd_intLength, Item.Pd_intDot, Item.Pd_chrFormat, Item.Pd_intType, Item.Pd_chrDefValue);
                _InsertDataBaseSQL.Add(Sql);
            }
            if (Ip != "")
                InsertProDLT(_InsertDataBaseSQL, "", Ip, UserName, Pwd);
            else if (AccessPath != "")
                InsertProDLT(_InsertDataBaseSQL, AccessPath, "", "", "");
            else
                InsertProDLT(_InsertDataBaseSQL, "", "", "", "");
        }
        /// <summary>
        /// 增加数据
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="AccessPath"></param>
        /// <param name="Ip"></param>
        /// <param name="UserName"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        private void InsertProDLT(List<string> InsertSql, string AccessPath, string Ip, string UserName, string Pwd)
        {
            bool flag = false;
            if (Ip != "")
                flag = ConnectDataBase("", Ip, UserName, Pwd);
            else if (AccessPath != "")
                flag = ConnectDataBase(AccessPath, "", "", "");
            else
                flag = ConnectDataBase("", "", "", "");

            if (!flag)
                return;
            for (int i = 0; i < InsertSql.Count; i++)
            {
                System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(InsertSql[i], _Data.Con);
                Cmd.ExecuteNonQuery();
                Cmd = null;
            }
        }
        #endregion
    }        
}

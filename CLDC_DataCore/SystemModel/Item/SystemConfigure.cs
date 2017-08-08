using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.DataBase;
using System.Xml;
using System.Windows.Forms;
using System.Net;

namespace CLDC_DataCore.SystemModel.Item
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemConfigure
    {
        /// <summary>
        /// 系统设置信息配置
        /// </summary>
        private Dictionary<string, CLDC_DataCore.Struct.StSystemInfo> _SystemMode;

        /// <summary>
        /// 
        /// </summary>
        public SystemConfigure()
        {
            _SystemMode = new Dictionary<string, CLDC_DataCore.Struct.StSystemInfo>();
        }
        /// <summary>
        /// 
        /// </summary>
        ~SystemConfigure()
        {
            _SystemMode = null;
        }
        /// <summary>
        /// 初始化系统配置信息
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            _SystemMode.Clear();            //清空系统配置集合
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_SYSTEMPATH, out _ErrorString);
            if (_ErrorString != "")
            {
                _XmlNode = clsXmlControl.CreateXmlNode("SystemInfo");
                #region 加载系统配置默认值
                this.CreateDefaultData(ref _XmlNode);
                #endregion

                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_SYSTEMPATH);
            }

            if (_XmlNode.ChildNodes.Count > 0)
            {
                if (_XmlNode.ChildNodes[0].Attributes.Count < 6)
                {
                    CLDC_DataCore.Function.File.RemoveFile(Application.StartupPath + Const.Variable.CONST_SYSTEMPATH);   //如果发现旧的系统配置文件就要删除掉，再重新创建
                    this.Load();
                    return;
                }
            }
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                CLDC_DataCore.Struct.StSystemInfo _Item = new CLDC_DataCore.Struct.StSystemInfo();

                _Item.Value = _XmlNode.ChildNodes[_i].Attributes[1].Value;       //项目值
                _Item.Name = _XmlNode.ChildNodes[_i].Attributes[2].Value;       //项目中文名称
                _Item.Description = _XmlNode.ChildNodes[_i].Attributes[3].Value;      //项目描述
                _Item.ClassName = _XmlNode.ChildNodes[_i].Attributes[4].Value;  //项目分类名称
                _Item.DataSource = _XmlNode.ChildNodes[_i].Attributes[5].Value; //数据源
                if (_SystemMode.ContainsKey(_XmlNode.ChildNodes[_i].Attributes[0].Value))
                    _SystemMode.Remove(_XmlNode.ChildNodes[_i].Attributes[0].Value);
                _SystemMode.Add(_XmlNode.ChildNodes[_i].Attributes[0].Value, _Item);
            }
        }
        /// <summary>
        /// 读取系统配置项目值
        /// </summary>
        /// <param name="Tkey">系统项目ID</param>
        /// <returns>系统项目配置值</returns>
        public string getValue(string Tkey)
        {
            if (_SystemMode.Count == 0)
                return "";
            if (_SystemMode.ContainsKey(Tkey))
                return _SystemMode[Tkey].Value;
            else
                return "";
        }
        /// <summary>
        /// 获取系统配置项目结构体
        /// </summary>
        /// <param name="Tkey">系统项目ID</param>
        /// <returns></returns>
        public CLDC_DataCore.Struct.StSystemInfo getItem(string Tkey)
        {
            if (_SystemMode.Count == 0)
                return new CLDC_DataCore.Struct.StSystemInfo();
            if (_SystemMode.ContainsKey(Tkey))
                return _SystemMode[Tkey];
            else
                return new CLDC_DataCore.Struct.StSystemInfo();
        }

        /// <summary>
        /// 添加系统配置项目
        /// </summary>
        /// <param name="Tkey">系统项目名称</param>
        /// <param name="Item">系统项目配置值</param>
        public void Add(string Tkey, CLDC_DataCore.Struct.StSystemInfo Item)
        {
            if (_SystemMode.ContainsKey(Tkey))
            {
                _SystemMode.Remove(Tkey);
                _SystemMode.Add(Tkey, Item);
            }
            else
                _SystemMode.Add(Tkey, Item);
            return;
        }

        /// <summary>
        /// 修改键值
        /// </summary>
        /// <param name="Tkey">关键字</param>
        /// <param name="TValue">修改的值</param>
        public bool EditValue(string Tkey, string TValue)
        {
            if (_SystemMode.ContainsKey(Tkey))
            {
                CLDC_DataCore.Struct.StSystemInfo _Item = _SystemMode[Tkey];
                _Item.Value = TValue;
                _SystemMode.Remove(Tkey);
                _SystemMode.Add(Tkey, _Item);
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// 系统配置项目个数
        /// </summary>
        public int Count
        {
            get
            {
                return _SystemMode.Count;
            }
        }

        /// <summary>
        /// 获取关键字列表
        /// </summary>
        /// <returns></returns>
        public List<string> getKeyNames()
        {
            List<string> _Keys = new List<string>();
            foreach (string _name in _SystemMode.Keys)
            {
                _Keys.Add(_name);
            }
            return _Keys;
        }
        /// <summary>
        /// 清空列表
        /// </summary>
        public void Clear()
        {
            _SystemMode.Clear();
        }


        /// <summary>
        /// 存储系统配置XML文档
        /// </summary>
        public void Save()
        {
            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("", "SystemInfo");
            foreach (string _Key in _SystemMode.Keys)
            {
                _Xml.appendchild(""
                                , "R"
                                , "Item", _Key
                                , "Value", _SystemMode[_Key].Value
                                , "Name", _SystemMode[_Key].Name
                                , "Description", _SystemMode[_Key].Description
                                , "ClassName", _SystemMode[_Key].ClassName
                                , "DataSource", _SystemMode[_Key].DataSource);
            }
            _Xml.SaveXml(Application.StartupPath + Const.Variable.CONST_SYSTEMPATH);
        }
        /// <summary>
        /// 创建系统默认配置文件
        /// </summary>
        /// <param name="xml"></param>
        private void CreateDefaultData(ref XmlNode xml)
        {

            #region 台体功能配置
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                            , "Item", CLDC_DataCore.Const.Variable.CTC_BWCOUNT
                                            , "Value", "6"
                                            , "Name", "台体表位数"
                                            , "Description", "台体表位数，该数量应和台架上表位数量一致"
                                            , "ClassName", "台体功能配置"
                                            , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                , "Item", CLDC_DataCore.Const.Variable.CTC_ISDEMO
                                , "Value", "演示模式"
                                , "Name", "运行模式"
                                , "Description", "系统运行模式，演示模式不会连接设备，只产生演示数据"
                                , "ClassName", "台体功能配置"
                                , "DataSource", "正式模式|演示模式"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                            , "Item", CLDC_DataCore.Const.Variable.CTC_DESKNO
                                            , "Value", "1"
                                            , "Name", "台体编号"
                                            , "Description", "台体编号，该编号直接影响到集控状态下和在服务器上的显示位置"
                                            , "ClassName", "台体功能配置"
                                            , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R", "Item", CLDC_DataCore.Const.Variable.CTC_DESKNAME
                                            , "Value", "未命名"
                                            , "Name", "台体名称"
                                            , "Description", "台体名称（如A区三台体)"
                                            , "ClassName", "台体功能配置"
                                            , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                            , "Item", CLDC_DataCore.Const.Variable.CTC_ISCONTROL
                                            , "Value", "被控"
                                            , "Name", "控制类型"
                                            , "Description", "台体控制类型，被控-标志在集控状态；主控-脱机状态单机控制。请勿擅自修改"
                                            , "ClassName", "台体功能配置"
                                            , "DataSource", "被控|主控"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                , "Item", CLDC_DataCore.Const.Variable.CTC_SEBIAO
                                , "Value", "不支持"
                                , "Name", "色标功能"
                                , "Description", "台体的对色标功能"
                                , "ClassName", "台体功能配置"
                                , "DataSource", "不支持|电压|电流"));


            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                            , "Item", CLDC_DataCore.Const.Variable.CTC_DESKTYPE
                                            , "Value", "三相台"
                                            , "Name", "台体类型"
                                            , "Description", "当前检定装置类型，可为三相台或是单相台"
                                            , "ClassName", "台体功能配置"
                                            , "DataSource", "三相台|单相台"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                            , "Item", CLDC_DataCore.Const.Variable.CTC_STDMETERREADTIME
                                            , "Value", "2000"
                                            , "Name", "标准表读取时间间隔"
                                             , "Description", "标准表读取时间间隔 以毫秒计算"
                                            , "ClassName", "台体功能配置"
                                            , "DataSource", ""));
            #endregion

            #region 网络信息配置
            string strIP = "127.0.0.1";
            IPHostEntry IPEntry = Dns.GetHostEntry(Dns.GetHostName());
            
            foreach (IPAddress item in IPEntry.AddressList)
            {
                if (!item.IsIPv6LinkLocal)
                {
                    strIP = item.ToString();
                }
            }

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                    , "Item", CLDC_DataCore.Const.Variable.CTC_SERVERIP
                    , "Value", strIP
                    , "Name", "集控服务器IP"
                    , "Description", "服务器地址，请勿擅自更改"
                    , "ClassName", "网络信息配置"
                    , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                    , "Item", CLDC_DataCore.Const.Variable.CTN_SERVERPORT
                    , "Value", "8536"
                    , "Name", "集控服务器端口"
                    , "Description", "服务器监听端口，请勿擅自更改，确认防火墙已放行该端口"
                    , "ClassName", "网络信息配置"
                    , "DataSource", ""));
            /*网络数据库服务器配置*/
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                      , "Item", CLDC_DataCore.Const.Variable.CTC_SQL_SERVERIP
                                      , "Value", "192.168.32.1,1433"
                                      , "Name", "网络数据库服务IP"
                                      , "Description", "SQL数据库服务器IP地址或是主机名，请勿擅自更改"
                                      , "ClassName", "网络信息配置"
                                      , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                      , "Item", CLDC_DataCore.Const.Variable.CTC_SQL_USERID
                                      , "Value", "sa"
                                      , "Name", "网络数据库用户名"
                                      , "Description", "登录SQL数据库的用户名，一般由系统管理员分配，默认为SA，请勿擅自更改"
                                      , "ClassName", "网络信息配置"
                                      , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                                , "Item", CLDC_DataCore.Const.Variable.CTC_SQL_PASSWORD
                                                , "Value", "CDClou001"
                                                , "Name", "网络数据库密码"
                                                , "Description", "登录SQL数据库的密码，请勿擅自更改"
                                                , "ClassName", "网络信息配置"
                                                , "DataSource", ""));



            #endregion

            #region ----------加密机参数---------

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_ENCRYPTION_DEFAULTTYPE
                   , "Value", "服务器类型09"
                   , "Name", "加密机类型"
                   , "Description", "加密机类型,默认服务器类型13。"
                   , "ClassName", "加密机配置"
                   , "DataSource", "服务器类型09|服务器类型13|简装类型|开发套件|融通加密机|企业服务器型"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_ENCRYPTION_MACHINEIP
                   , "Value", "10.98.97.254"
                   , "Name", "加密机IP"
                   , "Description", "加密机IP"
                   , "ClassName", "加密机配置"
                   , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_ENCRYPTION_MACHINEPORT
                   , "Value", "6666"
                   , "Name", "加密机端口"
                   , "Description", "加密机端口"
                   , "ClassName", "加密机配置"
                   , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_ENCRYPTION_MACHINEUSBKEY
                   , "Value", "11111111"
                   , "Name", "加密机USBKEY"
                   , "Description", "加密机USBKEY"
                   , "ClassName", "加密机配置"
                   , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_ENCRYPTION_STATUS
                   , "Value", "公钥"
                   , "Name", "默认认证状态"
                   , "Description", "身份认证时，被检表的默认认证状态。"
                   , "ClassName", "加密机配置"
                   , "DataSource", "公钥|私钥"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_ENCRYPTION_MACHINEISAUTOLINK
                   , "Value", "否"
                   , "Name", "是否进行密码机服务器连接"
                   , "Description", "是否默认进行密码机服务器连接"
                   , "ClassName", "加密机配置"
                   , "DataSource", "是|否"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_ENCRYPTION_MAXWAITDATABACKTIME
                   , "Value", "45"
                   , "Name", "费控项目操作最大等待返回时间"
                   , "Description", "费控项目操作后等待加密机返回的时间。建议不要太长。"
                   , "ClassName", "加密机配置"
                   , "DataSource", ""));

            #endregion

            #region ----------标准器参数---------

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_DRIVERF
                   , "Value", "1"
                   , "Name", "标准分频系数"
                   , "Description", "标准表标准分频系统，请查阅标准表相关说明。"
                   , "ClassName", "标准器配置"
                   , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_DRIVERFPULSES
                   , "Value", "固定常数"
                   , "Name", "标准表常数模式"
                   , "Description", "标准表常数，固定常数、自动常数。"
                   , "ClassName", "标准器配置"
                   , "DataSource", "固定常数|自动常数"));
            #endregion

            #region ----------误差检定配置------------
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_WC_TIMES_BASICERROR
                   , "Value", "2"
                   , "Name", "误差计算取值数"
                   , "Description", "每个误差点取几次误差参与计算"
                   , "ClassName", "检定配置"
                   , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                      , "Item", CLDC_DataCore.Const.Variable.CTC_WC_TIMES_WINDAGE
                      , "Value", "5"
                      , "Name", "标准偏差计算取值数"
                      , "Description", "每个标准偏差取几次误差参与计算"
                      , "ClassName", "检定配置"
                      , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                        , "Item", CLDC_DataCore.Const.Variable.CTC_WC_MAXTIMES
                        , "Value", "5"
                        , "Name", "最大处理次数"
                        , "Description", "每个误差点最大处理次数，请确保此数值大于标准偏差次数"
                        , "ClassName", "检定配置"
                        , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                        , "Item", CLDC_DataCore.Const.Variable.CTC_WC_MAXSECONDS
                        , "Value", "120"
                        , "Name", "最大处理时间(秒)"
                        , "Description", "每个误差点最大处理时间"
                        , "ClassName", "检定配置"
                        , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                       , "Item", CLDC_DataCore.Const.Variable.CTC_WC_JUMP
                       , "Value", "1"
                       , "Name", "跳差判定倍数"
                       , "Description", "当二次误差值相差大于此倍数X表等级时此点误差时此点不合格"
                       , "ClassName", "检定配置"
                       , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                      , "Item", CLDC_DataCore.Const.Variable.CTC_WC_IN
                      , "Value", "5"
                      , "Name", "互感器二次电流"
                      , "Description", "当被检表是经互感器接入时，互感器二次电流大小。一般为5A."
                      , "ClassName", "检定配置"
                      , "DataSource", ""));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                      , "Item", CLDC_DataCore.Const.Variable.CTC_WC_AVGPRECISION
                      , "Value", "4"
                      , "Name", "平均值小数位"
                      , "Description", "检定时误差平均值保留小数位数"
                      , "ClassName", "检定配置"
                      , "DataSource", ""));


            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                            , "Item", CLDC_DataCore.Const.Variable.CTC_CHANGEDATA
                                            , "Value", "否"
                                            , "Name", "允许修改检定数据"
                                            , "Description", "选择是，则在审核存盘的时候可修改试验数据"
                                            , "ClassName", "检定配置"
                                            , "DataSource", "是|否"));
            #endregion

            #region---------多功能检定参数----------
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                   , "Item", CLDC_DataCore.Const.Variable.CTC_DGN_POWERON_ATTERTIME
                   , "Value", "3"
                   , "Name", "多功能检定源稳定时间"
                   , "Description", "多功能检定时升源稳定时间，一般根据电能表在通电后多长时间能够进入电表系统而定,单位（秒）"
                   , "ClassName", "多功能检定参数配置"
                   , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                  , "Item", CLDC_DataCore.Const.Variable.CTC_DGN_WRITEMETERALARM
                  , "Value", "是"
                  , "Name", "对表进行写操作时提示"
                  , "Description", "当系统需要对表进行写操作时，提示操作人员打开编程开关"
                  , "ClassName", "多功能检定参数配置"
                  , "DataSource", "是|否"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                  , "Item", CLDC_DataCore.Const.Variable.CTC_DGN_RJSVERIFYTYPE
                  , "Value", "快速模式"
                  , "Name", "日计时误差试验方式"
                  , "Description", "快速模式测试时间为2分钟，标准模式测试时间为10分钟"
                  , "ClassName", "多功能检定参数配置"
                  , "DataSource", "快速模式|标准模式"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                  , "Item", CLDC_DataCore.Const.Variable.CTC_DGN_READDATAFROMRS485
                  , "Value", "使用485自动读取"
                  , "Name", "走字电量输入方式"
                  , "Description", "在进行走字时，如何确定起始电量"
                  , "ClassName", "多功能检定参数配置"
                  , "DataSource", "使用485自动读取|手动输入"));
            #endregion

            #region-----------载波检定参数------------------
            //xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            //        , "Item", CLDC_DataCore.Const.Variable.CTC_ZB_MODELTYPE
            //        , "Value", "2034"
            //        , "Name", "载波模块类型"
            //        , "Description", "载波模块类型，2034|2041"
            //        , "ClassName", "载波检定参数配置"
            //        , "DataSource", "2034|2041"));
            //xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            //        , "Item", CLDC_DataCore.Const.Variable.CTC_ZB_COMMUNICATIONTYPE
            //        , "Value", "CSerialCom"
            //        , "Name", "载波检定通讯方式"
            //        , "Description", "载波通讯选择的通讯方式，连接2018（CCL20181）还是普通的PC串口（CSerialCom）。"
            //        , "ClassName", "载波检定参数配置"
            //        , "DataSource", "CCL20181|CSerialCom"));

            //xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            //        , "Item", CLDC_DataCore.Const.Variable.CTC_ZB_READERTYPE
            //        , "Value", "东软"
            //        , "Name", "抄表器类型"
            //        , "Description", "抄表器类型，东软|鼎信|晓程|瑞斯康"
            //        , "ClassName", "载波检定参数配置"
            //        , "DataSource", "东软|鼎信|晓程|瑞斯康"));

            //xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            //        , "Item", CLDC_DataCore.Const.Variable.CTC_ZB_READERBAUDRATE
            //        , "Value", "9600,e,8,1"
            //        , "Name", "抄表器波特率"
            //        , "Description", "载波主板同抄表器的通讯波特率，此处设置的值要与选择的载波协议中设置的波特率一致。"
            //        , "ClassName", "载波检定参数配置"
            //        , "DataSource", ""));

            //xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            //        , "Item", CLDC_DataCore.Const.Variable.CTC_ZB_PARAMETER
            //        , "Value", "4,0.0.0.0:0:0"
            //        , "Name", "通讯参数"
            //        , "Description", "载波通讯参数（格式为：串口号+IP地址和端口号，PC直接通讯时用0.0.0.0:0:0表示，2018默认通讯IP为193.168.18.1:10003:20000。）。"
            //        , "ClassName", "载波检定参数配置"
            //        , "DataSource", ""));

            //xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            //        , "Item", CLDC_DataCore.Const.Variable.CTC_ZB_CHANNELNO
            //        , "Value", "2通道"
            //        , "Name", "通道号"
            //        , "Description", "抄表器所在的通道号，此处设置的值要与选择的载波协议中设置的通道号一致。"
            //        , "ClassName", "载波检定参数配置"
            //        , "DataSource", "0通道|1通道|2通道|3通道|4通道"));

            //xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            //        , "Item", CLDC_DataCore.Const.Variable.CTC_ZB_PHASE
            //        , "Value", "A相"
            //        , "Name", "电压相位"
            //        , "Description", "载波通讯时所加载到的电压相位。"
            //        , "ClassName", "载波检定参数配置"
            //        , "DataSource", "全断开|A相|B相|C相"));

            //xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            //        , "Item", CLDC_DataCore.Const.Variable.CTC_ZB_MODULE
            //        , "Value", "1"
            //        , "Name", "模块"
            //        , "Description", "暂未启用。"
            //        , "ClassName", "载波检定参数配置"
            //        , "DataSource", ""));

            //xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            //        , "Item", CLDC_DataCore.Const.Variable.CTC_ZB_PIN
            //        , "Value", "1"
            //        , "Name", "引脚"
            //        , "Description", "暂未启用。"
            //        , "ClassName", "载波检定参数配置"
            //        , "DataSource", ""));

            #endregion

            #region 其它配置
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                    , "Item", CLDC_DataCore.Const.Variable.CTC_DISPLAY_CHECKDESCRIPTION
                    , "Value", "否"
                    , "Name", "是否显示检定流程描述"
                    , "Description", "鼠标停留在业务名称上时，显示检定流程的描述!"
                    , "ClassName", "其它配置"
                    , "DataSource", "是|否"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                    , "Item", CLDC_DataCore.Const.Variable.CTC_OTHER_SELECTPROTOCOLFROMMETERFACTROY
                    , "Value", "是"
                    , "Name", "是否自动选择通信协议"
                    , "Description", "根据被检表制造厂家和表型号进行自动选择！！如果不使用自动选择，则需手工选择多功能试验时的通信协议!"
                    , "ClassName", "其它配置"
                    , "DataSource", "是|否"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                    , "Item", CLDC_DataCore.Const.Variable.CTC_OTHER_EQUIPSET_WAITTIME
                    , "Value", "100"
                    , "Name", "参数设置时间间隔"
                    , "Description", "系统每次设置台体参数后的时间间隔，可根据硬件实际情况调整。太小可能会影响正常检定，太大则会影响检定速度"
                    , "ClassName", "其它配置"
                    , "DataSource", ""));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_OTHER_DATAMANAGEEXEPATH
                , "Value", "/CLDC_DataManager.exe"
                , "Name", "数据管理程序路径"
                , "Description", "报表管理程序路径，一般不要改动"
                , "ClassName", "其它配置"
                , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_OTHER_DATAMANAGEPROCESSNAME
                , "Value", "ClDataManager"
                , "Name", "数据管理程序进程名"
                , "Description", "用于限制多个数据管理程序同时运行。建议不要修改"
                , "ClassName", "其它配置"
                , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_OTHER_PROTOCOLEXENAME
                , "Value", "/CLDC_MeterProtocolSetup.exe"
                , "Name", "多功能协议配置程序路径"
                , "Description", "多功能协议配置程序的相对或是绝对路径，建议不要修改"
                , "ClassName", "其它配置"
                , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_OTHER_PROTOCOLPROCESS
                , "Value", "CLDC_MeterProtocolSetup"
                , "Name", "多功能协议配置程序进程名"
                , "Description", "用于限制多个数据管理程序同时运行。建议不要修改"
                , "ClassName", "其它配置"
                , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_OTHER_PREFIXOFCERTIFICATEN
                , "Value", "CLOU"
                , "Name", "证书编号前缀"
                , "Description", "证书编号前缀"
                , "ClassName", "其它配置"
                , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
        , "Item", CLDC_DataCore.Const.Variable.CTC_AUTO_ISHAVECHECKMETER
        , "Value", "否"
        , "Name", "是否自动检测表位有无挂表"
        , "Description", "自动检测表位有无挂表，如果选择是，在参数录入是会把有挂表的表位提示出来!"
        , "ClassName", "其它配置"
        , "DataSource", "是|否"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
, "Item", CLDC_DataCore.Const.Variable.CTC_CHECK_COMM
, "Value", "是"
, "Name", "是否校验通信地址"
, "Description", "在做通信测试时是否校验通信地址!"
, "ClassName", "其它配置"
, "DataSource", "是|否"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
, "Item", CLDC_DataCore.Const.Variable.CTC_ADRRESS_LENGTH
, "Value", "8"
, "Name", "截取资产编号后几位"
, "Description", "除了载波表截取资产编号后几位，作为通信地址!"
, "ClassName", "其它配置"
, "DataSource", ""));
            #endregion

            #region  接口相关 营销接口配置

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_MIS_INTERFACETYPE
                , "Value", "生产调度系统"
                , "Name", "营销接口类型"
                , "Description", "营销接口类型"
                , "ClassName", "营销接口配置"
                , "DataSource", "东软SG186|普华SG186|生产调度系统|天津MIS接口|外部可执行程序接口|外部动态库接口"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_BasisDownInfo
                , "Value", "条形码"
                , "Name", "下载参数标识"
                , "Description", "选择依据“条形码”、或“出厂编号”等作为标识下载参数。"
                , "ClassName", "营销接口配置"
                , "DataSource", "条形码|出厂编号|表位号"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_MIS_DATASOURCE
                , "Value", "MCP_PB_TEST"
                , "Name", "营销系统数据源"
                , "Description", "营销系统数据源不要更改"
                , "ClassName", "营销接口配置"
                , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_MIS_USERID
                , "Value", "sxykjd"
                , "Name", "营销系统数据库用户名"
                , "Description", "营销系统数据库用户名不要更改"
                , "ClassName", "营销接口配置"
                , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_MIS_PASSWORD
                , "Value", "sxykjd"
                , "Name", "营销系统数据库密码"
                , "Description", "连接营销数据库数据库密码不要更改"
                , "ClassName", "营销接口配置"
                , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_MIS_WEBSERVICE_URL
                , "Value", "http://10.166.5.7:9000/InterfaceWS/InterfaceBusiness/services/DetectService"
                , "Name", "营销系统WebService"
                , "Description", "营销系统WebService不要更改"
                , "ClassName", "营销接口配置"
                , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                , "Item", CLDC_DataCore.Const.Variable.CTC_MIS_NOWDATA
                , "Value", "是"
                , "Name", "营销系统实时数据"
                , "Description", "连接营销实时数据"
                , "ClassName", "营销接口配置"
                , "DataSource", "是|否"));

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                    , "Item", CLDC_DataCore.Const.Variable.CTC_MIS_EXENAME
                    , "Value", "条形码解析.exe SZCLOU"
                    , "Name", "外部程序名称"
                    , "Description", "外部独立EXE程序,特殊流程：通过“换新表”按钮调用；解析条形码解析出被检表铭牌参数，并写入中间临时数据库；然后再通过“下载信息”按钮刷新数据"
                    , "ClassName", "营销接口配置"
                    , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                    , "Item", CLDC_DataCore.Const.Variable.CTC_MIS_EXEPROCESSNAME
                    , "Value", "Prj_DY"
                    , "Name", "外部程序进程名称"
                    , "Description", "外部独立EXE程序进程名称，用于限制多次运行冲突。"
                    , "ClassName", "营销接口配置"
                    , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                    , "Item", CLDC_DataCore.Const.Variable.CTC_INTERFACE_DllNAME
                    , "Value", ""
                    , "Name", "外部组件名称"
                    , "Description", "外部动态库名称，根据条形码解析出被检表铭牌参数，解析所需要用到的组件"
                    , "ClassName", "营销接口配置"
                    , "DataSource", ""));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                    , "Item", CLDC_DataCore.Const.Variable.CTC_INTERFACE_DLLCLASS
                    , "Value", ""
                    , "Name", "外部组件类"
                    , "Description", "外部动态库类名，根据条形码解析出被检表铭牌参数，解析所需要用到的组件组件类"
                    , "ClassName", "营销接口配置"
                    , "DataSource", ""));
            #endregion

            #region-----------检定标准-----------
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                            , "Item", CLDC_DataCore.Const.Variable.CTG_ELEC_GC
                                            , "Value", "JJG596-1997"
                                             , "Name", "电子表规程"
                                            , "Description", "电子式电能表检定参照规程"
                                            , "ClassName", "检定标准"
                                            , "DataSource", "JJG596-1999|JJG596-2006|JJG596-2012"));
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                                            , "Item", CLDC_DataCore.Const.Variable.CTG_VICA_GC
                                            , "Value", "JJG307-1988"
                                             , "Name", "感应表规程"
                                            , "Description", "感应式、机械表检定参照规程"
                                            , "ClassName", "检定标准"
                                            , "DataSource", "JJG307-1988|JJG307-2006"));

            #endregion

            #region ----------走字检定参数----------
            // [默认],不做任何排序，直接按方案项目顺序走字
            // [总与分费率同时做],选择此项后先读取总的起码，分费率做完后再读取总的止码.当做组合误差时此项默认选择.
            // [自动检定总时段内的所有分费率]。此选项适用于24小时走字，及固定费率与同步费率混合走字。

            //xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            //         , "Item", CLDC_DataCore.Const.Variable.CTC_ZZ_SELECTITEM
            //         , "Value", "默认"
            //         , "Name", "走字试验方式"
            //         , "Description", "默认,不做任何处理，直接按方案项目顺序走字;总与分费率同时做,选择此项后先读取总的起码，分费率做完后再读取总的止码.当做组合误差时此项默认选择;自动检定总时段内的所有分费率。此选项适用于24小时走字，及固定费率与同步费率混合走字。"
            //         , "ClassName", "检定配置"
            //         , "DataSource", "默认|总与分费率同时做|自动检定总时段内的所有分费率"));
            //xml.AppendChild(clsXmlControl.CreateXmlNode("R"
            //         , "Item", CLDC_DataCore.Const.Variable.CTC_ZZ_UNINERROR
            //         , "Value", "是"
            //         , "Name", "走字做组合误差"
            //         , "Description", "是否做组合误差，选择做组合误差后建议走字方案中添加'总'走字项目"
            //         , "ClassName", "检定配置"
            //         , "DataSource", "是|否"));

            #endregion

            #region ------标准表证书------------
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                  , "Item", CLDC_DataCore.Const.Variable.CTC_STDMETER_NAME
                  , "Value", "标准表名称SZ-K6D"
                  , "Name", "标准表名称"
                  , "Description", "标准表名称"
                  , "ClassName", "装置的标准表证书"
                  , "DataSource", "")
                  );
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_STDMETER_SIZE
                 , "Value", "标准表型号SZ-K6D"
                 , "Name", "标准表型号"
                 , "Description", "标准表型号"
                 , "ClassName", "装置的标准表证书"
                 , "DataSource", "")
                 );
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_STDMETER_NO
                 , "Value", "标准表编号SZ-K6D"
                 , "Name", "标准表编号"
                 , "Description", "标准表编号"
                 , "ClassName", "装置的标准表证书"
                 , "DataSource", "")
                 );

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_STDMETERRANGE_START
                 , "Value", "标准表测量起始值SZ-K6D"
                 , "Name", "标准表测量起始值"
                 , "Description", "标准表测量起始值"
                 , "ClassName", "装置的标准表证书"
                 , "DataSource", "")
                 );
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_STDMETERRANGE_END
                 , "Value", "标准表测量结束值SZ-K6D"
                 , "Name", "标准表测量结束值"
                 , "Description", "标准表测量结束值"
                 , "ClassName", "装置的标准表证书"
                 , "DataSource", "")
                 );

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_STDMETER_ERROR
                 , "Value", "标准表不确定度SZ-K6D"
                 , "Name", "标准表不确定度"
                 , "Description", "标准表不确定度"
                 , "ClassName", "装置的标准表证书"
                 , "DataSource", "")
                 );
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_STDMETER_ASSNO
                 , "Value", "标准表证书编号SZ-K6D"
                 , "Name", "标准表证书编号"
                 , "Description", "标准表编号"
                 , "ClassName", "装置的标准表证书"
                 , "DataSource", "")
                 );
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_STDMETER_EXPERDATE
                 , "Value", "标准表过期日间" + DateTime.Now.AddYears(20).ToString()
                 , "Name", "标准表过期日间"
                 , "Description", "标准表过期日间"
                 , "ClassName", "装置的标准表证书"
                 , "DataSource", "")
                 );
            #endregion

            #region ------校表台证书------------

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_EQUIPMENT_NAME
                 , "Value", "装置名称1981D"
                 , "Name", "装置名称"
                 , "Description", "装置名称"
                 , "ClassName", "装置证书"
                 , "DataSource", "")
                 );

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
               , "Item", CLDC_DataCore.Const.Variable.CTC_EQUIPMENT_SIZE
               , "Value", "装置型号1981D"
               , "Name", "装置型号"
               , "Description", "装置型号"
               , "ClassName", "装置证书"
               , "DataSource", "")
               );
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_EQUIPMENT_NO
                 , "Value", "装置编号1981D"
                 , "Name", "装置编号"
                 , "Description", "装置编号"
                 , "ClassName", "装置证书"
                 , "DataSource", "")
                 );

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_EQUIPMENTRANGE_START
                 , "Value", "装置测量起始值1981D"
                 , "Name", "装置测量起始值"
                 , "Description", "装置测量起始值"
                 , "ClassName", "装置证书"
                 , "DataSource", "")
                 );

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_EQUIPMENTRANGE_END
                 , "Value", "装置测量结束值1981D"
                 , "Name", "装置测量结束值"
                 , "Description", "装置测量结束值"
                 , "ClassName", "装置证书"
                 , "DataSource", "")
                 );
            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_EQUIPMENT_ERROR
                 , "Value", "装置不确定度1981D"
                 , "Name", "装置不确定度"
                 , "Description", "装置不确定度"
                 , "ClassName", "装置证书"
                 , "DataSource", "")
                 );

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_EQUIPMENT_ASSNO
                 , "Value", "装置证书编号1981D"
                 , "Name", "装置证书编号"
                 , "Description", "装置证书编号"
                 , "ClassName", "装置证书"
                 , "DataSource", "")
                 );

            xml.AppendChild(clsXmlControl.CreateXmlNode("R"
                 , "Item", CLDC_DataCore.Const.Variable.CTC_EQUIPMENT_EXPERDATE
                 , "Value", "装置有效时期" + DateTime.Now.AddYears(20).ToString()
                 , "Name", "装置有效时期"
                 , "Description", "装置有效时期"
                 , "ClassName", "装置证书"
                 , "DataSource", "")
                 );

            #endregion

        }

    }
}

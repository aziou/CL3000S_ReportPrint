using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_DataCore.DataBase;
using System.Windows.Forms;

namespace CLDC_DataCore.SystemModel.Item
{
    /// <summary>
    /// 
    /// </summary>
    public class csEventLogDic
    {
        /// <summary>
        /// 事件记录配置字典
        /// </summary>
        private Dictionary<string, Struct.StEventLogConfig> _EventLogConfig;
        /// <summary>
        /// 构造函数
        /// </summary>
        public csEventLogDic()
        {
            _EventLogConfig = new Dictionary<string, CLDC_DataCore.Struct.StEventLogConfig>();
        }
        /// <summary>
        /// 
        /// </summary>
        ~csEventLogDic()
        {
            _EventLogConfig = null;
        }
        /// <summary>
        /// 加载事件记录配置字典
        /// </summary>
        public void Load()
        {
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + Const.Variable.CONST_EVENTLOGDICTIONARY, out _ErrorString);
            if (_ErrorString != "" || _XmlNode.ChildNodes.Count < 26)             //新增加5条
            {
                #region 初始化事件记录参数信息
                _XmlNode = clsXmlControl.CreateXmlNode("EventLogConfig");
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "001", "Name", "失压记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "002", "Name", "过压记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "003", "Name", "欠压记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "004", "Name", "失流记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "005", "Name", "断流记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "006", "Name", "过流记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "007", "Name", "过载记录", "OutPramerter", "1|1|1|Imax|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "008", "Name", "断相记录", "OutPramerter", "1|1|1|Imax|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "009", "Name", "掉电记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "010", "Name", "全失压记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "011", "Name", "电压不平衡记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "012", "Name", "电流不平衡记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "013", "Name", "电压逆相序记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "014", "Name", "电流逆相序记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "015", "Name", "开表盖记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "016", "Name", "开端钮盒记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "017", "Name", "编程记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "018", "Name", "校时记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "019", "Name", "需量清零记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "020", "Name", "事件清零记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "021", "Name", "电表清零记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "022", "Name", "潮流反向记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "023", "Name", "功率反向记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "024", "Name", "需量超限记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "025", "Name", "功率因数超下限记录", "OutPramerter", "1|1|1|0Ib|1.0"));
                                
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "026", "Name", "过流(载波)记录", "OutPramerter", "1|1|1|0Ib|1.0"));

                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + Const.Variable.CONST_EVENTLOGDICTIONARY);
                #endregion

            }
            _EventLogConfig.Clear();
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                Struct.StEventLogConfig _EventLog = new CLDC_DataCore.Struct.StEventLogConfig();
                _EventLog.EventLogPrjID = _XmlNode.ChildNodes[_i].Attributes[0].Value;
                _EventLog.EventLogPrjName = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                _EventLog.OutPramerter = new CLDC_DataCore.Struct.StPowerPramerter();
                _EventLog.OutPramerter.Split(_XmlNode.ChildNodes[_i].Attributes[2].Value);
                _EventLogConfig.Add(_EventLog.EventLogPrjID, _EventLog);
            }

        }
        /// <summary>
        /// 存储事件记录配置字典
        /// </summary>
        public void Save()
        {
            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("", "EventLogConfig");
            foreach (string _n in _EventLogConfig.Keys)
            {
                Struct.StEventLogConfig _EventLog = _EventLogConfig[_n];
                _Xml.appendchild("", "R", "ID", _EventLog.EventLogPrjID, "Name", _EventLog.EventLogPrjName, "OutPramerter", _EventLog.OutPramerter.Jion());
            }
            _Xml.SaveXml(Application.StartupPath + Const.Variable.CONST_EVENTLOGDICTIONARY);
            return;
        }
        /// <summary>
        /// 添加和修改事件记录配置信息
        /// </summary>
        /// <param name="EventLogInfo">多功能配置信息结构体</param>
        public void Add(Struct.StEventLogConfig EventLogInfo)
        {
            if (_EventLogConfig.ContainsKey(EventLogInfo.EventLogPrjID))
            {
                this.Remove(EventLogInfo.EventLogPrjID);
            }
            _EventLogConfig.Add(EventLogInfo.EventLogPrjID, EventLogInfo);
            return;
        }
        /// <summary>
        /// 移除一个事件记录配置项目
        /// </summary>
        /// <param name="PrjID"></param>
        public void Remove(string PrjID)
        {
            if (!_EventLogConfig.ContainsKey(PrjID))
                return;
            _EventLogConfig.Remove(PrjID);
            return;
        }
        /// <summary>
        /// 获取事件记录项目信息列表
        /// </summary>
        /// <returns></returns>
        public List<Struct.StEventLogConfig> getEventLogPrj()
        {
            List<Struct.StEventLogConfig> _EventLog = new List<CLDC_DataCore.Struct.StEventLogConfig>();
            foreach (string _ID in _EventLogConfig.Keys)
            {
                _EventLog.Add(_EventLogConfig[_ID]);
            }
            return _EventLog;
        }
        /// <summary>
        /// 获取一个项目信息
        /// </summary>
        /// <param name="PrjID"></param>
        /// <returns></returns>
        public Struct.StEventLogConfig getEventLogPrj(string PrjID)
        {
            if (!_EventLogConfig.ContainsKey(PrjID))
                return new CLDC_DataCore.Struct.StEventLogConfig();
            return _EventLogConfig[PrjID];
        }

    }
}

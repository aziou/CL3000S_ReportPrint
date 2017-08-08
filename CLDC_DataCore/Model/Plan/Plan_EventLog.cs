using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_Comm.Enum;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Struct;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 事件记录方案
    /// </summary>
    [Serializable()]
    public class Plan_EventLog:Plan_Base 
    {
        /// <summary>
        /// 事件记录项目列表
        /// </summary>
        private List<CLDC_DataCore.Struct.StPlan_EventLog> _LstEventLog;

        public Plan_EventLog(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_EVENTLOG_FOLDERNAME, TaiType, vFAName)
        {
            this.Load();
        }
        ~Plan_EventLog()
        {
            _LstEventLog = null;
        }
        /// <summary>
        /// 加载事件记录方案
        /// </summary>
        private void Load()
        {
            _LstEventLog = new List<CLDC_DataCore.Struct.StPlan_EventLog>();
            string _ErrString="";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrString);
            if (_ErrString != "")
                return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                CLDC_DataCore.Struct.StPlan_EventLog _Item = new CLDC_DataCore.Struct.StPlan_EventLog();
                _Item.EventLogPrjID = _XmlNode.ChildNodes[_i].Attributes["PrjID"].Value;
                _Item.EventLogPrjName = _XmlNode.ChildNodes[_i].Attributes["PrjName"].Value;
                _Item.OutPramerter = new StPowerPramerter(); 
                _Item.OutPramerter.Split(_XmlNode.ChildNodes[_i].Attributes["PrjOutPut"].Value);
                _Item.PrjParm=_XmlNode.ChildNodes[_i].Attributes["PrjParameter"].Value;

                _LstEventLog.Add(_Item);
            }
            return;
        }
        /// <summary>
        /// 存储事件记录方案到XML文档
        /// </summary>
        public void Save()
        {
            clsXmlControl _XmlNode = new clsXmlControl();
            if (_LstEventLog.Count == 0)
                return;
            _XmlNode.appendchild("", "EventLogSy", "Name", Name);
            for (int _i = 0; _i < _LstEventLog.Count; _i++)
            { 
                _XmlNode.appendchild(""
                                    ,"R"
                                    ,"PrjID"
                                    , _LstEventLog[_i].EventLogPrjID
                                    ,"PrjName"
                                    ,_LstEventLog[_i].EventLogPrjName
                                    ,"PrjOutPut"
                                    ,_LstEventLog[_i].OutPramerter.Jion()
                                    ,"PrjParameter"
                                    ,_LstEventLog[_i].PrjParm);
            }
            _XmlNode.SaveXml(_FAPath);        
        }
        /// <summary>
        /// 增加一个新的事件记录方案项目
        /// </summary>
        /// <param name="PrjID">项目ID号</param>
        /// <param name="PrjName">项目名称</param>
        /// <param name="PrjOutPut">源输出参数(方向|元件|电压|电流|功率因素)</param>
        /// <param name="PrjParm">检定参数</param>
        /// <returns></returns>
        public bool Add(string PrjID, string PrjName, string PrjOutPut, string PrjParm)
        {
            CLDC_DataCore.Struct.StPlan_EventLog _Item = new CLDC_DataCore.Struct.StPlan_EventLog();
            _Item.EventLogPrjID = PrjID;
            _Item.EventLogPrjName = PrjName;
            _Item.OutPramerter = new StPowerPramerter();
            _Item.OutPramerter.Split(PrjOutPut);
            _Item.PrjParm = PrjParm;
            if (_LstEventLog.Contains(_Item))
                return false;
            _LstEventLog.Add(_Item);
            return true;
        }

        /// <summary>
        /// 删除方案所有项目内容
        /// </summary>
        public void RemoveAll()
        {
            _LstEventLog.Clear();
        }

        /// <summary>
        /// 返回当前方案项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstEventLog.Count;
            }
        }

        /// <summary>
        /// 根据列表索引ID获取项目数据
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public CLDC_DataCore.Struct.StPlan_EventLog getEventLogPrj(int i)
        {
            if (i >= _LstEventLog.Count)
                return new CLDC_DataCore.Struct.StPlan_EventLog();
            return _LstEventLog[i];
        }

        /// <summary>
        /// 移动事件记录项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, CLDC_DataCore.Struct.StPlan_EventLog Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstEventLog.Count ? _LstEventLog.Count - 1 : i;
            this.Remove(Item);
            _LstEventLog.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstEventLog.Count)
                return;
            _LstEventLog.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(CLDC_DataCore.Struct.StPlan_EventLog Item)
        {
            if (!_LstEventLog.Contains(Item))
                return;
            _LstEventLog.Remove(Item);
            return;
        }
    }
}

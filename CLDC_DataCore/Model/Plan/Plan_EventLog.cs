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
    /// �¼���¼����
    /// </summary>
    [Serializable()]
    public class Plan_EventLog:Plan_Base 
    {
        /// <summary>
        /// �¼���¼��Ŀ�б�
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
        /// �����¼���¼����
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
        /// �洢�¼���¼������XML�ĵ�
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
        /// ����һ���µ��¼���¼������Ŀ
        /// </summary>
        /// <param name="PrjID">��ĿID��</param>
        /// <param name="PrjName">��Ŀ����</param>
        /// <param name="PrjOutPut">Դ�������(����|Ԫ��|��ѹ|����|��������)</param>
        /// <param name="PrjParm">�춨����</param>
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
        /// ɾ������������Ŀ����
        /// </summary>
        public void RemoveAll()
        {
            _LstEventLog.Clear();
        }

        /// <summary>
        /// ���ص�ǰ������Ŀ����
        /// </summary>
        public int Count
        {
            get
            {
                return _LstEventLog.Count;
            }
        }

        /// <summary>
        /// �����б�����ID��ȡ��Ŀ����
        /// </summary>
        /// <param name="i">��Ŀ�б�����</param>
        /// <returns></returns>
        public CLDC_DataCore.Struct.StPlan_EventLog getEventLogPrj(int i)
        {
            if (i >= _LstEventLog.Count)
                return new CLDC_DataCore.Struct.StPlan_EventLog();
            return _LstEventLog[i];
        }

        /// <summary>
        /// �ƶ��¼���¼��Ŀ
        /// </summary>
        /// <param name="i">��Ҫ�ƶ������б�λ��</param>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Move(int i, CLDC_DataCore.Struct.StPlan_EventLog Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstEventLog.Count ? _LstEventLog.Count - 1 : i;
            this.Remove(Item);
            _LstEventLog.Insert(i, Item);
            return;
        }

        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="i">��Ŀ������</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstEventLog.Count)
                return;
            _LstEventLog.RemoveAt(i);
            return;
        }
        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(CLDC_DataCore.Struct.StPlan_EventLog Item)
        {
            if (!_LstEventLog.Contains(Item))
                return;
            _LstEventLog.Remove(Item);
            return;
        }
    }
}

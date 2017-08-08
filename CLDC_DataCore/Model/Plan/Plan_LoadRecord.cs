using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using System.Xml;
using CLDC_DataCore.DataBase;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 负荷记录
    /// </summary>
    [Serializable]
    public class Plan_LoadRecord : Plan_Base
    {
        private List<StPlan_LoadRecord> _LstA;

        public Plan_LoadRecord(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_LOADRECORD_FOLDERNAME, TaiType, vFAName)
        {
            Load();
        }
        /// <summary>
        /// 返回当前方案项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstA.Count;
            }
        }

        public StPlan_LoadRecord GetCurrentPrj(int i)
        {
            if (i >= _LstA.Count)
                return new StPlan_LoadRecord();
            return _LstA[i];
        }
        private bool Load()
        {
            _LstA = new List<StPlan_LoadRecord>();
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrorString);
            if (_ErrorString != "")
            {
                return false;
            }
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                StPlan_LoadRecord _Item = new StPlan_LoadRecord();
                _Item.PrjID = _XmlNode.ChildNodes[_i].Attributes["PrjID"].Value;
                _Item.OverTime = int.Parse(_XmlNode.ChildNodes[_i].Attributes["OverTime"].Value);
                _Item.danWei = _XmlNode.ChildNodes[_i].Attributes["danWei"].Value;
                _Item.MarginTime = int.Parse(_XmlNode.ChildNodes[_i].Attributes["MarginTime"].Value);
                _Item.ModeByte = _XmlNode.ChildNodes[_i].Attributes["ModeByte"].Value;
                _Item.RunningEPrj = new List<StRunningE>();                    //方案项目内容（走分费率）
                for (int _j = 0; _j < _XmlNode.ChildNodes[_i].ChildNodes.Count; _j++)
                {
                    XmlNode _ChildNode = _XmlNode.ChildNodes[_i].ChildNodes[_j];
                    StRunningE _Prj = new StRunningE();
                    _Prj.PowerFX = (Cus_PowerFangXiang)int.Parse(_ChildNode.Attributes["PowerFX"].Value);
                    _Prj.xIB = _ChildNode.Attributes["xIB"].Value;
                    _Prj.Glys = _ChildNode.Attributes["Glys"].Value;
                    _Prj.RunningTime = _ChildNode.Attributes["RunningTime"].Value;
                    _Item.RunningEPrj.Add(_Prj);
                }
                _LstA.Add(_Item);
            }
            return true;
        }
        public void Save()
        {
            if (_LstA.Count == 0)
                return;
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "LOADRECORD", "Name", Name);
            for (int _i = 0; _i < _LstA.Count; _i++)
            {
                StPlan_LoadRecord _Item = _LstA[_i];
                XmlNode _ChildNode = _XmlNode.appendchild(true
                                                    , "R"
                                                    , "PrjID"
                                                    , _Item.PrjID
                                                    , "OverTime"
                                                    , _Item.OverTime.ToString()
                                                    ,"danWei"
                                                    ,_Item.danWei
                                                    , "MarginTime"
                                                    , _Item.MarginTime.ToString()
                                                    , "ModeByte"
                                                    , _Item.ModeByte
                                                    
                                                    );
                for (int _j = 0; _j < _Item.RunningEPrj.Count; _j++)
                {
                    StRunningE _Prj = _Item.RunningEPrj[_j];
                    _ChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "PowerFX", ((int)_Prj.PowerFX).ToString(), "xIB", _Prj.xIB, "Glys", _Prj.Glys, "RunningTime", _Prj.RunningTime));
                }
            }
            _XmlNode.SaveXml(_FAPath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OverTime"></param>
        /// <param name="MarginTime"></param>
        /// <param name="ModeByte"></param>
        /// <param name="Prj"></param>
        /// <returns></returns>
        public bool Add(int OverTime,string danWei
                        , int MarginTime
                        , string ModeByte
                        , List<StRunningE> Prj)
        {
            StPlan_LoadRecord _ZouZi = new StPlan_LoadRecord();
            _ZouZi.PrjID = getPrjID();
            _ZouZi.OverTime = OverTime;
            _ZouZi.danWei = danWei;
            _ZouZi.MarginTime = MarginTime;
            _ZouZi.ModeByte = ModeByte;
            _ZouZi.RunningEPrj = Prj;

            if (_LstA.Contains(_ZouZi))
                return false;
            _LstA.Add(_ZouZi);
            return true;
        }

        private string getPrjID()
        {
            return "001";
        }

    }
}

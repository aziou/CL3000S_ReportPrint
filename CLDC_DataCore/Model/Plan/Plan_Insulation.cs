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
    /// 耐压试验方案
    /// </summary>
    [Serializable()]
    public class Plan_Insulation : Plan_Base
    {
        /// <summary>
        /// 耐压试验方案列表
        /// </summary>
        private List<CLDC_DataCore.Struct.StInsulationParam> listPlan = new List<CLDC_DataCore.Struct.StInsulationParam>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TaiType"></param>
        /// <param name="vFAName"></param>
        public Plan_Insulation( int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_INSULATION_FOLDERNAME, TaiType, vFAName)
        {
            this.Load();
        }

        /// <summary>
        /// 加载耐压试验方案
        /// </summary>
        private void Load()
        {
            listPlan = new List<CLDC_DataCore.Struct.StInsulationParam>();
            string _ErrString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrString);
            if (_ErrString != "")
                return;
            for (int i = 0; i < _XmlNode.ChildNodes.Count; i++)
            {
                CLDC_DataCore.Struct.StInsulationParam item = new CLDC_DataCore.Struct.StInsulationParam();
                int type = (int)item.InsulationType;
                int voltage = item.Voltage;
                int time = item.Time;
                int current = item.Current;
                int currentDevice = item.CurrentDevice;
                int.TryParse( _XmlNode.ChildNodes[i].Attributes["Type"].Value,out type);
                int.TryParse(  _XmlNode.ChildNodes[i].Attributes["Voltage"].Value,out voltage);
                int.TryParse(  _XmlNode.ChildNodes[i].Attributes["Current"].Value,out current);
                int.TryParse(_XmlNode.ChildNodes[i].Attributes["Time"].Value, out time);
                int.TryParse(_XmlNode.ChildNodes[i].Attributes["CurrentDevice"].Value, out currentDevice);

                item.Time = time;
                item.Voltage = voltage;
                item.Current = current;
                item.CurrentDevice=currentDevice;
                item.InsulationType=(CLDC_DataCore.Struct.StInsulationParam.EnumInsulationType)type;

                listPlan.Add(item);
            }
            return;
        }
        /// <summary>
        /// 存储耐压试验方案到XML文档
        /// </summary>
        public void Save()
        {
            clsXmlControl _XmlNode = new clsXmlControl();
            if (listPlan.Count == 0)
                return;
            _XmlNode.appendchild("", "Insulation");
            for (int _i = 0; _i < listPlan.Count; _i++)
            {
                _XmlNode.appendchild(""
                                    , "R"
                                    , "Type"
                                    , ((int)listPlan[_i].InsulationType).ToString()
                                    , "Voltage"
                                    , listPlan[_i].Voltage.ToString()
                                    , "Current"
                                    , listPlan[_i].Current.ToString()
                                    ,"CurrentDevice"
                                    ,listPlan[_i].CurrentDevice.ToString()
                                    , "Time"
                                    , listPlan[_i].Time.ToString());
            }
            _XmlNode.SaveXml(_FAPath);
        }
        /// <summary>
        /// 添加耐压试验
        /// </summary>
        public void Add(CLDC_DataCore.Struct.StInsulationParam insulationParam)
        {
            listPlan.Add(insulationParam);
        }
        /// <summary>
        /// 方案数量
        /// </summary>
        public int Count
        {
            get
            {
                return listPlan.Count;
            }
        }
        /// <summary>
        /// 移除所有
        /// </summary>
        public void RemoveAll()
        {
            listPlan.Clear();
        }
        /// <summary>
        /// 移除索引项
        /// </summary>
        /// <param name="index">索引</param>
        public void RemoveAt(int index)
        {
            if(index<listPlan.Count)
                listPlan.RemoveAt(index);
        }
        /// <summary>
        /// 获取耐压试验方案
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Struct.StInsulationParam GetPlan(int index)
        {
            if (index > listPlan.Count - 1)
            {
                CLDC_DataCore.Struct.StInsulationParam st = null;
                return st;
            }
            return listPlan[index];
        }
    }
}

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
    /// 智能表功能方案
    /// </summary>
    [Serializable()]
    public class Plan_Function : Plan_Base
    {
        /// <summary>
        /// 智能表功能项目列表
        /// </summary>
        private List<CLDC_DataCore.Struct.StPlan_Function> _LstFunction;
        

        public Plan_Function(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_GN_FOLDERNAME, TaiType, vFAName)
        {
            this.Load();
        }
        ~Plan_Function()
        {
            _LstFunction = null;
        }
        /// <summary>
        /// 加载智能表功能方案
        /// </summary>
        private void Load()
        {
            _LstFunction = new List<CLDC_DataCore.Struct.StPlan_Function>();
            
            string _ErrString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrString);
            if (_ErrString != "")
                return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                CLDC_DataCore.Struct.StPlan_Function _Item = new CLDC_DataCore.Struct.StPlan_Function();
                _Item.FunctionPrjID = _XmlNode.ChildNodes[_i].Attributes["PrjID"].Value;
                _Item.FunctionPrjName = _XmlNode.ChildNodes[_i].Attributes["PrjName"].Value;
                _Item.OutPramerter = new StPowerPramerter();
                _Item.OutPramerter.Split(_XmlNode.ChildNodes[_i].Attributes["PrjOutPut"].Value);
                _Item.PrjParm = _XmlNode.ChildNodes[_i].Attributes["PrjParameter"].Value;

                _LstFunction.Add(_Item);

            }
            return;
        }

        
        /// <summary>
        /// 存储智能表功能方案到XML文档
        /// </summary>
        public void Save()
        {
            clsXmlControl _XmlNode = new clsXmlControl();
            if (_LstFunction.Count == 0)
                return;
            _XmlNode.appendchild("", "FreezeSy", "Name", Name);
            for (int _i = 0; _i < _LstFunction.Count; _i++)
            {
                _XmlNode.appendchild(""
                                    , "R"
                                    , "PrjID"
                                    , _LstFunction[_i].FunctionPrjID
                                    , "PrjName"
                                    , _LstFunction[_i].FunctionPrjName
                                    , "PrjOutPut"
                                    , _LstFunction[_i].OutPramerter.Jion()
                                    , "PrjParameter"
                                    , _LstFunction[_i].PrjParm);
            }
            _XmlNode.SaveXml(_FAPath);
        }
        /// <summary>
        /// 增加一个新的智能表功能方案项目
        /// </summary>
        /// <param name="PrjID">项目ID号</param>
        /// <param name="PrjName">项目名称</param>
        /// <param name="PrjOutPut">源输出参数(方向|元件|电压|电流|功率因素)</param>
        /// <param name="PrjParm">检定参数</param>
        /// <returns></returns>
        public bool Add(string PrjID, string PrjName, string PrjOutPut, string PrjParm)
        {
            CLDC_DataCore.Struct.StPlan_Function _Item = new CLDC_DataCore.Struct.StPlan_Function();
            _Item.FunctionPrjID = PrjID;
            _Item.FunctionPrjName = PrjName;
            _Item.OutPramerter = new StPowerPramerter();
            _Item.OutPramerter.Split(PrjOutPut);
            _Item.PrjParm = PrjParm;
            if (_LstFunction.Contains(_Item))
                return false;
            _LstFunction.Add(_Item);
            return true;
        }
        
        /// <summary>
        /// 删除方案所有项目内容
        /// </summary>
        public void RemoveAll()
        {
            _LstFunction.Clear();
        }

        /// <summary>
        /// 返回当前方案项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstFunction.Count;
            }
        }
        
        /// <summary>
        /// 根据列表索引ID获取项目数据
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public CLDC_DataCore.Struct.StPlan_Function getFunctionPrj(int i)
        {
            if (i >= _LstFunction.Count)
                return new CLDC_DataCore.Struct.StPlan_Function();
            return _LstFunction[i];
        }
        public CLDC_DataCore.Struct.StPlan_Function getFunctionPrj(string ItemId)
        {
            foreach (StPlan_Function item in _LstFunction)
            {
                if (item.FunctionPrjID == ItemId)
                {
                    return item;
                }
            }
            return new StPlan_Function();
        }

        /// <summary>
        /// 移动智能表功能项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, CLDC_DataCore.Struct.StPlan_Function Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstFunction.Count ? _LstFunction.Count - 1 : i;
            this.Remove(Item);
            _LstFunction.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstFunction.Count)
                return;
            _LstFunction.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(CLDC_DataCore.Struct.StPlan_Function Item)
        {
            if (!_LstFunction.Contains(Item))
                return;
            _LstFunction.Remove(Item);
            return;
        }
    }
}

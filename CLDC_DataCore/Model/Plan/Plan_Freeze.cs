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
    /// 冻结方案
    /// </summary>
    [Serializable()]
    public class Plan_Freeze : Plan_Base
    {
        /// <summary>
        /// 冻结项目列表
        /// </summary>
        private List<CLDC_DataCore.Struct.StPlan_Freeze> _LstFreeze;

        public Plan_Freeze(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_FZ_FOLDERNAME, TaiType, vFAName)
        {
            this.Load();
        }
        ~Plan_Freeze()
        {
            _LstFreeze = null;
        }
        /// <summary>
        /// 加载冻结方案
        /// </summary>
        private void Load()
        { 
            _LstFreeze=new List<CLDC_DataCore.Struct.StPlan_Freeze>();
            string _ErrString="";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrString);
            if (_ErrString != "")
                return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                CLDC_DataCore.Struct.StPlan_Freeze _Item = new CLDC_DataCore.Struct.StPlan_Freeze();
                _Item.FreezePrjID = _XmlNode.ChildNodes[_i].Attributes["PrjID"].Value;
                _Item.FreezePrjName = _XmlNode.ChildNodes[_i].Attributes["PrjName"].Value;
                _Item.OutPramerter = new StPowerPramerter(); 
                _Item.OutPramerter.Split(_XmlNode.ChildNodes[_i].Attributes["PrjOutPut"].Value);
                _Item.PrjParm=_XmlNode.ChildNodes[_i].Attributes["PrjParameter"].Value;

                _LstFreeze.Add(_Item);
            }
            return;
        }
        /// <summary>
        /// 存储冻结方案到XML文档
        /// </summary>
        public void Save()
        {
            clsXmlControl _XmlNode = new clsXmlControl();
            if (_LstFreeze.Count == 0)
                return;
            _XmlNode.appendchild("", "FreezeSy", "Name", Name);
            for (int _i = 0; _i < _LstFreeze.Count; _i++)
            { 
                _XmlNode.appendchild(""
                                    ,"R"
                                    ,"PrjID"
                                    ,_LstFreeze[_i].FreezePrjID
                                    ,"PrjName"
                                    ,_LstFreeze[_i].FreezePrjName
                                    ,"PrjOutPut"
                                    ,_LstFreeze[_i].OutPramerter.Jion()
                                    ,"PrjParameter"
                                    ,_LstFreeze[_i].PrjParm);
            }
            _XmlNode.SaveXml(_FAPath);        
        }
        /// <summary>
        /// 增加一个新的冻结方案项目
        /// </summary>
        /// <param name="PrjID">项目ID号</param>
        /// <param name="PrjName">项目名称</param>
        /// <param name="PrjOutPut">源输出参数(方向|元件|电压|电流|功率因素)</param>
        /// <param name="PrjParm">检定参数</param>
        /// <returns></returns>
        public bool Add(string PrjID, string PrjName, string PrjOutPut, string PrjParm)
        {
            CLDC_DataCore.Struct.StPlan_Freeze _Item = new CLDC_DataCore.Struct.StPlan_Freeze();
            _Item.FreezePrjID = PrjID;
            _Item.FreezePrjName = PrjName;
            _Item.OutPramerter = new StPowerPramerter();
            _Item.OutPramerter.Split(PrjOutPut);
            _Item.PrjParm = PrjParm;
            if (_LstFreeze.Contains(_Item))
                return false;
            _LstFreeze.Add(_Item);
            return true;
        }

        /// <summary>
        /// 删除方案所有项目内容
        /// </summary>
        public void RemoveAll()
        {
            _LstFreeze.Clear();
        }

        /// <summary>
        /// 返回当前方案项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstFreeze.Count;
            }
        }

        /// <summary>
        /// 根据列表索引ID获取项目数据
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public CLDC_DataCore.Struct.StPlan_Freeze getFreezePrj(int i)
        {
            if (i >= _LstFreeze.Count)
                return new CLDC_DataCore.Struct.StPlan_Freeze();
            return _LstFreeze[i];
        }

        /// <summary>
        /// 移动冻结项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, CLDC_DataCore.Struct.StPlan_Freeze Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstFreeze.Count ? _LstFreeze.Count - 1 : i;
            this.Remove(Item);
            _LstFreeze.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstFreeze.Count)
                return;
            _LstFreeze.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(CLDC_DataCore.Struct.StPlan_Freeze Item)
        {
            if (!_LstFreeze.Contains(Item))
                return;
            _LstFreeze.Remove(Item);
            return;
        }
    }
}

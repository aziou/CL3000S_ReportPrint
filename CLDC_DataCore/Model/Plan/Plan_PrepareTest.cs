using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using System.Xml;
using CLDC_DataCore.DataBase;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 预先调试
    /// </summary>
    [Serializable]
    public class Plan_PrepareTest : Plan_Base
    {
        private List<StPlan_PrePareTest> _LstDgn;

        public Plan_PrepareTest(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_PREPARE_FOLDERNAME, TaiType, vFAName)
        {
            this.Load();
        }
        ~Plan_PrepareTest()
        {
            _LstDgn = null;
        }
        /// <summary>
        /// 加载多功能方案
        /// </summary>
        private void Load()
        { 
            _LstDgn=new List<StPlan_PrePareTest>();
            string _ErrString="";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrString);
            if (_ErrString != "")
            {
                return;
            }
            
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                StPlan_PrePareTest _Item = new StPlan_PrePareTest();
                _Item.PrePrjID = _XmlNode.ChildNodes[_i].Attributes["PrjID"].Value;
                _Item.PrePrjName = _XmlNode.ChildNodes[_i].Attributes["PrjName"].Value;
                _Item.OutPramerter = new StPowerPramerter(); 
                _Item.OutPramerter.Split(_XmlNode.ChildNodes[_i].Attributes["PrjOutPut"].Value);
                _Item.PrjParm=_XmlNode.ChildNodes[_i].Attributes["PrjParameter"].Value;

                _LstDgn.Add(_Item);

            }
            return;
        }
        /// <summary>
        /// 存储多功能方案到XML文档
        /// </summary>
        public void Save()
        {
            clsXmlControl _XmlNode = new clsXmlControl();
            if (_LstDgn.Count == 0)
                return;
            _XmlNode.appendchild("", "DgnSy", "Name", Name);
            for (int _i = 0; _i < _LstDgn.Count; _i++)
            { 
                _XmlNode.appendchild(""
                                    ,"R"
                                    ,"PrjID"
                                    ,_LstDgn[_i].PrePrjID
                                    ,"PrjName"
                                    ,_LstDgn[_i].PrePrjName
                                    ,"PrjOutPut"
                                    ,_LstDgn[_i].OutPramerter.Jion()
                                    ,"PrjParameter"
                                    ,_LstDgn[_i].PrjParm);
            }
            _XmlNode.SaveXml(_FAPath);        
        }
        /// <summary>
        /// 增加一个新的多功能方案项目
        /// </summary>
        /// <param name="PrjID">项目ID号</param>
        /// <param name="PrjName">项目名称</param>
        /// <param name="PrjOutPut">源输出参数(方向|元件|电压|电流|功率因素)</param>
        /// <param name="PrjParm">检定参数</param>
        /// <returns></returns>
        public bool Add(string PrjID, string PrjName, string PrjOutPut, string PrjParm)
        {
            StPlan_PrePareTest _Item = new StPlan_PrePareTest();
            _Item.PrePrjID = PrjID;
            _Item.PrePrjName = PrjName;
            _Item.OutPramerter = new StPowerPramerter();
            _Item.OutPramerter.Split(PrjOutPut);
            _Item.PrjParm = PrjParm;
            if (_LstDgn.Contains(_Item))
                return false;
            _LstDgn.Add(_Item);
            return true;
        }

        /// <summary>
        /// 删除方案所有项目内容
        /// </summary>
        public void RemoveAll()
        {
            _LstDgn.Clear();
        }

        /// <summary>
        /// 返回当前方案项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstDgn.Count;
            }
        }

        /// <summary>
        /// 根据列表索引ID获取项目数据
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public StPlan_PrePareTest getDgnPrj(int i)
        {
            if (i >= _LstDgn.Count)
                return new StPlan_PrePareTest();
            return _LstDgn[i];
        }

        /// <summary>
        /// 移动多功能项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, StPlan_PrePareTest Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstDgn.Count ? _LstDgn.Count - 1 : i;
            this.Remove(Item);
            _LstDgn.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstDgn.Count)
                return;
            _LstDgn.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(StPlan_PrePareTest Item)
        {
            if (!_LstDgn.Contains(Item))
                return;
            _LstDgn.Remove(Item);
            return;
        }
    }
}

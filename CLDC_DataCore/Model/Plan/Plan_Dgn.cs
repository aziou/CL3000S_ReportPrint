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
    /// 多功能方案
    /// </summary>
    [Serializable()]
    public class Plan_Dgn:Plan_Base 
    {
        /// <summary>
        /// 多功能项目列表
        /// </summary>
        private List<CLDC_DataCore.Struct.StPlan_Dgn> _LstDgn;

        public Plan_Dgn(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_DGN_FOLDERNAME, TaiType, vFAName)
        {
            this.Load();
        }
        ~Plan_Dgn()
        {
            _LstDgn = null;
        }
        /// <summary>
        /// 加载多功能方案
        /// </summary>
        private void Load()
        { 
            _LstDgn=new List<StPlan_Dgn>();
            string _ErrString="";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrString);
            if (_ErrString != "")
            {
                return;
            }
            int intPrjID = 0;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                StPlan_Dgn _Item = new StPlan_Dgn();
                _Item.DgnPrjID = _XmlNode.ChildNodes[_i].Attributes["PrjID"].Value;
                _Item.DgnPrjName = _XmlNode.ChildNodes[_i].Attributes["PrjName"].Value;
                _Item.OutPramerter = new StPowerPramerter(); 
                _Item.OutPramerter.Split(_XmlNode.ChildNodes[_i].Attributes["PrjOutPut"].Value);
                _Item.PrjParm=_XmlNode.ChildNodes[_i].Attributes["PrjParameter"].Value;

                _LstDgn.Add(_Item);

                #region //特别处理时段投切，四个方向，为了改动少但要展示为2级节点
                intPrjID = int.Parse(_Item.DgnPrjID);
                if (intPrjID == (int)Cus_DgnItem.时段投切)
                {
                    string[] _PrjParm = _Item.PrjParm.Split('|');
                    
                    if (_PrjParm != null && _PrjParm.Length > 1)
                    {
                        int _PrjParmLength = _PrjParm.Length;
                        if (_PrjParm[_PrjParmLength - 1].Length == 4)
                        {
                            if (_PrjParm[_PrjParmLength - 1][1] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.反向有功时段投切).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.反向有功时段投切.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                            if (_PrjParm[_PrjParmLength - 1][2] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.正向无功时段投切).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.正向无功时段投切.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                            if (_PrjParm[_PrjParmLength - 1][3] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.反向无功时段投切).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.反向无功时段投切.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                        }
                    }
                }
                else if (intPrjID == (int)Cus_DgnItem.计度器示值组合误差)
                {
                    string[] _PrjParm = _Item.PrjParm.Split('|');

                    if (_PrjParm != null && _PrjParm.Length > 1)
                    {
                        int _PrjParmLength = _PrjParm.Length;
                        if (_PrjParm[_PrjParmLength - 1].Length == 4)
                        {
                            if (_PrjParm[_PrjParmLength - 1][1] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.反向有功计度器示值组合误差).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.反向有功计度器示值组合误差.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                            if (_PrjParm[_PrjParmLength - 1][2] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.正向无功计度器示值组合误差).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.正向无功计度器示值组合误差.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                            if (_PrjParm[_PrjParmLength - 1][3] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.反向无功计度器示值组合误差).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.反向无功计度器示值组合误差.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                        }
                    }
                }
                else if (intPrjID == (int)Cus_DgnItem.费率时段示值误差)
                {
                    string[] _PrjParm = _Item.PrjParm.Split('|');

                    if (_PrjParm != null && _PrjParm.Length > 1)
                    {
                        int _PrjParmLength = _PrjParm.Length;
                        if (_PrjParm[_PrjParmLength - 1].Length == 4)
                        {
                            if (_PrjParm[_PrjParmLength - 1][1] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.反向有功费率时段示值误差).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.反向有功费率时段示值误差.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                            if (_PrjParm[_PrjParmLength - 1][2] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.正向无功费率时段示值误差).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.正向无功费率时段示值误差.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                            if (_PrjParm[_PrjParmLength - 1][3] == '1')
                            {
                                StPlan_Dgn _ItemCopy = new StPlan_Dgn();
                                _ItemCopy = _Item;
                                _ItemCopy.DgnPrjID = ((int)Cus_DgnItem.反向无功费率时段示值误差).ToString("000");
                                _ItemCopy.DgnPrjName = Cus_DgnItem.反向无功费率时段示值误差.ToString();
                                _LstDgn.Add(_ItemCopy);
                            }
                        }
                    }
                }
                #endregion
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
                                    ,_LstDgn[_i].DgnPrjID
                                    ,"PrjName"
                                    ,_LstDgn[_i].DgnPrjName
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
            StPlan_Dgn _Item = new StPlan_Dgn();
            _Item.DgnPrjID = PrjID;
            _Item.DgnPrjName = PrjName;
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
        public StPlan_Dgn getDgnPrj(int i)
        {
            if (i >= _LstDgn.Count)
                return new StPlan_Dgn();
            return _LstDgn[i];
        }

        /// <summary>
        /// 移动多功能项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, StPlan_Dgn Item)
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
        public void Remove(StPlan_Dgn Item)
        {
            if (!_LstDgn.Contains(Item))
                return;
            _LstDgn.Remove(Item);
            return;
        }
    }
}

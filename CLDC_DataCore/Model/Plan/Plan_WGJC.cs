using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;
using System.Xml;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 外观检查试验方案
    /// </summary>
    [Serializable()]
    public class Plan_WGJC:Plan_Base 
    {
        private List<StPlan_WGJC> _LstWGJC;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型0-三相台，1-单向台</param>
        /// <param name="vFAName">方案名称</param>
        public Plan_WGJC(int TaiType, string FAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_WGJC_FOLDERNAME , TaiType, FAName)
        {
            this.Load();
        }
        ~Plan_WGJC()
        {
            _LstWGJC = null;
        }
        /// <summary>
        /// 加载外观检查方案到预热数据列表
        /// </summary>
        private void Load()
        {
            _LstWGJC = new List<StPlan_WGJC>();
            string _ErrorString="";
            XmlNode _XmlNode=clsXmlControl.LoadXml(_FAPath,out _ErrorString);
            if (_ErrorString!="")
                return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                StPlan_WGJC _WGJC = new StPlan_WGJC();
                _WGJC.WGJCPrjID = (_i + 1).ToString("D3");
                _LstWGJC.Add(_WGJC);
            }
        }
        /// <summary>
        /// 存储外观检查方案
        /// </summary>
        public void Save()
        {
            if (_LstWGJC.Count == 0)
                return;
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "WGJC", "Name", Name);
            for (int _i = 0; _i < _LstWGJC.Count; _i++)
            {
                _XmlNode.appendchild(""
                                    ,"R"
                                    , "GLFX"
                                    , _LstWGJC[_i].WGJCPrjID
                                    );
            }
            _XmlNode.SaveXml(_FAPath);
        }
        /// <summary>
        /// 添加一个外观检查项目
        /// </summary>        
        /// <returns></returns>
        public bool Add(int Order)
        {
            StPlan_WGJC _Item = new StPlan_WGJC();

            _Item.WGJCPrjID = (Order + 1).ToString("D3");
            if (_LstWGJC.Contains(_Item))
                Move(Order, _Item);
            else
                _LstWGJC.Insert(Order, _Item);
            return true;
        }

        /// <summary>
        /// 获取外观检查项目
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public StPlan_WGJC getWGJCPrj(int i)
        {
            if (i >= _LstWGJC.Count)
                return new StPlan_WGJC();
            return _LstWGJC[i];
        }


        /// <summary>
        /// 移动外观检查项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, StPlan_WGJC Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstWGJC.Count ? _LstWGJC.Count - 1 : i;
            this.Remove(Item);
            _LstWGJC.Insert(i, Item);
            return;
        }
        /// <summary>
        /// 移除全部项目
        /// </summary>
        public void RemoveAll()
        {
            _LstWGJC.Clear();
        }

        /// <summary>
        /// 外观检查项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstWGJC.Count;
            }
        }
        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstWGJC.Count)
                return;
            _LstWGJC.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(StPlan_WGJC Item)
        {
            if (!_LstWGJC.Contains(Item))
                return;
            _LstWGJC.Remove(Item);
            return;
        }

    }
}

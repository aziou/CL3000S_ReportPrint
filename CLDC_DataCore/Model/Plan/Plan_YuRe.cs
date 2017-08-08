using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;
using System.Xml;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 预热方案
    /// </summary>
    [Serializable()]
    public class Plan_YuRe:Plan_Base 
    {
        private List<StPlan_YuRe> _LstYuRe;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型0-三相台，1-单向台</param>
        /// <param name="vFAName">方案名称</param>
        public Plan_YuRe(int TaiType, string FAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_YURE_FOLDERNAME , TaiType, FAName)
        {
            this.Load();
        }
        ~Plan_YuRe()
        {
            _LstYuRe = null;
        }
        /// <summary>
        /// 加载预热方案到预热数据列表
        /// </summary>
        private void Load()
        {
            _LstYuRe = new List<StPlan_YuRe>();
            string _ErrorString="";
            XmlNode _XmlNode=clsXmlControl.LoadXml(_FAPath,out _ErrorString);
            if (_ErrorString!="")
                return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                StPlan_YuRe _Yure = new StPlan_YuRe();
                if (_XmlNode.ChildNodes[_i].Attributes["GLFX"] == null)
                    _Yure.PowerFangXiang = CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功;
                else
                    _Yure.PowerFangXiang=(CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(_XmlNode.ChildNodes[_i].Attributes["GLFX"].Value);
                if (_XmlNode.ChildNodes[_i].Attributes["xIb"] ==null)
                    _Yure.xIb = "1";
                else
                    _Yure.xIb = _XmlNode.ChildNodes[_i].Attributes["xIb"].Value;
                if (_XmlNode.ChildNodes[_i].Attributes["Time"] == null)
                    _Yure.Times = 1;
                else
                    _Yure.Times = float.Parse(_XmlNode.ChildNodes[_i].Attributes["Time"].Value);
                _LstYuRe.Add(_Yure);
            }
        }
        /// <summary>
        /// 存储预热方案
        /// </summary>
        public void Save()
        {
            if (_LstYuRe.Count == 0)
                return;
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "YuRe", "Name", Name);
            for(int _i=0;_i<_LstYuRe.Count;_i++)
            {
                _XmlNode.appendchild(""
                                    ,"R"
                                    ,"GLFX"
                                    ,((int)_LstYuRe[_i].PowerFangXiang).ToString()
                                    ,"xIb"
                                    ,_LstYuRe[_i].xIb
                                    ,"Time"
                                    ,_LstYuRe[_i].Times.ToString());
            }
            _XmlNode.SaveXml(_FAPath);
        }
        /// <summary>
        /// 添加一个预热项目
        /// </summary>
        /// <param name="Glfx">功率方向</param>
        /// <param name="xIb">电流倍数Imax</param>
        /// <param name="Time">时间（分钟）</param>
        /// <returns></returns>
        public bool Add(int Order,CLDC_Comm.Enum.Cus_PowerFangXiang Glfx, string xIb, float Time)
        {
            StPlan_YuRe _Item = new StPlan_YuRe();
            _Item.PowerFangXiang = Glfx;
            _Item.xIb = xIb;
            _Item.Times = Time;
            if (_LstYuRe.Contains(_Item))
                Move(Order, _Item);
            else
                _LstYuRe.Insert(Order, _Item);
            return true;
        }

        /// <summary>
        /// 获取预热项目
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public StPlan_YuRe getYuRePrj(int i)
        {
            if (i >= _LstYuRe.Count)
                return new StPlan_YuRe();
            return _LstYuRe[i];
        }


        /// <summary>
        /// 移动预热项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i,StPlan_YuRe Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstYuRe.Count ? _LstYuRe.Count - 1 : i;
            this.Remove(Item);
            _LstYuRe.Insert(i, Item);
            return;
        }
        /// <summary>
        /// 移除全部项目
        /// </summary>
        public void RemoveAll()
        {
            _LstYuRe.Clear();
        }

        /// <summary>
        /// 预热项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstYuRe.Count;
            }
        }
        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstYuRe.Count)
                return;
            _LstYuRe.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(StPlan_YuRe Item)
        {
            if (!_LstYuRe.Contains(Item))
                return;
            _LstYuRe.Remove(Item);
            return;
        }

    }
}

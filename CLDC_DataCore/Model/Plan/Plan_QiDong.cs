using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_Comm;
using CLDC_DataCore.DataBase;
using CLDC_Comm.Enum;
using CLDC_DataCore.Struct;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 起动方案
    /// </summary>
    [Serializable()]
    public class Plan_QiDong:Plan_Base
    {

        /// <summary>
        /// 启动项目列表
        /// </summary>
        private List<StPlan_QiDong> _LstQiDong;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型</param>
        /// <param name="FAName">方案名称</param>
        public Plan_QiDong(int TaiType, string FAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_QID_FOLDERNAME , TaiType, FAName)
        {
            this.Load();
        }
        ~Plan_QiDong()
        {
            _LstQiDong = null;
        }
        /// <summary>
        /// 加载启动方案
        /// </summary>
        private void Load()
        {
            _LstQiDong = new List<StPlan_QiDong>();
            string _ErrorString="";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath,out  _ErrorString);
            if(_XmlNode==null)
                return;
            for(int _i=0;_i<_XmlNode.ChildNodes.Count;_i++)
            {
                StPlan_QiDong _Qidong = new StPlan_QiDong();
                _Qidong.PowerFangXiang = (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(_XmlNode.ChildNodes[_i].Attributes["GLFX"].Value);
                _Qidong.FloatxIb = float.Parse(_XmlNode.ChildNodes[_i].Attributes["xIb"].Value);
                _Qidong.xTime = float.Parse(_XmlNode.ChildNodes[_i].Attributes["xTime"].Value);
                _Qidong.DefaultValue = int.Parse(_XmlNode.ChildNodes[_i].Attributes["Default"].Value);
                _LstQiDong.Add(_Qidong);
            }
            return;
        }
        /// <summary>
        /// 存储启动方案
        /// </summary>
        public void Save()
        {
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "QiDong", "Name", Name);
            if (_LstQiDong.Count == 0)
            {
                _XmlNode.SaveXml(_FAPath);
                return;
            }
            for (int _i = 0; _i < _LstQiDong.Count; _i++)
            {
                _XmlNode.appendchild(""
                                    , "R"
                                    , "GLFX"
                                    , ((int)_LstQiDong[_i].PowerFangXiang).ToString()
                                    , "xIb"
                                    , _LstQiDong[_i].FloatxIb.ToString()
                                    , "xTime"
                                    , _LstQiDong[_i].xTime.ToString()
                                    , "Default"
                                    , _LstQiDong[_i].DefaultValue.ToString());
            }
            _XmlNode.SaveXml(_FAPath);
            return;
        }
        /// <summary>
        /// 增加一个启动项目
        /// </summary>
        /// <param name="Glfx">功率方向</param>
        /// <param name="xIb">电流倍数(数字)</param>
        /// <param name="xTime">时间倍数（多少倍起动时间）起动时间是根据规程计算</param>
        /// <param name="DefaultValue">是否默认合格0-不默认，1-默认，默认合格时该项目则不检定</param>
        /// <returns></returns>
        public bool Add(Cus_PowerFangXiang Glfx, float xIb, float xTime, int DefaultValue)
        {
            StPlan_QiDong _QiDong = new StPlan_QiDong();
            _QiDong.PowerFangXiang = Glfx;
            _QiDong.FloatxIb = xIb;
            _QiDong.xTime = xTime;
            _QiDong.DefaultValue=DefaultValue;
            if (_LstQiDong.Contains(_QiDong))
                return false;
            _LstQiDong.Add(_QiDong);
            return true;
        }
        /// <summary>
        /// 移除所有项目
        /// </summary>
        public void RemoveAll()
        {
            _LstQiDong.Clear();
        }


        /// <summary>
        /// 返回方案中包含的项目数量
        /// </summary>
        public int Count
        {
            get 
            {
                return _LstQiDong.Count;
            }
        }
        /// <summary>
        /// 根据列表索引ID获取项目数据
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public StPlan_QiDong getQiDongPrj(int i)
        {
            if (i >= _LstQiDong.Count)
                return new StPlan_QiDong();
            return _LstQiDong[i];
        }

        /// <summary>
        /// 移动启动项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, StPlan_QiDong Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstQiDong.Count ? _LstQiDong.Count - 1 : i;
            this.Remove(Item);
            _LstQiDong.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstQiDong.Count)
                return;
            _LstQiDong.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(StPlan_QiDong Item)
        {
            if (!_LstQiDong.Contains(Item))
                return;
            _LstQiDong.Remove(Item);
            return;
        }

    }
}

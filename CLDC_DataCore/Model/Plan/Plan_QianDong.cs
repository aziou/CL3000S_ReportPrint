using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;
using CLDC_DataCore.Struct;
using CLDC_Comm.Enum;
using CLDC_DataCore.DataBase;
using System.Xml;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 潜动方案
    /// </summary>
    [Serializable()]
    public class Plan_QianDong : Plan_Base
    {

        /// <summary>
        /// 日计时参数
        /// </summary>
        public string DayCheckTimesSetting;
        /// <summary>
        /// 潜动项目列表
        /// </summary>
        private List<StPlan_QianDong> _LstQianDong;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型0-三相台，1-单向台</param>
        /// <param name="vFAName">方案名称</param>
        public Plan_QianDong(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_QIAND_FOLDERNAME, TaiType, vFAName)
        {
            this.Load();
        }
        ~Plan_QianDong()
        {
            _LstQianDong = null;
        }
        /// <summary>
        /// 加载潜动方案
        /// </summary>
        private void Load()
        {
            _LstQianDong = new List<StPlan_QianDong>();
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out  _ErrorString);
            if (_XmlNode == null)
            {
                DayCheckTimesSetting = "0|1|5|60";
                return;
            }
            if (_XmlNode.ChildNodes.Count < 1)
            {
                DayCheckTimesSetting = "0|1|5|60";
            }
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count - 1; _i++)
            {
                StPlan_QianDong _Qiandong = new StPlan_QianDong();
                _Qiandong.PowerFangXiang = (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(_XmlNode.ChildNodes[_i].Attributes["GLFX"].Value);
                _Qiandong.FloatxU = float.Parse(_XmlNode.ChildNodes[_i].Attributes["xU"].Value);
                _Qiandong.FloatxIb = float.Parse(_XmlNode.ChildNodes[_i].Attributes["xQIb"].Value);
                _Qiandong.xTime = float.Parse(_XmlNode.ChildNodes[_i].Attributes["xTime"].Value);
                _Qiandong.DefaultValue = int.Parse(_XmlNode.ChildNodes[_i].Attributes["Default"].Value);
                if (_XmlNode.ChildNodes.Count > 0)
                {
                    try
                    {
                        if (_XmlNode.ChildNodes[_XmlNode.ChildNodes.Count - 1].OuterXml.Contains("PrjParameter"))
                        {
                            _Qiandong.DayCheckTimesSetting = _XmlNode.ChildNodes[_XmlNode.ChildNodes.Count - 1].Attributes["PrjParameter"].Value;
                            DayCheckTimesSetting = _Qiandong.DayCheckTimesSetting;
                        }
                        else
                        {
                            _Qiandong.DayCheckTimesSetting = "0|1|5|60";
                            DayCheckTimesSetting = _Qiandong.DayCheckTimesSetting;

                        }

                    }
                    catch (Exception ex)
                    {
                        CLDC_DataCore.Function.ErrorLog.Write(ex);
                        _Qiandong.DayCheckTimesSetting = "0|1|5|60";
                        DayCheckTimesSetting = _Qiandong.DayCheckTimesSetting;
                    }


                    _LstQianDong.Add(_Qiandong);
                }
            }
            if (_LstQianDong.Count > 1)
            {
                DayCheckTimesSetting = _LstQianDong[_LstQianDong.Count - 1].DayCheckTimesSetting;
            }
            else
            {
                DayCheckTimesSetting = "0|1|5|60";
            }
            return;
        }
        /// <summary>
        /// 存储潜动方案
        /// </summary>
        public void Save()
        {
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "QianDong", "Name", Name);
            if (_LstQianDong.Count == 0)
            {
                _XmlNode.SaveXml(_FAPath);
                return;
            }
            for (int _i = 0; _i < _LstQianDong.Count; _i++)
            {
                _XmlNode.appendchild(""
                                    , "R"
                                    , "GLFX"
                                    , ((int)_LstQianDong[_i].PowerFangXiang).ToString()
                                    , "xU"
                                    , _LstQianDong[_i].FloatxU.ToString()
                                    , "xQIb"
                                    , _LstQianDong[_i].FloatxIb.ToString()
                                    , "xTime"
                                    , _LstQianDong[_i].xTime.ToString()
                                    , "Default"
                                    , _LstQianDong[_i].DefaultValue.ToString());
            }

            #region----潜动日计时----

            _XmlNode.appendchild(""
                                , "R"
                                , "PrjID"
                                , "002"
                                , "PrjName"
                                , "日计时误差"
                                , "PrjOutPut"
                                , "1|1|1|0Ib|1.0"
                                , "PrjParameter"
                                , DayCheckTimesSetting
                                , "Default"
                                , "0|1|5|60");
            #endregion
            _XmlNode.SaveXml(_FAPath);
            return;
        }


        /// <summary>
        /// 增加一个潜动项目
        /// </summary>
        /// <param name="Glfx">功率方向</param>
        /// <param name="xU">电压倍数（数字）</param> 
        /// <param name="xIb">电流倍数(数字)</param>
        /// <param name="xTime">时间倍数（多少倍起动时间）起动时间是根据规程计算</param>
        /// <param name="Default">是否默认合格0-不默认，1-默认，默认合格时该项目则不检定</param>
        /// <returns></returns>
        public bool Add(Cus_PowerFangXiang Glfx, float xU, float xIb, float xTime, int DefaultValue)
        {
            StPlan_QianDong _QianDong = new StPlan_QianDong();
            _QianDong.PowerFangXiang = Glfx;
            _QianDong.FloatxU = xU;
            _QianDong.FloatxIb = xIb;
            _QianDong.xTime = xTime;
            _QianDong.DefaultValue = DefaultValue;
            if (_LstQianDong.Contains(_QianDong))
                return false;
            _LstQianDong.Add(_QianDong);
            return true;
        }

        /// <summary>
        /// 移除所有方案项目
        /// </summary>
        public void RemoveAll()
        {
            _LstQianDong.Clear();
        }

        /// <summary>
        /// 返回方案中包含的项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstQianDong.Count;
            }
        }
        /// <summary>
        /// 根据列表索引ID获取项目数据
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public StPlan_QianDong getQianDongPrj(int i)
        {
            if (i >= _LstQianDong.Count)
                return new StPlan_QianDong();
            return _LstQianDong[i];
        }

        /// <summary>
        /// 移动潜动项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, StPlan_QianDong Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstQianDong.Count ? _LstQianDong.Count - 1 : i;
            this.Remove(Item);
            _LstQianDong.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstQianDong.Count)
                return;
            _LstQianDong.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(StPlan_QianDong Item)
        {
            if (!_LstQianDong.Contains(Item))
                return;
            _LstQianDong.Remove(Item);
            return;
        }
    }
}

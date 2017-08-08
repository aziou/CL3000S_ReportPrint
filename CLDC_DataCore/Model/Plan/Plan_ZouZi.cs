using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
using System.Xml;
using CLDC_Comm.Enum;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 走字方案
    /// </summary>
    [Serializable()]
    public class Plan_ZouZi:Plan_Base  
    {

        /// <summary>
        /// 走字项目列表
        /// </summary>
        private List<StPlan_ZouZi> _LstZouZi;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型</param>
        /// <param name="FAName">方案名称</param>
        public Plan_ZouZi(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_ZOUZI_FOLDERNAME , TaiType, vFAName)
        {
            this.Load();
        }
        ~Plan_ZouZi()
        {
            _LstZouZi = null;
        }
        /// <summary>
        /// 加载走字方案
        /// </summary>
        private bool  Load()
        {
            _LstZouZi = new List<StPlan_ZouZi>();
            string _ErrorString="";
            XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrorString);
            if (_ErrorString != "")
                return false;
            try
            {
                //Cus_ZouZiMethod _Method =(Cus_ZouZiMethod)int.Parse(clsXmlControl.getNodeAttributeValue(_XmlNode, "JdFS"));
                for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
                {
                    StPlan_ZouZi _Item = new StPlan_ZouZi();
                    _Item.PrjID = _XmlNode.ChildNodes[_i].Attributes["PrjID"].Value;            //项目ID
                    _Item.Glys = _XmlNode.ChildNodes[_i].Attributes["Glys"].Value;              //功率因素
                    _Item.xIb = _XmlNode.ChildNodes[_i].Attributes["xIb"].Value;                //电流倍数
                    _Item.FeiLvString = _XmlNode.ChildNodes[_i].Attributes["feilv"].Value;      //费率描述
                    _Item.ZouZiMethod =(Cus_ZouZiMethod)int.Parse(_XmlNode.ChildNodes[_i].Attributes["CheckType"].Value);           //走字方法
                    _Item.ZuHeWc = _XmlNode.ChildNodes[_i].Attributes["ZuHeWc"].Value + "";         //是否做组合误差     
                    if(_Item.ZuHeWc=="") _Item.ZuHeWc = "0";         //是否做组合误差     
                    _Item.ZouZiPrj = new List<StPlan_ZouZi.StPrjFellv>();                    //方案项目内容（走分费率）
                    for (int _j = 0; _j < _XmlNode.ChildNodes[_i].ChildNodes.Count; _j++)
                    {
                        XmlNode _ChildNode = _XmlNode.ChildNodes[_i].ChildNodes[_j];
                        StPlan_ZouZi.StPrjFellv _Prj = new StPlan_ZouZi.StPrjFellv();
                        _Prj.FeiLv = (Cus_FeiLv)int.Parse(_ChildNode.Attributes["feilv"].Value);
                        _Prj.StartTime = _ChildNode.Attributes["StartTime"].Value;
                        _Prj.ZouZiTime = _ChildNode.Attributes["Time"].Value;
                        _Item.ZouZiPrj.Add(_Prj);
                    }
                    _LstZouZi.Add(_Item);
                    
                }
                return true ;
            }
            catch(Exception ex)
            {
                CLDC_DataCore.Function.ErrorLog.Write(ex);
                throw ex;
                //return false;
            }
        }

        /// <summary>
        /// 保存走字方案到XML文档
        /// </summary>
        public void Save()
        {
            if (_LstZouZi.Count == 0)
                return;
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "ZZSY", "Name", Name);//, "JdFS", ((int)_LstZouZi[0].ZouZiMethod).ToString());
            for (int _i = 0; _i < _LstZouZi.Count; _i++)
            {
                StPlan_ZouZi _Item = _LstZouZi[_i];
                XmlNode _ChildNode=_XmlNode.appendchild(true
                                                    ,"R"
                                                    ,"PrjID"
                                                    ,_Item.PrjID
                                                    ,"GLFX"
                                                    ,((int)_Item.PowerFangXiang).ToString()
                                                    ,"Yj"
                                                    ,((int)_Item.PowerYj).ToString()
                                                    ,"Glys"
                                                    ,_Item.Glys
                                                    ,"xIb"
                                                    ,_Item.xIb
                                                    ,"feilv"
                                                    ,_Item.FeiLvString 
                                                    ,"CheckType"
                                                    ,((int)_Item.ZouZiMethod).ToString()
                                                    ,"ZuHeWc"
                                                    ,_Item.ZuHeWc);
                for (int _j = 0; _j < _Item.ZouZiPrj.Count; _j++)
                {
                    StPlan_ZouZi.StPrjFellv _Prj = _Item.ZouZiPrj[_j];
                    _ChildNode.AppendChild(clsXmlControl.CreateXmlNode("C", "feilv", ((int)_Prj.FeiLv).ToString(), "StartTime", _Prj.StartTime, "Time", _Prj.ZouZiTime)); 
                }
            }
            _XmlNode.SaveXml(_FAPath);
            return;
        }

        /// <summary>
        /// 增加一个走字项目
        /// </summary>
        /// <param name="Glfx">功率方向</param>
        /// <param name="Method">试验方法（基本走字，还是标准表法）</param>
        /// <param name="Yj">元件</param>
        /// <param name="Glys">功率因素</param>
        /// <param name="xIb">电流倍数</param>
        /// <param name="Feilvstring">费率描述</param> 
        /// <param name="ZuHeWc">是否做组合误差0不做，1做</param>
        /// <param name="Prj">具体走字项目列表</param> 
        /// <returns></returns>
        public bool Add(Cus_PowerFangXiang Glfx
                        ,Cus_ZouZiMethod Method
                        ,Cus_PowerYuanJian Yj
                        ,string Glys
                        ,string xIb
                        ,string Feilvstring
                        ,string ZuHeWc
                        ,List<StPlan_ZouZi.StPrjFellv> Prj)
        {
            StPlan_ZouZi _ZouZi = new StPlan_ZouZi();
            _ZouZi.PrjID=getZouZiPrjID(Glfx,Yj,Glys,xIb);
            _ZouZi.xIb = xIb;
            _ZouZi.Glys = Glys;
            _ZouZi.FeiLvString = Feilvstring;
            _ZouZi.ZouZiMethod = Method;
            _ZouZi.ZuHeWc = ZuHeWc;
            _ZouZi.ZouZiPrj = Prj; 
            if (_LstZouZi.Contains(_ZouZi))
                return false;
            _LstZouZi.Add(_ZouZi);
            return true;
        }
 
        public void RemoveAll()
        {
            _LstZouZi.Clear();
        }

        /// <summary>
        /// 返回方案中包含的项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstZouZi.Count;
            }
        }
        /// <summary>
        /// 根据列表索引ID获取项目数据
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public StPlan_ZouZi getZouZiPrj(int i)
        {
            if (i >= _LstZouZi.Count)
                return new StPlan_ZouZi();
            return _LstZouZi[i];
        }

        /// <summary>
        /// 移动走字项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, StPlan_ZouZi Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstZouZi.Count ? _LstZouZi.Count - 1 : i;
            this.Remove(Item);
            _LstZouZi.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstZouZi.Count)
                return;
            _LstZouZi.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(StPlan_ZouZi Item)
        {
            if (!_LstZouZi.Contains(Item))
                return;
            _LstZouZi.Remove(Item);
            return;
        }
        /// <summary>
        /// 获取走字项目ID，带费率
        /// </summary>
        /// <param name="GLFX">功率方向</param>
        /// <param name="Yj">元件</param>
        /// <param name="FeiLv">费率</param>
        /// <param name="Glys">功率因素</param>
        /// <param name="xIb">电流倍数</param>
        /// <returns></returns>
        public static string getZouZiPrjID(Cus_PowerFangXiang GLFX, Cus_PowerYuanJian Yj, Cus_FeiLv FeiLv, string Glys, string xIb)
        {            
            return string.Format("{0}{1}{2}{3}{4}"
                                , (int)GLFX
                                , (int)Yj
                                , CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysID(Glys)
                                , CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.xIbDic.getxIbID(xIb)
                                , (int)FeiLv);
        }

        /// <summary>
        /// 获取走字项目ID,不带费率
        /// </summary>
        /// <param name="GLFX">功率方向</param>
        /// <param name="Yj">元件</param>
        /// <param name="Glys">功率因素</param>
        /// <param name="xIb">电流倍数</param>
        /// <returns></returns>
        public static string getZouZiPrjID(Cus_PowerFangXiang GLFX, Cus_PowerYuanJian Yj, string Glys, string xIb)
        {
            Glys = Glys.TrimStart('-');
            return string.Format("{0}{1}{2}{3}"
                                , (int)GLFX
                                , (int)Yj
                                , CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysID(Glys)
                                , CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.xIbDic.getxIbID(xIb));
        }

    }
}

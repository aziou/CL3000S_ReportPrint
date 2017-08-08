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
    /// 特殊检定方案
    /// </summary>
    [Serializable()]
    public class Plan_Specal:Plan_Base 
    {
        /// <summary>
        /// 特殊检定项目列表
        /// </summary>
        private List<StPlan_SpecalCheck> _LstSpecal;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型0-三相台，1-单相台</param>
        /// <param name="vFAName">方案名称</param>
        public Plan_Specal(int TaiType, string vFAName)
            : base(CLDC_DataCore.Const.Variable.CONST_FA_TS_FOLDERNAME, TaiType, vFAName)
        {
            this.Load();
        }
        ~Plan_Specal()
        {
            _LstSpecal = null;
        }
        /// <summary>
        /// 加载特殊检定方案
        /// </summary>
        private void Load()
        {
            string _ErrorString = "";
            try
            {
                _LstSpecal = new List<StPlan_SpecalCheck>();
                XmlNode _XmlNode = clsXmlControl.LoadXml(_FAPath, out _ErrorString);
                if (_ErrorString != "")
                    return;
                for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
                {
                    StPlan_SpecalCheck _Item = new StPlan_SpecalCheck();
                    _Item.PrjName = _XmlNode.ChildNodes[_i].Attributes["PrjName"].Value;            //项目名称
                    _Item.PowerFangXiang = (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(_XmlNode.ChildNodes[_i].Attributes["GLFX"].Value);       //功率方向
                    _Item.PowerYinSu = _XmlNode.ChildNodes[_i].Attributes["GLYS"].Value;            //功率因素
                    _Item.ExplainUString(_XmlNode.ChildNodes[_i].Attributes["xU"].Value);           //解析电压倍数
                    _Item.ExplainIString(_XmlNode.ChildNodes[_i].Attributes["xIb"].Value);          //解析电流倍数
                    _Item.ExplainXwString(_XmlNode.ChildNodes[_i].Attributes["Xw"].Value);          //解析相位
                    _Item.ExplainWcx(_XmlNode.ChildNodes[_i].Attributes["Wcx"].Value);              //解析误差限
                    _Item.PingLv = float.Parse(_XmlNode.ChildNodes[_i].Attributes["Pl"].Value);     //频率
                    _Item.WcCheckNumic = int.Parse(_XmlNode.ChildNodes[_i].Attributes["Wccs"].Value);   //误差次数
                    _Item.LapCount = int.Parse(_XmlNode.ChildNodes[_i].Attributes["qs"].Value);
                    _Item.XieBoFa = _XmlNode.ChildNodes[_i].Attributes["xiebo"].Value.Trim();           //谐波方案
                    if (_Item.XieBoFa == string.Empty)
                    {
                        _Item.XieBo = 0;
                    }
                    else
                    {
                        _Item.XieBo = 1;
                    }
                    _Item.XiangXu = int.Parse(_XmlNode.ChildNodes[_i].Attributes["xiangxu"].Value);
                    _LstSpecal.Add(_Item);
                }
            }
            catch
            { 
                
            }
            return;
        }
        /// <summary>
        /// 存储特殊检定方案到XML文档
        /// </summary>
        public void Save()
        {
            if (_LstSpecal.Count == 0)
                return;
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "TSJD", "Name", Name);
            for (int _i = 0; _i < _LstSpecal.Count; _i++)
            { 
                _XmlNode.appendchild(""
                                    ,"R"
                                    ,"PrjName",_LstSpecal[_i].PrjName 
                                    , "GLFX", ((int)_LstSpecal[_i].PowerFangXiang).ToString()
                                    ,"GLYS",_LstSpecal[_i].PowerYinSu
                                    ,"xU",_LstSpecal[_i].JoinxUString()
                                    ,"xIb",_LstSpecal[_i].JoinxIString()
                                    ,"Pl",_LstSpecal[_i].PingLv.ToString()
                                    ,"Xw",_LstSpecal[_i].JoinXwString()
                                    , "Wcx",_LstSpecal[_i].JoinWcxString()
                                    ,"Wccs",_LstSpecal[_i].WcCheckNumic.ToString()
                                    ,"qs",_LstSpecal[_i].LapCount.ToString()
                                    ,"xiebo",_LstSpecal[_i].XieBo==0?"":_LstSpecal[_i].XieBoFa 
                                    ,"xiangxu",_LstSpecal[_i].XiangXu.ToString());
            }
            _XmlNode.SaveXml(_FAPath);
            return;
        }
        /// <summary>
        /// 添加一个项目信息
        /// </summary>
        /// <param name="Item">特殊检定项目</param>
        /// <returns></returns>
        public bool Add(StPlan_SpecalCheck Item)
        {
            if (_LstSpecal.Contains(Item))
                return false;
            _LstSpecal.Add(Item);
            return true;
        }

        ///// <summary>
        ///// 添加一个项目信息
        ///// </summary>
        ///// <param name="PrjName">项目名称</param>
        ///// <param name="GLFX">功率方向</param>
        ///// <param name="Yj">元件</param>
        ///// <param name="xU">电压倍数（数字）</param>
        ///// <param name="xIb">电流倍数（数字）</param>
        ///// <param name="PL">频率（数字）</param>
        ///// <param name="Glys">功率因素(字典中存在的值)</param>
        ///// <param name="wcsx">误差上线</param>
        ///// <param name="wcxx">误差下线</param>
        ///// <param name="Qs">圈数</param>
        ///// <param name="xiebo">0不加，1加</param>
        ///// <param name="xiangxu">0正，1逆</param>
        ///// <returns></returns>
        //public bool Add(string PrjName
        //                ,Cus_PowerFangXiang GLFX
        //                ,Cus_PowerYuanJiang Yj
        //                ,float xU
        //                ,float xIb
        //                ,float PL
        //                ,string Glys
        //                ,string wcsx
        //                ,string wcxx
        //                ,int Qs
        //                ,string XieBo
        //                ,int xiangxu)
        //{
        //    StSpecalCheckPlan _Item = new StSpecalCheckPlan();
            
        //    _Item.PrjID = getTsjdPrjID(GLFX, Yj, Glys, XieBo==""?0:1, xiangxu);
        //    _Item.PrjName = PrjName;
        //    _Item.floatxUb = xU;
        //    _Item.floatxIb = xIb;
        //    _Item.PingLv = PL;
        //    _Item.Glys = Glys;
        //    _Item.LapCount = Qs;
        //    _Item.WuChaXian_Shang = float.Parse(wcsx.Replace("+", ""));
        //    _Item.WuChaXian_Xia = float.Parse(wcxx.Replace("+", ""));
        //    _Item.XieBoFa = XieBo;
        //    if (_LstSpecal.Contains(_Item))
        //        return false;
        //    _LstSpecal.Add(_Item);
        //    return true;
        //}
        /// <summary>
        /// 移除所有方案项目
        /// </summary>
        public void RemoveAll()
        {
            _LstSpecal.Clear();
        }
        /// <summary>
        /// 返回方案中包含的项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstSpecal.Count;
            }
        }

        /// <summary>
        /// 根据列表索引ID获取项目数据
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public StPlan_SpecalCheck getSpecalPrj(int i)
        {
            if (i >= _LstSpecal.Count)
                return new StPlan_SpecalCheck();

            StPlan_SpecalCheck Item = _LstSpecal[i];
            Item.LoadXieBo();       //加载一次谐波参数
            return Item;
        }

        /// <summary>
        /// 移动特殊检定项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, StPlan_SpecalCheck Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstSpecal.Count ? _LstSpecal.Count - 1 : i;
            this.Remove(Item);
            _LstSpecal.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 根据列表索引移除
        /// </summary>
        /// <param name="i">项目索引号</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstSpecal.Count)
                return;
            _LstSpecal.RemoveAt(i);
            return;
        }
        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(StPlan_SpecalCheck Item)
        {
            if (!_LstSpecal.Contains(Item))
                return;
            _LstSpecal.Remove(Item);
            return;
        }

        ///// <summary>
        ///// 获取特殊检定项目ID
        ///// </summary>
        ///// <param name="GLFX">功率方向</param>
        ///// <param name="Yj">元件</param>
        ///// <param name="Glys">功率因素</param>
        ///// <param name="XieBo">谐波 0不加，1加</param>
        ///// <param name="Xiangxu">相序 0正相续，1逆相序</param>
        ///// <returns></returns>
        //public static string getTsjdPrjID( Enum.Cus_PowerFangXiang GLFX, Enum.Cus_PowerYuanJiang Yj, string Glys, int XieBo, int Xiangxu)
        //{
        //    SystemModel.Item.csGlys _GlysDic = new Comm.SystemModel.Item.csGlys();
        //    _GlysDic.Load();
        //    return string.Format("{0}{1}{2}{3}{4}{5}"
        //                        ,"3"
        //                        , (int)GLFX
        //                        , (int)Yj
        //                        , _GlysDic.getGlysID(Glys)
        //                        , XieBo.ToString()
        //                        , Xiangxu.ToString());
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 误差一致性方案
    /// </summary>
    [Serializable()]
    public class Plan_ErrAccord : Plan_Base 
    {
        /// <summary>
        /// 方案检定点列表
        /// </summary>
        private List<StErrAccord> _LstErrAccord;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型0-三相台，1-单相台</param>
        /// <param name="vFAName">方案名称</param>
        public Plan_ErrAccord(int TaiType, string vFAName)
            :base(CLDC_DataCore.Const.Variable.CONST_FA_YZX_FOLDERNAME,TaiType,vFAName)
        {
            this.Load();
        }

        ~Plan_ErrAccord()
        {
            _LstErrAccord = null;
        }

        private void Load()
        {
            _LstErrAccord = new List<StErrAccord>();
            
            clsXmlControl _XmlNode = new clsXmlControl(_FAPath);

            if (_XmlNode.Count() == 0)
            {
                return;
            }

            for (int _i = 1; _i < 5; _i++)     //项目类型
            {
                XmlNode _Xml = _XmlNode.toXmlNode();
                _Xml = clsXmlControl.FindSencetion(_Xml, clsXmlControl.XPath(string.Format("R,PrjType,{0}", _i.ToString())));
                if (_Xml == null)
                {
                    continue;
                }

                CLDC_DataCore.Struct.StErrAccord _Prj = new StErrAccord();
                _Prj.PrjName = _Xml.Attributes["PrjName"].Value;
                _Prj.ErrAccordType = int.Parse(_Xml.Attributes["PrjType"].Value);
                _Prj.Time1 = float.Parse(_Xml.Attributes["Time1"].Value);
                _Prj.Time2 = float.Parse(_Xml.Attributes["Time2"].Value);

                //string[] _TmpArr = _XmlNode.AttributeValue("", "PrjName", "Time1", "Time2");
                
                //_Prj.PrjName = _TmpArr[0];
                //_Prj.Time1 = float.Parse(_TmpArr[1]);
                //_Prj.Time2 = float.Parse(_TmpArr[2]);
                _Prj.lstErrPoint = new List<StErrAccordbase>();
                //List<stErrAccordbase> _Prj
                for (int _j = 0; _j < _Xml.ChildNodes.Count; _j++)
                {
                    CLDC_DataCore.Struct.StErrAccordbase _Point = new CLDC_DataCore.Struct.StErrAccordbase();
                    _Point.PrjID = _Xml.ChildNodes[_j].Attributes["PrjID"].Value;
                    _Point.TestPointName = _Xml.ChildNodes[_j].Attributes["TestPointName"].Value;
                    _Point.PowerYinSu = _Xml.ChildNodes[_j].Attributes["GLYS"].Value;
                    _Point.PowerDianLiu = _Xml.ChildNodes[_j].Attributes["xIb"].Value;
                    _Prj.lstErrPoint.Add(_Point);
                }

                _LstErrAccord.Add(_Prj);
            }
            return;
        }

        /// <summary>
        /// 存储误差一致性方案
        /// </summary>
        public void Save()
        {
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("", "WcYzx", "Name", Name);
            for (int _i = 0; _i < _LstErrAccord.Count; _i++)
            {
                if (clsXmlControl.FindSencetion(_XmlNode.toXmlNode(), clsXmlControl.XPath(string.Format("R,PrjType,{0}", ((int)_LstErrAccord[_i].ErrAccordType).ToString()))) == null)
                {
                    _XmlNode.appendchild(""
                                    , "R"
                                    , "PrjType"
                                    , ((int)_LstErrAccord[_i].ErrAccordType).ToString()
                                    , "PrjName", _LstErrAccord[_i].PrjName
                                    , "Time1", _LstErrAccord[_i].Time1.ToString()
                                    , "Time2", _LstErrAccord[_i].Time2.ToString());
                }
                for (int _j = 0; _j < _LstErrAccord[_i].lstErrPoint.Count; _j++)
                {
                    _XmlNode.appendchild(clsXmlControl.XPath(string.Format("R,PrjType,{0}", ((int)_LstErrAccord[_i].ErrAccordType).ToString()))
                                     , "C"
                                     , "PrjID", _LstErrAccord[_i].lstErrPoint[_j].PrjID
                                     , "TestPointName", _LstErrAccord[_i].lstErrPoint[_j].TestPointName
                                     , "GLYS", _LstErrAccord[_i].lstErrPoint[_j].PowerYinSu
                                     , "xIb", _LstErrAccord[_i].lstErrPoint[_j].PowerDianLiu);
                }
            }
            _XmlNode.SaveXml(_FAPath);
        }

        /// <summary>
        /// 返回方案中包含的项目数量
        /// </summary>
        public int Count
        {
            get
            {
                return _LstErrAccord.Count;
            }
        }

        /// <summary>
        /// 添加一个需要检定的误差点
        /// </summary>
        /// <param name="WcType">误差类型</param>
        /// <param name="Time1">时间1</param>
        /// <param name="Time2">时间2</param>
        public void Add(CLDC_Comm.Enum.Cus_WcType WcType, string Para1, string Para2, string Para3, string Para4, float time1, float time2)
        {
            CLDC_DataCore.Struct.StErrAccord _Point = new CLDC_DataCore.Struct.StErrAccord();
            CLDC_DataCore.Struct.StErrAccordbase _PointBase = new StErrAccordbase();

            _Point.PrjName = string.Format("{0} {1} {2} {3} {4}",
                                            Para1, Para2, Para3, Para4, WcType.ToString());
            
            _Point.lstErrPoint = new List<StErrAccordbase>();
            string[] strPara = { Para1,Para2,Para3,Para4};


            //给默认参数
            //      功率方向：正向有功
            //      元    件：合元
            //      谐    波：0不加
            //      相    序：0正相续
            CLDC_Comm.Enum.Cus_PowerFangXiang _Fx = CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功;
            CLDC_Comm.Enum.Cus_PowerYuanJian _Yj = CLDC_Comm.Enum.Cus_PowerYuanJian.H;
            int _XieBo = 0;
            int _Xx    = 0;


            string strIb = "";
            string strGlys = "";
            string[] strTmp = new string[2];

            for (int i = 0; i < 4; i++)
            {
                if(strPara[i].Trim() != "")
                {
                    strTmp = strPara[i].Split('|');
                    strIb = strTmp[0];
                    strGlys = strTmp[1];
                    _PointBase = new StErrAccordbase();

                    _PointBase.PrjID = getWcPrjID(WcType, _Fx, _Yj, strGlys, strIb, _XieBo, _Xx);


                    string _GlfxString;
                    switch ((int)_Fx)
                    {
                        case 1:
                            {
                                _GlfxString = "P+";
                                break;
                            }
                        case 2:
                            {
                                _GlfxString = "P-";
                                break;
                            }
                        case 3:
                            {
                                _GlfxString = "Q+";
                                break;
                            }
                        case 4:
                            {
                                _GlfxString = "Q-";
                                break;
                            }
                        default:
                            {
                                _GlfxString = "P+";
                                break;
                            }
                    }

                    string strYj = "";

                    switch (_Yj.ToString().ToUpper())
                    {
                        case "H":
                            strYj = "合元";
                            break;
                        case "A":
                            strYj = "A元";
                            break;
                        case "B":
                            strYj = "B元";
                            break;
                        case "C":
                            strYj = "C元";
                            break;
                        default:
                            strYj = "合元";
                            break;
                    }

                    _PointBase.TestPointName = string.Format("{0} {1} {2} {3}",
                                                    _GlfxString, strYj, strGlys, strIb);
                    _PointBase.PowerYinSu = strGlys;
                    _PointBase.PowerDianLiu = strIb;
                    _Point.lstErrPoint.Add(_PointBase);
                }
            }

            _Point.ErrAccordType = (int)WcType - 3;
            _Point.Time1 = time1;
            _Point.Time2 = time2;

            _LstErrAccord.Add(_Point);
            return;
        }

        /// <summary>
        /// 获取误差项目ID
        /// </summary>
        /// <param name="WcType">误差类型</param>
        /// <param name="GLFX">功率方向</param>
        /// <param name="Yj">元件</param>
        /// <param name="Glys">功率因素</param>
        /// <param name="xIb">电流倍数</param>
        /// <param name="XieBo">谐波 0不加，1加</param>
        /// <param name="Xiangxu">相序 0正相续，1逆相序</param>
        /// <returns></returns>
        public static string getWcPrjID(CLDC_Comm.Enum.Cus_WcType WcType, CLDC_Comm.Enum.Cus_PowerFangXiang GLFX, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string Glys, string xIb, int XieBo, int Xiangxu)
        {            
            if (xIb == "10Ib")
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}"
                                    , (int)WcType
                                    , (int)GLFX
                                    , (int)Yj
                                    , CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysID(Glys)
                                    , "99"
                                    , XieBo.ToString(), Xiangxu.ToString());
            }
            else
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}"
                                    , (int)WcType
                                    , (int)GLFX
                                    , (int)Yj
                                    , CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysID(Glys)
                                    , CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.xIbDic.getxIbID(xIb)
                                    , XieBo.ToString(), Xiangxu.ToString());
            }
        }

        /// <summary>
        /// 获取误差一致性项目
        /// </summary>
        /// <param name="i">项目列表索引</param>
        /// <returns></returns>
        public StErrAccord getErrAccordPrj(int i)
        {
            if (i >= _LstErrAccord.Count)
                return new StErrAccord();
            return _LstErrAccord[i];
        }

        /// <summary>
        /// 移动误差一致性项目
        /// </summary>
        /// <param name="i">需要移动到的列表位置</param>
        /// <param name="Item">项目结构体</param>
        public void Move(int i, StErrAccord Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstErrAccord.Count ? _LstErrAccord.Count - 1 : i;
            this.Remove(Item);
            _LstErrAccord.Insert(i, Item);
            return;
        }

        /// <summary>
        /// 移除全部项目
        /// </summary>
        public void RemoveAll()
        {
            _LstErrAccord.Clear();
        }

        /// <summary>
        /// 根据项目移除
        /// </summary>
        /// <param name="Item">项目结构体</param>
        public void Remove(StErrAccord Item)
        {
            if (!_LstErrAccord.Contains(Item))
                return;
            _LstErrAccord.Remove(Item);
            return;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;
using CLDC_DataCore.DataBase;
using System.Xml;
using System.Windows.Forms;
using System.Threading;

namespace CLDC_DataCore.SystemModel.Item
{
    /// <summary>
    /// 
    /// </summary>
    public class csWcLimit
    {
        /// <summary>
        /// 加载进度
        /// </summary>
        enum  LoadProcess
        {
            NotStart = 0 ,
            Doing = 1,
            Complated = 2
        }

        private List<LoadProcess> LstLoadProcess = new List<LoadProcess>();
        /// <summary>
        /// XML节点对象
        /// </summary>
        private clsXmlControl _XmlNode;

        /// <summary>
        /// 
        /// </summary>
        public string WcLimitName = "";

        /// <summary>
        /// 
        /// </summary>
        public csWcLimit()
        { 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="LimitName"></param>
        public csWcLimit(string LimitName)
        {
            WcLimitName = LimitName;
            this.Load();
        }
        /// <summary>
        /// 
        /// </summary>
        ~csWcLimit()
        {
            _XmlNode = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Remove()
        {

            CLDC_DataCore.Function.File.RemoveFile(Application.StartupPath + Const.Variable.CONST_WCLIMIT + @"\" + WcLimitName + ".xml");
            _XmlNode = null;
        }

        ///// <summary>
        ///// 静态函数，返回内控误差限文件名
        ///// </summary>
        ///// <returns></returns>
        //public static List<string> getWcLimitFileNames()
        //{
        //    return Comm.Function.Folder.getFileNames(Application.StartupPath + Const.Variable.CONST_WCLIMIT,"*.xml");
        //}

        /// <summary>
        /// 加载误差限值文档
        /// </summary>
        public void Load()
        {
            _XmlNode = new clsXmlControl(Application.StartupPath + "\\Const\\WcLimit.xml");

            if (_XmlNode == null || _XmlNode.Count() == 0)
            {
                #region 初始误差限参数信息
                StringBuilder sbXmlNode = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                string[] _GuiChengName ={ "JJG596-1999", "JJG307-1988", "JJG307-2006" };
                
                sbXmlNode.Append("<WcLimit>");
                if (_GuiChengName.Length > 0)
                {
                    for (int _GC = 0; _GC < _GuiChengName.Length; _GC++)
                    {
                        //标志进度
                        if (LstLoadProcess.Count <= _GC)
                            LstLoadProcess.Add(LoadProcess.NotStart);
                        LstLoadProcess[_GC] = LoadProcess.NotStart;

                        ThreadPool.QueueUserWorkItem(new WaitCallback(thLoad), new object[] { _GuiChengName[_GC], sbXmlNode, _GC });
                    }

                    //等待所有线程执行完毕
                    for (int i = 0; i < LstLoadProcess.Count; i++)
                    {
                        Thread.Sleep(10);
                        if (LstLoadProcess[i] != LoadProcess.Complated)
                        {
                            i = 0;
                            continue;
                        }
                    }
                }
                sbXmlNode.Append("</WcLimit>");
                System.IO.File.WriteAllText(Application.StartupPath + Const.Variable.CONST_WCLIMIT + @"\" + WcLimitName + ".xml", sbXmlNode.ToString(), Encoding.UTF8);
                _XmlNode = new clsXmlControl(Application.StartupPath + Const.Variable.CONST_WCLIMIT + @"\" + WcLimitName + ".xml");
                #endregion

            } 

        }

        private void thLoad(object objParam)
        {
            string GuiChengName     = (string)((object[])objParam)[0];
            //clsXmlControl _XmlNode  = (clsXmlControl)((object[])objParam)[1]; 
            StringBuilder sbXmlNode = (StringBuilder)((object[])objParam)[1]; 
            int index = (int)((object[])objParam)[2];
            LstLoadProcess[index] = LoadProcess.Doing;
            StringBuilder sbTmp = new StringBuilder();
            this.Load(GuiChengName, sbTmp);
            
            sbXmlNode.Append(sbTmp);
            
            LstLoadProcess[index] = LoadProcess.Complated;
        }

        private void Load(string GuiChengName,StringBuilder sbXmlNode)
        {
            string[] _Dj ={ "0.2", "0.5", "1.0", "2.0", "3.0" };
            int[] _Yj ={ 1, 2, 3, 4 };
            string[] _xIb ={ "Imax", "0.5Imax", "3.0Ib", "2.0Ib", "(Imax-Ib)/2", "1.5Ib", "1.0Ib", "0.8Ib", "0.5Ib", "0.2Ib", "0.1Ib", "0.05Ib", "0.02Ib", "0.01Ib" };
            string[] _Glys ={ "1.0", "0.5L", "0.8C", "0.5C", "0.8L", "0.25L", "0.25C" };
            for (int _Yg = 0; _Yg <= 1; _Yg++)
            {
                if (GuiChengName.IndexOf("307") >= 0)
                    sbXmlNode.AppendFormat("<{0} {1}=\"{2}\" {3}=\"{4}\">","R","Name",GuiChengName,"YouGong",_Yg);
                else
                {
                    if (_Yg > 0)
                        continue;
                    else
                        sbXmlNode.AppendFormat("<{0} {1}=\"{2}\" {3}=\"{4}\">", "R", "Name", GuiChengName, "YouGong", "");
                }
                for (int _DjIndex = 0; _DjIndex < _Dj.Length; _DjIndex++)
                {
                    for (int _YjIndex = 0; _YjIndex < _Yj.Length; _YjIndex++)
                    {
                        for (int _xIbIndex = 0; _xIbIndex < _xIb.Length; _xIbIndex++)
                        {
                            for (int _GlysIndex = 0; _GlysIndex < _Glys.Length; _GlysIndex++)
                            {
                                for (int _Hgq = 0; _Hgq <= 1; _Hgq++)
                                {
                                    string _TmpYouGong = "";
                                    if (GuiChengName.IndexOf("307") >= 0)
                                        _TmpYouGong = _Yg.ToString();
                                    string _Wcx = Wcx(_xIb[_xIbIndex], GuiChengName, _Dj[_DjIndex], (CLDC_Comm.Enum.Cus_PowerYuanJian)_Yj[_YjIndex], _Glys[_GlysIndex], _Hgq == 1 ? true : false, _Yg == 1 ? true : false);
                                    
                                    sbXmlNode.AppendFormat("<C Dj=\"{0}\" Hgq=\"{1}\" Yj=\"{2}\" Glys=\"{3}\" xIb=\"{4}\" Max=\"{5}\" Min=\"{6}\" />"
                                            , _Dj[_DjIndex]
                                            , _Hgq
                                            , _Yj[_YjIndex]
                                            , _Glys[_GlysIndex]
                                            , _xIb[_xIbIndex]
                                            , "+" + _Wcx
                                            , "-" + _Wcx
                                        );

                                }
                            }
                        }
                    }
                }
                sbXmlNode.Append("</R>");
            }
        
        }

        /// <summary>
        /// 存储文档
        /// </summary>
        public void Save()
        {
            _XmlNode.SaveXml();
        }
        /// <summary>
        /// 获取规程列表
        /// </summary>
        /// <returns></returns>
        public List<string> getGuiChengName()
        {
            List<string> _Item = new List<string>();

            XmlNode _Xml = _XmlNode.toXmlNode();

            for (int i = 0; i < _Xml.ChildNodes.Count; i++)
            {
                bool _IsExist=false;
                foreach (string _Tmpstring in _Item)
                {
                    if (_Tmpstring == _Xml.ChildNodes[i].Attributes["Name"].Value)
                    {
                        _IsExist = true;
                        break;
                    }
                }
                if(!_IsExist)
                    _Item.Add(_Xml.ChildNodes[i].Attributes["Name"].Value);
            }

            return _Item;
        }
        /// <summary>
        /// 获取等级列表
        /// </summary>
        /// <returns></returns>
        public List<string> getDjStrings()
        {

            List<string> _Item = new List<string>();
            if (_XmlNode == null)
                return _Item;

            XmlNode _Xml = _XmlNode.toXmlNode().ChildNodes[0];

            XmlNodeList _XmlList=_Xml.SelectNodes(CLDC_DataCore.DataBase.clsXmlControl.XPath("C,Hgq,0,Yj,1,Glys,1.0,xIb,Imax"));
            for (int i = 0; i < _XmlList.Count; i++)
            {
                if(!_Item.Contains(_XmlList[i].Attributes["Dj"].Value))          //不存在才追加
                    _Item.Add(_XmlList[i].Attributes["Dj"].Value);
            }
            _Item.Sort();
            return _Item;
        
        }

        /// <summary>
        /// 获取误差上下限
        /// </summary>
        /// <param name="GuichengName">规程名称</param>
        /// <param name="YouGong">是否有功1-有功，0-无功</param>
        /// <param name="Dj">等级</param>
        /// <param name="Hgq">互感器0-不经互感器，1-经互感器</param>
        /// <param name="Yj">元件</param>
        /// <param name="Glys">功率因素</param>
        /// <param name="xIb">电流倍数</param>
        /// <returns>数组，下标0为上线，下标1为下线</returns>
        public string[] getWcx(string GuichengName,
                               int YouGong,
                               string Dj,
                               int Hgq,
                               CLDC_Comm.Enum.Cus_PowerYuanJian Yj,
                               string Glys,
                               string xIb) 
        {
            string[] _Wcx;
            string _YouGong = YouGong.ToString();
            if (GuichengName.IndexOf("307") == -1)
                _YouGong = "";
            string _Xpath = string.Format("R,Name,{0},YouGong,{1}|C,Dj,{2},Hgq,{3},Yj,{4},Glys,{5},xIb,{6}"
                                         , GuichengName
                                         , _YouGong
                                         , Dj
                                         , Hgq.ToString()
                                         , ((int)Yj).ToString()
                                         , Glys
                                         , xIb);
            _Xpath=clsXmlControl.XPath(_Xpath);
            
            _Wcx= _XmlNode.AttributeValue(_Xpath,"Max","Min");
            if (_Wcx[0] == null)      //如果在XML文档中找不到对应误差限，就从规程中获取误差限
            {
                string _TmpWcx = Wcx(xIb,GuichengName, Dj, Yj, Glys, Hgq==1?true:false, YouGong==1?true:false);
                _Wcx[0] = "+" + _TmpWcx;
                _Wcx[1] = "-" + _TmpWcx;
                this.SetWcx(GuichengName, YouGong, Dj, Hgq, Yj, Glys, xIb, _Wcx[0], _Wcx[1]);
            }
            return _Wcx;
        }
        /// <summary>
        /// 修改误差限
        /// </summary>
        /// <param name="GuichengName">规程名称</param>
        /// <param name="YouGong">是否有功0-无功，1-有功</param>
        /// <param name="Dj">等级</param>
        /// <param name="Hgq">是否经互感器1-经互感器，0-不经</param>
        /// <param name="Yj">元件</param>
        /// <param name="Glys">功率因素</param>
        /// <param name="xIb">电流倍数</param>
        /// <param name="Max">误差上限,可以为空，为空则不修改</param>
        /// <param name="Min">误差下限，可以为空，为空这不修改</param>
        public void SetWcx(string GuichengName,
                               int YouGong,
                               string Dj,
                               int Hgq,
                               CLDC_Comm.Enum.Cus_PowerYuanJian Yj,
                               string Glys,
                               string xIb, string Max, string Min)
        {
            string _YouGong = YouGong.ToString();
            if (GuichengName.IndexOf("307") == -1)
                _YouGong = "";
            string _Xpath = string.Format("R,Name,{0},YouGong,{1}|C,Dj,{2},Hgq,{3},Yj,{4},Glys,{5},xIb,{6}"
                                         , GuichengName
                                         , _YouGong
                                         , Dj
                                         , Hgq.ToString()
                                         , ((int)Yj).ToString()
                                         , Glys
                                         , xIb);
            _Xpath = clsXmlControl.XPath(_Xpath);
            bool _Result;
            if (Max != "" && Min != "")
                _Result = _XmlNode.EditAttibuteValue(_Xpath, "Max|" + Max, "Min|" + Min);
            else if (Max != "")
            {
                _Result = _XmlNode.EditAttibuteValue(_Xpath, "Max|" + Max);
                Min = Max.Replace("+", "-");
            }
            else if (Min != "")
            {
                _Result = _XmlNode.EditAttibuteValue(_Xpath, "Min|" + Min);
                Max = Min.Replace("-", "+");
            }
            else
                _Result = true;
            if (_Result != false)
                return;
            _XmlNode.appendchild(clsXmlControl.XPath("R,Name," + GuichengName + ",YouGong," + _YouGong)
                                 , "C"
                                 , "Dj"
                                 , Dj
                                 , "Hgq"
                                 , Hgq.ToString()
                                 , "Yj"
                                 , ((int)Yj).ToString()
                                 , "Glys"
                                 , Glys
                                 , "xIb"
                                 , xIb,
                                 "Max", Max, "Min", Min);
            return;
        }

        /// <summary>
        /// 修改误差限
        /// </summary>
        /// <param name="GuichengName">规程名称</param>
        /// <param name="YouGong">是否有功0-无功，1-有功</param>
        /// <param name="Dj">等级</param>
        /// <param name="Hgq">是否经互感器1-经互感器，0-不经</param>
        /// <param name="Yj">元件</param>
        /// <param name="Glys">功率因素</param>
        /// <param name="xIb">电流倍数</param>
        /// <param name="WcLimit">误差限（上限|下限）</param>
        public void SetWcx(string GuichengName,
                       int YouGong,
                       string Dj,
                       int Hgq,
                       CLDC_Comm.Enum.Cus_PowerYuanJian Yj,
                       string Glys,
                       string xIb, string WcLimit)
        {
            string[] Wc = WcLimit.Split('|');

            if (Wc.Length != 2) return;

            this.SetWcx(GuichengName, YouGong, Dj, Hgq, Yj, Glys, xIb, Wc[0], Wc[1]);
        
        }

        /// <summary>
        /// 获取误差限
        /// </summary>
        /// <param name="xIb">电流倍数</param>
        /// <param name="GuiChengName">规程名称</param>
        /// <param name="Dj">等级</param>
        /// <param name="Yj">元件</param>
        /// <param name="glys">功率因素</param>
        /// <param name="Hgq">互感器</param>
        /// <param name="YouGong">是否有功</param>
        /// <returns></returns>
        public static string Wcx(string xIb, string GuiChengName, string Dj, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq,bool YouGong)
        {
            switch (GuiChengName.ToUpper())
            {
                case "JJG596-1999":
                    {
                        switch (Dj)
                        {
                            case "0.02":
                                return getdz002(xIb, Yj, glys, Hgq);
                            case "0.05":
                                return getdz005(xIb, Yj, glys, Hgq);
                            case "0.1":
                                return getdz01(xIb, Yj, glys, Hgq);
                            case "0.2":
                                if ((int)Yj == 1)  //合元
                                {
                                    return getdz02(xIb, Yj, glys, Hgq);
                                }
                                else
                                {
                                    if (glys == "1.0")
                                        return "0.3";
                                    else
                                        return "0.4";
                                }
                            case "0.5":
                                if ((int)Yj == 1)  //合元
                                {
                                    return getdz05(xIb, Yj, glys, Hgq);
                                }
                                else
                                {
                                    if (glys == "1.0")
                                        return "0.6";
                                    else
                                        return "1.0";
                                }
                            case "1.0":
                                if ((int)Yj == 1)  //合元
                                {
                                    return getdz10(xIb, Yj, glys, Hgq);
                                }
                                else
                                {
                                    if (glys == "1.0")
                                        return "2.0";
                                    else
                                        return "2.0";
                                }
                            case "2.0":
                                if ((int)Yj == 1)  //合元
                                {
                                    return getdz20(xIb, Yj, glys, Hgq);
                                }
                                else
                                {
                                    if (glys == "1.0")
                                        return "3.0";
                                    else
                                        return "3.0";
                                }
                            case "3.0":
                                if ((int)Yj == 1)  //合元
                                {
                                    return getdz30(xIb, Yj, glys, Hgq);
                                }
                                else
                                {
                                    if (glys == "1.0")
                                        return "4.0";
                                    else
                                        return "4.0";
                                }
                            default:
                                return Dj;
                        }
                    }
                case "JJG307-1988":
                    {
                        return getGy(xIb, "JJG307-1988", Dj, Yj, glys, Hgq, YouGong);
                    }
                case "JJG307-2006":
                    {
                        return getGy(xIb, "JJG307-2006", Dj, Yj, glys, Hgq, YouGong);
                    }
                default :
                    return Dj;
            }
        }
        /// <summary>
        /// 电子式0.02级
        /// </summary>
        /// <param name="xIb">电流倍数</param>
        /// <param name="Yj">元件</param>
        /// <param name="glys">功率因素</param>
        /// <param name="Hgq">互感器</param>
        /// <returns></returns>
        private static string getdz002(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
                xIb = "1.0ib";
            if ((int)Yj == 1)
                if (glys == "1.0")
                {
                    _WcLimit = "0.02";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "0.04";
                }
                else
                {
                    _WcLimit = "0.02";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F)
                        _WcLimit = "0.03";
                    else if (xIb.ToLower().IndexOf("imax") >= 0 || xIb.ToLower().IndexOf("imax-ib") >= 0 || float.Parse(xIb.ToLower().Replace("ib", "")) > 0.1F)
                        _WcLimit = "0.03";
                    if (glys == "0.25L")
                        _WcLimit = "0.04";
                    else if (glys == "0.5C")
                        _WcLimit = "0.03";
                }
            else
            {
                _WcLimit = "0.03";
                if (glys != "1.0" && (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F))
                    _WcLimit = "0.04";
            }
            return _WcLimit;

        }
        /// <summary>
        /// 电子式0.05级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz005(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if ((int)Yj == 1)
            {
                if (glys == "1.0")
                {
                    _WcLimit = "0.05";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "0.1";
                }
                else
                {
                    _WcLimit = "0.05";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "0.15";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F)
                        _WcLimit = "0.075";
                    if (xIb.ToLower().IndexOf("imax") >= 0 || xIb.ToLower().IndexOf("imax-ib") >= 0 || float.Parse(xIb.ToLower().Replace("ib", "")) > 0.1F)
                        _WcLimit = "0.075";
                    if (glys.ToUpper() == "0.5C")
                        _WcLimit = "0.1";
                    if (glys.ToUpper() == "0.25L")
                        _WcLimit = "0.15";
                }
            }
            else
            {
                _WcLimit = "0.075";
                if (glys != "1.0" && (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F))
                    _WcLimit = "0.1";
            }
            return _WcLimit;
        }
        /// <summary>
        /// 电子式0.1级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz01(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        { 
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if ((int)Yj == 1)
                if (glys == "1.0")
                {
                    _WcLimit = "0.1";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "0.2";
                }
                else
                {
                    _WcLimit = "0.1";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "0.3";
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F)
                        _WcLimit = "0.15";
                    if (xIb.ToLower().IndexOf("imax") >= 0 || xIb.ToLower().IndexOf("imax-ib") >= 0 || float.Parse(xIb.ToLower().Replace("ib", "")) > 0.1F)
                        _WcLimit = "0.15";
                    if (glys == "0.5C")
                        _WcLimit = "0.2";
                    if (glys == "0.25L")
                        _WcLimit = "0.3";
                }
            else
            {
                _WcLimit = "0.15";
                if (glys != "1.0" && (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F))
                    _WcLimit = "0.2";
            }
            return _WcLimit;
        }
        /// <summary>
        /// 电子式0.2级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz02(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        { 
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if (glys == "1.0")
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.05F)
                        _WcLimit = "0.4";
                    else
                        _WcLimit = "0.2";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "0.4";
                    else
                        _WcLimit = "0.2";
                }
            else if (glys.ToUpper() == "0.5C" || glys.ToUpper() == "0.25L")
                _WcLimit = "0.5";
            else
            {
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "0.5";
                    else
                        _WcLimit = "0.3";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "0.5";
                    else
                        _WcLimit = "0.3";
                }
            }

            return _WcLimit;
        }
        /// <summary>
        /// 电子式0.5级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz05(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if (glys == "1.0")
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.05F)
                        _WcLimit = "1";
                    else
                        _WcLimit = "0.5";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "0.4";
                    else
                        _WcLimit = "0.2";
                }
            else if (glys.ToUpper() == "0.5C" || glys.ToUpper() == "0.25L")
                _WcLimit = "1";
            else
            {
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "1";
                    else
                        _WcLimit = "0.6";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "1";
                    else
                        _WcLimit = "0.6";
                }
            }

            return _WcLimit;
        }
        /// <summary>
        /// 电子式1级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz10(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if (glys == "1.0")
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.05F)
                        _WcLimit = "1.5";
                    else
                        _WcLimit = "1.0";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "1.5";
                    else
                        _WcLimit = "1.0";
                }
            else if (glys.ToUpper() == "0.5C")
                _WcLimit = "2.5";
            else if (glys.ToUpper() == "0.25L")
                _WcLimit = "3.5";
            else
            {
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "1.5";
                    else
                        _WcLimit = "1.0";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "1.5";
                    else
                        _WcLimit = "1.0";
                }
            }

            return _WcLimit;
        }
        /// <summary>
        /// 电子式2级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz20(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if (glys == "1.0")
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.05F)
                        _WcLimit = "2.5";
                    else
                        _WcLimit = "2.0";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "2.5";
                    else
                        _WcLimit = "2.0";
                }
            else
            {
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "2.5";
                    else
                        _WcLimit = "2.0";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "2.5";
                    else
                        _WcLimit = "2.0";
                }
            }

            return _WcLimit;
        }
        /// <summary>
        /// 电子式3级
        /// </summary>
        /// <param name="xIb"></param>
        /// <param name="Yj"></param>
        /// <param name="glys"></param>
        /// <param name="Hgq"></param>
        /// <returns></returns>
        private static string getdz30(string xIb, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if (glys == "1.0")
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.05F)
                        _WcLimit = "3.5";
                    else
                        _WcLimit = "3.0";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "3.5";
                    else
                        _WcLimit = "3.0";
                }
            else
            {
                if (Hgq)
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                        _WcLimit = "3.5";
                    else
                        _WcLimit = "3.0";
                }
                else
                {
                    if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                        _WcLimit = "3.5";
                    else
                        _WcLimit = "3.0";
                }
            }

            return _WcLimit;
        }

        /// <summary>
        /// 获取感应式电能表的误差限
        /// </summary>
        /// <param name="xIb">电流倍数</param>
        /// <param name="GuiChengName">规程名称</param>
        /// <param name="Dj">等级</param>
        /// <param name="Yj">元件</param>
        /// <param name="glys">功率因素</param>
        /// <param name="Hgq">互感器</param>
        /// <param name="YouGong">有无功</param>
        /// <returns></returns>
        private static string getGy(string xIb, string GuiChengName, string Dj, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string glys, bool Hgq, bool YouGong)
        {
            string _WcLimit = "";
            if (xIb.ToLower() == "ib")
            {
                xIb = "1.0ib";
            }
            if (YouGong)        //有功
            {
                if ((int)Yj == 1)      //合元
                {
                    if (glys == "1.0")
                    {
                        _WcLimit = Dj;
                        if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.1F)
                            _WcLimit = (float.Parse(_WcLimit) + 0.5F).ToString();
                    }
                    else if (glys.ToUpper() == "0.5C")
                    {
                        switch (Dj)
                        {
                            case "0.5":
                                {
                                    _WcLimit = "1.5";
                                    break;
                                }
                            case "1.0":
                                {
                                    _WcLimit = "2.5";
                                    break;
                                }
                            case "2.0":
                                {
                                    _WcLimit = "3.5";
                                    break;
                                }
                            default:
                                {
                                    _WcLimit = "3.5";
                                    break;
                                }
                        }
                    }
                    else if (glys.ToUpper() == "0.25L")
                    {
                        switch (Dj)
                        {
                            case "0.5":
                                {
                                    _WcLimit = "2.5";
                                    break;
                                }
                            case "1.0":
                                {
                                    _WcLimit = "3.5";
                                    break;
                                }
                            case "2.0":
                                {
                                    _WcLimit = "4.5";
                                    break;
                                }
                            default:
                                {
                                    _WcLimit = "4.5";
                                    break;
                                }
                        }
                    }
                    else
                    {
                        switch (Dj)
                        {
                            case "0.5":
                                {
                                    _WcLimit = "0.8";
                                    break;
                                }
                            case "1.0":
                                {
                                    _WcLimit = "1.0";
                                    break;
                                }
                            case "2.0":
                                {
                                    _WcLimit = "2.0";
                                    break;
                                }
                            default:
                                {
                                    _WcLimit = "2.0";
                                    break;
                                }
                        }
                        if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                            _WcLimit = (float.Parse(_WcLimit) + 0.5F).ToString();
                    }
                }
                else
                {
                    switch (Dj)
                    {
                        case "0.5":
                            {
                                _WcLimit = "1.5";
                                break;
                            }
                        case "1.0":
                            {
                                _WcLimit = "2.0";
                                break;
                            }
                        case "2.0":
                            {
                                _WcLimit = "2.0";
                                break;
                            }
                        default:
                            {
                                _WcLimit = "3.0";
                                break;
                            }

                    }
                    if (xIb.ToLower().IndexOf("imax") >= 0 || xIb.ToLower().IndexOf("imax-ib") >= 0 || float.Parse(xIb.ToLower().Replace("ib", "")) > 1F)
                        _WcLimit = (float.Parse(_WcLimit) + 1F).ToString();
                }
            }
            else//无功
            {
                if ((int)Yj == 1)           //合元
                {
                    if (glys == "1.0")
                    {
                        _WcLimit = Dj;
                        if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.2F)
                            _WcLimit = (float.Parse(_WcLimit) + 1.0F).ToString();
                    }
                    else if (glys.ToUpper() == "0.25C" || glys.ToUpper() == "0.25L")
                    {
                        _WcLimit = (float.Parse(Dj) * 2F).ToString();
                    }
                    else
                    {
                        if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F)
                            _WcLimit = (float.Parse(Dj) + 2.0F).ToString();
                        else
                            _WcLimit = Dj;

                        if (GuiChengName.ToUpper() == "JJG307-2006")
                        {
                            if (xIb.ToLower().IndexOf("imax") == -1 && xIb.ToLower().IndexOf("imax-ib") == -1 && float.Parse(xIb.ToLower().Replace("ib", "")) < 0.5F)
                                _WcLimit = (float.Parse(_WcLimit) - 1F).ToString();
                        }
                    }
                }
                else
                    _WcLimit = (float.Parse(Dj) + 1.0F).ToString();
            }

            return _WcLimit;

        }
    }
}
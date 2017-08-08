using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CLDC_DataCore.DataBase;
using System.Xml;
using CLDC_DataCore.Struct;
namespace CLDC_DataCore.Model.Plan
{

    /// <summary>
    /// 检定点方案 、 包括误差上下限
    /// </summary>
    [Serializable()]
    public class Plan_WcPoint:Plan_Base 
    {
        #region ///Lost

        ///// <summary>
        ///// 检定点数据
        ///// </summary>
        //private DataTable _DtCheckPoint;

        //public DataTable DtCheckPoint
        //{
        //    get
        //    {
        //        return _DtCheckPoint;
        //    }
        //}

        //#region 构造函数
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        //public Error_CheckPoint()
        //{
        //    _DtCheckPoint = new DataTable();
        //    _DtCheckPoint.Columns.Add("内部编号", typeof(int));
        //    _DtCheckPoint.Columns.Add("检定顺序", typeof(int));
        //    _DtCheckPoint.Columns.Add("功率元件", typeof(byte));
        //    _DtCheckPoint.Columns.Add("功率方向", typeof(byte));
        //    _DtCheckPoint.Columns.Add("功率因数", typeof(float));
        //    _DtCheckPoint.Columns.Add("负载电流", typeof(float));
        //    _DtCheckPoint.Columns.Add("是否要检", typeof(bool));
        //    _DtCheckPoint.Columns.Add("检定圈数", typeof(int));
        //    _DtCheckPoint.Columns.Add("误差上限", typeof(float));
        //    _DtCheckPoint.Columns.Add("误差下限", typeof(float));

        //    //内部编号不能重复
        //    _DtCheckPoint.PrimaryKey = new DataColumn[] { _DtCheckPoint.Columns["内部编号"] };
        //}
        //#endregion

        //#region 按条件查询所有被检点
        ///// <summary>
        ///// 查询所有被检点
        ///// </summary>
        ///// <param name="ischeck">筛选条件、是否要选</param>
        ///// <returns></returns>
        //public List<Struct.CheckPoint> GetAllPoint(bool ischeck)
        //{
        //    List<Struct.CheckPoint> LstPoint = GetAllPoint();
        //    for (int i = 0; i < LstPoint.Count; i++)
        //    {
        //        if (LstPoint[i].IsCheck != ischeck)
        //            LstPoint.RemoveAt(i--);
        //    }
        //    return LstPoint;
        //}

        ///// <summary>
        ///// 查询所有被检点
        ///// </summary>
        ///// <returns></returns>
        //public List<Struct.CheckPoint> GetAllPoint()
        //{
        //    List<Struct.CheckPoint> LstPoint = new List<Struct.CheckPoint>();
        //    foreach (DataRow Row in _DtCheckPoint.Rows)
        //    {
        //        Struct.CheckPoint point = new Struct.CheckPoint();
        //        SetCheckPointData(ref point, Row);
        //        LstPoint.Add(point);
        //    }
        //    return LstPoint;
        //}
        //#endregion

        //#region 根据误差检定顺序获取误差点
        ///// <summary>
        ///// 根据误差检定顺序获取误差点、
        ///// 可以根据 CheckPoint.PowerYuanJian == Cus_PowerYuanJian.Error 来判断这个点是否存在
        ///// </summary>
        ///// <param name="checkOrder">检定顺序</param>
        ///// <returns></returns>
        //public Struct.CheckPoint GetPointByCheckOrder(int checkOrder)
        //{
        //    Struct.CheckPoint point = new Comm.Struct.CheckPoint();
        //    foreach (DataRow Row in _DtCheckPoint.Rows)
        //    {
        //        if (Convert.ToInt32(Row["检定顺序"]) == checkOrder)
        //        {
        //            SetCheckPointData(ref point, Row);
        //            break;
        //        }
        //    }
        //    return point;
        //}
        //#endregion

        //#region 根据内部编号或者检定的点
        ///// <summary>
        ///// 根据内部编号或者检定的点
        ///// 可以根据 CheckPoint.PowerYuanJian == Cus_PowerYuanJian.Error 来判断这个点是否存在
        ///// </summary>
        ///// <param name="pointid">内部编号</param>
        ///// <returns></returns>
        //public Struct.CheckPoint GetPointByPointId(int pointid)
        //{
        //    Struct.CheckPoint point = new Comm.Struct.CheckPoint();
        //    foreach (DataRow Row in _DtCheckPoint.Rows)
        //    {
        //        if (Convert.ToInt32(Row["内部编号"]) == pointid)
        //        {
        //            SetCheckPointData(ref point, Row);
        //            break;
        //        }
        //    }
        //    return point;
        //}
        //#endregion

        //#region 根据检定顺序、修改误差检定方案 检定点数据
        ///// <summary>
        ///// 根据检定顺序、修改误差检定方案 检定点数据
        ///// </summary>
        ///// <param name="checkOrder"></param>
        ///// <param name="point"></param>
        ///// <returns></returns>
        //public bool SetPointByCheckOrder(int checkOrder, Struct.CheckPoint point)
        //{
        //    for (int i = 0; i < _DtCheckPoint.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt32(_DtCheckPoint.Rows[i]["检定顺序"]) == checkOrder)
        //        {
        //            GetCheckPointData(point, _DtCheckPoint.Rows[i]);
        //        }
        //    }
        //    return true;
        //}
        //#endregion

        //#region 根据内部编号、修改误差检定方案 检定点数据
        ///// <summary>
        ///// 根据内部编号、修改误差检定方案 检定点数据
        ///// </summary>
        ///// <param name="pointid"></param>
        ///// <param name="point"></param>
        ///// <returns></returns>
        //public bool SetPointByPointId(int pointid, Struct.CheckPoint point)
        //{
        //    for (int i = 0; i < _DtCheckPoint.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt32(_DtCheckPoint.Rows[i]["内部编号"]) == pointid)
        //        {
        //            GetCheckPointData(point, _DtCheckPoint.Rows[i]);
        //        }
        //    }
        //    return true;
        //}
        //#endregion

        //#region 添加一个检定点
        ///// <summary>
        ///// 添加一个检定点
        ///// </summary>
        ///// <param name="point"></param>
        ///// <returns></returns>
        //public bool AddPoint(Struct.CheckPoint point)
        //{
        //    int MaxPointId = 0;
        //    int MaxCheckOrder = 1;
        //    for (int i = 0; i < _DtCheckPoint.Rows.Count; i++)
        //    {
        //        if (MaxPointId < Convert.ToInt32(_DtCheckPoint.Rows[i]["内部编号"]))
        //            MaxPointId = Convert.ToInt32(_DtCheckPoint.Rows[i]["内部编号"]);
        //        if (MaxCheckOrder < Convert.ToInt32(_DtCheckPoint.Rows[i]["内部编号"]))
        //            MaxCheckOrder = Convert.ToInt32(_DtCheckPoint.Rows[i]["内部编号"]);
        //    }

        //    //内部编号重复
        //    if (GetPointByPointId(point.PointId).PowerFangXiang != Comm.Enum.Cus_PowerFangXiang.Error)
        //    {
        //        point.PointId = MaxPointId;
        //    }
        //    //序号重复
        //    if (GetPointByCheckOrder(point.nCheckOrder).PowerFangXiang != Comm.Enum.Cus_PowerFangXiang.Error)
        //    {
        //        point.nCheckOrder = MaxCheckOrder;
        //    }
        //    DataRow Row = _DtCheckPoint.NewRow();
        //    GetCheckPointData(point, Row);
        //    _DtCheckPoint.ImportRow(Row);
        //    return true;
        //}
        //#endregion

        //#region 添加一个检定点
        ///// <summary>
        ///// 添加一个检定点
        ///// </summary>
        ///// <param name="yuanjian">功率元件</param>
        ///// <param name="fangxiang">功率方向</param>
        ///// <param name="yinshu">功率因数</param>
        ///// <param name="dianliu">负载电流</param>
        ///// <param name="ischeck">是否需要检定</param>
        ///// <returns></returns>
        //public bool AddPoint(Enum.Cus_PowerYuanJiang yuanjian
        //    , Enum.Cus_PowerFangXiang fangxiang
        //    , float yinshu
        //    , float dianliu
        //    , bool ischeck)
        //{
        //    int MaxPointId = 0;
        //    int MaxCheckOrder = 1;
        //    for (int i = 0; i < _DtCheckPoint.Rows.Count; i++)
        //    {
        //        if (MaxPointId < Convert.ToInt32(_DtCheckPoint.Rows[i]["内部编号"]))
        //            MaxPointId = Convert.ToInt32(_DtCheckPoint.Rows[i]["内部编号"]);
        //        if (MaxCheckOrder < Convert.ToInt32(_DtCheckPoint.Rows[i]["内部编号"]))
        //            MaxCheckOrder = Convert.ToInt32(_DtCheckPoint.Rows[i]["内部编号"]);
        //    }
        //    Struct.CheckPoint point = new Comm.Struct.CheckPoint();

        //    point.PointId = MaxPointId;
        //    point.nCheckOrder = MaxCheckOrder;
        //    point.PowerYuanJian = yuanjian;
        //    point.PowerFangXiang = fangxiang;
        //    point.PowerYinSu = yinshu;
        //    point.PowerDianLiu = dianliu;
        //    point.IsCheck = ischeck;
        //    return AddPoint(point);
        //}
        //#endregion

        //#region 根据编号删除检定点
        ///// <summary>
        ///// 根据编号删除检定点
        ///// </summary>
        ///// <param name="pointid"></param>
        //public void DeletePointByPointId(int pointid)
        //{
        //    for (int i = 0; i < _DtCheckPoint.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt32(_DtCheckPoint.Rows[i]["内部编号"]) == pointid)
        //            _DtCheckPoint.Rows.RemoveAt(i--);
        //    }
        //}
        //#endregion

        //#region 根据检定顺序删除检定点
        ///// <summary>
        ///// 根据检定顺序删除检定点
        ///// </summary>
        ///// <param name="orderid"></param>
        //public void DeletePointByOrderId(int orderid)
        //{
        //    for (int i = 0; i < _DtCheckPoint.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt32(_DtCheckPoint.Rows[i]["检定顺序"]) == orderid)
        //            _DtCheckPoint.Rows.RemoveAt(i--);
        //    }
        //}
        //#endregion

        //#region 清空检定点
        ///// <summary>
        ///// 清空检定点
        ///// </summary>
        //public void Clear()
        //{
        //    _DtCheckPoint.Rows.Clear();
        //}
        //#endregion

        //#region private DataRow -> CheckPoint
        ///// <summary>
        ///// DataRow -> CheckPoint
        ///// </summary>
        ///// <param name="point"></param>
        ///// <param name="Row"></param>
        //private void SetCheckPointData(ref Struct.CheckPoint point, DataRow Row)
        //{
        //    point.PointId = Convert.ToInt32(Row["内部编号"]);
        //    point.nCheckOrder = Convert.ToInt32(Row["检定顺序"]);
        //    point.PowerYuanJian = (Comm.Enum.Cus_PowerYuanJiang)Convert.ToByte(Row["功率元件"]);
        //    point.PowerFangXiang = (Comm.Enum.Cus_PowerFangXiang)Convert.ToByte(Row["功率方向"]);
        //    point.PowerYinSu = Convert.ToSingle(Row["功率因数"]);
        //    point.PowerDianLiu = Convert.ToSingle(Row["负载电流"]);
        //    point.IsCheck = Convert.ToBoolean(Row["是否要检"]);
        //    point.LapCount = Convert.ToUInt16(Row["检定圈数"]);
        //    point.ErrorShangXian = Convert.ToSingle(Row["误差上限"]);
        //    point.ErrorXiaXian = Convert.ToSingle(Row["误差下限"]);
        //}
        //#endregion

        //#region private CheckPoint -> DataRow
        ///// <summary>
        ///// CheckPoint -> DataRow
        ///// </summary>
        ///// <param name="point"></param>
        ///// <param name="Row"></param>
        //private void GetCheckPointData(Struct.CheckPoint point, DataRow Row)
        //{
        //    Row["内部编号"] = point.PointId;
        //    Row["检定顺序"] = point.nCheckOrder;
        //    Row["功率元件"] = point.PowerYuanJian;
        //    Row["功率方向"] = point.PowerFangXiang;
        //    Row["功率因数"] = point.PowerYinSu;
        //    Row["负载电流"] = point.PowerDianLiu;
        //    Row["是否要检"] = point.IsCheck;
        //    Row["检定圈数"] = point.LapCount;
        //    Row["误差上限"] = point.ErrorShangXian;
        //    Row["误差下限"] = point.ErrorXiaXian;
        //}
        //#endregion
        #endregion

        /// <summary>
        /// 圈数参照电流倍数
        /// </summary>
        public string Qscz = "1.0Ib";
        /// <summary>
        /// 参照下的圈数
        /// </summary>
        public int Czqs = 1;

        /// <summary>
        /// 参照的误差限名称
        /// </summary>
        public string CzWcLimit = "规程误差限";
        /// <summary>
        /// 超时方案
        /// </summary>
        public string CzCheckOutTime = "默认超时方案";
        /// <summary>
        /// 方案检定点列表
        /// </summary>
        private List<StPlan_WcPoint> LstCheckPoint;


        public CLDC_DataCore.DataBase.clsWcLimitDataControl _WcLimit = new CLDC_DataCore.DataBase.clsWcLimitDataControl();
        /// <summary>
        /// 功率方向是否要检
        /// </summary>
        private bool[] GlfxYj = new bool[8];
        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型0-三相台，1-单向台</param>
        /// <param name="vFAName">方案名称</param>
        public Plan_WcPoint(int TaiType, string vFAName):base(CLDC_DataCore.Const.Variable.CONST_FA_WC_FOLDERNAME,TaiType,vFAName)
        {
            
            this.Load();

        }
        ~Plan_WcPoint()
        {
            LstCheckPoint = null;
        }

        /// <summary>
        /// 加载方案XML文档
        /// </summary>
        private void Load()
        {
            LstCheckPoint = new List<StPlan_WcPoint>();

            clsXmlControl _XmlNode = new clsXmlControl(_FAPath);

            if (_XmlNode.Count() == 0)
                return;
            string[] _TmpArr = _XmlNode.AttributeValue("", "QSCZ", "QS","CzWcLimit");
            Qscz = _TmpArr[0];
            Czqs = int.Parse(_TmpArr[1]);
            if (_TmpArr[2] != "")
            {
                CzWcLimit = _TmpArr[2];
            }

            for (int _i = 1; _i < 9; _i++)     //功率方向1，2，3，4,5,6,7,8
            {
                XmlNode _Xml = _XmlNode.toXmlNode();
                _Xml = clsXmlControl.FindSencetion(_Xml, clsXmlControl.XPath(string.Format("R,GLFX,{0}", _i.ToString())));
                if (_Xml == null)
                    continue;
                GlfxYj[_i - 1] = true;
                for (int _j = 0; _j < _Xml.ChildNodes.Count; _j++)
                {
                    StPlan_WcPoint _Point = new StPlan_WcPoint();
                    _Point.PrjID = _Xml.ChildNodes[_j].Attributes["PrjID"].Value;
                    _Point.PrjName = _Xml.ChildNodes[_j].Attributes["PrjName"].Value;
                    _Point.PowerYinSu = _Xml.ChildNodes[_j].Attributes["GLYS"].Value;
                    _Point.PowerDianLiu = _Xml.ChildNodes[_j].Attributes["xIb"].Value;
                    if (_Xml.ChildNodes[_j].Attributes.Count > 5)
                    {
                        _Point.Dif_Err_Flag = int.Parse(_Xml.ChildNodes[_j].Attributes["FHC"].Value);
                    }
                    LstCheckPoint.Add(_Point);
                }
            }
            return;
        }
        /// <summary>
        /// 存储XML文档（在存储的时候会进行检定点排序）
        /// </summary>
        public void Save()
        {
            //if (LstCheckPoint.Count == 0)
            //    return;
            this.Sort();
            clsXmlControl _XmlNode = new clsXmlControl();
            _XmlNode.appendchild("","Jbwc","Name",Name,"QSCZ",Qscz,"QS",Czqs.ToString(),"CzWcLimit",CzWcLimit);
            for (int _i = 0; _i < LstCheckPoint.Count; _i++)
            {
                if (clsXmlControl.FindSencetion(_XmlNode.toXmlNode(), clsXmlControl.XPath(string.Format("R,GLFX,{0}", ((int)LstCheckPoint[_i].PowerFangXiang).ToString()))) == null)
                {
                    _XmlNode.appendchild(""
                                    , "R"
                                    , "GLFX"
                                    , ((int)LstCheckPoint[_i].PowerFangXiang).ToString());
                }
                _XmlNode.appendchild(clsXmlControl.XPath(string.Format("R,GLFX,{0}", ((int)LstCheckPoint[_i].PowerFangXiang).ToString()))
                                 , "C"
                                 , "PrjID"
                                 , LstCheckPoint[_i].PrjID
                                 , "PrjName", LstCheckPoint[_i].PrjName
                                 , "GLYS", LstCheckPoint[_i].PowerYinSu
                                 , "xIb", LstCheckPoint[_i].PowerDianLiu
                                 , "PC", LstCheckPoint[_i].Pc.ToString()
                                 , "FHC", LstCheckPoint[_i].Dif_Err_Flag.ToString());
            }
            _XmlNode.SaveXml(_FAPath);
        }


        #region 列表排序
        /// <summary>
        /// 列表排序
        /// </summary>
        private void Sort()
        {
            #region ----------------------------------排序方式old--------------------------------------------------
            /*
            List<Comm.Struct.CheckPoint> _TmpList = new List<Comm.Struct.CheckPoint>();
            for (int i = 0; i < LstCheckPoint.Count; i++)
            {
                if (_TmpList.Count == 0)                //如果临时列表元素数量为0，则加入一个
                    _TmpList.Add(LstCheckPoint[i]);
                else
                {
                    for (int j = _TmpList.Count-1; j>=0 ; j--)              //临时列表从最大到最小开始训话
                    {
                        if (LstCheckPoint[i].PrjID.Substring(0) == "2")     //如果是偏差
                        {
                            if (LstCheckPoint[i].PrjID.Substring(1).CompareTo(_TmpList[j].PrjID.Substring(1)) > 0)      //则从第2个字符到最后进行大小比较，
                            {
                                if (j == _TmpList.Count - 1)                    //如果比临时列表的最后一个大，则直接追加
                                {
                                    _TmpList.Add(LstCheckPoint[i]);
                                }
                                else
                                {
                                    _TmpList.Insert(j + 1, LstCheckPoint[i]);   //反之则在当前位置追加，这样保证了大的永远在后面
                                }
                                break;
                            }
                        }
                        else
                        {
                            if (LstCheckPoint[i].PrjID.CompareTo(_TmpList[j].PrjID) > 0)
                            {
                                if (j == _TmpList.Count - 1)
                                {
                                    _TmpList.Add(LstCheckPoint[i]);
                                }
                                else
                                {
                                    _TmpList.Insert(j + 1, LstCheckPoint[i]);
                                }

                                break;
                            }

                        }
                        if (j == 0)                             //如果比较到最小一个都还没有结果，就在临时列表最前面插入一个
                        {
                            _TmpList.Insert(0, LstCheckPoint[i]);
                        }
                    }
                }
            
            }
            LstCheckPoint = _TmpList;
            */
            #endregion

            IDSort[] _TmpPrjIDCol;

            _TmpPrjIDCol = new IDSort[this.LstCheckPoint.Count];

            for (int i = 0; i < this.LstCheckPoint.Count; i++)
            {
                StPlan_WcPoint _TmpPrj = this.LstCheckPoint[i];
                string _TmpHi = _TmpPrj.PrjID.Substring(0, 3);            //取出误差类别+功率方向+元件
                string _TmpLo = _TmpPrj.PrjID.Substring(7);               //取出谐波+相序
                string _TmpGlys = _TmpPrj.PrjID.Substring(3, 2);          //取出功率因素
                string _TmpxIb = _TmpPrj.PrjID.Substring(5, 2);           //取出电流倍数
                if (_TmpHi.Substring(0, 1) == "2")            //如果是偏差的话，就做一个小处理，以保证默认排序能够按要求排序成功
                {
                    _TmpLo = string.Format("{0}{1}", _TmpLo.Substring(0, _TmpLo.Length - 1), (int.Parse(_TmpLo.Substring(_TmpLo.Length - 1)) + 1).ToString());
                    _TmpHi = "1" + _TmpHi.Substring(1);
                }

                _TmpPrjIDCol[i]=new IDSort(string.Format("{0}{1}{2}{3}",_TmpHi,_TmpxIb,_TmpGlys,_TmpLo),i);
            }

            Array.Sort(_TmpPrjIDCol);

            
            List<StPlan_WcPoint> _TmpLstCheckPiont = new List<StPlan_WcPoint>();

            for (int i = 0; i < _TmpPrjIDCol.Length; i++)
            { 
                _TmpLstCheckPiont.Add(LstCheckPoint[_TmpPrjIDCol[i].AscID]);
            }

            LstCheckPoint = _TmpLstCheckPiont;

        }


        public class IDSort : IComparable
        {
            public IDSort()
            {
 
            }

            private string _ID;

            private int _AscID;

            public int AscID
            {
                get
                {
                    return _AscID;
                }
            }

            public IDSort(string ID, int AscID)
            {
                _ID = ID;
                _AscID = AscID;
            }

            public int CompareTo(object obj)
            {
                IDSort _Sort = (IDSort)obj;

                return string.Compare(this._ID, _Sort._ID);
            }

        }


        #endregion

        /// <summary>
        /// 验证该功率方向是否要捡
        /// </summary>
        /// <param name="Glfx">功率方向</param>
        /// <returns></returns>
        public bool YaoJianGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang Glfx)
        {
            return GlfxYj[(int)Glfx - 1];
        }

        /// <summary>
        /// 是否选中要检
        /// </summary>
        /// <param name="PrjID">项目ID</param>
        /// <returns></returns>
        public bool CheckedYn(string PrjID)
        {
            for (int _i = 0; _i < LstCheckPoint.Count; _i++)
            {
                if (PrjID == LstCheckPoint[_i].PrjID)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 检定点数据
        /// </summary>
        /// <param name="i">检定顺序ID号</param>
        /// <returns></returns>
        public StPlan_WcPoint getCheckPoint(int i)
        {
            if (LstCheckPoint.Count <= i)
                return new StPlan_WcPoint();
            return LstCheckPoint[i];
        }
        /// <summary>
        /// 检定点数量
        /// </summary>
        public int Count
        {
            get
            {
                return LstCheckPoint.Count;
            }
        }
        /// <summary>
        /// 获取所有检定点列表
        /// </summary>
        /// <returns></returns>
        public List<StPlan_WcPoint> GetAll()
        {
            return LstCheckPoint;
        }
        /// <summary>
        /// 清空检定点
        /// </summary>
        public void Clear()
        {
            LstCheckPoint.Clear();
            return;
        }
        #region 弃用的
        /// <summary>
        /// 设置检定圈数(09-7-2之后不再使用)
        /// </summary>
        /// <param name="Current">电流参数1.5(6)</param>
        /// <param name="MeConst">当前表常数 有功（无功）</param>
        /// <param name="MinConst">当台表最小常数（数组 下标0=有功，1=无功）</param>
        //public void SetQs(string Current,string MeConst,int[] MinConst)
        //{
        //    for (int _i = 0; _i < LstCheckPoint.Count; _i++)
        //    {
        //        CLDC_DataCore.Struct.CheckPoint _Item = LstCheckPoint[_i];
        //        _Item.SetLapCount(MinConst, MeConst, Current, Qscz, Czqs);
        //        LstCheckPoint[_i] = _Item;

        //    }
        //}

        /// <summary>
        /// 设置各个测试点误差限(09-7-2之后不再使用)
        /// </summary>
        /// <param name="GuiChengName">规程名称"JJG596-1999"</param>
        /// <param name="DjString">等级字符串1.0,0.5S(2.0)</param>
        /// <param name="Hgq">是否经互感器0-不，1-要</param>
        //public void SetWcx(string GuiChengName,string DjString,int Hgq)
        //{

        //    CLDC_DataCore.DataBase.clsWcLimitDataControl _WcLimit= new clsWcLimitDataControl();

        //    bool _YouGong =true;

        //    string[] _Dj = CLDC_DataCore.Function.Number.getDj(DjString);

        //    if (CzWcLimit == "规程误差限")              //参照规程误差限则直接通过公式计算误差限
        //    {
        //        for (int _i = 0; _i < LstCheckPoint.Count; _i++)
        //        {


        //            if ((int)LstCheckPoint[_i].PowerFangXiang > 2)
        //                _YouGong = false;

        //            string _Wcx = "";

        //            CLDC_DataCore.Struct.CheckPoint _Item = LstCheckPoint[_i];

        //            if (LstCheckPoint[_i].Pc == 0)          //基本误差的误差限获取
        //            {
        //                _Wcx = clsWcLimitDataControl.Wcx(LstCheckPoint[_i].PowerDianLiu
        //                                                        , GuiChengName
        //                                                        , (int)LstCheckPoint[_i].PowerFangXiang > 2 ? _Dj[1] : _Dj[0]
        //                                                        , LstCheckPoint[_i].PowerYuanJian
        //                                                        , LstCheckPoint[_i].PowerYinSu
        //                                                        , Hgq == 0 ? false : true
        //                                                        , _YouGong);


        //                _Item.SetWcx(float.Parse(_Wcx), float.Parse(string.Format("-{0}", _Wcx)));      //设置误差限 
        //            }
        //            else
        //            {
        //                _Wcx = clsWcLimitDataControl.Pcx((int)LstCheckPoint[_i].PowerFangXiang > 2 ? _Dj[1] : _Dj[0]).ToString();      //如果是无功则使用无功等级       
        //                _Item.SetWcx(float.Parse(_Wcx), 0F);      //设置误差限 
        //            }

        //            LstCheckPoint[_i] = _Item;
        //        }
        //    }
        //    else
        //    {
               
        //        CLDC_DataCore.DataBase.IDAndValue _WcLimitName = _WcLimit.getWcLimitNameValue(CzWcLimit);
        //        CLDC_DataCore.DataBase.IDAndValue _GuiChengName = _WcLimit.getGuiChengValue(GuiChengName);
        //        CLDC_DataCore.DataBase.IDAndValue[] _DjValue = new IDAndValue[2];
        //        _DjValue[0] = _WcLimit.getDjValue(_Dj[0]);
        //        _DjValue[1] = _WcLimit.getDjValue(_Dj[1]);
        //        CLDC_DataCore.SystemModel.Item.csGlys _GlysCol = new CLDC_DataCore.SystemModel.Item.csGlys();
        //        CLDC_DataCore.SystemModel.Item.csxIbDic _xIbCol = new CLDC_DataCore.SystemModel.Item.csxIbDic();
        //        _GlysCol.Load();
        //        _xIbCol.Load();
        //        for (int _i = 0; _i < LstCheckPoint.Count; _i++)
        //        {

        //            CLDC_DataCore.Struct.CheckPoint _Item = LstCheckPoint[_i];

        //            CLDC_DataCore.DataBase.IDAndValue _GlysValue = new IDAndValue();         //功率因素值
        //            CLDC_DataCore.DataBase.IDAndValue _xIbValue = new IDAndValue();          //电流倍数值
                    
        //            _GlysValue.Value = LstCheckPoint[_i].PowerYinSu;                
        //            _GlysValue.id = long.Parse(_GlysCol.getGlysID(_GlysValue.Value));
                    
        //            _xIbValue.Value = LstCheckPoint[_i].PowerDianLiu;
        //            _xIbValue.id = long.Parse(_xIbCol.getxIbID(_xIbValue.Value));

        //            if ((int)LstCheckPoint[_i].PowerFangXiang > 2)
        //                _YouGong = false;


        //            if (LstCheckPoint[_i].Pc == 0)          //基本误差的误差限获取
        //            {
        //                string[] _Wcx = _WcLimit.GetWcx(_WcLimitName
        //                                               , _GuiChengName
        //                                               , (int)LstCheckPoint[_i].PowerFangXiang > 2 ? _DjValue[1] : _DjValue[0]
        //                                               , LstCheckPoint[_i].PowerYuanJian
        //                                               , Hgq == 0 ? false : true
        //                                               , _YouGong, _GlysValue, _xIbValue).Split('|');

        //                _Item.SetWcx(float.Parse(_Wcx[0].Replace("+", "")), float.Parse(_Wcx[1]));      //设置误差限
        //            }
        //            else
        //            {
        //                string[] _Wcx = _WcLimit.getPcxValue(_WcLimitName
        //                                                , _GuiChengName
        //                                                , (int)LstCheckPoint[_i].PowerFangXiang > 2 ? _DjValue[1] : _DjValue[0]).Split('|');

        //                _Item.SetWcx(float.Parse(_Wcx[0].Replace("+", "")), float.Parse(_Wcx[1]));      //设置误差限

        //            }

        //            LstCheckPoint[_i] = _Item;
        //        }
        //    }

        //}
        #endregion

        /// <summary>
        /// 添加一个需要检定的误差点
        /// </summary>
        /// <param name="WcType">误差类型</param>
        /// <param name="GLFX">功率方向 </param>
        /// <param name="Yj">元件</param>
        /// <param name="Glys">功率因素</param>
        /// <param name="xIb">电流倍数</param>
        /// <param name="XieBo">是否加谐波0-不加，1-加</param>
        /// <param name="Xiangxu">相序0-正相序，1-逆相序</param>
        public void Add(CLDC_Comm.Enum.Cus_WcType WcType, CLDC_Comm.Enum.Cus_PowerFangXiang GLFX, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string Glys, string xIb, int XieBo, int Xiangxu)
        {
            string _PrjID = getWcPrjID(WcType, GLFX, Yj, Glys, xIb, XieBo, Xiangxu);
            if (CheckedYn(_PrjID))
                return;
            StPlan_WcPoint _Point = new StPlan_WcPoint();
            _Point.PrjID = _PrjID;
            string _GlfxString;
            switch ((int)GLFX)
            {
                case 1:
                    {
                        _GlfxString = "P+";
                        GlfxYj[0] = true;
                        break;
                    }
                case 2:
                    {
                        _GlfxString = "P-";
                        GlfxYj[1] = true;
                        break;
                    }
                case 3:
                    {
                        _GlfxString = "Q+";
                        GlfxYj[2] = true;
                        break;
                    }
                case 4:
                    {
                        _GlfxString = "Q-";
                        GlfxYj[3] = true;
                        break;
                    }
                case 5:
                    {
                        _GlfxString = "Q1";
                        GlfxYj[4] = true;
                        break;
                    }
                case 6:
                    {
                        _GlfxString = "Q2";
                        GlfxYj[5] = true;
                        break;
                    }
                case 7:
                    {
                        _GlfxString = "Q3";
                        GlfxYj[6] = true;
                        break;
                    }
                case 8:
                    {
                        _GlfxString = "Q4";
                        GlfxYj[7] = true;
                        break;
                    }
                default:
                    {
                        _GlfxString = "P+";
                        break;
                    }
            }

            string _Yj="";

            switch (Yj.ToString().ToUpper())
            { 
                case "H":
                    _Yj = "合元";
                    break;
                case "A":
                    _Yj = "A元";
                    break;
                case "B":
                    _Yj = "B元";
                    break;
                case "C":
                    _Yj = "C元";
                    break;
                default:
                    _Yj = "合元";
                    break;
            }

            _Point.PrjName = string.Format("{0} {1} {2} {3} {4}",
                                            _GlfxString, _Yj, Glys, xIb, (int)WcType == 2 ? "标准偏差" : " ");//基本误差
            _Point.PowerYinSu = Glys;
            _Point.PowerDianLiu = xIb;

            LstCheckPoint.Add(_Point);
            return;
        }

        /// <summary>
        /// 移除一个检定点
        /// </summary>
        /// <param name="PrjID">项目编号</param>
        public void RemovePoint(string PrjID)
        {
            for (int _i = 0; _i < LstCheckPoint.Count; _i++)
            {
                if (PrjID == LstCheckPoint[_i].PrjID)
                {
                    StPlan_WcPoint _Point= LstCheckPoint[_i];
                    LstCheckPoint.Remove(_Point);
                }
            }
            return;
        }

        /// <summary>
        /// 删除所有的误差点
        /// </summary>
        public void RemoveAllPoint()
        {
            LstCheckPoint.Clear();
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
            return string.Format("{0}{1}{2}{3}{4}{5}{6}"
                                , (int)WcType
                                , (int)GLFX
                                , (int)Yj
                                , CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysID(Glys)
                                , CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.xIbDic.getxIbID(xIb)
                                , XieBo.ToString(), Xiangxu.ToString());
        }

        /// <summary>
        /// 添加一个需要检定的误差点
        /// </summary>
        /// <param name="WcType">误差类型</param>
        /// <param name="GLFX">功率方向 </param>
        /// <param name="Yj">元件</param>
        /// <param name="Glys">功率因素</param>
        /// <param name="xIb">电流倍数</param>
        /// <param name="XieBo">是否加谐波0-不加，1-加</param>
        /// <param name="Xiangxu">相序0-正相序，1-逆相序</param>
        public void Add(CLDC_Comm.Enum.Cus_WcType WcType, CLDC_Comm.Enum.Cus_PowerFangXiang GLFX, CLDC_Comm.Enum.Cus_PowerYuanJian Yj, string Glys, string xIb, int XieBo, int Xiangxu, string strLimit)
        {
            string _PrjID = getWcPrjID(WcType, GLFX, Yj, Glys, xIb, XieBo, Xiangxu);
            if (CheckedYn(_PrjID) || _PrjID.Length != 9)
                return;
            StPlan_WcPoint _Point = new StPlan_WcPoint();
            _Point.PrjID = _PrjID;
            string _GlfxString;
            switch ((int)GLFX)
            {
                case 1:
                    {
                        _GlfxString = "P+";
                        GlfxYj[0] = true;
                        break;
                    }
                case 2:
                    {
                        _GlfxString = "P-";
                        GlfxYj[1] = true;
                        break;
                    }
                case 3:
                    {
                        _GlfxString = "Q+";
                        GlfxYj[2] = true;
                        break;
                    }
                case 4:
                    {
                        _GlfxString = "Q-";
                        GlfxYj[3] = true;
                        break;
                    }
                case 5:
                    {
                        _GlfxString = "Q1";
                        GlfxYj[4] = true;
                        break;
                    }
                case 6:
                    {
                        _GlfxString = "Q2";
                        GlfxYj[5] = true;
                        break;
                    }
                case 7:
                    {
                        _GlfxString = "Q3";
                        GlfxYj[6] = true;
                        break;
                    }
                case 8:
                    {
                        _GlfxString = "Q4";
                        GlfxYj[7] = true;
                        break;
                    }
                default:
                    {
                        _GlfxString = "P+";
                        break;
                    }
            }

            string _Yj = "";

            switch (Yj.ToString().ToUpper())
            {
                case "H":
                    _Yj = "合元";
                    break;
                case "A":
                    _Yj = "A元";
                    break;
                case "B":
                    _Yj = "B元";
                    break;
                case "C":
                    _Yj = "C元";
                    break;
                default:
                    _Yj = "合元";
                    break;
            }

            _Point.PrjName = string.Format("{0} {1} {2} {3} {4}",
                                            _GlfxString, _Yj, Glys, xIb, (int)WcType == 2 ? "标准偏差" : "基本误差");
            _Point.PowerYinSu = Glys;
            _Point.PowerDianLiu = xIb;

            CLDC_DataCore.DataBase.IDAndValue _WcLimitName = _WcLimit.getWcLimitNameValue(CzWcLimit);
            CLDC_DataCore.DataBase.IDAndValue _GuiChengName = _WcLimit.getGuiChengValue("JJG596-2012");
            CLDC_DataCore.DataBase.IDAndValue[] _DjValue = new CLDC_DataCore.DataBase.IDAndValue[2];

            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo meter = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.GetFirstYaoJianMeterBwh()];
            string[] _DJ = CLDC_DataCore.Function.Number.getDj(meter.Mb_chrBdj);

            _DjValue[0] = _WcLimit.getDjValue(_DJ[0]);
            _DjValue[1] = _WcLimit.getDjValue(_DJ[1]);

            CLDC_DataCore.DataBase.IDAndValue _GlysValue = new CLDC_DataCore.DataBase.IDAndValue();         //功率因素值
            CLDC_DataCore.DataBase.IDAndValue _xIbValue = new CLDC_DataCore.DataBase.IDAndValue();          //电流倍数值

            _GlysValue.Value = _Point.PowerYinSu;
            _GlysValue.id = long.Parse(CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysID(_GlysValue.Value));

            _xIbValue.Value = _Point.PowerDianLiu;
            _xIbValue.id = long.Parse(CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.xIbDic.getxIbID(_xIbValue.Value));

            bool _YouGong = true;
            if ((int)_Point.PowerFangXiang > 2)
                _YouGong = false;

            if (_Point.Pc == 0)          //基本误差的误差限获取
            {
                _WcLimit.SetWcx(_WcLimitName
                                               , _GuiChengName
                                               , (int)_Point.PowerFangXiang > 2 ? _DjValue[1] : _DjValue[0]
                                               , _Point.PowerYuanJian
                                               , meter.Mb_BlnHgq
                                               , _YouGong, _GlysValue, _xIbValue, strLimit);

            }
            else
            {
                string[] _Wcx = _WcLimit.getPcxValue(_WcLimitName
                                                , _GuiChengName
                                                , (int)_Point.PowerFangXiang > 2 ? _DjValue[1] : _DjValue[0]).Split('|');

                _Point.SetWcx(float.Parse(_Wcx[0].Replace("+", "")), float.Parse(_Wcx[1]));      //设置误差限

            }

            LstCheckPoint.Add(_Point);
            return;
        }
        /// <summary>
        /// 设置不平衡负载与平衡负载只差标志
        /// </summary>
        /// <param name="PrjID"></param>
        /// <returns></returns>
        public bool SetFHCYn(string PrjID)
        {
            for (int _i = 0; _i < LstCheckPoint.Count; _i++)
            {
                if (PrjID == LstCheckPoint[_i].PrjID)
                {
                    StPlan_WcPoint stWc = LstCheckPoint[_i];
                    stWc.Dif_Err_Flag = 1;
                    LstCheckPoint.RemoveAt(_i);
                    LstCheckPoint.Insert(_i, stWc);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 检查是否有分合差
        /// </summary>
        /// <param name="PrjID"></param>
        /// <returns></returns>
        public bool CheckedFHC(string PrjID)
        {
            for (int _i = 0; _i < LstCheckPoint.Count; _i++)
            {
                if (PrjID == LstCheckPoint[_i].PrjID)
                {
                    return LstCheckPoint[_i].Dif_Err_Flag == 1; ;
                }
            }
            return false;
        }

    }
}

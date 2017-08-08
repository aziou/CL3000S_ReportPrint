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
    /// �춨�㷽�� �� �������������
    /// </summary>
    [Serializable()]
    public class Plan_WcPoint:Plan_Base 
    {
        #region ///Lost

        ///// <summary>
        ///// �춨������
        ///// </summary>
        //private DataTable _DtCheckPoint;

        //public DataTable DtCheckPoint
        //{
        //    get
        //    {
        //        return _DtCheckPoint;
        //    }
        //}

        //#region ���캯��
        ///// <summary>
        ///// ���캯��
        ///// </summary>
        //public Error_CheckPoint()
        //{
        //    _DtCheckPoint = new DataTable();
        //    _DtCheckPoint.Columns.Add("�ڲ����", typeof(int));
        //    _DtCheckPoint.Columns.Add("�춨˳��", typeof(int));
        //    _DtCheckPoint.Columns.Add("����Ԫ��", typeof(byte));
        //    _DtCheckPoint.Columns.Add("���ʷ���", typeof(byte));
        //    _DtCheckPoint.Columns.Add("��������", typeof(float));
        //    _DtCheckPoint.Columns.Add("���ص���", typeof(float));
        //    _DtCheckPoint.Columns.Add("�Ƿ�Ҫ��", typeof(bool));
        //    _DtCheckPoint.Columns.Add("�춨Ȧ��", typeof(int));
        //    _DtCheckPoint.Columns.Add("�������", typeof(float));
        //    _DtCheckPoint.Columns.Add("�������", typeof(float));

        //    //�ڲ���Ų����ظ�
        //    _DtCheckPoint.PrimaryKey = new DataColumn[] { _DtCheckPoint.Columns["�ڲ����"] };
        //}
        //#endregion

        //#region ��������ѯ���б����
        ///// <summary>
        ///// ��ѯ���б����
        ///// </summary>
        ///// <param name="ischeck">ɸѡ�������Ƿ�Ҫѡ</param>
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
        ///// ��ѯ���б����
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

        //#region �������춨˳���ȡ����
        ///// <summary>
        ///// �������춨˳���ȡ���㡢
        ///// ���Ը��� CheckPoint.PowerYuanJian == Cus_PowerYuanJian.Error ���ж�������Ƿ����
        ///// </summary>
        ///// <param name="checkOrder">�춨˳��</param>
        ///// <returns></returns>
        //public Struct.CheckPoint GetPointByCheckOrder(int checkOrder)
        //{
        //    Struct.CheckPoint point = new Comm.Struct.CheckPoint();
        //    foreach (DataRow Row in _DtCheckPoint.Rows)
        //    {
        //        if (Convert.ToInt32(Row["�춨˳��"]) == checkOrder)
        //        {
        //            SetCheckPointData(ref point, Row);
        //            break;
        //        }
        //    }
        //    return point;
        //}
        //#endregion

        //#region �����ڲ���Ż��߼춨�ĵ�
        ///// <summary>
        ///// �����ڲ���Ż��߼춨�ĵ�
        ///// ���Ը��� CheckPoint.PowerYuanJian == Cus_PowerYuanJian.Error ���ж�������Ƿ����
        ///// </summary>
        ///// <param name="pointid">�ڲ����</param>
        ///// <returns></returns>
        //public Struct.CheckPoint GetPointByPointId(int pointid)
        //{
        //    Struct.CheckPoint point = new Comm.Struct.CheckPoint();
        //    foreach (DataRow Row in _DtCheckPoint.Rows)
        //    {
        //        if (Convert.ToInt32(Row["�ڲ����"]) == pointid)
        //        {
        //            SetCheckPointData(ref point, Row);
        //            break;
        //        }
        //    }
        //    return point;
        //}
        //#endregion

        //#region ���ݼ춨˳���޸����춨���� �춨������
        ///// <summary>
        ///// ���ݼ춨˳���޸����춨���� �춨������
        ///// </summary>
        ///// <param name="checkOrder"></param>
        ///// <param name="point"></param>
        ///// <returns></returns>
        //public bool SetPointByCheckOrder(int checkOrder, Struct.CheckPoint point)
        //{
        //    for (int i = 0; i < _DtCheckPoint.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt32(_DtCheckPoint.Rows[i]["�춨˳��"]) == checkOrder)
        //        {
        //            GetCheckPointData(point, _DtCheckPoint.Rows[i]);
        //        }
        //    }
        //    return true;
        //}
        //#endregion

        //#region �����ڲ���š��޸����춨���� �춨������
        ///// <summary>
        ///// �����ڲ���š��޸����춨���� �춨������
        ///// </summary>
        ///// <param name="pointid"></param>
        ///// <param name="point"></param>
        ///// <returns></returns>
        //public bool SetPointByPointId(int pointid, Struct.CheckPoint point)
        //{
        //    for (int i = 0; i < _DtCheckPoint.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt32(_DtCheckPoint.Rows[i]["�ڲ����"]) == pointid)
        //        {
        //            GetCheckPointData(point, _DtCheckPoint.Rows[i]);
        //        }
        //    }
        //    return true;
        //}
        //#endregion

        //#region ���һ���춨��
        ///// <summary>
        ///// ���һ���춨��
        ///// </summary>
        ///// <param name="point"></param>
        ///// <returns></returns>
        //public bool AddPoint(Struct.CheckPoint point)
        //{
        //    int MaxPointId = 0;
        //    int MaxCheckOrder = 1;
        //    for (int i = 0; i < _DtCheckPoint.Rows.Count; i++)
        //    {
        //        if (MaxPointId < Convert.ToInt32(_DtCheckPoint.Rows[i]["�ڲ����"]))
        //            MaxPointId = Convert.ToInt32(_DtCheckPoint.Rows[i]["�ڲ����"]);
        //        if (MaxCheckOrder < Convert.ToInt32(_DtCheckPoint.Rows[i]["�ڲ����"]))
        //            MaxCheckOrder = Convert.ToInt32(_DtCheckPoint.Rows[i]["�ڲ����"]);
        //    }

        //    //�ڲ�����ظ�
        //    if (GetPointByPointId(point.PointId).PowerFangXiang != Comm.Enum.Cus_PowerFangXiang.Error)
        //    {
        //        point.PointId = MaxPointId;
        //    }
        //    //����ظ�
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

        //#region ���һ���춨��
        ///// <summary>
        ///// ���һ���춨��
        ///// </summary>
        ///// <param name="yuanjian">����Ԫ��</param>
        ///// <param name="fangxiang">���ʷ���</param>
        ///// <param name="yinshu">��������</param>
        ///// <param name="dianliu">���ص���</param>
        ///// <param name="ischeck">�Ƿ���Ҫ�춨</param>
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
        //        if (MaxPointId < Convert.ToInt32(_DtCheckPoint.Rows[i]["�ڲ����"]))
        //            MaxPointId = Convert.ToInt32(_DtCheckPoint.Rows[i]["�ڲ����"]);
        //        if (MaxCheckOrder < Convert.ToInt32(_DtCheckPoint.Rows[i]["�ڲ����"]))
        //            MaxCheckOrder = Convert.ToInt32(_DtCheckPoint.Rows[i]["�ڲ����"]);
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

        //#region ���ݱ��ɾ���춨��
        ///// <summary>
        ///// ���ݱ��ɾ���춨��
        ///// </summary>
        ///// <param name="pointid"></param>
        //public void DeletePointByPointId(int pointid)
        //{
        //    for (int i = 0; i < _DtCheckPoint.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt32(_DtCheckPoint.Rows[i]["�ڲ����"]) == pointid)
        //            _DtCheckPoint.Rows.RemoveAt(i--);
        //    }
        //}
        //#endregion

        //#region ���ݼ춨˳��ɾ���춨��
        ///// <summary>
        ///// ���ݼ춨˳��ɾ���춨��
        ///// </summary>
        ///// <param name="orderid"></param>
        //public void DeletePointByOrderId(int orderid)
        //{
        //    for (int i = 0; i < _DtCheckPoint.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt32(_DtCheckPoint.Rows[i]["�춨˳��"]) == orderid)
        //            _DtCheckPoint.Rows.RemoveAt(i--);
        //    }
        //}
        //#endregion

        //#region ��ռ춨��
        ///// <summary>
        ///// ��ռ춨��
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
        //    point.PointId = Convert.ToInt32(Row["�ڲ����"]);
        //    point.nCheckOrder = Convert.ToInt32(Row["�춨˳��"]);
        //    point.PowerYuanJian = (Comm.Enum.Cus_PowerYuanJiang)Convert.ToByte(Row["����Ԫ��"]);
        //    point.PowerFangXiang = (Comm.Enum.Cus_PowerFangXiang)Convert.ToByte(Row["���ʷ���"]);
        //    point.PowerYinSu = Convert.ToSingle(Row["��������"]);
        //    point.PowerDianLiu = Convert.ToSingle(Row["���ص���"]);
        //    point.IsCheck = Convert.ToBoolean(Row["�Ƿ�Ҫ��"]);
        //    point.LapCount = Convert.ToUInt16(Row["�춨Ȧ��"]);
        //    point.ErrorShangXian = Convert.ToSingle(Row["�������"]);
        //    point.ErrorXiaXian = Convert.ToSingle(Row["�������"]);
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
        //    Row["�ڲ����"] = point.PointId;
        //    Row["�춨˳��"] = point.nCheckOrder;
        //    Row["����Ԫ��"] = point.PowerYuanJian;
        //    Row["���ʷ���"] = point.PowerFangXiang;
        //    Row["��������"] = point.PowerYinSu;
        //    Row["���ص���"] = point.PowerDianLiu;
        //    Row["�Ƿ�Ҫ��"] = point.IsCheck;
        //    Row["�춨Ȧ��"] = point.LapCount;
        //    Row["�������"] = point.ErrorShangXian;
        //    Row["�������"] = point.ErrorXiaXian;
        //}
        //#endregion
        #endregion

        /// <summary>
        /// Ȧ�����յ�������
        /// </summary>
        public string Qscz = "1.0Ib";
        /// <summary>
        /// �����µ�Ȧ��
        /// </summary>
        public int Czqs = 1;

        /// <summary>
        /// ���յ����������
        /// </summary>
        public string CzWcLimit = "��������";
        /// <summary>
        /// ��ʱ����
        /// </summary>
        public string CzCheckOutTime = "Ĭ�ϳ�ʱ����";
        /// <summary>
        /// �����춨���б�
        /// </summary>
        private List<StPlan_WcPoint> LstCheckPoint;


        public CLDC_DataCore.DataBase.clsWcLimitDataControl _WcLimit = new CLDC_DataCore.DataBase.clsWcLimitDataControl();
        /// <summary>
        /// ���ʷ����Ƿ�Ҫ��
        /// </summary>
        private bool[] GlfxYj = new bool[8];
        

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="TaiType">̨������0-����̨��1-����̨</param>
        /// <param name="vFAName">��������</param>
        public Plan_WcPoint(int TaiType, string vFAName):base(CLDC_DataCore.Const.Variable.CONST_FA_WC_FOLDERNAME,TaiType,vFAName)
        {
            
            this.Load();

        }
        ~Plan_WcPoint()
        {
            LstCheckPoint = null;
        }

        /// <summary>
        /// ���ط���XML�ĵ�
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

            for (int _i = 1; _i < 9; _i++)     //���ʷ���1��2��3��4,5,6,7,8
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
        /// �洢XML�ĵ����ڴ洢��ʱ�����м춨������
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


        #region �б�����
        /// <summary>
        /// �б�����
        /// </summary>
        private void Sort()
        {
            #region ----------------------------------����ʽold--------------------------------------------------
            /*
            List<Comm.Struct.CheckPoint> _TmpList = new List<Comm.Struct.CheckPoint>();
            for (int i = 0; i < LstCheckPoint.Count; i++)
            {
                if (_TmpList.Count == 0)                //�����ʱ�б�Ԫ������Ϊ0�������һ��
                    _TmpList.Add(LstCheckPoint[i]);
                else
                {
                    for (int j = _TmpList.Count-1; j>=0 ; j--)              //��ʱ�б�������С��ʼѵ��
                    {
                        if (LstCheckPoint[i].PrjID.Substring(0) == "2")     //�����ƫ��
                        {
                            if (LstCheckPoint[i].PrjID.Substring(1).CompareTo(_TmpList[j].PrjID.Substring(1)) > 0)      //��ӵ�2���ַ��������д�С�Ƚϣ�
                            {
                                if (j == _TmpList.Count - 1)                    //�������ʱ�б�����һ������ֱ��׷��
                                {
                                    _TmpList.Add(LstCheckPoint[i]);
                                }
                                else
                                {
                                    _TmpList.Insert(j + 1, LstCheckPoint[i]);   //��֮���ڵ�ǰλ��׷�ӣ�������֤�˴����Զ�ں���
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
                        if (j == 0)                             //����Ƚϵ���Сһ������û�н����������ʱ�б���ǰ�����һ��
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
                string _TmpHi = _TmpPrj.PrjID.Substring(0, 3);            //ȡ��������+���ʷ���+Ԫ��
                string _TmpLo = _TmpPrj.PrjID.Substring(7);               //ȡ��г��+����
                string _TmpGlys = _TmpPrj.PrjID.Substring(3, 2);          //ȡ����������
                string _TmpxIb = _TmpPrj.PrjID.Substring(5, 2);           //ȡ����������
                if (_TmpHi.Substring(0, 1) == "2")            //�����ƫ��Ļ�������һ��С�����Ա�֤Ĭ�������ܹ���Ҫ������ɹ�
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
        /// ��֤�ù��ʷ����Ƿ�Ҫ��
        /// </summary>
        /// <param name="Glfx">���ʷ���</param>
        /// <returns></returns>
        public bool YaoJianGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang Glfx)
        {
            return GlfxYj[(int)Glfx - 1];
        }

        /// <summary>
        /// �Ƿ�ѡ��Ҫ��
        /// </summary>
        /// <param name="PrjID">��ĿID</param>
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
        /// �춨������
        /// </summary>
        /// <param name="i">�춨˳��ID��</param>
        /// <returns></returns>
        public StPlan_WcPoint getCheckPoint(int i)
        {
            if (LstCheckPoint.Count <= i)
                return new StPlan_WcPoint();
            return LstCheckPoint[i];
        }
        /// <summary>
        /// �춨������
        /// </summary>
        public int Count
        {
            get
            {
                return LstCheckPoint.Count;
            }
        }
        /// <summary>
        /// ��ȡ���м춨���б�
        /// </summary>
        /// <returns></returns>
        public List<StPlan_WcPoint> GetAll()
        {
            return LstCheckPoint;
        }
        /// <summary>
        /// ��ռ춨��
        /// </summary>
        public void Clear()
        {
            LstCheckPoint.Clear();
            return;
        }
        #region ���õ�
        /// <summary>
        /// ���ü춨Ȧ��(09-7-2֮����ʹ��)
        /// </summary>
        /// <param name="Current">��������1.5(6)</param>
        /// <param name="MeConst">��ǰ���� �й����޹���</param>
        /// <param name="MinConst">��̨����С���������� �±�0=�й���1=�޹���</param>
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
        /// ���ø������Ե������(09-7-2֮����ʹ��)
        /// </summary>
        /// <param name="GuiChengName">�������"JJG596-1999"</param>
        /// <param name="DjString">�ȼ��ַ���1.0,0.5S(2.0)</param>
        /// <param name="Hgq">�Ƿ񾭻�����0-����1-Ҫ</param>
        //public void SetWcx(string GuiChengName,string DjString,int Hgq)
        //{

        //    CLDC_DataCore.DataBase.clsWcLimitDataControl _WcLimit= new clsWcLimitDataControl();

        //    bool _YouGong =true;

        //    string[] _Dj = CLDC_DataCore.Function.Number.getDj(DjString);

        //    if (CzWcLimit == "��������")              //���չ���������ֱ��ͨ����ʽ���������
        //    {
        //        for (int _i = 0; _i < LstCheckPoint.Count; _i++)
        //        {


        //            if ((int)LstCheckPoint[_i].PowerFangXiang > 2)
        //                _YouGong = false;

        //            string _Wcx = "";

        //            CLDC_DataCore.Struct.CheckPoint _Item = LstCheckPoint[_i];

        //            if (LstCheckPoint[_i].Pc == 0)          //������������޻�ȡ
        //            {
        //                _Wcx = clsWcLimitDataControl.Wcx(LstCheckPoint[_i].PowerDianLiu
        //                                                        , GuiChengName
        //                                                        , (int)LstCheckPoint[_i].PowerFangXiang > 2 ? _Dj[1] : _Dj[0]
        //                                                        , LstCheckPoint[_i].PowerYuanJian
        //                                                        , LstCheckPoint[_i].PowerYinSu
        //                                                        , Hgq == 0 ? false : true
        //                                                        , _YouGong);


        //                _Item.SetWcx(float.Parse(_Wcx), float.Parse(string.Format("-{0}", _Wcx)));      //��������� 
        //            }
        //            else
        //            {
        //                _Wcx = clsWcLimitDataControl.Pcx((int)LstCheckPoint[_i].PowerFangXiang > 2 ? _Dj[1] : _Dj[0]).ToString();      //������޹���ʹ���޹��ȼ�       
        //                _Item.SetWcx(float.Parse(_Wcx), 0F);      //��������� 
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

        //            CLDC_DataCore.DataBase.IDAndValue _GlysValue = new IDAndValue();         //��������ֵ
        //            CLDC_DataCore.DataBase.IDAndValue _xIbValue = new IDAndValue();          //��������ֵ
                    
        //            _GlysValue.Value = LstCheckPoint[_i].PowerYinSu;                
        //            _GlysValue.id = long.Parse(_GlysCol.getGlysID(_GlysValue.Value));
                    
        //            _xIbValue.Value = LstCheckPoint[_i].PowerDianLiu;
        //            _xIbValue.id = long.Parse(_xIbCol.getxIbID(_xIbValue.Value));

        //            if ((int)LstCheckPoint[_i].PowerFangXiang > 2)
        //                _YouGong = false;


        //            if (LstCheckPoint[_i].Pc == 0)          //������������޻�ȡ
        //            {
        //                string[] _Wcx = _WcLimit.GetWcx(_WcLimitName
        //                                               , _GuiChengName
        //                                               , (int)LstCheckPoint[_i].PowerFangXiang > 2 ? _DjValue[1] : _DjValue[0]
        //                                               , LstCheckPoint[_i].PowerYuanJian
        //                                               , Hgq == 0 ? false : true
        //                                               , _YouGong, _GlysValue, _xIbValue).Split('|');

        //                _Item.SetWcx(float.Parse(_Wcx[0].Replace("+", "")), float.Parse(_Wcx[1]));      //���������
        //            }
        //            else
        //            {
        //                string[] _Wcx = _WcLimit.getPcxValue(_WcLimitName
        //                                                , _GuiChengName
        //                                                , (int)LstCheckPoint[_i].PowerFangXiang > 2 ? _DjValue[1] : _DjValue[0]).Split('|');

        //                _Item.SetWcx(float.Parse(_Wcx[0].Replace("+", "")), float.Parse(_Wcx[1]));      //���������

        //            }

        //            LstCheckPoint[_i] = _Item;
        //        }
        //    }

        //}
        #endregion

        /// <summary>
        /// ���һ����Ҫ�춨������
        /// </summary>
        /// <param name="WcType">�������</param>
        /// <param name="GLFX">���ʷ��� </param>
        /// <param name="Yj">Ԫ��</param>
        /// <param name="Glys">��������</param>
        /// <param name="xIb">��������</param>
        /// <param name="XieBo">�Ƿ��г��0-���ӣ�1-��</param>
        /// <param name="Xiangxu">����0-������1-������</param>
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
                    _Yj = "��Ԫ";
                    break;
                case "A":
                    _Yj = "AԪ";
                    break;
                case "B":
                    _Yj = "BԪ";
                    break;
                case "C":
                    _Yj = "CԪ";
                    break;
                default:
                    _Yj = "��Ԫ";
                    break;
            }

            _Point.PrjName = string.Format("{0} {1} {2} {3} {4}",
                                            _GlfxString, _Yj, Glys, xIb, (int)WcType == 2 ? "��׼ƫ��" : " ");//�������
            _Point.PowerYinSu = Glys;
            _Point.PowerDianLiu = xIb;

            LstCheckPoint.Add(_Point);
            return;
        }

        /// <summary>
        /// �Ƴ�һ���춨��
        /// </summary>
        /// <param name="PrjID">��Ŀ���</param>
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
        /// ɾ�����е�����
        /// </summary>
        public void RemoveAllPoint()
        {
            LstCheckPoint.Clear();
        }

        /// <summary>
        /// ��ȡ�����ĿID
        /// </summary>
        /// <param name="WcType">�������</param>
        /// <param name="GLFX">���ʷ���</param>
        /// <param name="Yj">Ԫ��</param>
        /// <param name="Glys">��������</param>
        /// <param name="xIb">��������</param>
        /// <param name="XieBo">г�� 0���ӣ�1��</param>
        /// <param name="Xiangxu">���� 0��������1������</param>
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
        /// ���һ����Ҫ�춨������
        /// </summary>
        /// <param name="WcType">�������</param>
        /// <param name="GLFX">���ʷ��� </param>
        /// <param name="Yj">Ԫ��</param>
        /// <param name="Glys">��������</param>
        /// <param name="xIb">��������</param>
        /// <param name="XieBo">�Ƿ��г��0-���ӣ�1-��</param>
        /// <param name="Xiangxu">����0-������1-������</param>
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
                    _Yj = "��Ԫ";
                    break;
                case "A":
                    _Yj = "AԪ";
                    break;
                case "B":
                    _Yj = "BԪ";
                    break;
                case "C":
                    _Yj = "CԪ";
                    break;
                default:
                    _Yj = "��Ԫ";
                    break;
            }

            _Point.PrjName = string.Format("{0} {1} {2} {3} {4}",
                                            _GlfxString, _Yj, Glys, xIb, (int)WcType == 2 ? "��׼ƫ��" : "�������");
            _Point.PowerYinSu = Glys;
            _Point.PowerDianLiu = xIb;

            CLDC_DataCore.DataBase.IDAndValue _WcLimitName = _WcLimit.getWcLimitNameValue(CzWcLimit);
            CLDC_DataCore.DataBase.IDAndValue _GuiChengName = _WcLimit.getGuiChengValue("JJG596-2012");
            CLDC_DataCore.DataBase.IDAndValue[] _DjValue = new CLDC_DataCore.DataBase.IDAndValue[2];

            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo meter = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.GetFirstYaoJianMeterBwh()];
            string[] _DJ = CLDC_DataCore.Function.Number.getDj(meter.Mb_chrBdj);

            _DjValue[0] = _WcLimit.getDjValue(_DJ[0]);
            _DjValue[1] = _WcLimit.getDjValue(_DJ[1]);

            CLDC_DataCore.DataBase.IDAndValue _GlysValue = new CLDC_DataCore.DataBase.IDAndValue();         //��������ֵ
            CLDC_DataCore.DataBase.IDAndValue _xIbValue = new CLDC_DataCore.DataBase.IDAndValue();          //��������ֵ

            _GlysValue.Value = _Point.PowerYinSu;
            _GlysValue.id = long.Parse(CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysID(_GlysValue.Value));

            _xIbValue.Value = _Point.PowerDianLiu;
            _xIbValue.id = long.Parse(CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.xIbDic.getxIbID(_xIbValue.Value));

            bool _YouGong = true;
            if ((int)_Point.PowerFangXiang > 2)
                _YouGong = false;

            if (_Point.Pc == 0)          //������������޻�ȡ
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

                _Point.SetWcx(float.Parse(_Wcx[0].Replace("+", "")), float.Parse(_Wcx[1]));      //���������

            }

            LstCheckPoint.Add(_Point);
            return;
        }
        /// <summary>
        /// ���ò�ƽ�⸺����ƽ�⸺��ֻ���־
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
        /// ����Ƿ��зֺϲ�
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

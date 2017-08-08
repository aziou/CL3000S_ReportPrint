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
    /// ���ַ���
    /// </summary>
    [Serializable()]
    public class Plan_ZouZi:Plan_Base  
    {

        /// <summary>
        /// ������Ŀ�б�
        /// </summary>
        private List<StPlan_ZouZi> _LstZouZi;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="TaiType">̨������</param>
        /// <param name="FAName">��������</param>
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
        /// �������ַ���
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
                    _Item.PrjID = _XmlNode.ChildNodes[_i].Attributes["PrjID"].Value;            //��ĿID
                    _Item.Glys = _XmlNode.ChildNodes[_i].Attributes["Glys"].Value;              //��������
                    _Item.xIb = _XmlNode.ChildNodes[_i].Attributes["xIb"].Value;                //��������
                    _Item.FeiLvString = _XmlNode.ChildNodes[_i].Attributes["feilv"].Value;      //��������
                    _Item.ZouZiMethod =(Cus_ZouZiMethod)int.Parse(_XmlNode.ChildNodes[_i].Attributes["CheckType"].Value);           //���ַ���
                    _Item.ZuHeWc = _XmlNode.ChildNodes[_i].Attributes["ZuHeWc"].Value + "";         //�Ƿ���������     
                    if(_Item.ZuHeWc=="") _Item.ZuHeWc = "0";         //�Ƿ���������     
                    _Item.ZouZiPrj = new List<StPlan_ZouZi.StPrjFellv>();                    //������Ŀ���ݣ��߷ַ��ʣ�
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
        /// �������ַ�����XML�ĵ�
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
        /// ����һ��������Ŀ
        /// </summary>
        /// <param name="Glfx">���ʷ���</param>
        /// <param name="Method">���鷽�����������֣����Ǳ�׼����</param>
        /// <param name="Yj">Ԫ��</param>
        /// <param name="Glys">��������</param>
        /// <param name="xIb">��������</param>
        /// <param name="Feilvstring">��������</param> 
        /// <param name="ZuHeWc">�Ƿ���������0������1��</param>
        /// <param name="Prj">����������Ŀ�б�</param> 
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
        /// ���ط����а�������Ŀ����
        /// </summary>
        public int Count
        {
            get
            {
                return _LstZouZi.Count;
            }
        }
        /// <summary>
        /// �����б�����ID��ȡ��Ŀ����
        /// </summary>
        /// <param name="i">��Ŀ�б�����</param>
        /// <returns></returns>
        public StPlan_ZouZi getZouZiPrj(int i)
        {
            if (i >= _LstZouZi.Count)
                return new StPlan_ZouZi();
            return _LstZouZi[i];
        }

        /// <summary>
        /// �ƶ�������Ŀ
        /// </summary>
        /// <param name="i">��Ҫ�ƶ������б�λ��</param>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Move(int i, StPlan_ZouZi Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstZouZi.Count ? _LstZouZi.Count - 1 : i;
            this.Remove(Item);
            _LstZouZi.Insert(i, Item);
            return;
        }

        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="i">��Ŀ������</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstZouZi.Count)
                return;
            _LstZouZi.RemoveAt(i);
            return;
        }
        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(StPlan_ZouZi Item)
        {
            if (!_LstZouZi.Contains(Item))
                return;
            _LstZouZi.Remove(Item);
            return;
        }
        /// <summary>
        /// ��ȡ������ĿID��������
        /// </summary>
        /// <param name="GLFX">���ʷ���</param>
        /// <param name="Yj">Ԫ��</param>
        /// <param name="FeiLv">����</param>
        /// <param name="Glys">��������</param>
        /// <param name="xIb">��������</param>
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
        /// ��ȡ������ĿID,��������
        /// </summary>
        /// <param name="GLFX">���ʷ���</param>
        /// <param name="Yj">Ԫ��</param>
        /// <param name="Glys">��������</param>
        /// <param name="xIb">��������</param>
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

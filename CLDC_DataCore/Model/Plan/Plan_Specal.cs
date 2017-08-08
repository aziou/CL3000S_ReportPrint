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
    /// ����춨����
    /// </summary>
    [Serializable()]
    public class Plan_Specal:Plan_Base 
    {
        /// <summary>
        /// ����춨��Ŀ�б�
        /// </summary>
        private List<StPlan_SpecalCheck> _LstSpecal;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="TaiType">̨������0-����̨��1-����̨</param>
        /// <param name="vFAName">��������</param>
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
        /// ��������춨����
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
                    _Item.PrjName = _XmlNode.ChildNodes[_i].Attributes["PrjName"].Value;            //��Ŀ����
                    _Item.PowerFangXiang = (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(_XmlNode.ChildNodes[_i].Attributes["GLFX"].Value);       //���ʷ���
                    _Item.PowerYinSu = _XmlNode.ChildNodes[_i].Attributes["GLYS"].Value;            //��������
                    _Item.ExplainUString(_XmlNode.ChildNodes[_i].Attributes["xU"].Value);           //������ѹ����
                    _Item.ExplainIString(_XmlNode.ChildNodes[_i].Attributes["xIb"].Value);          //������������
                    _Item.ExplainXwString(_XmlNode.ChildNodes[_i].Attributes["Xw"].Value);          //������λ
                    _Item.ExplainWcx(_XmlNode.ChildNodes[_i].Attributes["Wcx"].Value);              //���������
                    _Item.PingLv = float.Parse(_XmlNode.ChildNodes[_i].Attributes["Pl"].Value);     //Ƶ��
                    _Item.WcCheckNumic = int.Parse(_XmlNode.ChildNodes[_i].Attributes["Wccs"].Value);   //������
                    _Item.LapCount = int.Parse(_XmlNode.ChildNodes[_i].Attributes["qs"].Value);
                    _Item.XieBoFa = _XmlNode.ChildNodes[_i].Attributes["xiebo"].Value.Trim();           //г������
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
        /// �洢����춨������XML�ĵ�
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
        /// ���һ����Ŀ��Ϣ
        /// </summary>
        /// <param name="Item">����춨��Ŀ</param>
        /// <returns></returns>
        public bool Add(StPlan_SpecalCheck Item)
        {
            if (_LstSpecal.Contains(Item))
                return false;
            _LstSpecal.Add(Item);
            return true;
        }

        ///// <summary>
        ///// ���һ����Ŀ��Ϣ
        ///// </summary>
        ///// <param name="PrjName">��Ŀ����</param>
        ///// <param name="GLFX">���ʷ���</param>
        ///// <param name="Yj">Ԫ��</param>
        ///// <param name="xU">��ѹ���������֣�</param>
        ///// <param name="xIb">�������������֣�</param>
        ///// <param name="PL">Ƶ�ʣ����֣�</param>
        ///// <param name="Glys">��������(�ֵ��д��ڵ�ֵ)</param>
        ///// <param name="wcsx">�������</param>
        ///// <param name="wcxx">�������</param>
        ///// <param name="Qs">Ȧ��</param>
        ///// <param name="xiebo">0���ӣ�1��</param>
        ///// <param name="xiangxu">0����1��</param>
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
        /// �Ƴ����з�����Ŀ
        /// </summary>
        public void RemoveAll()
        {
            _LstSpecal.Clear();
        }
        /// <summary>
        /// ���ط����а�������Ŀ����
        /// </summary>
        public int Count
        {
            get
            {
                return _LstSpecal.Count;
            }
        }

        /// <summary>
        /// �����б�����ID��ȡ��Ŀ����
        /// </summary>
        /// <param name="i">��Ŀ�б�����</param>
        /// <returns></returns>
        public StPlan_SpecalCheck getSpecalPrj(int i)
        {
            if (i >= _LstSpecal.Count)
                return new StPlan_SpecalCheck();

            StPlan_SpecalCheck Item = _LstSpecal[i];
            Item.LoadXieBo();       //����һ��г������
            return Item;
        }

        /// <summary>
        /// �ƶ�����춨��Ŀ
        /// </summary>
        /// <param name="i">��Ҫ�ƶ������б�λ��</param>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Move(int i, StPlan_SpecalCheck Item)
        {
            i = i < 0 ? 0 : i;
            i = i >= _LstSpecal.Count ? _LstSpecal.Count - 1 : i;
            this.Remove(Item);
            _LstSpecal.Insert(i, Item);
            return;
        }

        /// <summary>
        /// �����б������Ƴ�
        /// </summary>
        /// <param name="i">��Ŀ������</param>
        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _LstSpecal.Count)
                return;
            _LstSpecal.RemoveAt(i);
            return;
        }
        /// <summary>
        /// ������Ŀ�Ƴ�
        /// </summary>
        /// <param name="Item">��Ŀ�ṹ��</param>
        public void Remove(StPlan_SpecalCheck Item)
        {
            if (!_LstSpecal.Contains(Item))
                return;
            _LstSpecal.Remove(Item);
            return;
        }

        ///// <summary>
        ///// ��ȡ����춨��ĿID
        ///// </summary>
        ///// <param name="GLFX">���ʷ���</param>
        ///// <param name="Yj">Ԫ��</param>
        ///// <param name="Glys">��������</param>
        ///// <param name="XieBo">г�� 0���ӣ�1��</param>
        ///// <param name="Xiangxu">���� 0��������1������</param>
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

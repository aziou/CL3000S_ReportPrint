using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// ����춨������Ŀ
    /// </summary>
    [Serializable()]
    public struct  StPlan_SpecalCheck
    {
        
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string PrjName;

        /// <summary>
        /// ���ʷ���
        /// </summary>
        public CLDC_Comm.Enum.Cus_PowerFangXiang PowerFangXiang;

        /// <summary>
        /// �����������磺1.0 ��0.5C ��-0.8L
        /// </summary>
        public string PowerYinSu;

        /// <summary>
        /// ��Ŀ��ţ�Ψһ��
        /// </summary>
        public string ProjectionNumber
        {
            get
            {
                string str=((int )PowerFangXiang).ToString ();
                str += PowerYinSu;
                str += xUa.ToString();
                str += xUb.ToString();
                str += xUc.ToString();
                str += xIa.ToString();
                str += xIb.ToString();
                str += xIc.ToString();
                str += PingLv.ToString();
                str += XiangXu.ToString();
                str += XieBo.ToString();
                return str;
            }
        }
        /// <summary>
        /// ��ѹ����(A��)
        /// </summary>
        public float xUa ;

        /// <summary>
        /// ��ѹ������B�ࣩ
        /// </summary>
        public float xUb;

        /// <summary>
        /// ��ѹ������C�ࣩ
        /// </summary>
        public float xUc;

        /// <summary>
        /// ����������A�ࣩ
        /// </summary>
        public float xIa ;
        /// <summary>
        /// ����������B�ࣩ
        /// </summary>
        public float xIb;
        /// <summary>
        /// ����������C�ࣩ
        /// </summary>
        public float xIc;

        /// <summary>
        /// ��ѹA����λ����ʱ���ã�
        /// </summary>
        public float fUa;

        /// <summary>
        /// ��ѹB����λ����ʱ���ã�
        /// </summary>
        public float fUb;
        /// <summary>
        /// ��ѹC����λ����ʱ���ã�
        /// </summary>
        public float fUc;

        /// <summary>
        /// ����A����λ����ʱ���ã�
        /// </summary>
        public float fIa;

        /// <summary>
        /// ����B����λ����ʱ���ã�
        /// </summary>
        public float fIb;
        /// <summary>
        /// ����C����λ����ʱ���ã�
        /// </summary>
        public float fIc;

        /// <summary>
        /// Ƶ��
        /// </summary>
        public float PingLv ;

        /// <summary>
        /// �������
        /// </summary>
        public float WuChaXian_Shang ;

        /// <summary>
        /// �������
        /// </summary>
        public float WuChaXian_Xia ;

        /// <summary>
        /// ������
        /// </summary>
        public int WcCheckNumic;

        /// <summary>
        /// Ȧ��
        /// </summary>
        public int LapCount;
        /// <summary>
        /// ����0-������1-������
        /// </summary>
        public int XiangXu;
        /// <summary>
        /// г��0-���ӣ�1-��
        /// </summary>
        public int XieBo;

        /// <summary>
        /// �����г�����򿴵�������г������
        /// </summary>
        public string XieBoFa;
        /// <summary>
        /// г�������б�
        /// </summary>
        public List<StXieBo> XieBoItem;


        /// <summary>
        /// ������ѹ�����ַ�����xUa|xUb|xUc��
        /// </summary>
        /// <param name="Ustring"></param>
        public void ExplainUString(string Ustring)
        {
            string[] xU = Ustring.Split('|');
            if (xU.Length != 3)
            {
                return;
            }
            this.xUa = float.Parse(xU[0]);
            this.xUb = float.Parse(xU[1]);
            this.xUc = float.Parse(xU[2]);
        }

        /// <summary>
        /// �������������ַ�����xIa|xIb|xIc��
        /// </summary>
        /// <param name="Istring"></param>
        public void ExplainIString(string Istring)
        {
            string[] xI = Istring.Split('|');
            if (xI.Length != 3) return;
            this.xIa = float.Parse(xI[0]);
            this.xIb = float.Parse(xI[1]);
            this.xIc = float.Parse(xI[2]);
        }
        /// <summary>
        /// ��λ��(Ua,Ub,Uc|Ia,Ib,Ic)����ʱ���ã�
        /// </summary>
        /// <param name="XwString"></param>
        public void ExplainXwString(string XwString)
        {
            string[] Xw = XwString.Split('|');
            if (Xw.Length != 2) return;
            string[] XwU = Xw[0].Split(',');
            if (XwU.Length != 3) return;
            this.fUa = float.Parse(XwU[0]);
            this.fUb = float.Parse(XwU[1]);
            this.fUc = float.Parse(XwU[2]);
            string[] XwI = Xw[1].Split('|');
            if (XwI.Length != 3) return;
            this.fIa = float.Parse(XwI[0]);
            this.fIb = float.Parse(XwI[1]);
            this.fIc = float.Parse(XwI[2]);

        }
        /// <summary>
        /// ��������ޣ�����|���ߣ�
        /// </summary>
        /// <param name="WcxString"></param>
        public void ExplainWcx(string WcxString)
        {
            string[] Wcx = WcxString.Split('|');
            if (Wcx.Length != 2) return;
            this.WuChaXian_Shang = float.Parse(Wcx[0].Replace("+", ""));
            this.WuChaXian_Xia = float.Parse(Wcx[1].Replace("+", ""));
        }

        /// <summary>
        /// ��ϵ�ѹ�����ַ���
        /// </summary>
        /// <returns></returns>
        public string JoinxUString()
        {
            return string.Format("{0}|{1}|{2}", this.xUa.ToString(), this.xUb.ToString(), this.xUc.ToString());
        }
        /// <summary>
        /// ��ϵ��������ַ���
        /// </summary>
        /// <returns></returns>
        public string JoinxIString()
        {
            return string.Format("{0}|{1}|{2}", this.xIa.ToString(), this.xIb.ToString(), this.xIc.ToString());
        }
        /// <summary>
        /// ���������ַ���
        /// </summary>
        /// <returns></returns>
        public String JoinWcxString()
        {
            return string.Format("{0}|{1}", this.WuChaXian_Shang > 0 ? string.Format("+{0}", this.WuChaXian_Shang.ToString()) : this.WuChaXian_Shang.ToString()
                                          , this.WuChaXian_Xia > 0 ? string.Format("+{0}", this.WuChaXian_Xia.ToString()) : this.WuChaXian_Xia.ToString());
        }

        /// <summary>
        /// �����λ�ַ�������ʱ���ã�
        /// </summary>
        /// <returns></returns>
        public string JoinXwString()
        {
            return string.Format("{0},{1},{2}|{3},{4},{5}", 
                                    this.fUa.ToString(), 
                                    this.fUb.ToString(), 
                                    this.fUc.ToString(), 
                                    this.fIa.ToString(), 
                                    this.fIb.ToString(), 
                                    this.fIc.ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        public void LoadXieBo()
        {
            XieBoItem = new List<StXieBo>();
            if (XieBo != 1) return;

            CLDC_DataCore.SystemModel.Item.csXieBo _XieBoItem = new CLDC_DataCore.SystemModel.Item.csXieBo();

            XieBoItem = _XieBoItem.getXieBoFa(XieBoFa);

        }

        /// <summary>
        /// ����춨����
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}����춨��{1},Ƶ��{2:f},{3},{4}"
                                ,PowerFangXiang.ToString()
                                ,this.PrjName
                                ,PingLv
                                ,(XieBo==1?"��г��":"��г��")
                                ,(XiangXu==1?"������":"������"));
        }

    }


}

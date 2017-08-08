using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices;
using CLDC_Comm.Enum;
using CLDC_DataCore.Function;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// ����
    /// </summary>
    [Serializable()]
    public struct StPlan_WcPoint
    {
        /// <summary>
        /// ��ĿID
        /// </summary>
        private string _PrjID;

        /// <summary>
        /// ��ĿID
        /// </summary>
        public string PrjID
        {
            get 
            {
                return _PrjID;
            }
            set 
            {
                _PrjID = value;
                this.Pc = int.Parse(_PrjID.Substring(0, 1)) == 2 ? 1 : 0;
                this.PowerFangXiang = (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(_PrjID.Substring(1, 1));
                this.PowerYuanJian = (CLDC_Comm.Enum.Cus_PowerYuanJian)int.Parse(_PrjID.Substring(2, 1));
                this.XieBo = int.Parse(_PrjID.Substring(7, 1));
                this.XiangXu = int.Parse(_PrjID.Substring(8, 1));
            }
        }
        /// <summary>
        /// ��Ŀ����
        /// </summary>
        public string PrjName;

        /// <summary>
        /// �ڲ����
        /// </summary>
        public int PointId;

        /// <summary>
        /// �춨��˳��
        /// </summary>
        public int nCheckOrder;

        /// <summary>
        /// ����Ԫ��
        /// </summary>
        public Cus_PowerYuanJian PowerYuanJian;

        /// <summary>
        /// ���ʷ���
        /// </summary>
        public Cus_PowerFangXiang PowerFangXiang;

        /// <summary>
        /// ��������
        /// </summary>
        public string  PowerYinSu;

        /// <summary>
        /// ���ص���xIb 
        /// </summary>
        public string PowerDianLiu;

        /// <summary>
        /// �Ƿ���ƫ��0����1��
        /// </summary>
        public int Pc;

        /// <summary>
        /// �Ƿ�Ҫ��
        /// </summary>
        public bool  IsCheck;

        /// <summary>
        /// �������(Ĭ��ֵ-999)
        /// </summary>
        public float ErrorShangXian;

        /// <summary>
        /// ������ޣ�Ĭ��ֵ-999��
        /// </summary>
        public float ErrorXiaXian;

        /// <summary>
        /// ���Ȧ��
        /// </summary>
        public int  LapCount ;

        /// <summary>
        /// ����0-������1-������
        /// </summary>
        public int XiangXu;

        /// <summary>
        /// г��0-���ӣ�1-��
        /// </summary>
        public int XieBo;
        /// <summary>
        /// ��ƽ�⸺����ƽ�⸺��ʱ���֮���־ 
        /// 0��û�У�1����
        /// </summary>
        public int Dif_Err_Flag
        {
            get
            {
                if (PowerYinSu == "1.0" && PowerDianLiu == "1.0Ib" && (PowerYuanJian == Cus_PowerYuanJian.A || PowerYuanJian == Cus_PowerYuanJian.B || PowerYuanJian == Cus_PowerYuanJian.C))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

            set { }
        }
        

        #region ���㡢���ü춨��ļ춨Ȧ��
        /// <summary>
        /// �����ȡ��Ҫ�춨Ȧ��
        /// </summary>
        /// <param name="MinConst">��̨������е���С����(���飬�±�0=�й����±�1=�޹�)</param>
        /// <param name="MeConst">��ǰ��ĳ��� �й�(�޹�)</param>
        /// <param name="Current">��������(1.5(6))</param>
        /// <param name="CzIb">���� ����</param>
        /// <param name="CzQS">����Ȧ��</param>
        public void SetLapCount(int[] MinConst,string MeConst,string Current,string CzIb,int CzQS)
        {
            int _MeConst = 0;

            int _MinConst = 0;

            if (this.PowerFangXiang == CLDC_Comm.Enum.Cus_PowerFangXiang.�����й� || this.PowerFangXiang == CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�)
            {
                _MeConst = CLDC_DataCore.Function.Number.GetBcs(MeConst, true);
                _MinConst = MinConst[0];
            }
            else
            {
                _MeConst = CLDC_DataCore.Function.Number.GetBcs(MeConst, false);
                _MinConst = MinConst[1];
            }

            float _Tqs = (float)CzQS * ((float)_MeConst / (float)_MinConst);

            _Tqs *= (Number.getxIb(this.PowerDianLiu, Current) / Number.getxIb(CzIb, Current)) * Number.getGlysValue(this.PowerYinSu);

            if ((int)this.PowerYuanJian != 1)      //���Ǻ�Ԫ
                _Tqs /= 3;
            int _QS = (int)Math.Round((double)_Tqs, 0);
            if (_QS == 0)
                _QS = 1;
            LapCount = _QS;
        }

        /// <summary>
        /// ��ȡ��ǰ�����������
        /// </summary>
        /// <param name="WcLimitName">���������</param>
        /// <param name="GuiChengName">�������</param>
        /// <param name="Dj">�ȼ�1.0(2.0)</param>
        /// <param name="Hgq">�Ƿ񾭻���������</param>
        public void SetWcx(string WcLimitName
                           , string GuiChengName
                           , string Dj
                           , bool Hgq)
        {
            string[] Arr_Dj = CLDC_DataCore.Function.Number.getDj(Dj);
            
            bool YouGong = true;

            if (((int)this.PowerFangXiang) > 2)
                YouGong = false;

            if (WcLimitName == "��������")
            {

                string _Wcx = "";

                if (this.Pc == 0)          //���
                {
                    _Wcx = CLDC_DataCore.DataBase.clsWcLimitDataControl.Wcx(this.PowerDianLiu
                                                                , GuiChengName
                                                                , (int)this.PowerFangXiang > 2 ? Arr_Dj[1] : Arr_Dj[0]
                                                                , this.PowerYuanJian, this.PowerYinSu, Hgq, YouGong);
                    this.SetWcx(float.Parse(_Wcx), float.Parse(string.Format("-{0}", _Wcx)));       //���������
                }
                else                      //ƫ��
                {
                    _Wcx = CLDC_DataCore.DataBase.clsWcLimitDataControl.Pcx((int)this.PowerFangXiang > 2 ? Arr_Dj[1] : Arr_Dj[0]).ToString();
                    this.SetWcx(float.Parse(_Wcx), 0F);     //���������
                }


            }
            else
            {
                CLDC_DataCore.DataBase.clsWcLimitDataControl _WcLimit = new CLDC_DataCore.DataBase.clsWcLimitDataControl();

                CLDC_DataCore.DataBase.IDAndValue _WcLimitName = _WcLimit.getWcLimitNameValue(WcLimitName);
                CLDC_DataCore.DataBase.IDAndValue _GuiChengName = _WcLimit.getGuiChengValue(GuiChengName);
                CLDC_DataCore.DataBase.IDAndValue[] _DjValue = new CLDC_DataCore.DataBase.IDAndValue[2];
                _DjValue[0] = _WcLimit.getDjValue(Arr_Dj[0]);
                _DjValue[1] = _WcLimit.getDjValue(Arr_Dj[1]);
                CLDC_DataCore.DataBase.IDAndValue _GlysValue = new CLDC_DataCore.DataBase.IDAndValue();
                CLDC_DataCore.DataBase.IDAndValue _xIbValue = new CLDC_DataCore.DataBase.IDAndValue();

                _GlysValue.Value = this.PowerYinSu;         //���������ַ���
                _GlysValue.id = long.Parse(this.PrjID.Substring(3, 2));     //ID�Ǵ�prjid�еĵ���λ��ȡ2λ
                _xIbValue.Value=this.PowerDianLiu;          //���������ַ���
                _xIbValue.id = long.Parse(this.PrjID.Substring(5, 2));  //ID�Ǵ�PrjID�еĵ�5λ��ȡ2λ

                if (this.Pc == 0)          //�������
                {
                    string[] _Wcx = _WcLimit.GetWcx(_WcLimitName
                                                  , _GuiChengName
                                                  , !YouGong ? _DjValue[1] : _DjValue[0]
                                                  , this.PowerYuanJian
                                                  , Hgq
                                                  , YouGong
                                                  , _GlysValue
                                                  , _xIbValue).Split('|');
                    this.SetWcx(float.Parse(_Wcx[0].Replace("+", "")), float.Parse(_Wcx[1]));       //���������
                }
                else
                {
                    string[] _Wcx = _WcLimit.getPcxValue(_WcLimitName, _GuiChengName, !YouGong ? _DjValue[1] : _DjValue[0]).Split('|');
                    this.SetWcx(float.Parse(_Wcx[0].Replace("+", "")), float.Parse(_Wcx[1]));           //����ƫ����
                }
                _WcLimit.Close();
                _WcLimit = null;
            }
        
        }



        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="Max">����</param>
        /// <param name="Min">����</param>
        internal void SetWcx(float Max,float Min)
        {
            if (this.ErrorShangXian==0F)
                this.ErrorShangXian = Max;
            if(this.ErrorXiaXian==0F)
                this.ErrorXiaXian = Min;
        }
        
        
 
        #endregion

        public override string ToString()
        {
            return PrjName + (Dif_Err_Flag == 1 ? " FHC" : "");
        }
        
    }
}

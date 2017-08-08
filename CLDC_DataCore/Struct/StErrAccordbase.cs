#region FileHeader And Descriptions
// ***************************************************************
//  stErrAccordbase   date: 2010-04-08 by Gqs
//  -------------------------------------------------------------
//  Description:
//   说明: 
//        1、误差限:默认采用“规程误差限”   
//        2、圈数:参照 CzIb = 1.0Ib  CzQs = 1
//  -------------------------------------------------------------
// ***************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    [Serializable()]
    public struct StErrAccordbase
    {
        /// <summary>
        /// 功率方向
        /// </summary>
        public CLDC_Comm.Enum.Cus_PowerFangXiang PowerFangXiang;

        /// <summary>
        /// 功率元件
        /// </summary>
        public CLDC_Comm.Enum.Cus_PowerYuanJian PowerYuanJian;

        /// <summary>
        /// 功率因数
        /// </summary>
        public string PowerYinSu;

        /// <summary>
        /// 负载电流xIb 
        /// </summary>
        public string PowerDianLiu;

        /// <summary>
        /// 相序0-正相序，1-逆相序
        /// </summary>
        public int XiangXu;

        /// <summary>
        /// 谐波0-不加，1-加
        /// </summary>
        public int XieBo;

        /// <summary>
        /// 误差上限(默认值-999)
        /// </summary>
        public float ErrorShangXian;

        /// <summary>
        /// 误差下限（默认值-999）
        /// </summary>
        public float ErrorXiaXian;

        /// <summary>
        /// 检测圈数
        /// </summary>
        public int LapCount;

        /// <summary>
        /// 是否要检
        /// </summary>
        public bool IsCheck;

        /// <summary>
        /// 测试点名称 
        /// </summary>
        public string TestPointName;

        /// <summary>
        /// 项目ID
        /// </summary>
        private string _PrjID;

        /// <summary>
        /// 项目ID
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
                this.PowerFangXiang = (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(_PrjID.Substring(1, 1));
                this.PowerYuanJian = (CLDC_Comm.Enum.Cus_PowerYuanJian)int.Parse(_PrjID.Substring(2, 1));
                this.XieBo = int.Parse(_PrjID.Substring(7, 1));
                this.XiangXu = int.Parse(_PrjID.Substring(8, 1));
            }
        }

        #region 计算、设置检定点的检定圈数
        /// <summary>
        /// 计算获取需要检定圈数
        /// </summary>
        /// <param name="MinConst">当台被检表中的最小常数(数组，下标0=有功，下标1=无功)</param>
        /// <param name="MeConst">当前表的常数 有功(无功)</param>
        /// <param name="Current">电流参数(1.5(6))</param>
        public void SetLapCount(int[] MinConst, string MeConst, string Current, string CzIb, int CzQS)
        {
            int _MeConst = 0;

            int _MinConst = 0;

            if (this.PowerFangXiang == CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功 || this.PowerFangXiang == CLDC_Comm.Enum.Cus_PowerFangXiang.反向有功)
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

            _Tqs *= (CLDC_DataCore.Function.Number.getxIb(this.PowerDianLiu, Current) / CLDC_DataCore.Function.Number.getxIb(CzIb, Current)) * CLDC_DataCore.Function.Number.getGlysValue(this.PowerYinSu);

            if ((int)this.PowerYuanJian != 1)      //不是合元
                _Tqs /= 3;
            int _QS = (int)Math.Round((double)_Tqs, 0);
            if (_QS == 0)
                _QS = 1;
            LapCount = _QS;
        }

        /// <summary>
        /// 获取当前被检点的误差限
        /// </summary>
        /// <param name="WcLimitName">误差限名称</param>
        /// <param name="GuiChengName">规程名称</param>
        /// <param name="Dj">等级1.0(2.0)</param>
        /// <param name="Hgq">是否经互感器接入</param>
        public void SetWcx(string WcLimitName
                           , string GuiChengName
                           , string Dj
                           , bool Hgq)
        {
            string[] Arr_Dj = CLDC_DataCore.Function.Number.getDj(Dj);

            bool YouGong = true;

            if (((int)this.PowerFangXiang) > 2)
                YouGong = false;

            if (WcLimitName == "规程误差限")
            {
                string _Wcx = "";

                _Wcx = CLDC_DataCore.DataBase.clsWcLimitDataControl.Wcx(this.PowerDianLiu
                                                            , GuiChengName
                                                            , (int)this.PowerFangXiang > 2 ? Arr_Dj[1] : Arr_Dj[0]
                                                            , this.PowerYuanJian, this.PowerYinSu, Hgq, YouGong);
                this.SetWcx(float.Parse(_Wcx), float.Parse(string.Format("-{0}", _Wcx)));       //设置误差限
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

                _GlysValue.Value = this.PowerYinSu;         //功率因素字符串
                _GlysValue.id = long.Parse(this.PrjID.Substring(3, 2));     //ID是从prjid中的第三位起，取2位
                _xIbValue.Value = this.PowerDianLiu;          //电流倍数字符串
                _xIbValue.id = long.Parse(this.PrjID.Substring(5, 2));  //ID是从PrjID中的第5位起，取2位

                string[] _Wcx = _WcLimit.GetWcx(_WcLimitName
                                              , _GuiChengName
                                              , !YouGong ? _DjValue[1] : _DjValue[0]
                                              , this.PowerYuanJian
                                              , Hgq
                                              , YouGong
                                              , _GlysValue
                                              , _xIbValue).Split('|');
                this.SetWcx(float.Parse(_Wcx[0].Replace("+", "")), float.Parse(_Wcx[1]));       //设置误差限


                _WcLimit.Close();
                _WcLimit = null;
            }
        }

        /// <summary>
        /// 设置误差限
        /// </summary>
        /// <param name="Max">上线</param>
        /// <param name="Min">下限</param>
        internal void SetWcx(float Max, float Min)
        {
            if (this.ErrorShangXian == 0F)
                this.ErrorShangXian = Max;
            if (this.ErrorXiaXian == 0F)
                this.ErrorXiaXian = Min;
        }
        #endregion
    }
}

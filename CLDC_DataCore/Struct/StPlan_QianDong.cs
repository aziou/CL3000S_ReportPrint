using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 潜动项目结构
    /// </summary>
    [Serializable()]
    public struct  StPlan_QianDong
    {


        /// <summary>
        /// 日计时参数
        /// </summary>
        public string DayCheckTimesSetting;
        /// <summary>
        /// 功率方向
        /// </summary>
        public CLDC_Comm.Enum.Cus_PowerFangXiang PowerFangXiang;
        /// <summary>
        /// 电压倍数
        /// </summary>
        public float FloatxU; 
        /// <summary>
        /// 电流倍数(数字)这个地方的倍数是指起动电流的倍数
        /// </summary>
        public float FloatxIb;
        /// <summary>
        /// 潜动时间，现在已经再是时间倍数，如果不为0，则是确切的潜动时间
        /// </summary>
        public float xTime;
        /// <summary>
        /// 实际试验时间（分钟）
        /// </summary>
        public float CheckTime;
        /// <summary>
        /// 潜动电流值
        /// </summary>
        public float FloatIb;
        /// <summary>
        /// 默认是否合格0-不默认，1-默认，默认合格情况下，则不会做起动试验
        /// </summary>
        public int DefaultValue;

        /// <summary>
        /// 获取潜动时间和潜动电流
        /// </summary>
        /// <param name="GuiChengName">规程名称</param>
        /// <param name="clfs">测量方式</param>
        /// <param name="U">电压</param>
        /// <param name="Ib">电流 Ib(Imax)</param>
        /// <param name="Dj">等级 有功（无功）</param>
        /// <param name="MeterConst">常数 有功（无功）</param>
        /// <param name="znq"></param>
        /// <param name="hgq"></param>
        public void CheckTimeAndIb(string GuiChengName, CLDC_Comm.Enum.Cus_Clfs clfs, float U, string Ib, string Dj, string MeterConst, bool znq, bool hgq)
        {
            CLDC_Comm.Enum.Cus_Ywg _Ywg = new CLDC_Comm.Enum.Cus_Ywg();

            if (this.PowerFangXiang == CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功 || this.PowerFangXiang == CLDC_Comm.Enum.Cus_PowerFangXiang.反向有功)
                _Ywg = CLDC_Comm.Enum.Cus_Ywg.P;
            else
                _Ywg = CLDC_Comm.Enum.Cus_Ywg.Q;

            float _ib = 0F;

            try
            {
                _ib = CLDC_DataCore.Function.Number.GetCurrentByIb("Ib", Ib);
            }
            catch
            {

            }

            if (_ib == 0F)
            {
                _ib = float.Parse(Ib.Substring(0, Ib.IndexOf("(")));
            }
            float qIb = 0;
            if ("JJG596-2012" != GuiChengName)
            {
                qIb = CLDC_DataCore.Function.QiDQianDFunction.getQiDongCurrent(GuiChengName, _ib, Dj, znq, hgq, _Ywg);
            }
            else
            {
                qIb = 0;
            }
            //float qIb = CLDC_DataCore.Function.QiDQianDFunction.getQianDongCurrent(_ib,hgq);   //海南专用潜动
            if (this.FloatxIb == 0F)
            {
                this.FloatIb = 0F;
            }
            else
            {
                this.FloatIb = this.FloatxIb * qIb;
            }

            if (this.xTime == 0)
            {
                if ("JJG596-2012" != GuiChengName)
                {
                    this.CheckTime = CLDC_DataCore.Function.QiDQianDFunction.getQianDongTime(GuiChengName, U, this.FloatxU, qIb, Dj, CLDC_DataCore.Function.Number.GetBcs(MeterConst, _Ywg == CLDC_Comm.Enum.Cus_Ywg.P ? true : false), clfs,_Ywg);
                }
                else
                {
                    this.CheckTime = CLDC_DataCore.Function.QiDQianDFunction.getQianDongTime(GuiChengName, U, this.FloatxU, CLDC_DataCore.Function.Number.GetCurrentByIb("Imax", Ib), Dj, CLDC_DataCore.Function.Number.GetBcs(MeterConst, _Ywg == CLDC_Comm.Enum.Cus_Ywg.P ? true : false), clfs, _Ywg);
                }
                //this.CheckTime = CLDC_DataCore.Function.QiDQianDFunction.getQianDongTime(U, FloatxU, qIb, CLDC_DataCore.Function.Number.GetCurrentByIb("Imax", Ib), Dj, CLDC_DataCore.Function.Number.GetBcs(MeterConst, _Ywg == CLDC_Comm.Enum.Cus_Ywg.P ? true : false), clfs, hgq, _Ywg);//海南专用
            }
            else
            {
                this.CheckTime = this.xTime;
            }

        }
        /// <summary>
        /// 获取潜动时间和潜动电流
        /// </summary>
        /// <param name="GuiChengName">规程名称</param>
        /// <param name="clfs">测量方式</param>
        /// <param name="U">电压</param>
        /// <param name="Ib">电流 Ib(Imax)</param>
        /// <param name="xIb">起动电流倍数</param>
        /// <param name="Dj">等级</param>
        /// <param name="MeterConst">表常数</param>
        /// <param name="znq">止逆器</param>
        /// <param name="hgq">互感器</param>
        public void CheckTimeAndIb(string GuiChengName, CLDC_Comm.Enum.Cus_Clfs clfs, float U, string Ib,float xIb, string Dj, string MeterConst, bool znq, bool hgq)
        {
             CheckTimeAndIb(GuiChengName, clfs, U, Ib, Dj, MeterConst, znq, hgq);  //海南专用
             return;
            //Comm.Enum.Cus_Ywg _Ywg = new Comm.Enum.Cus_Ywg();

            //if (this.PowerFangXiang == Comm.Enum.Cus_PowerFangXiang.正向有功 || this.PowerFangXiang == Comm.Enum.Cus_PowerFangXiang.反向有功)
            //    _Ywg = Comm.Enum.Cus_Ywg.P;
            //else
            //    _Ywg = Comm.Enum.Cus_Ywg.Q;

            //float _ib = 0F;

            //try
            //{
            //    _ib = Comm.Function.Number.GetCurrentByIb("Ib", Ib);
            //}
            //catch
            //{

            //}

            //if (_ib == 0F)
            //{
            //    _ib = float.Parse(Ib.Substring(0, Ib.IndexOf("(")));
            //}

            //float qIb = xIb * _ib;

            //if (this.FloatxIb == 0F)
            //{
            //    this.FloatIb = 0F;
            //}
            //else
            //{
            //    this.FloatIb = this.FloatxIb * qIb;
            //}

            //if (this.xTime == 0)
            //{
            //    this.CheckTime = Comm.Function.QiDQianDFunction.getQianDongTime(GuiChengName, U, this.FloatxU, qIb, Comm.Function.Number.GetBcs(MeterConst, _Ywg == Comm.Enum.Cus_Ywg.P ? true : false), clfs);
            //}
            //else
            //{
            //    this.CheckTime = this.xTime;
            //}

        }

        /// <summary>
        /// 重写ToString函数，返回潜动描述
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}潜动", PowerFangXiang.ToString());
        }

        public string PrjID
        {
            get
            {
                return String.Format("{0}{1}{2}"                                          //Key:参见数据结构设计附2
                        , (int)Cus_MeterResultPrjID.潜动试验
                        , ((int)PowerFangXiang).ToString()
                       , (Convert.ToInt32(FloatxU * 100)).ToString("D3"));
            }
        }
    }
}

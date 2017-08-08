using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Struct;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 特殊检定方案项目
    /// </summary>
    [Serializable()]
    public struct  StPlan_SpecalCheck
    {
        
        /// <summary>
        /// 项目描述
        /// </summary>
        public string PrjName;

        /// <summary>
        /// 功率方向
        /// </summary>
        public CLDC_Comm.Enum.Cus_PowerFangXiang PowerFangXiang;

        /// <summary>
        /// 功率因数，如：1.0 、0.5C 、-0.8L
        /// </summary>
        public string PowerYinSu;

        /// <summary>
        /// 项目编号（唯一）
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
        /// 电压倍数(A相)
        /// </summary>
        public float xUa ;

        /// <summary>
        /// 电压倍数（B相）
        /// </summary>
        public float xUb;

        /// <summary>
        /// 电压倍数（C相）
        /// </summary>
        public float xUc;

        /// <summary>
        /// 电流倍数（A相）
        /// </summary>
        public float xIa ;
        /// <summary>
        /// 电流倍数（B相）
        /// </summary>
        public float xIb;
        /// <summary>
        /// 电流倍数（C相）
        /// </summary>
        public float xIc;

        /// <summary>
        /// 电压A相相位（暂时无用）
        /// </summary>
        public float fUa;

        /// <summary>
        /// 电压B相相位（暂时无用）
        /// </summary>
        public float fUb;
        /// <summary>
        /// 电压C相相位（暂时无用）
        /// </summary>
        public float fUc;

        /// <summary>
        /// 电流A相相位（暂时无用）
        /// </summary>
        public float fIa;

        /// <summary>
        /// 电流B相相位（暂时无用）
        /// </summary>
        public float fIb;
        /// <summary>
        /// 电流C相相位（暂时无用）
        /// </summary>
        public float fIc;

        /// <summary>
        /// 频率
        /// </summary>
        public float PingLv ;

        /// <summary>
        /// 误差上限
        /// </summary>
        public float WuChaXian_Shang ;

        /// <summary>
        /// 误差下限
        /// </summary>
        public float WuChaXian_Xia ;

        /// <summary>
        /// 误差次数
        /// </summary>
        public int WcCheckNumic;

        /// <summary>
        /// 圈数
        /// </summary>
        public int LapCount;
        /// <summary>
        /// 相序0-正相序，1-逆相序
        /// </summary>
        public int XiangXu;
        /// <summary>
        /// 谐波0-不加，1-加
        /// </summary>
        public int XieBo;

        /// <summary>
        /// 如果加谐波，则看调用哪套谐波方案
        /// </summary>
        public string XieBoFa;
        /// <summary>
        /// 谐波参数列表
        /// </summary>
        public List<StXieBo> XieBoItem;


        /// <summary>
        /// 解析电压倍数字符串（xUa|xUb|xUc）
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
        /// 解析电流倍数字符串（xIa|xIb|xIc）
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
        /// 相位角(Ua,Ub,Uc|Ia,Ib,Ic)（暂时无用）
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
        /// 解析误差限（上线|下线）
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
        /// 组合电压倍数字符串
        /// </summary>
        /// <returns></returns>
        public string JoinxUString()
        {
            return string.Format("{0}|{1}|{2}", this.xUa.ToString(), this.xUb.ToString(), this.xUc.ToString());
        }
        /// <summary>
        /// 组合电流倍数字符串
        /// </summary>
        /// <returns></returns>
        public string JoinxIString()
        {
            return string.Format("{0}|{1}|{2}", this.xIa.ToString(), this.xIb.ToString(), this.xIc.ToString());
        }
        /// <summary>
        /// 组合误差限字符串
        /// </summary>
        /// <returns></returns>
        public String JoinWcxString()
        {
            return string.Format("{0}|{1}", this.WuChaXian_Shang > 0 ? string.Format("+{0}", this.WuChaXian_Shang.ToString()) : this.WuChaXian_Shang.ToString()
                                          , this.WuChaXian_Xia > 0 ? string.Format("+{0}", this.WuChaXian_Xia.ToString()) : this.WuChaXian_Xia.ToString());
        }

        /// <summary>
        /// 组合相位字符串（暂时无用）
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
        /// 特殊检定描述
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}特殊检定：{1},频率{2:f},{3},{4}"
                                ,PowerFangXiang.ToString()
                                ,this.PrjName
                                ,PingLv
                                ,(XieBo==1?"加谐波":"无谐波")
                                ,(XiangXu==1?"逆相序":"正相序"));
        }

    }


}

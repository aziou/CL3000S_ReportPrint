using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public struct StPowerPramerter
    {
        /// <summary>
        /// 功率方向
        /// </summary>
        public CLDC_Comm.Enum.Cus_PowerFangXiang GLFX;
        /// <summary>
        /// 元件
        /// </summary>
        public CLDC_Comm.Enum.Cus_PowerYuanJian YJ;
        /// <summary>
        /// 电压倍数
        /// </summary>
        public float xU;
        /// <summary>
        /// 电流倍数
        /// </summary>
        public string xIb;
        /// <summary>
        /// 功率因素
        /// </summary>
        public string GLYS;
        /// <summary>
        /// 组合结构体参数
        /// </summary>
        /// <returns></returns>
        public string Jion()
        {
            return ((int)GLFX).ToString() + "|" + ((int)YJ).ToString() + "|" + xU.ToString() + "|" + xIb + "|" + GLYS; 
        }

        /// <summary>
        /// 分解结构体参数组合字符串
        /// </summary>
        /// <param name="PramValue"></param>
        public void Split(string PramValue)
        {
            string[] _TmpPram = PramValue.Split('|');
            if (_TmpPram.Length != 5)
            {
                GLFX = CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功;
                YJ = CLDC_Comm.Enum.Cus_PowerYuanJian.H;
                xU = 1F;
                xIb="0Ib";
                GLYS = "1.0";
                return;
            }
            GLFX = (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(_TmpPram[0]);
            YJ = (CLDC_Comm.Enum.Cus_PowerYuanJian)int.Parse(_TmpPram[1]);
            xU = float.Parse(_TmpPram[2]);
            xIb = _TmpPram[3];
            GLYS = _TmpPram[4];
            return;
        }
    }
}

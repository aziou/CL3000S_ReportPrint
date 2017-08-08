using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 特殊检定项目结构
    /// </summary>
    [Serializable()]
    public class MeterSpecialErr : MeterError
    {
        
        /// <summary>
        /// 43项目名称
        /// </summary>
        public string Mse_PrjName = "";
        /// <summary>
        /// 方案名称
        /// </summary>
        public string Mse_chrMemo = "";
        /// <summary>
        /// 26 结论(合格/不合格)
        /// </summary>
        public string Mse_Result = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
        /// <summary>
        /// 项目编号（唯一）
        /// </summary>
        public string Mse_PrjNumber;

        /// <summary>
        /// 4误差类别
        /// </summary>
        public int Mse_intWcType = 1;
        /// <summary>
        /// 6元件
        /// </summary>
        public int Mse_intYj = 1;
        /// <summary>
        /// 类别
        /// </summary>
        public int Mse_intWcLb = 1;
        /// <summary>
        /// 5功率方向1-P+，2-P-,3-Q+，4-Q-
        /// </summary>
        public int Mse_Glfx = 1;
        /// <summary>
        /// 7电流倍数
        /// </summary>
        public float Mse_dblxIb = 1F;

        /// <summary>
        /// 8A相电流倍数的字符串（IB、IMAX）例如：0.5IB、1.2IMAX
        /// </summary>
        public string AVR_CUR_A_MULTIPLE_STRING { get; set; }
        /// <summary>
        /// 13电压倍数
        /// </summary>
        public int Mse_dblxUb = 1;

        /// <summary>
        /// 16误差上限
        /// </summary>
        public int Mse_swcbl = 1;
        /// <summary>
        /// 17误差下限
        /// </summary>
        public int Mse_xwcbl = 1;
        /// <summary>
        /// 18圈数
        /// </summary>
        public int Mse_Qs = 1;

        /// <summary>
        /// Ua|Ub|Uc(试验电压值，非倍数)
        /// </summary>
        public string Mse_Ub = "0|0|0";
        /// <summary>
        /// Ia|Ib|Ic(试验电流，非倍数)
        /// </summary>
        public string Mse_Ib = "0|0|0";
        /// <summary>
        /// Ua,Ub,Uc|Ia,Ib,Ic(相位)（目前填写功率因数值）
        /// </summary>
        public string Mse_Phase = "1.0";//"0,240,120|0,240,120";

        /// <summary>
        /// 频率
        /// </summary>
        public string Mse_Pl = "50";
        /// <summary>
        /// 22频率倍数
        /// </summary>
        public int Mse_dblxHz = 1;
        /// <summary>
        /// 20    0-正相序，1-逆相序
        /// </summary>
        public int Mse_Nxx = 0;
        /// <summary>
        /// 19   0-不加谐波，1-加谐波
        /// </summary>
        public int Mse_XieBo = 0;
        /// <summary>
        /// 21功率因素
        /// </summary>
        public string Mse_chrGlys = "";

        /// <summary>
        /// 31A相电压（根据方案倍数换算的值）
        /// </summary>
        public string AVR_VOT_A { get; set; }
        /// <summary>
        /// 32B相电压
        /// </summary>
        public float Mse_sngUb = 1F;
        /// <summary>
        /// 33C相电压
        /// </summary>
        public float Mse_sngUc = 1F;

        /// <summary>
        /// 34A相电流
        /// </summary>
        public string AVR_CUR_A { get; set; }
        /// B相电流
        /// </summary>
        public float Mse_sngIb = 1F;

        /// <summary>
        /// 10B相电流倍数的字符串（IB、IMAX）
        /// </summary>
        public string AVR_CUR_B_MULTIPLE_STRING { get; set; }
        /// C相电流
        /// </summary>
        public float Mse_sngIc = 1F;


        /// <summary>
        /// 12C相电流倍数的字符串（IB、IMAX）
        /// </summary>
        public string AVR_CUR_C_MULTIPLE_STRING { get; set; }
        /// <summary>
        /// 37A相电压夹角
        /// </summary>
        public float Mse_sngPhi_Ua = 1F;
        /// <summary>
        /// 38B相电压夹角
        /// </summary>
        public float Mse_sngPhi_Ub = 1F;
        /// <summary>
        /// 39C相电压夹角
        /// </summary>
        public float Mse_sngPhi_Uc = 1F;
        /// <summary>
        /// 40A相电流夹角
        /// </summary>
        public float Mse_sngPhi_Ia = 1F;
        /// <summary>
        /// 41B相电流夹角
        /// </summary>
        public float Mse_sngPhi_Ib = 1F;
        /// <summary>
        /// 42C相电流夹角
        /// </summary>
        public float Mse_sngPhi_Ic = 1F;



        /// <summary>
        /// 误差限（上限|下限）
        /// </summary>
        public string Mse_WcLimit = "+1|-1";


        /// <summary>
        /// 25误差值（误差一|误差二|...|误差平均|误差化整）
        /// </summary>
        public string Mse_Wc = "";
        /// <summary>
        /// 23误差化整值
        /// </summary>
        public string Mse_chrWcHz = "";
        /// <summary>
        /// 24误差平均值
        /// </summary>
        public string Mse_chrWc = "";




        /// <summary>
        /// 28变差值
        /// </summary>
        public string AVR_DIF_ERROR { get; set; }


        /// <summary>
        /// 29负载点类型，默认0，非基准点，1：基准点。
        /// </summary>
        public string CHR_BASE_LOAD_FLAG { get; set; }

        /// <summary>
        /// 30标准点编号[1,N] 
        /// </summary>
        public string AVR_BASE_LOAD_NO { get; set; }



        /// <summary>
        /// 14B电压倍数
        /// </summary>
        public string AVR_VOT_B_MULTIPLE { get; set; }

        /// <summary>
        /// 15C电压倍数
        /// </summary>
        public string AVR_VOT_C_MULTIPLE { get; set; }

        /// <summary>
        /// 9B电流倍数
        /// </summary>
        public string AVR_CUR_B_MULTIPLE { get; set; }

        /// <summary>
        /// 11C电流倍数
        /// </summary>
        public string AVR_CUR_C_MULTIPLE { get; set; }
     
        /// <summary>
        /// 误差0
        /// </summary>
        public string Mse_chrWc0 = "";
        /// <summary>
        /// 误差1
        /// </summary>
        public string Mse_chrWc1 = "";
        /// <summary>
        /// 误差2
        /// </summary>
        public string Mse_chrWc2 = "";
        /// <summary>
        /// 误差3
        /// </summary>
        public string Mse_chrWc3 = "";
        /// <summary>
        /// 误差4
        /// </summary>
        public string Mse_chrWc4 = "";
        /// <summary>
        /// 误差5
        /// </summary>
        public string Mse_chrWc5 = "";
        /// <summary>
        /// 误差6
        /// </summary>
        public string Mse_chrWc6 = "";
        /// <summary>
        /// 误差7
        /// </summary>
        public string Mse_chrWc7 = "";
        /// <summary>
        /// 误差8
        /// </summary>
        public string Mse_chrWc8 = "";
        /// <summary>
        /// 误差9
        /// </summary>
        public string Mse_chrWc9 = "";

        /// <summary>
        /// 打印报表数据-结论|平均值|化整值|误差1|误差2
        /// </summary>
        public string PrintData
        {
            get
            {
                string value = "";
                value = Mse_Result;
                string[] Arr_Err = Mse_Wc.Split('|');
                if (Arr_Err.Length == 4)
                {
                    value += "|" + Arr_Err[2];
                    value += "|" + Arr_Err[3];
                    value += "|" + Arr_Err[0];
                    value += "|" + Arr_Err[1];
                }
                else
                {
                    value += "||||";
                }
                return value;
            }
        }
    }
}

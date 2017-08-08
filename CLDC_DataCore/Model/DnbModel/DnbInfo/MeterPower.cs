using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 功耗检定数据
    /// </summary>
    [Serializable()]
    public class MeterPower : MeterErrorBase
    {
        /// <summary>
        /// 功耗项目ID	
        /// </summary>
        public string Md_PrjID;
        /// <summary>
        /// 项目名称		描述
        /// </summary>
        public string Md_PrjName;
        /// <summary>
        /// 项目值
        /// </summary>
        public string Md_chrValue;


        /// <summary>
        /// A相电压回路有功功率
        /// </summary>
        public string Md_Ua_ReactiveP;
        /// <summary>
        /// A相电压回路视在功率
        /// </summary>
        public string Md_Ua_ReactiveS;
        /// <summary>
        /// A相电流回路视在功率
        /// </summary>
        public string Md_Ia_ReactiveS;

        /// <summary>
        /// B相电压回路有功功率
        /// </summary>
        public string Md_Ub_ReactiveP;
        /// <summary>
        /// B相电压回路视在功率
        /// </summary>
        public string Md_Ub_ReactiveS;
        /// <summary>
        /// B相电流回路视在功率
        /// </summary>
        public string Md_Ib_ReactiveS;

        /// <summary>
        /// C相电压回路有功功率
        /// </summary>
        public string Md_Uc_ReactiveP;
        /// <summary>
        /// C相电压回路视在功率
        /// </summary>
        public string Md_Uc_ReactiveS;
        /// <summary>
        /// C相电流回路视在功率
        /// </summary>
        public string Md_Ic_ReactiveS;



        public string AVR_CUR_CIR_A_VOT;
        public string AVR_CUR_CIR_B_VOT;
        public string AVR_CUR_CIR_C_VOT;
        public string AVR_CUR_CIR_A_CUR;
        public string AVR_CUR_CIR_B_CUR;
        public string AVR_CUR_CIR_C_CUR;
        public string AVR_VOT_CIR_A_VOT;
        public string AVR_VOT_CIR_B_VOT;
        public string AVR_VOT_CIR_C_VOT;
        public string AVR_VOT_CIR_A_CUR;
        public string AVR_VOT_CIR_B_CUR;
        public string AVR_VOT_CIR_C_CUR;
        public string AVR_VOT_CIR_A_ANGLE;
        public string AVR_VOT_CIR_B_ANGLE;
        public string AVR_VOT_CIR_C_ANGLE;

        public string AVR_CUR_CIR_S_LIMIT;
        public string Mgh_chrISJL;
        public string AVR_VOT_CIR_S_LIMIT;
        public string Mgh_chrUSJL;
        public string AVR_VOT_CIR_P_LIMIT;
        public string Mgh_chrUPJL;
    }
}

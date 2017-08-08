using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    [Serializable()]
    public class MeterErrAccordBase : MeterErrorBase
    {
        public MeterErrAccordBase()
        {

        }
        /// <summary>
        /// 误差项目ID
        /// </summary>
        public string Mea_PrjID = "";
        /// <summary>
        /// 项目名称描述
        /// </summary>
        public string Mea_PrjName = "";
        /// <summary>
        /// 检定点结论		合格/不合格
        /// </summary>
        public string Mea_ItemResult = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
        /// <summary>
        /// 功率因素		1.0，0.5L等
        /// </summary>
        public string Mea_Glys = "";
        /// <summary>
        /// 电流倍数		Imax,3.0Ib等
        /// </summary>
        public string Mea_xib = "";
        /// <summary>
        /// 电压倍数		1,0.5,0.8,1.2等 
        /// </summary>
        public string Mea_xU = "";
        /// <summary>
        /// 误差限		   上线|下线
        /// </summary>
        public string Mea_WcLimit = "";
        /// <summary>
        /// 圈数
        /// </summary>
        public int Mea_Qs = 0;
        /// <summary>
        /// 频率
        /// </summary>
        public string Mea_PL = "";
        /// <summary>
        /// 误差值1(第一次测量)		误差一|误差二|...|误差平均|误差化整
        /// </summary>	
        public string Mea_Wc1 = "";
        /// <summary>
        /// 误差值2(第二次测量)		误差一|误差二|...|误差平均|误差化整
        /// </summary>	
        public string Mea_Wc2 = "";
        /// <summary>
        /// 差值
        /// </summary>
        public string Mea_Wc = "";
        /// <summary>
        /// 样品均值                只对误差一致性有效    
        /// </summary>
        public string Mea_WcAver = "";
        /// 误差一致性的子项目编号，值为：{0}{1}，误差一致性类型，子项目序号+1
        /// <summary>
        /// 误差一致性的子项目编号，值为：{0}{1}，误差一致性类型，子项目序号+1
        /// </summary>
        public string Sub_Item_ID = "";
    }
}

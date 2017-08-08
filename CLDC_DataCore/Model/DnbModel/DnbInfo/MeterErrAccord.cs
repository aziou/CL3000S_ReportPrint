using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    [Serializable()]
    public class MeterErrAccord : MeterErrorBase
    {
        public MeterErrAccord()
        {

        }

        /// <summary>
        /// 误差一致性类型 1：误差一致性 2：误差变差 3：负载电流升降 4：电流过载
        /// </summary>
        public int Mea_Type;
        
        /// <summary>
        /// 项目结论		合格/不合格
        /// </summary>
        public string Mea_Result = CLDC_DataCore.Const.Variable.CTG_HeGe;

        /// <summary>
        /// 进度值
        /// </summary>
        public string ProgressValue;

        /// <summary>
        /// 该项目下包含的检定点
        /// </summary>
        public Dictionary<string, MeterErrAccordBase> lstTestPoint = new Dictionary<string, MeterErrAccordBase>();
        /// <summary>
        /// 不合格原因
        /// </summary>
        public string Description;
    }
}

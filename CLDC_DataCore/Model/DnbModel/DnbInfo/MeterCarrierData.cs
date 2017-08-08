using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 功能描述：载波通讯检定
    /// 作    者：vs
    /// 编写日期：2010-09-13
    /// 修改记录：
    ///         修改日期		     修改人	            修改内容
    ///
    /// </summary>
    [Serializable()]
    public class MeterCarrierData : MeterErrorBase 
    {
        
        /// <summary>
        /// 项目ID	
        /// </summary>
        public string Mce_PrjID = "";
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Mce_PrjName = "";
        /// <summary>
        /// 项目值
        /// </summary>
        public string Mce_PrjValue = "";
        /// <summary>
        /// 项目ID
        /// </summary>
        public string Mce_PrjNumber = "";
        /// <summary>
        /// 检定点结论
        /// </summary>
        public string Mce_ItemResult = "";

        /// <summary>
        /// 7开始召测时间
        /// </summary>
        public string DTM_START_TIME { get; set; }

        /// <summary>
        /// 8返回时间
        /// </summary>
        public string DTM_END_TIME { get; set; }

        /// <summary>
        /// 9总次数
        /// </summary>
        public string AVR_NUMBER_TOTAL { get; set; }

        /// <summary>
        /// 10成功次数
        /// </summary>
        public string AVR_NUMBER_SUCCEED { get; set; }

        /// <summary>
        /// 11失败次数
        /// </summary>
        public string AVR_NUMBER_FAIL { get; set; }

        /// <summary>
        /// 12成功率（存%前面的数字）
        /// </summary>
        public string AVR_RATIO_SUCCEED { get; set; }
        /// <summary>
        /// 13成功率误差限（大于0 的一个值）
        /// </summary>
        public string AVR_LIMIT { get; set; }

        /// <summary>
        /// 14备注：载波设备信息
        /// </summary>
        public string AVR_RESERVE { get; set; }
    }
}

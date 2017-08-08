using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// 功能描述：红外通讯检定
    /// 作    者：zzg
    /// 编写日期：2014-04-07
    /// 修改记录：
    ///         修改日期		     修改人	            修改内容
    ///
    /// </summary>
    [Serializable()]
    public class MeterInfraredData : MeterErrorBase 
    {
        
        /// <summary>
        /// 项目ID	
        /// </summary>
        public string Mif_PrjID = "";
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Mif_PrjName = "";
        /// <summary>
        /// 红外抄收数据
        /// </summary>
        public string Mif_PrjInfrareValue = "";
        /// <summary>
        /// 485抄收数据
        /// </summary>
        public string Mif_Prj485Value = "";
        /// <summary>
        /// 项目ID
        /// </summary>
        public string Mif_PrjNumber = "";
        /// <summary>
        /// 检定点结论
        /// </summary>
        public string Mif_ItemResult = "";

        /// <summary>
        /// 7开始召测时间
        /// </summary>
        public string DTM_START_TIME { get; set; }

        /// <summary>
        /// 8返回时间
        /// </summary>
        public string DTM_END_TIME { get; set; }

        /// <summary>
        /// 14备注：红外设备信息
        /// </summary>
        public string AVR_RESERVE { get; set; }
    }
}

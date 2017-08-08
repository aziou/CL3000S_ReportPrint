using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{

    [Serializable()]
    public class MeterResult:MeterErrorBase 
    {
        
        /// <summary>
        /// 4结论ID
        /// </summary>
        public string Mr_chrRstId = "";
        /// <summary>
        /// 5结论名称
        /// </summary>
        public string Mr_chrRstName = "";
        /// <summary>
        /// 6结论  0：默认，-1：不合格，1：合格
        /// </summary>
        public string Mr_chrRstValue = "";
        /// <summary>
        /// 7备注
        /// </summary>
        public string Mr_chrNote = "";

        
    }
}

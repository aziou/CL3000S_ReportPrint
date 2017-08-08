using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace CLDC_DataManager.Class
{
    public class ClouMeterErrorType
    {
        private string error_GLYS;
        /// <summary>
        /// 功率因素
        /// </summary>
        public string Error_GLYS { get; set; }

        private string error_FZDL;
        /// <summary>
        /// 负载电流
        /// </summary>
        public string Error_FZDL { get; set; }

        private string error_GLFX;
        /// <summary>
        /// 功率方向
        /// </summary>
        public string Error_GLFX { get; set; }

        private string error_FY;
        /// <summary>
        /// 负载方向
        /// </summary>
        public string Error_FY { get; set; }

        private string error_HZZ;
        /// <summary>
        /// 误差化整值
        /// </summary>
        public string Error_HZZ { get; set; }



    }
}

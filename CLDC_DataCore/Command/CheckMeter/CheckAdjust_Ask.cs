using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// 请求调表、或者结束调表
    /// </summary>
    [Serializable]
    public class CheckAdjust_Ask:Command_Ask 
    {
        /// <summary>
        /// true调表、false结束调表
        /// </summary>
        public bool  IsAdjust;

        /// <summary>
        /// 
        /// </summary>
        public CheckAdjust_Ask()
        {
            AskMessage = "调表";
        }

    }



    


    





}

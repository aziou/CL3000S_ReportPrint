using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.CheckMeter
{
    /// <summary>
    /// ����������߽�������
    /// </summary>
    [Serializable]
    public class CheckAdjust_Ask:Command_Ask 
    {
        /// <summary>
        /// true����false��������
        /// </summary>
        public bool  IsAdjust;

        /// <summary>
        /// 
        /// </summary>
        public CheckAdjust_Ask()
        {
            AskMessage = "����";
        }

    }



    


    





}

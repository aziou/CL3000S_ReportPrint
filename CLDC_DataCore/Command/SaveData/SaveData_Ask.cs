using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.SaveData
{
    /// <summary>
    /// 保存数据
    /// </summary>
    [Serializable]
    public class SaveData_Ask:Command_Ask 
    {

        ///// <summary>
        ///// 总模型数据、使用中需要考虑当前数据依哪边为准！
        ///// </summary>
        //public CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup = null;
        /// <summary>
        /// 
        /// </summary>
        public SaveData_Ask()
        {
            AskMessage = "保存数据";
        }

    }

}

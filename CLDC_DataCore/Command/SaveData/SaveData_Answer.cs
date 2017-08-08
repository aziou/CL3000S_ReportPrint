using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.SaveData
{
    /// <summary>
    /// 保存数据
    /// </summary>
    [Serializable]
    public class SaveData_Answer:Command_Answer 
    {

        /// <summary>
        ///  处理结果
        /// </summary>
        public bool bAgree;

     
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Update
{
    /// <summary>
    /// 更新要检查信息
    /// </summary>
    [Serializable]
    public class UpdateYaoJian_Ask:Command_Ask
    {
        /// <summary>
        /// 表位号
        /// </summary>
        public int Bwh;

        /// <summary>
        /// 是否要检
        /// </summary>
        public bool IsYaoJian;

        /// <summary>
        /// 
        /// </summary>
        public UpdateYaoJian_Ask()
        {
            AskMessage = "表是否要检状态更新";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Command
{
    /// <summary>
    /// 检定点发生变化
    /// </summary>
    [Serializable()]
    public class ChangePoint: CLDC_CTNProtocol.CTNPCommand
    {
        /// <summary>
        /// 当前检定点
        /// </summary>
        public int CurVerifyPoint;
        /// <summary>
        /// 当前检定点名称
        /// </summary>
        public string CurVerifyName="";

    }
}

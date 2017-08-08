using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Plan
{

    /// <summary>
    /// 发送方案数据的返回
    /// </summary>
    [Serializable]
    public class GetPlanFile_Answer:Command_Answer 
    {
        /// <summary>
        /// 是否已经装载了数据的
        /// </summary>
        public bool IsLoadFile = false;

        /// <summary>
        /// 方案数据文件，Key =文件名(相对路径) ， Value = 对应文件的数据
        /// 包括方案文件和Const文件夹的文件
        /// </summary>
        public Dictionary<string, byte[]> LstFileData = new Dictionary<string, byte[]>();

        /// <summary>
        /// 
        /// </summary>
        public GetPlanFile_Answer()
        {
        }


    }


}

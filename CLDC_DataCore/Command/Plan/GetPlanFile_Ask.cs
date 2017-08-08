using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Plan
{

    /// <summary>
    /// 发送方案信息
    /// </summary>
    [Serializable] 
    public class GetPlanFile_Ask:Command_Ask 
    {
        /// 检定方案名称
        /// <summary>
        /// 检定方案名称
        /// </summary>
        public string ProjectName = string.Empty;
        /// 在CTNP建立连接时，将检定方案发到服务器
        /// <summary>
        /// 在CTNP建立连接时，将检定方案发到服务器
        /// </summary>
        public List<object> CheckProject = new List<object>();
        /// <summary>
        /// 
        /// </summary>
        public GetPlanFile_Ask()
        {
            AskMessage = "发送检定方案";
        }

    }



}

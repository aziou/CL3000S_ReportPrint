using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Update
{
    /// <summary>
    /// 局部数据更新[添加/修改/删除]
    /// </summary>
    [Serializable()]
    public class UpdateRealTimeData_Ask:Command_Ask
    {
        /// <summary>
        /// 操作数据类型
        /// </summary>
        public CLDC_Comm.Enum.Cus_MeterDataType DataType;
        /// <summary>
        /// 当前项目键值
        /// </summary>
        public string strItemKey;
        /// <summary>
        /// 开始结束标识
        /// </summary>
        public bool bStartFlag;
        /// <summary>
        /// 
        /// </summary>
        public UpdateRealTimeData_Ask()
        {
        }

    }
}

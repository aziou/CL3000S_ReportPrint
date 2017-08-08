using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Command.Update
{
    /// <summary>
    /// 局部数据更新[添加/修改/删除]
    /// </summary>
    [Serializable()]
    public class UpdateData_Ask : Command_Ask
    {
        /// <summary>
        /// 操作数据类型
        /// </summary>
        public CLDC_Comm.Enum.Cus_MeterDataType DataType;
        /// <summary>
        /// 要更新的键值
        /// </summary>
        public string[] strKey;  
        /// <summary>
        /// 对应键值的数据
        /// </summary>
        public object[] objData;
        /// <summary>
        /// 表位，如果为999则为所有表
        /// </summary>
        public int BW;        
        /// <summary>
        /// 是否是删除当前数据
        /// </summary>
        public bool isDelete=false; 
        /// <summary>
        /// 
        /// </summary>
        public UpdateData_Ask()
        {
        }

    }
}

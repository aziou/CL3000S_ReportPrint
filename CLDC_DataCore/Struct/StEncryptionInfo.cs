using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 功能描述：加密机实体
    /// 作    者：jeson wong
    /// 编写日期：2010-11-01
    /// </summary>
    [Serializable()]
    public class StEncryptionInfo
    {
        /// <summary>
        /// 密码机类型
        /// </summary>
        public string MachineType;
        /// <summary>
        /// 密码机IP
        /// </summary>
        public string MachineIP;
        /// <summary>
        /// 密码机端口
        /// </summary>
        public string MachinePort;
        /// <summary>
        /// UseKey密码
        /// </summary>
        public string MachineUseKey;
        /// <summary>
        /// 是否默认自动连接
        /// </summary>
        public int IsLink;
        /// <summary>
        /// 用户代码
        /// </summary>
        public string UserCode;
        /// <summary>
        /// 数据源，存在多个数据用管道符号分隔(服务器类型|简装类型|开发套件）
        /// </summary>
        public string DataSource;

    }
}

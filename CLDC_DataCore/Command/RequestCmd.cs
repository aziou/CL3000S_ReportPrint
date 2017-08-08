using System;
using System.Collections.Generic;
using System.Text;
using CLDC_CTNProtocol;
using CLDC_Comm.Enum;
using CLDC_DataCore.Const;
namespace CLDC_Comm.Command
{
    /// <summary>
    /// 请求操作
    /// </summary>
    [Serializable()]
   public class RequestCmd:CTNPCommand
	{
			/// <summary>
        /// 下面是数据字段
			/// </summary>
			public bool RequestOK=false;		//由接收端（这里不一定是客户端，服务器也有可能会发出请求）返回
	

	}

}

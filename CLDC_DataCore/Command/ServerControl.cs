using System;
using System.Collections.Generic;
using System.Text;
//using CLDC_DataCore.Model.DnbModel;
using CLDC_Comm.Enum;
using CLDC_Comm.Command;
using CLDC_CTNProtocol;

namespace CLDC_Comm.Command
{
    class ServerControl:CTNPCommand
	{
			//下面是数据字段
			//public DnbGroupInfo DnbData=null;		//电能表信息集合，由客户端返回,服务器从方案中获取当前工作到什么位置了
			public bool ControlOK=false;				//服务器返回，客户端收到确认后，立即受服务器控制； 
			public ServerControl()	//参数为对应命令类型
			{
                this.ControlOK = false;
			}
	}

}

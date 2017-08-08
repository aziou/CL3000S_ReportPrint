using System;
using System.Collections.Generic;
using System.Text;
using CLDC_CTNProtocol;
using CLDC_Comm.Enum;
//using CLDC_DataCore.Model.DnbModel;
//using CLDC_DataCore.Model.Plan;

namespace CLDC_Comm.Command
{
    class Control:CTNPCommand
	{
		//下面是数据字段
        //public Model_Plan  Pan=null ;	//服务器发送方案，当前检定方案中，需要立即切换到检定的测试项目会有标记，客户段读取该标志，进行检定切换
        //public DnbGroupInfo DnbData=null ;		//客户端返回，在执行完该项目后，返回测试数据
		public Control()	//参数为对应命令类型
		{

		}
	}

}

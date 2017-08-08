using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{


    /// <summary>
    /// 消息命令类别
    /// </summary>
    public enum  Cus_CommandType
    {
        /// <summary>
        /// 请求下载_检定方案
        /// </summary>0x10
        Cmd_Plan_DownPlan = 0x10 ,

        /// <summary>
        ///  Cmd_DownPlan 消息的回复
        /// </summary>
        Cmd_Plan_DownPlan_Answer = 0x11,

        /// <summary>
		/// 请求登陆
		/// </summary>
		Login = 0x01 ,	

        /// <summary>
        /// 文件下载
        /// </summary>
		DownFile	= 0x02 ,
		
	    /// <summary>
        /// 请求录入条码(参数)
        /// </summary>
		Txm	= 0x03 ,

	    /// <summary>
        /// 上传条码号
        /// </summary>
		InPutTxm	= 0x04 ,

        /// <summary>
        /// 录入条码（参数）完毕
        /// </summary>
	     TxmOk 	= 0x05 , 

        /// <summary>
        /// 开始检定
        /// </summary>
        Start = 0x10,

	    /// <summary>
        /// 立即停止检定
        /// </summary>
        Stop = 0x11,

         /// <summary>
        /// 检定状态
        /// </summary>
	   CheckState=0x12,

	   /// <summary>
        /// 检定完毕
        /// </summary>
	   UpdateCheck=0x13,

	   /// <summary>
        /// 客户端申请控制，集控状态下
        /// </summary>
        Control = 0x30,

	    /// <summary>
        /// 点对点控制申请
        /// </summary>
	    ServerControl=0x40,
	
	    /// <summary>
        /// 点对点控制
        /// </summary>
	   PtoPControl=0x41


    }

    

}

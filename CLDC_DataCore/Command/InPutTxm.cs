using System;
using System.Collections.Generic;
using System.Text;
using CLDC_CTNProtocol;
using CLDC_Comm.Enum;
using CLDC_DataCore.Const;
namespace CLDC_Comm.Command
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public class InPutTxm:CTNPCommand
    {
        //下面是数据字段

        /// <summary>
        /// 条码号
        /// </summary>
        public string Txm = "";		//输入的条码号
        /// <summary>
        /// 表位号.下标从0开始
        /// </summary>
        public int Bwh = 0;			//表位号,下标从0开始
        /// <summary>
        /// 是否处理完毕
        /// </summary>
        public bool TxmOk = false;	//由服务器返回，确认条码输入是否成功
        /// <summary>
        /// 构造函数
        /// </summary>
        public InPutTxm()
        {
            //this.SubID = MessageID.CUS_SUB_REQUESTINPUTTXM;
            this.TxmOk = false;
            this.Txm = "";
        }
    }

}

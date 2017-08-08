using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Command
{
    /// <summary>
    /// 走字试验数据上报
    /// </summary>
    [Serializable()]
    public class CheckZZ:CLDC_CTNProtocol.CTNPCommand 
    {
        /// <summary>
        ///  走字数据
        /// </summary>
        public CLDC_Comm.MessageArgs.EventConstandArgs zzDataArg=null;


    } 
}

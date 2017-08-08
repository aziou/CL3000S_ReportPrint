using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{
    /// 参数检查的模式
    /// <summary>
    /// 参数检查的模式
    /// </summary>
    public enum Cus_VerifyMode
    {
        仅核对=0,
        核对不一致后写入=1,
        写入后再核对=2,
    }
}

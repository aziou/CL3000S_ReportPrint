using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Comm.Enum
{

    /// <summary>
    /// 电流档位
    /// </summary>
    public enum Cus_StdMeterIDangWei
    {
        /// <summary>
        /// 
        /// </summary>
        档位100A = 0,
        /// <summary>
        /// 
        /// </summary>
        档位50A = 0x01,
        /// <summary>
        /// 
        /// </summary>
        档位20A = 0x02,
        /// <summary>
        /// 
        /// </summary>
        档位10A = 0x03,
        /// <summary>
        /// 
        /// </summary>
        档位5A = 0x04,
        /// <summary>
        /// 
        /// </summary>
        档位2A = 0x05,
        /// <summary>
        /// 
        /// </summary>
        档位1A = 0x06,
        /// <summary>
        /// 
        /// </summary>
        档位05A = 0x07,
        /// <summary>
        /// 
        /// </summary>
        档位02A = 0x08,
        /// <summary>
        /// 
        /// </summary>
        档位01A = 0x09,
        /// <summary>
        /// 
        /// </summary>
        档位005A = 0x0a,
        /// <summary>
        /// 
        /// </summary>
        档位002A = 0x0b,
        /// <summary>
        /// 
        /// </summary>
        档位001A = 0x0c
    }

    /// <summary>
    /// 电压档位
    /// </summary>
    public enum Cus_StdMeterVDangWei
    {
        /// <summary>
        /// 
        /// </summary>
        档位480V=1,
        /// <summary>
        /// 
        /// </summary>
        档位240V=2,
        /// <summary>
        /// 
        /// </summary>
        档位120V=3,
        /// <summary>
        /// 
        /// </summary>
        档位60V=4
    }
}

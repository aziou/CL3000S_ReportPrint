using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// 电源参数
    /// </summary>
    /// 
    [Serializable()]
    public struct StPower
    {
        /// <summary>
        /// 接线方式
        /// </summary>
        public Cus_Clfs Clfs;  //	 	
        /// <summary>
        /// 相位开关控制
        /// </summary>
        public byte Flip_ABC;     //   	'	
        /// <summary>
        /// 频率
        /// </summary>
        public float Freq;//	'	
        /// <summary>
        /// Ua档位
        /// </summary>
        public byte Scale_Ua;// 	' 	
        /// <summary>
        /// Ub档位
        /// </summary>
        public byte Scale_Ub;// 	' 	
        /// <summary>
        /// Uc档位
        /// </summary>
        public byte Scale_Uc;// 	' 	
        /// <summary>
        /// Ia档位
        /// </summary>
        public byte Scale_Ia;// 	' 	
        /// <summary>
        /// Ib档位
        /// </summary>
        public byte Scale_Ib;// 	' 	
        /// <summary>
        /// Ic档位
        /// </summary>
        public byte Scale_Ic;// 	' 	
        /// <summary>
        /// UA
        /// </summary>
        public float Ua;//	' 
        /// <summary>
        /// Ia
        /// </summary>
        public float Ia;//	' 	
        /// <summary>
        /// UB
        /// </summary>
        public float Ub;//	'  	
        /// <summary>
        /// Ib
        /// </summary>
        public float Ib;//  	
        /// <summary>
        /// UC
        /// </summary>
        public float Uc;// 	' 	
        /// <summary>
        /// Ic
        /// </summary>
        public float Ic;// ' 	
        /// <summary>
        /// Ua相位
        /// </summary>
        public float Phi_Ua;// 	' 	
        /// <summary>
        /// Ia相位
        /// </summary>
        public float Phi_Ia;// 	' 	
        /// <summary>
        /// UB相位
        /// </summary>
        public float Phi_Ub;//	' 	
        /// <summary>
        /// Ib相位 
        /// </summary>
        public float Phi_Ib;// 	'	
        /// <summary>
        /// UC相位
        /// </summary>
        public float Phi_Uc;// 	' 	
        /// <summary>
        /// Ic相位
        /// </summary>
        public float Phi_Ic;// 	' 	
        /// <summary>
        /// A相有功功率
        /// </summary>
        public float Pa;// 	' 	
        /// <summary>
        /// B相有功功率
        /// </summary>
        public float Pb;// 	'	
        /// <summary>
        /// C相有功功率
        /// </summary>
        public float Pc;// 	'	
        /// <summary>
        /// A相无功功率
        /// </summary>
        public float Qa;//	'	
        /// <summary>
        /// B相无功功率
        /// </summary>
        public float Qb;//	'	
        /// <summary>
        /// C相无功功率
        /// </summary>
        public float Qc;//	'	
        /// <summary>
        /// A相视在功率
        /// </summary>
        public float Sa;//	'	
        /// <summary>
        /// B相视在功率
        /// </summary>
        public float Sb;// 	'	
        /// <summary>
        /// C相视在功率
        /// </summary>
        public float Sc;// 	'	
        /// <summary>
        /// 总有功功率
        /// </summary>
        public float P;//		
        /// <summary>
        /// 总无功功率
        /// </summary>
        public float Q;//		
        /// <summary>
        /// 总视在功功率
        /// </summary>
        public float S;//		
        /// <summary>
        /// 有功功率因数
        /// </summary>
        public float COS;//		
        /// <summary>
        /// 无功功率因数
        /// </summary>
        public float SIN;//	
    }
}

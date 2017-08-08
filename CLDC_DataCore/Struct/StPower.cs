using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Struct
{
    /// <summary>
    /// ��Դ����
    /// </summary>
    /// 
    [Serializable()]
    public struct StPower
    {
        /// <summary>
        /// ���߷�ʽ
        /// </summary>
        public Cus_Clfs Clfs;  //	 	
        /// <summary>
        /// ��λ���ؿ���
        /// </summary>
        public byte Flip_ABC;     //   	'	
        /// <summary>
        /// Ƶ��
        /// </summary>
        public float Freq;//	'	
        /// <summary>
        /// Ua��λ
        /// </summary>
        public byte Scale_Ua;// 	' 	
        /// <summary>
        /// Ub��λ
        /// </summary>
        public byte Scale_Ub;// 	' 	
        /// <summary>
        /// Uc��λ
        /// </summary>
        public byte Scale_Uc;// 	' 	
        /// <summary>
        /// Ia��λ
        /// </summary>
        public byte Scale_Ia;// 	' 	
        /// <summary>
        /// Ib��λ
        /// </summary>
        public byte Scale_Ib;// 	' 	
        /// <summary>
        /// Ic��λ
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
        /// Ua��λ
        /// </summary>
        public float Phi_Ua;// 	' 	
        /// <summary>
        /// Ia��λ
        /// </summary>
        public float Phi_Ia;// 	' 	
        /// <summary>
        /// UB��λ
        /// </summary>
        public float Phi_Ub;//	' 	
        /// <summary>
        /// Ib��λ 
        /// </summary>
        public float Phi_Ib;// 	'	
        /// <summary>
        /// UC��λ
        /// </summary>
        public float Phi_Uc;// 	' 	
        /// <summary>
        /// Ic��λ
        /// </summary>
        public float Phi_Ic;// 	' 	
        /// <summary>
        /// A���й�����
        /// </summary>
        public float Pa;// 	' 	
        /// <summary>
        /// B���й�����
        /// </summary>
        public float Pb;// 	'	
        /// <summary>
        /// C���й�����
        /// </summary>
        public float Pc;// 	'	
        /// <summary>
        /// A���޹�����
        /// </summary>
        public float Qa;//	'	
        /// <summary>
        /// B���޹�����
        /// </summary>
        public float Qb;//	'	
        /// <summary>
        /// C���޹�����
        /// </summary>
        public float Qc;//	'	
        /// <summary>
        /// A�����ڹ���
        /// </summary>
        public float Sa;//	'	
        /// <summary>
        /// B�����ڹ���
        /// </summary>
        public float Sb;// 	'	
        /// <summary>
        /// C�����ڹ���
        /// </summary>
        public float Sc;// 	'	
        /// <summary>
        /// ���й�����
        /// </summary>
        public float P;//		
        /// <summary>
        /// ���޹�����
        /// </summary>
        public float Q;//		
        /// <summary>
        /// �����ڹ�����
        /// </summary>
        public float S;//		
        /// <summary>
        /// �й���������
        /// </summary>
        public float COS;//		
        /// <summary>
        /// �޹���������
        /// </summary>
        public float SIN;//	
    }
}

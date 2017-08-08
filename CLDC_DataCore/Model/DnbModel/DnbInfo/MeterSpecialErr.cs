using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_DataCore.Model.DnbModel.DnbInfo
{
    /// <summary>
    /// ����춨��Ŀ�ṹ
    /// </summary>
    [Serializable()]
    public class MeterSpecialErr : MeterError
    {
        
        /// <summary>
        /// 43��Ŀ����
        /// </summary>
        public string Mse_PrjName = "";
        /// <summary>
        /// ��������
        /// </summary>
        public string Mse_chrMemo = "";
        /// <summary>
        /// 26 ����(�ϸ�/���ϸ�)
        /// </summary>
        public string Mse_Result = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
        /// <summary>
        /// ��Ŀ��ţ�Ψһ��
        /// </summary>
        public string Mse_PrjNumber;

        /// <summary>
        /// 4������
        /// </summary>
        public int Mse_intWcType = 1;
        /// <summary>
        /// 6Ԫ��
        /// </summary>
        public int Mse_intYj = 1;
        /// <summary>
        /// ���
        /// </summary>
        public int Mse_intWcLb = 1;
        /// <summary>
        /// 5���ʷ���1-P+��2-P-,3-Q+��4-Q-
        /// </summary>
        public int Mse_Glfx = 1;
        /// <summary>
        /// 7��������
        /// </summary>
        public float Mse_dblxIb = 1F;

        /// <summary>
        /// 8A������������ַ�����IB��IMAX�����磺0.5IB��1.2IMAX
        /// </summary>
        public string AVR_CUR_A_MULTIPLE_STRING { get; set; }
        /// <summary>
        /// 13��ѹ����
        /// </summary>
        public int Mse_dblxUb = 1;

        /// <summary>
        /// 16�������
        /// </summary>
        public int Mse_swcbl = 1;
        /// <summary>
        /// 17�������
        /// </summary>
        public int Mse_xwcbl = 1;
        /// <summary>
        /// 18Ȧ��
        /// </summary>
        public int Mse_Qs = 1;

        /// <summary>
        /// Ua|Ub|Uc(�����ѹֵ���Ǳ���)
        /// </summary>
        public string Mse_Ub = "0|0|0";
        /// <summary>
        /// Ia|Ib|Ic(����������Ǳ���)
        /// </summary>
        public string Mse_Ib = "0|0|0";
        /// <summary>
        /// Ua,Ub,Uc|Ia,Ib,Ic(��λ)��Ŀǰ��д��������ֵ��
        /// </summary>
        public string Mse_Phase = "1.0";//"0,240,120|0,240,120";

        /// <summary>
        /// Ƶ��
        /// </summary>
        public string Mse_Pl = "50";
        /// <summary>
        /// 22Ƶ�ʱ���
        /// </summary>
        public int Mse_dblxHz = 1;
        /// <summary>
        /// 20    0-������1-������
        /// </summary>
        public int Mse_Nxx = 0;
        /// <summary>
        /// 19   0-����г����1-��г��
        /// </summary>
        public int Mse_XieBo = 0;
        /// <summary>
        /// 21��������
        /// </summary>
        public string Mse_chrGlys = "";

        /// <summary>
        /// 31A���ѹ�����ݷ������������ֵ��
        /// </summary>
        public string AVR_VOT_A { get; set; }
        /// <summary>
        /// 32B���ѹ
        /// </summary>
        public float Mse_sngUb = 1F;
        /// <summary>
        /// 33C���ѹ
        /// </summary>
        public float Mse_sngUc = 1F;

        /// <summary>
        /// 34A�����
        /// </summary>
        public string AVR_CUR_A { get; set; }
        /// B�����
        /// </summary>
        public float Mse_sngIb = 1F;

        /// <summary>
        /// 10B������������ַ�����IB��IMAX��
        /// </summary>
        public string AVR_CUR_B_MULTIPLE_STRING { get; set; }
        /// C�����
        /// </summary>
        public float Mse_sngIc = 1F;


        /// <summary>
        /// 12C������������ַ�����IB��IMAX��
        /// </summary>
        public string AVR_CUR_C_MULTIPLE_STRING { get; set; }
        /// <summary>
        /// 37A���ѹ�н�
        /// </summary>
        public float Mse_sngPhi_Ua = 1F;
        /// <summary>
        /// 38B���ѹ�н�
        /// </summary>
        public float Mse_sngPhi_Ub = 1F;
        /// <summary>
        /// 39C���ѹ�н�
        /// </summary>
        public float Mse_sngPhi_Uc = 1F;
        /// <summary>
        /// 40A������н�
        /// </summary>
        public float Mse_sngPhi_Ia = 1F;
        /// <summary>
        /// 41B������н�
        /// </summary>
        public float Mse_sngPhi_Ib = 1F;
        /// <summary>
        /// 42C������н�
        /// </summary>
        public float Mse_sngPhi_Ic = 1F;



        /// <summary>
        /// ����ޣ�����|���ޣ�
        /// </summary>
        public string Mse_WcLimit = "+1|-1";


        /// <summary>
        /// 25���ֵ�����һ|����|...|���ƽ��|������
        /// </summary>
        public string Mse_Wc = "";
        /// <summary>
        /// 23����ֵ
        /// </summary>
        public string Mse_chrWcHz = "";
        /// <summary>
        /// 24���ƽ��ֵ
        /// </summary>
        public string Mse_chrWc = "";




        /// <summary>
        /// 28���ֵ
        /// </summary>
        public string AVR_DIF_ERROR { get; set; }


        /// <summary>
        /// 29���ص����ͣ�Ĭ��0���ǻ�׼�㣬1����׼�㡣
        /// </summary>
        public string CHR_BASE_LOAD_FLAG { get; set; }

        /// <summary>
        /// 30��׼����[1,N] 
        /// </summary>
        public string AVR_BASE_LOAD_NO { get; set; }



        /// <summary>
        /// 14B��ѹ����
        /// </summary>
        public string AVR_VOT_B_MULTIPLE { get; set; }

        /// <summary>
        /// 15C��ѹ����
        /// </summary>
        public string AVR_VOT_C_MULTIPLE { get; set; }

        /// <summary>
        /// 9B��������
        /// </summary>
        public string AVR_CUR_B_MULTIPLE { get; set; }

        /// <summary>
        /// 11C��������
        /// </summary>
        public string AVR_CUR_C_MULTIPLE { get; set; }
     
        /// <summary>
        /// ���0
        /// </summary>
        public string Mse_chrWc0 = "";
        /// <summary>
        /// ���1
        /// </summary>
        public string Mse_chrWc1 = "";
        /// <summary>
        /// ���2
        /// </summary>
        public string Mse_chrWc2 = "";
        /// <summary>
        /// ���3
        /// </summary>
        public string Mse_chrWc3 = "";
        /// <summary>
        /// ���4
        /// </summary>
        public string Mse_chrWc4 = "";
        /// <summary>
        /// ���5
        /// </summary>
        public string Mse_chrWc5 = "";
        /// <summary>
        /// ���6
        /// </summary>
        public string Mse_chrWc6 = "";
        /// <summary>
        /// ���7
        /// </summary>
        public string Mse_chrWc7 = "";
        /// <summary>
        /// ���8
        /// </summary>
        public string Mse_chrWc8 = "";
        /// <summary>
        /// ���9
        /// </summary>
        public string Mse_chrWc9 = "";

        /// <summary>
        /// ��ӡ��������-����|ƽ��ֵ|����ֵ|���1|���2
        /// </summary>
        public string PrintData
        {
            get
            {
                string value = "";
                value = Mse_Result;
                string[] Arr_Err = Mse_Wc.Split('|');
                if (Arr_Err.Length == 4)
                {
                    value += "|" + Arr_Err[2];
                    value += "|" + Arr_Err[3];
                    value += "|" + Arr_Err[0];
                    value += "|" + Arr_Err[1];
                }
                else
                {
                    value += "||||";
                }
                return value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm;

namespace CLDC_DataCore.Const
{
    /// <summary>
    /// 标准表常数查表
    /// 查表原则:
    /// 如果在非临界电压电流区域直接按常数表查询返回
    /// 如果在临界电压或临界电流区域，首先查询本次电压和上次电压是否相同，如果相同则返回上
    /// 次查询结果，如果不同则返回0，提示客户端需要发送指令给标准表读取标准表常数
    /// </summary>
    public class StdMeterConst
    {
        #region 311v2
        /// <summary>
        /// 第一维为电压电流，第二维为常数
        /// </summary>
        private static Dictionary<string, int> dicStdConstSheet = new Dictionary<string, int>();
        private static int[] arrU = new int[5] { 60, 100, 220, 380, 1000 };
        private static int[] arrI = new int[5] { 1, 5, 10, 50, 100 };
        #endregion

        #region 3115
        /// <summary>
        /// 第一维为电压电流，第二维为常数
        /// </summary>
        private static Dictionary<string, int> dicStd3115ConstSheet = new Dictionary<string, int>();
        private static CLDC_Comm.Enum.Cus_StdMeterVDangWei[] arr3115U = new CLDC_Comm.Enum.Cus_StdMeterVDangWei[4] { CLDC_Comm.Enum.Cus_StdMeterVDangWei.档位60V, 
            CLDC_Comm.Enum.Cus_StdMeterVDangWei.档位120V, 
            CLDC_Comm.Enum.Cus_StdMeterVDangWei.档位240V, 
            CLDC_Comm.Enum.Cus_StdMeterVDangWei.档位480V };

        private static CLDC_Comm.Enum.Cus_StdMeterIDangWei[] arr3115I = new CLDC_Comm.Enum.Cus_StdMeterIDangWei[13] { CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位001A, 
            CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位002A, 
            CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位005A, 
            CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位01A, 
            CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位02A, 
            CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位05A, 
            CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位1A, 
            CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位2A, 
            CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位5A, 
            CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位10A, 
            CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位20A, 
            CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位50A, 
            CLDC_Comm.Enum.Cus_StdMeterIDangWei.档位100A };
        #endregion

        /// <summary>
        /// 上一次查询的电压
        /// </summary>
        public static float m_LastSearchU = 0F;      
        /// <summary>
        /// 上一次查询的电流
        /// </summary>
        public static float m_LastSearchI = 0F;    
        /// <summary>
        /// 上一次查询标准表常数
        /// </summary>
        public static int m_StdMeterConst = 0;
        ///// <summary>
        ///// 上一次查询的标准表类型，内部
        ///// </summary>
        //private static int m_LastMeterType = 0;
        /// <summary>
        /// 构造
        /// </summary>
        static StdMeterConst()
        {
            #region 311
            int[] consts = new int[25]{
            (int)1.2*100000000,(int)2.4*10000000,(int)1.2*10000000,(int)2.4*1000000,(int)1.2*1000000,
            6*10000000,(int)1.2*10000000,6*1000000,(int)1.2*1000000,6*100000,
                3*10000000,6*1000000,3*1000000,6*100000,3*100000,
                (int)1.5*10000000,3*1000000,(int)1.5*1000000,3*100000,(int)1.5*100000,
                6*1000000,(int)1.2*1000000,6*100000,(int)1.2*100000,6*10000
            };
            string strKey = "";
            for (int i = 0; i < arrU.Length; i++)
            {
                for (int j = 0; j < arrI.Length; j++)
                {
                    strKey = string.Format("{0}{1}", arrU[i], arrI[j]);
                    dicStdConstSheet.Add(strKey, consts[i * 5 + j]);
                }
            }
            #endregion

            #region 3115
            int[] consts3115 = new int[] { 2 * 1000000000, 2 * 1000000000, 2 * 1000000000, 2 * 1000000000, 2 * 1000000000, 2 * 1000000000, 2 * 1000000000, (int)1.6 * 1000000000, (int)6.4 * 100000000, (int)3.2 * 100000000, (int)1.6 * 100000000, (int)6.4 * 10000000, (int)3.2 * 10000000 ,
            2 * 1000000000, 2 * 1000000000, 2 * 1000000000, 2 * 1000000000, 2 * 1000000000, 2 * 1000000000, (int)1.6 * 1000000000, 8 * 100000000, (int)3.2 * 100000000, (int)1.6 * 100000000, 8 * 10000000, (int)3.2 * 10000000, (int)1.6 * 10000000 ,
            2 * 1000000000, 2 * 1000000000, 2 * 1000000000, 2 * 1000000000, 2 * 1000000000, (int)1.6 * 1000000000, 8 * 100000000, 4 * 100000000, (int)1.6 * 100000000, 8 * 10000000, 4 * 10000000, (int)1.6 * 10000000, 8 * 1000000 ,
            2 * 1000000000, 2 * 1000000000, 2 * 1000000000, 2 * 1000000000, 2 * 1000000000, 8 * 100000000, 4 * 100000000, 2 * 100000000, 8 * 10000000, 4 * 10000000, 2 * 10000000, 8 * 1000000, 4 * 1000000 ,};
            string strKey3115 = "";
            for (int i = 0; i < arr3115U.Length; i++)
            {
                for (int j = 0; j < arr3115I.Length; j++)
                {
                    strKey3115 = string.Format("{0}{1}", arr3115U[i], arr3115I[j]);
                    dicStd3115ConstSheet.Add(strKey3115, consts3115[i * 13 + j]);
                }
            }
            #endregion
        }
        /// <summary>
        /// 查表311
        /// </summary>
        /// <param name="sngU"></param>
        /// <param name="sngI"></param>
        /// <returns></returns>
        public static int SearchStdMeterConst(float sngU, float sngI)
        {
            bool bFound = false;
            int meterconst = 0;
            //查询是否是临界点
            if (IsCriticalValue(sngU, sngI))
            {
                //如果是临界点，则查询本次是否和上一次的电压电流一样
                if (sngU == m_LastSearchU && sngI == m_LastSearchI && m_StdMeterConst!=0)
                    meterconst = m_StdMeterConst;
                return meterconst;
            }
            for (int i = 0; i < arrU.Length; i++)
            {
                if (sngU < arrU[i] * 1.2F)
                {
                    for (int j = 0; j < arrI.Length; j++)
                    {
                        if (sngI < arrI[j] * 1.2F)
                        {
                            string strKey = string.Format("{0}{1}", arrU[i], arrI[j]);
                            if (dicStdConstSheet.ContainsKey(strKey))
                            {
                                meterconst = dicStdConstSheet[strKey]; 
                                bFound = true;
                                break;
                            }
                            else
                            {
                                throw new Exception("the key is not found"); 
                            }
                        }
                    }
                }
                if (bFound) break;
            }
            return meterconst;
        }
        /// <summary>
        /// 查表3115
        /// </summary>
        /// <param name="sngU"></param>
        /// <param name="sngI"></param>
        /// <returns></returns>
        public static int SearchStdMeterConst(CLDC_Comm.Enum.Cus_StdMeterVDangWei sngU, CLDC_Comm.Enum.Cus_StdMeterIDangWei sngI)
        {
            int meterconst = 0;

            string strKey = string.Format("{0}{1}", sngU, sngI);
            if (dicStd3115ConstSheet.ContainsKey(strKey))
            {
                meterconst = dicStd3115ConstSheet[strKey];
            }
            else
            {
                throw new Exception("the key is not found");
            }

            return meterconst;
        }
        /// <summary>
        /// 首先检测电压是否临界
        /// </summary>
        /// <param name="sngU"></param>
        /// <param name="sngI"></param>
        /// <returns></returns>
        public static bool IsCriticalValue(float sngU, float sngI)
        {
            //首先检测电压是否临界
            bool isCritical = false;
            float tmp = 0;
            for (int i = 0; i < arrU.Length; i++)
            {
                 tmp=arrU[i] * 1.2F;
                if (sngU == tmp)
                {
                    isCritical = true;
                    break;
                }
            }
            if (isCritical) return true;
            //检测电流是否临界
            for (int i = 0; i < arrI.Length; i++)
            {
                tmp=arrI[i] * 1.2F;
                if (sngI * 1000000F == tmp * 1000000F)
                {
                    isCritical = true;
                    break;
                }
            }
            return isCritical;
        }
        

        
    }
}

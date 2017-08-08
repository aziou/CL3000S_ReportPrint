using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.Enum;
using CLDC_DataCore.Struct;
using CLDC_DataCore.Function;

namespace CLDC_DataCore.Const
{
    public class GlobalUnit
    {
        #region 定义
        
        public static string DRIVERFPULSES
        {
            get
            {
                return GetConfig(Variable.CTC_DRIVERFPULSES, "固定常数");
            }
        }
        private static bool enableReadStd = true;
        /// <summary>
        /// 允许读取标准表信息
        /// 在设置标准表参数的时候不允许读取标准表参数
        /// </summary>
        public static bool EnableReadStd
        {
            get { return enableReadStd; }
            set { enableReadStd = value; }
        }

        private static int? _dispatcherType = null;
        /// <summary>
        /// 调度类型：0 不带调度；1 调度控制
        /// </summary>
        public static int DispatcherType
        {
            get 
            {
                return _dispatcherType == null ? 0 : _dispatcherType.Value; 
            }
            set
            {
                if (_dispatcherType == null)
                {
                    _dispatcherType = value;
                }
                else
                {
                    throw new Exception("只能在Main函数写一次");
                }
            }
        }
        /// <summary>
        /// 调度控制：是否可以自动开始检定
        /// </summary>
        public static bool DispatcherCanAutoStart
        {
            get;
            set;
        }
        /// <summary>
        /// 总检定开始时间
        /// </summary>
        private static DateTime _CheckTimeStartSum;
        /// <summary>
        /// 总检定开始时间
        /// </summary>
        public static DateTime g_CheckTimeStartSum
        {
            get { return _CheckTimeStartSum; }
            set
            {
                _CheckTimeStartSum = value;
            }
        }

        /// <summary>
        /// 总检定结束时间
        /// </summary>
        private static DateTime _CheckTimeSumEnd;
        /// <summary>
        /// 总检定结束时间
        /// </summary>
        public static DateTime g_CheckTimeEndSum
        {
            get { return _CheckTimeSumEnd; }
            set
            {
                _CheckTimeSumEnd = value;
            }
        }

        /// <summary>
        /// 当前检定项目开始时间
        /// </summary>
        private static DateTime _CheckTimeCurrentStart;
        /// <summary>
        /// 当前检定项目开始时间
        /// </summary>
        public static DateTime g_CheckTimeCurrentStart
        {
            get { return _CheckTimeCurrentStart; }
            set
            {
                _CheckTimeCurrentStart = value;
            }
        }

        /// <summary>
        /// 当前检定项目结束时间
        /// </summary>
        private static DateTime _CheckTimeCurrentEnd;
        /// <summary>
        /// 当前检定项目结束时间
        /// </summary>
        public static DateTime g_CheckTimeCurrentEnd
        {
            get { return _CheckTimeCurrentEnd; }
            set
            {
                _CheckTimeCurrentEnd = value;
            }
        }
        /// <summary>
        /// 设置当前不合格表要检
        /// </summary>
        public static void SetBuHeGeYaoJian()
        {

        }
        /// <summary>
        /// 检定人员
        /// </summary>
        private static string _CheckUserName = "";
        /// <summary>
        /// 检定人员
        /// </summary>
        public static string g_CheckUserName
        {
            get { return _CheckUserName; }
            set
            {
                _CheckUserName = value;
            }
        }
        /// <summary>
        /// 检定员权限
        /// </summary>
        private static string _CheckUserLevel = "";
        /// <summary>
        /// 检定人员权限
        /// </summary>
        public static string g_CheckUserLevel
        {
            get { return _CheckUserLevel; }
            set
            {
                _CheckUserLevel = value;
            }
        }

        /// <summary>
        /// 当前检定任务类型
        /// </summary>
        private static int _CurTestType = 00;

        /// <summary>
        /// 获取设置当前检定任务类型
        /// //任务类型,0=电能误差,1=需量周期,2=日计时误差,3=计数,4=对标,5=预付费功能检定,6=耐压实验,7=多功能脉冲计数试验
        /// </summary>
        public static int g_CurTestType
        {
            get { return _CurTestType; }
            set
            {
                _CurTestType = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool _ReadingPara;
        /// <summary>
        /// 当前是否处于读取参数试验中
        /// </summary>
        public static bool ReadingPara
        {
            get
            {
                return _ReadingPara;
            }
            set
            {
                _ReadingPara = value;
            }
        }
        /// <summary>
        /// 当前台体的色标功能
        /// </summary>
        public static Cus_SeiBiaoType SeBiaoType
        {
            get
            {
                try
                {
                    return (Cus_SeiBiaoType)(System.Enum.Parse(typeof(Cus_SeiBiaoType), CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SEBIAO, "无")));
                }
                catch
                {
                    return Cus_SeiBiaoType.不支持;
                }
            }
        }

        /// <summary>
        /// 是否处在被控状态
        /// </summary>
        public static bool IsBeingControl
        {
            get
            {
                return CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_ISCONTROL, CLDC_DataCore.Const.Variable.CTG_CONTROLMODEL_CONTROL) == CLDC_DataCore.Const.Variable.CTG_CONTROLMODEL_CONTROL ? true : false;
            }
        }
        /// <summary>
        /// 正式数据库相对路径。本地Access
        /// </summary>
        public static string DBPathOfAccess = System.Windows.Forms.Application.StartupPath + "\\Database\\ClouMeterData.mdb";
        /// <summary>
        /// 临时数据库相对路径。本地Access
        /// </summary>
        public static string DBPathOfTempAccess = System.Windows.Forms.Application.StartupPath + "\\Database\\ClouMeterDataTmp.mdb";
        //ID修改锁
        private static object objUpdateActiveIDLock = new object();


        /// <summary>
        /// 电能表数据公共模型
        /// 客户端程序专用
        /// </summary>
        public static CLDC_DataCore.CusModel g_CUS = null;
        /// <summary>
        /// 只为升源，不判任何条件
        /// </summary>
        public static bool OnlyToPower = false;
        /// <summary>
        /// 当前载波配置
        /// </summary>
        public static CLDC_DataCore.Model.CarrierProtocol.CarrierProtocolInfo[] CarrierInfos=null;
        /// <summary>
        /// 载波当前表位
        /// </summary>
        public static int Carrier_Cur_BwIndex = 0;
        #endregion
        /// <summary>
        /// 获取一块表基本信息
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public static CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Meter(int Index)
        {
            if (g_CUS == null) return null;
            if (Index < 0 || Index > g_CUS.DnbData._Bws)
                return null;
            return g_CUS.DnbData.MeterGroup[Index];
        }

        public static int intYaoJianMeterNum
        {
            get
            {
                int intNum = 0;
                if (g_CUS == null)
                    return intNum;
                for (int i = 0; i < g_CUS.DnbData._Bws; i++)
                {
                    if (g_CUS.DnbData.MeterGroup[i].YaoJianYn)
                        intNum++;
                }
                return intNum;
            }
        }

        /// <summary>
        /// 获取当前检定状态
        /// </summary>
        public static Cus_CheckStaute CheckState
        {
            get { return g_CUS.DnbData.CheckState; }
        }
       
        /// <summary>
        /// 更新检定ActiveID
        /// </summary>
        public static void UpdateActiveID(int NewID)
        {
            lock (objUpdateActiveIDLock)
            {
                if (g_CUS != null)
                {
                    g_CUS.DnbData.ActiveItemID = NewID;
                }
            }
        }
        /// <summary>
        /// 第一块表有效位
        /// </summary>
        public static int FirstYaoJianMeter
        {
            get
            {
                if (g_CUS == null)
                    return -1;
                for (int i = 0; i < g_CUS.DnbData._Bws; i++)
                {
                    if (g_CUS.DnbData.MeterGroup[i].YaoJianYn)
                        return i;
                }
                return -1;
            }
        }
        public static string strYaoJianMeter
        {
            get
            {
                string defaultYaoJian = "000000000000" + "000000000000";
                string strTmp1 = "";
                string strTmp2 = "";
                if (g_CUS == null)
                    return defaultYaoJian;
                for (int i = 0; i < g_CUS.DnbData._Bws; i++)
                {
                    if (g_CUS.DnbData.MeterGroup[i].YaoJianYn)
                        strTmp1 = "1" + strTmp1;
                    else
                        strTmp1 = "0" + strTmp1;
                }
                strTmp2 = defaultYaoJian + strTmp1;
                strTmp2 = strTmp2.Substring(strTmp1.Length, defaultYaoJian.Length);
                return strTmp2;
            }
        }
        /// <summary>
        /// 获取整体表位结论
        /// </summary>
        public static bool MeterResult
        {
            get
            {
                bool bResult = true;
                if (g_CUS == null)
                    return bResult;
                for (int i = 0; i < g_CUS.DnbData._Bws; i++)
                {
                    if (g_CUS.DnbData.MeterGroup[i].YaoJianYn)
                        if (g_CUS.DnbData.MeterGroup[i].Mb_chrResult == "不合格")
                        {
                            bResult = false;
                            break;
                        }
                }                
                return bResult;
            }
        }

        #region 电表基本信息

        /// <summary>
        /// Imax(A)
        /// </summary>
        public static float Imax
        {
            get
            {
                try
                {
                    if (FirstYaoJianMeter == -1)
                    {
                        return 4F;
                    }
                    else
                    {
                        return CLDC_DataCore.Function.Number.GetCurrentByIb("imax", Meter(FirstYaoJianMeter).Mb_chrIb);

                        // return (Single)Comm.Function.Number.SplitKF(Meter(FirstYaoJianMeter).Mb_chrIb, false);
                    }
                }
                catch
                {
                    return 4F;
                }
            }
        }
        /// <summary>
        /// Ib(A)
        /// </summary>
        public static float Ib
        {
            get
            {
                try
                {
                    if (FirstYaoJianMeter == -1)
                    {
                        return 1F;
                    }
                    else
                    {
                        return CLDC_DataCore.Function.Number.GetCurrentByIb("ib", Meter(FirstYaoJianMeter).Mb_chrIb);
                        //return (Single)Comm.Function.Number.SplitKF(Meter(FirstYaoJianMeter).Mb_chrIb, true);
                    }
                }
                catch
                {
                    return 1F;
                }
            }
        }

        /// <summary>
        /// U(V)
        /// </summary>
        public static float U
        {
            get
            {
                try
                {
                    if (FirstYaoJianMeter == -1)
                    {
                        return 57.7F;
                    }
                    else
                    {
                        return float.Parse(Meter(FirstYaoJianMeter).Mb_chrUb);
                    }
                }
                catch
                {
                    return 57.7F;
                }
            }
        }
        /// <summary>
        /// 测量方式
        /// </summary>
        public static CLDC_Comm.Enum.Cus_Clfs Clfs
        {
            get
            {
                Model.DnbModel.DnbInfo.MeterBasicInfo meterinfo = Meter(FirstYaoJianMeter);
                if (meterinfo == null) return Cus_Clfs.三相四线;
                return (CLDC_Comm.Enum.Cus_Clfs)meterinfo.Mb_intClfs;
            }
            set
            {
                Model.DnbModel.DnbInfo.MeterBasicInfo meterinfo = Meter(FirstYaoJianMeter);
                if (meterinfo != null)
                {
                    meterinfo.Mb_intClfs = (int)value;
                }
            }
        }

        /// <summary>
        /// 是否经互感器
        /// </summary>
        public static bool HGQ
        {
            get
            {
                if (FirstYaoJianMeter >= 0)
                {
                    return Meter(FirstYaoJianMeter).Mb_BlnHgq;
                }
                return false;
            }
        }

        /// <summary>
        /// 是否经止逆器
        /// </summary>
        public static bool ZNQ
        {
            get
            {
                if (FirstYaoJianMeter >= 0)
                {
                    return Meter(FirstYaoJianMeter).Mb_BlnZnq;
                }
                return false;
            }
        }

        /// <summary>
        /// 频率
        /// </summary>
        public static float PL
        {
            get
            {
                if (FirstYaoJianMeter >= 0)
                {
                    return float.Parse(Meter(FirstYaoJianMeter).Mb_chrHz);
                }
                return 50F;
            }
        }

        /// <summary>
        /// 常数
        /// </summary>
        public static int MeterConst
        {
            get
            {
                if (FirstYaoJianMeter >= 0)
                {
                    return  Convert.ToInt32 (Meter(FirstYaoJianMeter).Mb_chrBcs);
                }
                return 1600;
            }
        }

        public static int TestErMeterConst = 2;  //检定点圈数

        #endregion

        #region 变量声明        

        /// <summary>
        /// 系统配置模型，公用
        /// </summary>
        public static CLDC_DataCore.SystemModel.SystemInfo g_SystemConfig = null;

        /// <summary>
        /// 日志队列
        /// </summary>
        public static CLDC_DataCore.Function.RunLog Log = null;
        /// <summary>
        /// 集控联机状态    zzg soinlove@126.com
        /// </summary>
        public static CLDC_Comm.Enum.Cus_NetState NetState = Cus_NetState.DisConnected;
        /// <summary>
        /// 本地设备联机状态
        /// </summary>
        public static CLDC_Comm.Enum.Cus_NetState EquipConnectedState = Cus_NetState.DisConnected;
        /// <summary>
        /// 帧日志队列
        /// </summary>
        public static RunLogFrame FrameLog = null;

        private static log4net.ILog logger = null;

        /// <summary>
        /// 日志记录组件，用来替代 日志队列 Log成员
        /// </summary>
        public static log4net.ILog Logger
        {
            get
            {
                if (logger == null)
                {
                    logger = log4net.LogManager.GetLogger("AppLog");
                }
                return logger;
            }
        }

        /// <summary>
        ///事件队列 
        /// </summary>
        public static VerifyMsgControl g_MsgControl = null;

        /// <summary>
        /// 数据队列
        /// </summary>
        public static VerifyMsgControl g_DataControl = null;
        /// <summary>
        /// 通讯数据队列
        /// </summary>
        public static VerifyMsgControl g_485DataControl = null;
        /// <summary>
        /// 实时数据队列
        /// </summary>
        public static RealTimeMsgControl g_RealTimeDataControl = null;

        /// <summary>
        /// 误差板数据队列
        /// </summary>
        public static ErrorCounterInfoContainer g_ErrorCounterInfoControl = null;

        /// <summary>
        /// 应用程序是否已经退出.用于结束线程
        /// </summary>
        public static bool ApplicationIsOver = false;
        /// <summary>
        /// 检定线程是否停止，用于按钮状态
        /// </summary>
        public static bool TestThreadIsRunning = false;

        /// <summary>
        /// While循环时线程休眠时间，单位：MS
        /// </summary>
        public static int g_ThreadWaitTime = 1;

        /// <summary>
        /// 台体硬件配置
        /// 标准表（0-3115,1-3112,2-311V2）功率源（0-309,1-303）精密时基源（0-191B）误差板（0-L，1-E，2-G，3-H）
        /// 默认“0000”：3115,309,191B，188L
        /// </summary>
        public static string DriverTypes = "0000";

        /// <summary>
        /// 标准表功率[0:有功功率1:无功功率:2:视在功率]
        /// </summary>
        public static float[] g_StrandMeterP = new float[3];
        /// <summary>
        /// A元、B元、C元
        /// </summary>
        public static float[] g_StrandMeterU = new float[3];
        /// <summary>
        /// A元、B元、C元
        /// </summary>
        public static float[] g_StrandMeterI = new float[3];

        //是否所有要检表相同
        // public static bool g_AllMeterIsSame = true;
        /// <summary>
        /// 检验员
        /// </summary>
        public static StUserInfo User_Jyy;
        /// <summary>
        /// 触摸屏的监听
        /// </summary>
        public static bool IsStartListen = true;

        /// <summary>
        /// 核验员
        /// </summary>
        public static StUserInfo User_Hyy;
        private static StProgrammingTip _Tip_Programming = new StProgrammingTip();
        /// <summary>
        /// 编程提示
        /// </summary>
        public static StProgrammingTip Tip_Programming
        {
            get
            {
                _Tip_Programming.isTip = GetConfig(CLDC_DataCore.Const.Variable.CTC_DGN_WRITEMETERALARM, "是") == "是";
                _Tip_Programming.isReTip = true;//TODO:这个配置到系统配置里，重复提示
                if (_Tip_Programming.isTip == true)
                {
                    _Tip_Programming.TotalTipNum++;
                }
                return _Tip_Programming;
            }
            set
            {
                SetConfig(Variable.CTC_DGN_WRITEMETERALARM, value.isTip ? "是" : "否");
            }

        }

        /// <summary>
        /// 从数据库获取方案，flase，XML方案
        /// </summary>
        public static bool Plan_FromMDB = false;
        /// <summary>
        /// true 载波模式,false 485模式
        /// </summary>
        public static Cus_CommunType g_CommunType = Cus_CommunType.通讯485;
        /// <summary>
        /// 
        /// </summary>
        public static StCarrierInfo CarrierInfo = new StCarrierInfo();

        #endregion

        #region 读取配置
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="setValue"></param>
        public static void SetConfig(string strKey, string setValue)
        {
            if (g_SystemConfig.SystemMode.EditValue(strKey, setValue))
            {
                g_SystemConfig.SystemMode.Save();
            }
        }
        /// <summary>
        /// 取配置[文本类型]
        /// </summary>
        /// <param name="strKey">主键</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public static string GetConfig(string strKey, string DefaultValue)
        {
            if (g_SystemConfig == null)
                return DefaultValue;
            string strTmp = g_SystemConfig.SystemMode.getValue(strKey);
            if (Function.Common.IsEmpty(strTmp))
                return DefaultValue;
            return strTmp;
        }

        /// <summary>
        /// 取配置[Int类型]
        /// </summary>
        /// <param name="strKey">主键</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public static int GetConfig(string strKey, int DefaultValue)
        {
            string strValue = GetConfig(strKey, DefaultValue.ToString());
            if (!Function.Number.IsNumeric(strValue))
                return DefaultValue;
            return int.Parse(strValue);
        }

        /// <summary>
        /// 取配置[Float类型]
        /// </summary>
        /// <param name="strKey">主键</param>
        /// <param name="DefaultValue">默认值</param>
        /// <returns></returns>
        public static float GetConfig(string strKey, float DefaultValue)
        {
            string strValue = GetConfig(strKey, DefaultValue.ToString());
            if (!Function.Number.IsNumeric(strValue))
                return DefaultValue;
            return float.Parse(strValue);
        }
        #endregion

        #region 系统信息
        /// <summary>
        /// 是否是演示版本
        /// </summary>
        public static bool IsDemo
        {
            get { return GetConfig(Const.Variable.CTC_ISDEMO, "演示模式") == "演示模式"; }
        }

        /// <summary>
        /// 是否是单相台
        /// </summary>
        public static bool IsDan
        {
            get
            {
                return GetConfig(Const.Variable.CTC_DESKTYPE, "单相台") == "单相台";
            }
        }
        #endregion

        /// <summary>
        /// 参变量分类
        /// </summary>
        /// <param name="str_ID">协议标识</param>
        /// <returns>1类,2类,3类</returns>
        public static int CheckStrIDType(string str_IDs)
        {
            int tp = 0;
            string str_ID = str_IDs.ToUpper();
            if (g_CUS.DnbData.MeterGroup[GlobalUnit.FirstYaoJianMeter].DgnProtocol.HaveProgrammingkey)
            {
                return 4;//有编程键
            }
            if (DicIDType.ContainsKey(str_ID))
            {
                tp = 1;
            }
            else if (str_ID.IndexOf("040501", 0) > 0)
            {
                tp = 1;
            }
            else if (str_ID.IndexOf("040502", 0) > 0)
            {
                tp = 1;
            }
            else if (str_ID == "04001301")
            {
                tp = 3;
            }
            else
            {
                tp = 2;
            }
            return tp;
        }
        /// <summary>
        /// 参变量分类字典。key 协议标识 value 1类2类3类
        /// </summary>
        private static Dictionary<string, int> DicIDType = new Dictionary<string, int> { 
        {"04000108",1},{"04000109",1},{"04000306",1},{"04000307",1},
        {"04000402",1},{"0400040E",1},{"04001001",1},{"04001002",1},
        {"040501XX",1},{"040502XX",1},{"040604FF",1},{"040605FF",1},
        };



        public static string ENCRYPTION_MACHINE_TYPE { get; set; }
        public static string ENCRYPTION_MACHINE_IP { get; set; }
        public static string ENCRYPTION_MACHINE_PORT { get; set; }
        public static string ENCRYPTION_MACHINE_PASSWORD { get; set; }
        public static string ENCRYPTION_MACHINE_OUTTIME { get; set; }

        /// <summary>
        /// 时段投切点
        /// </summary>
        public static string[] strSDTQ  =null ;
        /// <summary>
        /// 时段投切是否自动读取
        /// </summary>
        public static bool isAutoRead = false;

       
    }
}

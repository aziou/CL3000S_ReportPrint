using System;
using System.Text;

namespace CLDC_DataCore.Const
{
    /// <summary>
    ///  常量壭声明：C+标记符号+使用模块(全局用G)+_常量名
    /// C:Cus缩写，用作所有常量声明的第一个字母
    /// M:Mark缩写，用作标记常量声明
    /// T:Text缩写,用作文本内容标记声明
    /// 模块简写对照：
    /// SC:SystemConfig模块
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// Grid表格常态颜色
        /// </summary>
        public static System.Drawing.Color Color_Grid_Normal = System.Drawing.Color.FromArgb(250, 250, 250);

        /// <summary>
        /// Grid表格间隔行颜色
        /// </summary>
        public static System.Drawing.Color Color_Grid_Alter = System.Drawing.Color.FromArgb(235, 250, 235);

        /// <summary>
        /// 固定行（列）颜色
        /// </summary>
        public static System.Drawing.Color Color_Grid_Frone = System.Drawing.Color.FromArgb(225, 225, 225);
        /// <summary>
        ///不合格颜色
        /// </summary>
        public static System.Drawing.Color Color_Grid_BuHeGe = System.Drawing.Color.Red;



        /// <summary>
        /// 合格文本内容
        /// </summary>
        public const string CTG_HeGe = "合格";
        /// <summary>
        /// 未出结论前的默认显示
        /// </summary>
        public const string CTG_DEFAULTRESULT = "--";
        /// <summary>
        /// 不合格文本内容
        /// </summary>
        public const string CTG_BuHeGe = "不合格";

        /// <summary>
        /// 合格标记。
        /// </summary>
        public const string CMG_HeGe = "√";

        /// <summary>
        /// 不合格标志
        /// </summary>
        public const string CMG_BuHeGe = "×";
        /// <summary>
        /// 服务器断开连接消息提示文本
        /// </summary>
        public const string CTG_SERVERUNCONNECT = "服务器断开连接";
        /// <summary>
        /// 项目检定完毕提示文本
        /// </summary>
        public const string CTG_VERIFYOVER = "所有项目检定完毕";
        /// <summary>
        /// 启动网络服务成功文本
        /// </summary>
        public const string CTG_CONNECTSERVERSUCCESS = "启动网络服务成功";
        /// <summary>
        /// 主控文本标识
        /// </summary>
        public const string CTG_CONTROLMODEL_CONTROL = "主控";
        /// <summary>
        /// 被控文本标识
        /// </summary>
        public const string CTG_CONTROLMODEL_BECONTROL = "被控";
        /// <summary>
        /// 没有出误差默认值
        /// </summary>
        public const float WUCHA_INVIADE = -999F;
        #region 系统文档，配置文档，常量文档存放路径

        /// <summary>
        /// 系统配置XML文档路径
        /// </summary>
        public const string CONST_SYSTEMPATH = "\\System\\System.xml";
        /// <summary>
        /// 登陆用户配置XML文档路径
        /// </summary>
        public const string CONST_USERSPATH = "\\System\\Users.xml";

        /// <summary>
        /// 实验方法与依据XML文档路径
        /// </summary>
        public const string CONST_MethodBasis = "\\System\\MethodBasis.xml";
        /// <summary>
        /// 实验参数XML文档路径
        /// </summary>
        public const string CONST_TestSet = "\\System\\TestSet.xml";
        /// <summary>
        /// 硬件特殊配置XML文档路径
        /// </summary>
        public const string CONST_SPECIALCONFIG = "\\System\\SpecialConfig.xml";

        /// <summary>
        /// 系统字典配置XML文档路径
        /// </summary>
        public const string CONST_DICTIONARY = "\\Const\\ZiDian.xml";
        /// <summary>
        /// 功率因素角度配置XML文档路径
        /// </summary>
        public const string CONST_GLYSDICTIONARY = "\\Const\\GLYS.xml";
        /// <summary>
        /// 电流倍数字典表
        /// </summary>
        public const string CONST_XIBDICTIONARY = "\\Const\\xIb.xml";
        /// <summary>
        /// 多功能项目字典
        /// </summary>
        public const string CONST_DGNDICTIONARY = "\\Const\\DngConfig.xml";

        /// <summary>
        /// 智能表功能项目字典
        /// </summary>
        public const string CONST_FUNCTIONDICTIONARY = "\\Const\\FunctionConfig.xml";
        /// <summary>
        /// 费控功能项目字典
        /// </summary>
        public const string CONST_COSTCONTROLDICTIONARY = "\\Const\\CostControlConfig.xml";
        /// <summary>
        /// 事件记录项目字典
        /// </summary>
        public const string CONST_EVENTLOGDICTIONARY = "\\Const\\EventLogConfig.xml";
        /// <summary>
        /// 冻结项目字典
        /// </summary>
        public const string CONST_FREEZEDICTIONARY = "\\Const\\FreezeConfig.xml";



        /// <summary>
        /// 误差限字典文件
        /// </summary>
        public const string CONST_WCLIMIT = "\\Const\\WcLimit.Mdb";
        /// <summary>
        /// 多功能协议配置文件
        /// </summary>
        public const string CONST_DGNPROTOCOL = "\\Const\\DgnProtocol.xml";
        /// <summary>
        /// 谐波方案配置文档
        /// </summary>
        public const string CONST_XIEBO = "\\Const\\XieBo.xml";
        /// <summary>
        /// 载波方案配置文件
        /// </summary>
        public const string CONST_CARRIER = "\\Const\\CarrierProtocol.xml";
        /// <summary>
        /// 数据标识字典配置文件
        /// </summary>
        public const string CONST_DATAFLAG_DICT = "\\Const\\DataFlagDict.xml";
        /// <summary>
        /// 列显示项目字典
        /// </summary>
        public const string CONST_COLSVISIABLE = "\\Const\\ColsConfig.xml";

        /// <summary>
        /// 报文显示列
        /// </summary>
        public const string CONST_SHOW_BWCOL = "\\Const\\BWColsConfig.xml";

        /// <summary>
        /// 脉冲通道配置文件
        /// </summary>
        public const string CONST_PULSETYPE = @"/Tmp/Pulse.xml";
        /// <summary>
        /// ManageInfo.ini
        /// </summary>
        public const string CONST_MANAGERINI = "ManageInfo.ini";

        /// <summary>
        /// 
        /// </summary>
        public const string CONST_WAITUPDATE = "\\WAITUPDATE";

        #endregion

        /// <summary>
        /// 预先调试方案文件夹
        /// </summary>
        public const string CONST_FA_PREPARE_FOLDERNAME = "PrepareTest";
        /// <summary>
        /// 误差方案文件夹
        /// </summary>
        public const string CONST_FA_WC_FOLDERNAME = "WC";
        /// <summary>
        /// 预热方案文件夹
        /// </summary>
        public const string CONST_FA_YURE_FOLDERNAME = "Yure";
        /// <summary>
        /// 外观检查试验文件夹
        /// </summary>
        public const string CONST_FA_WGJC_FOLDERNAME = "WGJC";
        /// <summary>
        /// 潜动方案文件夹
        /// </summary>
        public const string CONST_FA_QIAND_FOLDERNAME = "QianDong";
        /// <summary>
        /// 启动方案文件夹
        /// </summary>
        public const string CONST_FA_QID_FOLDERNAME = "QiDong";
        /// <summary>
        /// 走字方案文件夹
        /// </summary>
        public const string CONST_FA_ZOUZI_FOLDERNAME = "ZouZi";
        /// <summary>
        /// 通讯协议检查试验
        /// </summary>
        public const string CONST_FA_CONNPROTOCOL_FOLDERNAME = "ConnProtocol";
        /// <summary>
        /// 多功能方案文件夹
        /// </summary>
        public const string CONST_FA_DGN_FOLDERNAME = "Dgn";
        /// <summary>
        /// 载波试验文件夹
        /// </summary>
        public const string CONST_FA_ZB_FOLDERNAME = "Carrier";
        /// <summary>
        /// 红外数据比对试验文件夹
        /// </summary>
        public const string CONST_FA_HW_FOLDERNAME = "Infrared";
        /// <summary>
        /// 误差一致性文件夹
        /// </summary>
        public const string CONST_FA_YZX_FOLDERNAME = "WcAccord";
        /// <summary>
        /// 特殊检定方案文件夹
        /// </summary>
        public const string CONST_FA_TS_FOLDERNAME = "TeSu";
        /// <summary>
        /// 功耗试验文件夹
        /// </summary>
        public const string CONST_FA_GONGHAO_FOLDERNAME = "GongHao";


        /// <summary>
        /// 费控功能测试文件夹
        /// </summary>
        public const string CONST_FA_FK_FOLDERNAME = "CostControl";

        /// <summary>
        /// 智能表功能测试文件夹
        /// </summary>
        public const string CONST_FA_GN_FOLDERNAME = "Function";


        /// <summary>
        /// 事件记录方案文件夹
        /// </summary>
        public const string CONST_FA_EVENTLOG_FOLDERNAME = "EventLog";


        /// <summary>
        /// 数据转发
        /// </summary>
        public const string CONST_FA_DATASEND_FOLDERNAME = "DATASEND";


        /// <summary>
        /// 冻结测试文件夹
        /// </summary>
        public const string CONST_FA_FZ_FOLDERNAME = "Freeze";

        /// <summary>
        /// 耐压方案文件夹
        /// </summary>
        public const string CONST_FA_INSULATION_FOLDERNAME = "Insulation";
        /// <summary>
        /// 负荷记录
        /// </summary>
        public const string CONST_FA_LOADRECORD_FOLDERNAME = "LoadRecord";
        /// <summary>
        /// 总方案文件夹
        /// </summary>
        public const string CONST_FA_GROUP_FOLDERNAME = "Group";
        /// <summary>
        /// Access方案数据库,带后缀"DataBase\\ClouConfig.mdb"
        /// </summary>
        public const string CONST_ACCESS_FANAME = "DataBase\\ClouConfig.mdb";
        /// <summary>
        /// 协议配置EXE文件名
        /// </summary>
        //public const string CONST_PROTOCOL_SETUP_NAME = "CLMeterProtocolSetup";
        public const string CONST_PROTOCOL_SETUP_NAME = "CLDC_MeterProtocolSetup";
        /// <summary>
        /// 电机延迟超时时间(单位：S)
        /// </summary>
        public const int MotorTimeOut = 15;

        #region 系统字典键名常量---服务器端配置
        /*
         说明：
         * 内容：系统配置Xml节点名常量
         */
        /// <summary>
        /// 客户端数量键
        /// </summary>
        public const string CTSC_CLIENTCOUNT = "CLIENTCOUNT";
        /// <summary>
        /// 服务器监听地址
        /// </summary>
        public const string CTN_SERVERIP = "SERVERIP";
        /// <summary>
        /// 服务器服务端口
        /// </summary>
        public const string CTN_SERVERPORT = "SERVERPORT";
        /// <summary>
        /// 服务器端点标识
        /// </summary>
        public const uint CUN_SERVERFLAG = 0x100;
        /// <summary>
        /// 响应超时时间,单位(秒)
        /// </summary>
        public const string CTN_RESPONSEDELAYTIME = "RESPONSEDELAYTIME";
        ///// <summary>
        ///// 跑马灯滚动速度
        ///// </summary>
        // public const string CTSC_MARQUEESPEED = "MARQUEESPEED";

        #endregion


        #region -----标准表信息-----
        /// <summary>
        /// 标准表名称
        /// </summary>
        public const string CTC_STDMETER_NAME = "STDMETER_NAME";

        /// <summary>
        /// 标准表型号
        /// </summary>
        public const string CTC_STDMETER_SIZE = "STDMETER_SIZE";

        /// <summary>
        /// 标准表编号
        /// </summary>
        public const string CTC_STDMETER_NO = "STDMETER_NO";

        /// <summary>
        /// 标准表测量范围-开始
        /// </summary>
        public const string CTC_STDMETERRANGE_START = "STDMETERRANGE_START";

        /// <summary>
        /// 标准表测量范围-结束
        /// </summary>
        public const string CTC_STDMETERRANGE_END = "STDMETERRANGE_END";
        /// <summary>
        /// 标准表不确定度
        /// </summary>
        public const string CTC_STDMETER_ERROR = "STDMETER_ERROR";
        /// <summary>
        ///   标准表证书编号
        /// </summary>
        public const string CTC_STDMETER_ASSNO = "STDMETER_ASSNO";

        /// <summary>
        /// 标准表过期时间
        /// </summary>
        public const string CTC_STDMETER_EXPERDATE = "STDMETER_EXPERDATE";
        #endregion

        #region -----装置信息-----
        /// <summary>
        /// 装置名称
        /// </summary>
        public const string CTC_EQUIPMENT_NAME = "EQUIPMENT_NAME";
        /// <summary>
        /// 装置型号
        /// </summary>
        public const string CTC_EQUIPMENT_SIZE = "EQUIPMENT_SIZE";
        /// <summary>
        /// 装置编号
        /// </summary>
        public const string CTC_EQUIPMENT_NO = "EQUIPMENT_NO";
        /// <summary>
        /// 装置测量范围-开始 
        /// </summary>
        public const string CTC_EQUIPMENTRANGE_START = "EQUIPMENTRANGE_START";
        /// <summary>
        /// 装置测量范围结束
        /// </summary>
        public const string CTC_EQUIPMENTRANGE_END = "EQUIPMENTRANGE_END";
        /// <summary>
        /// 装置不确定度
        /// </summary>
        public const string CTC_EQUIPMENT_ERROR = "EQUIPMENT_ERROR";
        /// <summary>
        /// 装置证书编号
        /// </summary>
        public const string CTC_EQUIPMENT_ASSNO = "EQUIPMENT_ASSNO";
        /// <summary>
        /// 装置有效期
        /// </summary>
        public const string CTC_EQUIPMENT_EXPERDATE = "EQUIPMENT_EXPERDATE";
        #endregion

        #region 客户端系统配置主键

        /// <summary>
        /// 是否是正式版本
        /// </summary>
        public const string CTC_ISDEMO = "ISDEMO";

        /// <summary>
        /// 客户端表位数量
        /// </summary>
        public const string CTC_BWCOUNT = "BWCOUNT";
        /// <summary>
        /// 是否允许修改检定数据
        /// </summary>
        public const string CTC_CHANGEDATA = "CHANGEDATA";

        /// <summary>
        /// 远程服务器IP
        /// </summary>
        public const string CTC_SERVERIP = "SERVERIP";

        /// <summary>
        /// 控制模式:0:被控,1:主控
        /// </summary>
        public const string CTC_ISCONTROL = "ISCONTROL";
        /// <summary>
        /// 台体类型,1-单相台,0-三相台
        /// </summary>
        public const string CTC_DESKTYPE = "DESKTYPE";
        /// <summary>
        /// 台体编号，与台体端点号必须一致
        /// </summary>
        public const string CTC_DESKNO = "DESKNO";
        /// <summary>
        /// 台体名称
        /// </summary>
        public const string CTC_DESKNAME = "DESKNAME";
        ///// <summary>
        ///// 客户端网络端点.端点号必须大于0小于65535
        ///// </summary>
        //public const string CTC_POINTFLAG = "POINTFLAG";
        /// <summary>
        /// 标准脉冲分频系数，如果是科陆标准表则为1
        /// </summary>
        public const string CTC_DRIVERF = "DRIVERF";
        /// <summary>
        /// 标准表常数，自动，固定
        /// </summary>
        public const string CTC_DRIVERFPULSES = "DRIVERFPULSES";
        /// <summary>
        /// SQL数据库服务器IP
        /// </summary>
        public const string CTC_SQL_SERVERIP = "SQL_SERVERIP";
        /// <summary>
        /// SQL数据库用户名
        /// </summary>
        public const string CTC_SQL_USERID = "SQL_USERID";
        /// <summary>
        /// SQL登录密码
        /// </summary>
        public const string CTC_SQL_PASSWORD = "SQL_PASSWORD";
        /// <summary>
        /// SQL数据库名
        /// </summary>
        public const string CTC_SQL_DATABASENAME = "SQL_DATABASENAME";
        /// <summary>
        /// 操作失败重试次数.应用于所有对台体或是被检表操作。误差检定次数不在此列
        /// </summary>
        public const string CTC_RETRY = "RETRY";

        /// <summary>
        /// 色标功能
        /// </summary>
        public const string CTC_SEBIAO = "Sebiao";

        /// <summary>
        /// 标准表读取时间间隔
        /// </summary>
        public const string CTC_STDMETERREADTIME = "CTC_STDMETERREADTIME";
        #region --------------实验方法与依据--------------

        #region -------实验方法-------
        /// <summary>
        /// 实验方法(TM)-时段投切实验方法
        /// </summary>
        public const string CTC_TM_TIMECUT = "TESTMETHOD_TIMECUT";
        /// <summary>
        /// 实验方法(TM)-无功电量存储器实验方法
        /// </summary>
        public const string CTC_TM_QPOWERM = "TESTMETHOD_QPOWERMEMORY";
        /// <summary>
        /// 实验方法(TM)-拉合闸实验方法
        /// </summary>
        public const string CTC_TM_PULLGATE = "TESTMETHOD_PULLGATE";
        /// <summary>
        /// 实验方法(TM)-GPS取时方式
        /// </summary>
        public const string CTC_TM_GPSGETT = "TESTMETHOD_GPSGETTIME";
        /// <summary>
        /// 实验方法(TM)-负荷开关实验方法
        /// </summary>
        public const string CTC_TM_LOADSWITCH = "TESTMETHOD_LOADSWITCH";
        /// <summary>
        /// 实验方法(TM)-计时功能实验方法
        /// </summary>
        public const string CTC_TM_TIMEFUNCTION = "TESTMETHOD_TIMEFUNCTION";
        /// <summary>
        /// 实验方法(TM)-零点5分广播校时时是否做实验 
        /// </summary>
        public const string CTC_TM_ISTEST05BC = "TESTMETHOD_ISTBROADCALIBRATE";
        /// <summary>
        /// 实验方法(TM)-是否过滤大误差 
        /// </summary>
        public const string CTC_TM_ISFILTERMACROV = "TESTMETHOD_ISFVALUE";
        /// <summary>
        /// 实验方法(TM)-误差是否判断次数 
        /// </summary>
        public const string CTC_TM_ISJUDGECOUNT = "TESTMETHOD_ISJCOUNTOFERROR";
        /// <summary>
        /// 实验方法(TM)-拉合闸实验后加电流是否测试已合闸
        /// </summary>
        public const string CTC_TM_ISTESTCLOSEC = "TESTMETHOD_ISTCCURRENT";
        /// <summary>
        /// 实验方法(TM)-拉合闸实验加电流后是否电量清零
        /// </summary>
        public const string CTC_TM_ISCLEAR0CP= "TESTMETHOD_ISCLEAR0POWER";
        /// <summary>
        /// 实验方法(TM)-冻结指令是否采用广播方式下发
        /// </summary>
        public const string CTC_TM_IS_GB_Address = "IS_GB_Address";
        /// <summary>
        /// 实验方法(TM)-做潜动实验时采集的脉冲数不大于多少
        /// </summary>
        public const string CTC_TM_ISLESSPULSEC = "TESTMETHOD_ISLPULSECOUNT";
        /// <summary>
        /// 实验方法(TM)-做起动实验时采集的脉冲数不小于多少
        /// </summary>
        public const string CTC_TM_ISMOREPULSEC = "TESTMETHOD_ISMPULSECOUNT";
        /// <summary>
        /// 实验方法(TM)-CL2036控制器控制方式，包括关闭和开放
        /// </summary>
        public const string CTC_TM_CL2036CONTROLCM = "TESTMETHOD_CL2036CCONTROLMODE";
        /// <summary>
        /// 实验方法(TM)-提供语音提示功能接口
        /// </summary>
        public const string CTC_TM_ISSUPPLYVOICE = "TESTMETHOD_ISSUPPLYV";
        /// <summary>
        /// 实验方法(TM)-剩余电量递减准确度，当前电价标识
        /// </summary>
        public const string CTC_TM_LEFTRIGHTPRICE = "TESTMETHOD_RIGHTPRICE";
        /// <summary>
        /// 实验方法(TM)-控制功能，报警金额1、2限值写方法
        /// </summary>
        public const string CTC_TM_WRITEWRINGPRICE = "TESTMETHOD_WRITEWRINGPRICE";
        #endregion

        #region -------实验依据-------
        /// <summary>
        /// 实验依据(TB)-功耗测试判断规程依据
        /// </summary>
        public const string CTC_TB_POWERUSETESTJ= "TESTBASIS_POWERUSETESTJUDGE";
        /// <summary>
        /// 实验依据(TB)-机械表实验检定规程依据
        /// </summary>
        public const string CTC_TB_EMETER = "TESTBASIS_MACHINEMETER";
        /// <summary>
        /// 实验依据(TB)-需量示值误差判定依据
        /// </summary>
        public const string CTC_TB_NEEDVALUEIF = "TESTBASIS_NEEDVALUEIF";
        #endregion

        #region-------启动和潜动-------
        ///<summary>
        /// 实验依据与方法(TSB)-起动时间计算
        /// </summary>
        public const string CTC_TSB_STARTTIME = "TESTSANDB_STARTTIME";
        ///<summary>
        /// 实验依据与方法(TSB)-是南网表，还是国网表
        /// </summary>
        public const string CTC_TSB_METERTYPES = "TESTSANDB_METERTYPES";
        #endregion

        #region -------其它-------
        /// <summary>
        /// 实验依据与方法 其它(TO)-用载波法代替485进行试验
        /// </summary>
        public const string CTC_TO_CWREPLACE485 = "TESTOTHER_CWREPLACE485";
        /// <summary>
        /// 实验依据与方法 其它(TO)-寄存器检查
        /// </summary>
        public const string CTC_TO_CHECKREGISTER = "TESTOTHER_CHECKREGISTER";

        #endregion

        #endregion

        #region --------------实验参数--------------
        ///// <summary>
        ///// 实验参数(TS)-特殊检定方案设置电压和电流是否使用百分比
        ///// </summary>
        //public const string CTC_TS_ISUSEPEROFVA="TESTSET_ISUSEPOFVA";
        ///// <summary>
        ///// 实验参数(TS)-抄表日清空需要量是否需要恢复当前时间
        ///// </summary>
        //public const string CTC_TS_ISNEEDRTIME = "TESTSET_ISNREVERTTATCLEAR";
        ///// <summary>
        ///// 实验参数(TS)-是否采用三相四线模拟三相三线输出
        ///// </summary>
        //public const string CTC_TS_ISATSSOUTPUT= "TESTSET_ISNREVERTTATCLEAR";        


        #endregion

        #region ------------检定标准设置------------
        /// <summary>
        /// 电子式电表检定规程
        /// </summary>
        public const string CTG_ELEC_GC = "ELEC_GC";
        /// <summary>
        /// 感应式电表检定规程
        /// </summary>
        public const string CTG_VICA_GC = "VICA_GC";
        #endregion

        #region ----------------误差检定相关设置-------------
        /// <summary>
        /// 每个误差点取几次误差参与计算
        /// </summary>
        public const string CTC_WC_TIMES_BASICERROR = "TIMES_BASICERROR";
        /// <summary>
        /// 标准偏差取几次误差参与计算
        /// </summary>
        public const string CTC_WC_TIMES_WINDAGE = "TIMES_WINDAGE";
        /// <summary>
        /// 每个点误差最大处理次数
        /// </summary>
        public const string CTC_WC_MAXTIMES = "WC_MAXTIMES";
        /// <summary>
        /// 每个点最大检定时间
        /// </summary>
        public const string CTC_WC_MAXSECONDS = "WC_MAXSECONDS";
        /// <summary>
        /// 跳差倍数判定
        /// </summary>
        public const string CTC_WC_JUMP = "WC_JUMP";
        /// <summary>
        /// IN电流
        /// </summary>
        public const string CTC_WC_IN = "WC_IN";
        /// <summary>
        /// 平均值保留小数位
        /// </summary>
        public const string CTC_WC_AVGPRECISION = "AVGPRECISION";
        #endregion

        #region ----------走字试验相关设置----------
        /// <summary>
        /// 走字同时做组合误差，是|否
        /// </summary>
        public const string CTC_ZZ_UNINERROR = "ZZ_UNINERROR";
        /// <summary>
        /// 走字试验项目
        /// [默认],不做任何排序，直接按方案项目顺序走字
        /// [总与分费率同时做],选择此项后先读取总的起码，分费率做完后再读取总的止码.当做组合误差时此项默认选择.
        /// [自动检定总时段内的所有分费率]。此选项适用于24小时走字，及固定费率与同步费率混合走字。
        /// </summary>
        public const string CTC_ZZ_SELECTITEM = "ZZ_SELECTITEM";

        #endregion


        #region 多功能检定配置
        /// <summary>
        /// 多功能应用层发送数据后最大等待时间
        /// </summary>
        public const string CTC_DGN_MAXWAITDATABACKTIME = "MAXWAITDATABACKTIME";
        /// <summary>
        /// 多功能检定源稳定操作时间
        /// </summary>
        public const String CTC_DGN_POWERON_ATTERTIME = "POWERON_ATTERTIME";
        /// <summary>
        /// 当要对表进行写操作时发出提示
        /// </summary>
        public const string CTC_DGN_WRITEMETERALARM = "WRITEMETERALARM";
        /// <summary>
        /// 日计时误差检定类型:快速模式|标准模式
        /// </summary>
        public const string CTC_DGN_RJSVERIFYTYPE = "RJSVERIFYTYPE";

        /// <summary>
        /// 
        /// </summary>
        public const string CTC_DGN_READDATAFROMRS485 = "READDATAFROMRS485";
        #endregion


        #region----------其它项目设置----------
        /// <summary>
        /// 是否显示检定项目的描述！值为"是"、"否"
        /// </summary>
        public const string CTC_DISPLAY_CHECKDESCRIPTION="DISPLAYCHECKDESCRIPTION";
        /// <summary>
        /// 是否自动检测表位有没有挂表
        /// </summary>
        public const string CTC_AUTO_ISHAVECHECKMETER = "ISHAVEMETER";

        /// <summary>
        /// 是否校验通信地址
        /// </summary>
        public const string CTC_CHECK_COMM = "ISCHECKCOMMUNICATIONADDRESS";
        /// <summary>
        /// 截取资产编号后几位的长度
        /// </summary>
        public const string CTC_ADRRESS_LENGTH = "COMMUNICATIONADDRESSLENGTH";
        /// <summary>
        /// 是否根据制造厂家和表型号加载协议！值是“是”、“否”
        /// </summary>
        public const string CTC_OTHER_SELECTPROTOCOLFROMMETERFACTROY = "SELECTPROTOCOLFROMMETERFACTROY";
        /// <summary>
        /// 台体硬件参数设置后等待时间
        /// </summary>
        public const string CTC_OTHER_EQUIPSET_WAITTIME = "EQUIPSET_WAITTIME";
        /// <summary>
        /// 检定时显示控制灯
        /// </summary>
        public const string CTC_OTHER_SHOWRESULTLIGHT = "SHOWRESULTLIGHT";
        /// <summary>
        /// 2038发送数据帧等待时间倍数[手工配置]
        /// 如原来的等待时间为1000MS,此值设置为2,则等待时间为1000*2MS
        /// 此参数应用于发送等待返回时间及单字节时间间隔
        /// </summary>
        public const string CTC_OTHER_WAITTIME_SENDFRAME_2036 = "SENDFRAME_2036";
        /// <summary>
        /// 设置脉冲通道后等待时间[手工配置]
        /// </summary>
        public const string CTC_OTHER_WAITTIME_SELECTPULSECHANNEL = "WAITTIME_SELECTPULSECHANNEL";

        /// <summary>
        /// 设置功能参数后等待时间[手工配置]
        /// </summary>
        public const string CTC_OTHER_WAITTIME_SETTESTPARA = "WAITTIME_SETTESTPARA";
        /// <summary>
        /// 普通试验升源后等待时间[手工配置]
        /// </summary>
        public const string CTC_OTHER_WAITTIME_POWERON = "WAITTIME_POWERON";
        /// <summary>
        /// 启动检定后等待时间[手工配置]
        /// </summary>
        public const string CTC_OTHER_WAITTIME_STARTTASK = "WAITTIME_STARTTASK";

        /// <summary>
        /// 设置误差板参数后等待时间[手工配置]
        /// </summary>
        public const string CTC_OTHER_WAITTIME_SET188G = "WAITTIME_SET188G";
        /// <summary>
        /// 读取表数据等待时间
        /// </summary>
        public const string CTC_OTHER_WAITTIME_READDATA = "WAITTIME_READDATA";

        /// <summary>
        /// 确定源已经成功等待时间
        /// </summary>
        public const string CTC_OTHER_WAITTIME_POWERSTATE = "WAITTIME_POWERSTATE";
        /// <summary>
        /// 数据管理程序路径
        /// </summary>
        public const string CTC_OTHER_DATAMANAGEEXEPATH = "DATAMANAGEEXEPATH";
        /// <summary>
        /// 数据管理程序进程名
        /// </summary>
        public const string CTC_OTHER_DATAMANAGEPROCESSNAME = "DATAMANAGEPROCESSNAME";
        /// <summary>
        /// 协议配置程序路径
        /// </summary>
        public const string CTC_OTHER_PROTOCOLEXENAME = "PROTOCOLEXENAME";
        /// <summary>
        /// 协议配置程序进程名
        /// </summary>
        public const string CTC_OTHER_PROTOCOLPROCESS = "PROTOCOLPROCESS";
        /// <summary>
        /// 证书编号前缀
        /// </summary>
        public const string CTC_OTHER_PREFIXOFCERTIFICATEN = "PREFIXOFCERTIFICATENUMBER";//certificate
        #endregion


        #region -------------------接口相关-----------------------------------

        /// <summary>
        /// 接口条码解析所需要调用的EXE程序
        /// </summary>
        public const string CTC_MIS_EXENAME = "MIS_EXENAME";
        /// <summary>
        /// 接口条码解析所需要调用的EXE程序进程名
        /// </summary>
        public const string CTC_MIS_EXEPROCESSNAME = "MIS_EXEPROCESSNAME";
        /// <summary>
        /// 接口条码解析所需要调用的DLL组件
        /// </summary>
        public const string CTC_INTERFACE_DllNAME = "DLLNAME";
        /// <summary>
        /// 接口条码解析锁需要调用的DLL组件类
        /// </summary>
        public const string CTC_INTERFACE_DLLCLASS = "DLLCLASS";

        /// <summary>
        /// mis连接数据源
        /// </summary>
        public const string CTC_MIS_DATASOURCE = "MIS_DATASOURCE";
        /// <summary>
        /// mis登陆用户名
        /// </summary>
        public const string CTC_MIS_USERID = "MIS_USERID";
        /// <summary>
        /// mis登陆密码
        /// </summary>
        public const string CTC_MIS_PASSWORD = "MIS_PASSWORD";
        /// <summary>
        /// 是否上传实时数据
        /// </summary>
        public const string CTC_MIS_NOWDATA = "MIS_NOWDATA";
        /// <summary>
        /// webservice地址
        /// </summary>
        public const string CTC_MIS_WEBSERVICE_URL = "MIS_WEBSERVICE_URL";
        /// <summary>
        /// MIS接口类型
        /// </summary>
        public const string CTC_MIS_INTERFACETYPE = "MISINTERFACETYPE";
        /// <summary>
        /// 读取参数后依据什么编号下载信息
        /// </summary>
        public const string CTC_BasisDownInfo = "BasisDownInfo";

        #endregion

        #endregion


        #region---------------载波检定配置------------------
        ///// <summary>
        ///// 载波协议类型
        ///// </summary>
        //public const string CTC_ZB_PROCOTOCOLTYPE = "PROCOTOCOLTYPE";

        /// <summary>
        /// 载波模块类型
        /// </summary>
        public const string CTC_ZB_MODELTYPE = "MODELTYPE";
        /// <summary>
        /// 通讯方式
        /// </summary>
        public const string CTC_ZB_COMMUNICATIONTYPE = "COMMUNICATIONTYPE";

        /// <summary>
        /// 载波抄表器类型
        /// </summary>
        public const string CTC_ZB_READERTYPE = "READERTYPE";

        /// <summary>
        /// 载波抄表器波特率
        /// </summary>
        public const string CTC_ZB_READERBAUDRATE = "READERBAUDRATE";

        ///// <summary>
        ///// 串口号
        ///// </summary>
        //public const string CTC_ZB_COMPORT = "COMPORT";

        /// <summary>
        /// 通讯参数
        /// </summary>
        public const string CTC_ZB_PARAMETER = "PARAMETER";

        /// <summary>
        /// 通道号
        /// </summary>
        public const string CTC_ZB_CHANNELNO = "CHANNELNO";

        /// <summary>
        /// 模块
        /// </summary>
        public const string CTC_ZB_MODULE = "MODULE";

        /// <summary>
        /// 相位
        /// </summary>
        public const string CTC_ZB_PHASE = "PHASE";

        /// <summary>
        /// 引脚
        /// </summary>
        public const string CTC_ZB_PIN = "PIN";
        #endregion

        #region ----------------加密机相关-----------------
        /// <summary>
        /// 加密机默认类型
        /// </summary>
        public const string CTC_ENCRYPTION_DEFAULTTYPE = "ENCRYPTIONDEFAULTTYPE";

        /// <summary>
        /// 加密机IP
        /// </summary>
        public const string CTC_ENCRYPTION_MACHINEIP = "ENCRYPTIONMACHINEIP";

        /// <summary>
        /// 加密机端口
        /// </summary>
        public const string CTC_ENCRYPTION_MACHINEPORT = "ENCRYPTIONMACHINEPORT";

        /// <summary>
        /// 加密机USBKEY
        /// </summary>
        public const string CTC_ENCRYPTION_MACHINEUSBKEY = "ENCRYPTIONMACHINEUSBKEY";

        /// <summary>
        /// 是否进行密码机服务器连接
        /// </summary>
        public const string CTC_ENCRYPTION_MACHINEISAUTOLINK = "ENCRYPTIONMACHINEISAUTOLINK";

        /// <summary>
        /// 被检表的认证状态
        /// </summary>
        public const string CTC_ENCRYPTION_STATUS = "ENCRYPTIONSTATUS";

        /// <summary>
        /// 加密机应用层发送数据后最大等待时间
        /// </summary>
        public const string CTC_ENCRYPTION_MAXWAITDATABACKTIME = "ENCRYPTIONMAXWAITDATABACKTIME";

        /// <summary>
        /// 连接字符串
        /// </summary>
        public const string CONNECTSTRING = "CONNECTSTRING";


        /// <summary>
        /// 加密机配置文件
        /// </summary>
        public const string CONST_ENCRYPTION_INI = "\\Encryption\\Encryption.ini";
        /// <summary>
        /// 加密机配置
        /// </summary>
        public const string CONST_ENCRYPTION_SECTION = "EncryptionType";
        /// <summary>
        /// 加密机配置09 13
        /// </summary>
        public const string CONST_ENCRYPTION_NEWOLD = "EncryptionNewOld";

        #endregion
        
    }
}

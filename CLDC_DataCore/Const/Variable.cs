using System;
using System.Text;

namespace CLDC_DataCore.Const
{
    /// <summary>
    ///  ������������C+��Ƿ���+ʹ��ģ��(ȫ����G)+_������
    /// C:Cus��д���������г��������ĵ�һ����ĸ
    /// M:Mark��д��������ǳ�������
    /// T:Text��д,�����ı����ݱ������
    /// ģ���д���գ�
    /// SC:SystemConfigģ��
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// Grid���̬��ɫ
        /// </summary>
        public static System.Drawing.Color Color_Grid_Normal = System.Drawing.Color.FromArgb(250, 250, 250);

        /// <summary>
        /// Grid���������ɫ
        /// </summary>
        public static System.Drawing.Color Color_Grid_Alter = System.Drawing.Color.FromArgb(235, 250, 235);

        /// <summary>
        /// �̶��У��У���ɫ
        /// </summary>
        public static System.Drawing.Color Color_Grid_Frone = System.Drawing.Color.FromArgb(225, 225, 225);
        /// <summary>
        ///���ϸ���ɫ
        /// </summary>
        public static System.Drawing.Color Color_Grid_BuHeGe = System.Drawing.Color.Red;



        /// <summary>
        /// �ϸ��ı�����
        /// </summary>
        public const string CTG_HeGe = "�ϸ�";
        /// <summary>
        /// δ������ǰ��Ĭ����ʾ
        /// </summary>
        public const string CTG_DEFAULTRESULT = "--";
        /// <summary>
        /// ���ϸ��ı�����
        /// </summary>
        public const string CTG_BuHeGe = "���ϸ�";

        /// <summary>
        /// �ϸ��ǡ�
        /// </summary>
        public const string CMG_HeGe = "��";

        /// <summary>
        /// ���ϸ��־
        /// </summary>
        public const string CMG_BuHeGe = "��";
        /// <summary>
        /// �������Ͽ�������Ϣ��ʾ�ı�
        /// </summary>
        public const string CTG_SERVERUNCONNECT = "�������Ͽ�����";
        /// <summary>
        /// ��Ŀ�춨�����ʾ�ı�
        /// </summary>
        public const string CTG_VERIFYOVER = "������Ŀ�춨���";
        /// <summary>
        /// �����������ɹ��ı�
        /// </summary>
        public const string CTG_CONNECTSERVERSUCCESS = "�����������ɹ�";
        /// <summary>
        /// �����ı���ʶ
        /// </summary>
        public const string CTG_CONTROLMODEL_CONTROL = "����";
        /// <summary>
        /// �����ı���ʶ
        /// </summary>
        public const string CTG_CONTROLMODEL_BECONTROL = "����";
        /// <summary>
        /// û�г����Ĭ��ֵ
        /// </summary>
        public const float WUCHA_INVIADE = -999F;
        #region ϵͳ�ĵ��������ĵ��������ĵ����·��

        /// <summary>
        /// ϵͳ����XML�ĵ�·��
        /// </summary>
        public const string CONST_SYSTEMPATH = "\\System\\System.xml";
        /// <summary>
        /// ��½�û�����XML�ĵ�·��
        /// </summary>
        public const string CONST_USERSPATH = "\\System\\Users.xml";

        /// <summary>
        /// ʵ�鷽��������XML�ĵ�·��
        /// </summary>
        public const string CONST_MethodBasis = "\\System\\MethodBasis.xml";
        /// <summary>
        /// ʵ�����XML�ĵ�·��
        /// </summary>
        public const string CONST_TestSet = "\\System\\TestSet.xml";
        /// <summary>
        /// Ӳ����������XML�ĵ�·��
        /// </summary>
        public const string CONST_SPECIALCONFIG = "\\System\\SpecialConfig.xml";

        /// <summary>
        /// ϵͳ�ֵ�����XML�ĵ�·��
        /// </summary>
        public const string CONST_DICTIONARY = "\\Const\\ZiDian.xml";
        /// <summary>
        /// �������ؽǶ�����XML�ĵ�·��
        /// </summary>
        public const string CONST_GLYSDICTIONARY = "\\Const\\GLYS.xml";
        /// <summary>
        /// ���������ֵ��
        /// </summary>
        public const string CONST_XIBDICTIONARY = "\\Const\\xIb.xml";
        /// <summary>
        /// �๦����Ŀ�ֵ�
        /// </summary>
        public const string CONST_DGNDICTIONARY = "\\Const\\DngConfig.xml";

        /// <summary>
        /// ���ܱ�����Ŀ�ֵ�
        /// </summary>
        public const string CONST_FUNCTIONDICTIONARY = "\\Const\\FunctionConfig.xml";
        /// <summary>
        /// �ѿع�����Ŀ�ֵ�
        /// </summary>
        public const string CONST_COSTCONTROLDICTIONARY = "\\Const\\CostControlConfig.xml";
        /// <summary>
        /// �¼���¼��Ŀ�ֵ�
        /// </summary>
        public const string CONST_EVENTLOGDICTIONARY = "\\Const\\EventLogConfig.xml";
        /// <summary>
        /// ������Ŀ�ֵ�
        /// </summary>
        public const string CONST_FREEZEDICTIONARY = "\\Const\\FreezeConfig.xml";



        /// <summary>
        /// ������ֵ��ļ�
        /// </summary>
        public const string CONST_WCLIMIT = "\\Const\\WcLimit.Mdb";
        /// <summary>
        /// �๦��Э�������ļ�
        /// </summary>
        public const string CONST_DGNPROTOCOL = "\\Const\\DgnProtocol.xml";
        /// <summary>
        /// г�����������ĵ�
        /// </summary>
        public const string CONST_XIEBO = "\\Const\\XieBo.xml";
        /// <summary>
        /// �ز����������ļ�
        /// </summary>
        public const string CONST_CARRIER = "\\Const\\CarrierProtocol.xml";
        /// <summary>
        /// ���ݱ�ʶ�ֵ������ļ�
        /// </summary>
        public const string CONST_DATAFLAG_DICT = "\\Const\\DataFlagDict.xml";
        /// <summary>
        /// ����ʾ��Ŀ�ֵ�
        /// </summary>
        public const string CONST_COLSVISIABLE = "\\Const\\ColsConfig.xml";

        /// <summary>
        /// ������ʾ��
        /// </summary>
        public const string CONST_SHOW_BWCOL = "\\Const\\BWColsConfig.xml";

        /// <summary>
        /// ����ͨ�������ļ�
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
        /// Ԥ�ȵ��Է����ļ���
        /// </summary>
        public const string CONST_FA_PREPARE_FOLDERNAME = "PrepareTest";
        /// <summary>
        /// �����ļ���
        /// </summary>
        public const string CONST_FA_WC_FOLDERNAME = "WC";
        /// <summary>
        /// Ԥ�ȷ����ļ���
        /// </summary>
        public const string CONST_FA_YURE_FOLDERNAME = "Yure";
        /// <summary>
        /// ��ۼ�������ļ���
        /// </summary>
        public const string CONST_FA_WGJC_FOLDERNAME = "WGJC";
        /// <summary>
        /// Ǳ�������ļ���
        /// </summary>
        public const string CONST_FA_QIAND_FOLDERNAME = "QianDong";
        /// <summary>
        /// ���������ļ���
        /// </summary>
        public const string CONST_FA_QID_FOLDERNAME = "QiDong";
        /// <summary>
        /// ���ַ����ļ���
        /// </summary>
        public const string CONST_FA_ZOUZI_FOLDERNAME = "ZouZi";
        /// <summary>
        /// ͨѶЭ��������
        /// </summary>
        public const string CONST_FA_CONNPROTOCOL_FOLDERNAME = "ConnProtocol";
        /// <summary>
        /// �๦�ܷ����ļ���
        /// </summary>
        public const string CONST_FA_DGN_FOLDERNAME = "Dgn";
        /// <summary>
        /// �ز������ļ���
        /// </summary>
        public const string CONST_FA_ZB_FOLDERNAME = "Carrier";
        /// <summary>
        /// �������ݱȶ������ļ���
        /// </summary>
        public const string CONST_FA_HW_FOLDERNAME = "Infrared";
        /// <summary>
        /// ���һ�����ļ���
        /// </summary>
        public const string CONST_FA_YZX_FOLDERNAME = "WcAccord";
        /// <summary>
        /// ����춨�����ļ���
        /// </summary>
        public const string CONST_FA_TS_FOLDERNAME = "TeSu";
        /// <summary>
        /// ���������ļ���
        /// </summary>
        public const string CONST_FA_GONGHAO_FOLDERNAME = "GongHao";


        /// <summary>
        /// �ѿع��ܲ����ļ���
        /// </summary>
        public const string CONST_FA_FK_FOLDERNAME = "CostControl";

        /// <summary>
        /// ���ܱ��ܲ����ļ���
        /// </summary>
        public const string CONST_FA_GN_FOLDERNAME = "Function";


        /// <summary>
        /// �¼���¼�����ļ���
        /// </summary>
        public const string CONST_FA_EVENTLOG_FOLDERNAME = "EventLog";


        /// <summary>
        /// ����ת��
        /// </summary>
        public const string CONST_FA_DATASEND_FOLDERNAME = "DATASEND";


        /// <summary>
        /// ��������ļ���
        /// </summary>
        public const string CONST_FA_FZ_FOLDERNAME = "Freeze";

        /// <summary>
        /// ��ѹ�����ļ���
        /// </summary>
        public const string CONST_FA_INSULATION_FOLDERNAME = "Insulation";
        /// <summary>
        /// ���ɼ�¼
        /// </summary>
        public const string CONST_FA_LOADRECORD_FOLDERNAME = "LoadRecord";
        /// <summary>
        /// �ܷ����ļ���
        /// </summary>
        public const string CONST_FA_GROUP_FOLDERNAME = "Group";
        /// <summary>
        /// Access�������ݿ�,����׺"DataBase\\ClouConfig.mdb"
        /// </summary>
        public const string CONST_ACCESS_FANAME = "DataBase\\ClouConfig.mdb";
        /// <summary>
        /// Э������EXE�ļ���
        /// </summary>
        //public const string CONST_PROTOCOL_SETUP_NAME = "CLMeterProtocolSetup";
        public const string CONST_PROTOCOL_SETUP_NAME = "CLDC_MeterProtocolSetup";
        /// <summary>
        /// ����ӳٳ�ʱʱ��(��λ��S)
        /// </summary>
        public const int MotorTimeOut = 15;

        #region ϵͳ�ֵ��������---������������
        /*
         ˵����
         * ���ݣ�ϵͳ����Xml�ڵ�������
         */
        /// <summary>
        /// �ͻ���������
        /// </summary>
        public const string CTSC_CLIENTCOUNT = "CLIENTCOUNT";
        /// <summary>
        /// ������������ַ
        /// </summary>
        public const string CTN_SERVERIP = "SERVERIP";
        /// <summary>
        /// ����������˿�
        /// </summary>
        public const string CTN_SERVERPORT = "SERVERPORT";
        /// <summary>
        /// �������˵��ʶ
        /// </summary>
        public const uint CUN_SERVERFLAG = 0x100;
        /// <summary>
        /// ��Ӧ��ʱʱ��,��λ(��)
        /// </summary>
        public const string CTN_RESPONSEDELAYTIME = "RESPONSEDELAYTIME";
        ///// <summary>
        ///// ����ƹ����ٶ�
        ///// </summary>
        // public const string CTSC_MARQUEESPEED = "MARQUEESPEED";

        #endregion


        #region -----��׼����Ϣ-----
        /// <summary>
        /// ��׼������
        /// </summary>
        public const string CTC_STDMETER_NAME = "STDMETER_NAME";

        /// <summary>
        /// ��׼���ͺ�
        /// </summary>
        public const string CTC_STDMETER_SIZE = "STDMETER_SIZE";

        /// <summary>
        /// ��׼����
        /// </summary>
        public const string CTC_STDMETER_NO = "STDMETER_NO";

        /// <summary>
        /// ��׼�������Χ-��ʼ
        /// </summary>
        public const string CTC_STDMETERRANGE_START = "STDMETERRANGE_START";

        /// <summary>
        /// ��׼�������Χ-����
        /// </summary>
        public const string CTC_STDMETERRANGE_END = "STDMETERRANGE_END";
        /// <summary>
        /// ��׼��ȷ����
        /// </summary>
        public const string CTC_STDMETER_ERROR = "STDMETER_ERROR";
        /// <summary>
        ///   ��׼��֤����
        /// </summary>
        public const string CTC_STDMETER_ASSNO = "STDMETER_ASSNO";

        /// <summary>
        /// ��׼�����ʱ��
        /// </summary>
        public const string CTC_STDMETER_EXPERDATE = "STDMETER_EXPERDATE";
        #endregion

        #region -----װ����Ϣ-----
        /// <summary>
        /// װ������
        /// </summary>
        public const string CTC_EQUIPMENT_NAME = "EQUIPMENT_NAME";
        /// <summary>
        /// װ���ͺ�
        /// </summary>
        public const string CTC_EQUIPMENT_SIZE = "EQUIPMENT_SIZE";
        /// <summary>
        /// װ�ñ��
        /// </summary>
        public const string CTC_EQUIPMENT_NO = "EQUIPMENT_NO";
        /// <summary>
        /// װ�ò�����Χ-��ʼ 
        /// </summary>
        public const string CTC_EQUIPMENTRANGE_START = "EQUIPMENTRANGE_START";
        /// <summary>
        /// װ�ò�����Χ����
        /// </summary>
        public const string CTC_EQUIPMENTRANGE_END = "EQUIPMENTRANGE_END";
        /// <summary>
        /// װ�ò�ȷ����
        /// </summary>
        public const string CTC_EQUIPMENT_ERROR = "EQUIPMENT_ERROR";
        /// <summary>
        /// װ��֤����
        /// </summary>
        public const string CTC_EQUIPMENT_ASSNO = "EQUIPMENT_ASSNO";
        /// <summary>
        /// װ����Ч��
        /// </summary>
        public const string CTC_EQUIPMENT_EXPERDATE = "EQUIPMENT_EXPERDATE";
        #endregion

        #region �ͻ���ϵͳ��������

        /// <summary>
        /// �Ƿ�����ʽ�汾
        /// </summary>
        public const string CTC_ISDEMO = "ISDEMO";

        /// <summary>
        /// �ͻ��˱�λ����
        /// </summary>
        public const string CTC_BWCOUNT = "BWCOUNT";
        /// <summary>
        /// �Ƿ������޸ļ춨����
        /// </summary>
        public const string CTC_CHANGEDATA = "CHANGEDATA";

        /// <summary>
        /// Զ�̷�����IP
        /// </summary>
        public const string CTC_SERVERIP = "SERVERIP";

        /// <summary>
        /// ����ģʽ:0:����,1:����
        /// </summary>
        public const string CTC_ISCONTROL = "ISCONTROL";
        /// <summary>
        /// ̨������,1-����̨,0-����̨
        /// </summary>
        public const string CTC_DESKTYPE = "DESKTYPE";
        /// <summary>
        /// ̨���ţ���̨��˵�ű���һ��
        /// </summary>
        public const string CTC_DESKNO = "DESKNO";
        /// <summary>
        /// ̨������
        /// </summary>
        public const string CTC_DESKNAME = "DESKNAME";
        ///// <summary>
        ///// �ͻ�������˵�.�˵�ű������0С��65535
        ///// </summary>
        //public const string CTC_POINTFLAG = "POINTFLAG";
        /// <summary>
        /// ��׼�����Ƶϵ��������ǿ�½��׼����Ϊ1
        /// </summary>
        public const string CTC_DRIVERF = "DRIVERF";
        /// <summary>
        /// ��׼�������Զ����̶�
        /// </summary>
        public const string CTC_DRIVERFPULSES = "DRIVERFPULSES";
        /// <summary>
        /// SQL���ݿ������IP
        /// </summary>
        public const string CTC_SQL_SERVERIP = "SQL_SERVERIP";
        /// <summary>
        /// SQL���ݿ��û���
        /// </summary>
        public const string CTC_SQL_USERID = "SQL_USERID";
        /// <summary>
        /// SQL��¼����
        /// </summary>
        public const string CTC_SQL_PASSWORD = "SQL_PASSWORD";
        /// <summary>
        /// SQL���ݿ���
        /// </summary>
        public const string CTC_SQL_DATABASENAME = "SQL_DATABASENAME";
        /// <summary>
        /// ����ʧ�����Դ���.Ӧ�������ж�̨����Ǳ������������춨�������ڴ���
        /// </summary>
        public const string CTC_RETRY = "RETRY";

        /// <summary>
        /// ɫ�깦��
        /// </summary>
        public const string CTC_SEBIAO = "Sebiao";

        /// <summary>
        /// ��׼���ȡʱ����
        /// </summary>
        public const string CTC_STDMETERREADTIME = "CTC_STDMETERREADTIME";
        #region --------------ʵ�鷽��������--------------

        #region -------ʵ�鷽��-------
        /// <summary>
        /// ʵ�鷽��(TM)-ʱ��Ͷ��ʵ�鷽��
        /// </summary>
        public const string CTC_TM_TIMECUT = "TESTMETHOD_TIMECUT";
        /// <summary>
        /// ʵ�鷽��(TM)-�޹������洢��ʵ�鷽��
        /// </summary>
        public const string CTC_TM_QPOWERM = "TESTMETHOD_QPOWERMEMORY";
        /// <summary>
        /// ʵ�鷽��(TM)-����բʵ�鷽��
        /// </summary>
        public const string CTC_TM_PULLGATE = "TESTMETHOD_PULLGATE";
        /// <summary>
        /// ʵ�鷽��(TM)-GPSȡʱ��ʽ
        /// </summary>
        public const string CTC_TM_GPSGETT = "TESTMETHOD_GPSGETTIME";
        /// <summary>
        /// ʵ�鷽��(TM)-���ɿ���ʵ�鷽��
        /// </summary>
        public const string CTC_TM_LOADSWITCH = "TESTMETHOD_LOADSWITCH";
        /// <summary>
        /// ʵ�鷽��(TM)-��ʱ����ʵ�鷽��
        /// </summary>
        public const string CTC_TM_TIMEFUNCTION = "TESTMETHOD_TIMEFUNCTION";
        /// <summary>
        /// ʵ�鷽��(TM)-���5�ֹ㲥Уʱʱ�Ƿ���ʵ�� 
        /// </summary>
        public const string CTC_TM_ISTEST05BC = "TESTMETHOD_ISTBROADCALIBRATE";
        /// <summary>
        /// ʵ�鷽��(TM)-�Ƿ���˴���� 
        /// </summary>
        public const string CTC_TM_ISFILTERMACROV = "TESTMETHOD_ISFVALUE";
        /// <summary>
        /// ʵ�鷽��(TM)-����Ƿ��жϴ��� 
        /// </summary>
        public const string CTC_TM_ISJUDGECOUNT = "TESTMETHOD_ISJCOUNTOFERROR";
        /// <summary>
        /// ʵ�鷽��(TM)-����բʵ���ӵ����Ƿ�����Ѻ�բ
        /// </summary>
        public const string CTC_TM_ISTESTCLOSEC = "TESTMETHOD_ISTCCURRENT";
        /// <summary>
        /// ʵ�鷽��(TM)-����բʵ��ӵ������Ƿ��������
        /// </summary>
        public const string CTC_TM_ISCLEAR0CP= "TESTMETHOD_ISCLEAR0POWER";
        /// <summary>
        /// ʵ�鷽��(TM)-����ָ���Ƿ���ù㲥��ʽ�·�
        /// </summary>
        public const string CTC_TM_IS_GB_Address = "IS_GB_Address";
        /// <summary>
        /// ʵ�鷽��(TM)-��Ǳ��ʵ��ʱ�ɼ��������������ڶ���
        /// </summary>
        public const string CTC_TM_ISLESSPULSEC = "TESTMETHOD_ISLPULSECOUNT";
        /// <summary>
        /// ʵ�鷽��(TM)-����ʵ��ʱ�ɼ�����������С�ڶ���
        /// </summary>
        public const string CTC_TM_ISMOREPULSEC = "TESTMETHOD_ISMPULSECOUNT";
        /// <summary>
        /// ʵ�鷽��(TM)-CL2036���������Ʒ�ʽ�������رպͿ���
        /// </summary>
        public const string CTC_TM_CL2036CONTROLCM = "TESTMETHOD_CL2036CCONTROLMODE";
        /// <summary>
        /// ʵ�鷽��(TM)-�ṩ������ʾ���ܽӿ�
        /// </summary>
        public const string CTC_TM_ISSUPPLYVOICE = "TESTMETHOD_ISSUPPLYV";
        /// <summary>
        /// ʵ�鷽��(TM)-ʣ������ݼ�׼ȷ�ȣ���ǰ��۱�ʶ
        /// </summary>
        public const string CTC_TM_LEFTRIGHTPRICE = "TESTMETHOD_RIGHTPRICE";
        /// <summary>
        /// ʵ�鷽��(TM)-���ƹ��ܣ��������1��2��ֵд����
        /// </summary>
        public const string CTC_TM_WRITEWRINGPRICE = "TESTMETHOD_WRITEWRINGPRICE";
        #endregion

        #region -------ʵ������-------
        /// <summary>
        /// ʵ������(TB)-���Ĳ����жϹ������
        /// </summary>
        public const string CTC_TB_POWERUSETESTJ= "TESTBASIS_POWERUSETESTJUDGE";
        /// <summary>
        /// ʵ������(TB)-��е��ʵ��춨�������
        /// </summary>
        public const string CTC_TB_EMETER = "TESTBASIS_MACHINEMETER";
        /// <summary>
        /// ʵ������(TB)-����ʾֵ����ж�����
        /// </summary>
        public const string CTC_TB_NEEDVALUEIF = "TESTBASIS_NEEDVALUEIF";
        #endregion

        #region-------������Ǳ��-------
        ///<summary>
        /// ʵ�������뷽��(TSB)-��ʱ�����
        /// </summary>
        public const string CTC_TSB_STARTTIME = "TESTSANDB_STARTTIME";
        ///<summary>
        /// ʵ�������뷽��(TSB)-�����������ǹ�����
        /// </summary>
        public const string CTC_TSB_METERTYPES = "TESTSANDB_METERTYPES";
        #endregion

        #region -------����-------
        /// <summary>
        /// ʵ�������뷽�� ����(TO)-���ز�������485��������
        /// </summary>
        public const string CTC_TO_CWREPLACE485 = "TESTOTHER_CWREPLACE485";
        /// <summary>
        /// ʵ�������뷽�� ����(TO)-�Ĵ������
        /// </summary>
        public const string CTC_TO_CHECKREGISTER = "TESTOTHER_CHECKREGISTER";

        #endregion

        #endregion

        #region --------------ʵ�����--------------
        ///// <summary>
        ///// ʵ�����(TS)-����춨�������õ�ѹ�͵����Ƿ�ʹ�ðٷֱ�
        ///// </summary>
        //public const string CTC_TS_ISUSEPEROFVA="TESTSET_ISUSEPOFVA";
        ///// <summary>
        ///// ʵ�����(TS)-�����������Ҫ���Ƿ���Ҫ�ָ���ǰʱ��
        ///// </summary>
        //public const string CTC_TS_ISNEEDRTIME = "TESTSET_ISNREVERTTATCLEAR";
        ///// <summary>
        ///// ʵ�����(TS)-�Ƿ������������ģ�������������
        ///// </summary>
        //public const string CTC_TS_ISATSSOUTPUT= "TESTSET_ISNREVERTTATCLEAR";        


        #endregion

        #region ------------�춨��׼����------------
        /// <summary>
        /// ����ʽ���춨���
        /// </summary>
        public const string CTG_ELEC_GC = "ELEC_GC";
        /// <summary>
        /// ��Ӧʽ���춨���
        /// </summary>
        public const string CTG_VICA_GC = "VICA_GC";
        #endregion

        #region ----------------���춨�������-------------
        /// <summary>
        /// ÿ������ȡ�������������
        /// </summary>
        public const string CTC_WC_TIMES_BASICERROR = "TIMES_BASICERROR";
        /// <summary>
        /// ��׼ƫ��ȡ�������������
        /// </summary>
        public const string CTC_WC_TIMES_WINDAGE = "TIMES_WINDAGE";
        /// <summary>
        /// ÿ���������������
        /// </summary>
        public const string CTC_WC_MAXTIMES = "WC_MAXTIMES";
        /// <summary>
        /// ÿ�������춨ʱ��
        /// </summary>
        public const string CTC_WC_MAXSECONDS = "WC_MAXSECONDS";
        /// <summary>
        /// ������ж�
        /// </summary>
        public const string CTC_WC_JUMP = "WC_JUMP";
        /// <summary>
        /// IN����
        /// </summary>
        public const string CTC_WC_IN = "WC_IN";
        /// <summary>
        /// ƽ��ֵ����С��λ
        /// </summary>
        public const string CTC_WC_AVGPRECISION = "AVGPRECISION";
        #endregion

        #region ----------���������������----------
        /// <summary>
        /// ����ͬʱ���������|��
        /// </summary>
        public const string CTC_ZZ_UNINERROR = "ZZ_UNINERROR";
        /// <summary>
        /// ����������Ŀ
        /// [Ĭ��],�����κ�����ֱ�Ӱ�������Ŀ˳������
        /// [����ַ���ͬʱ��],ѡ�������ȶ�ȡ�ܵ����룬�ַ���������ٶ�ȡ�ܵ�ֹ��.����������ʱ����Ĭ��ѡ��.
        /// [�Զ��춨��ʱ���ڵ����зַ���]����ѡ��������24Сʱ���֣����̶�������ͬ�����ʻ�����֡�
        /// </summary>
        public const string CTC_ZZ_SELECTITEM = "ZZ_SELECTITEM";

        #endregion


        #region �๦�ܼ춨����
        /// <summary>
        /// �๦��Ӧ�ò㷢�����ݺ����ȴ�ʱ��
        /// </summary>
        public const string CTC_DGN_MAXWAITDATABACKTIME = "MAXWAITDATABACKTIME";
        /// <summary>
        /// �๦�ܼ춨Դ�ȶ�����ʱ��
        /// </summary>
        public const String CTC_DGN_POWERON_ATTERTIME = "POWERON_ATTERTIME";
        /// <summary>
        /// ��Ҫ�Ա����д����ʱ������ʾ
        /// </summary>
        public const string CTC_DGN_WRITEMETERALARM = "WRITEMETERALARM";
        /// <summary>
        /// �ռ�ʱ���춨����:����ģʽ|��׼ģʽ
        /// </summary>
        public const string CTC_DGN_RJSVERIFYTYPE = "RJSVERIFYTYPE";

        /// <summary>
        /// 
        /// </summary>
        public const string CTC_DGN_READDATAFROMRS485 = "READDATAFROMRS485";
        #endregion


        #region----------������Ŀ����----------
        /// <summary>
        /// �Ƿ���ʾ�춨��Ŀ��������ֵΪ"��"��"��"
        /// </summary>
        public const string CTC_DISPLAY_CHECKDESCRIPTION="DISPLAYCHECKDESCRIPTION";
        /// <summary>
        /// �Ƿ��Զ�����λ��û�йұ�
        /// </summary>
        public const string CTC_AUTO_ISHAVECHECKMETER = "ISHAVEMETER";

        /// <summary>
        /// �Ƿ�У��ͨ�ŵ�ַ
        /// </summary>
        public const string CTC_CHECK_COMM = "ISCHECKCOMMUNICATIONADDRESS";
        /// <summary>
        /// ��ȡ�ʲ���ź�λ�ĳ���
        /// </summary>
        public const string CTC_ADRRESS_LENGTH = "COMMUNICATIONADDRESSLENGTH";
        /// <summary>
        /// �Ƿ�������쳧�Һͱ��ͺż���Э�飡ֵ�ǡ��ǡ�������
        /// </summary>
        public const string CTC_OTHER_SELECTPROTOCOLFROMMETERFACTROY = "SELECTPROTOCOLFROMMETERFACTROY";
        /// <summary>
        /// ̨��Ӳ���������ú�ȴ�ʱ��
        /// </summary>
        public const string CTC_OTHER_EQUIPSET_WAITTIME = "EQUIPSET_WAITTIME";
        /// <summary>
        /// �춨ʱ��ʾ���Ƶ�
        /// </summary>
        public const string CTC_OTHER_SHOWRESULTLIGHT = "SHOWRESULTLIGHT";
        /// <summary>
        /// 2038��������֡�ȴ�ʱ�䱶��[�ֹ�����]
        /// ��ԭ���ĵȴ�ʱ��Ϊ1000MS,��ֵ����Ϊ2,��ȴ�ʱ��Ϊ1000*2MS
        /// �˲���Ӧ���ڷ��͵ȴ�����ʱ�估���ֽ�ʱ����
        /// </summary>
        public const string CTC_OTHER_WAITTIME_SENDFRAME_2036 = "SENDFRAME_2036";
        /// <summary>
        /// ��������ͨ����ȴ�ʱ��[�ֹ�����]
        /// </summary>
        public const string CTC_OTHER_WAITTIME_SELECTPULSECHANNEL = "WAITTIME_SELECTPULSECHANNEL";

        /// <summary>
        /// ���ù��ܲ�����ȴ�ʱ��[�ֹ�����]
        /// </summary>
        public const string CTC_OTHER_WAITTIME_SETTESTPARA = "WAITTIME_SETTESTPARA";
        /// <summary>
        /// ��ͨ������Դ��ȴ�ʱ��[�ֹ�����]
        /// </summary>
        public const string CTC_OTHER_WAITTIME_POWERON = "WAITTIME_POWERON";
        /// <summary>
        /// �����춨��ȴ�ʱ��[�ֹ�����]
        /// </summary>
        public const string CTC_OTHER_WAITTIME_STARTTASK = "WAITTIME_STARTTASK";

        /// <summary>
        /// �������������ȴ�ʱ��[�ֹ�����]
        /// </summary>
        public const string CTC_OTHER_WAITTIME_SET188G = "WAITTIME_SET188G";
        /// <summary>
        /// ��ȡ�����ݵȴ�ʱ��
        /// </summary>
        public const string CTC_OTHER_WAITTIME_READDATA = "WAITTIME_READDATA";

        /// <summary>
        /// ȷ��Դ�Ѿ��ɹ��ȴ�ʱ��
        /// </summary>
        public const string CTC_OTHER_WAITTIME_POWERSTATE = "WAITTIME_POWERSTATE";
        /// <summary>
        /// ���ݹ������·��
        /// </summary>
        public const string CTC_OTHER_DATAMANAGEEXEPATH = "DATAMANAGEEXEPATH";
        /// <summary>
        /// ���ݹ�����������
        /// </summary>
        public const string CTC_OTHER_DATAMANAGEPROCESSNAME = "DATAMANAGEPROCESSNAME";
        /// <summary>
        /// Э�����ó���·��
        /// </summary>
        public const string CTC_OTHER_PROTOCOLEXENAME = "PROTOCOLEXENAME";
        /// <summary>
        /// Э�����ó��������
        /// </summary>
        public const string CTC_OTHER_PROTOCOLPROCESS = "PROTOCOLPROCESS";
        /// <summary>
        /// ֤����ǰ׺
        /// </summary>
        public const string CTC_OTHER_PREFIXOFCERTIFICATEN = "PREFIXOFCERTIFICATENUMBER";//certificate
        #endregion


        #region -------------------�ӿ����-----------------------------------

        /// <summary>
        /// �ӿ������������Ҫ���õ�EXE����
        /// </summary>
        public const string CTC_MIS_EXENAME = "MIS_EXENAME";
        /// <summary>
        /// �ӿ������������Ҫ���õ�EXE���������
        /// </summary>
        public const string CTC_MIS_EXEPROCESSNAME = "MIS_EXEPROCESSNAME";
        /// <summary>
        /// �ӿ������������Ҫ���õ�DLL���
        /// </summary>
        public const string CTC_INTERFACE_DllNAME = "DLLNAME";
        /// <summary>
        /// �ӿ������������Ҫ���õ�DLL�����
        /// </summary>
        public const string CTC_INTERFACE_DLLCLASS = "DLLCLASS";

        /// <summary>
        /// mis��������Դ
        /// </summary>
        public const string CTC_MIS_DATASOURCE = "MIS_DATASOURCE";
        /// <summary>
        /// mis��½�û���
        /// </summary>
        public const string CTC_MIS_USERID = "MIS_USERID";
        /// <summary>
        /// mis��½����
        /// </summary>
        public const string CTC_MIS_PASSWORD = "MIS_PASSWORD";
        /// <summary>
        /// �Ƿ��ϴ�ʵʱ����
        /// </summary>
        public const string CTC_MIS_NOWDATA = "MIS_NOWDATA";
        /// <summary>
        /// webservice��ַ
        /// </summary>
        public const string CTC_MIS_WEBSERVICE_URL = "MIS_WEBSERVICE_URL";
        /// <summary>
        /// MIS�ӿ�����
        /// </summary>
        public const string CTC_MIS_INTERFACETYPE = "MISINTERFACETYPE";
        /// <summary>
        /// ��ȡ����������ʲô���������Ϣ
        /// </summary>
        public const string CTC_BasisDownInfo = "BasisDownInfo";

        #endregion

        #endregion


        #region---------------�ز��춨����------------------
        ///// <summary>
        ///// �ز�Э������
        ///// </summary>
        //public const string CTC_ZB_PROCOTOCOLTYPE = "PROCOTOCOLTYPE";

        /// <summary>
        /// �ز�ģ������
        /// </summary>
        public const string CTC_ZB_MODELTYPE = "MODELTYPE";
        /// <summary>
        /// ͨѶ��ʽ
        /// </summary>
        public const string CTC_ZB_COMMUNICATIONTYPE = "COMMUNICATIONTYPE";

        /// <summary>
        /// �ز�����������
        /// </summary>
        public const string CTC_ZB_READERTYPE = "READERTYPE";

        /// <summary>
        /// �ز�������������
        /// </summary>
        public const string CTC_ZB_READERBAUDRATE = "READERBAUDRATE";

        ///// <summary>
        ///// ���ں�
        ///// </summary>
        //public const string CTC_ZB_COMPORT = "COMPORT";

        /// <summary>
        /// ͨѶ����
        /// </summary>
        public const string CTC_ZB_PARAMETER = "PARAMETER";

        /// <summary>
        /// ͨ����
        /// </summary>
        public const string CTC_ZB_CHANNELNO = "CHANNELNO";

        /// <summary>
        /// ģ��
        /// </summary>
        public const string CTC_ZB_MODULE = "MODULE";

        /// <summary>
        /// ��λ
        /// </summary>
        public const string CTC_ZB_PHASE = "PHASE";

        /// <summary>
        /// ����
        /// </summary>
        public const string CTC_ZB_PIN = "PIN";
        #endregion

        #region ----------------���ܻ����-----------------
        /// <summary>
        /// ���ܻ�Ĭ������
        /// </summary>
        public const string CTC_ENCRYPTION_DEFAULTTYPE = "ENCRYPTIONDEFAULTTYPE";

        /// <summary>
        /// ���ܻ�IP
        /// </summary>
        public const string CTC_ENCRYPTION_MACHINEIP = "ENCRYPTIONMACHINEIP";

        /// <summary>
        /// ���ܻ��˿�
        /// </summary>
        public const string CTC_ENCRYPTION_MACHINEPORT = "ENCRYPTIONMACHINEPORT";

        /// <summary>
        /// ���ܻ�USBKEY
        /// </summary>
        public const string CTC_ENCRYPTION_MACHINEUSBKEY = "ENCRYPTIONMACHINEUSBKEY";

        /// <summary>
        /// �Ƿ�������������������
        /// </summary>
        public const string CTC_ENCRYPTION_MACHINEISAUTOLINK = "ENCRYPTIONMACHINEISAUTOLINK";

        /// <summary>
        /// ��������֤״̬
        /// </summary>
        public const string CTC_ENCRYPTION_STATUS = "ENCRYPTIONSTATUS";

        /// <summary>
        /// ���ܻ�Ӧ�ò㷢�����ݺ����ȴ�ʱ��
        /// </summary>
        public const string CTC_ENCRYPTION_MAXWAITDATABACKTIME = "ENCRYPTIONMAXWAITDATABACKTIME";

        /// <summary>
        /// �����ַ���
        /// </summary>
        public const string CONNECTSTRING = "CONNECTSTRING";


        /// <summary>
        /// ���ܻ������ļ�
        /// </summary>
        public const string CONST_ENCRYPTION_INI = "\\Encryption\\Encryption.ini";
        /// <summary>
        /// ���ܻ�����
        /// </summary>
        public const string CONST_ENCRYPTION_SECTION = "EncryptionType";
        /// <summary>
        /// ���ܻ�����09 13
        /// </summary>
        public const string CONST_ENCRYPTION_NEWOLD = "EncryptionNewOld";

        #endregion
        
    }
}

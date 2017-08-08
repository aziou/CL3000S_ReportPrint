using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using CLDC_DataCore.Struct;
using CLDC_DataCore.Model.DnbModel.DnbInfo;

namespace CLDC_DataCore.Model.DnbModel
{
    /// <summary>
    /// 可以被序列化，网络传输模型问题，断电损坏数据问题，模型改动反序列化问题
    /// 改为临时数据库
    /// </summary>
    [Serializable()]
    public class DnbGroupInfo : CLDC_CTNProtocol.CTNPCommand
    {

        #region 电能表总模型 、以及同步处理相关 public List<MeterBasicInfo> MeterGroup
        private object ObjMeterGroupLock = new object();


        private List<MeterBasicInfo> m_MeterGroup = new List<MeterBasicInfo>();

        /// <summary>
        /// 获取MeterGroup的使用权、使用完毕以后必须调用MeterGroupExit()
        /// 1、MeterGroupEnter() 和 MeterGroupExit() 必须成对使用、不能凭空调用MeterGroupExit()
        /// 2、MeterGroupExit() 必须在调用MeterGroupEnter() 的同一个线程
        /// </summary>
        public void MeterGroupEnter()
        {
            Monitor.Enter(MeterGroup);
        }

        /// <summary>
        /// 释放 MeterGroup 的使用权、使用完毕以后必须调用MeterGroupExit()
        /// 1、MeterGroupEnter() 和 MeterGroupExit() 必须成对使用、不能凭空调用MeterGroupExit()
        /// 2、MeterGroupExit() 必须在调用MeterGroupEnter() 的同一个线程
        /// </summary>
        public void MeterGroupExit()
        {
            Monitor.Exit(MeterGroup);
        }

        /// <summary>
        /// 表信息组，信息组中一个元素为一只表
        /// </summary>
        public List<MeterBasicInfo> MeterGroup
        {
            get
            {
                if (ObjMeterGroupLock == null)
                {
                    ObjMeterGroupLock = new object();
                }
                lock (ObjMeterGroupLock)
                {
                    return m_MeterGroup;
                }
            }
            set
            {
                if (ObjMeterGroupLock == null)
                {
                    ObjMeterGroupLock = new object();
                }
                lock (ObjMeterGroupLock)
                {
                    m_MeterGroup = value;
                }
            }
        }
        #endregion

        #region -------------------方案相关----------------------------------------------
        /// <summary>
        /// 电能表检定方案
        /// </summary>
        public List<object> m_CheckPlan = new List<object>();
        /// <summary>
        /// 方案项目是否检定
        /// </summary>
        public bool[] m_bCheckPlan = null;

        /// <summary>
        /// 所使用的方案的名称
        /// </summary>
        private string m_FaName = "";

        /// <summary>
        /// 参照圈数
        /// </summary>
        private int m_CzQs = 1;

        /// <summary>
        /// 参照电流倍数
        /// </summary>
        private string m_CzIb = "1.0Ib";

        /// <summary>
        /// 最小圈数，下标0有功，下标1无功
        /// </summary>
        public int[] MinConst = new int[2];

        /// <summary>
        /// 误差上限比率（默认100%）
        /// </summary>
        private float m_WcxUp = 1F;
        /// <summary>
        /// 误差下限比率（默认100%）
        /// </summary>
        private float m_WcxDown = 1F;

        /// <summary>
        /// 统一误差上限比率（只读）
        /// </summary>
        public float WcxUpPercent
        {
            get
            {
                return m_WcxUp;
            }
        }
        /// <summary>
        /// 统一误差下限比率（只读）
        /// </summary>
        public float WcxDownPercent
        {
            get
            {
                return m_WcxDown;
            }
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="Up"></param>
        /// <param name="Down"></param>
        public void SetWcxPercent(float Up, float Down)
        {
            m_WcxUp = Up;
            m_WcxDown = Down;
        }
        /// <summary>
        /// 误差限参照
        /// </summary>
        private string m_CzWcLimit = "规程误差限";

        /// <summary>
        /// 电能表检定方案（只读）,必须在调用CreateFA方法后才可使用
        /// </summary>
        public List<object> CheckPlan
        {
            get
            {
                return m_CheckPlan;
            }
        }

        /// <summary>
        /// 只有在特殊的情况才会使用，一般仅使用在CloneFA后，对方案的还原时使用
        /// </summary>
        /// <param name="PlanPrj"></param>
        public void SetCheckPlan(List<object> PlanPrj)
        {
            m_CheckPlan = PlanPrj;
        }
        /// <summary>
        /// 方案克隆
        /// </summary>
        /// <returns></returns>
        public List<object> CloneFA()
        {
            List<Object> Tmp = new List<object>();

            if (m_CheckPlan == null || m_CheckPlan.Count == 0)
            {
                return Tmp;
            }
            else
            {
                for (int i = 0; i < m_CheckPlan.Count; i++)
                {
                    Tmp.Add(m_CheckPlan[i]);
                }
                return Tmp;
            }
        }

        /// <summary>
        /// 所使用的方案的名称（只读）
        /// </summary>
        public string FaName
        {
            get
            {
                return m_FaName;
            }
        }

        /// <summary>
        /// 参照圈数（只读）
        /// </summary>
        public int CzQs
        {
            get
            {
                return m_CzQs;
            }
        }

        /// <summary>
        /// 外部设置参照圈数和参照电流倍数
        /// </summary>
        /// <param name="Qs">圈数</param>
        /// <param name="xIb">电流倍数</param>
        public void SetCzQsIb(int Qs, string xIb)
        {
            if (Qs > 0) m_CzQs = Qs;
            if (xIb != string.Empty) m_CzIb = xIb;
        }
        /// <summary>
        /// 外部设置参照误差线
        /// </summary>
        /// <param name="WcLimit"></param>
        public void SetWcLimit(string WcLimit)
        {
            if (WcLimit != string.Empty)
            {
                m_CzWcLimit = WcLimit;
            }
        }

        /// <summary>
        /// 参照电流倍数（只读）
        /// </summary>
        public string CzIb
        {
            get
            {
                return m_CzIb;
            }
        }

        /// <summary>
        /// 参照误差限（只读）
        /// </summary>
        public string CzWcLimit
        {
            get
            {
                return m_CzWcLimit;
            }
        }

        /// <summary>
        /// 创建方案
        /// </summary>
        /// <param name="TaiType">台体类型0-三相台，1-单相台</param>
        /// <param name="FaName">方案名称</param>
        /// <returns></returns>
        public bool CreateFA(int TaiType, string FaName)
        {
            Model.Plan.Model_Plan _Plan = new CLDC_DataCore.Model.Plan.Model_Plan(TaiType, FaName);
            this.m_WcxDown = 1;
            this.m_WcxUp = 1;
            m_CheckPlan = _Plan.CreateFA(m_MeterGroup[CLDC_DataCore.Const.GlobalUnit.FirstYaoJianMeter], ref m_CzIb, ref m_CzQs, ref m_CzWcLimit);

            m_bCheckPlan = new bool[m_CheckPlan.Count];

            if (m_CheckPlan.Count == 0)
                return false;
            else
            {
                m_FaName = FaName;
                return true;
            }
        }

        public string GetDispatcherPlanKeys(out Dictionary<string, string[]> Dic_keys)
        {
            Dic_keys = new Dictionary<string, string[]>();
            List<string> lst_keys = new List<string>();
            foreach (var item in m_CheckPlan)
            {
                string[] strParas = new string[5];
                string _FaDescription = "";
                string _ID = "";
                string _Type = "";
                string _Time = "";
                string _ViewID = "";

                #region
                if (item is StPlan_PrePareTest)
                {
                    _FaDescription = ((StPlan_PrePareTest)item).ToString();
                    _ID = "77" + ((StPlan_PrePareTest)item).PrePrjID;
                    _Type = "77";
                    _Time = "30";
                    _ViewID = "";
                }
                else if (item is StPlan_WGJC)         //如果是外观检查试验项目
                {
                    _FaDescription = ((StPlan_WGJC)item).ToString();
                    _ID = "00" + ((StPlan_WGJC)item).WGJCPrjID;
                    _Type = "00";
                    _Time = "30";
                    _ViewID = "";
                }
                else if (item is CLDC_DataCore.Struct.StInsulationParam)
                {
                    _FaDescription = ((StInsulationParam)item).ToString();
                    _ID = "03" + ((StInsulationParam)item).InsulationPrjID;
                    _Type = "03";
                    _Time = "60";
                    _ViewID = "";
                }
                else if (item is StPlan_QiDong)         //如果是起动试验项目
                {
                    _FaDescription = ((StPlan_QiDong)item).ToString();
                    _ID = "02" + ((StPlan_QiDong)item).PrjID;
                    _Type = "02";
                    _Time = (((StPlan_QiDong)item).xTime * 60).ToString();
                    _ViewID = "";
                }
                else if (item is StPlan_QianDong)         //如果是潜动试验项目
                {
                    _FaDescription = ((StPlan_QianDong)item).ToString();
                    _ID = "02" + ((StPlan_QianDong)item).PrjID;
                    _Type = "02";
                    _Time = (((StPlan_QianDong)item).xTime * 60).ToString();
                    _ViewID = "";
                }
                else if (item is StPlan_WcPoint)         //如果是基本误差试验项目
                {
                    _FaDescription = ((StPlan_WcPoint)item).ToString();
                    _ID = "02" + ((StPlan_WcPoint)item).PrjID;
                    _Type = "02";
                    _Time = "6";
                    _ViewID = "3";
                }
                else if (item is StPlan_ZouZi)         //如果是走字项目
                {
                    _FaDescription = ((StPlan_ZouZi)item).ToString();
                    _ID = "02" + ((StPlan_ZouZi)item).PrjID;
                    _Type = "02";
                    _Time = "180";
                    _ViewID = "";
                }
                else if (item is StPlan_ConnProtocol)         //如果是通讯协议检查试验
                {
                    _FaDescription = ((StPlan_ConnProtocol)item).ToString();
                    _ID = "05" + ((StPlan_ConnProtocol)item).PrjID;
                    _Type = "05";
                    _Time = "60";
                    _ViewID = "";
                }
                else if (item is CLDC_DataCore.Struct.StPlan_Dgn) //如果是多功能项目
                {
                    _FaDescription = ((CLDC_DataCore.Struct.StPlan_Dgn)item).ToString();
                    _ID = "08" + ((StPlan_Dgn)item).DgnPrjID;
                    _Type = "08";
                    _Time = "60";
                    _ViewID = "";
                }
                else if (item is StPlan_SpecalCheck)         //如果是特殊项目
                {
                    _FaDescription = ((StPlan_SpecalCheck)item).ToString();
                    _ID = "02" + ((StPlan_SpecalCheck)item).ProjectionNumber;
                    _Type = "02";
                    _Time = "6";
                    _ViewID = "";
                }
                else if (item is StPlan_Carrier)            //如果是载波项目
                {
                    _FaDescription = ((StPlan_Carrier)item).ToString();
                    _ID = "09" + ((StPlan_Carrier)item).str_PrjID;
                    _Type = "09";
                    _Time = "300";
                    _ViewID = "";
                }
                else if (item is CLDC_DataCore.Struct.StErrAccord)//如果是误差一致性项目
                {
                    _FaDescription = ((StErrAccord)item).ToString();
                    _ID = "06" + ((StErrAccord)item).ErrAccordType.ToString();
                    _Type = "06";
                    _Time = "1800";
                    _ViewID = "";
                }
                else if (item is CLDC_DataCore.Struct.StPowerConsume)//如果是功耗项目
                {
                    _FaDescription = ((StPowerConsume)item).ToString();
                    _ID = "03" + ((StPowerConsume)item).PowerConsumePrjID;
                    _Type = "03";
                    _Time = "60";
                    _ViewID = "";
                }
                else if (item is CLDC_DataCore.Struct.StDataSendForRelay)//如果是数据转发
                {
                    _FaDescription = ((StDataSendForRelay)item).ToString();
                    _ID = "05" + ((StDataSendForRelay)item).PrjID;
                    _Type = "05";
                    _Time = "60";
                    _ViewID = "";
                }
                else if (item is CLDC_DataCore.Struct.StPlan_Freeze)
                {
                    _FaDescription = ((StPlan_Freeze)item).ToString();
                    _ID = "10" + ((StPlan_Freeze)item).FreezePrjID;
                    _Type = "10";
                    _Time = "60";
                    _ViewID = "";
                }
                else if (item is CLDC_DataCore.Struct.StPlan_Function)
                {
                    _FaDescription = ((StPlan_Function)item).ToString();
                    _ID = "04" + ((StPlan_Function)item).FunctionPrjID;
                    _Type = "04";
                    _Time = "600";
                    _ViewID = "";
                }
                else if (item is CLDC_DataCore.Struct.StPlan_CostControl)
                {
                    _FaDescription = ((StPlan_CostControl)item).ToString();
                    _ID = "07" + ((StPlan_CostControl)item).CostControlPrjID;
                    _Type = "07";
                    _Time = "120";
                    _ViewID = "";
                }
                else if (item is CLDC_DataCore.Struct.StPlan_EventLog)
                {
                    _FaDescription = ((StPlan_EventLog)item).ToString();
                    _ID = "11" + ((StPlan_EventLog)item).EventLogPrjID;
                    _Type = "11";
                    _Time = "120";
                    _ViewID = "";
                }
                else if (item is CLDC_DataCore.Struct.StPlan_Infrared)
                {
                    _FaDescription = ((StPlan_Infrared)item).ToString();
                    _ID = "12" + ((StPlan_Infrared)item).str_PrjID;
                    _Type = "12";
                    _Time = "120";
                    _ViewID = "";
                }
                #endregion

                strParas[0] = _FaDescription;
                strParas[1] = _ID;
                strParas[2] = _Type;
                strParas[3] = _Time;
                strParas[4] = _ViewID;

                Dic_keys.Add(_ID, strParas);
                lst_keys.Add(_ID);

            }
            return string.Join(",", lst_keys.ToArray());
        }

        #endregion

        #region 字段、属性

        public Dictionary<string, string> SystemInfo = new Dictionary<string, string>();

        /// <summary>
        /// 台体编号
        /// </summary>
        public readonly int _TaiID = 0;

        /// <summary>
        /// 表位数
        /// </summary>
        public readonly int _Bws = 0;

        /// <summary>
        /// 当前试验时间，仅针对预热，启动，潜动，走字，多功能有效
        /// </summary>
        public float NowMinute = 0;

        /// <summary>
        /// 检定状态
        /// </summary>
        /// <remarks>
        /// 修改检定状态支持按位重叠。
        /// </remarks>
        public CLDC_Comm.Enum.Cus_CheckStaute CheckState = CLDC_Comm.Enum.Cus_CheckStaute.停止检定;

        /// <summary>
        /// 偏差检定次数，在偏差检定时窗体表格构建的时候会用到
        /// </summary>
        public int PcCheckNumic = 0;
        /// <summary>
        /// 误差检定次数，在误差检定时窗体表格构建的时候会用到
        /// </summary>
        public int WcCheckNumic = 0;

        /// <summary>
        /// 是否自动加载协议！
        /// </summary>
        private bool _AutoProtocol = false;

        /// <summary>
        /// 是否自动加载协议（只读）
        /// </summary>
        public bool AutoProtocol
        {
            get
            {
                return _AutoProtocol;
            }
        }
        /// <summary>
        /// 设置是否自动加载协议（只写）
        /// </summary>
        public bool SetAutoProtocol
        {
            set
            {
                _AutoProtocol = value;
            }
        }

        /// <summary>
        /// 当前检定ID，-1表示参数录入，-2表示方案配置，-3表示审核存盘，0~N表示当前检定点
        /// </summary>
        private int m_ItemID = -1;

        //操作进度的表示、与ActiveItemID含义不同
        private CLDC_CTNProtocol.EnumOption option = CLDC_CTNProtocol.EnumOption.AZ未联机;

        /// <summary>
        /// 当前检定项目ID，-1表示参数录入，-2表示方案配置，-3表示审核存盘，0~N表示当前检定点
        /// </summary>
        public int ActiveItemID
        {
            set { m_ItemID = value; }
            get { return m_ItemID; }
        }

        /// <summary>
        /// 获取、操作进度的表示、与ActiveItemID含义不同
        /// </summary>
        public new CLDC_CTNProtocol.EnumOption Option
        {
            get
            {
                return option;
            }
        }

        /// <summary>
        /// 设置、客户端主、被控状态 (true 被控 | false 主控)
        /// </summary>
        /// <param name="value"></param>
        public void SetOption(CLDC_CTNProtocol.EnumOption value)
        {
            option = value;
        }


        /// <summary>
        /// 准确标志当前检定项目的下标
        /// 参数录入完毕按纽将其设置为 -1
        /// 创建检定方案按钮将其设置为 -2
        /// 其他时候：为实际检定方案进度的下标
        /// </summary>
        public int CheckProgressIndex = -1;
        #endregion

        #region 构造，方法
        /// <summary>
        /// 台体编号
        /// </summary>
        /// <param name="Bws">表位号</param>
        /// <param name="TaiID">台体编号</param>
        public DnbGroupInfo(int Bws, int TaiID)
        {
            _Bws = Bws;
            _TaiID = TaiID;
            this.Init();
        }

        /// <summary>
        /// 获取第一只要检表
        /// </summary>
        /// <returns></returns>
        public int GetFirstYaoJianMeterBwh()
        {
            if (this.MeterGroup.Count == 0) return -1;
            for (int i = 0; i < this.MeterGroup.Count; i++)
            {
                if (this.MeterGroup[i].YaoJianYn)
                {
                    return i;
                }

            }
            return -1;
        }

        /// <summary>
        /// 初始化表信息模型
        /// </summary>
        private void Init()
        {
            MeterGroup = new List<MeterBasicInfo>();
            for (int _I = 0; _I < _Bws; _I++)
            {
                MeterGroup.Add(new MeterBasicInfo(_I + 1));
            }
        }

        /// <summary>
        /// 根据表位号获取表基本信息
        /// </summary>
        /// <param name="intBno"></param>
        /// <returns></returns>
        public MeterBasicInfo GetMeterBasicInfoByBwh(int intBno)
        {
            foreach (MeterBasicInfo info in MeterGroup)
            {
                if (info.Mb_intBno == intBno)
                    return info;
            }
            return null;
        }
        /// <summary>
        /// 保存PK到临时库
        /// </summary>
        private void SavePK()
        {
            CLDC_DataCore.Const.GlobalUnit.Logger.Debug("Calling SaveToDataBase:");
            CLDC_DataCore.DataBase.DataControl _Data;
            _Data = new CLDC_DataCore.DataBase.DataControl(false);        //构造连接本地默认ACCESS数据库

            #region 构造sql数据库
            DataBase.DataControl sqlDataDal = null;
            if (CLDC_DataCore.Const.GlobalUnit.NetState == CLDC_Comm.Enum.Cus_NetState.Connected)
            {
                string sqlIP = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SQL_SERVERIP, string.Empty);
                string sqlUserName = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SQL_USERID, string.Empty);
                string sqlPassWord = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SQL_PASSWORD, string.Empty);
                sqlDataDal = new DataBase.DataControl(sqlIP, sqlUserName, sqlPassWord);
            }
            #endregion

            #region----清空数据表----
            List<string> lst_sql = new List<string>();
            foreach (CLDC_Comm.Enum.Cus_DBTableName item in System.Enum.GetValues(typeof(CLDC_Comm.Enum.Cus_DBTableName)))
            {
                string str = string.Format("DELETE FROM TMP_{1} WHERE AVR_DEVICE_ID='{0}'", _TaiID, System.Enum.GetName(typeof(CLDC_Comm.Enum.Cus_DBTableName), item));
                lst_sql.Add(str);
            }
            _Data.ExecuteSqlTran(lst_sql);
            //sql操作
            if (CLDC_DataCore.Const.GlobalUnit.NetState == CLDC_Comm.Enum.Cus_NetState.Connected)
            {
                sqlDataDal.ExecuteSqlTran(lst_sql);
            }
            #endregion----清空数据表----

            #region 将表信息添加到access和sql数据库
            string sql = "";
            for (int i = 0; i < MeterGroup.Count; i++)
            {
                MeterBasicInfo _BasicInfo = MeterGroup[i];
                _BasicInfo._intMyId = CLDC_DataCore.Function.Common.GetUniquenessID8(i);
                sql = string.Format("Select count(PK_LNG_METER_ID) FROM TMP_METER_INFO where AVR_DEVICE_ID='{0}' and LNG_BENCH_POINT_NO={1}", _TaiID, i + 1);
                System.Data.OleDb.OleDbDataReader result = _Data.ExecuteReaderSql(sql);

                string err = string.Empty;
                sql_MeterInfo(false, _BasicInfo, out sql, out err);
                _Data.ExecuteSql(sql);
                if (CLDC_DataCore.Const.GlobalUnit.NetState == CLDC_Comm.Enum.Cus_NetState.Connected)
                {
                    sqlDataDal.ExecuteSql(sql);
                }
            }
            CLDC_DataCore.Const.GlobalUnit.Logger.Debug("End Calling:SaveToDataBase");
            _Data.CloseDB();
            _Data = null;
            #endregion
        }
        /// <summary>
        /// 更新临时库中的电表的是否检定标志位
        /// </summary>
        /// <param name="bw">表位号从0开始</param>
        /// <param name="value">更新的状态</param>
        /// <returns></returns>
        public bool UpdataMeterIsYaoJian(int bw, bool value)
        {
            CLDC_DataCore.DataBase.DataControl _Data = new CLDC_DataCore.DataBase.DataControl(false);
            string sql = string.Format(@"update TMP_METER_INFO set CHR_CHECKED='{0}' where AVR_DEVICE_ID='{1}' and LNG_BENCH_POINT_NO={2}", value ? "1" : "0", _TaiID, bw + 1);
            int i = _Data.ExecuteSql(sql);
            return i > 0 ? true : false;
        }
        /// <summary>
        /// 删除对应的表名对应台体的所有数据
        /// </summary>
        /// <param name="_Data">表名</param>
        /// <param name="dbName">表名</param>
        private void ClearDB(ref List<string> _Data, string dbName)
        {
            string sql = "";
            sql = string.Format("delete FROM {1} where AVR_DEVICE_ID='{0}'", _TaiID, dbName);
            _Data.Add(sql);
        }
        /// <summary>
        /// 挂新表
        /// </summary>
        public void NewMeters()
        {
            for (int i = 0; i < MeterGroup.Count; i++)
            {
                MeterGroup[i].GetNewMeter();
            }
            SavePK();
            this.m_CheckPlan.Clear();
            ActiveItemID = -1;
        }
        /// 将临时库的数据转移到正式库
        /// <summary>
        /// 将临时库的数据转移到正式库
        /// </summary>
        /// <returns>0:转移完成；1：转移到access失败；2：转移到sql失败；3：全失败</returns>
        public int TransportTempDBToDB()
        {
            //accecss
            CLDC_DataCore.Const.GlobalUnit.Logger.Debug("Calling SaveToDataBase:");
            CLDC_DataCore.DataBase.DataControl _Data;
            _Data = new CLDC_DataCore.DataBase.DataControl(false);        //构造连接本地默认ACCESS数据库

            //Sql
            string sqlIP = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SQL_SERVERIP, string.Empty);
            string sqlUserName = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SQL_USERID, string.Empty);
            string sqlPassWord = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SQL_PASSWORD, string.Empty);
            DataBase.DataControl sqlDataDal = new DataBase.DataControl(sqlIP, sqlUserName, sqlPassWord);

            #region 将数据转移到正式库
            List<string> sqlMoveDataList = new List<string>();
            foreach (CLDC_Comm.Enum.Cus_DBTableName item in System.Enum.GetValues(typeof(CLDC_Comm.Enum.Cus_DBTableName)))
            {
                string str = string.Format("insert into {1} select * from TMP_{1}  where AVR_DEVICE_ID='{0}'", _TaiID, System.Enum.GetName(typeof(CLDC_Comm.Enum.Cus_DBTableName), item));
                sqlMoveDataList.Add(str);
            }
             bool accessResult = _Data.ExecuteSqlTran(sqlMoveDataList);
            //sql操作
            bool sqlResult = sqlDataDal.ExecuteSqlTran(sqlMoveDataList);
            #endregion
            if (accessResult && sqlResult)
            {
                return 0;
            }
            else if (!accessResult && !sqlResult)
            {
                return 3;
            }
            else if (!accessResult)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        #endregion

        #region 序列化文件，改为数据库
        /// <summary>
        /// 存储临时文件，序列化存储
        /// </summary>
        public void Save()
        {
            try
            {
                string StrIniFile = @"\Tmp\tmp.ini";
                string StrTmpFileName = string.Format(@"tmp_{0}{1}{2}{3}{4}{5}.dat", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);

                string TmpFilePath = @"\Tmp\" + StrTmpFileName;
                //防止多个线程同时保存，造成数据错乱
                lock (this)
                {
                    if (CLDC_DataCore.Function.File.WriteFileData(TmpFilePath, this.GetBytes()))
                    {
                        string StrLastTmpFileName = CLDC_DataCore.Function.File.ReadInIString(StrIniFile, "System", "LastTmp", "");
                        //最近一次
                        string strCanDel = CLDC_DataCore.Function.File.ReadInIString(StrIniFile, "System", "LastNTmp", "");
                        //可以删的
                        string strNCanDel = CLDC_DataCore.Function.File.ReadInIString(StrIniFile, "System", "LastNDTmp", "");
                        //记录文件名,可删
                        CLDC_DataCore.Function.File.WriteInIString(StrIniFile, "System", "LastNDTmp", strCanDel);
                        //记录文件名,最近一次
                        CLDC_DataCore.Function.File.WriteInIString(StrIniFile, "System", "LastNTmp", StrLastTmpFileName);
                        //记录文件名
                        CLDC_DataCore.Function.File.WriteInIString(StrIniFile, "System", "LastTmp", StrTmpFileName);
                        //删掉上一次的文件
                        if (strNCanDel != string.Empty)
                        {
                            strNCanDel = CLDC_DataCore.Function.File.GetPhyPath(@"\Tmp\" + strNCanDel);
                            if (System.IO.File.Exists(strNCanDel))
                            {
                                System.IO.File.Delete(strNCanDel);
                            }
                        }
                    }
                }
            }
            catch { }
        }


        /// <summary>
        /// 读取序列化的临时文件
        /// </summary>
        /// <returns>返回一个对象</returns>
        public DnbGroupInfo LoadTmpData()
        {

            string StrIniFile = @"\Tmp\tmp.ini";
            string StrTmpFileName = CLDC_DataCore.Function.File.ReadInIString(StrIniFile, "System", "LastTmp", "");

            byte[] _TmpByte = CLDC_DataCore.Function.File.ReadFileData("\\Tmp\\" + StrTmpFileName);
            if (_TmpByte.Length == 1)
                return this;
            try
            {
                return (DnbGroupInfo)CLDC_CTNProtocol.CTNPCommand.GetObject(_TmpByte);
            }
            catch
            {
                try
                {
                    StrTmpFileName = CLDC_DataCore.Function.File.ReadInIString(StrIniFile, "System", "LastNTmp", "");
                    _TmpByte = Function.File.ReadFileData("\\Tmp\\" + StrTmpFileName);
                    return (DnbGroupInfo)CLDC_CTNProtocol.CTNPCommand.GetObject(_TmpByte);
                }
                catch
                {
                    try
                    {
                        StrTmpFileName = CLDC_DataCore.Function.File.ReadInIString(StrIniFile, "System", "LastNDTmp", "");
                        _TmpByte = Function.File.ReadFileData("\\Tmp\\" + StrTmpFileName);
                        return (DnbGroupInfo)CLDC_CTNProtocol.CTNPCommand.GetObject(_TmpByte);
                    }
                    catch
                    {
                        return this;
                    }
                }
            }
        }
        #endregion

        #region 老数据库操作

        /// <summary>
        /// 存入本地ACCESS数据库
        /// </summary>
        /// <param name="AccessPath">数据库路径</param>
        /// <param name="ErrString">错误返回字符串</param>
        /// <returns></returns>
        public bool SaveToDataBase(string AccessPath, out string ErrString)
        {
            return this.SaveToDataBase(AccessPath, "", "", "", out ErrString);
        }
        /// <summary>
        /// 存入SQL服务器
        /// </summary>
        /// <param name="Ip">SQL服务器地址</param>
        /// <param name="UserName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="ErrString">错误返回字符串</param>
        /// <returns></returns>
        public bool SaveToDataBase(string Ip, string UserName, string pwd, out string ErrString)
        {
            return this.SaveToDataBase("", Ip, UserName, pwd, out ErrString);
        }


        /// <summary>
        /// 存储本地默认ACCESS数据库
        /// </summary>
        /// <param name="ErrString">错误返回</param>
        /// <returns></returns>
        public bool SaveToDataBase(out string ErrString)
        {
            return this.SaveToDataBase("", "", "", "", out ErrString);
        }

        /// <summary>
        /// 存储数据到数据库
        /// </summary>
        /// <param name="AccessPath">本地存储路径，填写本地存储后不能填写服务器IP</param>
        /// <param name="Ip">服务器IP地址，填写服务器IP地址后表示服务器存储，不需要填写本地存储</param>
        /// <param name="UserName">服务器登陆用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="ErrString">错误返回</param>
        /// <returns>成功失败</returns>
        private bool SaveToDataBase(string AccessPath, string Ip, string UserName, string pwd, out string ErrString)
        {
            //string _ErrString = "";
            ErrString = "";
            return true;
            #region 老数据库
            /*
            CLDC_DataCore.Const.GlobalUnit.Logger.Debug("Calling SaveToDataBase:");

            CLDC_DataCore.DataBase.DataControl _Data;

            if (Ip != "")    //构造连接SQL服务器
                _Data = new CLDC_DataCore.DataBase.DataControl(Ip, UserName, pwd);
            else if (AccessPath != "")  //构造连接本地ACCESS数据库
                _Data = new CLDC_DataCore.DataBase.DataControl(AccessPath,true);
            else
                _Data = new CLDC_DataCore.DataBase.DataControl();        //构造连接本地默认ACCESS数据库

            if (!_Data.Connection)
            {
                ErrString = "数据库连接失败";
                return false;
            }

            for (int Int_I = 0; Int_I < this.MeterGroup.Count; Int_I++)
            {

                List<string> _InsertDataBaseSQL = new List<string>();

                #region-----------插入电能表基本数据------------------

                MeterBasicInfo _BasicInfo = MeterGroup[Int_I];
                if (_BasicInfo.YaoJianYn == false)      ///如果不是要检的表则不存档
                {
                    _BasicInfo = null;
                    continue;
                }

                long _MyID = _Data.ReadMaxAutoID(out _ErrString);  ///表唯一关键字ID
                _BasicInfo._intMyId = _MyID;

                string _TmpSql = "";
                if (Ip != "")
                    _TmpSql = "Insert Into MeterBasicInfo values(" + _BasicInfo._intMyId.ToString() + ",'"
                                                                     + _BasicInfo.Mb_TaiID.ToString() + "',"
                                                                     + _BasicInfo.Mb_intBno.ToString() + ",'"
                                                                     + _BasicInfo.Mb_ChrJlbh + "','"
                                                                     + _BasicInfo.Mb_ChrCcbh + "','"
                                                                     + _BasicInfo.Mb_ChrTxm + "','"
                                                                     + _BasicInfo.Mb_chrAddr + "','"
                                                                     + _BasicInfo.Mb_chrzzcj + "','"
                                                                     + _BasicInfo.Mb_Bxh + "','"
                                                                     + _BasicInfo.Mb_chrBcs + "','"
                                                                     + _BasicInfo.Mb_chrBlx + "','"
                                                                     + _BasicInfo.Mb_chrBdj + "','"
                                                                     + _BasicInfo.Mb_gygy.ToString() + "','"
                                                                     + _BasicInfo.Mb_chrCcrq + "','"
                                                                     + _BasicInfo.Mb_CHRSjdw + "','"
                                                                     + _BasicInfo.Mb_chrZsbh + "','"
                                                                     + _BasicInfo.Mb_ChrBmc + "',"
                                                                     + _BasicInfo.Mb_intClfs.ToString() + ",'"
                                                                     + _BasicInfo.Mb_chrUb + "','"
                                                                     + _BasicInfo.Mb_chrIb + "','"
                                                                     + _BasicInfo.Mb_chrHz + "',"
                                                                     + (_BasicInfo.Mb_BlnZnq == true ? "1" : "0") + ","
                                                                     + (_BasicInfo.Mb_BlnHgq == true ? "1" : "0") + ",'"
                                                                     + _BasicInfo.Mb_chrTestType + "',#"
                                                                     + _BasicInfo.Mb_DatJdrq + "#,#"
                                                                     + _BasicInfo.Mb_Datjjrq + "#,'"
                                                                     + _BasicInfo.Mb_chrWd + "','"
                                                                     + _BasicInfo.Mb_chrSd + "','"
                                                                     + _BasicInfo.Mb_chrResult + "','"
                                                                     + _BasicInfo.Mb_ChrJyy + "','"
                                                                     + _BasicInfo.Mb_ChrHyy + "','"
                                                                     + _BasicInfo.Mb_chrZhuGuan + "',"
                                                                     + (_BasicInfo.Mb_BlnToServer == true ? "1" : "0") + ","
                                                                     + (_BasicInfo.Mb_BlnToMis == true ? "1" : "0") + ",'"
                                                                     + _BasicInfo.Mb_chrQianFeng1 + "','"
                                                                     + _BasicInfo.Mb_chrQianFeng2 + "','"
                                                                     + _BasicInfo.Mb_chrQianFeng3 + "','"
                                                                     + _BasicInfo.Mb_chrOther1 + "','"
                                                                     + _BasicInfo.Mb_chrOther2 + "','"
                                                                     + _BasicInfo.Mb_chrOther3 + "','"
                                                                     + _BasicInfo.Mb_chrOther4 + "','"
                                                                     + _BasicInfo.Mb_chrOther5 + "')";
                else
                    _TmpSql = "Insert Into MeterBasicInfo values(" + _BasicInfo._intMyId.ToString() + ","
                                                                    + _BasicInfo.Mb_TaiID.ToString() + ","
                                                                    + _BasicInfo.Mb_intBno.ToString() + ",'"
                                                                    + _BasicInfo.Mb_ChrJlbh + "','"
                                                                    + _BasicInfo.Mb_ChrCcbh + "','"
                                                                    + _BasicInfo.Mb_ChrTxm + "','"
                                                                    + _BasicInfo.Mb_chrAddr + "','"
                                                                    + _BasicInfo.Mb_chrzzcj + "','"
                                                                    + _BasicInfo.Mb_Bxh + "','"
                                                                    + _BasicInfo.Mb_chrBcs + "','"
                                                                    + _BasicInfo.Mb_chrBlx + "','"
                                                                    + _BasicInfo.Mb_chrBdj + "','"
                                                                     + _BasicInfo.Mb_gygy.ToString() + "','"
                                                                    + _BasicInfo.Mb_chrCcrq + "','"
                                                                    + _BasicInfo.Mb_CHRSjdw + "','"
                                                                    + _BasicInfo.Mb_chrZsbh + "','"
                                                                    + _BasicInfo.Mb_ChrBmc + "',"
                                                                    + _BasicInfo.Mb_intClfs.ToString() + ",'"
                                                                    + _BasicInfo.Mb_chrUb + "','"
                                                                    + _BasicInfo.Mb_chrIb + "','"
                                                                    + _BasicInfo.Mb_chrHz + "',"
                                                                    + (_BasicInfo.Mb_BlnZnq == true ? "1" : "0") + ","
                                                                    + (_BasicInfo.Mb_BlnHgq == true ? "1" : "0") + ",'"
                                                                    + _BasicInfo.Mb_chrTestType + "',#"
                                                                    + _BasicInfo.Mb_DatJdrq + "#,#"
                                                                    + _BasicInfo.Mb_Datjjrq + "#,'"
                                                                    + _BasicInfo.Mb_chrWd + "','"
                                                                    + _BasicInfo.Mb_chrSd + "','"
                                                                    + _BasicInfo.Mb_chrResult + "','"
                                                                    + _BasicInfo.Mb_ChrJyy + "','"
                                                                    + _BasicInfo.Mb_ChrHyy + "','"
                                                                    + _BasicInfo.Mb_chrZhuGuan + "',"
                                                                    + (_BasicInfo.Mb_BlnToServer == true ? "1" : "0") + ","
                                                                    + (_BasicInfo.Mb_BlnToMis == true ? "1" : "0") + ",'"
                                                                    + _BasicInfo.Mb_chrQianFeng1 + "','"
                                                                    + _BasicInfo.Mb_chrQianFeng2 + "','"
                                                                    + _BasicInfo.Mb_chrQianFeng3 + "','"
                                                                    + _BasicInfo.Mb_chrOther1 + "','"
                                                                    + _BasicInfo.Mb_chrOther2 + "','"
                                                                    + _BasicInfo.Mb_chrOther3 + "','"
                                                                    + _BasicInfo.Mb_chrOther4 + "','"
                                                                    + _BasicInfo.Mb_chrOther5 + "')";
                _InsertDataBaseSQL.Add(_TmpSql);
                _BasicInfo = null;
                #endregion

                #region-------------插入电能表基本数据扩展表-------------

                if (MeterGroup[Int_I].MeterExtend.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterExtend.Keys)
                    {
                        if (Ip != "")
                        {
                            _TmpSql = string.Format("INSERT INTO MeterExtend Values({0},'{1}','{2}','{3}')"
                                                    , _MyID                         //唯一ID值
                                                    , _TaiID.ToString()             //台编号
                                                    , _Key                          //扩充标志
                                                    , MeterGroup[Int_I].MeterExtend[_Key]);         //扩充标志值
                        }
                        else
                        {
                            _TmpSql = string.Format("INSERT INTO MeterExtend Values({0},'{1}','{2}')"
                                                    , _MyID                         //唯一ID值
                                                    , _Key                          //扩充标志
                                                    , MeterGroup[Int_I].MeterExtend[_Key]);         //扩充标志值
                        }

                        _InsertDataBaseSQL.Add(_TmpSql);
                    }
                }
                else
                {
                    CLDC_DataCore.Const.GlobalUnit.Logger.Debug("没有扩展数据");
                }


                #endregion

                #region---------插入结论数据表SQL语句----------------
                if (MeterGroup[Int_I].MeterResults.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterResults.Keys)
                    {
                        MeterResult _Result = MeterGroup[Int_I].MeterResults[_Key];
                        _Result.Mr_lngMyID = _MyID;
                        if (Ip != "")
                            _TmpSql = "Insert into MeterResult Values(" + _Result.Mr_lngMyID.ToString() + ",'"
                                                                    + _TaiID.ToString() + "','"
                                                                    + _Result.Mr_PrjID + "','"
                                                                    + _Result.Mr_PrjName + "','"
                                                                    + _Result.Mr_Result + "','"
                                                                    + _Result.Mr_Time + "','"
                                                                    + _Result.Mr_Current + "')";
                        else
                            _TmpSql = "Insert into MeterResult Values(" + _Result.Mr_lngMyID.ToString() + ",'"
                                                                    + _Result.Mr_PrjID + "','"
                                                                    + _Result.Mr_PrjName + "','"
                                                                    + _Result.Mr_Result + "','"
                                                                    + _Result.Mr_Time + "','"
                                                                    + _Result.Mr_Current + "')";

                        //_TmpSql = _TmpSql.Replace('\n', '').Replace('\r', '');
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Result = null;
                    }
                }
                #endregion

                #region---------插入误差数据表SQL语句------------
                if (MeterGroup[Int_I].MeterErrors.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterErrors.Keys)
                    {
                        MeterError _Error = MeterGroup[Int_I].MeterErrors[_Key];
                        _Error.Me_lngMyID = _MyID;
                        if (Ip != "")
                            _TmpSql = "Insert into MeterError(Me_LngMyID,Me_TaiID,Me_PrjId,Me_PrjName,Me_Result,Me_Glys,Me_xib,Me_xU,Me_WcLimit,Me_Qs,Me_Pl,Me_Wc)"
                                                            + "Values(" + _Error.Me_lngMyID.ToString() + ",'" + _TaiID.ToString() + "','" + _Error.Me_PrjID + "','"
                                                            + _Error.Me_PrjName + "','" + _Error.Me_Result + "','"
                                                            + _Error.Me_Glys + "','" + _Error.Me_xib + "','"
                                                            + _Error.Me_xU + "','" + _Error.Me_WcLimit + "',"
                                                            + _Error.Me_Qs.ToString() + ",'" + _Error.Me_PL + "','"
                                                            + _Error.Me_Wc + "')";
                        else
                            _TmpSql = "Insert into MeterError(Me_LngMyID,Me_PrjId,Me_PrjName,Me_Result,Me_Glys,Me_xib,Me_xU,Me_WcLimit,Me_Qs,Me_Pl,Me_Wc)"
                                                            + "Values(" + _Error.Me_lngMyID.ToString() + ",'" + _Error.Me_PrjID + "','"
                                                            + _Error.Me_PrjName + "','" + _Error.Me_Result + "','"
                                                            + _Error.Me_Glys + "','" + _Error.Me_xib + "','"
                                                            + _Error.Me_xU + "','" + _Error.Me_WcLimit + "',"
                                                            + _Error.Me_Qs.ToString() + ",'" + _Error.Me_PL + "','"
                                                            + _Error.Me_Wc + "')";
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Error = null;
                    }
                }
                #endregion

                #region---------插入多功能试验数据表SQL语句----------
                if (MeterGroup[Int_I].MeterDgns.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterDgns.Keys)
                    {
                        MeterDgn _Dgn = MeterGroup[Int_I].MeterDgns[_Key];
                        _Dgn.Md_lngMyID = _MyID;
                        if (Ip != "")
                            _TmpSql = "Insert into MeterDgn Values(" + _Dgn.Md_lngMyID.ToString() + ",'"
                                                                 + _TaiID.ToString() + "','"
                                                                 + _Dgn.Md_PrjID + "','"
                                                                 + _Dgn.Md_PrjName + "','"
                                                                 + _Dgn.Md_chrValue + "')";
                        else
                            _TmpSql = "Insert into MeterDgn Values(" + _Dgn.Md_lngMyID.ToString() + ",'"
                                                                 + _Dgn.Md_PrjID + "','"
                                                                 + _Dgn.Md_PrjName + "','"
                                                                 + _Dgn.Md_chrValue + "')";
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Dgn = null;
                    }
                }
                #endregion

                #region---------插入载波试验试验数据表SQL语句----------
                if (MeterGroup[Int_I].MeterCarrierDatas.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterCarrierDatas.Keys)
                    {
                        MeterCarrierData _carrier = MeterGroup[Int_I].MeterCarrierDatas[_Key];
                        _carrier.Mce_lngMyID = _MyID;
                        if (Ip != "")
                            _TmpSql = "Insert into MeterCarrier Values(" + _carrier.Mce_lngMyID.ToString() + ",'"
                                                                 + _TaiID.ToString() + "','"
                                                                 + _carrier.Mce_PrjID + "','"
                                                                 + _carrier.Mce_PrjName + "','"
                                                                 + _carrier.Mce_PrjValue + "')";
                        else
                            _TmpSql = "Insert into MeterCarrier Values(" + _carrier.Mce_lngMyID.ToString() + ",'"
                                                                 + _carrier.Mce_PrjID + "','"
                                                                 + _carrier.Mce_PrjName + "','"
                                                                 + _carrier.Mce_PrjValue + "')";
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _carrier = null;
                    }
                }
                #endregion

                #region-------插入走字误差数据表SQL语句----------------------
                if (MeterGroup[Int_I].MeterZZErrors.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterZZErrors.Keys)
                    {
                        MeterZZError _ZZError = MeterGroup[Int_I].MeterZZErrors[_Key];
                        _ZZError.Mz_lngMyID = _MyID;
                        if (Ip != "")
                            _TmpSql = "Insert into MeterZZData(Mz_lngMyID,Mz_TaiID,Mz_PrjID,Mz_StartTime" +
                                                          ",Mz_xIb,Mz_Glys,Mz_Start,Mz_End" +
                                                          ",Mz_Diff,Mz_Wc,Mz_Result,Mz_U,Mz_i) values("
                                                                    + _ZZError.Mz_lngMyID.ToString() + ",'"
                                                                    + _TaiID.ToString() + "','"
                                                                    + _ZZError.Mz_PrjID + "','"
                                                                    + _ZZError.Mz_StartTime + "','"
                                                                    + _ZZError.Mz_xIb + "','"
                                                                    + _ZZError.Mz_Glys + "','"
                                                                    + _ZZError.Mz_Start.ToString() + "','"
                                                                    + _ZZError.Mz_End.ToString() + "','"
                                                                    + _ZZError.Mz_Diff + "','"
                                                                    + _ZZError.Mz_Wc + "','"
                                                                    + _ZZError.Mz_Result + "','"
                                                                    + _ZZError.Mz_U + "','"
                                                                    + _ZZError.Mz_i + "')";
                        else
                            _TmpSql = "Insert into MeterZZData(Mz_lngMyID,Mz_PrjID,Mz_StartTime" +
                                                          ",Mz_xIb,Mz_Glys,Mz_Start,Mz_End" +
                                                          ",Mz_Diff,Mz_Wc,Mz_Result,Mz_U,Mz_i) values("
                                                                    + _ZZError.Mz_lngMyID.ToString() + ",'"
                                                                    + _ZZError.Mz_PrjID + "','"
                                                                    + _ZZError.Mz_StartTime + "','"
                                                                    + _ZZError.Mz_xIb + "','"
                                                                    + _ZZError.Mz_Glys + "','"
                                                                    + _ZZError.Mz_Start.ToString() + "','"
                                                                    + _ZZError.Mz_End.ToString() + "','"
                                                                    + _ZZError.Mz_Diff + "','"
                                                                    + _ZZError.Mz_Wc + "','"
                                                                    + _ZZError.Mz_Result + "','"
                                                                    + _ZZError.Mz_U + "','"
                                                                    + _ZZError.Mz_i + "')";

                        _InsertDataBaseSQL.Add(_TmpSql);
                        _ZZError = null;
                    }
                }
                #endregion

                #region---------误差一致性检定数据--------------------

                if (MeterGroup[Int_I].MeterErrAccords != null && MeterGroup[Int_I].MeterErrAccords.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterErrAccords.Keys)
                    {
                        MeterErrAccord _ErrAccord = MeterGroup[Int_I].MeterErrAccords[_Key];
                        _ErrAccord.Mea_lngMyID = _MyID;
                        foreach (string _subKey in _ErrAccord.lstTestPoint.Keys)
                        {
                            MeterErrAccordBase _ErrBase = _ErrAccord.lstTestPoint[_subKey];

                            if (Ip != "")
                            {
                                _TmpSql = string.Format("Insert into MeterErrAccord(Mea_lngMyID,Mea_TaiID,Mea_PrjName,Mea_Result," +
                                                                    "Mea_Glfx,Mea_Ub,Mea_Ib,Mea_Pl," +
                                                                    "Mea_WcLimit,Mea_Qs,Mea_Wc1,Mea_Wc2,Mea_Wc)" +
                                                                    "VALUES({0},'{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}',{10},'{11}','{12}','{13}')"
                                                                    , _ErrAccord.Mea_lngMyID.ToString()
                                                                    , _TaiID.ToString()
                                                                    , _ErrBase.Mea_PrjName
                                                                    , _ErrAccord.Mea_Result
                                                                    , _ErrBase.Mea_Glys.ToString()
                                                                    , _ErrBase.Mea_xU
                                                                    , _ErrBase.Mea_xib
                                                                    , _ErrBase.Mea_PL
                                                                    , _ErrBase.Mea_WcLimit
                                                                    , _ErrBase.Mea_Qs.ToString()
                                                                    , _ErrBase.Mea_Wc1
                                                                    , _ErrBase.Mea_Wc2                                                                 
                                                                    , _ErrBase.Mea_Wc);

                            }
                            else
                            {
                                _TmpSql = string.Format("Insert into MeterErrAccord(Mea_lngMyID,Mea_PrjName,Mea_Result," +
                                                                    "Mea_Glfx,Mea_Ub,Mea_Ib,Mea_Pl," +
                                                                    "Mea_WcLimit,Mea_Qs,Mea_Wc1,Mea_Wc2,Mea_Wc)" +
                                                                    "VALUES({0},'{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}',{9},'{10}','{11}','{12}')"
                                                                    , _ErrAccord.Mea_lngMyID.ToString()
                                                                    , _TaiID.ToString()
                                                                    , _ErrBase.Mea_PrjName
                                                                    , _ErrAccord.Mea_Result
                                                                    , _ErrBase.Mea_Glys.ToString()
                                                                    , _ErrBase.Mea_xU
                                                                    , _ErrBase.Mea_xib
                                                                    , _ErrBase.Mea_PL
                                                                    , _ErrBase.Mea_WcLimit
                                                                    , _ErrBase.Mea_Qs.ToString()
                                                                    , _ErrBase.Mea_Wc1
                                                                    , _ErrBase.Mea_Wc2
                                                                    , _ErrBase.Mea_Wc);
                            }

                            _InsertDataBaseSQL.Add(_TmpSql);
                        }
                        _ErrAccord = null;
                    }

                }
                #endregion

                #region---------特殊检定数据--------------------

                if (MeterGroup[Int_I].MeterSpecialErrs != null && MeterGroup[Int_I].MeterSpecialErrs.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterSpecialErrs.Keys)
                    {
                        MeterSpecialErr _SpErr = MeterGroup[Int_I].MeterSpecialErrs[_Key];
                        _SpErr.Mse_LngMyID = _MyID;
                        if (Ip != "")
                        {
                            _TmpSql = string.Format("Insert into MeterSpecialErr(Mse_lngMyID,Mse_TaiID,Mse_PrjName,Mse_Result," +
                                                                "Mse_Glfx,Mse_Ub,Mse_Ib,Mse_Phase,Mse_Pl," +
                                                                "Mse_Nxx,Mse_XieBo,Mse_WcLimit,Mse_Qs,Mse_Wc)" +
                                                                "VALUES({0},'{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}',{9},{10},'{11}',{12},'{13}')"
                                                                , _SpErr.Mse_LngMyID.ToString()
                                                                , _TaiID.ToString()
                                                                , _SpErr.Mse_PrjName
                                                                , _SpErr.Mse_Result
                                                                , _SpErr.Mse_Glfx.ToString()
                                                                , _SpErr.Mse_Ub
                                                                , _SpErr.Mse_Ib
                                                                , _SpErr.Mse_Phase
                                                                , _SpErr.Mse_Pl
                                                                , _SpErr.Mse_Nxx.ToString()
                                                                , _SpErr.Mse_XieBo.ToString()
                                                                , _SpErr.Mse_WcLimit
                                                                , _SpErr.Mse_Qs.ToString()
                                                                , _SpErr.Mse_Wc);

                        }
                        else
                        {
                            _TmpSql = string.Format("Insert into MeterSpecialErr(Mse_lngMyID,Mse_PrjName,Mse_Result," +
                                                                "Mse_Glfx,Mse_Ub,Mse_Ib,Mse_Phase,Mse_Pl," +
                                                                "Mse_Nxx,Mse_XieBo,Mse_WcLimit,Mse_Qs,Mse_Wc)" +
                                                                "VALUES({0},'{1}','{2}',{3},'{4}','{5}','{6}','{7}',{8},{9},'{10}',{11},'{12}')"
                                                                , _SpErr.Mse_LngMyID.ToString()
                                                                , _SpErr.Mse_PrjName
                                                                , _SpErr.Mse_Result
                                                                , _SpErr.Mse_Glfx.ToString()
                                                                , _SpErr.Mse_Ub
                                                                , _SpErr.Mse_Ib
                                                                , _SpErr.Mse_Phase
                                                                , _SpErr.Mse_Pl
                                                                , _SpErr.Mse_Nxx.ToString()
                                                                , _SpErr.Mse_XieBo.ToString()
                                                                , _SpErr.Mse_WcLimit
                                                                , _SpErr.Mse_Qs.ToString()
                                                                , _SpErr.Mse_Wc);
                        }

                        _InsertDataBaseSQL.Add(_TmpSql);
                        _SpErr = null;
                    }

                }
                #endregion

                string _TmpString = "";

                bool _Return = _Data.SaveData(_InsertDataBaseSQL, out _TmpString);

                _ErrString += _TmpString;

                _InsertDataBaseSQL = null;
                _Data.CloseDB();
                CLDC_DataCore.Const.GlobalUnit.Logger.Debug("End Calling:SaveToDataBase");
            }

            _Data = null;

            if (_ErrString != "")
            {
                ErrString = _ErrString;
                return false;
            }
            else
            {
                ErrString = "";
                //Init();
                return true;
            }
            **/
            #endregion
        }

        #endregion

        #region --------------------------新方案加载后对当前数据进行核对----------------------
        /// <summary>
        /// 进行方案比较，同时检查数据
        /// </summary>
        /// <param name="DnbGroup"></param>
        public void CheckFAChangeAndDataRefrash()
        {
            int int_Bwh = GetFirstYaoJianMeterBwh();
            if (int_Bwh == -1) return;
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = GetMeterBasicInfoByBwh(int_Bwh + 1);

            string[] TmpKeys;

            #region -------------------启动、潜动项目数据和新方案检查更新------------------------
            if (MeterInfo.MeterResults.Count > 0)
            {
                TmpKeys = new string[MeterInfo.MeterResults.Count];

                MeterInfo.MeterResults.Keys.CopyTo(TmpKeys, 0);          //将结论关键字集拷贝到一个数组中，便于后续操作

                foreach (string Key in TmpKeys)
                {
                    if (Key.Length == 4 && Key.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString())  //启动
                    {
                        bool IsFind = false;          //是否找到节点的标志
                        for (int i = 0; i < CheckPlan.Count; i++)          //循环当前方案
                        {
                            if (CheckPlan[i] is StPlan_QiDong)      //如果当前方案项目是启动    
                            {
                                if ((int)((StPlan_QiDong)CheckPlan[i]).PowerFangXiang == int.Parse(Key.Substring(3)))      //如果当前方案项目和已存在的数据项目吻合，则表示找到对应节点
                                {
                                    IsFind = true;
                                    break;
                                }
                            }
                        }
                        if (!IsFind)            //如果没找到对应节点，就要删除当前检定过的数据节点
                        {
                            for (int i = 0; i < MeterGroup.Count; i++)
                            {
                                if (MeterGroup[i].YaoJianYn)
                                {
                                    if (MeterGroup[i].MeterResults.ContainsKey(Key))
                                    {
                                        MeterGroup[i].MeterResults.Remove(Key);
                                    }
                                }
                            }
                        }
                    }

                    if (Key.Length == 4 && Key.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString())      //潜动
                    {
                        bool IsFind = false;          //是否找到节点的标志
                        for (int i = 0; i < CheckPlan.Count; i++)          //循环当前方案
                        {
                            if (CheckPlan[i] is StPlan_QianDong)      //如果当前方案项目是启动    
                            {
                                if ((int)((StPlan_QianDong)CheckPlan[i]).PowerFangXiang == int.Parse(Key.Substring(3)))      //如果当前方案项目和已存在的数据项目吻合，则表示找到对应节点
                                {
                                    IsFind = true;
                                    break;
                                }
                            }
                        }
                        if (!IsFind)            //如果没找到对应节点，就要删除当前检定过的数据节点
                        {
                            for (int i = 0; i < MeterGroup.Count; i++)
                            {
                                if (MeterGroup[i].YaoJianYn)
                                {
                                    if (MeterGroup[i].MeterResults.ContainsKey(Key))
                                    {
                                        MeterGroup[i].MeterResults.Remove(Key);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (MeterInfo.MeterResults.Count > 0)
            {
                TmpKeys = new string[MeterInfo.MeterResults.Count];
                MeterInfo.MeterResults.Keys.CopyTo(TmpKeys, 0);     //将结论关键字集拷贝到一个数组中，便于后续操作
                foreach (string Key in TmpKeys)         //再次遍历，看是否存在有总结点，没有子节点的情况，如果存在就应该删除总结点，比如：存在109，但是不存在109（1-4）
                {
                    if (Key == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString())  //如果存在启动总结论
                    {
                        bool IsFind = false;        //分结论是否存在标志
                        for (int i = 1; i <= 4; i++)
                        {
                            if (MeterInfo.MeterResults.ContainsKey(Key + i.ToString()))         //如果找到分结论则跳出
                            {
                                IsFind = true;
                                break;
                            }
                        }
                        if (!IsFind)            //如果没有分结论，则开始查找并删除所有要检表的总结论
                        {
                            for (int i = 0; i < MeterGroup.Count; i++)
                            {
                                if (MeterGroup[i].YaoJianYn)
                                {
                                    if (MeterGroup[i].MeterResults.ContainsKey(Key))
                                    {
                                        MeterGroup[i].MeterResults.Remove(Key);
                                    }
                                }
                            }
                        }
                    }
                    if (Key == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString())  //如果存在潜动总结论
                    {
                        bool IsFind = false;         //分结论是否存在标志
                        for (int i = 1; i <= 4; i++)
                        {
                            if (MeterInfo.MeterResults.ContainsKey(Key + i.ToString())) //如果找到分结论则跳出
                            {
                                IsFind = true;
                                break;
                            }
                        }
                        if (!IsFind)        //如果没有分结论，则开始查找并删除所有要检表的总结论
                        {
                            for (int i = 0; i < MeterGroup.Count; i++)
                            {
                                if (MeterGroup[i].YaoJianYn)
                                {
                                    if (MeterGroup[i].MeterResults.ContainsKey(Key))
                                    {
                                        MeterGroup[i].MeterResults.Remove(Key);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region -------------------------基本误差数据和新方案检查更新---------------------

            #region ----------------------------清理误差数据表的多余数据------------------------
            if (MeterInfo.MeterErrors.Count > 0)
            {
                TmpKeys = new string[MeterInfo.MeterErrors.Count];

                MeterInfo.MeterErrors.Keys.CopyTo(TmpKeys, 0);

                foreach (string Key in TmpKeys)         //检查当前误差数据集关键字数组
                {
                    bool IsFind = false;            //是否找到标志

                    for (int i = 0; i < CheckPlan.Count; i++)          //循环方案
                    {
                        if (CheckPlan[i] is StPlan_WcPoint)        //如果当前方案项目是误差结构体
                        {
                            if (Key == ((StPlan_WcPoint)CheckPlan[i]).PrjID)       //如果当前方案项目在误差数据集里面存在
                            {
                                IsFind = true;
                                break;
                            }
                        }
                    }
                    if (!IsFind)                    //如果循环了方案都还找不到，即表示当前方案中不检改点，需要删除该负载点数据
                    {
                        for (int i = 0; i < MeterGroup.Count; i++)
                        {
                            if (MeterGroup[i].YaoJianYn)
                            {
                                if (MeterGroup[i].MeterErrors.Count > 0)
                                {
                                    try
                                    {
                                        MeterGroup[i].MeterErrors.Remove(Key);      //从数据集合冲移除该负载点数据
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region ------------------------清理结论数据表中的多余数据----------------------
            if (MeterInfo.MeterResults.Count > 0)      //清理结论表
            {
                if (MeterInfo.MeterErrors.Count == 0)       //如果误差数据集合没有数据
                {
                    #region ---------------没有误差数据，清理所有误差相关结论------------------------
                    for (int Bwh = 0; Bwh < MeterGroup.Count; Bwh++)
                    {
                        if (MeterGroup[Bwh].YaoJianYn)
                        {
                            for (int i = 1; i <= 4; i++)            //循环功率方向ID,,下面全部做容错，这样比用ContainsKey方法要快很多
                            {
                                try
                                {
                                    MeterGroup[Bwh].MeterResults.Remove(string.Format("{0}{1}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验, i));
                                }
                                catch { }
                                try
                                {
                                    MeterGroup[Bwh].MeterResults.Remove(string.Format("{0}{1}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差, i));
                                }
                                catch { }
                            }
                            try
                            {
                                MeterGroup[Bwh].MeterResults.Remove(string.Format("{0}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差));
                            }
                            catch { }
                            try
                            {
                                MeterGroup[Bwh].MeterResults.Remove(string.Format("{0}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验));
                            }
                            catch { }
                            try
                            {
                                MeterGroup[Bwh].MeterResults.Remove(string.Format("{0}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差));
                            }
                            catch { }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region --------------------清理分项数据-------------------------------
                    TmpKeys = new string[MeterInfo.MeterResults.Count];
                    MeterInfo.MeterResults.Keys.CopyTo(TmpKeys, 0);
                    foreach (string Key in TmpKeys)
                    {
                        if (Key.Length == 4 && Key.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString())
                        {
                            bool BlnFind = false;
                            foreach (string ErrKey in MeterInfo.MeterErrors.Keys)
                            {
                                if (ErrKey.Substring(0, 1) == ((int)CLDC_Comm.Enum.Cus_WcType.基本误差).ToString()
                                    && ErrKey.Substring(1, 1) == Key.Substring(3, 1))          //基本误差功率方向
                                {
                                    BlnFind = true;
                                    break;
                                }
                            }
                            if (!BlnFind)           //没找到，则删除该结论
                            {
                                for (int i = 0; i < MeterGroup.Count; i++)
                                {
                                    if (MeterGroup[i].YaoJianYn)
                                    {
                                        try
                                        {
                                            MeterGroup[i].MeterResults.Remove(Key);
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                        if (Key.Length == 4 && Key.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString())
                        {
                            bool BlnFind = false;
                            foreach (string ErrKey in MeterInfo.MeterErrors.Keys)
                            {
                                if (ErrKey.Substring(0, 1) == ((int)CLDC_Comm.Enum.Cus_WcType.标准偏差).ToString()
                                    && ErrKey.Substring(1, 1) == Key.Substring(3, 1))          //标准偏差功率方向
                                {
                                    BlnFind = true;
                                    break;
                                }
                            }
                            if (!BlnFind)           //没找到，则删除该结论
                            {
                                for (int i = 0; i < MeterGroup.Count; i++)
                                {
                                    if (MeterGroup[i].YaoJianYn)
                                    {
                                        try
                                        {
                                            MeterGroup[i].MeterResults.Remove(Key);
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                    }

                    if (MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString()))
                    {
                        bool IsFind = false;
                        for (int i = 1; i <= 4; i++)
                        {
                            if (MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString() + i.ToString()))
                            {
                                IsFind = true;
                                break;
                            }
                        }
                        if (!IsFind)
                        {
                            for (int i = 0; i < MeterGroup.Count; i++)
                            {
                                try
                                {
                                    MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.标准偏差).ToString());
                                }
                                catch { }
                                try
                                {
                                    MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.最大偏差).ToString());
                                }
                                catch { }
                            }
                        }
                    }
                    if (MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString()))
                    {
                        bool IsFind = false;
                        for (int i = 1; i <= 4; i++)
                        {
                            if (MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString() + i.ToString()))
                            {
                                IsFind = true;
                                break;
                            }
                        }
                        if (!IsFind)
                        {
                            for (int i = 0; i < MeterGroup.Count; i++)
                            {
                                try
                                {
                                    MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString());
                                }
                                catch { }
                            }
                        }
                    }


                    #endregion
                }

            }
            #endregion
            #endregion

            #region ----------------多功能数据和信方案检查更新-----------------------
            if (MeterInfo.MeterDgns.Count > 0)
            {
                TmpKeys = new string[MeterInfo.MeterDgns.Count];
                MeterInfo.MeterDgns.Keys.CopyTo(TmpKeys, 0);

                foreach (string Key in TmpKeys)
                {
                    bool BlnFind = false;
                    for (int i = 0; i < CheckPlan.Count; i++)
                    {
                        if (CheckPlan[i] is CLDC_DataCore.Struct.StPlan_Dgn)
                        {
                            if (Key.IndexOf(((CLDC_DataCore.Struct.StPlan_Dgn)CheckPlan[i]).DgnPrjID) == 0)
                            {
                                BlnFind = true;
                                break;
                            }
                        }
                    }
                    if (!BlnFind)
                    {
                        for (int i = 0; i < MeterGroup.Count; i++)
                        {
                            if (MeterGroup[i].YaoJianYn)
                            {
                                try
                                {
                                    MeterGroup[i].MeterDgns.Remove(Key);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            else if (MeterInfo.MeterDgns.Count == 0)
            {
                for (int i = 0; i < MeterGroup.Count; i++)
                {
                    if (MeterGroup[i].YaoJianYn)
                    {
                        try
                        {
                            MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.多功能试验).ToString());
                        }
                        catch { }
                    }
                }
            }
            #endregion

            #region -----------------------载波数据和新方案检查更新----------------------
            if (MeterInfo.MeterCarrierDatas.Count > 0)
            {
                TmpKeys = new string[MeterInfo.MeterCarrierDatas.Count];
                MeterInfo.MeterCarrierDatas.Keys.CopyTo(TmpKeys, 0);

                foreach (string Key in TmpKeys)
                {
                    bool BlnFind = false;
                    for (int i = 0; i < CheckPlan.Count; i++)
                    {
                        if (CheckPlan[i] is StPlan_Carrier)
                        {
                            if (Key == ((StPlan_Carrier)CheckPlan[i]).str_PrjID)
                            {
                                BlnFind = true;
                                break;
                            }
                        }
                    }
                    if (!BlnFind)
                    {
                        for (int i = 0; i < MeterGroup.Count; i++)
                        {
                            if (MeterGroup[i].YaoJianYn)
                            {
                                try
                                {
                                    MeterGroup[i].MeterCarrierDatas.Remove(Key);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            else if (MeterInfo.MeterCarrierDatas.Count == 0)
            {
                for (int i = 0; i < MeterGroup.Count; i++)
                {
                    if (MeterGroup[i].YaoJianYn)
                    {
                        try
                        {
                            MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.载波试验).ToString());
                        }
                        catch { }
                    }
                }
            }
            #endregion

            #region -----------------------红外数据和新方案检查更新----------------------
            if (MeterInfo.MeterInfraredDatas == null)
            {
                MeterInfo.MeterInfraredDatas = new Dictionary<string, MeterInfraredData>();
            }
            if (MeterInfo.MeterInfraredDatas.Count > 0)
            {
                TmpKeys = new string[MeterInfo.MeterInfraredDatas.Count];
                MeterInfo.MeterInfraredDatas.Keys.CopyTo(TmpKeys, 0);

                foreach (string Key in TmpKeys)
                {
                    bool BlnFind = false;
                    for (int i = 0; i < CheckPlan.Count; i++)
                    {
                        if (CheckPlan[i] is StPlan_Infrared)
                        {
                            if (Key == ((StPlan_Infrared)CheckPlan[i]).str_PrjID)
                            {
                                BlnFind = true;
                                break;
                            }
                        }
                    }
                    if (!BlnFind)
                    {
                        for (int i = 0; i < MeterGroup.Count; i++)
                        {
                            if (MeterGroup[i].YaoJianYn)
                            {
                                try
                                {
                                    MeterGroup[i].MeterInfraredDatas.Remove(Key);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            else if (MeterInfo.MeterInfraredDatas.Count == 0)
            {
                for (int i = 0; i < MeterGroup.Count; i++)
                {
                    if (MeterGroup[i].YaoJianYn)
                    {
                        try
                        {
                            MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.红外数据比对试验).ToString());
                        }
                        catch { }
                    }
                }
            }
            #endregion

            #region -----------------------误差一致性数据和新方案检查更新----------------------
            if (MeterInfo.MeterErrAccords.Count > 0)
            {
                TmpKeys = new string[MeterInfo.MeterErrAccords.Count];
                MeterInfo.MeterErrAccords.Keys.CopyTo(TmpKeys, 0);

                foreach (string Key in TmpKeys)
                {
                    bool BlnFind = false;
                    for (int i = 0; i < CheckPlan.Count; i++)
                    {
                        if (CheckPlan[i] is CLDC_DataCore.Struct.StErrAccord)
                        {
                            if (Key == ((CLDC_DataCore.Struct.StErrAccord)CheckPlan[i]).ErrAccordType.ToString())
                            {
                                BlnFind = true;
                                //这里可以比较lstErrPoint负载点的差异
                                break;
                            }
                        }
                    }
                    if (!BlnFind)
                    {
                        for (int i = 0; i < MeterGroup.Count; i++)
                        {
                            if (MeterGroup[i].YaoJianYn)
                            {
                                try
                                {
                                    MeterGroup[i].MeterErrAccords.Remove(Key);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            else if (MeterInfo.MeterErrAccords.Count == 0)
            {
                for (int i = 0; i < MeterGroup.Count; i++)
                {
                    if (MeterGroup[i].YaoJianYn)
                    {
                        try
                        {
                            MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.误差一致性).ToString());
                        }
                        catch { }
                    }
                }
            }
            #endregion

            #region -------------------走字、特殊检定数据和新方案检查更新----------------

            this.CheckZZAndTsData(int_Bwh + 1);

            #endregion
        }
        /// <summary>
        /// 进行走字和特殊检定的方案比较，走字和特殊检定的Key并非项目编号，检查难度非常大，所以独立出来写了个方法处理，该方法不做其他用途
        /// 只供CheckFAChangeAndDataRefrash函数调用，
        /// </summary>
        /// <param name="FirstBwh">第一只要检表</param>
        /// <param name="DnbGroup">电能表数据集合</param>
        private void CheckZZAndTsData(int FirstBwh)
        {
            string[] TmpKeys;

            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = GetMeterBasicInfoByBwh(FirstBwh);

            #region --------------------走字数据和新方案检查更新-----------------------------

            if (MeterInfo.MeterZZErrors.Count > 0)
            {
                List<string> TmpOldKeys = new List<string>();
                List<string> TmpNewKeys = new List<string>();

                for (int i = 0; i < CheckPlan.Count; i++)          //循环新方案
                {
                    if (CheckPlan[i] is StPlan_ZouZi)       //如果方案项目里面存在走字项目
                    {
                        StPlan_ZouZi FaZouItem = (StPlan_ZouZi)CheckPlan[i];


                        foreach (string Key in MeterInfo.MeterZZErrors.Keys)        //循环走字检定记录
                        {
                            if (FaZouItem.PrjID == Key)       //如果在记录里面找到该项目
                            {
                                if (!TmpOldKeys.Contains(Key))          //同时该项目关键字不能在旧关键字列表中存在
                                {
                                    TmpOldKeys.Add(Key);                //在旧关键字列表中增加走字检定记录项目关键字
                                    //TmpNewKeys.Add("P_" + i.ToString());    //在新关键字列表中增加新方案项目下标。
                                    break;
                                }
                            }
                        }
                    }
                }

                if (TmpOldKeys.Count == 0)     //如果旧关键字列表为空，则表示新方案和当前检定的走字数据没有共同项目，所以要移除所有的走字检定数据和结论
                {
                    for (int i = 0; i < MeterGroup.Count; i++)
                    {
                        if (!MeterGroup[i].YaoJianYn) continue;
                        MeterGroup[i].MeterZZErrors.Clear();       //清理走字检定数据
                        try
                        {
                            MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString());       //移除走字检定结论
                        }
                        catch { }
                        try
                        {
                            MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString());       //移除走字组合误差试验
                        }
                        catch { }
                        try
                        {
                            MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString());       //移除走字组合误差值
                        }
                        catch { }
                        for (int j = 1; j <= 4; j++)
                        {
                            try
                            {
                                MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString() + j.ToString());
                            }
                            catch { }
                            try
                            {
                                MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString() + j.ToString());       //移除走字检定结论
                            }
                            catch { }
                            try
                            {
                                MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString() + j.ToString());       //移除走字组合误差值
                            }
                            catch { }
                        }


                    }
                }
                else
                {
                    //Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError> ZZItems;

                    for (int i = 0; i < MeterGroup.Count; i++)
                    {
                        if (!MeterGroup[i].YaoJianYn) continue;
                        if (MeterGroup[i].MeterZZErrors.Count == 0) continue;

                        //ZZItems = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError>();
                        string[] strKeys = new string[MeterGroup[i].MeterZZErrors.Count];
                        MeterGroup[i].MeterZZErrors.Keys.CopyTo(strKeys, 0);
                        foreach (string key in strKeys)
                        {
                            if (Array.IndexOf<string>(TmpOldKeys.ToArray(), key) == -1)
                            {
                                try
                                {
                                    MeterGroup[i].MeterZZErrors.Remove(key);
                                }
                                catch { }
                            }
                        }
                        //for (int j = 0; j < TmpOldKeys.Count; j++)
                        //{
                        //    try
                        //    {
                        //        ZZItems.Add(TmpNewKeys[j], MeterGroup[i].MeterZZErrors[TmpOldKeys[j]]);
                        //    }
                        //    catch { }
                        //}
                        //MeterGroup[i].MeterZZErrors = ZZItems;
                        //ZZItems = null;
                    }
                    #region --------------------清理分项数据-------------------------------
                    TmpKeys = new string[MeterInfo.MeterResults.Count];
                    MeterInfo.MeterResults.Keys.CopyTo(TmpKeys, 0);
                    foreach (string Key in TmpKeys)
                    {
                        if (Key.Length == 4 &&
                                        (Key.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验).ToString() ||
                                         Key.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差试验).ToString() ||
                                         Key.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字组合误差值).ToString()))
                        {
                            bool BlnFind = false;
                            foreach (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError ZZItem in MeterInfo.MeterZZErrors.Values)
                            {
                                if (Key.Substring(3, 1) == ZZItem.Me_chrProjectNo.Substring(0, 1))
                                {
                                    BlnFind = true;
                                    break;
                                }
                            }
                            if (!BlnFind)           //没找到，则删除该结论
                            {
                                for (int i = 0; i < MeterGroup.Count; i++)
                                {
                                    if (MeterGroup[i].YaoJianYn)
                                    {
                                        try
                                        {
                                            MeterGroup[i].MeterResults.Remove(Key);
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region-------------------特殊检定数据和新方案检查更新-----------------------

            if (MeterInfo.MeterSpecialErrs.Count > 0)
            {
                List<string> TmpOldKeys = new List<string>();
                List<string> TmpNewKeys = new List<string>();
                for (int i = 0; i < CheckPlan.Count; i++)          //循环新方案
                {
                    if (CheckPlan[i] is StPlan_SpecalCheck)       //如果方案项目里面存在特殊项目
                    {
                        StPlan_SpecalCheck FaSpItem = (StPlan_SpecalCheck)CheckPlan[i];

                        foreach (string Key in MeterInfo.MeterSpecialErrs.Keys)        //循环特殊检定记录
                        {
                            if (FaSpItem.PrjName == MeterInfo.MeterSpecialErrs[Key].Mse_PrjName)       //如果在记录里面找到该项目
                            {
                                if (!TmpOldKeys.Contains(Key))          //同时该项目关键字不能在旧关键字列表中存在
                                {
                                    TmpOldKeys.Add(Key);                //在旧关键字列表中增加走字检定记录项目关键字
                                    TmpNewKeys.Add("P_" + i.ToString());    //在新关键字列表中增加新方案项目下标。
                                    break;
                                }
                            }
                        }
                    }
                }

                if (TmpOldKeys.Count == 0)
                {
                    for (int i = 0; i < MeterGroup.Count; i++)
                    {
                        if (!MeterGroup[i].YaoJianYn) continue;
                        MeterGroup[i].MeterSpecialErrs.Clear();       //清理特殊检定数据
                        try
                        {
                            MeterGroup[i].MeterResults.Remove(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.特殊检定).ToString());       //移除特殊检定结论
                        }
                        catch { }
                    }
                }
                else
                {
                    Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr> SpItems = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr>();

                    for (int i = 0; i < MeterGroup.Count; i++)
                    {
                        if (!MeterGroup[i].YaoJianYn) continue;
                        if (MeterGroup[i].MeterSpecialErrs.Count == 0) continue;

                        SpItems = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr>();

                        for (int j = 0; j < TmpOldKeys.Count; j++)
                        {
                            try
                            {
                                SpItems.Add(TmpNewKeys[j], MeterGroup[i].MeterSpecialErrs[TmpOldKeys[j]]);
                            }
                            catch { }
                        }
                        MeterGroup[i].MeterSpecialErrs = SpItems;
                        SpItems = null;
                    }

                }

            }
            #endregion
        }
        #endregion

        #region --------新写数据存储方法，整批所有数据存储-------2013-7-23
        /// <summary>
        /// 存储本地默认ACCESS数据库
        /// </summary>
        /// <param name="ErrString">错误返回</param>
        /// <param name="flag">flag = true:操作正式数据库  false：操作临时数据库</param>
        /// <returns></returns>
        public bool SaveToSQL(out string ErrString, bool flag)
        {
            return this.SaveToSQL("", "", "", "", out ErrString, flag);
        }
        /// <summary>
        /// 存入本地ACCESS数据库
        /// </summary>
        /// <param name="AccessPath">数据库路径</param>
        /// <param name="ErrString">错误返回字符串</param>
        /// <param name="flag">flag = true:操作正式数据库  false：操作临时数据库</param>
        /// <returns></returns>
        public bool SaveToSQL(string AccessPath, out string ErrString, bool flag)
        {
            return this.SaveToSQL(AccessPath, "", "", "", out ErrString, flag);
        }
        /// <summary>
        /// 存入SQL服务器
        /// </summary>
        /// <param name="Ip">SQL服务器地址</param>
        /// <param name="UserName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="ErrString">错误返回字符串</param>
        /// <param name="flag">flag = true:操作正式数据库  false：操作临时数据库</param>
        /// <returns></returns>
        public bool SaveToSQL(string Ip, string UserName, string pwd, out string ErrString, bool flag)
        {
            return this.SaveToSQL("", Ip, UserName, pwd, out ErrString, flag);
        }

        /// <summary>
        /// 存储到数据库,整批所有数据存储
        /// 如果是本地库：正式库对应正式表，临时库对应临时表
        /// </summary>
        /// <param name="AccessPath">本地数据库路径</param>
        /// <param name="Ip">网络数据库IP</param>
        /// <param name="UserName">网络数据库登录名</param>
        /// <param name="pwd">网络数据库登录密码</param>
        /// <param name="ErrString">出参错误信息</param>
        /// <param name="flag">true正式表，false临时表</param>
        /// <returns>执行结果</returns>
        private bool SaveToSQL(string AccessPath, string Ip, string UserName, string pwd, out string ErrString, bool flag)
        {
            string _ErrString = "";
            CLDC_DataCore.Const.GlobalUnit.Logger.Debug("Calling SaveToDataBase:");
            CLDC_DataCore.DataBase.DataControl _Data;

            if (Ip != "")    //构造连接SQL服务器
                _Data = new CLDC_DataCore.DataBase.DataControl(Ip, UserName, pwd);
            else if (AccessPath != "")  //构造连接本地ACCESS数据库
                _Data = new CLDC_DataCore.DataBase.DataControl(AccessPath, true);
            else
                _Data = new CLDC_DataCore.DataBase.DataControl(flag);        //构造连接本地默认ACCESS数据库

            if (!_Data.Connection)
            {
                ErrString = "数据库连接失败";
                return false;
            }

            for (int Int_I = 0; Int_I < this.MeterGroup.Count; Int_I++)
            {
                List<string> _InsertDataBaseSQL = new List<string>();

                string _TmpSql = "";
                string strErr = "";
                MeterBasicInfo _BasicInfo = MeterGroup[Int_I];
                if (_BasicInfo.YaoJianYn == false)
                {
                    _BasicInfo = null;
                    continue;
                }
                //if (false == flag)
                //{//TODO:临时用，必须修改，服务器要加台体编号
                //    _TmpSql = string.Format("DELETE FROM Tmp_MeterInfo WHERE intBno={0}", Int_I + 1);
                //    _InsertDataBaseSQL.Add(_TmpSql);
                //}
                //TODO:在保存、换新表、到处到临时表这3个地方才更新_intMyId，凡是加载都是读的临时表的_intMyId
                long _MyID = _BasicInfo._intMyId;
                int _BwNo = Int_I + 1;
                // long.Parse(DateTime.Now.ToString("MMddHHmmss")) + Int_I;//_Data.ReadMaxAutoID(out _ErrString);  //表唯一关键字ID
                //_BasicInfo._intMyId = _MyID;
                #region ------------插入被检表信息------fjk------建议格式-----
                {
                    bool blnRun = sql_MeterInfo(flag, _BasicInfo, out _TmpSql, out strErr);
                    CheckDBResult(blnRun, strErr);
                    _InsertDataBaseSQL.Add(_TmpSql);
                    _BasicInfo = null;
                }
                #endregion

                #region ------------插入被检表多功能信息-----------------
                if (MeterGroup[Int_I].MeterDgns.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterDgns.Keys)
                    {
                        MeterDgn _Dgn = MeterGroup[Int_I].MeterDgns[_Key];
                        _Dgn._intMyId = _MyID;
                        _Dgn._intBno = _BwNo;
                        _Dgn._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterDgn(flag, _Dgn, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Dgn = null;
                    }
                }
                #endregion

                #region ------------插入被检表误差数据-----------------
                if (MeterGroup[Int_I].MeterErrors.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterErrors.Keys)
                    {
                        MeterError _Error = MeterGroup[Int_I].MeterErrors[_Key];
                        _Error._intMyId = _MyID;
                        _Error._intBno = _BwNo;
                        _Error._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterError(flag, _Error, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Error = null;
                    }
                }
                #endregion

                #region ------------插入一致性试验数据-----------------
                if (MeterGroup[Int_I].MeterConsistencys.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterConsistencys.Keys)
                    {
                        MeterConsistency _Mc = MeterGroup[Int_I].MeterConsistencys[_Key];
                        _Mc._intMyId = _MyID;
                        _Mc._intBno = _BwNo;
                        _Mc._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterConsistency(flag, _Mc, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Mc = null;
                    }
                }
                if (MeterGroup[Int_I].MeterErrAccords.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterErrAccords.Keys)
                    {
                        MeterErrAccord _Mc = MeterGroup[Int_I].MeterErrAccords[_Key];
                        _Mc._intMyId = _MyID;
                        _Mc._intBno = _BwNo;
                        _Mc._intTaiNo = _TaiID.ToString();
                        foreach (string _subKey in _Mc.lstTestPoint.Keys)
                        {
                            MeterErrAccordBase _ErrBase = _Mc.lstTestPoint[_subKey];
                            _ErrBase._intMyId = _Mc._intMyId;
                            bool blnRun = sql_MeterConsistency(flag, _ErrBase, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                        }
                        _Mc = null;
                    }
                }
                #endregion

                #region ------------插入事件记录信息-----------------
                if (MeterGroup[Int_I].MeterSjJLgns.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterSjJLgns.Keys)
                    {
                        MeterSjJLgn _SjJL = MeterGroup[Int_I].MeterSjJLgns[_Key];
                        _SjJL._intMyId = _MyID;
                        _SjJL._intBno = _BwNo;
                        _SjJL._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterFunEventRecord(flag, _SjJL, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _SjJL = null;
                    }
                }
                #endregion

                #region ------------插入功耗数据-----------------
                if (MeterGroup[Int_I].MeterPowers.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterPowers.Keys)
                    {
                        MeterPower _Gh = MeterGroup[Int_I].MeterPowers[_Key];
                        _Gh._intMyId = _MyID;
                        _Gh._intBno = _BwNo;
                        _Gh._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterPower(flag, _Gh, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Gh = null;
                    }
                }
                #endregion

                #region ------------插入数据显示功能信息-----------------
                if (MeterGroup[Int_I].MeterShows.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterShows.Keys)
                    {
                        MeterShow _Show = MeterGroup[Int_I].MeterShows[_Key];
                        _Show._intMyId = _MyID;
                        _Show._intBno = _BwNo;
                        _Show._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterShow(flag, _Show, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Show = null;
                    }
                }
                #endregion

                #region ------------插入潜动启动数据-----------------
                if (MeterGroup[Int_I].MeterQdQids.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterQdQids.Keys)
                    {
                        MeterQdQid _Qd = MeterGroup[Int_I].MeterQdQids[_Key];
                        _Qd._intMyId = _MyID;
                        _Qd._intBno = _BwNo;
                        _Qd._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterQdQid(flag, _Qd, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Qd = null;
                    }
                }
                #endregion

                #region ------------插入特殊检定数据-----------------
                if (MeterGroup[Int_I].MeterSpecialErrs != null && MeterGroup[Int_I].MeterSpecialErrs.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterSpecialErrs.Keys)
                    {
                        MeterSpecialErr _SpErr = MeterGroup[Int_I].MeterSpecialErrs[_Key];
                        _SpErr._intMyId = _MyID;
                        _SpErr._intBno = _BwNo;
                        _SpErr._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterSpecialErr(flag, _SpErr, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _SpErr = null;
                    }
                }
                #endregion

                #region ------------插入规约一致性数据-----------------
                if (MeterGroup[Int_I].MeterDLTDatas.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterDLTDatas.Keys)
                    {
                        MeterDLTData _Mdlt = MeterGroup[Int_I].MeterDLTDatas[_Key];
                        _Mdlt._intMyId = _MyID;
                        _Mdlt._intBno = _BwNo;
                        _Mdlt._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterDLTData(flag, _Mdlt, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Mdlt = null;
                    }
                }
                #endregion
                
                #region ------------插入智能表功能试验数据-----------------
                if (MeterGroup[Int_I].MeterFunctions.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterFunctions.Keys)
                    {
                        MeterFunction _Mdlt = MeterGroup[Int_I].MeterFunctions[_Key];
                        _Mdlt._intMyId = _MyID;
                        _Mdlt._intBno = _BwNo;
                        _Mdlt._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterFunctions(flag, _Mdlt, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Mdlt = null;
                    }
                }
                #endregion

                #region ------------插入费控数据-----------------
                if (MeterGroup[Int_I].MeterCostControls.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterCostControls.Keys)
                    {
                        MeterFK _Fk = MeterGroup[Int_I].MeterCostControls[_Key];
                        _Fk._intMyId = _MyID;
                        _Fk._intBno = _BwNo;
                        _Fk._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterFK(flag, _Fk, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Fk = null;
                    }
                }
                #endregion

                //#region ------------插入计量功能----------------

                //foreach (string item in MeterGroup[Int_I].MeterFunctions.Keys)
                //{
                //    if (item.IndexOf("001") != 0) continue;
                //    MeterJLgn jlgn = new MeterJLgn();
                //    MeterFunction meterfuncion = MeterGroup[Int_I].MeterFunctions[item];
                //    jlgn.FK_LNG_METER_ID = _MyID.ToString();
                //    jlgn.AVR_DEVICE_ID = _TaiID.ToString();
                //    jlgn._intBno = _BwNo;
                //    jlgn.AVR_GROUP_TYPE = "001";
                //    jlgn.AVR_LIST_NO = item;
                //    jlgn.AVR_PROJECT_NAME = meterfuncion.Mf_PrjName;
                //    jlgn.AVR_RECORD_OTHER = meterfuncion.Mf_chrValue;
                //    jlgn.AVR_ITEM_CONC = meterfuncion.Mf_Result;

                //    bool blnRunjlgn = sql_MeterJLgn(flag, jlgn, out _TmpSql, out strErr);
                //    CheckDBResult(blnRunjlgn, strErr);
                //    _InsertDataBaseSQL.Add(_TmpSql);
                //    jlgn = null;
                //}
                
                //#endregion



                //#region ------------插入费率时段功能---------------
                //foreach (string item in MeterGroup[Int_I].MeterFunctions.Keys)
                //{
                //    if (item.IndexOf("004") != 0) continue;
                //    MeterFLSDgn flsdgn = new MeterFLSDgn();
                //    MeterFunction meterfuncion = MeterGroup[Int_I].MeterFunctions[item];

                //    flsdgn._intMyId = _MyID;
                //    flsdgn._intBno = _BwNo;
                //    flsdgn._intTaiNo = _TaiID.ToString();
                //    flsdgn.Mfl_chrProjectName = meterfuncion.Mf_PrjName;
                //    flsdgn.Mfl_chrListNo = "004";
                //    flsdgn.Mfl_intItemType = item;
                //    flsdgn.Mfl_chrItemJL = meterfuncion.Mf_Result;
                //    flsdgn.Mfl_chrRecordOther = meterfuncion.Mf_chrValue;

                //    bool blnRunflsdgn = sql_MeterFLSDgn(flag, flsdgn, out _TmpSql, out strErr);
                //    CheckDBResult(blnRunflsdgn, strErr);
                //    _InsertDataBaseSQL.Add(_TmpSql);
                //    flsdgn = null;
                //}
                
                //#endregion

                //#region ------------插入需量功能-----------------
                //if (MeterGroup[Int_I].MeterFunctions.Count != 0)
                //{
                //    foreach (string _Key in MeterGroup[Int_I].MeterFunctions.Keys)
                //    {
                //        if (_Key.IndexOf("006") != 0) continue;
                //        MeterXLgn _Xl = new MeterXLgn();
                //        MeterFunction meterfunction = MeterGroup[Int_I].MeterFunctions[_Key];
                //        _Xl._intMyId = _MyID;
                //        _Xl._intBno = _BwNo;
                //        _Xl.AVR_GRP_TYPE = "006";
                //        _Xl.AVR_LIST_NO = _Key;
                //        _Xl.FK_LNG_METER_ID = _MyID.ToString();
                //        _Xl.AVR_DEVICE_ID = _TaiID.ToString();
                //        _Xl.LNG_BENCH_POINT_NO = _BwNo;
                //        _Xl.AVR_PROJECT_NAME = meterfunction.Mf_PrjName;
                //        _Xl.AVR_RECORD_OTHER = meterfunction.Mf_chrValue;
                //        _Xl.AVR_ITEM_CONC = meterfunction.Mf_Result;
                //        bool blnRun = sql_MeterXLgn(flag, _Xl, out _TmpSql, out strErr);
                //        CheckDBResult(blnRun, strErr);
                //        _InsertDataBaseSQL.Add(_TmpSql);
                //        _Xl = null;
                //    }
                //}
                //#endregion

                #region ------------插入冻结------------
                if (MeterGroup[Int_I].MeterFreezes.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterFreezes.Keys)
                    {
                        MeterFreeze _Fk = MeterGroup[Int_I].MeterFreezes[_Key];
                        _Fk._intMyId = _MyID;
                        _Fk._intBno = _BwNo;
                        _Fk._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterFreezes(flag, _Fk, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Fk = null;
                    }
                }
                #endregion

                #region ------------插入走字试验数据-----------------
                if (MeterGroup[Int_I].MeterZZErrors.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterZZErrors.Keys)
                    {
                        MeterZZError _ZZError = MeterGroup[Int_I].MeterZZErrors[_Key];
                        _ZZError._intMyId = _MyID;
                        _ZZError._intBno = _BwNo;
                        _ZZError._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterZZError(flag, _ZZError, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _ZZError = null;
                    }
                }
                #endregion

                #region ------------插入载波485数据-----------------
                if (MeterGroup[Int_I].MeterCarrierDatas.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterCarrierDatas.Keys)
                    {
                        MeterCarrierData _carrier = MeterGroup[Int_I].MeterCarrierDatas[_Key];
                        _carrier._intMyId = _MyID;
                        _carrier._intBno = _BwNo;
                        _carrier._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterCarrierData(flag, _carrier, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _carrier = null;
                    }
                }
                #endregion                

                #region ------------插入负荷记录-----------------
                if (MeterGroup[Int_I].MeterLoadRecords != null)
                {
                    if (MeterGroup[Int_I].MeterLoadRecords.Count != 0)
                    {
                        foreach (string _Key in MeterGroup[Int_I].MeterLoadRecords.Keys)
                        {
                            MeterLoadRecord meterfunction = MeterGroup[Int_I].MeterLoadRecords[_Key];
                            meterfunction._intMyId = _MyID;
                            meterfunction._intBno = _BwNo;
                            meterfunction._intTaiNo = _TaiID.ToString();
                            bool blnRun = sql_MeterLoadRecord(flag, meterfunction, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            meterfunction = null;
                        }
                    }
                }
                #endregion

                #region ------------插入结论信息-----------------
                if (MeterGroup[Int_I].MeterResults.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterResults.Keys)
                    {
                        MeterResult _Result = MeterGroup[Int_I].MeterResults[_Key];
                        _Result._intMyId = _MyID;
                        _Result._intBno = _BwNo;
                        _Result._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterResult(flag, _Result, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        _Result = null;
                    }
                }
                #endregion

                #region ------------插入耐压数据-----------------
                if (MeterGroup[Int_I].MeterInsulations.Count != 0)
                {
                    foreach (string _Key in MeterGroup[Int_I].MeterInsulations.Keys)
                    {
                        MeterInsulation insulation = MeterGroup[Int_I].MeterInsulations[_Key];
                        insulation._intMyId = _MyID;
                        insulation._intBno = _BwNo;
                        insulation._intTaiNo = _TaiID.ToString();
                        bool blnRun = sql_MeterHighVoltage(flag, insulation, out _TmpSql, out strErr);
                        CheckDBResult(blnRun, strErr);
                        _InsertDataBaseSQL.Add(_TmpSql);
                        insulation = null;
                    }
                }
                #endregion 插入耐压数据

                string _TmpString = "";
                bool _Return = _Data.SaveData(_InsertDataBaseSQL, out _TmpString);
                _ErrString += _TmpString;

                _InsertDataBaseSQL = null;
                _Data.CloseDB();
                CLDC_DataCore.Const.GlobalUnit.Logger.Debug("End Calling:SaveToDataBase");
            }
            _Data = null;
            if (_ErrString != "")
            {
                ErrString = _ErrString;
                return false;
            }
            else
            {
                ErrString = "";
                return true;
            }
        }
        #endregion

        #region 临时数据的单表、单条数据操作
        #region 公开 SaveToTempDB
        /// <summary>
        /// 存储本地默认ACCESS数据库
        /// 只操作临时表
        /// </summary>
        /// <param name="ErrString">错误返回</param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool SaveToTempDB(out string ErrString, string[] key, CLDC_Comm.Enum.Cus_DBTableName flag)
        {
            return this.SaveToTempDB("", "", "", "", key, out ErrString, flag);
        }
        /// <summary>
        /// 存入本地ACCESS数据库
        /// 只操作临时表
        /// </summary>
        /// <param name="AccessPath">数据库路径</param>
        /// <param name="ErrString">错误返回字符串</param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool SaveToTempDB(string AccessPath, out string ErrString, CLDC_Comm.Enum.Cus_DBTableName flag)
        {
            return this.SaveToTempDB(AccessPath, "", "", "", null, out ErrString, flag);
        }
        /// <summary>
        /// 存入SQL服务器
        /// 只操作临时表
        /// </summary>
        /// <param name="Ip">SQL服务器地址</param>
        /// <param name="UserName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="ErrString">错误返回字符串</param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool SaveToTempDB(string Ip, string UserName, string pwd, out string ErrString, string[] key, CLDC_Comm.Enum.Cus_DBTableName flag)
        {
            return this.SaveToTempDB("", Ip, UserName, pwd, key, out ErrString, flag);
        }
        #endregion

        #region 私有 SaveToTempDB
        /// <summary>
        /// 存储到数据库,整批单临时表数据存储
        /// 只操作临时表
        /// </summary>
        /// <param name="AccessPath">本地数据库路径</param>
        /// <param name="Ip">网络数据库IP</param>
        /// <param name="UserName">网络数据库登录名</param>
        /// <param name="pwd">网络数据库登录密码</param>
        /// <param name="ErrString">出参错误信息</param>
        /// <param name="PlanType">项目类型，决定存到哪个表</param>
        /// <returns>执行结果</returns>
        private bool SaveToTempDB(string AccessPath, string Ip, string UserName, string pwd, string[] key, out string ErrString, CLDC_Comm.Enum.Cus_DBTableName TableType)
        {
            string _ErrString = "";
            CLDC_DataCore.Const.GlobalUnit.Logger.Debug("Calling SaveToDataBase:");
            CLDC_DataCore.DataBase.DataControl _Data;

            if (Ip != "")    //构造连接SQL服务器
                _Data = new CLDC_DataCore.DataBase.DataControl(Ip, UserName, pwd);
            else if (AccessPath != "")  //构造连接本地ACCESS数据库
                _Data = new CLDC_DataCore.DataBase.DataControl(AccessPath, true);
            else
                _Data = new CLDC_DataCore.DataBase.DataControl(false);        //构造连接本地默认ACCESS数据库

            if (!_Data.Connection)
            {
                ErrString = "数据库连接失败";
                return false;
            }
            for (int Int_I = 0; Int_I < this.MeterGroup.Count; Int_I++)
            {
                List<string> _InsertDataBaseSQL = new List<string>();
                MeterBasicInfo _BasicInfo = MeterGroup[Int_I];
                if (TableType != CLDC_Comm.Enum.Cus_DBTableName.METER_INFO)
                {
                    if (key == null) break;
                    if (key[Int_I] == null || key[Int_I].Length < 1)
                    {
                        continue;
                    }
                    if (_BasicInfo.YaoJianYn == false)
                    {
                        _BasicInfo = null;
                        continue;
                    }
                }



                string _TmpSql = "";
                string strErr = "";

                //TODO:其它临时表的SQL

                long _MyID = _BasicInfo._intMyId;
                int _BwNo = Int_I + 1;
                switch (TableType)
                {
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_INFO:

                        #region SQL METER_INFO表

                        {
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE LNG_BENCH_POINT_NO={0} and AVR_DEVICE_ID='{2}'", Int_I + 1, TableType.ToString(), _TaiID);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            bool blnRun = sql_MeterInfo(false, _BasicInfo, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }

                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_ERROR:
                        #region SQL METER_ERROR表

                        {
                            MeterError error = _BasicInfo.MeterErrors[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NO='{2}'", error._intMyId, TableType.ToString(), error.Me_chrProjectNo);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            bool blnRun = sql_MeterError(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }

                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_COMMUNICATION:
                        #region SQL METER_COMMUNICATION表
                        {
                            if (_BasicInfo.MeterDgns.ContainsKey(key[Int_I]) == false) continue;
                            MeterDgn error = _BasicInfo.MeterDgns[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NO='{2}'", error._intMyId, TableType.ToString(), error.Md_PrjID);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterDgn(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_ENERGY_TEST_DATA:
                        #region SQL METER_ENERGY_TEST_DATA表

                        {
                            MeterZZError error = _BasicInfo.MeterZZErrors[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NO='{2}'", error._intMyId, TableType.ToString(), error.Me_chrProjectNo);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterZZError(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_START_NO_LOAD:

                        #region SQL METER_START_NO_LOAD表

                        {
                            MeterQdQid error = _BasicInfo.MeterQdQids[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NO='{2}'", error._intMyId, TableType.ToString(), error.Mqd_chrProjectNo);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterQdQid(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }

                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_POWER_CONSUM_DATA:

                        #region SQL METER_POWER_CONSUM_DATA表

                        {
                            MeterPower error = _BasicInfo.MeterPowers[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_LIST_NO='{2}'", error._intMyId, TableType.ToString(), error.Md_PrjID);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterPower(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }

                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_CARRIER_WAVE:
                        #region SQL METER_CARRIER_WAVE表

                        {
                            MeterCarrierData error = _BasicInfo.MeterCarrierDatas[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NO='{2}'", error._intMyId, TableType.ToString(), error.Mce_PrjNumber);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterCarrierData(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }

                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_SPECIAL_DATA:
                        #region SQL METER_SPECIAL_DATA表

                        {
                            MeterSpecialErr error = _BasicInfo.MeterSpecialErrs[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NO='{2}'", error._intMyId, TableType.ToString(), error.Mse_PrjNumber);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterSpecialErr(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }

                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_RATES_CONTROL:

                        #region SQL METER_RATES_CONTROL表

                        {
                            MeterFK error = _BasicInfo.MeterCostControls[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_ITEM_TYPE='{2}'", error._intMyId, TableType.ToString(), error.Mfk_chrItemType);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterFK(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_FUN_RATES_TIME_CONS:

                        #region SQL METER_FUN_RATES_TIME_CONS表

                        {
                            MeterFLSDgn flsdgn = new MeterFLSDgn();

                            foreach (string item in _BasicInfo.MeterFunctions.Keys)
                            {
                                MeterFunction meterfuncion = _BasicInfo.MeterFunctions[item];

                                flsdgn._intMyId = _MyID;
                                flsdgn._intBno = _BwNo;
                                flsdgn._intTaiNo = _TaiID.ToString();
                                flsdgn.Mfl_chrProjectName = meterfuncion.Mf_PrjName;
                                switch (item)
                                {
                                    case "004":
                                        flsdgn.Mfl_chrItemJL = meterfuncion.Mf_chrValue;        
                                        break;
                                    case "00401":
                                        //jlgn.AVR_ACTIVE_STATE = meterfuncion.Mf_chrValue;
                                        break;
                                    case "00402":

                                        break;
                                    case "00403":
                                        
                                        break;
                                    case "00404":

                                        break;
                                    case "00405":
                                        
                                        break;
                                    case "00406":

                                        break;
                                    case "00407":
                                        
                                        break;
                                    default:
                                        break;
                                }

                            }


                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NAME='{2}'", flsdgn._intMyId, TableType.ToString(), flsdgn.Mfl_chrProjectName);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterFLSDgn(false, flsdgn, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_FUN_ENERGY_MEASURE:
                        #region SQL MeterJLgn表

                        {
                            
                            MeterJLgn jlgn = new MeterJLgn();
                            
                            foreach (string item in _BasicInfo.MeterFunctions.Keys)
                            { 
                                MeterFunction meterfuncion = _BasicInfo.MeterFunctions[item];
                                jlgn.FK_LNG_METER_ID = _MyID.ToString();
                                jlgn.AVR_DEVICE_ID = _TaiID.ToString();
                                jlgn._intBno = _BwNo;
                                jlgn.AVR_GROUP_TYPE = meterfuncion.Mf_PrjID;
                                jlgn.AVR_PROJECT_NAME = meterfuncion.Mf_PrjName;
                                switch (item)
                                {
                                    case "001":
                                        jlgn.AVR_ITEM_CONC = meterfuncion.Mf_chrValue;
                                        break;
                                    case "00101":
                                        //jlgn.AVR_ACTIVE_STATE = meterfuncion.Mf_chrValue;
                                        break;
                                    case "00102":

                                        break;
                                    case "00103":
                                        jlgn.AVR_ACTIVE_GROUP_DATA = meterfuncion.Mf_chrValue;
                                        break;
                                    case "00104":
                                        
                                        break;
                                    case "00105":
                                        jlgn.AVR_ACTIVE_FORWARD_DATA = meterfuncion.Mf_chrValue;
                                        break;
                                    case "00106":

                                        break;
                                    case "00107":
                                        jlgn.AVR_ACTIVE_REVERSE_DATA = meterfuncion.Mf_chrValue;
                                        break;
                                    case "00108":

                                        break;
                                    default:
                                        break;

                                }
                                
                            }


                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NAME='{2}'", jlgn.FK_LNG_METER_ID, TableType.ToString(), jlgn.AVR_PROJECT_NAME);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterJLgn(false, jlgn, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_FUN_SHOW:
                        #region SQL MeterShow表

                        {
                            MeterShow error = _BasicInfo.MeterShows[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NO='{2}'", error._intMyId, TableType.ToString(), error.Msh_intItemType);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterShow(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_FUN_MAX_DEMAND:
                        #region SQL MeterXLgn表

                        {
                            if (!_BasicInfo.MeterXLgns.ContainsKey(key[Int_I])) continue;
                            MeterXLgn error = _BasicInfo.MeterXLgns[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NO='{2}'", error._intMyId, TableType.ToString(), error.AVR_LIST_NO);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterXLgn(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_FUN_EVENT_RECORD:
                        #region SQL MeterFunEventRecord表

                        {
                            if (!_BasicInfo.MeterSjJLgns.ContainsKey(key[Int_I])) continue;
                            MeterSjJLgn error = _BasicInfo.MeterSjJLgns[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_GROUP_TYPE='{2}'", error._intMyId, TableType.ToString(), error.GroupType);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterFunEventRecord(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_STANDARD_DLT_DATA:
                        #region SQL METER_STANDARD_DLT_DATA表

                        {
                            MeterDLTData error = _BasicInfo.MeterDLTDatas[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NO='{2}'", error._intMyId, TableType.ToString(), error.Mdlt_intItemID);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterDLTData(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_CONSISTENCY_DATA:
                        #region SQL METER_CONSISTENCY_DATA表

                        {
                            MeterErrAccord error = _BasicInfo.MeterErrAccords[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            foreach (string _subKey in error.lstTestPoint.Keys)
                            {
                                MeterErrAccordBase _errBase = error.lstTestPoint[_subKey];
                                _errBase._intMyId = _MyID;
                                _errBase._intBno = _BwNo;
                                _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_ITEM_NO='{2}'", error._intMyId, TableType.ToString(), _errBase.Mea_PrjID);
                                _InsertDataBaseSQL.Add(_TmpSql);
                                bool blnRun = sql_MeterConsistency(false, _errBase, out _TmpSql, out strErr);
                                CheckDBResult(blnRun, strErr);
                                _InsertDataBaseSQL.Add(_TmpSql);
                            }

                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_RESULTS:
                        #region SQL MeterResult表

                        {
                            MeterResult error = _BasicInfo.MeterResults[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NO='{2}'", error._intMyId, TableType.ToString(), error.Mr_chrRstId);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterResult(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_FREEZE:
                        #region SQL METER_FREEZE表

                        {
                            if (!_BasicInfo.MeterFreezes.ContainsKey(key[Int_I])) continue;
                            MeterFreeze error = _BasicInfo.MeterFreezes[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NO='{2}'", error._intMyId, TableType.ToString(), error.Md_PrjID);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterFreezes(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_FUN_TIME_KEEPING:
                        #region SQL METER_FUN_TIME_KEEPING表

                        {
                            MeterFunction error = _BasicInfo.MeterFunctions[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_PROJECT_NO='{2}'", error._intMyId, TableType.ToString(), error.Mf_PrjID);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterFunctions(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    case CLDC_Comm.Enum.Cus_DBTableName.METER_HIGH_VOLTAGE:
                        #region SQL METER_HIGH_VOLTAGE表
                        {
                            MeterInsulation error = _BasicInfo.MeterInsulations[key[Int_I]];
                            error._intMyId = _MyID;
                            error._intBno = _BwNo;
                            _TmpSql = string.Format("DELETE FROM TMP_{1} WHERE FK_LNG_METER_ID='{0}' and AVR_TYPE='{2}'", error._intMyId, TableType.ToString(), error.InsulationType);
                            _InsertDataBaseSQL.Add(_TmpSql);

                            bool blnRun = sql_MeterHighVoltage(false, error, out _TmpSql, out strErr);
                            CheckDBResult(blnRun, strErr);
                            _InsertDataBaseSQL.Add(_TmpSql);
                            _BasicInfo = null;
                        }
                        #endregion
                        break;
                    default:
                        break;
                }

                string _TmpString = "";
                bool _Return = _Data.SaveData(_InsertDataBaseSQL, out _TmpString);
                _ErrString += _TmpString;

                _InsertDataBaseSQL = null;

                CLDC_DataCore.Const.GlobalUnit.Logger.Debug("End Calling:SaveToDataBase");
            }
            _Data.CloseDB();
            _Data = null;
            if (_ErrString != "")
            {
                ErrString = _ErrString;
                return false;
            }
            else
            {
                ErrString = "";
                return true;
            }
        }
        #endregion
        #endregion

        #region 每个单表SQL,Insert
        /// <summary>
        /// 基本信息表的sql语句,
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterInfo(bool flag, MeterBasicInfo _BasicInfo, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_INFO";
            }
            else
            {
                name = "TMP_METER_INFO";
            }

            try
            {
                #region 拼sql
                _TmpSql = "Insert Into " + name + " (PK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,"
                    + "AVR_ASSET_NO,AVR_MADE_NO,AVR_BAR_CODE,AVR_ADDRESS,AVR_FACTORY,AVR_METER_MODEL,AVR_AR_CONSTANT,AVR_METER_TYPE,AVR_AR_CLASS,AVR_MADE_DATE,AVR_CUSTOMER,AVR_CERTIFICATE_NO,"
                + "AVR_METER_NAME,AVR_WIRING_MODE,AVR_UB,AVR_IB,AVR_FREQUENCY,CHR_CC_PREVENT_FLAG,CHR_CT_CONNECTION_FLAG,AVR_TEST_TYPE,DTM_TEST_DATE,DTM_VALID_DATE,AVR_TEMPERATURE,AVR_HUMIDITY,"
                + "AVR_TOTAL_CONCLUSION,AVR_TEST_PERSON,AVR_AUDIT_PERSON,AVR_SUPERVISOR,CHR_CHECKED,CHR_UPLOAD_FLAG,AVR_SEAL_1,AVR_SEAL_2,AVR_SEAL_3,AVR_SEAL_4,AVR_SEAL_5,AVR_SOFT_VER,AVR_HARD_VER,"
                + "AVR_ARRIVE_BATCH_NO,FK_LNG_SCHEME_ID,FK_PROTOCOL_ID,AVR_PROTOCOL_NAME,CHR_RATES_TYPE,AVR_TASK_NO,AVR_WORK_NO,AVR_OTHER_1,AVR_OTHER_2,AVR_OTHER_3,AVR_OTHER_4,AVR_OTHER_5,AVR_CARR_PROTC_NAME,AVR_PULSE_TYPE) values ("
                    + _BasicInfo._intMyId + ",'" + _TaiID + "'," + _BasicInfo.Mb_intBno + ",'"
                    + _BasicInfo.Mb_ChrJlbh + "','"
                    + _BasicInfo.Mb_ChrCcbh + "','"
                    + _BasicInfo.Mb_ChrTxm + "','"
                    + _BasicInfo.Mb_chrAddr + "','"
                    + _BasicInfo.Mb_chrzzcj + "','"
                    + _BasicInfo.Mb_Bxh + "','"
                    + _BasicInfo.Mb_chrBcs + "','"
                    + _BasicInfo.Mb_chrBlx + "','"
                    + _BasicInfo.Mb_chrBdj + "','"
                    + _BasicInfo.Mb_chrCcrq + "','"
                    + _BasicInfo.Mb_chrSjdw + "','"
                    + _BasicInfo.Mb_chrZsbh + "','"
                    + _BasicInfo.Mb_ChrBmc + "',"
                    + _BasicInfo.Mb_intClfs + ",'"
                    + _BasicInfo.Mb_chrUb + "','"
                    + _BasicInfo.Mb_chrIb + "','"
                    + _BasicInfo.Mb_chrHz + "','"
                    + (_BasicInfo.Mb_BlnZnq == true ? "1" : "0") + "','"
                    + (_BasicInfo.Mb_BlnHgq == true ? "1" : "0") + "','"
                    + _BasicInfo.Mb_chrTestType + "',#"
                    + (string.IsNullOrEmpty(_BasicInfo.Mb_DatJdrq.Trim()) ? (DateTime.Now.ToString()) : _BasicInfo.Mb_DatJdrq) + "#,#"//_BasicInfo.Mb_DatJdrq
                    + (string.IsNullOrEmpty(_BasicInfo.Mb_Datjjrq.Trim())?((DateTime.Now.AddMonths(12).AddDays(-1)).ToString()):_BasicInfo.Mb_Datjjrq) + "#,'"//_BasicInfo.Mb_Datjjrq DateTime.Now.AddMonths(12)
                    + _BasicInfo.Mb_chrWd + "','"
                    + _BasicInfo.Mb_chrSd + "','"
                    + _BasicInfo.Mb_chrResult + "','"
                    + CLDC_DataCore.Const.GlobalUnit.User_Jyy.UserName + "','"//_BasicInfo.Mb_ChrJyy
                    + CLDC_DataCore.Const.GlobalUnit.User_Hyy.UserName + "','"//_BasicInfo.Mb_ChrHyy
                    + _BasicInfo.Mb_chrZhuGuan + "','"
                    + (_BasicInfo.YaoJianYn == true ? 1 : 0) + "','"
                    + (_BasicInfo.Mb_BlnToServer == true ? "1" : "0") + "','"
                    + _BasicInfo.Mb_chrQianFeng1 + "','"
                    + _BasicInfo.Mb_chrQianFeng2 + "','"
                    + _BasicInfo.Mb_chrQianFeng3 + "','"
                    + _BasicInfo.AVR_SEAL_4 + "','"
                    + _BasicInfo.AVR_SEAL_5 + "','"
                    + _BasicInfo.Mb_chrSoftVer + "','"
                    + _BasicInfo.Mb_chrHardVer + "','"
                    + _BasicInfo.Mb_chrArriveBatchNo + "',"
                    + _BasicInfo.Mb_intSchemeID + ","
                    + _BasicInfo.Mb_intProtocolID + ",'"
                    + _BasicInfo.AVR_PROTOCOL_NAME + "','"
                    + _BasicInfo.Mb_intFKType + "','"
                    + _BasicInfo.AVR_TASK_NO + "','"
                    + _BasicInfo.AVR_WORK_NO + "','"
                    + _BasicInfo.Mb_chrOther1 + "','"
                    + _BasicInfo.Mb_chrOther2 + "','"
                    + _BasicInfo.Mb_chrOther3 + "','"
                    + _BasicInfo.Mb_chrOther4 + "','"
                    + _BasicInfo.Mb_chrOther5 + "','"
                    + _BasicInfo.AVR_CARR_PROTC_NAME + "','"
                    + _BasicInfo.Mb_gygy.ToString() + "')";
                #endregion
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 多功能表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_Dgn">一块表多功能数据</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterDgn(bool flag, MeterDgn _Dgn, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_COMMUNICATION";
            }
            else
            {
                name = "TMP_METER_COMMUNICATION";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_PROJECT_NO,AVR_VALUE,AVR_CONCLUSION,FK_LNG_SCHEME_ID,AVR_DIS_REASON) Values("
                    + _Dgn._intMyId + ",'" + _TaiID + "'," + _Dgn._intBno + ",'"
                    + _Dgn.Md_PrjID + "','"
                    + _Dgn.Md_chrValue + "','"
                    + _Dgn.AVR_CONCLUSION + "',"
                    + _Dgn.FK_LNG_SCHEME_ID + ",'"
                    + _Dgn.AVR_DIS_REASON + "'"
                    + ")";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterError(bool flag, MeterError _Error, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_ERROR";
            }
            else
            {
                name = "TMP_METER_ERROR";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_PROJECT_NO,CHR_ERROR_TYPE,CHR_POWER_TYPE,CHR_COMPONENT,AVR_IB_MULTIPLE,"
                + "AVR_IB_MULTIPLE_STRING,AVR_POWER_FACTOR,AVR_ERROR_ROUNDING,AVR_ERROR_AVERAGE,AVR_STANDARD_ERROR,AVR_ERROR_MORE,AVR_UPPER_LIMIT,AVR_LOWER_LIMIT,AVR_CIRCLE_COUNT,"
                + "AVR_ERROR_CONCLUSION,CHR_DIF_ERR_FLAG,AVR_DIF_H_ERRORS,AVR_DIF_H_ERR_AVG,AVR_DIF_H_ERR_ROUND,AVR_DIF_ERR_AVG,AVR_DIF_ERR_ROUND,AVR_RESERVE,FK_LNG_SCHEME_ID,AVR_DIS_REASON) Values("
                    + _Error._intMyId + ",'" + _TaiID + "'," + _Error._intBno + ",'"
                    + _Error.Me_chrProjectNo + "','"
                    + _Error.Me_intWcType + "','"
                    + (((int)((CLDC_Comm.Enum.Cus_PowerFangXiang)(Enum.Parse(typeof(CLDC_Comm.Enum.Cus_PowerFangXiang), _Error.Me_Glfx)))) - 1) + "','"
                    + _Error.Me_intYj + "','"
                    + _Error.Me_dblxIb + "','"
                    + _Error.AVR_IB_MULTIPLE_STRING + "','"
                    + _Error.Me_chrGlys + "','"
                    + _Error.Me_chrWcHz + "','"
                    + _Error.Me_chrWc + "','"
                    + _Error.AVR_STANDARD_ERROR + "','"
                    + _Error.Me_chrWcMore + "','"
                    + _Error.Me_WcLimit + "','"
                    + _Error.AVR_LOWER_LIMIT + "','"
                    + _Error.AVR_CIRCLE_COUNT + "','"
                    + _Error.Me_chrWcJl + "','"
                    + _Error.CHR_DIF_ERR_FLAG + "','"
                    + _Error.AVR_DIF_H_ERRORS + "','"
                    + _Error.AVR_DIF_H_ERR_AVG + "','"
                    + _Error.AVR_DIF_H_ERR_ROUND + "','"
                    + _Error.AVR_DIF_ERR_AVG + "','"
                    + _Error.AVR_DIF_ERR_ROUND + "','"
                    + _Error.Me_chrMemo + "','"
                    + _Error.FK_LNG_SCHEME_ID + "','"
                    + _Error.AVR_DIS_REASON + "')";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterConsistency(bool flag, MeterConsistency _Mc, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_CONSISTENCY_DATA";
            }
            else
            {
                name = "TMP_METER_CONSISTENCY_DATA";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_GRP_TYPE,AVR_ITEM_TYPE,AVR_ITEM_NO,AVR_PARAMETER,AVR_CONC,AVR_DATA_1_ROUNDING,"
                + "AVR_DATA_1_AVG,AVR_DATAS_1,AVR_DATA_2_ROUNDING,AVR_DATA_2_AVG,AVR_DATAS_2,AVR_DIF_DATA_ROUNDING,AVR_DIF_DATA_AVG,AVR_DIF_ERROR_LIMIT,FK_LNG_SCHEME_ID,AVR_DIS_REASON) Values ("
                    + _Mc._intMyId + ",'" + _TaiID + "'," + _Mc._intBno + ",'"
                    + _Mc.Mc_chrGrpType + "','"
                    + _Mc.Mc_chrItemType + "','"
                    + _Mc.Mc_intItemNo + "','"
                    + _Mc.AVR_PARAMETER + "','"
                    + _Mc.Mc_chrJL + "','"
                    + _Mc.AVR_DATA_1_ROUNDING + "','"
                    + _Mc.AVR_DATA_1_AVG + "','"
                    + _Mc.Mc_chrData + "','"
                    + _Mc.AVR_DATA_2_ROUNDING + "','"
                    + _Mc.AVR_DATA_2_AVG + "','"
                    + _Mc.AVR_DATAS_2 + "','"
                    + _Mc.Mc_chrDataInt + "','"
                    + _Mc.Mc_chrDataAvg + "','"
                    + _Mc.AVR_DIF_ERROR_LIMIT + "',"
                    + _Mc.FK_LNG_SCHEME_ID + ",'"
                    + _Mc.AVR_DIS_REASON + "'"
                    + ")";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        private bool sql_MeterConsistency(bool flag, MeterErrAccordBase _Mc, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_CONSISTENCY_DATA";
            }
            else
            {
                name = "TMP_METER_CONSISTENCY_DATA";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_GRP_TYPE,AVR_ITEM_TYPE,AVR_ITEM_NO,AVR_PARAMETER,AVR_CONC,AVR_DATA_1_ROUNDING,AVR_DATA_1_AVG,AVR_DATAS_1,AVR_DATA_2_ROUNDING,AVR_DATA_2_AVG,AVR_DATAS_2,AVR_DIF_DATA_ROUNDING,AVR_DIF_DATA_AVG,AVR_DIF_ERROR_LIMIT,FK_LNG_SCHEME_ID,AVR_DIS_REASON) Values ("
                    + _Mc._intMyId + ",'" + _TaiID + "'," + _Mc._intBno + ",'"
                    + "06" + "','"
                    + _Mc.Sub_Item_ID + "','"
                    + _Mc.Mea_PrjID + "','"
                    + _Mc.Mea_PrjName + "," + _Mc.Mea_xU + "," + _Mc.Mea_xib + "," + _Mc.Mea_Glys + "," + _Mc.Mea_Qs + "','"
                    + _Mc.Mea_ItemResult + "','"
                    + "" + "','"
                    + "" + "','"
                    + _Mc.Mea_Wc1 + "','"
                    + "" + "','"
                    + _Mc.Mea_WcAver + "','"
                    + _Mc.Mea_Wc2 + "','"
                    + _Mc.Mea_Wc + "','"
                    + _Mc.Mea_Wc + "','"
                    + _Mc.Mea_WcLimit + "',"
                    + "0" + ",'"
                    + "" + "'"
                    + ")";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }

        /// <summary>
        /// 表的sql语句
        /// </summary>
        ///事件记录数据表
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterFunEventRecord(bool flag, MeterSjJLgn _SjJL, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_FUN_EVENT_RECORD";
            }
            else
            {
                name = "TMP_METER_FUN_EVENT_RECORD";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_GROUP_TYPE,AVR_LIST_NO,AVR_ITEM_NAME,AVR_STATUS_NO,AVR_SUB_ITEM_NAME,"
                    + "AVR_TEST_START_TIME,AVR_TEST_END_TIME,AVR_SUM_TIMES,AVR_USE_TIME,AVR_RECORD_START_TIME,AVR_RECORD_END_TIME,AVR_SUB_ITEM_CONC,AVR_ITEM_CONC,AVR_A_SUM_TIMES,AVR_A_USE_TIME,"
                    + "AVR_B_SUM_TIMES,AVR_B_USE_TIME,AVR_C_SUM_TIMES,AVR_C_USE_TIME,AVR_RECORD_NO,AVR_A_RECORD_START_TIME,AVR_A_RECORD_END_TIME,AVR_A_RECORD_START_DATA,AVR_A_RECORD_END_DATA,AVR_A_RECORDING_DATA,AVR_B_RECORD_START_TIME,AVR_B_RECORD_END_TIME,AVR_B_RECORD_START_DATA,AVR_B_RECORD_END_DATA,AVR_B_RECORDING_DATA,AVR_C_RECORD_START_TIME,AVR_C_RECORD_END_TIME,AVR_C_RECORD_START_DATA,AVR_C_RECORD_END_DATA,AVR_C_RECORDING_DATA,AVR_USER_CODE,AVR_DI_CODE,AVR_RECORD_OTHER,AVR_IMBALANCE_RATIO,FK_LNG_SCHEME_ID,AVR_DIS_REASON) Values ("
                    + _SjJL._intMyId.ToString() + ","
                   + _TaiID.ToString() + ","
                   + _SjJL._intBno.ToString() + ",'"
                    + _SjJL.GroupType + "','"
                    + _SjJL.ListNo + "','"

                    + _SjJL.ItemName + "','"
                    + _SjJL.StatusNo + "','"
                    + _SjJL.SubItemName + "','"
                    + _SjJL.TestStartTime + "','"
                    + _SjJL.TestEndTime + "','"

                    + _SjJL.SumTimes + "','"
                    + _SjJL.UseTime + "','"
                    + _SjJL.RecordStartTime + "','"
                    + _SjJL.RecordEndTime + "','"
                    + _SjJL.SubItemConc + "','"

                    + _SjJL.ItemConc + "','"
                    + _SjJL.ASumTimes + "','"
                    + _SjJL.AUseTime + "','"
                    + _SjJL.BSumTimes + "','"
                    + _SjJL.BUseTime + "','"

                    + _SjJL.CSumTimes + "','"
                    + _SjJL.CUseTime + "','"
                    + _SjJL.RecordNo + "','"
                    + _SjJL.ARecordStartTime + "','"
                     + _SjJL.ARecordEndTime + "','"

                      + _SjJL.ARecordStartData + "','"
                       + _SjJL.ARecordEndData + "','"
                        + _SjJL.ARecordingData + "','"
                         + _SjJL.BRecordStartTime + "','"
                          + _SjJL.BRecordEndTime + "','"

                           + _SjJL.BRecordStartData + "','"
                            + _SjJL.BRecordEndData + "','"
                             + _SjJL.BRecordingData + "','"
                              + _SjJL.CRecordStartTime + "','"
                               + _SjJL.CRecordEndTime + "','"

                               + _SjJL.CRecordStartData + "','"
                               + _SjJL.CRecordEndData + "','"
                               + _SjJL.CRecordingData + "','"
                               + _SjJL.UserCode + "','"
                               + _SjJL.DICode + "','"

                               + _SjJL.RecordOther + "','"
                               + _SjJL.ImbalanceRatio + "','"
                               + _SjJL.SchemeID + "','"
                    + _SjJL.DisReasion + "')";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterPower(bool flag, MeterPower _Gh, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_POWER_CONSUM_DATA";
            }
            else
            {
                name = "TMP_METER_POWER_CONSUM_DATA";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,"
                    + "AVR_LIST_NO,AVR_CUR_CIR_A_VOT,AVR_CUR_CIR_B_VOT,AVR_CUR_CIR_C_VOT,AVR_CUR_CIR_A_CUR,AVR_CUR_CIR_B_CUR,AVR_CUR_CIR_C_CUR,AVR_CUR_A_APPARENT_POWER,AVR_CUR_B_APPARENT_POWER,"
                + "AVR_CUR_C_APPARENT_POWER,AVR_CUR_APPARENT_LIMIT,AVR_CUR_APPARENT_CONCLUSION,AVR_VOT_CIR_A_VOT,AVR_VOT_CIR_B_VOT,AVR_VOT_CIR_C_VOT,AVR_VOT_CIR_A_CUR,AVR_VOT_CIR_B_CUR,"
                + "AVR_VOT_CIR_C_CUR,AVR_VOT_CIR_A_ANGLE,AVR_VOT_CIR_B_ANGLE,AVR_VOT_CIR_C_ANGLE,AVR_VOT_A_APPARENT_POWER,AVR_VOT_B_APPARENT_POWER,AVR_VOT_C_APPARENT_POWER,AVR_VOT_APPARENT_LIMIT,"
                + "AVR_VOT_APPARENT_CONCLUSION,AVR_VOT_A_ACTIVE_POWER,AVR_VOT_B_ACTIVE_POWER,AVR_VOT_C_ACTIVE_POWER,AVR_VOT_ACTIVE_LIMIT,AVR_VOT_ACTIVE_CONCLUSION,AVR_DIS_REASON,FK_LNG_SCHEME_ID,AVR_CONCLUSION) Values ("
                   + _Gh._intMyId + ",'" + _TaiID + "'," + _Gh._intBno + ",'"
                   + _Gh.Md_PrjID + "','"
                   + _Gh.AVR_CUR_CIR_A_VOT + "','"
                   + _Gh.AVR_CUR_CIR_B_VOT + "','"
                   + _Gh.AVR_CUR_CIR_C_VOT + "','"
                   + _Gh.AVR_CUR_CIR_A_CUR + "','"
                   + _Gh.AVR_CUR_CIR_B_CUR + "','"
                   + _Gh.AVR_CUR_CIR_C_CUR + "','"
                   + _Gh.Md_Ia_ReactiveS + "','"
                   + _Gh.Md_Ib_ReactiveS + "','"
                   + _Gh.Md_Ic_ReactiveS + "','"
                   + _Gh.AVR_CUR_CIR_S_LIMIT + "','"
                   + _Gh.Mgh_chrISJL + "','"
                   + _Gh.AVR_VOT_CIR_A_VOT + "','"
                   + _Gh.AVR_VOT_CIR_B_VOT + "','"
                   + _Gh.AVR_VOT_CIR_C_VOT + "','"
                   + _Gh.AVR_VOT_CIR_A_CUR + "','"
                   + _Gh.AVR_VOT_CIR_B_CUR + "','"
                   + _Gh.AVR_VOT_CIR_C_CUR + "','"
                   + _Gh.AVR_VOT_CIR_A_ANGLE + "','"
                   + _Gh.AVR_VOT_CIR_B_ANGLE + "','"
                   + _Gh.AVR_VOT_CIR_C_ANGLE + "','"
                   + _Gh.Md_Ua_ReactiveS + "','"
                   + _Gh.Md_Ub_ReactiveS + "','"
                   + _Gh.Md_Uc_ReactiveS + "','"
                   + _Gh.AVR_VOT_CIR_S_LIMIT + "','"
                   + _Gh.Mgh_chrUSJL + "','"
                   + _Gh.Md_Ua_ReactiveP + "','"
                   + _Gh.Md_Ub_ReactiveP + "','"
                   + _Gh.Md_Uc_ReactiveP + "','"
                   + _Gh.AVR_VOT_CIR_P_LIMIT + "','"
                   + _Gh.Mgh_chrUPJL + "','"
                   + _Gh.AVR_DIS_REASON + "',"
                   + _Gh.FK_LNG_SCHEME_ID + ",'"
                   + _Gh.Md_chrValue + "')";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterShow(bool flag, MeterShow _Show, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "Meter_Fun_Show";
            }
            else
            {
                name = "Tmp_Meter_Fun_Show";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_PROJECT_NAME,AVR_GROUP_TYPE,AVR_ITEM_TYPE,AVR_CONC,AVR_CHILD_ITEM_TYPE,AVR_CHILD_ITEM_NAME,LNG_SUB_ITEM_NO,AVR_SUB_ITEM_NAME,"
                    + "AVR_SYMBOL_ID,LNG_VALUE_LENGTH,LNG_VALUE_DOT_LENGTH,AVR_VALUE_FORMAT,LNG_READ_WRITE_FLAG,AVR_COMPARISON_CONTENT,AVR_READ_DATA,FK_LNG_SCHEME_ID,AVR_DIS_REASON,AVR_OTHER_1,AVR_OTHER_2,AVR_OTHER_3,AVR_OTHER_4,AVR_OTHER_5) Values ("
                    + _Show._intMyId + ",'" + _TaiID + "'," + _Show._intBno + ",'"
                    + _Show.Msh_chrProjectName + "','"
                    + _Show.Msh_chrGrpType + "','"
                    + _Show.Msh_intItemType + "','"
                    + _Show.Msh_chrJL + "',"
                    + _Show.Msh_intType + ",'"
                    + _Show.Msh_chrType + "',"
                    + _Show.Msh_intSubItem + ",'"
                    + _Show.Msh_chrSubItem + "','"
                    + _Show.Msh_chrID + "',"
                    + _Show.Msh_intLength + ","
                    + _Show.Msh_intDot + ",'"
                    + _Show.Msh_chrFormat + "',"
                    + _Show.Msh_intReadWrite + ",'"
                    + _Show.Msh_chrContent + "','"
                    + _Show.Msh_chrData + "','"
                    + _Show.FK_LNG_SCHEME_ID + "','"
                    + _Show.AVR_DIS_REASON + "','"
                    + _Show.Msh_c_Other1 + "','"
                    + _Show.Msh_c_Other2 + "','"
                    + _Show.Msh_c_Other3 + "','"
                    + _Show.Msh_c_Other4 + "','"
                    + _Show.Msh_c_Other5 + "')";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterQdQid(bool flag, MeterQdQid _Qd, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_START_NO_LOAD";
            }
            else
            {
                name = "TMP_METER_START_NO_LOAD";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_PROJECT_NO,CHR_POWER_TYPE,AVR_PROJECT_NAME,AVR_CONCLUSION,AVR_STANDARD_TIME,"
                + "DTM_BEGIN_TIME,DTM_OVER_TIME,AVR_ACTUAL_TIME,AVR_CURRENT,AVR_VOLTAGE,FK_LNG_SCHEME_ID,AVR_DIS_REASON) Values ("
                    + _Qd._intMyId + ",'" + _TaiID + "'," + _Qd._intBno + ",'"
                    + _Qd.Mqd_chrProjectNo + "','"
                    + _Qd.Mqd_chrJdfx + "','"
                    + _Qd.Mqd_chrProjectName + "','"
                    + _Qd.Mqd_chrJL + "','"
                    + _Qd.AVR_STANDARD_TIME + "',#"
                    + DateTime.Now + "#,#"
                    + DateTime.Now + "#,'"
                    + _Qd.AVR_ACTUAL_TIME + "','"
                    + _Qd.Mqd_chrDL + "','"
                    + _Qd.AVR_VOLTAGE + "',"
                    + _Qd.FK_LNG_SCHEME_ID + ",'"
                    + _Qd.AVR_DIS_REASON + "'"
                    + ")";

                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterSpecialErr(bool flag, MeterSpecialErr _SpErr, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_SPECIAL_DATA";
            }
            else
            {
                name = "TMP_METER_SPECIAL_DATA";
            }
            try
            {
                _TmpSql = "Insert Into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,"
                    + "AVR_PROJECT_NO,CHR_ERROR_TYPE,CHR_POWER_TYPE,CHR_COMPONENT,AVR_CUR_A_MULTIPLE,AVR_CUR_A_MULTIPLE_STRING,AVR_CUR_B_MULTIPLE,AVR_CUR_B_MULTIPLE_STRING,AVR_CUR_C_MULTIPLE,"
                + "AVR_CUR_C_MULTIPLE_STRING,AVR_VOT_A_MULTIPLE,AVR_VOT_B_MULTIPLE,AVR_VOT_C_MULTIPLE,AVR_UPPER_LIMIT,AVR_LOWER_LIMIT,AVR_CIRCLE_COUNT,CHR_HARMONIC_WAVE_FLAG,"
                + "CHR_NEGATIVE_PHASE_FLAG,AVR_POWER_FACTOR,AVR_FREQUENCY_MULTIPLE,AVR_ERROR_ROUNDING,AVR_ERROR_AVERAGE,AVR_ERROR_MORE,AVR_ERR_CONCLUSION,AVR_DIF_H_ERR_AVG,AVR_DIF_ERROR,"
                + "CHR_BASE_LOAD_FLAG,AVR_BASE_LOAD_NO,AVR_VOT_A,AVR_VOT_B,AVR_VOT_C,AVR_CUR_A,AVR_CUR_B,AVR_CUR_C,AVR_VOT_A_ANGLE,AVR_VOT_B_ANGLE,AVR_VOT_C_ANGLE,AVR_CUR_A_ANGLE,AVR_CUR_B_ANGLE,"
                + "AVR_CUR_C_ANGLE,AVR_ITEM_NAME,FK_LNG_SCHEME_ID,AVR_DIS_REASON) values("
                    + _SpErr._intMyId + ",'" + _TaiID + "'," + _SpErr._intBno + ",'"
                    + _SpErr.Mse_PrjNumber + "','"
                    + _SpErr.Mse_intWcType + "','"
                    + _SpErr.Mse_Glfx + "','"
                    + _SpErr.Mse_intYj + "','"
                    + _SpErr.Mse_dblxIb + "','"
                    + _SpErr.AVR_CUR_A_MULTIPLE_STRING + "','"
                    + _SpErr.AVR_CUR_B_MULTIPLE + "','"
                    + _SpErr.AVR_CUR_B_MULTIPLE_STRING + "','"
                    + _SpErr.AVR_CUR_C_MULTIPLE + "','"
                    + _SpErr.AVR_CUR_C_MULTIPLE_STRING + "','"
                    + _SpErr.Mse_dblxUb + "','"
                    + _SpErr.AVR_VOT_B_MULTIPLE + "','"
                    + _SpErr.AVR_VOT_C_MULTIPLE + "','"
                    + _SpErr.Mse_swcbl + "','"
                    + _SpErr.Mse_xwcbl + "','"
                    + _SpErr.Mse_Qs + "','"
                    + _SpErr.Mse_XieBo.ToString() + "','"
                    + _SpErr.Mse_Nxx.ToString() + "','"
                    + _SpErr.Mse_chrGlys + "','"
                    + _SpErr.Mse_dblxHz + "','"
                    + _SpErr.Mse_chrWcHz + "','"
                    + _SpErr.Mse_chrWc + "','"
                    + _SpErr.Mse_Wc + "','"
                    + _SpErr.Mse_Result + "','"
                    + _SpErr.AVR_DIF_H_ERR_AVG + "','"
                    + _SpErr.AVR_DIF_ERROR + "','"
                    + _SpErr.CHR_BASE_LOAD_FLAG + "','"
                    + _SpErr.AVR_BASE_LOAD_NO + "','"
                    + _SpErr.AVR_VOT_A + "','"
                    + _SpErr.Mse_sngUb + "','"
                    + _SpErr.Mse_sngUc + "','"
                    + _SpErr.AVR_CUR_A + "','"
                    + _SpErr.Mse_sngIb + "','"
                    + _SpErr.Mse_sngIc + "','"
                    + _SpErr.Mse_sngPhi_Ua + "','"
                    + _SpErr.Mse_sngPhi_Ub + "','"
                    + _SpErr.Mse_sngPhi_Uc + "','"
                    + _SpErr.Mse_sngPhi_Ia + "','"
                    + _SpErr.Mse_sngPhi_Ib + "','"
                    + _SpErr.Mse_sngPhi_Ic + "','"
                    + _SpErr.Mse_PrjName + "',"
                    + _SpErr.FK_LNG_SCHEME_ID + ",'"
                    + _SpErr.AVR_DIS_REASON + "'"
                    + ")";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterDLTData(bool flag, MeterDLTData _Mdlt, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_STANDARD_DLT_DATA";
            }
            else
            {
                name = "TMP_METER_STANDARD_DLT_DATA";
            }
            try
            {
                _TmpSql = "Insert into " + name
                    + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,"
                    + "FK_LNG_ITEM_ID,AVR_DI0_DI3,AVR_DI_MSG,AVR_DI_LEN,AVR_DI_FORMAT,AVR_VALUE,AVR_COMPARISON_VALUE,AVR_CONDITION,FK_LNG_SCHEME_ID,AVR_CONC,AVR_DIS_REASON,AVR_PROJECT_NO) Values ("
                    + _Mdlt._intMyId + ",'" + _TaiID + "'," + _Mdlt._intBno + ",'"
                    + _Mdlt.Mdlt_intItemID + "','"
                    + _Mdlt.AVR_DI0_DI3 + "','"
                    + _Mdlt.AVR_DI_MSG + "','"
                    + _Mdlt.AVR_DI_LEN + "','"
                    + _Mdlt.AVR_DI_FORMAT + "','"
                    + _Mdlt.Mdlt_chrValue + "','"
                    + _Mdlt.AVR_COMPARISON_VALUE + "','"
                    + _Mdlt.AVR_CONDITION + "',"
                    + _Mdlt.FK_LNG_SCHEME_ID + ",'"
                    + _Mdlt.AVR_CONC + "','"
                    + _Mdlt.AVR_DIS_REASON + "','"
                    + _Mdlt.Mdlt_intItemID + "'"
                    + ")";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterJLgn(bool flag, MeterJLgn _Jl, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_FUN_ENERGY_MEASURE";
            }
            else
            {
                name = "TMP_METER_FUN_ENERGY_MEASURE";
            }
            try
            {

                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_PROJECT_NAME,AVR_GROUP_TYPE,AVR_LIST_NO,AVR_ITEM_CONC,AVR_ACTIVE_STATE_STD,"
                    + "AVR_ACTIVE_STATE,AVR_ACTIVE_GROUP_DATA,AVR_ACTIVE_FORWARD_DATA,AVR_ACTIVE_REVERSE_DATA,AVR_ACTIVE_STATE_CONC,AVR_R_STATE_1_STD,AVR_R_STATE_1,AVR_R_STATE_2_STD,AVR_R_STATE_2,AVR_R_STATE_1_DATA,AVR_R_STATE_2_DATA,"
                    + "AVR_R_QUADRANT_1_DATA,AVR_R_QUADRANT_2_DATA,AVR_R_QUADRANT_3_DATA,AVR_R_QUADRANT_4_DATA,AVR_R_STATE_1_CONC,AVR_R_STATE_2_CONC,AVR_A_DATA,AVR_B_DATA,AVR_C_DATA,AVR_SUM_ABC_CONC,AVR_SUM_RATES_CONC,FK_LNG_SCHEME_ID,AVR_RECORD_OTHER,AVR_DIS_REASON) Values ("
                    + _Jl.FK_LNG_METER_ID + ",'"
                    + _Jl.AVR_DEVICE_ID + "',"
                    + _Jl._intBno + ",'"
                    + _Jl.AVR_PROJECT_NAME + "',"
                    + _Jl.AVR_GROUP_TYPE + ",'"
                    + _Jl.AVR_LIST_NO + "','"
                    + _Jl.AVR_ITEM_CONC + "','"
                    + _Jl.AVR_ACTIVE_STATE_STD + "','"
                    + _Jl.AVR_ACTIVE_STATE + "','"
                    + _Jl.AVR_ACTIVE_GROUP_DATA + "','"
                    + _Jl.AVR_ACTIVE_FORWARD_DATA + "','"
                    + _Jl.AVR_ACTIVE_REVERSE_DATA + "','"
                    + _Jl.AVR_ACTIVE_STATE_CONC + "','"
                    + _Jl.AVR_R_STATE_1_STD + "','"
                    + _Jl.AVR_R_STATE_1 + "','"
                    + _Jl.AVR_R_STATE_2_STD + "','"
                    + _Jl.AVR_R_STATE_2 + "','"
                    + _Jl.AVR_R_STATE_1_DATA + "','"
                    + _Jl.AVR_R_STATE_2_DATA + "','"
                    + _Jl.AVR_R_QUADRANT_1_DATA + "','"
                    + _Jl.AVR_R_QUADRANT_2_DATA + "','"
                    + _Jl.AVR_R_QUADRANT_3_DATA + "','"
                    + _Jl.AVR_R_QUADRANT_4_DATA + "','"
                    + _Jl.AVR_R_STATE_1_CONC + "','"
                    + _Jl.AVR_R_STATE_2_CONC + "','"
                    + _Jl.AVR_A_DATA + "','"
                    + _Jl.AVR_B_DATA + "','"
                    + _Jl.AVR_C_DATA + "','"
                    + _Jl.AVR_SUM_ABC_CONC + "','"
                    + _Jl.AVR_SUM_RATES_CONC + "',"
                    + _Jl.FK_LNG_SCHEME_ID + ",'"
                    + _Jl.AVR_RECORD_OTHER + "','"
                    + _Jl.AVR_DIS_REASON + "')";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterFK(bool flag, MeterFK _Fk, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_RATES_CONTROL";
            }
            else
            {
                name = "TMP_METER_RATES_CONTROL";
            }
            try
            {
                _TmpSql = "Insert into " + name
                    + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,"
                    + "AVR_GROUP_TYPE,AVR_ITEM_TYPE,AVR_CONCLUSION,AVR_DATAS,FK_LNG_SCHEME_ID,AVR_DIS_REASON) Values ("
                    + _Fk._intMyId + ",'" + _TaiID + "'," + _Fk._intBno + ",'"
                    + _Fk.Mfk_chrGrpType + "','"
                    + _Fk.Mfk_chrItemType + "','"
                    + _Fk.Mfk_chrJL + "','"
                    + _Fk.Mfk_chrData + "',"
                    + _Fk.FK_LNG_SCHEME_ID + ",'"
                    + _Fk.AVR_DIS_REASON + "')";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterFLSDgn(bool flag, MeterFLSDgn _Fl, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_FUN_RATES_TIME_CONS";
            }
            else
            {
                name = "Tmp_METER_FUN_RATES_TIME_CONS";
            }
            try
            {

                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_PROJECT_NAME,AVR_GROUP_TYPE,AVR_LIST_NO,AVR_ITEM_TYPE,AVR_ITEM_CONCLUSION,"
                    + "AVR_TZ_CHANGE_CONC,AVR_TZ_STATE_DATA,AVR_TZ_DAT_CONC,AVR_TZ_WRITE_VALUE,AVR_TZ_READ_VALUE,AVR_TZ_PROGRAMMING_CONC,AVR_TZ_PROGRAMMING_RECORD,AVR_TZ_CONVT_FREEZE_CONC,AVR_TZ_CONVT_FREEZE_DATA,AVR_TC_CHANGE_CONC,AVR_TC_STATE_DATA,"
                    + "AVR_TC_DATJL,AVR_TC_WRITE_DATA,AVR_TC_READ_DATA,AVR_TC_PROGRAMMING_CONC,AVR_TC_PROGRAMMING_RECORD,AVR_TC_CONCT_FREEZE_CONC,AVR_TC_CONVT_FREEZE_DATA,AVR_HD_PROGRAMMING_CONC,AVR_HD_PROGRAMMING_RECORD,AVR_FT_PROGRAMMING_CONC,AVR_FT_PROGRAMMING_RECORD,"
                    + "AVR_TCC_CHANGE_CONC,AVR_TCC_SHARP_DATA,AVR_TCC_PEAK_DATA,AVR_TCC_ORDINARY_DATA,AVR_TCC_VALLEY_DATA,AVR_TCCP_CHANGE_CONC,AVR_TCCP_SHARP_DATA,AVR_TCCP_PEAK_DATA,AVR_TCCP_ORDINARY_DATA,AVR_TCCP_VALLEY_DATA,FK_LNG_SCHEME_ID,chrOther1,chrOther2,chrOther3,chrOther4,"
                    + "chrOther5) Values ("
                    + _Fl._intMyId + ",'"
                    + _Fl._intTaiNo + "',"
                    + _Fl._intBno + ",'"
                    + _Fl.Mfl_chrProjectName + "','"
                    + _Fl.Mfl_chrGrpType + "','"
                    + _Fl.Mfl_chrListNo + "','"
                    + _Fl.Mfl_intItemType + "','"
                    + _Fl.Mfl_chrItemJL + "','"
                    + _Fl.Mfl_chrSqZtzJL + "','"
                    + _Fl.Mfl_chrSqWdat + "','"
                    + _Fl.Mfl_chrSqRdat + "','"
                    + _Fl.Mfl_chrSqBcJL + "','"
                    + _Fl.Mfl_chrSqBcDat + "','"
                    + _Fl.Mfl_chrSqYddjJL + "','"
                    + _Fl.Mfl_chrSqYddjDat + "','"
                    + _Fl.Mfl_chrSdZtzJL + "','"
                    + _Fl.Mfl_chrSdZtzDat + "','"
                    + _Fl.Mfl_chrSdDatJL + "','"
                    + _Fl.Mfl_chrSdWdat + "','"
                    + _Fl.Mfl_chrSdRdat + "','"
                    + _Fl.Mfl_chrSdBcJL + "','"
                    + _Fl.Mfl_chrSdBcDat + "','"
                    + _Fl.Mfl_chrSdYddjJL + "','"
                    + _Fl.Mfl_chrSdYddjDat + "','"
                    + _Fl.Mfl_chrZxrBcJL + "','"
                    + _Fl.Mfl_chrZxrDat + "','"
                    + _Fl.Mfl_chrJjrBcJL + "','"
                    + _Fl.Mfl_chrJjrDat + "','"
                    + _Fl.Mfl_chrSdDlQhJL + "','"
                    + _Fl.Mfl_chrJdlDat + "','"
                    + _Fl.Mfl_chrFdlDat + "','"
                    + _Fl.Mfl_chrPdlDat + "','"
                    + _Fl.Mfl_chrGdlDat + "','"
                    + _Fl.Mfl_chrSdMCQhJL + "','"
                    + _Fl.Mfl_chrJmcDat + "','"
                    + _Fl.Mfl_chrFmcDat + "','"
                    + _Fl.Mfl_chrPmcDat + "','"
                    + _Fl.Mfl_chrGmcDat + "','"
                    + _Fl.Mfl_chrGmcDat + "','"
                    + _Fl.Mfl_chrGmcDat + "',"
                    + 0 + ",'"
                    + _Fl.Mfl_chrOther1 + "','"
                    + _Fl.Mfl_chrOther2 + "','"
                    + _Fl.Mfl_chrOther3 + "','"
                    + _Fl.Mfl_chrOther4 + "','"
                    + _Fl.Mfl_chrOther5 + "')";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterZZError(bool flag, MeterZZError _ZZError, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_ENERGY_TEST_DATA";
            }
            else
            {
                name = "TMP_METER_ENERGY_TEST_DATA";
            }
            try
            {
                _TmpSql = "Insert into " + name
                    + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,"
                    + "AVR_PROJECT_NO,CHR_POWER_TYPE,CHR_COMPONENT,AVR_POWER_FACTOR,AVR_IB_MULTIPLE,AVR_IB_MULTIPLE_STRING,AVR_RATES,AVR_START_TIME,AVR_NEED_TIME,AVR_NEED_ENERGY,AVR_START_ENERGY,AVR_END_ENERGY,AVR_START_SUM_ENERGY,"
                + "AVR_END_SUM_ENERGY,AVR_DIF_ENERGY,AVR_NUMBER_PULSES,AVR_STANDARD_METER_ENERGY,AVR_ERROR,AVR_TEST_WAY,AVR_CONCLUSION,FK_LNG_SCHEME_ID,AVR_DIS_REASON) values("
                    + _ZZError._intMyId + ",'" + _TaiID + "'," + _ZZError._intBno + ",'"
                    + _ZZError.Me_chrProjectNo + "','"
                    + _ZZError.Mz_chrJdfx + "','"
                    + _ZZError.Mz_chrYj + "','"
                    + _ZZError.Mz_chrGlys + "','"
                    + _ZZError.Mz_chrxIb + "','"
                    + _ZZError.Mz_chrxIbString + "','"
                    + _ZZError.Mz_chrFl + "','"
                    + _ZZError.Mz_chrStartTime + "','"
                    + _ZZError.Mz_chrNeedTime + "','"
                    + _ZZError.AVR_NEED_ENERGY + "','"
                    + _ZZError.Mz_chrQiMa.ToString() + "','"
                    + _ZZError.Mz_chrZiMa.ToString() + "','"
                    + _ZZError.Mz_chrQiMaZ + "','"
                    + _ZZError.Mz_chrZiMaZ + "','"
                    + _ZZError.Mz_chrQiZiMaC + "','"
                    + _ZZError.Mz_chrPules + "','"
                    + _ZZError.AVR_STANDARD_METER_ENERGY + "','"
                    + _ZZError.Mz_chrWc + "','"
                    + _ZZError.AVR_TEST_WAY + "','"
                    + _ZZError.Mz_chrJL + "',"
                    + _ZZError.FK_LNG_SCHEME_ID + ",'"
                    + _ZZError.AVR_DIS_REASON + "'"
                    + ")";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterCarrierData(bool flag, MeterCarrierData _carrier, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_CARRIER_WAVE";
            }
            else
            {
                name = "TMP_METER_CARRIER_WAVE";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_PROJECT_NO,AVR_ITEM_NAME,AVR_CONCLUSION,AVR_VALUES,DTM_START_TIME,DTM_END_TIME,AVR_NUMBER_TOTAL,"
                + "AVR_NUMBER_SUCCEED,AVR_NUMBER_FAIL,AVR_RATIO_SUCCEED,AVR_LIMIT,AVR_RESERVE,FK_LNG_SCHEME_ID,AVR_DIS_REASON) Values("
                    + _carrier._intMyId + ",'" + _carrier._intTaiNo + "'," + _carrier._intBno + ",'"
                    + _carrier.Mce_PrjNumber + "','"
                    + _carrier.Mce_PrjName + "','"
                    + _carrier.Mce_ItemResult + "','"
                    + _carrier.Mce_PrjValue + "',#"
                    + DateTime.Now.ToString() + "#,#"
                    + DateTime.Now.ToString() + "#,'"
                    + _carrier.AVR_NUMBER_TOTAL + "','"
                    + _carrier.AVR_NUMBER_SUCCEED + "','"
                    + _carrier.AVR_NUMBER_FAIL + "','"
                    + _carrier.AVR_RATIO_SUCCEED + "','"
                    + _carrier.AVR_LIMIT + "','"
                    + _carrier.AVR_RESERVE + "',"
                    + _carrier.FK_LNG_SCHEME_ID + ",'"
                    + _carrier.AVR_DIS_REASON + "'"
                    + ")";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// METER_HIGH_VOLTAGE交流电压试验表的sql语句(兼耐压)
        /// </summary>
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterHighVoltage(bool flag, MeterInsulation _insulation, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_HIGH_VOLTAGE";
            }
            else
            {
                name = "TMP_METER_HIGH_VOLTAGE";
            }
            try
            {
                _TmpSql = "Insert into " + name
                    + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,"
                    + "AVR_TYPE,AVR_V_VALUE,AVR_USE_TIME,AVR_A_LEAKAGE,AVR_RESULT_VALUE,FK_LNG_SCHEME_ID,AVR_DIS_REASON) values("
                    + _insulation._intMyId + ",'" + _TaiID + "'," + _insulation._intBno + ",'"
                    + _insulation.InsulationType + "','"
                    + _insulation.Voltage + "','"
                    + _insulation.Time + "','"
                    + _insulation.stringCurrent + "','"
                    + _insulation.Result + "','"
                    + _insulation.FK_LNG_SCHEME_ID + "','"
                    + _insulation.AVR_DIS_REASON + "'"
                    + ")";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }

        /// <summary>
        /// METER_FUN_LOAD_RECORD负荷记录表的sql语句
        /// </summary>
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterLoadRecord(bool flag, MeterLoadRecord _loadrecord, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_FUN_LOAD_RECORD";
            }
            else
            {
                name = "TMP_METER_FUN_LOAD_RECORD";
            }
            try
            {
                _TmpSql = "Insert into " + name
                    + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,"
                    + "AVR_PROJECT_NAME,AVR_PROJECT_SUB_NAME,AVR_LIST_NO,AVR_RESULT_VALUE,AVR_RECORD_OTHER,FK_LNG_SCHEME_ID,AVR_DIS_REASON) values("
                    + _loadrecord._intMyId + ",'" + _TaiID + "'," + _loadrecord._intBno + ",'"
                    + _loadrecord.Ml_PrjName + "','"
                    + _loadrecord.Ml_SubItemName + "','"
                    + _loadrecord.Ml_PrjID + "','"
                    + _loadrecord.Ml_Result + "','"
                    + _loadrecord.Ml_chrValue + "','"
                    + _loadrecord.FK_LNG_SCHEME_ID + "','"
                    + _loadrecord.AVR_DIS_REASON + "'"
                    + ")";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }
        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterXLgn(bool flag, MeterXLgn _Xl, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_FUN_MAX_DEMAND";
            }
            else
            {
                name = "TMP_METER_FUN_MAX_DEMAND";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_PROJECT_NAME,AVR_GRP_TYPE,AVR_LIST_NO,AVR_ITEM_TYPE,AVR_ITEM_CONC,AVR_GK,AVR_CLEAR_DEMAND_TYPE,"
                    + "AVR_PRAGRAMMING_FLAG,AVR_LESS_PERIOD_DATA,AVR_PRAGRAMMING_INFLUENCE,AVR_FREEZE_DATA,AVR_CLEAR_RECORD,AVR_DEMAND_CONC,AVR_LAST_ACTIVE_FORWARD,AVR_LAST_ACTIVE_REVERSE,AVR_LAST_R_GROUP_1,AVR_LAST_R_GROUP_2,AVR_LAST_R_QUADRANT_1,AVR_LAST_R_QUADRANT_2,"
                    + "AVR_LAST_R_QUADRANT_3,AVR_LAST_R_QUADRANT_4,AVR_ACTIVE_FORWARD,AVR_ACTIVE_REVERSE,AVR_R_GROUP_1,AVR_R_GROUP_2,AVR_R_QUADRANT_1,AVR_R_QUADRANT_2,AVR_R_QUADRANT_3,AVR_R_QUADRANT_4,FK_LNG_SCHEME_ID,AVR_DIS_REASON,AVR_OTHER_1,AVR_OTHER_2,AVR_OTHER_3,AVR_OTHER_4,AVR_OTHER_5,"
                    + "AVR_RECORD_OTHER) Values ('"
                    + _Xl.FK_LNG_METER_ID + "','"
                    + _Xl.AVR_DEVICE_ID + "',"
                    + _Xl.LNG_BENCH_POINT_NO + ",'"
                    + _Xl.AVR_PROJECT_NAME + "','"
                    + _Xl.AVR_GRP_TYPE + "','"
                    + _Xl.AVR_LIST_NO + "','"
                    + _Xl.AVR_ITEM_TYPE + "','"
                    + _Xl.AVR_ITEM_CONC + "','"
                    + _Xl.AVR_GK + "','"
                    + _Xl.AVR_CLEAR_DEMAND_TYPE + "','"
                    + _Xl.AVR_PRAGRAMMING_FLAG + "','"
                    + _Xl.AVR_LESS_PERIOD_DATA + "','"
                    + _Xl.AVR_PRAGRAMMING_INFLUENCE + "','"
                    + _Xl.AVR_FREEZE_DATA + "','"
                    + _Xl.AVR_CLEAR_RECORD + "','"
                    + _Xl.AVR_DEMAND_CONC + "','"
                    + _Xl.AVR_LAST_ACTIVE_FORWARD + "','"
                    + _Xl.AVR_LAST_ACTIVE_REVERSE + "','"
                    + _Xl.AVR_LAST_R_GROUP_1 + "','"
                    + _Xl.AVR_LAST_R_GROUP_2 + "','"
                    + _Xl.AVR_LAST_R_QUADRANT_1 + "','"
                    + _Xl.AVR_LAST_R_QUADRANT_2 + "','"
                    + _Xl.AVR_LAST_R_QUADRANT_3 + "','"
                    + _Xl.AVR_LAST_R_QUADRANT_4 + "','"
                    + _Xl.AVR_ACTIVE_FORWARD + "','"
                    + _Xl.AVR_ACTIVE_REVERSE + "','"
                    + _Xl.AVR_R_GROUP_1 + "','"
                    + _Xl.AVR_R_GROUP_2 + "','"
                    + _Xl.AVR_R_QUADRANT_1 + "','"
                    + _Xl.AVR_R_QUADRANT_2 + "','"
                    + _Xl.AVR_R_QUADRANT_3 + "','"
                    + _Xl.AVR_R_QUADRANT_4 + "',"
                    + _Xl.FK_LNG_SCHEME_ID + ",'"
                    + _Xl.AVR_DIS_REASON + "','"
                    + _Xl.AVR_OTHER_1 + "','"
                    + _Xl.AVR_OTHER_2 + "','"
                    + _Xl.AVR_OTHER_3 + "','"
                    + _Xl.AVR_OTHER_4 + "','"
                    + _Xl.AVR_OTHER_5 + "','"
                    +_Xl.AVR_RECORD_OTHER + "')";

                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }

        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterFreezes(bool flag, MeterFreeze Freeze, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_FREEZE";
            }
            else
            {
                name = "TMP_METER_FREEZE";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_RESULT_VALUE,FK_LNG_SCHEME_ID,AVR_PROJECT_NAME,AVR_PROJECT_NO,AVR_DIS_REASON)"
                    +" Values ("
                    + Freeze._intMyId + ",'"
                    + Freeze._intTaiNo + "','"
                    + Freeze._intBno + "','"
                    + Freeze.Md_chrValue + "',"
                    + Freeze.FK_LNG_SCHEME_ID + ",'"
                    + Freeze.Md_PrjName + "','"
                    + Freeze.Md_PrjID  + "','"                    
                    + Freeze.AVR_DIS_REASON  + "')";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }


        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterFunctions(bool flag, MeterFunction Function, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_FUNCTION";
            }
            else
            {
                name = "TMP_METER_FUNCTION";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_PROJECT_NO,AVR_PROJECT_NAME,AVR_VALUE,AVR_CONCLUSION,FK_LNG_SCHEME_ID,AVR_DIS_REASON)"
                    + " Values ("
                    + Function._intMyId + ",'"
                    + Function._intTaiNo + "','"
                    + Function._intBno + "','"
                    + Function.Mf_PrjID + "','"
                    + Function.Mf_PrjName + "','"
                    + Function.Mf_chrValue + "','"
                    + Function.Mf_Result + "',"
                    + Function.FK_LNG_SCHEME_ID + ",'"
                    + Function.AVR_DIS_REASON + "')";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }

        /// <summary>
        /// 表的sql语句
        /// </summary>
        /// fjk
        /// <param name="flag">true正式表，false临时表</param>
        /// <param name="_BasicInfo">一块表基本信息</param>
        /// <param name="_TmpSql">sql语句串</param>
        /// <param name="ErrMessage">失败信息</param>
        /// <returns>执行成功true、失败false</returns>
        private bool sql_MeterResult(bool flag, MeterResult _Result, out string _TmpSql, out string ErrMessage)
        {
            bool bln_Running = false;
            string name = "";

            _TmpSql = "";
            ErrMessage = "";

            if (flag)
            {
                name = "METER_RESULTS";
            }
            else
            {
                name = "TMP_METER_RESULTS";
            }
            try
            {
                _TmpSql = "Insert into " + name + " (FK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,AVR_RESULT_ID,AVR_RESULT_NAME,AVR_RESULT_VALUE,AVR_NOTE,FK_LNG_SCHEME_ID) Values("
                    + _Result._intMyId + ",'" + _TaiID + "'," + _Result._intBno + ",'"
                    + _Result.Mr_chrRstId + "','"
                    + _Result.Mr_chrRstName + "','"
                    + _Result.Mr_chrRstValue + "','"
                    + _Result.Mr_chrNote + "',"
                    + _Result.FK_LNG_SCHEME_ID + ")";
                bln_Running = true;
                ErrMessage = "";
            }
            catch (Exception e)
            {
                bln_Running = false;
                ErrMessage = e.Message;
            }
            return bln_Running;
        }

        /// <summary>
        /// 返回出错的内容
        /// </summary>
        /// <param name="result">错误代码</param>
        public static void CheckDBResult(bool result, string ErrMsg)
        {
            if (result != true)
            {
                string msg = "";
                msg = ":\r";
                msg += ErrMsg;

                throw new Exception(msg);
            }
        }
        #endregion


    }
}

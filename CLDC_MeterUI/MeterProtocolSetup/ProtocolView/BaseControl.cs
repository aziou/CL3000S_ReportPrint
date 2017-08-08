using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CLDC_Comm;
using CLDC_DataCore.Const;



namespace CLDC_MeterUI.ProtocolView
{
    public partial class BaseControl : UserControl
    {
        protected bool blnStop = false;

        /// <summary>
        /// 下一个
        /// </summary>
        protected bool Nexted = false;
        /// <summary>
        /// 测试协议
        /// </summary>
        protected CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo TestProtocol;
        /// <summary>
        /// 被检表通信地址
        /// </summary>
        protected string MeterAdr = "999999999999";
        /// <summary>
        /// 设备控制组件
        /// </summary>
        //protected VerifyAdapter.Helper.EquipHelper m_EquipMentUnit=null;//VerifyAdapter.EquipUnit m_EquipMentUnit = null;
        /// <summary>
        /// 检定结果(BOO)
        /// </summary>
        protected bool Result = false;

        protected string strResult = string.Empty;

        protected string[] arrResult = new string[0];

        private bool isReturn = false;

        public BaseControl()
        {
            InitializeComponent();
            //多功能事件
        }

        public CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo TestProtocolPra
        {
            set
            {
                TestProtocol = value;
            }

        }

        /// <summary>
        /// 设置/获取参数配置框窗口对象
        /// </summary>
        private Panel m_ParaPanel = null;
        protected Panel ParaPanel
        {
            set { m_ParaPanel = value; }
            get { return m_ParaPanel; }
        }

        #region----------开始、停止测试----------
        public virtual void StartTest(string Adr, float U, int Index)
        {
            //还原标识
            // ReSet();
            if (!getInfo(ref TestProtocol)) return;
            //Comm.GlobalUnit.ForceVerifyStop = false;
            blnStop = false;
            MeterAdr = Adr;
            //Comm.Enum.Cus_Clfs CLFS;
            float Ub = 0;
            if (U == 100)
            {
                // CLFS = Comm.Enum.Cus_Clfs.三相三线;
                Ub = 0;
            }
            else
            {
                // CLFS = Comm.Enum.Cus_Clfs.三相四线;
                Ub = U;
            }
            ////Comm.GlobalUnit.Clfs = CLFS;//更新测量方式
            //VerifyAdapter.Helper.EquipHelper.Instance.PowerOn(
            //    U, Ub, U, 0, 0, 0, Comm.Enum.Cus_PowerYuanJian.H, 50, "1.0", true, false, false, Comm.Enum.Cus_PowerFangXiang.正向有功);
            //// m_EquipMentUnit.SetTestPoint(CLFS, U, 0F, "1.0", Comm.Enum.Cus_PowerYuanJian.H, true);    //升源
            //设置表协议参数
            setMeterData(Index);
            int bwCount = GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_BWCOUNT, 24);

            bool[] isOpen = new bool[bwCount];
            for (int i = 0; i < isOpen.Length; i++) isOpen[i] = true;
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetCommSwitch(isOpen);

            //开始测试
            //DoTest(Index);
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(DoTest), Index); 
        }
        /// <summary>
        /// 测试
        /// </summary>
        private void DoTest(object objIndex)
        {
            int Index = (int)objIndex;
            //遍历所有下面的子控件
            foreach (Control _Item in ParaPanel.Controls)
            {
                if (_Item is CheckBox && ((CheckBox)_Item).Checked)
                {
                    if (blnStop) return;
                    this.Nexted = false;    //这个标志是在接收到返回信息后置为真！！切记
                    switch (((CheckBox)_Item).Name.ToLower())
                    {

                        case "chk_commtest":            //通信测试
                            Result = CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.CommTest(Index);
                            // IsReturn();
                            showResult("Result_CommTest", Result);
                            break;
                        case "chk_readtime":            //读时间
                            DateTime readTime = CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.ReadDateTime(Index);
                            //IsReturn();
                            showResult("Result_ReadTime", readTime.ToString(), readTime.Year == 1900);
                            break;
                        case "chk_writetime":           //写时间
                            string strDateTime = DateTime.Now.ToString("yyMMddHHmmss");
                            Result = CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.WriteDateTime(strDateTime, Index);
                            //IsReturn();
                            showResult("Result_WriteTime", Result);
                            break;
                        case "chk_clearxl":             //清空需量

                            Result = CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.ClearDemand(Index);
                            //IsReturn();
                            showResult("Result_ClearXL", Result);
                            break;
                        case "chk_readxl":              //读取需量
                            float demand = CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.ReadDemand(0, 0, Index);
                            //IsReturn();
                            showResult("Result_ReadXL", demand.ToString(), demand == -1F);
                            break;
                        case "chk_readdl":              //读取电量
                            float energy = CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.ReadEnergy(0, 0, Index);
                            //IsReturn();
                            showResult("Result_ReadDL", energy.ToString(), energy == -1F);

                            break;
                        case "chk_readsd":              //读时段

                            string[] sd = CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.ReadPeriodTime(Index);
                            // IsReturn();
                            showResult("Result_ReadSD", string.Join(" ", sd, 0, sd.Length), sd.Length == 0);
                            break;
                        case "chk_cleardl":             //清空电量
                            Result = CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.ClearEnergy(Index);
                            //IsReturn();
                            showResult("Result_ClearDL", Result);
                            break;
                        case "chk_eventlog":            //事件记录
                            // Do_EventLog(Index);
                            float ret = ReadData(CLDC_Comm.Enum.Cus_DgnProcotolPara.事件记录, Index);
                            showResult("Result_EventLog", ret != -1F);
                            break;
                        case "chk_nowrom":              //瞬时寄存器
                             ret = ReadData(CLDC_Comm.Enum.Cus_DgnProcotolPara.瞬时寄存器, Index);
                            showResult("Result_NowRom", ret != -1F);

                            //Do_Nowrom(Index);
                            break;
                        case "chk_state":               //状态寄存器
                             ret = ReadData(CLDC_Comm.Enum.Cus_DgnProcotolPara.状态寄存器, Index);
                            showResult("Result_State", ret != -1F);

                            break;
                        case "chk_lost":                //失压寄存器
                             ret = ReadData(CLDC_Comm.Enum.Cus_DgnProcotolPara.失压寄存器, Index);
                            showResult("Result_Lost", ret != -1);

                            break;
                        case "chk_run":                 //运行状态
                             ret = ReadData(CLDC_Comm.Enum.Cus_DgnProcotolPara.运行状态, Index);
                            showResult("Result_Run", ret != -1);

                            break;
                        case "chk_yff":                 //预付费
                             ret = ReadData(CLDC_Comm.Enum.Cus_DgnProcotolPara.预付费检查, Index);
                            showResult("Result_Yff", ret != -1);

                            break;
                        default:
                            this.Nexted = true;
                            break;

                    }


                    // this.WaitSleep(100);        //休眠，直到满足条件（条件为Nexted为真，或blnStop为真）  
                }
            }
            CLDC_DataCore.Const.GlobalUnit.g_MsgControl.OutMessage("所有项目测试完毕！", false, CLDC_Comm.Enum.Cus_MessageType.提示消息);
            //VerifyAdapter.Helper.EquipHelper.Instance.PowerOff();
        }
        #region---------- 检定过程----------
        /// <summary>
        /// 事件记录检定
        /// </summary>
        /// <param name="Index">表位号</param>
        private float ReadData(CLDC_Comm.Enum.Cus_DgnProcotolPara dType, int Index)
        {
            //获取标识码
            string[] arrPata = getType(((int)dType).ToString("000"));
            //string strLogValue = string.Empty;
            try
            {
                float retValue = CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.ReadData(
                     arrPata[0], int.Parse(arrPata[1]), int.Parse(arrPata[2]), Index);
                return retValue;
                //IsReturn();
                //if (!Result)
                // {
                //    throw new Exception("读取" + dType.ToString() + "失败!");
                // }
                //strLogValue = strResult;    //记录下读取到的内容
                //ReSet();                    //清空标识 
                //string strDateTime = DateTime.Now.ToString("yyMMddHHmmss");
                //VerifyAdapter.MeterProtocolAdapter.Instance.WriteDateTime(Index, strDateTime);
                //IsReturn();
                //if (!Result)
                //{
                //    throw new Exception("对表进行写操作失败!");
                //}
                //System.Threading.Thread.Sleep(500);
                ////再次读取
                //ReSet();        //清空标识 
                //VerifyAdapter.MeterProtocolAdapter.Instance.ReadData(Index,
                //    arrPata[0], int.Parse(arrPata[1]), int.Parse(arrPata[2]));
                //IsReturn();
                //if (!Result)
                //{
                //    throw new Exception("第二次读取事件记录失败!");
                //}
                //int j = int.Parse(strLogValue);
                //int k = int.Parse(strResult);
                //Result = k > j;
                //showResult("Result_EventLog", Result);
            }
            catch (Exception e)
            {
                CLDC_DataCore.Const.GlobalUnit.g_MsgControl.OutMessage(e.Message, false, CLDC_Comm.Enum.Cus_MessageType.提示消息);
                return -1F;
            }
        }

        /// <summary>
        /// 瞬时寄存器
        /// </summary>
        /// <param name="Index">表位号</param>
        private void Do_Nowrom(int Index)
        {

        }

        #endregion
        /// <summary>
        ///      停止测试
        /// </summary>
        public virtual void StopTest()
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.PowerOff();
        }
        #endregion

        #region----------设置表通讯参数----------
        private void setMeterData(int Index)
        {
            int BwCount = CLDC_VerifyAdapter.Adapter.Instance.BwCount;
            CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.SetBwCount(BwCount);
            //ClInterface.CAmMeterInfo[] meterDgnInfo = new CAmMeterInfo[BwCount];
            if (!(TestProtocol is CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo))
            {
                MessageBox.Show("协议加载失败！");
                return;
            }
            TestProtocol.Load();
            string[] arrAddress = new string[BwCount];
            CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo[] protocols = new CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo[BwCount];
            for (int k = 0; k < BwCount; k++)
            {
                // meterDgnInfo[k] = new CAmMeterInfo();
                protocols[k] = TestProtocol;
                arrAddress[k] = MeterAdr;
                //if (k == Index)
                //{

                //VerifyAdapter.MeterProtocolAdapter.Instance.Selected[k] = true;
                //meterDgnInfo[k].DllFile = TestProtocol.DllFile;     //动态
                //meterDgnInfo[k].ClassName = TestProtocol.ClassName;
                //meterDgnInfo[k].Address = MeterAdr;
                //meterDgnInfo[k].Setting = TestProtocol.Setting;
                //meterDgnInfo[k].UserID = TestProtocol.UserID;
                //meterDgnInfo[k].VerifyPasswordType = TestProtocol.VerifyPasswordType;
                //meterDgnInfo[k].WritePassword = TestProtocol.WritePassword;
                //meterDgnInfo[k].WritePswClass = TestProtocol.WriteClass;
                //meterDgnInfo[k].ClearDemandPassword = TestProtocol.ClearDemandPassword;
                //meterDgnInfo[k].ClearDemandPswClass = TestProtocol.ClearDemandClass;
                //meterDgnInfo[k].ClearEnergyPassword = TestProtocol.ClearDLPassword;
                //meterDgnInfo[k].ClearEnergyPswClass = TestProtocol.ClearDLClass;
                //meterDgnInfo[k].DataFieldPassword = TestProtocol.DataFieldPassword;
                //meterDgnInfo[k].BlockAddAA = TestProtocol.BlockAddAA;
                //meterDgnInfo[k].TariffOrderType = TestProtocol.TariffOrderType;
                //meterDgnInfo[k].DateTimeFormat = TestProtocol.DateTimeFormat;
                //meterDgnInfo[k].SundayIndex = TestProtocol.SundayIndex;
                //meterDgnInfo[k].ConfigFile = TestProtocol.ConfigFile;
                //meterDgnInfo[k].ComTestType = this.getType("001", 1);
                //// meterDgnInfo[k].BroadcastTimeType = 1;
                //meterDgnInfo[k].ReadEnergyType = this.getType("006", 1);
                //meterDgnInfo[k].ReadDemandType = this.getType("005", 1);
                //meterDgnInfo[k].ReadDateTimeType = this.getType("003", 1);
                ////meterDgnInfo[k].ReadAddressType = 1;
                //meterDgnInfo[k].ReadPeriodTimeType = this.getType("007", 1);
                //// meterDgnInfo[k].ReadDataType = 1;
                //// meterDgnInfo[k].WriteAddressType = 1;
                //meterDgnInfo[k].WriteDateTimeType = this.getType("002", 1);
                //// meterDgnInfo[k].WritePeriodTimeType = 1;
                //// meterDgnInfo[k].WriteDataType = 1;
                //meterDgnInfo[k].ClearDemandType = this.getType("004", 1);
                //meterDgnInfo[k].ClearEnergyType = this.getType("008", 1);
                ////meterDgnInfo[k].ClearEventLogType = 1;
                //// meterDgnInfo[k].SetPulseComType = 1;
                //// meterDgnInfo[k].FreezeCmdType = 1;
                ////meterDgnInfo[k].ChangeSettingType = 1;
                ////meterDgnInfo[k].ChangePasswordType = 1;
                //meterDgnInfo[k].FECount = TestProtocol.FECount;
                //后面跟其它信息
                //}
            }
            CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.Initialize(protocols, arrAddress);
            //VerifyAdapter.MeterProtocolAdapter.Instance.AmMeterInfo = meterDgnInfo;
        }
        //获取多功能协议配置参数
        protected int getType(string PramKey, int DefaultValue)
        {
            Dictionary<string, string> DgnPram = TestProtocol.DgnPras;
            if (!DgnPram.ContainsKey(PramKey))
            {
                return DefaultValue;
            }
            string[] Arr_Pram = DgnPram[PramKey].Split('|');
            if (Arr_Pram.Length == 2)
            {
                return int.Parse(Arr_Pram[0]);
            }
            else
            {
                return DefaultValue;
            }
        }

        protected string[] getType(string PramKey)
        {
            Dictionary<string, string> DgnPram = TestProtocol.DgnPras;
            if (!DgnPram.ContainsKey(PramKey))
            {
                return new string[0];
            }
            return DgnPram[PramKey].Split('|');
        }


        #endregion

        protected void ReSet()
        {
            Result = false;

            strResult = string.Empty;

            arrResult = new string[0];

            isReturn = false;
        }
        public virtual bool getInfo(ref CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo ProtocolInfo)
        {
            return true;
        }

        #region----------设备控制模块----------

        //多功能初始化
        private void InitDgn()
        {
            //VerifyAdapter.MeterProtocolAdapter.Instance.BWCount = 1;
            //事件绑定
            //VerifyAdapter.MeterProtocolAdapter.Instance.OnEventRxFrame += new ClInterface.Dge_EventRxFrame(m_IMeterControler_OnEventRxFrame);
            //VerifyAdapter.MeterProtocolAdapter.Instance.OnEventTxFrame += new ClInterface.Dge_EventTxFrame(m_IMeterControler_OnEventTxFrame);
            //VerifyAdapter.MeterProtocolAdapter.Instance.OnEventCommTest += new ClInterface.DelegateEventCommTest(m_IMeterControler_OnEventCommTest);
            //VerifyAdapter.MeterProtocolAdapter.Instance.OnEventReadDateTime += new DelegateReadDateTime(m_IMeterControler_OnEventReadDateTime);
            //VerifyAdapter.MeterProtocolAdapter.Instance.OnEventWriteDateTime += new DelegateWriteDateTime(m_IMeterControler_OnEventWriteDateTime);
            //VerifyAdapter.MeterProtocolAdapter.Instance.OnEventClearDemand += new DelegateClearDemand(m_IMeterControler_OnEventClearDemand);
            //VerifyAdapter.MeterProtocolAdapter.Instance.OnEventReadDemand += new DelegateReadDemand(m_IMeterControler_OnEventReadDemand);
            //VerifyAdapter.MeterProtocolAdapter.Instance.OnEventReadEnergy += new DelegateReadEnergy(m_IMeterControler_OnEventReadEnergy);
            //VerifyAdapter.MeterProtocolAdapter.Instance.OnEventReadPeriodTime += new DelegateReadPeriodTime(m_IMeterControler_OnEventReadPeriodTime);
            //VerifyAdapter.MeterProtocolAdapter.Instance.OnEventClearEnergy += new DelegateClearEnergy(m_IMeterControler_OnEventClearEnergy);
            //VerifyAdapter.MeterProtocolAdapter.Instance.OnEventReadData += new DelegateReadData(m_IMeterControler_OnEventReadData);
        }





        #endregion

        #region ----------多功能返回事件----------
        //通讯测试
        void m_IMeterControler_OnEventCommTest(int int_Index, bool bln_Result)
        {

            Result = bln_Result;
            isReturn = true;

            //throw new Exception("The method or operation is not implemented.");
        }
        //读取时间
        void m_IMeterControler_OnEventReadDateTime(int int_Index, bool bln_Result, string str_DateTime)
        {
            Result = bln_Result;
            isReturn = true;
            strResult = str_DateTime;
            //throw new Exception("The method or operation is not implemented.");
        }


        void m_IMeterControler_OnEventTxFrame(string str_Frame)
        {

            CLDC_DataCore.Const.GlobalUnit.g_485DataControl.OutMessage("==>" + str_Frame);
            // throw new Exception("The method or operation is not implemented.");
        }

        void m_IMeterControler_OnEventRxFrame(string str_Frame)
        {
            CLDC_DataCore.Const.GlobalUnit.g_485DataControl.OutMessage("<==" + str_Frame);
            //throw new Exception("The method or operation is not implemented.");
        }
        void m_IMeterControler_OnEventClearEnergy(int int_Index, bool bln_Result)
        {
            Result = bln_Result;
            isReturn = true;
            //throw new Exception("The method or operation is not implemented.");
        }

        void m_IMeterControler_OnEventReadPeriodTime(int int_Index, bool bln_Result, string[] str_PTime)
        {
            Result = bln_Result;
            isReturn = true;
            strResult = string.Join("", str_PTime);
            //throw new Exception("The method or operation is not implemented.");
        }

        void m_IMeterControler_OnEventReadEnergy(int int_Index, bool bln_Result, float[] sng_Energy)
        {
            Result = bln_Result;
            isReturn = true;
            strResult = sng_Energy[0].ToString();
            //throw new Exception("The method or operation is not implemented.");
        }

        void m_IMeterControler_OnEventReadDemand(int int_Index, bool bln_Result, float[] sng_Demand)
        {
            Result = bln_Result;
            isReturn = true;
            strResult = sng_Demand[0].ToString();
            //throw new Exception("The method or operation is not implemented.");
        }

        void m_IMeterControler_OnEventClearDemand(int int_Index, bool bln_Result)
        {
            Result = bln_Result;
            isReturn = true;
            //throw new Exception("The method or operation is not implemented.");
        }

        void m_IMeterControler_OnEventWriteDateTime(int int_Index, bool bln_Result)
        {
            Result = bln_Result;
            isReturn = true;
            //throw new Exception("The method or operation is not implemented.");
        }
        void m_IMeterControler_OnEventReadData(int int_Index, bool bln_Result, string str_Value)
        {
            Result = bln_Result;
            strResult = str_Value;
            isReturn = true;
            //throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region----------检测返回----------
        protected void IsReturn()
        {
            //while (!isReturn)
            //{
            //    System.Threading.Thread.Sleep(25);
            //    if (isReturn)
            //        break;
            //    if (Comm.GlobalUnit.ForceVerifyStop)
            //        break;
            //    if (blnStop)
            //        break;
            //}
        }
        #endregion

        #region  ----------显示结果----------
        /// <summary>
        /// 显示数据到指定控件
        /// </summary>
        /// <param name="control">控制名称</param>
        /// <param name="isHeGe">是否合格,如果不合格，文件将以红色突出显示</param>
        protected void showResult(string controlName, bool isHeGe)
        {
            //if (!ParaPanel.Controls.ContainsKey(controlName))
            //    return;
            //Control control = ParaPanel.Controls[controlName];
            if (isHeGe)
            {
                showResult(controlName, CLDC_DataCore.Const.Variable.CMG_HeGe, false);
            }
            else
            {
                showResult(controlName, CLDC_DataCore.Const.Variable.CMG_BuHeGe, true);
            }
        }
        /// <summary>
        /// 显示数据到指定控件
        /// </summary>
        /// <param name="ctrl">控件名称</param>
        /// <param name="text">要显示的文件</param>
        /// <param name="isRead">是否红色显示</param>
        protected void showResult(string controlName, string text, bool isRead)
        {
            if (!ParaPanel.Controls.ContainsKey(controlName))
                return;
            Control ctrl = ParaPanel.Controls[controlName];
            CLDC_DataCore.Function.SetControl.SetForceColor(ctrl, Color.Black);
            CLDC_DataCore.Function.SetControl.SetText(ctrl, text);
            if (isRead)
            {
                CLDC_DataCore.Function.SetControl.SetForceColor(ctrl, Color.Red);
            }

        }
        #endregion
    }
}

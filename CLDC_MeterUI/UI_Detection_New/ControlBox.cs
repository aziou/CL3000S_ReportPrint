using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using DevComponents.DotNetBar;
using CLDC_Encryption;
using System.Runtime.InteropServices;

namespace CLDC_MeterUI.UI_Detection_New
{
    public partial class ControlBox : Office2007Form
    {
        #region 
        private static ControlBox instance;
        private static object syncRoot = new Object();
        
        public static ControlBox Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ControlBox();
                    }
                }
                return instance;
            }
        }
        public void ShowD()
        {
            if (instance != null)
            {
                if (instance.Visible != true)
                {
                    instance.ShowDialog();
                }

            }
        }
        #endregion

        EncryptionManager em = new EncryptionManager();//加密机对象
        private int BwCount;
        public ControlBox()
        {
            InitializeComponent();
            this.TopMost = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            CLDC_DataCore.Const.GlobalUnit.g_CommunType = CLDC_Comm.Enum.Cus_CommunType.通讯485;
            RegsvrClickEventHandler();
            EncryptionTool = encryptionFactory.CreateEncryptionControler();
            BwCount = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws;
            
        }

        private void RegsvrClickEventHandler()
        {
            #region 注销事件
            btn_DownDJ.Click -= new EventHandler(btn_DownDJ_Click);
            btn_UpDJ.Click -= new EventHandler(btn_UpDJ_Click);
            btn_OnDJ.Click -= new EventHandler(btn_OnDJ_Click);
            btn_BackDJ.Click -= new EventHandler(btn_BackDJ_Click);
            btn_BwPass.Click -= new EventHandler(btn_BwPass_Click);
            btn_BwReset.Click -= new EventHandler(btn_BwReset_Click);
            btn_MCJSState.Click -= new EventHandler(btn_MCJSState_Click);
            btn_ReadElectStates.Click -= new EventHandler(btn_ReadElectStates_Click);
            btn_ReadIaGH.Click -= new EventHandler(btn_ReadIaGH_Click);
            btn_ReadIbGH.Click -= new EventHandler(btn_ReadIbGH_Click);
            btn_ReadIcGH.Click -= new EventHandler(btn_ReadIcGH_Click);
            btn_ReadUaGH.Click -= new EventHandler(btn_ReadUaGH_Click);
            btn_ReadUbGH.Click -= new EventHandler(btn_ReadUbGH_Click);
            btn_ReadUcGH.Click -= new EventHandler(btn_ReadUcGH_Click);
            btn_RJSState.Click -= new EventHandler(btn_RJSState_Click);
            btn_StopAllStates.Click -= new EventHandler(btn_StopAllStates_Click);
            btn_StopCurrentState.Click -= new EventHandler(btn_StopCurrentState_Click);
            btn_WCState.Click -= new EventHandler(btn_WCState_Click);
            btn_XLZQState.Click -= new EventHandler(btn_XLZQState_Click);
            btn_InitCarr.Click -= new EventHandler(btn_InitCarr_Click);
            btn_ZBD.Click -= new EventHandler(btn_ZBD_Click);
            //btn_ReadCT.Click -= new EventHandler(btn_ReadCT_Click);
            btu_CT100A.Click -= new EventHandler(btu_CT100A_Click);
            btu_CT2A.Click -= new EventHandler(btu_CT2A_Click);
            btu_giveEquipmentPower.Click -= new EventHandler(btu_giveEquipmentPower_Click);
            btu_StopEquipmentPower.Click -= new EventHandler(btu_StopEquipmentPower_Click);
            btu_HuiLu1.Click -= new EventHandler(btu_HuiLu1_Click);
            btu_HuiLu2.Click -= new EventHandler(btu_HuiLu2_Click);
            btu_address.Click -= new EventHandler(btu_address_Click);
            btu_JDQ.Click -= new EventHandler(btu_JDQ_Click);
            btu_JDQ2.Click -= new EventHandler(btu_JDQ2_Click);
            btu_JDQ3.Click -= new EventHandler(btu_JDQ3_Click);
            btn_JDQ3HG.Click -= new EventHandler(btn_JDQ3HG_Click);
            btn_uifordJDQ.Click -= new EventHandler(btn_uifordJDQ_Click);
            //三色灯设置显示
            //循显三色灯
            buttonX1.Click -= new EventHandler(btn_X1_Click);
            //黄
            buttonX2.Click -= new EventHandler(btn_X2_Click);
            //绿
            buttonX3.Click -= new EventHandler(btn_X3_Click);
            //红
            buttonX4.Click -= new EventHandler(btn_X4_Click);
            //灭
            buttonX5.Click -= new EventHandler(btn_X5_Click);
            //切时钟脉冲

            btn_Time.Click -= new EventHandler(btn_Time_Click);
            //切为标准脉冲
            btn_StdMaiCon.Click -= new EventHandler(btn_StdMaiCon_Click);
            //读取标准表数据
            btn_ReadStdMeter.Click -= new EventHandler(btn_ReadStdMeter_Click);
            //设定标准表档位
            btn_SetStdMeterR.Click -= new EventHandler(btn_SetStdMeter_Click);
            //设定标准表常数
            btn_SetStdMeterConst.Click -= new EventHandler(btn_SetStdMeterConst_Click);
            //启动有功脉冲输出
            btn_SetEgOut.Click -= new EventHandler(btn_SetEgOut_Click);
            //停止脉冲输出
            btn_SetEgOutStop.Click -= new EventHandler(btn_SetEgOutStop_Click);
            //无功脉冲输出
            btn_SetEgOutWg.Click -= new EventHandler(btn_SetEgOutWg_Click);
            //读取标准表常数
            btn_setStdConsunit.Click -= new EventHandler(btn_setStdConsunit_Click);

            btn_ErrorOfPlateLimit.Click -= new EventHandler(btn_ErrorOfPlateLimit_Click);
            btn_HighVOutput.Click -= new EventHandler(btn_HighVOutput_Click);
            btn_PrepareWSV.Click -= new EventHandler(btn_PrepareWSV_Click);
            btn_ShutDownHighVoltage.Click -= new EventHandler(btn_ShutDownHighVoltage_Click);
            btn_WSVLimit.Click -= new EventHandler(btn_WSVLimit_Click);
            btn_ReadWcbGHParam.Click -= new EventHandler(btn_ReadWcbGHParam_Click);
            btn_ReadWcbTemperature.Click -= new EventHandler(btn_Temperature_Click);
            btn_YXOut.Click -= new EventHandler(btn_YXOut_Click);
            btn_ZLOut.Click -= new EventHandler(btn_ZLOut_Click);
            btn_YXStop.Click -= new EventHandler(btn_YXStop_Click);
            btn_FuKDL.Click -= new EventHandler(btn_FuKDL_Click);
            btn_FuKFW.Click -= new EventHandler(btn_FuKFW_Click);
            btn_FuKKL.Click -= new EventHandler(btn_FuKKL_Click);
            btn_DgnJS.Click -= new EventHandler(btn_DgnJS_Click);
            btn_DuiBiao.Click -= new EventHandler(btn_DuiBiao_Click);
            btn_OutData.Click -= new EventHandler(btn_OutData_Click);
            btn_ClearFreamLog.Click -= new EventHandler(btn_ClearFreamLog_Click);
            #region 加密机
            btnOpenUsbkey.Click -= new EventHandler(btnOpenUsbkey_Click);
            btnLgServer.Click -= new EventHandler(btnLgServer_Click);
            btnIdentityAuthentication.Click -= new EventHandler(btnIdentityAuthentication_Click);
            btnGetInd2013.Click -= new EventHandler(btnGetInd2013_Click);
            btnRand.Click -= new EventHandler(btnRand_Click);
            btnClose.Click -= new EventHandler(btnClose_Click);
            btnNo.Click -= new EventHandler(btnNo_Click);
            buttonX9.Click -= new EventHandler(buttonX9_Click);
            btn_TestRongtong.Click -= new EventHandler(btn_TestRongtong_Click);
            btnOpenDevice.Click -= new EventHandler(btnOpenDevice_Click);
            btnFCLgServer.Click -= new EventHandler(btnFCLgServer_Click);

            
             btnLinkSouth.Click -= new EventHandler(btnLinkSouth_Click);
            btnUnLinkSouth.Click -= new EventHandler(btnUnLinkSouth_Click);
            btnSouthUpdata.Click -= new EventHandler(btnSouthUpdata_Click);
           
            #endregion
            btn_StartRepeat.Click -= new EventHandler(btn_StartRepeat_Click);
            btn_StopRepeat.Click -= new EventHandler(btn_StopRepeat_Click);
            btn_DevIdc.Click -= new EventHandler(btn_DevIdc_Click);
            btn_InfraredStart.Click -= new EventHandler(btn_InfraredStart_Click);
            btn_InfraredStop.Click -= new EventHandler(btn_InfraredStop_Click);
            btn_InfraredRead.Click -= new EventHandler(btn_InfraredRead_Click);
            btn_ReadReversalStatus.Click -= new EventHandler(btn_ReadReversalStatus_Click);

            btn_ReadGPS.Click -= new EventHandler(btn_ReadGPS_Click);
            btn_SetPCTime.Click -= new EventHandler(btn_SetPCTime_Click);
            btn_shunshizheng90.Click -= new EventHandler(btn_shunshizheng90_Click);
            btn_Nishizheng90.Click -= new EventHandler(btn_Nishizheng90_Click);
            btn_shunshizheng1.Click -= new EventHandler(btn_shunshizheng1_Click);
            btn_Nishizheng1.Click -= new EventHandler(btn_Nishizheng1_Click);
            #endregion
            //
            #region 注册事件
            btn_DownDJ.Click += new EventHandler(btn_DownDJ_Click);
            btn_UpDJ.Click += new EventHandler(btn_UpDJ_Click);
            btn_OnDJ.Click += new EventHandler(btn_OnDJ_Click);
            btn_BackDJ.Click += new EventHandler(btn_BackDJ_Click);
            btn_BwPass.Click += new EventHandler(btn_BwPass_Click);
            btn_BwReset.Click += new EventHandler(btn_BwReset_Click);
            btn_MCJSState.Click += new EventHandler(btn_MCJSState_Click);
            btn_ReadElectStates.Click += new EventHandler(btn_ReadElectStates_Click);
            btn_ReadIaGH.Click += new EventHandler(btn_ReadIaGH_Click);
            btn_ReadIbGH.Click += new EventHandler(btn_ReadIbGH_Click);
            btn_ReadIcGH.Click += new EventHandler(btn_ReadIcGH_Click);
            btn_ReadUaGH.Click += new EventHandler(btn_ReadUaGH_Click);
            btn_ReadUbGH.Click += new EventHandler(btn_ReadUbGH_Click);
            btn_ReadUcGH.Click += new EventHandler(btn_ReadUcGH_Click);
            btn_RJSState.Click += new EventHandler(btn_RJSState_Click);
            btn_StopAllStates.Click += new EventHandler(btn_StopAllStates_Click);
            btn_StopCurrentState.Click += new EventHandler(btn_StopCurrentState_Click);
            btn_WCState.Click += new EventHandler(btn_WCState_Click);
            btn_XLZQState.Click += new EventHandler(btn_XLZQState_Click);
            btn_InitCarr.Click += new EventHandler(btn_InitCarr_Click);
            btn_ZBD.Click += new EventHandler(btn_ZBD_Click);
            //btn_ReadCT.Click += new EventHandler(btn_ReadCT_Click);//读取CT档位 ,zjl 20130926
            btu_CT100A.Click += new EventHandler(btu_CT100A_Click);//切换档位100A,zjl 20130926
            btu_CT2A.Click += new EventHandler(btu_CT2A_Click);//切换档位2A,zjl 20130926
            btu_giveEquipmentPower.Click += new EventHandler(btu_giveEquipmentPower_Click);//台体供电,zjl 20130926
            btu_StopEquipmentPower.Click += new EventHandler(btu_StopEquipmentPower_Click);//台体断电,zjl 20130926
            btu_HuiLu1.Click += new EventHandler(btu_HuiLu1_Click);//一回路,zjl 20130926
            btu_HuiLu2.Click += new EventHandler(btu_HuiLu2_Click);//二回路,zjl 20130926
            btu_address.Click += new EventHandler(btu_address_Click);//读取表地址,zjl 20130926
            btu_JDQ.Click += new EventHandler(btu_JDQ_Click);//继电器控制板 耐压供电,zjl 20130926
            btu_JDQ2.Click += new EventHandler(btu_JDQ2_Click);//继电器控制板 载波供电,zjl 20130926
            btu_JDQ3.Click += new EventHandler(btu_JDQ3_Click);//继电器控制板 普通供电,zjl 20130926
            btn_JDQ3HG.Click += new EventHandler(btn_JDQ3HG_Click);
            btn_uifordJDQ.Click += new EventHandler(btn_uifordJDQ_Click); //电压电流对地 //zxr 20141108
            //三色灯设置显示
            btn_YellowS.Click += new EventHandler(btn_YellowS_Click);
            btn_GreenS.Click += new EventHandler(btn_GreenS_Click);
            btn_RedS.Click += new EventHandler(btn_RedS_Click);
            //循显三色灯
            buttonX1.Click += new EventHandler(btn_X1_Click);
            //红
            buttonX2.Click += new EventHandler(btn_X2_Click);
            //绿
            buttonX3.Click += new EventHandler(btn_X3_Click);
            //黄
            buttonX4.Click += new EventHandler(btn_X4_Click);
            //灭
            buttonX5.Click += new EventHandler(btn_X5_Click);
            //切时钟脉冲

            btn_Time.Click += new EventHandler(btn_Time_Click);
            //切为标准脉冲
            btn_StdMaiCon.Click += new EventHandler(btn_StdMaiCon_Click);
            //读取标准表数据
            btn_ReadStdMeter.Click += new EventHandler(btn_ReadStdMeter_Click);
            //设定标准表档位
            btn_SetStdMeterR.Click += new EventHandler(btn_SetStdMeter_Click);
            //设定标准表常数
            btn_SetStdMeterConst.Click += new EventHandler(btn_SetStdMeterConst_Click);
            //启动有功脉冲输出
            btn_SetEgOut.Click += new EventHandler(btn_SetEgOut_Click);
            //停止脉冲输出
            btn_SetEgOutStop.Click += new EventHandler(btn_SetEgOutStop_Click);
            //无功脉冲输出
            btn_SetEgOutWg.Click += new EventHandler(btn_SetEgOutWg_Click);
            //读取标准表常数
            btn_setStdConsunit.Click += new EventHandler(btn_setStdConsunit_Click);

            btn_ErrorOfPlateLimit.Click += new EventHandler(btn_ErrorOfPlateLimit_Click);
            btn_HighVOutput.Click += new EventHandler(btn_HighVOutput_Click);
            btn_PrepareWSV.Click += new EventHandler(btn_PrepareWSV_Click);
            btn_ShutDownHighVoltage.Click += new EventHandler(btn_ShutDownHighVoltage_Click);
            btn_WSVLimit.Click += new EventHandler(btn_WSVLimit_Click);
            btn_ReadWcbGHParam.Click += new EventHandler(btn_ReadWcbGHParam_Click);
            btn_ReadWcbTemperature.Click += new EventHandler(btn_Temperature_Click);
            btn_YXOut.Click += new EventHandler(btn_YXOut_Click);
            btn_ZLOut.Click += new EventHandler(btn_ZLOut_Click);
            btn_YXStop.Click += new EventHandler(btn_YXStop_Click);
            btn_FuKDL.Click += new EventHandler(btn_FuKDL_Click);
            btn_FuKFW.Click += new EventHandler(btn_FuKFW_Click);
            btn_FuKKL.Click += new EventHandler(btn_FuKKL_Click);
            btn_DgnJS.Click += new EventHandler(btn_DgnJS_Click);
            btn_DuiBiao.Click += new EventHandler(btn_DuiBiao_Click);
            btn_OutData.Click += new EventHandler(btn_OutData_Click);
            btn_ClearFreamLog.Click += new EventHandler(btn_ClearFreamLog_Click);
            #region 加密机
            btnOpenUsbkey.Click += new EventHandler(btnOpenUsbkey_Click);
            btnLgServer.Click += new EventHandler(btnLgServer_Click);
            btnIdentityAuthentication.Click += new EventHandler(btnIdentityAuthentication_Click);
            btnGetInd2013.Click += new EventHandler(btnGetInd2013_Click);
            btnRand.Click += new EventHandler(btnRand_Click);
            btnClose.Click += new EventHandler(btnClose_Click);
            btnNo.Click += new EventHandler(btnNo_Click);
            buttonX9.Click += new EventHandler(buttonX9_Click);//控制命令密文
            btn_TestRongtong.Click += new EventHandler(btn_TestRongtong_Click);
            btnOpenDevice.Click+=new EventHandler(btnOpenDevice_Click);
            btnFCLgServer.Click+=new EventHandler(btnFCLgServer_Click);

            btnLinkSouth.Click += new EventHandler(btnLinkSouth_Click);
            btnUnLinkSouth.Click += new EventHandler(btnUnLinkSouth_Click);
            btnSouthUpdata.Click += new EventHandler(btnSouthUpdata_Click);
            #endregion
            btn_StartRepeat.Click += new EventHandler(btn_StartRepeat_Click);
            btn_StopRepeat.Click += new EventHandler(btn_StopRepeat_Click);
            btn_DevIdc.Click += new EventHandler(btn_DevIdc_Click);
            btn_InfraredStart.Click += new EventHandler(btn_InfraredStart_Click);
            btn_InfraredStop.Click += new EventHandler(btn_InfraredStop_Click);
            btn_InfraredRead.Click += new EventHandler(btn_InfraredRead_Click);
            btn_ReadReversalStatus.Click += new EventHandler(btn_ReadReversalStatus_Click);

            btn_ReadGPS.Click += new EventHandler(btn_ReadGPS_Click);
            btn_SetPCTime.Click += new EventHandler(btn_SetPCTime_Click);
            btn_shunshizheng90.Click += new EventHandler(btn_shunshizheng90_Click);
            btn_Nishizheng90.Click += new EventHandler(btn_Nishizheng90_Click);
            btn_shunshizheng1.Click += new EventHandler(btn_shunshizheng1_Click);
            btn_Nishizheng1.Click += new EventHandler(btn_Nishizheng1_Click);
            #endregion
        }

        void btnSouthUpdata_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void btnUnLinkSouth_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void btnLinkSouth_Click(object sender, EventArgs e)
        {
            //SouthLink
          bool rts=  CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.SouthLink();
           
               
        }

        void btn_Nishizheng1_Click(object sender, EventArgs e)
        {
            bool[] blBwcount =new bool[BwCount];
            for(int i =0 ;i<this.BwCount ; i++)
            {
                blBwcount[i]=true ;
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReversalMachineryPole(blBwcount, 1,ushort.Parse(txtSudu.Text), 1);
        }

        void btn_shunshizheng1_Click(object sender, EventArgs e)
        {
            bool[] blBwcount = new bool[BwCount];
            for (int i = 0; i < this.BwCount; i++)
            {
                blBwcount[i] = true;
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReversalMachineryPole(blBwcount, 0, ushort.Parse(txtSudu.Text), 1);
        }

        void btn_Nishizheng90_Click(object sender, EventArgs e)
        {
            bool[] blBwcount = new bool[BwCount];
            for (int i = 0; i < this.BwCount; i++)
            {
                blBwcount[i] = true;
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReversalMachineryPole(blBwcount, 1, ushort.Parse(txtSudu.Text), 90);
        }

        void btn_shunshizheng90_Click(object sender, EventArgs e)
        {
            bool[] blBwcount = new bool[BwCount];
            for (int i = 0; i < this.BwCount; i++)
            {
                blBwcount[i] = true;
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReversalMachineryPole(blBwcount, 0, ushort.Parse(txtSudu.Text), 90);
        }

        #region 修改电脑时间
        [DllImport("Kernel32.dll")]
        public static extern void GetLocalTime(SystemTime st);
        [DllImport("Kernel32.dll")]
        public static extern void SetLocalTime(SystemTime st);


        [StructLayout(LayoutKind.Sequential)]
        public class SystemTime
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort Whour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;

        }
        private bool isReReadGPS = false;
        private DateTime dtGPS = new DateTime();
        void btn_SetPCTime_Click(object sender, EventArgs e)
        {
            if (isReReadGPS)
            {
                try
                {

                    SystemTime st = new SystemTime();
                    st.wYear = (ushort)dtGPS.Year;
                    st.wMonth = (ushort)dtGPS.Month;
                    st.wDay = (ushort)dtGPS.Day;
                    st.Whour = (ushort)dtGPS.Hour;
                    st.wMinute = (ushort)dtGPS.Minute;
                    st.wSecond = (ushort)dtGPS.Second;
                    SetLocalTime(st);
                    MessageBoxEx.Show(this,"系统时间设置成功!", "系统时间设置成功!");
                }
                catch
                {
                    MessageBoxEx.Show(this,"系统时间设置失败!", "设置失败");
                }
                isReReadGPS = false;
            }
        }

        void btn_ReadGPS_Click(object sender, EventArgs e)
        {
            dtGPS = CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadGpsTime();
            txt_GPS.Text = dtGPS.ToString("yyyy-MM-dd HH:mm:ss");
            isReReadGPS = true;

        }
        #endregion


        #region 人体红外检测
        void btn_InfraredRead_Click(object sender, EventArgs e)
        {

            int[] InfraredState = new int[BwCount];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.GetInfraredState(out InfraredState);
            if (Array.IndexOf(InfraredState, 1) != -1)
            {
                lab_InfraredState.Text = "1";
                lab_InfraredState.BackColor = Color.Red;
            }
            else
            {
                lab_InfraredState.Text = "0";
                lab_InfraredState.BackColor = Color.Transparent;
            }
        }

        void btn_InfraredStop_Click(object sender, EventArgs e)
        {
            bool[] InfraredState = new bool[BwCount];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.StopInfrared(out InfraredState);
            if (Array.IndexOf(InfraredState, false) == -1)
            {
                MessageBoxEx.Show(this, "停止成功。", "提示");
            }
            else
            {
                MessageBoxEx.Show(this, "停止失败！", "提示");
            }
        }

        void btn_InfraredStart_Click(object sender, EventArgs e)
        {
            bool[] InfraredState = new bool[BwCount];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.StartInfrared(out InfraredState);
            if (Array.IndexOf(InfraredState, false) == -1)
            {
                MessageBoxEx.Show(this, "启动成功。", "提示");
            }
            else
            {
                MessageBoxEx.Show(this, "启动失败！", "提示");
            }
        }
        #endregion

        #region 循环电机测试
        /// <summary>
        /// 停止循环电机
        /// </summary>
        private bool bln_StopRepeat = true;
        /// <summary>
        /// 间隔时间
        /// </summary>
        private int splittime
        {
            get
            {
                int tm = 10000;
                if (!string.IsNullOrEmpty(txt_spliteTime.Text))
                {
                    int.TryParse(txt_spliteTime.Text, out tm);
                }
                return tm;
            }
        }
        /// <summary>
        /// 停止循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_StopRepeat_Click(object sender, EventArgs e)
        {
            btn_StartRepeat.Enabled = true;
            bln_StopRepeat = true; 
        }
        /// <summary>
        /// 开始循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_StartRepeat_Click(object sender, EventArgs e)
        {
            btn_StartRepeat.Enabled = false;
            bln_StopRepeat = false;
            Thread thr = new Thread(new ThreadStart(() =>
            {
                bool[] rest = new bool[0];
                string[] strRD = new string[0];
                int tmp = 0;
                bool[] YJMeter = null;
                while (!bln_StopRepeat)
                {
                    //down
                    BoxYJMeter(ref YJMeter);
                    if (Array.IndexOf(YJMeter, true) != -1)
                    {
                        CLDC_VerifyAdapter.Helper.EquipHelper.Instance.EquipmentPress(true, YJMeter, out rest, out strRD);
                    }
                    //else
                    //{
                    //    break;
                    //}
                    //waitting
                    tmp = splittime;
                    while (tmp > 0 && !bln_StopRepeat)
                    {
                        Thread.Sleep(1000);
                        tmp--;
                    }
                    if (bln_StopRepeat)
                    {
                        break;
                    }
                    //up
                    BoxYJMeter(ref YJMeter);
                    if (Array.IndexOf(YJMeter, true) != -1)
                    {
                        CLDC_VerifyAdapter.Helper.EquipHelper.Instance.EquipmentPress(false, YJMeter, out rest, out strRD);
                    }
                    //else
                    //{
                    //    break;
                    //}
                    //waitting
                    tmp = splittime;
                    while (tmp > 0 && !bln_StopRepeat)
                    {
                        Thread.Sleep(1000);
                        tmp--;
                    }

                }
            }));
            thr.Start();
        }
        #endregion

        #region  清空报文库 导出功耗数据txt
        /// <summary>
        /// 清空报文库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ClearFreamLog_Click(object sender, EventArgs e)
        {
            if (CLDC_DataCore.Const.GlobalUnit.FrameLog.DeleteAll())
            {
                CLDC_DataCore.Const.GlobalUnit.FrameLog.CompressAccess();
                MessageBoxEx.Show(this, "清空成功。");
            }
            else
            {
                MessageBoxEx.Show(this, "清空失败！");
            }

        }
        /// <summary>
        /// 导出功耗数据txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_OutData_Click(object sender, EventArgs e)
        {
            int _RCount = dgv_GH.Rows.Count;
            int _CCount = dgv_GH.Columns.Count;
            string[] strDatas = new string[_RCount];
            string strs = "";
            for (int chi = 0; chi < _CCount; chi++)
            {
                strs += dgv_GH.Columns[chi].HeaderText + "\t"; 
            }
            strDatas[0] += System.Environment.NewLine;
            for (int ri = 0; ri < _RCount; ri++)
            {
                DataGridViewRow _row = dgv_GH.Rows[ri];
                for (int ci = 0; ci < _CCount; ci++)
                {
                    strDatas[ri] += _row.Cells[ci].Value.ToString() + "\t";
                }
                strDatas[ri] += System.Environment.NewLine;
            }
            
            for (int i = 0; i < _RCount; i++)
            {
                strs += strDatas[i];
            }

            if (CLDC_DataCore.Function.File.WriteTXT("功耗导出数据", strs))
            {
                MessageBoxEx.Show(this, "导出数据成功。");
            }
            else
            {
                MessageBoxEx.Show(this, "导出数据失败！");
            }
        }
        #endregion

        #region 对标，多功能计数，
        void btn_DuiBiao_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.InitPara_DuiSeBiao(CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功, 220, 0.01F, 1);
        }

        void btn_DgnJS_Click(object sender, EventArgs e)
        {
            bool[] YJMeter = null;
            BoxYJMeter(ref YJMeter);
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                YJMeter[i] = true;
            }
            int[] pulseCount=new int[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.InitTimePeriod(YJMeter, pulseCount);
        }
        #endregion

        #region 遥信，直流，二次开路、短路
        void btn_FuKKL_Click(object sender, EventArgs e)
        {
            bool[] YJMeter = null;
            BoxYJMeter(ref YJMeter);
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                YJMeter[i] = true;
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.RequestFuKJDQ(YJMeter, 1);
        }

        void btn_FuKFW_Click(object sender, EventArgs e)
        {
            bool[] YJMeter = null;
            BoxYJMeter(ref YJMeter);
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                YJMeter[i] = true;
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.RequestFuKJDQ(YJMeter, 0);
        }

        void btn_FuKDL_Click(object sender, EventArgs e)
        {
            bool[] YJMeter = null;
            BoxYJMeter(ref YJMeter);
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                YJMeter[i] = true;
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.RequestFuKJDQ(YJMeter, 2);
        }
        //停止遥信、直流
        void btn_YXStop_Click(object sender, EventArgs e)
        {
            bool[] YJMeter = null;
            BoxYJMeter(ref YJMeter);
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                YJMeter[i] = true;
            }
            //停止遥信
            int yXTestNo = 1;
            for (int i = 0; i < 8; i++)
            {
                Control ctrYX = panelEx_YX.Controls["chk_YX" + (i + 1)];
                if (ctrYX is DevComponents.DotNetBar.Controls.CheckBoxX)
                {
                    DevComponents.DotNetBar.Controls.CheckBoxX yxchk = ctrYX as DevComponents.DotNetBar.Controls.CheckBoxX;
                    if (yxchk.Checked == false)
                    {
                        continue;
                    }
                    yXTestNo = int.Parse(yxchk.Text) - 1;
                }
                CLDC_VerifyAdapter.Helper.EquipHelper.Instance.RequestStopPCYXZL(YJMeter, 0, yXTestNo);
                Thread.Sleep(1000);
            }
            //停止直流
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.RequestStopPCYXZL(YJMeter,1, 0);
        }

        void btn_ZLOut_Click(object sender, EventArgs e)
        {
            bool[] YJMeter = null;
            BoxYJMeter(ref YJMeter);
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                YJMeter[i] = true;
            }
            float flt_ZL = 0;
            if (txt_ZhiLiu.Text.Trim() != "")
            {
                flt_ZL = float.Parse(txt_ZhiLiu.Text);
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.RequestStartZLMN(YJMeter, flt_ZL);
        }

        void btn_YXOut_Click(object sender, EventArgs e)
        {
            bool[] YJMeter = null;
            BoxYJMeter(ref YJMeter);
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                YJMeter[i] = true;
            }
            int yXTestNo = 1;
            int yxTestType = 0;
            int yxTestPulseNum = 10;
            float yxTestPulseHz = 50;
            float yxTestOutMultiple = 0.5F;
            for (int i = 0; i < 8; i++)
            {
                Control ctrYX = panelEx_YX.Controls["chk_YX" + (i + 1)];
                if (ctrYX is DevComponents.DotNetBar.Controls.CheckBoxX)
                {
                    DevComponents.DotNetBar.Controls.CheckBoxX yxchk = ctrYX as DevComponents.DotNetBar.Controls.CheckBoxX;
                    if (yxchk.Checked == false)
                    {
                        continue;
                    }
                    yXTestNo = int.Parse(yxchk.Text) - 1;//0-7是1-8路
                }
                yxTestType = chk_DianPing.Checked ? 0 : 1;
                if (yxTestType == 1)
                {
                    if (txt_MCGS.Text.Trim() != "")
                    {
                        yxTestPulseNum = int.Parse(txt_MCGS.Text);
                    }
                    if (txt_MCHz.Text.Trim() != "")
                    {
                        yxTestPulseHz = float.Parse(txt_MCHz.Text);
                    }
                    if (txt_ZKB.Text.Trim() != "")
                    {
                        yxTestOutMultiple = float.Parse(txt_ZKB.Text);
                    }
                }
                CLDC_VerifyAdapter.Helper.EquipHelper.Instance.RequestStartYXOut(YJMeter, yXTestNo, yxTestType, yxTestPulseNum, yxTestPulseHz, yxTestOutMultiple);
                Thread.Sleep(1000);
            }
        }
        #endregion

        #region 耐压
        /// <summary>
        /// 写耐压仪阀值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_WSVLimit_Click(object sender, EventArgs e)
        {
            string strL = txt_WSVLimit.Text;
            if (string.IsNullOrEmpty(strL.Trim()))
            {
                strL = "15";
            }
            float lmt = Convert.ToSingle(strL);
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.WriteThresholdValue(lmt);
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ShutDownHighVoltage_Click(object sender, EventArgs e)
        {
            
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.WriteControlCmdData(false);
        }
        /// <summary>
        /// 复位耐压仪状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_PrepareWSV_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ResetStateData();
            
        }
        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_HighVOutput_Click(object sender, EventArgs e)
        {
            string strL = txt_HighVOutput.Text;
            int needTime = 10;
            int.TryParse(txt_NeedTime.Text.Trim(), out needTime);
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetTime(needTime);
            if (string.IsNullOrEmpty(strL.Trim()))
            {
                strL = "220";
            }
            float lmt = Convert.ToSingle(strL);
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.WriteVoltage(lmt);
            Thread.Sleep(200);
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.WriteControlCmdData(true);
        }
        /// <summary>
        /// 设置误差板漏电流，并启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ErrorOfPlateLimit_Click(object sender, EventArgs e)
        {
            string strL = txt_ErrorOfPlateLimit.Text;
            if (string.IsNullOrEmpty(strL.Trim()))
            {
                strL = "5";
            }
            float lmt = Convert.ToSingle(strL);
            bool[] isOnOff = new bool[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                isOnOff[i] = true;
            }
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.WSVSetErrorOfPlateLimit(isOnOff, lmt);
            //Thread.Sleep(2000);
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.InitWSV(isOnOff, lmt,1);
        }
        #endregion

        #region 载波
        //TODO:载波
        //读时间
        void btn_ZBD_Click(object sender, EventArgs e)
        {
            CLDC_DataCore.Const.GlobalUnit.g_CommunType = CLDC_Comm.Enum.Cus_CommunType.通讯载波;
            CLDC_VerifyAdapter.Adapter.Instance.UpdateMeterProtocol(); 
            //CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.SetBwCount(CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws);
            //CLDC_VerifyAdapter.Helper.MeterDataHelper.Instance.Init();
            //CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.ReadDateTime();
            //bool bLoadProtocol = CLDC_DataCore.Const.GlobalUnit.Meter(CLDC_DataCore.Const.GlobalUnit.FirstYaoJianMeter).DgnProtocol.Loading;
            //if (bLoadProtocol == false)
            //{
            //    MessageBoxEx.Show(this, "协议加载失败。");
            //    return;
            //}
            DateTime dt = CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.ReadDateTime(0);
            CLDC_DataCore.Const.GlobalUnit.g_CommunType = CLDC_Comm.Enum.Cus_CommunType.通讯485;
        }
        //初始化
        void btn_InitCarr_Click(object sender, EventArgs e)
        {
            string str_addr = txt_Address.Text.Trim().PadLeft(12, '0');
            
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.Init2041(0);

            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.AddCarrierNode(1, str_addr, 0);

            CLDC_DataCore.Const.GlobalUnit.g_CommunType = CLDC_Comm.Enum.Cus_CommunType.通讯485;
        }
        #endregion

        #region 误差版
        /// <summary>
        /// 需量周期状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_XLZQState_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.InitPara_InitDemandPeriod(1, 3);
        }
        /// <summary>
        /// 误差状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_WCState_Click(object sender, EventArgs e)
        {
            int[] circleCount=new int[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.InitPara_BasicError(CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功, circleCount,false  );
        }
        /// <summary>
        /// 停止当前功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_StopCurrentState_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetCurFunctionOnOrOff(false);
        }
        /// <summary>
        /// 停止误差板所有功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_StopAllStates_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 日计时状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_RJSState_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.InitPara_InitTimeAccuracy();
        }
        /// <summary>
        /// 脉冲计数状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_MCJSState_Click(object sender, EventArgs e)
        {
            int[] impluseCount = new int[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.InitPara_Constant(CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功, impluseCount);
        }
        #endregion

        #region 误差板温度
        void btn_Temperature_Click(object sender, EventArgs e)
        {
            string[][] tagTemperatureA = null;
            string[][] tagTemperatureB = null;
            string[][] tagTemperatureC = null; 

            bool[] YJMeter = null;
            BoxYJMeter(ref YJMeter);
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                YJMeter[i] = true;
            }
            if (Array.IndexOf(YJMeter, true) != -1)
            {
                CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadErrPltTemperature(YJMeter, out tagTemperatureA, out tagTemperatureB, out tagTemperatureC);
            }
            if (tagTemperatureA == null)
            {
                return;
            }
            int count = tagTemperatureA.Length;
            dgv_GH.Rows.Clear();
//单相1回路温度值进温度/三相查询相别进温度
//单相1回路温度值出温度/三相查询相别进温度
//单相2回路温度值进温度/
//单相2回路温度值出温度/
            dgv_GH.Columns.Clear();
            dgv_GH.Columns.Add("Col0", "表位");
            if (CLDC_DataCore.Const.GlobalUnit.IsDan == true)
            {
                dgv_GH.Columns.Add("Col1", "1回路温度进");
                dgv_GH.Columns.Add("Col2", "1回路温度出");
                dgv_GH.Columns.Add("Col3", "2回路温度进");
                dgv_GH.Columns.Add("Col4", "2回路温度出");
                
                
                for (int i = 0; i < count; i++)
                {
                    int rd = dgv_GH.Rows.Add();
                    DataGridViewRow row = dgv_GH.Rows[rd];
                    row.Cells[0].Value = i + 1;
                    if (null != tagTemperatureA[i] && null != tagTemperatureB[i] && null != tagTemperatureC[i])
                    {
                        row.Cells[1].Value = tagTemperatureA[i][0];
                        row.Cells[2].Value = tagTemperatureA[i][1];
                        row.Cells[3].Value = tagTemperatureA[i][2];
                        row.Cells[4].Value = tagTemperatureA[i][3];
                        
                    }
                }
            }
            else
            {

                dgv_GH.Columns.Add("Col1", "温度A进");
                dgv_GH.Columns.Add("Col2", "温度A出");
                dgv_GH.Columns.Add("Col3", "温度B进");
                dgv_GH.Columns.Add("Col4", "温度B出");
                dgv_GH.Columns.Add("Col5", "温度C进");
                dgv_GH.Columns.Add("Col6", "温度C出");
                

                for (int i = 0; i < count; i++)
                {
                    int rd = dgv_GH.Rows.Add();
                    DataGridViewRow row = dgv_GH.Rows[rd];
                    row.Cells[0].Value = i + 1;
                    if (null != tagTemperatureA[i] && null != tagTemperatureB[i] && null != tagTemperatureC[i])
                    {
                        row.Cells[1].Value = tagTemperatureA[i][0];
                        row.Cells[2].Value = tagTemperatureA[i][1];
                        
                        row.Cells[3].Value = tagTemperatureB[i][0];
                        row.Cells[4].Value = tagTemperatureB[i][1];
                        
                        row.Cells[5].Value = tagTemperatureC[i][0];
                        row.Cells[6].Value = tagTemperatureC[i][1];
                        
                    }
                }
            }
        }
        #endregion

        #region 功耗
        /// <summary>
        /// 功耗数据，计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ReadWcbGHParam_Click(object sender, EventArgs e)
        {
            CLDC_DataCore.Struct.stGHPram[] tagGH = null;
            bool[] YJMeter = null;
            BoxYJMeter(ref YJMeter);
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                YJMeter[i] = true;
            }
            if (Array.IndexOf(YJMeter, true) != -1)
            {
                CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadErrPltGHPram(YJMeter, out tagGH);
            }
            if (tagGH == null)
            { 
                return;
            }
            int count=tagGH.Length;
            dgv_GH.Rows.Clear();
            //string strGH="表位 A相电压回路电流 B相电压回路电流 C相电压回路电压 A相电流回路电压 B相电流回路电压 C相电流回路电压 A相电压回路相角 B相电压回路相角 C相电压回路相角\r\n";
            for (int i = dgv_GH.Columns.Count; dgv_GH.Columns.Count > 13; i--)
            {
                dgv_GH.Columns.Remove(dgv_GH.Columns[i - 1]);
            }
            bool isDan = CLDC_DataCore.Const.GlobalUnit.IsDan;
            string[] hname = new string[22] { "表位", 
                "Ua_I(mA)", "Ub_I(mA)", "Uc_I(mA)", 
                "Ia_U(mV)", "Ib_U(mV)", "Ic_U(mV)", 
                "Ua_Phi(°)", "Ub_Phi(°)", "Uc_Phi(°)", 
                "Ua_P(W)", "Ua_Q(W)", "Ua_S(VA)", 
                "Ub_P(W)", "Ub_Q(W)", "Ub_S(VA)", 
                "Uc_P(W)", "Uc_Q(W)", "Uc_S(VA)", 
                "Ia_S(W)", "Ib_S(W)", "Ic_S(W)" };
            if (isDan == true)
            {
                string[] hnameD = new string[10] { "表位", 
                    "U_I(mA)", "L1_U(mV)", "L2_U(mV)", "U_Phi", 
                    "U_P(W)", "U_Q(W)", "U_S(VA)", 
                    "L1_S(VA)", "L2_S(VA)" };
                hname = hnameD;
            }
            
            int hnameCount = hname.Length;
            for (int i = dgv_GH.Columns.Count; dgv_GH.Columns.Count > hnameCount; i--)
            {
                dgv_GH.Columns.Remove(dgv_GH.Columns[i - 1]);
            }
            for (int i = dgv_GH.Columns.Count; dgv_GH.Columns.Count < hnameCount; i++)
            {
                dgv_GH.Columns.Add("Col" + i, hname[i]);
            }
            int cols = dgv_GH.Columns.Count;
            for (int i = 1; i < cols; i++)
            {
                dgv_GH.Columns[i].HeaderText = hname[i];
            }

            if (isDan == true)
            {
                for (int i = 0; i < count; i++)
                {
                    int rd = dgv_GH.Rows.Add();
                    DataGridViewRow row = dgv_GH.Rows[rd];
                    row.Cells[0].Value = tagGH[i].MeterIndex + 1;
                    row.Cells[1].Value = tagGH[i].AU_Ia_or_I.ToString("F6");
                    row.Cells[2].Value = tagGH[i].BU_Ib_or_L1_U.ToString("F6");
                    row.Cells[3].Value = tagGH[i].CU_Ic_or_L2_U.ToString("F6");
                    row.Cells[4].Value = tagGH[i].AU_Phia_or_Phi.ToString("F6");
                    
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    int rd = dgv_GH.Rows.Add();
                    DataGridViewRow row = dgv_GH.Rows[rd];
                    row.Cells[0].Value = tagGH[i].MeterIndex + 1;
                    row.Cells[1].Value = tagGH[i].AU_Ia_or_I.ToString("F6");
                    row.Cells[2].Value = tagGH[i].BU_Ib_or_L1_U.ToString("F6");
                    row.Cells[3].Value = tagGH[i].CU_Ic_or_L2_U.ToString("F6");
                    row.Cells[4].Value = tagGH[i].AI_Ua.ToString("F6");
                    row.Cells[5].Value = tagGH[i].BI_Ub.ToString("F6");
                    row.Cells[6].Value = tagGH[i].CI_Uc.ToString("F6");
                    row.Cells[7].Value = tagGH[i].AU_Phia_or_Phi.ToString("F6");
                    row.Cells[8].Value = tagGH[i].AU_Phia_or_Phi.ToString("F6");
                    row.Cells[9].Value = tagGH[i].AU_Phia_or_Phi.ToString("F6");

                }
            }
            //标准表值
            CLDC_DataCore.Struct.StPower tagPower = new CLDC_DataCore.Struct.StPower();
            tagPower = CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadPowerInfo();
            if (isDan == true)
            {
                for (int i = 0; i < count; i++)
                {
                    DataGridViewRow row = dgv_GH.Rows[i];
                    //A相电压回路
                    double Upa = tagPower.Ua * tagGH[i].AU_Ia_or_I * Math.Cos(tagGH[i].AU_Phia_or_Phi / 180F * Math.PI) / 1000F;
                    double Uqa = tagPower.Ua * tagGH[i].AU_Ia_or_I * Math.Sin(tagGH[i].AU_Phia_or_Phi / 180F * Math.PI) / 1000F;
                    double Usa = tagPower.Ua * tagGH[i].AU_Ia_or_I / 1000F;
                    
                    double IL1s = tagPower.Ia * tagGH[i].BU_Ib_or_L1_U / 1000F;
                    double IL2s = tagPower.Ia * tagGH[i].CU_Ic_or_L2_U / 1000F;
                    

                    row.Cells[5].Value = Upa;
                    row.Cells[6].Value = Uqa;
                    row.Cells[7].Value = Usa;
                    row.Cells[8].Value = IL1s;
                    row.Cells[9].Value = IL2s;
                    
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    DataGridViewRow row = dgv_GH.Rows[i];
                    //A相电压回路
                    double Upa = tagPower.Ua * tagGH[i].AU_Ia_or_I * Math.Cos(tagGH[i].AU_Phia_or_Phi / 180F * Math.PI) / 1000F;
                    double Uqa = tagPower.Ua * tagGH[i].AU_Ia_or_I * Math.Sin(tagGH[i].AU_Phia_or_Phi / 180F * Math.PI) / 1000F;
                    double Usa = Math.Sqrt(Upa * Upa + Uqa * Uqa);
                    //B相电压回路
                    double Upb = tagPower.Ub * tagGH[i].BU_Ib_or_L1_U * Math.Cos(tagGH[i].AU_Phia_or_Phi / 180F * Math.PI) / 1000F;
                    double Uqb = tagPower.Ub * tagGH[i].BU_Ib_or_L1_U * Math.Sin(tagGH[i].AU_Phia_or_Phi / 180F * Math.PI) / 1000F;
                    double Usb = Math.Sqrt(Upb * Upb + Uqb * Uqb);
                    //C相电压回路
                    double Upc = tagPower.Uc * tagGH[i].CU_Ic_or_L2_U * Math.Cos(tagGH[i].AU_Phia_or_Phi / 180F * Math.PI) / 1000F;
                    double Uqc = tagPower.Uc * tagGH[i].CU_Ic_or_L2_U * Math.Sin(tagGH[i].AU_Phia_or_Phi / 180F * Math.PI) / 1000F;
                    double Usc = Math.Sqrt(Upc * Upc + Uqc * Uqc);
                    //A相电流回路
                    //double Ipa = tagPower.Ia * tagGH[i].AI_Ua * Math.Cos(tagGH[i].??? / 180F * Math.PI) / 1000F;
                    //double Iqa = tagPower.Ia * tagGH[i].AI_Ua * Math.Sin(tagGH[i].??? / 180F * Math.PI) / 1000F;
                    double Isa = tagPower.Ia * tagGH[i].AI_Ua / 1000F;
                    double Isb = tagPower.Ib * tagGH[i].BI_Ub / 1000F;
                    double Isc = tagPower.Ic * tagGH[i].CI_Uc / 1000F;

                    row.Cells[10].Value = Upa;
                    row.Cells[11].Value = Uqa;
                    row.Cells[12].Value = Usa;
                    row.Cells[13].Value = Upb;
                    row.Cells[14].Value = Uqb;
                    row.Cells[15].Value = Usb;
                    row.Cells[16].Value = Upc;
                    row.Cells[17].Value = Uqc;
                    row.Cells[18].Value = Usc;
                    
                    row.Cells[19].Value = Isa;
                    row.Cells[20].Value = Isb;
                    row.Cells[21].Value = Isc;
                    
                }
            }
        }
        /// <summary>
        /// 读Uc功耗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ReadUcGH_Click(object sender, EventArgs e)
        {
            int ghBw = 0;
            if (!GHBwCheck(out ghBw))
            {
                return;
            }
            float[] flt_PD = new float[4];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadPowerDissipation(ghBw, 5, out flt_PD);
            setGHValue(flt_PD);
        }
        /// <summary>
        /// 读Ub功耗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ReadUbGH_Click(object sender, EventArgs e)
        {
            int ghBw = 0;
            if (!GHBwCheck(out ghBw))
            {
                return;
            }
            float[] flt_PD = new float[4];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadPowerDissipation(ghBw, 3, out flt_PD);
            setGHValue(flt_PD);
        }
        /// <summary>
        /// 读Ua功耗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ReadUaGH_Click(object sender, EventArgs e)
        {
            int ghBw = 0;
            if (!GHBwCheck(out ghBw))
            {
                return;
            }
            float[] flt_PD = new float[4];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadPowerDissipation(ghBw, 1, out flt_PD);
            setGHValue(flt_PD);
        }
        /// <summary>
        /// 读Ic功耗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ReadIcGH_Click(object sender, EventArgs e)
        {
            int ghBw = 0;
            if (!GHBwCheck(out ghBw))
            {
                return;
            }
            float[] flt_PD = new float[4];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadPowerDissipation(ghBw, 6, out flt_PD);
            setGHValue(flt_PD);
        }
        /// <summary>
        /// 读Ib功耗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ReadIbGH_Click(object sender, EventArgs e)
        {
            int ghBw = 0;
            if (!GHBwCheck(out ghBw))
            {
                return;
            }
            float[] flt_PD = new float[4];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadPowerDissipation(ghBw, 4, out flt_PD);
            setGHValue(flt_PD);
        }

        private void setGHValue(float[] flt_PD)
        {
            txt_UxGH.Text = flt_PD[0].ToString() + "V";
            txt_IxGH.Text = flt_PD[1].ToString() + "A";
            txt_YGGH.Text = flt_PD[2].ToString() + "W";
            txt_WGGH.Text = flt_PD[3].ToString() + "Var";
        }
        /// <summary>
        /// 读Ia功耗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ReadIaGH_Click(object sender, EventArgs e)
        {
            int ghBw = 0;
            if (!GHBwCheck(out ghBw))
            {
                return;
            }
            float[] flt_PD = new float[4];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadPowerDissipation(ghBw, 2, out flt_PD);
            setGHValue(flt_PD);
        }

        private bool GHBwCheck(out int ghBw)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            bool rs = int.TryParse(txt_GHBW.Text, out ghBw);
            if (rs == false || ghBw <= 0)
            {
                MessageBoxEx.Show(this, "请输入正确的表位号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                rs = false;
            }
            return rs;
        }
        #endregion
        
        #region 复位隔离
        /// <summary>
        /// 表位复位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_BwReset_Click(object sender, EventArgs e)
        {
            //bool[] YJMeter = null;
            //BoxYJMeter(ref YJMeter);
            bool[] isOnOff = new bool[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                isOnOff[i] = true; //复位//YJMeter[i];//
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetMeterOnOff(isOnOff);
        }
        /// <summary>
        /// 表位隔离
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_BwPass_Click(object sender, EventArgs e)
        {
            bool[] YJMeter = null;
            BoxYJMeter(ref YJMeter);
            bool[] isOnOff = new bool[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                isOnOff[i] = !YJMeter[i];//false 隔离
            }

            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetMeterOnOff(isOnOff);
        }
        #endregion

        #region private 显示处理
        /// <summary>
        /// 显示处理
        /// </summary>
        private void setStatus(int index,StatusType st, Color cr)
        {
            statesShow1.setStatus(index, st, cr);
            
        }
        /// <summary>
        /// 显示处理
        /// </summary>
        private void setStatus(int index, StatusType st, string strFmt)
        {
            statesShow1.setStatus(index, st, statesShow1.ConvertToImage(strFmt));

        }
        #endregion

        #region 电机
        /// <summary>
        /// 压电机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DownDJ_Click(object sender, EventArgs e)
        {

            if (DialogResult.No == MessageBoxEx.Show(this, "下压电机，请确定电表放置正确！避免电表损坏！", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            btn_DownDJ.Enabled = false;
            bool[] rest = new bool[0];
            string[] strRD = new string[0];

            bool[] YJMeter = null;
            BoxYJMeter(ref YJMeter);
            if (Array.IndexOf(YJMeter, true) != -1)
            {
                this.BeginInvoke(new Action<string>(v =>
                {
                    CLDC_VerifyAdapter.Helper.EquipHelper.Instance.EquipmentPress(true, YJMeter, out rest, out strRD);
                    btn_DownDJ.Enabled = true;
                }), "");

            }
            else
            {
                btn_DownDJ.Enabled = true;
            }
            
        }

        private void BoxYJMeter(ref bool[] YJMeter)
        {
            YJMeter = new bool[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            bool[] BWChked = statesShow1.GetBWCheckeds();
            Array.Copy(BWChked, YJMeter, YJMeter.Length);
        }
        /// <summary>
        /// 松开电机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_UpDJ_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBoxEx.Show(this, "松开电机，请确定电表的倾斜位置！避免电表掉落损坏！", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            btn_UpDJ.Enabled = false;
            bool[] rest = new bool[0];
            string[] strRD = new string[0];
            bool[] BWChked = statesShow1.GetBWCheckeds();
            bool[] YJMeter = new bool[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            Array.Copy(BWChked, YJMeter, YJMeter.Length);
            if (Array.IndexOf(YJMeter, true) != -1)
            {
                this.BeginInvoke(new Action<string>(v =>
                {
                    CLDC_VerifyAdapter.Helper.EquipHelper.Instance.EquipmentPress(false, YJMeter, out rest, out strRD);
                    btn_UpDJ.Enabled = true;
                }), "");
            }
            else
            {
                btn_UpDJ.Enabled = true;
            }
        }
        /// <summary>
        /// 读电机状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ReadElectStates_Click(object sender, EventArgs e)
        {
            string[] str_Status = new string[0];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.GetBWStatus(out str_Status);
            string strData = "";
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                if (str_Status[i] == null || str_Status[i] == "")
                {

                    ////上限位
                    setStatus(i, StatusType.上限位, Color.Black);
                    ////挂表
                    setStatus(i, StatusType.表位, Color.Black);
                    ////下限位
                    setStatus(i, StatusType.下限位, Color.Black);

                    continue;
                }
                strData = str_Status[i].Substring(0, 1);

                if (strData != "1")
                {
                    ////下限没到位
                    setStatus(i, StatusType.下限位, Color.Red);
                }
                else if (strData == "1")
                {
                    ////下限到位
                    setStatus(i, StatusType.下限位, Color.Green);
                }
                strData = str_Status[i].Substring(1, 1);
                if (strData != "1")
                {
                    ////上限没到位
                    setStatus(i, StatusType.上限位, Color.Red);
                }
                else if (strData == "1")
                {
                    ////上限到位
                    setStatus(i, StatusType.上限位, Color.Green);
                }
                strData = str_Status[i].Substring(2, 1);
                if (strData != "1")
                {
                    ////没挂表
                    setStatus(i, StatusType.表位, Color.Red);
                }
                else if (strData == "1")
                {
                    ////挂表
                    setStatus(i, StatusType.表位, Color.Green);
                }

            }
        }
        /// <summary>
        /// 向后翻转倾斜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OnDJ_Click(object sender, EventArgs e)
        {
            
            bool[] rest = new bool[0];
            string[] strRD = new string[0];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.EquipmentReversal(true, out rest, out strRD);
        }
        /// <summary>
        /// 向前翻转直立
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_BackDJ_Click(object sender, EventArgs e)
        {
            if (DialogResult.No == MessageBoxEx.Show(this, "向前翻转直立，请确定电表压接可靠！避免电表掉落损坏！", "提示", MessageBoxButtons.YesNo))
            {
                return;
            }
            bool[] rest = new bool[0];
            string[] strRD = new string[0];
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.EquipmentReversal(false, out rest, out strRD);
        }
        /// <summary>
        /// 读翻转状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_ReadReversalStatus_Click(object sender, EventArgs e)
        {
            int[] meterNo;
            int[] states;
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.GetEquipmentReversalStatus(out meterNo, out states);
            
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                setStatus(i, StatusType.上限位, Color.Black);
                setStatus(i, StatusType.表位, Color.Black);
                setStatus(i, StatusType.下限位, Color.Black);

                if (states[i] == 1)
                {
                    ////上限位
                    setStatus(i, StatusType.上限位, Color.Green);
                }
                else if (states[i] == 0)
                {
                    ////挂表
                    setStatus(i, StatusType.表位, Color.Red);
                }
                else if (states[i] == -1)
                {
                    ////下限位
                    setStatus(i, StatusType.下限位, Color.Green);
                }
            }
        }

        #endregion

        #region 继电器,直接式，互感式，CT供电，载波
        /// <summary>
        /// 直接式供电
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btu_JDQ3_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetPowerSupplyType(3,false);
        }

        void btu_JDQ2_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetPowerSupplyType(2);
        }

        /// 供电类型，耐压供电=1、载波供电=2、普通供电=3       
        void btu_JDQ_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetPowerSupplyType(1);
        }
        //耐压 电压电流对地
        void btn_uifordJDQ_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetPowerSupplyType(7);
        }
        /// <summary>
        /// 互感式供电
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btn_JDQ3HG_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetPowerSupplyType(3,true);
        }
        #endregion

        #region 读表地址
        /// <summary>
        /// 读表地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btu_address_Click(object sender, EventArgs e)
        {
            bool[] rest = new bool[0];
            CLDC_VerifyAdapter.Adapter.Instance.UpdateMeterProtocol(); 
            string[] strAddr=CLDC_VerifyAdapter.MeterProtocolAdapter.Instance.ReadAddress();
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                if (strAddr[i] == null || strAddr[i] == "")
                {
                    ////挂表
                    setStatus(i, StatusType.表位, Color.Black);
                }
                setStatus(i, StatusType.表位, strAddr[i]);
                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[i].Mb_chrAddr = strAddr[i];
            }
        }
        #endregion

        #region 回路
        /// <summary>
        /// 二回路
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btu_HuiLu2_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetPowerSupplyType(5, false);
            //if (false)
            //{
            bool[] rest = new bool[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                rest[i] = true;
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SelectCheckRoad(rest, CLDC_Comm.Enum.Cus_BothIRoadType.第二个电流回路, CLDC_Comm.Enum.Cus_BothVRoadType.直接接入式);
            //}
        }
        /// <summary>
        /// 一回路
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btu_HuiLu1_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetPowerSupplyType(3, false);
            //if (false)
            //{
            bool[] rest = new bool[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                rest[i] = true;
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SelectCheckRoad(rest, CLDC_Comm.Enum.Cus_BothIRoadType.第一个电流回路, CLDC_Comm.Enum.Cus_BothVRoadType.直接接入式);
            //}
        }
        #endregion

        private void ShowStates()
        {
        }

        #region CT
        /// <summary>
        /// CT切换100A档位 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btu_CT100A_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ControlCtSwitch(100);
        }
        /// <summary>
        /// CT切换2A档位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btu_CT2A_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ControlCtSwitch(2);
        }
        #endregion

        #region 远程上电
        /// <summary>
        /// 台体断电
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btu_StopEquipmentPower_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.RemoteControlOnOrOff(false);
        }
        /// <summary>
        /// 台体供电
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btu_giveEquipmentPower_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.RemoteControlOnOrOff(true);
        }
        #endregion

        #region 灯
        private void btn_YellowS_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(19, 2);
            Thread.Sleep(300);
        }
        private void btn_GreenS_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(20, 2);
            Thread.Sleep(300);
        }
        private void btn_RedS_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(18, 2);
            Thread.Sleep(300);
        }
        //循显三色红绿黄
        private void btn_X1_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(18, 1);
            Thread.Sleep(1000);
            //吉林之后的多功能板改了，不用关闭其它
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(18, 0);
            //Thread.Sleep(300);
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(19, 1);
            Thread.Sleep(1000);
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(19, 0);
            //Thread.Sleep(300);
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(20, 1);
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(18, 2);
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(19, 2);
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(20, 1);
            Thread.Sleep(1000);
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(20, 0);
            //Thread.Sleep(300);
        }

        //黄
        private void btn_X2_Click(object sender, EventArgs e)
        {
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(18, 0);
            //Thread.Sleep(300);
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(19, 1);
            Thread.Sleep(300);
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(20, 0);
            //Thread.Sleep(300);
        }

        //绿
        private void btn_X3_Click(object sender, EventArgs e)
        {
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(18, 0);
            //Thread.Sleep(300);
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(19, 0);
            //Thread.Sleep(300);
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(20, 1);
            Thread.Sleep(300);

        }
        //红
        private void btn_X4_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(18, 1);
            Thread.Sleep(300);
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(19, 0);
            //Thread.Sleep(300);
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(20, 0);
            //Thread.Sleep(300);
        }
        //灭
        private void btn_X5_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(18, 0);
            Thread.Sleep(300);
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(19, 0);
            //Thread.Sleep(300);
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentThreeColor(20, 0);
            //Thread.Sleep(300);
        }
        #endregion

        ////继电器跳闸
        //private void btn_JDQContral_Click(object sender, EventArgs e)
        //{
        //    int MeterNo = 1;
        //    MeterNo = Int32.Parse(textBox_MeterNo.Text.ToString());
        //    CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentJdq(MeterNo, 0);
        //}
        ////继电器合闸
        //private void btn_JDQheZha_Click(object sender, EventArgs e)
        //{
        //    int MeterNo = 1;
        //    MeterNo = Int32.Parse(textBox_MeterNo.Text.ToString());
        //    CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEquipmentJdq(MeterNo, 1);
        //}

        #region 191,标准表
        // 切时钟脉冲
        private void btn_Time_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetTimeMaiCon(true);
        }
        // 切标准脉冲
        private void btn_StdMaiCon_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetTimeMaiCon(false);
        }
        
        //读取标准表常数
        private void btn_setStdConsunit_Click(object sender, EventArgs e)
        {
            long lStdConst = 0;
            if (CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadStdMeterConst(out lStdConst))
            {
                textBox_ReadConsunit.Text = lStdConst.ToString();
            }
            else
            {
                MessageBox.Show("读取标准表常数失败！");
            }
        }

        private void btn_ReadStdMeter_Click(object sender, EventArgs e)
        {
            CLDC_DataCore.Struct.StPower tagPower = new CLDC_DataCore.Struct.StPower();
            tagPower = CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadPowerInfo();
            //
            textBox_Ua.Text = tagPower.Ua.ToString();
            textBox_Ub.Text = tagPower.Ub.ToString();
            textBox_Uc.Text = tagPower.Uc.ToString();//
            textBox_Ia.Text = tagPower.Ia.ToString();//
            textBox_Ib.Text = tagPower.Ib.ToString();//
            textBox_Ic.Text = tagPower.Ic.ToString();//
            textBox_Phia.Text = tagPower.Phi_Ia.ToString();//
            textBox_Phib.Text = tagPower.Phi_Ib.ToString();// 
            textBox_Phic.Text = tagPower.Phi_Ic.ToString();//
        }
        //设置标准表档位
        private void btn_SetStdMeter_Click(object sender, EventArgs e)
        {
            if (CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetStdMeterR(float.Parse(txtBoxUnDw.Text.ToString()), float.Parse(textBox_InDw.Text.ToString())))
            {
                MessageBox.Show("设定档位成功");
            }
        }
        //设置标准表常数
        private void btn_SetStdMeterConst_Click(object sender, EventArgs e)
        {
            if (CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetStdMeterConst(int.Parse(textBox_ConstUnit.Text.ToString())))
            {
                MessageBox.Show("设定标准表常数成功");
            }

        }

        private void btn_SetEgOut_Click(object sender, EventArgs e)
        {
            if (CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEgOut(1))
            {
                MessageBox.Show("设定有功电能成功");
            }
        }

        private void btn_SetEgOutStop_Click(object sender, EventArgs e)
        {
            if (CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEgOut(0))
            {
                MessageBox.Show("设定有功电能成功");
            }
        }

        private void btn_SetEgOutWg_Click(object sender, EventArgs e)
        {
            if (CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetEgOut(2))
            {
                MessageBox.Show("设定有功电能成功");
            }
        }
        #endregion

        private void chk_DianPing_CheckedChanged(object sender, EventArgs e)
        {
            txt_MCGS.Enabled = false;
            txt_MCHz.Enabled = false;
            txt_ZKB.Enabled = false;
        }

        private void chk_MC_CheckedChanged(object sender, EventArgs e)
        {
            txt_MCGS.Enabled = true;
            txt_MCHz.Enabled = true;
            txt_ZKB.Enabled = true;
        }
        #region 加密机
        
        /// <summary>
        /// 加密机操作
        /// </summary>
        
        private CLDC_Encryption.CLEncryption.EncryptionFactory encryptionFactory = new CLDC_Encryption.CLEncryption.EncryptionFactory();
        private CLDC_Encryption.CLEncryption.Interface.IAmMeterEncryption EncryptionTool;

        private void btnOpenUsbkey_Click(object sender, EventArgs e)
        {
             
            string msg = "";
            if (em.OpenUsbkey(ref msg))
            {
                MessageBoxEx.Show(this,"打开USB成功");
            }
            else
            {
                MessageBoxEx.Show(this, "打开USB失败:" + msg);
            }
            
        }

        private void btnLgServer_Click(object sender, EventArgs e)
        {
            string msg = "";
            string m_str_Ip = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getValue(CLDC_DataCore.Const.Variable.CTC_ENCRYPTION_MACHINEIP);
            string m_str_Port = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getValue(CLDC_DataCore.Const.Variable.CTC_ENCRYPTION_MACHINEPORT);
            string str_Password = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getValue(CLDC_DataCore.Const.Variable.CTC_ENCRYPTION_MACHINEUSBKEY);
            if (string.IsNullOrEmpty(m_str_Port))
            {
                m_str_Port = "8001";
            }
            if (em.LgServer(m_str_Ip, ushort.Parse(m_str_Port), 8, str_Password, ref msg))
            {
                MessageBoxEx.Show(this, "连接加密机成功");

            }
            else
            {
                MessageBoxEx.Show(this, "连接加密机失败:" + msg);

            }
        }

        private void btnOpenDevice_Click(object sender, EventArgs e)
        {

            string msg = "";
            if (EncryptionTool.Link())
            {
                MessageBoxEx.Show(this, "连接加密机成功");
            }
            else
            {
                MessageBoxEx.Show(this, "连接加密机失败:" + msg);
            }

        }
        private void btnFCLgServer_Click(object sender, EventArgs e)
        {

            if (EncryptionTool.UnLink())
            {
                MessageBoxEx.Show(this, "断开加密机成功");

            }
            else
            {
                MessageBoxEx.Show(this, "断开加密机失败");

            }
        }

        private void btnIdentityAuthentication_Click(object sender, EventArgs e)
        {
            string msg = "";
            string rand = "";
            string endata="";
            string div = "000012345678".PadLeft(16,'0');
            try
            {
                if (em.IdentityAuthentication(0, div, ref rand, ref endata, "", ref msg))
                {
                    MessageBoxEx.Show(this, "随机数为" + rand + ",密文为" + endata);

                }
                else
                {
                    MessageBoxEx.Show(this, "获取密文失败," + msg);

                }
            }
            catch (Exception ex)
            {
                CLDC_DataCore.Const.GlobalUnit.g_MsgControl.OutMessage(ex.Message, false);                
            }
        }

        private void btnGetInd2013_Click(object sender, EventArgs e)
        {
            string msg = "";
            string rand = "";
            string endata = "";
            string div = "000012345678".PadLeft(16, '0');
            if (em.Meter_Formal_IdentityAuthentication(0, div, ref rand, ref endata, ref msg))
            {
                MessageBoxEx.Show(this, "随机数为" + rand + ",密文为" + endata);

            }
            else
            {
                MessageBoxEx.Show(this, "获取密文失败," + msg);

            }
        }

        private void btnRand_Click(object sender, EventArgs e)
        {
            string msg = "";
            string rand="";
            if (em.Create_Rand(ref rand, ref msg))
            {
                MessageBoxEx.Show(this, "随机数为" + rand);
            }
            else
            {
                MessageBoxEx.Show(this, "获取随机数失败," + msg);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (em.LgoutServer(ref msg))
            {
                MessageBoxEx.Show(this, "断开成功");
            }
            else
            {
                MessageBoxEx.Show(this, "断开失败，," + msg);
            }
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (em.ClseUsbkey(ref msg))
            {
                MessageBoxEx.Show(this, "断开成功");
            }
            else
            {
                MessageBoxEx.Show(this, "断开失败，," + msg);
            }
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            string msg = "";
            string rand = "12345678";
            string endata = "";
            string esamNo = "000012345678".PadLeft(16, '0');
            string pudata = "1234567891234567891234";
            string div = "000012345678".PadLeft(16, '0');
            if (em.UserControl(0, rand, div, esamNo, pudata, ref endata, ref msg))
            {
                MessageBoxEx.Show(this, "明文为" + pudata + ",长度为" + pudata.Length + ",密文长度为" + endata.Length + ",密文为" + endata);

            }
            else
            {
                MessageBoxEx.Show(this, "获取密文失败," + msg);

            }
        }

        void btn_DevIdc_Click(object sender, EventArgs e)
        {
            string msg = "";
            string rand = "";
            string endata = "";
            string div = "000012345678".PadLeft(16, '0');
            try
            {
                if (EncryptionTool.IdentityAuthentication(0, div + "00", ref rand, ref endata))
                {
                    MessageBoxEx.Show(this, "随机数为" + rand + ",密文为" + endata);

                }
                else
                {
                    msg = EncryptionTool.LostMessage;
                    MessageBoxEx.Show(this, "获取密文失败," + msg);

                }
            }
            catch (Exception ex)
            {
                CLDC_DataCore.Const.GlobalUnit.g_MsgControl.OutMessage(ex.Message, false);
            }
        }


        void btn_TestRongtong_Click(object sender, EventArgs e)
        {
            byte[] randdata = new byte[32];
            int i = TestEnegyRt.IdentityAuthentication("111111111111111111", randdata);
            if (i == 0)
            {
                MessageBox.Show("成功!!");
            }
            else
            {
                MessageBox.Show("失败!!" + i.ToString());
            }
        }
        
        #endregion

        private void BtnMeterReplace_Click(object sender, EventArgs e)
        {

            CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.Control("挂表位",false  );
        }

        private void BtnMeterOK_Click(object sender, EventArgs e)
        {

            CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.Control("就位",false  );
        }

        private void BtnNaiYaTest_Click(object sender, EventArgs e)
        {

                CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.Control("倾斜",false  );
        }

        private void button1_Click(object sender, EventArgs e)
        {
                    }



        


    }

    public class TestEnegyRt
    {
        [DllImport(@"Encryption\DLL_DEVELOP_2009_RT\TestZhuzhan.dll")]
        public static extern int IdentityAuthentication([MarshalAs(UnmanagedType.LPStr)] string Div, [MarshalAs(UnmanagedType.LPArray)] byte[] RandAdnEnData);

        //[DllImport(@"TestZhuzhan.dll", EntryPoint = "IdentityAuthentication", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        //unsafe public static extern int IdentityAuthentication(byte* PutDiv, byte* OutRandAndEndata);
    }
}

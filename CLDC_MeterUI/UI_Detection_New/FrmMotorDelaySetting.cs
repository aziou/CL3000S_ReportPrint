using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
namespace CLDC_MeterUI.UI_Detection_New
{
    public partial class FrmMotorDelaySetting : Office2007Form
    {
        #region
        private static FrmMotorDelaySetting instance;
        private static object syncRoot = new Object();
        public static FrmMotorDelaySetting Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new FrmMotorDelaySetting();
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

        public FrmMotorDelaySetting()
        {
            InitializeComponent();
            eLoad();
        }

        private void eLoad()
        {
            motorDelayTimeControl1.DownDelayClick += new EventHandler(motorDelayTimeControl1_DownDelayClick);
            motorDelayTimeControl1.UpDelayClick += new EventHandler(motorDelayTimeControl1_UpDelayClick);
            motorDelayTimeControl1.ReadUpDownClick += new EventHandler(motorDelayTimeControl1_ReadUpDownClick);
            motorDelayTimeControl1.ReadTurnClick += new EventHandler(motorDelayTimeControl1_ReadTurnClick);
            motorDelayTimeControl1.StandupDelayClick += new EventHandler(motorDelayTimeControl1_StandupDelayClick);
            motorDelayTimeControl1.LeanDelayClick += new EventHandler(motorDelayTimeControl1_LeanDelayClick);

        }
        /// <summary>
        /// 翻转电机倾斜设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void motorDelayTimeControl1_LeanDelayClick(object sender, EventArgs e)
        {
            int[] cltime = motorDelayTimeControl1.GetLeanMeterDelay();
            bool[] _checked = motorDelayTimeControl1.GetTurnCheck();
            int option = motorDelayTimeControl1.GetOperation();
            string errmsg = "";
            
            if (CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetReversalTime(_checked, 1, option, cltime, ref errmsg))
            {
                MessageBoxEx.Show(this, "设置成功。", "提示", MessageBoxButtons.OK);
            }
            else
            {
                MessageBoxEx.Show(this, "设置失败：" + errmsg, "提示", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// 翻转电机竖直设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void motorDelayTimeControl1_StandupDelayClick(object sender, EventArgs e)
        {
            int[] cltime = motorDelayTimeControl1.GetStandupMeterDelay();
            bool[] _checked = motorDelayTimeControl1.GetTurnCheck();
            int option = motorDelayTimeControl1.GetOperation();
            string errmsg = "";
            
            if (CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetReversalTime(_checked, 0, option, cltime, ref errmsg))
            {
                MessageBoxEx.Show(this, "设置成功。", "提示", MessageBoxButtons.OK);
            }
            else
            {
                MessageBoxEx.Show(this, "设置失败：" + errmsg, "提示", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// 读翻转电机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void motorDelayTimeControl1_ReadTurnClick(object sender, EventArgs e)
        {
            int[] _uptime=null;
            int[] _downtime=null;
            string errmsg="";
            bool[] _checked = motorDelayTimeControl1.GetTurnCheck();
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadReversalTime(_checked, out _uptime, out _downtime, ref errmsg);
            //显示
            motorDelayTimeControl1.ShowTurn(_uptime, _downtime);
        }
        /// <summary>
        /// 读压接电机延时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void motorDelayTimeControl1_ReadUpDownClick(object sender, EventArgs e)
        {
            int[] _uptime = null;
            int[] _downtime = null;
            string errmsg = "";
            int bwCount = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws;
            bool[] _checked = new bool[bwCount];
            for (int i = 0; i < bwCount; i++)
            {
                _checked[i] = true;
            }
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ReadElectromotorTime(_checked, out _uptime, out _downtime, ref errmsg);
            //显示
            motorDelayTimeControl1.ShowUpDown(_uptime, _downtime);
        }
        /// <summary>
        /// 设置上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void motorDelayTimeControl1_UpDelayClick(object sender, EventArgs e)
        {
            int[] cltime = motorDelayTimeControl1.GetUpMeterDelay();
            bool[] _checked = motorDelayTimeControl1.GetUpDownCheck();
            int option = motorDelayTimeControl1.GetOperation();
            string errmsg = "";
            
            if (CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetElectromotorTime(_checked, 0, option, cltime, ref errmsg))
            {
                MessageBoxEx.Show(this, "设置成功。", "提示", MessageBoxButtons.OK);
            }
            else
            {
                MessageBoxEx.Show(this, "设置失败：" + errmsg, "提示", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// 设置下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void motorDelayTimeControl1_DownDelayClick(object sender, EventArgs e)
        {
            int[] cltime = motorDelayTimeControl1.GetDownMeterDelay();
            bool[] _checked = motorDelayTimeControl1.GetUpDownCheck();
            int option=motorDelayTimeControl1.GetOperation();
            string errmsg = "";
            
            if (CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetElectromotorTime(_checked, 1, option, cltime, ref errmsg))
            {
                MessageBoxEx.Show(this, "设置成功。", "提示", MessageBoxButtons.OK);
            }
            else
            {
                MessageBoxEx.Show(this, "设置失败："+errmsg, "提示", MessageBoxButtons.OK);
            }
        }
        

    }
}

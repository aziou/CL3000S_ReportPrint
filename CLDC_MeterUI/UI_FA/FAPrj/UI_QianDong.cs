using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_QianDong : UI_TableBase
    {
        #region----潜动日计时参数----

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = chkDayCheckTimes.Checked;
            Txt_Wcx.Enabled = status;
            TxtWcCount.Enabled = status;
            TxtTestCount.Enabled = status;
        }

        #endregion

        #region -------------------构造-----------------
        public UI_QianDong()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="Ttype">台体类型</param>
        public UI_QianDong(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            this.InitUI();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Ttype">台体类型</param>
        /// <param name="faname">方案名称</param>
        public UI_QianDong(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(faname);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Ttype">台体类型</param>
        /// <param name="FaItem">方案项目内容</param>
        public UI_QianDong(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_QianDong FaItem)
            : base(Ttype, FaItem.Name)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(FaItem);
        }

        #endregion


        #region -------------------私有方法、函数--------------
        /// <summary>
        /// UI初始化
        /// </summary>
        private void InitUI()
        {
            this.UI_QDPz.Glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功;
            this.UI_QDPf.Glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.反向有功;
            this.UI_QDQz.Glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.正向无功;
            this.UI_QDQf.Glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.反向无功;

            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_QIAND_FOLDERNAME);
        }


        #region ----------------公共方法、函数-----------------------

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="FAName">方案名称</param>
        public void LoadFA(string FAName)
        {

            CLDC_DataCore.Model.Plan.Plan_QianDong _QianDong = new CLDC_DataCore.Model.Plan.Plan_QianDong((int)base.TaiType, FAName);         //打开一个方案

            this.LoadFA(_QianDong);
        }

        #endregion

        /// <summary>
        /// 方案加载
        /// </summary>
        /// <param name="FaItem">方案项目</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_QianDong FaItem)
        {
            base.FaName = FaItem.Name;

            try
            {
                Cmb_Fa.Text = FaItem.Name;
            }
            catch
            {
                Cmb_Fa.SelectedIndex = 0;
            }

            this.UI_QDPz.LoadFA(FaItem);
            this.UI_QDPf.LoadFA(FaItem);
            this.UI_QDQz.LoadFA(FaItem);
            this.UI_QDQf.LoadFA(FaItem);
            #region ----日计时参数刷新到UI----
            string[] CheckTimesSetting = FaItem.DayCheckTimesSetting.Split('|');
            try
            {
                if (CheckTimesSetting.Length == 4)
                {
                    chkDayCheckTimes.Checked = true;
                    if (CheckTimesSetting[0] == "0")
                    {
                        chkDayCheckTimes.Checked = false;
                    }
                    Txt_Wcx.Text = CheckTimesSetting[1];
                    TxtWcCount.Text = CheckTimesSetting[2];
                    TxtTestCount.Text = CheckTimesSetting[3];
                }
                else
                {
                    chkDayCheckTimes.Checked = false;
                    Txt_Wcx.Text = "1";
                    TxtWcCount.Text = "10";
                    TxtTestCount.Text = "60";
                }
            }
            catch
            {
                    chkDayCheckTimes.Checked = false;
                    Txt_Wcx.Text = "1";
                    TxtWcCount.Text = "10";
                    TxtTestCount.Text="60";
            }

            #endregion
        }


        /// <summary>
        /// 方案名称（只写），设置后将自动加载方案：前提是该方案存在
        /// </summary>
        public string FAName
        {
            set
            {
                this.LoadFA(value);
            }
        }
        /// <summary>
        /// 拷贝方案
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_QianDong Copy()
        {
            CLDC_DataCore.Model.Plan.Plan_QianDong _Obj = new CLDC_DataCore.Model.Plan.Plan_QianDong((int)TaiType, "");

            this.UI_QDPz.Copy(ref _Obj);
            this.UI_QDPf.Copy(ref _Obj);
            this.UI_QDQz.Copy(ref _Obj);
            this.UI_QDQf.Copy(ref _Obj);
            #region----日计时参数保存
            string DayCheckTimesSetting = "0";
            if (chkDayCheckTimes.Checked == true)
            {
                DayCheckTimesSetting = "1";
            }
            DayCheckTimesSetting += ("|" + Txt_Wcx.Text);
            DayCheckTimesSetting += ("|" + TxtWcCount.Text);
            DayCheckTimesSetting += ("|" + TxtTestCount.Text);
            _Obj.DayCheckTimesSetting = DayCheckTimesSetting;
            #endregion
            _Obj.SetPram((int)base.TaiType, base.FaName);
            return _Obj;
        }


        #endregion


        #region ---------------事件------------------------
        /// <summary>
        /// UI显示数据清理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            this.UI_QDPz.ClearData();
            this.UI_QDPf.ClearData();
            this.UI_QDQz.ClearData();
            this.UI_QDQf.ClearData();
        }

        /// <summary>
        /// 方案选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Fa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Cmb_Fa.SelectedIndex == 0) return;

            this.FAName = Cmb_Fa.Text;
        }

        #endregion


    }
}

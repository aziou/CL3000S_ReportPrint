using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.ShowDataView
{
    public partial class Frm_CheckParamView : Office2007Form
    {

        public delegate void Event_SendData();

        public event Event_SendData SendData;

        CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup;

        public Frm_CheckParamView()
        {
            InitializeComponent();
        }


        public Frm_CheckParamView(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            _DnbGroup = MeterGroup;
            InitializeComponent();
            this.SetTabs();
        }


        private void SetTabs()
        {
            if (this.Tab_Check.TabPages.Count > 0) this.Tab_Check.TabPages.Clear();

            UserControl _Control=null;

            for (int i = 0; i < _DnbGroup.CheckPlan.Count; i++)
            {
                if (_DnbGroup.CheckPlan[i] is StPlan_QiDong)
                {
                    if (this.Tab_Check.TabPages["起动试验"] != null) continue;

                    this.Tab_Check.TabPages.Add("起动试验", "起动试验");
                    _Control = new FaParam.QiDongPrjView(ref _DnbGroup);
                    this.Tab_Check.TabPages["起动试验"].Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }

                if (_DnbGroup.CheckPlan[i] is StPlan_QianDong)
                {
                    if (this.Tab_Check.TabPages["潜动试验"] != null) continue;

                    this.Tab_Check.TabPages.Add("潜动试验", "潜动试验");
                    _Control = new FaParam.QianDongPrjView(ref _DnbGroup);
                    this.Tab_Check.TabPages["潜动试验"].Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }

                if (_DnbGroup.CheckPlan[i] is StPlan_WcPoint)
                {
                    if (this.Tab_Check.TabPages["基本误差"] != null) continue;

                    this.Tab_Check.TabPages.Add("基本误差", "基本误差");
                    _Control=new FaParam.WcPrjView(ref _DnbGroup);
                    this.Tab_Check.TabPages["基本误差"].Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                if (_DnbGroup.CheckPlan[i] is CLDC_DataCore.Struct.StPlan_Dgn || _DnbGroup.CheckPlan[i] is StPlan_ZouZi)
                {
                    if (this.Tab_Check.TabPages["多功能参数"] != null) continue;

                    this.Tab_Check.TabPages.Add("多功能参数", "多功能参数");
                    _Control = new FaParam.DgnPramView(ref _DnbGroup);
                    this.Tab_Check.TabPages["多功能参数"].Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }

                if (_DnbGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定 && _Control!=null)
                {
                    _Control.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lab_Close_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        /// <summary>
        /// 检查方案参数内容是否被修改，如果修改则下发，最后关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Ok_Click(object sender, EventArgs e)
        {
            bool blnChanged = false;
            for (int i = 0; i < Tab_Check.TabCount; i++)
            { 
                UserControl _Control=(UserControl)this.Tab_Check.TabPages[i].Controls[0];

                if (_Control is FaParam.WcPrjView)
                {
                    if(((FaParam.WcPrjView)_Control).ChangeFAPram())
                    {
                        blnChanged=true;
                    }
                }

                if (_Control is FaParam.QiDongPrjView)
                {
                    if (((FaParam.QiDongPrjView)_Control).ChangeFAPram())
                    {
                        blnChanged = true;
                    }
                }

                if (_Control is FaParam.QianDongPrjView)
                {
                    if (((FaParam.QianDongPrjView)_Control).ChangeFAPram())
                    {
                        blnChanged = true;
                    }
                }
                if (_Control is FaParam.DgnPramView)
                {
                    if (((FaParam.DgnPramView)_Control).ChangeFAPram())
                    {
                        blnChanged = true;
                    }
                }
            }

            if (blnChanged)
            {
                if (SendData != null)
                {
                    SendData();
                }
            }
            this.Lab_Close_Click(sender, e);
        }
    }
}
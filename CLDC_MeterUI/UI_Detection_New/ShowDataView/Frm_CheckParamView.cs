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
                    if (this.Tab_Check.TabPages["������"] != null) continue;

                    this.Tab_Check.TabPages.Add("������", "������");
                    _Control = new FaParam.QiDongPrjView(ref _DnbGroup);
                    this.Tab_Check.TabPages["������"].Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }

                if (_DnbGroup.CheckPlan[i] is StPlan_QianDong)
                {
                    if (this.Tab_Check.TabPages["Ǳ������"] != null) continue;

                    this.Tab_Check.TabPages.Add("Ǳ������", "Ǳ������");
                    _Control = new FaParam.QianDongPrjView(ref _DnbGroup);
                    this.Tab_Check.TabPages["Ǳ������"].Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }

                if (_DnbGroup.CheckPlan[i] is StPlan_WcPoint)
                {
                    if (this.Tab_Check.TabPages["�������"] != null) continue;

                    this.Tab_Check.TabPages.Add("�������", "�������");
                    _Control=new FaParam.WcPrjView(ref _DnbGroup);
                    this.Tab_Check.TabPages["�������"].Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }
                if (_DnbGroup.CheckPlan[i] is CLDC_DataCore.Struct.StPlan_Dgn || _DnbGroup.CheckPlan[i] is StPlan_ZouZi)
                {
                    if (this.Tab_Check.TabPages["�๦�ܲ���"] != null) continue;

                    this.Tab_Check.TabPages.Add("�๦�ܲ���", "�๦�ܲ���");
                    _Control = new FaParam.DgnPramView(ref _DnbGroup);
                    this.Tab_Check.TabPages["�๦�ܲ���"].Controls.Add(_Control);
                    _Control.Dock = DockStyle.Fill;
                    _Control.Margin = new System.Windows.Forms.Padding(0);
                }

                if (_DnbGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨 && _Control!=null)
                {
                    _Control.Enabled = false;
                }
            }
        }
        /// <summary>
        /// �رմ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lab_Close_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        /// <summary>
        /// ��鷽�����������Ƿ��޸ģ�����޸����·������رմ���
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.ShowDataView
{
    public partial class Frm_DnbDataView : Office2007Form
    {

        public delegate void Event_SendData();

        public event Event_SendData SendData;

        CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup;

        public Frm_DnbDataView()
        {
            InitializeComponent();
        }


        public Frm_DnbDataView(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            _DnbGroup = MeterGroup;
            InitializeComponent();
            this.SetTabs();
        }


        private void SetTabs()
        {
            CLDC_MeterUI.DisplayInfo.DisplayMeterDetailInfo ChildDetail = null;

            ChildDetail = new CLDC_MeterUI.DisplayInfo.DisplayMeterDetailInfo(_DnbGroup, false);

            this.Tab_Check.TabPages.Add("详细数据", "详细数据");
            this.Tab_Check.TabPages["详细数据"].Controls.Add(ChildDetail);
            ChildDetail.Dock = DockStyle.Fill;
            ChildDetail.Margin = new System.Windows.Forms.Padding(0);

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
        
    }
}
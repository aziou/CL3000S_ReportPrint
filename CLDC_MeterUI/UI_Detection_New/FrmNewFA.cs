using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New
{
    partial class FrmNewFA : Office2007Form
    {
        private string m_NewFaName = "";

        public FrmNewFA(int tType)
        {
            InitializeComponent();

            List<string> FaNameList = CLDC_DataCore.Model.Plan.Model_Plan.getFileNames(CLDC_DataCore.Const.Variable.CONST_FA_GROUP_FOLDERNAME, tType);
            Cmb_FaList.Items.Clear();

            Cmb_FaList.Items.Add("请通过下拉列表选择一个方案...");
            for (int i = 0; i < FaNameList.Count; i++)
            {
                Cmb_FaList.Items.Add(FaNameList[i]);
            }
            Cmb_FaList.SelectedIndex = 0;
        }

        public void SetFaName(string FaName)
        {
            try
            {
                Cmb_FaList.SelectedItem = FaName;
            }
            catch { }
        }
        /// <summary>
        /// 获取方案名称
        /// </summary>
        public String FaName
        {
            get
            {
                return m_NewFaName;
            }
        }

        private void FrmNewFA_Load(object sender, EventArgs e)
        {
            

        }

        private void Cmd_Ok_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Cmb_FaList.SelectedIndex == 0)
            {
                MessageBoxEx.Show(this,"请选择一个正确的方案...", "方案选择", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            m_NewFaName = Cmb_FaList.SelectedItem.ToString();
            this.Close();
        }

        private void Cmd_Cancel_Click(object sender, EventArgs e)
        {
            m_NewFaName = "";
            this.Close();
        }
    }
}
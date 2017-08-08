using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI
{
    public partial class UI_ZouZiFeiLv : UserControl
    {

        public delegate void Evt_ClosePanel(List<StPlan_ZouZi.StPrjFellv> FeiLvItem);

        public event Evt_ClosePanel ClosePanel;

        /// <summary>
        /// ����
        /// </summary>
        public UI_ZouZiFeiLv()
        {
            InitializeComponent();

            for (int i = 0; i < 5; i++)
            {
                Cmb_Feilv.Items.Add(((CLDC_Comm.Enum.Cus_FeiLv)i).ToString());
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="enumMethod">���ַ���</param>
        public UI_ZouZiFeiLv(CLDC_Comm.Enum.Cus_ZouZiMethod enumMethod)
        {
            InitializeComponent();

            switch (enumMethod)
            { 
                case CLDC_Comm.Enum.Cus_ZouZiMethod.У�˳���:
                case CLDC_Comm.Enum.Cus_ZouZiMethod.��׼��:
                    this.Ltv_FeiLv.Columns[2].Text = "����(��)";
                    Lab_Dw.Text = "���ȣ�";
                    Lab_Pram.Text = "���ֵ�����";
                    break;
                case CLDC_Comm.Enum.Cus_ZouZiMethod.�������ַ�:
                    this.Ltv_FeiLv.Columns[2].Text = "ʱ��(��)";
                    Lab_Dw.Text = "���֣�";
                    Lab_Pram.Text = "����ʱ����";
                    break;
                default:
                    this.Ltv_FeiLv.Columns[2].Text = "ʱ��(��)";
                    Lab_Dw.Text = "���֣�";
                    Lab_Pram.Text = "����ʱ����";
                    break;
            }

            for (int i = 0; i < 5; i++)
            {
                Cmb_Feilv.Items.Add(((CLDC_Comm.Enum.Cus_FeiLv)i).ToString());
            }

        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="enumMethod">���ַ���</param>
        public UI_ZouZiFeiLv(CLDC_Comm.Enum.Cus_ZouZiMethod enumMethod, List<StPlan_ZouZi.StPrjFellv> FeiLvItem)
        {
            InitializeComponent();

            switch (enumMethod)
            {
                case CLDC_Comm.Enum.Cus_ZouZiMethod.У�˳���:
                case CLDC_Comm.Enum.Cus_ZouZiMethod.��׼��:
                    this.Ltv_FeiLv.Columns[2].Text = "����(��)";
                    Lab_Dw.Text = "���ȣ�";
                    Lab_Pram.Text = "���ֵ�����";
                    break;
                case CLDC_Comm.Enum.Cus_ZouZiMethod.�������ַ�:
                    this.Ltv_FeiLv.Columns[2].Text = "ʱ��(��)";
                    Lab_Dw.Text = "���֣�";
                    Lab_Pram.Text = "����ʱ����";
                    break;
                default:
                    this.Ltv_FeiLv.Columns[2].Text = "ʱ��(��)";
                    Lab_Dw.Text = "���֣�";
                    Lab_Pram.Text = "����ʱ����";
                    break;
            }

            for (int i = 0; i < 5; i++)
            {
                Cmb_Feilv.Items.Add(((CLDC_Comm.Enum.Cus_FeiLv)i).ToString());
            }

            this.InsertListItem(FeiLvItem);

        }


        private void InsertListItem(List<StPlan_ZouZi.StPrjFellv> FeiLvItem)
        {
            for (int i = 0; i < FeiLvItem.Count; i++)
            {
                ListViewItem Item = new ListViewItem(FeiLvItem[i].FeiLv.ToString());
                Item.Tag = (int)FeiLvItem[i].FeiLv;
                Item.SubItems.Add( FeiLvItem[i].StartTime);

                Item.SubItems.Add( FeiLvItem[i].ZouZiTime);

                Ltv_FeiLv.Items.Add(Item);
            }
        }




        /// <summary>
        /// �Ƿ���Ҫ�޸ı�ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_NotSet_CheckedChanged(object sender, EventArgs e)
        {
            this.DTP_StartTime.Enabled = Chk_NotSet.CheckState == CheckState.Checked ? true : false;
        }


        #region -----------------------���ʲ���-----------------------------
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Insert_Click(object sender, EventArgs e)
        {
            if (Cmb_Feilv.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this,"��ѡ����ȷ�ķ���...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_LongTime.Text))
            {
                MessageBoxEx.Show(this,"�����"+this.Lab_Pram.Text.Replace(":","")+"����Ϊ����...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ListViewItem _Item = Ltv_FeiLv.Items.Add(Cmb_Feilv.SelectedItem.ToString());
            _Item.Tag = Cmb_Feilv.SelectedIndex;
            _Item.SubItems.Add(Chk_NotSet.Checked ? DTP_StartTime.Value.ToString("HH':'mm") : "");
            _Item.SubItems.Add(Txt_LongTime.Text);
        }

        #endregion 

        /// <summary>
        /// �Ƴ�һ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Remove_Click(object sender, EventArgs e)
        {
            if (Ltv_FeiLv.SelectedItems.Count == 0)
                return;
            for (int i = 0; i < Ltv_FeiLv.SelectedItems.Count; i++)
                Ltv_FeiLv.Items.RemoveAt(Ltv_FeiLv.SelectedItems[i].Index);

        }

        /// <summary>
        /// �ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Close_Click(object sender, EventArgs e)
        {
            if (ClosePanel == null) return;

            List<StPlan_ZouZi.StPrjFellv> Items = new List<StPlan_ZouZi.StPrjFellv>();

            for (int i = 0; i < Ltv_FeiLv.Items.Count; i++)
            {
                StPlan_ZouZi.StPrjFellv Item = new StPlan_ZouZi.StPrjFellv();

                Item.FeiLv = (CLDC_Comm.Enum.Cus_FeiLv)((int)Ltv_FeiLv.Items[i].Tag);
                Item.StartTime = Ltv_FeiLv.Items[i].SubItems[1].Text;
                Item.ZouZiTime = Ltv_FeiLv.Items[i].SubItems[2].Text;
                Items.Add(Item);
            }

            this.ClosePanel(Items);
        }
    }
}

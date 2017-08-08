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
        #region----Ǳ���ռ�ʱ����----

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool status = chkDayCheckTimes.Checked;
            Txt_Wcx.Enabled = status;
            TxtWcCount.Enabled = status;
            TxtTestCount.Enabled = status;
        }

        #endregion

        #region -------------------����-----------------
        public UI_QianDong()
        {
            InitializeComponent();
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Ttype">̨������</param>
        public UI_QianDong(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            this.InitUI();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Ttype">̨������</param>
        /// <param name="faname">��������</param>
        public UI_QianDong(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(faname);
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Ttype">̨������</param>
        /// <param name="FaItem">������Ŀ����</param>
        public UI_QianDong(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_QianDong FaItem)
            : base(Ttype, FaItem.Name)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(FaItem);
        }

        #endregion


        #region -------------------˽�з���������--------------
        /// <summary>
        /// UI��ʼ��
        /// </summary>
        private void InitUI()
        {
            this.UI_QDPz.Glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�;
            this.UI_QDPf.Glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�;
            this.UI_QDQz.Glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�;
            this.UI_QDQf.Glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�;

            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_QIAND_FOLDERNAME);
        }


        #region ----------------��������������-----------------------

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="FAName">��������</param>
        public void LoadFA(string FAName)
        {

            CLDC_DataCore.Model.Plan.Plan_QianDong _QianDong = new CLDC_DataCore.Model.Plan.Plan_QianDong((int)base.TaiType, FAName);         //��һ������

            this.LoadFA(_QianDong);
        }

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="FaItem">������Ŀ</param>
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
            #region ----�ռ�ʱ����ˢ�µ�UI----
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
        /// �������ƣ�ֻд�������ú��Զ����ط�����ǰ���Ǹ÷�������
        /// </summary>
        public string FAName
        {
            set
            {
                this.LoadFA(value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_QianDong Copy()
        {
            CLDC_DataCore.Model.Plan.Plan_QianDong _Obj = new CLDC_DataCore.Model.Plan.Plan_QianDong((int)TaiType, "");

            this.UI_QDPz.Copy(ref _Obj);
            this.UI_QDPf.Copy(ref _Obj);
            this.UI_QDQz.Copy(ref _Obj);
            this.UI_QDQf.Copy(ref _Obj);
            #region----�ռ�ʱ��������
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


        #region ---------------�¼�------------------------
        /// <summary>
        /// UI��ʾ��������
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
        /// ����ѡ��
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_Error :UI_TableBase// UserControl
    {

        #region-------------���캯��--------------------

        private PrjUI.WcFaSetup WcSetup_H;
        private PrjUI.WcFaSetup WcSetup_A;
        private PrjUI.WcFaSetup WcSetup_B;
        private PrjUI.WcFaSetup WcSetup_C;

        public UI_Error()
        {
            InitializeComponent();
            this.InitUI();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ttype">̨������</param>
        public UI_Error(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            this.InitUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ttype">̨������</param>
        /// <param name="FaName">��������</param>
        public UI_Error(CLDC_Comm.Enum.Cus_TaiType Ttype, string FaName)
            : base(Ttype, FaName)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(FaName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ttype">̨������</param>
        /// <param name="FaItem">������Ŀ</param>
        public UI_Error(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_WcPoint FaItem)
            : base(Ttype, FaItem.Name)
        {
            InitializeComponent();
            this.InitUI();
            this.LoadFA(FaItem);
        }

        #endregion 


        #region ------------------------˽�к���������---------------------------------
        /// <summary>
        /// UI������Ϣ��ʼ��
        /// </summary>
        private void InitUI()
        {
            Cmb_Fa.SelectionChangeCommitted += new EventHandler(Cmb_Fa_SelectionChangeCommitted);

            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_WC_FOLDERNAME);            //��ʼ�����������б�

            #region --------------��ʼ�����ʷ���ͼ��----------------
            this.SetGlfxImageStyle(false, Tab_Pz);
            this.SetGlfxImageStyle(false, Tab_Pf);
            this.SetGlfxImageStyle(false, Tab_Qz);
            this.SetGlfxImageStyle(false, Tab_Qf);
            this.SetGlfxImageStyle(false, Tab_Q1);
            this.SetGlfxImageStyle(false, Tab_Q2);
            this.SetGlfxImageStyle(false, Tab_Q3);
            this.SetGlfxImageStyle(false, Tab_Q4);
            #endregion 

            #region -----------------��ʼ�����ʷ���ѡ�ť�¼�----------
            Tab_Pz.Click += new EventHandler(ToolGLFX_Click);
            Tab_Pf.Click += new EventHandler(ToolGLFX_Click);
            Tab_Qz.Click += new EventHandler(ToolGLFX_Click);
            Tab_Qf.Click += new EventHandler(ToolGLFX_Click);
            Tab_Q1.Click += new EventHandler(ToolGLFX_Click);
            Tab_Q2.Click += new EventHandler(ToolGLFX_Click);
            Tab_Q3.Click += new EventHandler(ToolGLFX_Click);
            Tab_Q4.Click += new EventHandler(ToolGLFX_Click);
            #endregion

            #region ----------------�����������춨��ѡ�����ݱ�----------------
            WcSetup_H = new PrjUI.WcFaSetup(CLDC_Comm.Enum.Cus_PowerYuanJian.H);
            WcSetup_H.PointCountChange += new PrjUI.WcFaSetup.EventPointCountChange(WcSetup_H_PointCountChange);
           
            if (base.TaiType == CLDC_Comm.Enum.Cus_TaiType.����̨)
            {
                WcSetup_A = new PrjUI.WcFaSetup(CLDC_Comm.Enum.Cus_PowerYuanJian.A);
                WcSetup_B = new PrjUI.WcFaSetup(CLDC_Comm.Enum.Cus_PowerYuanJian.B);
                WcSetup_C = new PrjUI.WcFaSetup(CLDC_Comm.Enum.Cus_PowerYuanJian.C);
                WcSetup_A.PointCountChange += new PrjUI.WcFaSetup.EventPointCountChange(WcSetup_A_PointCountChange);
                WcSetup_B.PointCountChange += new PrjUI.WcFaSetup.EventPointCountChange(WcSetup_B_PointCountChange);
                WcSetup_C.PointCountChange += new PrjUI.WcFaSetup.EventPointCountChange(WcSetup_C_PointCountChange);
            }
            this.Tab_H.Controls.Add(this.WcSetup_H);
            WcSetup_H.Margin = new System.Windows.Forms.Padding(0);
            WcSetup_H.Dock = DockStyle.Fill;

            if (base.TaiType == CLDC_Comm.Enum.Cus_TaiType.����̨)
            {
                this.Tab_A.Controls.Add(this.WcSetup_A);
                this.Tab_B.Controls.Add(this.WcSetup_B);
                this.Tab_C.Controls.Add(this.WcSetup_C);
                WcSetup_A.Margin = new System.Windows.Forms.Padding(0);
                WcSetup_A.Dock = DockStyle.Fill;
                WcSetup_B.Margin = new System.Windows.Forms.Padding(0);
                WcSetup_B.Dock = DockStyle.Fill;
                WcSetup_C.Margin = new System.Windows.Forms.Padding(0);
                WcSetup_C.Dock = DockStyle.Fill;
            }
            else
            {
                this.Tab_A.Parent = null;
                this.Tab_B.Parent = null;
                this.Tab_C.Parent = null;
            }
            #endregion 

            Cmb_xIb.Items.AddRange(WcSetup_H.getxIbName());         //��ʼ�������˵�

#if Release_NanTong
            ///������ͨ�ͻ�Ҫ��,Ȧ��ֻ����Ib
            ///�˴�������������ų���Release_NanTong
            Cmb_xIb.Enabled = false;
#endif

            this.LoadWcLimit();             //�����������Ϣ�б�
            this.LoadCheckOutTime();//���س�ʱ����
        }
        /// <summary>
        /// ���س�ʱ����
        /// </summary>
        private void LoadCheckOutTime()
        {
            Cmb_CheckOutTime.Items.Clear();
            Cmb_CheckOutTime.Items.Add("Ĭ�ϳ�ʱ����");
            Cmb_CheckOutTime.SelectedIndex = 0;
        }

        /// <summary>
        /// �����������Ϣ���ڳ�ʼ��UI������޸��ĵ�ʱ�����أ�
        /// </summary>
        private void LoadWcLimit()
        {
            CLDC_DataCore.DataBase.clsWcLimitDataControl _WcLimit = new CLDC_DataCore.DataBase.clsWcLimitDataControl();

            List<CLDC_DataCore.DataBase.IDAndValue> _WcLimitNames = _WcLimit.WcLimitName();

            Cmb_WcLimit.Items.Clear();

            Cmb_WcLimit.Items.Add("��������");
            for (int i = 0; i < _WcLimitNames.Count; i++)
            {
                Cmb_WcLimit.Items.Add(_WcLimitNames[i].Value);
            }

            Cmb_WcLimit.SelectedIndex = 0;

            _WcLimit.Close();
            _WcLimit = null;
        }


        /// <summary>
        /// �������ѡ�����������������
        /// </summary>
        /// <param name="Glfx"></param>
        private void SetWcSetupGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang Glfx)
        {
            if (WcSetup_H != null) WcSetup_H.GLFX = Glfx;
            if (WcSetup_A != null) WcSetup_A.GLFX = Glfx;
            if (WcSetup_B != null) WcSetup_B.GLFX = Glfx;
            if (WcSetup_C != null) WcSetup_C.GLFX = Glfx;
        }

        /// <summary>
        /// ���ù��ʷ���ť���ͼƬ����ʽ������ù��ʷ�������Ҫ��㣬��򹳣�û����Ϊ����
        /// </summary>
        private void SetGlfxImageStyle(CLDC_Comm.Enum.Cus_PowerFangXiang Glfx)
        {
            ToolStripButton Item = null;

            switch (Glfx)
            { 
                case CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�:
                    Item = Tab_Pz;
                    break;
                case CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�:
                    Item = Tab_Pf;
                    break;
                case CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�:
                    Item = Tab_Qz;
                    break;
                case CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�:
                    Item = Tab_Qf;
                    break;
                case CLDC_Comm.Enum.Cus_PowerFangXiang.��һ�����޹�:
                    Item = Tab_Q1;
                    break;
                case CLDC_Comm.Enum.Cus_PowerFangXiang.�ڶ������޹�:
                    Item = Tab_Q2;
                    break;
                case CLDC_Comm.Enum.Cus_PowerFangXiang.���������޹�:
                    Item = Tab_Q3;
                    break;
                case CLDC_Comm.Enum.Cus_PowerFangXiang.���������޹�:
                    Item = Tab_Q4;
                    break;
            }

            if (Item == null) return;

            if (this.WcSetup_A != null && this.WcSetup_B != null && this.WcSetup_C != null)
                if (this.WcSetup_H.PointCount + this.WcSetup_A.PointCount + this.WcSetup_B.PointCount + this.WcSetup_C.PointCount == 0)
                {
                    Item.Image = Pic_Not.Image;
                }
                else
                {
                    Item.Image = Pic_Have.Image;
                }
            else
            {
                if (this.WcSetup_H.PointCount == 0)
                {
                    Item.Image = Pic_Not.Image;
                }
                else
                {
                    Item.Image = Pic_Have.Image;
                }
            }
        }

        /// <summary>
        ///  ���ù��ʷ���ť���ͼƬ����ʽ��
        /// </summary>
        /// <param name="YaoJian">�Ƿ���Ҫ��ĵ�</param>
        /// <param name="Button">��ť����</param>
        private void SetGlfxImageStyle(bool YaoJian, ToolStripButton Button)
        {
            if (YaoJian)
            {
                Button.Image = Pic_Have.Image;
            }
            else
            {
                Button.Image = Pic_Not.Image;
            }
        }

        #endregion 


        #region -----------------------�¼�-----------------------

        /// <summary>
        /// ���ʷ����л��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolGLFX_Click(Object sender, EventArgs e)
        {
            Tab_Pz.Checked = false;
            Tab_Pf.Checked = false;
            Tab_Qz.Checked = false;
            Tab_Qf.Checked = false;
            Tab_Q1.Checked = false;
            Tab_Q2.Checked = false;
            Tab_Q3.Checked = false;
            Tab_Q4.Checked = false;
            ((ToolStripButton)sender).Checked = true;

            switch (((ToolStripButton)sender).Name.ToLower())
            {
                case "tab_pz":
                    SetWcSetupGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�);
                    break;
                case "tab_pf":
                    SetWcSetupGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�);
                    break;
                case "tab_qz":
                    SetWcSetupGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�);
                    break;
                case "tab_qf":
                    SetWcSetupGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�);
                    break;
                case "tab_q1":
                    SetWcSetupGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.��һ�����޹�);
                    break;
                case "tab_q2":
                    SetWcSetupGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.�ڶ������޹�);
                    break;
                case "tab_q3":
                    SetWcSetupGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.���������޹�);
                    break;
                case "tab_q4":
                    SetWcSetupGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.���������޹�);
                    break;
            }

        }

        /// <summary>
        /// ���������б�ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Fa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.LoadFA(Cmb_Fa.Text);
        }

        /// <summary>
        /// ��Ŀ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            this.LoadFA("");
            Cmb_Fa.SelectedIndex = 0;
        }

        /// <summary>
        /// ��ʾ�ڿ���������ô���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Pic_WcLimit_Click(object sender, EventArgs e)
        {
            CLDC_MeterUI.UI_FA.UI_FA_WcLimit Item = new CLDC_MeterUI.UI_FA.UI_FA_WcLimit();

            Item.ShowDialog();

            Item.Close();

            Item.Dispose();

            Item = null;

            this.LoadWcLimit();
        }



        #region --------------------------�춨�������仯�¼�---------------------------
        /// <summary>
        /// �춨�������仯�¼�����Ԫ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Count"></param>
        private void WcSetup_H_PointCountChange(object sender, int Count)
        {
            Tab_H.Text = string.Format("��Ԫ����{0}�� ", this.WcSetup_H.PointCount);
            this.SetGlfxImageStyle(this.WcSetup_H.GLFX);
        }
        /// <summary>
        /// �춨�������仯�¼���AԪ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Count"></param>
        private void WcSetup_A_PointCountChange(object sender, int Count)
        {
            Tab_A.Text = string.Format("AԪ����{0}��", this.WcSetup_A.PointCount);
            this.SetGlfxImageStyle(this.WcSetup_A.GLFX);
        }
        /// <summary>
        /// �춨�������仯�¼���BԪ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Count"></param>
        private void WcSetup_B_PointCountChange(object sender, int Count)
        {
            Tab_B.Text = string.Format("BԪ����{0}��", this.WcSetup_B.PointCount);
            this.SetGlfxImageStyle(this.WcSetup_B.GLFX);
        }
        /// <summary>
        /// �춨�������仯�¼���CԪ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="Count"></param>
        private void WcSetup_C_PointCountChange(object sender, int Count)
        {
            Tab_C.Text = string.Format("CԪ����{0}��", this.WcSetup_C.PointCount);
            this.SetGlfxImageStyle(this.WcSetup_C.GLFX);
        }
        #endregion

        #endregion 



        #region ------------��������������----------------
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="FAName">��������</param>
        public void LoadFA(string FAName)
        {
            CLDC_DataCore.Model.Plan.Plan_WcPoint _CheckPoint = new CLDC_DataCore.Model.Plan.Plan_WcPoint((int)base.TaiType, FAName);         //��һ������

            this.LoadFA(_CheckPoint);
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="FaItem">������Ŀ</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_WcPoint FaItem)
        {
            base.FaName = FaItem.Name;

            Cmb_Fa.SelectedIndex = 0;

            try
            {
                Cmb_Fa.Text = FaItem.Name;
            }
            catch
            {
                Cmb_Fa.SelectedIndex = 0;
            }

            if (FaItem.Count > 0)
            {
                StPlan_WcPoint _Item = FaItem.getCheckPoint(0);
                switch (_Item.PowerFangXiang)
                {
                    case CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�:
                        this.ToolGLFX_Click(Tab_Pz, new EventArgs());
                        break;
                    case CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�:
                        this.ToolGLFX_Click(Tab_Pf, new EventArgs());
                        break;
                    case CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�:
                        this.ToolGLFX_Click(Tab_Qz, new EventArgs());
                        break;
                    case CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�:
                        this.ToolGLFX_Click(Tab_Qf, new EventArgs());
                        break;
                    case CLDC_Comm.Enum.Cus_PowerFangXiang.��һ�����޹�:
                        this.ToolGLFX_Click(Tab_Q1, new EventArgs());
                        break;
                    case CLDC_Comm.Enum.Cus_PowerFangXiang.�ڶ������޹�:
                        this.ToolGLFX_Click(Tab_Q2, new EventArgs());
                        break;
                    case CLDC_Comm.Enum.Cus_PowerFangXiang.���������޹�:
                        this.ToolGLFX_Click(Tab_Q3, new EventArgs());
                        break;
                    case CLDC_Comm.Enum.Cus_PowerFangXiang.���������޹�:
                        this.ToolGLFX_Click(Tab_Q4, new EventArgs());
                        break;
                }
            }
            else
            {
                this.ToolGLFX_Click(Tab_Pz, new EventArgs());
            }

            this.Cmb_xIb.Text = FaItem.Qscz;    //���յ���

            this.Txt_Qs.Text = FaItem.Czqs.ToString();      //����Ȧ��

            this.Cmb_WcLimit.Text = FaItem.CzWcLimit;       //���������
            this.Cmb_CheckOutTime.Text = FaItem.CzCheckOutTime;

            #region --------���ù��ʷ���ť��ʽ-------------

            this.SetGlfxImageStyle(FaItem.YaoJianGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�), Tab_Pz);              
            this.SetGlfxImageStyle(FaItem.YaoJianGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�), Tab_Pf);
            this.SetGlfxImageStyle(FaItem.YaoJianGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�), Tab_Qz);
            this.SetGlfxImageStyle(FaItem.YaoJianGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�), Tab_Qf);
            this.SetGlfxImageStyle(FaItem.YaoJianGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.��һ�����޹�), Tab_Q1);
            this.SetGlfxImageStyle(FaItem.YaoJianGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.�ڶ������޹�), Tab_Q2);
            this.SetGlfxImageStyle(FaItem.YaoJianGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.���������޹�), Tab_Q3);
            this.SetGlfxImageStyle(FaItem.YaoJianGlfx(CLDC_Comm.Enum.Cus_PowerFangXiang.���������޹�), Tab_Q4);

            #endregion

            if (this.WcSetup_H != null) this.WcSetup_H.SetWcChecked(FaItem);
            if (this.WcSetup_A != null) this.WcSetup_A.SetWcChecked(FaItem);
            if (this.WcSetup_B != null) this.WcSetup_B.SetWcChecked(FaItem);
            if (this.WcSetup_C != null) this.WcSetup_C.SetWcChecked(FaItem);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_WcPoint Copy()
        {
            CLDC_DataCore.Model.Plan.Plan_WcPoint _Obj = new CLDC_DataCore.Model.Plan.Plan_WcPoint((int)TaiType, "");

            if (this.WcSetup_H != null) _Obj = this.WcSetup_H.GetFaInfo();

            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Qs.Text))
            {
                _Obj.Czqs = 1;
            }
            else
            {
                _Obj.Czqs = int.Parse(Txt_Qs.Text);
            }

            _Obj.CzWcLimit = Cmb_WcLimit.Text;

            _Obj.CzCheckOutTime = Cmb_CheckOutTime.Text;

            _Obj.Qscz = Cmb_xIb.Text;

            _Obj.SetPram((int)base.TaiType, base.FaName);

            return _Obj;

        }


        #endregion 

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            WcSetup_H.ClearPrj();
            WcSetup_A.ClearPrj();
            WcSetup_B.ClearPrj();
            WcSetup_C.ClearPrj();
        }





    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_ZouZi :UI_TableBase //UserControl
    {

        const string CONST_NOTESTRING = "�鿴���޸���˫��";

        const string CONST_ADD = "���汾��";

        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        #region ----------------���캯��------------------

        public UI_ZouZi()
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        public UI_ZouZi(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        public UI_ZouZi(CLDC_Comm.Enum.Cus_TaiType Ttype,string faname):base(Ttype,faname)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
            this.LoadFA(faname);
        }


        public UI_ZouZi(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_ZouZi FAItem)
            : base(Ttype, FAItem.Name)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
            this.LoadFA(FAItem);
        }

        #endregion

        #region -----------------�¼�-----------------------
        private void Dgv_Data_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Dgv_Data_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Dgv_Data.EndEdit();
        }
        /// <summary>
        /// ���ò鿴���޸���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Data_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)           //��Ŀ����
            {
                if (Dgv_Data[e.ColumnIndex - 1, e.RowIndex].Value == null)
                {
                    MessageBoxEx.Show(this,"��ѡ�����ַ�ʽ...", "������ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                CLDC_Comm.Enum.Cus_ZouZiMethod _Tmp = new CLDC_Comm.Enum.Cus_ZouZiMethod();

                if (Dgv_Data[e.ColumnIndex - 1, e.RowIndex].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.��׼��.ToString())
                {
                    _Tmp = CLDC_Comm.Enum.Cus_ZouZiMethod.��׼��;
                }
                else if (Dgv_Data[e.ColumnIndex - 1, e.RowIndex].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.�������ַ�.ToString())
                {
                    _Tmp = CLDC_Comm.Enum.Cus_ZouZiMethod.�������ַ�;
                }
                else if (Dgv_Data[e.ColumnIndex - 1, e.RowIndex].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.�ƶ����巨.ToString())
                {
                    _Tmp = CLDC_Comm.Enum.Cus_ZouZiMethod.�ƶ����巨;
                }
                else if (Dgv_Data[e.ColumnIndex - 1, e.RowIndex].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.У�˳���.ToString())
                {
                    _Tmp = CLDC_Comm.Enum.Cus_ZouZiMethod.У�˳���;
                }

                if (this.Controls[0] is PrjUI.UI_ZouZiFeiLv) return;
                PrjUI.UI_ZouZiFeiLv _Panel;
                if (Dgv_Data.CurrentCell.Tag is List<StPlan_ZouZi.StPrjFellv>)
                {
                    _Panel = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_ZouZiFeiLv(_Tmp, (List<StPlan_ZouZi.StPrjFellv>)Dgv_Data.CurrentCell.Tag);
                }
                else
                {
                    _Panel = new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_ZouZiFeiLv(_Tmp);
                }
                _Panel.ClosePanel += new CLDC_MeterUI.UI_FA.FAPrj.PrjUI.UI_ZouZiFeiLv.Evt_ClosePanel(FeiLv_ClosePanel);

                this.Controls.Add(_Panel);
                this.Controls.SetChildIndex(_Panel, 0);

                _Panel.Left = Dgv_Data.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Left;

                _Panel.Top = Dgv_Data.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Bottom + this.panel1.Height;

                _Panel.BringToFront();

                Dgv_Data.Enabled = false;

            }
        }

        private void Dgv_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpDownButtonState(e.RowIndex);

            if (e.ColumnIndex == Dgv_Data.Columns.Count - 1)         //���һ��
            {
                if (Dgv_Data[e.ColumnIndex, e.RowIndex].Value.ToString() != CONST_ADD)
                {
                    if (MessageBoxEx.Show(this,"��ȷ��Ҫɾ���÷�����Ŀô��", "ɾ��ѯ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Dgv_Data.Rows.RemoveAt(e.RowIndex);
                        this.CallOrder();
                    }
                    else
                    {
                        Dgv_Data[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;
                    }
                    return;
                }

                if (!CheckOK(e.RowIndex)) return;

                Dgv_Data[e.ColumnIndex, e.RowIndex].Value = "ɾ��";
                Dgv_Data[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;

                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[6].Value = CONST_NOTESTRING;
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = CONST_ADD;
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Style.ForeColor = Color.Blue;
                this.CallOrder();
            }
            else
            {
                Dgv_Data.BeginEdit(true);
                if (Dgv_Data.CurrentCell is DataGridViewComboBoxCell)
                {
                    if (BeforeRowIndex != e.RowIndex || BeforeColIndex != e.ColumnIndex)
                    {
                        SendKeys.Send("{F4}");
                    }
                }

                BeforeColIndex = e.ColumnIndex;

                BeforeRowIndex = e.RowIndex;
            }
        }

        /// <summary>
        /// �������ݽ���ر��¼�
        /// </summary>
        /// <param name="FeiLvItem"></param>
        private void FeiLv_ClosePanel(List<StPlan_ZouZi.StPrjFellv> FeiLvItem)
        {
            PrjUI.UI_ZouZiFeiLv _Panel = this.Controls[0] as PrjUI.UI_ZouZiFeiLv;

            if (FeiLvItem.Count > 0)
            {
                Dgv_Data.CurrentCell.Tag = FeiLvItem;
                string zouziDesp = string.Empty;
                for (int i = 0; i < FeiLvItem.Count; i++)
                {
                    zouziDesp += FeiLvItem[i].FeiLv.ToString() + FeiLvItem[i].ZouZiTime.ToString() + " ";
                }
                Dgv_Data.CurrentCell.Value = zouziDesp;
            }
            this.Controls.RemoveAt(0);
            _Panel.Dispose();
            _Panel = null;

            Dgv_Data.Enabled = true;
        }

        private void Cmd_MoveUp_Click(object sender, EventArgs e)
        {
            base.CmdMoveUp_Click(sender, e);
        }

        private void Cmd_MoveDown_Click(object sender, EventArgs e)
        {
            base.CmdMoveDown_Click(sender, e);
        }
        /// <summary>
        /// �ӷ����б���ѡ�񷽰�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Fa_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Cmb_Fa.SelectedIndex == 0) return;

            this.FAName = Cmb_Fa.Text;
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Clear_Click(object sender, EventArgs e)
        {
            base.ClearDataView();
            Cmb_Fa.SelectedIndex = 0;
        }

                #endregion 

        #region ------------------˽�з���������---------------

        /// <summary>
        /// ��ʼ�����ComboBoxEx
        /// </summary>
        private void DefaultCombo()
        {
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_ZOUZI_FOLDERNAME);

            #region ---------���ʷ��������˵�----------------------
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("�����й�");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("�����й�");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("�����޹�");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("�����޹�");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("��һ�����޹�");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("�ڶ������޹�");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("���������޹�");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("���������޹�");
            #endregion 

            #region -----------��ʼ��Ԫ�������˵�----------------

            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[2]).Items.Add(CLDC_Comm.Enum.Cus_PowerYuanJian.H.ToString());
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[2]).Items.Add(CLDC_Comm.Enum.Cus_PowerYuanJian.A.ToString());
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[2]).Items.Add(CLDC_Comm.Enum.Cus_PowerYuanJian.B.ToString());
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[2]).Items.Add(CLDC_Comm.Enum.Cus_PowerYuanJian.C.ToString());

            #endregion 

            #region -------------------��ʼ���������������˵�---------------

            List<string> _Glyss = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysName();

            for(int i =0;i<_Glyss.Count;i++)
            {
                ((DataGridViewComboBoxColumn)Dgv_Data.Columns[3]).Items.Add(_Glyss[i]);
            }

            _Glyss = null;
            
            #endregion 

            #region ------------��ʼ���������������˵�--------------------
            CLDC_DataCore.SystemModel.Item.csxIbDic _xIbCol = new CLDC_DataCore.SystemModel.Item.csxIbDic();
            _xIbCol.Load();
            List<string> _xIbs = _xIbCol.getxIb();

            for (int i = 0; i < _xIbs.Count; i++)
            {
                ((DataGridViewComboBoxColumn)Dgv_Data.Columns[4]).Items.Add(_xIbs[i]);
            }

            _xIbs = null;
            _xIbCol = null;
            #endregion

            #region -----------��ʼ�����ַ�ʽ�����˵�--------------
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[5]).Items.Add(CLDC_Comm.Enum.Cus_ZouZiMethod.У�˳���.ToString());
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[5]).Items.Add(CLDC_Comm.Enum.Cus_ZouZiMethod.��׼��.ToString());
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[5]).Items.Add(CLDC_Comm.Enum.Cus_ZouZiMethod.�������ַ�.ToString());

            #endregion 

            int RowIndex = Dgv_Data.Rows.Add();
            Dgv_Data.Rows[RowIndex].Cells[6].Value = CONST_NOTESTRING;
            Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = CONST_ADD;
            Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Style.ForeColor = Color.Blue;
            Dgv_Data.Refresh();
        }

        /// <summary>
        /// ����׼ȷ��У��
        /// </summary>
        /// <param name="RowIndex">�����</param>
        /// <returns></returns>
        private bool CheckOK(int RowIndex)
        {
            if (Dgv_Data[1, RowIndex].Value == null || Dgv_Data[1, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"��ѡ����ȷ�Ĺ��ʷ���...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[1, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[2, RowIndex].Value == null || Dgv_Data[2, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"��ѡ����ȷ��Ԫ��...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[2, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[3, RowIndex].Value == null || Dgv_Data[3, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"��ѡ����ȷ�Ĺ�������...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[3, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[4, RowIndex].Value == null || Dgv_Data[4, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"��ѡ����ȷ�ĵ�������...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[4, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[5, RowIndex].Value == null || Dgv_Data[5, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"��ѡ����ȷ�����ַ�ʽ...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[5, RowIndex].Selected = true;
                return false;
            }
            if (!(Dgv_Data[6, RowIndex].Tag is List<StPlan_ZouZi.StPrjFellv>))
            {
                MessageBoxEx.Show(this,"û�о������������...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        #endregion

        #region  -----------------���з���������-----------

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
        /// <param name="FAName">��������</param>
        public void LoadFA(string FAName)
        {

            Dgv_Data.Rows.Clear();          //���������б�����

            CLDC_DataCore.Model.Plan.Plan_ZouZi _ZouZi = new CLDC_DataCore.Model.Plan.Plan_ZouZi((int)base.TaiType, FAName);        //��һ������

            this.LoadFA(_ZouZi);
        }

        /// <summary>
        /// ���ط�����Ŀ
        /// </summary>
        /// <param name="FaItem">������Ŀ</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_ZouZi FaItem)
        {
            Dgv_Data.Rows.Clear();

            base.FaName = FaItem.Name;

            try
            {
                Cmb_Fa.Text = FaItem.Name;
            }
            catch
            {
                Cmb_Fa.SelectedIndex = 0;
            }
            for (int _i = 0; _i < FaItem.Count; _i++)            //ѭ����������
            {
                StPlan_ZouZi _Obj = FaItem.getZouZiPrj(_i);         //ȡ��һ��������Ŀ
                string zouziShortDecript = string.Empty;
                for (int i = 0; i < _Obj.ZouZiPrj.Count; i++)
                {
                    zouziShortDecript += _Obj.ZouZiPrj[i].FeiLv + _Obj.ZouZiPrj[i].ZouZiTime + " ";
                }
                //Dgv_Data.Rows[RowIndex].Cells[6].Value = zouziShortDecript;
                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[0].Value = _i + 1;
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[1]).Value = _Obj.PowerFangXiang.ToString();        //���ʷ���
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[2]).Value = _Obj.PowerYj.ToString();               //Ԫ��
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[3]).Value = _Obj.Glys;                             //��������
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[4]).Value = _Obj.xIb;                             //��������
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[5]).Value = _Obj.ZouZiMethod.ToString();          //���ַ���
                Dgv_Data.Rows[RowIndex].Cells[6].Value = zouziShortDecript;
                Dgv_Data.Rows[RowIndex].Cells[6].Tag =_Obj.ZouZiPrj;                            //��������
                Dgv_Data.Rows[RowIndex].Cells[7].Value = _Obj.ZuHeWc == "0" ? false : true;            //������
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "ɾ��";       //ɾ����ť
                Dgv_Data[Dgv_Data.Columns.Count - 1, RowIndex].Style.ForeColor = Color.Red;     //ɾ����ťΪ��ɫ
            }

            {
                int RowIndex = Dgv_Data.Rows.Add();                 //�������һ�����У���������
                Dgv_Data.Rows[RowIndex].Cells[6].Value = CONST_NOTESTRING;
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = CONST_ADD;
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Style.ForeColor = Color.Blue;
            }

            this.UpDownButtonState(0);    //���������ƶ���ť״̬

        }


        /// <summary>
        /// ��������
        /// </summary>
        public CLDC_DataCore.Model.Plan.Plan_ZouZi Copy()
        {
            if (Dgv_Data.Rows.Count == 1) return new CLDC_DataCore.Model.Plan.Plan_ZouZi((int)TaiType, "");

            CLDC_DataCore.Model.Plan.Plan_ZouZi _Obj = new CLDC_DataCore.Model.Plan.Plan_ZouZi((int)TaiType, "");

            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Dgv_Data[Dgv_Data.Columns.Count - 1, i].Value.ToString() == CONST_ADD)
                    break;
                else
                {
                    if (!this.CheckOK(i)) return new CLDC_DataCore.Model.Plan.Plan_ZouZi((int)TaiType, "");

                    CLDC_Comm.Enum.Cus_PowerFangXiang _Tmp = new CLDC_Comm.Enum.Cus_PowerFangXiang();

                    if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.��һ�����޹�.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.��һ�����޹�;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.�ڶ������޹�.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.�ڶ������޹�;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.���������޹�.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.���������޹�;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.���������޹�.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.���������޹�;
                    else
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�;

                    CLDC_Comm.Enum.Cus_ZouZiMethod _TmpMethod = new CLDC_Comm.Enum.Cus_ZouZiMethod();

                    if (Dgv_Data[5, i].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.��׼��.ToString())
                        _TmpMethod = CLDC_Comm.Enum.Cus_ZouZiMethod.��׼��;
                    else if (Dgv_Data[5, i].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.�������ַ�.ToString())
                        _TmpMethod = CLDC_Comm.Enum.Cus_ZouZiMethod.�������ַ�;
                    else if (Dgv_Data[5, i].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.�ƶ����巨.ToString())
                        _TmpMethod = CLDC_Comm.Enum.Cus_ZouZiMethod.�ƶ����巨;
                    else if (Dgv_Data[5, i].Value.ToString() == CLDC_Comm.Enum.Cus_ZouZiMethod.У�˳���.ToString())
                    {
                        _TmpMethod = CLDC_Comm.Enum.Cus_ZouZiMethod.У�˳���;
                    }
                    else
                    {
                        _TmpMethod = CLDC_Comm.Enum.Cus_ZouZiMethod.��׼��;
                    }
                    CLDC_Comm.Enum.Cus_PowerYuanJian _TmpYuan = new CLDC_Comm.Enum.Cus_PowerYuanJian();

                    if (Dgv_Data[2, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerYuanJian.H.ToString())
                        _TmpYuan = CLDC_Comm.Enum.Cus_PowerYuanJian.H;
                    else if (Dgv_Data[2, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerYuanJian.A.ToString())
                        _TmpYuan = CLDC_Comm.Enum.Cus_PowerYuanJian.A;
                    else if (Dgv_Data[2, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerYuanJian.B.ToString())
                        _TmpYuan = CLDC_Comm.Enum.Cus_PowerYuanJian.B;
                    else if (Dgv_Data[2, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerYuanJian.C.ToString())
                        _TmpYuan = CLDC_Comm.Enum.Cus_PowerYuanJian.C;
                    else
                        _TmpYuan = CLDC_Comm.Enum.Cus_PowerYuanJian.H;



                    _Obj.Add(_Tmp,                                  //���ʷ��� 
                             _TmpMethod,                            //���ַ���
                             _TmpYuan,                              //Ԫ��
                             Dgv_Data[3, i].Value.ToString(),       //�������� 
                             Dgv_Data[4, i].Value.ToString(),       //��������    
                             "",                                    //��������
                             Dgv_Data[7, i].Value==null?"0":(bool)Dgv_Data[7,i].Value==false?"0":"1",       //������
                             (List<StPlan_ZouZi.StPrjFellv>)Dgv_Data[6, i].Tag);                   //������Ŀ
                }
            }
            _Obj.SetPram((int)base.TaiType, base.FaName);

            return _Obj;
        }

        #endregion 

        private void Dgv_Data_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

    }
}

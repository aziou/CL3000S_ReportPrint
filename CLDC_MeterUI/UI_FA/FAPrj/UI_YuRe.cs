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
    public partial class UI_YuRe :UI_TableBase // UserControl 
    {

        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        #region ----------------����------------------

        public UI_YuRe()
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Ttype">̨������</param>
        public UI_YuRe(CLDC_Comm.Enum.Cus_TaiType Ttype)
            : base(Ttype, "")
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Ttype">̨������</param>
        /// <param name="faname">��Ҫ���صķ�������</param>
        public UI_YuRe(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
            : base(Ttype, faname)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
            this.LoadFA(faname);
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Ttype">̨������</param>
        /// <param name="FAItem">Ԥ�ȷ�����Ŀ</param>
        public UI_YuRe(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_YuRe FAItem):base(Ttype,FAItem.Name)
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

        private void Dgv_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpDownButtonState(e.RowIndex);

            if (e.ColumnIndex == Dgv_Data.Columns.Count - 1)         //���һ��
            {
                if (Dgv_Data[e.ColumnIndex, e.RowIndex].Value.ToString() != "���")
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

                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "���";
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

        private void Cmd_MoveUp_Click(object sender, EventArgs e)
        {
            base.CmdMoveUp_Click(sender, e);
        }

        private void Cmd_MoveDown_Click(object sender, EventArgs e)
        {
            base.CmdMoveDown_Click(sender, e);
        }

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

        #region --------------------˽�з���������--------------
        /// <summary>
        /// ��ʼ�����ComboBoxEx
        /// </summary>
        private void DefaultCombo()
        {
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_YURE_FOLDERNAME);

            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("�����й�");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("�����й�");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("�����޹�");
            ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add("�����޹�");

            #region ��ʼ���������������˵�
            CLDC_DataCore.SystemModel.Item.csxIbDic _xIbCol = new CLDC_DataCore.SystemModel.Item.csxIbDic();
            _xIbCol.Load();
            List<string> _xIbs = _xIbCol.getxIb();

            for (int i = 0; i < _xIbs.Count; i++)
            {
                ((DataGridViewComboBoxColumn)Dgv_Data.Columns[2]).Items.Add(_xIbs[i]);
            }
            #endregion

            int RowIndex = Dgv_Data.Rows.Add();
            Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "���";
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
                MessageBoxEx.Show(this,"��ѡ����ȷ��Ԥ�ȵ���...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[2, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[3, RowIndex].Value == null || !CLDC_DataCore.Function.Number.IsNumeric(Dgv_Data[3, RowIndex].Value.ToString().Trim()))
            {
                MessageBoxEx.Show(this,"����д��ȷ��Ԥ��ʱ�䣬ʱ����Ϊһ�������������...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[3, RowIndex].Selected = true;
                return false;
            }
            return true;
        }



        #endregion 


        #region ----------���з���������------------

        /// <summary>
        /// ��������
        /// </summary>
        public CLDC_DataCore.Model.Plan.Plan_YuRe Copy()
        {
            if (Dgv_Data.Rows.Count == 1) return new CLDC_DataCore.Model.Plan.Plan_YuRe((int)TaiType,"");

            CLDC_DataCore.Model.Plan.Plan_YuRe _Obj = new CLDC_DataCore.Model.Plan.Plan_YuRe((int)TaiType, "");

            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Dgv_Data[Dgv_Data.Columns.Count - 1, i].Value.ToString() == "���")
                    break;
                else
                {
                    if (!this.CheckOK(i)) return new CLDC_DataCore.Model.Plan.Plan_YuRe((int)TaiType,"");

                    CLDC_Comm.Enum.Cus_PowerFangXiang _Tmp = new CLDC_Comm.Enum.Cus_PowerFangXiang();

                    if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�;
                    else if (Dgv_Data[1, i].Value.ToString() == CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�.ToString())
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�;
                    else
                        _Tmp = CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�;

                    _Obj.Add(i, _Tmp, Dgv_Data[2, i].Value.ToString(), float.Parse(Dgv_Data[3, i].Value.ToString()));
                }
            }
            _Obj.SetPram((int)base.TaiType, base.FaName);

            return _Obj;
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
        /// <param name="FAName">��������</param>
        public void LoadFA(string FAName)
        {

            Dgv_Data.Rows.Clear();          //���������б�����

            CLDC_DataCore.Model.Plan.Plan_YuRe _YuRe = new CLDC_DataCore.Model.Plan.Plan_YuRe((int)base.TaiType, FAName);        //��һ������

            this.LoadFA(_YuRe);
        }

        /// <summary>
        /// ���ط�����Ŀ
        /// </summary>
        /// <param name="FaItem">������Ŀ</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_YuRe FaItem)
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
                StPlan_YuRe _Obj = FaItem.getYuRePrj(_i);         //ȡ��һ��������Ŀ

                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[0].Value = _i + 1;
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[1]).Value = _Obj.PowerFangXiang.ToString();        //���ʷ���
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[2]).Value = _Obj.xIb;              //��������
                Dgv_Data.Rows[RowIndex].Cells[3].Value = _Obj.Times;                            //Ԥ��ʱ�� 
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "ɾ��";       //ɾ����ť
                Dgv_Data[Dgv_Data.Columns.Count - 1, RowIndex].Style.ForeColor = Color.Red;     //ɾ����ťΪ��ɫ
            }

            {
                int RowIndex = Dgv_Data.Rows.Add();                 //�������һ�����У���������
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "���";
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Style.ForeColor = Color.Blue;
            }

            this.UpDownButtonState(0);    //���������ƶ���ť״̬

        }

        #endregion 

        private void Dgv_Data_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}

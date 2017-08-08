using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_PowerConsume : UI_TableBaseNew
    {
        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        #region ----------------����------------------
        /// <summary>
        /// ���캯��
        /// </summary>
        public UI_PowerConsume()
        {
            InitializeComponent();

            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Ttype">̨������</param>
        public UI_PowerConsume(CLDC_Comm.Enum.Cus_TaiType Ttype)
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
        public UI_PowerConsume(CLDC_Comm.Enum.Cus_TaiType Ttype, string faname)
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
        /// <param name="FAItem">�������鷽����Ŀ</param>
        public UI_PowerConsume(CLDC_Comm.Enum.Cus_TaiType Ttype, CLDC_DataCore.Model.Plan.Plan_PowerConsume FAItem)
            : base(Ttype, FAItem.Name)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
            this.LoadFA(FAItem);
        }
        #endregion

        #region -----------------�¼�-----------------------
        private void Dgv_Data_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Dgv_Data.EndEdit();
        }

        private void Dgv_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.UpDownButtonState(e.RowIndex);

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
        /// ��ʼ�����ComboBox
        /// </summary>
        private void DefaultCombo()
        {
            //��ʼ��������Ŀ���������˵�
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_GONGHAO_FOLDERNAME);

            #region 2010-05-13 Add by ����Ĭ�Ϲ���������
            int RowIndex = 0;

            RowIndex = Dgv_Data.Rows.Add();
            Dgv_Data.Rows[RowIndex].Cells[0].Value = RowIndex + 1;
            Dgv_Data.Rows[RowIndex].Cells[1].Value = false;
            Dgv_Data.Rows[RowIndex].Cells[2].Value = "��������";
            Dgv_Data.Rows[RowIndex].Cells[3].Value = "";

            Dgv_Data.Refresh();
            #endregion
        }

        #endregion

        #region ----------���з���������------------

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_PowerConsume Copy()
        {
            if (Dgv_Data.Rows.Count < 1) return new CLDC_DataCore.Model.Plan.Plan_PowerConsume((int)TaiType, "");

            CLDC_DataCore.Model.Plan.Plan_PowerConsume _Obj = new CLDC_DataCore.Model.Plan.Plan_PowerConsume((int)TaiType, "");
            int iOrderIndex = 0;

            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                string _Tmp = "";
                bool bYn = false;

                bYn = bool.Parse(((DataGridViewCheckBoxCell)Dgv_Data.Rows[i].Cells[1]).Value.ToString());
                if (bYn)
                {
                    if (Dgv_Data.Rows[i].Cells[3].Value == null || Dgv_Data.Rows[i].Cells[3].Value.ToString() == "")
                    {
                        _Tmp = "1.5,";
                    }
                    else
                    {
                        _Tmp = Dgv_Data.Rows[i].Cells[3].Value.ToString() + ",";
                    }
                    if (Dgv_Data.Rows[i].Cells[4].Value == null || Dgv_Data.Rows[i].Cells[4].Value.ToString() == "")
                    {
                        _Tmp += "6,";
                    }
                    else
                    {
                        _Tmp += Dgv_Data.Rows[i].Cells[4].Value.ToString() + ",";
                    }
                    if (Dgv_Data.Rows[i].Cells[5].Value == null || Dgv_Data.Rows[i].Cells[5].Value.ToString() == "")
                    {
                        _Tmp += "0.2";
                    }
                    else
                    {
                        _Tmp += Dgv_Data.Rows[i].Cells[5].Value.ToString();
                    }
                    _Obj.Add(iOrderIndex++, ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.��������).ToString() + "11", Dgv_Data.Rows[i].Cells[2].Value.ToString(), _Tmp);
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

            CLDC_DataCore.Model.Plan.Plan_PowerConsume _StPowerConsume = new CLDC_DataCore.Model.Plan.Plan_PowerConsume((int)base.TaiType, FAName);        //��һ������

            this.LoadFA(_StPowerConsume);
        }

        /// <summary>
        /// ���ط�����Ŀ
        /// </summary>
        /// <param name="FaItem">������Ŀ</param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_PowerConsume FaItem)
        {
            Dgv_Data.Rows.Clear();
            DefaultCombo();

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
                CLDC_DataCore.Struct.StPowerConsume _Obj = FaItem.getPowerConsumePrj(_i);         //ȡ��һ��������Ŀ
                //�����б����Ƿ��иü춨���ΪҪ��
                for (int _j = 0; _j < Dgv_Data.Rows.Count; _j++)
                {
                    if (Dgv_Data.Rows[_j].Cells[2].Value.ToString() == _Obj.PowerConsumePrjName)
                    {
                        ((DataGridViewCheckBoxCell)Dgv_Data.Rows[_j].Cells[1]).Value = true;
                        try
                        {
                            string[] strPara = _Obj.PrjParm.Split(',');
                            Dgv_Data.Rows[_j].Cells[3].Value = strPara[0];
                            Dgv_Data.Rows[_j].Cells[4].Value = strPara[1];
                            Dgv_Data.Rows[_j].Cells[5].Value = strPara[2];
                        }
                        catch { }
                    }
                }
            }
            this.UpDownButtonState(0);    //���������ƶ���ť״̬
        }

        #endregion
    }
}

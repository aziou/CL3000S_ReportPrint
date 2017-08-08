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
    /// <summary>
    /// �����������������ݱȶԷ������
    /// ��    �ߣ�zzg soinlove@126.com
    /// ��д���ڣ�2014-05-07
    /// �޸ļ�¼��
    ///         �޸�����		     �޸���	            �޸�����
    ///
    /// </summary>
    public partial class UI_Infrared : UI_TableBase
    {
        #region--------------˽�б���-----------------
        private int BeforeColIndex = 0;

        private int BeforeRowIndex = 0;

        private CLDC_DataCore.SystemModel.Item.csDataFlag _csDataFlag;
        
        #endregion------------------------------------

        #region--------------��������-----------------
        /// <summary>
        /// �������ƣ�ֻд�������ú��Զ����ط�����ǰ���Ǹ÷�������
        /// </summary>
        public string PlanName
        {
            set
            {
                this.LoadPlan(value);
            }
        }
        #endregion------------------------------------

        #region--------------���캯��-----------------
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public UI_Infrared()
        {
            InitializeComponent();

            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="p_ctt_Ttype">̨������</param>
        public UI_Infrared(CLDC_Comm.Enum.Cus_TaiType p_ctt_Ttype)
            : base(p_ctt_Ttype, "")
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="p_ctt_Ttype">̨������</param>
        /// <param name="p_str_PlanName">��Ҫ���صķ�������</param>
        public UI_Infrared(CLDC_Comm.Enum.Cus_TaiType p_ctt_Ttype, string p_str_PlanName)
            : base(p_ctt_Ttype, p_str_PlanName)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
            this.LoadPlan(p_str_PlanName);
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="p_ctt_Ttype">̨������</param>
        /// <param name="p_plc_InfraredName">�������ݱȶ����鷽����Ŀ</param>
        public UI_Infrared(CLDC_Comm.Enum.Cus_TaiType p_ctt_Ttype, CLDC_DataCore.Model.Plan.Plan_Infrared p_plc_InfraredName)
            : base(p_ctt_Ttype, p_plc_InfraredName.Name)
        {
            InitializeComponent();
            base.Init(Dgv_Data, Cmd_MoveUp, Cmd_MoveDown);
            this.DefaultCombo();
   
            this.LoadPlan(p_plc_InfraredName);
        }
        #endregion------------------------------------

        #region--------------˽���¼�-----------------
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

            this.PlanName = Cmb_Fa.Text;
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

        #endregion------------------------------------

        #region--------------˽�к���-----------------
        /// <summary>
        /// ��ʼ�����ComboBoxEx
        /// </summary>
        private void DefaultCombo()
        {
            //����ʼ��������Ŀ���������˵���
            base.FaNameCombInit(Cmb_Fa, CLDC_DataCore.Const.Variable.CONST_FA_HW_FOLDERNAME);
                        

            //����ʼ����Ŀ���������˵���
            _csDataFlag = new CLDC_DataCore.SystemModel.Item.csDataFlag();
            _csDataFlag.Load();

            List<string> lst_DataFlagNames = _csDataFlag.GetDataFlagNameList();

            foreach (string name in lst_DataFlagNames)
            {
                ((DataGridViewComboBoxColumn)Dgv_Data.Columns[1]).Items.Add(name);
            }            

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
                MessageBoxEx.Show(this,"����д��Ŀ����...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[1, RowIndex].Selected = true;
                return false;
            }
            if (Dgv_Data[2, RowIndex].Value == null || Dgv_Data[2, RowIndex].Value.ToString().Trim() == "")
            {
                MessageBoxEx.Show(this,"����д��ʶ��...", "¼�����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dgv_Data[2, RowIndex].Selected = true;
                return false;
            }            

            return true;
        }
        #endregion------------------------------------

        #region--------------��������-----------------

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public CLDC_DataCore.Model.Plan.Plan_Infrared Copy()
        {
            if (Dgv_Data.Rows.Count == 1) return new CLDC_DataCore.Model.Plan.Plan_Infrared((int)TaiType, "");

            CLDC_DataCore.Model.Plan.Plan_Infrared _Obj = new CLDC_DataCore.Model.Plan.Plan_Infrared((int)TaiType, "");

            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Dgv_Data[Dgv_Data.Columns.Count - 1, i].Value.ToString() == "���")
                    break;
                else
                {
                    if (!this.CheckOK(i)) return new CLDC_DataCore.Model.Plan.Plan_Infrared((int)TaiType, "");

                    _Obj.Add(i, Dgv_Data.Rows[i].Cells[1].Value.ToString(),
                               Dgv_Data.Rows[i].Cells[2].Value.ToString()
                               );
                }
            }

            _Obj.SetPram((int)base.TaiType, base.FaName);

            return _Obj;
        }



        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_str_PlanName">��������</param>
        public void LoadPlan(string p_str_PlanName)
        {
            //�������б����ݡ�
            Dgv_Data.Rows.Clear();

            //����һ��������
            CLDC_DataCore.Model.Plan.Plan_Infrared pcc_InfraredPlan = new CLDC_DataCore.Model.Plan.Plan_Infrared((int)base.TaiType, p_str_PlanName);

            //�����ط�����
            this.LoadPlan(pcc_InfraredPlan);
        }

        /// <summary>
        /// ���ط�����Ŀ
        /// </summary>
        /// <param name="p_pcc_Item">������Ŀ</param>
        public void LoadPlan(CLDC_DataCore.Model.Plan.Plan_Infrared p_pcc_Item)
        {
            Dgv_Data.Rows.Clear();

            base.FaName = p_pcc_Item.Name;

            try
            {
                Cmb_Fa.Text = p_pcc_Item.Name;
            }
            catch
            {
                Cmb_Fa.SelectedIndex = 0;
            }

            //��������������
            for (int _i = 0; _i < p_pcc_Item.Count; _i++)
            {
                //��ȡ��һ��������Ŀ��
                StPlan_Infrared _Obj = p_pcc_Item.GetCarrierPrj(_i);

                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[0].Value = _i + 1;                
                ((DataGridViewComboBoxCell)Dgv_Data.Rows[RowIndex].Cells[1]).Value = _Obj.str_Name;                 //��Ŀ����
                ((DataGridViewCell)Dgv_Data.Rows[RowIndex].Cells[2]).Value = _Obj.str_Code;                 //��ʶ��                
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "ɾ��";                           //ɾ����ť
                Dgv_Data[Dgv_Data.Columns.Count - 1, RowIndex].Style.ForeColor = Color.Red;                         //ɾ����ťΪ��ɫ
            }

            //���������һ�����У�����������
            {
                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Value = "���";
                Dgv_Data.Rows[RowIndex].Cells[Dgv_Data.Columns.Count - 1].Style.ForeColor = Color.Blue;
            }

            //�����������ƶ���ť״̬��
            this.UpDownButtonState(0);

        }
        #endregion------------------------------------

        private void Dgv_Data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string strDataFlagName = "";
            CLDC_DataCore.Struct.StDataFlagInfo _DataFlag;
            if (e.ColumnIndex == 1 && e.RowIndex >= 0)
            {
                strDataFlagName = Dgv_Data[e.ColumnIndex, e.RowIndex].Value.ToString();
                _DataFlag = _csDataFlag.GetDataFlagInfo(strDataFlagName);
                
                ((DataGridViewCell)Dgv_Data.Rows[e.RowIndex].Cells[e.ColumnIndex + 1]).Value = _DataFlag.DataFlag;
                
            }
        }

        private void Dgv_Data_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        
       

    }
}

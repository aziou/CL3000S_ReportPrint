using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_TableBaseNew : UserControl
    {
        private DataGridView BaseDataGrid = null;

        private ButtonX _MoveUp = null;

        private ButtonX _MoveDown = null;
        /// <summary>
        /// ̨������
        /// </summary>
        protected CLDC_Comm.Enum.Cus_TaiType TaiType;
        /// <summary>
        /// ��������
        /// </summary>
        protected string FaName = "";

        public UI_TableBaseNew()
        {
            InitializeComponent();
        }

        public UI_TableBaseNew(CLDC_Comm.Enum.Cus_TaiType enumType, string faname)
        {
            TaiType = enumType;
            FaName = faname;
            InitializeComponent();
        }

        protected void Init(DataGridView Item, ButtonX Up, ButtonX Down)
        {
            BaseDataGrid = Item;
            _MoveDown = Down;
            _MoveUp = Up;
        }

        protected List<string> GetFaNames(string FaTypeString)
        {
            return CLDC_DataCore.Model.Plan.Model_Plan.getFileNames(FaTypeString,(int)this.TaiType);
        }

        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        protected void FaNameCombInit(ComboBoxEx Item, string FaTypeString)
        {
            #region --------------���ط�����Ϣ�б�----------------------------------
            List<string> _FaNames = this.GetFaNames(FaTypeString);
            Item.Items.Clear();
            if (_FaNames.Count == 0)
            {
                Item.Items.Add("û�з���...");
            }
            else
            {
                Item.Items.Add("��������б���ѡ��...");
            }
            for (int i = 0; i < _FaNames.Count; i++)
            {
                Item.Items.Add(_FaNames[i]);
            }

            Item.SelectedIndex = 0;

            #endregion
        }

        protected void ClearDataView()
        {
            for (int i =  BaseDataGrid.Rows.Count - 2; i >=0; i--)
            {
                BaseDataGrid.Rows.RemoveAt(i); 
            }
            _MoveDown.Enabled = false;
            _MoveUp.Enabled = false;
        }

        /// <summary>
        /// ��Ŀ���ƶ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CmdMoveUp_Click(object sender, EventArgs e)
        {
            if (BaseDataGrid.SelectedRows[0].Index == 0) return;
            int RowIndex = BaseDataGrid.SelectedRows[0].Index;
            BaseDataGrid.Rows.Insert(RowIndex - 1, 1);
            for (int i = 0; i < BaseDataGrid.Columns.Count; i++)                //��¡�����⣬ֻ��ʹ��ѭ����ֵ���츱��
            {
                BaseDataGrid.Rows[RowIndex - 1].Cells[i].Value = BaseDataGrid.SelectedRows[0].Cells[i].Value;
                BaseDataGrid.Rows[RowIndex - 1].Cells[i].Tag = BaseDataGrid.SelectedRows[0].Cells[i].Tag;
            }
            BaseDataGrid.Rows.Remove(BaseDataGrid.SelectedRows[0]);
            BaseDataGrid.ClearSelection();
            BaseDataGrid.Rows[RowIndex - 1].Selected = true;
            BaseDataGrid.Rows[RowIndex - 1].Cells[BaseDataGrid.Columns.Count - 1].Style.ForeColor = Color.Red;
            this.CallOrder();

            this.UpDownButtonState(RowIndex - 1);
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CmdMoveDown_Click(object sender, EventArgs e)
        {
            if (BaseDataGrid.SelectedRows[0].Index == BaseDataGrid.Rows.Count - 1) return;
            int RowIndex = BaseDataGrid.SelectedRows[0].Index;
            BaseDataGrid.Rows.Insert(RowIndex + 2, 1);
            for (int i = 0; i < BaseDataGrid.Columns.Count; i++)                //��¡�����⣬ֻ��ʹ��ѭ����ֵ���츱��
            {
                BaseDataGrid.Rows[RowIndex + 2].Cells[i].Value = BaseDataGrid.SelectedRows[0].Cells[i].Value;
                BaseDataGrid.Rows[RowIndex + 2].Cells[i].Tag = BaseDataGrid.SelectedRows[0].Cells[i].Tag;
            }
            BaseDataGrid.Rows.Remove(BaseDataGrid.SelectedRows[0]);
            BaseDataGrid.ClearSelection();
            BaseDataGrid.Rows[RowIndex + 1].Selected = true;
            BaseDataGrid.Rows[RowIndex + 1].Cells[BaseDataGrid.Columns.Count - 1].Style.ForeColor = Color.Red;
            this.CallOrder();

            this.UpDownButtonState(RowIndex + 1);
        }

        /// <summary>
        /// �����ƶ���ť״̬
        /// </summary>
        /// <param name="RowIndex"></param>
        protected void UpDownButtonState(int RowIndex)
        {
            if (BaseDataGrid.Rows.Count > 1 && RowIndex < BaseDataGrid.Rows.Count )  //�����������2,��ѡ���в������һ��
                if (RowIndex == 0)
                {
                    _MoveDown.Enabled = true;
                    _MoveUp.Enabled = false;
                }
                else if (RowIndex == BaseDataGrid.Rows.Count - 1)
                {
                    _MoveDown.Enabled = false;
                    _MoveUp.Enabled = true;
                }
                else
                {
                    _MoveDown.Enabled = _MoveUp.Enabled = true;
                }
            else
            {
                _MoveDown.Enabled = _MoveUp.Enabled = false;
            }
        }

        /// <summary>
        /// ���򷽷�
        /// </summary>
        protected void CallOrder()
        {
            for (int i = 0; i < BaseDataGrid.Rows.Count - 1; i++)
            {
                BaseDataGrid[0, i].Value = i + 1;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.DisplayInfo
{
    /// <summary>
    /// 启动、潜动检定数据
    /// </summary>
    public partial class DisplayQiQianDong : Base
    {
        public DisplayQiQianDong():base()
        {
            InitializeComponent();
        }
        public DisplayQiQianDong(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
            : base(MeterInfo, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            //Dgv_PrjData.TopLeftHeaderCell.Value = "项目名称";
            //if (allowedit)          //是否启用编辑
            //{
            //    Dgv_PrjData.ReadOnly = false;
            //}
            this.Refresh();
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {
            base.SetData(MeterInfo, allowedit);
            if (allowedit)          //是否启用编辑
            {
                Dgv_PrjData.ReadOnly = false;
            }
            this.Refresh(MeterInfo);
        }

        /// <summary>
        /// 刷新表格数据
        /// </summary>
        public void Refresh(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo)
        {
            if (MeterInfo == null)
                return;
            Dgv_PrjData.Rows.Clear();

            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterQdQid> _QdQids = MeterInfo.MeterQdQids;
            foreach (string _Key in _QdQids.Keys)
            {
                if (_Key.Length > 3 && (_Key.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString()
                                      ||
                                     _Key.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString()))           //只有大于3才可能是小项目,并且当中要包含启动ID和潜动ID
                {
                    int RowIndex = Dgv_PrjData.Rows.Add();
                    Dgv_PrjData.Rows[RowIndex].HeaderCell.Value = _QdQids[_Key].Mqd_chrProjectName;    //项目名称
                    Dgv_PrjData.Rows[RowIndex].Tag = _Key;          //将ID保存到Tag
                    Dgv_PrjData.Rows[RowIndex].Cells[0].Value = MeterInfo.ToString();
                    Dgv_PrjData.Rows[RowIndex].Cells[1].Value = _QdQids[_Key].Mqd_chrDL;      //电流
                    Dgv_PrjData.Rows[RowIndex].Cells[2].Value = _QdQids[_Key].AVR_STANDARD_TIME;             //试验时间
                    Dgv_PrjData.Rows[RowIndex].Cells[3].Value = _QdQids[_Key].Mqd_chrTime;
                    Dgv_PrjData.Rows[RowIndex].Cells[4].Value = _QdQids[_Key].Mqd_chrJL;           //结论
                }
            }
            
        }
        public DisplayQiQianDong(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
            : base(MeterGroup, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            Dgv_PrjData.TopLeftHeaderCell.Value = "项目名称";
            if (allowedit)          //是否启用编辑
            {
                Dgv_PrjData.ReadOnly = false;
            }
            this.Refresh();    
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            base.SetData(MeterGroup, allowedit);
            if (allowedit)          //是否启用编辑
            {
                Dgv_PrjData.ReadOnly = false;
            }
            this.Refresh(MeterGroup);   
        }

        /// <summary>
        /// 刷新表格数据
        /// </summary>
        public void Refresh(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            //string strData;
            if (base._MeterGroup == null)
                return;
            Dgv_PrjData.Rows.Clear();
            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;
                Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterQdQid> _QdQids = MeterGroup.MeterGroup[i].MeterQdQids;
                foreach (string _Key in _QdQids.Keys)
                {
                    if (_Key.Length > 3 && (_Key.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验).ToString()
                                          ||
                                         _Key.Substring(0, 3) == ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString()))           //只有大于3才可能是小项目,并且当中要包含启动ID和潜动ID
                    {
                        int RowIndex = Dgv_PrjData.Rows.Add();
                        
                        Dgv_PrjData.Rows[RowIndex].HeaderCell.Value = _QdQids[_Key].Mqd_chrProjectName;    //项目名称
                        Dgv_PrjData.Rows[RowIndex].Tag = _Key;          //将ID保存到Tag
                        Dgv_PrjData.Rows[RowIndex].Cells[0].Value = MeterGroup.MeterGroup[i].ToString();
                        
                        Dgv_PrjData.Rows[RowIndex].Cells[1].Value = _QdQids[_Key].Mqd_chrDL;      //电流
                        Dgv_PrjData.Rows[RowIndex].Cells[2].Value = _QdQids[_Key].AVR_STANDARD_TIME;             //试验时间
                        Dgv_PrjData.Rows[RowIndex].Cells[3].Value = _QdQids[_Key].Mqd_chrTime;
                        Dgv_PrjData.Rows[RowIndex].Cells[4].Value = _QdQids[_Key].Mqd_chrJL;           //结论
                    }
                }
            }
            SpanRow(0, Dgv_PrjData.Rows.Count, 0);
        }
        /// <summary>
        /// 单元格单击，如果在可修改状态，则设置可编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_PrjData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (base.AllowEdit)
            {
                Dgv_PrjData.BeginEdit(false);
                Dgv_PrjData.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = Dgv_PrjData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }
        /// <summary>
        /// 数据修改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_PrjData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (base.AllowEdit)
            {
                string _Key=Dgv_PrjData.Rows[e.RowIndex].Tag.ToString();
                DataGridViewCell Cell= Dgv_PrjData.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (e.ColumnIndex == 1)      //电流被修改
                {
                    if (!CLDC_DataCore.Function.Number.IsNumeric(Cell.Value.ToString()))
                    {
                        MessageBox.Show("填写的电流值必须是一个数字，修改被撤销...", "修改错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cell.Value = Cell.Tag;
                    }
                    else
                    {
                        //_MeterGroup.MeterGroup[e.RowIndex].MeterResults[_Key].Mr_Current = Cell.Value.ToString();
                    }
                }
                if (e.ColumnIndex == 2)     //时间被修改
                {
                    if (!CLDC_DataCore.Function.Number.IsNumeric(Cell.Value.ToString()))
                    {
                        MessageBox.Show("填写的时间值必须是一个数字，修改被撤销...", "修改错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cell.Value = Cell.Tag;
                    }
                    //else
                        //_MeterGroup.MeterGroup[e.RowIndex].MeterResults[_Key].Mr_Time = Cell.Value.ToString();
                }
                if (e.ColumnIndex == 3)          //修改结论
                {
                    if (Cell.Value.ToString() != CLDC_DataCore.Const.Variable.CTG_HeGe && Cell.Value.ToString() != CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                    {
                        MessageBox.Show("填写的结论必须为“合格”或“不合格”，修改被撤销...", "修改错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cell.Value = Cell.Tag;
                    }
                    else
                        _MeterGroup.MeterGroup[e.RowIndex].MeterResults[_Key].Mr_chrRstValue = Cell.Value.ToString();
                }
            }
            Dgv_PrjData.EndEdit();
        }

        private void SpanRow(int startRowIndex, int rowCount, int col)
        {
            if (rowCount == 0) return;

            int rowSpan = 1;

            int intstart = startRowIndex;

            for (int i = startRowIndex + 1; i < rowCount + intstart; i++)
            {
                if (Dgv_PrjData[col, startRowIndex].Value == null) continue;
                if (Dgv_PrjData[col, i].Value.ToString() == Dgv_PrjData[col, startRowIndex].Value.ToString())
                {
                    rowSpan++;
                }
                else
                {
                    ((CLZDataGridView.DataGridViewTextBoxCellEx)Dgv_PrjData[col, startRowIndex]).RowSpan = rowSpan;
                    if (col < 2)
                    {
                        SpanRow(startRowIndex, rowSpan, col + 1);
                    }
                    startRowIndex = i;
                    rowSpan = 1;
                }

            }

            ((CLZDataGridView.DataGridViewTextBoxCellEx)Dgv_PrjData[col, startRowIndex]).RowSpan = rowSpan;

            if (col < 2)
            {
                SpanRow(startRowIndex, rowSpan, col + 1);
            }
        }

    }
}

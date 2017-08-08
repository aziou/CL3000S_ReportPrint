using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UI.DisplayInfo
{
    /// <summary>
    /// 启动、潜动检定数据
    /// </summary>
    public partial class CheckQiQianDong : Base
    {
        public CheckQiQianDong():base()
        {
            InitializeComponent();
        }

        public CheckQiQianDong(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit):base(meterInfo,allowedit)
        {
            InitializeComponent();
            if (Comm.Function.Common.IsVSDevenv()) return;

            Dgv_PrjData.TopLeftHeaderCell.Value = "项目名称";
            if (allowedit)          //是否启用编辑
            {
                Dgv_PrjData.ReadOnly = false;
            }
            this.Refresh();    
        }

        public override void SetData(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        {
            base.SetData(meterInfo, allowedit);
            if (allowedit)          //是否启用编辑
            {
                Dgv_PrjData.ReadOnly = false;
            }
            this.Refresh();   
        }

        /// <summary>
        /// 刷新表格数据
        /// </summary>
        public new void Refresh()
        {
            if (base.MeterInfo.MeterResults.Count == 0)
                return;
            Dgv_PrjData.Rows.Clear();
            Dictionary<string,Comm.Model.DnbModel.DnbInfo.MeterResult> _Results = base.MeterInfo.MeterResults;
            foreach (string _Key in _Results.Keys)
            { 
                if(_Key.Length>3 && (_Key.Substring(0,3)==((int)Comm.Enum.Cus_MeterResultPriID.启动试验).ToString()
                                      ||  
                                     _Key.Substring(0,3)==((int)Comm.Enum.Cus_MeterResultPriID.潜动试验).ToString()))           //只有大于3才可能是小项目,并且当中要包含启动ID和潜动ID
                {
                    int RowIndex=Dgv_PrjData.Rows.Add();
                    Dgv_PrjData.Rows[RowIndex].HeaderCell.Value = _Results[_Key].Mr_PrjName;    //项目名称
                    Dgv_PrjData.Rows[RowIndex].Tag = _Key;          //将ID保存到Tag
                    Dgv_PrjData.Rows[RowIndex].Cells[0].Value = _Results[_Key].Mr_Current;      //电流
                    Dgv_PrjData.Rows[RowIndex].Cells[1].Value = _Results[_Key].Mr_Time;             //试验时间
                    Dgv_PrjData.Rows[RowIndex].Cells[2].Value = _Results[_Key].Mr_Result;           //结论
                }
            }
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
                if (e.ColumnIndex == 0)      //电流被修改
                {
                    if (!Comm.Function.Number.IsNumeric(Cell.Value.ToString()))
                    {
                        MessageBox.Show("填写的电流值必须是一个数字，修改被撤销...", "修改错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cell.Value = Cell.Tag;
                    }
                    else
                        MeterInfo.MeterResults[_Key].Mr_Current = Cell.Value.ToString();
                }
                if (e.ColumnIndex == 1)     //时间被修改
                {
                    if (!Comm.Function.Number.IsNumeric(Cell.Value.ToString()))
                    {
                        MessageBox.Show("填写的时间值必须是一个数字，修改被撤销...", "修改错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cell.Value = Cell.Tag;
                    }
                    else
                        MeterInfo.MeterResults[_Key].Mr_Time = Cell.Value.ToString();
                }
                if (e.ColumnIndex == 2)          //修改结论
                {
                    if (Cell.Value.ToString() != Comm.Const.Variable.CTG_HeGe && Cell.Value.ToString() != Comm.Const.Variable.CTG_BuHeGe)
                    {
                        MessageBox.Show("填写的结论必须为“合格”或“不合格”，修改被撤销...", "修改错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cell.Value = Cell.Tag;
                    }
                    else
                        MeterInfo.MeterResults[_Key].Mr_Result = Cell.Value.ToString();
                }
            }
            Dgv_PrjData.EndEdit();
        }

        

    }
}

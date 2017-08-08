using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.CLZDataGridView
{
    public class DataGridViewTextBoxCellEx : DataGridViewTextBoxCell, ISpannedCell
    {


        #region ISpannedCell 成员

        private int columnSpan = 1;

        private int rowSpan = 1;

        private DataGridViewTextBoxCellEx ownerCell;

        /// <summary>
        /// 列合并
        /// </summary>
        public int ColumnSpan
        {
            get { return columnSpan; }
            set
            {
                if (DataGridView == null || ownerCell != null) return;
                if (value < 1 || ColumnIndex + value > DataGridView.ColumnCount)
                {
                    throw new System.ArgumentOutOfRangeException("value");
                }
                if (columnSpan != value)
                {
                    SetSpan(value, rowSpan);
                }
            }
        }

        /// <summary>
        /// 行合并
        /// </summary>
        public int RowSpan
        {
            get { return rowSpan; }
            set
            {
                if (DataGridView == null || ownerCell != null) return;
                if (value < 1 || RowIndex + value > DataGridView.RowCount)
                {
                    throw new System.ArgumentOutOfRangeException("value");
                }
                if (rowSpan != value)
                {
                    SetSpan(columnSpan, value);
                }
            }
        }

        public DataGridViewCell OwnerCell
        {
            get { return ownerCell; }
            private set { ownerCell = value as DataGridViewTextBoxCellEx; }
        }

        #endregion

        #region ----------OnPainting------------
        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (ownerCell != null && ownerCell.DataGridView == null) ownerCell = null;

            if (DataGridView == null || (ownerCell == null && columnSpan == 1 && rowSpan == 1))
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
                return;
            }

            var _ownerCell = this;

            var columnIndex = ColumnIndex;
            var _columnSpan = columnSpan;
            var _rowSpan = rowSpan;
            if (ownerCell != null)
            {
                _ownerCell = ownerCell;
                columnIndex = ownerCell.ColumnIndex;
                rowIndex = ownerCell.RowIndex;
                _columnSpan = ownerCell.ColumnSpan;
                _rowSpan = ownerCell.RowSpan;
                value = ownerCell.GetValue(rowIndex);
                errorText = ownerCell.GetErrorText(rowIndex);
                cellState = ownerCell.State;
                cellStyle = ownerCell.GetInheritedStyle(null, rowIndex, true);
                formattedValue = ownerCell.GetFormattedValue(value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Display);
            }

            if (CellsRegionContainsSelectedCell(columnIndex, rowIndex, _columnSpan, _rowSpan))
            {
                cellState |= DataGridViewElementStates.Selected;
            }

            var cellBounds2 = DataGridViewCellExHelper.GetSpannedCellBoundsFromChildCellBounds
                                                (this
                                                , cellBounds
                                                , DataGridViewCellExHelper.SingleVerticalBorderAdded(DataGridView)
                                                , DataGridViewCellExHelper.SingleHorizontalBorderAdded(DataGridView));
            clipBounds = DataGridViewCellExHelper.GetSpannedCellClipBounds
                                                (_ownerCell
                                                , cellBounds2
                                                , DataGridViewCellExHelper.SingleVerticalBorderAdded(DataGridView)
                                                , DataGridViewCellExHelper.SingleHorizontalBorderAdded(DataGridView));

            using (var g = DataGridView.CreateGraphics())
            {
                g.SetClip(clipBounds);

                advancedBorderStyle = DataGridViewCellExHelper.AdjustCellBorderStyle(_ownerCell);

                _ownerCell.NativePaint(g, clipBounds, cellBounds2, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts & ~DataGridViewPaintParts.Border);

                if ((paintParts & DataGridViewPaintParts.Border) != DataGridViewPaintParts.None)          //这表示是合并单元格的最左边的单元格，边框不能画四边，只能画左上2边
                {
                    var leftTopCell = _ownerCell;
                    var advancedBorderStyle2 = new DataGridViewAdvancedBorderStyle
                    {
                        Left = advancedBorderStyle.Left,
                        Top = advancedBorderStyle.Top,
                        Right = DataGridViewAdvancedCellBorderStyle.None,
                        Bottom = DataGridViewAdvancedCellBorderStyle.None
                    };
                    leftTopCell.PaintBorder(g, clipBounds, cellBounds2, cellStyle, advancedBorderStyle2);

                    var rightBottomCell = DataGridView[columnIndex + _columnSpan - 1, rowIndex + _rowSpan - 1] as DataGridViewTextBoxCellEx ?? this;

                    var advancedBorderStyle3 = new DataGridViewAdvancedBorderStyle
                    {
                        Left = DataGridViewAdvancedCellBorderStyle.None,
                        Top = DataGridViewAdvancedCellBorderStyle.None,
                        Right = advancedBorderStyle.Right,
                        Bottom = advancedBorderStyle.Bottom
                    };
                    rightBottomCell.PaintBorder(g, clipBounds, cellBounds2, cellStyle, advancedBorderStyle3);
                }
            }

        }

        private void NativePaint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }
        #endregion


        #region --------------------------重载函数-------------------------

        /// <summary>
        /// 是否只读
        /// </summary>
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;
                if (ownerCell == null && (columnSpan > 1 || rowSpan > 1) && DataGridView != null)
                {
                    foreach (var col in DataGridViewCellExHelper.Range(ColumnIndex, columnSpan))
                    {
                        foreach (var row in DataGridViewCellExHelper.Range(RowIndex, rowSpan))
                        {
                            DataGridView[col, row].ReadOnly = value;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        protected override object GetValue(int rowIndex)
        {
            if (ownerCell != null)
            {
                return ownerCell.GetValue(ownerCell.RowIndex);
            }
            return base.GetValue(rowIndex);
        }
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override bool SetValue(int rowIndex, object value)
        {
            if (ownerCell != null)
            {
                return ownerCell.SetValue(ownerCell.RowIndex, value);
            }
            return base.SetValue(rowIndex, value);
        }

        protected override void OnDataGridViewChanged()
        {
            base.OnDataGridViewChanged();
            if (DataGridView == null)
            {
                columnSpan = 1;
                rowSpan = 1;
            }
        }

        public override System.Drawing.Rectangle PositionEditingPanel(System.Drawing.Rectangle cellBounds, System.Drawing.Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            if (ownerCell == null && columnSpan == 1 && rowSpan == 1)
            {
                return base.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
            }

            var _ownerCell = this;

            if (ownerCell != null)
            {
                var rowIndex = ownerCell.RowIndex;
                cellStyle = ownerCell.GetInheritedStyle(null, rowIndex, true);
                ownerCell.GetFormattedValue(ownerCell.Value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
                var editingControl = DataGridView.EditingControl as IDataGridViewEditingControl;
                if (editingControl != null)
                {
                    editingControl.ApplyCellStyleToEditingControl(cellStyle);
                    var editingPanel = DataGridView.EditingControl.Parent;
                    if (editingPanel != null) editingPanel.BackColor = cellStyle.BackColor;
                }
                _ownerCell = ownerCell;
            }
            cellBounds = DataGridViewCellExHelper.GetSpannedCellBoundsFromChildCellBounds(this, cellBounds, singleVerticalBorderAdded, singleHorizontalBorderAdded);
            cellClip = DataGridViewCellExHelper.GetSpannedCellClipBounds(_ownerCell, cellBounds, singleVerticalBorderAdded, singleHorizontalBorderAdded);

            return base.PositionEditingPanel(cellBounds
                                                            , cellClip
                                                            , cellStyle
                                                            , singleVerticalBorderAdded
                                                            , singleHorizontalBorderAdded
                                                            , DataGridViewCellExHelper.InFirstDisplayedColumn(_ownerCell)
                                                            , DataGridViewCellExHelper.InFirstDisplayedRow(_ownerCell));


        }


        protected override System.Drawing.Size GetPreferredSize(System.Drawing.Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, System.Drawing.Size constraintSize)
        {
            if (ownerCell != null) return new System.Drawing.Size(0, 0);

            var size = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);

            var grid = DataGridView;

            int rangwidth = 0;

            foreach (int w in DataGridViewCellExHelper.Range(ColumnIndex + 1, columnSpan - 1))
            {
                rangwidth += grid.Columns[w].Width;
            }

            var width = size.Width - rangwidth;

            int rangheight = 0;

            foreach (int w in DataGridViewCellExHelper.Range(RowIndex + 1, RowSpan - 1))
            {
                rangheight += grid.Rows[w].Height;
            }

            var height = size.Height - rangheight;

            return new System.Drawing.Size(width, height);
        }

        protected override System.Drawing.Rectangle BorderWidths(DataGridViewAdvancedBorderStyle advancedBorderStyle)
        {
            if (ownerCell == null && columnSpan == 1 && rowSpan == 1)
            {
                return base.BorderWidths(advancedBorderStyle);
            }
            if (ownerCell != null)
            {
                return ownerCell.BorderWidths(advancedBorderStyle);
            }
            var leftTop = base.BorderWidths(advancedBorderStyle);
            var rightBottomCell = DataGridView[ColumnIndex + columnSpan - 1, RowIndex + rowSpan - 1] as DataGridViewTextBoxCellEx;

            var rightBottom = rightBottomCell != null ? rightBottomCell.NativeBorderWidths(advancedBorderStyle) : leftTop;

            return new System.Drawing.Rectangle(leftTop.X, leftTop.Y, rightBottom.Width, rightBottom.Height);


        }

        #endregion

        #region --------------------------合并-------------------------

        private void SetSpan(int columnspan, int rowspan)
        {
            int prevColumnSpan = columnSpan;
            int prevRowSpan = rowSpan;

            columnSpan = columnspan;

            rowSpan = rowspan;

            if (DataGridView != null)
            {
                //清除之前的合并
                foreach (int rowIndex in DataGridViewCellExHelper.Range(RowIndex, prevRowSpan))
                {
                    foreach (int colIndex in DataGridViewCellExHelper.Range(ColumnIndex, prevColumnSpan))
                    {
                        var cell = DataGridView[colIndex, rowIndex] as DataGridViewTextBoxCellEx;
                        if (cell != null) cell.OwnerCell = null;
                    }
                }
                //设置新的合并
                foreach (int row in DataGridViewCellExHelper.Range(RowIndex, rowSpan))
                {
                    foreach (int col in DataGridViewCellExHelper.Range(ColumnIndex, columnSpan))
                    {
                        var cell = DataGridView[col, row] as DataGridViewTextBoxCellEx;
                        if (cell != null && cell != this)
                        {
                            if (cell.ColumnSpan > 1) cell.ColumnSpan = 1;
                            if (cell.RowSpan > 1) cell.RowSpan = 1;
                            cell.OwnerCell = this;
                        }
                    }
                }
                OwnerCell = null;
                DataGridView.Invalidate();
            }
        }

        #endregion


        private System.Drawing.Rectangle NativeBorderWidths(DataGridViewAdvancedBorderStyle advancedBorderStyle)
        {
            return base.BorderWidths(advancedBorderStyle);
        }

        private bool CellsRegionContainsSelectedCell(int columnIndex, int rowIndex, int columnSpan, int rowSpan)
        {
            if (DataGridView == null)
                return false;

            foreach (int col in DataGridViewCellExHelper.Range(columnIndex, columnSpan))
            {
                foreach (int row in DataGridViewCellExHelper.Range(rowIndex, rowSpan))
                {
                    if (DataGridView[col, row].Selected) return true;
                }
            }
            return false;
        }

        #region ---------------------静态函数----------------------



        #endregion


    }
}

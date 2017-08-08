using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.CLZDataGridView
{
    static class DataGridViewCellExHelper
    {
        /// <summary>
        /// 生成一个序列
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        internal static System.Collections.Generic.IEnumerable<int> Range(int start, int count)
        {
            for (int i = start; i < count + start; i++)
            {
                yield return i;
            }

        }

        internal static System.Drawing.Rectangle GetSpannedCellClipBounds<TCell>
            (TCell ownerCell, System.Drawing.Rectangle cellBounds, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded)
            where TCell : DataGridViewCell, ISpannedCell
        {
            var datagridView = ownerCell.DataGridView;

            var clipBounds = cellBounds;

            foreach (var colIndex in Range(ownerCell.ColumnIndex, ownerCell.ColumnSpan))
            {
                var column = datagridView.Columns[colIndex];

                if (!column.Visible) continue;

                if (column.Frozen || colIndex > datagridView.FirstDisplayedScrollingColumnIndex)
                {
                    break;
                }
                if (colIndex == datagridView.FirstDisplayedScrollingColumnIndex)
                {
                    clipBounds.Width -= datagridView.FirstDisplayedScrollingColumnHiddenWidth;
                    if (datagridView.RightToLeft != RightToLeft.Yes)
                    {
                        clipBounds.X += datagridView.FirstDisplayedScrollingColumnHiddenWidth;
                    }
                    break;
                }
                clipBounds.Width -= column.Width;
                if (datagridView.RightToLeft != RightToLeft.Yes)
                {
                    clipBounds.X += column.Width;
                }
            }
            foreach (var rowIndex in Range(ownerCell.RowIndex, ownerCell.RowSpan))
            {
                var row = datagridView.Rows[rowIndex];

                if (!row.Visible) continue;

                if (row.Frozen || rowIndex >= datagridView.FirstDisplayedScrollingRowIndex)
                {
                    break;
                }

                clipBounds.Y += row.Height;
                clipBounds.Height += row.Height;
            }

            if (datagridView.BorderStyle != BorderStyle.None)
            {
                var clientRectangle = datagridView.ClientRectangle;
                clientRectangle.Width--;
                clientRectangle.Height--;
                if (datagridView.RightToLeft == RightToLeft.Yes)
                {
                    clientRectangle.X++;
                    clientRectangle.Y++;
                }
                clipBounds.Intersect(clientRectangle);
            }
            return clipBounds;
        }


        internal static System.Drawing.Rectangle GetSpannedCellBoundsFromChildCellBounds<TCell>
        (TCell childCell, System.Drawing.Rectangle childCellBounds, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded)
        where TCell : DataGridViewCell, ISpannedCell
        {
            var datagridView = childCell.DataGridView;

            var ownerCell = childCell.OwnerCell as TCell ?? childCell;

            var spanedCellBounds = childCellBounds;

            var firstVisibleColumnIndex = 0;

            foreach (var m in Range(ownerCell.ColumnIndex, ownerCell.ColumnSpan))
            {
                if (datagridView.Columns[m].Visible)
                {
                    firstVisibleColumnIndex = m;
                    break;
                }
            }

            if (datagridView.Columns[firstVisibleColumnIndex].Frozen)
            {
                spanedCellBounds.X = datagridView.GetColumnDisplayRectangle(firstVisibleColumnIndex, false).X;
            }
            else
            {
                var dx = 0;

                foreach (var m in Range(firstVisibleColumnIndex, childCell.ColumnIndex - firstVisibleColumnIndex))
                {
                    if (datagridView.Columns[m].Visible)
                    {
                        dx += datagridView.Columns[m].Width;
                    }
                }
                spanedCellBounds.X = datagridView.RightToLeft == RightToLeft.Yes ? spanedCellBounds.X + dx : spanedCellBounds.X - dx;
            }

            var firstVisibleRowIndex = 0;

            foreach (var m in Range(ownerCell.RowIndex, ownerCell.RowSpan))
            {
                if (datagridView.Rows[m].Visible)
                {
                    firstVisibleRowIndex = m;
                    break;
                }
            }
            if (datagridView.Rows[firstVisibleRowIndex].Frozen)
            {
                spanedCellBounds.Y = datagridView.GetRowDisplayRectangle(firstVisibleRowIndex, false).Y;
            }
            else
            {
                foreach (var m in Range(firstVisibleRowIndex, childCell.RowIndex - firstVisibleRowIndex))
                {
                    if (datagridView.Rows[m].Visible)
                    {
                        spanedCellBounds.Y -= datagridView.Rows[m].Height;
                    }
                }
            }

            var spannedCellWidth = 0;

            foreach (var m in Range(ownerCell.ColumnIndex, ownerCell.ColumnSpan))
            {
                if (datagridView.Columns[m].Visible)
                {
                    spannedCellWidth += datagridView.Columns[m].Width;
                }
            }

            if (datagridView.RightToLeft == RightToLeft.Yes)
            {
                spanedCellBounds.X = spanedCellBounds.Right - spannedCellWidth;
            }
            spanedCellBounds.Width = spannedCellWidth;

            int sHeight = 0;

            foreach (var m in Range(ownerCell.RowIndex, ownerCell.RowSpan))
            {
                if (datagridView.Rows[m].Visible)
                {
                    sHeight += datagridView.Rows[m].Height;
                }
            }
            spanedCellBounds.Height = sHeight;

            if (singleVerticalBorderAdded && InFirstDisplayedColumn(ownerCell))
            {
                spanedCellBounds.Width++;
                if (datagridView.RightToLeft == RightToLeft.Yes)
                {
                    if (childCell.ColumnIndex != datagridView.FirstDisplayedScrollingColumnIndex)
                    {
                        spanedCellBounds.X--;
                    }
                }
                else
                {
                    if (childCell.ColumnIndex == datagridView.FirstDisplayedScrollingColumnIndex)
                    {
                        spanedCellBounds.X--;
                    }
                }
            }
            if (singleHorizontalBorderAdded && InFirstDisplayedRow(ownerCell))
            {
                spanedCellBounds.Height++;
                if (childCell.RowIndex != datagridView.FirstDisplayedScrollingRowIndex)
                {
                    spanedCellBounds.Y--;
                }
            }

            return spanedCellBounds;
        }

        internal static bool InFirstDisplayedColumn<TCell>(TCell cell)
        where TCell : DataGridViewCell, ISpannedCell
        {
            var datagridView = cell.DataGridView;
            return datagridView.FirstDisplayedScrollingColumnIndex >= cell.ColumnIndex && datagridView.FirstDisplayedScrollingColumnIndex < cell.ColumnIndex + cell.ColumnSpan;
        }

        internal static bool InFirstDisplayedRow<TCell>(TCell cell)
        where TCell : DataGridViewCell, ISpannedCell
        {
            var datagridView = cell.DataGridView;
            return datagridView.FirstDisplayedScrollingRowIndex >= cell.RowIndex && datagridView.FirstDisplayedScrollingRowIndex < cell.RowIndex + cell.RowSpan;
        }

        internal static DataGridViewAdvancedBorderStyle AdjustCellBorderStyle<TCell>(TCell cell)
        where TCell : DataGridViewCell, ISpannedCell
        {
            var dataGridViewAdvancedBorderStylePlaceHolder = new DataGridViewAdvancedBorderStyle();
            var dataGridView = cell.DataGridView;
            return cell.AdjustCellBorderStyle(dataGridView.AdvancedCellBorderStyle, dataGridViewAdvancedBorderStylePlaceHolder, SingleVerticalBorderAdded(dataGridView), SingleHorizontalBorderAdded(dataGridView), InFirstDisplayedColumn(cell), InFirstDisplayedRow(cell));
        }

        internal static bool SingleHorizontalBorderAdded(DataGridView dataGridView)
        {
            return !dataGridView.ColumnHeadersVisible && (dataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single || dataGridView.CellBorderStyle == DataGridViewCellBorderStyle.SingleHorizontal);
        }

        internal static bool SingleVerticalBorderAdded(DataGridView dataGridView)
        {
            return !dataGridView.RowHeadersVisible && (dataGridView.AdvancedCellBorderStyle.All == DataGridViewAdvancedCellBorderStyle.Single || dataGridView.CellBorderStyle == DataGridViewCellBorderStyle.SingleVertical);
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
namespace CLDC_MeterUI.CLZDataGridView
{
    /// <summary>
    /// 单元格合并接口
    /// </summary>
    public interface ISpannedCell
    {
        int ColumnSpan { get; }
        int RowSpan { get; }
        DataGridViewCell OwnerCell { get; }

    }
}

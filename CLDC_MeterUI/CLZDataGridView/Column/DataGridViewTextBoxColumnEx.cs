using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
namespace CLDC_MeterUI.CLZDataGridView
{
    public class DataGridViewTextBoxColumnEx : DataGridViewColumn
    {
        public DataGridViewTextBoxColumnEx()
            : base(new DataGridViewTextBoxCellEx())
        {
        }


    }
}

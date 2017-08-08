using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.DgnDataView
{
    public partial class ViewGpsTime : UserControl
    {
        private const string Key = "01301";                     //时间误差在集合中存储的关键字 

        public ViewGpsTime()
        {
            InitializeComponent();
        }
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            if (Data_View.Rows.Count != MeterGroup.Count)
            {
                Data_View.Rows.Clear();
                for (int i = 0; i < MeterGroup.Count; i++)
                {
                    int RowIndex = Data_View.Rows.Add();
                    if ((RowIndex + 1) % 2 == 0)
                    {
                        Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal; 
                    }
                    Data_View.Rows[RowIndex].HeaderCell.Value = MeterGroup[i].ToString();
                }
                Data_View.Refresh();
            }

            for (int i = 0; i < MeterGroup.Count; i++)
            {
                if (!MeterGroup[i].MeterDgns.ContainsKey(Key))           //如果多功能集合中不存在改关键字则不处理
                    continue;
                string[] _Values = MeterGroup[i].MeterDgns[Key].Md_chrValue.Split('|');     //分割数据，被检表时间|GPS时间|时间差
                if (_Values.Length != 3) continue;          //如果长度不为3，则表示数据有错，不处理
                for (int j = 0; j < _Values.Length; j++)
                {
                    Data_View.Rows[i].Cells[j].Value = _Values[j];
                }
            }

        }

    }
}

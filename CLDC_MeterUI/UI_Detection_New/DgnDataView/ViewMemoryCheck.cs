using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.DgnDataView
{
    public partial class ViewMemoryCheck : UserControl
    {
        private const string Key = "01909";                     //存储器检查电量在集合中存储的关键字        

        public ViewMemoryCheck()
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
                if (!MeterGroup[i].MeterDgns.ContainsKey(Key) || MeterGroup[i].MeterDgns[Key].Md_chrValue==null)           //如果多功能集合中不存在该关键字，则不处理
                    continue;
                string[] _Values = MeterGroup[i].MeterDgns[Key].Md_chrValue.Split('|');

                if (_Values.Length != 5) continue;          //如果数据长度不等于5，则表示数据有问题，跳过不处理
                for (int j = 0; j < _Values.Length; j++)
                {
                    Data_View.Rows[i].Cells[j].Value = _Values[j];
                }
            }   
        
        }
    }
}

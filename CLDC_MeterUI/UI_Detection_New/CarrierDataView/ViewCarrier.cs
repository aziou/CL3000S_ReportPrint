using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.CarrierView
{
    public partial class ViewCarrier : UserControl
    {        

        public ViewCarrier()
        {
            InitializeComponent();
        }
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup,string strKey)
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
                if (!MeterGroup[i].YaoJianYn)
                    continue;
                if (!MeterGroup[i].MeterCarrierDatas.ContainsKey(strKey))           //如果多功能集合中不存在改关键字则不处理
                    continue;
                
                Data_View.Rows[i].Cells[0].Value = MeterGroup[i].MeterCarrierDatas[strKey].Mce_PrjName;
                Data_View.Rows[i].Cells[1].Value = MeterGroup[i].MeterCarrierDatas[strKey].Mce_PrjValue;
                
            }

        }

    }
}

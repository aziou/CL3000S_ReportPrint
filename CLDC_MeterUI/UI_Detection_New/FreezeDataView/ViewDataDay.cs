using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.FreezeDataView
{
    public partial class ViewDataDay : UserControl
    {
        private const string Key = "00301";                     //月冻结电量在集合中存储的关键字
        public ViewDataDay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            if (Data_View.Columns.Count == 0 || MeterGroup.Count == 0) return;

            if (Data_View.Rows.Count != MeterGroup.Count)          //如果当前数据表单行数小于电能表表位数，则需要重新创建对应数据表单
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
                Data_View.Rows[i].Cells[0].Value = string.Format("第{0}表位", i + 1);
                if ((MeterGroup[i].MeterFreezes.ContainsKey(Key) && !string.IsNullOrEmpty(MeterGroup[i].MeterFreezes[Key].Md_chrValue)))
                {
                    string[] _Values = MeterGroup[i].MeterFreezes[Key].Md_chrValue.Split('|');
                    if (_Values.Length != 3) continue;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _Values.Length; j++)
                    {
                        Data_View.Rows[i].Cells[j + 1].Value = _Values[j];
                    }
                }
            }
        }
        /// <summary>
        /// 刷新单个电能表信息
        /// </summary>
        /// <param name="MeterInfo">电能表信息</param>
        /// <param name="allowedit"></param>
        public void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {

            Data_View.Rows.Clear();
            if (MeterInfo == null) return;
            if (MeterInfo.MeterFreezes.Count > 0)
            {
                int RowIndex = Data_View.Rows.Add();
                Data_View.Rows[RowIndex].Cells[0].Value = string.Format("{0}表位", MeterInfo._intBno + 1);
                Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;

                if ((MeterInfo.MeterFreezes.ContainsKey(Key) && !string.IsNullOrEmpty(MeterInfo.MeterFreezes[Key].Md_chrValue)))
                {
                    string[] _Values = MeterInfo.MeterFreezes[Key].Md_chrValue.Split('|');
                    if (_Values.Length != 3) return ;          //如果数据长度不等于3，则表示数据有问题，跳过不处理
                    for (int j = 0; j < _Values.Length; j++)
                    {
                        Data_View.Rows[RowIndex].Cells[j + 1].Value = _Values[j];
                    }
                }

            }
        }
    }
}

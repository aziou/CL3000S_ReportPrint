using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.FunctionDataView
{
    public partial class ViewShowFunction : UserControl
    {
        /// <summary>
        /// 显示功能项目ID
        /// </summary>
        private const string Key = "003";
              
        public ViewShowFunction()
        {
            InitializeComponent();

            Data_View.ReadOnly = true;
            int _ColIndex = Data_View.Columns.Add("Data_Z", "表位号");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].Width = 60;
            Data_View.Columns[_ColIndex].Resizable = DataGridViewTriState.False;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            _ColIndex = Data_View.Columns.Add("Data_Z", "显示功能数据内容");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            _ColIndex = Data_View.Columns.Add("Data_Z", "标准内容");
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        
        /// <summary>
        /// 刷新表单数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        public void SetData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            if (Data_View.Columns.Count == 0 || MeterGroup.Count == 0) return;

            if (Data_View.Rows.Count != MeterGroup.Count)           //如果当前数据表单行数小于电能表表位数，则需要重新创建对应数据表单
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
                    Data_View.Rows[i].Cells[0].Value = MeterGroup[i].ToString();
                }
                Data_View.Refresh();
            }


            for (int i = 0; i < MeterGroup.Count; i++)         //循环将电能表数据集合中的数据值插入到表格中
            {  
                string _Key = Key;
                if (!MeterGroup[i].YaoJianYn) continue;
                if (!MeterGroup[i].MeterShows.ContainsKey(_Key))
                {
                    Data_View.Rows[i].Cells[1].Value = "";    
                    continue;
                }
                if (MeterGroup[i].MeterShows[_Key].Msh_chrData == null || MeterGroup[i].MeterShows[_Key].Msh_chrData == string.Empty) continue;

                Data_View.Rows[i].Cells[1].Value = MeterGroup[i].MeterShows[_Key].Msh_chrData;
                Data_View.Rows[i].Cells[2].Value = MeterGroup[i].MeterShows[_Key].Msh_chrContent; 
            }
        }
		/// <summary>
        /// 刷新单个电能表信息
        /// </summary>
        /// <param name="MeterInfo">电能表信息</param>
        /// <param name="allowedit"></param>
        public void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {

            if (MeterInfo.MeterShows.Count == 0) return;
            Data_View.Rows.Clear();

            int RowIndex = Data_View.Rows.Add();
            if ((RowIndex + 1) % 2 == 0)
            {
                Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
            }
            else
            {
                Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
            }
            Data_View.Rows[RowIndex].Cells[0].Value = MeterInfo.ToString();
            string _Key = Key + "01";
                if (!MeterInfo.MeterShows.ContainsKey(_Key))
                {
                    Data_View.Rows[RowIndex].Cells[0].Value = "";    
                    return ;
                }
                if (MeterInfo.MeterShows[_Key].Msh_chrData == null || MeterInfo.MeterShows[_Key].Msh_chrData == string.Empty) return ;

                Data_View.Rows[RowIndex].Cells[0].Value = MeterInfo.MeterShows[_Key].Msh_chrData; 
            }
        

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CostControlDataView
{
    //用户开开户
    public partial class ViewUserCardInitUser : UserControl
    {
        private string Key = "025";

        public ViewUserCardInitUser()
        {
            InitializeComponent();
        }

        public ViewUserCardInitUser(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="CostControlPlan">用户卡开户</param>
        public ViewUserCardInitUser(CLDC_DataCore.Struct.StPlan_CostControl CostControlPlan)
        {
            InitializeComponent();
            if (CostControlPlan.CostControlPrjID != Key)        //如果项目ID不是切换本地模式则退出！！
                return;

            int _ColIndex = Data_View.Columns.Add("Data_Z", "测试密钥下开户失败");  //1
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            _ColIndex = Data_View.Columns.Add("Data_Z", "开户卡中表号与表内不一致开户失败"); //2
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            _ColIndex = Data_View.Columns.Add("Data_Z", "正式密钥下开户失败");   //3
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            _ColIndex = Data_View.Columns.Add("Data_Z", "剩余金额超囤积金额开户失败"); //4
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            _ColIndex = Data_View.Columns.Add("Data_Z", "囤积金额限值设为0发行开户卡开户成功");//5
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

            _ColIndex = Data_View.Columns.Add("Data_Z", "正式密钥下开户充值成功");//6
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;


            _ColIndex = Data_View.Columns.Add("Data_Z", "已开户下再次本地开户充值成功");//7
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;


            _ColIndex = Data_View.Columns.Add("Data_Z", "远程开户成功");//8
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Data_View.Columns[_ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Data_View.Columns[_ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;


            _ColIndex = Data_View.Columns.Add("Data_Z", "表购电次数大于1开户卡购电次数为0或1时仍可开户成功");//9
            Data_View.Columns[_ColIndex].Tag = 0;
            Data_View.Columns[_ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
                }
                Data_View.Refresh();
            }


            for (int i = 0; i < MeterGroup.Count; i++)         //循环将电能表数据集合中的时段投切数据值插入到表格中
            {
                for (int j = 1; j <= 9; j++)
                {
                    string _Key = Key + j.ToString("D2");
                    if (!MeterGroup[i].YaoJianYn) continue;
                    if (!MeterGroup[i].MeterCostControls.ContainsKey(_Key))
                    {
                        Data_View.Rows[i].Cells[j - 1].Value = "";
                        continue;
                    }
                    if (MeterGroup[i].MeterCostControls[_Key].Mfk_chrData == null || MeterGroup[i].MeterCostControls[_Key].Mfk_chrData == string.Empty) continue;

                    Data_View.Rows[i].Cells[j - 1].Value = MeterGroup[i].MeterCostControls[_Key].Mfk_chrData;
                }
            }
        }


    }
}

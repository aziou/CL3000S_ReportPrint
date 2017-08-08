using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.DgnDataView
{
    public partial class ViewDianLiang : UserControl
    {
        #region ---------------------------------------ID常量声明-----------------------------------------------
        /// <summary>
        /// 读电量项目ID
        /// </summary>
        private const string Key = "017";

        /// <summary>
        /// 正向有功
        /// </summary>
        private const string Key_Pz = "01";

        /// <summary>
        /// 反向有功
        /// </summary>
        private const string Key_Pf = "02";

        /// <summary>
        /// 正向无功
        /// </summary>
        private const string Key_Qz = "03";

        /// <summary>
        /// 反向无功
        /// </summary>
        private const string Key_Qf = "04";
        /// <summary>
        /// 一象限无功
        /// </summary>
        private const string Key_Q1 = "05";

        /// <summary>
        /// 二象限无功
        /// </summary>
        private const string Key_Q2 = "06";
        /// <summary>
        /// 三象线无功
        /// </summary>
        private const string Key_Q3 = "07";

        /// <summary>
        /// 四象限无功
        /// </summary>
        private const string Key_Q4 = "08";

        #endregion

        public ViewDianLiang()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="DgnPlan">多功能方案（读电量）</param>
        public ViewDianLiang(CLDC_DataCore.Struct.StPlan_Dgn DgnPlan)
        {
            InitializeComponent();

            if (DgnPlan.DgnPrjID != Key) return;          //如果当前传入的多功能方案项目不是读电量则退出

            string[] _Values = DgnPlan.PrjParm.Split('|');   //分割方案内容，类型|方向 (类型（总峰平谷尖）|方向（P+P-Q+Q-Q1Q2Q3Q4）)读为1，不读为0

            string[] _GLFX ={ "P+", "P-", "Q+", "Q-", "Q1", "Q2", "Q3", "Q4" };

            string[] _FeiLv ={ "(总)", "(峰)", "(平)", "(谷)", "(尖)" };

            string[] _FeiLvNo = { "0", "2", "3", "4", "1" };

            for (int i = 0; i < _Values[1].Length; i++)     //从方向开始循环
            {
                if (_Values[1].Substring(i, 1) != "1") continue;        //如果不等于1表示不读,也就不需要显示
                for (int j = 0; j < _Values[0].Length; j++)             //如果方向要显示则开始循环类型
                {
                    if (_Values[0].Substring(j, 1) != "1") continue;        //如果不等于1表示不读，也不需要显示

                    string _ColName = _GLFX[i] + _FeiLv[j];
                    string _Key = string.Format("{0:d}|{1:d}", i, _FeiLvNo[j]);         //方向|费率
                    int ColIndex = Data_View.Columns.Add(string.Format("Data_{0:D}{1:D}", i, j), _ColName);         //插入一个新列
                    Data_View.Columns[ColIndex].Tag = _Key;
                    Data_View.Columns[ColIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    Data_View.Columns[ColIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Data_View.Columns[ColIndex].ReadOnly = true;
                    Data_View.Columns[ColIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }

        }

        public delegate void SetDianLiangFormData(DataGridView dr, List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup);

        public void SetData(DataGridView dv, List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterGroup)
        {
            if (dv.InvokeRequired)
	        {
                SetDianLiangFormData setdata = new SetDianLiangFormData(SetData);
                dv.Invoke(setdata,new object[] { dv, MeterGroup });
	        }
            else
            {
                for (int i = 0; i < MeterGroup.Count; i++)
                {
                    if (!MeterGroup[i].YaoJianYn) continue;
                    for (int j = 0; j < Data_View.Columns.Count; j++)
                    {                        
                        Application.DoEvents();
                        Data_View.Rows[i].Cells[j].Value = "";
                        string[] _KeyValues = Data_View.Columns[j].Tag.ToString().Split('|'); //获取初始化时存入的临时KEY值     功率方向|费率
                        if (_KeyValues.Length != 2) continue;
                        string _Key = string.Format("{0}{1}", Key, getChildKey(_KeyValues[0]));

                        if (!MeterGroup[i].MeterDgns.ContainsKey(_Key)) continue;       //如果数据不存在，则跳过 
                        if (!string.IsNullOrEmpty(MeterGroup[i].MeterDgns[_Key].Md_chrValue))
                        {
                            string[] _Values = MeterGroup[i].MeterDgns[_Key].Md_chrValue.Split('|');        //分割数据 总|尖|峰|平|谷
                            if (_Values.Length == 5)            //等于5是由于包括总，尖，峰，平,谷
                                Data_View.Rows[i].Cells[j].Value = _Values[int.Parse(_KeyValues[1])];              //插入对应值，_Values[]数组里面的下标就是上面分割出来的关键字的数组的第二个元素
                        }
                    }
                }
            }
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

            SetData(Data_View, MeterGroup);

        }
        /// <summary>
        /// 获取对应Key值
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        private string getChildKey(string Key)
        {
            switch (Key)
            {
                case "0": return Key_Pz;
                case "1": return Key_Pf;
                case "2": return Key_Qz;
                case "3": return Key_Qf;
                case "4": return Key_Q1;
                case "5": return Key_Q2;
                case "6": return Key_Q3;
                case "7": return Key_Q4;
                default: return Key_Pz;
            }

        }
    }
}

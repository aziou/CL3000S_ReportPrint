using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl
{
    /// 阶梯电价对应方案的UI
    /// <summary>
    /// 阶梯电价对应方案的UI
    /// </summary>
    public partial class CostRatesTime : CostControlBase
    {
        #region 构造函数
        /// <summary>
        /// 阶梯电价检查
        /// </summary>
        public CostRatesTime()
        {
            InitializeComponent();
        }
        /// 根据参数加载阶梯电价控件
        /// <summary>
        /// 根据参数加载阶梯电价控件
        /// </summary>
        /// <param name="Item"></param>
        public CostRatesTime(CLDC_DataCore.Struct.StCostControlConfig Item)
            : base(Item)
        {
            InitializeComponent();
            base.SetPanel = Panel_Back;
        }
        #endregion 构造函数

        #region 控件的读写
        /// 是否要捡
        /// <summary>
        /// 是否要捡
        /// </summary>
        private bool isChecked
        {
            get { return Panel_Back.Checked; }
            set 
            {
                Panel_Back.Checked = value;
                if (Panel_Back.Checked)
                {
                    checkBoxReadTest.Enabled = true;
                    buttonAdd.Enabled = true;
                    buttonX1.Enabled = true;
                    checkBoxWriteTest.Enabled = true;
                }
                else
                {
                    checkBoxReadTest.Enabled = false;
                    checkBoxWrite1.Enabled = false;
                    buttonAdd.Enabled = false;
                    buttonX1.Enabled = false;
                    checkBoxWriteTest.Enabled = false;
                }
            }
        }
        /// 方案名称
        /// <summary>
        /// 方案名称
        /// </summary>
        private string planName
        {
            get { return Panel_Back.CaptionText; }
            set { Panel_Back.CaptionText = value; }
        }
        /// 有多少阶梯
        /// <summary>
        /// 有多少阶梯
        /// </summary>
        private int stepCount
        {
            get
            {
                int rowCount = dataGridViewX1.Rows.Count;
                if (rowCount > 1)
                {
                    return rowCount - 1;
                }
                else
                {
                    return 0;
                }
            }
            set 
            {
                labelStepCount.Text = string.Format("阶梯电价数量:{0}", stepCount);
            }
        }
        /// 是否进行读测试
        /// <summary>
        /// 是否进行读测试
        /// </summary>
        private bool readTest
        {
            get{ return checkBoxReadTest.Checked; }
            set { checkBoxReadTest.Checked = value; }
        }
        /// 是否进行写测试
        /// <summary>
        /// 是否进行写测试
        /// </summary>
        private bool writeTest 
        {
            get{ return checkBoxWriteTest.Checked; }
            set 
            { 
                checkBoxWriteTest.Checked = value;
                if (writeTest)
                {
                    checkBoxWrite1.Enabled = true;
                }
                else
                {
                    checkBoxWrite1.Enabled = false;
                }
            }
        }
        /// 配置信息相同时是否继续写操作
        /// <summary>
        /// 配置信息相同时是否继续写操作
        /// </summary>
        private bool enableWriteSame
        {
            get { return checkBoxWrite1.Checked; }
            set { checkBoxWrite1.Checked = value; }
        }
        /// 每个阶梯电价的相关信息，其中第一行必须为0KWH
        /// <summary>
        /// 每个阶梯电价的相关信息，其中第一行必须为0KWH
        /// </summary>
        private List<double> ratesTime
        {
            get
            {
                List<double> stepList = new List<double>();
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    stepList.Add(double.Parse(row.Cells[1].Value.ToString()));
                }
                return stepList;
            }
            set
            {
                List<double> stepList = value;
                while (dataGridViewX1.Rows.Count != stepList.Count)
                {
                    if (dataGridViewX1.Rows.Count > stepList.Count)
                    {
                        dataGridViewX1.Rows.RemoveAt(dataGridViewX1.Rows.Count - 1);
                    }
                    else
                    {
                        dataGridViewX1.Rows.Add();
                    }
                }
                for (int i = 0; i < stepList.Count;i++ )
                {
                    dataGridViewX1.Rows[i].Cells[0].Value=(i+1).ToString();
                    dataGridViewX1.Rows[i].Cells[1].Value = (string.Format("{0:0.00}", stepList[i])); 
                }
                
            }
        }
        #endregion 控件的读写

        #region 无用
        public override Color PanelBackColor
        {
            set
            {
                Panel_Back.BackColor = value;
            }
        }

        public override Color CaptionColor
        {
            set
            {
                Panel_Back.CaptionColorOne = value;
            }
        }

        public override Color CaptionColorTwo
        {
            set
            {
                Panel_Back.CaptionColorTwo = value;
            }
        }
        #endregion 无用

        #region 重写基本类中的方法
        /// 将控件的参数保存到多功能试验的参数里面
        /// <summary>
        /// 将控件的参数保存到多功能试验的参数里面
        /// </summary>
        public override CLDC_DataCore.Struct.StPlan_CostControl CostControlPlanPrj
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(isChecked.ToString() + "|");
                stringBuilder.Append(readTest.ToString() + "|");
                stringBuilder.Append(writeTest.ToString() + "|");
                stringBuilder.Append(enableWriteSame.ToString());
                foreach (double rateTime in ratesTime)
                {
                    stringBuilder.Append("|" + string.Format("{0:0.0000}", rateTime));
                }

                base.Parm = stringBuilder.ToString();
                base._IsCheck = isChecked;

                return base.CostControlPlanPrj;
            }
        }
        /// 根据多功能参数设置控件的内容
        /// <summary>
        /// 根据多功能参数设置控件的内容
        /// </summary>
        public override string Parm
        {
            get
            {
                return base.Parm;
            }
            set
            {
                try
                {
                    string[] _Arr = value.Split('|');
                    if (_Arr != null)
                    {
                        isChecked = bool.Parse(_Arr[0]);
                        readTest = bool.Parse(_Arr[1]);
                        writeTest = bool.Parse(_Arr[2]);
                        enableWriteSame = bool.Parse(_Arr[3]);
                        int itemCount = _Arr.Length - 4;
                        List<double> stepList = new List<double>();
                        for (int i = 0; i < itemCount; i++)
                        {
                            stepList.Add(double.Parse(_Arr[4 + i]));
                        }
                        ratesTime = stepList;
                    }
                }
                catch
                { }
            }
        }
        /// 重新加载控件内容
        /// <summary>
        /// 重新加载控件内容
        /// </summary>
        /// <param name="FaItem"></param>
        public override void LoadFA(CLDC_DataCore.Model.Plan.Plan_CostControl FaItem)
        {
            base.LoadFA(FaItem);
            if (base.Parm == string.Empty)
            {
                return;
            }
            Parm = base.Parm;
            stepCount = dataGridViewX1.Rows.Count;
        }
        #endregion 控件的参数与字符串参数的转换

        #region 用户操作
        /// 添加新的阶梯电价
        /// <summary>
        /// 添加新的阶梯电价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int rowIndex=dataGridViewX1.Rows.Add();
            DataGridViewRow row = dataGridViewX1.Rows[rowIndex];
            row.Cells[0].Value = rowIndex+1;
            row.Cells[1].Value = string.Format("{0:F2}", 0.5F);

            stepCount = dataGridViewX1.Rows.Count;
        }
        /// 删除阶梯电价
        /// <summary>
        /// 删除阶梯电价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            dataGridViewX1.Rows.RemoveAt(dataGridViewX1.Rows.Count - 1);

            stepCount = dataGridViewX1.Rows.Count;
        }
        /// 值校验，如果不满足要求回复原来的值
        /// <summary>
        /// 值校验，如果不满足要求回复原来的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewX1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string stringValue = cell.Value.ToString();
            double doubleValue = 0;
            if (!double.TryParse(stringValue, out doubleValue))
            {
                cell.Value = cellOldValue;
            }
        }
        private string cellOldValue = string.Empty;
        /// 保存开始编辑时的值
        /// <summary>
        /// 保存开始编辑时的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewX1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewCell cell = dataGridViewX1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cellOldValue = cell.Value.ToString();
        }
        private void checkBoxWriteTest_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWriteTest.Checked)
            {
                checkBoxWrite1.Enabled = true;
            }
            else
            {
                checkBoxWrite1.Enabled = false;
            }
        }
        private void Panel_Back_CheckedChanged(object sender, EventArgs e)
        {
            isChecked = Panel_Back.Checked;
        }
        #endregion 用户操作
    }
}
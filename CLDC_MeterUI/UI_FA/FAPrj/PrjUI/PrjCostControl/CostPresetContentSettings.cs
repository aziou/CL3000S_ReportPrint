using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjCostControl
{
    /// <summary>
    /// 预置内容设置 @C_B
    /// </summary>
    public partial class CostPresetContentSettings :CostControlBase
    {
        public CostPresetContentSettings():base()
        {
            InitializeComponent();
        }
        public CostPresetContentSettings(CLDC_DataCore.Struct.StCostControlConfig Item)
            : base(Item)
        {
            InitializeComponent();
            base.SetPanel = Panel_Back;
        }
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
        public override CLDC_DataCore.Struct.StPlan_CostControl CostControlPlanPrj
        {
            get
            {
                //if (string.IsNullOrEmpty(base.Parm))
                //{
                //    base.Parm = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", txtPresetMoney.Text, txtWarnMoney1.Text, txtWarnMoney2.Text, chkSetPrice.Checked, chkRealMoney.Checked, chkWarnMoney1.Checked, chkWarnMoney2.Checked, chkSetPrice2.Checked);
                //}
                //else if (base.Parm.Split('|').Length > 8)
                //{
                //    string[] split = base.Parm.Split('|');
                //    StringBuilder setPriceParam = new StringBuilder();
                //    for (int i = 8; i < split.Length; i++)
                //    {
                //        setPriceParam.Append("|").Append(split[i]);
                //    }
                //    base.Parm = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}{8}", txtPresetMoney.Text, txtWarnMoney1.Text, txtWarnMoney2.Text, chkSetPrice.Checked, chkRealMoney.Checked, chkWarnMoney1.Checked, chkWarnMoney2.Checked, chkSetPrice2.Checked,setPriceParam);
                //}
                //else
                //{
                //    base.Parm = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", txtPresetMoney.Text, txtWarnMoney1.Text, txtWarnMoney2.Text, chkSetPrice.Checked, chkRealMoney.Checked, chkWarnMoney1.Checked, chkWarnMoney2.Checked, chkSetPrice2.Checked);
                //}

                if (chkSetPrice.Enabled)
                {
                    StringBuilder param = new StringBuilder();
                    param.Append("True|2");
                    foreach (float f in ratesPrice)
                    {
                        param.Append(string.Format("|{0:0.0000}", f));
                    }
                    base.Parm = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}", txtPresetMoney.Text, txtWarnMoney1.Text, txtWarnMoney2.Text, chkSetPrice.Checked, chkRealMoney.Checked, chkWarnMoney1.Checked, chkWarnMoney2.Checked, chkSetPrice2.Checked,param.ToString());
                }
                else
                {
                    base.Parm = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", txtPresetMoney.Text, txtWarnMoney1.Text, txtWarnMoney2.Text, chkSetPrice.Checked, chkRealMoney.Checked, chkWarnMoney1.Checked, chkWarnMoney2.Checked, chkSetPrice2.Checked);
                }
                base._IsCheck = Panel_Back.Checked;
                return base.CostControlPlanPrj;
            }
        }

        public override string Parm
        {
            get
            {
                 return base.Parm;
            }
            set
            {
                string[] _Arr = value.Split('|');
                if (_Arr != null && _Arr.Length > 8)
                {
                    txtPresetMoney.Text = _Arr[0];
                    txtWarnMoney1.Text = _Arr[1];
                    txtWarnMoney2.Text = _Arr[2];
                    chkSetPrice.Checked = bool.Parse(_Arr[3]);
                    chkRealMoney.Checked = bool.Parse(_Arr[4]);
                    chkWarnMoney1.Checked = bool.Parse(_Arr[5]);
                    chkWarnMoney2.Checked = bool.Parse(_Arr[6]);
                    chkSetPrice2.Checked = bool.Parse(_Arr[7]);
                    int itemCount = _Arr.Length;
                    List<float> stepList = new List<float>();
                    for (int i = 10; i < itemCount; i++)
                    {
                        stepList.Add(float.Parse(_Arr[i]));
                    }
                    ratesPrice = stepList;
                }
                else if (_Arr != null && _Arr.Length == 8)
                {
                    txtPresetMoney.Text = _Arr[0];
                    txtWarnMoney1.Text = _Arr[1];
                    txtWarnMoney2.Text = _Arr[2];
                    chkSetPrice.Checked = bool.Parse(_Arr[3]);
                    chkRealMoney.Checked = bool.Parse(_Arr[4]);
                    chkWarnMoney1.Checked = bool.Parse(_Arr[5]);
                    chkWarnMoney2.Checked = bool.Parse(_Arr[6]);
                    chkSetPrice2.Checked = bool.Parse(_Arr[7]);
                }
                base.Parm = value;
            }
        }
        /// <summary>
        /// 有多少电价
        /// </summary>
        private int stepCount
        {
            get
            {
                int rowCount = dataGridViewX1.Rows.Count;
                if (rowCount > 1)
                {
                    return rowCount;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
               
            }
        }
        public override void LoadFA(CLDC_DataCore.Model.Plan.Plan_CostControl FaItem)
        {
            base.LoadFA(FaItem);
            if (base.Parm == string.Empty)
            {
                return;
            }
            this.Parm = base.Parm;
        }

        private void txtWarnMoney1_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (t.Text != "")
            {
                char[] chr = t.Text.ToCharArray();
                char c = chr[chr.Length - 1];
                if (((c <'0' || c >'9')&&c!='.') || (c == '.' && t.Text.IndexOf(c) != t.Text.LastIndexOf(c)))
                {
                    t.Text = t.Text.Remove(t.Text.Length - 1);
                    t.SelectionStart = t.Text.Length;
                }
            }
        }
        #region 用户操作
        /// 添加新的阶梯电价
        /// <summary>
        /// 添加新的阶梯电价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridViewX1.Rows.Add();
            DataGridViewRow row = dataGridViewX1.Rows[rowIndex];
            row.Cells[0].Value = rowIndex + 1;
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
            if (dataGridViewX1.Rows.Count > 1)
            {
                dataGridViewX1.Rows.RemoveAt(dataGridViewX1.Rows.Count - 1);
                stepCount = dataGridViewX1.Rows.Count;
            }
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
        #endregion 用户操作
        /// <summary>
        /// 每个阶梯电价的相关信息，其中第一行必须为0KWH
        /// </summary>
        private List<float> ratesPrice
        {
            get
            {
                List<float> stepList = new List<float>();
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    stepList.Add(float.Parse(row.Cells[1].Value.ToString()));
                }
                return stepList;
            }
            set
            {
                List<float> stepList = value;
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
                for (int i = 0; i < stepList.Count; i++)
                {
                    dataGridViewX1.Rows[i].Cells[0].Value = (i + 1).ToString();
                    dataGridViewX1.Rows[i].Cells[1].Value = (string.Format("{0:0.0000}", stepList[i]));
                }
            }
        }

        private void chkSetPrice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSetPrice.Checked)
            {
                this.dataGridViewX1.Enabled = true;
                this.buttonAdd.Enabled = true;
                this.buttonX1.Enabled = true;
                chkSetPrice2.Enabled = true;
            }
            else
            {
                this.dataGridViewX1.Enabled = false;
                this.buttonAdd.Enabled = false;
                this.buttonX1.Enabled = false;
                chkSetPrice2.Checked = false;
                chkSetPrice2.Enabled = false;
            }
        }
    }
}

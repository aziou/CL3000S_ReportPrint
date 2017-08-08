using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CLDC_DataCore.Struct;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI
{
    /// <summary>
    /// 耐压试验面板
    /// </summary>
    public partial class UI_InsulationItem : UserControl
    {
        #region 控件信息和方法
        public UI_InsulationItem()
        {
            InitializeComponent();
            InsulationParam = new StInsulationParam();
            LoadVariable();
        }

        private StInsulationParam insulationParam = new StInsulationParam();
        /// <summary>
        /// 耐压试验参数
        /// </summary>
        public StInsulationParam InsulationParam
        {
            get { return insulationParam; }
            set
            {
                insulationParam = value;
                LoadVariable();
            }
        }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsChecked
        {
            get
            {
                return container.Checked;
            }
            set
            {
                container.Checked = value;
            }
        }

        /// <summary>
        /// 刷新控件数据
        /// </summary>
        public void LoadVariable()
        {
            container.CaptionText = InsulationParam.ToString();
            textBoxXTime.Text = InsulationParam.Time.ToString();
            textBoxXVoltage.Text = InsulationParam.Voltage.ToString();
            textBoxXCurrent.Text = InsulationParam.Current.ToString();
            textBoxXCurrentAll.Text = InsulationParam.CurrentDevice.ToString();
        }
        #endregion 控件信息和方法

        #region 私有方法控件和Model的交互
        private void textBoxXTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == 8))
                e.Handled = true;
            else
            {
            }
        }
        #endregion 私有方法控件和Model的交互

        private void textBoxX_TextChanged(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.Controls.TextBoxX textbox = sender as DevComponents.DotNetBar.Controls.TextBoxX;
            if (textbox == null) return;
            switch (textbox.Name)
            {
                case "textBoxXTime":
                    int timeValue = 0;
                    int.TryParse(textBoxXTime.Text, out timeValue);
                    insulationParam.Time = timeValue;
                    break;

                case "textBoxXCurrent":
                    int currentValue = 10;
                    int.TryParse(textBoxXCurrent.Text, out currentValue);
                    insulationParam.Current = currentValue;
                    break;

                case "textBoxXVoltage":
                    int voltageValue = 2000;
                    int.TryParse(textBoxXVoltage.Text, out voltageValue);
                    if (insulationParam.InsulationType == StInsulationParam.EnumInsulationType.AnalogEarth)
                    {
                        if (voltageValue > 2000)
                        {
                            textBoxXVoltage.Text = "2000";
                            voltageValue = 2000;
                        }
                    }
                    else if (insulationParam.InsulationType == StInsulationParam.EnumInsulationType.DigitalEarth)
                    {
                        if (voltageValue > 4000)
                        {
                            textBoxXVoltage.Text = "4000";
                            voltageValue = 4000;
                        }
                    }
                    insulationParam.Voltage = voltageValue;
                    break;

                case "textBoxXCurrentAll":
                    int currentAllValue = 10;
                    int.TryParse(textBoxXCurrentAll.Text, out currentAllValue);
                    insulationParam.CurrentDevice = currentAllValue;
                    break;
            }
        }
    }
}

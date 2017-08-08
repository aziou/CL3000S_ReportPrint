using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.ShowDataView
{
    public partial class StateBarCircle : UserControl
    {
        public StateBarCircle()
        {
            InitializeComponent();
        }
        private string _strStateNum = "";
        public string strStateNum
        {
            set
            {
                _strStateNum = value;
                this.label1.Text = _strStateNum;
            }
        }
        private bool _isEnabled = false;
        public bool isEnabled
        {
            set
            {
                _isEnabled = value;
                this.pictureBox1.Enabled = _isEnabled;
            }
        }
        //
        private string _totalTime = "";
        /// <summary>
        /// 设置预计总时间，包括“预计”字样
        /// </summary>
        public string TotalTime
        {
            set
            {
                _totalTime = value;
                this.lab_TotalTime.Text = _totalTime;
            }
        }
        private string _lastTime = "";
        /// <summary>
        /// 设置预计总时间，包括“剩余”字样
        /// </summary>
        public string LastTime
        {
            set
            {
                _lastTime = value;
                this.lab_LastTime.Text = _lastTime;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI
{
    public partial class Waiting : UserControl
    {
        /// <summary>
        /// 等待提示框 , 本线程提示框，如果调用此提示框的线程阻塞，本框同样处于阻塞状态 
        /// </summary>
        public Waiting()
        {

            InitializeComponent();
        }

        /// <summary>
        /// 等待提示框, 本线程提示框，如果调用此提示框的线程阻塞，本框同样处于阻塞状态 
        /// </summary>
        /// <param name="strNotice">提示文字</param>
        public Waiting(string strNotice)
        {
            InitializeComponent();
            SetNotice(strNotice);
        }

        /// <summary>
        /// 设置提示文字
        /// </summary>
        /// <param name="strNotice">提示文字</param>
        public void SetNotice(string strNotice)
        {
            Lab_Notice.Text = strNotice;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI
{
    public partial class UI_AboutUs : Office2007Form
    {
        public UI_AboutUs()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void UI_AboutUs_LocationChanged(object sender, EventArgs e)
        {
            Version softVer = new Version(Application.ProductVersion);
            label2.Text = softVer.ToString();
        }
        protected override void OnResize(EventArgs e)
        {
            
            //base.OnResize(e);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
    
}
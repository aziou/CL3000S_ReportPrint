using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI
{
    public partial class UI_Login : Office2007Form
    {
        //登录处理
        public delegate void LoginHandel(CLDC_DataCore.Struct.StLogin LoginInfo);
        //public event LoginHandel Login;

        public UI_Login()
        {
                InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            CLDC_DataCore.Struct.StLogin _LoginInfo = new CLDC_DataCore.Struct.StLogin();
            string JYY = comboBox1.Text;
            string JYYPW = textBox1.Text;
            string HYY = comboBox3.Text;
            string HYYPW = textBox2.Text;
            if (JYY.Length == 0 || JYYPW.Length == 0 || HYY.Length == 0 || HYYPW.Length == 0)
            {
                MessageBoxEx.Show(this,"请正确填写登录信息！");
                return;
            }
            //验证身份
            CLDC_DataCore.Struct.StUserInfo tagUserInfo;
            bool Result=false;
            //检验员登录
            Result = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.UserGroup.CheckIn(JYY, JYYPW, out tagUserInfo);
            if (!Result)
            {
                MessageBoxEx.Show(this,"检验员身份验证失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //核验员登录
            Result = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.UserGroup.CheckIn(HYY, HYYPW, out tagUserInfo);
            if (!Result)
            {
                MessageBoxEx.Show(this,"核验员身份验证失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _LoginInfo.strHYY = HYY;
            _LoginInfo.strJYY = JYY;
            _LoginInfo.strHYYPWD = HYYPW;
            _LoginInfo.strJYYPWD = JYYPW;
            
           // LoginHandel(
        }
    }
}
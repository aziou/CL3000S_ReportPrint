using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CLDC_DataManager
{
    public partial class Frm_SelectJDY : Form
    {
        const string CONST_MISINTERFACE_PATH = @"C:\PMSJYT\CURRENT";
        public bool isOK = false;
        public string FileName = string.Empty;

        public Frm_SelectJDY()
        {
            InitializeComponent();
        }

        private void Frm_SelectJDY_Load(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(CONST_MISINTERFACE_PATH)) return;

            Cmb_filename.Items.Clear();
            foreach (System.IO.FileInfo finfo in new System.IO.DirectoryInfo(CONST_MISINTERFACE_PATH).GetFiles())
            {
                Cmb_filename.Items.Add(finfo.Name);
            }
        }

        private void Cmd_Import_Click(object sender, EventArgs e)
        {
            if (Cmb_filename.SelectedIndex < 0)
            {
                MessageBox.Show("请选择文件名...", "消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cmb_filename.Focus();
                return;
            }

            DialogResult rt = MessageBox.Show("确定上传到【" + Cmb_filename.Text + "】吗？", "提示",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Exclamation,
            MessageBoxDefaultButton.Button2);
            if (rt == DialogResult.Yes)
            {
                isOK = true;
            }
            else { isOK = false; }

            string[] strName = Cmb_filename.Text.Split('.');
            //Comm.LoginSettingData LoginData = new Comm.LoginSettingData();
            FileName = strName[0];
            this.Close();
        }

        private void Cmd_Close_Click(object sender, EventArgs e)
        {
            isOK = false;
            this.Close();
        }
    }
}

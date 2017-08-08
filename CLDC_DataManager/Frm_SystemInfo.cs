using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CLDC_DataManager
{
    public partial class Frm_SystemInfo : Form, CLDC_DataCore.Interfaces.IControlPanel
    {
        [DllImport("user32")]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);
        [DllImport("user32")]
        private static extern int RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

        public Frm_SystemInfo()
        {
            InitializeComponent();

            RemoveMenu(GetSystemMenu(this.Handle, false), 0xf060, 0x1000);


            this.Load += new EventHandler(Frm_SystemInfo_Load);

            Cmd_AddYj.Click+=new EventHandler(Cmd_AddInfo_Click);

            Cmd_AddBz.Click += new EventHandler(Cmd_AddInfo_Click);

            Cmd_NewInfo.Click += new EventHandler(Cmd_NewInfo_Click);

            Cmd_DelYj.Click+=new EventHandler(Cmd_DelInfo_Click);

            Cmd_DelBz.Click += new EventHandler(Cmd_DelInfo_Click);


        }


        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_SystemInfo_Load(object sender, EventArgs e)
        {
            int Int_TmpCount = 0;

            #region ------------填充检定依据和制造标准---------------------
            Int_TmpCount =int.Parse(clsMain.getIniString("检定依据","JCount","0"));

            for(int i=1;i<=Int_TmpCount;i++)
            {
                Lst_Yj.Items.Add(clsMain.getIniString("检定依据",i.ToString(),""));
            }

            Int_TmpCount=int.Parse(clsMain.getIniString("制造标准","ZCount","0"));

            for(int i=1;i<=Int_TmpCount;i++)
            {
                Lst_Bz.Items.Add(clsMain.getIniString("制造标准",i.ToString(),""));
            }
            #endregion

            #region ---------------数据参数获取--------------

            Chk_Server.Checked = clsMain.getIniString("Server", "Run", "0") == "0" ? false : true;      //是否从服务器获取数据
            if (Chk_Server.Checked) Gb_Server.Enabled = true;
            Txt_Ip.Text = clsMain.getIniString("Server", "Host");           //服务器地址
            Txt_User.Text = clsMain.getIniString("Server", "Name");         //服务器登录用户名
            Txt_Pwd.Text = clsMain.getIniString("Server", "Pwd");           //服务器登录密码
            Chk_HzzFh.Checked = clsMain.getIniString("DataSetup", "hzfh", "1") == "1" ? true : false;       //化整值是否显示符号
            #endregion

            string str_DataPath="";
            str_DataPath=clsMain.getIniString("Path","DataPath");
            txt_DataPath.Text = str_DataPath;

            #region 装置信息加载
            string str_txt_BzbName = "", str_txt_BzbType = "", str_txt_BzbNum = "", str_txt_BzbRank = "", str_txt_BzbRange = "", str_txt_BzbCertificate = "", str_txt_BzbValidity = "",
                   str_txt_ZzName = "", str_txt_ZzType = "", str_txt_ZzNum = "", str_txt_ZzRange = "", str_txt_ZzRank = "", str_txt_ZzCertificate = "", str_txt_ZzValidity = "";
            str_txt_BzbName = clsMain.getIniString("DeviceInfo", "txt_BzbName");
            txt_BzbName.Text = str_txt_BzbName;

            str_txt_BzbType = clsMain.getIniString("DeviceInfo", "txt_BzbType");
            txt_BzbType.Text = str_txt_BzbType;

            str_txt_BzbNum = clsMain.getIniString("DeviceInfo", "txt_BzbNum");
            txt_BzbNum.Text = str_txt_BzbNum;

            str_txt_BzbRank = clsMain.getIniString("DeviceInfo", "txt_BzbRank");
            txt_BzbRank.Text = str_txt_BzbRank;

            str_txt_BzbRange = clsMain.getIniString("DeviceInfo", "txt_BzbRange");
            txt_BzbRange.Text = str_txt_BzbRange;

            str_txt_BzbCertificate = clsMain.getIniString("DeviceInfo", "txt_BzbCertificate");
            txt_BzbCertificate.Text = str_txt_BzbCertificate;

            str_txt_BzbValidity = clsMain.getIniString("DeviceInfo", "txt_BzbValidity");
            txt_BzbValidity.Text = str_txt_BzbValidity;

            str_txt_ZzName = clsMain.getIniString("DeviceInfo", "txt_ZzName");
            txt_ZzName.Text = str_txt_ZzName;

            str_txt_ZzType = clsMain.getIniString("DeviceInfo", "txt_ZzType");
            txt_ZzType.Text = str_txt_ZzType;

            str_txt_ZzNum = clsMain.getIniString("DeviceInfo", "txt_ZzNum");
            txt_ZzNum.Text = str_txt_ZzNum;

            str_txt_ZzRange = clsMain.getIniString("DeviceInfo", "txt_ZzRange");
            txt_ZzRange.Text = str_txt_ZzRange;

            str_txt_ZzRank = clsMain.getIniString("DeviceInfo", "txt_ZzRank");
            txt_ZzRank.Text = str_txt_ZzRank;

            str_txt_ZzCertificate = clsMain.getIniString("DeviceInfo", "txt_ZzCertificate");
            txt_ZzCertificate.Text = str_txt_ZzCertificate;

            str_txt_ZzValidity = clsMain.getIniString("DeviceInfo", "txt_ZzValidity");
            txt_ZzValidity.Text = str_txt_ZzValidity;

           
            #region 不确定度
            str_txt_ZzValidity = clsMain.getIniString("DeviceInfo", "txt_Unsure");
            txt_Unsure.Text = str_txt_ZzValidity;
            str_txt_ZzValidity = clsMain.getIniString("DeviceInfo", "txt_Unsure02");
            txt_Unsure02.Text = str_txt_ZzValidity;
            str_txt_ZzValidity = clsMain.getIniString("DeviceInfo", "txt_Unsure03");
            txt_Unsure03.Text = str_txt_ZzValidity;
            str_txt_ZzValidity = clsMain.getIniString("DeviceInfo", "txt_Unsure04");
            txt_Unsure04.Text = str_txt_ZzValidity;
            str_txt_ZzValidity = clsMain.getIniString("DeviceInfo", "txt_Unsure05");
            txt_Unsure05.Text = str_txt_ZzValidity;
            str_txt_ZzValidity = clsMain.getIniString("DeviceInfo", "txt_Unsure06");
            txt_Unsure06.Text = str_txt_ZzValidity;
            str_txt_ZzValidity = clsMain.getIniString("DeviceInfo", "txt_Unsure07");
            txt_Unsure07.Text = str_txt_ZzValidity;
            #endregion

            #region Lable Set   txt_lableWidth   txt_lableHeight  txt_lableX  txt_lableY  txt_labFix
            str_txt_ZzValidity = clsMain.getIniString("OtherInfo", "txt_lableWidth");
            txt_lableWidth.Text = str_txt_ZzValidity;
            str_txt_ZzValidity = clsMain.getIniString("OtherInfo", "txt_lableHeight");
            txt_lableHeight.Text = str_txt_ZzValidity;
            str_txt_ZzValidity = clsMain.getIniString("OtherInfo", "txt_lableX");
            txt_lableX.Text = str_txt_ZzValidity;
            str_txt_ZzValidity = clsMain.getIniString("OtherInfo", "txt_lableY");
            txt_lableY.Text = str_txt_ZzValidity;
            str_txt_ZzValidity = clsMain.getIniString("OtherInfo", "txt_labFix");
            txt_labFix.Text = str_txt_ZzValidity;
            str_txt_ZzValidity = clsMain.getIniString("OtherInfo", "cmb_labDirection");
            str_txt_ZzValidity = str_txt_ZzValidity == "" ? "1" : str_txt_ZzValidity;
            cmb_labDirection.SelectedIndex = Convert.ToInt16(str_txt_ZzValidity);
            
            #endregion

            str_txt_ZzValidity = clsMain.getIniString("OtherInfo", "Equiment");
            tab_EquitMentNUM.Text = str_txt_ZzValidity;
            #endregion
            if (clsMain.dataToMis != null)
            {
                clsMain.dataToMis.ShowPanel(this);
            }

            if (clsMain.report != null)
            {
                clsMain.report.ShowPanel(this);
            }

        }


        /// <summary>
        /// 参数设置完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Close_Click(object sender, EventArgs e)
        {

            #region--------------检定依据，制造标准存储------------

            clsMain.WriteIni("制造标准", "ZCount", Lst_Bz.Items.Count.ToString());

            for (int i = 0; i < Lst_Bz.Items.Count; i++)
            {
                clsMain.WriteIni("制造标准", (i + 1).ToString(), Lst_Bz.Items[i].ToString());
            }

            clsMain.WriteIni("检定依据", "JCount", Lst_Yj.Items.Count.ToString());

            for (int i = 0; i < Lst_Yj.Items.Count; i++)
            {
                clsMain.WriteIni("检定依据", (i + 1).ToString(), Lst_Yj.Items[i].ToString());
            }

            #endregion

            #region -----------------数据参数存储--------------

            clsMain.WriteIni("Server", "Run", Chk_Server.Checked ? "1" : "0");  //是否从服务器获取数据
            clsMain.WriteIni("Server", "Host", Txt_Ip.Text);                    //服务器IP
            clsMain.WriteIni("Server", "Name", Txt_User.Text);                  //服务器用户名
            clsMain.WriteIni("Server", "Pwd", Txt_Pwd.Text);                    //服务器密码
            clsMain.WriteIni("DataSetup", "hzfh", Chk_HzzFh.Checked ? "1" : "0");   //化整符号
            #endregion 

            
            if (txt_DataPath.Text != null && txt_DataPath.Text != "")
            {
                clsMain.WriteIni("Path", "DataPath", txt_DataPath.Text.ToString().Trim());
            }

            #region 装置信息 保存
            if (txt_BzbName.Text != null && txt_BzbName.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_BzbName", txt_BzbName.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_BzbName", txt_BzbName.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
            }

            if (txt_BzbType.Text != null && txt_BzbType.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_BzbType", txt_BzbType.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_BzbType", txt_BzbType.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
        
            }
            if (txt_BzbNum.Text != null && txt_BzbNum.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_BzbNum", txt_BzbNum.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_BzbNum", txt_BzbNum.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
        
            }
            if (txt_BzbRange.Text != null && txt_BzbRange.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_BzbRange", txt_BzbRange.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_BzbRange", txt_BzbRange.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
        
            }
            if (txt_BzbRank.Text != null && txt_BzbRank.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_BzbRank", txt_BzbRank.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_BzbRank", txt_BzbRank.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
        
            }
            if (txt_BzbCertificate.Text != null && txt_BzbCertificate.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_BzbCertificate", txt_BzbCertificate.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_BzbCertificate", txt_BzbCertificate.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
        
            }
            if (txt_BzbValidity.Text != null && txt_BzbValidity.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_BzbValidity", txt_BzbValidity.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_BzbValidity", txt_BzbValidity.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
        
            }

            if (txt_ZzName.Text != null && txt_ZzName.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_ZzName", txt_ZzName.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_ZzName", txt_ZzName.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
        
            }
            if (txt_ZzType.Text != null && txt_ZzType.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_ZzType", txt_ZzType.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_ZzType", txt_ZzType.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
        
            }
            if (txt_ZzNum.Text != null && txt_ZzNum.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_ZzNum", txt_ZzNum.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_ZzNum", txt_ZzNum.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
        
            }
            if (txt_ZzRange.Text != null && txt_ZzRange.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_ZzRange", txt_ZzRange.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_ZzRange", txt_ZzRange.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
        
            }
            if (txt_ZzRank.Text != null && txt_ZzRank.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_ZzRank", txt_ZzRank.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_ZzRank", txt_ZzRank.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
        
            }
            if (txt_ZzCertificate.Text != null && txt_ZzCertificate.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_ZzCertificate", txt_ZzCertificate.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_ZzCertificate", txt_ZzCertificate.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
        
            }
            
            if (txt_ZzValidity.Text != null && txt_ZzValidity.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_ZzValidity", txt_ZzValidity.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_ZzValidity", txt_ZzValidity.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            #region 不确定度保存
            if (txt_Unsure.Text != null && txt_Unsure.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_Unsure", txt_Unsure.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_Unsure", txt_Unsure.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            if (txt_Unsure02.Text != null && txt_Unsure02.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_Unsure02", txt_Unsure02.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_Unsure02", txt_Unsure02.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            if (txt_Unsure03.Text != null && txt_Unsure03.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_Unsure03", txt_Unsure03.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_Unsure03", txt_Unsure03.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            if (txt_Unsure04.Text != null && txt_Unsure04.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_Unsure04", txt_Unsure04.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_Unsure04", txt_Unsure04.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            if (txt_Unsure05.Text != null && txt_Unsure05.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_Unsure05", txt_Unsure05.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_Unsure05", txt_Unsure05.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            if (txt_Unsure06.Text != null && txt_Unsure06.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_Unsure06", txt_Unsure06.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_Unsure06", txt_Unsure06.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            if (txt_Unsure07.Text != null && txt_Unsure07.Text != "")
            {
                clsMain.WriteIni("DeviceInfo", "txt_Unsure07", txt_Unsure07.Text.ToString().Trim());
                clsMain.WriteIni("DeviceInfo", "txt_Unsure07", txt_Unsure07.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }

            #endregion
            if (tab_EquitMentNUM.Text != null && tab_EquitMentNUM.Text != "")
            {
                clsMain.WriteIni("OtherInfo", "Equiment", tab_EquitMentNUM.Text.ToString().Trim());
                clsMain.WriteIni("OtherInfo", "Equiment", tab_EquitMentNUM.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            #region lable Set  txt_lableWidth   txt_lableHeight  txt_lableX  txt_lableY  txt_labFix
            if (txt_lableWidth.Text != null && txt_lableWidth.Text != "")
            {
                clsMain.WriteIni("OtherInfo", "txt_lableWidth", txt_lableWidth.Text.ToString().Trim());
                clsMain.WriteIni("OtherInfo", "txt_lableWidth", txt_lableWidth.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            if (txt_lableHeight.Text != null && txt_lableHeight.Text != "")
            {
                clsMain.WriteIni("OtherInfo", "txt_lableHeight", txt_lableHeight.Text.ToString().Trim());
                clsMain.WriteIni("OtherInfo", "txt_lableHeight", txt_lableHeight.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            if (txt_lableX.Text != null && txt_lableX.Text != "")
            {
                clsMain.WriteIni("OtherInfo", "txt_lableX", txt_lableX.Text.ToString().Trim());
                clsMain.WriteIni("OtherInfo", "txt_lableX", txt_lableX.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            if (txt_lableY.Text != null && txt_lableY.Text != "")
            {
                clsMain.WriteIni("OtherInfo", "txt_lableY", txt_lableY.Text.ToString().Trim());
                clsMain.WriteIni("OtherInfo", "txt_lableY", txt_lableY.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            if (txt_labFix.Text != null && txt_labFix.Text != "")
            {
                clsMain.WriteIni("OtherInfo", "txt_labFix", txt_labFix.Text.ToString().Trim());
                clsMain.WriteIni("OtherInfo", "txt_labFix", txt_labFix.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }

            if (cmb_labDirection.Text != null && cmb_labDirection.Text != "")
            {
                clsMain.WriteIni("OtherInfo", "cmb_labDirection", cmb_labDirection.SelectedIndex.ToString().Trim());
                clsMain.WriteIni("OtherInfo", "cmb_labDirection", cmb_labDirection.SelectedIndex.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

            }
            #endregion 
            #endregion
            if (Save != null)
            {
                Save(this, new EventArgs());
            }

            this.Close();
        }

        #region ---------------依据、标准控制操作事件-----------
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_DelInfo_Click(object sender, EventArgs e)
        {
            if ((Button)sender == Cmd_DelYj)
            {
                if (Lst_Yj.SelectedIndex < 0) return;
                Lst_Yj.Items.RemoveAt(Lst_Yj.SelectedIndex);
            }
            else
            {
                if (Lst_Bz.SelectedIndex < 0) return;
                Lst_Bz.Items.RemoveAt(Lst_Bz.SelectedIndex);
            }
        }

        /// <summary>
        /// 增加新的依据、标准Panel显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_AddInfo_Click(object sender, EventArgs e)
        {
            Panel_Info.Visible = true;
            Panel_Info.Tag = sender;
            Txt_Info.Focus();
        }
        /// <summary>
        /// 增加新的依据、标准
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_NewInfo_Click(object sender, EventArgs e)
        {
            if (Txt_Info.Text == string.Empty)
            {
                Panel_Info.Tag = null;
                Panel_Info.Visible = false;
                return;
            }

            if ((Button)Panel_Info.Tag == Cmd_AddYj)
            {
                Lst_Yj.Items.Add(Txt_Info.Text);
            }
            else
            {
                Lst_Bz.Items.Add(Txt_Info.Text);
            }
            Txt_Info.Text = "";
            Panel_Info.Tag = null;
            Panel_Info.Visible = false;

        }
        #endregion 


        private void Chk_Server_CheckedChanged(object sender, EventArgs e)
        {
            Gb_Server.Enabled = Chk_Server.Checked;
        }

        


        #region IControlPanel 成员

        public event EventHandler Save;

        private System.ComponentModel.Design.ServiceContainer serviceContainer = new System.ComponentModel.Design.ServiceContainer();

        #region IServiceProvider 成员

        public new object GetService(Type serviceType)
        {
            return serviceContainer.GetService(serviceType);
        }

        #endregion

        public TabPage[] tabPages(Dictionary<string,string> tabs)
        {
            TabPage[] tabpages=new TabPage[tabs.Count];
            int i=0;
            foreach (string key in tabs.Keys)
            { 
                if(!TAB_2.TabPages.ContainsKey(key))
                {
                    TAB_2.TabPages.Add(key, tabs[key]);
                }
                tabpages[i++] = TAB_2.TabPages[key];
            }
            return tabpages;
        }

        #endregion

        private void btn_SearchFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;
                txt_DataPath.Text = file;
            }
        }

    }
}
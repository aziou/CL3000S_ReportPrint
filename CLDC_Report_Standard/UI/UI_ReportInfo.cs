using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLReport_Standard.UI
{
    public partial class UI_ReportInfo : UserControl
    {
        public UI_ReportInfo()
        {
            InitializeComponent();
            this.Load += new EventHandler(UI_ReportInfo_Load);
            Chk_Save.CheckedChanged+=new EventHandler(Chk_Save_CheckedChanged);
            Cmd_Path.Click+=new EventHandler(Cmd_Path_Click);
            Chk_Preview.Click+=new EventHandler(Chk_Preview_Click);
            Chk_SaveOnly.Click+=new EventHandler(Chk_SaveOnly_Click);
        }

        public bool IsInLoad = false;

        private void UI_ReportInfo_Load(object sender, EventArgs e)
        {
            #region--------------报表信息设置获取--------------

            Txt_Head.Text = clsMain.getIniString("ReportInfo", "Head", "" );       //获取报表抬头
            Txt_EHead.Text = clsMain.getIniString("ReportInfo", "EHead", "" );       //获取报表抬头
            Txt_CheckAdr.Text = clsMain.getIniString("ReportInfo", "CheckAdr", "" );       //检测地点
            Txt_Adr.Text = clsMain.getIniString("ReportInfo", "Adr", "" );       //单位地址
            Txt_Tel.Text = clsMain.getIniString("ReportInfo", "Tel", "" );       //电话号码
            Txt_Tex.Text = clsMain.getIniString("ReportInfo", "Tex", "" );       //传真
            Txt_Email.Text = clsMain.getIniString("ReportInfo", "Email", "" );       //邮箱
            Txt_Zip.Text = clsMain.getIniString("ReportInfo", "Zip", "" );       //邮编
            Txt_Num.Text = clsMain.getIniString("ReportInfo", "Num", "" );       //授权号
            Txt_Dw.Text = clsMain.getIniString("ReportInfo", "Dw", "" );       //授权单位
            Txt_PageHead.Text = clsMain.getIniString("ReportInfo", "PageHead", "" );       //页眉
            Txt_NotCheck.Text = clsMain.getIniString("ReportInfo", "NotCheck", "" );       //未检标志
            Chk_PrintHuman.Checked = clsMain.getIniString("OtherInfo", "PrintHuman", "0" ) == "0" ? false : true;   //是否打印检定员    
            Txt_BHG.Text = clsMain.getIniString("OtherInfo", "BHG", "" );   //不合格标志
            Chk_Save.Checked = clsMain.getIniString("OtherInfo", "Save", "0" ) == "0" ? false : true;   //是否存档  
            Txt_Path.Text = clsMain.getIniString("OtherInfo", "SavePath", Application.StartupPath );   //存档路径 
            Chk_SaveOnly.Checked = clsMain.getIniString("OtherInfo", "SaveOnly", "0" ) == "0" ? false : true;   //是否仅存档  
            if (Chk_Save.Checked) Gb_Save.Visible = true;
            Cmb_Save.SelectedIndex = int.Parse(clsMain.getIniString("OtherInfo", "SaveBag", "0" ));   //是否打印检定员  
            Chk_Preview.Checked = clsMain.getIniString("OtherInfo", "Preview", "0" ) == "0" ? false : true;   //是否预览   
            Cmb_PrintStyle.SelectedIndex = int.Parse(clsMain.getIniString("OtherInfo", "PrintStyle", "0" ));   //是否打印检定员    


            //Txt_Head.Text = clsMain.getIniString("ReportInfo", "Head", "", clsMain.getFilePath("ReportInfo.ini"));       //获取报表抬头
            //Txt_EHead.Text = clsMain.getIniString("ReportInfo", "EHead", "", clsMain.getFilePath("ReportInfo.ini"));       //获取报表抬头
            //Txt_CheckAdr.Text = clsMain.getIniString("ReportInfo", "CheckAdr", "", clsMain.getFilePath("ReportInfo.ini"));       //检测地点
            //Txt_Adr.Text = clsMain.getIniString("ReportInfo", "Adr", "", clsMain.getFilePath("ReportInfo.ini"));       //单位地址
            //Txt_Tel.Text = clsMain.getIniString("ReportInfo", "Tel", "", clsMain.getFilePath("ReportInfo.ini"));       //电话号码
            //Txt_Tex.Text = clsMain.getIniString("ReportInfo", "Tex", "", clsMain.getFilePath("ReportInfo.ini"));       //传真
            //Txt_Email.Text = clsMain.getIniString("ReportInfo", "Email", "", clsMain.getFilePath("ReportInfo.ini"));       //邮箱
            //Txt_Zip.Text = clsMain.getIniString("ReportInfo", "Zip", "", clsMain.getFilePath("ReportInfo.ini"));       //邮编
            //Txt_Num.Text = clsMain.getIniString("ReportInfo", "Num", "", clsMain.getFilePath("ReportInfo.ini"));       //授权号
            //Txt_Dw.Text = clsMain.getIniString("ReportInfo", "Dw", "", clsMain.getFilePath("ReportInfo.ini"));       //授权单位
            //Txt_PageHead.Text = clsMain.getIniString("ReportInfo", "PageHead", "", clsMain.getFilePath("ReportInfo.ini"));       //页眉
            //Txt_NotCheck.Text = clsMain.getIniString("ReportInfo", "NotCheck", "", clsMain.getFilePath("ReportInfo.ini"));       //未检标志
            //Chk_PrintHuman.Checked = clsMain.getIniString("OtherInfo", "PrintHuman", "0", clsMain.getFilePath("ReportInfo.ini")) == "0" ? false : true;   //是否打印检定员    
            //Txt_BHG.Text = clsMain.getIniString("OtherInfo", "BHG", "", clsMain.getFilePath("ReportInfo.ini"));   //不合格标志
            //Chk_Save.Checked = clsMain.getIniString("OtherInfo", "Save", "0", clsMain.getFilePath("ReportInfo.ini")) == "0" ? false : true;   //是否存档  
            //Txt_Path.Text = clsMain.getIniString("OtherInfo", "SavePath", Application.StartupPath, clsMain.getFilePath("ReportInfo.ini"));   //存档路径 
            //Chk_SaveOnly.Checked = clsMain.getIniString("OtherInfo", "SaveOnly", "0", clsMain.getFilePath("ReportInfo.ini")) == "0" ? false : true;   //是否仅存档  
            //if (Chk_Save.Checked) Gb_Save.Visible = true;
            //Cmb_Save.SelectedIndex = int.Parse(clsMain.getIniString("OtherInfo", "SaveBag", "0", clsMain.getFilePath("ReportInfo.ini")));   //是否打印检定员  
            //Chk_Preview.Checked = clsMain.getIniString("OtherInfo", "Preview", "0", clsMain.getFilePath("ReportInfo.ini")) == "0" ? false : true;   //是否预览   
            //Cmb_PrintStyle.SelectedIndex = int.Parse(clsMain.getIniString("OtherInfo", "PrintStyle", "0", clsMain.getFilePath("ReportInfo.ini")));   //是否打印检定员    



            #endregion
            IsInLoad = true;
        }


        public void Save()
        {
            #region----------报表信息数据存储--------------
            if (IsInLoad == true)
            {
                clsMain.WriteIni("ReportInfo", "Head", Txt_Head.Text);       //获取报表抬头
                clsMain.WriteIni("ReportInfo", "EHead", Txt_EHead.Text);       //获取E报表抬头
                clsMain.WriteIni("ReportInfo", "CheckAdr", Txt_CheckAdr.Text);       //检定地点
                clsMain.WriteIni("ReportInfo", "Adr", Txt_Adr.Text);       //地址
                clsMain.WriteIni("ReportInfo", "Tel", Txt_Tel.Text);       //电话
                clsMain.WriteIni("ReportInfo", "Tex", Txt_Tex.Text);       //传真
                clsMain.WriteIni("ReportInfo", "Email", Txt_Email.Text);       //电邮
                clsMain.WriteIni("ReportInfo", "Zip", Txt_Zip.Text);       //邮编
                clsMain.WriteIni("ReportInfo", "Num", Txt_Num.Text);       //授权号
                clsMain.WriteIni("ReportInfo", "Dw", Txt_Dw.Text);       //单位
                clsMain.WriteIni("ReportInfo", "PageHead", Txt_PageHead.Text);       //页眉
                clsMain.WriteIni("ReportInfo", "NotCheck", Txt_NotCheck.Text);       //未见标记
                clsMain.WriteIni("OtherInfo", "PrintHuman", Chk_PrintHuman.Checked ? "1" : "0");       //是否打印检定员
                clsMain.WriteIni("OtherInfo", "BHG", Txt_BHG.Text);       //不合格数据标志
                clsMain.WriteIni("OtherInfo", "Save", Chk_Save.Checked ? "1" : "0");       //是否存盘
                clsMain.WriteIni("OtherInfo", "SaveBag", Cmb_Save.SelectedIndex == -1 ? "0" : Cmb_Save.SelectedIndex.ToString());       //存档打包
                clsMain.WriteIni("OtherInfo", "SaveOnly", Chk_SaveOnly.Checked ? "1" : "0");       //是否仅存档
                clsMain.WriteIni("OtherInfo", "SavePath", Txt_Path.Text);       //存档路径
                clsMain.WriteIni("OtherInfo", "Preview", Chk_Preview.Checked ? "1" : "0");       //是否预览
                clsMain.WriteIni("OtherInfo", "PrintStyle", Cmb_PrintStyle.SelectedIndex.ToString());       //打印样式


            }
            
            //clsMain.WriteIni("ReportInfo", "Head", Txt_Head.Text, clsMain.getFilePath("ReportInfo.ini"));       //获取报表抬头
            //clsMain.WriteIni("ReportInfo", "EHead", Txt_EHead.Text, clsMain.getFilePath("ReportInfo.ini"));       //获取E报表抬头
            //clsMain.WriteIni("ReportInfo", "CheckAdr", Txt_CheckAdr.Text, clsMain.getFilePath("ReportInfo.ini"));       //检定地点
            //clsMain.WriteIni("ReportInfo", "Adr", Txt_Adr.Text, clsMain.getFilePath("ReportInfo.ini"));       //地址
            //clsMain.WriteIni("ReportInfo", "Tel", Txt_Tel.Text, clsMain.getFilePath("ReportInfo.ini"));       //电话
            //clsMain.WriteIni("ReportInfo", "Tex", Txt_Tex.Text, clsMain.getFilePath("ReportInfo.ini"));       //传真
            //clsMain.WriteIni("ReportInfo", "Email", Txt_Email.Text, clsMain.getFilePath("ReportInfo.ini"));       //电邮
            //clsMain.WriteIni("ReportInfo", "Zip", Txt_Zip.Text, clsMain.getFilePath("ReportInfo.ini"));       //邮编
            //clsMain.WriteIni("ReportInfo", "Num", Txt_Num.Text, clsMain.getFilePath("ReportInfo.ini"));       //授权号
            //clsMain.WriteIni("ReportInfo", "Dw", Txt_Dw.Text, clsMain.getFilePath("ReportInfo.ini"));       //单位
            //clsMain.WriteIni("ReportInfo", "PageHead", Txt_PageHead.Text, clsMain.getFilePath("ReportInfo.ini"));       //页眉
            //clsMain.WriteIni("ReportInfo", "NotCheck", Txt_NotCheck.Text, clsMain.getFilePath("ReportInfo.ini"));       //未见标记
            //clsMain.WriteIni("OtherInfo", "PrintHuman", Chk_PrintHuman.Checked ? "1" : "0", clsMain.getFilePath("ReportInfo.ini"));       //是否打印检定员
            //clsMain.WriteIni("OtherInfo", "BHG", Txt_BHG.Text, clsMain.getFilePath("ReportInfo.ini"));       //不合格数据标志
            //clsMain.WriteIni("OtherInfo", "Save", Chk_Save.Checked ? "1" : "0", clsMain.getFilePath("ReportInfo.ini"));       //是否存盘
            //clsMain.WriteIni("OtherInfo", "SaveBag", Cmb_Save.SelectedIndex == -1 ? "0" : Cmb_Save.SelectedIndex.ToString(), clsMain.getFilePath("ReportInfo.ini"));       //存档打包
            //clsMain.WriteIni("OtherInfo", "SaveOnly", Chk_SaveOnly.Checked ? "1" : "0", clsMain.getFilePath("ReportInfo.ini"));       //是否仅存档
            //clsMain.WriteIni("OtherInfo", "SavePath", Txt_Path.Text, clsMain.getFilePath("ReportInfo.ini"));       //存档路径
            //clsMain.WriteIni("OtherInfo", "Preview", Chk_Preview.Checked ? "1" : "0", clsMain.getFilePath("ReportInfo.ini"));       //是否预览
            //clsMain.WriteIni("OtherInfo", "PrintStyle", Cmb_PrintStyle.SelectedIndex.ToString(), clsMain.getFilePath("ReportInfo.ini"));       //打印样式

            #endregion 
        }

        #region----------------存档路径控制操作事件-------------------
        /// <summary>
        /// 是否存档选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_Save_CheckedChanged(object sender, EventArgs e)
        {
            Gb_Save.Visible = Chk_Save.Checked;
        }
        /// <summary>
        /// 选择路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Path_Click(object sender, EventArgs e)
        {
            if (Txt_Path.Text == string.Empty)
            {
                this.folderDialog.SelectedPath = Application.StartupPath;
            }
            else
            {
                this.folderDialog.SelectedPath = Txt_Path.Text;
            }
            this.folderDialog.ShowDialog();
            Txt_Path.Text = this.folderDialog.SelectedPath;
        }
        /// <summary>
        /// 预览选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_Preview_Click(object sender, EventArgs e)
        {
            Chk_SaveOnly.Checked = Chk_Preview.Checked ? false : Chk_SaveOnly.Checked;
        }
        /// <summary>
        /// 仅存档选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_SaveOnly_Click(object sender, EventArgs e)
        {
            Chk_Preview.Checked = Chk_Save.Checked ? false : Chk_Preview.Checked;
        }


        #endregion 

    }
}

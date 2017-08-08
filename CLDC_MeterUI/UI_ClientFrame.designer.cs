namespace CLDC_MeterUI
{
    partial class UI_ClientFrame
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_ClientFrame));
            this.MenuMain = new System.Windows.Forms.MenuStrip();
            this.Menu_SystemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_SystemConfig_SysConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_SystemConfig_Link = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_SystemConfig_UnLink = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_SystemConfig_PowerOn = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_SystemConfig_PowerOff = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_SystemConfig_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_SchemeManage = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_SchemeManage_Total = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_SchemeManage_Total_DanXiang = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_SchemeManage_Total_SanXiang = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tools_XieBo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tools_CheckMeterUI = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tools_ReadStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tools_SetPort = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tools_ComAdapter = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tools_ShowData = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tools_DgnProtocolSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_View = new System.Windows.Forms.ToolStripMenuItem();
            this.Tool_DataManage = new System.Windows.Forms.ToolStripMenuItem();
            this.更新软件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolMenu_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Help_HelpDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Tool_AboutUs = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.MenuMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuMain
            // 
            this.MenuMain.AutoSize = false;
            this.MenuMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.MenuMain.Dock = System.Windows.Forms.DockStyle.None;
            this.MenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_SystemConfig,
            this.Menu_SchemeManage,
            this.Menu_Tools,
            this.Menu_View,
            this.更新软件ToolStripMenuItem,
            this.Menu_Help});
            this.MenuMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MenuMain.Location = new System.Drawing.Point(0, 0);
            this.MenuMain.Name = "MenuMain";
            this.MenuMain.Padding = new System.Windows.Forms.Padding(6, 0, 0, 2);
            this.MenuMain.Size = new System.Drawing.Size(389, 20);
            this.MenuMain.TabIndex = 1;
            this.MenuMain.Text = "系统菜单";
            this.MenuMain.Visible = false;
            // 
            // Menu_SystemConfig
            // 
            this.Menu_SystemConfig.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_SystemConfig_SysConfig,
            this.Menu_SystemConfig_Link,
            this.Menu_SystemConfig_UnLink,
            this.Menu_SystemConfig_PowerOn,
            this.Menu_SystemConfig_PowerOff,
            this.Menu_SystemConfig_Exit});
            this.Menu_SystemConfig.Name = "Menu_SystemConfig";
            this.Menu_SystemConfig.Size = new System.Drawing.Size(59, 18);
            this.Menu_SystemConfig.Text = "系统(&S)";
            // 
            // Menu_SystemConfig_SysConfig
            // 
            this.Menu_SystemConfig_SysConfig.Name = "Menu_SystemConfig_SysConfig";
            this.Menu_SystemConfig_SysConfig.Size = new System.Drawing.Size(136, 22);
            this.Menu_SystemConfig_SysConfig.Text = "系统配置(&C)";
            this.Menu_SystemConfig_SysConfig.Click += new System.EventHandler(this.Menu_SystemConfig_SysConfig_Click);
            // 
            // Menu_SystemConfig_Link
            // 
            this.Menu_SystemConfig_Link.Name = "Menu_SystemConfig_Link";
            this.Menu_SystemConfig_Link.Size = new System.Drawing.Size(136, 22);
            this.Menu_SystemConfig_Link.Text = "联机(&L)";
            this.Menu_SystemConfig_Link.Click += new System.EventHandler(this.Menu_SystemConfig_Link_Click);
            // 
            // Menu_SystemConfig_UnLink
            // 
            this.Menu_SystemConfig_UnLink.Name = "Menu_SystemConfig_UnLink";
            this.Menu_SystemConfig_UnLink.Size = new System.Drawing.Size(136, 22);
            this.Menu_SystemConfig_UnLink.Text = "脱机(&U)";
            this.Menu_SystemConfig_UnLink.Click += new System.EventHandler(this.Menu_SystemConfig_UnLink_Click);
            // 
            // Menu_SystemConfig_PowerOn
            // 
            this.Menu_SystemConfig_PowerOn.Name = "Menu_SystemConfig_PowerOn";
            this.Menu_SystemConfig_PowerOn.Size = new System.Drawing.Size(136, 22);
            this.Menu_SystemConfig_PowerOn.Text = "升源(&P)";
            this.Menu_SystemConfig_PowerOn.Click += new System.EventHandler(this.Menu_SystemConfig_PowerOn_Click);
            // 
            // Menu_SystemConfig_PowerOff
            // 
            this.Menu_SystemConfig_PowerOff.Name = "Menu_SystemConfig_PowerOff";
            this.Menu_SystemConfig_PowerOff.Size = new System.Drawing.Size(136, 22);
            this.Menu_SystemConfig_PowerOff.Text = "关源(&O)";
            this.Menu_SystemConfig_PowerOff.Click += new System.EventHandler(this.Menu_SystemConfig_PowerOff_Click);
            // 
            // Menu_SystemConfig_Exit
            // 
            this.Menu_SystemConfig_Exit.Name = "Menu_SystemConfig_Exit";
            this.Menu_SystemConfig_Exit.Size = new System.Drawing.Size(136, 22);
            this.Menu_SystemConfig_Exit.Text = "退出(&E)";
            this.Menu_SystemConfig_Exit.Click += new System.EventHandler(this.Menu_SystemConfig_Exit_Click);
            // 
            // Menu_SchemeManage
            // 
            this.Menu_SchemeManage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_SchemeManage_Total,
            this.Menu_Tools_XieBo});
            this.Menu_SchemeManage.Name = "Menu_SchemeManage";
            this.Menu_SchemeManage.Size = new System.Drawing.Size(65, 18);
            this.Menu_SchemeManage.Text = "方案管理";
            // 
            // Menu_SchemeManage_Total
            // 
            this.Menu_SchemeManage_Total.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_SchemeManage_Total_DanXiang,
            this.Menu_SchemeManage_Total_SanXiang});
            this.Menu_SchemeManage_Total.Name = "Menu_SchemeManage_Total";
            this.Menu_SchemeManage_Total.Size = new System.Drawing.Size(130, 22);
            this.Menu_SchemeManage_Total.Text = "总方案管理";
            // 
            // Menu_SchemeManage_Total_DanXiang
            // 
            this.Menu_SchemeManage_Total_DanXiang.Name = "Menu_SchemeManage_Total_DanXiang";
            this.Menu_SchemeManage_Total_DanXiang.Size = new System.Drawing.Size(94, 22);
            this.Menu_SchemeManage_Total_DanXiang.Text = "单相";
            this.Menu_SchemeManage_Total_DanXiang.Click += new System.EventHandler(this.Menu_SchemeManage_Total_DanXiang_Click);
            // 
            // Menu_SchemeManage_Total_SanXiang
            // 
            this.Menu_SchemeManage_Total_SanXiang.Name = "Menu_SchemeManage_Total_SanXiang";
            this.Menu_SchemeManage_Total_SanXiang.Size = new System.Drawing.Size(94, 22);
            this.Menu_SchemeManage_Total_SanXiang.Text = "三相";
            this.Menu_SchemeManage_Total_SanXiang.Click += new System.EventHandler(this.Menu_SchemeManage_Total_SanXiang_Click);
            // 
            // Menu_Tools_XieBo
            // 
            this.Menu_Tools_XieBo.Name = "Menu_Tools_XieBo";
            this.Menu_Tools_XieBo.Size = new System.Drawing.Size(130, 22);
            this.Menu_Tools_XieBo.Text = "谐波设置";
            this.Menu_Tools_XieBo.Click += new System.EventHandler(this.Menu_Tools_XieBo_Click);
            // 
            // Menu_Tools
            // 
            this.Menu_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Tools_CheckMeterUI,
            this.Menu_Tools_ReadStatus,
            this.Menu_Tools_SetPort,
            this.Menu_Tools_ComAdapter,
            this.Menu_Tools_ShowData,
            this.Menu_Tools_DgnProtocolSetup});
            this.Menu_Tools.Name = "Menu_Tools";
            this.Menu_Tools.Size = new System.Drawing.Size(65, 18);
            this.Menu_Tools.Text = "辅助工具";// CLDC_ResourceManager.GlobalRes.rm.GetString("辅助工具");
            // 
            // Menu_Tools_CheckMeterUI
            // 
            this.Menu_Tools_CheckMeterUI.Name = "Menu_Tools_CheckMeterUI";
            this.Menu_Tools_CheckMeterUI.Size = new System.Drawing.Size(154, 22);
            this.Menu_Tools_CheckMeterUI.Text = "表接线测试";
            this.Menu_Tools_CheckMeterUI.Visible = false;
            this.Menu_Tools_CheckMeterUI.Click += new System.EventHandler(this.Menu_Tools_CheckMeterUI_Click);
            // 
            // Menu_Tools_ReadStatus
            // 
            this.Menu_Tools_ReadStatus.Name = "Menu_Tools_ReadStatus";
            this.Menu_Tools_ReadStatus.Size = new System.Drawing.Size(154, 22);
            this.Menu_Tools_ReadStatus.Text = "读取设备状态";
            this.Menu_Tools_ReadStatus.Visible = false;
            this.Menu_Tools_ReadStatus.Click += new System.EventHandler(this.Menu_Tools_ReadStatus_Click);
            // 
            // Menu_Tools_SetPort
            // 
            this.Menu_Tools_SetPort.Name = "Menu_Tools_SetPort";
            this.Menu_Tools_SetPort.Size = new System.Drawing.Size(154, 22);
            this.Menu_Tools_SetPort.Text = "485通道配置";// CLDC_ResourceManager.GlobalRes.rm.GetString("485通道配置");
            this.Menu_Tools_SetPort.Click += new System.EventHandler(this.Menu_Tools_SetPort_Click);
            // 
            // Menu_Tools_ComAdapter
            // 
            this.Menu_Tools_ComAdapter.Name = "Menu_Tools_ComAdapter";
            this.Menu_Tools_ComAdapter.Size = new System.Drawing.Size(154, 22);
            this.Menu_Tools_ComAdapter.Text = "设备通道配置";// CLDC_ResourceManager.GlobalRes.rm.GetString("设备通道配置");
            this.Menu_Tools_ComAdapter.Click += new System.EventHandler(this.Menu_Tools_ComAdapter_Click);
            // 
            // Menu_Tools_ShowData
            // 
            this.Menu_Tools_ShowData.Name = "Menu_Tools_ShowData";
            this.Menu_Tools_ShowData.Size = new System.Drawing.Size(154, 22);
            this.Menu_Tools_ShowData.Text = "显示通讯数据";
            this.Menu_Tools_ShowData.Visible = false;
            this.Menu_Tools_ShowData.Click += new System.EventHandler(this.Menu_Tools_ShowData_Click);
            // 
            // Menu_Tools_DgnProtocolSetup
            // 
            this.Menu_Tools_DgnProtocolSetup.Name = "Menu_Tools_DgnProtocolSetup";
            this.Menu_Tools_DgnProtocolSetup.Size = new System.Drawing.Size(154, 22);
            this.Menu_Tools_DgnProtocolSetup.Text = "多功能协议配置";
            this.Menu_Tools_DgnProtocolSetup.Visible = false;
            this.Menu_Tools_DgnProtocolSetup.Click += new System.EventHandler(this.Menu_Tools_DgnProtocolSetup_Click);
            // 
            // Menu_View
            // 
            this.Menu_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Tool_DataManage});
            this.Menu_View.Name = "Menu_View";
            this.Menu_View.Size = new System.Drawing.Size(41, 18);
            this.Menu_View.Text = "查看";
            // 
            // Tool_DataManage
            // 
            this.Tool_DataManage.Name = "Tool_DataManage";
            this.Tool_DataManage.Size = new System.Drawing.Size(118, 22);
            this.Tool_DataManage.Text = "数据管理";
            this.Tool_DataManage.Click += new System.EventHandler(this.Tool_DataManage_Click);
            // 
            // 更新软件ToolStripMenuItem
            // 
            this.更新软件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolMenu_Update});
            this.更新软件ToolStripMenuItem.Name = "更新软件ToolStripMenuItem";
            this.更新软件ToolStripMenuItem.Size = new System.Drawing.Size(41, 18);
            this.更新软件ToolStripMenuItem.Text = "更新";
            this.更新软件ToolStripMenuItem.Visible = false;
            // 
            // ToolMenu_Update
            // 
            this.ToolMenu_Update.Name = "ToolMenu_Update";
            this.ToolMenu_Update.Size = new System.Drawing.Size(118, 22);
            this.ToolMenu_Update.Text = "更新软件";
            // 
            // Menu_Help
            // 
            this.Menu_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Help_HelpDoc,
            this.Menu_Tool_AboutUs});
            this.Menu_Help.Name = "Menu_Help";
            this.Menu_Help.Size = new System.Drawing.Size(41, 18);
            this.Menu_Help.Text = "帮助";
            // 
            // Menu_Help_HelpDoc
            // 
            this.Menu_Help_HelpDoc.Name = "Menu_Help_HelpDoc";
            this.Menu_Help_HelpDoc.Size = new System.Drawing.Size(130, 22);
            this.Menu_Help_HelpDoc.Text = "操作手册";
            this.Menu_Help_HelpDoc.Click += new System.EventHandler(this.Menu_Help_HelpDoc_Click);
            // 
            // Menu_Tool_AboutUs
            // 
            this.Menu_Tool_AboutUs.Name = "Menu_Tool_AboutUs";
            this.Menu_Tool_AboutUs.Size = new System.Drawing.Size(130, 22);
            this.Menu_Tool_AboutUs.Text = "关于本软件";
            this.Menu_Tool_AboutUs.Click += new System.EventHandler(this.Menu_Tool_AboutUs_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 21);
            this.panel1.Margin = new System.Windows.Forms.Padding(1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(784, 532);
            this.panel1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(786, 554);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.MenuMain, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(786, 20);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // UI_ClientFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 554);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UI_ClientFrame";
            this.Text = "CL3000S-J集中控制系统";
            this.MenuMain.ResumeLayout(false);
            this.MenuMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuMain;
        private System.Windows.Forms.ToolStripMenuItem Menu_SystemConfig;
        private System.Windows.Forms.ToolStripMenuItem Menu_SystemConfig_SysConfig;
        private System.Windows.Forms.ToolStripMenuItem Menu_SystemConfig_Exit;
        private System.Windows.Forms.ToolStripMenuItem Menu_SchemeManage;
        private System.Windows.Forms.ToolStripMenuItem Menu_SchemeManage_Total;
        private System.Windows.Forms.ToolStripMenuItem Menu_SchemeManage_Total_DanXiang;
        private System.Windows.Forms.ToolStripMenuItem Menu_SchemeManage_Total_SanXiang;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tools;
        private System.Windows.Forms.ToolStripMenuItem Menu_View;
        private System.Windows.Forms.ToolStripMenuItem Menu_Help;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem Menu_SystemConfig_Link;
        private System.Windows.Forms.ToolStripMenuItem Menu_SystemConfig_UnLink;
        private System.Windows.Forms.ToolStripMenuItem Menu_SystemConfig_PowerOn;
        private System.Windows.Forms.ToolStripMenuItem Menu_SystemConfig_PowerOff;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tools_CheckMeterUI;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tools_ReadStatus;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tools_SetPort;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tools_ShowData;
        private System.Windows.Forms.ToolStripMenuItem Menu_Help_HelpDoc;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tool_AboutUs;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tools_DgnProtocolSetup;
        private System.Windows.Forms.ToolStripMenuItem Tool_DataManage;
        private System.Windows.Forms.ToolStripMenuItem 更新软件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolMenu_Update;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tools_ComAdapter;
        private System.Windows.Forms.ToolStripMenuItem Menu_Tools_XieBo;

    }
}
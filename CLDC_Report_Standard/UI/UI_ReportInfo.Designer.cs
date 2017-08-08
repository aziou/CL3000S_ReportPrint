namespace CLReport_Standard.UI
{
    partial class UI_ReportInfo
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.Cmb_PrintStyle = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.Gb_Save = new System.Windows.Forms.GroupBox();
            this.Cmd_Path = new System.Windows.Forms.Button();
            this.Chk_SaveOnly = new System.Windows.Forms.CheckBox();
            this.Txt_Path = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.Cmb_Save = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.Chk_Preview = new System.Windows.Forms.CheckBox();
            this.Chk_Save = new System.Windows.Forms.CheckBox();
            this.Txt_BHG = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.Chk_PrintHuman = new System.Windows.Forms.CheckBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.Txt_NotCheck = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.Txt_PageHead = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.Txt_Dw = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.Txt_Num = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.Txt_Zip = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.Txt_Email = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.Txt_Tex = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.Txt_Tel = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.Txt_Adr = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.Txt_CheckAdr = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.Txt_EHead = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.Txt_Head = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.Gb_Save.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.groupBox9, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(561, 341);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.Cmb_PrintStyle);
            this.groupBox10.Controls.Add(this.label35);
            this.groupBox10.Controls.Add(this.Gb_Save);
            this.groupBox10.Controls.Add(this.Chk_Preview);
            this.groupBox10.Controls.Add(this.Chk_Save);
            this.groupBox10.Controls.Add(this.Txt_BHG);
            this.groupBox10.Controls.Add(this.label32);
            this.groupBox10.Controls.Add(this.Chk_PrintHuman);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox10.Location = new System.Drawing.Point(3, 120);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(555, 218);
            this.groupBox10.TabIndex = 2;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "其他设置";
            // 
            // Cmb_PrintStyle
            // 
            this.Cmb_PrintStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_PrintStyle.FormattingEnabled = true;
            this.Cmb_PrintStyle.Items.AddRange(new object[] {
            "证书打印化整值；通知书打印原始值",
            "证书打印化整值；通知书打印化整值",
            "证书打印原始值；通知书打印原始值",
            "证书打印原始值；通知书打印化整值"});
            this.Cmb_PrintStyle.Location = new System.Drawing.Point(53, 76);
            this.Cmb_PrintStyle.Name = "Cmb_PrintStyle";
            this.Cmb_PrintStyle.Size = new System.Drawing.Size(198, 20);
            this.Cmb_PrintStyle.TabIndex = 29;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(4, 79);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(53, 12);
            this.label35.TabIndex = 28;
            this.label35.Text = "打印时：";
            // 
            // Gb_Save
            // 
            this.Gb_Save.Controls.Add(this.Cmd_Path);
            this.Gb_Save.Controls.Add(this.Chk_SaveOnly);
            this.Gb_Save.Controls.Add(this.Txt_Path);
            this.Gb_Save.Controls.Add(this.label34);
            this.Gb_Save.Controls.Add(this.Cmb_Save);
            this.Gb_Save.Controls.Add(this.label33);
            this.Gb_Save.Location = new System.Drawing.Point(108, 34);
            this.Gb_Save.Name = "Gb_Save";
            this.Gb_Save.Size = new System.Drawing.Size(445, 34);
            this.Gb_Save.TabIndex = 27;
            this.Gb_Save.TabStop = false;
            this.Gb_Save.Visible = false;
            // 
            // Cmd_Path
            // 
            this.Cmd_Path.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Cmd_Path.Location = new System.Drawing.Point(322, 12);
            this.Cmd_Path.Name = "Cmd_Path";
            this.Cmd_Path.Size = new System.Drawing.Size(25, 16);
            this.Cmd_Path.TabIndex = 30;
            this.Cmd_Path.Text = "…";
            this.Cmd_Path.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Cmd_Path.UseVisualStyleBackColor = true;
            // 
            // Chk_SaveOnly
            // 
            this.Chk_SaveOnly.AutoSize = true;
            this.Chk_SaveOnly.Location = new System.Drawing.Point(357, 12);
            this.Chk_SaveOnly.Name = "Chk_SaveOnly";
            this.Chk_SaveOnly.Size = new System.Drawing.Size(84, 16);
            this.Chk_SaveOnly.TabIndex = 29;
            this.Chk_SaveOnly.Text = "是否仅存档";
            this.Chk_SaveOnly.UseVisualStyleBackColor = true;
            // 
            // Txt_Path
            // 
            this.Txt_Path.Location = new System.Drawing.Point(178, 9);
            this.Txt_Path.Name = "Txt_Path";
            this.Txt_Path.Size = new System.Drawing.Size(172, 21);
            this.Txt_Path.TabIndex = 27;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(118, 13);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(65, 12);
            this.label34.TabIndex = 28;
            this.label34.Text = "存档路径：";
            // 
            // Cmb_Save
            // 
            this.Cmb_Save.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Save.FormattingEnabled = true;
            this.Cmb_Save.Items.AddRange(new object[] {
            "天",
            "月",
            "年"});
            this.Cmb_Save.Location = new System.Drawing.Point(66, 10);
            this.Cmb_Save.Name = "Cmb_Save";
            this.Cmb_Save.Size = new System.Drawing.Size(49, 20);
            this.Cmb_Save.TabIndex = 26;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(6, 13);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(65, 12);
            this.label33.TabIndex = 25;
            this.label33.Text = "存档打包：";
            // 
            // Chk_Preview
            // 
            this.Chk_Preview.AutoSize = true;
            this.Chk_Preview.Location = new System.Drawing.Point(6, 60);
            this.Chk_Preview.Name = "Chk_Preview";
            this.Chk_Preview.Size = new System.Drawing.Size(72, 16);
            this.Chk_Preview.TabIndex = 26;
            this.Chk_Preview.Text = "是否预览";
            this.Chk_Preview.UseVisualStyleBackColor = true;
            // 
            // Chk_Save
            // 
            this.Chk_Save.AutoSize = true;
            this.Chk_Save.Location = new System.Drawing.Point(6, 38);
            this.Chk_Save.Name = "Chk_Save";
            this.Chk_Save.Size = new System.Drawing.Size(96, 16);
            this.Chk_Save.TabIndex = 25;
            this.Chk_Save.Text = "是否报表存档";
            this.Chk_Save.UseVisualStyleBackColor = true;
            // 
            // Txt_BHG
            // 
            this.Txt_BHG.Location = new System.Drawing.Point(259, 13);
            this.Txt_BHG.Name = "Txt_BHG";
            this.Txt_BHG.Size = new System.Drawing.Size(49, 21);
            this.Txt_BHG.TabIndex = 24;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(162, 17);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(101, 12);
            this.label32.TabIndex = 1;
            this.label32.Text = "不合格数据标志：";
            // 
            // Chk_PrintHuman
            // 
            this.Chk_PrintHuman.AutoSize = true;
            this.Chk_PrintHuman.Location = new System.Drawing.Point(6, 16);
            this.Chk_PrintHuman.Name = "Chk_PrintHuman";
            this.Chk_PrintHuman.Size = new System.Drawing.Size(150, 16);
            this.Chk_PrintHuman.TabIndex = 0;
            this.Chk_PrintHuman.Text = "是否打印检定员\\核验员";
            this.Chk_PrintHuman.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.Txt_NotCheck);
            this.groupBox9.Controls.Add(this.label29);
            this.groupBox9.Controls.Add(this.Txt_PageHead);
            this.groupBox9.Controls.Add(this.label30);
            this.groupBox9.Controls.Add(this.Txt_Dw);
            this.groupBox9.Controls.Add(this.label31);
            this.groupBox9.Controls.Add(this.Txt_Num);
            this.groupBox9.Controls.Add(this.label26);
            this.groupBox9.Controls.Add(this.Txt_Zip);
            this.groupBox9.Controls.Add(this.label27);
            this.groupBox9.Controls.Add(this.Txt_Email);
            this.groupBox9.Controls.Add(this.label28);
            this.groupBox9.Controls.Add(this.Txt_Tex);
            this.groupBox9.Controls.Add(this.label23);
            this.groupBox9.Controls.Add(this.Txt_Tel);
            this.groupBox9.Controls.Add(this.label24);
            this.groupBox9.Controls.Add(this.Txt_Adr);
            this.groupBox9.Controls.Add(this.label25);
            this.groupBox9.Controls.Add(this.Txt_CheckAdr);
            this.groupBox9.Controls.Add(this.label22);
            this.groupBox9.Controls.Add(this.Txt_EHead);
            this.groupBox9.Controls.Add(this.label21);
            this.groupBox9.Controls.Add(this.Txt_Head);
            this.groupBox9.Controls.Add(this.label20);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox9.Location = new System.Drawing.Point(3, 3);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(555, 111);
            this.groupBox9.TabIndex = 1;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "报表单位信息设置";
            // 
            // Txt_NotCheck
            // 
            this.Txt_NotCheck.Location = new System.Drawing.Point(433, 81);
            this.Txt_NotCheck.Name = "Txt_NotCheck";
            this.Txt_NotCheck.Size = new System.Drawing.Size(117, 21);
            this.Txt_NotCheck.TabIndex = 25;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(372, 84);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(65, 12);
            this.label29.TabIndex = 26;
            this.label29.Text = "未检标志：";
            // 
            // Txt_PageHead
            // 
            this.Txt_PageHead.Location = new System.Drawing.Point(247, 81);
            this.Txt_PageHead.Name = "Txt_PageHead";
            this.Txt_PageHead.Size = new System.Drawing.Size(117, 21);
            this.Txt_PageHead.TabIndex = 23;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(186, 84);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(65, 12);
            this.label30.TabIndex = 24;
            this.label30.Text = "页眉信息：";
            // 
            // Txt_Dw
            // 
            this.Txt_Dw.Location = new System.Drawing.Point(67, 81);
            this.Txt_Dw.Name = "Txt_Dw";
            this.Txt_Dw.Size = new System.Drawing.Size(117, 21);
            this.Txt_Dw.TabIndex = 21;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 84);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(65, 12);
            this.label31.TabIndex = 22;
            this.label31.Text = "授权单位：";
            // 
            // Txt_Num
            // 
            this.Txt_Num.Location = new System.Drawing.Point(433, 59);
            this.Txt_Num.Name = "Txt_Num";
            this.Txt_Num.Size = new System.Drawing.Size(117, 21);
            this.Txt_Num.TabIndex = 19;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(384, 62);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 12);
            this.label26.TabIndex = 20;
            this.label26.Text = "授权号：";
            // 
            // Txt_Zip
            // 
            this.Txt_Zip.Location = new System.Drawing.Point(247, 59);
            this.Txt_Zip.Name = "Txt_Zip";
            this.Txt_Zip.Size = new System.Drawing.Size(117, 21);
            this.Txt_Zip.TabIndex = 17;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(186, 62);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 12);
            this.label27.TabIndex = 18;
            this.label27.Text = "邮    编：";
            // 
            // Txt_Email
            // 
            this.Txt_Email.Location = new System.Drawing.Point(67, 59);
            this.Txt_Email.Name = "Txt_Email";
            this.Txt_Email.Size = new System.Drawing.Size(117, 21);
            this.Txt_Email.TabIndex = 15;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(24, 62);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(47, 12);
            this.label28.TabIndex = 16;
            this.label28.Text = "Email：";
            // 
            // Txt_Tex
            // 
            this.Txt_Tex.Location = new System.Drawing.Point(433, 37);
            this.Txt_Tex.Name = "Txt_Tex";
            this.Txt_Tex.Size = new System.Drawing.Size(117, 21);
            this.Txt_Tex.TabIndex = 13;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(372, 40);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(65, 12);
            this.label23.TabIndex = 14;
            this.label23.Text = "传    真：";
            // 
            // Txt_Tel
            // 
            this.Txt_Tel.Location = new System.Drawing.Point(247, 37);
            this.Txt_Tel.Name = "Txt_Tel";
            this.Txt_Tel.Size = new System.Drawing.Size(117, 21);
            this.Txt_Tel.TabIndex = 11;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(186, 40);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(65, 12);
            this.label24.TabIndex = 12;
            this.label24.Text = "电话号码：";
            // 
            // Txt_Adr
            // 
            this.Txt_Adr.Location = new System.Drawing.Point(67, 37);
            this.Txt_Adr.Name = "Txt_Adr";
            this.Txt_Adr.Size = new System.Drawing.Size(117, 21);
            this.Txt_Adr.TabIndex = 9;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 40);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(65, 12);
            this.label25.TabIndex = 10;
            this.label25.Text = "单位地址：";
            // 
            // Txt_CheckAdr
            // 
            this.Txt_CheckAdr.Location = new System.Drawing.Point(433, 15);
            this.Txt_CheckAdr.Name = "Txt_CheckAdr";
            this.Txt_CheckAdr.Size = new System.Drawing.Size(117, 21);
            this.Txt_CheckAdr.TabIndex = 7;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(372, 18);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(65, 12);
            this.label22.TabIndex = 8;
            this.label22.Text = "检测地点：";
            // 
            // Txt_EHead
            // 
            this.Txt_EHead.Location = new System.Drawing.Point(247, 15);
            this.Txt_EHead.Name = "Txt_EHead";
            this.Txt_EHead.Size = new System.Drawing.Size(117, 21);
            this.Txt_EHead.TabIndex = 5;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(186, 18);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 12);
            this.label21.TabIndex = 6;
            this.label21.Text = "英文抬头：";
            // 
            // Txt_Head
            // 
            this.Txt_Head.Location = new System.Drawing.Point(67, 15);
            this.Txt_Head.Name = "Txt_Head";
            this.Txt_Head.Size = new System.Drawing.Size(117, 21);
            this.Txt_Head.TabIndex = 3;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 18);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 12);
            this.label20.TabIndex = 4;
            this.label20.Text = "报表抬头：";
            // 
            // UI_ReportInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UI_ReportInfo";
            this.Size = new System.Drawing.Size(561, 341);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.Gb_Save.ResumeLayout(false);
            this.Gb_Save.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox Txt_NotCheck;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox Txt_PageHead;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox Txt_Dw;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox Txt_Num;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox Txt_Zip;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox Txt_Email;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox Txt_Tex;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox Txt_Tel;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox Txt_Adr;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox Txt_CheckAdr;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox Txt_EHead;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox Txt_Head;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.ComboBox Cmb_PrintStyle;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.GroupBox Gb_Save;
        private System.Windows.Forms.Button Cmd_Path;
        private System.Windows.Forms.CheckBox Chk_SaveOnly;
        private System.Windows.Forms.TextBox Txt_Path;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox Cmb_Save;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.CheckBox Chk_Preview;
        private System.Windows.Forms.CheckBox Chk_Save;
        private System.Windows.Forms.TextBox Txt_BHG;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.CheckBox Chk_PrintHuman;
        private System.Windows.Forms.FolderBrowserDialog folderDialog;
    }
}

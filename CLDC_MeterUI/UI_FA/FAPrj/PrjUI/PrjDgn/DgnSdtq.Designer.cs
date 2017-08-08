namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn
{
    partial class DgnSdtq
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DgnSdtq));
            this.Panel_Back = new CLDC_Comm.ExtendedPanel.ExtendedPanel();
            this.Cmd_Remove = new DevComponents.DotNetBar.ButtonX();
            this.Cmd_Insert = new DevComponents.DotNetBar.ButtonX();
            this.Lst_SD = new System.Windows.Forms.ListBox();
            this.ChkAutoCut = new System.Windows.Forms.CheckBox();
            this.Cmb_FeiLv = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label4 = new System.Windows.Forms.Label();
            this.DTP_SD = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.Chk_Pz = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Chk_Qz = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Chk_Pf = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Chk_Qf = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkBoxX3 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkBoxX2 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkBoxX4 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.Panel_Back.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Back
            // 
            this.Panel_Back.BorderColor = System.Drawing.Color.Gray;
            this.Panel_Back.CaptionColorOne = System.Drawing.Color.Silver;
            this.Panel_Back.CaptionColorTwo = System.Drawing.Color.SeaShell;
            this.Panel_Back.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Panel_Back.CaptionSize = 25;
            this.Panel_Back.CaptionTextColor = System.Drawing.Color.Black;
            this.Panel_Back.Controls.Add(this.Cmd_Remove);
            this.Panel_Back.Controls.Add(this.Chk_Pf);
            this.Panel_Back.Controls.Add(this.Chk_Qf);
            this.Panel_Back.Controls.Add(this.Chk_Pz);
            this.Panel_Back.Controls.Add(this.Chk_Qz);
            this.Panel_Back.Controls.Add(this.Cmd_Insert);
            this.Panel_Back.Controls.Add(this.Lst_SD);
            this.Panel_Back.Controls.Add(this.ChkAutoCut);
            this.Panel_Back.Controls.Add(this.Cmb_FeiLv);
            this.Panel_Back.Controls.Add(this.label4);
            this.Panel_Back.Controls.Add(this.DTP_SD);
            this.Panel_Back.Controls.Add(this.label3);
            this.Panel_Back.CornerStyle = CLDC_Comm.ExtendedPanel.CornerStyle.Normal;
            this.Panel_Back.DirectionCtrlColor = System.Drawing.Color.DarkGray;
            this.Panel_Back.DirectionCtrlHoverColor = System.Drawing.Color.Orange;
            this.Panel_Back.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Back.Location = new System.Drawing.Point(0, 0);
            this.Panel_Back.Name = "Panel_Back";
            this.Panel_Back.ShowCheckBox = true;
            this.Panel_Back.Size = new System.Drawing.Size(509, 106);
            this.Panel_Back.TabIndex = 2;
            // 
            // Cmd_Remove
            // 
            this.Cmd_Remove.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Remove.Enabled = false;
            this.Cmd_Remove.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_Remove.Image")));
            this.Cmd_Remove.Location = new System.Drawing.Point(260, 82);
            this.Cmd_Remove.Name = "Cmd_Remove";
            this.Cmd_Remove.Size = new System.Drawing.Size(71, 20);
            this.Cmd_Remove.TabIndex = 13;
            this.Cmd_Remove.Text = "移  除";
            this.Cmd_Remove.Click += new System.EventHandler(this.Cmd_Remove_Click);
            // 
            // Cmd_Insert
            // 
            this.Cmd_Insert.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Insert.Enabled = false;
            this.Cmd_Insert.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_Insert.Image")));
            this.Cmd_Insert.Location = new System.Drawing.Point(185, 82);
            this.Cmd_Insert.Name = "Cmd_Insert";
            this.Cmd_Insert.Size = new System.Drawing.Size(69, 20);
            this.Cmd_Insert.TabIndex = 12;
            this.Cmd_Insert.Text = "插  入";
            this.Cmd_Insert.Click += new System.EventHandler(this.Cmd_Insert_Click);
            // 
            // Lst_SD
            // 
            this.Lst_SD.Enabled = false;
            this.Lst_SD.FormattingEnabled = true;
            this.Lst_SD.ItemHeight = 12;
            this.Lst_SD.Location = new System.Drawing.Point(28, 27);
            this.Lst_SD.Name = "Lst_SD";
            this.Lst_SD.Size = new System.Drawing.Size(151, 76);
            this.Lst_SD.TabIndex = 11;
            // 
            // ChkAutoCut
            // 
            this.ChkAutoCut.AutoSize = true;
            this.ChkAutoCut.BackColor = System.Drawing.Color.Transparent;
            this.ChkAutoCut.Location = new System.Drawing.Point(379, 31);
            this.ChkAutoCut.Name = "ChkAutoCut";
            this.ChkAutoCut.Size = new System.Drawing.Size(96, 16);
            this.ChkAutoCut.TabIndex = 14;
            this.ChkAutoCut.Text = "自动读取时段";
            this.ChkAutoCut.UseVisualStyleBackColor = false;
            this.ChkAutoCut.CheckedChanged += new System.EventHandler(this.ChkAutoCut_CheckedChanged);
            // 
            // Cmb_FeiLv
            // 
            this.Cmb_FeiLv.Enabled = false;
            this.Cmb_FeiLv.FormattingEnabled = true;
            this.Cmb_FeiLv.Items.AddRange(new object[] {
            "尖",
            "峰",
            "平",
            "谷"});
            this.Cmb_FeiLv.Location = new System.Drawing.Point(273, 55);
            this.Cmb_FeiLv.Name = "Cmb_FeiLv";
            this.Cmb_FeiLv.Size = new System.Drawing.Size(58, 20);
            this.Cmb_FeiLv.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(185, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "时 段 费 率 ：";
            // 
            // DTP_SD
            // 
            this.DTP_SD.CustomFormat = "HH\':\'ss";
            this.DTP_SD.Enabled = false;
            this.DTP_SD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTP_SD.Location = new System.Drawing.Point(273, 26);
            this.DTP_SD.Name = "DTP_SD";
            this.DTP_SD.ShowUpDown = true;
            this.DTP_SD.Size = new System.Drawing.Size(58, 21);
            this.DTP_SD.TabIndex = 8;
            this.DTP_SD.Value = new System.DateTime(2009, 1, 13, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(185, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "时段起始时间：";
            // 
            // Chk_Pz
            // 
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.Class = "";
            this.Chk_Pz.Checked = true;
            this.Chk_Pz.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Chk_Pz.CheckValue = "Y";
            this.Chk_Pz.Location = new System.Drawing.Point(337, 55);
            this.Chk_Pz.Name = "Chk_Pz";
            this.Chk_Pz.Size = new System.Drawing.Size(80, 23);
            this.Chk_Pz.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Chk_Pz.TabIndex = 15;
            this.Chk_Pz.Text = "正向有功";
            // 
            // Chk_Qz
            // 
            // 
            // 
            // 
            this.checkBoxX2.BackgroundStyle.Class = "";
            this.Chk_Qz.Location = new System.Drawing.Point(423, 55);
            this.Chk_Qz.Name = "Chk_Qz";
            this.Chk_Qz.Size = new System.Drawing.Size(80, 23);
            this.Chk_Qz.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Chk_Qz.TabIndex = 16;
            this.Chk_Qz.Text = "正向无功";
            // 
            // Chk_Pf
            // 
            // 
            // 
            // 
            this.checkBoxX3.BackgroundStyle.Class = "";
            this.Chk_Pf.Location = new System.Drawing.Point(337, 79);
            this.Chk_Pf.Name = "Chk_Pf";
            this.Chk_Pf.Size = new System.Drawing.Size(80, 23);
            this.Chk_Pf.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Chk_Pf.TabIndex = 17;
            this.Chk_Pf.Text = "反向有功";
            // 
            // Chk_Qf
            // 
            // 
            // 
            // 
            this.checkBoxX4.BackgroundStyle.Class = "";
            this.Chk_Qf.Location = new System.Drawing.Point(423, 79);
            this.Chk_Qf.Name = "Chk_Qf";
            this.Chk_Qf.Size = new System.Drawing.Size(80, 23);
            this.Chk_Qf.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.Chk_Qf.TabIndex = 18;
            this.Chk_Qf.Text = "反向无功";
            // 
            // checkBoxX1
            // 
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.Class = "";
            this.checkBoxX1.Location = new System.Drawing.Point(337, 55);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(100, 23);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 15;
            this.checkBoxX1.Text = "checkBoxX1";
            // 
            // checkBoxX3
            // 
            // 
            // 
            // 
            this.checkBoxX3.BackgroundStyle.Class = "";
            this.checkBoxX3.Location = new System.Drawing.Point(337, 79);
            this.checkBoxX3.Name = "checkBoxX3";
            this.checkBoxX3.Size = new System.Drawing.Size(100, 23);
            this.checkBoxX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX3.TabIndex = 17;
            this.checkBoxX3.Text = "checkBoxX3";
            // 
            // checkBoxX2
            // 
            // 
            // 
            // 
            this.checkBoxX2.BackgroundStyle.Class = "";
            this.checkBoxX2.Location = new System.Drawing.Point(432, 55);
            this.checkBoxX2.Name = "checkBoxX2";
            this.checkBoxX2.Size = new System.Drawing.Size(100, 23);
            this.checkBoxX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX2.TabIndex = 16;
            this.checkBoxX2.Text = "checkBoxX2";
            // 
            // checkBoxX4
            // 
            // 
            // 
            // 
            this.checkBoxX4.BackgroundStyle.Class = "";
            this.checkBoxX4.Location = new System.Drawing.Point(432, 79);
            this.checkBoxX4.Name = "checkBoxX4";
            this.checkBoxX4.Size = new System.Drawing.Size(100, 23);
            this.checkBoxX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX4.TabIndex = 18;
            this.checkBoxX4.Text = "checkBoxX4";
            // 
            // DgnSdtq
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Back);
            this.Name = "DgnSdtq";
            this.Size = new System.Drawing.Size(509, 106);
            this.Panel_Back.ResumeLayout(false);
            this.Panel_Back.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CLDC_Comm.ExtendedPanel.ExtendedPanel Panel_Back;
        private DevComponents.DotNetBar.ButtonX Cmd_Remove;
        private DevComponents.DotNetBar.ButtonX Cmd_Insert;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Cmb_FeiLv;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker DTP_SD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ChkAutoCut;
        private System.Windows.Forms.ListBox Lst_SD;
        private DevComponents.DotNetBar.Controls.CheckBoxX Chk_Pz;
        private DevComponents.DotNetBar.Controls.CheckBoxX Chk_Qf;
        private DevComponents.DotNetBar.Controls.CheckBoxX Chk_Pf;
        private DevComponents.DotNetBar.Controls.CheckBoxX Chk_Qz;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX3;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX4;

    }
}

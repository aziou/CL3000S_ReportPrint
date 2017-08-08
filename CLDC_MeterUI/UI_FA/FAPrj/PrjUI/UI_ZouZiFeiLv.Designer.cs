namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI
{
    partial class UI_ZouZiFeiLv
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Cmd_Close = new DevComponents.DotNetBar.ButtonX();
            this.Cmd_Insert = new DevComponents.DotNetBar.ButtonX();
            this.Cmd_Remove = new DevComponents.DotNetBar.ButtonX();
            this.Ltv_FeiLv = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.Col_Pram = new System.Windows.Forms.ColumnHeader();
            this.Cmb_Feilv = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label5 = new System.Windows.Forms.Label();
            this.DTP_StartTime = new System.Windows.Forms.DateTimePicker();
            this.Lab_Dw = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Txt_LongTime = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.Lab_Pram = new System.Windows.Forms.Label();
            this.Chk_NotSet = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.groupBox3.Controls.Add(this.Cmd_Close);
            this.groupBox3.Controls.Add(this.Cmd_Insert);
            this.groupBox3.Controls.Add(this.Cmd_Remove);
            this.groupBox3.Controls.Add(this.Ltv_FeiLv);
            this.groupBox3.Controls.Add(this.Cmb_Feilv);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.DTP_StartTime);
            this.groupBox3.Controls.Add(this.Lab_Dw);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.Txt_LongTime);
            this.groupBox3.Controls.Add(this.Lab_Pram);
            this.groupBox3.Controls.Add(this.Chk_NotSet);
            this.groupBox3.Location = new System.Drawing.Point(2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(228, 224);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "走字内容";
            // 
            // Cmd_Close
            // 
            this.Cmd_Close.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Close.Location = new System.Drawing.Point(6, 197);
            this.Cmd_Close.Name = "Cmd_Close";
            this.Cmd_Close.Size = new System.Drawing.Size(216, 22);
            this.Cmd_Close.TabIndex = 33;
            this.Cmd_Close.Text = "确认关闭(&C)";
            this.Cmd_Close.Click += new System.EventHandler(this.Cmd_Close_Click);
            // 
            // Cmd_Insert
            // 
            this.Cmd_Insert.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Insert.Location = new System.Drawing.Point(192, 158);
            this.Cmd_Insert.Name = "Cmd_Insert";
            this.Cmd_Insert.Size = new System.Drawing.Size(30, 33);
            this.Cmd_Insert.TabIndex = 32;
            this.Cmd_Insert.Text = "插入";
            this.Cmd_Insert.Click += new System.EventHandler(this.Cmd_Insert_Click);
            // 
            // Cmd_Remove
            // 
            this.Cmd_Remove.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Remove.Location = new System.Drawing.Point(192, 120);
            this.Cmd_Remove.Name = "Cmd_Remove";
            this.Cmd_Remove.Size = new System.Drawing.Size(30, 34);
            this.Cmd_Remove.TabIndex = 31;
            this.Cmd_Remove.Text = "移除";
            this.Cmd_Remove.Click += new System.EventHandler(this.Cmd_Remove_Click);
            // 
            // Ltv_FeiLv
            // 
            this.Ltv_FeiLv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Ltv_FeiLv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.Col_Pram});
            this.Ltv_FeiLv.FullRowSelect = true;
            this.Ltv_FeiLv.GridLines = true;
            this.Ltv_FeiLv.Location = new System.Drawing.Point(7, 13);
            this.Ltv_FeiLv.Name = "Ltv_FeiLv";
            this.Ltv_FeiLv.Size = new System.Drawing.Size(215, 105);
            this.Ltv_FeiLv.TabIndex = 30;
            this.Ltv_FeiLv.UseCompatibleStateImageBehavior = false;
            this.Ltv_FeiLv.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "费率";
            this.columnHeader2.Width = 44;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "起始时间";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 70;
            // 
            // Col_Pram
            // 
            this.Col_Pram.Text = "走字时长(分)";
            this.Col_Pram.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Col_Pram.Width = 96;
            // 
            // Cmb_Feilv
            // 
            this.Cmb_Feilv.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Cmb_Feilv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Feilv.FormattingEnabled = true;
            this.Cmb_Feilv.Location = new System.Drawing.Point(66, 124);
            this.Cmb_Feilv.Name = "Cmb_Feilv";
            this.Cmb_Feilv.Size = new System.Drawing.Size(82, 22);
            this.Cmb_Feilv.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "走字费率：";
            // 
            // DTP_StartTime
            // 
            this.DTP_StartTime.CustomFormat = "HH\':\'mm";
            this.DTP_StartTime.Enabled = false;
            this.DTP_StartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DTP_StartTime.Location = new System.Drawing.Point(90, 149);
            this.DTP_StartTime.Name = "DTP_StartTime";
            this.DTP_StartTime.ShowUpDown = true;
            this.DTP_StartTime.Size = new System.Drawing.Size(58, 21);
            this.DTP_StartTime.TabIndex = 26;
            this.DTP_StartTime.Value = new System.DateTime(2009, 1, 13, 0, 0, 0, 0);
            // 
            // Lab_Dw
            // 
            this.Lab_Dw.AutoSize = true;
            this.Lab_Dw.Location = new System.Drawing.Point(148, 178);
            this.Lab_Dw.Name = "Lab_Dw";
            this.Lab_Dw.Size = new System.Drawing.Size(41, 12);
            this.Lab_Dw.TabIndex = 29;
            this.Lab_Dw.Text = "(分钟)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "起始时间：";
            // 
            // Txt_LongTime
            // 
            // 
            // 
            // 
            this.Txt_LongTime.Border.Class = "";
            this.Txt_LongTime.Location = new System.Drawing.Point(66, 173);
            this.Txt_LongTime.Name = "Txt_LongTime";
            this.Txt_LongTime.Size = new System.Drawing.Size(82, 21);
            this.Txt_LongTime.TabIndex = 28;
            // 
            // Lab_Pram
            // 
            this.Lab_Pram.AutoSize = true;
            this.Lab_Pram.Location = new System.Drawing.Point(6, 178);
            this.Lab_Pram.Name = "Lab_Pram";
            this.Lab_Pram.Size = new System.Drawing.Size(65, 12);
            this.Lab_Pram.TabIndex = 25;
            this.Lab_Pram.Text = "走字时长：";
            // 
            // Chk_NotSet
            // 
            this.Chk_NotSet.AutoSize = true;
            // 
            // 
            // 
            this.Chk_NotSet.BackgroundStyle.Class = "";
            this.Chk_NotSet.Location = new System.Drawing.Point(72, 152);
            this.Chk_NotSet.Name = "Chk_NotSet";
            this.Chk_NotSet.Size = new System.Drawing.Size(88, 18);
            this.Chk_NotSet.TabIndex = 27;
            this.Chk_NotSet.Text = "不设表时间";
            this.Chk_NotSet.CheckedChanged += new System.EventHandler(this.Chk_NotSet_CheckedChanged);
            // 
            // UI_ZouZiFeiLv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.DoubleBuffered = true;
            this.Name = "UI_ZouZiFeiLv";
            this.Size = new System.Drawing.Size(233, 230);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private DevComponents.DotNetBar.ButtonX Cmd_Insert;
        private DevComponents.DotNetBar.ButtonX Cmd_Remove;
        private System.Windows.Forms.ListView Ltv_FeiLv;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader Col_Pram;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Cmb_Feilv;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker DTP_StartTime;
        private System.Windows.Forms.Label Lab_Dw;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.TextBoxX Txt_LongTime;
        private System.Windows.Forms.Label Lab_Pram;
        private DevComponents.DotNetBar.Controls.CheckBoxX Chk_NotSet;
        private DevComponents.DotNetBar.ButtonX Cmd_Close;
    }
}

namespace UI.DisplayInfo
{
    partial class DisplayInfo
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.Cmb_MeterList = new System.Windows.Forms.ComboBox();
            this.Chk_AllowEdit = new System.Windows.Forms.CheckBox();
            this.uiMeterInfo = new UI.DisplayInfo.MeterInfo();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.uiMeterInfo, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(628, 375);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Cmb_MeterList, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.Chk_AllowEdit, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(628, 30);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Right;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(245, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "共有{0}只表数据、当前显示";
            // 
            // Cmb_MeterList
            // 
            this.Cmb_MeterList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Cmb_MeterList.FormattingEnabled = true;
            this.Cmb_MeterList.Location = new System.Drawing.Point(461, 6);
            this.Cmb_MeterList.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.Cmb_MeterList.Name = "Cmb_MeterList";
            this.Cmb_MeterList.Size = new System.Drawing.Size(94, 20);
            this.Cmb_MeterList.TabIndex = 1;
            // 
            // Chk_AllowEdit
            // 
            this.Chk_AllowEdit.AutoSize = true;
            this.Chk_AllowEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Chk_AllowEdit.Location = new System.Drawing.Point(561, 8);
            this.Chk_AllowEdit.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.Chk_AllowEdit.Name = "Chk_AllowEdit";
            this.Chk_AllowEdit.Size = new System.Drawing.Size(64, 19);
            this.Chk_AllowEdit.TabIndex = 2;
            this.Chk_AllowEdit.Text = "可修改";
            this.Chk_AllowEdit.UseVisualStyleBackColor = true;
            this.Chk_AllowEdit.Visible = false;
            // 
            // uiMeterInfo
            // 
            this.uiMeterInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiMeterInfo.Location = new System.Drawing.Point(1, 31);
            this.uiMeterInfo.Margin = new System.Windows.Forms.Padding(1);
            this.uiMeterInfo.Name = "uiMeterInfo";
            this.uiMeterInfo.Size = new System.Drawing.Size(626, 343);
            this.uiMeterInfo.TabIndex = 1;
            // 
            // DisplayInfo
            // 
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "DisplayInfo";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Cmb_MeterList;
        private MeterInfo uiMeterInfo;
        private System.Windows.Forms.CheckBox Chk_AllowEdit;
    }
}
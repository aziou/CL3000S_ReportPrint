namespace CLDC_MeterUI.UI_FA.FAPrj
{
    partial class UI_LoadRecord
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnl_Prjs = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.dgv_RunningE = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgv_ModeByte = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_marginTime = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_overTime = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_Title = new System.Windows.Forms.Panel();
            this.pnl_Prjs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_RunningE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ModeByte)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Prjs
            // 
            this.pnl_Prjs.AutoScroll = true;
            this.pnl_Prjs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.pnl_Prjs.Controls.Add(this.label6);
            this.pnl_Prjs.Controls.Add(this.dgv_RunningE);
            this.pnl_Prjs.Controls.Add(this.label5);
            this.pnl_Prjs.Controls.Add(this.label4);
            this.pnl_Prjs.Controls.Add(this.dgv_ModeByte);
            this.pnl_Prjs.Controls.Add(this.label3);
            this.pnl_Prjs.Controls.Add(this.label2);
            this.pnl_Prjs.Controls.Add(this.txt_marginTime);
            this.pnl_Prjs.Controls.Add(this.comboBox1);
            this.pnl_Prjs.Controls.Add(this.label1);
            this.pnl_Prjs.Controls.Add(this.txt_overTime);
            this.pnl_Prjs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Prjs.Location = new System.Drawing.Point(0, 35);
            this.pnl_Prjs.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_Prjs.Name = "pnl_Prjs";
            this.pnl_Prjs.Size = new System.Drawing.Size(616, 400);
            this.pnl_Prjs.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "工况配置：";
            // 
            // dgv_RunningE
            // 
            this.dgv_RunningE.AllowUserToAddRows = false;
            this.dgv_RunningE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_RunningE.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12});
            this.dgv_RunningE.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_RunningE.Location = new System.Drawing.Point(30, 197);
            this.dgv_RunningE.Name = "dgv_RunningE";
            this.dgv_RunningE.RowHeadersVisible = false;
            this.dgv_RunningE.RowTemplate.Height = 23;
            this.dgv_RunningE.Size = new System.Drawing.Size(500, 200);
            this.dgv_RunningE.TabIndex = 9;
            this.dgv_RunningE.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RunningE_CellLeave);
            this.dgv_RunningE.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_RunningE_CellClick);
            this.dgv_RunningE.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_RunningE_DataError);
            // 
            // Column7
            // 
            this.Column7.HeaderText = "序号";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 55;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "功率类型";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "电流倍数";
            this.Column9.Name = "Column9";
            this.Column9.Width = 80;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "功率因数";
            this.Column10.Name = "Column10";
            this.Column10.Width = 80;
            // 
            // Column11
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column11.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column11.HeaderText = "运行时间(分)";
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "操作";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 80;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(257, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "注：0代表不记录此类数据，1代表记录此类数据";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "负荷记录模式字：";
            // 
            // dgv_ModeByte
            // 
            this.dgv_ModeByte.AllowUserToAddRows = false;
            this.dgv_ModeByte.AllowUserToDeleteRows = false;
            this.dgv_ModeByte.ColumnHeadersHeight = 35;
            this.dgv_ModeByte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_ModeByte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6});
            this.dgv_ModeByte.Location = new System.Drawing.Point(30, 72);
            this.dgv_ModeByte.Name = "dgv_ModeByte";
            this.dgv_ModeByte.RowHeadersVisible = false;
            this.dgv_ModeByte.RowHeadersWidth = 45;
            this.dgv_ModeByte.RowTemplate.Height = 23;
            this.dgv_ModeByte.Size = new System.Drawing.Size(500, 67);
            this.dgv_ModeByte.TabIndex = 6;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column1.HeaderText = "当前需量";
            this.Column1.Name = "Column1";
            this.Column1.Width = 61;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column2.HeaderText = "四象限无功总电能";
            this.Column2.Name = "Column2";
            this.Column2.Width = 83;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column3.HeaderText = "有、无功总电能";
            this.Column3.Name = "Column3";
            this.Column3.Width = 83;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column4.HeaderText = "功率因数";
            this.Column4.Name = "Column4";
            this.Column4.Width = 61;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column5.HeaderText = "有、无功功率";
            this.Column5.Name = "Column5";
            this.Column5.Width = 72;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column6.HeaderText = "电压、电流、频率";
            this.Column6.Name = "Column6";
            this.Column6.Width = 94;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(361, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "分";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "间隔时间";
            // 
            // txt_marginTime
            // 
            this.txt_marginTime.Location = new System.Drawing.Point(313, 18);
            this.txt_marginTime.Name = "txt_marginTime";
            this.txt_marginTime.Size = new System.Drawing.Size(45, 21);
            this.txt_marginTime.TabIndex = 3;
            this.txt_marginTime.Text = "15";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "分钟",
            "小时",
            "天"});
            this.comboBox1.Location = new System.Drawing.Point(136, 18);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(60, 20);
            this.comboBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "运行时长";
            // 
            // txt_overTime
            // 
            this.txt_overTime.Location = new System.Drawing.Point(74, 18);
            this.txt_overTime.Name = "txt_overTime";
            this.txt_overTime.Size = new System.Drawing.Size(61, 21);
            this.txt_overTime.TabIndex = 0;
            this.txt_overTime.Text = "45";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pnl_Prjs, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnl_Title, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(616, 435);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pnl_Title
            // 
            this.pnl_Title.AutoScroll = true;
            this.pnl_Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.pnl_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Title.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnl_Title.Location = new System.Drawing.Point(0, 0);
            this.pnl_Title.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_Title.Name = "pnl_Title";
            this.pnl_Title.Size = new System.Drawing.Size(616, 35);
            this.pnl_Title.TabIndex = 6;
            // 
            // UI_LoadRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UI_LoadRecord";
            this.pnl_Prjs.ResumeLayout(false);
            this.pnl_Prjs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_RunningE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ModeByte)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Prjs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl_Title;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_overTime;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_marginTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgv_ModeByte;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgv_RunningE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column8;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column9;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
    }
}

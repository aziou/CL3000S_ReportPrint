namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    partial class CheckFunction
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Tab_Function = new System.Windows.Forms.TabControl();
            this.Pag_Result = new System.Windows.Forms.TabPage();
            this.Data_Dgn = new System.Windows.Forms.DataGridView();
            this.DatCol_1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.表位号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatCol_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatCol_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatCol_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel2.SuspendLayout();
            this.Tab_Function.SuspendLayout();
            this.Pag_Result.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Data_Dgn)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.Tab_Function, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(738, 354);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // Tab_Function
            // 
            this.Tab_Function.Controls.Add(this.Pag_Result);
            this.Tab_Function.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab_Function.Location = new System.Drawing.Point(0, 0);
            this.Tab_Function.Margin = new System.Windows.Forms.Padding(0);
            this.Tab_Function.Name = "Tab_Function";
            this.Tab_Function.SelectedIndex = 0;
            this.Tab_Function.Size = new System.Drawing.Size(738, 354);
            this.Tab_Function.TabIndex = 5;
            // 
            // Pag_Result
            // 
            this.Pag_Result.Controls.Add(this.Data_Dgn);
            this.Pag_Result.Location = new System.Drawing.Point(4, 21);
            this.Pag_Result.Name = "Pag_Result";
            this.Pag_Result.Padding = new System.Windows.Forms.Padding(3);
            this.Pag_Result.Size = new System.Drawing.Size(730, 329);
            this.Pag_Result.TabIndex = 0;
            this.Pag_Result.Text = "检定信息";
            this.Pag_Result.UseVisualStyleBackColor = true;
            // 
            // Data_Dgn
            // 
            this.Data_Dgn.AllowUserToAddRows = false;
            this.Data_Dgn.AllowUserToDeleteRows = false;
            this.Data_Dgn.AllowUserToResizeRows = false;
            this.Data_Dgn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Data_Dgn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Data_Dgn.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Data_Dgn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Data_Dgn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DatCol_1,
            this.表位号,
            this.DatCol_2,
            this.DatCol_3,
            this.DatCol_5});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Data_Dgn.DefaultCellStyle = dataGridViewCellStyle5;
            this.Data_Dgn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Data_Dgn.Location = new System.Drawing.Point(3, 3);
            this.Data_Dgn.Margin = new System.Windows.Forms.Padding(0);
            this.Data_Dgn.MultiSelect = false;
            this.Data_Dgn.Name = "Data_Dgn";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Data_Dgn.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Data_Dgn.RowHeadersVisible = false;
            this.Data_Dgn.RowHeadersWidth = 80;
            this.Data_Dgn.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Data_Dgn.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.Data_Dgn.RowTemplate.Height = 23;
            this.Data_Dgn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Data_Dgn.Size = new System.Drawing.Size(724, 323);
            this.Data_Dgn.TabIndex = 2;
            this.Data_Dgn.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Data_Dgn_CellMouseUp);
            this.Data_Dgn.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Data_Dgn_CellDoubleClick);
            this.Data_Dgn.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Data_Dgn_CellClick);
            this.Data_Dgn.SelectionChanged += new System.EventHandler(this.Data_Dgn_SelectionChanged);
            // 
            // DatCol_1
            // 
            this.DatCol_1.FillWeight = 18F;
            this.DatCol_1.HeaderText = "要检";
            this.DatCol_1.Name = "DatCol_1";
            this.DatCol_1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DatCol_1.Width = 40;
            // 
            // 表位号
            // 
            this.表位号.HeaderText = "表位号";
            this.表位号.Name = "表位号";
            this.表位号.ReadOnly = true;
            this.表位号.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.表位号.Width = 55;
            // 
            // DatCol_2
            // 
            this.DatCol_2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DatCol_2.DefaultCellStyle = dataGridViewCellStyle2;
            this.DatCol_2.FillWeight = 40F;
            this.DatCol_2.HeaderText = "当前检定项目";
            this.DatCol_2.Name = "DatCol_2";
            this.DatCol_2.ReadOnly = true;
            this.DatCol_2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DatCol_3
            // 
            this.DatCol_3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DatCol_3.DefaultCellStyle = dataGridViewCellStyle3;
            this.DatCol_3.FillWeight = 35F;
            this.DatCol_3.HeaderText = "检定进度";
            this.DatCol_3.Name = "DatCol_3";
            this.DatCol_3.ReadOnly = true;
            this.DatCol_3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DatCol_5
            // 
            this.DatCol_5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DatCol_5.DefaultCellStyle = dataGridViewCellStyle4;
            this.DatCol_5.FillWeight = 18F;
            this.DatCol_5.HeaderText = "是否合格";
            this.DatCol_5.Name = "DatCol_5";
            this.DatCol_5.ReadOnly = true;
            this.DatCol_5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CheckFunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "CheckFunction";
            this.Size = new System.Drawing.Size(738, 354);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.Tab_Function.ResumeLayout(false);
            this.Pag_Result.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Data_Dgn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

        private System.Windows.Forms.TabControl Tab_Function;
        private System.Windows.Forms.TabPage Pag_Result;
        private System.Windows.Forms.DataGridView Data_Dgn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DatCol_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 表位号;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_3;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_5;
    }
}

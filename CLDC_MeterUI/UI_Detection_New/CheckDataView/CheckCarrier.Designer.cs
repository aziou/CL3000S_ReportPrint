namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    partial class CheckCarrier
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Tab_Carrier = new System.Windows.Forms.TabControl();
            this.Pag_Result = new System.Windows.Forms.TabPage();
            this.Data_Carrier = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.DatCol_1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.表位号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatCol_2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatCol_3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DatCol_5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tab_Carrier.SuspendLayout();
            this.Pag_Result.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Data_Carrier)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tab_Carrier
            // 
            this.Tab_Carrier.Controls.Add(this.Pag_Result);
            this.Tab_Carrier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tab_Carrier.Location = new System.Drawing.Point(0, 0);
            this.Tab_Carrier.Margin = new System.Windows.Forms.Padding(0);
            this.Tab_Carrier.Name = "Tab_Carrier";
            this.Tab_Carrier.SelectedIndex = 0;
            this.Tab_Carrier.Size = new System.Drawing.Size(631, 427);
            this.Tab_Carrier.TabIndex = 5;
            // 
            // Pag_Result
            // 
            this.Pag_Result.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Pag_Result.Controls.Add(this.Data_Carrier);
            this.Pag_Result.Location = new System.Drawing.Point(4, 21);
            this.Pag_Result.Margin = new System.Windows.Forms.Padding(0);
            this.Pag_Result.Name = "Pag_Result";
            this.Pag_Result.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Pag_Result.Size = new System.Drawing.Size(623, 402);
            this.Pag_Result.TabIndex = 0;
            this.Pag_Result.Text = "检定信息";
            // 
            // Data_Carrier
            // 
            this.Data_Carrier.AllowUserToAddRows = false;
            this.Data_Carrier.AllowUserToDeleteRows = false;
            this.Data_Carrier.AllowUserToResizeRows = false;
            this.Data_Carrier.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Data_Carrier.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Data_Carrier.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Data_Carrier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Data_Carrier.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Data_Carrier.DefaultCellStyle = dataGridViewCellStyle5;
            this.Data_Carrier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Data_Carrier.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.Data_Carrier.Location = new System.Drawing.Point(3, 3);
            this.Data_Carrier.Margin = new System.Windows.Forms.Padding(0);
            this.Data_Carrier.MultiSelect = false;
            this.Data_Carrier.Name = "Data_Carrier";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Data_Carrier.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Data_Carrier.RowHeadersVisible = false;
            this.Data_Carrier.RowHeadersWidth = 80;
            this.Data_Carrier.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Data_Carrier.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.Data_Carrier.RowTemplate.Height = 23;
            this.Data_Carrier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Data_Carrier.Size = new System.Drawing.Size(617, 399);
            this.Data_Carrier.TabIndex = 2;
            // 
            // DatCol_1
            // 
            this.DatCol_1.FillWeight = 18F;
            this.DatCol_1.HeaderText = "要检";
            this.DatCol_1.Name = "DatCol_1";
            this.DatCol_1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DatCol_1.Width = 40;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.Tab_Carrier, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(631, 427);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "表位号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 55;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn2.FillWeight = 40F;
            this.dataGridViewTextBoxColumn2.HeaderText = "当前检定项目";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn3.FillWeight = 35F;
            this.dataGridViewTextBoxColumn3.HeaderText = "检定进度";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn4.FillWeight = 18F;
            this.dataGridViewTextBoxColumn4.HeaderText = "是否合格";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // CheckCarrier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "CheckCarrier";
            this.Size = new System.Drawing.Size(631, 427);
            this.Tab_Carrier.ResumeLayout(false);
            this.Pag_Result.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Data_Carrier)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_3;
        private System.Windows.Forms.TabControl Tab_Carrier;
        private System.Windows.Forms.TabPage Pag_Result;
        private DevComponents.DotNetBar.Controls.DataGridViewX Data_Carrier;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DatCol_1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 表位号;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DatCol_5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

    }
}

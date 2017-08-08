namespace CLDC_MeterUI.DisplayInfo
{
    partial class DisplayQiQianDong
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Dgv_PrjData = new System.Windows.Forms.DataGridView();
            this.Column1 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.Col_Dl = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.Col_Time = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.Column2 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.Col_Result = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_PrjData)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgv_PrjData
            // 
            this.Dgv_PrjData.AllowUserToAddRows = false;
            this.Dgv_PrjData.AllowUserToDeleteRows = false;
            this.Dgv_PrjData.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Dgv_PrjData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgv_PrjData.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_PrjData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Dgv_PrjData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgv_PrjData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Col_Dl,
            this.Col_Time,
            this.Column2,
            this.Col_Result});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_PrjData.DefaultCellStyle = dataGridViewCellStyle3;
            this.Dgv_PrjData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgv_PrjData.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Dgv_PrjData.Location = new System.Drawing.Point(0, 0);
            this.Dgv_PrjData.MultiSelect = false;
            this.Dgv_PrjData.Name = "Dgv_PrjData";
            this.Dgv_PrjData.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_PrjData.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.Dgv_PrjData.RowHeadersWidth = 220;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgv_PrjData.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.Dgv_PrjData.RowTemplate.Height = 23;
            this.Dgv_PrjData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Dgv_PrjData.Size = new System.Drawing.Size(653, 400);
            this.Dgv_PrjData.TabIndex = 0;
            this.Dgv_PrjData.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_PrjData_CellEndEdit);
            this.Dgv_PrjData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Dgv_PrjData_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "表位";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Col_Dl
            // 
            this.Col_Dl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Col_Dl.FillWeight = 33F;
            this.Col_Dl.HeaderText = "电流（A）";
            this.Col_Dl.Name = "Col_Dl";
            this.Col_Dl.ReadOnly = true;
            this.Col_Dl.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Col_Dl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Col_Time
            // 
            this.Col_Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Col_Time.FillWeight = 33F;
            this.Col_Time.HeaderText = "试验规程时间";
            this.Col_Time.Name = "Col_Time";
            this.Col_Time.ReadOnly = true;
            this.Col_Time.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Col_Time.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "试验实际时间";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Col_Result
            // 
            this.Col_Result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Col_Result.FillWeight = 34F;
            this.Col_Result.HeaderText = "结论";
            this.Col_Result.Name = "Col_Result";
            this.Col_Result.ReadOnly = true;
            this.Col_Result.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Col_Result.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DisplayQiQianDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Dgv_PrjData);
            this.Name = "DisplayQiQianDong";
            this.Size = new System.Drawing.Size(653, 400);
            ((System.ComponentModel.ISupportInitialize)(this.Dgv_PrjData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Dgv_PrjData;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx Column1;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx Col_Dl;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx Col_Time;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx Column2;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx Col_Result;
    }
}

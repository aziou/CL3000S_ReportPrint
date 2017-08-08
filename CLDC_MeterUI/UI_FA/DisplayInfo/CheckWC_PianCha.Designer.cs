namespace UI.DisplayInfo
{
    partial class CheckWC_PianCha
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Grid_Main = new UI.CLZDataGridView.DataGrid();
            this.功率方向 = new UI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.功率元件 = new UI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.功率因数 = new UI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.负载电流 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.化整值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.原始值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Main)).BeginInit();
            this.SuspendLayout();
            // 
            // Grid_Main
            // 
            this.Grid_Main.AllowUserToAddRows = false;
            this.Grid_Main.AllowUserToDeleteRows = false;
            this.Grid_Main.AllowUserToResizeColumns = false;
            this.Grid_Main.AllowUserToResizeRows = false;
            this.Grid_Main.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grid_Main.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Grid_Main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid_Main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.功率方向,
            this.功率元件,
            this.功率因数,
            this.负载电流,
            this.化整值,
            this.原始值});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grid_Main.DefaultCellStyle = dataGridViewCellStyle8;
            this.Grid_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_Main.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Grid_Main.Location = new System.Drawing.Point(0, 0);
            this.Grid_Main.MultiSelect = false;
            this.Grid_Main.Name = "Grid_Main";
            this.Grid_Main.ReadOnly = true;
            this.Grid_Main.RowHeadersVisible = false;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grid_Main.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.Grid_Main.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Grid_Main.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grid_Main.RowTemplate.Height = 23;
            this.Grid_Main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Grid_Main.Size = new System.Drawing.Size(361, 252);
            this.Grid_Main.TabIndex = 1;
            // 
            // 功率方向
            // 
            this.功率方向.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.功率方向.DefaultCellStyle = dataGridViewCellStyle2;
            this.功率方向.FillWeight = 10F;
            this.功率方向.HeaderText = "功率方向";
            this.功率方向.Name = "功率方向";
            this.功率方向.ReadOnly = true;
            this.功率方向.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 功率元件
            // 
            this.功率元件.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.功率元件.DefaultCellStyle = dataGridViewCellStyle3;
            this.功率元件.FillWeight = 10F;
            this.功率元件.HeaderText = "功率元件";
            this.功率元件.Name = "功率元件";
            this.功率元件.ReadOnly = true;
            this.功率元件.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 功率因数
            // 
            this.功率因数.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.功率因数.DefaultCellStyle = dataGridViewCellStyle4;
            this.功率因数.FillWeight = 10F;
            this.功率因数.HeaderText = "功率因数";
            this.功率因数.Name = "功率因数";
            this.功率因数.ReadOnly = true;
            this.功率因数.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 负载电流
            // 
            this.负载电流.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.负载电流.DefaultCellStyle = dataGridViewCellStyle5;
            this.负载电流.FillWeight = 10F;
            this.负载电流.HeaderText = "负载电流";
            this.负载电流.Name = "负载电流";
            this.负载电流.ReadOnly = true;
            this.负载电流.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 化整值
            // 
            this.化整值.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.化整值.DefaultCellStyle = dataGridViewCellStyle6;
            this.化整值.FillWeight = 5F;
            this.化整值.HeaderText = "化整值";
            this.化整值.Name = "化整值";
            this.化整值.ReadOnly = true;
            this.化整值.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // 原始值
            // 
            this.原始值.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.原始值.DefaultCellStyle = dataGridViewCellStyle7;
            this.原始值.FillWeight = 5F;
            this.原始值.HeaderText = "原始值";
            this.原始值.Name = "原始值";
            this.原始值.ReadOnly = true;
            this.原始值.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CheckWC_PianCha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Grid_Main);
            this.Name = "CheckWC_PianCha";
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.CLZDataGridView.DataGrid Grid_Main;
        private UI.CLZDataGridView.DataGridViewTextBoxColumnEx 功率方向;
        private UI.CLZDataGridView.DataGridViewTextBoxColumnEx 功率元件;
        private UI.CLZDataGridView.DataGridViewTextBoxColumnEx 功率因数;
        private System.Windows.Forms.DataGridViewTextBoxColumn 负载电流;
        private System.Windows.Forms.DataGridViewTextBoxColumn 化整值;
        private System.Windows.Forms.DataGridViewTextBoxColumn 原始值;

    }
}

namespace CLDC_MeterUI.DisplayInfo
{
    partial class DisplayEventLog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Dgw_Data = new System.Windows.Forms.DataGridView();
            this.表位 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.项目名称 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.项目结论 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            ((System.ComponentModel.ISupportInitialize)(this.Dgw_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // Dgw_Data
            // 
            this.Dgw_Data.AllowUserToAddRows = false;
            this.Dgw_Data.AllowUserToDeleteRows = false;
            this.Dgw_Data.AllowUserToResizeColumns = false;
            this.Dgw_Data.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Dgw_Data.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Dgw_Data.BackgroundColor = System.Drawing.SystemColors.Control;
            this.Dgw_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Dgw_Data.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Dgw_Data.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Dgw_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Dgw_Data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.表位,
            this.项目名称,
            this.项目结论});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Dgw_Data.DefaultCellStyle = dataGridViewCellStyle5;
            this.Dgw_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Dgw_Data.Location = new System.Drawing.Point(0, 0);
            this.Dgw_Data.Name = "Dgw_Data";
            this.Dgw_Data.ReadOnly = true;
            this.Dgw_Data.RowHeadersVisible = false;
            this.Dgw_Data.RowTemplate.Height = 23;
            this.Dgw_Data.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Dgw_Data.Size = new System.Drawing.Size(361, 252);
            this.Dgw_Data.TabIndex = 1;
            // 
            // 表位
            // 
            this.表位.HeaderText = "表位";
            this.表位.Name = "表位";
            this.表位.ReadOnly = true;
            // 
            // 项目名称
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.项目名称.DefaultCellStyle = dataGridViewCellStyle3;
            this.项目名称.FillWeight = 10F;
            this.项目名称.HeaderText = "项目名称";
            this.项目名称.Name = "项目名称";
            this.项目名称.ReadOnly = true;
            this.项目名称.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.项目名称.Width = 120;
            // 
            // 项目结论
            // 
            this.项目结论.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.项目结论.DefaultCellStyle = dataGridViewCellStyle4;
            this.项目结论.HeaderText = "项目结论";
            this.项目结论.Name = "项目结论";
            this.项目结论.ReadOnly = true;
            this.项目结论.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DisplayEventLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Dgw_Data);
            this.Name = "DisplayEventLog";
            ((System.ComponentModel.ISupportInitialize)(this.Dgw_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Dgw_Data;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 表位;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 项目名称;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 项目结论;
    }
}

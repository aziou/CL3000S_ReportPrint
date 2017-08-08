namespace CLDC_MeterUI.DisplayInfo
{
    partial class DisplayZZ
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
            this.Grid_Main = new CLDC_MeterUI.CLZDataGridView.DataGrid();
            this.表位 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.功率方向 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.功率元件 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.功率因数 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.负载电流 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.费率 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.起码 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.止码 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.表码差 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.表脉冲 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.标准表脉冲 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.误差 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            this.结论 = new CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx();
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Main)).BeginInit();
            this.SuspendLayout();
            // 
            // Grid_Main
            // 
            this.Grid_Main.AllowUserToAddRows = false;
            this.Grid_Main.AllowUserToDeleteRows = false;
            this.Grid_Main.AllowUserToResizeColumns = false;
            this.Grid_Main.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Grid_Main.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Grid_Main.BackgroundColor = System.Drawing.SystemColors.Control;
            this.Grid_Main.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grid_Main.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Grid_Main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Grid_Main.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.表位,
            this.功率方向,
            this.功率元件,
            this.功率因数,
            this.负载电流,
            this.费率,
            this.起码,
            this.止码,
            this.表码差,
            this.表脉冲,
            this.标准表脉冲,
            this.误差,
            this.结论});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grid_Main.DefaultCellStyle = dataGridViewCellStyle3;
            this.Grid_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Grid_Main.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.Grid_Main.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.Grid_Main.Location = new System.Drawing.Point(0, 0);
            this.Grid_Main.MultiSelect = false;
            this.Grid_Main.Name = "Grid_Main";
            this.Grid_Main.ReadOnly = true;
            this.Grid_Main.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Grid_Main.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Grid_Main.RowTemplate.Height = 23;
            this.Grid_Main.Size = new System.Drawing.Size(645, 252);
            this.Grid_Main.TabIndex = 0;
            // 
            // 表位
            // 
            this.表位.HeaderText = "表位";
            this.表位.Name = "表位";
            this.表位.ReadOnly = true;
            // 
            // 功率方向
            // 
            this.功率方向.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.功率方向.FillWeight = 32.78036F;
            this.功率方向.HeaderText = "功率方向";
            this.功率方向.Name = "功率方向";
            this.功率方向.ReadOnly = true;
            // 
            // 功率元件
            // 
            this.功率元件.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.功率元件.FillWeight = 31.77523F;
            this.功率元件.HeaderText = "功率元件";
            this.功率元件.Name = "功率元件";
            this.功率元件.ReadOnly = true;
            // 
            // 功率因数
            // 
            this.功率因数.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.功率因数.FillWeight = 14.35323F;
            this.功率因数.HeaderText = "功率因数";
            this.功率因数.Name = "功率因数";
            this.功率因数.ReadOnly = true;
            // 
            // 负载电流
            // 
            this.负载电流.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.负载电流.FillWeight = 14.35323F;
            this.负载电流.HeaderText = "负载电流";
            this.负载电流.Name = "负载电流";
            this.负载电流.ReadOnly = true;
            // 
            // 费率
            // 
            this.费率.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.费率.FillWeight = 14.35323F;
            this.费率.HeaderText = "费率";
            this.费率.Name = "费率";
            this.费率.ReadOnly = true;
            // 
            // 起码
            // 
            this.起码.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.起码.FillWeight = 14.35323F;
            this.起码.HeaderText = "起码";
            this.起码.Name = "起码";
            this.起码.ReadOnly = true;
            // 
            // 止码
            // 
            this.止码.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.止码.FillWeight = 14.35323F;
            this.止码.HeaderText = "止码";
            this.止码.Name = "止码";
            this.止码.ReadOnly = true;
            // 
            // 表码差
            // 
            this.表码差.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.表码差.FillWeight = 14.35323F;
            this.表码差.HeaderText = "表码差";
            this.表码差.Name = "表码差";
            this.表码差.ReadOnly = true;
            // 
            // 表脉冲
            // 
            this.表脉冲.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.表脉冲.FillWeight = 14.35323F;
            this.表脉冲.HeaderText = "表脉冲";
            this.表脉冲.Name = "表脉冲";
            this.表脉冲.ReadOnly = true;
            // 
            // 标准表脉冲
            // 
            this.标准表脉冲.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.标准表脉冲.FillWeight = 20.61856F;
            this.标准表脉冲.HeaderText = "标准表脉冲";
            this.标准表脉冲.Name = "标准表脉冲";
            this.标准表脉冲.ReadOnly = true;
            // 
            // 误差
            // 
            this.误差.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.误差.FillWeight = 14.35323F;
            this.误差.HeaderText = "误差";
            this.误差.Name = "误差";
            this.误差.ReadOnly = true;
            // 
            // 结论
            // 
            this.结论.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.结论.FillWeight = 14.35323F;
            this.结论.HeaderText = "结论";
            this.结论.Name = "结论";
            this.结论.ReadOnly = true;
            // 
            // DisplayZZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Grid_Main);
            this.Name = "DisplayZZ";
            this.Size = new System.Drawing.Size(645, 252);
            ((System.ComponentModel.ISupportInitialize)(this.Grid_Main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CLDC_MeterUI.CLZDataGridView.DataGrid Grid_Main;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 表位;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 功率方向;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 功率元件;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 功率因数;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 负载电流;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 费率;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 起码;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 止码;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 表码差;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 表脉冲;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 标准表脉冲;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 误差;
        private CLDC_MeterUI.CLZDataGridView.DataGridViewTextBoxColumnEx 结论;

    }
}

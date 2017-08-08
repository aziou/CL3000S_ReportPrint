namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI
{
    partial class WcFaSetup
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
            this.DGW_FA = new DevComponents.DotNetBar.Controls.DataGridViewX();
            ((System.ComponentModel.ISupportInitialize)(this.DGW_FA)).BeginInit();
            this.SuspendLayout();
            // 
            // DGW_FA
            // 
            this.DGW_FA.AllowUserToAddRows = false;
            this.DGW_FA.AllowUserToDeleteRows = false;
            this.DGW_FA.AllowUserToResizeColumns = false;
            this.DGW_FA.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGW_FA.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGW_FA.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.DGW_FA.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DGW_FA.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGW_FA.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGW_FA.ColumnHeadersHeight = 33;
            this.DGW_FA.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGW_FA.DefaultCellStyle = dataGridViewCellStyle3;
            this.DGW_FA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGW_FA.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DGW_FA.EnableHeadersVisualStyles = false;
            this.DGW_FA.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.DGW_FA.Location = new System.Drawing.Point(0, 0);
            this.DGW_FA.MultiSelect = false;
            this.DGW_FA.Name = "DGW_FA";
            this.DGW_FA.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGW_FA.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DGW_FA.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGW_FA.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DGW_FA.RowTemplate.Height = 23;
            this.DGW_FA.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGW_FA.Size = new System.Drawing.Size(539, 326);
            this.DGW_FA.TabIndex = 2;
            this.DGW_FA.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGW_FA_CellMouseClick);
            this.DGW_FA.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DGW_FA_CellPainting);
            this.DGW_FA.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGW_FA_CellClick);
            // 
            // WcFaSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DGW_FA);
            this.Name = "WcFaSetup";
            this.Size = new System.Drawing.Size(539, 326);
            ((System.ComponentModel.ISupportInitialize)(this.DGW_FA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX DGW_FA;
    }
}

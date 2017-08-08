namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI
{
    partial class WcLimitSetup
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
            this.DGW_WcLimit = new DevComponents.DotNetBar.Controls.DataGridViewX();
            ((System.ComponentModel.ISupportInitialize)(this.DGW_WcLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // DGW_WcLimit
            // 
            this.DGW_WcLimit.AllowUserToAddRows = false;
            this.DGW_WcLimit.AllowUserToDeleteRows = false;
            this.DGW_WcLimit.AllowUserToResizeColumns = false;
            this.DGW_WcLimit.AllowUserToResizeRows = false;
            this.DGW_WcLimit.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.DGW_WcLimit.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGW_WcLimit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGW_WcLimit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGW_WcLimit.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGW_WcLimit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGW_WcLimit.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.DGW_WcLimit.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.DGW_WcLimit.Location = new System.Drawing.Point(0, 0);
            this.DGW_WcLimit.Margin = new System.Windows.Forms.Padding(0);
            this.DGW_WcLimit.Name = "DGW_WcLimit";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGW_WcLimit.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGW_WcLimit.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.DGW_WcLimit.RowTemplate.Height = 23;
            this.DGW_WcLimit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGW_WcLimit.Size = new System.Drawing.Size(581, 350);
            this.DGW_WcLimit.TabIndex = 0;
            this.DGW_WcLimit.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGW_WcLimit_CellValueChanged);
            this.DGW_WcLimit.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DGW_WcLimit_CellBeginEdit);
            this.DGW_WcLimit.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGW_WcLimit_CellEndEdit);
            this.DGW_WcLimit.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGW_WcLimit_CellClick);
            // 
            // WcLimitSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DGW_WcLimit);
            this.Name = "WcLimitSetup";
            this.Size = new System.Drawing.Size(581, 350);
            ((System.ComponentModel.ISupportInitialize)(this.DGW_WcLimit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX DGW_WcLimit;




    }
}

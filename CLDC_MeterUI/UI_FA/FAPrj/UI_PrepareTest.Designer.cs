namespace CLDC_MeterUI.UI_FA.FAPrj
{
    partial class UI_PrepareTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_PrepareTest));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Panel_Prjs = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.Cmd_Clear = new DevComponents.DotNetBar.ButtonX();
            this.Cmb_Fa = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Panel_Prjs, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel9, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(644, 185);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // Panel_Prjs
            // 
            this.Panel_Prjs.AutoScroll = true;
            this.Panel_Prjs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Panel_Prjs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Prjs.Location = new System.Drawing.Point(0, 35);
            this.Panel_Prjs.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Prjs.Name = "Panel_Prjs";
            this.Panel_Prjs.Size = new System.Drawing.Size(644, 150);
            this.Panel_Prjs.TabIndex = 5;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.panel9.Controls.Add(this.Cmd_Clear);
            this.panel9.Controls.Add(this.Cmb_Fa);
            this.panel9.Controls.Add(this.label9);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Margin = new System.Windows.Forms.Padding(0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(644, 35);
            this.panel9.TabIndex = 4;
            // 
            // Cmd_Clear
            // 
            this.Cmd_Clear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Clear.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_Clear.Image")));
            this.Cmd_Clear.Location = new System.Drawing.Point(374, 6);
            this.Cmd_Clear.Name = "Cmd_Clear";
            this.Cmd_Clear.Size = new System.Drawing.Size(124, 23);
            this.Cmd_Clear.TabIndex = 2;
            this.Cmd_Clear.Text = "清理所有项目";
            // 
            // Cmb_Fa
            // 
            this.Cmb_Fa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Cmb_Fa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Fa.FormattingEnabled = true;
            this.Cmb_Fa.Location = new System.Drawing.Point(150, 8);
            this.Cmb_Fa.Name = "Cmb_Fa";
            this.Cmb_Fa.Size = new System.Drawing.Size(218, 22);
            this.Cmb_Fa.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(149, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "可选择的多功能试验方案：";
            // 
            // UI_PrepareTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UI_PrepareTest";
            this.Size = new System.Drawing.Size(644, 185);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel Panel_Prjs;
        private System.Windows.Forms.Panel panel9;
        private DevComponents.DotNetBar.ButtonX Cmd_Clear;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Cmb_Fa;
        private System.Windows.Forms.Label label9;
    }
}

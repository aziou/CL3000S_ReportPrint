namespace CLDC_MeterUI.UI_FA.FAPrj
{
    partial class UI_Special
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_Special));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Cmd_Clear = new DevComponents.DotNetBar.ButtonX();
            this.Cmb_Fa = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Panel_Items = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Cmd_ShowXieBo = new DevComponents.DotNetBar.ButtonX();
            this.Cmd_AddNew = new DevComponents.DotNetBar.ButtonX();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.panel1.Controls.Add(this.Cmd_Clear);
            this.panel1.Controls.Add(this.Cmb_Fa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(717, 35);
            this.panel1.TabIndex = 0;
            // 
            // Cmd_Clear
            // 
            this.Cmd_Clear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_Clear.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_Clear.Image")));
            this.Cmd_Clear.Location = new System.Drawing.Point(310, 6);
            this.Cmd_Clear.Name = "Cmd_Clear";
            this.Cmd_Clear.Size = new System.Drawing.Size(112, 23);
            this.Cmd_Clear.TabIndex = 2;
            this.Cmd_Clear.Text = "清理所有项目";
            // 
            // Cmb_Fa
            // 
            this.Cmb_Fa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Cmb_Fa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Fa.FormattingEnabled = true;
            this.Cmb_Fa.Location = new System.Drawing.Point(135, 8);
            this.Cmb_Fa.Name = "Cmb_Fa";
            this.Cmb_Fa.Size = new System.Drawing.Size(168, 22);
            this.Cmb_Fa.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "可选择的特殊检定方案：";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(717, 431);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Controls.Add(this.Panel_Items, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 396F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(717, 396);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // Panel_Items
            // 
            this.Panel_Items.AutoScroll = true;
            this.Panel_Items.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.Panel_Items.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Items.Location = new System.Drawing.Point(0, 0);
            this.Panel_Items.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Items.Name = "Panel_Items";
            this.Panel_Items.Size = new System.Drawing.Size(687, 396);
            this.Panel_Items.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.panel2.Controls.Add(this.Cmd_ShowXieBo);
            this.panel2.Controls.Add(this.Cmd_AddNew);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(687, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(30, 396);
            this.panel2.TabIndex = 1;
            // 
            // Cmd_ShowXieBo
            // 
            this.Cmd_ShowXieBo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_ShowXieBo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Cmd_ShowXieBo.Location = new System.Drawing.Point(4, 84);
            this.Cmd_ShowXieBo.Name = "Cmd_ShowXieBo";
            this.Cmd_ShowXieBo.Size = new System.Drawing.Size(23, 91);
            this.Cmd_ShowXieBo.TabIndex = 1;
            this.Cmd_ShowXieBo.Text = "查看谐波方案";
            // 
            // Cmd_AddNew
            // 
            this.Cmd_AddNew.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Cmd_AddNew.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Cmd_AddNew.Location = new System.Drawing.Point(4, 3);
            this.Cmd_AddNew.Name = "Cmd_AddNew";
            this.Cmd_AddNew.Size = new System.Drawing.Size(23, 77);
            this.Cmd_AddNew.TabIndex = 0;
            this.Cmd_AddNew.Text = "添加项目";
            // 
            // UI_Special
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UI_Special";
            this.Size = new System.Drawing.Size(717, 431);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX Cmd_Clear;
        private DevComponents.DotNetBar.Controls.ComboBoxEx Cmb_Fa;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel Panel_Items;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.ButtonX Cmd_AddNew;
        private DevComponents.DotNetBar.ButtonX Cmd_ShowXieBo;
    }
}

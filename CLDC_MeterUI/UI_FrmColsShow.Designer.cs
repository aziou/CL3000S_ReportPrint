namespace CLDC_MeterUI
{
    partial class UI_FrmColsShow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv_CXLRColsVisiable = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgv_SHCPColsVisiable = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Cansel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CXLRColsVisiable)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SHCPColsVisiable)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(417, 416);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(411, 370);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv_CXLRColsVisiable);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(403, 344);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "参数录入列显示";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv_CXLRColsVisiable
            // 
            this.dgv_CXLRColsVisiable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_CXLRColsVisiable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_CXLRColsVisiable.Location = new System.Drawing.Point(3, 3);
            this.dgv_CXLRColsVisiable.Name = "dgv_CXLRColsVisiable";
            this.dgv_CXLRColsVisiable.RowTemplate.Height = 23;
            this.dgv_CXLRColsVisiable.Size = new System.Drawing.Size(397, 338);
            this.dgv_CXLRColsVisiable.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv_SHCPColsVisiable);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(403, 344);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "审核存盘列显示";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgv_SHCPColsVisiable
            // 
            this.dgv_SHCPColsVisiable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SHCPColsVisiable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_SHCPColsVisiable.Location = new System.Drawing.Point(3, 3);
            this.dgv_SHCPColsVisiable.Name = "dgv_SHCPColsVisiable";
            this.dgv_SHCPColsVisiable.RowTemplate.Height = 23;
            this.dgv_SHCPColsVisiable.Size = new System.Drawing.Size(397, 338);
            this.dgv_SHCPColsVisiable.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Cansel);
            this.panel1.Controls.Add(this.btn_OK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 379);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 34);
            this.panel1.TabIndex = 1;
            // 
            // btn_Cansel
            // 
            this.btn_Cansel.Location = new System.Drawing.Point(317, 8);
            this.btn_Cansel.Name = "btn_Cansel";
            this.btn_Cansel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cansel.TabIndex = 1;
            this.btn_Cansel.Text = "取消";
            this.btn_Cansel.UseVisualStyleBackColor = true;
            this.btn_Cansel.Click += new System.EventHandler(this.btn_Cansel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(216, 8);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 0;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // UI_FrmColsShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 416);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(433, 454);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(433, 454);
            this.Name = "UI_FrmColsShow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据列显示配置";
            this.TopMost = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CXLRColsVisiable)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SHCPColsVisiable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv_CXLRColsVisiable;
        private System.Windows.Forms.DataGridView dgv_SHCPColsVisiable;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Cansel;
        private System.Windows.Forms.Button btn_OK;
    }
}
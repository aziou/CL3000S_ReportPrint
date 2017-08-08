namespace CLDC_MeterUI.DisplayInfo
{
    partial class DisplayWcAndPianCha
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Chk_DisplayAll = new System.Windows.Forms.CheckBox();
            this.checkWC_Normal1 = new CLDC_MeterUI.DisplayInfo.DisplayWC_Normal();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkWC_PianCha1 = new CLDC_MeterUI.DisplayInfo.DisplayWC_PianCha();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(453, 296);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.Chk_DisplayAll);
            this.tabPage1.Controls.Add(this.checkWC_Normal1);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(445, 271);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本误差";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Chk_DisplayAll
            // 
            this.Chk_DisplayAll.AutoSize = true;
            this.Chk_DisplayAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Chk_DisplayAll.Location = new System.Drawing.Point(97, 59);
            this.Chk_DisplayAll.Name = "Chk_DisplayAll";
            this.Chk_DisplayAll.Size = new System.Drawing.Size(96, 16);
            this.Chk_DisplayAll.TabIndex = 1;
            this.Chk_DisplayAll.Text = "显示全部数据";
            this.Chk_DisplayAll.UseVisualStyleBackColor = true;
            // 
            // checkWC_Normal1
            // 
            this.checkWC_Normal1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkWC_Normal1.Location = new System.Drawing.Point(3, 3);
            this.checkWC_Normal1.Margin = new System.Windows.Forms.Padding(0);
            this.checkWC_Normal1.Name = "checkWC_Normal1";
            this.checkWC_Normal1.Size = new System.Drawing.Size(439, 265);
            this.checkWC_Normal1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkWC_PianCha1);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(445, 271);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "标准偏差";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkWC_PianCha1
            // 
            this.checkWC_PianCha1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkWC_PianCha1.Location = new System.Drawing.Point(3, 3);
            this.checkWC_PianCha1.Margin = new System.Windows.Forms.Padding(0);
            this.checkWC_PianCha1.Name = "checkWC_PianCha1";
            this.checkWC_PianCha1.Size = new System.Drawing.Size(439, 265);
            this.checkWC_PianCha1.TabIndex = 0;
            // 
            // CheckWcAndPianCha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "CheckWcAndPianCha";
            this.Size = new System.Drawing.Size(453, 296);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private DisplayWC_Normal checkWC_Normal1;
        private DisplayWC_PianCha checkWC_PianCha1;
        private System.Windows.Forms.CheckBox Chk_DisplayAll;
    }
}

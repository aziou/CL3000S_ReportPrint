namespace CLDC_MeterUI
{
    partial class ShowCheckStep
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
            this.labelBllName = new System.Windows.Forms.Label();
            this.pictureBoxBll = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBll)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelBllName
            // 
            this.labelBllName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelBllName.AutoSize = true;
            this.labelBllName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelBllName.Location = new System.Drawing.Point(3, 4);
            this.labelBllName.Name = "labelBllName";
            this.labelBllName.Size = new System.Drawing.Size(104, 16);
            this.labelBllName.TabIndex = 0;
            this.labelBllName.Text = "检定项目名称";
            // 
            // pictureBoxBll
            // 
            this.pictureBoxBll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxBll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxBll.Location = new System.Drawing.Point(3, 27);
            this.pictureBoxBll.Name = "pictureBoxBll";
            this.pictureBoxBll.Size = new System.Drawing.Size(294, 170);
            this.pictureBoxBll.TabIndex = 1;
            this.pictureBoxBll.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelBllName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxBll, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 200);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // ShowCheckStep
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "ShowCheckStep";
            this.Text = "检定规程描述";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBll)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBllName;
        private System.Windows.Forms.PictureBox pictureBoxBll;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
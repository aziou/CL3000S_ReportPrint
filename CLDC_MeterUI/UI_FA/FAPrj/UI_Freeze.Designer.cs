using DevComponents.DotNetBar.Controls;
namespace CLDC_MeterUI.UI_FA.FAPrj
{
    partial class UI_Freeze
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI_Dgn));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_Prjs = new System.Windows.Forms.Panel();
            this.pnl_Title = new System.Windows.Forms.Panel();
            this.Cmd_Clear = new System.Windows.Forms.Button();
            this.Cmb_Fa = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnl_Title.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pnl_Prjs, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnl_Title, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(595, 465);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnl_Prjs
            // 
            this.pnl_Prjs.AutoScroll = true;
            this.pnl_Prjs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.pnl_Prjs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Prjs.Location = new System.Drawing.Point(0, 35);
            this.pnl_Prjs.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_Prjs.Name = "pnl_Prjs";
            this.pnl_Prjs.Size = new System.Drawing.Size(595, 430);
            this.pnl_Prjs.TabIndex = 7;
            // 
            // pnl_Title
            // 
            this.pnl_Title.AutoScroll = true;
            this.pnl_Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.pnl_Title.Controls.Add(this.Cmd_Clear);
            this.pnl_Title.Controls.Add(this.Cmb_Fa);
            this.pnl_Title.Controls.Add(this.label1);
            this.pnl_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Title.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnl_Title.Location = new System.Drawing.Point(0, 0);
            this.pnl_Title.Margin = new System.Windows.Forms.Padding(0);
            this.pnl_Title.Name = "pnl_Title";
            this.pnl_Title.Size = new System.Drawing.Size(595, 35);
            this.pnl_Title.TabIndex = 6;
            // 
            // Cmd_Clear
            // 
            this.Cmd_Clear.Image = ((System.Drawing.Image)(resources.GetObject("Cmd_Clear.Image")));
            this.Cmd_Clear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Cmd_Clear.Location = new System.Drawing.Point(373, 8);
            this.Cmd_Clear.Name = "Cmd_Clear";
            this.Cmd_Clear.Size = new System.Drawing.Size(102, 23);
            this.Cmd_Clear.TabIndex = 3;
            this.Cmd_Clear.Text = "清理所有项目";
            this.Cmd_Clear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Cmd_Clear.UseVisualStyleBackColor = true;
            this.Cmd_Clear.Click += new System.EventHandler(this.Cmd_Clear_Click);
            // 
            // Cmb_Fa
            // 
            this.Cmb_Fa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_Fa.FormattingEnabled = true;
            this.Cmb_Fa.Location = new System.Drawing.Point(151, 8);
            this.Cmb_Fa.Name = "Cmb_Fa";
            this.Cmb_Fa.Size = new System.Drawing.Size(218, 20);
            this.Cmb_Fa.TabIndex = 2;
            this.Cmb_Fa.SelectionChangeCommitted += new System.EventHandler(this.Cmb_Fa_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "可选择的冻结实验方案：";
            // 
            // UI_Freeze
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "UI_Freeze";
            this.Size = new System.Drawing.Size(595, 465);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnl_Title.ResumeLayout(false);
            this.pnl_Title.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnl_Title;
        private System.Windows.Forms.Panel pnl_Prjs;
        private System.Windows.Forms.Label label1;
        private ComboBoxEx Cmb_Fa;
        private System.Windows.Forms.Button Cmd_Clear;


    }
}

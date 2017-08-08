namespace CLDC_DataManager
{
    partial class Frm_SelectJDY
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
            this.Cmd_Close = new System.Windows.Forms.Button();
            this.Cmd_Import = new System.Windows.Forms.Button();
            this.Cmb_filename = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Cmd_Close
            // 
            this.Cmd_Close.Location = new System.Drawing.Point(165, 53);
            this.Cmd_Close.Name = "Cmd_Close";
            this.Cmd_Close.Size = new System.Drawing.Size(58, 27);
            this.Cmd_Close.TabIndex = 14;
            this.Cmd_Close.Text = "关闭(&C)";
            this.Cmd_Close.UseVisualStyleBackColor = true;
            this.Cmd_Close.Click += new System.EventHandler(this.Cmd_Close_Click);
            // 
            // Cmd_Import
            // 
            this.Cmd_Import.Location = new System.Drawing.Point(98, 53);
            this.Cmd_Import.Name = "Cmd_Import";
            this.Cmd_Import.Size = new System.Drawing.Size(61, 27);
            this.Cmd_Import.TabIndex = 13;
            this.Cmd_Import.Text = "上传(&U)";
            this.Cmd_Import.UseVisualStyleBackColor = true;
            this.Cmd_Import.Click += new System.EventHandler(this.Cmd_Import_Click);
            // 
            // Cmb_filename
            // 
            this.Cmb_filename.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmb_filename.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Cmb_filename.FormattingEnabled = true;
            this.Cmb_filename.Location = new System.Drawing.Point(98, 23);
            this.Cmb_filename.Name = "Cmb_filename";
            this.Cmb_filename.Size = new System.Drawing.Size(128, 22);
            this.Cmb_filename.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "选择文件名：";
            // 
            // Frm_SelectJDY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 107);
            this.Controls.Add(this.Cmd_Close);
            this.Controls.Add(this.Cmd_Import);
            this.Controls.Add(this.Cmb_filename);
            this.Controls.Add(this.label1);
            this.Name = "Frm_SelectJDY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EXCEL文件选择";
            this.Load += new System.EventHandler(this.Frm_SelectJDY_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cmd_Close;
        private System.Windows.Forms.Button Cmd_Import;
        private System.Windows.Forms.ComboBox Cmb_filename;
        private System.Windows.Forms.Label label1;
    }
}
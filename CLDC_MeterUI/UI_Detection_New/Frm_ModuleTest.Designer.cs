namespace CLDC_MeterUI.UI_Detection_New
{
    partial class Frm_ModuleTest
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
            this.ctr_ErrPlate1 = new CLDC_MeterUI.UI_Detection_New.ModuleTestControl.Ctr_ErrPlate();
            this.SuspendLayout();
            // 
            // ctr_ErrPlate1
            // 
            this.ctr_ErrPlate1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ctr_ErrPlate1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctr_ErrPlate1.Location = new System.Drawing.Point(0, 0);
            this.ctr_ErrPlate1.Margin = new System.Windows.Forms.Padding(0);
            this.ctr_ErrPlate1.Name = "ctr_ErrPlate1";
            this.ctr_ErrPlate1.Size = new System.Drawing.Size(709, 399);
            this.ctr_ErrPlate1.TabIndex = 0;
            // 
            // Frm_ModuleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 399);
            this.Controls.Add(this.ctr_ErrPlate1);
            this.Name = "Frm_ModuleTest";
            this.Text = "Frm_ModuleTest";
            this.ResumeLayout(false);

        }

        #endregion

        private CLDC_MeterUI.UI_Detection_New.ModuleTestControl.Ctr_ErrPlate ctr_ErrPlate1;
    }
}
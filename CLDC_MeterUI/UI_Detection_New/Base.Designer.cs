namespace CLDC_MeterUI.UI_Detection_New
{
    partial class Base
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Base));
            this.Btn_DoComplated = new DevComponents.DotNetBar.ButtonX();
            
            
            this.SuspendLayout();
            // 
            // Btn_DoComplated
            // 
            this.Btn_DoComplated.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.Btn_DoComplated.Cursor = System.Windows.Forms.Cursors.Hand;
            //this.Btn_DoComplated.Image = ((System.Drawing.Image)(resources.GetObject("Btn_DoComplated.Image")));
            this.Btn_DoComplated.Location = new System.Drawing.Point(203, 44);
            this.Btn_DoComplated.Margin = new System.Windows.Forms.Padding(1);
            this.Btn_DoComplated.Name = "Btn_DoComplated";
            this.Btn_DoComplated.Size = new System.Drawing.Size(120, 48);
            this.Btn_DoComplated.TabIndex = 1;
            this.Btn_DoComplated.Text = "操作完成(&O)";
            
            // 
            // Base
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            
            this.Controls.Add(this.Btn_DoComplated);
            this.Name = "Base";
            this.Size = new System.Drawing.Size(449, 251);
            this.ResumeLayout(false);

        }

        #endregion

        protected  DevComponents.DotNetBar.ButtonX Btn_DoComplated;        

    }
}

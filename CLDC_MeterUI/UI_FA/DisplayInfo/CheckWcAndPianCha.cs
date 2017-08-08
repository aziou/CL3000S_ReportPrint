using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UI.DisplayInfo
{


    public partial class CheckWcAndPianCha :  Base
    {
        /// <summary>
        /// 指示当前误差是否全部显示 = false 着只显示不合格误差数据
        /// </summary>
        public bool IsDisplayAll = false;

        public CheckWcAndPianCha()
        {
            InitializeComponent();
        }

        public CheckWcAndPianCha(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
            : base(meterInfo, allowedit)
        {
            InitializeComponent();
            if (Comm.Function.Common.IsVSDevenv()) return;

            this.Controls.Add(Chk_DisplayAll);
            this.Controls.SetChildIndex(Chk_DisplayAll, 0);
            Chk_DisplayAll.Location = new Point(150, 3);
            Chk_DisplayAll.CheckedChanged += new EventHandler(Chk_DisplayAll_CheckedChanged);
            

            SetData(MeterInfo, AllowEdit);
        }

        void Chk_DisplayAll_CheckedChanged(object sender, EventArgs e)
        {
            SetData(MeterInfo, AllowEdit);
        }

        public override void SetData(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        {
            base.SetData(meterInfo, allowedit);

            IsDisplayAll = Chk_DisplayAll.Checked;

            checkWC_Normal1.IsDisplayAll = IsDisplayAll;
            checkWC_PianCha1.IsDisplayAll = IsDisplayAll;

            checkWC_Normal1.SetData(meterInfo, allowedit);
            checkWC_PianCha1.SetData(meterInfo, allowedit);

        }



    }
}

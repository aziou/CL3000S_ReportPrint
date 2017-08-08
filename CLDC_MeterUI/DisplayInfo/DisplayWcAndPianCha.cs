using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.DisplayInfo
{


    public partial class DisplayWcAndPianCha :  Base
    {
        /// <summary>
        /// 指示当前误差是否全部显示 = false 着只显示不合格误差数据
        /// </summary>
        public bool IsDisplayAll = false;

        public DisplayWcAndPianCha()
        {
            InitializeComponent();
        }
        public DisplayWcAndPianCha(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
            : base(MeterInfo, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            this.Controls.Add(Chk_DisplayAll);
            this.Controls.SetChildIndex(Chk_DisplayAll, 0);
            Chk_DisplayAll.Location = new Point(150, 3);
            Chk_DisplayAll.CheckedChanged += new EventHandler(Chk_DisplayAll_CheckedChanged);


            SetData(MeterInfo, AllowEdit);
        }
        public DisplayWcAndPianCha(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
            : base(MeterGroup, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            this.Controls.Add(Chk_DisplayAll);
            this.Controls.SetChildIndex(Chk_DisplayAll, 0);
            Chk_DisplayAll.Location = new Point(150, 3);
            Chk_DisplayAll.CheckedChanged += new EventHandler(Chk_DisplayAll_CheckedChanged);
            

            SetData(MeterGroup, AllowEdit);
        }

        void Chk_DisplayAll_CheckedChanged(object sender, EventArgs e)
        {
            SetData(_MeterGroup, AllowEdit);
        }
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {
            base.SetData(MeterInfo, allowedit);

            IsDisplayAll = Chk_DisplayAll.Checked;

            checkWC_Normal1.IsDisplayAll = IsDisplayAll;
            checkWC_PianCha1.IsDisplayAll = IsDisplayAll;

            checkWC_Normal1.SetData(MeterInfo, allowedit);
            checkWC_PianCha1.SetData(MeterInfo, allowedit);

        }
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            base.SetData(MeterGroup, allowedit);

            IsDisplayAll = Chk_DisplayAll.Checked;

            checkWC_Normal1.IsDisplayAll = IsDisplayAll;
            checkWC_PianCha1.IsDisplayAll = IsDisplayAll;

            checkWC_Normal1.SetData(MeterGroup, allowedit);
            checkWC_PianCha1.SetData(MeterGroup, allowedit);
        }

    }
}

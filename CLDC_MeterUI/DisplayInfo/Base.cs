using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.DisplayInfo
{
    public partial class Base : UserControl
    {
        /// <summary>
        /// 电能表数据模型
        /// </summary>
        protected CLDC_DataCore.Model.DnbModel.DnbGroupInfo _MeterGroup;
        /// <summary>
        /// 电能表数据模型
        /// </summary>
        protected CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo;

        /// <summary>
        /// 允许修改
        /// </summary>
        protected bool AllowEdit = false;

        public Base()
        {
            InitializeComponent();
        }

        public Base(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            this._MeterGroup = MeterGroup;
            this.AllowEdit = allowedit;
        }

        public virtual void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            this._MeterGroup = MeterGroup;
            this.AllowEdit = allowedit;
        }

        public Base(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        {
            this.MeterInfo = meterInfo;
            this.AllowEdit = allowedit;
        }

        public virtual void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        {
            this.MeterInfo = meterInfo;
            this.AllowEdit = allowedit;
        }

    }
}

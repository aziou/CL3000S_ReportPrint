using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UI.DisplayInfo
{
    public partial class Base : UserControl
    {
        /// <summary>
        /// ���ܱ�����ģ��
        /// </summary>
        protected Comm.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo;

        /// <summary>
        /// �����޸�
        /// </summary>
        protected bool AllowEdit = false;

        public Base()
        {
            InitializeComponent();
        }

        public Base(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        {
            this.MeterInfo = meterInfo;
            this.AllowEdit = allowedit;
        }

        public virtual void SetData(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        {
            this.MeterInfo = meterInfo;
            this.AllowEdit = allowedit;
        }

    }
}

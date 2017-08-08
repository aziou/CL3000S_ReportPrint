using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UI.DisplayInfo
{
    public partial class DisplayInfo : Form
    {

        private Comm.Model.DnbModel.DnbGroupInfo MeterGroup;
        public DisplayInfo()
        {
            InitializeComponent();
        }

        public DisplayInfo(Comm.Model.DnbModel.DnbGroupInfo meterGroup)
        {
            if (Comm.Function.Common.IsVSDevenv()) return;
            this.MeterGroup = meterGroup;

            //设置下拉
            DataTable dtTmp = new DataTable();
            dtTmp.Columns.Add("名称", typeof(string));  //第01表位
            dtTmp.Columns.Add("键值", typeof(string));  //数组下标
            int index = 0;
            foreach (Comm.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo in MeterGroup.MeterGroup)
            {
                dtTmp.Rows.Add(new object[] { string.Format("第{0:d2}表位", MeterInfo.Mb_intBno), index++ });
            }

            Cmb_MeterList.DisplayMember = "名称";
            Cmb_MeterList.ValueMember = "键值";
            Cmb_MeterList.DataSource = dtTmp;
        }

        private void Cmb_MeterList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(Cmb_MeterList.SelectedValue);
            Comm.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = MeterGroup.MeterGroup[index];
            uiMeterInfo.SetData(MeterInfo,Chk_AllowEdit.Checked);
        }

        private void Chk_AllowEdit_CheckedChanged(object sender, EventArgs e)
        {
            Cmb_MeterList_SelectedIndexChanged(sender, e);
        }


    }
}
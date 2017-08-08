using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UI.DisplayInfo
{
    /// <summary>
    /// 多功能检定数据
    /// </summary>
    public partial class CheckDgn : Base 
    {
        public CheckDgn()
        {
            InitializeComponent();
        }
        public CheckDgn(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
            : base(meterInfo, allowedit)
        {
            InitializeComponent();
            if (Comm.Function.Common.IsVSDevenv()) return;
            SetData(MeterInfo, AllowEdit);
        }

        public override void SetData(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        {
            if (meterInfo.MeterDgns.Count == 0) return;

            foreach(string _Key in meterInfo.MeterDgns.Keys)
            { 
                Comm.Model.DnbModel.DnbInfo.MeterDgn _Dgn=meterInfo.MeterDgns[_Key];
                if (_Dgn.Md_PrjID.Length == 3)         //大ID
                {
                    Dgw_Data.Rows.Add(_Dgn.Md_PrjName, "    "+_Dgn.Md_chrValue);
                }
            }


            base.SetData(meterInfo, allowedit);

        }
    }
}

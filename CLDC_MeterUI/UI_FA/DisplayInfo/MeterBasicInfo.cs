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
    /// 电能表基本参数信息
    /// </summary>
    public partial class MeterBasicInfo : Base
    {

        public delegate void Event_ValueChanged(string PropertyName, object Value, int Bwh);

        public event Event_ValueChanged ValueChanged;

        private int _Bwh = 0;

        public MeterBasicInfo()
        {
            InitializeComponent();
        }

        public MeterBasicInfo(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
            : base(meterInfo, allowedit)
        {
            InitializeComponent();
            if (Comm.Function.Common.IsVSDevenv()) return;

            this.SetData(meterInfo, allowedit);

            this.Txt_ccrq.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
            this.Txt_Jdyj.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
            this.Txt_QianFeng1.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
            this.Txt_QianFeng2.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
            this.Txt_QianFeng3.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
            this.Txt_Zsbh.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
            this.Txt_zzbz.KeyPress += new KeyPressEventHandler(Txt_KeyPress);


        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 13) return;

            if (this.ValueChanged == null)
                return;

            TextBox _TmpControl = sender as TextBox;

            if (_TmpControl == null) return;

            string _ProtocolName = "";

            switch (_TmpControl.Name.ToLower())
            { 
                case "txt_ccrq":
                    _ProtocolName = "Mb_chrCcrq";
                    break;
                case "txt_jdyj":
                    _ProtocolName = "Mb_chrOther5";
                    break;
                case "txt_qiangfeng1":
                    _ProtocolName = "Mb_chrQianFeng1";
                    break;
                case "txt_qiangfeng2":
                    _ProtocolName = "Mb_chrQianFeng2";
                    break;
                case "txt_qiangfeng3":
                    _ProtocolName = "Mb_chrQianFeng3";
                    break;
                case "txt_zsbh":
                    _ProtocolName = "Mb_chrZsbh";
                    break;
                case "txt_zzbz":
                    _ProtocolName = "Mb_chrOther4";
                    break;
                default:
                    break;
            }

            this.ValueChanged(_ProtocolName,_TmpControl.Text,this._Bwh);

        }


        public override void SetData(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        {
            base.SetData(meterInfo, allowedit);

            Lst_Info.Items.Clear();

            this._Bwh = meterInfo.Mb_intBno;

            string clfs = "";

            switch (meterInfo.Mb_intClfs)
            {
                case (int)Comm.Enum.Cus_Clfs.三相四线有功:
                case (int)Comm.Enum.Cus_Clfs.三相四线无功:
                    clfs = "三相四线";
                    break;
                case (int)Comm.Enum.Cus_Clfs.三相三线有功:
                case (int)Comm.Enum.Cus_Clfs.三相三线无功:
                    clfs = "三相三线";
                    break;
                case (int)Comm.Enum.Cus_Clfs.二元件跨相60:
                case (int)Comm.Enum.Cus_Clfs.二元件跨相90:
                case (int)Comm.Enum.Cus_Clfs.三元件跨相90:
                    clfs = "三相三线";
                    break;
                case (int)Comm.Enum.Cus_Clfs.单相:
                    clfs = "单相";
                    break;
                default:
                    clfs = "三相四线";
                    break;
            }


            meterInfo.Mb_ChrBmc = string.Format("{0}{1}电能表", clfs, meterInfo.Mb_chrBlx);

            Lst_Info.Items.Add("");

            Lst_Info.Items.Add(string.Format("  电表名称      =      {0}", meterInfo.Mb_ChrBmc));

            Lst_Info.Items.Add(string.Format("  制造厂家      =      {0}", meterInfo.Mb_chrzzcj));

            Lst_Info.Items.Add(string.Format("  出厂日期      =      {0}", meterInfo.Mb_chrCcrq));

            Lst_Info.Items.Add(string.Format("  电表型号      =      {0}", meterInfo.Mb_Bxh));

            Lst_Info.Items.Add(string.Format("  条 码 号      =      {0}", meterInfo.Mb_ChrTxm));

            Lst_Info.Items.Add(string.Format("  出厂编号      =      {0}", meterInfo.Mb_ChrCcbh));

            Lst_Info.Items.Add(string.Format("  电    压      =      {0}{1}V", meterInfo.Mb_intClfs != 5 ? "3×" : "", meterInfo.Mb_chrUb));

            Lst_Info.Items.Add(string.Format("  电    流      =      {0}{1}A", meterInfo.Mb_intClfs != 5 ? "3×" : "", meterInfo.Mb_chrIb));

            Lst_Info.Items.Add(string.Format("  常    数      =      {0}", meterInfo.Mb_chrBcs));

            Lst_Info.Items.Add(string.Format("  类    型      =      {0}", meterInfo.Mb_chrBlx));

            Lst_Info.Items.Add(string.Format("  等    级      =      {0}", meterInfo.Mb_chrBdj));

            Lst_Info.Items.Add(string.Format("  止 逆 器      =      {0}", meterInfo.Mb_BlnZnq ? "有止逆" : "无止逆"));

            Lst_Info.Items.Add(string.Format("  接线方式      =      {0}", meterInfo.Mb_BlnHgq ? "经互感器接入" : "直接接入"));

            Lst_Info.Items.Add(string.Format("  送检单位      =      {0}", meterInfo.Mb_CHRSjdw));

            Lst_Info.Items.Add(string.Format("  证书编号      =      {0}", meterInfo.Mb_chrZsbh));

            this.Txt_Zsbh.Text = meterInfo.Mb_chrZsbh;

            this.Txt_ccrq.Text = meterInfo.Mb_chrCcrq;

            this.Txt_Jdyj.Text = meterInfo.Mb_chrOther5;

            this.Txt_zzbz.Text = meterInfo.Mb_chrOther4;

            this.Txt_QianFeng1.Text = meterInfo.Mb_chrQianFeng1;

            this.Txt_QianFeng2.Text = meterInfo.Mb_chrQianFeng2;

            this.Txt_QianFeng3.Text = meterInfo.Mb_chrQianFeng3;

        }

        private void Txt_Zsbh_TextChanged(object sender, EventArgs e)
        {
            MeterInfo.Mb_chrZsbh = Txt_Zsbh.Text;
        }

        private void Txt_ccrq_TextChanged(object sender, EventArgs e)
        {
            MeterInfo.Mb_chrCcrq = Txt_ccrq.Text; 
        }

        private void Txt_zzbz_TextChanged(object sender, EventArgs e)
        {
            MeterInfo.Mb_chrOther4 = Txt_zzbz.Text; 
        }

        private void Txt_Jdyj_TextChanged(object sender, EventArgs e)
        {
            MeterInfo.Mb_chrOther5 = Txt_Jdyj.Text;
        }

        private void Txt_QianFeng1_TextChanged(object sender, EventArgs e)
        {
            MeterInfo.Mb_chrQianFeng1 = Txt_QianFeng1.Text;
        }

        private void Txt_QianFeng2_TextChanged(object sender, EventArgs e)
        {
            MeterInfo.Mb_chrQianFeng2 = Txt_QianFeng2.Text;
        }

        private void Txt_QianFeng3_TextChanged(object sender, EventArgs e)
        {
            MeterInfo.Mb_chrQianFeng3 = Txt_QianFeng3.Text;
        }




        






    }
}

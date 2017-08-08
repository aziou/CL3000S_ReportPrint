using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.ShowDataView
{
    public partial class Frm_DnbInfoView : Office2007Form
    {
        public delegate void Event_ValueChanged(string PropertyName,object Value,int Bwh);


        public event Event_ValueChanged ValueChanged;

        int _Bwh = 0;

        public Frm_DnbInfoView()
        {
            InitializeComponent();

            List<string> _Names= CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo.getProtocolNames();

            Cmb_Protocol.Items.Clear();

            Cmb_Protocol.Items.Add("");

            for (int i = 0; i < _Names.Count; i++)
            {
                Cmb_Protocol.Items.Add(_Names[i]);
            }

            Txt_zzbz.KeyPress += new KeyPressEventHandler(Txt_KeyPress); 
            Txt_zsbh.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
            Txt_Adr.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
            Txt_jdgc.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
            Txt_QianFeng1.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
            Txt_QianFeng2.KeyPress += new KeyPressEventHandler(Txt_KeyPress);
            Txt_QianFeng3.KeyPress += new KeyPressEventHandler(Txt_KeyPress);

            Cmb_Protocol.SelectionChangeCommitted += new EventHandler(Cmb_Protocol_SelectionChangeCommitted);  

        }

        private void Cmb_Protocol_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            if (Cmb_Protocol.Tag != null & Cmb_Protocol.Tag.ToString() != Cmb_Protocol.Text)
            {
                CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo _Protocol = new CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo(Cmb_Protocol.Text);
                if (!_Protocol.Loading)
                {
                    Cmb_Protocol.Text = Cmb_Protocol.Tag.ToString();
                    return;
                }

                if (MessageBoxEx.Show(this,"是否所有表都使用该通信协议？\n点击确认将自动更新所有被检表的通信协议，点击取消将只更新当前选中被检表的通信协议。"
                                    , "询问"
                                    , MessageBoxButtons.OKCancel
                                    , MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    this.ValueChanged("DgnProtocol",_Protocol,this._Bwh);
                }
                else
                {
                    this.ValueChanged("DgnProtocol", _Protocol, 999);          //约定全部更新使用999
                }

            }

        }

        private void Txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != 13) return;

            TextBox _TmpControl=sender as TextBox;

            if (_TmpControl.Text.Trim()==_TmpControl.Tag.ToString().Trim())
            {
                return;
            }

            if(this.ValueChanged==null)
                return;

            string _PropertyName="";

            switch (_TmpControl.Name.ToLower())
            { 
                case "txt_zzbz":
                    _PropertyName="Mb_chrOther4";
                    break;
                case "txt_zsbh":
                    _PropertyName="Mb_chrZsbh";
                    break;
                case "txt_adr":
                    _PropertyName="Mb_chrAddr";
                    break;
                case "txt_jdgc":
                    _PropertyName="Mb_chrOther5";
                    break;
                case "txt_qianfeng1":
                    _PropertyName="Mb_chrQianFeng1";
                    break;
                case "txt_qianfeng2":
                    _PropertyName="Mb_chrQianFeng2";
                    break;
                case "txt_qianfeng3":
                    _PropertyName="Mb_chrQianFeng3";
                    break;
                default:
                    return;
            }

            this.ValueChanged(_PropertyName, _TmpControl.Text,this._Bwh);
        }

        private void Lab_Close_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


        public void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo,bool EditYn)
        {
            if (EditYn)     //如果是可编辑模式
            {
                Panel_Edit.Enabled = true;
            }
            else
            {
                Panel_Edit.Enabled = false;
            }

            this._Bwh = MeterInfo.Mb_intBno;

            Txt_Adr.Text = MeterInfo.Mb_chrAddr;        //表通信地址
            Txt_Adr.Tag = MeterInfo.Mb_chrAddr;
            if (MeterInfo.DgnProtocol.Loading)//TODO:加载协议处理，null异常
            {
                Cmb_Protocol.Text = MeterInfo.DgnProtocol.ProtocolName; //表通信协议 
                Cmb_Protocol.Tag = MeterInfo.DgnProtocol.ProtocolName;
            }
            else
            {
                Cmb_Protocol.Tag = "";
            }
            Txt_jdgc.Text = MeterInfo.Mb_chrOther5;         //检定规程
            Txt_jdgc.Tag = MeterInfo.Mb_chrOther5; 
            Txt_zzbz.Text = MeterInfo.Mb_chrOther4;         //制造标准
            Txt_zzbz.Tag = MeterInfo.Mb_chrOther4; 
            Txt_zsbh.Text = MeterInfo.Mb_chrZsbh;           //证书编号
            Txt_zsbh.Tag = MeterInfo.Mb_chrZsbh;
            Txt_QianFeng1.Text = MeterInfo.Mb_chrQianFeng1; //铅封一
            Txt_QianFeng2.Text = MeterInfo.Mb_chrQianFeng2; //铅封二
            Txt_QianFeng3.Text = MeterInfo.Mb_chrQianFeng3; //铅封三
            Txt_QianFeng1.Tag = MeterInfo.Mb_chrQianFeng1; //铅封一
            Txt_QianFeng2.Tag = MeterInfo.Mb_chrQianFeng2; //铅封二
            Txt_QianFeng3.Tag = MeterInfo.Mb_chrQianFeng3; //铅封三
            Lst_Info.Items.Clear();

            string clfs = "";

            switch (MeterInfo.Mb_intClfs)
            {
                case (int)CLDC_Comm.Enum.Cus_Clfs.三相四线:                
                    clfs = "三相四线";
                    break;
                case (int)CLDC_Comm.Enum.Cus_Clfs.三相三线:                
                    clfs = "三相三线";
                    break;
                case (int)CLDC_Comm.Enum.Cus_Clfs.二元件跨相60:
                case (int)CLDC_Comm.Enum.Cus_Clfs.二元件跨相90:
                case (int)CLDC_Comm.Enum.Cus_Clfs.三元件跨相90:
                    clfs = "三相三线";
                    break;
                case (int)CLDC_Comm.Enum.Cus_Clfs.单相:
                    clfs = "单相";
                    break;
                default:
                    clfs = "三相四线";
                    break;
            }


            MeterInfo.Mb_ChrBmc = string.Format("{0}{1}电能表", clfs, MeterInfo.Mb_chrBlx);

            Lst_Info.Items.Add("");

            Lst_Info.Items.Add(string.Format("  电表名称    =    {0}", MeterInfo.Mb_ChrBmc));

            Lst_Info.Items.Add(string.Format("  制造厂家    =    {0}", MeterInfo.Mb_chrzzcj));

            Lst_Info.Items.Add(string.Format("  出厂日期    =    {0}", MeterInfo.Mb_chrCcrq));

            Lst_Info.Items.Add(string.Format("  电表型号    =    {0}", MeterInfo.Mb_Bxh));

            Lst_Info.Items.Add(string.Format("  条 码 号    =    {0}", MeterInfo.Mb_ChrTxm));

            Lst_Info.Items.Add(string.Format("  出厂编号    =    {0}", MeterInfo.Mb_ChrCcbh));

            Lst_Info.Items.Add(string.Format("  电    压    =    {0}{1}V", MeterInfo.Mb_intClfs != 5 ? "3×" : "", MeterInfo.Mb_chrUb));

            Lst_Info.Items.Add(string.Format("  电    流    =    {0}{1}A", MeterInfo.Mb_intClfs != 5 ? "3×" : "", MeterInfo.Mb_chrIb));

            Lst_Info.Items.Add(string.Format("  常    数    =    {0}", MeterInfo.Mb_chrBcs));

            Lst_Info.Items.Add(string.Format("  类    型    =    {0}", MeterInfo.Mb_chrBlx));

            Lst_Info.Items.Add(string.Format("  等    级    =    {0}", MeterInfo.Mb_chrBdj));

            Lst_Info.Items.Add(string.Format("  止 逆 器    =    {0}", MeterInfo.Mb_BlnZnq ? "有止逆" : "无止逆"));

            Lst_Info.Items.Add(string.Format("  接线方式    =    {0}", MeterInfo.Mb_BlnHgq ? "经互感器接入" : "直接接入"));

            Lst_Info.Items.Add(string.Format("  送检单位    =    {0}", MeterInfo.Mb_chrSjdw));

            Lst_Info.Items.Add(string.Format("  证书编号    =    {0}", MeterInfo.Mb_chrZsbh));
  
        }

        


    }
} 
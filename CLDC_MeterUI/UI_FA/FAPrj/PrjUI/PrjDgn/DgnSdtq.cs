using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI.PrjDgn
{
    public partial class DgnSdtq : DgnBase 
    {
        public DgnSdtq()
        {
            InitializeComponent();
            
        }

        public DgnSdtq(CLDC_DataCore.Struct.StDgnConfig DgnItem)
            : base(DgnItem)
        {
            InitializeComponent();
            base.SetPanel = Panel_Back;
            //this.InitPrj();
        }
        public override Color PanelBackColor
        {
            set
            {
                Panel_Back.BackColor = value;
            }
        }

        public override Color CaptionColor
        {
            set
            {
                Panel_Back.CaptionColorOne = value;
            }
        }
        public override Color CaptionColorTwo
        {
            set
            {
                Panel_Back.CaptionColorTwo = value;
            }
        }
        //private void InitPrj()
        //{
        //    string TmpString = "07:00(峰)|11:00(平)|23:00(谷)";
        //    Lst_SD.Items.AddRange(TmpString.Split('|'));
        //}

        public override CLDC_DataCore.Struct.StPlan_Dgn DgnPlanPrj
        {
            get
            {
                string TmpString = "";

                for (int i = 0; i < Lst_SD.Items.Count; i++)
                {
                    if (TmpString == "")
                    {
                        TmpString = Lst_SD.Items[i].ToString();
                    }
                    else
                    {
                        TmpString += string.Format("|{0}", Lst_SD.Items[i].ToString());
                    }
                }
                if (TmpString == "")
                {
                    base._IsCheck = false;
                }
                int AutoCut=0;
                if (ChkAutoCut.Checked)
                    AutoCut = 1;
                
                TmpString += string.Format("|{0}", AutoCut);

                TmpString += string.Format("|{0}{1}{2}{3}"
                        , Chk_Pz.Checked == true ? "1" : "0"
                        , Chk_Pf.Checked == true ? "1" : "0"
                        , Chk_Qz.Checked == true ? "1" : "0"
                        , Chk_Qf.Checked == true ? "1" : "0");
                
                base.Parm = TmpString;
                return base.DgnPlanPrj;
            }
        }


        public override string Parm
        {
            get
            {
                return base.Parm;
            }
            set
            {
                try
                {
                    Lst_SD.Items.Clear();
                    string [] str = value.Split('|');
                    int strLength = str.Length;
                    int Count;
                    if (str != null && str.Length >= 2)
                    {
                        Count = strLength;
                        if (str[strLength - 1].Length == 4)
                        {
                            Chk_Pz.Checked = str[strLength - 1][0] == '1' ? true : false;          //P+
                            Chk_Pf.Checked = str[strLength - 1][1] == '1' ? true : false;          //P-
                            Chk_Qz.Checked = str[strLength - 1][2] == '1' ? true : false;          //Q+    
                            Chk_Qf.Checked = str[strLength - 1][3] == '1' ? true : false;          //Q-
                            if (str[strLength - 2] == "0" || str[strLength - 2] == "1")
                            {

                                if (str[strLength - 2] == "0")
                                {
                                    ChkAutoCut.Checked = false;
                                }
                                else
                                {
                                    ChkAutoCut.Checked = true;
                                }
                                
                            }
                            else
                            {
                                ChkAutoCut.Checked = false;
                            }
                            Count = strLength - 2;
                        }
                        else
                        {
                            if (str[strLength - 1] == "0" || str[strLength - 1] == "1")
                            {

                                if (str[strLength - 1] == "0")
                                {
                                    ChkAutoCut.Checked = false;
                                }
                                else
                                {
                                    ChkAutoCut.Checked = true;
                                }
                                Count = strLength - 1;
                            }
                            else
                            {
                                ChkAutoCut.Checked = false;
                            }
                        }
                        
                        
                        for (int i = 0; i < Count; i++)
                        {
                            if (str[i] != "")
                                Lst_SD.Items.Add(str[i]);
                        }

                        

                    }
                    
                }
                catch
                { }
            }
        }

        public override void LoadFA(CLDC_DataCore.Model.Plan.Plan_Dgn FaItem)
        {
            base.LoadFA(FaItem);
            if (base.Parm == string.Empty)
            {
                return;
            }
            this.Parm = base.Parm;
        }


        #region  时段费率操作
        /// <summary>
        /// 插入一个时段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Insert_Click(object sender, EventArgs e)
        {
            if (Cmb_FeiLv.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this,"请选择一个费率...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (Lst_SD.Items.Contains(string.Format("{0}({1})", DTP_SD.Value.ToString("HH':'ss"), Cmb_FeiLv.Text)))
            {
                return;
            }
            if (Lst_SD.Items.IndexOf(DTP_SD.Value.ToString("HH':'ss")) != -1)
            {
                MessageBoxEx.Show(this,"该时段已经存在，请重新设置...", "录入错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                DTP_SD.Focus();
                return;
            }
            Lst_SD.Items.Add(string.Format("{0}({1})", DTP_SD.Value.ToString("HH':'ss"), Cmb_FeiLv.Text));
        }

        /// <summary>
        /// 移除一个时段费率
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Remove_Click(object sender, EventArgs e)
        {
            if (Lst_SD.SelectedIndex == -1)
                return;
            Lst_SD.Items.RemoveAt(Lst_SD.SelectedIndex);
        }

        private void ChkAutoCut_CheckedChanged(object sender, EventArgs e)
        {
            bool status = !ChkAutoCut.Checked;
            DTP_SD.Enabled = status;
            Cmb_FeiLv.Enabled = status;
            Lst_SD.Enabled = status;
            Cmd_Insert.Enabled = status;
            Cmd_Remove.Enabled = status;
        }
        #endregion


    }
}

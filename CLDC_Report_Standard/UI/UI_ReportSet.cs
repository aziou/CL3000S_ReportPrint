using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLReport_Standard.UI
{
    public partial class UI_ReportSet : UserControl
    {

        private string iniPath = clsMain.getFilePath(@"Res\Templet.ini");

        private string iniDir = clsMain.getFilePath("Res");

        public UI_ReportSet()
        {
            InitializeComponent();

            this.Load += new EventHandler(UI_ReportSet_Load);
            this.Cmb_Templet.SelectionChangeCommitted+=new EventHandler(Cmb_Templet_SelectionChangeCommitted);

            Opt_Dzs.Click += new EventHandler(Opt_OptReport_Click);

            Opt_Gys.Click += new EventHandler(Opt_OptReport_Click);

            Opt_Pz.Click += new EventHandler(Opt_OptReport_Click);

            Opt_Qz.Click += new EventHandler(Opt_OptReport_Click);

            Opt_Pzf.Click += new EventHandler(Opt_OptReport_Click);

            Opt_Qzf.Click += new EventHandler(Opt_OptReport_Click);

            Opt_PQ.Click += new EventHandler(Opt_OptReport_Click);

            Opt_Dgn.Click += new EventHandler(Opt_OptReport_Click);

            Opt_Dx.Click += new EventHandler(Opt_OptReport_Click);

            Cmb_PrintType.SelectionChangeCommitted += new EventHandler(Opt_OptReport_Click);

            Lst_Templet.DoubleClick+=new EventHandler(Lst_Templet_DoubleClick);

            Cmd_New.Click+=new EventHandler(Cmd_New_Click);

            Cmd_NewTemplet.Click+=new EventHandler(Cmd_NewTemplet_Click);
            Tool_Save.Click+=new EventHandler(Tool_Save_Click);
            Lst_Report.DoubleClick+=new EventHandler(Lst_Report_DoubleClick);
            Tool_Up.Click+=new EventHandler(Tool_Up_Click);
            Tool_Down.Click+=new EventHandler(Tool_Down_Click);
            Tool_Del.Click+=new EventHandler(Tool_Del_Click);
            Cmd_NewType.Click+=new EventHandler(Cmd_NewType_Click);
            Cmd_PrintTypeOk.Click+=new EventHandler(Cmd_PrintTypeOk_Click);
            Txt_PrintType.KeyPress+=new KeyPressEventHandler(Txt_PrintType_KeyPress);
            Cmd_DelType.Click+=new EventHandler(Cmd_DelType_Click);
        }

        private void UI_ReportSet_Load(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(iniDir)) System.IO.Directory.CreateDirectory(iniDir);

            int TaoSum = int.Parse(clsMain.getIniString("TaoName", "NameSum", "0", iniPath));

            Cmb_Templet.Items.Clear();

            for (int i = 1; i <= TaoSum; i++)
            {
                Cmb_Templet.Items.Add(clsMain.getIniString("TaoName", "Name_" + i.ToString(), "", iniPath));
            }

            if (Cmb_Templet.Items.Count > 0)
            {
                Cmb_Templet.SelectedIndex = 0;
                this.Cmb_Templet_SelectionChangeCommitted(Cmb_Templet, e);
            }

            this.LoadTempletFile();
        }


        #region ------------------报表模板配置操作--------------

        /// <summary>
        /// 模板套型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_Templet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string sTrPrintType = clsMain.getIniString("Type_" + (Cmb_Templet.SelectedIndex + 1).ToString(), "TypeName", "", iniPath);
            if (sTrPrintType == "") return;
            Cmb_PrintType.Items.Clear();
            Cmb_PrintType.Items.AddRange(sTrPrintType.Split(','));
            Cmb_PrintType.SelectedIndex = 0;
        }



        /// <summary>
        /// 加载文件
        /// </summary>
        private void LoadTempletFile()
        {
            System.IO.DirectoryInfo Folder = new System.IO.DirectoryInfo(iniDir);

            if (Folder == null) return;

            Lst_Templet.Items.Clear();
            foreach (System.IO.FileInfo Item in Folder.GetFiles())
            {
                if (Item.Extension.ToLower() == ".doc" 
                    || Item.Extension.ToLower() == ".dot"
                    || Item.Extension.ToLower()==".docx"
                    || Item.Extension.ToLower()==".dotx")
                {
                    Lst_Templet.Items.Add(Item.Name);
                }
            }

        }

        /// <summary>
        /// 写列表
        /// </summary>
        /// <param name="vKey"></param>
        /// <param name="TaoID"></param>
        private void WriteList(string vKey, int TaoID)
        {
            string[] Arr_Value;
            string sTr_TmpValue = "";

            this.LoadTempletFile();

            sTr_TmpValue = clsMain.getIniString("PrintType_" + TaoID.ToString(), vKey, "", iniPath);
            if (sTr_TmpValue == "")
            {
                if (Lst_Report.Items.Count == 0) return;

                if (MessageBox.Show("当前无最新模板对应，请问是否需要复制上一步模板操作？", "模板配置", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    Lst_Report.Items.Clear();
                }
                return;
            }

            Lst_Report.Items.Clear();

            Arr_Value = sTr_TmpValue.Split(',');
            for (int i = 0; i < Arr_Value.Length; i++)
            {
                Lst_Report.Items.Add(Arr_Value[i]);
                if (Lst_Templet.Items.Contains(Arr_Value[i]))
                {
                    Lst_Templet.Items.Remove(Arr_Value[i]);
                }
            }


        }
        /// <summary>
        /// 类型单选框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Opt_OptReport_Click(object sender, EventArgs e)
        {
            string TmpLet_Key = "";
            string sTr_Tmp = "";

            if (Cmb_Templet.SelectedIndex < 0)
            {
                MessageBox.Show("请选择所需要加载的模板套型名称...", "模板配置", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cmb_Templet.Focus();
                return;
            }

            sTr_Tmp = GetOptValue(ReportType.表类型);
            TmpLet_Key = sTr_Tmp;
            sTr_Tmp = GetOptValue(ReportType.表属性);
            if (sTr_Tmp == "") return;
            TmpLet_Key += string.Format("_{0}", sTr_Tmp);
            sTr_Tmp = GetOptValue(ReportType.报表类型);
            if (sTr_Tmp == "") return;
            TmpLet_Key += string.Format("_{0}", sTr_Tmp);

            this.WriteList(TmpLet_Key, Cmb_Templet.SelectedIndex + 1);
        }


        private void Cmd_New_Click(object sender, EventArgs e)
        {
            Panel_NewTemplet.Visible = true;
        }

        private void Cmd_NewTemplet_Click(object sender, EventArgs e)
        {
            if (Txt_TempletName.Text == string.Empty)
            {
                //MessageBox.Show("请输入一个有效的模板套型名称...", "添加模板", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Panel_NewTemplet.Visible = false;
                return;
            }
            Cmb_Templet.Items.Add(Txt_TempletName.Text);
            Cmb_Templet.SelectedIndex = Cmb_Templet.Items.Count - 1;
            this.Cmb_Templet_SelectionChangeCommitted(Cmb_Templet, e);
            int Int_NameSum = int.Parse(clsMain.getIniString("TaoName", "NameSum", "0",iniPath));

            Int_NameSum++;

            clsMain.WriteIni("TaoName", "NameSum", Int_NameSum.ToString(), iniPath );

            clsMain.WriteIni("TaoName", "Name_" + Int_NameSum, Txt_TempletName.Text,iniPath );

            Txt_TempletName.Text = "";
            Panel_NewTemplet.Visible = false;
        }


        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tool_Save_Click(object sender, EventArgs e)
        {
            //if (Lst_Report.Items.Count == 0) return;

            if (Cmb_Templet.SelectedIndex < 0)
            {
                MessageBox.Show("请选择所需要加载的模板套型名称...", "报表模板配置", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string TempLet_Key = "";
            string sTr_Tmp = "";

            sTr_Tmp = GetOptValue(ReportType.表类型);

            if (sTr_Tmp == "")
            {
                MessageBox.Show("请选择表类型...", "报表模板配置", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            TempLet_Key = string.Format("{0}", sTr_Tmp);

            sTr_Tmp = GetOptValue(ReportType.表属性);
            if (sTr_Tmp == "")
            {
                MessageBox.Show("请选择表属性...", "报表模板配置", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            TempLet_Key = string.Format("{0}_{1}", TempLet_Key, sTr_Tmp);

            sTr_Tmp = GetOptValue(ReportType.报表类型);
            if (sTr_Tmp == "")
            {
                MessageBox.Show("请选择报表类型...", "报表模板配置", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            TempLet_Key = string.Format("{0}_{1}", TempLet_Key, sTr_Tmp);

            if (Lst_Report.Items.Count == 0)
            { 
                clsMain.WriteIni("PrintType_" + (Cmb_Templet.SelectedIndex + 1).ToString(), TempLet_Key, "", iniPath);
                return;
            }


            string[] Reprot_Arr = new string[Lst_Report.Items.Count];

            for (int i = 0; i < Lst_Report.Items.Count; i++)
            {
                Reprot_Arr[i] = Lst_Report.Items[i].ToString();
            }

            clsMain.WriteIni("PrintType_" + (Cmb_Templet.SelectedIndex + 1).ToString(), TempLet_Key, string.Join(",", Reprot_Arr), iniPath);
        }

        private string GetOptValue(ReportType vType)
        {
            string ReturnValue = "";
            switch (vType)
            {
                case ReportType.表类型:
                    if (Opt_Dzs.Checked)
                        ReturnValue = "0";
                    else if (Opt_Gys.Checked)
                    {
                        ReturnValue = "1";
                    }
                    else
                    {
                        ReturnValue = "";
                    }
                    break;
                case ReportType.表属性:
                    if (Opt_Pz.Checked) ReturnValue = "0";
                    if (Opt_Qz.Checked) ReturnValue = "1";
                    if (Opt_Pzf.Checked) ReturnValue = "2";
                    if (Opt_Qzf.Checked) ReturnValue = "3";
                    if (Opt_PQ.Checked) ReturnValue = "4";
                    if (Opt_Dgn.Checked) ReturnValue = "5";
                    if (Opt_Dx.Checked) ReturnValue = "6";
                    break;
                case ReportType.报表类型:
                    if (Cmb_PrintType.SelectedIndex >= 0)
                    {
                        ReturnValue = Cmb_PrintType.SelectedIndex.ToString();
                    }
                    break;
            }

            return ReturnValue;
        }
        /// <summary>
        /// 模板列表选择模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lst_Templet_DoubleClick(object sender, EventArgs e)
        {
            if (Lst_Templet.SelectedIndex < 0) return;
            Lst_Report.Items.Add(Lst_Templet.SelectedItem);
            Lst_Templet.Items.RemoveAt(Lst_Templet.SelectedIndex);
        }
        /// <summary>
        /// 选择列表中抛出模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lst_Report_DoubleClick(object sender, EventArgs e)
        {
            if (Lst_Report.SelectedIndex < 0) return;
            if (!Lst_Templet.Items.Contains(Lst_Report.SelectedItem))
            {
                Lst_Templet.Items.Add(Lst_Report.SelectedItem);
            }
            Lst_Report.Items.RemoveAt(Lst_Report.SelectedIndex);
        }
        /// <summary>
        /// 上移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tool_Up_Click(object sender, EventArgs e)
        {
            if (Lst_Report.SelectedIndex < 0) return;
            int Index = Lst_Report.SelectedIndex;
            if (Index == 0) return;
            string Value = Lst_Report.SelectedItem.ToString();
            Lst_Report.Items.Remove(Value);
            Lst_Report.Items.Insert(Index - 1, Value);
            Lst_Report.SelectedIndex = Index - 1;
        }
        /// <summary>
        /// 下移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tool_Down_Click(object sender, EventArgs e)
        {
            if (Lst_Report.SelectedIndex < 0) return;
            int Index = Lst_Report.SelectedIndex;
            if (Index == Lst_Report.Items.Count - 1) return;
            string Value = Lst_Report.SelectedItem.ToString();
            Lst_Report.Items.Remove(Value);
            Lst_Report.Items.Insert(Index + 1, Value);
            Lst_Report.SelectedIndex = Index + 1;
        }
        /// <summary>
        /// 清空列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tool_Del_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Lst_Report.Items.Count; i++)
            {
                if (!Lst_Templet.Items.Contains(Lst_Report.Items[i]))
                {
                    Lst_Templet.Items.Add(Lst_Report.Items[i]);
                }
            }

            Lst_Report.Items.Clear();
        }

        /// <summary>
        /// 新增加类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_NewType_Click(object sender, EventArgs e)
        {
            Panel_PrintType.Visible = true;
            Txt_PrintType.Focus();
        }
        /// <summary>
        /// 新增加类型确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_PrintTypeOk_Click(object sender, EventArgs e)
        {
            if (Txt_PrintType.Text != string.Empty)
            {
                Cmb_PrintType.Items.Add(Txt_PrintType.Text);
                Cmb_PrintType.SelectedIndex = Cmb_PrintType.Items.Count - 1;
                this.Opt_OptReport_Click(Cmb_PrintType, e);
                this.WritePrintTypeString();
            }
            Panel_PrintType.Visible = false;
            Txt_PrintType.Text = "";
        }
        /// <summary>
        /// 添加打印类型（回车事件）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_PrintType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                if (Txt_PrintType.Text != string.Empty)
                {
                    this.Cmd_PrintTypeOk_Click(Cmd_PrintTypeOk, new EventArgs());
                }
        }




        /// <summary>
        /// 写报表类型字符串（证书，通知书，原始记录，....）
        /// </summary>
        /// <returns></returns>
        private void WritePrintTypeString()
        {
            List<string> Items = new List<string>();

            for (int i = 0; i < Cmb_PrintType.Items.Count; i++)
            {
                Items.Add(Cmb_PrintType.Items[i].ToString());
            }
            clsMain.WriteIni("Type_" + (Cmb_Templet.SelectedIndex + 1).ToString(), "TypeName", string.Join(",", Items.ToArray()), iniPath);
        }
        /// <summary>
        /// 删除报表类型字符串
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_DelType_Click(object sender, EventArgs e)
        {
            Cmb_PrintType.Items.Remove(Cmb_PrintType.Text);
            this.WritePrintTypeString();
            this.Lst_Report.Items.Clear();
            if (Cmb_PrintType.Items.Count != 0)
            {
                Cmb_PrintType.SelectedIndex = 0;
            }
            this.Opt_OptReport_Click(Cmb_PrintType, e);
        }


        #endregion

        private enum ReportType
        {
            表类型 = 1,
            表属性 = 2,
            报表类型 = 3
        }
    }
}

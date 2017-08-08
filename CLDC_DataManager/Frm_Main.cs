using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CLDC_DataCore.Const;
using System.Data.OleDb;
using CLDC_ManagerStart;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
namespace CLDC_DataManager
{
    public partial class Frm_Main : Form
    {
        /// <summary>
        /// MIS����
        /// </summary>
        private string m_strMinInterfaceType = "";

        private bool BlnShowData = false;

        const int CONST_PANEL_CONTROL_PANEL1_HEIGHT = 100;

        private List<CheckBox> ChkList = new List<CheckBox>();

        private List<ComboBox> CmbNameList = new List<ComboBox>();

        private List<ComboBox> CmbFhList = new List<ComboBox>();

        private List<ComboBox> CmbValueList = new List<ComboBox>();
        /// <summary>
        /// ��ʱ������
        /// </summary>
        private CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = null;
        public List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Meter_out;
        public Frm_Main()
        {
            InitializeComponent();

            CLDC_DataCore.Const.GlobalUnit.g_SystemConfig = new CLDC_DataCore.SystemModel.SystemInfo();
            CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.Load();

            CLDC_DataCore.Const.GlobalUnit.g_MsgControl = new CLDC_DataCore.VerifyMsgControl();                       //��Ϣ����

            m_strMinInterfaceType = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_MIS_INTERFACETYPE, "����SG186");

            this.Load += new EventHandler(Frm_Main_Load);
            this.SizeChanged += new EventHandler(Frm_Main_SizeChanged);

            #region ����һ���ؼ�����
            ChkList.Add(Chk_Tj1);
            ChkList.Add(Chk_Tj2);
            ChkList.Add(Chk_Tj3);
            CmbNameList.Add(Cmb_Name1);
            CmbNameList.Add(Cmb_Name2);
            CmbNameList.Add(Cmb_Name3);
            CmbFhList.Add(Cmb_Fh1);
            CmbFhList.Add(Cmb_Fh2);
            CmbFhList.Add(Cmb_Fh3);
            CmbValueList.Add(Cmb_Value1);
            CmbValueList.Add(Cmb_Value2);
            CmbValueList.Add(Cmb_Value3);

            for (int i = 0; i < ChkList.Count; i++)
            {
                ChkList[i].CheckedChanged += new EventHandler(Frm_Main_CheckedChanged);
                CmbNameList[i].SelectedValueChanged += new EventHandler(Frm_Main_SelectionChangeCommitted);
               
            }
            #endregion

            StartLoadTimeAndData();
        }


        #region ���ݲ�ѯ���

        /// <summary>
        /// ��ʾ��ѯ��ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tl_Screen_Click(object sender, EventArgs e)
        {
            Tl_Print.Checked = false;
            Panel_Print.Visible = false;
            if (Tl_Screen.Checked)
            {
                Panel_Screen.Visible = true;
                Sp_Control.Panel1Collapsed = false;
                Sp_Control.SplitterDistance = CONST_PANEL_CONTROL_PANEL1_HEIGHT;

                CLDC_DataCore.DataBase.clsDataManage Item;

                if (clsMain.getIniString("Server", "Run") == "1")           //����������
                {
                    Cmb_TaiNum.Items.Clear();
                    Cmb_TaiNum.Items.Add("ȫ��");
                    Cmb_TaiNum.Visible = true;
                    Lab_Tai.Visible = true;
                    Item = new CLDC_DataCore.DataBase.clsDataManage(clsMain.getIniString("Server", "Host"), clsMain.getIniString("Server", "Name"), clsMain.getIniString("Server", "Pwd"));

                    if (!Item.Connection)
                    {
                        MessageBox.Show("���ݿ����ʧ�ܣ���������...\n������Ƿ������ˣ���ѡ����Ϣ�����еķ��������ʡ�", "���ݿ����ӳ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Cmb_TaiNum.Items.AddRange(Item.getTaiIDList().ToArray());
                    Cmb_TaiNum.SelectedIndex = 0;
                    Item.DbClose();

                    Item = null;

                }

            }
            else
            {
                Panel_Screen.Visible = false;
                Sp_Control.Panel1Collapsed = true;
            }
        }

        #region ������������������
        private void StartLoadTimeAndData()
        {
            try
            {
                #region load Time
                Tl_Print.Checked = false;
                Panel_Print.Visible = false;
                if (Tl_Screen.Checked)
                {
                    Panel_Screen.Visible = true;
                    Sp_Control.Panel1Collapsed = false;
                    Sp_Control.SplitterDistance = CONST_PANEL_CONTROL_PANEL1_HEIGHT;

                    CLDC_DataCore.DataBase.clsDataManage Item;

                    if (clsMain.getIniString("Server", "Run") == "1")           //����������
                    {
                        Cmb_TaiNum.Items.Clear();
                        Cmb_TaiNum.Items.Add("ȫ��");
                        Cmb_TaiNum.Visible = true;
                        Lab_Tai.Visible = true;
                        Item = new CLDC_DataCore.DataBase.clsDataManage(clsMain.getIniString("Server", "Host"), clsMain.getIniString("Server", "Name"), clsMain.getIniString("Server", "Pwd"));

                        if (!Item.Connection)
                        {
                            MessageBox.Show("���ݿ����ʧ�ܣ���������...\n������Ƿ������ˣ���ѡ����Ϣ�����еķ��������ʡ�", "���ݿ����ӳ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        Cmb_TaiNum.Items.AddRange(Item.getTaiIDList().ToArray());
                        Cmb_TaiNum.SelectedIndex = 0;
                        Item.DbClose();

                        Item = null;

                    }

                }
                else
                {
                    Panel_Screen.Visible = false;
                    Sp_Control.Panel1Collapsed = true;
                }
                #endregion
                Chk_Tj1.Checked = true;
                Cmb_Name1.Text = "�춨����";
                Cmb_Value1.SelectedIndex = 0;
                #region ��������
                List<string> ScreenItem = new List<string>();

                List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items;


                for (int i = 0; i < ChkList.Count; i++)
                {
                    if (!ChkList[i].Checked) continue;
                    if (CmbNameList[i].Text == "") continue;
                    if (CmbFhList[i].Text == "") continue;
                    if (CmbValueList[i].Text == "") continue;

                    if ((CLDC_DataCore.DataBase.clsDataManage.ScreenType)CmbNameList[i].SelectedIndex == CLDC_DataCore.DataBase.clsDataManage.ScreenType.�Ƿ����ϴ�)
                    {
                        if (CmbValueList[i].Text.Trim() == "��")
                        {
                            ScreenItem.Add(string.Format("{0},{1},{2}", CmbNameList[i].SelectedIndex.ToString(), CmbFhList[i].Text, "1"));
                        }
                        else if (CmbValueList[i].Text.Trim() == "��")
                        {
                            ScreenItem.Add(string.Format("{0},{1},{2}", CmbNameList[i].SelectedIndex.ToString(), CmbFhList[i].Text, "0"));
                        }
                    }
                    else
                    {
                        ScreenItem.Add(string.Format("{0},{1},{2}", CmbNameList[i].SelectedIndex.ToString(), CmbFhList[i].Text, CmbValueList[i].Text));
                    }
                }

                //if (ScreenItem.Count == 0)
                //{
                //    if (!System.IO.File.Exists(Application.StartupPath + @"\Tmp\tmp.ini"))
                //    {
                //        MessageBox.Show("û��ѡ���κβ�ѯ�������޷���ɲ�ѯ...", "��ѯʧ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        return;
                //    }
                //    string TmpData = clsMain.getIniString("System", "LastTmp", "", Application.StartupPath + @"\Tmp\tmp.ini");
                //    if (TmpData == string.Empty)
                //    {
                //        MessageBox.Show("û��ѡ���κβ�ѯ�������޷���ɲ�ѯ...", "��ѯʧ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        return;
                //    }
                //    Byte[] BytTmp = Comm.Function.File.ReadFileData(Application.StartupPath + @"\Tmp\" + TmpData);
                //    Items = ((CLDC_DataCore.Model.DnbModel.DnbGroupInfo)CTProtocol.CTPCommand.GetObject(BytTmp)).MeterGroup;
                //}
                //else
                //{
                CLDC_DataCore.DataBase.clsDataManage DataManage;

                if (clsMain.getIniString("Server", "Run") == "1")           //����������
                {
                    DataManage = new CLDC_DataCore.DataBase.clsDataManage(clsMain.getIniString("Server", "Host"), clsMain.getIniString("Server", "Name"), clsMain.getIniString("Server", "Pwd"));
                }
                else   //���ط���
                {
                    DataManage = new CLDC_DataCore.DataBase.clsDataManage(clsMain.getIniString("Path", "DataPath"), true);
                }
                if (!DataManage.Connection)
                {
                    MessageBox.Show("���ݿ����ʧ�ܣ���������...\n������Ƿ������ˣ���ѡ����Ϣ�����еķ��������ʡ�", "���ݿ����ӳ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                CLDC_DataCore.DataBase.clsDataManage.ScreenHeGeType HeGeType = new CLDC_DataCore.DataBase.clsDataManage.ScreenHeGeType();

                if (Opt_BHG.Checked)
                {
                    HeGeType = CLDC_DataCore.DataBase.clsDataManage.ScreenHeGeType.���ϸ�;
                }
                else if (Opt_HG.Checked)
                {
                    HeGeType = CLDC_DataCore.DataBase.clsDataManage.ScreenHeGeType.�ϸ�;
                }
                else
                {
                    HeGeType = CLDC_DataCore.DataBase.clsDataManage.ScreenHeGeType.ȫ��;
                }

                Items = DataManage.getScreenDnbInfo(ScreenItem, HeGeType);
                Meter_out = Items;
                DataManage.DbClose();
                //}
                this.RefreashGrid(Items);

                Sp_Control.Panel1Collapsed = true;
                Tl_Screen.Checked = false;
                Sp_Control.Panel1Collapsed = true;
                Sp_Data.SplitterDistance = Sp_Data.Height - Sp_Data.Panel2MinSize;     //�������λ��
                #endregion
            }
            catch
            { 
            
            }
            
        }
        #endregion


        /// <summary>
        /// ��ѯ�����б�ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_Main_SelectionChangeCommitted(object sender, EventArgs e)
        {
            for (int i = 0; i < CmbNameList.Count; i++)
            {
                if (CmbNameList[i] == (ComboBox)sender)
                {
                    CLDC_DataCore.DataBase.clsDataManage Item = this.NewConnection();
                    CmbValueList[i].Text = "";
                    CmbValueList[i].Items.Clear();

                    if (Item == null) return;
                    CLDC_DataCore.DataBase.clsDataManage.ScreenType selectScreenType = (CLDC_DataCore.DataBase.clsDataManage.ScreenType)CmbNameList[i].SelectedIndex;
                    if (selectScreenType == CLDC_DataCore.DataBase.clsDataManage.ScreenType.�Ƿ����ϴ�)
                    {
                        CmbValueList[i].Items.Add("��");
                        CmbValueList[i].Items.Add("��");
                    }
                    else
                    {
                        if (!Item.IsServer)     //���ʱ������ݿ�
                        {
                            CmbValueList[i].Items.AddRange(Item.GetDataFieldList((CLDC_DataCore.DataBase.clsDataManage.ScreenType)CmbNameList[i].SelectedIndex).ToArray());
                        }
                        else   //�����������ݷ�����
                        {
                            CmbValueList[i].Items.AddRange(Item.GetDataFieldList((CLDC_DataCore.DataBase.clsDataManage.ScreenType)CmbNameList[i].SelectedIndex
                                                          , Cmb_TaiNum.SelectedIndex == 0 ? "true" : Cmb_TaiNum.Text.Trim()).ToArray());
                        }
                    }
                    Item.DbClose();
                    Item = null;
                    CmbFhList[i].SelectedIndex = 0;
                    break;
                }
            }

        }


        /// <summary>
        /// ����ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_Main_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkList.Count; i++)
            {
                if (ChkList[i] == (CheckBox)sender)
                {
                    CmbNameList[i].Enabled = ChkList[i].Checked;
                    CmbFhList[i].Enabled = ChkList[i].Checked;
                    CmbValueList[i].Enabled = ChkList[i].Checked;
                    break;
                }
            }
        }

        /// <summary>
        /// ����������ѯ�����ݣ����DATAGRIDVIEW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Screen_Click(object sender, EventArgs e)
        {
            List<string> ScreenItem = new List<string>();

            List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items;

            if (this.BlnShowData)       //�������ϸ������ʾ״̬����ر�һ��
            {
                this.Lab_ShowData_Click(Lab_ShowData, e);
            }
            for (int i = 0; i < ChkList.Count; i++)
            {
                if (!ChkList[i].Checked) continue;
                if (CmbNameList[i].Text == "") continue;
                if (CmbFhList[i].Text == "") continue;
                if (CmbValueList[i].Text == "") continue;

                if ((CLDC_DataCore.DataBase.clsDataManage.ScreenType)CmbNameList[i].SelectedIndex == CLDC_DataCore.DataBase.clsDataManage.ScreenType.�Ƿ����ϴ�)
                {
                    if (CmbValueList[i].Text.Trim() == "��")
                    {
                        ScreenItem.Add(string.Format("{0},{1},{2}", CmbNameList[i].SelectedIndex.ToString(), CmbFhList[i].Text, "1"));
                    }
                    else if (CmbValueList[i].Text.Trim() == "��")
                    {
                        ScreenItem.Add(string.Format("{0},{1},{2}", CmbNameList[i].SelectedIndex.ToString(), CmbFhList[i].Text, "0"));
                    }
                }
                else
                {
                    ScreenItem.Add(string.Format("{0},{1},{2}", CmbNameList[i].SelectedIndex.ToString(), CmbFhList[i].Text, CmbValueList[i].Text));
                }
            }

            //if (ScreenItem.Count == 0)
            //{
            //    if (!System.IO.File.Exists(Application.StartupPath + @"\Tmp\tmp.ini"))
            //    {
            //        MessageBox.Show("û��ѡ���κβ�ѯ�������޷���ɲ�ѯ...", "��ѯʧ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return;
            //    }
            //    string TmpData = clsMain.getIniString("System", "LastTmp", "", Application.StartupPath + @"\Tmp\tmp.ini");
            //    if (TmpData == string.Empty)
            //    {
            //        MessageBox.Show("û��ѡ���κβ�ѯ�������޷���ɲ�ѯ...", "��ѯʧ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return;
            //    }
            //    Byte[] BytTmp = Comm.Function.File.ReadFileData(Application.StartupPath + @"\Tmp\" + TmpData);
            //    Items = ((CLDC_DataCore.Model.DnbModel.DnbGroupInfo)CTProtocol.CTPCommand.GetObject(BytTmp)).MeterGroup;
            //}
            //else
            //{
            CLDC_DataCore.DataBase.clsDataManage DataManage;

            if (clsMain.getIniString("Server", "Run") == "1")           //����������
            {
                DataManage = new CLDC_DataCore.DataBase.clsDataManage(clsMain.getIniString("Server", "Host"), clsMain.getIniString("Server", "Name"), clsMain.getIniString("Server", "Pwd"));
            }
            else   //���ط���
            {
                DataManage = new CLDC_DataCore.DataBase.clsDataManage(clsMain.getIniString("Path", "DataPath"), true);
            }
            if (!DataManage.Connection)
            {
                MessageBox.Show("���ݿ����ʧ�ܣ���������...\n������Ƿ������ˣ���ѡ����Ϣ�����еķ��������ʡ�", "���ݿ����ӳ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            CLDC_DataCore.DataBase.clsDataManage.ScreenHeGeType HeGeType = new CLDC_DataCore.DataBase.clsDataManage.ScreenHeGeType();

            if (Opt_BHG.Checked)
            {
                HeGeType = CLDC_DataCore.DataBase.clsDataManage.ScreenHeGeType.���ϸ�;
            }
            else if (Opt_HG.Checked)
            {
                HeGeType = CLDC_DataCore.DataBase.clsDataManage.ScreenHeGeType.�ϸ�;
            }
            else
            {
                HeGeType = CLDC_DataCore.DataBase.clsDataManage.ScreenHeGeType.ȫ��;
            }

            Items = DataManage.getScreenDnbInfo(ScreenItem, HeGeType);
            Meter_out = Items;
            DataManage.DbClose();
            //}
            this.RefreashGrid(Items);

            Sp_Control.Panel1Collapsed = true;
            Tl_Screen.Checked = false;
            Sp_Control.Panel1Collapsed = true;
            Sp_Data.SplitterDistance = Sp_Data.Height - Sp_Data.Panel2MinSize;     //�������λ��
        }
        /// <summary>
        /// ������ˢ��
        /// </summary>
        /// <param name="Items"></param>
        private void RefreashGrid(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items)
        {
            Dgv_Data.Rows.Clear();
            long lng_BHege = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                int RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[i].Tag = Items[i];
                Dgv_Data[1, RowIndex].Value = Items[i]._intTaiNo.Trim();
                Dgv_Data[2, RowIndex].Value = Items[i].Mb_intBno.ToString().Trim();
                Dgv_Data[3, RowIndex].Value = Items[i]._intMyId;  //���Ӽ������
                Dgv_Data[4, RowIndex].Value = Items[i].Mb_ChrJlbh;
                Dgv_Data[5, RowIndex].Value = Items[i].Mb_ChrCcbh;
                Dgv_Data[6, RowIndex].Value = Items[i].Mb_DatJdrq;
                Dgv_Data[7, RowIndex].Value = Items[i].Mb_ChrBmc;
                Dgv_Data[8, RowIndex].Value = Items[i].Mb_Bxh;
                Dgv_Data[9, RowIndex].Value = Items[i].Mb_chrQianFeng1;
                Dgv_Data[10, RowIndex].Value = Items[i].Mb_chrQianFeng2;
                Dgv_Data[11, RowIndex].Value = Items[i].Mb_chrQianFeng3;
                Dgv_Data[12, RowIndex].Value = Items[i].Mb_chrzzcj;
                Dgv_Data[13, RowIndex].Value = Items[i].Mb_ChrJyy;
                Dgv_Data[14, RowIndex].Value = Items[i].Mb_ChrHyy;
                Dgv_Data[15, RowIndex].Value = Items[i].Mb_chrResult;
                Dgv_Data[16, RowIndex].Value = Items[i].Mb_BlnToMis ? "���ϴ�" : "δ�ϴ�";
                //Dgv_Data[13, RowIndex].Value = Items[i].Mb_chrResult;
                //Dgv_Data[14, RowIndex].Value = Items[i].Mb_BlnToMis ? "���ϴ�" : "δ�ϴ�";
                //Dgv_Data[15, RowIndex].Value = Items[i].Mb_chrQianFeng1;
                //Dgv_Data[16, RowIndex].Value = Items[i].Mb_chrQianFeng2;

                if (Items[i].Mb_chrResult == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                {
                    Dgv_Data.Rows[RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                    lng_BHege++;
                }
            }
            St_Label.Text = string.Format("���β�ѯ����ѯ�� {0:D} �����ݣ����кϸ����� {1:D} �����ϸ���Ϊ {2:F}%", Items.Count, Items.Count - lng_BHege, Math.Round((Double)(Items.Count - lng_BHege) / Items.Count, 4) * 100);
        }

        #endregion


        #region ���ݽ����ʾ���
        /// <summary>
        /// ��ϸ��Ϣ��ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Lab_ShowData_Click(object sender, EventArgs e)
        {
            if (!BlnShowData)
            {
                if (Dgv_Data.SelectedRows.Count == 0)
                {
                    MessageBox.Show("û��ѡ����Ҫ�鿴��ϸ���ݵľ���������...", "���ݲ�ѯ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                Lab_ShowData.Image = global::CLDC_DataManager.Properties.Resources.AllClose;
                this.ShowData((CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)Dgv_Data.SelectedRows[0].Tag);
                Sp_Data.SplitterDistance = Sp_Data.Height / 2;              //�����λ��
                BlnShowData = true;
            }
            else
            {
                Lab_ShowData.Image = global::CLDC_DataManager.Properties.Resources.All;
                Sp_Data.SplitterDistance = Sp_Data.Height - Sp_Data.Panel2MinSize;        //�����λ��
                BlnShowData = false;
                _MeterInfo = null;
            }
        }
        /// <summary>
        /// �л�ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Data_SelectionChanged(object sender, EventArgs e)
        {
            if (!BlnShowData) return;
            if (Dgv_Data.SelectedRows.Count == 0) return;
            if (Dgv_Data.SelectedRows[0].Tag is CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)
            {
                this.ShowData((CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)Dgv_Data.SelectedRows[0].Tag);
            }
        }

        /// <summary>
        /// ��ϸ�������
        /// </summary>
        /// <param name="Item"></param>
        private void ShowData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item)
        {
            CLDC_DataCore.DataBase.clsDataManage DataManage = this.NewConnection();

            if (DataManage == null) return;

            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = DataManage.getDnbInfoNew(Item, true);
            _MeterInfo = MeterInfo;
            //View_MeterData = new CLDC_MeterUI.DisplayInfo.DisplayMeterDetailInfo();
            //View_MeterInfo = new CLDC_MeterUI.DisplayInfo.MeterBasicInfo();
            displayMeterDetailInfo1.SetData(MeterInfo, false);
            meterBasicInfo1.SetData(MeterInfo, false);
        }

        /// <summary>
        /// ��ʾ����ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Data_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && Dgv_Data.Rows[e.RowIndex].Tag is CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)
            {
                this.Tip_Info.ToolTipTitle = "������Ϣ";

                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Info = Dgv_Data.Rows[e.RowIndex].Tag as CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo;

                string TipString = string.Format("���쳧��:{0}��������:{1}\n" +
                                   "��������:{2}�����ͺ�:{3}\n" +
                                   "���ѹ:{4}�궨����:{5}\n" +
                                   "���߳���:{6}���ߵȼ�:{7}\n" +
                                   "���뷽ʽ:{8}����ֹ��:{9}\n" +
                                   "���߷�ʽ:{10}����Ƶ��:{11}",
                                   Info.Mb_chrzzcj.PadRight(20),
                                   Info.Mb_chrCcrq.PadRight(20),
                                   Info.Mb_chrBlx.PadRight(20),
                                   Info.Mb_Bxh.PadRight(20),
                                   Info.Mb_chrUb.PadRight(20),
                                   Info.Mb_chrIb.PadRight(20),
                                   Info.Mb_chrBcs.PadRight(20),
                                   Info.Mb_chrBdj.PadRight(20),
                                   Info.Mb_BlnHgq ? ("������������").PadRight(20) : ("ֱ�ӽ���").PadRight(20),
                                   Info.Mb_BlnZnq ? ("��ֹ��").PadRight(20) : ("��ֹ��").PadRight(20),
                                   ((CLDC_Comm.Enum.Cus_Clfs)Info.Mb_intClfs).ToString().PadRight(20),
                                   Info.Mb_chrHz.PadRight(20));

                this.Tip_Info.Show(TipString, this.Dgv_Data, 5000);
            }
        }

        #endregion


        /// <summary>
        /// ȫ��ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (Dgv_Data.Rows.Count == 0)
            {
                Chk_SelectAll.Checked = false;
                return;
            }
            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                ((DataGridViewCheckBoxCell)Dgv_Data.Rows[i].Cells[0]).Value = Chk_SelectAll.Checked;
            }
        }
        /// <summary>
        /// ��ʼ����ѯ��ʽ
        /// </summary>
        private void DefaultQueryControl()
        {
            for (int i = 0; i < ChkList.Count; i++)
            {
                ChkList[i].Checked = false;
                CmbNameList[i].SelectedIndex = -1;
                CmbFhList[i].SelectedIndex = -1;
                CmbValueList[i].Text = "";
            }
        }

        #region ������ʽ��� 
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_Main_Load(object sender, EventArgs e)
        {
            //Frm_Main_Limited();//lee ����Ȩ��
            
            Sp_Control.Panel1Collapsed = true;
            Sp_Data.SplitterDistance = Sp_Data.Height - Sp_Data.Panel2MinSize;     //�������λ��
            this.LoadYjAndBzList();

            if (!clsMain.isLoadReportInterface)
            {
                Tl_Print.Enabled = false;
            }
            if (!clsMain.isLoadMisInterface)
            {
                Tl_Update.Enabled = false;
            }
        }

        /// <summary>
        /// Ȩ�����ô��� lees
        /// </summary>
        /// 
        private void Frm_Main_Limited()
        { 
            //string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
            //string Sql_word_2 = ";Persist Security Info=False";
            //string fakepath = AppDomain.CurrentDomain.BaseDirectory + @"DataBase\ClouConfig.mdb";

            //string path = AppDomain.CurrentDomain.BaseDirectory + @"\UserBase\ClouConfig.mdb";
            //List<string> temp = new List<string>();
            // using (OleDbConnection conn = new OleDbConnection(Sql_word_1 + path + Sql_word_2))
            //{
            //    if (conn.State == ConnectionState.Closed)
            //        conn.Open();
            //    string sql = "SELECT chrQX FROM Info_User WHERE chrGrpCode = 'loging' ";

            //    OleDbCommand cmd = new OleDbCommand(sql, conn);


            //    OleDbDataReader myReader = null;
            //    myReader = cmd.ExecuteReader();

            //    while (myReader.Read())
            //    {
                    
            //        temp.Add(myReader["chrQx"].ToString());
            //    }
                 
            //    string sql2="UPDATE Info_User SET chrGrpCode = '0' WHERE chrGrpCode = 'loging'";

            //     OleDbCommand cmd2=new OleDbCommand (sql2,conn);

            //    cmd2.ExecuteNonQuery();
            //}
            // if (temp.Count != 0)
            // {
            //     switch (temp[0])
            //     {
            //         case "1":
            //             Tl_Print.Enabled = false;
            //             Tl_Info.Enabled = false;
            //             break;
            //         case "2":
            //             Tl_Info.Enabled = false;
            //             break;
            //         case "3":
            //             break;
            //         default:
            //             break;

            //     }

            // }



        }
        


        private void LoadYjAndBzList()
        {
            int InfoCount = int.Parse(clsMain.getIniString("�춨����", "JCount", "0"));
            Cmb_Jdyj.Items.Clear();
            for (int i = 1; i <= InfoCount; i++)
            {
                Cmb_Jdyj.Items.Add(clsMain.getIniString("�춨����", i.ToString()));
            }
            InfoCount = int.Parse(clsMain.getIniString("�����׼", "ZCount", "0"));
            for (int i = 1; i <= InfoCount; i++)
            {
                Cmb_Zzbz.Items.Add(clsMain.getIniString("�����׼", i.ToString()));
            }
            if (Cmb_Jdyj.Items.Count > 0)
            {
                Cmb_Jdyj.SelectedIndex = 0;
            }
            if (Cmb_Zzbz.Items.Count > 0)
            {
                Cmb_Zzbz.SelectedIndex = 0;
            }
        }



        private delegate void DgtRefreshTabs();
        /// <summary>
        /// ��ϸ��Ϣ�ؼ���Сˢ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sp_Data_Panel2_SizeChanged(object sender, EventArgs e)
        {
            if (Tab_Data.IsHandleCreated)
            {
                Tab_Data.BeginInvoke(new DgtRefreshTabs(RefreshTabs));
            }
        }

        private void RefreshTabs()
        {
            TabPage TmpItem = Tab_Data.SelectedTab;

            Tab_Data.SelectedTab = Tab_Data.TabPages[Tab_Data.TabPages.Count - 1];
            Tab_Data.SelectedTab = Tab_Data.TabPages[0];
            Tab_Data.SelectedTab = TmpItem;
        }

        /// <summary>
        /// �����С�仯�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_Main_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (!BlnShowData)
                {
                    Sp_Data.SplitterDistance = Sp_Data.Height - Sp_Data.Panel2MinSize;     //�������λ��
                }
                if (Sp_Control.Height != 0)
                {
                    Sp_Control.Panel2MinSize = Sp_Control.Height - CONST_PANEL_CONTROL_PANEL1_HEIGHT;
                }
                if (!Sp_Control.Panel1Collapsed)     //��������۵�״̬
                {
                    Sp_Control.SplitterDistance = CONST_PANEL_CONTROL_PANEL1_HEIGHT;
                }
            }
            catch
            { }
        }

        #endregion

        #region ϵͳ��ť�¼�

        /// <summary>
        /// ϵͳ���ð�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tl_Info_Click(object sender, EventArgs e)
        {
            Frm_SystemInfo Item = new Frm_SystemInfo();
            Item.ShowDialog();
            this.LoadYjAndBzList();

            Cmb_TaiNum.Items.Clear();
            if (clsMain.getIniString("Server", "Run", "0") == "1")          //����Ǵӷ�������ȡ���ݣ�����Ҫ��ʾ̨��ѡ��
            {
                Cmb_TaiNum.Items.Add("ȫ��");

                Lab_Tai.Visible = true;
                Cmb_TaiNum.Visible = true;

            }
            else
            {
                Lab_Tai.Visible = false;
                Cmb_TaiNum.Visible = false;
            }

            this.DefaultQueryControl();
        }

        #endregion

        

        /// <summary>
        /// ����һ�����ݿ�������Ӷ���
        /// </summary>
        /// <returns></returns>
        private CLDC_DataCore.DataBase.clsDataManage NewConnection()
        {
            CLDC_DataCore.DataBase.clsDataManage DataManage;

            if (clsMain.getIniString("Server", "Run") == "1")           //����������
            {
                DataManage = new CLDC_DataCore.DataBase.clsDataManage(clsMain.getIniString("Server", "Host"), clsMain.getIniString("Server", "Name"), clsMain.getIniString("Server", "Pwd"));
            }
            else   //���ط���
            {
                DataManage = new CLDC_DataCore.DataBase.clsDataManage(clsMain.getIniString("Path","DataPath"),true);
            }
            if (!DataManage.Connection)
            {
                MessageBox.Show("���ݿ����ʧ�ܣ���������...\n������Ƿ������ˣ���ѡ����Ϣ�����еķ��������ʡ�", "���ݿ����ӳ���", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            return DataManage;
        }


        #region �����ӡ���
        /// <summary>
        /// ��ʾ�����ӡ����ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tl_Print_Click(object sender, EventArgs e)
        {
            if (Dgv_Data.SelectedRows.Count == 0)
            {
                MessageBox.Show("û��ѡ���κ���Ҫ��ӡ��������Ϣ...", "��ӡ����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Tl_Screen.Checked = false;
            Panel_Screen.Visible = false;

            if (Tl_Print.Checked)
            {
                Panel_Print.Visible = true;
                Sp_Control.Panel1Collapsed = false;
                Sp_Control.SplitterDistance = CONST_PANEL_CONTROL_PANEL1_HEIGHT;

                Cmb_PrintStyle.Items.Clear();

                foreach (string s in clsMain.report.ReportTaoXing())
                {
                    Cmb_PrintStyle.Items.Add(s);
                }
                if (Cmb_PrintStyle.Items.Count == 0) return;

                Cmb_PrintStyle.SelectedIndex = 0;

                this.Cmb_PrintStyle_SelectionChangeCommitted(Cmb_PrintStyle, new EventArgs());
            }
            else
            {
                Panel_Print.Visible = false;
                Sp_Control.Panel1Collapsed = true;
            }

        }

        /// <summary>
        /// ģ����ʽѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_PrintStyle_SelectionChangeCommitted(object sender, EventArgs e)
        {

            Cmb_PrintType.Items.Clear();

            string[] arrType = clsMain.report.ReportType(Cmb_PrintStyle.SelectedIndex + 1);

            if (arrType.Length == 0) return;

            Cmb_PrintType.Items.AddRange(arrType);

        }

        /// <summary>
        /// ��ʼ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Print_Click(object sender, EventArgs e)
        {
            if (Cmb_PrintStyle.SelectedIndex < 0)
            {
                MessageBox.Show("��ѡ���ӡ��ʽ...", "��ӡʧ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cmb_PrintStyle.Focus();
                return;
            }
            if (Cmb_PrintType.SelectedIndex < 0)
            {
                MessageBox.Show("��ѡ���ӡ����...", "��ӡʧ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cmb_PrintType.Focus();
                return;
            }

            #region �жϱ�Ľ��ۣ���ʾ�ͻ�
            string meterResult = "";
            for (int i = Dgv_Data.SelectedRows.Count - 1; i >= 0; i--)
            {

                if (Dgv_Data.SelectedRows[i].Tag is CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)
                {
                    meterResult = Dgv_Data[15, i].Value.ToString().Trim();
                   
                    break;
                }
            }
            if (Cmb_PrintType.Text.ToString().Contains("�춨") && meterResult=="���ϸ�")
            {

                DialogResult result = MessageBox.Show("�õ��Ľ���Ϊ���ϸ�ȷ��Ҫ��ӡ��" + Cmb_PrintType.Text.ToString().Trim()+ "����", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (result == DialogResult.OK)
                {
                    //ִ��OK�Ĵ���
                }
                else
                {
                    return;
                }
            }
            #endregion

            List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items = new List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo>();

            CLDC_DataCore.DataBase.clsDataManage DataManage = this.NewConnection();

            if (Cmb_PrintType.Text.ToString().Contains("У׼"))
            {
                if (Cmb_PrintType.Text.ToString().Contains("ԭʼ"))
                {
                    clsMain.WriteIni("Certi", "Start", " ",System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
            
                }
                else
                {
                    clsMain.WriteIni("Certi", "Start", "X", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
                }

                }
                
            else
            {

                if (Cmb_PrintType.Text.ToString().Contains("ԭʼ"))
                {
                    clsMain.WriteIni("Certi", "Start", " ", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

                }
                else
                {
                    clsMain.WriteIni("Certi", "Start", "J", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
                }
               
            }
            string TEXTU = clsMain.getIniString("Certi", "CheckDate", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini").ToString();
            try
            {
                int testY = Convert.ToInt16(TEXTU);
                //int testX = Convert.ToInt16(Convert.ToDateTime(Cmb_Value1.Text).ToString("yyyy"));
                int testX;
                if (Cmb_Value1.Text == "")
                {
                    testX = Convert.ToInt16(Convert.ToDateTime(clsMain.getIniString("Certi", "cmb_value", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini")).ToString("yyyy"));
                }
                else
                {
                    testX = Convert.ToInt16(Convert.ToDateTime(Cmb_Value1.Text).ToString("yyyy"));
                }

                if (testX > testY)
                {
                    clsMain.WriteIni("Certi", "CheckDate", Convert.ToInt16(Convert.ToDateTime(Cmb_Value1.Text).ToString("yyyy")).ToString(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
                    clsMain.WriteIni("OtherInfo", "ZSBH" + "_" + Convert.ToDateTime(Cmb_Value1.Text).ToString("yyyy"), "00001", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
                }
            }
            catch (Exception ex_error)
            {
                //int testX=Dgv_Data.Rows[0].Cells[6].Value.ToString();
            }
         

             if (DataManage == null) return;

            for (int i = Dgv_Data.SelectedRows.Count - 1; i >= 0; i--)
            {

                if (Dgv_Data.SelectedRows[i].Tag is CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)
                {
                    Items.Add(DataManage.getDnbInfoNew((CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)Dgv_Data.SelectedRows[i].Tag, true));
                }
            }

            if (Items.Count > 0)
            {
                Panel_Print.Enabled = false;
                Tool_System.Enabled = false;
                clsMain.LoadReportScript(Items, Cmb_PrintStyle.SelectedIndex + 1, Cmb_PrintType.SelectedIndex, Cmb_Jdyj.Text, Cmb_Zzbz.Text);
                Panel_Print.Enabled = true;
                Tool_System.Enabled = true;
            }

        }

        #endregion

        #region ���ݾ� ��ӡ���ֱ���
        private void RePringt()
        {
            if (Cmb_PrintStyle.SelectedIndex < 0)
            {
                MessageBox.Show("��ѡ���ӡ��ʽ...", "��ӡʧ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cmb_PrintStyle.Focus();
                return;
            }
            if (Cmb_PrintType.SelectedIndex < 0)
            {
                MessageBox.Show("��ѡ���ӡ����...", "��ӡʧ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cmb_PrintType.Focus();
                return;
            }

            List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items = new List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo>();

            CLDC_DataCore.DataBase.clsDataManage DataManage = this.NewConnection();

            if (Cmb_PrintType.Text.ToString().Contains("У׼"))
            {
                if (Cmb_PrintType.Text.ToString().Contains("ԭʼ"))
                {
                    clsMain.WriteIni("Certi", "Start", " ", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

                }
                else
                {
                    clsMain.WriteIni("Certi", "Start", "X", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
                }

            }

            else
            {

                if (Cmb_PrintType.Text.ToString().Contains("ԭʼ"))
                {
                    clsMain.WriteIni("Certi", "Start", " ", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");

                }
                else
                {
                    clsMain.WriteIni("Certi", "Start", "J", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
                }

            }
            string TEXTU = clsMain.getIniString("Certi", "CheckDate", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini").ToString();
            int testY = Convert.ToInt16(TEXTU);
            //int testX = Convert.ToInt16(Convert.ToDateTime(Cmb_Value1.Text).ToString("yyyy"));
            int testX;
            if (Cmb_Value1.Text == "")
            {
                testX = Convert.ToInt16(Convert.ToDateTime(clsMain.getIniString("Certi", "cmb_value", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini")).ToString("yyyy"));
            }
            else
            {
                testX = Convert.ToInt16(Convert.ToDateTime(Cmb_Value1.Text).ToString("yyyy"));
            }

            if (testX > testY)
            {
                clsMain.WriteIni("Certi", "CheckDate", Convert.ToInt16(Convert.ToDateTime(Cmb_Value1.Text).ToString("yyyy")).ToString(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
                clsMain.WriteIni("OtherInfo", "ZSBH" + "_" + Convert.ToDateTime(Cmb_Value1.Text).ToString("yyyy"), "00001", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");


            }
            if (DataManage == null) return;

            for (int i = Dgv_Data.SelectedRows.Count - 1; i >= 0; i--)
            {

                if (Dgv_Data.SelectedRows[i].Tag is CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)
                {
                    Items.Add(DataManage.getDnbInfoNew((CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)Dgv_Data.SelectedRows[i].Tag, true));
                }
            }

            if (Items.Count > 0)
            {
                Panel_Print.Enabled = false;
                Tool_System.Enabled = false;
                clsMain.LoadReportScript(Items, Cmb_PrintStyle.SelectedIndex + 1, Cmb_PrintType.SelectedIndex, Cmb_Jdyj.Text, Cmb_Zzbz.Text);
                Panel_Print.Enabled = true;
                Tool_System.Enabled = true;
            }
        }

        #endregion

        #region ���ݽӿ��ϴ�����
        /// <summary>
        /// �����ϴ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
      

        #endregion

        

        #region �춨ǰ���ݵ���
        private void Tl_DataImport_Click(object sender, EventArgs e)
        {
            Frm_DataImport Item = new Frm_DataImport();
            Item.ShowDialog();
        }
        #endregion

        private bool IsHzFh()
        {
            return clsMain.getIniString("datasetup", "hzfh", "0") == "0" ? false : true;
        }



        /// <summary>
        /// �������Ϣɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_Del_Click(object sender, EventArgs e)
        {
            if (Dgv_Data.SelectedRows.Count == 0) return;

            CLDC_DataCore.DataBase.clsDataManage Manage = NewConnection();

            if (!Manage.Connection) return;

            foreach (DataGridViewRow DRow in Dgv_Data.SelectedRows)
            {
                if (DRow.Tag is CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)
                {
                    if (Manage.DeleteMeterInfo(((CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)DRow.Tag)._intMyId, ((CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)DRow.Tag)._intTaiNo))
                    {
                        Dgv_Data.Rows.Remove(DRow);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("�����Ϊ��{0}�ı���Ϣɾ��ʧ��...", DRow.Cells[1].Value.ToString()), "ɾ��ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// ��ʾ�������ϸ��Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Menu_ShowInfo_Click(object sender, EventArgs e)
        {
            if (BlnShowData)
            {
                Menu_ShowInfo.Text = "�鿴��ϸ��Ϣ";
            }
            else
            {
                Menu_ShowInfo.Text = "�رղ鿴";
            }
            this.Lab_ShowData_Click(Lab_ShowData, e);
        }

        /// <summary>
        /// ϵͳ��Ϣ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tl_BasicData_Click(object sender, EventArgs e)
        {
            CLDC_MeterUI.UI_SystemManager _SystemConfig = new CLDC_MeterUI.UI_SystemManager(CLDC_DataCore.Const.GlobalUnit.g_SystemConfig);
            _SystemConfig.ShowD();

        }

        private void Dgv_Data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {//�޸ļ춨���ݽ�� lsx
            if (_MeterInfo == null) return;
            CLDC_DataCore.DataBase.DataControl datacontrl = new CLDC_DataCore.DataBase.DataControl();
            if (datacontrl == null) return;
            if (datacontrl.Connection == false) return;
          
            string Result = Dgv_Data[13, e.RowIndex].Value.ToString().Trim();  //�ܽ���
            string hyy = Dgv_Data[12, e.RowIndex].Value.ToString().Trim();//������
            string jyy = Dgv_Data[11, e.RowIndex].Value.ToString().Trim();//У��Ա
            string sjdw = Dgv_Data[10 , e.RowIndex].Value.ToString().Trim();//�ͼ쵥λ
            string TableName = "METER_INFO";
            string Updatesql = string.Format("Update " + TableName + " Set AVR_TOTAL_CONCLUSION='{1}',AVR_TEST_PERSON='{2}',AVR_AUDIT_PERSON='{3}',AVR_CUSTOMER='{4}' WHERE PK_LNG_METER_ID='{0}'", _MeterInfo._intMyId, Result, jyy, hyy,sjdw);

          int cout=  datacontrl.ExecuteSql(Updatesql);

            datacontrl.CloseDB();
        }

        private void Tl_ExcelForDetailData_Click(object sender, EventArgs e)
        {

            if (Dgv_Data.SelectedRows.Count == 0)
            {
                MessageBox.Show("û��ѡ���κ����ݣ��޷���ɵ���...", "Excel����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items = new List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo>();

            CLDC_DataCore.DataBase.clsDataManage DataManage = this.NewConnection();

            if (DataManage == null) return;

            Dgv_Data.EndEdit();

            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Dgv_Data.Rows[i].Cells[0].Value == null) continue;
                Application.DoEvents();
                if ((Dgv_Data.Rows[i].Cells[0].Value.ToString() == "1" || Dgv_Data.Rows[i].Cells[0].Value.ToString().ToLower() == "true") && Dgv_Data.Rows[i].Tag is CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)
                {
                    Items.Add(DataManage.getDnbInfoNew((CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)Dgv_Data.Rows[i].Tag, true));
                }
            }

            if (Items.Count > 0)
            {
                clsExcelControl Excel = new clsExcelControl();

                string Path = Excel.CreateExcelForDetailed(Items);

                if (Path != string.Empty)
                {
                    MessageBox.Show("���ɳɹ�������·��Ϊ��" + Path, "Excel����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Excel = null;
            }
        }

        private void Tl_ExcelForThanData_Click(object sender, EventArgs e)
        {
            if (Dgv_Data.SelectedRows.Count == 0)
            {
                MessageBox.Show("û��ѡ���κ����ݣ��޷���ɵ���...", "Excel����", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items = new List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo>();

            CLDC_DataCore.DataBase.clsDataManage DataManage = this.NewConnection();

            if (DataManage == null) return;

            Dgv_Data.EndEdit();

            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Dgv_Data.Rows[i].Cells[0].Value == null) continue;
                Application.DoEvents();
                if ((Dgv_Data.Rows[i].Cells[0].Value.ToString() == "1" || Dgv_Data.Rows[i].Cells[0].Value.ToString().ToLower() == "true") && Dgv_Data.Rows[i].Tag is CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)
                {
                    Items.Add(DataManage.getDnbInfoNew((CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo)Dgv_Data.Rows[i].Tag, true));
                }
            }

            if (Items.Count > 0)
            {
                clsExcelControl Excel = new clsExcelControl();

                string Path = Excel.CreateExcelForThanDataThree(Items);

                if (Path != string.Empty)
                {
                    MessageBox.Show("���ɳɹ�������·��Ϊ��" + Path, "Excel����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Excel = null;
            }
        }

        private void Tl_SwitchName_Click(object sender, EventArgs e)
        {
            CLDC_ManagerStart.ManagerStart loginFrom = new ManagerStart();
            
            loginFrom.Show();
            loginFrom.MdiParent = null;
            this.Close();
            string path = System.Windows.Forms.Application.StartupPath.Substring(1, System.Windows.Forms.Application.StartupPath.LastIndexOf("\\") - 1);
            Process.Start(System.Windows.Forms.Application.StartupPath + "\\CLDC_ManagerStart.exe");
                    
            
        }

        private void tl_findFile_Click(object sender, EventArgs e)
        {
            Process.Start(System.Windows.Forms.Application.StartupPath + "\\viewFile\\ViewLocalFile.exe");
        }

        private void btn_printLable_Click(object sender, EventArgs e)
        {
            if (Dgv_Data.Rows.Count == 0)
            {
                MessageBox.Show("�빴ѡҪ��ӡ�ı�");
                return;
            }
            int result = 5;
            string selectItem = "", str_time = "", txt_jyyName = "";
            List<string> Lis_Zcbh = new List<string>();
            str_time = Convert.ToDateTime(clsMain.getIniString("Certi", "cmb_value", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini")).ToShortDateString().ToString();
            List<string> Lis_man = new List<string>();
            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {

                DataGridViewCheckBoxCell chkBoxCell = (DataGridViewCheckBoxCell)Dgv_Data.Rows[i].Cells[0];
                DataGridViewTextBoxCell txtBoxCell = (DataGridViewTextBoxCell)Dgv_Data.Rows[i].Cells[4];
                txt_jyyName = ((DataGridViewTextBoxCell)Dgv_Data.Rows[i].Cells[13]).Value.ToString();
                if (chkBoxCell != null && ((bool)chkBoxCell.EditingCellFormattedValue == true || (bool)chkBoxCell.FormattedValue == true))
                {
                    selectItem = selectItem + "," + (i+1).ToString();

                    Lis_Zcbh.Add(txtBoxCell.Value.ToString());
                    
                }
            }
            foreach (string temp in Lis_Zcbh)
            {
               result= PrintLable.Print.PrintTheLable(temp, txt_jyyName, str_time);
                //Print(temp, txt_jyyName, str_time);
            }
            if (result == 0)
            {
                MessageBox.Show("��ӡ�ɹ���");
            }
            else
            {
                MessageBox.Show("��ӡʧ�ܣ������ǩ�����Ƿ������ϣ��Լ��ͺ��Ƿ�ΪPOSTEK C168/300s��");
            }
           
            
        }


        public static void Print(string lis_zcbh, string man, string timeForCheck)
        {

            PrintLab.OpenPort("POSTEK C168/300s");//�򿪴�ӡ���˿�
            PrintLab.PTK_ClearBuffer();           //��ջ�����
            PrintLab.PTK_SetPrintSpeed(4);        //���ô�ӡ�ٶ�
            PrintLab.PTK_SetDarkness(20);         //���ô�ӡ�ڶ�
            PrintLab.PTK_SetLabelHeight(300, 16); //���ñ�ǩ�ĸ߶ȺͶ�λ��϶\����\���׵ĸ߶�
            PrintLab.PTK_SetLabelWidth(900);

            for (int i = 1; i <= 1; i++)
            {
                //PrintLab.PTK_DrawTextTrueTypeW(200, 300, 40, 40, "����", 1, 400, false, true, true, "1", "12456789");//��ӡһ�� TrueType Font����
                //PrintLab.PTK_DrawBarcode(100, 20, 0, "1", 3, 3, 80, 'N', "12345");//��ӡһ������
                //PrintLab.PTK_SetPagePrintCount(1, 1);//�����ӡ��ִ�д�ӡ����

                //// ������
                //PrintLab.PTK_DrawRectangle(58, 15, 3, 558, 312);


                //// ��ӡPCXͼƬ ��ʽһ
                //PrintLab.PTK_PcxGraphicsDel("PCX");
                //PrintLab.PTK_PcxGraphicsDownload("PCX", "logo.pcx");
                //PrintLab.PTK_DrawPcxGraphics(80, 20, "PCX");

                //// ��ӡPCXͼƬ ��ʽ��
                //// PTK_PrintPCX(80,30,pchar('logo.pcx'));

                //// ��ӡһ������;
                //PrintLab.PTK_DrawBarcode(300, 23, 0, "1", 2, 2, 50, 'B', "123456789");              

                //// �����ָ���
                //PrintLab.PTK_DrawLineOr(58, 100, 500, 3);               

                //// ��ӡһ��TrueTypeFont����;
                //PrintLab.PTK_DrawTextTrueTypeW(80, 120, 40, 0, "Arial", 1, 400, false, false, false, "A1", "TrueTypeFont");                

                // ��ӡһ���ı�����(���������������);
                //PrintLab.PTK_DrawText(380, 180, 2, 2, 1, 1, 'N', "09001FE00000000007963031");
                //PrintLab.PTK_DrawText(380, 150, 2, 2, 1, 1, 'N', "09001FE00000000007963031");

                string zcbh_patr1 = "", zcbh_part2 = "";
                zcbh_patr1 = lis_zcbh.Substring(1, (lis_zcbh.Length / 2));
                zcbh_part2 = lis_zcbh.Substring((lis_zcbh.Length / 2), (lis_zcbh.Length / 2));


                PrintLab.PTK_DrawText(150, 165 - 40, 0, 2, 1, 1, 'N', zcbh_patr1);
                PrintLab.PTK_DrawText(150, 190 - 40, 0, 2, 1, 1, 'N', zcbh_part2);
                //Console.WriteLine(i);
                PrintLab.PTK_DrawText(150, 220 - 30, 0, 2, 1, 1, 'N', timeForCheck);
                PrintLab.PTK_DrawText(150, 275 - 30, 0, 2, 1, 1, 'N', man);


                

                //// ��ӡPDF417��
                //PrintLab.PTK_DrawBar2D_Pdf417(80, 210, 400, 300, 0, 0, 3, 7, 10, 2, 0, 0, "123456789");//PDF417��

                //// ��ӡQR��
                //PrintLab.PTK_DrawBar2D_QR(420, 120, 180, 180, 0, 3, 2, 0, 0, "Postek Electronics Co., Ltd.");


                //// ��ӡһ��TrueTypeFont������ת;
                //PrintLab.PTK_DrawTextTrueTypeW(520, 102, 22, 0, "Arial", 2, 400, false, false, false, "A2", "www.postek.com.cn");
                //PrintLab.PTK_DrawTextTrueTypeW(80, 260, 19, 0, "Arial", 1, 700, false, false, false, "A3", "Use different ID_NAME for different Truetype font objects");


                // �����ӡ��ִ�д�ӡ����
                PrintLab.PTK_PrintLabel(1, 1);
                PrintLab.ClosePort();//�رմ�ӡ���˿�
            }
        }

        private void ����Ǧ����ϢToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> lis_seal_01 = new List<string>();
            List<string> lis_seal_02 = new List<string>();
            List<string> lis_seal_03= new List<string>();
            List<string> lis_meter_zcbh = new List<string>();
            string CheckTime = Meter_out[0].Mb_DatJdrq.ToString().Trim();
            for (int i = 0; i < Meter_out.Count; i++)
            {
                lis_meter_zcbh.Add(Meter_out[i].Mb_ChrJlbh.Trim());
                if (Meter_out[i].Mb_chrQianFeng1 !=null && Meter_out[i].Mb_chrQianFeng1.Trim() != "")
                    {
                        lis_seal_01.Add(Meter_out[i].Mb_chrQianFeng1.Trim());
                    }
                if (Meter_out[i].Mb_chrQianFeng2 != null && Meter_out[i].Mb_chrQianFeng2.Trim() != "")
                    {
                        lis_seal_02.Add(Meter_out[i].Mb_chrQianFeng2.Trim());
                    }
                if (Meter_out[i].Mb_chrQianFeng3 != null && Meter_out[i].Mb_chrQianFeng3.Trim() != "")
                    {
                        lis_seal_03.Add(Meter_out[i].Mb_chrQianFeng3.Trim());
                    }
                
            }
            if (lis_meter_zcbh.Count > 0)
            {
                clsExcelControl.OutputExcel(lis_meter_zcbh, lis_seal_01, lis_seal_02, lis_seal_03, CheckTime);
                MessageBox.Show("����Ǧ����Ϣ�ɹ���");
            }
            else
            {
                MessageBox.Show("����Ǧ����Ϣʧ�ܣ�");
            }
            
        }
        private void Accomplish()
        {
            CLDC_DataCore.Function.TopWaiting.HideWaiting();
            string SavePath = clsMain.getIniString("OtherInfo", "SavePath", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\ReportInfo.ini");

            MessageBox.Show("�춨���ݵ����ɹ���" + "�ļ������:" + CLDC_DataManager.Const.Gb_Attribute.str_ExcelSavePath);


            System.Diagnostics.Process.Start(CLDC_DataManager.Const.Gb_Attribute.str_ExcelSavePath);
        }
        private void �����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region SaveFileDiolog
            SaveFileDialog sfd = new SaveFileDialog();
            //sfd.InitialDirectory = "E:\\";
            sfd.Filter = "xls�ļ�(*.xls)|*.xls";
            sfd.Title = "ѡ��Excel�ļ�����λ��";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                CLDC_DataManager.Const.Gb_Attribute.str_ExcelSavePath = sfd.FileName;
            }

            #endregion
            CLDC_DataManager.Const.Gb_Attribute.chr_CheckYJ = Cmb_Jdyj.Text.ToString().Trim();
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("����׼��������������.....");
            List<string> MeterID = new List<string>();
            for (int i = 0; i < Meter_out.Count; i++)
            {
                MeterID.Add(Meter_out[i]._intMyId.ToString().Trim());
                Const.Gb_Attribute.CHR_CT_CONNECTION_FLAG = Meter_out[i].Mb_BlnHgq.ToString().Trim();

            }
            clsExcelControl excelOut = new clsExcelControl();
            UpdateEle MeterId_Ele = new UpdateEle();
            excelOut.TaskCallBack += Accomplish;
            MeterId_Ele.AddItemLsit = MeterID;
            Thread UpThread = new Thread(new ParameterizedThreadStart(excelOut.OutPutAllError));
            UpThread.Start(MeterId_Ele);
            //clsExcelControl.OutPutAllError(MeterID);
           // MessageBox.Show("������춨��Ϣ�ɹ���");
           // clsExcelControl.GetErrorInfo("1.0", "0", "1", "6400269609107406103");
        }

        private void Cmb_Value1_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsMain.WriteIni("Certi", "cmb_value", Cmb_Value1.Text.ToString().Trim(), System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
               
        }
    }
    public static class PrintLab
    {
        [DllImport("WINPSK.dll")]
        public static extern int OpenPort(string printname);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_SetPrintSpeed(uint px);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_SetDarkness(uint id);
        [DllImport("WINPSK.dll")]
        public static extern int ClosePort();
        [DllImport("WINPSK.dll")]
        public static extern int PTK_PrintLabel(uint number, uint cpnumber);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawTextTrueTypeW
                                            (int x, int y, int FHeight,
                                            int FWidth, string FType,
                                            int Fspin, int FWeight,
                                            bool FItalic, bool FUnline,
                                            bool FStrikeOut,
                                            string id_name,
                                            string data);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawBarcode(uint px,
                                        uint py,
                                        uint pdirec,
                                        string pCode,
                                        uint pHorizontal,
                                        uint pVertical,
                                        uint pbright,
                                        char ptext,
                                        string pstr);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_SetLabelHeight(uint lheight, uint gapH);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_SetLabelWidth(uint lwidth);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_ClearBuffer();
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawRectangle(uint px, uint py, uint thickness, uint pEx, uint pEy);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawLineOr(uint px, uint py, uint pLength, uint pH);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawBar2D_QR(uint x, uint y, uint w, uint v, uint o, uint r, uint m, uint g, uint s, string pstr);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawBar2D_Pdf417(uint x, uint y, uint w, uint v, uint s, uint c, uint px, uint py, uint r, uint l, uint t, uint o, string pstr);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_PcxGraphicsDel(string pid);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_PcxGraphicsDownload(string pcxname, string pcxpath);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawPcxGraphics(uint px, uint py, string gname);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawText(uint px, uint py, uint pdirec, uint pFont, uint pHorizontal, uint pVertical, char ptext, string pstr);


    }

}
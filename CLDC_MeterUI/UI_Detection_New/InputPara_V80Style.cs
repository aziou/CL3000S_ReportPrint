using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;
using CLDC_DataCore.DataBase;
using System.Threading;
using CLDC_Comm.Enum;
using CLDC_Comm;
using CLDC_VerifyAdapter.Helper;
using System.Xml;
using CLDC_DataCore.Const;
using CLDC_Dispatcher;

namespace CLDC_MeterUI.UI_Detection_New
{
    public partial class InputPara_V80Style : Base
    {

        private bool _BlnLianXuID = false;       //������ű��


        private string _strNowRun = "��ǰ״̬";
        public string strNowRun
        {
            set
            {
                _strNowRun = value;
                this.labelX3.Text = "��ǰ״̬��" + _strNowRun;
            }
        }

        public InputPara_V80Style()
        {
            InitializeComponent();
        }

        public InputPara_V80Style(
            Main parent
            , CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup
            , int taiType
            , int taiId)
            : base(parent, meterGroup, taiType, taiId)
        {
            CLDC_DataCore.Const.GlobalUnit.ReadingPara = false;
            InitializeComponent();

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            InitControls();
            this.Load += new EventHandler(InputPara_Load);  

            this.Grid_ShowMeter.CellMouseDown += new DataGridViewCellMouseEventHandler(Grid_ShowMeter_CellMouseDown);
            this.Grid_ShowMeter.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(Grid_ShowMeter_ColumnHeaderMouseClick);


        }
        /// <summary>
        ///����ˢ�±�λ��û�йҵ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResfreshMeterFlogTime_Tick(object sender, EventArgs e)
        {
            if (GlobalUnit.GetConfig(Variable.CTC_AUTO_ISHAVECHECKMETER, "��") == "��" || GlobalUnit.IsDemo)
            {
                ResfreshMeterFlogTime.Enabled = false;
                return;
            }
            if (CLDC_VerifyAdapter.Helper.EquipHelper.Instance.isConnected)
            {
                bool[] flog = CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.GetIsHaveMeters();
                for (int i = 0; i < flog.Length; i++)
                {
                    if (i < Grid_ShowMeter.RowCount)
                    {
                        if (flog[i])
                        {
                            Grid_ShowMeter.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(115, 181, 24);
                        }
                        else
                        {
                            if (i % 2 == 0)
                            {
                                Grid_ShowMeter.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(250, 250, 250); ;
                            }
                            else
                            {
                                Grid_ShowMeter.Rows[i].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                            }
                        }
                    }
                }
                CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.Control("��״̬", false);
            }
        }
        #region �Ҽ�
        void Grid_ShowMeter_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex < 0)
                {
                    Add_ctMS_Header_Data();
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                if (e.RowIndex < 0 && e.ColumnIndex == 1)
                {//��ӵ�������ʱ�Ĵ�����
                    ResfreshMeterFlogTime.Enabled = false;
                    CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.Control("��״̬", false);
                    ResfreshMeterFlogTime.Enabled = true;
                    ResfreshMeterFlogTime_Tick(null, null);
                    
                }
            }
        }

        void Add_ctMS_Header_Data()
        {
            string _ErrorString = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_COLSVISIABLE, out _ErrorString);
            ctMS_Header.Items.Clear();

            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                CLDC_DataCore.Struct.StColsVisiable _Col = new CLDC_DataCore.Struct.StColsVisiable();
                _Col.ColName = _XmlNode.ChildNodes[_i].Attributes[0].Value;
                _Col.ColShowName = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                _Col.ColShowType = int.Parse(_XmlNode.ChildNodes[_i].Attributes[2].Value);
                ctMS_Header.Items.Add(_Col.ColShowName, imglst_IsChecked.Images[_Col.ColShowType], tsbX_Click);
            }

            ctMS_Header.Show(MousePosition.X, MousePosition.Y);
        }

        void Grid_ShowMeter_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    Grid_ShowMeter.Rows[e.RowIndex].Selected = true;

                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                }
            }
        }

        private void InputPara_Load(object sender, EventArgs e)
        {
            ToolStripButton bi_Up = new ToolStripButton();//, 
            bi_Up.Name = "UpSelect";
            bi_Up.Text = "����ѡ��";
            bi_Up.Click -= new EventHandler(bi_Up_Click);
            bi_Up.Click += new EventHandler(bi_Up_Click);
            contextMenuStrip1.Items.Add(bi_Up);

            ToolStripButton bi_Down = new ToolStripButton();
            bi_Down.Name = "DownSelect";
            bi_Down.Text = "����ѡ��";
            bi_Down.Click -= new EventHandler(bi_Down_Click);
            bi_Down.Click += new EventHandler(bi_Down_Click);
            contextMenuStrip1.Items.Add(bi_Down);

            ToolStripButton bi_UpCancel = new ToolStripButton();//, 
            bi_UpCancel.Name = "UpCancelSelect";
            bi_UpCancel.Text = "����ȡ��ѡ��";
            bi_UpCancel.Click -= new EventHandler(bi_UpCancel_Click);
            bi_UpCancel.Click += new EventHandler(bi_UpCancel_Click);
            contextMenuStrip1.Items.Add(bi_UpCancel);

            ToolStripButton bi_DownCancel = new ToolStripButton();
            bi_DownCancel.Name = "DownCancelSelect";
            bi_DownCancel.Text = "����ȡ��ѡ��";
            bi_DownCancel.Click -= new EventHandler(bi_DownCancel_Click);
            bi_DownCancel.Click += new EventHandler(bi_DownCancel_Click);
            contextMenuStrip1.Items.Add(bi_DownCancel);

            ////�������Ҽ�
            //List<CLDC_DataCore.Struct.StColsVisiable> lst_CV = GlobalUnit.g_SystemConfig.ColsVisiable.getColPrj();
            //foreach (CLDC_DataCore.Struct.StColsVisiable item in lst_CV)
            //{
            //    ctMS_Header.Items.Add(item.ColShowName, imglst_IsChecked.Images[item.ColShowType], tsbX_Click);
            //}

            //ToolStripMenuItem tsb_C2 = new ToolStripMenuItem("Ǧ��1");//, 
            //tsb_C2.Name = "C2";
            ////tsb_C2.Text = "Ǧ��1";
            ////tsb_C2.Image = Image.FromFile("F:\\images\\photo_add_watermark_pop_03.gif");
            //tsb_C2.Click -= new EventHandler(tsbX_Click);
            //tsb_C2.Click += new EventHandler(tsbX_Click);
            //ctMS_Header.Items.Add(tsb_C2);
            ////ctMS_Header.Items.Add("Ǧ��1", Image.FromFile("F:\\images\\photo_add_watermark_pop_03.gif"));
            //ctMS_Header.Items[1].Image = imglst_IsChecked.Images[1];

        }

        void bi_Down_Click(object sender, EventArgs e)
        {
            for (int i = Grid_ShowMeter.SelectedRows[0].Index; i < MeterGroup.MeterGroup.Count; i++)
            {
                DataGridViewRow Row = Grid_ShowMeter.Rows[i];
                Row.Cells["Ҫ��"].Value = true;

            }

        }

        void bi_Up_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Grid_ShowMeter.SelectedRows[0].Index; i++)
            {
                DataGridViewRow Row = Grid_ShowMeter.Rows[i];
                Row.Cells["Ҫ��"].Value = true;

            }

        }

        void bi_DownCancel_Click(object sender, EventArgs e)
        {
            for (int i = Grid_ShowMeter.SelectedRows[0].Index; i < MeterGroup.MeterGroup.Count; i++)
            {
                DataGridViewRow Row = Grid_ShowMeter.Rows[i];
                Row.Cells["Ҫ��"].Value = false;

            }

        }

        void bi_UpCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Grid_ShowMeter.SelectedRows[0].Index; i++)
            {
                DataGridViewRow Row = Grid_ShowMeter.Rows[i];
                Row.Cells["Ҫ��"].Value = false;

            }

        }

        void tsbX_Click(object sender, EventArgs e)
        {
            //TODO:����ʾ���Ҽ����¼�
            List<CLDC_DataCore.Struct.StColsVisiable> lst_CV = GlobalUnit.g_SystemConfig.ColsVisiable.getColPrj();
            for (int i = 0; i < lst_CV.Count; i++)
            {
                if (lst_CV[i].ColShowName == sender.ToString())
                {
                    CLDC_DataCore.Struct.StColsVisiable Col = new CLDC_DataCore.Struct.StColsVisiable();
                    Col.ColName = sender.ToString();
                    Col.ColShowName = sender.ToString();

                    if (lst_CV[i].ColShowType == 1)
                    {
                        Col.ColShowType = 0;
                        Grid_ShowMeter.Columns[Col.ColShowName].Visible = false;
                    }
                    else if (lst_CV[i].ColShowType == 0)
                    {
                        Col.ColShowType = 1;
                        Grid_ShowMeter.Columns[Col.ColShowName].Visible = true;
                    }

                    GlobalUnit.g_SystemConfig.ColsVisiable.Add(Col);
                    GlobalUnit.g_SystemConfig.ColsVisiable.Save();

                }

            }
        }
        #endregion

        #region SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        /// <summary>
        /// ˢ�±������
        /// </summary>
        /// <param name="meterGroup"></param>
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {
            base.SetData(meterGroup, taiType, taiId);

            //����λ������
            for (int i = 1; i <= MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = MeterGroup.GetMeterBasicInfoByBwh(i);


                DataGridViewRow Row = Grid_ShowMeter.Rows[i - 1];

                if (MeterInfo != null)  //���û���ҵ�����˵��ǰ�����ô��ݵĲ����д��󣬾������Ϊ����λ��������������Ϊ��
                {
                    Row.Cells["Ҫ��"].Value = MeterInfo.YaoJianYn;
                    Row.Cells["��λ��"].Value = i.ToString().PadLeft(2, '0');//"��" + i.ToString().PadLeft(2, '0') + "��λ";
                    Row.Cells["������"].Value = MeterInfo.Mb_ChrTxm;
                    Row.Cells["����"].Value = MeterInfo.Mb_gygy.ToString();
                    Row.Cells["�ʲ����"].Value = MeterInfo.Mb_ChrJlbh;
                    Row.Cells["ͨѶ��ַ"].Value = MeterInfo.Mb_chrAddr;
                    SetGridViewComboCellValue("ͨѶЭ��", Row.Cells["ͨѶЭ��"], MeterInfo.AVR_PROTOCOL_NAME);
                    SetGridViewComboCellValue("�ز�Э��", Row.Cells["�ز�Э��"], MeterInfo.AVR_CARR_PROTC_NAME);
                    SetGridViewComboCellValue("����", Row.Cells["����"], MeterInfo.Mb_chrBcs);
                    SetGridViewComboCellValue("�ȼ�", Row.Cells["�ȼ�"], MeterInfo.Mb_chrBdj);
                    SetGridViewComboCellValue("���쳧��", Row.Cells["���쳧��"], MeterInfo.Mb_chrzzcj);
                    SetGridViewComboCellValue("������", Row.Cells["������"], MeterInfo.Mb_chrBlx);
                    SetGridViewComboCellValue("���ͺ�", Row.Cells["���ͺ�"], MeterInfo.Mb_Bxh);
                    SetGridViewComboCellValue("�ͼ쵥λ", Row.Cells["�ͼ쵥λ"], MeterInfo.Mb_chrSjdw);
                    Row.Cells["������"].Value = MeterInfo.AVR_TASK_NO;
                    Row.Cells["������"].Value = MeterInfo.AVR_WORK_NO;
                    Row.Cells["�������"].Value = MeterInfo.Mb_ChrJlbh;
                    Row.Cells["�������"].Value = MeterInfo.Mb_ChrCcbh;
                    Row.Cells["��������"].Value = MeterInfo.Mb_chrCcrq;
                    Row.Cells["֤����"].Value = MeterInfo.Mb_chrZsbh;
                    Row.Cells["������"].Value = MeterInfo.Mb_ChrBmc;
                    Row.Cells[getColName("Ǧ��1").ColShowName].Value = MeterInfo.Mb_chrQianFeng1;
                    Row.Cells[getColName("Ǧ��2").ColShowName].Value = MeterInfo.Mb_chrQianFeng2;
                    Row.Cells[getColName("Ǧ��3").ColShowName].Value = MeterInfo.Mb_chrQianFeng3;
                    Row.Cells[getColName("Ǧ��4").ColShowName].Value = MeterInfo.AVR_SEAL_4;
                    Row.Cells[getColName("Ǧ��5").ColShowName].Value = MeterInfo.AVR_SEAL_5;
                    Row.Cells["����汾��"].Value = MeterInfo.Mb_chrSoftVer;
                    Row.Cells["Ӳ���汾��"].Value = MeterInfo.Mb_chrHardVer;
                    Row.Cells["�������κ�"].Value = MeterInfo.Mb_chrArriveBatchNo;
                    Row.Cells["����1"].Value = MeterInfo.Mb_chrOther1;
                    Row.Cells["����2"].Value = MeterInfo.Mb_chrOther2;
                    Row.Cells["����3"].Value = MeterInfo.Mb_chrOther3;
                    Row.Cells["����4"].Value = MeterInfo.Mb_chrOther4;
                    Row.Cells["����5"].Value = MeterInfo.Mb_chrOther5;
                }
            }

            //���� ��ѹ��������Ϣ
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = null;

                //��ȡ��һֻҪ��ĵ�һֻ��
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
                {
                    if (MeterGroup.MeterGroup[i].YaoJianYn == true)
                    {
                        MeterInfo = MeterGroup.MeterGroup[i];
                        break;
                    }
                }
                if (MeterInfo == null) return;

                //��ѹ
                if (MeterInfo.Mb_chrUb.Length > 0)
                {
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_DianYa, MeterInfo.Mb_chrUb);
                }

                //����
                if (MeterInfo.Mb_chrIb.Length > 0)
                {
                    string[] tmp = MeterInfo.Mb_chrIb.Split('(');
                    if (tmp.Length > 0)
                        CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_DianLiu_Ib, tmp[0]);
                    if (tmp.Length > 1)
                        CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_DianLiu_Imax, tmp[1].Replace(")", ""));
                }

                //Ƶ��
                if (MeterInfo.Mb_chrHz.Length > 0)
                    Cmb_PinLv.Value = decimal.Parse(MeterInfo.Mb_chrHz);

                //�׼��ܼ� Other1
                if (MeterInfo.Mb_chrOther3 != "")
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_ShouJianZhouJian, MeterInfo.Mb_chrOther3);

                //������� Cmb_JianCeLeiXing Other2
                if (MeterInfo.Mb_chrTestType != "")
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_JianCeLeiXing, MeterInfo.Mb_chrTestType);

                //���ò�����ʽ
                if (GlobalUnit.IsDan == true)
                { 
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_CeLiangFangShi, ((int)Cus_Clfs.����).ToString()); 
                }
                else
                {
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_CeLiangFangShi, MeterInfo.Mb_intClfs.ToString());
                }
                //������
                if (MeterInfo.Mb_BlnHgq == true)
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_HuGanQi, "��������");
                else
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_HuGanQi, "ֱ�ӽ���");

                //ֹ����
                if (MeterInfo.Mb_BlnZnq == true)
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_ZhiNiQi, "��ֹ��");
            }
        }

        #endregion

        /// <summary>
        /// ����COMBO������ʾ��������ֵ����Ҳ�����Ӧ���ݣ�����Ҫ���°�
        /// </summary>
        /// <param name="ColName"></param>
        /// <param name="Cell"></param>
        /// <param name="StrValue"></param>
        private void SetGridViewComboCellValue(string ColName, DataGridViewCell Cell, string StrValue)
        {
            if (!(Cell is DataGridViewComboBoxCell)) return;
            DataGridViewComboBoxCell CmbCell = (DataGridViewComboBoxCell)Cell;
            for (int i = 0; i < CmbCell.Items.Count; i++)
            {
                if (((DataRowView)CmbCell.Items[i]).Row["ֵ"].ToString() == StrValue.Trim())
                {
                    CmbCell.Value = StrValue;
                    CmbCell.Tag = StrValue;
                    return;
                }
            }
            //��������ڣ������°�

            CmbCell.DataSource = this.BindComboBoxDataSource(ColName, StrValue);
            CmbCell.Value = StrValue;
            CmbCell.Tag = StrValue;
        }



        #region RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)

        public override void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {
            base.RefreshData(meterGroup, taiType, taiId);
            SetData(meterGroup, taiType, taiId);
        }
        #endregion

        #region ��ʼ���ؼ��Լ������������ void InitControls()
        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControls()
        {
            // ===================== Grid_ShowMeter =====================

            string _ErrorString = "";
            string _ColName = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_COLSVISIABLE, out _ErrorString);

            DataTable dtTmp = new DataTable();
            dtTmp.Columns.Add("Ҫ��", typeof(bool)); //�Ƿ�Ҫ��
            dtTmp.Columns.Add("��λ��", typeof(string));
            dtTmp.Columns.Add("������", typeof(string));
            dtTmp.Columns.Add("�ʲ����", typeof(string));
            dtTmp.Columns.Add("������", typeof(string));
            dtTmp.Columns.Add("����", typeof(string));
            dtTmp.Columns.Add("�ȼ�", typeof(string));
            dtTmp.Columns.Add("����", typeof(string));
            dtTmp.Columns.Add("���ͺ�", typeof(string));
            dtTmp.Columns.Add("�춨���", typeof(string));
            dtTmp.Columns.Add("ͨѶЭ��", typeof(string));
            dtTmp.Columns.Add("�ز�Э��", typeof(string));
            dtTmp.Columns.Add("ͨѶ��ַ", typeof(string));
            dtTmp.Columns.Add("���쳧��", typeof(string));
            dtTmp.Columns.Add("�ͼ쵥λ", typeof(string));
            if (_XmlNode == null) return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                bool add = true;
                _ColName = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                for (int i = 0; i < dtTmp.Columns.Count; i++)
                {
                    if (dtTmp.Columns[i].ColumnName == _ColName)
                    {
                        add = false;
                        break;
                    }
                }
                if (add)
                {
                    dtTmp.Columns.Add(_ColName, typeof(string));
                }
            }

            /*
             Grid_ShowMeter ���е���������˵����
             * 1��ÿһ�е������ؼ�ѡ�������  [��ʾ�ı�]��[��Ӧ��ֵ]��һ������� [��ʾ�ı�] == [��Ӧ��ֵ]
             * 2��ÿһ�������ؼ��󶨵����ݱ����һ�����ݸ�ʽ�������£�
             *    [ֵ] = [����]|[��������]
             * 
             * 3������Ҫ���������һ�е�
             *    [��ʾ�ı�] = "===����==="
             *    [ֵ]       = [����]|[�������ݵ�һ����������ƥ��]
             */
            Grid_ShowMeter.Columns.Clear();
            foreach (DataColumn dtCol in dtTmp.Columns)
            {
                DataGridViewColumn GridCol = null;
                if (dtCol.ColumnName == "Ҫ��")
                {
                    GridCol = new DataGridViewCheckBoxColumn();
                    GridCol.Width = Chk_CheckAll.Width;
                }
                else if (dtCol.ColumnName == "������" || dtCol.ColumnName == "����" || dtCol.ColumnName == "�ȼ�"
                    || dtCol.ColumnName == "���ͺ�" || dtCol.ColumnName == "���쳧��" || dtCol.ColumnName == "ͨѶЭ��" || dtCol.ColumnName == "�ز�Э��" || dtCol.ColumnName == "�ͼ쵥λ" ||
                    dtCol.ColumnName == "����" || dtCol.ColumnName == "�춨���"
                    )
                {
                    GridCol = new DataGridViewComboBoxColumn();

                    DataTable dtDpl = this.BindComboBoxDataSource(dtCol.ColumnName, "");

                    // ���������� dtDpl���ݱ�����ݰ󶨵����е������С� �����ơ���ʾ����ֵ����Ϊֵ
                    ((DataGridViewComboBoxColumn)GridCol).DisplayMember = "����";
                    ((DataGridViewComboBoxColumn)GridCol).ValueMember = "ֵ";
                    ((DataGridViewComboBoxColumn)GridCol).DataSource = dtDpl;

                    //((DataGridViewComboBoxColumn)GridCol).DropDownWidth = 200;
                    ((DataGridViewComboBoxColumn)GridCol).DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    ((DataGridViewComboBoxColumn)GridCol).FlatStyle = FlatStyle.Standard;
                    ((DataGridViewComboBoxColumn)GridCol).AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
                else
                {
                    GridCol = new DataGridViewTextBoxColumn();
                }
                GridCol.Name = dtCol.ColumnName;
                GridCol.HeaderText = dtCol.ColumnName;
                Grid_ShowMeter.Columns.Add(GridCol);
            }

            Grid_ShowMeter.Columns[0].Frozen = true;
            Grid_ShowMeter.Columns[1].Frozen = true;
            Grid_ShowMeter.Columns[2].Frozen = true;
            Grid_ShowMeter.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            Grid_ShowMeter.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            Grid_ShowMeter.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            Grid_ShowMeter.GridColor = SystemColors.ActiveBorder;
            Grid_ShowMeter.RowHeadersVisible = false;
            Grid_ShowMeter.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(Grid_ShowMeter_EditingControlShowing);
            Grid_ShowMeter.Columns["��λ��"].ReadOnly = true;

            #region //���ؼ춨��������
            {
                CLDC_DataCore.SystemModel.Item.csDictionary Dic = new CLDC_DataCore.SystemModel.Item.csDictionary();
                Dic.Load();
                List<string> LstData = Dic.getValues("��ѹ");
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_DianYa, LstData, true, "��ѹ", @"\d+", true, null);
                LstData = Dic.getValues("����");
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_DianLiu_Ib, LstData, true, "����", @"((\d+\.)?\d)", true, null);//����1.5(6)��@"((\d+\.)?\d+\((\d+\.)?\d+\))"
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_DianLiu_Imax, LstData, true, "����", @"((\d+\.)?\d)", true, null);
                //LstData.Clear();
                //LstData.Add("����");
                //LstData.Add("��������");
                //LstData.Add("��������");
                //LstData.Add("��Ԫ������90��");
                //LstData.Add("��Ԫ������60��");
                //LstData.Add("��Ԫ������90��");
                //Comm.Function.BindCombox.BindComboxItem(Cmb_CeLiangFangShi, LstData);
                {
                    DataTable dtTmp2 = new DataTable();
                    dtTmp2.Columns.Add("����", typeof(string));
                    dtTmp2.Columns.Add("ֵ", typeof(string));
                    dtTmp2.Rows.Add(new object[] { "��������", "0" });
                    dtTmp2.Rows.Add(new object[] { "��������", "1" });
                    dtTmp2.Rows.Add(new object[] { "��Ԫ������90��", "2" });
                    dtTmp2.Rows.Add(new object[] { "��Ԫ������60��", "3" });
                    dtTmp2.Rows.Add(new object[] { "��Ԫ������90��", "4" });
                    dtTmp2.Rows.Add(new object[] { "����", "5" });
                    Cmb_CeLiangFangShi.ValueMember = "ֵ";
                    Cmb_CeLiangFangShi.DisplayMember = "����";
                    Cmb_CeLiangFangShi.DataSource = dtTmp2;
                }
                if (CLDC_DataCore.Const.GlobalUnit.IsDan)
                {
                    Cmb_CeLiangFangShi.Text = "����";
                    Cmb_CeLiangFangShi.Enabled = false;
                }
                //������
                LstData.Clear();
                LstData.Add("��������");
                LstData.Add("ֱ�ӽ���");
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_HuGanQi, LstData);
                Cmb_HuGanQi.SelectedIndex = 0;

                //ֹ����
                LstData.Clear();
                LstData.Add("��ֹ��");
                LstData.Add("��ֹ��");
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_ZhiNiQi, LstData);
                Cmb_ZhiNiQi.SelectedIndex = 1;

                //�������
                LstData.Clear();
                LstData.Add("�ͻ�����");
                LstData.Add("�������");
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_JianCeLeiXing, LstData);
                Cmb_JianCeLeiXing.SelectedIndex = 0;

                //�׼��ܼ�
                LstData = Dic.getValues("�춨����");
                if (LstData.Count == 0)
                {
                    LstData.Add("�׼�");
                    LstData.Add("�ܼ�");
                    LstData.Add("���");
                    LstData.Add("���׼�");
                }
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_ShouJianZhouJian, LstData);
                Cmb_ShouJianZhouJian.SelectedIndex = 0;


                //��Ӧʽ���
                //LstData.Clear();
                //LstData.Add("JJG307-2006");
                //LstData.Add("JJG307-1988");
                //CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_HGQ_In, LstData);
                //Cmb_HGQ_In.SelectedIndex = 0;

                //���ӹ��
                LstData.Clear();
                LstData.Add("Զ�̷ѿ�");//JJG596-1999
                LstData.Add("���طѿ�");
                LstData.Add("�����ѿ�");
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_FKType, LstData);
                Cmb_FKType.SelectedIndex = 0;

                //��ȡ��һ֧Ҫ���
                int FirstIndex = Main.GetFirstYaoJianMeterIndex(MeterGroup);

                if (FirstIndex > -1)
                {

                    //if (MeterGroup.MeterGroup[FirstIndex].GuiChengName_GanYing.Length > 0)
                    //    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_HGQ_In, MeterGroup.MeterGroup[FirstIndex].GuiChengName_GanYing);
                    if (MeterGroup.MeterGroup[FirstIndex].FKType.Length > 0)
                        CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_FKType, MeterGroup.MeterGroup[FirstIndex].FKType);
                }

                //���÷��������б�
                this.SetFaList();

            }
            #endregion
            if (Grid_ShowMeter.Rows.Count != MeterGroup._Bws)
            {
                Grid_ShowMeter.Rows.Clear();
                for (int i = 0; i < MeterGroup._Bws; i++)
                {
                    Grid_ShowMeter.Rows.Add();
                }
            }
            Grid_ShowMeter.CurrentCell = Grid_ShowMeter.Rows[0].Cells[1];

            //��������
            SetData(MeterGroup, TaiType, TaiId);

            //������ɫ
            Grid_ShowMeter.BackgroundColor = Color.FromArgb(250, 250, 250);

            //��Ԫ���ӱ�����ɫ
            Grid_ShowMeter.DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;//Color.FromArgb(250, 250, 250);

            //ǰ���е�Ԫ�񱳾���ɫ
            //Grid_ShowMeter.Columns["Ҫ��"].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            //Grid_ShowMeter.Columns["��λ��"].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            //Grid_ShowMeter.Columns["������"].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            for (int i = 1; i < Grid_ShowMeter.Rows.Count; i += 2)
            {
                Grid_ShowMeter.Rows[i].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;//Color.FromArgb(235, 250, 235); ;//Color.FromArgb(240, 250, 240);
            }
            Grid_ShowMeter.Columns["��λ��"].DefaultCellStyle.Font = new System.Drawing.Font("System", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Grid_ShowMeter.Columns["��λ��"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid_ShowMeter.Columns["��λ��"].DefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 80);

            //�߿�
            Grid_ShowMeter.BorderStyle = BorderStyle.None;

            //���ܶ���ѡ��
            Grid_ShowMeter.MultiSelect = false;
            //�¼�����
            Grid_ShowMeter.Resize += new EventHandler(Grid_ShowMeter_Resize);
            Grid_ShowMeter_Resize(Grid_ShowMeter, new EventArgs());
            //Grid_ShowMeter.SelectionChanged += new EventHandler(Grid_ShowMeter_SelectionChanged);
            Grid_ShowMeter.CellValueChanged += new DataGridViewCellEventHandler(Grid_ShowMeter_CellValueChanged);

            //�ر�����
            for (int i = 0; i < Grid_ShowMeter.Columns.Count; i++)
            {
                Grid_ShowMeter.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            Grid_ShowMeter.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            string ColName = "";
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                if (_XmlNode.ChildNodes[_i].Attributes[2].Value != "1")
                {
                    ColName = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                    Grid_ShowMeter.Columns[ColName].Visible = false;
                }
            }

        }

        /// <summary>
        /// ������Դ
        /// </summary>
        /// <param name="KeyName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        private DataTable BindComboBoxDataSource(string KeyName, string Value)
        {
            DataTable dtDpl = new DataTable();
            dtDpl.Columns.Add("ֵ", typeof(string));
            dtDpl.Columns.Add("����", typeof(string));

            if (KeyName != "ͨѶЭ��" && KeyName != "�ز�Э��")
            {
                CLDC_DataCore.SystemModel.Item.csDictionary Dict = new CLDC_DataCore.SystemModel.Item.csDictionary();
                Dict.Load();
                List<string> LstData = Dict.getValues(KeyName);
                if (null != Value && Value.Trim() != string.Empty && !LstData.Contains(Value))
                {
                    Dict.Add(KeyName, Value);
                    Dict.Save();
                    LstData.Add(Value);
                }

                for (int i = 0; i < LstData.Count; i++)
                {
                    dtDpl.Rows.Add(LstData[i], LstData[i]);
                }
                CLDC_DataCore.Function.DoDataTable.Sort(ref dtDpl, "����", true);
            }

            //�����������������
            switch (KeyName)
            {
                case "������":
                    dtDpl.Rows.Add(string.Format(@"{0}|.+", KeyName), "===����===");
                    break;
                case "����":
                    dtDpl.Rows.Add(string.Format(@"{0}|(\d+\(\d+\))|(\d+)", KeyName), "===����===");
                    break;
                case "�ȼ�":
                    dtDpl.Rows.Add(string.Format(@"{0}|(\d+(\.\d+)?s?\(\d+(\.\d+)?s?\))|(\d+(\.\d+)?s?)", KeyName), "===����===");
                    break;
                case "���ͺ�":
                    dtDpl.Rows.Add(string.Format(@"{0}|[\d\w\-_]+", KeyName), "===����===");
                    break;
                case "���쳧��":
                    dtDpl.Rows.Add(string.Format(@"{0}|.+", KeyName), "===����===");
                    break;
                case "ͨѶЭ��":
                    {
                        Dictionary<string, string> DicProtocol = CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo.getProtocolString();
                        foreach (string Key in DicProtocol.Keys)
                        {
                            dtDpl.Rows.Add(Key, Key);
                        }
                        if (Value.Trim() != string.Empty)
                        {
                            dtDpl.Rows.Add(Value.Trim(), Value.Trim());
                        }
                        dtDpl.Rows.Add("", "");
                    }
                    break;
                case "�ز�Э��":
                    //-----------TODO------------
                    List<string> lst_CarrierNames = CLDC_DataCore.Model.CarrierProtocol.CarrierProtocolInfo.GetProtocolNameList();

                    foreach (string name in lst_CarrierNames)
                    {
                        dtDpl.Rows.Add(name, name);
                    }

                    dtDpl.Rows.Add(string.Format(@"{0}|.+", KeyName));

                    break;
                case "�ͼ쵥λ":
                    dtDpl.Rows.Add(string.Format(@"{0}|.+", KeyName), "===����===");
                    break;
                case "�춨���":                    
                    dtDpl.Rows.Add("JJG307-2003", "JJG307-2003");
                    dtDpl.Rows.Add("JJG596-1999", "JJG596-1999");
                    dtDpl.Rows.Add("JJG596-2012", "JJG596-2012");
                    dtDpl.Rows.Add(string.Format(@"{0}|.+", KeyName), " ");
                    break;
                case "����":
                    dtDpl.Rows.Add("����", "����");
                    dtDpl.Rows.Add("����", "����");
                    dtDpl.Rows.Add(string.Format(@"{0}|.+", KeyName), " ");
                    break;
                default:
                    dtDpl.Rows.Add(string.Format("{0}|.+", KeyName), "===����===");
                    break;
            }
            return dtDpl;

        }


        /// <summary>
        /// �����ܷ��������б�
        /// </summary>
        private void SetFaList()
        {
            List<string> TmpList = new List<string>();

            TmpList = CLDC_DataCore.Model.Plan.Model_Plan.getFileNames(CLDC_DataCore.Const.Variable.CONST_FA_GROUP_FOLDERNAME, base.TaiType);

            TmpList.Insert(0, "");          //����һ������

            CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_FA, TmpList);

            if (MeterGroup.FaName != "")
                CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_FA, MeterGroup.FaName);

        }

        private void Grid_ShowMeter_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (!(e.Control is DataGridViewComboBoxEditingControl)) return;
            ((DataGridViewComboBoxEditingControl)e.Control).SelectedIndexChanged -= new EventHandler(InputPara_V80Style_SelectedIndexChanged);
            ((DataGridViewComboBoxEditingControl)e.Control).SelectedIndexChanged += new EventHandler(InputPara_V80Style_SelectedIndexChanged);
        }

        private bool bDoSelectIndexChanged = false; //��ǰ�Ƿ����ڴ��� SelectIndexChanged�¼�

        private void InputPara_V80Style_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bDoSelectIndexChanged == true) return;

            bDoSelectIndexChanged = true;

            ComboBox CmbBox = (ComboBox)sender;
            if (CmbBox.SelectedIndex < 0) goto Exit;
            string StrValue = ((DataRowView)CmbBox.SelectedItem).Row["ֵ"].ToString();
            string StrColName;
            string StrRegFilter;
            string StrText;

            //ѡ��������һ����Чֵ ���� [����],������ֵ
            if (StrValue.IndexOf("|") == -1 /*|| StrValue.Substring(StrValue.IndexOf("|")) == "|.+"*/)
            {
                //ѡ�����±�
                int RowIndex = Grid_ShowMeter.SelectedCells[0].RowIndex;

                //Grid���� �������������һ��Ŀ��ѡ��������
                StrColName = ((DataRowView)CmbBox.Items[CmbBox.Items.Count - 1]).Row["ֵ"].ToString();

                if (StrColName == string.Empty)     //���Ϊ�գ�����ͨѶЭ��
                {
                    StrColName = "ͨѶЭ��";
                }
                else if (StrColName.IndexOf("|") < 0)
                {
                    StrColName = "�ز�Э��";
                }
                else
                {
                    StrColName = StrColName.Substring(0, StrColName.IndexOf("|"));
                }

                for (int i = RowIndex; i < Grid_ShowMeter.Rows.Count; i++)
                {
                    SetGridViewComboCellValue(StrColName, Grid_ShowMeter.Rows[i].Cells[StrColName], StrValue);
                }

                ////�Զ�ѡ��Э�� [�����ж�����]
                //if (StrColName == "���쳧��" || StrColName == "���ͺ�")
                //{
                //    if (MeterGroup.AutoProtocol)         //������Զ�ʶ��
                //    {
                //        object FactoryName = null; //����
                //        object MeterMode = null;//���ͺ�
                //        string ProtocolName = string.Empty;//Э������

                //        for (int i = 0; i < Grid_ShowMeter.Rows.Count; i++)
                //        {
                //            FactoryName = Grid_ShowMeter.Rows[i].Cells["���쳧��"].Value;
                //            MeterMode = Grid_ShowMeter.Rows[i].Cells["���ͺ�"].Value;
                //            if (FactoryName == null || FactoryName.ToString() == "" || MeterMode == null || MeterMode.ToString() == "")
                //            {
                //                ProtocolName = string.Empty;
                //            }
                //            else
                //            {
                //                ProtocolName = CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo.getProtocolName(FactoryName.ToString(), MeterMode.ToString());
                //            }

                //            SetGridViewComboCellValue("ͨ��Э��", Grid_ShowMeter.Rows[i].Cells["ͨѶЭ��"], ProtocolName.ToString());
                //        }
                //    }
                //}
                goto Exit;
            }
            StrColName = StrValue.Substring(0, StrValue.IndexOf("|"));
            StrRegFilter = StrValue.Substring(StrColName.Length);
            StrText = ((DataRowView)CmbBox.SelectedItem).Row["����"].ToString();

            //ѡ�� [����] ��
            if (StrText == "===����===")
            {
                //����Ϊ"����"ʱ������������
                if (StrColName == "����") return;
                Form FrmBindCmbBox = new CLDC_DataCore.Function.BindCombox_NewValue((ComboBox)CmbBox, StrColName, ((ComboBox)CmbBox).SelectedValue.ToString());
                if (FrmBindCmbBox.ShowDialog() != DialogResult.OK)
                {
                    if (((ComboBox)CmbBox).SelectedIndex > 1)
                    {
                        ((ComboBox)CmbBox).SelectedIndex = 0;
                    }
                    else
                    {
                        ((ComboBox)CmbBox).SelectedIndex = -1;
                    }
                    bDoSelectIndexChanged = false;
                    this.InputPara_V80Style_SelectedIndexChanged(sender, e);

                }
                else    //�����ɹ���ˢ��  
                {
                    bDoSelectIndexChanged = false;
                    Grid_ShowMeter.CurrentCell.Value = FrmBindCmbBox.Tag.ToString();
                    Grid_ShowMeter.CurrentCell.Tag = FrmBindCmbBox.Tag.ToString();
                    this.InputPara_V80Style_SelectedIndexChanged(sender, e);
                }
                FrmBindCmbBox.Dispose();
                goto Exit;
            }
            else if (StrText == "")
            {
                //ѡ�����±�
                int RowIndex = Grid_ShowMeter.SelectedCells[0].RowIndex;

                for (int i = RowIndex; i < Grid_ShowMeter.Rows.Count; i++)
                {
                    SetGridViewComboCellValue(StrColName, Grid_ShowMeter.Rows[i].Cells[StrColName], "");
                }
            }
        Exit:
            ThreadPool.QueueUserWorkItem(new WaitCallback(thSetbDoSelectIndexChangedFalse));
        }

        private void thSetbDoSelectIndexChangedFalse(object obj)
        {
            Thread.Sleep(50);
            bDoSelectIndexChanged = false;
        }
        #endregion

        #region UI -> Model

        private void SetSaveToModel(DataGridViewRow Row)
        {
            int Bwh = int.Parse(Row.Cells["��λ��"].Value.ToString().Replace("��", "").Replace("��λ", "").Trim());
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = ((CLDC_DataCore.Model.DnbModel.DnbGroupInfo)MeterGroup).GetMeterBasicInfoByBwh(Bwh);
            MeterInfo.YaoJianYn = (bool)Row.Cells["Ҫ��"].Value;
            MeterInfo.YaoJianYnSave = (bool)Row.Cells["Ҫ��"].Value;
            //if (MeterInfo.YaoJianYn)            //���Ҫ��ż�������
            //{
            //=Row["��λ��"] = "��" + i.ToString().PadLeft(2, '0') + "��λ";
            MeterInfo.Mb_ChrTxm = Row.Cells["������"].Value == null ? "" : Row.Cells["������"].Value.ToString();
            MeterInfo.Mb_ChrJlbh = Row.Cells["�ʲ����"].Value == null ? "" : Row.Cells["�ʲ����"].Value.ToString();

            MeterInfo.GuiChengName = Row.Cells["�춨���"].Value == null ? "" : Row.Cells["�춨���"].Value.ToString();
            MeterInfo.AVR_PROTOCOL_NAME = Row.Cells["ͨѶЭ��"].Value == null ? "" : Row.Cells["ͨѶЭ��"].Value.ToString();
            MeterInfo.AVR_CARR_PROTC_NAME = Row.Cells["�ز�Э��"].Value == null ? "" : Row.Cells["�ز�Э��"].Value.ToString();
            MeterInfo.Mb_chrAddr = Row.Cells["ͨѶ��ַ"].Value == null ? "" : Row.Cells["ͨѶ��ַ"].Value.ToString();
            MeterInfo.Mb_chrBlx = Row.Cells["������"].Value == null ? "" : Row.Cells["������"].Value.ToString();
            MeterInfo.Mb_chrBcs = Row.Cells["����"].Value == null ? "" : Row.Cells["����"].Value.ToString();
            MeterInfo.Mb_chrBdj = Row.Cells["�ȼ�"].Value == null ? "" : Row.Cells["�ȼ�"].Value.ToString();
            MeterInfo.Mb_chrzzcj = Row.Cells["���쳧��"].Value == null ? "" : Row.Cells["���쳧��"].Value.ToString();
            MeterInfo.Mb_Bxh = Row.Cells["���ͺ�"].Value == null ? "" : Row.Cells["���ͺ�"].Value.ToString();
            MeterInfo.Mb_chrSjdw = Row.Cells["�ͼ쵥λ"].Value == null ? "" : Row.Cells["�ͼ쵥λ"].Value.ToString();

            MeterInfo.AVR_TASK_NO = Row.Cells["������"].Value == null ? "" : Row.Cells["������"].Value.ToString();
            MeterInfo.AVR_WORK_NO = Row.Cells["������"].Value == null ? "" : Row.Cells["������"].Value.ToString();
            //MeterInfo.Mb_ChrJlbh = Row.Cells["�������"].Value == null ? "" : Row.Cells["�������"].Value.ToString();
            MeterInfo.Mb_ChrCcbh = Row.Cells["�������"].Value == null ? "" : Row.Cells["�������"].Value.ToString();
            MeterInfo.Mb_chrCcrq = Row.Cells["��������"].Value == null ? "" : Row.Cells["��������"].Value.ToString();
            MeterInfo.Mb_chrZsbh = Row.Cells["֤����"].Value == null ? "" : Row.Cells["֤����"].Value.ToString();
            MeterInfo.Mb_ChrBmc = Row.Cells["������"].Value == null ? "" : Row.Cells["������"].Value.ToString();
            MeterInfo.Mb_chrQianFeng1 = Row.Cells[getColName("Ǧ��1").ColShowName].Value == null ? "" : Row.Cells[getColName("Ǧ��1").ColShowName].Value.ToString();
            MeterInfo.Mb_chrQianFeng2 = Row.Cells[getColName("Ǧ��2").ColShowName].Value == null ? "" : Row.Cells[getColName("Ǧ��2").ColShowName].Value.ToString();
            MeterInfo.Mb_chrQianFeng3 = Row.Cells[getColName("Ǧ��3").ColShowName].Value == null ? "" : Row.Cells[getColName("Ǧ��3").ColShowName].Value.ToString();
            MeterInfo.AVR_SEAL_4 = Row.Cells[getColName("Ǧ��4").ColShowName].Value == null ? "" : Row.Cells[getColName("Ǧ��4").ColShowName].Value.ToString();
            MeterInfo.AVR_SEAL_5 = Row.Cells[getColName("Ǧ��5").ColShowName].Value == null ? "" : Row.Cells[getColName("Ǧ��5").ColShowName].Value.ToString();
            MeterInfo.Mb_chrSoftVer = Row.Cells["����汾��"].Value == null ? "" : Row.Cells["����汾��"].Value.ToString();
            MeterInfo.Mb_chrHardVer = Row.Cells["Ӳ���汾��"].Value == null ? "" : Row.Cells["Ӳ���汾��"].Value.ToString();
            MeterInfo.Mb_chrArriveBatchNo = Row.Cells["�������κ�"].Value == null ? "" : Row.Cells["�������κ�"].Value.ToString();
            MeterInfo.Mb_chrOther1 = Row.Cells["����1"].Value == null ? "" : Row.Cells["����1"].Value.ToString();
            MeterInfo.Mb_chrOther2 = Row.Cells["����2"].Value == null ? "" : Row.Cells["����2"].Value.ToString();
            MeterInfo.Mb_chrOther3 = Row.Cells["����3"].Value == null ? "" : Row.Cells["����3"].Value.ToString();
            MeterInfo.Mb_chrOther4 = Row.Cells["����4"].Value == null ? "" : Row.Cells["����4"].Value.ToString();
            MeterInfo.Mb_chrOther5 = Row.Cells["����5"].Value == null ? "" : Row.Cells["����5"].Value.ToString();

            MeterInfo._intTaiNo = this.TaiId.ToString();
            try
            {
                MeterInfo.Mb_gygy = (CLDC_Comm.Enum.Cus_GyGyType)(Enum.Parse(typeof(CLDC_Comm.Enum.Cus_GyGyType), Row.Cells["����"].Value.ToString()));
            }
            catch
            {
                MeterInfo.Mb_gygy = Cus_GyGyType.����;
            }
            int _Const = CLDC_DataCore.Function.Number.GetBcs(MeterInfo.Mb_chrBcs, true);      //�й�

            if (_Const != 1 && (MeterGroup.MinConst[0] == 0 || MeterGroup.MinConst[0] > _Const)) MeterGroup.MinConst[0] = _Const;

            _Const = CLDC_DataCore.Function.Number.GetBcs(MeterInfo.Mb_chrBcs, false);     //�޹�

            if (_Const != 1 && (MeterGroup.MinConst[1] == 0 || MeterGroup.MinConst[1] > _Const)) MeterGroup.MinConst[1] = _Const;

            ////���ò�����ʽ
            //if (Cmb_CeLiangFangShi.SelectedIndex != -1)
            //{
            //    MeterInfo.Mb_intClfs = int.Parse(CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_CeLiangFangShi));
            //}

            ////������
            //MeterInfo.Mb_BlnHgq = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_HuGanQi) == "��������";

            ////ֹ����
            //MeterInfo.Mb_BlnZnq = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_ZhiNiQi) == "��ֹ��";

            //}
        }
        /// <summary>
        /// UI -> Model
        /// </summary>
        private void SetSaveToModel()
        {
            foreach (DataGridViewRow Row in Grid_ShowMeter.Rows)
            {
                SetSaveToModel(Row);
            }
        }
        #endregion

        #region Grid_ShowMeter ���¼�

        #region Resize�¼�
        /// <summary>
        /// ���� Grid_ShowMeter ���п��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Grid_ShowMeter_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < Grid_ShowMeter.Columns.Count; i++)
            {
                switch (Grid_ShowMeter.Columns[i].HeaderText)
                {
                    case "Ҫ��":
                        Grid_ShowMeter.Columns[i].Width = 40;
                        break;
                    case "��λ��":
                        Grid_ShowMeter.Columns["��λ��"].Width = 47;
                        break;
                    case "������":
                        Grid_ShowMeter.Columns["������"].Width = 120;
                        break;
                    case "�ʲ����":
                        Grid_ShowMeter.Columns["�ʲ����"].Width = 120;
                        break;
                    case "�������":
                        Grid_ShowMeter.Columns["�������"].Width = 80;
                        break;
                    case "�ͼ쵥λ":
                        Grid_ShowMeter.Columns["�ͼ쵥λ"].Width = 120;
                        break;
                    case "ͨѶ��ַ":
                        Grid_ShowMeter.Columns["ͨѶ��ַ"].Width = 90;
                        break;
                    case "������":
                        Grid_ShowMeter.Columns["������"].Width = 75;
                        break;
                    case "����":
                        Grid_ShowMeter.Columns["����"].Width = 100;
                        break;
                    case "�ȼ�":
                        Grid_ShowMeter.Columns["�ȼ�"].Width = 70;
                        break;
                    case "���ͺ�":
                        Grid_ShowMeter.Columns["���ͺ�"].Width = 100;
                        break;
                    case "�춨���":
                        Grid_ShowMeter.Columns["�춨���"].Width = 110;
                        break;
                    case "ͨѶЭ��":
                        Grid_ShowMeter.Columns["ͨѶЭ��"].Width = 110;
                        break;
                    case "���쳧��":
                        Grid_ShowMeter.Columns["���쳧��"].Width = 120;
                        break;
                    default:
                        //Grid_ShowMeter.Columns[i].Visible = false;
                        Grid_ShowMeter.Columns[i].Width = 90;
                        break;
                }

                if (Grid_ShowMeter.Columns[i].HeaderText.Length < 1)
                    Grid_ShowMeter.Columns[i].Width = 250;
            }
        }
        #endregion

        #region CellValueChanged
        private void Grid_ShowMeter_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            //string strSubBarCode = "";
            if (Grid_ShowMeter.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null || Grid_ShowMeter.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length < 1) return;

            string ColName = Grid_ShowMeter.Columns[e.ColumnIndex].HeaderText;

            if (ColName == "������" || ColName == "�ʲ����" || ColName == "�������")
            {
                string strValue = Grid_ShowMeter.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
                for (int i = 0; i < Grid_ShowMeter.Rows.Count; i++)
                {
                    if (Grid_ShowMeter.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && Grid_ShowMeter.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim() != string.Empty
                        && e.RowIndex != i
                        && Grid_ShowMeter.Rows[i].Cells[e.ColumnIndex].Value != null
                        && Grid_ShowMeter.Rows[i].Cells[e.ColumnIndex].Value.ToString() == strValue && !_BlnLianXuID)
                    {
                        MessageBoxEx.Show(this, string.Format("{0} �ظ���", ColName), "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Grid_ShowMeter.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                        ThreadPool.QueueUserWorkItem(new WaitCallback(thChangGridSelectRow), new int[] { e.RowIndex, e.ColumnIndex });
                        return;
                    }
                }

                //��Ӫ��ϵͳ��ȡ����
                //if (ColName == "������" && !CLDC_DataCore.Const.GlobalUnit.ReadingPara && Chk_TiaoMaJX.Checked)
                //{
                    //strSubBarCode = strValue.Substring(strValue.Length - 4, 4);
                    //strValue = strValue.Substring(0, strValue.Length - 1);

                    //GlobalUnit.g_MsgControl.OutMessage(strSubBarCode, false, Cus_MessageType.������Ϣ);
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(SetValueFromSG186), new string[] { strValue, e.RowIndex.ToString() });
                //}
            }
        }

        private void thChangGridSelectRow(object objIndexs)
        {
            Thread.Sleep(100);
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new EventInvokeChangeGridSelectRow(InvokeChangeGridSelectRow), ((int[])objIndexs)[0], ((int[])objIndexs)[1]);
            }
        }
        private delegate void EventInvokeChangeGridSelectRow(int RowIndex, int ColIndex);
        private void InvokeChangeGridSelectRow(int RowIndex, int ColIndex)
        {
            Grid_ShowMeter.Rows[RowIndex].Cells[ColIndex].Selected = true;
        }
        #endregion

        #endregion

        #region Chk_CheckAll_CheckedChanged(object sender, EventArgs e)
        private void Chk_CheckAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Grid_ShowMeter.Rows.Count; i++)
            {
                Grid_ShowMeter.Rows[i].Cells[0].Value = Chk_CheckAll.Checked;
            }


        }
        #endregion

        #region bool ProcessCmdKey(ref Message msg, Keys keyData)
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {

                if (this.Grid_ShowMeter.IsCurrentCellInEditMode)                //�������ڱ༭״̬
                {
                    int _ColIndex = this.Grid_ShowMeter.CurrentCell.ColumnIndex;

                    int _RowIndex = this.Grid_ShowMeter.CurrentCell.RowIndex;
                    ///ֻ������������߳�����Ŷ����Խ���
                    if ((_ColIndex == 2 || _ColIndex == 4) && _RowIndex >= 0)          //��������س�
                    {
                        Grid_ShowMeter.EndEdit();

                        if (this.Grid_ShowMeter[_ColIndex, _RowIndex].Value == null || this.Grid_ShowMeter[_ColIndex, _RowIndex].Value.ToString() == string.Empty)   //���Ϊ��
                        {
                            Grid_ShowMeter.BeginEdit(true);
                            return true;    //�˳�������
                        }
                        //string key = Grid_ShowMeter[_ColIndex, _RowIndex].Value.ToString();
                        //if (_ColIndex == 2)     //�����
                        //{
                        //    if (key != null && key.Trim() != "")
                        //    {
                        //        string strSubBarCode;


                        //        //for (int i = 4; i > 0; i--)
                        //        //{
                        //        //    strSubBarCode = key.Substring(key.Length - i, 1);
                        //        //    GlobalUnit.g_MsgControl.OutMessage(strSubBarCode, false, Cus_MessageType.������Ϣ);
                        //        //}
                        //        int startKey = 0;
                        //        int subLengh = 4;
                        //        if (key.Length < 4)
                        //        {
                        //            startKey = 0;
                        //            subLengh = key.Length;
                        //        }
                        //        else
                        //        {
                        //            startKey = key.Length - 4;
                        //        }
                        //        strSubBarCode = key.Substring(startKey, subLengh);
                        //        //Comm.Speechs.Speech.Instance.SpeechMessage(strSubBarCode);
                        //        CLDC_Comm.Speechs.Speech.Instance.SpeakNum(strSubBarCode);
                        //        key = key.Substring(0, key.Length - 1);
                        //        CLDC_VerifyAdapter.Helper.EquipHelper.Instance.ShowSerialNoToErrPanel(_RowIndex + 1, Convert.ToInt32(strSubBarCode));
                        //    }
                        //}

                        ///��ȡMeterBasicInfoʵ��
                        //CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo();
                        //MeterInfo = ParentMain.GetMeterInfo(key);                        

                        //if (MeterInfo != null)
                        //{
                        //    MeterInfo.SetBno(_RowIndex + 1);
                        //    MeterInfo.YaoJianYn = true;//fjk
                        //    MeterGroup.MeterGroup[_RowIndex] = MeterInfo;
                        //    this.RefreshData(MeterGroup, TaiType, TaiId);
                        //}
                    }

                    Grid_ShowMeter.BeginEdit(true);
                }
            }
            else if (keyData == (Keys.Control | Keys.C))
            {
                //���� Ctrl+C��ϼ�����DataGridview��ѡ��ģʽΪ����ѡ��ʱ��Ctrl+C���Ƶ������е����ݡ�
                if (Grid_ShowMeter.CurrentCell.Value != null && Grid_ShowMeter.CurrentCell.Value.ToString().Length > 0)
                    Clipboard.SetText(Grid_ShowMeter.CurrentCell.Value.ToString());
                return true;
            }
            //Ĭ�Ͻ���ϵͳ����
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region bool IsInputComplated()  ��������Ƿ�����
        /// <summary>
        /// ��������Ƿ�����
        /// </summary>
        /// <returns></returns>
        private bool IsInputComplated()
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            //����Ƿ���������
            if (Cmb_DianYa.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "��ѡ���ѹ!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_DianYa.Focus();
                return false;
            }

            if (Cmb_DianLiu_Ib.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "��ѡ�����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_DianLiu_Ib.Focus();
                return false;
            }

            if (Cmb_PinLv.Value < 1)
            {
                MessageBoxEx.Show(this, "������Ƶ��!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_PinLv.Focus();
                return false;
            }

            if (Cmb_ShouJianZhouJian.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "�������׼��ܼ�!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_ShouJianZhouJian.Focus();
                return false;
            }

            if (Cmb_CeLiangFangShi.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "�����ò�����ʽ!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_CeLiangFangShi.Focus();
                return false;
            }

            if (Cmb_HuGanQi.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "�����û�����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_HuGanQi.Focus();
                return false;
            }

            if (Cmb_ZhiNiQi.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "������ֹ����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_ZhiNiQi.Focus();
                return false;
            }

            if (Cmb_JianCeLeiXing.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "�����ü������!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_JianCeLeiXing.Focus();
                return false;
            }


            if (Cmb_FA.Text == "")
            {
                MessageBox.Show(this, "��ѡ����Ҫʹ�õļ춨����...", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_FA.Focus();
                return false;
            }


            return true;
        }
        #endregion

        #region Btn_DoComplated_Click(object sender, EventArgs e)
        /// <summary>
        /// ����Ƿ�������ͬʱ�����������ѹ�ȼ춨��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DoComplated_Click(object sender, EventArgs e)
        {
            //���ü춨��ʼʱ��
            CLDC_DataCore.Const.GlobalUnit.g_CheckTimeStartSum = DateTime.Now;

            MessageBoxEx.UseSystemLocalizedString = true;
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetCurFunctionOnOrOff(true);
            MeterGroup.MinConst[0] = 0;
            MeterGroup.MinConst[1] = 0; 
            SetSaveToModel();

            //�����Ƿ�����
            if (!IsInputComplated()) return;



            DateTime _DatJdrq = DateTime.Now;

            //����ѡ��ֵ
            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                #region
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = MeterGroup.MeterGroup[i];
                //��λ��
                MeterInfo._intBno = i + 1;
                //��ѹ
                MeterInfo.Mb_chrUb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianYa);

                //����
                MeterInfo.Mb_chrIb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Ib) + "(" + CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Imax) + ")";

                //Ƶ��
                MeterInfo.Mb_chrHz = Cmb_PinLv.Value.ToString();

                //�춨����
                MeterInfo.Mb_DatJdrq = _DatJdrq.ToString();

                //���ò�����ʽ
                MeterInfo.Mb_intClfs = int.Parse(CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_CeLiangFangShi));

                //������
                if (GlobalUnit.IsDan == true)
                {
                    MeterInfo.Mb_BlnHgq = false;
                }
                else
                {
                    MeterInfo.Mb_BlnHgq = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_HuGanQi) == "��������";
                }
                //ֹ����
                MeterInfo.Mb_BlnZnq = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_ZhiNiQi) == "��ֹ��";

                //�׼��ܼ� Other3
                MeterInfo.Mb_chrOther3 = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_ShouJianZhouJian);

                //������� Cmb_JianCeLeiXing Other2
                MeterInfo.Mb_chrTestType = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_JianCeLeiXing);

                //����ʽʹ�ù��

                MeterInfo.GuiChengName_DianZi = "JJG596-2012";// CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_FKType);

                //��Ӧʽʹ�ù��

                //MeterInfo.GuiChengName_GanYing = CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_HGQ_In);
                //�ѿ�����
                MeterInfo.FKType = CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_FKType);

                //��ǰ�����ѡ�ù��
                if (MeterInfo.MeterType_DzOrGy == CLDC_Comm.Enum.Cus_MeterType_DianziOrGanYing.DianZiShi)
                {
                    MeterInfo.GuiChengName = MeterInfo.GuiChengName_DianZi;
                }
                else
                {
                    MeterInfo.GuiChengName = MeterInfo.GuiChengName_GanYing;
                }
                //�춨����
                MeterInfo.Mb_chrOther5 = MeterInfo.GuiChengName;
                #endregion

                #region �����๦��Э�����ü���

                MeterInfo.DgnProtocol = new CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo();     //ʵ����һ���յ�Э��ģ�ͣ�

                if (MeterInfo.YaoJianYn)     //�����Ҫ��
                {
                    bool bFindProtocol = false;           //Ѱ����ͬЭ�飬��������Ŀ��������ͬЭ��ı�ָ��ͬһ��Э���ַ�����ٿռ�ռ��

                    //Ѱ��Э����ͬ�ı�CopyЭ��
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (MeterGroup.AutoProtocol)            //������Զ�ʶ��
                        {
                            if (MeterInfo.AVR_PROTOCOL_NAME == "")           //�����ǰ���Э������Ϊ��
                            {
                                if (MeterGroup.MeterGroup[j].YaoJianYn && MeterInfo.Mb_chrzzcj == MeterGroup.MeterGroup[j].Mb_chrzzcj
                                    && MeterInfo.Mb_Bxh == MeterGroup.MeterGroup[j].Mb_Bxh)
                                {
                                    MeterInfo.DgnProtocol = MeterGroup.MeterGroup[j].DgnProtocol;               //�����ͬ����ָ��ͬһ����ַ�ڴ�
                                    MeterInfo.AVR_PROTOCOL_NAME = MeterGroup.MeterGroup[j].AVR_PROTOCOL_NAME;
                                    bFindProtocol = true;
                                    break;
                                }
                            }
                            else      //�����Ϊ�յĻ������Э��������ѡ��
                            {
                                if (MeterGroup.MeterGroup[j].YaoJianYn && MeterInfo.AVR_PROTOCOL_NAME == MeterGroup.MeterGroup[j].AVR_PROTOCOL_NAME)
                                {
                                    MeterInfo.DgnProtocol = MeterGroup.MeterGroup[j].DgnProtocol;           //ָ��ͬһ���ڴ��ַ
                                    bFindProtocol = true;
                                    break;
                                }
                            }
                        }
                        else    //�����Զ�ʶ������ݲ���¼���ʱ����ѡ���Э������
                        {
                            if (MeterGroup.MeterGroup[j].YaoJianYn && MeterGroup.MeterGroup[j].AVR_PROTOCOL_NAME == MeterInfo.AVR_PROTOCOL_NAME)         //���ʹ�õĶ�����ͬ��ͨ��Э��
                            {
                                MeterInfo.DgnProtocol = MeterGroup.MeterGroup[j].DgnProtocol;           //ָ��ͬһ���ڴ��ַ
                                bFindProtocol = true;
                                break;
                            }
                        }
                    }

                    if (!bFindProtocol)          //����Ҳ�����ͬ��Э��
                    {
                        if (MeterGroup.AutoProtocol && MeterInfo.AVR_PROTOCOL_NAME == "")         //������Զ�ʶ��(����û��ѡ��๦��Э��)
                        {
                            if (MeterInfo.Mb_chrzzcj != "" && MeterInfo.Mb_Bxh != "")
                            {
                                MeterInfo.DgnProtocol.Load(MeterInfo.Mb_chrzzcj, MeterInfo.Mb_Bxh);         //���ض๦��Э��
                                MeterInfo.AVR_PROTOCOL_NAME = MeterInfo.DgnProtocol.ProtocolName;
                            }
                        }
                        else
                        {
                            if (MeterInfo.AVR_PROTOCOL_NAME != "")               //���ѡ��Э�鲻Ϊ������أ����Ϊ�յĻ����Ͳ����ض๦��Э��
                            {
                                MeterInfo.DgnProtocol.Load(MeterInfo.AVR_PROTOCOL_NAME);
                                MeterInfo.AVR_PROTOCOL_NAME = MeterInfo.DgnProtocol.ProtocolName;
                            }
                        }

                    }
                }

                #endregion

            }

            bool bYaoJianMeter = false;
            int nYaoJianCount = 0;
            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                #region
                if (MeterGroup.MeterGroup[i].YaoJianYn)
                {
                    nYaoJianCount++;
                    //��������Ƿ�����
                    //if (MeterGroup.MeterGroup[i].Mb_ChrTxm.Length < 1)
                    //{
                    //    MessageBoxEx.Show(this,"ȱ��������", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    Grid_ShowMeter.Rows[MeterGroup.MeterGroup[i].Mb_intBno - 1].Selected = true;
                    //    return;
                    //}                    
                    if (MeterGroup.MeterGroup[i].Mb_chrBlx.Length < 1)
                    {
                        MessageBoxEx.Show(this, "ȱ�ٱ�����", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Grid_ShowMeter.Rows[MeterGroup.MeterGroup[i].Mb_intBno - 1].Selected = true;
                        return;
                    }
                    if (MeterGroup.MeterGroup[i].Mb_chrBcs.Length < 1)
                    {
                        MessageBoxEx.Show(this, "ȱ�ٳ���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Grid_ShowMeter.Rows[MeterGroup.MeterGroup[i].Mb_intBno - 1].Selected = true;
                        return;
                    }
                    if (MeterGroup.MeterGroup[i].Mb_chrBdj.Length < 1)
                    {
                        MessageBoxEx.Show(this, "ȱ�ٵȼ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Grid_ShowMeter.Rows[MeterGroup.MeterGroup[i].Mb_intBno - 1].Selected = true;
                        return;
                    }
                    if (MeterGroup.MeterGroup[i].GuiChengName.Length < 1)
                    {
                        MessageBoxEx.Show(this, "ȱ�ټ춨���", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Grid_ShowMeter.Rows[MeterGroup.MeterGroup[i].Mb_intBno - 1].Selected = true;
                        return;
                    }
                    bYaoJianMeter = true;
                }
                #endregion
            }
            if (!bYaoJianMeter)
            {
                MessageBoxEx.Show(this, "û��¼���κα���Ϣ!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int fyjb = MeterGroup.GetFirstYaoJianMeterBwh();
            string Escn = (MeterGroup.MeterGroup[fyjb].DgnProtocol.HaveProgrammingkey == false) ? "13" : "09";
            CLDC_DataCore.Function.File.WriteInIString(CLDC_DataCore.Const.Variable.CONST_ENCRYPTION_INI, CLDC_DataCore.Const.Variable.CONST_ENCRYPTION_SECTION, CLDC_DataCore.Const.Variable.CONST_ENCRYPTION_NEWOLD, Escn);

            #region//��ѹ����������
            float MaxDianYa = float.Parse(CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianYa).ToLower());
            float MaxDianLiu = float.Parse(CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Imax));
            if (CLDC_Comm.LoginSettingData.LoginSetting != null)
            {
                if (MaxDianYa > CLDC_Comm.LoginSettingData.LoginSetting.MaxDianYa)
                {
                    MessageBoxEx.Show(this, "������������ѹ!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cmb_DianYa.Focus();
                    return;
                }

                if (MaxDianLiu > CLDC_Comm.LoginSettingData.LoginSetting.MaxDianLiu)
                {
                    MessageBoxEx.Show(this, "��������������!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cmb_DianLiu_Ib.Focus();
                    return;
                }
            }
            #endregion

            #region ��������(������춨)

            if (MeterGroup.FaName != string.Empty)
            {
                //if (MeterGroup.FaName != Cmb_FA.Text)
                //{
                int intFirst = Main.GetFirstYaoJianMeterIndex(MeterGroup);
                if (MeterGroup.MeterGroup[intFirst].MeterResults.Count != 0
                    || MeterGroup.MeterGroup[intFirst].MeterErrors.Count != 0
                    || MeterGroup.MeterGroup[intFirst].MeterDgns.Count != 0
                    || MeterGroup.MeterGroup[intFirst].MeterZZErrors.Count != 0)
                {
                    //if (MessageBoxEx.Show(this, "���ύ������������춨���ݣ������Ƿ�����ύ��ѡ�����ȡ����ǰ�ύ������", "����ѯ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    CLDC_DataCore.Function.TopWaiting.ShowWaiting("��������춨����...");

                    //    for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
                    //    {
                    //        if (MeterGroup.MeterGroup[i].YaoJianYn)
                    //            MeterGroup.MeterGroup[i].ClearData();
                    //    }
                    //    CLDC_DataCore.Function.TopWaiting.HideWaiting();
                    //}
                }
                //}
            }

            if (!MeterGroup.CreateFA(base.TaiType, Cmb_FA.Text))
            {
                MessageBoxEx.Show(this, "��������ʧ�ܣ������Ƿ���ȷ��ѡ���˷�������ѡ��ļ춨��������Ϊ��...", "����ʧ��", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                DispatcherManager.Instance.Parms.In_AddSchemesStrName = MeterGroup.FaName;
                Dictionary<string, string[]> dic_plan = null;
                DispatcherManager.Instance.Parms.In_AddSchemesCheckIDs = MeterGroup.GetDispatcherPlanKeys(out dic_plan);
                DispatcherManager.Instance.Excute(CLDC_Dispatcher.DispatcherEnum.AddSchemes);

                DispatcherManager.Instance.Parms.In_CurSchemeID = DispatcherManager.Instance.Parms.Out_SchemeID;
                DispatcherManager.Instance.Excute(CLDC_Dispatcher.DispatcherEnum.SetCurScheme);
                DispatcherManager.Instance.Parms.In_MDicCheck = dic_plan;
                DispatcherManager.Instance.Excute(CLDC_Dispatcher.DispatcherEnum.ZoomSetDicCheckID);

                DispatcherManager.Instance.Excute(CLDC_Dispatcher.DispatcherEnum.SetSchemeChanged);
            }
            catch (Exception ex)
            {
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }

            #endregion

            if (GlobalUnit.DispatcherType != 1 && MessageBox.Show(string.Format(@"��ȷ����������:{0}

1��������ʽ�� {4} 
2����ѹ    �� {1}V
3������    �� {2}A
4��Ƶ��    �� {3}Hz
5����������� {5}ֻ

{6}"
 , ""
                                 , CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianYa)
                                 , CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Ib)
                                 , Cmb_PinLv.Value.ToString()
                                 , CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_CeLiangFangShi)
                                 , nYaoJianCount
                                 , "�Ƿ�ȷ��������?".PadRight(20, ' '))
                                , "ȷ����ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            WritePKToModel();
            CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.Control("��λ", false);

            CLDC_DataCore.Function.TopWaiting.ShowWaiting("�����ύ��������...");
            CLDC_DataCore.Const.GlobalUnit.g_CUS.SaveTempDB();//��ʱ��
            if (ParentMain.Evt_InputParam_OnOk == null)
            {
                CLDC_DataCore.Function.TopWaiting.HideWaiting();
                MessageBoxEx.Show(this, "¼�������ɰ�ť�¼�û�б�����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ParentMain.Evt_InputParam_OnOk(MeterGroup, TaiType, TaiId);

                GlobalUnit.g_RealTimeDataControl.OutUpdateRealTimeData("", Cus_MeterDataType.��������Ϣ����);
            }
            try
            {
                DispatcherManager.Instance.Excute(CLDC_Dispatcher.DispatcherEnum.SetMeterChanged);
            }
            catch (Exception ex)
            {
                CLDC_DataCore.Function.ErrorLog.Write(ex);
            }
            CLDC_DataCore.Function.TopWaiting.HideWaiting();
        }
        #endregion
        /// <summary>
        /// ����ʱ���ݿ��ȡPK��Model
        /// </summary>
        private void WritePKToModel()
        {
            string sql = string.Empty;
            int _TaiID = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._TaiID;
            CLDC_DataCore.DataBase.DataControl _Data;
            _Data = new CLDC_DataCore.DataBase.DataControl(false);        //�������ӱ���Ĭ��ACCESS���ݿ�
            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                sql = string.Format("Select PK_LNG_METER_ID FROM TMP_METER_INFO where AVR_DEVICE_ID='{0}' and LNG_BENCH_POINT_NO={1}", _TaiID, i + 1);
                System.Data.OleDb.OleDbDataReader reader = _Data.ExecuteReaderSql(sql);
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        object obj = new object();
                        obj = reader["PK_LNG_METER_ID"];
                        if (obj is string)
                        {
                            long curID;
                            if (long.TryParse(reader["PK_LNG_METER_ID"].ToString(), out curID))
                            {
                                if (curID == 0)
                                {
                                    curID = CLDC_DataCore.Function.Common.GetUniquenessID8(i);
                                }
                                CLDC_DataCore.Const.GlobalUnit.Meter(i)._intMyId = curID;
                            }
                        }
                    }
                }
            }
            _Data.CloseDB();
            _Data = null;
        }


        private DataGridViewCell Cell_LastSelected = null;
        private void Grid_ShowMeter_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Cell_LastSelected != null)
                Cell_LastSelected.Style.SelectionBackColor = Grid_ShowMeter.RowsDefaultCellStyle.SelectionBackColor;
            Cell_LastSelected = Grid_ShowMeter.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Cell_LastSelected.Style.SelectionBackColor = Color.Gray; //Color.FromArgb(220,220,220);

            Grid_ShowMeter.BeginEdit(true);
        }

        //���±�ť
        private void Btn_ClearMeter_Click(object sender, EventArgs e)
        {
            if (CLDC_DataCore.Const.StdMeterConst.m_LastSearchU > 36.0f || CLDC_DataCore.Const.StdMeterConst.m_LastSearchI > 0.1)
            {
                if (CLDC_DataCore.Const.GlobalUnit.IsDemo == false)
                {
                    MessageBoxEx.Show(this, "���ȹص�Դ");
                }
            }


            MessageBoxEx.UseSystemLocalizedString = true;
            if (MessageBoxEx.Show(this, "���Ҫ���±���?", "������ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            if (ParentMain.Evt_OnHangUpNewMeter != null)
            {
                //Comm.Function.TopWaiting.ShowWaiting("����ִ�й��±����...");
                if (!ParentMain.Evt_OnHangUpNewMeter(TaiType, TaiId))
                {
                    CLDC_DataCore.Function.TopWaiting.HideWaiting();
                    MessageBoxEx.Show(this, "����ʧ��!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //���������ڵ�һ�������      
                Grid_ShowMeter.Focus();
                Grid_ShowMeter.Rows[0].Cells[2].Selected = true;
            }

            CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.Control("�ұ�λ", false);

        }

        //�Զ����ɱ��
        private void Btn_BuildData_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;



            if (Cell_LastSelected == null)
            {
                MessageBoxEx.Show(this, "����ѡ��һ����Ҫ�������ݵĵ�Ԫ��!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int RowIndex = Cell_LastSelected.RowIndex;
            int ColIndex = Cell_LastSelected.ColumnIndex;
            string ColName = Grid_ShowMeter.Columns[ColIndex].HeaderText;
            if (ColName == "������" || ColName == "�ʲ����" || ColName == "�������" || ColName == "ͨѶ��ַ" || ColName == "�������"
                || ColName == "�ͼ쵥λ" || ColName == getColName("Ǧ��1").ColShowName || ColName == getColName("Ǧ��2").ColShowName || ColName == getColName("Ǧ��3").ColShowName || ColName == getColName("Ǧ��4").ColShowName
                || ColName == getColName("Ǧ��5").ColShowName || ColName == "֤����")
            {
                string strStartNumber = "";
                if (!CLDC_DataCore.Function.InputBox.Show("��������ʼ����:", ColName, ref strStartNumber))
                {
                    return;
                }
                if (ColName == "֤����")
                {
                    string sKey = CLDC_DataCore.Const.Variable.CTC_OTHER_PREFIXOFCERTIFICATEN;
                    string sPrefix = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getValue(sKey);
                    strStartNumber = sPrefix + strStartNumber;
                }
                strStartNumber = strStartNumber.Trim();
                string StartString = string.Empty; //����벿��
                string LastNumber = string.Empty; //�����벿��
                int LastNumberLen = 0;
                for (int i = strStartNumber.Length - 1; i >= 0; i--)
                {
                    if (LastNumber.Length >= 9)    //�������ֵĴ�С��ֹ���
                    {
                        break;
                    }

                    if ("0123456789".IndexOf(strStartNumber[i]) != -1)
                    {
                        LastNumber = strStartNumber[i] + LastNumber;
                    }
                    else
                    {//ѡ��ֻ��ȡ�ַ���������һ�����ִ�
                        if (ColName == "֤����") break;
                    }
                }
                StartString = strStartNumber.Substring(0, strStartNumber.Length - LastNumber.Length);
                LastNumberLen = LastNumber.Length;
                if (LastNumberLen == 0)
                {
                    LastNumber = "0";
                }
                //Begain Edit
                //�޸��ظ���Ŀ��ⷽ��
                List<string> lstExists = new List<string>();
                //End Edit
                for (int i = RowIndex, j = 0; i < Grid_ShowMeter.Rows.Count; i++, j++)
                {
                    string strValue = "";
                    //�������
                    if (strStartNumber.Length > 0 && Chk_LianXuData.Checked)
                    {

                        strValue = string.Format("{0}{1}", StartString, (long.Parse(LastNumber) + j).ToString().PadLeft(LastNumberLen, '0'));
                        _BlnLianXuID = true;
                    }
                    //ȫ����ͬ
                    else if (strStartNumber.Length > 0)
                    {
                        strValue = strStartNumber;
                    }
                    //����� ������|�ʲ����|������� ͬʱ������������š���ֻ�����һ��
                    if ((ColName == "������" || ColName == "�ʲ����" || ColName == "�������") && strValue.Length > 0 && !Chk_LianXuData.Checked && i == RowIndex)
                    {
                        //for (int k = 0; k < Grid_ShowMeter.Rows.Count; k++)
                        //{
                        //  if (Grid_ShowMeter.Rows[k].Cells[ColIndex].Value != null
                        //     && Grid_ShowMeter.Rows[k].Cells[ColIndex].Value.ToString() == strValue)
                        if (lstExists.Contains(strValue) && strValue != string.Empty)
                        {
                            MessageBoxEx.Show(this, string.Format("{0} �ظ�!", ColName), "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ThreadPool.QueueUserWorkItem(new WaitCallback(thChangGridSelectRow), new int[] { RowIndex, ColIndex });
                            return;
                        }
                        else
                        {
                            lstExists.Add(strValue);
                        }
                        //}
                    }
                    else if ((ColName == "������" || ColName == "�ʲ����" || ColName == "�������") && strValue.Length > 0 && !Chk_LianXuData.Checked && i > RowIndex)
                    {
                        return;
                    }
                    Grid_ShowMeter.Rows[i].Cells[ColIndex].Value = strValue;

                }

                _BlnLianXuID = false;
            }
            else
            {
                MessageBoxEx.Show(this, "��ǰѡ�е��в����Զ���������!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Grid_ShowMeter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        /// <summary>
        /// �鿴������ϸ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_FA_Click(object sender, EventArgs e)
        {
            CLDC_MeterUI.UI_FA.Frm_FaSetup FaGroup = new CLDC_MeterUI.UI_FA.Frm_FaSetup((CLDC_Comm.Enum.Cus_TaiType)base.TaiType);

            string TmpString = Cmb_FA.Text;

            FaGroup.SetSelectFa(Cmb_FA.Text);
            FaGroup.ShowDialog();               //ģʽ�����

            this.SetFaList();               //������䷽���б�

            this.Cmb_FA.Text = TmpString;
        }

        private delegate void EventInvokeSetRowNewValue(string meternum, string RowIndex);
        /// <summary>
        /// ��Ӫ��ϵͳ��ȡ����
        /// </summary>
        /// <param name="setRowData"></param>
        private void SetValueFromSG186(object strdata)
        {
            Thread.Sleep(100);
            if (this.IsHandleCreated)
            {
                this.BeginInvoke(new EventInvokeSetRowNewValue(SetValueFromSG186), ((string[])strdata)[0], ((string[])strdata)[1]);
            }
        }
        /// <summary>
        /// ��Ӫ��ϵͳ��ȡ����
        /// </summary>
        /// <param name="meterNum"></param>
        /// <param name="rowIndex"></param>
        private void SetValueFromSG186(string barcode, string index)
        {
            
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="serder"></param>
        /// <param name="e"></param>
        private void Btn_DownMeterInfo_Click(object serder, EventArgs e)
        {
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_DownMeterInfo, false);
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_DownSchemeInfo, false);
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_ReadPara, false);
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_DoComplated, false);
            
            SetSaveToModel();

            MeterGroup.ActiveItemID = -1;

            if (ParentMain.Evt_DownMeterInfoFromMis == null)
            {
                CLDC_DataCore.Function.TopWaiting.HideWaiting();
                MessageBox.Show("���ص����Ϣ��ť�¼�û�б�����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ParentMain.Evt_DownMeterInfoFromMis(MeterGroup, TaiType, TaiId);
            }

            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_DownMeterInfo, true);
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_DownSchemeInfo, true);
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_ReadPara, true);
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_DoComplated, true);
        }

        /// <summary>
        /// ���ط���
        /// </summary>
        /// <param name="serder"></param>
        /// <param name="e"></param>
        private void Btn_DownSchemeInfo_Click(object serder, EventArgs e)
        {
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_DownMeterInfo, false);
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_DownSchemeInfo, false);
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_ReadPara, false);
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_DoComplated, false);

            Cmb_FA.Text = "";

            MeterGroup.ActiveItemID = -1;

            if (ParentMain.Evt_DwonSchemeInfoFromMis == null)
            {
                CLDC_DataCore.Function.TopWaiting.HideWaiting();
                MessageBox.Show("���ط�����Ϣ��ť�¼�û�б�����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ParentMain.Evt_DwonSchemeInfoFromMis(MeterGroup, TaiType, TaiId);
            }

            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_DownMeterInfo, true);
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_DownSchemeInfo, true);
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_ReadPara, true);
            CLDC_DataCore.Function.SetControl.SetEnabled(this.Btn_DoComplated, true);
        }
        private void Btn_ReadPara_Click(object sender, EventArgs e)
        {
            if (CLDC_DataCore.Const.GlobalUnit.g_CheckUserLevel != "����Ա")
            {
                MessageBoxEx.Show(this,"�ǹ���ԱȨ��,Ȩ�޲��㣡");
                return;
            }

            int nYaoJianCount = 0;
            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {                
                if (MeterGroup.MeterGroup[i].YaoJianYn)
                {
                    nYaoJianCount++;
                }
            }
            if (GlobalUnit.DispatcherType != 1 && MessageBox.Show(string.Format(@"��ȷ����������:{0}

1��������ʽ�� {4} 
2����ѹ    �� {1}V
3������    �� {2}A
4��Ƶ��    �� {3}Hz
5����������� {5}ֻ

{6}"
 , ""
                                 , CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianYa)
                                 , CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Ib)
                                 , Cmb_PinLv.Value.ToString()
                                 , CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_CeLiangFangShi)
                                 , nYaoJianCount
                                 , "�Ƿ�ȷ��������?".PadRight(20, ' '))
                                , "ȷ����ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            DateTime _DatJdrq = DateTime.Now;
            MessageBoxEx.UseSystemLocalizedString = true;
            CLDC_DataCore.Const.GlobalUnit.ReadingPara = true;
            if (!IsInputComplated()) return;
            
            SetSaveToModel();
            //����ѡ��ֵ
            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                

                #region
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = MeterGroup.MeterGroup[i];
                //��λ��
                MeterInfo._intBno = 1 + 1;
                //��ѹ
                MeterInfo.Mb_chrUb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianYa);

                //����
                MeterInfo.Mb_chrIb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Ib) + "(" + CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Imax) + ")";

                //Ƶ��
                MeterInfo.Mb_chrHz = Cmb_PinLv.Value.ToString();

                //�춨����
                MeterInfo.Mb_DatJdrq = _DatJdrq.ToString();

                //���ò�����ʽ
                MeterInfo.Mb_intClfs = int.Parse(CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_CeLiangFangShi));

                //������
                if (GlobalUnit.IsDan == true)
                {
                    MeterInfo.Mb_BlnHgq = false;
                }
                else
                {
                    MeterInfo.Mb_BlnHgq = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_HuGanQi) == "��������";
                }
                //ֹ����
                MeterInfo.Mb_BlnZnq = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_ZhiNiQi) == "��ֹ��";

                //�׼��ܼ� Other3
                MeterInfo.Mb_chrOther3 = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_ShouJianZhouJian);

                //������� Cmb_JianCeLeiXing Other2
                MeterInfo.Mb_chrTestType = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_JianCeLeiXing);

                //����ʽʹ�ù��

                MeterInfo.GuiChengName_DianZi = "JJG596-2012";// CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_FKType);

                //��Ӧʽʹ�ù��

                //MeterInfo.GuiChengName_GanYing = CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_HGQ_In);
                //�ѿ�����
                MeterInfo.FKType = CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_FKType);

                //��ǰ�����ѡ�ù��
                if (MeterInfo.MeterType_DzOrGy == CLDC_Comm.Enum.Cus_MeterType_DianziOrGanYing.DianZiShi)
                {
                    MeterInfo.GuiChengName = MeterInfo.GuiChengName_DianZi;
                }
                else
                {
                    MeterInfo.GuiChengName = MeterInfo.GuiChengName_GanYing;
                }
                //�춨����
                MeterInfo.Mb_chrOther5 = MeterInfo.GuiChengName;
                #endregion

                #region �����๦��Э�����ü���

                MeterInfo.DgnProtocol = new CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo();     //ʵ����һ���յ�Э��ģ�ͣ�

                //��ѹ
                MeterInfo.Mb_chrUb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianYa);

                //����
                //MeterInfo.Mb_chrIb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Ib);//�˴�����ֻ��һ��IBֵ��Ҫ����Imax��ֵ
                MeterInfo.Mb_chrIb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Ib) + "(" + CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Imax) + ")";

                if (MeterInfo.YaoJianYn)     //�����Ҫ��
                {
                    bool bFindProtocol = false;           //Ѱ����ͬЭ�飬��������Ŀ��������ͬЭ��ı�ָ��ͬһ��Э���ַ�����ٿռ�ռ��

                    //Ѱ��Э����ͬ�ı�CopyЭ��
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (MeterGroup.AutoProtocol)            //������Զ�ʶ��
                        {
                            if (MeterInfo.AVR_PROTOCOL_NAME == "")           //�����ǰ���Э������Ϊ��
                            {
                                if (MeterGroup.MeterGroup[j].YaoJianYn && MeterInfo.Mb_chrzzcj == MeterGroup.MeterGroup[j].Mb_chrzzcj
                                    && MeterInfo.Mb_Bxh == MeterGroup.MeterGroup[j].Mb_Bxh)
                                {
                                    MeterInfo.DgnProtocol = MeterGroup.MeterGroup[j].DgnProtocol;               //�����ͬ����ָ��ͬһ����ַ�ڴ�
                                    MeterInfo.AVR_PROTOCOL_NAME = MeterGroup.MeterGroup[j].AVR_PROTOCOL_NAME;
                                    bFindProtocol = true;
                                    break;
                                }
                            }
                            else      //�����Ϊ�յĻ������Э��������ѡ��
                            {
                                if (MeterGroup.MeterGroup[j].YaoJianYn && MeterInfo.AVR_PROTOCOL_NAME == MeterGroup.MeterGroup[j].AVR_PROTOCOL_NAME)
                                {
                                    MeterInfo.DgnProtocol = MeterGroup.MeterGroup[j].DgnProtocol;           //ָ��ͬһ���ڴ��ַ
                                    bFindProtocol = true;
                                    break;
                                }
                            }
                        }
                        else    //�����Զ�ʶ������ݲ���¼���ʱ����ѡ���Э������
                        {
                            if (MeterGroup.MeterGroup[j].YaoJianYn && MeterGroup.MeterGroup[j].AVR_PROTOCOL_NAME == MeterInfo.AVR_PROTOCOL_NAME)         //���ʹ�õĶ�����ͬ��ͨ��Э��
                            {
                                MeterInfo.DgnProtocol = MeterGroup.MeterGroup[j].DgnProtocol;           //ָ��ͬһ���ڴ��ַ
                                bFindProtocol = true;
                                break;
                            }
                        }
                    }

                    if (!bFindProtocol)          //����Ҳ�����ͬ��Э��
                    {
                        if (MeterGroup.AutoProtocol && MeterInfo.AVR_PROTOCOL_NAME == "")         //������Զ�ʶ��(����û��ѡ��๦��Э��)
                        {
                            if (MeterInfo.Mb_chrzzcj != "" && MeterInfo.Mb_Bxh != "")
                            {
                                MeterInfo.DgnProtocol.Load(MeterInfo.Mb_chrzzcj, MeterInfo.Mb_Bxh);         //���ض๦��Э��
                                MeterInfo.AVR_PROTOCOL_NAME = MeterInfo.DgnProtocol.ProtocolName;
                            }
                        }
                        else
                        {
                            if (MeterInfo.AVR_PROTOCOL_NAME != "")               //���ѡ��Э�鲻Ϊ������أ����Ϊ�յĻ����Ͳ����ض๦��Э��
                            {
                                MeterInfo.DgnProtocol.Load(MeterInfo.AVR_PROTOCOL_NAME);
                                MeterInfo.AVR_PROTOCOL_NAME = MeterInfo.DgnProtocol.ProtocolName;
                            }
                        }

                    }
                }
                #endregion
            }

            MeterGroup.ActiveItemID = -1;

            if (ParentMain.Evt_ReadPara == null)
            {
                CLDC_DataCore.Function.TopWaiting.HideWaiting();
                MessageBoxEx.Show(this, "��ȡ������ť�¼�û�б�����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ParentMain.Evt_ReadPara(MeterGroup, TaiType, TaiId);
            }
            CLDC_DataCore.Function.TopWaiting.HideWaiting();

        }

        private void Grid_ShowMeter_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void Cmb_FA_Click(object sender, EventArgs e)
        {
            SetFaList();
        }
        /// <summary>
        /// ��ȡ����¼��Ҫ��ʾ������
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        private CLDC_DataCore.Struct.StColsVisiable getColName(string strName)
        {
            return GlobalUnit.g_SystemConfig.ColsVisiable.getColPrj(strName);
        }
    }
}
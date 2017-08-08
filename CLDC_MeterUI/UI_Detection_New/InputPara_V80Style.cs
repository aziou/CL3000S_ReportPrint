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

        private bool _BlnLianXuID = false;       //连续编号标记


        private string _strNowRun = "当前状态";
        public string strNowRun
        {
            set
            {
                _strNowRun = value;
                this.labelX3.Text = "当前状态：" + _strNowRun;
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
        ///用来刷新表位有没有挂电表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResfreshMeterFlogTime_Tick(object sender, EventArgs e)
        {
            if (GlobalUnit.GetConfig(Variable.CTC_AUTO_ISHAVECHECKMETER, "否") == "否" || GlobalUnit.IsDemo)
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
                CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.Control("读状态", false);
            }
        }
        #region 右键
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
                {//添加单击表名时的处理函数
                    ResfreshMeterFlogTime.Enabled = false;
                    CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.Control("读状态", false);
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
            bi_Up.Text = "向上选表";
            bi_Up.Click -= new EventHandler(bi_Up_Click);
            bi_Up.Click += new EventHandler(bi_Up_Click);
            contextMenuStrip1.Items.Add(bi_Up);

            ToolStripButton bi_Down = new ToolStripButton();
            bi_Down.Name = "DownSelect";
            bi_Down.Text = "向下选表";
            bi_Down.Click -= new EventHandler(bi_Down_Click);
            bi_Down.Click += new EventHandler(bi_Down_Click);
            contextMenuStrip1.Items.Add(bi_Down);

            ToolStripButton bi_UpCancel = new ToolStripButton();//, 
            bi_UpCancel.Name = "UpCancelSelect";
            bi_UpCancel.Text = "向上取消选表";
            bi_UpCancel.Click -= new EventHandler(bi_UpCancel_Click);
            bi_UpCancel.Click += new EventHandler(bi_UpCancel_Click);
            contextMenuStrip1.Items.Add(bi_UpCancel);

            ToolStripButton bi_DownCancel = new ToolStripButton();
            bi_DownCancel.Name = "DownCancelSelect";
            bi_DownCancel.Text = "向下取消选表";
            bi_DownCancel.Click -= new EventHandler(bi_DownCancel_Click);
            bi_DownCancel.Click += new EventHandler(bi_DownCancel_Click);
            contextMenuStrip1.Items.Add(bi_DownCancel);

            ////标题列右键
            //List<CLDC_DataCore.Struct.StColsVisiable> lst_CV = GlobalUnit.g_SystemConfig.ColsVisiable.getColPrj();
            //foreach (CLDC_DataCore.Struct.StColsVisiable item in lst_CV)
            //{
            //    ctMS_Header.Items.Add(item.ColShowName, imglst_IsChecked.Images[item.ColShowType], tsbX_Click);
            //}

            //ToolStripMenuItem tsb_C2 = new ToolStripMenuItem("铅封1");//, 
            //tsb_C2.Name = "C2";
            ////tsb_C2.Text = "铅封1";
            ////tsb_C2.Image = Image.FromFile("F:\\images\\photo_add_watermark_pop_03.gif");
            //tsb_C2.Click -= new EventHandler(tsbX_Click);
            //tsb_C2.Click += new EventHandler(tsbX_Click);
            //ctMS_Header.Items.Add(tsb_C2);
            ////ctMS_Header.Items.Add("铅封1", Image.FromFile("F:\\images\\photo_add_watermark_pop_03.gif"));
            //ctMS_Header.Items[1].Image = imglst_IsChecked.Images[1];

        }

        void bi_Down_Click(object sender, EventArgs e)
        {
            for (int i = Grid_ShowMeter.SelectedRows[0].Index; i < MeterGroup.MeterGroup.Count; i++)
            {
                DataGridViewRow Row = Grid_ShowMeter.Rows[i];
                Row.Cells["要检"].Value = true;

            }

        }

        void bi_Up_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Grid_ShowMeter.SelectedRows[0].Index; i++)
            {
                DataGridViewRow Row = Grid_ShowMeter.Rows[i];
                Row.Cells["要检"].Value = true;

            }

        }

        void bi_DownCancel_Click(object sender, EventArgs e)
        {
            for (int i = Grid_ShowMeter.SelectedRows[0].Index; i < MeterGroup.MeterGroup.Count; i++)
            {
                DataGridViewRow Row = Grid_ShowMeter.Rows[i];
                Row.Cells["要检"].Value = false;

            }

        }

        void bi_UpCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i <= Grid_ShowMeter.SelectedRows[0].Index; i++)
            {
                DataGridViewRow Row = Grid_ShowMeter.Rows[i];
                Row.Cells["要检"].Value = false;

            }

        }

        void tsbX_Click(object sender, EventArgs e)
        {
            //TODO:列显示的右键列事件
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
        /// 刷新表格数据
        /// </summary>
        /// <param name="meterGroup"></param>
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int taiType, int taiId)
        {
            base.SetData(meterGroup, taiType, taiId);

            //按表位数加行
            for (int i = 1; i <= MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = MeterGroup.GetMeterBasicInfoByBwh(i);


                DataGridViewRow Row = Grid_ShowMeter.Rows[i - 1];

                if (MeterInfo != null)  //如果没有找到，则说明前方配置传递的参数有错误，具体表现为：该位置行上所有数据为空
                {
                    Row.Cells["要检"].Value = MeterInfo.YaoJianYn;
                    Row.Cells["表位号"].Value = i.ToString().PadLeft(2, '0');//"第" + i.ToString().PadLeft(2, '0') + "表位";
                    Row.Cells["条形码"].Value = MeterInfo.Mb_ChrTxm;
                    Row.Cells["脉冲"].Value = MeterInfo.Mb_gygy.ToString();
                    Row.Cells["资产编号"].Value = MeterInfo.Mb_ChrJlbh;
                    Row.Cells["通讯地址"].Value = MeterInfo.Mb_chrAddr;
                    SetGridViewComboCellValue("通讯协议", Row.Cells["通讯协议"], MeterInfo.AVR_PROTOCOL_NAME);
                    SetGridViewComboCellValue("载波协议", Row.Cells["载波协议"], MeterInfo.AVR_CARR_PROTC_NAME);
                    SetGridViewComboCellValue("常数", Row.Cells["常数"], MeterInfo.Mb_chrBcs);
                    SetGridViewComboCellValue("等级", Row.Cells["等级"], MeterInfo.Mb_chrBdj);
                    SetGridViewComboCellValue("制造厂家", Row.Cells["制造厂家"], MeterInfo.Mb_chrzzcj);
                    SetGridViewComboCellValue("表类型", Row.Cells["表类型"], MeterInfo.Mb_chrBlx);
                    SetGridViewComboCellValue("表型号", Row.Cells["表型号"], MeterInfo.Mb_Bxh);
                    SetGridViewComboCellValue("送检单位", Row.Cells["送检单位"], MeterInfo.Mb_chrSjdw);
                    Row.Cells["任务编号"].Value = MeterInfo.AVR_TASK_NO;
                    Row.Cells["工单号"].Value = MeterInfo.AVR_WORK_NO;
                    Row.Cells["计量编号"].Value = MeterInfo.Mb_ChrJlbh;
                    Row.Cells["出厂编号"].Value = MeterInfo.Mb_ChrCcbh;
                    Row.Cells["出厂日期"].Value = MeterInfo.Mb_chrCcrq;
                    Row.Cells["证书编号"].Value = MeterInfo.Mb_chrZsbh;
                    Row.Cells["表名称"].Value = MeterInfo.Mb_ChrBmc;
                    Row.Cells[getColName("铅封1").ColShowName].Value = MeterInfo.Mb_chrQianFeng1;
                    Row.Cells[getColName("铅封2").ColShowName].Value = MeterInfo.Mb_chrQianFeng2;
                    Row.Cells[getColName("铅封3").ColShowName].Value = MeterInfo.Mb_chrQianFeng3;
                    Row.Cells[getColName("铅封4").ColShowName].Value = MeterInfo.AVR_SEAL_4;
                    Row.Cells[getColName("铅封5").ColShowName].Value = MeterInfo.AVR_SEAL_5;
                    Row.Cells["软件版本号"].Value = MeterInfo.Mb_chrSoftVer;
                    Row.Cells["硬件版本号"].Value = MeterInfo.Mb_chrHardVer;
                    Row.Cells["到货批次号"].Value = MeterInfo.Mb_chrArriveBatchNo;
                    Row.Cells["备用1"].Value = MeterInfo.Mb_chrOther1;
                    Row.Cells["备用2"].Value = MeterInfo.Mb_chrOther2;
                    Row.Cells["备用3"].Value = MeterInfo.Mb_chrOther3;
                    Row.Cells["备用4"].Value = MeterInfo.Mb_chrOther4;
                    Row.Cells["备用5"].Value = MeterInfo.Mb_chrOther5;
                }
            }

            //设置 电压电流等信息
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = null;

                //获取第一只要检的第一只表
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
                {
                    if (MeterGroup.MeterGroup[i].YaoJianYn == true)
                    {
                        MeterInfo = MeterGroup.MeterGroup[i];
                        break;
                    }
                }
                if (MeterInfo == null) return;

                //电压
                if (MeterInfo.Mb_chrUb.Length > 0)
                {
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_DianYa, MeterInfo.Mb_chrUb);
                }

                //电流
                if (MeterInfo.Mb_chrIb.Length > 0)
                {
                    string[] tmp = MeterInfo.Mb_chrIb.Split('(');
                    if (tmp.Length > 0)
                        CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_DianLiu_Ib, tmp[0]);
                    if (tmp.Length > 1)
                        CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_DianLiu_Imax, tmp[1].Replace(")", ""));
                }

                //频率
                if (MeterInfo.Mb_chrHz.Length > 0)
                    Cmb_PinLv.Value = decimal.Parse(MeterInfo.Mb_chrHz);

                //首检周检 Other1
                if (MeterInfo.Mb_chrOther3 != "")
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_ShouJianZhouJian, MeterInfo.Mb_chrOther3);

                //检测类型 Cmb_JianCeLeiXing Other2
                if (MeterInfo.Mb_chrTestType != "")
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_JianCeLeiXing, MeterInfo.Mb_chrTestType);

                //设置测量方式
                if (GlobalUnit.IsDan == true)
                { 
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_CeLiangFangShi, ((int)Cus_Clfs.单相).ToString()); 
                }
                else
                {
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_CeLiangFangShi, MeterInfo.Mb_intClfs.ToString());
                }
                //互感器
                if (MeterInfo.Mb_BlnHgq == true)
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_HuGanQi, "经互感器");
                else
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_HuGanQi, "直接接入");

                //止逆器
                if (MeterInfo.Mb_BlnZnq == true)
                    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_ZhiNiQi, "有止逆");
            }
        }

        #endregion

        /// <summary>
        /// 设置COMBO数据显示，如果在字典中找不到对应数据，则需要重新绑定
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
                if (((DataRowView)CmbCell.Items[i]).Row["值"].ToString() == StrValue.Trim())
                {
                    CmbCell.Value = StrValue;
                    CmbCell.Tag = StrValue;
                    return;
                }
            }
            //如果不存在，则重新绑定

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

        #region 初始化控件以及填充下拉数据 void InitControls()
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        {
            // ===================== Grid_ShowMeter =====================

            string _ErrorString = "";
            string _ColName = "";
            XmlNode _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_COLSVISIABLE, out _ErrorString);

            DataTable dtTmp = new DataTable();
            dtTmp.Columns.Add("要检", typeof(bool)); //是否要检
            dtTmp.Columns.Add("表位号", typeof(string));
            dtTmp.Columns.Add("条形码", typeof(string));
            dtTmp.Columns.Add("资产编号", typeof(string));
            dtTmp.Columns.Add("表类型", typeof(string));
            dtTmp.Columns.Add("常数", typeof(string));
            dtTmp.Columns.Add("等级", typeof(string));
            dtTmp.Columns.Add("脉冲", typeof(string));
            dtTmp.Columns.Add("表型号", typeof(string));
            dtTmp.Columns.Add("检定规程", typeof(string));
            dtTmp.Columns.Add("通讯协议", typeof(string));
            dtTmp.Columns.Add("载波协议", typeof(string));
            dtTmp.Columns.Add("通讯地址", typeof(string));
            dtTmp.Columns.Add("制造厂家", typeof(string));
            dtTmp.Columns.Add("送检单位", typeof(string));
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
             Grid_ShowMeter 表中的下拉数据说明、
             * 1、每一列的下拉控件选择包括、  [显示文本]和[对应的值]，一般情况下 [显示文本] == [对应的值]
             * 2、每一列下拉控件绑定的数据表最后一行数据格式比如如下：
             *    [值] = [列名]|[其他数据]
             * 
             * 3、若需要新增则最后一列的
             *    [显示文本] = "===新增==="
             *    [值]       = [列名]|[输入数据的一个完整正则匹配]
             */
            Grid_ShowMeter.Columns.Clear();
            foreach (DataColumn dtCol in dtTmp.Columns)
            {
                DataGridViewColumn GridCol = null;
                if (dtCol.ColumnName == "要检")
                {
                    GridCol = new DataGridViewCheckBoxColumn();
                    GridCol.Width = Chk_CheckAll.Width;
                }
                else if (dtCol.ColumnName == "表类型" || dtCol.ColumnName == "常数" || dtCol.ColumnName == "等级"
                    || dtCol.ColumnName == "表型号" || dtCol.ColumnName == "制造厂家" || dtCol.ColumnName == "通讯协议" || dtCol.ColumnName == "载波协议" || dtCol.ColumnName == "送检单位" ||
                    dtCol.ColumnName == "脉冲" || dtCol.ColumnName == "检定规程"
                    )
                {
                    GridCol = new DataGridViewComboBoxColumn();

                    DataTable dtDpl = this.BindComboBoxDataSource(dtCol.ColumnName, "");

                    // 绑定下拉、将 dtDpl数据表的数据绑定到该列的下拉中、 【名称】显示、【值】作为值
                    ((DataGridViewComboBoxColumn)GridCol).DisplayMember = "名称";
                    ((DataGridViewComboBoxColumn)GridCol).ValueMember = "值";
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
            Grid_ShowMeter.Columns["表位号"].ReadOnly = true;

            #region //加载检定环境下拉
            {
                CLDC_DataCore.SystemModel.Item.csDictionary Dic = new CLDC_DataCore.SystemModel.Item.csDictionary();
                Dic.Load();
                List<string> LstData = Dic.getValues("电压");
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_DianYa, LstData, true, "电压", @"\d+", true, null);
                LstData = Dic.getValues("电流");
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_DianLiu_Ib, LstData, true, "电流", @"((\d+\.)?\d)", true, null);//规则1.5(6)：@"((\d+\.)?\d+\((\d+\.)?\d+\))"
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_DianLiu_Imax, LstData, true, "电流", @"((\d+\.)?\d)", true, null);
                //LstData.Clear();
                //LstData.Add("单相");
                //LstData.Add("三相四线");
                //LstData.Add("三相三线");
                //LstData.Add("二元件跨相90°");
                //LstData.Add("二元件跨相60°");
                //LstData.Add("三元件跨相90°");
                //Comm.Function.BindCombox.BindComboxItem(Cmb_CeLiangFangShi, LstData);
                {
                    DataTable dtTmp2 = new DataTable();
                    dtTmp2.Columns.Add("名称", typeof(string));
                    dtTmp2.Columns.Add("值", typeof(string));
                    dtTmp2.Rows.Add(new object[] { "三相四线", "0" });
                    dtTmp2.Rows.Add(new object[] { "三相三线", "1" });
                    dtTmp2.Rows.Add(new object[] { "二元件跨相90°", "2" });
                    dtTmp2.Rows.Add(new object[] { "二元件跨相60°", "3" });
                    dtTmp2.Rows.Add(new object[] { "三元件跨相90°", "4" });
                    dtTmp2.Rows.Add(new object[] { "单相", "5" });
                    Cmb_CeLiangFangShi.ValueMember = "值";
                    Cmb_CeLiangFangShi.DisplayMember = "名称";
                    Cmb_CeLiangFangShi.DataSource = dtTmp2;
                }
                if (CLDC_DataCore.Const.GlobalUnit.IsDan)
                {
                    Cmb_CeLiangFangShi.Text = "单相";
                    Cmb_CeLiangFangShi.Enabled = false;
                }
                //互感器
                LstData.Clear();
                LstData.Add("经互感器");
                LstData.Add("直接接入");
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_HuGanQi, LstData);
                Cmb_HuGanQi.SelectedIndex = 0;

                //止逆器
                LstData.Clear();
                LstData.Add("有止逆");
                LstData.Add("无止逆");
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_ZhiNiQi, LstData);
                Cmb_ZhiNiQi.SelectedIndex = 1;

                //检测类型
                LstData.Clear();
                LstData.Add("客户代检");
                LstData.Add("质量抽检");
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_JianCeLeiXing, LstData);
                Cmb_JianCeLeiXing.SelectedIndex = 0;

                //首检周检
                LstData = Dic.getValues("检定类型");
                if (LstData.Count == 0)
                {
                    LstData.Add("首检");
                    LstData.Add("周检");
                    LstData.Add("抽检");
                    LstData.Add("非首检");
                }
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_ShouJianZhouJian, LstData);
                Cmb_ShouJianZhouJian.SelectedIndex = 0;


                //感应式规程
                //LstData.Clear();
                //LstData.Add("JJG307-2006");
                //LstData.Add("JJG307-1988");
                //CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_HGQ_In, LstData);
                //Cmb_HGQ_In.SelectedIndex = 0;

                //电子规程
                LstData.Clear();
                LstData.Add("远程费控");//JJG596-1999
                LstData.Add("本地费控");
                LstData.Add("不带费控");
                CLDC_DataCore.Function.BindCombox.BindComboxItem(Cmb_FKType, LstData);
                Cmb_FKType.SelectedIndex = 0;

                //获取第一支要检表
                int FirstIndex = Main.GetFirstYaoJianMeterIndex(MeterGroup);

                if (FirstIndex > -1)
                {

                    //if (MeterGroup.MeterGroup[FirstIndex].GuiChengName_GanYing.Length > 0)
                    //    CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_HGQ_In, MeterGroup.MeterGroup[FirstIndex].GuiChengName_GanYing);
                    if (MeterGroup.MeterGroup[FirstIndex].FKType.Length > 0)
                        CLDC_DataCore.Function.BindCombox.BindSelectItem(Cmb_FKType, MeterGroup.MeterGroup[FirstIndex].FKType);
                }

                //设置方案下拉列表
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

            //设置数据
            SetData(MeterGroup, TaiType, TaiId);

            //背景颜色
            Grid_ShowMeter.BackgroundColor = Color.FromArgb(250, 250, 250);

            //单元格子背景颜色
            Grid_ShowMeter.DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;//Color.FromArgb(250, 250, 250);

            //前三列单元格背景颜色
            //Grid_ShowMeter.Columns["要检"].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            //Grid_ShowMeter.Columns["表位号"].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            //Grid_ShowMeter.Columns["条形码"].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            for (int i = 1; i < Grid_ShowMeter.Rows.Count; i += 2)
            {
                Grid_ShowMeter.Rows[i].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;//Color.FromArgb(235, 250, 235); ;//Color.FromArgb(240, 250, 240);
            }
            Grid_ShowMeter.Columns["表位号"].DefaultCellStyle.Font = new System.Drawing.Font("System", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            Grid_ShowMeter.Columns["表位号"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid_ShowMeter.Columns["表位号"].DefaultCellStyle.ForeColor = Color.FromArgb(80, 80, 80);

            //边框
            Grid_ShowMeter.BorderStyle = BorderStyle.None;

            //不能多行选中
            Grid_ShowMeter.MultiSelect = false;
            //事件处理
            Grid_ShowMeter.Resize += new EventHandler(Grid_ShowMeter_Resize);
            Grid_ShowMeter_Resize(Grid_ShowMeter, new EventArgs());
            //Grid_ShowMeter.SelectionChanged += new EventHandler(Grid_ShowMeter_SelectionChanged);
            Grid_ShowMeter.CellValueChanged += new DataGridViewCellEventHandler(Grid_ShowMeter_CellValueChanged);

            //关闭排序
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
        /// 绑定数据源
        /// </summary>
        /// <param name="KeyName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        private DataTable BindComboBoxDataSource(string KeyName, string Value)
        {
            DataTable dtDpl = new DataTable();
            dtDpl.Columns.Add("值", typeof(string));
            dtDpl.Columns.Add("名称", typeof(string));

            if (KeyName != "通讯协议" && KeyName != "载波协议")
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
                CLDC_DataCore.Function.DoDataTable.Sort(ref dtDpl, "名称", true);
            }

            //添加正则到下拉数据中
            switch (KeyName)
            {
                case "表类型":
                    dtDpl.Rows.Add(string.Format(@"{0}|.+", KeyName), "===新增===");
                    break;
                case "常数":
                    dtDpl.Rows.Add(string.Format(@"{0}|(\d+\(\d+\))|(\d+)", KeyName), "===新增===");
                    break;
                case "等级":
                    dtDpl.Rows.Add(string.Format(@"{0}|(\d+(\.\d+)?s?\(\d+(\.\d+)?s?\))|(\d+(\.\d+)?s?)", KeyName), "===新增===");
                    break;
                case "表型号":
                    dtDpl.Rows.Add(string.Format(@"{0}|[\d\w\-_]+", KeyName), "===新增===");
                    break;
                case "制造厂家":
                    dtDpl.Rows.Add(string.Format(@"{0}|.+", KeyName), "===新增===");
                    break;
                case "通讯协议":
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
                case "载波协议":
                    //-----------TODO------------
                    List<string> lst_CarrierNames = CLDC_DataCore.Model.CarrierProtocol.CarrierProtocolInfo.GetProtocolNameList();

                    foreach (string name in lst_CarrierNames)
                    {
                        dtDpl.Rows.Add(name, name);
                    }

                    dtDpl.Rows.Add(string.Format(@"{0}|.+", KeyName));

                    break;
                case "送检单位":
                    dtDpl.Rows.Add(string.Format(@"{0}|.+", KeyName), "===新增===");
                    break;
                case "检定规程":                    
                    dtDpl.Rows.Add("JJG307-2003", "JJG307-2003");
                    dtDpl.Rows.Add("JJG596-1999", "JJG596-1999");
                    dtDpl.Rows.Add("JJG596-2012", "JJG596-2012");
                    dtDpl.Rows.Add(string.Format(@"{0}|.+", KeyName), " ");
                    break;
                case "脉冲":
                    dtDpl.Rows.Add("共阴", "共阴");
                    dtDpl.Rows.Add("共阳", "共阳");
                    dtDpl.Rows.Add(string.Format(@"{0}|.+", KeyName), " ");
                    break;
                default:
                    dtDpl.Rows.Add(string.Format("{0}|.+", KeyName), "===新增===");
                    break;
            }
            return dtDpl;

        }


        /// <summary>
        /// 设置总方案下拉列表
        /// </summary>
        private void SetFaList()
        {
            List<string> TmpList = new List<string>();

            TmpList = CLDC_DataCore.Model.Plan.Model_Plan.getFileNames(CLDC_DataCore.Const.Variable.CONST_FA_GROUP_FOLDERNAME, base.TaiType);

            TmpList.Insert(0, "");          //插入一个空行

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

        private bool bDoSelectIndexChanged = false; //当前是否正在处理 SelectIndexChanged事件

        private void InputPara_V80Style_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bDoSelectIndexChanged == true) return;

            bDoSelectIndexChanged = true;

            ComboBox CmbBox = (ComboBox)sender;
            if (CmbBox.SelectedIndex < 0) goto Exit;
            string StrValue = ((DataRowView)CmbBox.SelectedItem).Row["值"].ToString();
            string StrColName;
            string StrRegFilter;
            string StrText;

            //选中了其中一个有效值 、非 [新增],包括空值
            if (StrValue.IndexOf("|") == -1 /*|| StrValue.Substring(StrValue.IndexOf("|")) == "|.+"*/)
            {
                //选择行下标
                int RowIndex = Grid_ShowMeter.SelectedCells[0].RowIndex;

                //Grid列名 、在下拉的最后一项目中选择列名、
                StrColName = ((DataRowView)CmbBox.Items[CmbBox.Items.Count - 1]).Row["值"].ToString();

                if (StrColName == string.Empty)     //如果为空，则是通讯协议
                {
                    StrColName = "通讯协议";
                }
                else if (StrColName.IndexOf("|") < 0)
                {
                    StrColName = "载波协议";
                }
                else
                {
                    StrColName = StrColName.Substring(0, StrColName.IndexOf("|"));
                }

                for (int i = RowIndex; i < Grid_ShowMeter.Rows.Count; i++)
                {
                    SetGridViewComboCellValue(StrColName, Grid_ShowMeter.Rows[i].Cells[StrColName], StrValue);
                }

                ////自动选择协议 [所有行都处理]
                //if (StrColName == "制造厂家" || StrColName == "表型号")
                //{
                //    if (MeterGroup.AutoProtocol)         //如果是自动识别
                //    {
                //        object FactoryName = null; //表厂家
                //        object MeterMode = null;//表型号
                //        string ProtocolName = string.Empty;//协议名称

                //        for (int i = 0; i < Grid_ShowMeter.Rows.Count; i++)
                //        {
                //            FactoryName = Grid_ShowMeter.Rows[i].Cells["制造厂家"].Value;
                //            MeterMode = Grid_ShowMeter.Rows[i].Cells["表型号"].Value;
                //            if (FactoryName == null || FactoryName.ToString() == "" || MeterMode == null || MeterMode.ToString() == "")
                //            {
                //                ProtocolName = string.Empty;
                //            }
                //            else
                //            {
                //                ProtocolName = CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo.getProtocolName(FactoryName.ToString(), MeterMode.ToString());
                //            }

                //            SetGridViewComboCellValue("通信协议", Grid_ShowMeter.Rows[i].Cells["通讯协议"], ProtocolName.ToString());
                //        }
                //    }
                //}
                goto Exit;
            }
            StrColName = StrValue.Substring(0, StrValue.IndexOf("|"));
            StrRegFilter = StrValue.Substring(StrColName.Length);
            StrText = ((DataRowView)CmbBox.SelectedItem).Row["名称"].ToString();

            //选中 [新增] 项
            if (StrText == "===新增===")
            {
                //列名为"脉冲"时，不允许增加
                if (StrColName == "脉冲") return;
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
                else    //新增成功后刷新  
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
                //选择行下标
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
            int Bwh = int.Parse(Row.Cells["表位号"].Value.ToString().Replace("第", "").Replace("表位", "").Trim());
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = ((CLDC_DataCore.Model.DnbModel.DnbGroupInfo)MeterGroup).GetMeterBasicInfoByBwh(Bwh);
            MeterInfo.YaoJianYn = (bool)Row.Cells["要检"].Value;
            MeterInfo.YaoJianYnSave = (bool)Row.Cells["要检"].Value;
            //if (MeterInfo.YaoJianYn)            //如果要检才加入数据
            //{
            //=Row["表位号"] = "第" + i.ToString().PadLeft(2, '0') + "表位";
            MeterInfo.Mb_ChrTxm = Row.Cells["条形码"].Value == null ? "" : Row.Cells["条形码"].Value.ToString();
            MeterInfo.Mb_ChrJlbh = Row.Cells["资产编号"].Value == null ? "" : Row.Cells["资产编号"].Value.ToString();

            MeterInfo.GuiChengName = Row.Cells["检定规程"].Value == null ? "" : Row.Cells["检定规程"].Value.ToString();
            MeterInfo.AVR_PROTOCOL_NAME = Row.Cells["通讯协议"].Value == null ? "" : Row.Cells["通讯协议"].Value.ToString();
            MeterInfo.AVR_CARR_PROTC_NAME = Row.Cells["载波协议"].Value == null ? "" : Row.Cells["载波协议"].Value.ToString();
            MeterInfo.Mb_chrAddr = Row.Cells["通讯地址"].Value == null ? "" : Row.Cells["通讯地址"].Value.ToString();
            MeterInfo.Mb_chrBlx = Row.Cells["表类型"].Value == null ? "" : Row.Cells["表类型"].Value.ToString();
            MeterInfo.Mb_chrBcs = Row.Cells["常数"].Value == null ? "" : Row.Cells["常数"].Value.ToString();
            MeterInfo.Mb_chrBdj = Row.Cells["等级"].Value == null ? "" : Row.Cells["等级"].Value.ToString();
            MeterInfo.Mb_chrzzcj = Row.Cells["制造厂家"].Value == null ? "" : Row.Cells["制造厂家"].Value.ToString();
            MeterInfo.Mb_Bxh = Row.Cells["表型号"].Value == null ? "" : Row.Cells["表型号"].Value.ToString();
            MeterInfo.Mb_chrSjdw = Row.Cells["送检单位"].Value == null ? "" : Row.Cells["送检单位"].Value.ToString();

            MeterInfo.AVR_TASK_NO = Row.Cells["任务编号"].Value == null ? "" : Row.Cells["任务编号"].Value.ToString();
            MeterInfo.AVR_WORK_NO = Row.Cells["工单号"].Value == null ? "" : Row.Cells["工单号"].Value.ToString();
            //MeterInfo.Mb_ChrJlbh = Row.Cells["计量编号"].Value == null ? "" : Row.Cells["计量编号"].Value.ToString();
            MeterInfo.Mb_ChrCcbh = Row.Cells["出厂编号"].Value == null ? "" : Row.Cells["出厂编号"].Value.ToString();
            MeterInfo.Mb_chrCcrq = Row.Cells["出厂日期"].Value == null ? "" : Row.Cells["出厂日期"].Value.ToString();
            MeterInfo.Mb_chrZsbh = Row.Cells["证书编号"].Value == null ? "" : Row.Cells["证书编号"].Value.ToString();
            MeterInfo.Mb_ChrBmc = Row.Cells["表名称"].Value == null ? "" : Row.Cells["表名称"].Value.ToString();
            MeterInfo.Mb_chrQianFeng1 = Row.Cells[getColName("铅封1").ColShowName].Value == null ? "" : Row.Cells[getColName("铅封1").ColShowName].Value.ToString();
            MeterInfo.Mb_chrQianFeng2 = Row.Cells[getColName("铅封2").ColShowName].Value == null ? "" : Row.Cells[getColName("铅封2").ColShowName].Value.ToString();
            MeterInfo.Mb_chrQianFeng3 = Row.Cells[getColName("铅封3").ColShowName].Value == null ? "" : Row.Cells[getColName("铅封3").ColShowName].Value.ToString();
            MeterInfo.AVR_SEAL_4 = Row.Cells[getColName("铅封4").ColShowName].Value == null ? "" : Row.Cells[getColName("铅封4").ColShowName].Value.ToString();
            MeterInfo.AVR_SEAL_5 = Row.Cells[getColName("铅封5").ColShowName].Value == null ? "" : Row.Cells[getColName("铅封5").ColShowName].Value.ToString();
            MeterInfo.Mb_chrSoftVer = Row.Cells["软件版本号"].Value == null ? "" : Row.Cells["软件版本号"].Value.ToString();
            MeterInfo.Mb_chrHardVer = Row.Cells["硬件版本号"].Value == null ? "" : Row.Cells["硬件版本号"].Value.ToString();
            MeterInfo.Mb_chrArriveBatchNo = Row.Cells["到货批次号"].Value == null ? "" : Row.Cells["到货批次号"].Value.ToString();
            MeterInfo.Mb_chrOther1 = Row.Cells["备用1"].Value == null ? "" : Row.Cells["备用1"].Value.ToString();
            MeterInfo.Mb_chrOther2 = Row.Cells["备用2"].Value == null ? "" : Row.Cells["备用2"].Value.ToString();
            MeterInfo.Mb_chrOther3 = Row.Cells["备用3"].Value == null ? "" : Row.Cells["备用3"].Value.ToString();
            MeterInfo.Mb_chrOther4 = Row.Cells["备用4"].Value == null ? "" : Row.Cells["备用4"].Value.ToString();
            MeterInfo.Mb_chrOther5 = Row.Cells["备用5"].Value == null ? "" : Row.Cells["备用5"].Value.ToString();

            MeterInfo._intTaiNo = this.TaiId.ToString();
            try
            {
                MeterInfo.Mb_gygy = (CLDC_Comm.Enum.Cus_GyGyType)(Enum.Parse(typeof(CLDC_Comm.Enum.Cus_GyGyType), Row.Cells["脉冲"].Value.ToString()));
            }
            catch
            {
                MeterInfo.Mb_gygy = Cus_GyGyType.共阴;
            }
            int _Const = CLDC_DataCore.Function.Number.GetBcs(MeterInfo.Mb_chrBcs, true);      //有功

            if (_Const != 1 && (MeterGroup.MinConst[0] == 0 || MeterGroup.MinConst[0] > _Const)) MeterGroup.MinConst[0] = _Const;

            _Const = CLDC_DataCore.Function.Number.GetBcs(MeterInfo.Mb_chrBcs, false);     //无功

            if (_Const != 1 && (MeterGroup.MinConst[1] == 0 || MeterGroup.MinConst[1] > _Const)) MeterGroup.MinConst[1] = _Const;

            ////设置测量方式
            //if (Cmb_CeLiangFangShi.SelectedIndex != -1)
            //{
            //    MeterInfo.Mb_intClfs = int.Parse(CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_CeLiangFangShi));
            //}

            ////互感器
            //MeterInfo.Mb_BlnHgq = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_HuGanQi) == "经互感器";

            ////止逆器
            //MeterInfo.Mb_BlnZnq = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_ZhiNiQi) == "有止逆";

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

        #region Grid_ShowMeter 的事件

        #region Resize事件
        /// <summary>
        /// 计算 Grid_ShowMeter 各列宽度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Grid_ShowMeter_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < Grid_ShowMeter.Columns.Count; i++)
            {
                switch (Grid_ShowMeter.Columns[i].HeaderText)
                {
                    case "要检":
                        Grid_ShowMeter.Columns[i].Width = 40;
                        break;
                    case "表位号":
                        Grid_ShowMeter.Columns["表位号"].Width = 47;
                        break;
                    case "条形码":
                        Grid_ShowMeter.Columns["条形码"].Width = 120;
                        break;
                    case "资产编号":
                        Grid_ShowMeter.Columns["资产编号"].Width = 120;
                        break;
                    case "出厂编号":
                        Grid_ShowMeter.Columns["出厂编号"].Width = 80;
                        break;
                    case "送检单位":
                        Grid_ShowMeter.Columns["送检单位"].Width = 120;
                        break;
                    case "通讯地址":
                        Grid_ShowMeter.Columns["通讯地址"].Width = 90;
                        break;
                    case "表类型":
                        Grid_ShowMeter.Columns["表类型"].Width = 75;
                        break;
                    case "常数":
                        Grid_ShowMeter.Columns["常数"].Width = 100;
                        break;
                    case "等级":
                        Grid_ShowMeter.Columns["等级"].Width = 70;
                        break;
                    case "表型号":
                        Grid_ShowMeter.Columns["表型号"].Width = 100;
                        break;
                    case "检定规程":
                        Grid_ShowMeter.Columns["检定规程"].Width = 110;
                        break;
                    case "通讯协议":
                        Grid_ShowMeter.Columns["通讯协议"].Width = 110;
                        break;
                    case "制造厂家":
                        Grid_ShowMeter.Columns["制造厂家"].Width = 120;
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

            if (ColName == "条形码" || ColName == "资产编号" || ColName == "出厂编号")
            {
                string strValue = Grid_ShowMeter.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
                for (int i = 0; i < Grid_ShowMeter.Rows.Count; i++)
                {
                    if (Grid_ShowMeter.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && Grid_ShowMeter.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim() != string.Empty
                        && e.RowIndex != i
                        && Grid_ShowMeter.Rows[i].Cells[e.ColumnIndex].Value != null
                        && Grid_ShowMeter.Rows[i].Cells[e.ColumnIndex].Value.ToString() == strValue && !_BlnLianXuID)
                    {
                        MessageBoxEx.Show(this, string.Format("{0} 重复！", ColName), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Grid_ShowMeter.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                        ThreadPool.QueueUserWorkItem(new WaitCallback(thChangGridSelectRow), new int[] { e.RowIndex, e.ColumnIndex });
                        return;
                    }
                }

                //从营销系统提取数据
                //if (ColName == "条形码" && !CLDC_DataCore.Const.GlobalUnit.ReadingPara && Chk_TiaoMaJX.Checked)
                //{
                    //strSubBarCode = strValue.Substring(strValue.Length - 4, 4);
                    //strValue = strValue.Substring(0, strValue.Length - 1);

                    //GlobalUnit.g_MsgControl.OutMessage(strSubBarCode, false, Cus_MessageType.语音消息);
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

                if (this.Grid_ShowMeter.IsCurrentCellInEditMode)                //如果表格在编辑状态
                {
                    int _ColIndex = this.Grid_ShowMeter.CurrentCell.ColumnIndex;

                    int _RowIndex = this.Grid_ShowMeter.CurrentCell.RowIndex;
                    ///只输入条形码或者出厂编号都可以解析
                    if ((_ColIndex == 2 || _ColIndex == 4) && _RowIndex >= 0)          //条码输入回车
                    {
                        Grid_ShowMeter.EndEdit();

                        if (this.Grid_ShowMeter[_ColIndex, _RowIndex].Value == null || this.Grid_ShowMeter[_ColIndex, _RowIndex].Value.ToString() == string.Empty)   //如果为空
                        {
                            Grid_ShowMeter.BeginEdit(true);
                            return true;    //退出不换行
                        }
                        //string key = Grid_ShowMeter[_ColIndex, _RowIndex].Value.ToString();
                        //if (_ColIndex == 2)     //条码号
                        //{
                        //    if (key != null && key.Trim() != "")
                        //    {
                        //        string strSubBarCode;


                        //        //for (int i = 4; i > 0; i--)
                        //        //{
                        //        //    strSubBarCode = key.Substring(key.Length - i, 1);
                        //        //    GlobalUnit.g_MsgControl.OutMessage(strSubBarCode, false, Cus_MessageType.语音消息);
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

                        ///获取MeterBasicInfo实例
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
                //处理 Ctrl+C组合键，当DataGridview的选择模式为整行选择时，Ctrl+C复制的是整行的数据。
                if (Grid_ShowMeter.CurrentCell.Value != null && Grid_ShowMeter.CurrentCell.Value.ToString().Length > 0)
                    Clipboard.SetText(Grid_ShowMeter.CurrentCell.Value.ToString());
                return true;
            }
            //默认交由系统处理
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        #region bool IsInputComplated()  检查输入是否完整
        /// <summary>
        /// 检查输入是否完整
        /// </summary>
        /// <returns></returns>
        private bool IsInputComplated()
        {
            MessageBoxEx.UseSystemLocalizedString = true;
            //检查是否输入完整
            if (Cmb_DianYa.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请选择电压!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_DianYa.Focus();
                return false;
            }

            if (Cmb_DianLiu_Ib.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请选择电流!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_DianLiu_Ib.Focus();
                return false;
            }

            if (Cmb_PinLv.Value < 1)
            {
                MessageBoxEx.Show(this, "请设置频率!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_PinLv.Focus();
                return false;
            }

            if (Cmb_ShouJianZhouJian.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请设置首检周检!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_ShouJianZhouJian.Focus();
                return false;
            }

            if (Cmb_CeLiangFangShi.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请设置测量方式!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_CeLiangFangShi.Focus();
                return false;
            }

            if (Cmb_HuGanQi.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请设置互感器!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_HuGanQi.Focus();
                return false;
            }

            if (Cmb_ZhiNiQi.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请设置止逆器!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_ZhiNiQi.Focus();
                return false;
            }

            if (Cmb_JianCeLeiXing.SelectedIndex == -1)
            {
                MessageBoxEx.Show(this, "请设置检测类型!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_JianCeLeiXing.Focus();
                return false;
            }


            if (Cmb_FA.Text == "")
            {
                MessageBox.Show(this, "请选择需要使用的检定方案...", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cmb_FA.Focus();
                return false;
            }


            return true;
        }
        #endregion

        #region Btn_DoComplated_Click(object sender, EventArgs e)
        /// <summary>
        /// 检测是否完整，同时保存电流，电压等检定环境数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_DoComplated_Click(object sender, EventArgs e)
        {
            //设置检定开始时间
            CLDC_DataCore.Const.GlobalUnit.g_CheckTimeStartSum = DateTime.Now;

            MessageBoxEx.UseSystemLocalizedString = true;
            //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetCurFunctionOnOrOff(true);
            MeterGroup.MinConst[0] = 0;
            MeterGroup.MinConst[1] = 0; 
            SetSaveToModel();

            //输入是否完整
            if (!IsInputComplated()) return;



            DateTime _DatJdrq = DateTime.Now;

            //设置选定值
            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                #region
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = MeterGroup.MeterGroup[i];
                //表位号
                MeterInfo._intBno = i + 1;
                //电压
                MeterInfo.Mb_chrUb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianYa);

                //电流
                MeterInfo.Mb_chrIb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Ib) + "(" + CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Imax) + ")";

                //频率
                MeterInfo.Mb_chrHz = Cmb_PinLv.Value.ToString();

                //检定日期
                MeterInfo.Mb_DatJdrq = _DatJdrq.ToString();

                //设置测量方式
                MeterInfo.Mb_intClfs = int.Parse(CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_CeLiangFangShi));

                //互感器
                if (GlobalUnit.IsDan == true)
                {
                    MeterInfo.Mb_BlnHgq = false;
                }
                else
                {
                    MeterInfo.Mb_BlnHgq = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_HuGanQi) == "经互感器";
                }
                //止逆器
                MeterInfo.Mb_BlnZnq = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_ZhiNiQi) == "有止逆";

                //首检周检 Other3
                MeterInfo.Mb_chrOther3 = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_ShouJianZhouJian);

                //检测类型 Cmb_JianCeLeiXing Other2
                MeterInfo.Mb_chrTestType = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_JianCeLeiXing);

                //电子式使用规程

                MeterInfo.GuiChengName_DianZi = "JJG596-2012";// CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_FKType);

                //感应式使用规程

                //MeterInfo.GuiChengName_GanYing = CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_HGQ_In);
                //费控类型
                MeterInfo.FKType = CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_FKType);

                //当前被检表选用规程
                if (MeterInfo.MeterType_DzOrGy == CLDC_Comm.Enum.Cus_MeterType_DianziOrGanYing.DianZiShi)
                {
                    MeterInfo.GuiChengName = MeterInfo.GuiChengName_DianZi;
                }
                else
                {
                    MeterInfo.GuiChengName = MeterInfo.GuiChengName_GanYing;
                }
                //检定依据
                MeterInfo.Mb_chrOther5 = MeterInfo.GuiChengName;
                #endregion

                #region 被检表多功能协议设置加载

                MeterInfo.DgnProtocol = new CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo();     //实例化一个空的协议模型，

                if (MeterInfo.YaoJianYn)     //如果是要检
                {
                    bool bFindProtocol = false;           //寻找相同协议，这样做的目的是让相同协议的表指向同一个协议地址，减少空间占用

                    //寻找协议相同的表Copy协议
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (MeterGroup.AutoProtocol)            //如果是自动识别
                        {
                            if (MeterInfo.AVR_PROTOCOL_NAME == "")           //如果当前表的协议名称为空
                            {
                                if (MeterGroup.MeterGroup[j].YaoJianYn && MeterInfo.Mb_chrzzcj == MeterGroup.MeterGroup[j].Mb_chrzzcj
                                    && MeterInfo.Mb_Bxh == MeterGroup.MeterGroup[j].Mb_Bxh)
                                {
                                    MeterInfo.DgnProtocol = MeterGroup.MeterGroup[j].DgnProtocol;               //如果相同，则指向同一个地址内存
                                    MeterInfo.AVR_PROTOCOL_NAME = MeterGroup.MeterGroup[j].AVR_PROTOCOL_NAME;
                                    bFindProtocol = true;
                                    break;
                                }
                            }
                            else      //如果不为空的话则根据协议名称来选择
                            {
                                if (MeterGroup.MeterGroup[j].YaoJianYn && MeterInfo.AVR_PROTOCOL_NAME == MeterGroup.MeterGroup[j].AVR_PROTOCOL_NAME)
                                {
                                    MeterInfo.DgnProtocol = MeterGroup.MeterGroup[j].DgnProtocol;           //指向同一个内存地址
                                    bFindProtocol = true;
                                    break;
                                }
                            }
                        }
                        else    //不是自动识别则根据参数录入的时候所选择的协议名称
                        {
                            if (MeterGroup.MeterGroup[j].YaoJianYn && MeterGroup.MeterGroup[j].AVR_PROTOCOL_NAME == MeterInfo.AVR_PROTOCOL_NAME)         //如果使用的都是相同的通信协议
                            {
                                MeterInfo.DgnProtocol = MeterGroup.MeterGroup[j].DgnProtocol;           //指向同一个内存地址
                                bFindProtocol = true;
                                break;
                            }
                        }
                    }

                    if (!bFindProtocol)          //如果找不到相同的协议
                    {
                        if (MeterGroup.AutoProtocol && MeterInfo.AVR_PROTOCOL_NAME == "")         //如果是自动识别(并且没有选择多功能协议)
                        {
                            if (MeterInfo.Mb_chrzzcj != "" && MeterInfo.Mb_Bxh != "")
                            {
                                MeterInfo.DgnProtocol.Load(MeterInfo.Mb_chrzzcj, MeterInfo.Mb_Bxh);         //加载多功能协议
                                MeterInfo.AVR_PROTOCOL_NAME = MeterInfo.DgnProtocol.ProtocolName;
                            }
                        }
                        else
                        {
                            if (MeterInfo.AVR_PROTOCOL_NAME != "")               //如果选择协议不为空则加载，如果为空的话，就不加载多功能协议
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
                    //检查数据是否完整
                    //if (MeterGroup.MeterGroup[i].Mb_ChrTxm.Length < 1)
                    //{
                    //    MessageBoxEx.Show(this,"缺少条形码", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    Grid_ShowMeter.Rows[MeterGroup.MeterGroup[i].Mb_intBno - 1].Selected = true;
                    //    return;
                    //}                    
                    if (MeterGroup.MeterGroup[i].Mb_chrBlx.Length < 1)
                    {
                        MessageBoxEx.Show(this, "缺少表类型", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Grid_ShowMeter.Rows[MeterGroup.MeterGroup[i].Mb_intBno - 1].Selected = true;
                        return;
                    }
                    if (MeterGroup.MeterGroup[i].Mb_chrBcs.Length < 1)
                    {
                        MessageBoxEx.Show(this, "缺少常数", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Grid_ShowMeter.Rows[MeterGroup.MeterGroup[i].Mb_intBno - 1].Selected = true;
                        return;
                    }
                    if (MeterGroup.MeterGroup[i].Mb_chrBdj.Length < 1)
                    {
                        MessageBoxEx.Show(this, "缺少等级", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Grid_ShowMeter.Rows[MeterGroup.MeterGroup[i].Mb_intBno - 1].Selected = true;
                        return;
                    }
                    if (MeterGroup.MeterGroup[i].GuiChengName.Length < 1)
                    {
                        MessageBoxEx.Show(this, "缺少检定规程", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Grid_ShowMeter.Rows[MeterGroup.MeterGroup[i].Mb_intBno - 1].Selected = true;
                        return;
                    }
                    bYaoJianMeter = true;
                }
                #endregion
            }
            if (!bYaoJianMeter)
            {
                MessageBoxEx.Show(this, "没有录入任何表信息!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int fyjb = MeterGroup.GetFirstYaoJianMeterBwh();
            string Escn = (MeterGroup.MeterGroup[fyjb].DgnProtocol.HaveProgrammingkey == false) ? "13" : "09";
            CLDC_DataCore.Function.File.WriteInIString(CLDC_DataCore.Const.Variable.CONST_ENCRYPTION_INI, CLDC_DataCore.Const.Variable.CONST_ENCRYPTION_SECTION, CLDC_DataCore.Const.Variable.CONST_ENCRYPTION_NEWOLD, Escn);

            #region//过压、过流保护
            float MaxDianYa = float.Parse(CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianYa).ToLower());
            float MaxDianLiu = float.Parse(CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Imax));
            if (CLDC_Comm.LoginSettingData.LoginSetting != null)
            {
                if (MaxDianYa > CLDC_Comm.LoginSettingData.LoginSetting.MaxDianYa)
                {
                    MessageBoxEx.Show(this, "超过最大允许电压!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cmb_DianYa.Focus();
                    return;
                }

                if (MaxDianLiu > CLDC_Comm.LoginSettingData.LoginSetting.MaxDianLiu)
                {
                    MessageBoxEx.Show(this, "超过最大允许电流!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cmb_DianLiu_Ib.Focus();
                    return;
                }
            }
            #endregion

            #region 创建方案(或继续检定)

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
                    //if (MessageBoxEx.Show(this, "该提交操作将会清理检定数据，请问是否继续提交？选择否则取消当前提交操作。", "清理询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在清理检定数据...");

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
                MessageBoxEx.Show(this, "方案创建失败，请检查是否正确的选择了方案或所选择的检定方案内容为空...", "创建失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (GlobalUnit.DispatcherType != 1 && MessageBox.Show(string.Format(@"请确认以下数据:{0}

1、测量方式： {4} 
2、电压    ： {1}V
3、电流    ： {2}A
4、频率    ： {3}Hz
5、被检表数： {5}只

{6}"
 , ""
                                 , CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianYa)
                                 , CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Ib)
                                 , Cmb_PinLv.Value.ToString()
                                 , CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_CeLiangFangShi)
                                 , nYaoJianCount
                                 , "是否确定本操作?".PadRight(20, ' '))
                                , "确定提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            WritePKToModel();
            CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.Control("就位", false);

            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在提交被检表参数...");
            CLDC_DataCore.Const.GlobalUnit.g_CUS.SaveTempDB();//临时库
            if (ParentMain.Evt_InputParam_OnOk == null)
            {
                CLDC_DataCore.Function.TopWaiting.HideWaiting();
                MessageBoxEx.Show(this, "录入参数完成按钮事件没有被调用!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                ParentMain.Evt_InputParam_OnOk(MeterGroup, TaiType, TaiId);

                GlobalUnit.g_RealTimeDataControl.OutUpdateRealTimeData("", Cus_MeterDataType.条形码信息数据);
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
        /// 从临时数据库读取PK到Model
        /// </summary>
        private void WritePKToModel()
        {
            string sql = string.Empty;
            int _TaiID = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._TaiID;
            CLDC_DataCore.DataBase.DataControl _Data;
            _Data = new CLDC_DataCore.DataBase.DataControl(false);        //构造连接本地默认ACCESS数据库
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

        //换新表按钮
        private void Btn_ClearMeter_Click(object sender, EventArgs e)
        {
            if (CLDC_DataCore.Const.StdMeterConst.m_LastSearchU > 36.0f || CLDC_DataCore.Const.StdMeterConst.m_LastSearchI > 0.1)
            {
                if (CLDC_DataCore.Const.GlobalUnit.IsDemo == false)
                {
                    MessageBoxEx.Show(this, "请先关掉源");
                }
            }


            MessageBoxEx.UseSystemLocalizedString = true;
            if (MessageBoxEx.Show(this, "真的要换新表吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            if (ParentMain.Evt_OnHangUpNewMeter != null)
            {
                //Comm.Function.TopWaiting.ShowWaiting("正在执行挂新表操作...");
                if (!ParentMain.Evt_OnHangUpNewMeter(TaiType, TaiId))
                {
                    CLDC_DataCore.Function.TopWaiting.HideWaiting();
                    MessageBoxEx.Show(this, "操作失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //将焦点置于第一行条码号      
                Grid_ShowMeter.Focus();
                Grid_ShowMeter.Rows[0].Cells[2].Selected = true;
            }

            CLDC_VerifyAdapter.Helper.MotorSafeControl.Instance.Control("挂表位", false);

        }

        //自动生成编号
        private void Btn_BuildData_Click(object sender, EventArgs e)
        {
            MessageBoxEx.UseSystemLocalizedString = true;



            if (Cell_LastSelected == null)
            {
                MessageBoxEx.Show(this, "请先选中一个需要生成数据的单元格!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int RowIndex = Cell_LastSelected.RowIndex;
            int ColIndex = Cell_LastSelected.ColumnIndex;
            string ColName = Grid_ShowMeter.Columns[ColIndex].HeaderText;
            if (ColName == "条形码" || ColName == "资产编号" || ColName == "出厂编号" || ColName == "通讯地址" || ColName == "计量编号"
                || ColName == "送检单位" || ColName == getColName("铅封1").ColShowName || ColName == getColName("铅封2").ColShowName || ColName == getColName("铅封3").ColShowName || ColName == getColName("铅封4").ColShowName
                || ColName == getColName("铅封5").ColShowName || ColName == "证书编号")
            {
                string strStartNumber = "";
                if (!CLDC_DataCore.Function.InputBox.Show("请输入起始数据:", ColName, ref strStartNumber))
                {
                    return;
                }
                if (ColName == "证书编号")
                {
                    string sKey = CLDC_DataCore.Const.Variable.CTC_OTHER_PREFIXOFCERTIFICATEN;
                    string sPrefix = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.SystemMode.getValue(sKey);
                    strStartNumber = sPrefix + strStartNumber;
                }
                strStartNumber = strStartNumber.Trim();
                string StartString = string.Empty; //号码半部分
                string LastNumber = string.Empty; //号码后半部分
                int LastNumberLen = 0;
                for (int i = strStartNumber.Length - 1; i >= 0; i--)
                {
                    if (LastNumber.Length >= 9)    //控制数字的大小防止溢出
                    {
                        break;
                    }

                    if ("0123456789".IndexOf(strStartNumber[i]) != -1)
                    {
                        LastNumber = strStartNumber[i] + LastNumber;
                    }
                    else
                    {//选项只提取字符串最后面第一个数字串
                        if (ColName == "证书编号") break;
                    }
                }
                StartString = strStartNumber.Substring(0, strStartNumber.Length - LastNumber.Length);
                LastNumberLen = LastNumber.Length;
                if (LastNumberLen == 0)
                {
                    LastNumber = "0";
                }
                //Begain Edit
                //修改重复项目检测方法
                List<string> lstExists = new List<string>();
                //End Edit
                for (int i = RowIndex, j = 0; i < Grid_ShowMeter.Rows.Count; i++, j++)
                {
                    string strValue = "";
                    //连续编号
                    if (strStartNumber.Length > 0 && Chk_LianXuData.Checked)
                    {

                        strValue = string.Format("{0}{1}", StartString, (long.Parse(LastNumber) + j).ToString().PadLeft(LastNumberLen, '0'));
                        _BlnLianXuID = true;
                    }
                    //全部相同
                    else if (strStartNumber.Length > 0)
                    {
                        strValue = strStartNumber;
                    }
                    //如果是 条形码|资产编号|出厂编号 同时勾中了连续编号、则只输入第一行
                    if ((ColName == "条形码" || ColName == "资产编号" || ColName == "出厂编号") && strValue.Length > 0 && !Chk_LianXuData.Checked && i == RowIndex)
                    {
                        //for (int k = 0; k < Grid_ShowMeter.Rows.Count; k++)
                        //{
                        //  if (Grid_ShowMeter.Rows[k].Cells[ColIndex].Value != null
                        //     && Grid_ShowMeter.Rows[k].Cells[ColIndex].Value.ToString() == strValue)
                        if (lstExists.Contains(strValue) && strValue != string.Empty)
                        {
                            MessageBoxEx.Show(this, string.Format("{0} 重复!", ColName), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ThreadPool.QueueUserWorkItem(new WaitCallback(thChangGridSelectRow), new int[] { RowIndex, ColIndex });
                            return;
                        }
                        else
                        {
                            lstExists.Add(strValue);
                        }
                        //}
                    }
                    else if ((ColName == "条形码" || ColName == "资产编号" || ColName == "出厂编号") && strValue.Length > 0 && !Chk_LianXuData.Checked && i > RowIndex)
                    {
                        return;
                    }
                    Grid_ShowMeter.Rows[i].Cells[ColIndex].Value = strValue;

                }

                _BlnLianXuID = false;
            }
            else
            {
                MessageBoxEx.Show(this, "当前选中的列不能自动生成数据!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Grid_ShowMeter_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        /// <summary>
        /// 查看方案详细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_FA_Click(object sender, EventArgs e)
        {
            CLDC_MeterUI.UI_FA.Frm_FaSetup FaGroup = new CLDC_MeterUI.UI_FA.Frm_FaSetup((CLDC_Comm.Enum.Cus_TaiType)base.TaiType);

            string TmpString = Cmb_FA.Text;

            FaGroup.SetSelectFa(Cmb_FA.Text);
            FaGroup.ShowDialog();               //模式窗体打开

            this.SetFaList();               //重新填充方案列表

            this.Cmb_FA.Text = TmpString;
        }

        private delegate void EventInvokeSetRowNewValue(string meternum, string RowIndex);
        /// <summary>
        /// 从营销系统提取数据
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
        /// 从营销系统提取数据
        /// </summary>
        /// <param name="meterNum"></param>
        /// <param name="rowIndex"></param>
        private void SetValueFromSG186(string barcode, string index)
        {
            
        }

        /// <summary>
        /// 下载信息
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
                MessageBox.Show("下载电表信息按钮事件没有被调用!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// 下载方案
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
                MessageBox.Show("下载方案信息按钮事件没有被调用!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (CLDC_DataCore.Const.GlobalUnit.g_CheckUserLevel != "管理员")
            {
                MessageBoxEx.Show(this,"非管理员权限,权限不足！");
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
            if (GlobalUnit.DispatcherType != 1 && MessageBox.Show(string.Format(@"请确认以下数据:{0}

1、测量方式： {4} 
2、电压    ： {1}V
3、电流    ： {2}A
4、频率    ： {3}Hz
5、被检表数： {5}只

{6}"
 , ""
                                 , CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianYa)
                                 , CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Ib)
                                 , Cmb_PinLv.Value.ToString()
                                 , CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_CeLiangFangShi)
                                 , nYaoJianCount
                                 , "是否确定本操作?".PadRight(20, ' '))
                                , "确定提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            DateTime _DatJdrq = DateTime.Now;
            MessageBoxEx.UseSystemLocalizedString = true;
            CLDC_DataCore.Const.GlobalUnit.ReadingPara = true;
            if (!IsInputComplated()) return;
            
            SetSaveToModel();
            //设置选定值
            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                

                #region
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = MeterGroup.MeterGroup[i];
                //表位号
                MeterInfo._intBno = 1 + 1;
                //电压
                MeterInfo.Mb_chrUb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianYa);

                //电流
                MeterInfo.Mb_chrIb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Ib) + "(" + CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Imax) + ")";

                //频率
                MeterInfo.Mb_chrHz = Cmb_PinLv.Value.ToString();

                //检定日期
                MeterInfo.Mb_DatJdrq = _DatJdrq.ToString();

                //设置测量方式
                MeterInfo.Mb_intClfs = int.Parse(CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_CeLiangFangShi));

                //互感器
                if (GlobalUnit.IsDan == true)
                {
                    MeterInfo.Mb_BlnHgq = false;
                }
                else
                {
                    MeterInfo.Mb_BlnHgq = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_HuGanQi) == "经互感器";
                }
                //止逆器
                MeterInfo.Mb_BlnZnq = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_ZhiNiQi) == "有止逆";

                //首检周检 Other3
                MeterInfo.Mb_chrOther3 = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_ShouJianZhouJian);

                //检测类型 Cmb_JianCeLeiXing Other2
                MeterInfo.Mb_chrTestType = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_JianCeLeiXing);

                //电子式使用规程

                MeterInfo.GuiChengName_DianZi = "JJG596-2012";// CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_FKType);

                //感应式使用规程

                //MeterInfo.GuiChengName_GanYing = CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_HGQ_In);
                //费控类型
                MeterInfo.FKType = CLDC_DataCore.Function.BindCombox.GetSelectItemText(Cmb_FKType);

                //当前被检表选用规程
                if (MeterInfo.MeterType_DzOrGy == CLDC_Comm.Enum.Cus_MeterType_DianziOrGanYing.DianZiShi)
                {
                    MeterInfo.GuiChengName = MeterInfo.GuiChengName_DianZi;
                }
                else
                {
                    MeterInfo.GuiChengName = MeterInfo.GuiChengName_GanYing;
                }
                //检定依据
                MeterInfo.Mb_chrOther5 = MeterInfo.GuiChengName;
                #endregion

                #region 被检表多功能协议设置加载

                MeterInfo.DgnProtocol = new CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo();     //实例化一个空的协议模型，

                //电压
                MeterInfo.Mb_chrUb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianYa);

                //电流
                //MeterInfo.Mb_chrIb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Ib);//此处电流只有一个IB值，要加上Imax的值
                MeterInfo.Mb_chrIb = CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Ib) + "(" + CLDC_DataCore.Function.BindCombox.GetSelectItemValue(Cmb_DianLiu_Imax) + ")";

                if (MeterInfo.YaoJianYn)     //如果是要检
                {
                    bool bFindProtocol = false;           //寻找相同协议，这样做的目的是让相同协议的表指向同一个协议地址，减少空间占用

                    //寻找协议相同的表Copy协议
                    for (int j = i - 1; j >= 0; j--)
                    {
                        if (MeterGroup.AutoProtocol)            //如果是自动识别
                        {
                            if (MeterInfo.AVR_PROTOCOL_NAME == "")           //如果当前表的协议名称为空
                            {
                                if (MeterGroup.MeterGroup[j].YaoJianYn && MeterInfo.Mb_chrzzcj == MeterGroup.MeterGroup[j].Mb_chrzzcj
                                    && MeterInfo.Mb_Bxh == MeterGroup.MeterGroup[j].Mb_Bxh)
                                {
                                    MeterInfo.DgnProtocol = MeterGroup.MeterGroup[j].DgnProtocol;               //如果相同，则指向同一个地址内存
                                    MeterInfo.AVR_PROTOCOL_NAME = MeterGroup.MeterGroup[j].AVR_PROTOCOL_NAME;
                                    bFindProtocol = true;
                                    break;
                                }
                            }
                            else      //如果不为空的话则根据协议名称来选择
                            {
                                if (MeterGroup.MeterGroup[j].YaoJianYn && MeterInfo.AVR_PROTOCOL_NAME == MeterGroup.MeterGroup[j].AVR_PROTOCOL_NAME)
                                {
                                    MeterInfo.DgnProtocol = MeterGroup.MeterGroup[j].DgnProtocol;           //指向同一个内存地址
                                    bFindProtocol = true;
                                    break;
                                }
                            }
                        }
                        else    //不是自动识别则根据参数录入的时候所选择的协议名称
                        {
                            if (MeterGroup.MeterGroup[j].YaoJianYn && MeterGroup.MeterGroup[j].AVR_PROTOCOL_NAME == MeterInfo.AVR_PROTOCOL_NAME)         //如果使用的都是相同的通信协议
                            {
                                MeterInfo.DgnProtocol = MeterGroup.MeterGroup[j].DgnProtocol;           //指向同一个内存地址
                                bFindProtocol = true;
                                break;
                            }
                        }
                    }

                    if (!bFindProtocol)          //如果找不到相同的协议
                    {
                        if (MeterGroup.AutoProtocol && MeterInfo.AVR_PROTOCOL_NAME == "")         //如果是自动识别(并且没有选择多功能协议)
                        {
                            if (MeterInfo.Mb_chrzzcj != "" && MeterInfo.Mb_Bxh != "")
                            {
                                MeterInfo.DgnProtocol.Load(MeterInfo.Mb_chrzzcj, MeterInfo.Mb_Bxh);         //加载多功能协议
                                MeterInfo.AVR_PROTOCOL_NAME = MeterInfo.DgnProtocol.ProtocolName;
                            }
                        }
                        else
                        {
                            if (MeterInfo.AVR_PROTOCOL_NAME != "")               //如果选择协议不为空则加载，如果为空的话，就不加载多功能协议
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
                MessageBoxEx.Show(this, "读取参数按钮事件没有被调用!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// 获取参数录入要显示的列名
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        private CLDC_DataCore.Struct.StColsVisiable getColName(string strName)
        {
            return GlobalUnit.g_SystemConfig.ColsVisiable.getColPrj(strName);
        }
    }
}
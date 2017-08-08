using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using System.Threading;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckErrAccord : UserControl
    {
        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup = null;

        /// <summary>
        /// 选中行变更的委托
        /// </summary>
        /// <param name="RowIndex">当前选中行号</param>
        public delegate void Evt_GridSelectRowIndexChanged(int RowIndex);
        /// <summary>
        /// 选中行变更后触发的事件
        /// </summary>
        public event Evt_GridSelectRowIndexChanged GridSelectRowIndexChanged;
       
        private Main ParentMain;

        public CheckErrAccord()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数，当前检定项目ID
        /// </summary>
        /// <param name="parent">Main窗体</param>
        /// <param name="meterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        /// <param name="TaiID">台体编号</param>
        /// <param name="taiType">台体类型</param>
        public CheckErrAccord(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
        {
            InitializeComponent();

            ParentMain = parent;

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
                        
            this.InitializationGrid(meterGroup);

            this.RefreshGrid(meterGroup, CheckOrderID);

        }
        
        /// <summary>
        /// 初始化数据菜单
        /// </summary>
        /// <param name="MeterGroup"></param>
        private void InitializationGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            int _Count = MeterGroup.MeterGroup.Count;

            if (dgv_ErrAccord.Rows.Count != _Count)
            {
                dgv_ErrAccord.Rows.Clear();
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)          //根据电能台表位数添加表位行
                {
                    int _RowIndex = dgv_ErrAccord.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                    {
                        dgv_ErrAccord.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        dgv_ErrAccord.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    dgv_ErrAccord.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    dgv_ErrAccord.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
                dgv_ErrAccord.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv_ErrAccord.Refresh();
            }
        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            //string strMessage = "";
            _DnbGroup = MeterGroup;
            //_DnbGroup.NowMinute ==""
            int FirstYJMeter = Main.GetFirstYaoJianMeterIndex(MeterGroup);
            if (CheckOrderID >= MeterGroup.CheckPlan.Count
                || !(MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StErrAccord))
            {
                return;
            }

            //当前检定方案项
            CLDC_DataCore.Struct.StErrAccord _Item = (CLDC_DataCore.Struct.StErrAccord)MeterGroup.CheckPlan[CheckOrderID];

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = dgv_ErrAccord.Rows[i];
                //表位号
                _Row.Cells[1].Value = _MeterInfo.ToString();            //插入表位号

                if (!_MeterInfo.YaoJianYn)           //如果不检
                {
                    _Row.Cells[0].Value = false;
                    //如果不检，并且常数或者等级都为空，则将勾选单元格设置为只读
                    if (_MeterInfo.Mb_chrBcs == String.Empty || _MeterInfo.Mb_chrBdj == String.Empty)       
                    {
                        _Row.Cells[0].ReadOnly = true;
                    }
                    for (int j = 2; j < _Row.Cells.Count; j++)
                    {
                        _Row.Cells[j].Value = string.Empty;
                    }
                    continue;
                }

                _Row.Cells[0].Value = true;

                _Row.Cells[2].Value = _Item.ToString();

                if (dgv_ErrAccord.Tag == null) return;

                if (_MeterInfo.MeterErrAccords == null)
                    _MeterInfo.MeterErrAccords = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrAccord>();
                //如果数据中存在值那么就需要插入数据，这个地方插的值都是合格或者不合格，因为取的都是大编号，不是带具体值的小编号
                if (_MeterInfo.MeterErrAccords.ContainsKey(dgv_ErrAccord.Tag.ToString()))           
                {
                    _Row.Cells[4].Value = _MeterInfo.MeterErrAccords[dgv_ErrAccord.Tag.ToString()].Mea_Result;
                    if ((MeterGroup.CheckState & CLDC_Comm.Enum.Cus_CheckStaute.检定) == CLDC_Comm.Enum.Cus_CheckStaute.检定 ||
                        (MeterGroup.CheckState & CLDC_Comm.Enum.Cus_CheckStaute.单步检定) == CLDC_Comm.Enum.Cus_CheckStaute.单步检定)
                    {
                        //switch (_Item.ErrAccordType)
                        //{
                        //    case 1:
                        //        strMessage = "误差一致性项目检定中...";                                
                        //        break;
                        //    case 2:
                        //        strMessage = "误差变差项目检定中...";   
                        //        break;
                        //    case 3:
                        //        strMessage = "负载电流升降变差项目检定中...";   
                        //        break;
                        //    case 4:
                        //        strMessage = "电流过载试验项目检定中...";   
                        //        break;
                        //}
                        //更新检测进度
                        string CurProgrocssValue = _MeterInfo.MeterErrAccords[dgv_ErrAccord.Tag.ToString()].ProgressValue;

                        _Row.Cells[3].Value = CurProgrocssValue;
                        _Row.Cells[4].Value =  _MeterInfo.MeterErrAccords[dgv_ErrAccord.Tag.ToString()].Mea_Result;
                    }
                    if (_Row.Cells[4].Value != null)
                    {
                        if (_Row.Cells[4].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //不合格修改当前行背景颜色
                        {
                            _Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                            foreach (DataGridViewCell cell in _Row.Cells)
                            {
                                cell.ToolTipText = _MeterInfo.MeterErrAccords[dgv_ErrAccord.Tag.ToString()].AVR_DIS_REASON;
                            }
                        }
                        else
                        {
                            _Row.DefaultCellStyle.ForeColor = Color.Black;
                            foreach (DataGridViewCell cell in _Row.Cells)
                            {
                                cell.ToolTipText = string.Empty;
                            }
                        }
                    }
                }
                else
                {
                    _Row.DefaultCellStyle.ForeColor = Color.Black;
                    _Row.Cells[3].Value = "检定完毕";

                    _Row.Cells[4].Value = CLDC_DataCore.Const.Variable.CTG_HeGe;
                }
            }

            #region -----------------------------------------数据页刷新-------------------------------------------
            if (Tab_ErrAccord.TabPages.Count != 2) return;           //如果没有附加数据页则返回

            Control _Control = Tab_ErrAccord.TabPages[1].Controls[0];

            if (_Control is CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewErrAccord)    //误差一致性   
            {
                ((CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewErrAccord)_Control).SetData(MeterGroup.MeterGroup,_Item);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewContrast)     //误差变差
            {
                ((CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewContrast)_Control).SetData(MeterGroup.MeterGroup, _Item);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewUpDown)       //负载电流升降变差
            {
                ((CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewUpDown)_Control).SetData(MeterGroup.MeterGroup, _Item);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewOver)         //电流过载
            {
                ((CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewOver)_Control).SetData(MeterGroup.MeterGroup, _Item);
                return;
            }
            #endregion
        }

        /// <summary>
        /// 刷新数据事件
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="CheckOrderID"></param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            if (!(meterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StErrAccord)) return;

            CLDC_DataCore.Struct.StErrAccord _Item = (CLDC_DataCore.Struct.StErrAccord)meterGroup.CheckPlan[CheckOrderID];

            bool bFind = false;

            if (Tab_ErrAccord.TabPages.Count > 1)            //如果Tab的页数大于1，那表示存在动态增加的数据页
            {
                if (dgv_ErrAccord.Tag != null && dgv_ErrAccord.Tag.ToString() == _Item.ErrAccordType.ToString())
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_ErrAccord.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_ErrAccord.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                dgv_ErrAccord.Tag = _Item.ErrAccordType;   //将ID值放到数据列表的Tag中，供数据刷新使用

                switch (_Item.ErrAccordType)
                {
                    case 1:
                        Tab_ErrAccord.TabPages.Add("误差一致性数据");
                        CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewErrAccord _ErrorAccord = new CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewErrAccord();
                        Tab_ErrAccord.TabPages[1].Controls.Add(_ErrorAccord);
                        _ErrorAccord.Dock = DockStyle.Fill;
                        _ErrorAccord.Margin = new System.Windows.Forms.Padding(0);
                        break;
                    case 2:
                        Tab_ErrAccord.TabPages.Add("误差变差数据");
                        CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewContrast _Contrast = new CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewContrast();
                        Tab_ErrAccord.TabPages[1].Controls.Add(_Contrast);
                        _Contrast.Dock = DockStyle.Fill;
                        _Contrast.Margin = new System.Windows.Forms.Padding(0);
                        break;
                    case 3:
                        Tab_ErrAccord.TabPages.Add("负载电流升降变差数据");
                        CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewUpDown _UpDown = new CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewUpDown();
                        Tab_ErrAccord.TabPages[1].Controls.Add(_UpDown);
                        _UpDown.Dock = DockStyle.Fill;
                        _UpDown.Margin = new System.Windows.Forms.Padding(0);
                        break;
                    case 4:
                        Tab_ErrAccord.TabPages.Add("电流过载数据");
                        CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewOver _Over = new CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewOver();
                        Tab_ErrAccord.TabPages[1].Controls.Add(_Over);
                        _Over.Dock = DockStyle.Fill;
                        _Over.Margin = new System.Windows.Forms.Padding(0);
                        break;
                }
            }

            this.RefreshGrid(meterGroup, CheckOrderID);

            dgv_ErrAccord.Enabled = true;
        }

        #region 显示基本信息窗体相关

        public delegate void Evt_SetDnbInfoViewData(int Index);

        public event Evt_SetDnbInfoViewData SetDnbInfoViewData;

        #endregion

        /// <summary>
        /// 获取或设置当前选中的行号
        /// </summary>
        public int SelectRowIndex
        {
            get
            {
                if (dgv_ErrAccord.SelectedRows.Count == 0)
                    return 0;
                else
                    return dgv_ErrAccord.SelectedRows[0].Index;
            }
            set
            {
                if (dgv_ErrAccord.IsHandleCreated)
                {
                    if (value >= 0 && dgv_ErrAccord.Rows.Count > value)
                    {
                        dgv_ErrAccord.Rows[value].Selected = true;
                        dgv_ErrAccord.CurrentCell = dgv_ErrAccord.Rows[value].Cells[1];
                    }
                }
            }
        }

        private void dgv_ErrAccord_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
    }
}

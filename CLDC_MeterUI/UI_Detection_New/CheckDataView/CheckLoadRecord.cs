using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    /// <summary>
    /// 负荷记录
    /// </summary>
    public partial class CheckLoadRecord : UserControl
    {
        /// <summary>
        /// 选中行变更的委托
        /// </summary>
        /// <param name="RowIndex">当前选中行号</param>
        public delegate void Evt_GridSelectRowIndexChanged(int RowIndex);
        /// <summary>
        /// 选中行变更后触发的事件
        /// </summary>
        public event Evt_GridSelectRowIndexChanged GridSelectRowIndexChanged;

        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup = null;



        private Main ParentMain;
        /// <summary>
        /// 台体编号
        /// </summary>
        private int _TaiID = 0;
        /// <summary>
        /// 台体类型
        /// </summary>
        private int _TaiType = 0;

        public CheckLoadRecord()
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
        public CheckLoadRecord(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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
            _DnbGroup = MeterGroup;

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            int _Count = MeterGroup.MeterGroup.Count;

            if (Data_LoadRecord.Rows.Count != _Count)
            {
                Data_LoadRecord.Rows.Clear();

                for (int i = 0; i < _Count; i++)
                {
                    int Index = Data_LoadRecord.Rows.Add();
                    if ((Index + 1) % 2 == 0)
                    {
                        Data_LoadRecord.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_LoadRecord.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_LoadRecord.Rows[Index].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_LoadRecord.Rows[Index].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
            }
            Data_LoadRecord.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Data_LoadRecord.Refresh();

        }
        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            CLDC_DataCore.Struct.StPlan_LoadRecord _Item = (CLDC_DataCore.Struct.StPlan_LoadRecord)MeterGroup.CheckPlan[CheckOrderID];

            bool bFind = false;

            if (Tab_EventLog.TabPages.Count > 1)            //如果Tab的页数大于1，那表示存在动态增加的数据页
            {
                if (Data_LoadRecord.Tag != null && Data_LoadRecord.Tag.ToString() == _Item.PrjID)
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_EventLog.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_EventLog.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                Data_LoadRecord.Tag = _Item.PrjID;          //将ID值放到数据列表的Tag中，供数据刷新使用

                switch (_Item.PrjID)
                {
                    case "001":
                        {
                            Tab_EventLog.TabPages.Add("负荷记录数据");
                            CLDC_MeterUI.UI_Detection_New.LoadRecordDataView.ViewLoadRecord _View = new CLDC_MeterUI.UI_Detection_New.LoadRecordDataView.ViewLoadRecord();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                }
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow Row = Data_LoadRecord.Rows[i];
                //表位号
                Row.Cells[1].Value = _MeterInfo.ToString();
                if (!_MeterInfo.YaoJianYn)
                {
                    Row.Cells[0].Value = false;
                    if (_MeterInfo.Mb_chrBcs == String.Empty || _MeterInfo.Mb_chrBdj == String.Empty)       //如果不检，并且常数或者等级都为空，则将勾选单元格设置为只读
                    {
                        Row.Cells[0].ReadOnly = true;
                    }
                    for (int j = 2; j < Row.Cells.Count; j++)
                    {
                        Row.Cells[j].Value = string.Empty;
                    }
                    continue;
                }

                Row.Cells[0].Value = true;

                Row.Cells[2].Value = _Item.ToString();

                if (Data_LoadRecord.Tag == null) return;

                if (_MeterInfo.MeterLoadRecords == null)
                    _MeterInfo.MeterLoadRecords = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterLoadRecord>();

                if (_MeterInfo.MeterLoadRecords.ContainsKey(Data_LoadRecord.Tag.ToString()))           //如果数据中存在值那么久需要插入数据，这个地方插的值都是合格或者不合格，因为取的都是大编号，不是带具体值的小编号
                {
                    Row.Cells[3].Value = "100%";
                    Row.Cells[4].Value = _MeterInfo.MeterLoadRecords[Data_LoadRecord.Tag.ToString()].Ml_Result;                    
                }
                else
                {
                    //_Row.DefaultCellStyle.ForeColor = Color.Black;
                    Row.Cells[4].Value = "";
                }
                if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                {
                    Row.DefaultCellStyle.ForeColor = Color.Red;
                }
                else
                {
                    Row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }

            if (Tab_EventLog.TabPages.Count != 2) return;           //如果没有附加数据页则返回

            Control _Control = Tab_EventLog.TabPages[1].Controls[0];

            if (_Control is CLDC_MeterUI.UI_Detection_New.LoadRecordDataView.ViewLoadRecord)
            {
                ((CLDC_MeterUI.UI_Detection_New.LoadRecordDataView.ViewLoadRecord)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }



        }
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            this.RefreshGrid(meterGroup, CheckOrderID);
            Data_LoadRecord.Enabled = true;

            CLDC_DataCore.Struct.StPlan_LoadRecord _Item = (CLDC_DataCore.Struct.StPlan_LoadRecord)meterGroup.CheckPlan[CheckOrderID];

            bool bFind = false;

            if (Tab_EventLog.TabPages.Count > 1)            //如果Tab的页数大于1，那表示存在动态增加的数据页
            {
                if (Data_LoadRecord.Tag != null && Data_LoadRecord.Tag.ToString() == _Item.PrjID)
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_EventLog.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_EventLog.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                Data_LoadRecord.Tag = _Item.PrjID;          //将ID值放到数据列表的Tag中，供数据刷新使用

                switch (_Item.PrjID)
                {
                    case "001":
                        {
                            Tab_EventLog.TabPages.Add("负荷记录数据");
                            CLDC_MeterUI.UI_Detection_New.LoadRecordDataView.ViewLoadRecord _View = new CLDC_MeterUI.UI_Detection_New.LoadRecordDataView.ViewLoadRecord();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                }
            }
        }
        private void Data_Insulation_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex != 0 || e.RowIndex == -1)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    try
                    {
                        this.GridSelectRowIndexChanged(e.RowIndex);
                    }
                    catch
                    { }
                }
                return;     //如果不是第一列，则退出
            }
            try
            {
                this.GridSelectRowIndexChanged(e.RowIndex);
            }
            catch { }

            if (Data_LoadRecord[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_LoadRecord.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_LoadRecord.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_LoadRecord.EndEdit();
                }
                Data_LoadRecord.Enabled = false;
                //Comm.Function.TopWaiting.ShowWaiting("正在更改...");
                ParentMain.Evt_OnYaoJianChanged(_TaiType, _TaiID, e.RowIndex, Yn);
                //Comm.Function.TopWaiting.HideWaiting();
            }
            else
            {
                MessageBoxEx.Show(this,"没有处理事件Evt_OnYaoJianChanged", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        #region 显示基本信息窗体相关

        public delegate void Evt_SetDnbInfoViewData(int Index);

        public event Evt_SetDnbInfoViewData SetDnbInfoViewData;

        private void Data_Insulation_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_LoadRecord.SelectedRows[0].Index);
            }
            catch
            {
                SetDnbInfoViewData(0);         //如果出现错误就自动选择第一个表位
            }

        }

        #endregion

        /// <summary>
        /// 获取或设置当前选中的行号
        /// </summary>
        public int SelectRowIndex
        {
            get
            {
                if (Data_LoadRecord.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_LoadRecord.SelectedRows[0].Index;
            }
            set
            {
                if (Data_LoadRecord.IsHandleCreated)
                {
                    if (value >= 0 && Data_LoadRecord.Rows.Count > value)
                    {
                        Data_LoadRecord.Rows[value].Selected = true;
                        Data_LoadRecord.CurrentCell = Data_LoadRecord.Rows[value].Cells[1];
                    }
                }
            }
        }

    }
}

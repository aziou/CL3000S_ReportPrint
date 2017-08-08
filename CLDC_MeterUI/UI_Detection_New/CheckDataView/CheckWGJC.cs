using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using CLDC_Comm.Enum;
using CLDC_DataCore.Const;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckWGJC : UserControl
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
        /// <summary>
        /// 刷新数据的委托
        /// </summary>
        /// <param name="CheckOrderID"></param>
        public delegate void Evt_RefreshRightGridView(int CheckOrderID);

        public event Evt_RefreshRightGridView RefreshRightGridView;


        private Main ParentMain;
        /// <summary>
        /// 台体编号
        /// </summary>
        private int _TaiID = 0;
        /// <summary>
        /// 台体类型
        /// </summary>
        private int _TaiType = 0;

        public CheckWGJC()
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
        public CheckWGJC(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (Data_WGJC.Rows.Count != _Count)
            {
                Data_WGJC.Rows.Clear();
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)          //根据电能台表位数添加表位行
                {
                    int _RowIndex = Data_WGJC.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                    {
                        Data_WGJC.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_WGJC.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_WGJC.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_WGJC.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
                Data_WGJC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Data_WGJC.Refresh();
            }
        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            string strKey = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.外观检查试验).ToString();
            CLDC_DataCore.Struct.StPlan_WGJC _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_WGJC)
            {
                _Item = (CLDC_DataCore.Struct.StPlan_WGJC)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = Data_WGJC.Rows[i];
                //表位号
                _Row.Cells[1].Value = _MeterInfo.ToString();            //插入表位号

                if (!_MeterInfo.YaoJianYn)           //如果不检
                {
                    _Row.Cells[0].Value = false;
                    if (_MeterInfo.Mb_chrBcs == String.Empty || _MeterInfo.Mb_chrBdj == String.Empty)       //如果不检，并且常数或者等级都为空，则将勾选单元格设置为只读
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

                if (!_MeterInfo.MeterResults.ContainsKey(strKey))            //检查是否存在外观检查试验
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpMeterResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                    _TmpMeterResult.Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;
                    _TmpMeterResult._intMyId = MeterGroup.MeterGroup[i]._intMyId;
                    _TmpMeterResult.Mr_chrRstId = strKey;
                    _TmpMeterResult.Mr_chrRstName = Cus_MeterResultPrjID.外观检查试验.ToString();
                    _MeterInfo.MeterResults.Add(strKey, _TmpMeterResult);
                }

                if (_MeterInfo.MeterResults.ContainsKey(strKey))           //如果数据中存在值那么久需要插入数据，这个地方插的值都是合格或者不合格，因为取的都是大编号，不是带具体值的小编号
                {
                    _Row.Cells[4].Value = _MeterInfo.MeterResults[strKey].Mr_chrRstValue;
                    if ((MeterGroup.CheckState & CLDC_Comm.Enum.Cus_CheckStaute.检定) == CLDC_Comm.Enum.Cus_CheckStaute.检定 ||
                        (MeterGroup.CheckState & CLDC_Comm.Enum.Cus_CheckStaute.单步检定) == CLDC_Comm.Enum.Cus_CheckStaute.单步检定)
                    {                        
                        _Row.Cells[3].Value = "";                               
                    }                    
                }
                else
                {
                    //_Row.DefaultCellStyle.ForeColor = Color.Black;
                    _Row.Cells[4].Value = "";
                }
                if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                    _Row.DefaultCellStyle.ForeColor = Color.Red;
                else
                    _Row.DefaultCellStyle.ForeColor = Color.Black;
            }
            CLDC_DataCore.Const.GlobalUnit.g_RealTimeDataControl.OutUpdateRealTimeData("100", CLDC_Comm.Enum.Cus_MeterDataType.检定结论);
        }

        /// <summary>
        /// 刷新数据事件
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="taiType"></param>
        /// <param name="taiId"></param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            if (!(meterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_WGJC)) return;

            this.RefreshGrid(meterGroup, CheckOrderID);

            Data_WGJC.Enabled = true;
        }

        private void Data_Dgn_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

            if (Data_WGJC[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_WGJC.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_WGJC.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_WGJC.EndEdit();
                }
                Data_WGJC.Enabled = false;
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

        private void Data_Dgn_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_WGJC.SelectedRows[0].Index);
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
                if (Data_WGJC.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_WGJC.SelectedRows[0].Index;
            }
            set
            {
                if (Data_WGJC.IsHandleCreated)
                {
                    if (value >= 0 && Data_WGJC.Rows.Count > value)
                    {
                        Data_WGJC.Rows[value].Selected = true;
                        Data_WGJC.CurrentCell = Data_WGJC.Rows[value].Cells[1];
                    }
                }
            }
        }

        private void Data_WGJC_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string strKey = ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.外观检查试验).ToString();
            if (e.ColumnIndex != 4 || e.RowIndex == -1)
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

            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[e.RowIndex];

            DataGridViewRow _Row = Data_WGJC.Rows[e.RowIndex];
            //表位号
            _Row.Cells[1].Value = _MeterInfo.ToString();            //插入表位号

            if (!_MeterInfo.YaoJianYn)           //如果不检
            {
                return;
            }

            _Row.Cells[0].Value = true;            

            if (!_MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.外观检查试验).ToString()))            //检查是否存在外观检查试验
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _TmpMeterResult = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                _TmpMeterResult.Mr_chrRstValue = CLDC_DataCore.Const.Variable.CTG_HeGe;
                _TmpMeterResult._intMyId = CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[e.RowIndex]._intMyId;
                _TmpMeterResult.Mr_chrRstId = ((int)Cus_MeterResultPrjID.外观检查试验).ToString();
                _TmpMeterResult.Mr_chrRstName = Cus_MeterResultPrjID.外观检查试验.ToString();
                _MeterInfo.MeterResults.Add(((int)Cus_MeterResultPrjID.外观检查试验).ToString(), _TmpMeterResult);
            }

            if (_MeterInfo.MeterResults.ContainsKey(strKey))           //如果数据中存在值那么久需要插入数据，这个地方插的值都是合格或者不合格，因为取的都是大编号，不是带具体值的小编号
            {
                if (_MeterInfo.MeterResults[strKey].Mr_chrRstValue == Variable.CTG_HeGe)
                    _MeterInfo.MeterResults[strKey].Mr_chrRstValue = Variable.CTG_BuHeGe;
                else
                    _MeterInfo.MeterResults[strKey].Mr_chrRstValue = Variable.CTG_HeGe;
                _Row.Cells[4].Value = _MeterInfo.MeterResults[strKey].Mr_chrRstValue;

                CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.MeterGroup[e.RowIndex].MeterResults[strKey].Mr_chrRstValue = _MeterInfo.MeterResults[strKey].Mr_chrRstValue;

                if (_MeterInfo.Mb_Result == Variable.CTG_BuHeGe)
                    _Row.DefaultCellStyle.ForeColor = Color.Red;
                else
                    _Row.DefaultCellStyle.ForeColor = Color.Black;

                this.GridSelectRowIndexChanged(e.RowIndex);

                
                this.BeginInvoke(new Action<string>(A =>
                {
                    RefreshRightGridView(CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData.ActiveItemID);
                }), "");
                   
            }
        }

    }
}

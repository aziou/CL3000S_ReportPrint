using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckYuRe : UserControl
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


        public CheckYuRe()
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
        public CheckYuRe(Main parent , CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup,int CheckOrderID,int taiType,int TaiID)
        {
            InitializeComponent();

            ParentMain = parent;

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
         
            this.InitializationGrid(meterGroup);
            this.RefreshGrid(meterGroup, CheckOrderID);


        }      


        private void InitializationGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            _DnbGroup = MeterGroup;
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;           
            int _Count = MeterGroup.MeterGroup.Count;

            if (Data_YuRe.Rows.Count != _Count)
            {
                Data_YuRe.Rows.Clear();

                for (int i = 0; i < _Count; i++)
                {
                    int Index = Data_YuRe.Rows.Add();
                    if ((Index + 1) % 2 == 0)
                    {
                        Data_YuRe.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_YuRe.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_YuRe.Rows[Index].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_YuRe.Rows[Index].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
            }
            Data_YuRe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Data_YuRe.Refresh();
            
        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup,int CheckOrderID)
        {
            StPlan_YuRe _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is StPlan_YuRe)
            {
                _Item = (StPlan_YuRe)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow Row = Data_YuRe.Rows[i];


                //表位号
                Row.Cells[1].Value = _MeterInfo.ToString();

                if (!_MeterInfo.YaoJianYn)
                {
                    Row.Cells[0].Value = false;
                    if (_MeterInfo.Mb_chrBcs==String.Empty || _MeterInfo.Mb_chrBdj==String.Empty)       //如果不检，并且常数或者等级都为空，则将勾选单元格设置为只读
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

                if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                {
                    Row.DefaultCellStyle.ForeColor = Color.Red;

                    foreach (DataGridViewCell cell in Row.Cells)
                    {
                        cell.ToolTipText = _MeterInfo.AVR_DIS_REASON;
                    }
                }
                else
                {
                    Row.DefaultCellStyle.ForeColor = Color.Black;

                    foreach (DataGridViewCell cell in Row.Cells)
                    {
                        cell.ToolTipText = string.Empty;
                    }
                }
                try
                {
                    Row.Cells[2].Value = _Item.ToString();

                    Row.Cells[3].Value = string.Format("{0:F}/{1:F}(当前/总(分))", MeterGroup.NowMinute, _Item.Times);

                }
                catch (Exception ex)
                {
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
                }
            }

        }



        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            this.RefreshGrid(meterGroup,CheckOrderID);
            Data_YuRe.Enabled = true;            
        }

        /// <summary>
        /// 设置是否要检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_YuRe_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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
            if (Data_YuRe[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_YuRe.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_YuRe.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_YuRe.EndEdit();
                }
                Data_YuRe.Enabled = false;
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

        public event  Evt_SetDnbInfoViewData SetDnbInfoViewData;


        private void Data_YuRe_SelectionChanged(object sender, System.EventArgs e)
        {
            try
            {
                 SetDnbInfoViewData(Data_YuRe.SelectedRows[0].Index);
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
                if (Data_YuRe.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_YuRe.SelectedRows[0].Index;
            }
            set
            {
                if (Data_YuRe.IsHandleCreated)
                {
                    if (value >=0 && Data_YuRe.Rows.Count > value)
                    {
                        Data_YuRe.Rows[value].Selected = true;
                        Data_YuRe.CurrentCell = Data_YuRe.Rows[value].Cells[1];
                    }
                }
            }
        }

    }
}

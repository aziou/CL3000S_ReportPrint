using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    /// <summary>
    /// 绝缘耐压检定数据的显示
    /// </summary>
    public partial class CheckInsulation : UserControl
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


        public CheckInsulation()
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
        public CheckInsulation(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (Data_Insulation.Rows.Count != _Count)
            {
                Data_Insulation.Rows.Clear();

                for (int i = 0; i < _Count; i++)
                {
                    int Index = Data_Insulation.Rows.Add();
                    if ((Index + 1) % 2 == 0)
                    {
                        Data_Insulation.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_Insulation.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_Insulation.Rows[Index].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Insulation.Rows[Index].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
            }
            Data_Insulation.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Data_Insulation.Refresh();

        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            CLDC_DataCore.Struct.StInsulationParam _Item;
            //CLDC_DataCore.Struct.StQiDong _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StInsulationParam)
            {
                _Item = (CLDC_DataCore.Struct.StInsulationParam)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow Row = Data_Insulation.Rows[i];


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

                try
                {
                    Row.Cells[2].Value = _Item.ToString();

                    string _Key = string.Format("{0}{1}", ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.工频耐压试验).ToString(), ((int)_Item.InsulationType).ToString());
                    if (MeterGroup.MeterGroup[i].MeterInsulations.ContainsKey(_Key))            //如果这个项目做过
                    {
                        if (MeterGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定)        //如果不是在停止状态，则显示进度
                        {
                            Row.Cells[3].Value = string.Format("{0:F}/{1:F}(当前/总(秒))", MeterGroup.MeterGroup[i].MeterInsulations[_Key].TestTime, MeterGroup.MeterGroup[i].MeterInsulations[_Key].Time);
                            Row.Cells[5].Value = MeterGroup.MeterGroup[i].MeterInsulations[_Key].Result.ToString();
                        }
                        else
                        {
                            Row.Cells[3].Value = "100%";
                            Row.Cells[4].Value = MeterGroup.MeterGroup[i].MeterInsulations[_Key].stringCurrent;
                            Row.Cells[5].Value = MeterGroup.MeterGroup[i].MeterInsulations[_Key].Result;
                        }
                        if (Row.Cells[5].Value != null && Row.Cells[5].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)          //如果是不合格则显示红色
                        { 
                            Row.DefaultCellStyle.ForeColor = Color.Red;

                            foreach (DataGridViewCell cell in Row.Cells)
                            {
                                cell.ToolTipText = MeterGroup.MeterGroup[i].MeterInsulations[_Key].Description;
                            }
                        }
                        else
                            Row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        Row.DefaultCellStyle.ForeColor = Color.Black;
                        Row.Cells[3].Value = string.Format("{0:F}/{1:F}(当前/总(秒))", 0 , _Item.Time);
                        Row.Cells[4].Value = string.Empty;
                        Row.Cells[5].Value = string.Empty;
                    }
                    //if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                    //    Row.DefaultCellStyle.ForeColor = Color.Red;
                    //else
                    //    Row.DefaultCellStyle.ForeColor = Color.Black;
                }
                catch (Exception ex)
                {
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
                }
            }
        }


        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            this.RefreshGrid(meterGroup, CheckOrderID);
            Data_Insulation.Enabled = true;            
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

            if (Data_Insulation[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_Insulation.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_Insulation.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_Insulation.EndEdit();
                }
                Data_Insulation.Enabled = false;
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
                SetDnbInfoViewData(Data_Insulation.SelectedRows[0].Index);
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
                if (Data_Insulation.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_Insulation.SelectedRows[0].Index;
            }
            set
            {
                if (Data_Insulation.IsHandleCreated)
                {
                    if (value >= 0 && Data_Insulation.Rows.Count > value)
                    {
                        Data_Insulation.Rows[value].Selected = true;
                        Data_Insulation.CurrentCell = Data_Insulation.Rows[value].Cells[1];
                    }
                }
            }
        }        

    }
}

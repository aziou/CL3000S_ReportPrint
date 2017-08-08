using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckPowerConsume : UserControl
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

        private Main ParentMain;
        /// <summary>
        /// 台体编号
        /// </summary>
        private int _TaiID = 0;
        /// <summary>
        /// 台体类型
        /// </summary>
        private int _TaiType = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckPowerConsume()
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
        public CheckPowerConsume(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (dgv_Data.Rows.Count != _Count)
            {
                dgv_Data.Rows.Clear();

                for (int i = 0; i < _Count; i++)
                {
                    int Index = dgv_Data.Rows.Add();
                    if ((Index + 1) % 2 == 0)
                    {
                        dgv_Data.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        dgv_Data.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    dgv_Data.Rows[Index].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    dgv_Data.Rows[Index].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
            }
            dgv_Data.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Data.Refresh();

        }



        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            CLDC_DataCore.Struct.StPowerConsume _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPowerConsume)
            {
                _Item = (CLDC_DataCore.Struct.StPowerConsume)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow Row = dgv_Data.Rows[i];

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

                    string _Key = string.Format("{0}{1}", ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.功耗试验).ToString(), "11");

                    if (MeterGroup.MeterGroup[i].MeterPowers.ContainsKey(_Key))            //如果这个项目做过
                    {
                        if (MeterGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定)        //如果不是在停止状态，则显示进度
                        {

                            Row.Cells[3].Value = "正在检定......";
                            Row.Cells[4].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_chrValue;

                        }
                        else
                        {
                            Row.Cells[3].Value = "检定完成";
                            Row.Cells[4].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_chrValue;

                            Row.Cells[5].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ua_ReactiveP;
                            Row.Cells[6].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ub_ReactiveP;
                            Row.Cells[7].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Uc_ReactiveP;

                            Row.Cells[8].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ua_ReactiveS;
                            Row.Cells[9].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ub_ReactiveS;
                            Row.Cells[10].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Uc_ReactiveS;

                            Row.Cells[11].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ia_ReactiveS;
                            Row.Cells[12].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ib_ReactiveS;
                            Row.Cells[13].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ic_ReactiveS;

                            
                        }
                        if (Row.Cells[4].Value != null && Row.Cells[4].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)          //如果是不合格则显示红色
                        {
                            Row.DefaultCellStyle.ForeColor = Color.Red;

                            foreach (DataGridViewCell cell in Row.Cells)
                            {
                                cell.ToolTipText = MeterGroup.MeterGroup[i].MeterPowers[_Key].AVR_DIS_REASON;
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
                    }
                    else
                    {
                        Row.DefaultCellStyle.ForeColor = Color.Black;
                        Row.Cells[3].Value = "准备就绪";
                    }

                }
                catch (Exception ex)
                {
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
                }
            }
        }


        /// <summary>
        /// 数据刷新，外部调用方法
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="CheckOrderID"></param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            this.RefreshGrid(meterGroup,CheckOrderID);
            dgv_Data.Enabled = true;
        }

        /// <summary>
        /// 设置是否要检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_Block_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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
            if (dgv_Data[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)dgv_Data.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    dgv_Data.EndEdit();
                }
                else
                {
                    Yn = true;
                    dgv_Data.EndEdit();
                }
                dgv_Data.Enabled = false;
                //Comm.Function.TopWaiting.ShowWaiting("正在更改...");
                ParentMain.Evt_OnYaoJianChanged(_TaiType, _TaiID, e.RowIndex, Yn);
                //Comm.Function.TopWaiting.HideWaiting();
            }
            else
            {
                MessageBox.Show("没有处理事件Evt_OnYaoJianChanged", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 显示基本信息窗体相关

        public delegate void Evt_SetDnbInfoViewData(int Index);

        public event  Evt_SetDnbInfoViewData SetDnbInfoViewData;

        private void Data_Block_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(dgv_Data.SelectedRows[0].Index);
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
                if (dgv_Data.SelectedRows.Count == 0)
                    return 0;
                else
                    return dgv_Data.SelectedRows[0].Index;
            }
            set
            {
                if (dgv_Data.IsHandleCreated)
                {
                    if (value >=0 && dgv_Data.Rows.Count > value)
                    {
                        dgv_Data.Rows[value].Selected = true;
                        dgv_Data.CurrentCell = dgv_Data.Rows[value].Cells[1];
                    }
                }
            }
        }
    }
}

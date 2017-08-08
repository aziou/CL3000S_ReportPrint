using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{//lsx--潜动里添加日计时误差
    public partial class CheckQianDong : UserControl
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


        public CheckQianDong()
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
        public CheckQianDong(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (Data_QianDong.Rows.Count != _Count)
            {
                Data_QianDong.Rows.Clear();

                for (int i = 0; i < _Count; i++)
                {
                    int Index = Data_QianDong.Rows.Add();
                    if ((Index + 1) % 2 == 0)
                    {
                        Data_QianDong.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_QianDong.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_QianDong.Rows[Index].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_QianDong.Rows[Index].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
            }
            Data_QianDong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Data_QianDong.Refresh();

        }

      
        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            
            StPlan_QianDong  _Item;
            _DnbGroup = MeterGroup;
            if (MeterGroup.CheckPlan[CheckOrderID] is StPlan_QianDong)
            {
                _Item = (StPlan_QianDong)MeterGroup.CheckPlan[CheckOrderID];
                
               
            }
            else
            {
                    return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow Row = Data_QianDong.Rows[i];


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

                    string _Key = string.Format("{0}{1}{2}", ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验).ToString(), ((int)_Item.PowerFangXiang).ToString(), (Convert.ToInt32(_Item.FloatxU * 100)).ToString("D3"));//,_Item .FloatxU .ToString ()

                    if (MeterGroup.MeterGroup[i].MeterQdQids.ContainsKey(_Key))            //如果这个项目做过
                    {
                        if (MeterGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定)        //如果不是在停止状态，则显示进度
                        {
                            //if (MeterGroup.NowMinute >= float.Parse(MeterGroup.MeterGroup[i].MeterResults[_Key].Mr_Time))
                            //{
                            Row.Cells[3].Value = _Item.CheckTime;
                            Row.Cells[4].Value = _Item.FloatIb;
                            Row.Cells[5].Value = _Item.FloatxU * CLDC_DataCore.Const.GlobalUnit.U;
                            //}
                            //else
                            {
                                Row.Cells[6].Value = string.Format("{0:F}/{1:F}(当前/总(分))", MeterGroup.NowMinute, _Item.CheckTime);
                                Row.Cells[7].Value = MeterGroup.MeterGroup[i].MeterQdQids[_Key].Mqd_chrJL;
                            }
                        }
                        else
                        {
                            Row.Cells[3].Value = _Item.CheckTime;
                            Row.Cells[4].Value = _Item.FloatIb;
                            Row.Cells[5].Value = _Item.FloatxU * CLDC_DataCore.Const.GlobalUnit.U;
                            Row.Cells[6].Value = "100%";
                            Row.Cells[7].Value = MeterGroup.MeterGroup[i].MeterQdQids[_Key].Mqd_chrJL;
                        }
                        if (Row.Cells[7].Value != null && Row.Cells[7].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)          //如果是不合格则显示红色
                        {
                            Row.DefaultCellStyle.ForeColor = Color.Red;
                            foreach (DataGridViewCell cell in Row.Cells)
                            {
                                cell.ToolTipText = MeterGroup.MeterGroup[i].MeterQdQids[_Key].AVR_DIS_REASON;
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
                        //Row.DefaultCellStyle.ForeColor = Color.Black;
                        Row.Cells[3].Value = _Item.CheckTime;
                        Row.Cells[4].Value = _Item.FloatIb;
                        Row.Cells[5].Value = _Item.FloatxU * CLDC_DataCore.Const.GlobalUnit.U;
                        Row.Cells[6].Value = string.Format("{0:F}/{1:F}(当前/总(分))", MeterGroup.NowMinute, _Item.CheckTime);
                        Row.Cells[7].Value = CLDC_DataCore.Const.Variable.CTG_DEFAULTRESULT;
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
                catch (Exception ex)
                {
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
                }
            }
            #region ----刷新日记时误差数据表格----
            if (tabControl1.TabPages.Count == 2)
            {
                Control _Control = tabControl1.TabPages[1].Controls[0];
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDateTimeErr)_Control).SetData(MeterGroup.MeterGroup);
            }
            #endregion
        }
        /// <summary>
        /// 初始化日计时数据
        /// </summary>
        /// <param name="meterGroup"></param>
        private void InitDataTimeErr(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup,int CheckOrderID)
        {
            StPlan_QianDong _Item;
            _Item = (StPlan_QianDong)meterGroup.CheckPlan[CheckOrderID];
            string[] GetString = new string[4];
            if (_Item.DayCheckTimesSetting != null)
            {
                GetString = _Item.DayCheckTimesSetting.Split('|');
            }
            else
            {
                GetString[0] = "0";
            }
            if (GetString[0] == "1")
            {
                if (tabControl1.TabPages.Count ==1)            //如果Tab的页数大于1，那表示存在动态增加的数据页
                {
                    int Count;
                    if (GetString.Length > 3)
                    {
                        int.TryParse(GetString[2], out Count);
                    }
                    else
                    {
                        Count = 10;
                    }
                    tabControl1.TabPages.Add("日计时误差数据");
                    CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDateTimeErr _DateTimeErr = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDateTimeErr(Count);
                    tabControl1.TabPages[1].Controls.Add(_DateTimeErr);
                    _DateTimeErr.Dock = DockStyle.Fill;
                    _DateTimeErr.Margin = new System.Windows.Forms.Padding(0);
                }
            }
            else
            {
                if (tabControl1.TabPages.Count > 1)
                {
                    tabControl1.TabPages.RemoveAt(tabControl1.TabPages.Count-1);
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
          #region-----刷新日记时误差数据表格-----
            InitDataTimeErr(meterGroup,CheckOrderID );
          #endregion
            this.RefreshGrid(meterGroup, CheckOrderID);
            Data_QianDong.Enabled = true;            
           
        }
        /// <summary>
        /// 要检，不检，切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_QianDong_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

            if (Data_QianDong[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_QianDong.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_QianDong.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_QianDong.EndEdit();
                }
                Data_QianDong.Enabled = false;
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

        private void Data_QianDong_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_QianDong.SelectedRows[0].Index);
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
                if (Data_QianDong.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_QianDong.SelectedRows[0].Index;
            }
            set
            {
                if (Data_QianDong.IsHandleCreated)
                {
                    if (value >= 0 && Data_QianDong.Rows.Count > value)
                    {
                        Data_QianDong.Rows[value].Selected = true;
                        Data_QianDong.CurrentCell = Data_QianDong.Rows[value].Cells[1];
                    }
                }
            }
        }

    }
}

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
{

    /// <summary>
    /// 通讯协议检查试验
    /// 新增试验
    /// 余伟
    /// 2012-10-25
    /// </summary>
    public partial class CheckConnProtocol : UserControl
    {
        private CLDC_DataCore.SystemModel.Item.csDataFlag _csDataFlag = new CLDC_DataCore.SystemModel.Item.csDataFlag();
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


        public CheckConnProtocol()
        {
            _csDataFlag.Load();
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
        public CheckConnProtocol(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
        {
            _csDataFlag.Load();
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

            if (Data_ConnProtocol.Rows.Count != _Count)
            {
                Data_ConnProtocol.Rows.Clear();

                for (int i = 0; i < _Count; i++)
                {
                    int Index = Data_ConnProtocol.Rows.Add();
                    if ((Index + 1) % 2 == 0)
                    {
                        Data_ConnProtocol.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_ConnProtocol.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_ConnProtocol.Rows[Index].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_ConnProtocol.Rows[Index].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
            }
            Data_ConnProtocol.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Data_ConnProtocol.Refresh();

        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            StPlan_ConnProtocol _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is StPlan_ConnProtocol)
            {
                _Item = (StPlan_ConnProtocol)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow Row = Data_ConnProtocol.Rows[i];


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

                    string _Key = _Item.PrjID;// string.Format("{0}{1}{2}", ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.通讯协议检查试验).ToString(), _Item.ItemCode, ((int)_Item.OperType).ToString());//_csDataFlag.GetDataFlagNo(_Item.ConnProtocolItem).ToString().PadLeft(2, '0')
                    if (_MeterInfo.MeterDLTDatas.ContainsKey(_Key))            //如果这个项目做过
                    {
                        if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定)        //如果不是在停止状态，则显示进度
                        {
                            Row.Cells[3].Value = _MeterInfo.MeterDLTDatas[_Key]._str_progress;
                            Row.Cells[4].Value = _MeterInfo.MeterDLTDatas[_Key].AVR_CONC != null ? _MeterInfo.MeterDLTDatas[_Key].AVR_CONC : "";

                            Row.Cells[5].Value = _MeterInfo.MeterDLTDatas[_Key].Mdlt_chrValue;
                            //if (Row.Cells[4].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe || Row.Cells[4].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_HeGe)
                            //{
                            //    if (Row.Cells[4].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)          //如果是不合格则显示红色
                            //        Row.DefaultCellStyle.ForeColor = Color.Red;
                            //    else
                            //        Row.DefaultCellStyle.ForeColor = Color.Black;
                            //}
                            //else
                            //{
                            //    Row.DefaultCellStyle.ForeColor = Color.Black;
                            //}
                        }
                        else
                        {
                            Row.Cells[3].Value = _MeterInfo.MeterDLTDatas[_Key]._str_progress;
                            Row.Cells[4].Value = "";
                            Row.Cells[5].Value = "";
                            //Row.DefaultCellStyle.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        Row.Cells[3].Value = "";
                        Row.Cells[4].Value = CLDC_DataCore.Const.Variable.CTG_DEFAULTRESULT;
                        Row.Cells[5].Value = "";
                        //Row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                        Row.DefaultCellStyle.ForeColor = Color.Red;
                    else
                        Row.DefaultCellStyle.ForeColor = Color.Black;
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
            Data_ConnProtocol.Enabled = true;           
        }

        private void Data_QiDong_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

            if (Data_ConnProtocol[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_ConnProtocol.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_ConnProtocol.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_ConnProtocol.EndEdit();
                }
                Data_ConnProtocol.Enabled = false;
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

        private void Data_QiDong_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_ConnProtocol.SelectedRows[0].Index);
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
                if (Data_ConnProtocol.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_ConnProtocol.SelectedRows[0].Index;
            }
            set
            {
                if (Data_ConnProtocol.IsHandleCreated)
                {
                    if (value >= 0 && Data_ConnProtocol.Rows.Count > value)
                    {
                        Data_ConnProtocol.Rows[value].Selected = true;
                        Data_ConnProtocol.CurrentCell = Data_ConnProtocol.Rows[value].Cells[1];
                    }
                }
            }
        }      


    }
}

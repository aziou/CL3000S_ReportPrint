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
    public partial class CheckZouZi : UserControl
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
        /// 是否是录入起码（单机版才有效）
        /// </summary>
        private bool IsInputStartValue = false;


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
        /// 数据模型
        /// </summary>
        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _MeterGroup = null;
        /// <summary>
        /// 当前检定点序号
        /// </summary>
        private int _CheckOrderID = 0;


        public CheckZouZi()
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
        public CheckZouZi(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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
            int _Count = MeterGroup.MeterGroup.Count;

            if (Data_ZouZi.Rows.Count != _Count)
            {
                Data_ZouZi.Rows.Clear();
                for (int i = 0; i < _Count; i++)
                {
                    int RowIndex = Data_ZouZi.Rows.Add();

                    if ((RowIndex + 1) % 2 == 0)
                    {
                        Data_ZouZi.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_ZouZi.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_ZouZi.Rows[RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_ZouZi.Rows[RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
                Data_ZouZi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Data_ZouZi.Refresh();
            }
        }

        /// <summary>
        /// 数据刷新
        /// </summary>
        /// <param name="MeterGroup">电能表数据集合</param>
        /// <param name="CheckOrderID">当前检定点</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {

            _MeterGroup = MeterGroup;

            _CheckOrderID = CheckOrderID;

            StPlan_ZouZi _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is StPlan_ZouZi)
            {
                _Item = (StPlan_ZouZi)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = Data_ZouZi.Rows[i];
                //表位号
                _Row.Cells[1].Value = _MeterInfo.ToString();            //插入表位号

                if (!_MeterInfo.YaoJianYn)
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

                _Row.Cells[2].Value = _Item.ToString();     //项目描述

                if (_Item.ZouZiMethod == CLDC_Comm.Enum.Cus_ZouZiMethod.标准表法)
                {
                    _Row.Cells[4].Value = string.Format("当前:{0:F5}/总:{1:F}(度)", MeterGroup.NowMinute, _Item.UseMinutes);        //进度
                }
                else if (_Item.ZouZiMethod == CLDC_Comm.Enum.Cus_ZouZiMethod.计读脉冲法)
                {
                   _Row.Cells[4].Value = string.Format("当前:{0:F}/总:{1:F}(个)", MeterGroup.NowMinute, _Item.UseMinutes);        //进度
                }
                else if(_Item.ZouZiMethod == CLDC_Comm.Enum.Cus_ZouZiMethod.校核常数)
                {
                    _Row.Cells[4].Value = string.Format("当前:{0:F5}/总:{1:F}(度)", MeterGroup.NowMinute, _Item.UseMinutes);        //进度
                }
                else
                {
                   _Row.Cells[4].Value = string.Format("当前:{0:F}/总:{1:F}(分)", MeterGroup.NowMinute, _Item.UseMinutes);        //进度
                }

                string _Key = _Item.itemKey;

                if (_MeterInfo.MeterZZErrors.ContainsKey(_Key))
                {
                    if (_MeterInfo.MeterZZErrors[_Key].Mz_chrQiMa == -1F)
                        _Row.Cells[3].Value = "";           //起码
                    else
                        _Row.Cells[3].Value = _MeterInfo.MeterZZErrors[_Key].Mz_chrQiMa.ToString("F2");           //起码
                    if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定)           //如果是停滞状态，直接赋值进度100%
                    {
                        _Row.Cells[4].Value = "100%";
                    }
                    //脉冲数
                    _Row.Cells[7].Value = _MeterInfo.MeterZZErrors[_Key].AVR_STANDARD_METER_ENERGY;
                    _Row.Cells[6].Value = _MeterInfo.MeterZZErrors[_Key].Mz_chrPules;
                    
                    //止码
                    if (_MeterInfo.MeterZZErrors[_Key].Mz_chrZiMa != -1F && _MeterInfo.MeterZZErrors[_Key].Mz_chrZiMa.ToString().Length > 0)
                    {
                        _Row.Cells[5].Value = _MeterInfo.MeterZZErrors[_Key].Mz_chrZiMa.ToString("F2");         //止码
                    }
                    else
                    {
                        _Row.Cells[5].Value = "";
                    }
                    if (_MeterInfo.MeterZZErrors[_Key].Mz_chrWc != null && _MeterInfo.MeterZZErrors[_Key].Mz_chrWc.Length > 0)
                    {
                        _Row.Cells[8].Value = _MeterInfo.MeterZZErrors[_Key].Mz_chrWc;             //误差
                    }

                    if (_MeterInfo.MeterZZErrors[_Key].Mz_chrJL != null && _MeterInfo.MeterZZErrors[_Key].Mz_chrJL.Length > 0)
                    {
                        _Row.Cells[9].Value = _MeterInfo.MeterZZErrors[_Key].Mz_chrJL;         //结论
                    }

                    //if (_MeterInfo.MeterZZErrors[_Key].Mz_Result != null && _MeterInfo.MeterZZErrors[_Key].Mz_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                    //{
                    //    _Row.DefaultCellStyle.ForeColor = Color.Red;                //如果不合格则设置为红色
                    //}
                    //else
                    //{
                    //    _Row.DefaultCellStyle.ForeColor = Color.Black;
                    //}

                }
                else
                {
                    //_Row.DefaultCellStyle.ForeColor = Color.Black;
                    for (int j = 2; j < Data_ZouZi.Columns.Count; j++)
                    {
                        _Row.Cells[j].Value = string.Empty;
                    }
                }
                if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                {
                    _Row.DefaultCellStyle.ForeColor = Color.Red;

                    foreach (DataGridViewCell cell in _Row.Cells)
                    {
                        if (MeterGroup.MeterGroup[i].MeterZZErrors.ContainsKey(_Key))
                        {
                            cell.ToolTipText = MeterGroup.MeterGroup[i].MeterZZErrors[_Key].AVR_DIS_REASON;
                        }
    
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
        /// <summary>
        /// 外部调用刷新事件
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="CheckOrderID"></param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {

            this.RefreshGrid(meterGroup, CheckOrderID);

            if (meterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.停止检定)
            {
                Table_CheckDns.Enabled = false;
            }

            Data_ZouZi.Enabled = true;

        }


        /// <summary>
        /// 录入起码、止码事件(该事件仅在单机版中才用到)
        /// </summary>
        /// <param name="IsStartNumber">是否是录入起码</param>
        /// <returns></returns>
        public bool Fun_InputZZNumber(bool IsStartNumber)
        {
            //if (BtnButton == null) return false;

            //BtnButton.Text = "录入完毕";

            //BtnButton.Visible = true;

            IsInputStartValue = IsStartNumber;

            Table_CheckDns.Enabled = true;
            int RowIndex = _MeterGroup.GetFirstYaoJianMeterBwh();  //获取第一个当前要检表位
            if (IsStartNumber)
            {
                Data_ZouZi.Columns[3].ReadOnly = false;         //打开起码录入
                Data_ZouZi.Rows[RowIndex].Cells[3].Selected = true;        //焦点房到第一个单元格
                Lab_Dns.Text = "相同起码：";
            }
            else
            {
                Data_ZouZi.Columns[5].ReadOnly = false;         //打开止码录入
                Data_ZouZi.Rows[RowIndex].Cells[5].Selected = true;    //焦点放到第一个单元格
                Chk_MathAdd.Visible = true;
                Chk_MathAdd.Checked = false;
                Lab_Dns.Text = "相同止码：";
            }
            //Data_ZouZi.BeginEdit(true);
            return true;

        }


        bool IsDoComplated = false;
        /// <summary>
        /// 录入起码、止码完成(该事件仅在单机版本中才用到)
        /// </summary>
        /// <param name="sender">当前按钮</param>
        /// <param name="e"></param>
        public void Btn_DoComplated_Click()
        {
            IsDoComplated = true;
            Txt_Dns.Text = "";
            IsDoComplated = false;
            Data_ZouZi.Columns[3].ReadOnly = true;
            Data_ZouZi.Columns[5].ReadOnly = true;
            string strKey = "";
            object objplan = _MeterGroup.CheckPlan[_CheckOrderID];
            if (objplan is StPlan_ZouZi)
            {
                strKey = ((StPlan_ZouZi)objplan).PrjID;
            }
            if (IsInputStartValue)
            {
                for (int i = 0; i < Data_ZouZi.Rows.Count; i++)
                {
                    if (!_MeterGroup.MeterGroup[i].YaoJianYn) continue;
                    DataGridViewCell Cell = Data_ZouZi.Rows[i].Cells[3];
                    if (!CLDC_DataCore.Function.Number.IsNumeric(Cell.Value.ToString()))
                    {
                        MessageBoxEx.Show(this, "起码必须是一个数字，请重新输入...", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cell.Selected = true;
                        Cell.Value = "";
                        return;
                    }
                    else
                    {

                        _MeterGroup.MeterGroup[i].MeterZZErrors[strKey].Mz_chrQiMa = float.Parse(Cell.Value.ToString());
                    }
                }
            }
            else
            {
                for (int i = 0; i < Data_ZouZi.Rows.Count; i++)
                {
                    if (!_MeterGroup.MeterGroup[i].YaoJianYn) continue;
                    DataGridViewCell Cell = Data_ZouZi.Rows[i].Cells[5];
                    if (!CLDC_DataCore.Function.Number.IsNumeric(Cell.Value.ToString()))
                    {
                        MessageBoxEx.Show(this, "止码必须是一个数字，请重新输入...", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cell.Selected = true;
                        Cell.Value = "";
                        return;
                    }
                    else
                    {
                        _MeterGroup.MeterGroup[i].MeterZZErrors[strKey].Mz_chrZiMa = float.Parse(Cell.Value.ToString());
                    }
                }
            }
            if (ParentMain.Evt_OnInputNumberEnd != null)
            {
                if (!ParentMain.Evt_OnInputNumberEnd(_MeterGroup, _TaiType, _TaiID))
                {
                    MessageBoxEx.Show(this, "操作失败!");
                    return;
                }
                Table_CheckDns.Enabled = false;
            }
        }


        /// <summary>
        /// 输入起止码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_ZouZi_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.BeginInvoke(new Evt_CellEndEdit(CellEndEdit), e.RowIndex, e.ColumnIndex);
        }
        /// <summary>
        /// 输入起止码的委托
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="ColIndex"></param>
        delegate void Evt_CellEndEdit(int RowIndex, int ColIndex);
        /// <summary>
        /// 输入起止码后进行数据合法性检测和自动移动到下一行，这个过程使用了一个委托处理，因为在当前单元格编辑完成后，需要等待表格的CellSelectedChange事件完成后才能处理
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="ColIndex"></param>
        private void CellEndEdit(int RowIndex, int ColIndex)
        {
            if (ColIndex == 0) return;
            DataGridViewCell Cell = Data_ZouZi.Rows[RowIndex].Cells[ColIndex];
            if (!Convert.ToBoolean(Data_ZouZi[0, RowIndex].Value)) return;
            if (Cell.Value == null || !CLDC_DataCore.Function.Number.IsNumeric(Cell.Value.ToString()))
            {
                MessageBoxEx.Show(this,"起码或止码必须是一个数字，请重新输入...", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Data_ZouZi.Rows[RowIndex].Cells[ColIndex].Selected = true;
                Data_ZouZi.Rows[RowIndex].Cells[ColIndex].Value = "";
                Data_ZouZi.BeginEdit(true);
                return;
            }
            else
            {
                for (int i = RowIndex + 1; i < Data_ZouZi.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(Data_ZouZi[0, i].Value))
                    {
                        Data_ZouZi[ColIndex, i].Selected = true;
                        Data_ZouZi.BeginEdit(true);
                        break;
                    }
                }
            }
        }


        private void Data_ZouZi_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (CLDC_DataCore.Const.GlobalUnit.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.停止检定) return;
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
            if (Data_ZouZi[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //如果是只读则退出！
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_ZouZi.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_ZouZi.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_ZouZi.EndEdit();
                }
                Data_ZouZi.Enabled = false;
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

        private void Data_ZouZi_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_ZouZi.SelectedRows[0].Index);
            }
            catch
            {
                SetDnbInfoViewData(0);         //如果出现错误就自动选择第一个表位
            }
        }

        #endregion

        /// <summary>
        /// 底度录入完毕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Ok_Click(object sender, EventArgs e)
        {
            this.Btn_DoComplated_Click();
        }

        /// <summary>
        /// 起码止码全部相同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Dns_TextChanged(object sender, EventArgs e)
        {
            if (IsDoComplated) return;
            if (Txt_Dns.Text == string.Empty || CLDC_DataCore.Function.Number.IsNumeric(Txt_Dns.Text))
            {
                for (int i = 0; i < Data_ZouZi.Rows.Count; i++)
                {
                    if (this.IsInputStartValue)
                    {
                        if (Convert.ToBoolean(Data_ZouZi[0, i].Value))
                        {
                            Data_ZouZi.Rows[i].Cells[3].Value = Txt_Dns.Text;           //起码
                        }
                        else
                            {
                                Data_ZouZi.Rows[i].Cells[3].Value = "";           //起码
                            }
                    }
                    else
                    {
                        if (Convert.ToBoolean(Data_ZouZi[0, i].Value))
                        {
                            if (Chk_MathAdd.Checked == true)
                            {

                                if (Data_ZouZi.Rows[i].Cells[3].Value != null && CLDC_DataCore.Function.Number.IsNumeric(Data_ZouZi.Rows[i].Cells[3].Value.ToString()))
                                {
                                    if (Txt_Dns.Text != "")
                                    {
                                        Data_ZouZi.Rows[i].Cells[5].Value = (float.Parse(Data_ZouZi.Rows[i].Cells[3].Value.ToString()) + float.Parse(Txt_Dns.Text)).ToString();
                                    }
                                    else
                                    {
                                        Data_ZouZi.Rows[i].Cells[5].Value = float.Parse(Data_ZouZi.Rows[i].Cells[3].Value.ToString());
                                    }
                                }

                            }
                            else
                            {
                                Data_ZouZi.Rows[i].Cells[5].Value = Txt_Dns.Text;           //止码
                            }
                        }
                        else
                        {
                            Data_ZouZi.Rows[i].Cells[5].Value = "";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置当前选中的行号
        /// </summary>
        public int SelectRowIndex
        {
            get
            {
                if (Data_ZouZi.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_ZouZi.SelectedRows[0].Index;
            }
            set
            {
                if (Data_ZouZi.IsHandleCreated)
                {
                    if (value >= 0 && Data_ZouZi.Rows.Count > value)
                    {
                        Data_ZouZi.Rows[value].Selected = true;
                        Data_ZouZi.CurrentCell = Data_ZouZi.Rows[value].Cells[1];
                    }
                }
            }
        }

        /// <summary>
        /// 止码=起码加文本框内容还是止码=文本框内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_MathAdd_CheckedChanged(object sender, EventArgs e)
        {
            this.Txt_Dns_TextChanged(Txt_Dns, e);
        }


    }
}

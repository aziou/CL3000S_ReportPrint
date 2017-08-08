using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using System.Threading;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI
{
    public partial class WcLimitSetup : UserControl
    {


        CLDC_DataCore.DataBase.clsWcLimitDataControl _WC;

        /// <summary>
        /// 元件
        /// </summary>
        private CLDC_Comm.Enum.Cus_PowerYuanJian _Yj;

        /// <summary>
        /// 误差限名称
        /// </summary>
        private CLDC_DataCore.DataBase.IDAndValue _WcLimitName;
        /// <summary>
        /// 规程名称
        /// </summary>
        private CLDC_DataCore.DataBase.IDAndValue _GuiChengName;
        /// <summary>
        /// 等级
        /// </summary>
        private CLDC_DataCore.DataBase.IDAndValue _Dj;
        /// <summary>
        /// 是否经互感器接入，0-直接接入
        /// </summary>
        private bool _Hgq = false;
        /// <summary>
        /// 是否有功0-无功，1-有功
        /// </summary>
        /// 
        private bool _Yg = true;

        /// <summary>
        /// 行头字符串数组
        /// </summary>
        private string[] _Rows;
        /// <summary>
        /// 列头字符串数组
        /// </summary>
        private string[] _Columns;

        /// <summary>
        /// 是否是由内部直接操作数据
        /// </summary>
        private bool InsertValue = false;

        public WcLimitSetup()
        {
            InitializeComponent();
        }

        public WcLimitSetup(CLDC_DataCore.DataBase.clsWcLimitDataControl WcLimit)
        {
            InitializeComponent();
            _WC = WcLimit;
        }

        public WcLimitSetup(CLDC_Comm.Enum.Cus_PowerYuanJian Yj, CLDC_DataCore.DataBase.clsWcLimitDataControl WcLimit)
        {
            InitializeComponent();
            _WC = WcLimit;
            _Yj = Yj;

            #region 初始化表格样式


            List<string> _Glyss = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysName();
            _Columns = new string[_Glyss.Count];
            for (int i = 0; i < _Glyss.Count; i++)
            {
                _Columns[i] = _Glyss[i];

            }

            List<string> _xIbs = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.xIbDic.getxIb();

            _Rows = new string[_xIbs.Count];

            for (int i = 0; i < _xIbs.Count; i++)
            {
                _Rows[i] = _xIbs[i];
            }
            _xIbs = null;
            #endregion
        }
        /// <summary>
        /// 返回表格行数
        /// </summary>
        public int RowsCount
        {
            get
            {
                return DGW_WcLimit.Rows.Count;
            }
        }

        /// <summary>
        /// 返回表格列数
        /// </summary>
        public int ColumnsCount
        {
            get
            {
                return DGW_WcLimit.Columns.Count; 
            }
        }
        /// <summary>
        /// 设置单元格值
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="ColIndex"></param>
        /// <param name="Value"></param>
        public void SetCellValue(int RowIndex, int ColIndex, string Value)
        {
            InsertValue = true;
            DGW_WcLimit[ColIndex, RowIndex].Value = Value;
            InsertValue = false;
        }
        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="ColIndex"></param>
        /// <returns></returns>
        public string getCellValue(int RowIndex, int ColIndex)
        {
            return DGW_WcLimit[ColIndex, RowIndex].Value.ToString();
        }

        /// <summary>
        /// 设置误差限表格
        /// </summary>
        /// <param name="WcLimit">误差限数据对象</param>
        /// <param name="GuiChengName">规程名称</param>
        /// <param name="Dj">等级</param>
        /// <param name="Hgq">是否经互感器，0-不经过，1-经过</param>
        /// <param name="Yg">是否有功，0-无功，1-有功</param>
        public void SetWcx(CLDC_DataCore.DataBase.IDAndValue WcLimitName, CLDC_DataCore.DataBase.IDAndValue GuiChengName, CLDC_DataCore.DataBase.IDAndValue Dj, bool Hgq, bool Yg)
        {
            CLDC_DataCore.Function.TopWaiting.ShowWaiting();

            _WcLimitName = WcLimitName;
            _GuiChengName = GuiChengName;
            _Dj = Dj;
            _Hgq = Hgq;
            _Yg = Yg;

            
            DGW_WcLimit.Columns.Clear();
            DGW_WcLimit.Tag = false;            //标志存储，在加载数据的时候不触发修改事件
            DGW_WcLimit.DataSource = _WC.getDataSource(_WcLimitName, _GuiChengName, _Dj, _Yj,_Hgq, _Yg, _Rows, _Columns);


            #region  ----------------------------固化误差限列表样式--------------------
           
            for (int i = 0; i < _Columns.Length; i++)
            {
                DGW_WcLimit.Columns[i].HeaderCell.Value = _Columns[i];
                DGW_WcLimit.Columns[i].Tag = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysID(_Columns[i]);
                DGW_WcLimit.Columns[i].Width = 80;
                DGW_WcLimit.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGW_WcLimit.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGW_WcLimit.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            
            }

            for (int i = 0; i < _Rows.Length; i++)
            {
                DGW_WcLimit.RowHeadersWidth = 120;
                DGW_WcLimit.Rows[i].Height = 25;
                DGW_WcLimit.Rows[i].HeaderCell.Value = _Rows[i].Trim().PadLeft(8, ' ');
                DGW_WcLimit.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                DGW_WcLimit.Rows[i].Tag = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.xIbDic.getxIbID(_Rows[i]);

            }
            #endregion

            DGW_WcLimit.Tag = true;             //标志存储，在加载完成数据后触发修改事件

            CLDC_DataCore.Function.TopWaiting.HideWaiting();
        }

        #region    -----------------------刷新误差限列表-----------------------------
        /// <summary>
        /// 设置规程名称
        /// </summary>
        public CLDC_DataCore.DataBase.IDAndValue GuiChengName
        {
            set 
            {
                _GuiChengName = value;
                this.SetWcx(_WcLimitName, _GuiChengName, _Dj, _Hgq, _Yg);
            }
        }

        /// <summary>
        /// 设置等级
        /// </summary>
        public CLDC_DataCore.DataBase.IDAndValue Dj
        {
            set
            {
                _Dj = value;
                this.SetWcx(_WcLimitName, _GuiChengName, _Dj, _Hgq, _Yg);
            }
        }
        /// <summary>
        /// 设置是否经互感器接入
        /// </summary>
        public bool Hgq
        {
            set
            {
                _Hgq = value;
                this.SetWcx(_WcLimitName, _GuiChengName, _Dj, _Hgq, _Yg);
            }
        }
        /// <summary>
        /// 设置是否有功
        /// </summary>
        public bool Yg
        {
            set
            {
                _Yg = value;
                this.SetWcx(_WcLimitName, _GuiChengName, _Dj, _Hgq, _Yg);
            }

        }

        #endregion


        public void Clear()
        {
            DGW_WcLimit.Columns.Clear();
        }

        /// <summary>
        /// 单元格单机事件，触发编辑开启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGW_WcLimit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DGW_WcLimit.BeginEdit(true);

        }

        /// <summary>
        /// 单元误差限制修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGW_WcLimit_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!(bool)DGW_WcLimit.Tag) return;     //如果是初始化加载数据时则直接返回
            
            if (!InsertValue)
            {
                string[] _TmpValue = DGW_WcLimit[e.ColumnIndex, e.RowIndex].Value.ToString().Split('|');
                if (_TmpValue.Length != 2)
                {
                    MessageBoxEx.Show(this,"修改的误差限格式不正确，正确的格式为[误差上限|误差下限],例：+0.1|-0.1");
                    DGW_WcLimit[e.ColumnIndex, e.RowIndex].Value = DGW_WcLimit[e.ColumnIndex, e.RowIndex].Tag;
                    return;
                }
                if (!CLDC_DataCore.Function.Number.IsNumeric(_TmpValue[0].Replace("+", "")) || !CLDC_DataCore.Function.Number.IsNumeric(_TmpValue[1].Replace("+", "")))
                {
                    MessageBoxEx.Show(this,"修改的误差限格式不正确，正确的格式为[误差上限|误差下限],例：+0.1|-0.1");
                    DGW_WcLimit[e.ColumnIndex, e.RowIndex].Value = DGW_WcLimit[e.ColumnIndex, e.RowIndex].Tag;
                    return;
                }

                if (float.Parse(_TmpValue[0].Replace("+", "")) >= 0)
                    _TmpValue[0] =string.Format("+{0}", _TmpValue[0].Replace("+", ""));
                if (float.Parse(_TmpValue[1].Replace("+", "")) >= 0)
                    _TmpValue[1] = string.Format("+{0}", _TmpValue[0].Replace("+", ""));

            

                DGW_WcLimit[e.ColumnIndex, e.RowIndex].Value = string.Format("{0}|{1}", _TmpValue[0], _TmpValue[1]);
                DGW_WcLimit.Tag = false;
                
            }

            _WC.SaveWcx(_WcLimitName.id
                        , _GuiChengName.id
                        , _Dj.id
                        , (int)_Yj
                        , _Hgq, _Yg
                        , int.Parse(DGW_WcLimit.Rows[e.RowIndex].Tag.ToString())
                        , int.Parse(DGW_WcLimit.Columns[e.ColumnIndex].Tag.ToString())
                        , DGW_WcLimit[e.ColumnIndex, e.RowIndex].Value.ToString());

            DGW_WcLimit.Tag = true;
        }

        private void DGW_WcLimit_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DGW_WcLimit[e.ColumnIndex, e.RowIndex].Tag = DGW_WcLimit[e.ColumnIndex, e.RowIndex].Value;
        }

        private void DGW_WcLimit_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DGW_WcLimit[e.ColumnIndex, e.RowIndex].Tag = null;
        }


    }
}

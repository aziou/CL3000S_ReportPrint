using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.PublicControls
{
    public partial class UI_ClientTable : UserControl
    {
        /// <summary>
        /// 参数设置类型
        /// </summary>
        public enum SetType
        {
            Error = 0,
            表位数 = 1,
            每行表位数 = 2,
            多选框宽度 = 3,
            固定列宽度 = 4,
            列距 = 5,
            行距 = 6,
            文本行高 = 7
        }

        /// <summary>
        /// 多选框列宽度（默认值）
        /// </summary>
        const int CONST_COLCHECKBOX = 24;
        /// <summary>
        /// 固定列宽度（默认值）
        /// </summary>
        const int CONST_COLFIXATION = 24;
        /// <summary>
        /// 文本列宽度（默认值）
        /// </summary>
        const int CONST_COLTEXT = 200;
        /// <summary>
        /// 列距（默认值）
        /// </summary>
        const int CONST_COLSPACE = 10;
        /// <summary>
        /// 行距（默认值）
        /// </summary>
        const int CONST_ROWSPACE = 10;
        /// <summary>
        /// 文本行高（默认值）
        /// </summary>
        const int CONST_ROWTEXT = 20;
        /// <summary>
        /// 表位数（默认值）
        /// </summary>
        const int CONST_METERNUM = 12;
        /// <summary>
        /// 每行放置表位数(默认值)
        /// </summary>
        const int CONST_ROWMETERNUM = 4;

        private Color BuHeGe = Color.Red;

        private Color HeGe = Color.Black;

        private struct CellRowCol
        {
            public int Col;
            public int Row;
        }

        private Dictionary<int, CellRowCol> Meter;

        #region -------------------------------属性------------------------------

        /// <summary>
        /// 多选框列宽度
        /// </summary>
        private int m_ColCheckBoxWidth = CONST_COLCHECKBOX;
        /// <summary>
        /// 固定列宽度
        /// </summary>
        private int m_ColFixationWidth = CONST_COLCHECKBOX;
        /// <summary>
        /// 列距
        /// </summary>
        private int m_ColSpace = CONST_COLSPACE;
        /// <summary>
        /// 行距
        /// </summary>
        private int m_RowSpace = CONST_ROWSPACE;
        /// <summary>
        /// 文本行高
        /// </summary>
        private int m_RowHeight = CONST_ROWTEXT;

        /// <summary>
        /// 表位数
        /// </summary>
        private int m_MeterNum = CONST_METERNUM;
        /// <summary>
        /// 每行放置表位数
        /// </summary>
        private int m_RowMeterNum = CONST_ROWMETERNUM;

        private Font m_TextFont = new Font("宋体",9F);

        /// <summary>
        /// 设置文本单元格字体
        /// </summary>
        public Font TextCellFont
        {
            get
            {
                return m_TextFont;
            }
            set
            {
                m_TextFont = value;
                Dgw.DefaultCellStyle.Font = m_TextFont;
            }
        }


        /// <summary>
        /// 获取或设置多选框列宽度
        /// </summary>
        public int ColCheckBoxWidth
        {
            get
            {
                return m_ColCheckBoxWidth;
            }
            set
            {
                this.m_ColCheckBoxWidth = value;
            }

        }

        /// <summary>
        /// 获取或设置固定列宽度
        /// </summary>
        public int ColFixationWidth
        {
            get
            {
                return this.m_ColFixationWidth;
            }
            set
            {
                this.m_ColFixationWidth = value;
            }
        }

        /// <summary>
        /// 获取或设置列距
        /// </summary>
        public int ColSpace
        {
            get
            {
                return this.m_ColSpace;
            }
            set
            {
                this.m_ColSpace = value;
            }
        }


        /// <summary>
        /// 获取或设置行距
        /// </summary>
        public int RowSpace
        {
            get
            {
                return this.m_RowSpace;
            }
            set
            {
                this.m_RowSpace = value;
            }
        }
        /// <summary>
        /// 获取或设置文本行高
        /// </summary>
        public int RowHeight
        {
            get
            {
                return this.m_RowHeight;
            }
            set
            {
                this.m_RowHeight = value;
            }
        }
        /// <summary>
        /// 获取或设置表位数(以让表格产生多少个表位文本框)
        /// </summary>
        public int MeterNum
        {
            get
            {
                return this.m_MeterNum;
            }
            set
            {
                if (value <= 0) return;
                this.m_MeterNum = value;
            }
        }
        /// <summary>
        /// 获取或设置表位数（以让表格每行产生多少个表位文本框）
        /// </summary>
        public int RowMeterNum
        {
            get
            {
                return this.m_RowMeterNum;
            }
            set
            {
                if (value <= 0) return;
                this.m_RowMeterNum = value;
            }
        }

        /// <summary>
        /// 获取或设置背景颜色
        /// </summary>
        public Color BackGroundColor
        {
            get
            {
                return Dgw.BackgroundColor;
            }
            set
            {
                Dgw.BackgroundColor = value;
            }
        }

        /// <summary>
        /// 获取或设置单元格网格线颜色
        /// </summary>
        public Color GridColor
        {
            get
            {
                return Dgw.GridColor;
            }
            set
            {
                Dgw.GridColor = value;
            }

        }

        /// <summary>
        /// 获取或设置当前选中的表位号文本框
        /// </summary>
        public int SelectedBwh
        {
            get
            {

                if (Dgw.CurrentCell.Tag != null || Dgw.CurrentCell.Tag.ToString() != string.Empty)
                    return (int)Dgw.CurrentCell.Tag;
                else
                {
                    return 1;
                }
            }
            set
            {
                if (value <= 0 || value > m_MeterNum) return;
                if (!Meter.ContainsKey(value)) return;
                CellRowCol _Item = Meter[value];
                Dgw[_Item.Col, _Item.Row].Selected = true;

            }
        }

        /// <summary>
        /// 获取或设置是否只读
        /// </summary>
        public bool ReadOnly
        {
            set
            {
                for (int i = 0; i < Dgw.Columns.Count; i++)
                {
                    if (i % 4 != 0)
                    {
                        continue;
                    }
                    if (i == Dgw.Columns.Count - 1) break;
                    this.BeginInvoke(new Det_SetReadOnly(SetReadOnly), Dgw.Columns[i + 1], value);
                    this.BeginInvoke(new Det_SetReadOnly(SetReadOnly), Dgw.Columns[i + 3], value);
                }
            }
        }

        private delegate void Det_SetReadOnly(DataGridViewColumn Col, bool isRead);

        private void SetReadOnly(DataGridViewColumn Col, bool isRead)
        {
            Col.ReadOnly = isRead;
        }




        #endregion


        #region --------------------------------------方法----------------------------------

        #region ------------设置或获取文本框内容-------------
        /// <summary>
        /// 设置文本框显示值
        /// </summary>
        /// <param name="Values">值数组，如果值数组长度不等于表位数，该方法则不处理</param>
        public void SetTextValue(string[] Values)
        {
            if (Values.Length != this.m_MeterNum) return;

            for (int i = 0; i < Values.Length; i++)
            {
                this.SetTextValue(i + 1, Values[i]);
            }
        }
        /// <summary>
        /// 设置文本框显示值
        /// </summary>
        /// <param name="Bwh">表位号</param>
        /// <param name="Value">对应文本框显示值</param>
        public void SetTextValue(int Bwh, string Value)
        {
            if (!Meter.ContainsKey(Bwh)) return;
            CellRowCol _Item = Meter[Bwh];
            Dgw[_Item.Col, _Item.Row].Value = Value;
        }

        /// <summary>
        /// 获取文本框内容
        /// </summary>
        /// <param name="Bwh">表位号</param>
        /// <returns></returns>
        public string GetTextValue(int Bwh)
        {
            if (!Meter.ContainsKey(Bwh)) return "";
            CellRowCol _Item = Meter[Bwh];
            if (Dgw[_Item.Col, _Item.Row].Value == null)
            {
                return "";
            }
            else
            {
                return Dgw[_Item.Col, _Item.Row].Value.ToString();
            }
        }
        /// <summary>
        /// 获取所有文本框内容
        /// </summary>
        /// <returns></returns>
        public string[] GetTextValue()
        {
            string[] Arr_Value = new string[m_MeterNum];

            for (int i = 0; i < Arr_Value.Length; i++)
            {
                Arr_Value[i] = GetTextValue(i + 1);
            }
            return Arr_Value;
        }

        #endregion 

        #region ---------------设置或获取文本框颜色------------------
        /// <summary>
        /// 设置文本框背景颜色
        /// </summary>
        /// <param name="Values">值数组，如果值数组长度不等于表位数，该方法则不处理</param>
        public void SetTextBackColorValue(bool[] Values)
        {
            if (Values.Length != this.m_MeterNum) return;

            for (int i = 0; i < Values.Length; i++)
            {
                this.SetTextBackColorValue(i + 1, Values[i]);
            }
        }
        /// <summary>
        /// 设置文本框背景颜色
        /// </summary>
        /// <param name="Bwh">表位号</param>
        /// <param name="Value">真显示红色，假显示白色</param>
        public void SetTextBackColorValue(int Bwh, bool Value)
        {
            if (!Meter.ContainsKey(Bwh)) return;
            CellRowCol _Item = Meter[Bwh];
            Dgw[_Item.Col, _Item.Row].Style.ForeColor = !Value?HeGe:BuHeGe;
        }

        /// <summary>
        /// 获取文本框背景颜色
        /// </summary>
        /// <param name="Bwh">表位号</param>
        /// <returns></returns>
        public bool GetBackColorValue(int Bwh)
        {
            if (!Meter.ContainsKey(Bwh)) return false;
            CellRowCol _Item = Meter[Bwh];

            return Dgw[_Item.Col, _Item.Row].Style.ForeColor==BuHeGe?true:false;
        }
        /// <summary>
        /// 获取所有文本框背景颜色
        /// </summary>
        /// <returns></returns>
        public bool[] GetBackColorValue()
        {
            bool[] Arr_Value = new bool[m_MeterNum];

            for (int i = 0; i < Arr_Value.Length; i++)
            {
                Arr_Value[i] = GetBackColorValue(i + 1);
            }
            return Arr_Value;
        }



        #endregion
 

        #region -------------------------设置或获取多选框属性------------------------
        /// <summary>
        /// 设置多选框属性值
        /// </summary>
        /// <param name="Bwh">表位号</param>
        /// <param name="Value">值</param>
        public void SetCheckBoxValue(int Bwh, bool Value)
        {
            if (!Meter.ContainsKey(Bwh)) return;
            CellRowCol _Item = Meter[Bwh];
            Dgw[_Item.Col-2, _Item.Row].Value = Value;
            if (!Value)
            {
                Dgw[_Item.Col, _Item.Row].Value = string.Empty;
            }
        }
        /// <summary>
        /// 设置所有多选框属性值
        /// </summary>
        /// <param name="Value">值数组</param>
        public void SetCheckBoxValue(bool[] Value)
        {
            if (Value.Length != m_MeterNum) return;
            for (int i = 0; i < Value.Length; i++)
            {
                this.SetCheckBoxValue(i + 1, Value[i]);
            }
        }
        /// <summary>
        /// 获取一个表位的多选框的值（一个表位是否要检）
        /// </summary>
        /// <param name="Bwh"></param>
        /// <returns></returns>
        public bool GetCheckBoxValue(int Bwh)
        {
            if (!Meter.ContainsKey(Bwh)) return false;
            CellRowCol _Item = Meter[Bwh];
            if (Dgw[_Item.Col-2, _Item.Row].Value == null)
            {
                return false;
            }
            else
            {
                return (bool)Dgw[_Item.Col-2, _Item.Row].Value;
            }
        }
        /// <summary>
        /// 获取所有表位的多选框的值(所有表位是否要检)
        /// </summary>
        /// <returns></returns>
        public bool[] GetCheckBoxValue()
        {
            bool[] Arr_Value = new bool[m_MeterNum];

            for (int i = 0; i < Arr_Value.Length; i++)
            {
                Arr_Value[i] = GetCheckBoxValue(i + 1);
            }
            return Arr_Value;
        }

        #endregion


        #region ---------------------单元格闪烁---------------------------
        /// <summary>
        /// 使单元格闪烁
        /// </summary>
        /// <param name="Bwh">表位号</param>
        public void FlickerCell(int Bwh)
        {
            this.FlickerCell(Bwh, Color.Red);
        }
        /// <summary>
        /// 使单元格闪烁
        /// </summary>
        /// <param name="Bwh">表位号</param>
        /// <param name="FickerColor">闪烁颜色</param>
        public void FlickerCell(int Bwh, Color FlickerColor)
        {
            this.FlickerCell(Bwh, FlickerColor, 3);
        }
        /// <summary>
        /// 使单元格闪烁
        /// </summary>
        /// <param name="Bwh">表位号</param>
        /// <param name="FickerColor">闪烁颜色</param>
        /// <param name="FickerNum">闪烁次数</param>
        public void FlickerCell(int Bwh, Color FlickerColor, int FlickerNum)
        {
            if (Bwh <= 0 || Bwh > m_MeterNum) return;
            if (!Meter.ContainsKey(Bwh)) return;

            CellRowCol _Item=Meter[Bwh];

            DataGridViewCell _Cell = Dgw[_Item.Col, _Item.Row];
            
            this.BeginInvoke(new Evt_Flicker(Flicker),_Cell,FlickerColor,FlickerNum);

        }

        private delegate void Evt_Flicker(DataGridViewCell Cell, Color FlickerColor, int FlickerNum);

        private System.Threading.AutoResetEvent Are = new System.Threading.AutoResetEvent(false);

        private DataGridViewCell CellFlicker;

        private void Flicker(DataGridViewCell Cell,Color FlickerColor, int FlickerNum)
        {
            Color _BackColor = Cell.Style.BackColor;

            CellFlicker = Cell;

            for (int i = 0; i < FlickerNum; i++)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Flicker), FlickerColor);
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(Flicker), _BackColor);
                Are.WaitOne();
            }
            CellFlicker.Style.BackColor = _BackColor;
        }

        private object Locked=new object();

        private void Flicker(object obj)
        {
            lock (Locked)
            {
                int j = 0;
                while (j < 15)
                {
                    j++;
                    if ((Color)obj != Dgw.DefaultCellStyle.BackColor) System.Threading.Thread.Sleep(2);
                    System.Threading.Thread.Sleep(5);
                }
                CellFlicker.Style.BackColor = (Color)obj ;
            }
            Are.Set();
        }
        #endregion

        #region ------------------------------------刷新表格----------------------------------------
        /// <summary>
        /// 设置并刷新表格
        /// </summary>
        /// <param name="Bws">表位数</param>
        /// <param name="RowBws">每行表位数</param>
        public void RefreshGrid(int Bws, int RowBws)
        {
            if (Bws <= 0 || RowBws <= 0) return;

            this.m_MeterNum = Bws;

            this.m_RowMeterNum = RowBws;

            this.RefreshGrid();
        }

        /// <summary>
        /// 设置并刷新表格
        /// </summary>
        /// <param name="Value">参数值</param>
        /// <param name="PramType">参数类型</param>
        public void RefreshGrid(int Value, SetType PramType)
        {
            switch (PramType)
            {
                case SetType.表位数:
                    this.m_MeterNum = Value;
                    break;
                case SetType.每行表位数:
                    this.m_RowMeterNum = Value;
                    break;
                case SetType.多选框宽度:
                    this.m_ColCheckBoxWidth = Value;
                    break;
                case SetType.固定列宽度:
                    this.m_ColFixationWidth = Value;
                    break;
                case SetType.列距:
                    this.m_ColSpace = Value;
                    break;
                case SetType.行距:
                    this.m_RowSpace = Value;
                    break;
                case SetType.文本行高:
                    this.m_RowHeight = Value;
                    break;
                default:
                    return;
            }

            this.RefreshGrid();

        }


        /// <summary>
        /// 表格刷新，设置完表格后一定要进行刷新
        /// </summary>
        public void RefreshGrid()
        {
            int InsertRow = 0;

            if (this.m_MeterNum % this.m_RowMeterNum != 0)
            {
                InsertRow = (int)this.m_MeterNum / this.m_RowMeterNum + 1;
            }
            else
            {
                InsertRow = (int)this.m_MeterNum / this.m_RowMeterNum;
            }

            int _Rows = (InsertRow) * 2;

            int _Cols = m_RowMeterNum * 4;          //一个表位有3列一列列距离，多选框，表位号，文本框，最后一列不需要列距所以
            Dgw.Rows.Clear();
            Dgw.Columns.Clear();

            #region -------------------------------创建列---------------------------------------------------

            for (int i = 0; i < _Cols; i++)
            {
                int _ColIndex;
                if (i % 4 != 0)
                {
                    continue;
                }

                _ColIndex = Dgw.Columns.Add("Col" + i.ToString(), "");

                this.SetColumnStyleWithColSpace(Dgw.Columns[_ColIndex]);           //列距离

                CreateColumnStyleWithCheckBox(_ColIndex + 1);

                _ColIndex = Dgw.Columns.Add("Col" + (i + 1).ToString(), "");

                this.SetColumnStyleWithBwh(Dgw.Columns[_ColIndex]);                  //表位号

                _ColIndex = Dgw.Columns.Add("Col" + (i + 3).ToString(), "");

                this.SetColumnStyleWithText(Dgw.Columns[_ColIndex]);                //文本

                if (_ColIndex == _Cols - 1)         //多加一列作为列间距
                {
                    _ColIndex = Dgw.Columns.Add("Col" + i.ToString(), "");

                    this.SetColumnStyleWithColSpace(Dgw.Columns[_ColIndex]);           //列距离
                }

            }

            #endregion

            int _Bwh = 0;

            Meter = new Dictionary<int, CellRowCol>();

            for (int i = 0; i < _Rows; i++)
            {
                int _RowIndex = Dgw.Rows.Add();

                if (i % 2 == 0)         //插入行距行
                {
                    Dgw.Rows[_RowIndex].Height = this.m_RowSpace;
                    continue;
                }

                Dgw.Rows[_RowIndex].Height = this.m_RowHeight;         //插入数据行

                for (int j = 0; j < _Cols; j++)
                {
                    if (j % 4 != 0)
                    {
                        continue;
                    }
                    _Bwh++;
                    if (_Bwh > this.m_MeterNum)         //如果超过表位数则不显示任何数据
                    {
                        Dgw.Rows[_RowIndex].Cells[j + 1].Tag = 0;
                        Dgw.Rows[_RowIndex].Cells[j + 2].Value = "";           //插入表位号
                        Dgw.Rows[_RowIndex].Cells[j + 3].Tag = 0;
                    }
                    else
                    {
                        Dgw.Rows[_RowIndex].Cells[j + 1].Tag = _Bwh;
                        Dgw.Rows[_RowIndex].Cells[j + 1].ReadOnly = true;
                        Dgw.Rows[_RowIndex].Cells[j + 2].Value = _Bwh.ToString("D2");           //插入表位号
                        Dgw.Rows[_RowIndex].Cells[j + 3].Tag = _Bwh;
                        Dgw.Rows[_RowIndex].Cells[j + 3].ReadOnly = true;

                        CellRowCol _Item = new CellRowCol();
                        _Item.Row = _RowIndex;
                        _Item.Col = j + 3;
                        Meter.Add(_Bwh, _Item);
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 全选，反选
        /// </summary>
        /// <param name="CheckYn"></param>
        public void SelectAll(bool CheckYn)
        {
            foreach(int i in Meter.Keys)
            {
                Dgw[Meter[i].Col-2, Meter[i].Row].Value = CheckYn;
            }

        }

        #endregion

        public UI_ClientTable()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            this.RefreshGrid();
        }

        #region ---------------------------表格列样式控制----------------------------------------
        /// <summary>
        /// 设置Checkbox列样式
        /// </summary>
        /// <param name="ColIndex"></param>
        private void CreateColumnStyleWithCheckBox(int ColIndex)
        {
            Dgw.Columns.AddRange(new DataGridViewCheckBoxColumn());
            Dgw.Columns[ColIndex].Width = this.m_ColCheckBoxWidth;
            Dgw.Columns[ColIndex].ReadOnly = false;

        }
        /// <summary>
        /// 设置固定列样式
        /// </summary>
        /// <param name="ControlCol"></param>
        private void SetColumnStyleWithBwh(DataGridViewColumn ControlCol)
        {
            ControlCol.Width = this.m_ColFixationWidth;
            ControlCol.ReadOnly = true;
            ControlCol.DefaultCellStyle.Font = new Font("宋体", 10F, FontStyle.Bold);
            ControlCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        /// <summary>
        /// 设置文本列样式
        /// </summary>
        /// <param name="ControlCol"></param>
        private void SetColumnStyleWithText(DataGridViewColumn ControlCol)
        {
            ControlCol.FillWeight = 100 / this.RowMeterNum;
            ControlCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ControlCol.ReadOnly = false;
            ControlCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }

        /// <summary>
        /// 设置列间距列样式
        /// </summary>
        /// <param name="ControlCol"></param>
        private void SetColumnStyleWithColSpace(DataGridViewColumn ControlCol)
        {
            ControlCol.Width = this.m_ColSpace;
            ControlCol.ReadOnly = true;
            ControlCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        #endregion


        #region ------------------------------------------表格单元格重绘------------------------------------------
        /// <summary>
        /// 重绘行距行和列距行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgw_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

            using
            (
            Brush gridBrush = new SolidBrush(this.Dgw.GridColor),
            backColorBrush = new SolidBrush(Dgw.BackgroundColor)
            )
                if (e.RowIndex % 2 == 0 && e.ColumnIndex >= 0)          //清除能被2整除的行的单元格边框
                {

                    {
                        using (Pen gridLinePen = new Pen(gridBrush))
                        {
                            // 清除单元格
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                            // 画 Grid 边线（仅画单元格的底边线）

                            if (e.ColumnIndex < Dgw.Columns.Count)
                            {
                                if (e.ColumnIndex % 4 != 0)         //同时不画作为列距的边框
                                {
                                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                                    e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                                    e.CellBounds.Bottom - 1);
                                }
                            }

                            e.Handled = true;
                        }
                    }
                }
                else if (e.RowIndex % 2 != 0 && e.ColumnIndex >= 0 && e.ColumnIndex % 4 == 0)       //如果行号不能被2整除，同时列号能被4整除的单元格边框，因为该单元格为间距列
                {
                    {
                        using (Pen gridLinePen = new Pen(gridBrush))
                        {
                            // 清除单元格
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                            // 画 Grid 边线（仅画单元格的右边线）

                            if (e.ColumnIndex < Dgw.Columns.Count - 1)       //如果不是最后一列才画右边线
                            {
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                            }
                            e.Handled = true;
                        }

                    }

                }
            Dgw.ClearSelection();
        }

        #endregion


        #region ----------------事件------------------------------

        /// <summary>
        /// 文本框回车事件，抛出当前文本框的内容和表位号
        /// </summary>
        /// <param name="Bwh">表位号</param>
        /// <param name="Value">文本框内容</param>
        public delegate bool Event_TxtInputOver(int Bwh, string Value);

        /// <summary>
        /// 信息输入完毕事件
        /// </summary>
        public event Event_TxtInputOver TxtInputOver;

        /// <summary>
        /// 多选框值变更事件，抛出当前选择框的内容和表位号
        /// </summary>
        /// <param name="Bwh"></param>
        /// <param name="Value"></param>
        public delegate void Event_CheckOver(int Bwh,bool Value);

        /// <summary>
        /// 要检，不检选择完毕抛出事件
        /// </summary>
        public event Event_CheckOver CheckOver;

        /// <summary>
        /// 抓取回车消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int _RowIndex = Dgw.CurrentCell.RowIndex;
            int _ColIndex = Dgw.CurrentCell.ColumnIndex;
            string strData = "";
            if (keyData == Keys.Enter)
            {
                if (_RowIndex % 2 != 0)             //必须是单行（数据行）
                {
                    if ((_ColIndex + 1) % 4 == 0)       //必须是文本框（表位文本框）
                    {
                        Dgw.EndEdit();
                        if (Dgw.CurrentCell.Value != null && Dgw.CurrentCell.Value.ToString().Trim() != string.Empty)
                        {
                            bool inputResult = false;
                            try
                            {
                                strData = Dgw.CurrentCell.Value.ToString().Trim();
                                strData = strData.Substring(0, strData.Length - 1);
                                inputResult = this.TxtInputOver((int)Dgw.CurrentCell.Tag, strData);
                            }
                            catch
                            { }
                            if (inputResult)
                            {
                                SetCheckBoxValue((int)Dgw.CurrentCell.Tag, true);           //设置要捡

                                try
                                {
                                    this.CheckOver((int)Dgw.CurrentCell.Tag, true);       //
                                }
                                catch
                                { }
                                this.SelectedBwh = (int)Dgw.CurrentCell.Tag + 1;            //设置焦点到下一个表位上
                            }
                            else
                            {
                                Dgw.CurrentCell.Value = "";
                                Dgw.BeginEdit(true);
                                //this.SelectedBwh = (int)Dgw.CurrentCell.Tag +1;            //设置焦点到下一个表位上
                                //this.SelectedBwh = (int)Dgw.CurrentCell.Tag ;            //设置焦点到下一个表位上

                            }
                            return true;        //退出不换行
                        }
                        else
                        {
                            Dgw.BeginEdit(true);
                            return true;    //退出不换行
                        }
                    }

                }
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }


        /// <summary>
        /// 鼠标按下，只处理CheckBox类型的单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgw_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Dgw.CurrentCell is DataGridViewCheckBoxCell)
            {
                if (Dgw[Dgw.CurrentCell.ColumnIndex + 2, Dgw.CurrentCell.RowIndex].Value == null || Dgw[Dgw.CurrentCell.ColumnIndex + 2, Dgw.CurrentCell.RowIndex].Value.ToString() == string.Empty)
                {
                    return;
                }
                
                Dgw.BeginEdit(false);

            }

        }

        /// <summary>
        /// 鼠标抬起，只处理CheckBox类型的单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgw_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Dgw.CurrentCell is DataGridViewCheckBoxCell)
            {
                Dgw.EndEdit();
                try
                {
                    this.CheckOver((int)Dgw.CurrentCell.Tag, (bool)Dgw.CurrentCell.Value);
                }
                catch
                { }
            }
        }


        /// <summary>
        /// 单元格获取焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgw_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex % 2 == 0) return;
            Dgw.BeginEdit(true);
        }
        /// <summary>
        /// 单元格离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgw_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Dgw.EndEdit();
        }

        #endregion 






    }
}

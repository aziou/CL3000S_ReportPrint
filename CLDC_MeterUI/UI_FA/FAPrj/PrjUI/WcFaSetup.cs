using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI
{
    public partial class WcFaSetup : UserControl
    {

        public delegate void EventPointCountChange(object sender, int Count);

        public event EventPointCountChange PointCountChange;

        private int _PointCount = 0;
        /// <summary>
        /// 偏差行号
        /// </summary>
        private int _intPcRpw = 0;
        /// <summary>
        /// 功率方向
        /// </summary>
        private CLDC_Comm.Enum.Cus_PowerFangXiang _Glfx;


        private CLDC_Comm.Enum.Cus_PowerYuanJian _Yj;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Yj">元件（H，A,B,C）</param>
        public WcFaSetup(CLDC_Comm.Enum.Cus_PowerYuanJian Yj)
        {
            InitializeComponent();

            
            _Yj = Yj;

            #region 初始化表格样式

            CLDC_DataCore.SystemModel.Item.csxIbDic _xIb = new CLDC_DataCore.SystemModel.Item.csxIbDic();
            _xIb.Load();
            List<string> _xIbs = _xIb.getxIb();
            for (int i = 0; i < _xIbs.Count; i++)
            {
                DGW_FA.Columns.Add("Col_" + _xIb.getxIbID(_xIbs[i]), _xIbs[i]);
                DGW_FA.Columns[i].Width = 50;
                DGW_FA.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                DGW_FA.Columns[i].Tag = _xIb.getxIbID(_xIbs[i]);
                DGW_FA.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }


            List<string> _Glyss = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysName();

            for (int i = 0; i < _Glyss.Count; i++)
            {
                DGW_FA.RowHeadersWidth = 90;
                DGW_FA.Rows.Add("");
                DGW_FA.Rows[i].HeaderCell.Value = _Glyss[i];
                DGW_FA.Rows[i].HeaderCell.Tag = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysID(_Glyss[i]);
            }

            DGW_FA.RowHeadersWidth = 90;
            DGW_FA.Rows.Add("");
            DGW_FA.Rows[_Glyss.Count].HeaderCell.Value = "标准偏差";
            _intPcRpw = _Glyss.Count;
            for (int i = 0; i < DGW_FA.Columns.Count; i++)
                DGW_FA.Rows[_Glyss.Count].Cells[i].Style.BackColor = this.BackColor;
            for (int i = 0; i < _Glyss.Count; i++)
            {
                DGW_FA.RowHeadersWidth = 90;
                DGW_FA.Rows.Add("");
                DGW_FA.Rows[i + _Glyss.Count + 1].HeaderCell.Value = _Glyss[i];
                DGW_FA.Rows[i + _Glyss.Count + 1].HeaderCell.Tag = CLDC_DataCore.Const.GlobalUnit.g_SystemConfig.GlysZiDian.getGlysID(_Glyss[i]);
            }
            DGW_FA.TopLeftHeaderCell.Value = _Yj== CLDC_Comm.Enum.Cus_PowerYuanJian.H?"合元件":_Yj.ToString() + "元件";

            #endregion
            this.DGW_FA.Rows[0].Cells[0].Selected = false;

        }
        /// <summary>
        /// 获取或设置功率方向值
        /// </summary>
        public CLDC_Comm.Enum.Cus_PowerFangXiang GLFX
        {
            get
            {
                return _Glfx;
            }
            set
            {
                _Glfx = value;
                if (this.Tag is CLDC_DataCore.Model.Plan.Plan_WcPoint)
                {
                    this.SetWcChecked((CLDC_DataCore.Model.Plan.Plan_WcPoint)this.Tag);
                }
            }
        }


        public CLDC_DataCore.Model.Plan.Plan_WcPoint GetFaInfo()
        {
            if (this.Tag is CLDC_DataCore.Model.Plan.Plan_WcPoint)
            {
                return this.Tag as CLDC_DataCore.Model.Plan.Plan_WcPoint;
            }
            else
            {
                return new CLDC_DataCore.Model.Plan.Plan_WcPoint(0, "");
            }
        }

        /// <summary>
        /// 重绘标准偏差文字行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGW_FA_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 对第1列相同单元格进行合并
            if (e.RowIndex == _intPcRpw && e.ColumnIndex >= 0)
            {
                using
                    (
                    Brush gridBrush = new SolidBrush(this.DGW_FA.GridColor),
                    backColorBrush = new SolidBrush(e.CellStyle.BackColor)
                    )
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        // 清除单元格
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                        // 画 Grid 边线（仅画单元格的底边线）

                        if (e.ColumnIndex < DGW_FA.Columns.Count)
                        {
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                            e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                            e.CellBounds.Bottom - 1);

                        }
                        if (e.ColumnIndex == DGW_FA.Columns.Count - 1)
                        {
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

                        }
                        e.Handled = true;
                    }
                }
            }
            DGW_FA.ClearSelection();
        }

        /// <summary>
        /// 方案项目选择，左键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGW_FA_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                DGW_FA.ClearSelection();
                return;
            }

            if (_Glfx == 0)
            {
                MessageBoxEx.Show(this,"请先选择功率方向后再进行方案配置...", "配置错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || e.RowIndex==_intPcRpw)
                return;
            //if (DGW_FA.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor == Color.DarkGreen)
            if (IsSelect(DGW_FA.Rows[e.RowIndex].Cells[e.ColumnIndex]))
            {
                //DGW_FA.DefaultCellStyle.SelectionBackColor = Color.White;
                //DGW_FA.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                SelectPoint(DGW_FA.Rows[e.RowIndex].Cells[e.ColumnIndex], false);
                PointCountChange(sender, _PointCount--);                //移除数量--
                string _PrjID = string.Format("{0}{1:D}{2:D}{3}{4}00"
                            , ((e.RowIndex / (float)_intPcRpw) < 1 ? "1" : "2")
                            , (int)_Glfx
                            , (int)_Yj
                            , DGW_FA.Rows[e.RowIndex].HeaderCell.Tag.ToString()
                            , DGW_FA.Columns[e.ColumnIndex].Tag.ToString());          
                ((CLDC_DataCore.Model.Plan.Plan_WcPoint)this.Tag).RemovePoint(_PrjID);       //移除一个点
            }
            else
            {
                //DGW_FA.DefaultCellStyle.SelectionBackColor = Color.DarkGreen;
                //DGW_FA.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.DarkGreen;
                SelectPoint(DGW_FA.Rows[e.RowIndex].Cells[e.ColumnIndex], true);
                PointCountChange(sender, _PointCount++);                //选中数量++
                ((CLDC_DataCore.Model.Plan.Plan_WcPoint)this.Tag).Add((CLDC_Comm.Enum.Cus_WcType)int.Parse((e.RowIndex / (float)_intPcRpw) < 1 ? "1" : "2")
                                      , _Glfx
                                      , _Yj
                                      , DGW_FA.Rows[e.RowIndex].HeaderCell.Value.ToString()
                                      , DGW_FA.Columns[e.ColumnIndex].HeaderCell.Value.ToString()
                                      , 0
                                      , 0);         //增加一个点
            }
            DGW_FA.Refresh();
        }
        /// <summary>
        /// 鼠标点击事件，只处理右键：平衡负载和不平衡负载差
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGW_FA_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if ((int)_Yj <= 1)
                {
                    return;
                }
                if (e.ColumnIndex < 0 || e.RowIndex < 0 || e.RowIndex >= _intPcRpw)
                {
                    return;
                }
                SelectPointFHC(DGW_FA.Rows[e.RowIndex].Cells[e.ColumnIndex], true);
                string _PrjID = string.Format("{0}{1:D}{2:D}{3}{4}00"
                            , ((e.RowIndex / (float)_intPcRpw) < 1 ? "1" : "2")
                            , (int)_Glfx
                            , (int)_Yj
                            , DGW_FA.Rows[e.RowIndex].HeaderCell.Tag.ToString()
                            , DGW_FA.Columns[e.ColumnIndex].Tag.ToString());
                ((CLDC_DataCore.Model.Plan.Plan_WcPoint)this.Tag).SetFHCYn(_PrjID);
            }

        }

        /// <summary>
        /// 设置方案列表选择情况
        /// </summary>
        /// <param name="CheckPoint"></param>
        public void SetWcChecked(CLDC_DataCore.Model.Plan.Plan_WcPoint CheckPoint)
        {
            _PointCount = 0;
            this.Tag = CheckPoint;
            if (CheckPoint.Count == 0)
            {
                PointCountChange(DGW_FA, _PointCount);
                for (int Col = 0; Col < DGW_FA.Columns.Count; Col++)
                {
                    for (int Row = 0; Row < DGW_FA.Rows.Count; Row++)
                    {
                        if (Row == _intPcRpw)
                            continue;
                        //DGW_FA.Rows[Row].Cells[Col].Style.BackColor = Color.White;
                        SelectPoint(DGW_FA.Rows[Row].Cells[Col], false);

                    }
                }
                return;
            }
            for (int Col = 0; Col < DGW_FA.Columns.Count; Col++)
            {
                for (int Row = 0; Row < DGW_FA.Rows.Count; Row++)
                {
                    if (Row == _intPcRpw)
                        continue;
                    string _PrjID = string.Format("{0}{1:D}{2:D}{3}{4}00"
                                                ,((Row/(float)_intPcRpw)<1?"1":"2")
                                                ,(int)_Glfx
                                                ,(int)_Yj
                                                ,DGW_FA.Rows[Row].HeaderCell.Tag.ToString()
                                                ,DGW_FA.Columns[Col].Tag.ToString());
                    if (CheckPoint.CheckedYn(_PrjID))
                    {
                        //DGW_FA.Rows[Row].Cells[Col].Style.BackColor = Color.DarkGreen;
                        SelectPoint(DGW_FA.Rows[Row].Cells[Col], true);
                        _PointCount++;              //选中数量++
                        if (CheckPoint.CheckedFHC(_PrjID))
                        {
                            SelectPointFHC(DGW_FA.Rows[Row].Cells[Col], true);
                        }
                    }
                    else
                    {
                        //DGW_FA.Rows[Row].Cells[Col].Style.BackColor = Color.White;
                        SelectPoint(DGW_FA.Rows[Row].Cells[Col], false);
                    }
                }
            }
            PointCountChange(DGW_FA, _PointCount);
        }
        /// <summary>
        /// 设置误差方案项目   （目前未使用！！）
        /// </summary>
        /// <param name="CheckPoint"></param>
        public void CreateWcChecked(ref CLDC_DataCore.Model.Plan.Plan_WcPoint CheckPoint)
        {
            if (_PointCount == 0)
                return;
            for (int Col = 0; Col < DGW_FA.Columns.Count; Col++)
            {
                for (int Row = 0; Row < DGW_FA.Rows.Count; Row++)
                {
                    if (Row == _intPcRpw)
                        continue;
                    //if (DGW_FA.Rows[Row].Cells[Col].Style.BackColor == Color.DarkGreen)
                    if (IsSelect(DGW_FA.Rows[Row].Cells[Col]))
                        CheckPoint.Add((CLDC_Comm.Enum.Cus_WcType)int.Parse((Row / (float)_intPcRpw) < 1 ? "1" : "2")
                                      , _Glfx
                                      , _Yj
                                      , DGW_FA.Rows[Row].HeaderCell.Value.ToString()
                                      , DGW_FA.Columns[Col].HeaderCell.Value.ToString()
                                      , 0
                                      , 0);
                        
                }
            }
        
        }

        /// <summary>
        /// 清空数据表
        /// </summary>
        public void ClearPrj()
        {
            for (int Col = 0; Col < DGW_FA.Columns.Count; Col++)
            {
                for (int Row = 0; Row < DGW_FA.Rows.Count; Row++)
                {
                    if (Row == _intPcRpw)
                        continue;
                    //DGW_FA.Rows[Row].Cells[Col].Style.BackColor = Color.White;
                    SelectPoint(DGW_FA.Rows[Row].Cells[Col],false);
                }
            }
        }

        public object[] getxIbName()
        {
            object[] _Tmp = new object[DGW_FA.Columns.Count];
            for (int i = 0; i < DGW_FA.Columns.Count; i++)
                _Tmp[i]=DGW_FA.Columns[i].HeaderText;
            return _Tmp;
        }


        /// <summary>
        /// 设置一个单元格选择状态
        /// </summary>
        /// <param name="cell">单元格</param>
        /// <param name="Select">是否选择</param>
        private void SelectPoint(DataGridViewCell cell, bool Select)
        {
            if (Select)
            {
                cell.Style.ForeColor = Color.Red;
                cell.Style.Font = new Font("宋体", 18, FontStyle.Bold);
                cell.Value = "√";
                //cell.Style.Font
                //cell.Style.Font.Bold = true;

            }
            else
            {
                cell.Style.ForeColor = Color.White;
                cell.Value = "";
            }
        }
        /// <summary>
        /// 设置一个单元格分合差状态
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="Select"></param>
        private void SelectPointFHC(DataGridViewCell cell, bool Select)
        {
            if (Select)
            {
                cell.Style.ForeColor = Color.LightSkyBlue;
                
            }
            else
            {
                cell.Style.ForeColor = Color.Red;
                
            }
        }
        /// <summary>
        /// 当前单元格是否被选中
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private bool IsSelect(DataGridViewCell cell)
        {
            return (cell.Value.ToString() == "√");
        }
        #region 元件检定点数量

        public int PointCount
        {
            get
            {
                return _PointCount;
            }
        }

        #endregion
        
    }


}

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms ;
using System.Drawing;
using System.Data;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// 将 Grid 单元格绑定系统其他控件
    /// 使用本类的方法将占用资源：
    /// 1、被绑定对象的 Tag 属性
    /// 2、被绑定单元格的 Tag 属性
    /// 3、Grid的Tag 属性
    /// 4、==== 目前对于滚动条事件未作处理 ====
    /// </summary>
    public class BindGridCellControl
    {

        #region 绑定 TextBox
        /// <summary>
        /// 将TextBox绑定到Grid某个单元格中
        /// 1、TextBox.Tag = "(RowIndex)|(ColIndex)" 不要改变它
        /// 2、单元格行高固定按 20px 计算
        /// 3、Cell.Tag = TextBox
        /// 4、Grid 的Tag 被占用
        /// </summary>
        /// <param name="Cell"></param>
        /// <returns></returns>
        public  TextBox BindTextBox(DataGridViewCell Cell)
        {
            DataGridView Grid   = Cell.DataGridView;

            //// 禁止用户改变Grid的所有列的列宽
            //Grid.AllowUserToResizeColumns = false;

            ////禁止用户改变Grid所有行的行高
            //Grid.AllowUserToResizeRows = false;
            int RowIndex        = Cell.RowIndex;
            int ColIndex        = Cell.ColumnIndex;

            TextBox txtBox      = new TextBox();
            txtBox.Name         = string.Format("{0}_TextBox_{1}_{2}"
                                , Grid.Name
                                , RowIndex
                                , ColIndex);

            Grid.Controls.Add(txtBox);
            txtBox.Height   = Grid.Rows[RowIndex].Height - 1;
            txtBox.Width    = Grid.Columns[ColIndex].Width - 1;
            txtBox.Tag      = String.Format("{0}|{1}",RowIndex ,ColIndex );
            txtBox.Parent   = Grid;
            Cell.Tag        = txtBox;

            txtBox.Location = GetCellPos(Grid, RowIndex, ColIndex);
            txtBox.TextChanged += new EventHandler(txtBox_TextChanged);

            Grid.CellValueChanged -= new DataGridViewCellEventHandler(Grid_CellValueChanged);
            Grid.CellValueChanged += new DataGridViewCellEventHandler(Grid_CellValueChanged);

            if (!(Grid.Tag is string) || (string)Grid.Tag != "0|0")
            {
                Grid.Scroll += new ScrollEventHandler(Grid_Scroll);
                Grid.Tag = "0|0";
            }
            return txtBox;
        }


        void txtBox_TextChanged(object sender, EventArgs e)
        {
            string strTag = ((TextBox)sender).Tag.ToString();
            int RowIndex = int.Parse(strTag.Split('|')[0]);
            int ColIndex = int.Parse(strTag.Split('|')[1]);

            ((DataGridView)((TextBox)sender).Parent).Rows[RowIndex].Cells[ColIndex].Value = ((TextBox)sender).Text;
        }

        #endregion

        #region 绑定 ComboBox
        /// <summary>
        /// 将ComboBox绑定到Grid某个单元格中
        /// 1、ComboBox.Tag = "(RowIndex)|(ColIndex)" 不要改变它
        /// 2、单元格行高固定按 20px 计算
        /// 3、Cell.Tag = ComboBox
        /// 4、Grid 的Tag 被占用
        /// </summary>
        /// <param name="Cell"></param>
        /// <returns></returns>
        public ComboBox BindComboBox(DataGridViewCell Cell)
        {
            DataGridView Grid = Cell.DataGridView;

            //// 禁止用户改变Grid的所有列的列宽
            //Grid.AllowUserToResizeColumns = false;

            ////禁止用户改变Grid所有行的行高
            //Grid.AllowUserToResizeRows = false;

            int RowIndex = Cell.RowIndex;
            int ColIndex = Cell.ColumnIndex;

            ComboBox cmbBox = new ComboBox();
            cmbBox.Name = string.Format("{0}_CmoboBox_{1}_{2}"
                                , Grid.Name
                                , RowIndex
                                , ColIndex);

            Grid.Controls.Add(cmbBox);
            cmbBox.Height = Grid.Rows[RowIndex].Height - 1;
            cmbBox.Width = Grid.Columns[ColIndex].Width - 1;
            cmbBox.Tag = String.Format("{0}|{1}", RowIndex, ColIndex);
            cmbBox.Parent = Grid;
            Cell.Tag = cmbBox;

            cmbBox.Location = GetCellPos(Grid, RowIndex, ColIndex);
            cmbBox.SelectedValueChanged += new EventHandler(cmbBox_SelectedValueChanged);
            cmbBox.SelectedIndexChanged += new EventHandler(cmbBox_SelectedIndexChanged);
            Grid.CellValueChanged -= new DataGridViewCellEventHandler(Grid_CellValueChanged);
            Grid.CellValueChanged += new DataGridViewCellEventHandler(Grid_CellValueChanged);

            if (!(Grid.Tag is string) || (string)Grid.Tag != "0|0")
            {
                Grid.Scroll += new ScrollEventHandler(Grid_Scroll);
                Grid.Tag = "0|0";
            }
            return cmbBox;
        }

        void cmbBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBox_SelectedValueChanged( sender,  e);
        }

        void cmbBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox CmbBox = (ComboBox)sender;
            if (CmbBox.SelectedIndex == -1) return;

            DataGridView Grid = (DataGridView)CmbBox.Parent;
            string strTag = CmbBox.Tag.ToString();
            int RowIndex = int.Parse(strTag.Split('|')[0]);
            int ColIndex = int.Parse(strTag.Split('|')[1]);

            if (CmbBox.DataSource == null)
            {
                Grid.Rows[RowIndex].Cells[ColIndex].Value = CmbBox.Items[CmbBox.SelectedIndex];
            }
            else
            {
                Grid.Rows[RowIndex].Cells[ColIndex].Value = CmbBox.SelectedValue;
            }
        }

        /// <summary>
        /// 将ComboBox绑定到Grid某个单元格中（单击单元格才显示出来）
        /// 1、ComboBox.Tag = "(RowIndex)|(ColIndex)" 不要改变它
        /// 2、单元格行高固定按 20px 计算
        /// 3、Cell.Tag = ComboBox
        /// 4、Grid 的Tag 被占用
        /// </summary>
        /// <param name="Cell"></param>
        /// <returns></returns>
        public ComboBox BindComboBoxOnClick(DataGridViewCell Cell)
        {
            DataGridView Grid = Cell.DataGridView;

            //// 禁止用户改变Grid的所有列的列宽
            //Grid.AllowUserToResizeColumns = false;

            ////禁止用户改变Grid所有行的行高
            //Grid.AllowUserToResizeRows = false;

            int RowIndex = Cell.RowIndex;
            int ColIndex = Cell.ColumnIndex;

            ComboBox cmbBox = new ComboBox();
            cmbBox.Name = string.Format("{0}_CmoboBox_{1}_{2}"
                                , Grid.Name
                                , RowIndex
                                , ColIndex);

            Grid.Controls.Add(cmbBox);
            cmbBox.Height = Grid.Rows[RowIndex].Height - 1;
            cmbBox.Width = Grid.Columns[ColIndex].Width - 1;
            cmbBox.Tag = String.Format("{0}|{1}", RowIndex, ColIndex);
            cmbBox.Parent = Grid;
            Cell.Tag = cmbBox;

            cmbBox.Location = GetCellPos(Grid, RowIndex, ColIndex);
            cmbBox.SelectedValueChanged += new EventHandler(cmbBox_SelectedValueChanged);
            cmbBox.SelectedIndexChanged += new EventHandler(cmbBox_SelectedIndexChanged);
            Grid.CellValueChanged -= new DataGridViewCellEventHandler(Grid_CellValueChanged);
            Grid.CellValueChanged += new DataGridViewCellEventHandler(Grid_CellValueChanged);

            if (!(Grid.Tag is string) || (string)Grid.Tag != "0|0")
            {
                Grid.Scroll += new ScrollEventHandler(Grid_Scroll);
                Grid.Tag = "0|0";
            }
            cmbBox.Visible = false;
            return cmbBox;
        }
        #endregion

        #region 绑定 CheckBox
        /// <summary>
        /// 将CheckBox绑定到Grid某个单元格中
        /// 1、CheckBox.Tag = "(RowIndex)|(ColIndex)" 不要改变它
        /// 2、单元格行高固定按 20px 计算
        /// 3、Cell.Tag = CheckBox
        /// 4、Grid 的Tag 被占用
        /// </summary>
        /// <param name="Cell"></param>
        /// <returns></returns>
        public CheckBox BindCheckBox(DataGridViewCell Cell)
        {
            DataGridView Grid = Cell.DataGridView;

            //// 禁止用户改变Grid的所有列的列宽
            //Grid.AllowUserToResizeColumns = false;

            ////禁止用户改变Grid所有行的行高
            //Grid.AllowUserToResizeRows = false;

            int RowIndex = Cell.RowIndex;
            int ColIndex = Cell.ColumnIndex;

            CheckBox  ChkBox = new CheckBox();
            ChkBox.Name = string.Format("{0}_ChkBox_{1}_{2}"
                                , Grid.Name
                                , RowIndex
                                , ColIndex);

            Grid.Controls.Add(ChkBox);
            ChkBox.Height = Grid.Rows[RowIndex].Height - 1;
            ChkBox.Width = Grid.Columns[ColIndex].Width - 1;
            ChkBox.Tag = String.Format("{0}|{1}", RowIndex, ColIndex);
            ChkBox.Parent = Grid;
            Cell.Tag = ChkBox;

            ChkBox.Location = GetCellPos(Grid, RowIndex, ColIndex);
            ChkBox.Click += new EventHandler(ChkBox_Click);

            Grid.CellValueChanged -= new DataGridViewCellEventHandler(Grid_CellValueChanged);
            Grid.CellValueChanged += new DataGridViewCellEventHandler(Grid_CellValueChanged);


            if (!(Grid.Tag is string) || (string)Grid.Tag !=  "0|0")
            {
                Grid.Scroll += new ScrollEventHandler(Grid_Scroll);
                Grid.Tag = "0|0";
            }
            return ChkBox;
        }

        void ChkBox_Click(object sender, EventArgs e)
        {
            CheckBox ChkBox = (CheckBox)sender;
            DataGridView Grid = (DataGridView)ChkBox.Parent;
            string strTag = ChkBox.Tag.ToString();
            int RowIndex = int.Parse(strTag.Split('|')[0]);
            int ColIndex = int.Parse(strTag.Split('|')[1]);
            Grid.Rows[RowIndex].Cells[ColIndex].Value = ChkBox.Checked;
        }
        #endregion


        #region 绑定 BindProgressBar
        /// <summary>
        /// 将CheckBox绑定到Grid某个单元格中
        /// 1、CheckBox.Tag = "(RowIndex)|(ColIndex)" 不要改变它
        /// 2、单元格行高固定按 20px 计算
        /// 3、Cell.Tag = CheckBox
        /// 4、Grid 的Tag 被占用
        /// </summary>
        /// <param name="Cell"></param>
        /// <returns></returns>
        public ProgressBar BindProgressBar(DataGridViewCell Cell)
        {
            DataGridView Grid = Cell.DataGridView;

            //// 禁止用户改变Grid的所有列的列宽
            //Grid.AllowUserToResizeColumns = false;

            ////禁止用户改变Grid所有行的行高
            //Grid.AllowUserToResizeRows = false;

            int RowIndex = Cell.RowIndex;
            int ColIndex = Cell.ColumnIndex;

            ProgressBar ChkBox = new ProgressBar();
            ChkBox.Name = string.Format("{0}_Process_{1}_{2}"
                                , Grid.Name
                                , RowIndex
                                , ColIndex);

            Grid.Controls.Add(ChkBox);
            ChkBox.Height = Grid.Rows[RowIndex].Height - 1;
            ChkBox.Width = Grid.Columns[ColIndex].Width - 1;
            ChkBox.Tag = String.Format("{0}|{1}", RowIndex, ColIndex);
            ChkBox.Parent = Grid;
            Cell.Tag = ChkBox;

            ChkBox.Location = GetCellPos(Grid, RowIndex, ColIndex);

            Grid.CellValueChanged -= new DataGridViewCellEventHandler(Grid_CellValueChanged);
            Grid.CellValueChanged += new DataGridViewCellEventHandler(Grid_CellValueChanged);

            if (!(Grid.Tag is string) || (string)Grid.Tag != "0|0")
            {
                Grid.Scroll += new ScrollEventHandler(Grid_Scroll);
                Grid.Tag = "0|0";
            }
            return ChkBox;
        }

        #endregion

        /// <summary>
        /// Grid 滚动条事件
        /// Grid.Tag = "X|Y"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Grid_Scroll(object sender, ScrollEventArgs e)
        {
            return;
            //if (e.Type != ScrollEventType.EndScroll) return;

            //DataGridView Grid = (DataGridView)sender;

            //string txtBoxName = "";
            //string CmbBoxName = "";
            //string ChkBoxName = "";
            
            //int ScrollX = 0;
            //int ScrollY = 0;
            ////水平滚动
            //if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            //{
            //    if (Grid.Tag is string)
            //    {
            //        string strTag = Grid.Tag.ToString();
            //        ScrollY = int.Parse(strTag.Split('|')[1]);
            //    }
            //    else
            //    {
            //        ScrollY = 0;
            //    }
            //    ScrollX = e.NewValue;
            //}
            //else if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            //{
            //    if (Grid.Tag is string)
            //    {
            //        string strTag = Grid.Tag.ToString();
            //        ScrollX  = int.Parse(strTag.Split('|')[0]);
            //    }
            //    else
            //    {
            //        ScrollX  = 0;
            //    }
            //    ScrollY = e.NewValue;
            //}
            //Grid.Tag = string.Format("{0}|{1}", ScrollX, ScrollY);
            ////Grid.s
          
            //for (int i = 0; i < Grid.Rows.Count; i++)
            //{
            //    for (int j = 0; j < Grid.Columns.Count; j++)
            //    {
            //        txtBoxName      = string.Format("{0}_TextBox_{1}_{2}", Grid.Name, i, j);
            //        object[] objs   = Grid.Controls.Find(txtBoxName, false);

            //        if (objs.Length < 1)
            //        {
            //            CmbBoxName = string.Format("{0}_CmoboBox_{1}_{2}", Grid.Name, i, j);
            //            objs = Grid.Controls.Find(CmbBoxName, false);
            //        }
            //        if (objs.Length < 1)
            //        {
            //            ChkBoxName = string.Format("{0}_ChkBox_{1}_{2}", Grid.Name, i, j);
            //            objs = Grid.Controls.Find(ChkBoxName, false);
            //        }

            //        if (objs.Length > 0)
            //        {
            //            Point Pos = GetCellPos(Grid, i, j);
            //            Pos.X -= ScrollX;
            //            Pos.Y -= ScrollY;
            //            ((Control)objs[0]).Location = Pos;
            //        }
                    
            //    }
            //}

        }

        #region 查找绑定到单元格的控件
        /// <summary>
        /// 查找绑定到单元格的控件
        /// </summary>
        /// <param name="Grid"></param>
        /// <param name="RowIndex"></param>
        /// <param name="ColIndex"></param>
        /// <returns></returns>
        public static Control GetBindControl(DataGridView Grid, int RowIndex, int ColIndex)
        {
            string txtBoxName = "";
            string CmbBoxName = "";
            string ChkBoxName = "";

            txtBoxName = string.Format("{0}_TextBox_{1}_{2}", Grid.Name, RowIndex , ColIndex );
            object[] objs = Grid.Controls.Find(txtBoxName, false);

            if (objs.Length < 1)
            {
                CmbBoxName = string.Format("{0}_CmoboBox_{1}_{2}", Grid.Name, RowIndex, ColIndex);
                objs = Grid.Controls.Find(CmbBoxName, false);
            }
            if (objs.Length < 1)
            {
                ChkBoxName = string.Format("{0}_ChkBox_{1}_{2}", Grid.Name, RowIndex, ColIndex);
                objs = Grid.Controls.Find(ChkBoxName, false);
            }
            if (objs.Length > 0)
            {
                return (Control)objs[0];
            }
            return null;
        }
        #endregion

        private Point GetCellPos(DataGridView Grid, int RowIndex, int ColIndex)
        {
            int PosX = 0;
            int PosY = Grid.Bounds.Top - 2;

            int RowHeight = 20;
            for (int i = 0; i < RowIndex; i++)
            {
                PosY += RowHeight;
            }
            if (Grid.Bounds.Top > RowHeight )
                PosY -= RowHeight;

            for (int i = 0; i < ColIndex; i++)
            {
                PosX += Grid.Columns[i].Width;
                if (i % 3 == 0)
                    PosX += 1;
            }
            PosX--;

            return new Point(PosX, PosY);
        }


        void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Control objControl = GetBindControl((DataGridView)sender, e.RowIndex, e.ColumnIndex);
            if (objControl == null) return;

            string strValue = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (objControl is TextBox)
            {
                TextBox TxtBox = (TextBox)objControl;
                if (TxtBox.Text != strValue)
                    TxtBox.Text = strValue;
            }
            else if (objControl is ComboBox)
            {
                ComboBox CmbBox = (ComboBox)objControl;
                for (int i = 0; i < CmbBox.Items.Count; i++)
                {
                    //直接设置 Item项的情况
                    if (CmbBox.DataSource == null)
                    {
                        if (CmbBox.Items[i].ToString() == strValue)
                        {
                            CmbBox.SelectedIndex = i;
                            return;
                        }
                    }
                    else
                    {
                        //通过 DataSource 设定下拉项的情况
                        CmbBox.SelectedValue = strValue;
                        return;
                    }
                }
                CmbBox.SelectedIndex = -1;
            }
            else if (objControl is CheckBox)
            {
                if (strValue.ToLower() == "1")
                    ((CheckBox)objControl).Checked = true;
                else if (strValue.ToLower() == "true")
                    ((CheckBox)objControl).Checked = true;
                else
                    ((CheckBox)objControl).Checked = false;
            }
            else if (objControl is ProgressBar)
            {
                ProgressBar ProgBar = (ProgressBar)objControl;
                if (ProgBar.Value.ToString() != strValue)
                    ProgBar.Value = int.Parse(strValue);
            }
        }


    }
}

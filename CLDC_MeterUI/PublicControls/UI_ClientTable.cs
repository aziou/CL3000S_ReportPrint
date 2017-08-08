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
        /// ������������
        /// </summary>
        public enum SetType
        {
            Error = 0,
            ��λ�� = 1,
            ÿ�б�λ�� = 2,
            ��ѡ���� = 3,
            �̶��п�� = 4,
            �о� = 5,
            �о� = 6,
            �ı��и� = 7
        }

        /// <summary>
        /// ��ѡ���п�ȣ�Ĭ��ֵ��
        /// </summary>
        const int CONST_COLCHECKBOX = 24;
        /// <summary>
        /// �̶��п�ȣ�Ĭ��ֵ��
        /// </summary>
        const int CONST_COLFIXATION = 24;
        /// <summary>
        /// �ı��п�ȣ�Ĭ��ֵ��
        /// </summary>
        const int CONST_COLTEXT = 200;
        /// <summary>
        /// �оࣨĬ��ֵ��
        /// </summary>
        const int CONST_COLSPACE = 10;
        /// <summary>
        /// �оࣨĬ��ֵ��
        /// </summary>
        const int CONST_ROWSPACE = 10;
        /// <summary>
        /// �ı��иߣ�Ĭ��ֵ��
        /// </summary>
        const int CONST_ROWTEXT = 20;
        /// <summary>
        /// ��λ����Ĭ��ֵ��
        /// </summary>
        const int CONST_METERNUM = 12;
        /// <summary>
        /// ÿ�з��ñ�λ��(Ĭ��ֵ)
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

        #region -------------------------------����------------------------------

        /// <summary>
        /// ��ѡ���п��
        /// </summary>
        private int m_ColCheckBoxWidth = CONST_COLCHECKBOX;
        /// <summary>
        /// �̶��п��
        /// </summary>
        private int m_ColFixationWidth = CONST_COLCHECKBOX;
        /// <summary>
        /// �о�
        /// </summary>
        private int m_ColSpace = CONST_COLSPACE;
        /// <summary>
        /// �о�
        /// </summary>
        private int m_RowSpace = CONST_ROWSPACE;
        /// <summary>
        /// �ı��и�
        /// </summary>
        private int m_RowHeight = CONST_ROWTEXT;

        /// <summary>
        /// ��λ��
        /// </summary>
        private int m_MeterNum = CONST_METERNUM;
        /// <summary>
        /// ÿ�з��ñ�λ��
        /// </summary>
        private int m_RowMeterNum = CONST_ROWMETERNUM;

        private Font m_TextFont = new Font("����",9F);

        /// <summary>
        /// �����ı���Ԫ������
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
        /// ��ȡ�����ö�ѡ���п��
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
        /// ��ȡ�����ù̶��п��
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
        /// ��ȡ�������о�
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
        /// ��ȡ�������о�
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
        /// ��ȡ�������ı��и�
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
        /// ��ȡ�����ñ�λ��(���ñ��������ٸ���λ�ı���)
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
        /// ��ȡ�����ñ�λ�������ñ��ÿ�в������ٸ���λ�ı���
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
        /// ��ȡ�����ñ�����ɫ
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
        /// ��ȡ�����õ�Ԫ����������ɫ
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
        /// ��ȡ�����õ�ǰѡ�еı�λ���ı���
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
        /// ��ȡ�������Ƿ�ֻ��
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


        #region --------------------------------------����----------------------------------

        #region ------------���û��ȡ�ı�������-------------
        /// <summary>
        /// �����ı�����ʾֵ
        /// </summary>
        /// <param name="Values">ֵ���飬���ֵ���鳤�Ȳ����ڱ�λ�����÷����򲻴���</param>
        public void SetTextValue(string[] Values)
        {
            if (Values.Length != this.m_MeterNum) return;

            for (int i = 0; i < Values.Length; i++)
            {
                this.SetTextValue(i + 1, Values[i]);
            }
        }
        /// <summary>
        /// �����ı�����ʾֵ
        /// </summary>
        /// <param name="Bwh">��λ��</param>
        /// <param name="Value">��Ӧ�ı�����ʾֵ</param>
        public void SetTextValue(int Bwh, string Value)
        {
            if (!Meter.ContainsKey(Bwh)) return;
            CellRowCol _Item = Meter[Bwh];
            Dgw[_Item.Col, _Item.Row].Value = Value;
        }

        /// <summary>
        /// ��ȡ�ı�������
        /// </summary>
        /// <param name="Bwh">��λ��</param>
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
        /// ��ȡ�����ı�������
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

        #region ---------------���û��ȡ�ı�����ɫ------------------
        /// <summary>
        /// �����ı��򱳾���ɫ
        /// </summary>
        /// <param name="Values">ֵ���飬���ֵ���鳤�Ȳ����ڱ�λ�����÷����򲻴���</param>
        public void SetTextBackColorValue(bool[] Values)
        {
            if (Values.Length != this.m_MeterNum) return;

            for (int i = 0; i < Values.Length; i++)
            {
                this.SetTextBackColorValue(i + 1, Values[i]);
            }
        }
        /// <summary>
        /// �����ı��򱳾���ɫ
        /// </summary>
        /// <param name="Bwh">��λ��</param>
        /// <param name="Value">����ʾ��ɫ������ʾ��ɫ</param>
        public void SetTextBackColorValue(int Bwh, bool Value)
        {
            if (!Meter.ContainsKey(Bwh)) return;
            CellRowCol _Item = Meter[Bwh];
            Dgw[_Item.Col, _Item.Row].Style.ForeColor = !Value?HeGe:BuHeGe;
        }

        /// <summary>
        /// ��ȡ�ı��򱳾���ɫ
        /// </summary>
        /// <param name="Bwh">��λ��</param>
        /// <returns></returns>
        public bool GetBackColorValue(int Bwh)
        {
            if (!Meter.ContainsKey(Bwh)) return false;
            CellRowCol _Item = Meter[Bwh];

            return Dgw[_Item.Col, _Item.Row].Style.ForeColor==BuHeGe?true:false;
        }
        /// <summary>
        /// ��ȡ�����ı��򱳾���ɫ
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
 

        #region -------------------------���û��ȡ��ѡ������------------------------
        /// <summary>
        /// ���ö�ѡ������ֵ
        /// </summary>
        /// <param name="Bwh">��λ��</param>
        /// <param name="Value">ֵ</param>
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
        /// �������ж�ѡ������ֵ
        /// </summary>
        /// <param name="Value">ֵ����</param>
        public void SetCheckBoxValue(bool[] Value)
        {
            if (Value.Length != m_MeterNum) return;
            for (int i = 0; i < Value.Length; i++)
            {
                this.SetCheckBoxValue(i + 1, Value[i]);
            }
        }
        /// <summary>
        /// ��ȡһ����λ�Ķ�ѡ���ֵ��һ����λ�Ƿ�Ҫ�죩
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
        /// ��ȡ���б�λ�Ķ�ѡ���ֵ(���б�λ�Ƿ�Ҫ��)
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


        #region ---------------------��Ԫ����˸---------------------------
        /// <summary>
        /// ʹ��Ԫ����˸
        /// </summary>
        /// <param name="Bwh">��λ��</param>
        public void FlickerCell(int Bwh)
        {
            this.FlickerCell(Bwh, Color.Red);
        }
        /// <summary>
        /// ʹ��Ԫ����˸
        /// </summary>
        /// <param name="Bwh">��λ��</param>
        /// <param name="FickerColor">��˸��ɫ</param>
        public void FlickerCell(int Bwh, Color FlickerColor)
        {
            this.FlickerCell(Bwh, FlickerColor, 3);
        }
        /// <summary>
        /// ʹ��Ԫ����˸
        /// </summary>
        /// <param name="Bwh">��λ��</param>
        /// <param name="FickerColor">��˸��ɫ</param>
        /// <param name="FickerNum">��˸����</param>
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

        #region ------------------------------------ˢ�±��----------------------------------------
        /// <summary>
        /// ���ò�ˢ�±��
        /// </summary>
        /// <param name="Bws">��λ��</param>
        /// <param name="RowBws">ÿ�б�λ��</param>
        public void RefreshGrid(int Bws, int RowBws)
        {
            if (Bws <= 0 || RowBws <= 0) return;

            this.m_MeterNum = Bws;

            this.m_RowMeterNum = RowBws;

            this.RefreshGrid();
        }

        /// <summary>
        /// ���ò�ˢ�±��
        /// </summary>
        /// <param name="Value">����ֵ</param>
        /// <param name="PramType">��������</param>
        public void RefreshGrid(int Value, SetType PramType)
        {
            switch (PramType)
            {
                case SetType.��λ��:
                    this.m_MeterNum = Value;
                    break;
                case SetType.ÿ�б�λ��:
                    this.m_RowMeterNum = Value;
                    break;
                case SetType.��ѡ����:
                    this.m_ColCheckBoxWidth = Value;
                    break;
                case SetType.�̶��п��:
                    this.m_ColFixationWidth = Value;
                    break;
                case SetType.�о�:
                    this.m_ColSpace = Value;
                    break;
                case SetType.�о�:
                    this.m_RowSpace = Value;
                    break;
                case SetType.�ı��и�:
                    this.m_RowHeight = Value;
                    break;
                default:
                    return;
            }

            this.RefreshGrid();

        }


        /// <summary>
        /// ���ˢ�£����������һ��Ҫ����ˢ��
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

            int _Cols = m_RowMeterNum * 4;          //һ����λ��3��һ���о��룬��ѡ�򣬱�λ�ţ��ı������һ�в���Ҫ�о�����
            Dgw.Rows.Clear();
            Dgw.Columns.Clear();

            #region -------------------------------������---------------------------------------------------

            for (int i = 0; i < _Cols; i++)
            {
                int _ColIndex;
                if (i % 4 != 0)
                {
                    continue;
                }

                _ColIndex = Dgw.Columns.Add("Col" + i.ToString(), "");

                this.SetColumnStyleWithColSpace(Dgw.Columns[_ColIndex]);           //�о���

                CreateColumnStyleWithCheckBox(_ColIndex + 1);

                _ColIndex = Dgw.Columns.Add("Col" + (i + 1).ToString(), "");

                this.SetColumnStyleWithBwh(Dgw.Columns[_ColIndex]);                  //��λ��

                _ColIndex = Dgw.Columns.Add("Col" + (i + 3).ToString(), "");

                this.SetColumnStyleWithText(Dgw.Columns[_ColIndex]);                //�ı�

                if (_ColIndex == _Cols - 1)         //���һ����Ϊ�м��
                {
                    _ColIndex = Dgw.Columns.Add("Col" + i.ToString(), "");

                    this.SetColumnStyleWithColSpace(Dgw.Columns[_ColIndex]);           //�о���
                }

            }

            #endregion

            int _Bwh = 0;

            Meter = new Dictionary<int, CellRowCol>();

            for (int i = 0; i < _Rows; i++)
            {
                int _RowIndex = Dgw.Rows.Add();

                if (i % 2 == 0)         //�����о���
                {
                    Dgw.Rows[_RowIndex].Height = this.m_RowSpace;
                    continue;
                }

                Dgw.Rows[_RowIndex].Height = this.m_RowHeight;         //����������

                for (int j = 0; j < _Cols; j++)
                {
                    if (j % 4 != 0)
                    {
                        continue;
                    }
                    _Bwh++;
                    if (_Bwh > this.m_MeterNum)         //���������λ������ʾ�κ�����
                    {
                        Dgw.Rows[_RowIndex].Cells[j + 1].Tag = 0;
                        Dgw.Rows[_RowIndex].Cells[j + 2].Value = "";           //�����λ��
                        Dgw.Rows[_RowIndex].Cells[j + 3].Tag = 0;
                    }
                    else
                    {
                        Dgw.Rows[_RowIndex].Cells[j + 1].Tag = _Bwh;
                        Dgw.Rows[_RowIndex].Cells[j + 1].ReadOnly = true;
                        Dgw.Rows[_RowIndex].Cells[j + 2].Value = _Bwh.ToString("D2");           //�����λ��
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
        /// ȫѡ����ѡ
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
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // ��ֹ��������.
            SetStyle(ControlStyles.DoubleBuffer, true); // ˫����
            this.RefreshGrid();
        }

        #region ---------------------------�������ʽ����----------------------------------------
        /// <summary>
        /// ����Checkbox����ʽ
        /// </summary>
        /// <param name="ColIndex"></param>
        private void CreateColumnStyleWithCheckBox(int ColIndex)
        {
            Dgw.Columns.AddRange(new DataGridViewCheckBoxColumn());
            Dgw.Columns[ColIndex].Width = this.m_ColCheckBoxWidth;
            Dgw.Columns[ColIndex].ReadOnly = false;

        }
        /// <summary>
        /// ���ù̶�����ʽ
        /// </summary>
        /// <param name="ControlCol"></param>
        private void SetColumnStyleWithBwh(DataGridViewColumn ControlCol)
        {
            ControlCol.Width = this.m_ColFixationWidth;
            ControlCol.ReadOnly = true;
            ControlCol.DefaultCellStyle.Font = new Font("����", 10F, FontStyle.Bold);
            ControlCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        /// <summary>
        /// �����ı�����ʽ
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
        /// �����м������ʽ
        /// </summary>
        /// <param name="ControlCol"></param>
        private void SetColumnStyleWithColSpace(DataGridViewColumn ControlCol)
        {
            ControlCol.Width = this.m_ColSpace;
            ControlCol.ReadOnly = true;
            ControlCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        #endregion


        #region ------------------------------------------���Ԫ���ػ�------------------------------------------
        /// <summary>
        /// �ػ��о��к��о���
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
                if (e.RowIndex % 2 == 0 && e.ColumnIndex >= 0)          //����ܱ�2�������еĵ�Ԫ��߿�
                {

                    {
                        using (Pen gridLinePen = new Pen(gridBrush))
                        {
                            // �����Ԫ��
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                            // �� Grid ���ߣ�������Ԫ��ĵױ��ߣ�

                            if (e.ColumnIndex < Dgw.Columns.Count)
                            {
                                if (e.ColumnIndex % 4 != 0)         //ͬʱ������Ϊ�о�ı߿�
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
                else if (e.RowIndex % 2 != 0 && e.ColumnIndex >= 0 && e.ColumnIndex % 4 == 0)       //����кŲ��ܱ�2������ͬʱ�к��ܱ�4�����ĵ�Ԫ��߿���Ϊ�õ�Ԫ��Ϊ�����
                {
                    {
                        using (Pen gridLinePen = new Pen(gridBrush))
                        {
                            // �����Ԫ��
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                            // �� Grid ���ߣ�������Ԫ����ұ��ߣ�

                            if (e.ColumnIndex < Dgw.Columns.Count - 1)       //����������һ�вŻ��ұ���
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


        #region ----------------�¼�------------------------------

        /// <summary>
        /// �ı���س��¼����׳���ǰ�ı�������ݺͱ�λ��
        /// </summary>
        /// <param name="Bwh">��λ��</param>
        /// <param name="Value">�ı�������</param>
        public delegate bool Event_TxtInputOver(int Bwh, string Value);

        /// <summary>
        /// ��Ϣ��������¼�
        /// </summary>
        public event Event_TxtInputOver TxtInputOver;

        /// <summary>
        /// ��ѡ��ֵ����¼����׳���ǰѡ�������ݺͱ�λ��
        /// </summary>
        /// <param name="Bwh"></param>
        /// <param name="Value"></param>
        public delegate void Event_CheckOver(int Bwh,bool Value);

        /// <summary>
        /// Ҫ�죬����ѡ������׳��¼�
        /// </summary>
        public event Event_CheckOver CheckOver;

        /// <summary>
        /// ץȡ�س���Ϣ
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
                if (_RowIndex % 2 != 0)             //�����ǵ��У������У�
                {
                    if ((_ColIndex + 1) % 4 == 0)       //�������ı��򣨱�λ�ı���
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
                                SetCheckBoxValue((int)Dgw.CurrentCell.Tag, true);           //����Ҫ��

                                try
                                {
                                    this.CheckOver((int)Dgw.CurrentCell.Tag, true);       //
                                }
                                catch
                                { }
                                this.SelectedBwh = (int)Dgw.CurrentCell.Tag + 1;            //���ý��㵽��һ����λ��
                            }
                            else
                            {
                                Dgw.CurrentCell.Value = "";
                                Dgw.BeginEdit(true);
                                //this.SelectedBwh = (int)Dgw.CurrentCell.Tag +1;            //���ý��㵽��һ����λ��
                                //this.SelectedBwh = (int)Dgw.CurrentCell.Tag ;            //���ý��㵽��һ����λ��

                            }
                            return true;        //�˳�������
                        }
                        else
                        {
                            Dgw.BeginEdit(true);
                            return true;    //�˳�������
                        }
                    }

                }
            }
            
            return base.ProcessCmdKey(ref msg, keyData);
        }


        /// <summary>
        /// ��갴�£�ֻ����CheckBox���͵ĵ�Ԫ��
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
        /// ���̧��ֻ����CheckBox���͵ĵ�Ԫ��
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
        /// ��Ԫ���ȡ�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgw_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex % 2 == 0) return;
            Dgw.BeginEdit(true);
        }
        /// <summary>
        /// ��Ԫ���뿪�¼�
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

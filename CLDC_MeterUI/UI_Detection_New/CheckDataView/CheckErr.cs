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
using System.Threading;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    /// <summary>
    /// ����������
    /// </summary>
    public partial class CheckErr : UserControl
    {
        /// <summary>
        /// ѡ���б����ί��
        /// </summary>
        /// <param name="RowIndex">��ǰѡ���к�</param>
        public delegate void Evt_GridSelectRowIndexChanged(int RowIndex);
        /// <summary>
        /// ѡ���б���󴥷����¼�
        /// </summary>
        public event Evt_GridSelectRowIndexChanged GridSelectRowIndexChanged;


        private Main ParentMain;
        /// <summary>
        /// ̨����
        /// </summary>
        private int _TaiID = 0;
        /// <summary>
        /// ̨������
        /// </summary>
        private int _TaiType = 0;


        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup = null;

       

        public CheckErr()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ���캯������ǰ�춨��ĿID
        /// </summary>
        /// <param name="parent">Main����</param>
        /// <param name="meterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨��</param>
        /// <param name="TaiID">̨����</param>
        /// <param name="taiType">̨������</param>
        public CheckErr(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
        {
            InitializeComponent();

            ParentMain = parent;

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            this.DoubleBuffered = true; //����˫����
            this._TaiID = TaiID;
            this._TaiType = taiType;

            
            this.InitializationGrid(meterGroup);

            this.RefreshGrid(meterGroup, CheckOrderID);

        }        


        /// <summary>
        /// ��ʼ�����ݲ˵�
        /// </summary>
        /// <param name="MeterGroup"></param>
        private void InitializationGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            int _Count = MeterGroup.MeterGroup.Count;

            if (Data_Error.Rows.Count != _Count)           //��ʼ���Ҳ����ݱ�
            {
                Data_Error.Rows.Clear();
                for (int i = 0; i < _Count; i++)
                {
                    int index = Data_Error.Rows.Add();
                    if ((index + 1) % 2 == 0)
                        Data_Error.Rows[index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    else
                        Data_Error.Rows[index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    Data_Error.Rows[index].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Error.Rows[index].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
            }

            Data_Error.Refresh();
        }

        /// <summary>
        /// ˢ�������б�
        /// </summary>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {

            _DnbGroup = MeterGroup;

            int FirstYJMeter = Main.GetFirstYaoJianMeterIndex(MeterGroup);
            if (CheckOrderID >= MeterGroup.CheckPlan.Count
                || !(MeterGroup.CheckPlan[CheckOrderID] is StPlan_WcPoint))
            {
                return;
            }

            StPlan_WcPoint _Item = (StPlan_WcPoint)MeterGroup.CheckPlan[CheckOrderID];

            #region ---------------------��̬�������ݱ����ʽ-------------------------------
            if (_Item.ToString().IndexOf("FHC") != -1)
            {
                //���ֺϲ�����ʽ
                if (Data_Error.Columns.Count != MeterGroup.WcCheckNumic + 7)          //���������ݱ�����������Ĭ������������Ҫɾ�����ڲ���
                {
                    for (int i = Data_Error.Columns.Count - 1; i >= 4; i--)
                    {
                        Data_Error.Columns.RemoveAt(i);
                    }

                    for (int i = 0; i < MeterGroup.WcCheckNumic; i++)
                    {
                        Data_Error.Columns.Add("Tmp_Col" + i.ToString(), "���" + (i + 1).ToString());
                    }
                    Data_Error.Columns.Add("Tmp_Pj", "ƽ��ֵ");

                    Data_Error.Columns.Add("Tmp_Hz", "����ֵ");

                    Data_Error.Columns.Add("Tmp_FhcRand", "��ֵ������");

                    Data_Error.Columns.Add("Tmp_Fhc", "��ֵ");

                    this.CreateColumnStyle();           //�����������ʽ
                }
            }
            else if (_Item.ToString().IndexOf("��׼ƫ��") == -1)       //������Ǳ�׼ƫ��
            {   //��ͨ�����ʽ
                if (Data_Error.Columns.Count != MeterGroup.WcCheckNumic + 6)          //���������ݱ�����������Ĭ������������Ҫɾ�����ڲ���
                {
                    for (int i = Data_Error.Columns.Count - 1; i >= 4; i--)
                    {
                        Data_Error.Columns.RemoveAt(i);
                    }

                    for (int i = 0; i < MeterGroup.WcCheckNumic; i++)
                    {
                        Data_Error.Columns.Add("Tmp_Col" + i.ToString(), "���" + (i + 1).ToString());
                    }
                    Data_Error.Columns.Add("Tmp_Pj", "ƽ��ֵ");

                    Data_Error.Columns.Add("Tmp_Hz", "����ֵ");

                    this.CreateColumnStyle();           //�����������ʽ
                }
            }
            else
            {   //ƫ������ʽ
                if (Data_Error.Columns.Count != MeterGroup.PcCheckNumic + 6)          //���������ݱ�����������Ĭ������������Ҫɾ�����ڲ���
                {
                    for (int i = Data_Error.Columns.Count - 1; i >= 4; i--)
                    {
                        Data_Error.Columns.RemoveAt(i);
                    }

                    for (int i = 0; i < MeterGroup.PcCheckNumic; i++)
                    {
                        Data_Error.Columns.Add("Tmp_Col" + i.ToString(), "���" + (i + 1).ToString());
                    }

                    Data_Error.Columns.Add("Tmp_Pj", "ƫ��ֵ");

                    Data_Error.Columns.Add("Tmp_Hz", "����ֵ");

                    this.CreateColumnStyle();           //�����������ʽ
                }

            }

            #endregion

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow Row = Data_Error.Rows[i];

                Row.Cells[1].Value = _MeterInfo.ToString();      //�����λ��

                if (!_MeterInfo.YaoJianYn)        //�������
                {

                    Row.Cells[0].Value = false;
                    if (_MeterInfo.Mb_chrBcs == String.Empty || _MeterInfo.Mb_chrBdj == String.Empty)       //������죬���ҳ������ߵȼ���Ϊ�գ��򽫹�ѡ��Ԫ������Ϊֻ��
                    {
                        Row.Cells[0].ReadOnly = true;
                    }
                    for (int j = 2; j < Row.Cells.Count; j++)
                    {
                        Row.Cells[j].Value = string.Empty;
                    }
                    continue;
                }

                //if (_MeterInfo.Mb_chrResult == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                //{
                //    Row.HeaderCell.Style.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                //}
                //else
                //{
                //    Row.HeaderCell.Style.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                //}

                Row.Cells[0].Value = true;

                string _Key = _Item.PrjID;

                if (_MeterInfo.MeterErrors.ContainsKey(_Key))          //�������ģ�����Ѿ����ڸõ������
                {
                    #region
                    try
                    {
                        if (_MeterInfo.MeterErrors[_Key].Me_chrWcJl == CLDC_DataCore.Const.Variable.CTG_BuHeGe)            //������ϸ��޸ĵ�ǰ�б�����ɫΪ��ɫ
                        {
                            Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                            foreach (DataGridViewCell cell in Row.Cells)
                            {
                                cell.ToolTipText = _MeterInfo.MeterErrors[_Key].AVR_DIS_REASON;
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
                    catch { }
                    try
                    {
                        string[] Arr_WcLimit = _MeterInfo.MeterErrors[_Key].Me_WcLimit.Split('|');      //�ֽ������
                        if (Arr_WcLimit.Length == 2)
                        {
                            Row.Cells[2].Value = string.Format("{0}  {1}", Arr_WcLimit[0], Arr_WcLimit[1]);       //��Ŀ�����  
                        }
                        else
                        {
                            Row.Cells[2].Value = "";
                        }
                    }
                    catch { }
                    try
                    {
                        Row.Cells[3].Value = _MeterInfo.MeterErrors[_Key].Me_chrWcJl;
                    }
                    catch { }
                    try
                    {
                        string[] Arr_Err = _MeterInfo.MeterErrors[_Key].Me_chrWcMore.Split('|');           //�ֽ����

                        if (Arr_Err.Length == -1) continue;
                        for (int j = 0; j < Arr_Err.Length; j++)
                        {
                            if (Data_Error.Columns.Count <= j + 4) break;           //�������С�ڵ�ǰ����������Զ��˳�
                            Row.Cells[j + 4].Value = Arr_Err[j];
                        }
                    }
                    catch { }
                    try
                    {
                        if (_Item.Dif_Err_Flag == 1)
                        {
                            Row.Cells[8].Value = _MeterInfo.MeterErrors[_Key].AVR_UPPER_LIMIT + " " + _MeterInfo.MeterErrors[_Key].AVR_LOWER_LIMIT;
                            Row.Cells[9].Value = _MeterInfo.MeterErrors[_Key].AVR_DIF_ERR_AVG;//AVR_DIF_ERR_AVG
                        }
                    }
                    catch { }
                    #endregion
                }
                else
                {
                    //Row.DefaultCellStyle.ForeColor = Color.Black;
                    for (int j = 2; j < Data_Error.Columns.Count; j++)
                        Row.Cells[j].Value = "";
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
        }

        /// <summary>
        /// ���ô�������е���ʽ
        /// </summary>
        /// <param name="Index">���±�</param>
        private void CreateColumnStyle()
        {

            if (Data_Error.Columns.Count <= 3) return;

            float _FillWeight = 100F / (Data_Error.Columns.Count - 3);

            for (int i = 3; i < Data_Error.Columns.Count; i++)
            {
                DataGridViewColumn _Column = Data_Error.Columns[i];
                _Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                _Column.FillWeight = _FillWeight;
                _Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            Data_Error.Refresh();

        }
        /// <summary>
        /// �ⲿ��������ˢ�£�
        /// </summary>
        /// <param name="meterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨ID</param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            this.RefreshGrid(meterGroup, CheckOrderID);
            Data_Error.Enabled = true;            
        }        
        /// <summary>
        /// ��λ��ѡ�У�ѡ���Ƿ�Ҫ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_Error_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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
                return;     //������ǵ�һ�У����˳�
            }
            try
            {
                this.GridSelectRowIndexChanged(e.RowIndex);
            }
            catch { }
            if (Data_Error[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //�����ֻ�����˳���
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_Error.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_Error.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_Error.EndEdit();
                }
                Data_Error.Enabled = true;
                //Comm.Function.TopWaiting.ShowWaiting("���ڸ���...");
                ParentMain.Evt_OnYaoJianChanged(_TaiType, _TaiID, e.RowIndex, Yn);
                //Comm.Function.TopWaiting.HideWaiting();
            }
            else
            {
                MessageBoxEx.Show(this,"û�д����¼�Evt_OnYaoJianChanged", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region ��ʾ������Ϣ�������

        public delegate void Evt_SetDnbInfoViewData(int Index);

        public event Evt_SetDnbInfoViewData SetDnbInfoViewData;

        private void Data_Error_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_Error.SelectedRows[0].Index);
            }
            catch
            {
                SetDnbInfoViewData(0);         //������ִ�����Զ�ѡ���һ����λ
            }
        }

        #endregion



        /// <summary>
        /// ��ȡ�����õ�ǰѡ�е��к�
        /// </summary>
        public int SelectRowIndex
        {
            get
            {
                if (Data_Error.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_Error.SelectedRows[0].Index;
            }
            set
            {
                if (Data_Error.IsHandleCreated)
                {
                    if (value >= 0 && Data_Error.Rows.Count > value)
                    {
                        Data_Error.Rows[value].Selected = true;
                        Data_Error.CurrentCell = Data_Error.Rows[value].Cells[1];
                    }
                }
            }
        }
    }
}

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
    public partial class CheckTeShu : UserControl
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

        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup = null;       

        private Main ParentMain;
        /// <summary>
        /// ̨����
        /// </summary>
        private int _TaiID = 0;
        /// <summary>
        /// ̨������
        /// </summary>
        private int _TaiType = 0;


        public CheckTeShu()
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
        public CheckTeShu(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
        {
            InitializeComponent();

            ParentMain = parent;

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            
            this.InitializationGrid(meterGroup);

            this.RefreshGrid(meterGroup, CheckOrderID);

        }     


        /// <summary>
        /// ��ʼ�����ݲ˵�
        /// </summary>
        /// <param name="MeterGroup"></param>
        private void InitializationGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            _DnbGroup = MeterGroup;

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            int _Count = MeterGroup.MeterGroup.Count;

            if (Data_Error.Rows.Count != _Count)
            {
                Data_Error.Rows.Clear();
                for (int i = 0; i < _Count; i++)
                {
                    int _RowIndex = Data_Error.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                        Data_Error.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    else
                        Data_Error.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    Data_Error.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Error.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;

                }
            }
        }

        /// <summary>
        /// ���ô�������е���ʽ
        /// </summary>
        /// <param name="Index">���±�</param>
        private void CreateColumnStyle(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, StPlan_SpecalCheck Item)
        {

            if (Data_Error.Columns.Count != Item.WcCheckNumic + 4 + 2)          //���������ݱ�����������Ĭ������������Ҫɾ�����ڲ��� ������+�̶���+ƽ��ֵ������ֵ��
            {
                for (int i = Data_Error.Columns.Count - 1; i > 3; i--)
                {
                    Data_Error.Columns.RemoveAt(i);
                }


                for (int i = 0; i < Item.WcCheckNumic; i++)
                {
                    Data_Error.Columns.Add("Tmp_Col" + i.ToString(), "���" + (i + 1).ToString());
                }

                Data_Error.Columns.Add("Tmp_Pj", "ƽ��ֵ");

                Data_Error.Columns.Add("Tmp_Hz", "����ֵ");

            }

            float _FillWeight = 100F / (Data_Error.Columns.Count - 4);

            for (int i = 4; i < Data_Error.Columns.Count; i++)
            {
                DataGridViewColumn _Column = Data_Error.Columns[i];
                _Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                _Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                _Column.FillWeight = _FillWeight;
            }
            Data_Error.Refresh();

        }

        /// <summary>
        /// ����ˢ��
        /// </summary>
        /// <param name="MeterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨��</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            if (!(MeterGroup.CheckPlan[CheckOrderID] is StPlan_SpecalCheck)) return;

            StPlan_SpecalCheck _Item = (StPlan_SpecalCheck)MeterGroup.CheckPlan[CheckOrderID];

            #region ---------------------��̬�������ݱ����ʽ------------------------------

            this.CreateColumnStyle(MeterGroup, _Item);           //�����������ʽ

            #endregion

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = Data_Error.Rows[i];

                _Row.Cells[1].Value = _MeterInfo.ToString();            //��λ��

                if (!_MeterInfo.YaoJianYn)   //�������
                {
                    _Row.Cells[0].Value = false;
                    if (_MeterInfo.Mb_chrBcs == String.Empty || _MeterInfo.Mb_chrBdj == String.Empty)       //������죬���ҳ������ߵȼ���Ϊ�գ��򽫹�ѡ��Ԫ������Ϊֻ��
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

                _Row.Cells[2].Value = _Item.ToString();             //��Ŀ����

                string _Key = "P_" + CheckOrderID.ToString();

                if (_MeterInfo.MeterSpecialErrs.ContainsKey(_Key))            //�������ģ�����Ѿ����ڸĵ������
                {
                    _Row.Cells[3].Value = _MeterInfo.MeterSpecialErrs[_Key].Mse_Result;
                    string[] Arr_Err = _MeterInfo.MeterSpecialErrs[_Key].Mse_Wc.Split('|');
                    if (Arr_Err.Length <= 1) continue;

                    for (int j = 0; j < Arr_Err.Length; j++)
                    {
                        if (Data_Error.Columns.Count <= j + 4) break;
                        _Row.Cells[j + 4].Value = Arr_Err[j];
                    }

                    if (_MeterInfo.MeterSpecialErrs[_Key].Mse_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //������ϸ���Ҫ�޸ĵ�ǰ�б�����ɫΪ��ɫ
                    {
                        _Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                    //}
                        foreach (DataGridViewCell cell in _Row.Cells)
                        {
                            cell.ToolTipText = MeterGroup.MeterGroup[i].MeterSpecialErrs[_Key].AVR_DIS_REASON;
                        }
                    }
                    else
                    {
                        _Row.DefaultCellStyle.ForeColor = Color.Black;
                    //}
                        foreach (DataGridViewCell cell in _Row.Cells)
                        {
                            cell.ToolTipText = string.Empty;
                        }
                    }

                }
                else
                {
                    //_Row.DefaultCellStyle.ForeColor = Color.Black;
                    for (int j = 3; j < Data_Error.Columns.Count; j++)
                    {
                        _Row.Cells[j].Value = "";
                    }
                }
                if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                {
                    _Row.DefaultCellStyle.ForeColor = Color.Red;

                }
                else
                {
                    _Row.DefaultCellStyle.ForeColor = Color.Black;

                }
            }
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
                Data_Error.Enabled = false;
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
                if(Data_Error.SelectedRows.Count > 0)
                    SetDnbInfoViewData(Data_Error.SelectedRows[0].Index);
                else
                    SetDnbInfoViewData(0);
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    /// <summary>
    /// ��Ե��ѹ�춨���ݵ���ʾ
    /// </summary>
    public partial class CheckInsulation : UserControl
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


        public CheckInsulation()
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
        public CheckInsulation(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (Data_Insulation.Rows.Count != _Count)
            {
                Data_Insulation.Rows.Clear();

                for (int i = 0; i < _Count; i++)
                {
                    int Index = Data_Insulation.Rows.Add();
                    if ((Index + 1) % 2 == 0)
                    {
                        Data_Insulation.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_Insulation.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_Insulation.Rows[Index].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Insulation.Rows[Index].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
            }
            Data_Insulation.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Data_Insulation.Refresh();

        }

        /// <summary>
        /// ����ˢ��
        /// </summary>
        /// <param name="MeterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨��</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            CLDC_DataCore.Struct.StInsulationParam _Item;
            //CLDC_DataCore.Struct.StQiDong _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StInsulationParam)
            {
                _Item = (CLDC_DataCore.Struct.StInsulationParam)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow Row = Data_Insulation.Rows[i];


                //��λ��
                Row.Cells[1].Value = _MeterInfo.ToString();

                if (!_MeterInfo.YaoJianYn)
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

                Row.Cells[0].Value = true;

                try
                {
                    Row.Cells[2].Value = _Item.ToString();

                    string _Key = string.Format("{0}{1}", ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.��Ƶ��ѹ����).ToString(), ((int)_Item.InsulationType).ToString());
                    if (MeterGroup.MeterGroup[i].MeterInsulations.ContainsKey(_Key))            //��������Ŀ����
                    {
                        if (MeterGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨)        //���������ֹͣ״̬������ʾ����
                        {
                            Row.Cells[3].Value = string.Format("{0:F}/{1:F}(��ǰ/��(��))", MeterGroup.MeterGroup[i].MeterInsulations[_Key].TestTime, MeterGroup.MeterGroup[i].MeterInsulations[_Key].Time);
                            Row.Cells[5].Value = MeterGroup.MeterGroup[i].MeterInsulations[_Key].Result.ToString();
                        }
                        else
                        {
                            Row.Cells[3].Value = "100%";
                            Row.Cells[4].Value = MeterGroup.MeterGroup[i].MeterInsulations[_Key].stringCurrent;
                            Row.Cells[5].Value = MeterGroup.MeterGroup[i].MeterInsulations[_Key].Result;
                        }
                        if (Row.Cells[5].Value != null && Row.Cells[5].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)          //����ǲ��ϸ�����ʾ��ɫ
                        { 
                            Row.DefaultCellStyle.ForeColor = Color.Red;

                            foreach (DataGridViewCell cell in Row.Cells)
                            {
                                cell.ToolTipText = MeterGroup.MeterGroup[i].MeterInsulations[_Key].Description;
                            }
                        }
                        else
                            Row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        Row.DefaultCellStyle.ForeColor = Color.Black;
                        Row.Cells[3].Value = string.Format("{0:F}/{1:F}(��ǰ/��(��))", 0 , _Item.Time);
                        Row.Cells[4].Value = string.Empty;
                        Row.Cells[5].Value = string.Empty;
                    }
                    //if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                    //    Row.DefaultCellStyle.ForeColor = Color.Red;
                    //else
                    //    Row.DefaultCellStyle.ForeColor = Color.Black;
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
            Data_Insulation.Enabled = true;            
        }

        private void Data_Insulation_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

            if (Data_Insulation[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //�����ֻ�����˳���
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_Insulation.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_Insulation.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_Insulation.EndEdit();
                }
                Data_Insulation.Enabled = false;
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

        private void Data_Insulation_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_Insulation.SelectedRows[0].Index);
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
                if (Data_Insulation.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_Insulation.SelectedRows[0].Index;
            }
            set
            {
                if (Data_Insulation.IsHandleCreated)
                {
                    if (value >= 0 && Data_Insulation.Rows.Count > value)
                    {
                        Data_Insulation.Rows[value].Selected = true;
                        Data_Insulation.CurrentCell = Data_Insulation.Rows[value].Cells[1];
                    }
                }
            }
        }        

    }
}

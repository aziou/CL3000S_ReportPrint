using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckPowerConsume : UserControl
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

        /// <summary>
        /// ���캯��
        /// </summary>
        public CheckPowerConsume()
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
        public CheckPowerConsume(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            int _Count = MeterGroup.MeterGroup.Count;

            if (dgv_Data.Rows.Count != _Count)
            {
                dgv_Data.Rows.Clear();

                for (int i = 0; i < _Count; i++)
                {
                    int Index = dgv_Data.Rows.Add();
                    if ((Index + 1) % 2 == 0)
                    {
                        dgv_Data.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        dgv_Data.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    dgv_Data.Rows[Index].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    dgv_Data.Rows[Index].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
            }
            dgv_Data.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_Data.Refresh();

        }



        /// <summary>
        /// ����ˢ��
        /// </summary>
        /// <param name="MeterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨��</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            CLDC_DataCore.Struct.StPowerConsume _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPowerConsume)
            {
                _Item = (CLDC_DataCore.Struct.StPowerConsume)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow Row = dgv_Data.Rows[i];

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

                    string _Key = string.Format("{0}{1}", ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.��������).ToString(), "11");

                    if (MeterGroup.MeterGroup[i].MeterPowers.ContainsKey(_Key))            //��������Ŀ����
                    {
                        if (MeterGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨)        //���������ֹͣ״̬������ʾ����
                        {

                            Row.Cells[3].Value = "���ڼ춨......";
                            Row.Cells[4].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_chrValue;

                        }
                        else
                        {
                            Row.Cells[3].Value = "�춨���";
                            Row.Cells[4].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_chrValue;

                            Row.Cells[5].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ua_ReactiveP;
                            Row.Cells[6].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ub_ReactiveP;
                            Row.Cells[7].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Uc_ReactiveP;

                            Row.Cells[8].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ua_ReactiveS;
                            Row.Cells[9].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ub_ReactiveS;
                            Row.Cells[10].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Uc_ReactiveS;

                            Row.Cells[11].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ia_ReactiveS;
                            Row.Cells[12].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ib_ReactiveS;
                            Row.Cells[13].Value = MeterGroup.MeterGroup[i].MeterPowers[_Key].Md_Ic_ReactiveS;

                            
                        }
                        if (Row.Cells[4].Value != null && Row.Cells[4].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)          //����ǲ��ϸ�����ʾ��ɫ
                        {
                            Row.DefaultCellStyle.ForeColor = Color.Red;

                            foreach (DataGridViewCell cell in Row.Cells)
                            {
                                cell.ToolTipText = MeterGroup.MeterGroup[i].MeterPowers[_Key].AVR_DIS_REASON;
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
                    else
                    {
                        Row.DefaultCellStyle.ForeColor = Color.Black;
                        Row.Cells[3].Value = "׼������";
                    }

                }
                catch (Exception ex)
                {
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
                }
            }
        }


        /// <summary>
        /// ����ˢ�£��ⲿ���÷���
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="CheckOrderID"></param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            this.RefreshGrid(meterGroup,CheckOrderID);
            dgv_Data.Enabled = true;
        }

        /// <summary>
        /// �����Ƿ�Ҫ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_Block_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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
            if (dgv_Data[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //�����ֻ�����˳���
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)dgv_Data.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    dgv_Data.EndEdit();
                }
                else
                {
                    Yn = true;
                    dgv_Data.EndEdit();
                }
                dgv_Data.Enabled = false;
                //Comm.Function.TopWaiting.ShowWaiting("���ڸ���...");
                ParentMain.Evt_OnYaoJianChanged(_TaiType, _TaiID, e.RowIndex, Yn);
                //Comm.Function.TopWaiting.HideWaiting();
            }
            else
            {
                MessageBox.Show("û�д����¼�Evt_OnYaoJianChanged", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region ��ʾ������Ϣ�������

        public delegate void Evt_SetDnbInfoViewData(int Index);

        public event  Evt_SetDnbInfoViewData SetDnbInfoViewData;

        private void Data_Block_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(dgv_Data.SelectedRows[0].Index);
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
                if (dgv_Data.SelectedRows.Count == 0)
                    return 0;
                else
                    return dgv_Data.SelectedRows[0].Index;
            }
            set
            {
                if (dgv_Data.IsHandleCreated)
                {
                    if (value >=0 && dgv_Data.Rows.Count > value)
                    {
                        dgv_Data.Rows[value].Selected = true;
                        dgv_Data.CurrentCell = dgv_Data.Rows[value].Cells[1];
                    }
                }
            }
        }
    }
}

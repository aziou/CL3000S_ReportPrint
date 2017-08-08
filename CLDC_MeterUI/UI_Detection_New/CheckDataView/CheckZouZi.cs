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
        /// ѡ���б����ί��
        /// </summary>
        /// <param name="RowIndex">��ǰѡ���к�</param>
        public delegate void Evt_GridSelectRowIndexChanged(int RowIndex);
        /// <summary>
        /// ѡ���б���󴥷����¼�
        /// </summary>
        public event Evt_GridSelectRowIndexChanged GridSelectRowIndexChanged;

        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup = null;

        /// <summary>
        /// �Ƿ���¼�����루���������Ч��
        /// </summary>
        private bool IsInputStartValue = false;


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
        /// ����ģ��
        /// </summary>
        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _MeterGroup = null;
        /// <summary>
        /// ��ǰ�춨�����
        /// </summary>
        private int _CheckOrderID = 0;


        public CheckZouZi()
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
        /// ����ˢ��
        /// </summary>
        /// <param name="MeterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨��</param>
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
                //��λ��
                _Row.Cells[1].Value = _MeterInfo.ToString();            //�����λ��

                if (!_MeterInfo.YaoJianYn)
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

                _Row.Cells[2].Value = _Item.ToString();     //��Ŀ����

                if (_Item.ZouZiMethod == CLDC_Comm.Enum.Cus_ZouZiMethod.��׼��)
                {
                    _Row.Cells[4].Value = string.Format("��ǰ:{0:F5}/��:{1:F}(��)", MeterGroup.NowMinute, _Item.UseMinutes);        //����
                }
                else if (_Item.ZouZiMethod == CLDC_Comm.Enum.Cus_ZouZiMethod.�ƶ����巨)
                {
                   _Row.Cells[4].Value = string.Format("��ǰ:{0:F}/��:{1:F}(��)", MeterGroup.NowMinute, _Item.UseMinutes);        //����
                }
                else if(_Item.ZouZiMethod == CLDC_Comm.Enum.Cus_ZouZiMethod.У�˳���)
                {
                    _Row.Cells[4].Value = string.Format("��ǰ:{0:F5}/��:{1:F}(��)", MeterGroup.NowMinute, _Item.UseMinutes);        //����
                }
                else
                {
                   _Row.Cells[4].Value = string.Format("��ǰ:{0:F}/��:{1:F}(��)", MeterGroup.NowMinute, _Item.UseMinutes);        //����
                }

                string _Key = _Item.itemKey;

                if (_MeterInfo.MeterZZErrors.ContainsKey(_Key))
                {
                    if (_MeterInfo.MeterZZErrors[_Key].Mz_chrQiMa == -1F)
                        _Row.Cells[3].Value = "";           //����
                    else
                        _Row.Cells[3].Value = _MeterInfo.MeterZZErrors[_Key].Mz_chrQiMa.ToString("F2");           //����
                    if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨)           //�����ͣ��״̬��ֱ�Ӹ�ֵ����100%
                    {
                        _Row.Cells[4].Value = "100%";
                    }
                    //������
                    _Row.Cells[7].Value = _MeterInfo.MeterZZErrors[_Key].AVR_STANDARD_METER_ENERGY;
                    _Row.Cells[6].Value = _MeterInfo.MeterZZErrors[_Key].Mz_chrPules;
                    
                    //ֹ��
                    if (_MeterInfo.MeterZZErrors[_Key].Mz_chrZiMa != -1F && _MeterInfo.MeterZZErrors[_Key].Mz_chrZiMa.ToString().Length > 0)
                    {
                        _Row.Cells[5].Value = _MeterInfo.MeterZZErrors[_Key].Mz_chrZiMa.ToString("F2");         //ֹ��
                    }
                    else
                    {
                        _Row.Cells[5].Value = "";
                    }
                    if (_MeterInfo.MeterZZErrors[_Key].Mz_chrWc != null && _MeterInfo.MeterZZErrors[_Key].Mz_chrWc.Length > 0)
                    {
                        _Row.Cells[8].Value = _MeterInfo.MeterZZErrors[_Key].Mz_chrWc;             //���
                    }

                    if (_MeterInfo.MeterZZErrors[_Key].Mz_chrJL != null && _MeterInfo.MeterZZErrors[_Key].Mz_chrJL.Length > 0)
                    {
                        _Row.Cells[9].Value = _MeterInfo.MeterZZErrors[_Key].Mz_chrJL;         //����
                    }

                    //if (_MeterInfo.MeterZZErrors[_Key].Mz_Result != null && _MeterInfo.MeterZZErrors[_Key].Mz_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                    //{
                    //    _Row.DefaultCellStyle.ForeColor = Color.Red;                //������ϸ�������Ϊ��ɫ
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
        /// �ⲿ����ˢ���¼�
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="CheckOrderID"></param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {

            this.RefreshGrid(meterGroup, CheckOrderID);

            if (meterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨)
            {
                Table_CheckDns.Enabled = false;
            }

            Data_ZouZi.Enabled = true;

        }


        /// <summary>
        /// ¼�����롢ֹ���¼�(���¼����ڵ������в��õ�)
        /// </summary>
        /// <param name="IsStartNumber">�Ƿ���¼������</param>
        /// <returns></returns>
        public bool Fun_InputZZNumber(bool IsStartNumber)
        {
            //if (BtnButton == null) return false;

            //BtnButton.Text = "¼�����";

            //BtnButton.Visible = true;

            IsInputStartValue = IsStartNumber;

            Table_CheckDns.Enabled = true;
            int RowIndex = _MeterGroup.GetFirstYaoJianMeterBwh();  //��ȡ��һ����ǰҪ���λ
            if (IsStartNumber)
            {
                Data_ZouZi.Columns[3].ReadOnly = false;         //������¼��
                Data_ZouZi.Rows[RowIndex].Cells[3].Selected = true;        //���㷿����һ����Ԫ��
                Lab_Dns.Text = "��ͬ���룺";
            }
            else
            {
                Data_ZouZi.Columns[5].ReadOnly = false;         //��ֹ��¼��
                Data_ZouZi.Rows[RowIndex].Cells[5].Selected = true;    //����ŵ���һ����Ԫ��
                Chk_MathAdd.Visible = true;
                Chk_MathAdd.Checked = false;
                Lab_Dns.Text = "��ֹͬ�룺";
            }
            //Data_ZouZi.BeginEdit(true);
            return true;

        }


        bool IsDoComplated = false;
        /// <summary>
        /// ¼�����롢ֹ�����(���¼����ڵ����汾�в��õ�)
        /// </summary>
        /// <param name="sender">��ǰ��ť</param>
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
                        MessageBoxEx.Show(this, "���������һ�����֣�����������...", "�������", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBoxEx.Show(this, "ֹ�������һ�����֣�����������...", "�������", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBoxEx.Show(this, "����ʧ��!");
                    return;
                }
                Table_CheckDns.Enabled = false;
            }
        }


        /// <summary>
        /// ������ֹ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_ZouZi_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.BeginInvoke(new Evt_CellEndEdit(CellEndEdit), e.RowIndex, e.ColumnIndex);
        }
        /// <summary>
        /// ������ֹ���ί��
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <param name="ColIndex"></param>
        delegate void Evt_CellEndEdit(int RowIndex, int ColIndex);
        /// <summary>
        /// ������ֹ���������ݺϷ��Լ����Զ��ƶ�����һ�У��������ʹ����һ��ί�д�����Ϊ�ڵ�ǰ��Ԫ��༭��ɺ���Ҫ�ȴ�����CellSelectedChange�¼���ɺ���ܴ���
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
                MessageBoxEx.Show(this,"�����ֹ�������һ�����֣�����������...", "�������", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (CLDC_DataCore.Const.GlobalUnit.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨) return;
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
            if (Data_ZouZi[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //�����ֻ�����˳���
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

        private void Data_ZouZi_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_ZouZi.SelectedRows[0].Index);
            }
            catch
            {
                SetDnbInfoViewData(0);         //������ִ�����Զ�ѡ���һ����λ
            }
        }

        #endregion

        /// <summary>
        /// �׶�¼������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmd_Ok_Click(object sender, EventArgs e)
        {
            this.Btn_DoComplated_Click();
        }

        /// <summary>
        /// ����ֹ��ȫ����ͬ
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
                            Data_ZouZi.Rows[i].Cells[3].Value = Txt_Dns.Text;           //����
                        }
                        else
                            {
                                Data_ZouZi.Rows[i].Cells[3].Value = "";           //����
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
                                Data_ZouZi.Rows[i].Cells[5].Value = Txt_Dns.Text;           //ֹ��
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
        /// ��ȡ�����õ�ǰѡ�е��к�
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
        /// ֹ��=������ı������ݻ���ֹ��=�ı�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_MathAdd_CheckedChanged(object sender, EventArgs e)
        {
            this.Txt_Dns_TextChanged(Txt_Dns, e);
        }


    }
}

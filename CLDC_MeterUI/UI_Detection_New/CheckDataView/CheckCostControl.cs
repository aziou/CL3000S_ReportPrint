using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckCostControl : UserControl
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

        private  CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup = null;


        private Main ParentMain;
        /// <summary>
        /// ̨����
        /// </summary>
        private int _TaiID = 0;
        /// <summary>
        /// ̨������
        /// </summary>
        private int _TaiType = 0;

        public CheckCostControl()
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
        public CheckCostControl(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (Data_Cost.Rows.Count != _Count)
            {
                Data_Cost.Rows.Clear();
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)          //���ݵ���̨��λ����ӱ�λ��
                {
                    int _RowIndex = Data_Cost.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                    {
                        Data_Cost.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_Cost.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_Cost.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Cost.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
                Data_Cost.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Data_Cost.Refresh();
            }
        }

        /// <summary>
        /// ����ˢ��
        /// </summary>
        /// <param name="MeterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨��</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {

            CLDC_DataCore.Struct.StPlan_CostControl _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_CostControl)
            {
                _Item = (CLDC_DataCore.Struct.StPlan_CostControl)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = Data_Cost.Rows[i];
                //��λ��
                _Row.Cells[1].Value = _MeterInfo.ToString();            //�����λ��

                if (!_MeterInfo.YaoJianYn)           //�������
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

                _Row.Cells[2].Value = _Item.ToString();

                if (Data_Cost.Tag == null) return;

                if (_MeterInfo.MeterCostControls == null)
                    _MeterInfo.MeterCostControls = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFK>();
                if (_MeterInfo.MeterCostControls.ContainsKey(Data_Cost.Tag.ToString()))           //��������д���ֵ��ô����Ҫ�������ݣ�����ط����ֵ���Ǻϸ���߲��ϸ���Ϊȡ�Ķ��Ǵ��ţ����Ǵ�����ֵ��С���
                {
                    _Row.Cells[3].Value = "100%";
                    _Row.Cells[4].Value = _MeterInfo.MeterCostControls[Data_Cost.Tag.ToString()].Mfk_chrJL;
                    if (MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.�춨 ||
                        MeterGroup.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.�����춨)
                    {
                        switch (_Item.CostControlPrjID)
                        {                            
                            case "001":
                                _Row.Cells[3].Value = _MeterInfo.MeterCostControls[Data_Cost.Tag.ToString()].Mfk_chrData;
                                break;
                            default:
                                _Row.Cells[3].Value = _MeterInfo.MeterCostControls[Data_Cost.Tag.ToString()].Mfk_chrData;
                                break;
                        }
                    }
                    if (_Row.Cells[4].Value != null)
                    {
                        if (_Row.Cells[4].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //���ϸ��޸ĵ�ǰ�б�����ɫ
                        {
                            _Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                            foreach (DataGridViewCell cell in _Row.Cells)
                            {
                                cell.ToolTipText = _MeterInfo.MeterCostControls[Data_Cost.Tag.ToString()].AVR_DIS_REASON;
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
                else
                {
                    //_Row.DefaultCellStyle.ForeColor = Color.Black;
                    _Row.Cells[4].Value = "";
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

            #region -----------------------------------------����ҳˢ��-------------------------------------------
            if (Tab_CostControl.TabPages.Count != 2) return;           //���û�и�������ҳ�򷵻�

            Control _Control = Tab_CostControl.TabPages[1].Controls[0];

            
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewESAMDataReturn)            //ESAM���ݻس����ݱ�
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewESAMDataReturn)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserControlData)            //Զ�̿������ݱ�
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserControlData)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLoadSwitchData)            //���Ͽ������ݱ�
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLoadSwitchData)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewDirectCloseTrip)            //Զ�̿���ֱ�Ӻ�բ
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewDirectCloseTrip)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewWaringData)                 //��������
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewWaringData)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyUpdateData)                 //��Կ����
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyUpdateData)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyRecoveryData)                 //��Կ�ָ�
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyRecoveryData)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewControlFunction )                 //��Կ����
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewControlFunction)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime)            //����Ƿ��ʵ�ۼ���
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff)            //����Ƿ��ʵ�ۼ���
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLeftEnergyAccuracy)                 //ʣ������ݼ�׼ȷ��
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLeftEnergyAccuracy)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewPresetContent)                 //Ԥ���������� @C_B
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewPresetContent)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }

            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeLocalMode)                
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeLocalMode)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            //
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeRemoteMode)
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeRemoteMode)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }

            //
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserCardInitUser)
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserCardInitUser)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewOverdrawData)
            {
                ((CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewOverdrawData)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }

            #endregion
        }

        /// <summary>
        /// ˢ�������¼�
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="taiType"></param>
        /// <param name="taiId"></param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            if (!(meterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_CostControl)) return;

            CLDC_DataCore.Struct.StPlan_CostControl _Item = (CLDC_DataCore.Struct.StPlan_CostControl)meterGroup.CheckPlan[CheckOrderID];

            bool bFind = false;

            if (Tab_CostControl.TabPages.Count > 1)            //���Tab��ҳ������1���Ǳ�ʾ���ڶ�̬���ӵ�����ҳ
            {
                if (Data_Cost.Tag != null && Data_Cost.Tag.ToString() == _Item.CostControlPrjID)
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_CostControl.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_CostControl.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                Data_Cost.Tag = _Item.CostControlPrjID;          //��IDֵ�ŵ������б��Tag�У�������ˢ��ʹ��

                switch (_Item.CostControlPrjID)
                {
                    case "002":
                        {
                            Tab_CostControl.TabPages.Add("Զ�̿�������");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserControlData _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserControlData(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "003":
                        {
                            Tab_CostControl.TabPages.Add("������������");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewWaringData _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewWaringData(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "007":
                        {
                            Tab_CostControl.TabPages.Add("ESAM���ݻس�����");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewESAMDataReturn _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewESAMDataReturn(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "009":
                        {
                            Tab_CostControl.TabPages.Add("ʣ������ݼ�׼ȷ��");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLeftEnergyAccuracy _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLeftEnergyAccuracy(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "011":
                        {
                            Tab_CostControl.TabPages.Add("���ɿ�������");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLoadSwitchData _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewLoadSwitchData(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "013":
                        {
                            Tab_CostControl.TabPages.Add("��Կ��������");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyUpdateData _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyUpdateData(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "014":
                        {
                            Tab_CostControl.TabPages.Add("��Կ�ָ�����");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyRecoveryData _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewKeyRecoveryData(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "015":
                        {
                            Tab_CostControl.TabPages.Add("���ƹ�������");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewControlFunction _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewControlFunction(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "016":
                        {
                            Tab_CostControl.TabPages.Add("���ݵ�ۼ��");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff stepPrice = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(stepPrice);
                            stepPrice.Dock = DockStyle.Fill;
                            stepPrice.Margin = new System.Windows.Forms.Padding(0);
                        }
                        break;
                    case "017":
                        {
                            Tab_CostControl.TabPages.Add("���ʵ�ۼ��");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime ratesPrice = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(ratesPrice);
                            ratesPrice.Dock = DockStyle.Fill;
                            ratesPrice.Margin = new System.Windows.Forms.Padding(0);
                        }
                        break;
                    case "018":
                        {
                            Tab_CostControl.TabPages.Add("Զ�̿���ֱ�Ӻ�բ����");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewDirectCloseTrip _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewDirectCloseTrip(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "022": //@C_B
                        {
                            Tab_CostControl.TabPages.Add("Ԥ����������");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewPresetContent _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewPresetContent(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "023":
                        {
                            Tab_CostControl.TabPages.Add("����ģʽ�л�Զ��ģʽ");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeRemoteMode _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeRemoteMode(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                        }
                        break;
                    case "024":
                        {
                            Tab_CostControl.TabPages.Add("Զ��ģʽ�л�����ģʽ");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeLocalMode _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewChangeLocalMode(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                        }
                        break;
                    case "025":
                        {
                            Tab_CostControl.TabPages.Add("�û�������");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserCardInitUser _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewUserCardInitUser(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                        }
                        break;
                    case "026":
                        {
                            Tab_CostControl.TabPages.Add("͸֧����");
                            CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewOverdrawData _View = new CLDC_MeterUI.UI_Detection_New.CostControlDataView.ViewOverdrawData(_Item);
                            Tab_CostControl.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                        }
                        break;
                        

                }
            }

            this.RefreshGrid(meterGroup, CheckOrderID);

            Data_Cost.Enabled = true;
        }

        private void Data_Cost_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

            if (Data_Cost[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //�����ֻ�����˳���
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_Cost.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_Cost.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_Cost.EndEdit();
                }
                Data_Cost.Enabled = false;
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

        public event Evt_SetDnbInfoViewData SetDnbInfoViewData;

        private void Data_Cost_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_Cost.SelectedRows[0].Index);
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
                if (Data_Cost.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_Cost.SelectedRows[0].Index;
            }
            set
            {
                if (Data_Cost.IsHandleCreated)
                {
                    if (value >= 0 && Data_Cost.Rows.Count > value)
                    {
                        Data_Cost.Rows[value].Selected = true;
                        Data_Cost.CurrentCell = Data_Cost.Rows[value].Cells[1];
                    }
                }
            }
        }

        /// <summary>
        /// ���������Ժ��л� ui_Popup_MeterIndex ���л������ϸ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_Cost_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CLDC_DataCore.Const.GlobalUnit.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨 && e.ColumnIndex == 0)
            {
                if (ParentMain.Evt_OnYaoJianChanged != null)
                {
                    bool Yn;
                    if ((bool)Data_Cost.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                    {
                        Yn = false;
                        Data_Cost.EndEdit();
                    }
                    else
                    {
                        Yn = true;
                        Data_Cost.EndEdit();
                    }
                    Data_Cost.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Yn;
                    _DnbGroup.MeterGroup[e.RowIndex].YaoJianYn = Yn;
                    Data_Cost.Enabled = false;
                    //Comm.Function.TopWaiting.ShowWaiting("���ڸ���...");
                    ParentMain.Evt_OnYaoJianChanged(_TaiType, _TaiID, e.RowIndex, Yn);
                    //Comm.Function.TopWaiting.HideWaiting();
                }
                else
                {
                    MessageBox.Show("û�д����¼�Evt_OnYaoJianChanged", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (Data_Cost.SelectedRows.Count < 1) return;
            
        }

        private void Data_Cost_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Data_Cost_CellClick(Data_Cost, e);
            
        }

    }
}

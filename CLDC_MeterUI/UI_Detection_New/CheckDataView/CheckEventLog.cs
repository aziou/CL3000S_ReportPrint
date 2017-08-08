using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckEventLog : UserControl
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

        public CheckEventLog()
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
        public CheckEventLog(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (Data_EventLog.Rows.Count != _Count)
            {
                Data_EventLog.Rows.Clear();
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)          //���ݵ���̨��λ����ӱ�λ��
                {
                    int _RowIndex = Data_EventLog.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                    {
                        Data_EventLog.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_EventLog.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_EventLog.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_EventLog.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
                Data_EventLog.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Data_EventLog.Refresh();
            }
        }

        /// <summary>
        /// ����ˢ��
        /// </summary>
        /// <param name="MeterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨��</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {

            CLDC_DataCore.Struct.StPlan_EventLog _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_EventLog)
            {
                _Item = (CLDC_DataCore.Struct.StPlan_EventLog)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = Data_EventLog.Rows[i];
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

                if (Data_EventLog.Tag == null) return;

                if (_MeterInfo.MeterSjJLgns == null)
                    _MeterInfo.MeterSjJLgns = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSjJLgn>();

                if (_MeterInfo.MeterSjJLgns.ContainsKey(Data_EventLog.Tag.ToString()))           //��������д���ֵ��ô����Ҫ�������ݣ�����ط����ֵ���Ǻϸ���߲��ϸ���Ϊȡ�Ķ��Ǵ��ţ����Ǵ�����ֵ��С���
                {
                    _Row.Cells[3].Value = "100%";
                    _Row.Cells[4].Value = _MeterInfo.MeterSjJLgns[Data_EventLog.Tag.ToString()].ItemConc;
                    
                    //if (_Row.Cells[4].Value != null)
                    //{
                    //    if (_Row.Cells[4].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //���ϸ��޸ĵ�ǰ�б�����ɫ
                    //    {
                    //        _Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                    //    }
                    //    else
                    //    {
                    //        _Row.DefaultCellStyle.ForeColor = Color.Black;
                    //    }
                    //}
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
            if (Tab_EventLog.TabPages.Count != 2) return;           //���û�и�������ҳ�򷵻�

            Control _Control = Tab_EventLog.TabPages[1].Controls[0];

            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseVoltage)         //ʧѹ��¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseVoltage)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseFullVoltage)         //ȫʧѹ��¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseFullVoltage)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverVoltage)         //��ѹ��¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverVoltage)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventUnderVoltage)         //Ƿѹ��¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventUnderVoltage)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseCurrent)            //ʧ����¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseCurrent)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverCurrent)         //������¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverCurrent)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopCurrent)               //������¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopCurrent)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverLoad)            //���ؼ�¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverLoad)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopPhase)               //�����¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopPhase)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }            
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventACdump)            //�����¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventACdump)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPrograme)            //��̼�¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPrograme)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenMeterCover)          //���Ǽ�¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenMeterCover)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEnergy)          //�����¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEnergy)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseU)            //��ѹ�������¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseU)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseI)            //�����������¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseI)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenButtonCover)            //����ť�Ǽ�¼
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenButtonCover)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceU)            //��ѹ��ƽ��
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceU)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceI)            //������ƽ��
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceI)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearDemand)            //������
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearDemand)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEvent)            //�¼�����
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEvent)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventCalibrationTime)            //Уʱ
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventCalibrationTime)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReverseTrend)          //��������
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReverseTrend)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePower)          //���ʷ���
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePower)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransDemand)          //��������
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransDemand)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransPF)          //��������������
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransPF)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPassCurrent_485)            //����485
            {
                ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPassCurrent_485)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            //if (_Control is CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPassCurrent_ZB)            //�����ز�
            //{
            //    ((CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPassCurrent_ZB)_Control).SetData(MeterGroup.MeterGroup);
            //    return;
            //}
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
            if (!(meterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_EventLog)) return;

            CLDC_DataCore.Struct.StPlan_EventLog _Item = (CLDC_DataCore.Struct.StPlan_EventLog)meterGroup.CheckPlan[CheckOrderID];

            bool bFind = false;

            if (Tab_EventLog.TabPages.Count > 1)            //���Tab��ҳ������1���Ǳ�ʾ���ڶ�̬���ӵ�����ҳ
            {
                if (Data_EventLog.Tag != null && Data_EventLog.Tag.ToString() == _Item.EventLogPrjID)
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_EventLog.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_EventLog.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                Data_EventLog.Tag = _Item.EventLogPrjID;          //��IDֵ�ŵ������б��Tag�У�������ˢ��ʹ��

                switch (_Item.EventLogPrjID)
                {
                    case "001":
                        {
                            Tab_EventLog.TabPages.Add("ʧѹ�¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseVoltage _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseVoltage();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "002":
                        {
                            Tab_EventLog.TabPages.Add("��ѹ�¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverVoltage _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverVoltage();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "003":
                        {
                            Tab_EventLog.TabPages.Add("Ƿѹ�¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventUnderVoltage _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventUnderVoltage();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "004":
                        {
                            Tab_EventLog.TabPages.Add("ʧ���¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseCurrent _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseCurrent();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "005":
                        {
                            Tab_EventLog.TabPages.Add("�����¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopCurrent _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopCurrent();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "006":
                        {
                            Tab_EventLog.TabPages.Add("�����¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverCurrent _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverCurrent();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    //case "006":
                    //    {
                    //        Tab_EventLog.TabPages.Add("����(485)�¼���¼����");
                    //        CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPassCurrent_485 _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPassCurrent_485();
                    //        Tab_EventLog.TabPages[1].Controls.Add(_View);
                    //        _View.Dock = DockStyle.Fill;
                    //        _View.Margin = new Padding(0);
                    //        break;
                    //    }
                    case "007":
                        {
                            Tab_EventLog.TabPages.Add("�����¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverLoad _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOverLoad();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "008":
                        {
                            Tab_EventLog.TabPages.Add("�����¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopPhase _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventStopPhase();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }                   
                                   
                    case "009":
                        {
                            Tab_EventLog.TabPages.Add("�����¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventACdump _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventACdump();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "010":
                        {
                            Tab_EventLog.TabPages.Add("ȫʧѹ�¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseFullVoltage _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventLoseFullVoltage();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "011":
                        {
                            Tab_EventLog.TabPages.Add("��ѹ��ƽ���¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceU _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceU();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "012":
                        {
                            Tab_EventLog.TabPages.Add("������ƽ���¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceI _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventImbalanceI();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "013":
                        {
                            Tab_EventLog.TabPages.Add("��ѹ�������¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseU _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseU();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "014":
                        {
                            Tab_EventLog.TabPages.Add("�����������¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseI _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePhaseI();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "015":
                        {
                            Tab_EventLog.TabPages.Add("������¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenMeterCover _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenMeterCover();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "016":
                        {
                            Tab_EventLog.TabPages.Add("����ť���¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenButtonCover _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventOpenButtonCover();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "017":
                        {
                            Tab_EventLog.TabPages.Add("����¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPrograme _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventPrograme();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "018":
                        {
                            Tab_EventLog.TabPages.Add("Уʱ�¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventCalibrationTime _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventCalibrationTime();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "019":
                        {
                            Tab_EventLog.TabPages.Add("���������¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearDemand _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearDemand();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "020":
                        {
                            Tab_EventLog.TabPages.Add("�¼������¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEvent _View = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEvent();
                            Tab_EventLog.TabPages[1].Controls.Add(_View);
                            _View.Dock = DockStyle.Fill;
                            _View.Margin = new Padding(0);
                            break;
                        }
                    case "021":
                        {
                            Tab_EventLog.TabPages.Add("��������¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEnergy _Event = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventClearEnergy();
                            Tab_EventLog.TabPages[1].Controls.Add(_Event);
                            _Event.Dock = DockStyle.Fill;
                            _Event.Margin = new Padding(0);
                            break;
                        }
                    case "022":
                        {
                            Tab_EventLog.TabPages.Add("���������¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReverseTrend _Event = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReverseTrend();
                            Tab_EventLog.TabPages[1].Controls.Add(_Event);
                            _Event.Dock = DockStyle.Fill;
                            _Event.Margin = new Padding(0);
                            break;
                        }
                    case "023":
                        {
                            Tab_EventLog.TabPages.Add("���ʷ����¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePower _Event = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventReversePower();
                            Tab_EventLog.TabPages[1].Controls.Add(_Event);
                            _Event.Dock = DockStyle.Fill;
                            _Event.Margin = new Padding(0);
                            break;
                        }
                    case "024":
                        {
                            Tab_EventLog.TabPages.Add("���������¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransDemand _Event = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransDemand();
                            Tab_EventLog.TabPages[1].Controls.Add(_Event);
                            _Event.Dock = DockStyle.Fill;
                            _Event.Margin = new Padding(0);
                            break;
                        }
                    case "025":
                        {
                            Tab_EventLog.TabPages.Add("���������������¼���¼����");
                            CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransPF _Event = new CLDC_MeterUI.UI_Detection_New.EventLogDataView.ViewEventTransPF();
                            Tab_EventLog.TabPages[1].Controls.Add(_Event);
                            _Event.Dock = DockStyle.Fill;
                            _Event.Margin = new Padding(0);
                            break;
                        }

                }
            }


            this.RefreshGrid(meterGroup, CheckOrderID);

            Data_EventLog.Enabled = true;
        }

        private void Data_Dgn_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

            if (Data_EventLog[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //�����ֻ�����˳���
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_EventLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_EventLog.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_EventLog.EndEdit();
                }
                Data_EventLog.Enabled = false;
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

        private void Data_Dgn_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_EventLog.SelectedRows[0].Index);
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
                if (Data_EventLog.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_EventLog.SelectedRows[0].Index;
            }
            set
            {
                if (Data_EventLog.IsHandleCreated)
                {
                    if (value >= 0 && Data_EventLog.Rows.Count > value)
                    {
                        Data_EventLog.Rows[value].Selected = true;
                        Data_EventLog.CurrentCell = Data_EventLog.Rows[value].Cells[1];
                    }
                }
            }
        }

        /// <summary>
        /// ���������Ժ��л� ui_Popup_MeterIndex ���л������ϸ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_Dgn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (CLDC_DataCore.Const.GlobalUnit.CheckState == CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨 && e.ColumnIndex == 0)
            {
                if (ParentMain.Evt_OnYaoJianChanged != null)
                {
                    bool Yn;
                    if ((bool)Data_EventLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                    {
                        Yn = false;
                        Data_EventLog.EndEdit();
                    }
                    else
                    {
                        Yn = true;
                        Data_EventLog.EndEdit();
                    }
                    Data_EventLog.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Yn;
                    _DnbGroup.MeterGroup[e.RowIndex].YaoJianYn = Yn;
                    Data_EventLog.Enabled = false;
                    //Comm.Function.TopWaiting.ShowWaiting("���ڸ���...");
                    ParentMain.Evt_OnYaoJianChanged(_TaiType, _TaiID, e.RowIndex, Yn);
                    //Comm.Function.TopWaiting.HideWaiting();
                }
                else
                {
                    MessageBox.Show("û�д����¼�Evt_OnYaoJianChanged", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (Data_EventLog.SelectedRows.Count < 1) return;
            
        }

        private void Data_Dgn_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Data_Dgn_CellClick(Data_EventLog, e);
            
        }

    }
}

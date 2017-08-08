using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckDgn : UserControl
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

        public CheckDgn()
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
        public CheckDgn(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (Data_Dgn.Rows.Count != _Count)
            {
                Data_Dgn.Rows.Clear();
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)          //���ݵ���̨��λ����ӱ�λ��
                {
                    int _RowIndex = Data_Dgn.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                    {
                        Data_Dgn.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_Dgn.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_Dgn.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Dgn.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
                Data_Dgn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                Data_Dgn.Refresh();
            }
        }

        /// <summary>
        /// ����ˢ��
        /// </summary>
        /// <param name="MeterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨��</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {

            CLDC_DataCore.Struct.StPlan_Dgn _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_Dgn)
            {
                _Item = (CLDC_DataCore.Struct.StPlan_Dgn)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = Data_Dgn.Rows[i];
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

                if (Data_Dgn.Tag == null) return;

                if (_MeterInfo.MeterDgns.ContainsKey(Data_Dgn.Tag.ToString()))           //��������д���ֵ��ô����Ҫ�������ݣ�����ط����ֵ���Ǻϸ���߲��ϸ���Ϊȡ�Ķ��Ǵ��ţ����Ǵ�����ֵ��С���
                {
                    _Row.Cells[3].Value = "100%";
                    _Row.Cells[4].Value = _MeterInfo.MeterDgns[Data_Dgn.Tag.ToString()].Md_chrValue;
                    if ((MeterGroup.CheckState & CLDC_Comm.Enum.Cus_CheckStaute.�춨) == CLDC_Comm.Enum.Cus_CheckStaute.�춨 ||
                        (MeterGroup.CheckState & CLDC_Comm.Enum.Cus_CheckStaute.�����춨) == CLDC_Comm.Enum.Cus_CheckStaute.�����춨)
                    {
                        switch (_Item.DgnPrjID)
                        {
                            case "014":
                            case "015":
                            case "016":
                                string[] data = _Item.PrjParm.Split('|');
                                float time = float.Parse(data[0]);
                                if (data.Length > 3)
                                {
                                    float hcsj = data[1] != "" ? float.Parse(data[1]) : 0;
                                    float hccs = data[2] != "" ? float.Parse(data[2]) : 0;
                                    time += hcsj * hccs;
                                }
                                //_Row.Cells[3].Value = string.Format("{0}/{1} ����ǰ����/����ʱ�䣨�֣���", MeterGroup.NowMinute, _Item.PrjParm.Substring(0, _Item.PrjParm.IndexOf("|")));
                                _Row.Cells[3].Value = string.Format("{0}/{1} ����ǰ����/����ʱ�䣨�֣���", MeterGroup.NowMinute, time.ToString());
                                break;
                            case "029":     //У������
                                _Row.Cells[3].Value = string.Format("{0}/{1} ��������Ҫʱ��/��ǰ�ѽ���ʱ��(��)��", "2", MeterGroup.NowMinute);
                                break;
                            default:
                                _Row.Cells[3].Value = "";
                                break;
                        }
                    }
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
                    _Row.DefaultCellStyle.ForeColor = Color.Red;
                else
                    _Row.DefaultCellStyle.ForeColor = Color.Black;
            }

            #region -----------------------------------------����ҳˢ��-------------------------------------------
            if (Tab_Dgn.TabPages.Count != 2) return;           //���û�и�������ҳ�򷵻�

            Control _Control = Tab_Dgn.TabPages[1].Controls[0];

            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDateTimeErr)         //������ռ�ʱ���
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDateTimeErr)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewReadPeriod)            //����Ƿ���ʱ�μ��
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewReadPeriod)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewSdtq)            //�����ʱ��Ͷ��
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewSdtq)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatePeriod)            //����Ƿ���ʱ��ʾֵ���
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatePeriod)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRegister)            //����Ǽƶ���ʾֵ������
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRegister)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMemoryCheck)            //����ÿؼ��ǵ����洢���������ݱ�
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMemoryCheck)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl)          //����ÿؼ���������������ݱ�
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDianLiang)          //����ÿؼ��Ƕ����������ݱ�
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDianLiang)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewGpsTime)            //�����ʱ��������ݱ�
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewGpsTime)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff)            //�����ʱ��������ݱ�
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff)_Control).SetData(MeterGroup.MeterGroup);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime)            //����Ƿ��ʵ�ۼ���
            {
                ((CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime)_Control).SetData(MeterGroup.MeterGroup);
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
            if (!(meterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StPlan_Dgn)) return;

            CLDC_DataCore.Struct.StPlan_Dgn _Item = (CLDC_DataCore.Struct.StPlan_Dgn)meterGroup.CheckPlan[CheckOrderID];

            bool bFind = false;

            if (Tab_Dgn.TabPages.Count > 1)            //���Tab��ҳ������1���Ǳ�ʾ���ڶ�̬���ӵ�����ҳ
            {
                if (Data_Dgn.Tag != null && Data_Dgn.Tag.ToString() == _Item.DgnPrjID)
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_Dgn.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_Dgn.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                Data_Dgn.Tag = _Item.DgnPrjID;          //��IDֵ�ŵ������б��Tag�У�������ˢ��ʹ��

                switch (_Item.DgnPrjID)
                {
                    case "002":
                        {
                            int Count;
                            string[] str = _Item.PrjParm.Split('|');
                            if (str.Length >= 2)
                          int.TryParse(str[1], out Count);
                            else
                            {
                                Count = 10;
                            }
                            Tab_Dgn.TabPages.Add("�ռ�ʱ�������");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDateTimeErr _DateTimeErr = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDateTimeErr(Count);
                            Tab_Dgn.TabPages[1].Controls.Add(_DateTimeErr);
                            _DateTimeErr.Dock = DockStyle.Fill;
                            _DateTimeErr.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "003":
                        {
                            Tab_Dgn.TabPages.Add("����ʱ������");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewReadPeriod _Period = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewReadPeriod(_Item);
                            Tab_Dgn.TabPages[1].Controls.Add(_Period);
                            _Period.Dock = DockStyle.Fill;
                            _Period.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "004":
                    case "028":
                    case "029":
                    case "030":
                        {
                            Tab_Dgn.TabPages.Add("ʱ��Ͷ������");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewSdtq _Sdtq = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewSdtq(_Item);
                            Tab_Dgn.TabPages[1].Controls.Add(_Sdtq);
                            _Sdtq.Dock = DockStyle.Fill;
                            _Sdtq.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "005":
                    case "031":
                    case "032":
                    case "033":
                        {
                            Tab_Dgn.TabPages.Add("�ƶ���ʾֵ����������");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRegister _Register = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRegister(_Item);
                            Tab_Dgn.TabPages[1].Controls.Add(_Register);
                            _Register.Dock = DockStyle.Fill;
                            _Register.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "006":
                    case "034":
                    case "035":
                    case "036":
                        {
                            Tab_Dgn.TabPages.Add("����ʱ��ʾֵ�������");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatePeriod _RatePeriod = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatePeriod(_Item);
                            Tab_Dgn.TabPages[1].Controls.Add(_RatePeriod);
                            _RatePeriod.Dock = DockStyle.Fill;
                            _RatePeriod.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "013":
                        {
                            Tab_Dgn.TabPages.Add("ʱ�����");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewGpsTime _Gps = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewGpsTime();
                            Tab_Dgn.TabPages[1].Controls.Add(_Gps);
                            _Gps.Dock = DockStyle.Fill;
                            _Gps.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "019":
                        {
                            Tab_Dgn.TabPages.Add("�����Ĵ������");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMemoryCheck _MemortCheck = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMemoryCheck();
                            Tab_Dgn.TabPages[1].Controls.Add(_MemortCheck);
                            _MemortCheck.Dock = DockStyle.Fill;
                            _MemortCheck.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "014":
                        {
                            Tab_Dgn.TabPages.Add("�������0.1Ib����");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl _MaxXl = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl(_Item);
                            Tab_Dgn.TabPages[1].Controls.Add(_MaxXl);
                            _MaxXl.Dock = DockStyle.Fill;
                            _MaxXl.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "015":
                        {
                            Tab_Dgn.TabPages.Add("�������1.0Ib����");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl _MaxXl = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl(_Item);
                            Tab_Dgn.TabPages[1].Controls.Add(_MaxXl);
                            _MaxXl.Dock = DockStyle.Fill;
                            _MaxXl.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "016":
                        {
                            Tab_Dgn.TabPages.Add("�������Imax����");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl _MaxXl = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewMaxXl(_Item);
                            Tab_Dgn.TabPages[1].Controls.Add(_MaxXl);
                            _MaxXl.Dock = DockStyle.Fill;
                            _MaxXl.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    case "017":
                        {
                            Tab_Dgn.TabPages.Add("��ȡ��������");
                            CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDianLiang _DianLiang = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewDianLiang(_Item);
                            Tab_Dgn.TabPages[1].Controls.Add(_DianLiang);
                            _DianLiang.Dock = DockStyle.Fill;
                            _DianLiang.Margin = new System.Windows.Forms.Padding(0);
                            break;
                        }
                    //case "043":
                    //    {
                    //        Tab_Dgn.TabPages.Add("���ݵ�ۼ��");
                    //        CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff stepPrice = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewStepTariff(_Item);
                    //        Tab_Dgn.TabPages[1].Controls.Add(stepPrice);
                    //        stepPrice.Dock = DockStyle.Fill;
                    //        stepPrice.Margin = new System.Windows.Forms.Padding(0);
                    //    }
                    //    break;
                    //case "044":
                    //    {
                    //        Tab_Dgn.TabPages.Add("���ʵ�ۼ��");
                    //        CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime ratesPrice = new CLDC_MeterUI.UI_Detection_New.DgnDataView.ViewRatesTime(_Item);
                    //        Tab_Dgn.TabPages[1].Controls.Add(ratesPrice);
                    //        ratesPrice.Dock = DockStyle.Fill;
                    //        ratesPrice.Margin = new System.Windows.Forms.Padding(0);
                    //    }
                    //    break;
                }
            }
            this.RefreshGrid(meterGroup, CheckOrderID);

            Data_Dgn.Enabled = true;
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

            if (Data_Dgn[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //�����ֻ�����˳���
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_Dgn.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_Dgn.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_Dgn.EndEdit();
                }
                Data_Dgn.Enabled = false;
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

        private void Data_Dgn_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_Dgn.SelectedRows[0].Index);
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
                if (Data_Dgn.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_Dgn.SelectedRows[0].Index;
            }
            set
            {
                if (Data_Dgn.IsHandleCreated)
                {
                    if (value >= 0 && Data_Dgn.Rows.Count > value)
                    {
                        Data_Dgn.Rows[value].Selected = true;
                        Data_Dgn.CurrentCell = Data_Dgn.Rows[value].Cells[1];
                    }
                }
            }
        }

    }
}

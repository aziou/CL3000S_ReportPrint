using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;
using System.Threading;

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckErrAccord : UserControl
    {
        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup = null;

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

        public CheckErrAccord()
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
        public CheckErrAccord(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
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

            if (dgv_ErrAccord.Rows.Count != _Count)
            {
                dgv_ErrAccord.Rows.Clear();
                for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)          //���ݵ���̨��λ����ӱ�λ��
                {
                    int _RowIndex = dgv_ErrAccord.Rows.Add();

                    if ((_RowIndex + 1) % 2 == 0)
                    {
                        dgv_ErrAccord.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        dgv_ErrAccord.Rows[_RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    dgv_ErrAccord.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    dgv_ErrAccord.Rows[_RowIndex].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
                dgv_ErrAccord.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv_ErrAccord.Refresh();
            }
        }

        /// <summary>
        /// ����ˢ��
        /// </summary>
        /// <param name="MeterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨��</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            //string strMessage = "";
            _DnbGroup = MeterGroup;
            //_DnbGroup.NowMinute ==""
            int FirstYJMeter = Main.GetFirstYaoJianMeterIndex(MeterGroup);
            if (CheckOrderID >= MeterGroup.CheckPlan.Count
                || !(MeterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StErrAccord))
            {
                return;
            }

            //��ǰ�춨������
            CLDC_DataCore.Struct.StErrAccord _Item = (CLDC_DataCore.Struct.StErrAccord)MeterGroup.CheckPlan[CheckOrderID];

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow _Row = dgv_ErrAccord.Rows[i];
                //��λ��
                _Row.Cells[1].Value = _MeterInfo.ToString();            //�����λ��

                if (!_MeterInfo.YaoJianYn)           //�������
                {
                    _Row.Cells[0].Value = false;
                    //������죬���ҳ������ߵȼ���Ϊ�գ��򽫹�ѡ��Ԫ������Ϊֻ��
                    if (_MeterInfo.Mb_chrBcs == String.Empty || _MeterInfo.Mb_chrBdj == String.Empty)       
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

                if (dgv_ErrAccord.Tag == null) return;

                if (_MeterInfo.MeterErrAccords == null)
                    _MeterInfo.MeterErrAccords = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrAccord>();
                //��������д���ֵ��ô����Ҫ�������ݣ�����ط����ֵ���Ǻϸ���߲��ϸ���Ϊȡ�Ķ��Ǵ��ţ����Ǵ�����ֵ��С���
                if (_MeterInfo.MeterErrAccords.ContainsKey(dgv_ErrAccord.Tag.ToString()))           
                {
                    _Row.Cells[4].Value = _MeterInfo.MeterErrAccords[dgv_ErrAccord.Tag.ToString()].Mea_Result;
                    if ((MeterGroup.CheckState & CLDC_Comm.Enum.Cus_CheckStaute.�춨) == CLDC_Comm.Enum.Cus_CheckStaute.�춨 ||
                        (MeterGroup.CheckState & CLDC_Comm.Enum.Cus_CheckStaute.�����춨) == CLDC_Comm.Enum.Cus_CheckStaute.�����춨)
                    {
                        //switch (_Item.ErrAccordType)
                        //{
                        //    case 1:
                        //        strMessage = "���һ������Ŀ�춨��...";                                
                        //        break;
                        //    case 2:
                        //        strMessage = "�������Ŀ�춨��...";   
                        //        break;
                        //    case 3:
                        //        strMessage = "���ص������������Ŀ�춨��...";   
                        //        break;
                        //    case 4:
                        //        strMessage = "��������������Ŀ�춨��...";   
                        //        break;
                        //}
                        //���¼�����
                        string CurProgrocssValue = _MeterInfo.MeterErrAccords[dgv_ErrAccord.Tag.ToString()].ProgressValue;

                        _Row.Cells[3].Value = CurProgrocssValue;
                        _Row.Cells[4].Value =  _MeterInfo.MeterErrAccords[dgv_ErrAccord.Tag.ToString()].Mea_Result;
                    }
                    if (_Row.Cells[4].Value != null)
                    {
                        if (_Row.Cells[4].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      //���ϸ��޸ĵ�ǰ�б�����ɫ
                        {
                            _Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;
                            foreach (DataGridViewCell cell in _Row.Cells)
                            {
                                cell.ToolTipText = _MeterInfo.MeterErrAccords[dgv_ErrAccord.Tag.ToString()].AVR_DIS_REASON;
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
                    _Row.DefaultCellStyle.ForeColor = Color.Black;
                    _Row.Cells[3].Value = "�춨���";

                    _Row.Cells[4].Value = CLDC_DataCore.Const.Variable.CTG_HeGe;
                }
            }

            #region -----------------------------------------����ҳˢ��-------------------------------------------
            if (Tab_ErrAccord.TabPages.Count != 2) return;           //���û�и�������ҳ�򷵻�

            Control _Control = Tab_ErrAccord.TabPages[1].Controls[0];

            if (_Control is CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewErrAccord)    //���һ����   
            {
                ((CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewErrAccord)_Control).SetData(MeterGroup.MeterGroup,_Item);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewContrast)     //�����
            {
                ((CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewContrast)_Control).SetData(MeterGroup.MeterGroup, _Item);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewUpDown)       //���ص����������
            {
                ((CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewUpDown)_Control).SetData(MeterGroup.MeterGroup, _Item);
                return;
            }
            if (_Control is CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewOver)         //��������
            {
                ((CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewOver)_Control).SetData(MeterGroup.MeterGroup, _Item);
                return;
            }
            #endregion
        }

        /// <summary>
        /// ˢ�������¼�
        /// </summary>
        /// <param name="meterGroup"></param>
        /// <param name="CheckOrderID"></param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            if (!(meterGroup.CheckPlan[CheckOrderID] is CLDC_DataCore.Struct.StErrAccord)) return;

            CLDC_DataCore.Struct.StErrAccord _Item = (CLDC_DataCore.Struct.StErrAccord)meterGroup.CheckPlan[CheckOrderID];

            bool bFind = false;

            if (Tab_ErrAccord.TabPages.Count > 1)            //���Tab��ҳ������1���Ǳ�ʾ���ڶ�̬���ӵ�����ҳ
            {
                if (dgv_ErrAccord.Tag != null && dgv_ErrAccord.Tag.ToString() == _Item.ErrAccordType.ToString())
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_ErrAccord.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_ErrAccord.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                dgv_ErrAccord.Tag = _Item.ErrAccordType;   //��IDֵ�ŵ������б��Tag�У�������ˢ��ʹ��

                switch (_Item.ErrAccordType)
                {
                    case 1:
                        Tab_ErrAccord.TabPages.Add("���һ��������");
                        CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewErrAccord _ErrorAccord = new CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewErrAccord();
                        Tab_ErrAccord.TabPages[1].Controls.Add(_ErrorAccord);
                        _ErrorAccord.Dock = DockStyle.Fill;
                        _ErrorAccord.Margin = new System.Windows.Forms.Padding(0);
                        break;
                    case 2:
                        Tab_ErrAccord.TabPages.Add("���������");
                        CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewContrast _Contrast = new CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewContrast();
                        Tab_ErrAccord.TabPages[1].Controls.Add(_Contrast);
                        _Contrast.Dock = DockStyle.Fill;
                        _Contrast.Margin = new System.Windows.Forms.Padding(0);
                        break;
                    case 3:
                        Tab_ErrAccord.TabPages.Add("���ص��������������");
                        CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewUpDown _UpDown = new CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewUpDown();
                        Tab_ErrAccord.TabPages[1].Controls.Add(_UpDown);
                        _UpDown.Dock = DockStyle.Fill;
                        _UpDown.Margin = new System.Windows.Forms.Padding(0);
                        break;
                    case 4:
                        Tab_ErrAccord.TabPages.Add("������������");
                        CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewOver _Over = new CLDC_MeterUI.UI_Detection_New.ErrAccordView.ViewOver();
                        Tab_ErrAccord.TabPages[1].Controls.Add(_Over);
                        _Over.Dock = DockStyle.Fill;
                        _Over.Margin = new System.Windows.Forms.Padding(0);
                        break;
                }
            }

            this.RefreshGrid(meterGroup, CheckOrderID);

            dgv_ErrAccord.Enabled = true;
        }

        #region ��ʾ������Ϣ�������

        public delegate void Evt_SetDnbInfoViewData(int Index);

        public event Evt_SetDnbInfoViewData SetDnbInfoViewData;

        #endregion

        /// <summary>
        /// ��ȡ�����õ�ǰѡ�е��к�
        /// </summary>
        public int SelectRowIndex
        {
            get
            {
                if (dgv_ErrAccord.SelectedRows.Count == 0)
                    return 0;
                else
                    return dgv_ErrAccord.SelectedRows[0].Index;
            }
            set
            {
                if (dgv_ErrAccord.IsHandleCreated)
                {
                    if (value >= 0 && dgv_ErrAccord.Rows.Count > value)
                    {
                        dgv_ErrAccord.Rows[value].Selected = true;
                        dgv_ErrAccord.CurrentCell = dgv_ErrAccord.Rows[value].Cells[1];
                    }
                }
            }
        }

        private void dgv_ErrAccord_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
       
    }
}

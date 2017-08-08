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

namespace CLDC_MeterUI.UI_Detection_New.CheckDataView
{
    public partial class CheckCarrier : UserControl
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


        public CheckCarrier()
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
        public CheckCarrier(Main parent, CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID, int taiType, int TaiID)
        {
            InitializeComponent();

            ParentMain = parent;

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
           
            this.InitializationGrid(meterGroup);
            this.RefreshGrid(meterGroup, CheckOrderID);
            Data_Carrier.CellMouseUp +=new DataGridViewCellMouseEventHandler(Data_Carrier_CellMouseUp);

        }

        private void InitializationGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            int _Count = MeterGroup.MeterGroup.Count;

            if (Data_Carrier.Rows.Count != _Count)
            {
                Data_Carrier.Rows.Clear();

                for (int i = 0; i < _Count; i++)
                {
                    int Index = Data_Carrier.Rows.Add();
                    if ((Index + 1) % 2 == 0)
                    {
                        Data_Carrier.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_Carrier.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_Carrier.Rows[Index].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_Carrier.Rows[Index].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
            }
            Data_Carrier.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Data_Carrier.Refresh();

        }

        /// <summary>
        /// ����ˢ��
        /// </summary>
        /// <param name="MeterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨��</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            StPlan_Carrier _Item;
            _DnbGroup = MeterGroup;
            if (MeterGroup.CheckPlan[CheckOrderID] is StPlan_Carrier)
            {
                _Item = (StPlan_Carrier)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }
            string _Key = string.Format("{0}{1}{2}", ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.�ز�����).ToString(), _Item.str_Code,_Item .str_Type );
            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow Row = Data_Carrier.Rows[i];

                Row.Cells[1].Value = _MeterInfo.ToString();                 //��λ��

                if (!_MeterInfo.YaoJianYn)
                {
                    Row.Cells[0].Value = false;

                    //��������죬���ҳ������ߵȼ���Ϊ�գ��򽫹�ѡ��Ԫ������Ϊֻ����
                    if (_MeterInfo.Mb_chrBcs == String.Empty || _MeterInfo.Mb_chrBdj == String.Empty)     
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

                

                Row.Cells[2].Value = _Item.ToString();

                if (_MeterInfo.MeterCarrierDatas == null)
                    _MeterInfo.MeterCarrierDatas = new Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterCarrierData>();

                //if (_MeterInfo.MeterCarrierErrs.ContainsKey(_Key))
                //{
                //    Row.Cells[3].Value = "�춨���";

                //    Row.Cells[4].Value = _MeterInfo.MeterCarrierErrs[_Key].Mce_ItemResult;
                //}
                //else
                //{
                //    Row.Cells[3].Value = "�ز���Ŀ�춨��...";
                //    Row.Cells[4].Value = CLDC_DataCore.Const.Variable.CTG_HeGe;
                //}

                if (MeterGroup.MeterGroup[i].MeterCarrierDatas.ContainsKey(_Key))
                {
                    Row.Cells[3].Value = "�춨���";

                    Row.Cells[4].Value = MeterGroup.MeterGroup[i].MeterCarrierDatas[_Key].Mce_ItemResult;
                }
                else
                {
                    Row.Cells[3].Value = "��Ŀ�춨��...";
                    Row.Cells[4].Value = "";
                }

                if (Row.Cells[4].Value != null)
                {
                    //�����ϸ��޸ĵ�ǰ�б�����ɫ��
                    if (Row.Cells[4].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)      
                    {
                        Row.DefaultCellStyle.ForeColor = CLDC_DataCore.Const.Variable.Color_Grid_BuHeGe;

                        foreach (DataGridViewCell cell in Row.Cells)
                        {
                            cell.ToolTipText = MeterGroup.MeterGroup[i].MeterCarrierDatas[_Key].AVR_DIS_REASON;
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
                //if (_MeterInfo.Mb_Result == CLDC_DataCore.Const.Variable.CTG_BuHeGe)
                //    Row.DefaultCellStyle.ForeColor = Color.Red;
                //else
                //    Row.DefaultCellStyle.ForeColor = Color.Black;

            }
            if (Tab_Carrier.TabPages.Count != 2) return;           //���û�и�������ҳ�򷵻�

            

            //CLDC_MeterUI.UI_Detection_New.CarrierView.ViewCarrier _DataCarrier = new CLDC_MeterUI.UI_Detection_New.CarrierView.ViewCarrier();
            //Tab_Carrier.TabPages[1].Controls.Add(_DataCarrier);
            //_DataCarrier.Dock = DockStyle.Fill;
            //_DataCarrier.Margin = new System.Windows.Forms.Padding(0);

            Control _Control = Tab_Carrier.TabPages[1].Controls[0];
            if (_Control is CLDC_MeterUI.UI_Detection_New.CarrierView.ViewCarrier)         
            {
                ((CLDC_MeterUI.UI_Detection_New.CarrierView.ViewCarrier)_Control).SetData(MeterGroup.MeterGroup, _Key);
                return;
            }

        }

        /// <summary>
        /// ����ˢ��
        /// </summary>
        /// <param name="meterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨��</param>
        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            bool bFind = false;

            StPlan_Carrier _Item = (StPlan_Carrier)meterGroup.CheckPlan[CheckOrderID];

            if (Tab_Carrier.TabPages.Count > 1)            //���Tab��ҳ������1���Ǳ�ʾ���ڶ�̬���ӵ�����ҳ
            {
                if (Data_Carrier.Tag != null)
                {
                    bFind = true;
                }
                else
                {
                    for (int i = Tab_Carrier.TabPages.Count - 1; i > 0; i--)
                    {
                        Tab_Carrier.TabPages.RemoveAt(i);
                    }
                    bFind = false;
                }
            }

            if (!bFind)
            {
                Data_Carrier.Tag = _Item.str_PrjID;   //��IDֵ�ŵ������б��Tag�У�������ˢ��ʹ��


                Tab_Carrier.TabPages.Add("�ز�����");
                CLDC_MeterUI.UI_Detection_New.CarrierView.ViewCarrier _Carrier = new CLDC_MeterUI.UI_Detection_New.CarrierView.ViewCarrier();
                Tab_Carrier.TabPages[1].Controls.Add(_Carrier);
                _Carrier.Dock = DockStyle.Fill;
                _Carrier.Margin = new System.Windows.Forms.Padding(0);
            }
            this.RefreshGrid(meterGroup, CheckOrderID);
            Data_Carrier.Enabled = true;            
        }

        /// <summary>
        /// �����Ƿ�Ҫ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Data_Carrier_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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
            if (Data_Carrier[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //�����ֻ�����˳���
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_Carrier.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_Carrier.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_Carrier.EndEdit();
                }
                Data_Carrier.Enabled = false;
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

        private void Data_Carrier_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_Carrier.SelectedRows[0].Index);
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
                if (Data_Carrier.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_Carrier.SelectedRows[0].Index;
            }
            set
            {
                if (Data_Carrier.IsHandleCreated)
                {
                    if (value >= 0 && Data_Carrier.Rows.Count > value)
                    {
                        Data_Carrier.Rows[value].Selected = true;
                        Data_Carrier.CurrentCell = Data_Carrier.Rows[value].Cells[1];
                    }
                }
            }
        }        
        
    }
}

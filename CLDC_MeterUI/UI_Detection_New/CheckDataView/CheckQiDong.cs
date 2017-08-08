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
    public partial class CheckQiDong : UserControl
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


        public CheckQiDong()
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
        public CheckQiDong(Main parent , CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup,int CheckOrderID,int taiType,int TaiID)
        {
            InitializeComponent();

            ParentMain = parent;

            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            Data_QiDong.CellMouseUp +=new DataGridViewCellMouseEventHandler(Data_QiDong_CellMouseUp);
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

            if (Data_QiDong.Rows.Count != _Count)
            {
                Data_QiDong.Rows.Clear();

                for (int i = 0; i < _Count; i++)
                {
                    int Index = Data_QiDong.Rows.Add();
                    if ((Index + 1) % 2 == 0)
                    {
                        Data_QiDong.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                    }
                    else
                    {
                        Data_QiDong.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                    }
                    Data_QiDong.Rows[Index].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                    Data_QiDong.Rows[Index].Cells[1].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
                }
            }
            Data_QiDong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Data_QiDong.Refresh();

        }

        /// <summary>
        /// ����ˢ��
        /// </summary>
        /// <param name="MeterGroup">���ܱ����ݼ���</param>
        /// <param name="CheckOrderID">��ǰ�춨��</param>
        private void RefreshGrid(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, int CheckOrderID)
        {
            StPlan_QiDong _Item;

            if (MeterGroup.CheckPlan[CheckOrderID] is StPlan_QiDong)
            {
                _Item = (StPlan_QiDong)MeterGroup.CheckPlan[CheckOrderID];
            }
            else
            {
                return;
            }

            for (int i = 0; i < MeterGroup.MeterGroup.Count; i++)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = MeterGroup.MeterGroup[i];

                DataGridViewRow Row = Data_QiDong.Rows[i];


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

                    string _Key = string.Format("{0}{1}", ((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.������).ToString(), ((int)_Item.PowerFangXiang).ToString());
                    if (MeterGroup.MeterGroup[i].MeterQdQids.ContainsKey(_Key))            //��������Ŀ����
                    {
                        if (MeterGroup.CheckState != CLDC_Comm.Enum.Cus_CheckStaute.ֹͣ�춨)        //���������ֹͣ״̬������ʾ����
                        {
                            //if (MeterGroup.NowMinute >= float.Parse(MeterGroup.MeterGroup[i].MeterResults[_Key].Mr_Time))
                            //{
                            //    Row.Cells[3].Value = "100%";
                            //    Row.Cells[4].Value = MeterGroup.MeterGroup[i].MeterResults[_Key].Mr_Result;
                            //}
                            //else
                            {
                                Row.Cells[3].Value = _Item.CheckTime;
                                Row.Cells[4].Value = _Item.FloatIb;
                                Row.Cells[5].Value = _Item.FloatXUb * CLDC_DataCore.Const.GlobalUnit.U;
                                Row.Cells[6].Value = string.Format("{0:F}/{1:F}(��ǰ/��(��))", MeterGroup.NowMinute, _Item.CheckTime);
                                Row.Cells[7].Value = MeterGroup.MeterGroup[i].MeterQdQids[_Key].Mqd_chrJL;
                            }
                        }
                        else
                        {
                            Row.Cells[3].Value = _Item.CheckTime;
                            Row.Cells[4].Value = _Item.FloatIb;
                            Row.Cells[5].Value = _Item.FloatXUb * CLDC_DataCore.Const.GlobalUnit.U;
                            Row.Cells[6].Value = "100%";
                            Row.Cells[7].Value = MeterGroup.MeterGroup[i].MeterQdQids[_Key].Mqd_chrJL;
                        }
                        if (Row.Cells[7].Value != null && Row.Cells[7].Value.ToString() == CLDC_DataCore.Const.Variable.CTG_BuHeGe)          //����ǲ��ϸ�����ʾ��ɫ
                        {
                            Row.DefaultCellStyle.ForeColor = Color.Red;
                            foreach (DataGridViewCell cell in Row.Cells)
                            {
                                cell.ToolTipText = MeterGroup.MeterGroup[i].MeterQdQids[_Key].AVR_DIS_REASON;
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
                        //Row.DefaultCellStyle.ForeColor = Color.Black;
                        Row.Cells[3].Value = _Item.CheckTime;
                        Row.Cells[4].Value = _Item.FloatIb;
                        Row.Cells[5].Value = _Item.FloatXUb * CLDC_DataCore.Const.GlobalUnit.U;
                        Row.Cells[6].Value = string.Format("{0:F}/{1:F}(��ǰ/��(��))", MeterGroup.NowMinute, _Item.CheckTime);
                        Row.Cells[7].Value = CLDC_DataCore.Const.Variable.CTG_DEFAULTRESULT;
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
                catch (Exception ex)
                {
                    CLDC_DataCore.Function.ErrorLog.Write(ex);
                }
            }
        }


        public void RefreshData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo meterGroup, int CheckOrderID)
        {
            this.RefreshGrid(meterGroup, CheckOrderID);
            Data_QiDong.Enabled = true;            
        }

        private void Data_QiDong_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
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

            if (Data_QiDong[e.ColumnIndex, e.RowIndex].ReadOnly) return;      //�����ֻ�����˳���
            if (ParentMain.Evt_OnYaoJianChanged != null)
            {
                bool Yn;
                if ((bool)Data_QiDong.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)
                {
                    Yn = false;
                    Data_QiDong.EndEdit();
                }
                else
                {
                    Yn = true;
                    Data_QiDong.EndEdit();
                }
                Data_QiDong.Enabled = false;
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

        private void Data_QiDong_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                SetDnbInfoViewData(Data_QiDong.SelectedRows[0].Index);
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
                if (Data_QiDong.SelectedRows.Count == 0)
                    return 0;
                else
                    return Data_QiDong.SelectedRows[0].Index;
            }
            set
            {
                if (Data_QiDong.IsHandleCreated)
                {
                    if (value >= 0 && Data_QiDong.Rows.Count > value)
                    {
                        Data_QiDong.Rows[value].Selected = true;
                        Data_QiDong.CurrentCell = Data_QiDong.Rows[value].Cells[1];
                    }
                }
            }
        }        

    }
}

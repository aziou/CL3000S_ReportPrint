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

namespace CLDC_MeterUI.UI_Detection_New.ShowDataView.FaParam
{
    public partial class QiDongPrjView : UserControl
    {
        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup;

        private float _Ub;

        private string _Ib;

        private string _Dj;

        private string _GuiCheng;

        private string _Const;

        private bool _Znq;

        private bool _Hgq;


        private CLDC_Comm.Enum.Cus_Clfs _Clfs;

        public QiDongPrjView()
        {
            InitializeComponent();
        }

        public QiDongPrjView(ref CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            _DnbGroup = MeterGroup;

            InitializeComponent();

        }

        private void QiDongPrjView_Load(object sender, EventArgs e)
        {
            //if (Dgv_QiDong.IsHandleCreated)
            //{
            //System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(CreateFaInfo));
            //}
            this.CreateFaInfo();
        }

        /// <summary>
        /// ��ʼ�����
        /// </summary>
        private void CreateFaInfo()
        {
            int _FirstYaoJian = CLDC_MeterUI.UI_Detection_New.Main.GetFirstYaoJianMeterIndex(_DnbGroup);

            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _DnbInfo = _DnbGroup.MeterGroup[_FirstYaoJian];

            _Ub = float.Parse(_DnbInfo.Mb_chrUb);     //��ѹ

            _Ib = _DnbInfo.Mb_chrIb;                //�����ַ���

            _Dj = _DnbInfo.Mb_chrBdj;               //�ȼ� �й����޹���

            _GuiCheng = _DnbInfo.GuiChengName;        //ʹ�õĹ������

            _Const = _DnbInfo.Mb_chrBcs;              //����

            _Znq = _DnbInfo.Mb_BlnZnq;              //ֹ����

            _Hgq = _DnbInfo.Mb_BlnHgq;              //������

            _Clfs = (CLDC_Comm.Enum.Cus_Clfs)_DnbInfo.Mb_intClfs;      //������ʽ

            for (int i = 0; i < _DnbGroup.CheckPlan.Count; i++)
            {
                if (!(_DnbGroup.CheckPlan[i] is StPlan_QiDong)) continue;

                StPlan_QiDong _Item = (StPlan_QiDong)_DnbGroup.CheckPlan[i];

                int _RowIndex = Dgv_QiDong.Rows.Add();

                Dgv_QiDong.Rows[_RowIndex].HeaderCell.Value = _Item.ToString();

                Dgv_QiDong.Rows[_RowIndex].Tag = i;

                Dgv_QiDong.Rows[_RowIndex].Cells[0].Value = _Item.PowerFangXiang.ToString();

                _Item.CheckTimeAndIb(_GuiCheng, _Clfs, _Ub, _Ib, _Dj, _Const, _Znq, _Hgq);          //�����𶯵�������ʱ��

                Dgv_QiDong.Rows[_RowIndex].Cells[2].Value = _Item.FloatIb;          //�𶯵���

                Dgv_QiDong.Rows[_RowIndex].Cells[1].Value = Math.Round(_Item.FloatIb / CLDC_DataCore.Function.Number.GetCurrentByIb("1.0Ib", _Ib), 3);      //�����𶯵�������

                Dgv_QiDong.Rows[_RowIndex].Cells[1].Tag = Dgv_QiDong.Rows[_RowIndex].Cells[1].Value;            //�𶯵�����������

                Dgv_QiDong.Rows[_RowIndex].Cells[3].Value = Math.Round(_Item.CheckTime, 1);           //������ʱ��

                Dgv_QiDong.Rows[_RowIndex].Cells[3].Tag = Dgv_QiDong.Rows[_RowIndex].Cells[3].Value;        //��ʱ�丱��

                Dgv_QiDong.Rows[_RowIndex].Cells[4].Value = _Item.DefaultValue == 1 ? true : false;         //�Ƿ�Ĭ�Ϻϸ�

                Dgv_QiDong.Rows[_RowIndex].Cells[4].Tag = Dgv_QiDong.Rows[_RowIndex].Cells[4].Value;        //�Ƿ�Ĭ�Ϻϸ񸱱�

            }
        }
        /// <summary>
        /// �༭���Ա༭�ĵ�Ԫ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_QiDong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
            if (!Dgv_QiDong[e.ColumnIndex, e.RowIndex].ReadOnly)
            {
                Dgv_QiDong.BeginEdit(true);
            }
        }
        /// <summary>
        /// �༭���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_QiDong_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (!Dgv_QiDong[e.ColumnIndex, e.RowIndex].ReadOnly)
            {
                if (Dgv_QiDong[e.ColumnIndex, e.RowIndex].Value == null)
                {
                    Dgv_QiDong.EndEdit();
                    return;
                }
                string _TmpValue = Dgv_QiDong[e.ColumnIndex, e.RowIndex].Value.ToString();
                Dgv_QiDong.EndEdit();
                if (e.ColumnIndex != 4)
                {
                    if (Dgv_QiDong[e.ColumnIndex, e.RowIndex].Value == null && Dgv_QiDong[e.ColumnIndex, e.RowIndex].Value.ToString() == string.Empty)
                    {
                        Dgv_QiDong[e.ColumnIndex, e.RowIndex].Value = "0";
                    }
                    else if (!CLDC_DataCore.Function.Number.IsNumeric(Dgv_QiDong[e.ColumnIndex, e.RowIndex].Value.ToString()))
                    {
                        Dgv_QiDong[e.ColumnIndex, e.RowIndex].Value = _TmpValue;
                        return;
                    }

                }
                if (e.ColumnIndex == 1)
                {
                    StPlan_QiDong _Item = (StPlan_QiDong)_DnbGroup.CheckPlan[(int)Dgv_QiDong.Rows[e.RowIndex].Tag];
                    _Item.FloatxIb = float.Parse(Dgv_QiDong[e.ColumnIndex, e.RowIndex].Value.ToString());
                    _Item.CheckTimeAndIb(_GuiCheng, _Clfs, _Ub, _Ib, _Dj, _Const, _Znq, _Hgq);
                    Dgv_QiDong[2, e.RowIndex].Value = _Item.FloatIb;
                    Dgv_QiDong[3, e.RowIndex].Value = _Item.CheckTime;
                    Dgv_QiDong[3, e.RowIndex].Tag = Dgv_QiDong[3, e.RowIndex].Value;
                }

            }
        }


        /// <summary>
        /// ��鷽����Ŀ�����Ƿ񱻸ı䣬����ı����Ҫ��¼��׼���·�
        /// </summary>
        /// <returns></returns>
        public bool ChangeFAPram()
        {
            bool Changed = false;

            for (int i = 0; i < Dgv_QiDong.Rows.Count; i++)
            {
                bool PrjChanged = false;

                StPlan_QiDong _Item = (StPlan_QiDong)_DnbGroup.CheckPlan[(int)Dgv_QiDong.Rows[i].Tag];
                if (Dgv_QiDong[1, i].Value != Dgv_QiDong[1, i].Tag)          //���Ⱦ������޸�
                {
                    Changed = true;
                    PrjChanged = true;
                    _Item.FloatxIb = float.Parse(Dgv_QiDong[1, i].Value.ToString());
                }
                if (Dgv_QiDong[3, i].Value != Dgv_QiDong[3, i].Tag)          //���Ⱦ������޸�
                {
                    Changed = true;
                    PrjChanged = true;
                    _Item.xTime = float.Parse(Dgv_QiDong[3, i].Value.ToString());
                }
                if (Dgv_QiDong[4, i].Value != Dgv_QiDong[4, i].Tag)          //���Ⱦ������޸�
                {
                    Changed = true;
                    PrjChanged = true;
                    _Item.DefaultValue = (bool)Dgv_QiDong[4, i].Value == false ? 0 : 1;
                }
                if (PrjChanged)
                {
                    _DnbGroup.CheckPlan.RemoveAt((int)Dgv_QiDong.Rows[i].Tag);
                    _DnbGroup.CheckPlan.Insert((int)Dgv_QiDong.Rows[i].Tag, _Item);
                }
            }

            return Changed;
        }

    }
}

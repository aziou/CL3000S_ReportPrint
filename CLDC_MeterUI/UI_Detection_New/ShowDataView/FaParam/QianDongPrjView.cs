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
    public partial class QianDongPrjView : UserControl
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

        public QianDongPrjView()
        {
            InitializeComponent();
        }

        public QianDongPrjView(ref CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            _DnbGroup = MeterGroup;

            InitializeComponent();

        }

        private void QianDongPrjView_Load(object sender, EventArgs e)
        {
            this.CreateFaInfo();
        }

        private void CreateFaInfo()
        {
            int _FirstYaoJian = CLDC_MeterUI.UI_Detection_New.Main.GetFirstYaoJianMeterIndex(_DnbGroup);

            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _DnbInfo = _DnbGroup.MeterGroup[_FirstYaoJian];

            _Ub=float.Parse(_DnbInfo.Mb_chrUb);     //��ѹ

            _Ib = _DnbInfo.Mb_chrIb;                //�����ַ���

            _Dj = _DnbInfo.Mb_chrBdj;               //�ȼ� �й����޹���

            _GuiCheng=_DnbInfo.GuiChengName;        //ʹ�õĹ������

            _Const=_DnbInfo.Mb_chrBcs;              //����

            _Znq = _DnbInfo.Mb_BlnZnq;              //ֹ����

            _Hgq = _DnbInfo.Mb_BlnHgq;              //������

            _Clfs=(CLDC_Comm.Enum.Cus_Clfs)_DnbInfo.Mb_intClfs;      //������ʽ

            for (int i = 0; i < _DnbGroup.CheckPlan.Count; i++)
            {
                if (!(_DnbGroup.CheckPlan[i] is StPlan_QianDong)) continue;

                StPlan_QianDong _Item = (StPlan_QianDong)_DnbGroup.CheckPlan[i];

                int _RowIndex = Dgv_QianDong.Rows.Add();

                Dgv_QianDong.Rows[_RowIndex].HeaderCell.Value = _Item.ToString();

                Dgv_QianDong.Rows[_RowIndex].Tag = i;

                Dgv_QianDong.Rows[_RowIndex].Cells[0].Value = _Item.PowerFangXiang.ToString();

                Dgv_QianDong.Rows[_RowIndex].Cells[1].Value = _Item.FloatxU * 100F;               //��ѹ����

                Dgv_QianDong.Rows[_RowIndex].Cells[1].Tag = Dgv_QianDong.Rows[_RowIndex].Cells[1].Value;        //��ѹ��������

                Dgv_QianDong.Rows[_RowIndex].Cells[2].Value = _Item.FloatxIb;         //Ǳ������+���ٱ��𶯵���
 
                Dgv_QianDong.Rows[_RowIndex].Cells[2].Tag = Dgv_QianDong.Rows[_RowIndex].Cells[2].Value;

                _Item.CheckTimeAndIb(_GuiCheng,_Clfs,_Ub,_Ib,_Dj,_Const,_Znq,_Hgq);         //����Ǳ��������Ǳ��ʱ��

                Dgv_QianDong.Rows[_RowIndex].Cells[3].Value = _Item.FloatIb;              //Ǳ������

                Dgv_QianDong.Rows[_RowIndex].Cells[4].Value = Math.Round(_Item.CheckTime,1);            //Ǳ��ʱ��

                Dgv_QianDong.Rows[_RowIndex].Cells[4].Tag = Dgv_QianDong.Rows[_RowIndex].Cells[4].Value;        //Ǳ��ʱ�丱��

                Dgv_QianDong.Rows[_RowIndex].Cells[5].Value = _Item.DefaultValue == 1 ? true : false;         //�Ƿ�Ĭ�Ϻϸ�

                Dgv_QianDong.Rows[_RowIndex].Cells[5].Tag = Dgv_QianDong.Rows[_RowIndex].Cells[5].Value;        //�Ƿ�Ĭ�Ϻϸ񸱱�

            }
        }

        private void Dgv_QianDong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
            if (!Dgv_QianDong[e.ColumnIndex, e.RowIndex].ReadOnly)
            {
                Dgv_QianDong.BeginEdit(true);
            }
        }

        /// <summary>
        /// �༭���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_QianDong_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (!Dgv_QianDong[e.ColumnIndex, e.RowIndex].ReadOnly)
            {
                if (Dgv_QianDong[e.ColumnIndex, e.RowIndex].Value == null)
                {
                    Dgv_QianDong.EndEdit();
                    return;
                }
                string _TmpValue = Dgv_QianDong[e.ColumnIndex, e.RowIndex].Value.ToString();

                Dgv_QianDong.EndEdit();

                if (e.ColumnIndex != 5 && !CLDC_DataCore.Function.Number.IsNumeric(Dgv_QianDong[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    Dgv_QianDong[e.ColumnIndex, e.RowIndex].Value = _TmpValue;
                    return;
                }

                if (e.ColumnIndex == 1 || e.ColumnIndex==2)
                {
                    StPlan_QianDong _Item = (StPlan_QianDong)_DnbGroup.CheckPlan[(int)Dgv_QianDong.Rows[e.RowIndex].Tag];
                    _Item.FloatxU= float.Parse(Dgv_QianDong[1, e.RowIndex].Value.ToString())/100F;
                    _Item.FloatxIb = float.Parse(Dgv_QianDong[2, e.RowIndex].Value.ToString());
                    _Item.CheckTimeAndIb(_GuiCheng, _Clfs, _Ub, _Ib, _Dj, _Const, _Znq, _Hgq);
                    Dgv_QianDong[3, e.RowIndex].Value = _Item.FloatIb;
                    Dgv_QianDong[4, e.RowIndex].Value = Math.Round(_Item.CheckTime,1);
                    Dgv_QianDong[4, e.RowIndex].Tag = Dgv_QianDong[4, e.RowIndex].Value;
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

            for (int i = 0; i < Dgv_QianDong.Rows.Count; i++)
            {
                bool PrjChanged = false;

                StPlan_QianDong _Item = (StPlan_QianDong)_DnbGroup.CheckPlan[(int)Dgv_QianDong.Rows[i].Tag];

                if (Dgv_QianDong[1, i].Value != Dgv_QianDong[1, i].Tag)          //���Ⱦ������޸�
                {
                    Changed = true;
                    PrjChanged = true;
                    _Item.FloatxU = float.Parse(Dgv_QianDong[1, i].Value.ToString())/100F;
                }

                if (Dgv_QianDong[2, i].Value != Dgv_QianDong[2, i].Tag)          //���Ⱦ������޸�
                {
                    Changed = true;
                    PrjChanged = true;
                    _Item.FloatxIb = float.Parse(Dgv_QianDong[2, i].Value.ToString());
                }

                if (Dgv_QianDong[4, i].Value != Dgv_QianDong[4, i].Tag)          //���Ⱦ������޸�
                {
                    Changed = true;
                    PrjChanged = true;
                    _Item.xTime = float.Parse(Dgv_QianDong[4, i].Value.ToString());
                }
                if (Dgv_QianDong[5, i].Value != Dgv_QianDong[5, i].Tag)          //���Ⱦ������޸�
                {
                    Changed = true;
                    PrjChanged = true;
                    _Item.DefaultValue = (bool)Dgv_QianDong[5, i].Value == false ? 0 : 1;
                }
                if (PrjChanged)
                {
                    _DnbGroup.CheckPlan.RemoveAt((int)Dgv_QianDong.Rows[i].Tag);
                    _DnbGroup.CheckPlan.Insert((int)Dgv_QianDong.Rows[i].Tag, _Item);
                }
            }

            return Changed;
        }
    }
}

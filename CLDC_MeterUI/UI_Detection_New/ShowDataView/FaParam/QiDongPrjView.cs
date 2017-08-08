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
        /// 初始化表格
        /// </summary>
        private void CreateFaInfo()
        {
            int _FirstYaoJian = CLDC_MeterUI.UI_Detection_New.Main.GetFirstYaoJianMeterIndex(_DnbGroup);

            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _DnbInfo = _DnbGroup.MeterGroup[_FirstYaoJian];

            _Ub = float.Parse(_DnbInfo.Mb_chrUb);     //电压

            _Ib = _DnbInfo.Mb_chrIb;                //电流字符串

            _Dj = _DnbInfo.Mb_chrBdj;               //等级 有功（无功）

            _GuiCheng = _DnbInfo.GuiChengName;        //使用的规程名称

            _Const = _DnbInfo.Mb_chrBcs;              //常数

            _Znq = _DnbInfo.Mb_BlnZnq;              //止逆器

            _Hgq = _DnbInfo.Mb_BlnHgq;              //互感器

            _Clfs = (CLDC_Comm.Enum.Cus_Clfs)_DnbInfo.Mb_intClfs;      //测量方式

            for (int i = 0; i < _DnbGroup.CheckPlan.Count; i++)
            {
                if (!(_DnbGroup.CheckPlan[i] is StPlan_QiDong)) continue;

                StPlan_QiDong _Item = (StPlan_QiDong)_DnbGroup.CheckPlan[i];

                int _RowIndex = Dgv_QiDong.Rows.Add();

                Dgv_QiDong.Rows[_RowIndex].HeaderCell.Value = _Item.ToString();

                Dgv_QiDong.Rows[_RowIndex].Tag = i;

                Dgv_QiDong.Rows[_RowIndex].Cells[0].Value = _Item.PowerFangXiang.ToString();

                _Item.CheckTimeAndIb(_GuiCheng, _Clfs, _Ub, _Ib, _Dj, _Const, _Znq, _Hgq);          //计算起动电流和起动时间

                Dgv_QiDong.Rows[_RowIndex].Cells[2].Value = _Item.FloatIb;          //起动电流

                Dgv_QiDong.Rows[_RowIndex].Cells[1].Value = Math.Round(_Item.FloatIb / CLDC_DataCore.Function.Number.GetCurrentByIb("1.0Ib", _Ib), 3);      //反算起动电流倍数

                Dgv_QiDong.Rows[_RowIndex].Cells[1].Tag = Dgv_QiDong.Rows[_RowIndex].Cells[1].Value;            //起动电流倍数副本

                Dgv_QiDong.Rows[_RowIndex].Cells[3].Value = Math.Round(_Item.CheckTime, 1);           //起动试验时间

                Dgv_QiDong.Rows[_RowIndex].Cells[3].Tag = Dgv_QiDong.Rows[_RowIndex].Cells[3].Value;        //起动时间副本

                Dgv_QiDong.Rows[_RowIndex].Cells[4].Value = _Item.DefaultValue == 1 ? true : false;         //是否默认合格

                Dgv_QiDong.Rows[_RowIndex].Cells[4].Tag = Dgv_QiDong.Rows[_RowIndex].Cells[4].Value;        //是否默认合格副本

            }
        }
        /// <summary>
        /// 编辑可以编辑的单元格
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
        /// 编辑完成
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
        /// 检查方案项目参数是否被改变，如果改变就需要记录并准备下发
        /// </summary>
        /// <returns></returns>
        public bool ChangeFAPram()
        {
            bool Changed = false;

            for (int i = 0; i < Dgv_QiDong.Rows.Count; i++)
            {
                bool PrjChanged = false;

                StPlan_QiDong _Item = (StPlan_QiDong)_DnbGroup.CheckPlan[(int)Dgv_QiDong.Rows[i].Tag];
                if (Dgv_QiDong[1, i].Value != Dgv_QiDong[1, i].Tag)          //不等就是有修改
                {
                    Changed = true;
                    PrjChanged = true;
                    _Item.FloatxIb = float.Parse(Dgv_QiDong[1, i].Value.ToString());
                }
                if (Dgv_QiDong[3, i].Value != Dgv_QiDong[3, i].Tag)          //不等就是有修改
                {
                    Changed = true;
                    PrjChanged = true;
                    _Item.xTime = float.Parse(Dgv_QiDong[3, i].Value.ToString());
                }
                if (Dgv_QiDong[4, i].Value != Dgv_QiDong[4, i].Tag)          //不等就是有修改
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

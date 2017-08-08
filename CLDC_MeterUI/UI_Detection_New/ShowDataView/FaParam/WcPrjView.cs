using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using CLDC_DataCore.Struct;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.ShowDataView.FaParam
{

    public partial class WcPrjView : UserControl
    {
        private System.Threading.AutoResetEvent AsyncOpDone = new System.Threading.AutoResetEvent(false);

        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbGroup;

        /// <summary>
        /// 第一只要检表的常数
        /// </summary>
        private string _FirstConst = "";

        /// <summary>
        /// 第一只要检表的电流
        /// </summary>
        private string _FirstIb = "";

        /// <summary>
        /// 第一只要检表的等级
        /// </summary>
        private string _FirstDj = "";

        /// <summary>
        /// 第一只表所使用的规程
        /// </summary>
        private string _FirstGuiCheng = "";

        /// <summary>
        /// 第一只表是否经互感器
        /// </summary>
        private bool _FirstHgq = false;

        /// <summary>
        /// 误差限名称
        /// </summary>
        private string _WcLimitName = "";
        /// <summary>
        /// 圈数参照电流倍数
        /// </summary>
        private string _xIb = "";
        /// <summary>
        /// 参照圈数
        /// </summary>
        private int _Qs = 0;

        public WcPrjView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="FaList">方案列表</param>
        /// <param name="CzFzd">参照负载点</param>
        /// <param name="CzQs">参照圈数</param>
        public WcPrjView(ref CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            _DnbGroup = MeterGroup;

            InitializeComponent();

            #region 加载电流倍数字典列表
            CLDC_DataCore.SystemModel.Item.csxIbDic _xIbDic = new CLDC_DataCore.SystemModel.Item.csxIbDic();
            _xIbDic.Load();
            List<string> _xIbs = _xIbDic.getxIb();
            Cmb_xIb.Items.Clear();
            for (int i = 0; i < _xIbs.Count; i++)
            {
                Cmb_xIb.Items.Add(_xIbs[i]);
            }

            _xIbs = null;

            _xIbDic = null;
            #endregion


            #region 加载误差限列表

            CLDC_DataCore.DataBase.clsWcLimitDataControl _WcLimit = new CLDC_DataCore.DataBase.clsWcLimitDataControl();

            List<CLDC_DataCore.DataBase.IDAndValue> _WcLimitNames = _WcLimit.WcLimitName();

            Cmb_WcLimit.Items.Add("规程误差限");

            for (int i = 0; i < _WcLimitNames.Count; i++)
            {
                Cmb_WcLimit.Items.Add(_WcLimitNames[i].Value);
            }

            _WcLimitNames = null;

            _WcLimit = null;

            #endregion

            this.CreateFaInfo();

        }

        private void CreateFaInfo()
        {

            Cmb_xIb.Text = _DnbGroup.CzIb;
            Txt_Qs.Text = _DnbGroup.CzQs.ToString();
            Cmb_WcLimit.Text = _DnbGroup.CzWcLimit;
            Txt_Down.Text = (_DnbGroup.WcxDownPercent * 100).ToString();
            Txt_Up.Text = (_DnbGroup.WcxUpPercent * 100).ToString();

            _WcLimitName = _DnbGroup.CzWcLimit;

            _xIb = _DnbGroup.CzIb;

            _Qs = _DnbGroup.CzQs;

            int _FirstYaoJian = CLDC_MeterUI.UI_Detection_New.Main.GetFirstYaoJianMeterIndex(_DnbGroup);

            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = _DnbGroup.MeterGroup[_FirstYaoJian];

            _FirstConst = _MeterInfo.Mb_chrBcs;

            _FirstIb = _MeterInfo.Mb_chrIb;

            _FirstDj = _MeterInfo.Mb_chrBdj;

            _FirstGuiCheng = _MeterInfo.GuiChengName;

            _FirstHgq = _MeterInfo.Mb_BlnHgq;

            _MeterInfo = null;

            for (int i = 0; i < _DnbGroup.CheckPlan.Count; i++)
            {
                if (!(_DnbGroup.CheckPlan[i] is StPlan_WcPoint)) continue;      //如果不是误差则跳出

                StPlan_WcPoint _Item = (StPlan_WcPoint)_DnbGroup.CheckPlan[i];

                if (_Item.Pc == 1) continue;         //如果是偏差则跳出

                int _RowIndex = Dgv_Wc.Rows.Add();

                Dgv_Wc.Rows[_RowIndex].HeaderCell.Value = _Item.ToString();   //方案项目描述

                Dgv_Wc.Rows[_RowIndex].Tag = i;                                 //检定序号


            }


            int _ThreadSum = (int)((int)Dgv_Wc.Rows.Count / 10) + 1;            //归纳需要多少个线程来进行处理，固定一个线程最多处理10行数据

            for (int i = 0; i < _ThreadSum; i++)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(CheckQs), i * 10);  //计算圈数

                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(CheckWcLimit), i * 10); //计算误差限
            }

            AsyncOpDone.WaitOne();

        }


        private object LocdedQs = new object();
        /// <summary>
        /// 计算圈数
        /// </summary>
        /// <param name="obj"></param>
        private void CheckQs(object obj)
        {
            try
            {

                int intFirst = (int)obj;

                for (int i = intFirst; i < intFirst + 10; i++)
                {
                    lock (LocdedQs)
                    {
                        if (i >= Dgv_Wc.Rows.Count)
                        {
                            AsyncOpDone.Set();
                            return;
                        }

                        StPlan_WcPoint _Item = (StPlan_WcPoint)_DnbGroup.CheckPlan[(int)Dgv_Wc.Rows[i].Tag];

                        _Item.SetLapCount(_DnbGroup.MinConst, _FirstConst, _FirstIb, _xIb, _Qs);

                        Dgv_Wc.Rows[i].Cells[0].Value = _Item.LapCount;         //圈数
                        Dgv_Wc.Rows[i].Cells[0].Tag = _Item.LapCount;           //保存一个副本
                    }
                }
                AsyncOpDone.Set();
            }
            catch
            {
                AsyncOpDone.Set();
                return;
            }
        }



        private object LockedWcLimit = new object();
        /// <summary>
        /// 获取误差限
        /// </summary>
        /// <param name="obj"></param>
        private void CheckWcLimit(object obj)
        {
            int intFirst = (int)obj;
            try
            {
                for (int i = intFirst; i < intFirst + 10; i++)
                {
                    lock (LockedWcLimit)
                    {
                        if (i >= Dgv_Wc.Rows.Count)
                        {
                            AsyncOpDone.Set();
                            return;
                        }

                        StPlan_WcPoint _Item = (StPlan_WcPoint)_DnbGroup.CheckPlan[(int)Dgv_Wc.Rows[i].Tag];

                        _Item.SetWcx(_WcLimitName, _FirstGuiCheng, _FirstDj, _FirstHgq);

                        Dgv_Wc.Rows[i].Cells[1].Value = _Item.ErrorShangXian;       //上线

                        Dgv_Wc.Rows[i].Cells[3].Value = _Item.ErrorXiaXian;         //下限

                        Dgv_Wc.Rows[i].Cells[2].Value = _Item.ErrorShangXian * _DnbGroup.WcxUpPercent;

                        Dgv_Wc.Rows[i].Cells[4].Value = _Item.ErrorShangXian * _DnbGroup.WcxDownPercent;
                    }
                }
                AsyncOpDone.Set();
            }
            catch
            {
                AsyncOpDone.Set();
            }
        }


        private void Dgv_Wc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
            if (!Dgv_Wc[e.ColumnIndex, e.RowIndex].ReadOnly)
            {
                Dgv_Wc.BeginEdit(true);
            }
        }

        private void Dgv_Wc_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (!Dgv_Wc[e.ColumnIndex, e.RowIndex].ReadOnly)
            {
                if (Dgv_Wc[e.ColumnIndex, e.RowIndex].Value == null)
                {
                    Dgv_Wc.EndEdit();
                    return;
                }
                string _TmpValue = Dgv_Wc[e.ColumnIndex, e.RowIndex].Value.ToString();
                Dgv_Wc.EndEdit();
                if (!CLDC_DataCore.Function.Number.IsNumeric(Dgv_Wc[e.ColumnIndex, e.RowIndex].Value.ToString()))
                {
                    Dgv_Wc[e.ColumnIndex, e.RowIndex].Value = _TmpValue;
                }

            }
        }
        /// <summary>
        /// 圈数值发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Qs_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Txt_Qs.Text))
            {
				Txt_Qs.Text = "1";
                return;
            }
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Qs.Text))
            {
                MessageBoxEx.Show(this,"圈数设置必须为数字...", "设置错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Txt_Qs.Text = "1";
				return;
            }



            _xIb = Cmb_xIb.Text;

            _Qs = int.Parse(Txt_Qs.Text);

            int ThreadSum = (int)((int)Dgv_Wc.Rows.Count / 10) + 1;


            for (int i = 0; i < ThreadSum; i++)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(CheckQs), i * 10);

            }

            AsyncOpDone.WaitOne();

        }
        /// <summary>
        /// 圈数参照电流倍数发生变化时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_xIb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.Txt_Qs_TextChanged(sender, e);
        }

        /// <summary>
        /// 上限百分比
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Up_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Up.Text == string.Empty)
            {
                for (int i = 0; i < Dgv_Wc.Rows.Count; i++)
                {
                    Dgv_Wc.Rows[i].Cells[2].Value = string.Empty;
                    Dgv_Wc.Rows[i].Cells[2].Tag = null;
                }
                return;
            }

            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Up.Text))
            {
                MessageBoxEx.Show(this,"上限百分比必须为数字...", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < Dgv_Wc.Rows.Count; i++)
            {
                Dgv_Wc.Rows[i].Cells[2].Value = Math.Round((float)Dgv_Wc.Rows[i].Cells[1].Value * float.Parse(Txt_Up.Text) / 100F,2);
                Dgv_Wc.Rows[i].Cells[2].Tag = Dgv_Wc.Rows[i].Cells[2].Value;
            }

        }
        /// <summary>
        /// 下限百分比
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Down_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Down.Text == string.Empty )
            {
                for (int i = 0; i < Dgv_Wc.Rows.Count; i++)
                {
                    Dgv_Wc.Rows[i].Cells[4].Value = string.Empty;
                    Dgv_Wc.Rows[i].Cells[4].Tag = null;
                }
                return;
            }
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Down.Text))
            {
                MessageBoxEx.Show(this,"下限百分比必须为数字...", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < Dgv_Wc.Rows.Count; i++)
            {
                Dgv_Wc.Rows[i].Cells[4].Value = Math.Round(((float)Dgv_Wc.Rows[i].Cells[3].Value * float.Parse(Txt_Down.Text)) / 100F,2);
                Dgv_Wc.Rows[i].Cells[4].Tag = Dgv_Wc.Rows[i].Cells[4].Value;
            }

        }

        /// <summary>
        /// 误差限选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cmb_WcLimit_SelectionChangeCommitted(object sender, EventArgs e)
        {

            _WcLimitName = Cmb_WcLimit.Text;

            int ThreadSum = (int)((int)Dgv_Wc.Rows.Count / 10) + 1;

            for (int i = 0; i < ThreadSum; i++)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(CheckWcLimit), i * 10);
            }

            AsyncOpDone.WaitOne();

            Txt_Up.Text = (_DnbGroup.WcxUpPercent*100F).ToString();

            Txt_Down.Text = (_DnbGroup.WcxDownPercent*100F).ToString();
        }


        /// <summary>
        /// 检查方案项目参数是否被改变，如果改变就需要记录并准备下发
        /// </summary>
        /// <returns></returns>
        public bool ChangeFAPram()
        {
            bool Changed = false;

            if (_DnbGroup.CzQs != _Qs || _DnbGroup.CzIb != _xIb)
            {
                _DnbGroup.SetCzQsIb(_Qs,_xIb);
                Changed = true;
            }

            if (Txt_Up.Text != string.Empty)
            {
                if (float.Parse(Txt_Up.Text) / 100F != _DnbGroup.WcxUpPercent)
                {
                    _DnbGroup.SetWcxPercent(float.Parse(Txt_Up.Text) / 100F, _DnbGroup.WcxDownPercent);
                    Changed = true;
                }
            }
            else
            {
                if (_DnbGroup.WcxUpPercent != 1F)
                {
                    _DnbGroup.SetWcxPercent(1F, _DnbGroup.WcxDownPercent);
                    Changed = true;
                }
            }

            if (Txt_Down.Text != string.Empty)
            {
                if (float.Parse(Txt_Down.Text) / 100F != _DnbGroup.WcxDownPercent)
                {
                    _DnbGroup.SetWcxPercent(_DnbGroup.WcxUpPercent,float.Parse(Txt_Down.Text) / 100F);
                    Changed = true;
                }
            }
            else
            {
                if (_DnbGroup.WcxDownPercent != 1F)
                {
                    _DnbGroup.SetWcxPercent(_DnbGroup.WcxUpPercent, 1F);
                    Changed = true;
                }
            }

            if (_DnbGroup.CzWcLimit != _WcLimitName)
            {
                _DnbGroup.SetWcLimit(_WcLimitName);
                Changed = true;
            }
            return Changed;

        }

    }
}

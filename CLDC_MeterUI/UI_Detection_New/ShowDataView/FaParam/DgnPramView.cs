using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;using DevComponents.DotNetBar;using DevComponents;using DevComponents.AdvTree;using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.UI_Detection_New.ShowDataView.FaParam
{
    public partial class DgnPramView : UserControl
    {


        private System.Threading.AutoResetEvent AsyncOpDone = new System.Threading.AutoResetEvent(false);

        private CLDC_DataCore.Model.DnbModel.DnbGroupInfo _DnbInfo;

        private DataGridViewComboBoxCell _ProtocolNameCol = new DataGridViewComboBoxCell();

        public DgnPramView()
        {
            InitializeComponent();
        }



        public DgnPramView(ref CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup)
        {
            _DnbInfo = MeterGroup;
            InitializeComponent();

            List<string> _DgnProtoNames = CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo.getProtocolNames();

            for (int i = 0; i < _DgnProtoNames.Count; i++)
            {
                _ProtocolNameCol.Items.Add(_DgnProtoNames[i]);
            }


        }




        /// <summary>
        /// 通信地址参照条形码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Opt_Txm_CheckedChanged(object sender, EventArgs e)
        {
            this.SetControlText(Opt_Txm.Checked ? Opt_Txm.Tag.ToString() : "");
        }
        /// <summary>
        /// 通信地址参照计量编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Opt_jlbh_CheckedChanged(object sender, EventArgs e)
        {
            this.SetControlText(Opt_jlbh.Checked ? Opt_jlbh.Tag.ToString() : "");
        }
        /// <summary>
        /// 通信地址参照出厂编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Opt_Ccbh_CheckedChanged(object sender, EventArgs e)
        {
            this.SetControlText(Opt_Ccbh.Checked ? Opt_Ccbh.Tag.ToString() : "");
        }
        /// <summary>
        /// 设置通信地址获取的控制文本
        /// </summary>
        /// <param name="TxtString"></param>
        private void SetControlText(string TxtString)
        {
            Opt_DengYu.Text = Opt_DengYu.Tag.ToString() + TxtString;
            Opt_In.Text = Opt_In.Tag.ToString() + TxtString;
            groupBox2.Enabled = true;
            Opt_DengYu_CheckedChanged(new object(), new EventArgs());
            Opt_In_CheckedChanged(new object(), new EventArgs());
        }

        /// <summary>
        /// 通信地址取出厂编号或计量编号或条形码中的某一部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Opt_In_CheckedChanged(object sender, EventArgs e)
        {
            Txt_Start.Enabled = Opt_In.Checked;
            Txt_Num.Enabled = Opt_In.Checked;

            if (Opt_In.Checked)
            {
                Txt_Num_TextChanged(sender, e);
            }
        }


        /// <summary>
        /// 通信地址等于出厂编号或计量编号或条形码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Opt_DengYu_CheckedChanged(object sender, EventArgs e)
        {
            if (!Opt_DengYu.Checked) return;

            if (Opt_Txm.Checked)
            {
                SetProtoAdr(1);
            }
            if (Opt_jlbh.Checked)
            {
                SetProtoAdr(2);
            }
            if (Opt_Ccbh.Checked)
            {
                SetProtoAdr(3);
            }
        }

        /// <summary>
        /// 全部等于通信地址
        /// </summary>
        /// <param name="ColIndex"></param>
        private void SetProtoAdr(int ColIndex)
        {
            this.SetProtoAdr(ColIndex, -1, -1);
        }
        /// <summary>
        /// 从中取部分为通信地址
        /// </summary>
        /// <param name="ColIndex"></param>
        /// <param name="StartNum"></param>
        /// <param name="LenNum"></param>
        private void SetProtoAdr(int ColIndex, int StartNum, int LenNum)
        {
            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Dgv_Data.Rows[i].Cells[ColIndex].Value == null || Dgv_Data.Rows[i].Cells[ColIndex].Value.ToString() == string.Empty) continue;

                string AdrValue = Dgv_Data.Rows[i].Cells[ColIndex].Value.ToString();

                if (StartNum == -1 || LenNum == -1)
                {
                    Dgv_Data.Rows[i].Cells[6].Value = AdrValue;
                }
                else
                {
                    if (LenNum == 0)
                    {
                        Dgv_Data.Rows[i].Cells[6].Value = "";
                    }
                    else if (AdrValue.Length < StartNum + LenNum)
                    {
                        Dgv_Data.Rows[i].Cells[6].Value = AdrValue.Substring(StartNum);
                    }
                    else
                    {
                        Dgv_Data.Rows[i].Cells[6].Value = AdrValue.Substring(StartNum, LenNum);
                    }
                }
            }

        }

        /// <summary>
        /// 从---开始---取多少位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Start_TextChanged(object sender, EventArgs e)
        {
            this.Txt_Num_TextChanged(sender, e);
        }

        /// <summary>
        /// 从---开始---取多少位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Num_TextChanged(object sender, EventArgs e)
        {
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Start.Text))
            {
                Txt_Start.Text = string.Empty;
                return;
            }
            if (!CLDC_DataCore.Function.Number.IsNumeric(Txt_Num.Text))
            {
                Txt_Num.Text = string.Empty;
                return;
            }

            if (int.Parse(Txt_Start.Text) < 0)
            {
                Txt_Start.Text = string.Empty;
                return;
            }
            if (int.Parse(Txt_Num.Text) < 0)
            {
                Txt_Num.Text = string.Empty;
                return;
            }

            if (Opt_Txm.Checked)
            {
                SetProtoAdr(1, int.Parse(Txt_Start.Text), int.Parse(Txt_Num.Text));
            }
            if (Opt_jlbh.Checked)
            {
                SetProtoAdr(2, int.Parse(Txt_Start.Text), int.Parse(Txt_Num.Text));
            }
            if (Opt_Ccbh.Checked)
            {
                SetProtoAdr(3, int.Parse(Txt_Start.Text), int.Parse(Txt_Num.Text));
            }



        }

        private void DgnPramView_Load(object sender, EventArgs e)
        {
            this.CreateGrid();
        }

        private void CreateGrid()
        {
            Dgv_Data.SuspendLayout();
            if (Dgv_Data.Rows.Count != 0) Dgv_Data.Rows.Clear();

            for (int i = 0; i < _DnbInfo.MeterGroup.Count; i++)
            {
                int _RowIndex = Dgv_Data.Rows.Add();
                Dgv_Data.Rows[_RowIndex].Cells[0].Style.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Frone;
            }

            int ThreadSum = ((int)Dgv_Data.Rows.Count / 10) + 1;

            for (int i = 0; i < ThreadSum; i++)
            {
                System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(InsertGridRow), i * 10);
            }
            AsyncOpDone.WaitOne();
            Dgv_Data.ResumeLayout();
        }


        private object Locked = new object();

        /// <summary>
        /// 线程处理一个线程最多处理10行数据
        /// </summary>
        /// <param name="obj"></param>
        private void InsertGridRow(object obj)
        {
            int _firstRowIndex = (int)obj;

            for (int Index = _firstRowIndex; Index < _firstRowIndex + 10; Index++)
            {
                lock (Locked)
                {
                    if (Index >= Dgv_Data.Rows.Count)
                    {
                        AsyncOpDone.Set();
                        return;
                    }

                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo = _DnbInfo.MeterGroup[Index];

                    Dgv_Data.Rows[Index].Cells[0].Value = MeterInfo.ToString();

                    Dgv_Data.Rows[Index].Cells[4] = (DataGridViewComboBoxCell)_ProtocolNameCol.Clone();

                    if (MeterInfo.YaoJianYn)
                    {
                        Dgv_Data.Rows[Index].Cells[1].Value = MeterInfo.Mb_ChrTxm;
                        Dgv_Data.Rows[Index].Cells[2].Value = MeterInfo.Mb_ChrJlbh;
                        Dgv_Data.Rows[Index].Cells[3].Value = MeterInfo.Mb_ChrCcbh;
                        if (MeterInfo.DgnProtocol.Loading)
                        {
                            Dgv_Data.Rows[Index].Cells[4].Value = MeterInfo.DgnProtocol.ProtocolName;
                            Dgv_Data.Rows[Index].Cells[5].Value = MeterInfo.DgnProtocol.Setting;
                            Dgv_Data.Rows[Index].Cells[6].Value = MeterInfo.Mb_chrAddr;
                            Dgv_Data.Rows[Index].Cells[7].Value = MeterInfo.DgnProtocol.WritePassword + MeterInfo.DgnProtocol.WriteClass;
                            Dgv_Data.Rows[Index].Cells[7].Tag = Dgv_Data.Rows[Index].Cells[7].Value;
                            Dgv_Data.Rows[Index].Cells[8].Value = MeterInfo.DgnProtocol.ClearDemandPassword + MeterInfo.DgnProtocol.ClearDemandClass;
                            Dgv_Data.Rows[Index].Cells[8].Tag = Dgv_Data.Rows[Index].Cells[8].Value;
                            Dgv_Data.Rows[Index].Cells[9].Value = MeterInfo.DgnProtocol.ClearDLPassword + MeterInfo.DgnProtocol.ClearDLClass;
                            Dgv_Data.Rows[Index].Cells[9].Tag = Dgv_Data.Rows[Index].Cells[9].Value;
                        }

                        if (Index % 2 == 0)         //设置单双行背景颜色
                        {
                            Dgv_Data.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                        }
                        else
                        {
                            Dgv_Data.Rows[Index].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                        }

                    }
                    else            //设置不检表的行背景颜色
                    {
                        Dgv_Data.Rows[Index].DefaultCellStyle.BackColor = Color.FromName(System.Drawing.KnownColor.Control.ToString());
                        Dgv_Data.Rows[Index].ReadOnly = true;
                    }
                }
            }

            AsyncOpDone.Set();

        }

        private void Dgv_Data_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            Dgv_Data.EndEdit();
        }

        private void SetNewProtocolInfo(CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo Protocol, int RowIndex)
        {
            Dgv_Data.Rows[RowIndex].Cells[4].Value = Protocol.ProtocolName;
            Dgv_Data.Rows[RowIndex].Cells[5].Value = Protocol.Setting;
            Dgv_Data.Rows[RowIndex].Cells[7].Value = Protocol.WritePassword + Protocol.WriteClass;
            Dgv_Data.Rows[RowIndex].Cells[7].Tag = Dgv_Data.Rows[RowIndex].Cells[7].Value;
            Dgv_Data.Rows[RowIndex].Cells[8].Value = Protocol.ClearDemandPassword + Protocol.ClearDemandClass;
            Dgv_Data.Rows[RowIndex].Cells[8].Tag = Dgv_Data.Rows[RowIndex].Cells[8].Value;
            Dgv_Data.Rows[RowIndex].Cells[9].Value = Protocol.ClearDLPassword + Protocol.ClearDLClass;
            Dgv_Data.Rows[RowIndex].Cells[9].Tag = Dgv_Data.Rows[RowIndex].Cells[9].Value;
        }





        private void Dgv_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                return;
            }
            if (Dgv_Data[e.ColumnIndex, e.RowIndex].ReadOnly) return;

            Dgv_Data.BeginEdit(true);
            if (e.ColumnIndex == 4)
            {
                Dgv_Data[e.ColumnIndex, e.RowIndex].Tag = Dgv_Data[e.ColumnIndex, e.RowIndex].Value;
            }
        }
        /// <summary>
        /// 第4列（协议名称）选择变化触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Data_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex == 4)
            {
                if (Dgv_Data[e.ColumnIndex, e.RowIndex].Tag != null)           //如果还没有初始化完成则退出
                {
                    if (Dgv_Data[e.ColumnIndex, e.RowIndex].Tag == Dgv_Data[e.ColumnIndex, e.RowIndex].Value)
                    {
                        return;
                    }
                }
                else
                {
                    Dgv_Data[e.ColumnIndex, e.RowIndex].Tag = string.Empty;
                }
                CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo _Protocol = new CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo(Dgv_Data[e.ColumnIndex, e.RowIndex].Value.ToString());

                if (!_Protocol.Loading)      //如果打开协议失败，则警告并返回
                {
                    //MessageBoxEx.Show(this,"协议加载失败，请选择其他协议，或联系供应商解决...", "加载失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Dgv_Data[e.ColumnIndex, e.RowIndex].Value = Dgv_Data[e.ColumnIndex, e.RowIndex].Tag;
                    return;
                }

                if (e.RowIndex == 0)            //如果是第一行，则全部相同
                {
                    for (int i = 0; i < Dgv_Data.Rows.Count; i++)
                    {
                        if (Dgv_Data.Rows[i].Cells[1].Value == null || Dgv_Data.Rows[i].Cells[1].Value.ToString() == string.Empty) continue;

                        this.SetNewProtocolInfo(_Protocol, i);
                    }
                }
                else
                {
                    this.SetNewProtocolInfo(_Protocol, e.RowIndex);
                }
                Dgv_Data[e.ColumnIndex, e.RowIndex].Tag = Dgv_Data[e.ColumnIndex, e.RowIndex].Value;
            }
        }

        private void Dgv_Data_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
            if (e.ColumnIndex == 4 && Dgv_Data[e.ColumnIndex, e.RowIndex].IsInEditMode)            //协议下拉框选择
                Dgv_Data.EndEdit();
        }


        #region  设置密码全部相同
        /// <summary>
        /// 写密码相同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_WritePwd_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Txt_WritePwd.Text == string.Empty)
                {
                    Dgv_Data.Rows[i].Cells[7].Value = Dgv_Data.Rows[i].Cells[7].Tag;
                }
                else
                {
                    Dgv_Data.Rows[i].Cells[7].Value = Txt_WritePwd.Text;
                }
            }
        }

        /// <summary>
        /// 清需量密码相同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_ClearXl_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Txt_WritePwd.Text == string.Empty)
                {
                    Dgv_Data.Rows[i].Cells[8].Value = Dgv_Data.Rows[i].Cells[8].Tag;
                }
                else
                {
                    Dgv_Data.Rows[i].Cells[8].Value = Txt_ClearXl.Text;
                }
            }

        }
        /// <summary>
        /// 清电量密码相同
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_ClearDl_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                if (Txt_WritePwd.Text == string.Empty)
                {
                    Dgv_Data.Rows[i].Cells[9].Value = Dgv_Data.Rows[i].Cells[9].Tag;
                }
                else
                {
                    Dgv_Data.Rows[i].Cells[9].Value = Txt_ClearDl.Text;
                }
            }

        }

        #endregion

        /// <summary>
        /// 如果项目参数改变
        /// </summary>
        /// <returns></returns>
        public bool ChangeFAPram()
        {
            bool Changed = false;

            CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo _Protocol = new CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo();

            for (int i = 0; i < _DnbInfo.MeterGroup.Count; i++)
            {
                if (!_DnbInfo.MeterGroup[i].YaoJianYn) continue;
                if (Dgv_Data.Rows.Count <= i) return Changed;
                if (Dgv_Data.Rows[i].Cells[4].Value == null || Dgv_Data.Rows[i].Cells[4].Value.ToString() == string.Empty)         //如果没有协议就不需要处理了！
                {
                    if (_DnbInfo.MeterGroup[i].DgnProtocol.Loading)
                    {
                        Changed = true;
                        _DnbInfo.MeterGroup[i].DgnProtocol = _Protocol;             //赋值一个空的协议
                    }
                    continue;
                }
                if (!_DnbInfo.MeterGroup[i].DgnProtocol.Loading
                    || _DnbInfo.MeterGroup[i].DgnProtocol.ProtocolName != Dgv_Data.Rows[i].Cells[4].Value.ToString())         //如果协议为空，或者当前选择协议和模型中协议不一致，则重新创建
                {
                    CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo _NewProtocol = new CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo(Dgv_Data.Rows[i].Cells[4].Value.ToString());
                    if (_NewProtocol.Loading)           //如果新选择的协议加载成功则改变，否则不做任何操作
                    {
                        Changed = true;
                        _DnbInfo.MeterGroup[i].DgnProtocol = _NewProtocol;
                        _DnbInfo.MeterGroup[i].AVR_PROTOCOL_NAME = _NewProtocol.ProtocolName;
                    }
                }
                if (!_DnbInfo.MeterGroup[i].DgnProtocol.Loading) continue;

                CLDC_DataCore.Model.DgnProtocol.DgnProtocolInfo _MeterProtocol = _DnbInfo.MeterGroup[i].DgnProtocol;

                if (_MeterProtocol.Setting != Dgv_Data.Rows[i].Cells[5].Value.ToString())
                {
                    _MeterProtocol.Setting = Dgv_Data.Rows[i].Cells[5].Value.ToString();
                    Changed = true;
                }

                #region ----------------------------------------------密码修改---------------------------------------------------------
                if (_MeterProtocol.WritePassword + _MeterProtocol.WriteClass != Dgv_Data.Rows[i].Cells[7].Value.ToString())     //写表密码
                {
                    Changed = true;
                    try
                    {
                        if (_MeterProtocol.WriteClass != string.Empty)         //如果等级不空，则表示是645的协议，如果等级空则是其他协议，不带写等级的
                        {
                            _MeterProtocol.WriteClass = Dgv_Data.Rows[i].Cells[7].Value.ToString().Substring(Dgv_Data.Rows[i].Cells[7].Value.ToString().Length - 2);

                            _MeterProtocol.WritePassword = Dgv_Data.Rows[i].Cells[7].Value.ToString().Substring(0, Dgv_Data.Rows[i].Cells[7].Value.ToString().Length - 2);
                        }
                        else
                        {
                            _MeterProtocol.WritePassword = Dgv_Data.Rows[i].Cells[7].Value.ToString();
                        }
                    }
                    catch
                    { }
                }

                if (_MeterProtocol.ClearDemandPassword + _MeterProtocol.ClearDemandClass != Dgv_Data.Rows[i].Cells[7].Value.ToString())     //清需量密码
                {
                    Changed = true;
                    try
                    {
                        if (_MeterProtocol.ClearDemandClass != string.Empty)         //如果等级不空，则表示是645的协议，如果等级空则是其他协议，不带写等级的
                        {
                            _MeterProtocol.ClearDemandClass = Dgv_Data.Rows[i].Cells[8].Value.ToString().Substring(Dgv_Data.Rows[i].Cells[8].Value.ToString().Length - 2);

                            _MeterProtocol.ClearDemandPassword = Dgv_Data.Rows[i].Cells[8].Value.ToString().Substring(0, Dgv_Data.Rows[i].Cells[8].Value.ToString().Length - 2);
                        }
                        else
                        {
                            _MeterProtocol.ClearDemandPassword = Dgv_Data.Rows[i].Cells[8].Value.ToString();
                        }
                    }
                    catch
                    { }
                }
                if (_MeterProtocol.ClearDLPassword + _MeterProtocol.ClearDLClass != Dgv_Data.Rows[i].Cells[7].Value.ToString())         //清电量密码
                {
                    Changed = true;
                    try
                    {
                        if (_MeterProtocol.ClearDLClass != string.Empty)         //如果等级不空，则表示是645的协议，如果等级空则是其他协议，不带写等级的
                        {
                            _MeterProtocol.ClearDLClass = Dgv_Data.Rows[i].Cells[9].Value.ToString().Substring(Dgv_Data.Rows[i].Cells[9].Value.ToString().Length - 2);

                            _MeterProtocol.ClearDLPassword = Dgv_Data.Rows[i].Cells[9].Value.ToString().Substring(0, Dgv_Data.Rows[i].Cells[9].Value.ToString().Length - 2);
                        }
                        else
                        {
                            _MeterProtocol.ClearDLPassword = Dgv_Data.Rows[i].Cells[9].Value.ToString();
                        }
                    }
                    catch
                    { }
                }

                #endregion

                if (Dgv_Data.Rows[i].Cells[6].Value != null && Dgv_Data.Rows[i].Cells[6].Value.ToString() != string.Empty)
                {
                    if (_DnbInfo.MeterGroup[i].Mb_chrAddr != Dgv_Data.Rows[i].Cells[6].Value.ToString())
                    {
                        Changed = true;

                        _DnbInfo.MeterGroup[i].Mb_chrAddr = Dgv_Data.Rows[i].Cells[6].Value.ToString();
                    }
                }
            }

            return Changed;
        }


    }
}

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

namespace CLDC_MeterUI.UI_FA.FAPrj.PrjUI
{
    public partial class UI_QianDongPrj : UserControl
    {
        private CLDC_Comm.Enum.Cus_PowerFangXiang _Glfx = new CLDC_Comm.Enum.Cus_PowerFangXiang();
        /// <summary>
        /// ���캯��
        /// </summary>
        public UI_QianDongPrj()
        {
            InitializeComponent();

            if (CLDC_DataCore.Const.GlobalUnit.User_Jyy.Level == 0)
            {
                Chk_Default.Enabled = true;
            }
            else
            {
                Chk_Default.Enabled = false;
            }

        }

        #region --------------�¼�-------------------------
        /// <summary>
        /// �÷�����Ŀ�Ƿ�Ҫ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk_Glfx_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_Glfx.Checked == true)
            {
                if (!(Chk_80.Checked | Chk_90.Checked | Chk_100.Checked | Chk_110.Checked))
                    Chk_115.Checked = true;
                if (!(Rb_02xIb.Checked | Rb_025xIb.Checked))
                    Rb_Zero.Checked = true;
                if (!(Rb_CustomT.Checked))
                    Rb_AutoT.Checked = true;
            }


            TabLay_Pz.Enabled = Chk_Glfx.CheckState == CheckState.Unchecked ? false : true;
        }
        /// <summary>
        /// �Ƿ�����Զ���Ǳ��ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rb_CustomT_CheckedChanged(object sender, EventArgs e)
        {
            Txt_T.Enabled = Rb_CustomT.Checked;
        }

        #endregion


        [DefaultValue(CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�)]
        [Description("���ʷ���")]
        public CLDC_Comm.Enum.Cus_PowerFangXiang Glfx
        {
            get
            {
                return _Glfx;
            }
            set
            {
                _Glfx = value;
                Chk_Glfx.Text = _Glfx.ToString();
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="FaItem"></param>
        public void LoadFA(CLDC_DataCore.Model.Plan.Plan_QianDong FaItem)
        {
            this.ClearData();

            for (int i = 0; i < FaItem.Count; i++)
            {
                StPlan_QianDong _Obj = FaItem.getQianDongPrj(i);

                if (_Obj.PowerFangXiang != _Glfx) continue;


                Chk_Glfx.CheckState = CheckState.Checked;
                Chk_Default.CheckState = _Obj.DefaultValue == 0 ? CheckState.Unchecked : CheckState.Checked;

                if (_Obj.FloatxU == 0.8F)
                {
                    Chk_80.CheckState = CheckState.Checked;
                }
                else if (_Obj.FloatxU == 0.9F)
                {
                    Chk_90.CheckState = CheckState.Checked;
                }
                else if (_Obj.FloatxU == 1.0F)
                {
                    Chk_100.CheckState = CheckState.Checked;
                }
                else if (_Obj.FloatxU == 1.1F)
                {
                    Chk_110.CheckState = CheckState.Checked;
                }
                else if (_Obj.FloatxU == 1.15F)
                {
                    Chk_115.CheckState = CheckState.Checked;
                }

                if (_Obj.FloatxIb == 0)
                {
                    Rb_Zero.Checked = true;
                }
                else if (_Obj.FloatxIb == 0.2F)
                {
                    Rb_02xIb.Checked = true;
                }
                else if (_Obj.FloatxIb == 0.25F)
                {
                    Rb_025xIb.Checked = true;
                }

                if (_Obj.xTime == 0F)
                {
                    Rb_AutoT.Checked = true;
                }
                else
                {
                    Rb_CustomT.Checked = true;
                    Txt_T.Text = _Obj.xTime.ToString();
                }
            }

        }

        /// <summary>
        /// ��������
        /// </summary>
        public void ClearData()
        {
            Chk_Glfx.CheckState = CheckState.Unchecked;
            Chk_Default.CheckState = CheckState.Unchecked;
            Chk_80.CheckState = CheckState.Unchecked;
            Chk_90.CheckState = CheckState.Unchecked;
            Chk_100.CheckState = CheckState.Unchecked;
            Chk_110.CheckState = CheckState.Unchecked;
            Chk_115.CheckState = CheckState.Unchecked;
            Rb_Zero.Checked = false;
            Rb_025xIb.Checked = false;
            Rb_02xIb.Checked = false;
            Rb_AutoT.Checked = false;
            Rb_CustomT.Checked = false;
            Txt_T.Text = string.Empty;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="FaItem"></param>
        public void Copy(ref CLDC_DataCore.Model.Plan.Plan_QianDong FaItem)
        {
            if (Chk_Glfx.CheckState == CheckState.Unchecked) return;

            StPlan_QianDong _Item = new StPlan_QianDong();

            _Item.PowerFangXiang = this._Glfx;
            _Item.DefaultValue = this.Chk_Default.CheckState == CheckState.Checked ? 1 : 0;
            if (Rb_Zero.Checked)            //���ӵ���
            {
                _Item.FloatxIb = 0F;
            }
            else if (Rb_02xIb.Checked)      //��1/5���𶯵���
            {
                _Item.FloatxIb = 0.2F;
            }
            else if (Rb_025xIb.Checked)     //��0.25���𶯵���
            {
                _Item.FloatxIb = 0.25F;
            }
            if (Rb_AutoT.Checked)           //�����Ǳ��ʱ��
            {
                _Item.xTime = 0F;
            }
            if (Rb_CustomT.Checked)         //�Զ���Ǳ��ʱ��
            {
                if (CLDC_DataCore.Function.Number.IsNumeric(Txt_T.Text)) //����ı���Ǳ��ʱ��Ϊ����
                {
                    _Item.xTime = float.Parse(Txt_T.Text);
                }
                else
                {
                    _Item.xTime = 0F;        //��Ϊ���־Ͱ������
                }
            }

            if (Chk_80.CheckState == CheckState.Checked)
            {
                FaItem.Add(_Item.PowerFangXiang, 0.8F, _Item.FloatxIb, _Item.xTime, _Item.DefaultValue);
            }
            if (Chk_90.CheckState == CheckState.Checked)
            {
                FaItem.Add(_Item.PowerFangXiang, 0.9F, _Item.FloatxIb, _Item.xTime, _Item.DefaultValue);
            }
            if (Chk_100.CheckState == CheckState.Checked)
            {
                FaItem.Add(_Item.PowerFangXiang, 1F, _Item.FloatxIb, _Item.xTime, _Item.DefaultValue);
            }
            if (Chk_110.CheckState == CheckState.Checked)
            {
                FaItem.Add(_Item.PowerFangXiang, 1.1F, _Item.FloatxIb, _Item.xTime, _Item.DefaultValue);
            }
            if (Chk_115.CheckState == CheckState.Checked)
            {
                FaItem.Add(_Item.PowerFangXiang, 1.15F, _Item.FloatxIb, _Item.xTime, _Item.DefaultValue);
            }
        }

    }
}

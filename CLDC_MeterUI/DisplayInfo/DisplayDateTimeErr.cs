using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.DisplayInfo
{
    public partial class DisplayDateTimeErr : Base
    {
        /// <summary>
        /// 日计时误差项目ID,化整值
        /// </summary>
        private const string Key = "00201";
        /// <summary>
        /// 日计时误差项目ID，1-5次误差
        /// </summary>
        private const string Key1to5 = "00202";

        /// <summary>
        /// 日计时误差项目ID,6-10次误差
        /// </summary>
        private const string Key6to10 = "00203";

        public DisplayDateTimeErr()
        {
            InitializeComponent();
        }

        public DisplayDateTimeErr(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
            : base(MeterInfo, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterInfo, AllowEdit);
        }
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {
            Data_View.Rows.Clear();
                        
            int RowIndex = Data_View.Rows.Add();
            if ((RowIndex + 1) % 2 == 0)
            {
                Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
            }
            else
            {
                Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
            }
            Data_View.Rows[RowIndex].HeaderCell.Value = MeterInfo.ToString();


            if (!MeterInfo.MeterDgns.ContainsKey(Key1to5))
                return;
            if (MeterInfo.MeterDgns[Key1to5].Md_chrValue == null)
                return;
            string[] _Values = MeterInfo.MeterDgns[Key1to5].Md_chrValue.Split('|');

            if (_Values.Length != 5) return;

            for (int j = 0; j < _Values.Length; j++)
            {
                Data_View.Rows[0].Cells[j].Value = _Values[j];
            }

            if (!MeterInfo.MeterDgns.ContainsKey(Key6to10))
                return;
            if (MeterInfo.MeterDgns[Key6to10].Md_chrValue == null)
                return;
            _Values = MeterInfo.MeterDgns[Key6to10].Md_chrValue.Split('|');

            if (_Values.Length != 5) return;

            for (int j = 0; j < _Values.Length; j++)
            {
                Data_View.Rows[0].Cells[j + 5].Value = _Values[j];
            }

            if (!MeterInfo.MeterDgns.ContainsKey(Key))
                return;

            Data_View.Rows[0].Cells[Data_View.Columns.Count - 1].Value = MeterInfo.MeterDgns[Key].Md_chrValue;
        
        }
        public DisplayDateTimeErr(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
            : base(MeterGroup, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterGroup, AllowEdit);
        }
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            Data_View.Rows.Clear();

            for (int i = 0; i < MeterGroup._Bws; i++)
            {
                //if (!MeterGroup.MeterGroup[i].YaoJianYn) continue;
                int RowIndex = Data_View.Rows.Add();
                if ((RowIndex + 1) % 2 == 0)
                {
                    Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Alter;
                }
                else
                {
                    Data_View.Rows[RowIndex].DefaultCellStyle.BackColor = CLDC_DataCore.Const.Variable.Color_Grid_Normal;
                }
                Data_View.Rows[RowIndex].HeaderCell.Value = MeterGroup.MeterGroup[i].ToString();


                if (!MeterGroup.MeterGroup[i].MeterDgns.ContainsKey(Key1to5))
                    return;
                if (MeterGroup.MeterGroup[i].MeterDgns[Key1to5].Md_chrValue == null)
                    return;
                string[] _Values = MeterGroup.MeterGroup[i].MeterDgns[Key1to5].Md_chrValue.Split('|');

                if (_Values.Length != 5) return;

                for (int j = 0; j < _Values.Length; j++)
                {
                    Data_View.Rows[RowIndex].Cells[j].Value = _Values[j];
                }

                if (!MeterGroup.MeterGroup[i].MeterDgns.ContainsKey(Key6to10))
                    return;
                if (MeterGroup.MeterGroup[i].MeterDgns[Key6to10].Md_chrValue == null)
                    return;
                _Values = MeterGroup.MeterGroup[i].MeterDgns[Key6to10].Md_chrValue.Split('|');
                                
                if (_Values.Length != 5) return;

                for (int j = 0; j < _Values.Length; j++)
                {
                    Data_View.Rows[RowIndex].Cells[j + 5].Value = _Values[j];
                }

                if (!MeterGroup.MeterGroup[i].MeterDgns.ContainsKey(Key))
                    return;

                Data_View.Rows[RowIndex].Cells[Data_View.Columns.Count - 1].Value = MeterGroup.MeterGroup[i].MeterDgns[Key].Md_chrValue;
            }
        }
    }
}

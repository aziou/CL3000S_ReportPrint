using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.DisplayInfo
{
    public partial class DisplayWGJC : Base
    {
        string ItemKey = "100";
        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo _MeterInfo = null;
        public DisplayWGJC()
        {
            InitializeComponent();
        }
        public DisplayWGJC(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
            : base(MeterInfo, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterInfo, AllowEdit);
        }
        /// <summary>
        /// 显示一个电表的数据
        /// </summary>
        /// <param name="MeterInfo"></param>
        /// <param name="allowedit"></param>
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {

            if (MeterInfo.MeterResults.Count == 0)
            {
                _MeterInfo = null;
                return;
            }
            _MeterInfo = MeterInfo;
            Dgw_Data.Rows.Clear();

            if (MeterInfo.MeterResults.ContainsKey(ItemKey))
            {

                int rowIndex = Dgw_Data.Rows.Add();
                Dgw_Data["表位", rowIndex].Value = MeterInfo.ToString();
                Dgw_Data["项目名称", rowIndex].Value = MeterInfo.MeterResults[ItemKey].Mr_chrRstName;
                Dgw_Data["项目结论", rowIndex].Value = "    " + MeterInfo.MeterResults[ItemKey].Mr_chrRstValue;

            }


            base.SetData(MeterInfo, allowedit);
        }
        /// <summary>
        /// 显示所有要检电表的数据
        /// </summary>
        /// <param name="MeterGroup"></param>
        /// <param name="allowedit"></param>
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {

            Dgw_Data.Rows.Clear();
            for (int i = 0; i < MeterGroup._Bws; i++)
            {

                if (MeterGroup.MeterGroup[i].YaoJianYn == false) continue;
                MeterInfo = MeterGroup.MeterGroup[i];
                if (MeterInfo.MeterResults.ContainsKey(ItemKey))
                {
                    int rowIndex = Dgw_Data.Rows.Add();
                    Dgw_Data["表位", rowIndex].Value = MeterInfo.ToString();
                    Dgw_Data["项目名称", rowIndex].Value = MeterInfo.MeterResults[ItemKey].Mr_chrRstName;
                    Dgw_Data["项目结论", rowIndex].Value = "    " + MeterInfo.MeterResults[ItemKey].Mr_chrRstValue;

                }

            }
            SpanRow(0, Dgw_Data.Rows.Count, 0);
            base.SetData(MeterGroup, allowedit);

        }
        private void Dgw_Data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_MeterInfo == null) return;
            if (_MeterInfo.MeterResults.ContainsKey(ItemKey) == false) return;
            string curValue = Dgw_Data.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Trim();
            string value = _MeterInfo.MeterResults[ItemKey].Mr_chrRstValue;

            if (value != curValue)
            {
             
                CLDC_DataCore.DataBase.DataControl datacontrl = new CLDC_DataCore.DataBase.DataControl( );   
                if (datacontrl == null) return;
                if (datacontrl.Connection == false) return;
                string TableName = "METER_RESULTS";     
                string Updatesql = string.Format("Update " + TableName + " Set AVR_RESULT_VALUE='{0}' WHERE FK_LNG_METER_ID='{1}' AND AVR_RESULT_ID='{2}'", curValue, _MeterInfo._intMyId, ItemKey);

                datacontrl.ExecuteSql(Updatesql);
             
                datacontrl.CloseDB();
                

            }
        }

        private void SpanRow(int startRowIndex, int rowCount, int col)
        {
            if (rowCount == 0) return;

            int rowSpan = 1;

            int intstart = startRowIndex;

            for (int i = startRowIndex + 1; i < rowCount + intstart; i++)
            {
                if (Dgw_Data[col, i].Value.ToString() == Dgw_Data[col, startRowIndex].Value.ToString())
                {
                    rowSpan++;
                }
                else
                {
                    ((CLZDataGridView.DataGridViewTextBoxCellEx)Dgw_Data[col, startRowIndex]).RowSpan = rowSpan;
                    if (col < 2)
                    {
                        SpanRow(startRowIndex, rowSpan, col + 1);
                    }
                    startRowIndex = i;
                    rowSpan = 1;
                }

            }

            ((CLZDataGridView.DataGridViewTextBoxCellEx)Dgw_Data[col, startRowIndex]).RowSpan = rowSpan;

            if (col < 2)
            {
                SpanRow(startRowIndex, rowSpan, col + 1);
            }
        }
    }
}

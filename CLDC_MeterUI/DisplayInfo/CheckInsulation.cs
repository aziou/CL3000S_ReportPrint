using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents;
using DevComponents.AdvTree;
using DevComponents.DotNetBar.Controls;

namespace CLDC_MeterUI.DisplayInfo
{
    /// <summary>
    /// �๦�ܼ춨����
    /// </summary>
    public partial class CheckInsulation : Base
    {
        public CheckInsulation()
        {
            InitializeComponent();
        }
        public CheckInsulation(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
            : base(meterInfo, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;
            SetData(MeterInfo, AllowEdit);
        }

        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        {
            if (meterInfo.MeterCarrierDatas.Count == 0) return;

            foreach (string _Key in meterInfo.MeterInsulations.Keys)
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterInsulation insulation = meterInfo.MeterInsulations[_Key];

                string checkName = string.Empty;

                if (insulation.InsulationType == ((int)CLDC_DataCore.Struct.StInsulationParam.EnumInsulationType.DigitalEarth).ToString())
                    checkName = "�������Ӷ������ѹ";
                else if (insulation.InsulationType == ((int)CLDC_DataCore.Struct.StInsulationParam.EnumInsulationType.AnalogEarth).ToString())
                    checkName = "ģ�������Ӷ������ѹ";
                else
                    checkName = "���Ӽ���ѹ";

                Dgw_Data.Rows.Add(checkName, insulation.stringCurrent, insulation.Result);
            }

            base.SetData(meterInfo, allowedit);

        }
    }
}

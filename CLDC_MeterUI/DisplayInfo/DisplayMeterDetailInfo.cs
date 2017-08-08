using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.DisplayInfo
{


    public partial class DisplayMeterDetailInfo :  Base
    {
        /// <summary>
        /// 指示当前误差是否全部显示 = false 着只显示不合格误差数据
        /// </summary>
        public bool IsDisplayAll = false;

        public DisplayMeterDetailInfo()
        {
            InitializeComponent();
        }

        public DisplayMeterDetailInfo(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
            : base(MeterGroup, allowedit)
        {
            InitializeComponent();
            if (CLDC_DataCore.Function.Common.IsVSDevenv()) return;

            SetData(MeterGroup, AllowEdit);
        }

        void Chk_DisplayAll_CheckedChanged(object sender, EventArgs e)
        {
            SetData(_MeterGroup, AllowEdit);
        }
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, bool allowedit)
        {
            base.SetData(MeterInfo, allowedit);

            IsDisplayAll = true;

            checkWC_Normal1.IsDisplayAll = IsDisplayAll;
            checkWC_PianCha1.IsDisplayAll = IsDisplayAll;

            checkQiQianDong1.SetData(MeterInfo, allowedit);
            checkWC_Normal1.SetData(MeterInfo, allowedit);
            checkWC_PianCha1.SetData(MeterInfo, allowedit);
            checkZZ1.SetData(MeterInfo, allowedit);
            checkConn1.SetData(MeterInfo, allowedit);
            checkDgn1.SetData(MeterInfo, allowedit);
            checkCarrier1.SetData(MeterInfo, allowedit);
            checkESAMDataReturn1.SetData(MeterInfo, allowedit);
            checkDateTimeErr1.SetData(MeterInfo, allowedit);
            checkReadPeriod1.SetData(MeterInfo, allowedit);
            checkSdtq1.SetData(MeterInfo, allowedit);
            checkRegister1.SetData(MeterInfo, allowedit);
            checkRatePeriod1.SetData(MeterInfo, allowedit);

            displayCost1.SetData(MeterInfo, allowedit);
            displaySpecial1.SetData(MeterInfo, allowedit);//影响量
            displayWGJC1.SetData(MeterInfo, true );//外观检查
           //误差一致性
            checkErrAccord1.SetData(MeterInfo, allowedit);
            checkContrast1.SetData(MeterInfo, allowedit);
            checkUpDown1.SetData(MeterInfo, allowedit);
            checkOver1.SetData(MeterInfo, allowedit);
            //功能
            displayFunction1.SetData(MeterInfo, allowedit);
            viewComputationData1.SetData(MeterInfo, allowedit);
            viewTimingData1.SetData(MeterInfo, allowedit);
            viewRatePeriodFunction1.SetData(MeterInfo, allowedit);
            viewShowFunction1.SetData(MeterInfo, allowedit);
            viewMaxDemand1.SetData(MeterInfo, allowedit);
            viewLoadRecord1.SetData(MeterInfo, allowedit);
            //事件
            displayEventLog1.SetData(MeterInfo, allowedit);
            viewEventPrograme1.SetData(MeterInfo, allowedit);
            viewEventClearEnergy1.SetData(MeterInfo, allowedit);
            viewEventLoseVoltage1.SetData(MeterInfo, allowedit);
            viewEventLoseCurrent1.SetData(MeterInfo, allowedit);
            viewEventLosePhase1.SetData(MeterInfo, allowedit);
            viewEventOverLoad1.SetData(MeterInfo, allowedit);
            viewEventACdump1.SetData(MeterInfo, allowedit);
            viewEventOpenCover1.SetData(MeterInfo, allowedit);
            viewEventReversePhase1.SetData(MeterInfo, allowedit);
            viewEventOpenButtonCover1.SetData(MeterInfo, allowedit);
            viewEventOverCurrent1.SetData(MeterInfo, allowedit);
            viewEventReversePhaseI1.SetData(MeterInfo, allowedit);
            
            viewEventOverVoltage1.SetData(MeterInfo, allowedit);
            viewEventUnderVoltage1.SetData(MeterInfo, allowedit);
            viewEventImbalanceU1.SetData(MeterInfo, allowedit);
            viewEventImbalanceI1.SetData(MeterInfo, allowedit);
            viewEventStopCurrent1.SetData(MeterInfo, allowedit);
            viewEventLoseFullVoltage1.SetData(MeterInfo, allowedit);
            viewEventCalibrationTime1.SetData(MeterInfo, allowedit);
            viewEventClearDemand1.SetData(MeterInfo, allowedit);
            viewEventClearEvent1.SetData(MeterInfo, allowedit);
            viewEventReversePower1.SetData(MeterInfo, allowedit);
            viewEventReverseTrend1.SetData(MeterInfo, allowedit);
            viewEventTransPF1.SetData(MeterInfo, allowedit);
            //冻结
            displayFreeze1.SetData(MeterInfo, allowedit);
            viewDataTiming1.SetData(MeterInfo, allowedit);
            viewDataDay1.SetData(MeterInfo, allowedit);
            viewDataWholePoint1.SetData(MeterInfo, allowedit);
            viewDataAppoint1.SetData(MeterInfo, allowedit);
            viewDataInstant1.SetData(MeterInfo, allowedit);
           
        }
        public override void SetData(CLDC_DataCore.Model.DnbModel.DnbGroupInfo MeterGroup, bool allowedit)
        {
            base.SetData(MeterGroup, allowedit);

            IsDisplayAll = true;

            checkWC_Normal1.IsDisplayAll = IsDisplayAll;
            checkWC_PianCha1.IsDisplayAll = IsDisplayAll;

            checkQiQianDong1.SetData(MeterGroup, allowedit);
            checkWC_Normal1.SetData(MeterGroup, allowedit);
            checkWC_PianCha1.SetData(MeterGroup, allowedit);
            checkZZ1.SetData(MeterGroup, allowedit);
            checkConn1.SetData(MeterGroup, allowedit);
            checkDgn1.SetData(MeterGroup, allowedit);
            checkCarrier1.SetData(MeterGroup, allowedit);
            checkESAMDataReturn1.SetData(MeterGroup, allowedit);
            checkDateTimeErr1.SetData(MeterGroup, allowedit);
            checkReadPeriod1.SetData(MeterGroup, allowedit);
            checkSdtq1.SetData(MeterGroup, allowedit);
            checkRegister1.SetData(MeterGroup, allowedit);
            checkRatePeriod1.SetData(MeterGroup, allowedit);
            displayCost1.SetData(MeterGroup, allowedit);
            displayFunction1.SetData(MeterGroup, allowedit);
            displayEventLog1.SetData(MeterGroup, allowedit);
            displaySpecial1.SetData(MeterGroup, allowedit);//影响量
            displayWGJC1.SetData(MeterGroup, allowedit);//外观检查
            //误差一致性
            checkErrAccord1.SetData(MeterGroup, allowedit);
            checkContrast1.SetData(MeterGroup, allowedit);
            checkUpDown1.SetData(MeterGroup, allowedit);
            checkOver1.SetData(MeterGroup, allowedit);
            //功能
            viewComputationData1.SetData(MeterGroup.MeterGroup);
            viewTimingData1.SetData(MeterGroup.MeterGroup);
            viewRatePeriodFunction1.SetData(MeterGroup.MeterGroup);
            viewShowFunction1.SetData(MeterGroup.MeterGroup);
            viewMaxDemand1.SetData(MeterGroup.MeterGroup);
            viewLoadRecord1.SetData(MeterGroup.MeterGroup);
            //事件
            viewEventPrograme1.SetData(MeterGroup.MeterGroup);
            viewEventClearEnergy1.SetData(MeterGroup.MeterGroup);
            viewEventLoseVoltage1.SetData(MeterGroup.MeterGroup);
            viewEventLoseCurrent1.SetData(MeterGroup.MeterGroup);
            viewEventLosePhase1.SetData(MeterGroup.MeterGroup);
            viewEventOverLoad1.SetData(MeterGroup.MeterGroup);
            viewEventACdump1.SetData(MeterGroup.MeterGroup);
            viewEventOpenCover1.SetData(MeterGroup.MeterGroup);
            viewEventReversePhase1.SetData(MeterGroup.MeterGroup);
            viewEventReversePhaseI1.SetData(MeterGroup.MeterGroup);
            viewEventOpenButtonCover1.SetData(MeterGroup.MeterGroup);
            
            viewEventOverCurrent1.SetData(MeterGroup.MeterGroup);
            viewEventOverVoltage1.SetData(MeterGroup.MeterGroup);
            viewEventUnderVoltage1.SetData(MeterGroup.MeterGroup);
            viewEventImbalanceU1.SetData(MeterGroup.MeterGroup);
            viewEventImbalanceI1.SetData(MeterGroup.MeterGroup);
            viewEventStopCurrent1.SetData(MeterGroup.MeterGroup);
            viewEventLoseFullVoltage1.SetData(MeterGroup.MeterGroup);
            viewEventCalibrationTime1.SetData(MeterGroup.MeterGroup);
            viewEventClearDemand1.SetData(MeterGroup.MeterGroup);
            viewEventClearEvent1.SetData(MeterGroup.MeterGroup);
            viewEventReversePower1.SetData(MeterGroup.MeterGroup);
            viewEventReverseTrend1.SetData(MeterGroup.MeterGroup);
            viewEventTransPF1.SetData(MeterGroup.MeterGroup);

            //冻结
            displayFreeze1.SetData(MeterGroup, allowedit);
            viewDataTiming1.SetData(MeterGroup.MeterGroup);
            viewDataDay1.SetData(MeterGroup.MeterGroup);
            viewDataWholePoint1.SetData(MeterGroup.MeterGroup);
            viewDataAppoint1.SetData(MeterGroup.MeterGroup);
            viewDataInstant1.SetData(MeterGroup.MeterGroup);
        }
    }
}

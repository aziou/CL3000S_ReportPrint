using System;
namespace CLDC_Dispatcher
{
    interface IDispatcherHelper
    {
        DispatcherModel _MParm { get; }
        
        bool AddSchemes();
        bool GetTaskNo();
        bool LoginOn();
        bool LoginOut();
        bool SetCheckMessage();
        bool SetCurCheckID();
        bool SetCurScheme();
        bool SetErrorBoard();
        bool SetMeterChanged();
        bool SetPressStatus();
        bool SetProgressBar();
        bool SetQuarantineStatus();
        bool SetReverseStatus();
        bool SetSchemeChanged();
        bool SetStdMeterData();
        bool SetTaskFlagChecking();
        bool SetTaskFlagError();
        bool SetTaskFlagFinished();
        bool SetTaskFlagIdle();
        bool SetTaskFlagReady();
        bool WriteRunningLog();
        bool WriteRunningLogA();
        bool ZoomSetDicCheckID();
        bool SetClientVersion();
        bool SetCurCheckState();
        bool SetCurCheckStateStop();
        bool SetHighVoltage();
    }
}

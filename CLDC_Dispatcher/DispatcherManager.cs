using System;
using System.Collections.Generic;
using System.Text;

namespace CLDC_Dispatcher
{
    ///<summary>
    ///FileName:DispatcherManager.cs
    ///CLRVersion:2.0.50727.5472
    ///Author:kaury
    ///Corporation:
    ///Description:
    ///DateTime:6/10/2014 3:28:32 PM
    ///</summary>
    public class DispatcherManager : CLDC_Comm.BaseClass.SingletonBase<DispatcherManager>
    {
        private IDispatcherHelper IDH = null;

        public DispatcherManager()
        {
            int type = 1;//get from config
            switch (type)
            {
                case 1:
                    IDH = new Helper.DispatcherHelper();
                    break;
                default:
                    IDH = new Helper.DispatcherHelper();
                    break;
            }
            
        }
        public DispatcherModel Parms
        {
            get { return IDH._MParm; }
        } 
        public bool Excute(DispatcherEnum _dspt)
        {
            if (CLDC_DataCore.Const.GlobalUnit.IsDemo || CLDC_DataCore.Const.GlobalUnit.DispatcherType == 0)
            {
                return true;
            }
            switch (_dspt)
            {
                case DispatcherEnum.AddSchemes:
                    return IDH.AddSchemes();
                case DispatcherEnum.GetTaskNo:
                    return IDH.GetTaskNo();
                case DispatcherEnum.LoginOn:
                    return IDH.LoginOn();
                case DispatcherEnum.LoginOut:
                    return IDH.LoginOut();
                case DispatcherEnum.SetCheckMessage:
                    return IDH.SetCheckMessage();
                case DispatcherEnum.SetCurCheckID:
                    return IDH.SetCurCheckID();
                case DispatcherEnum.SetCurScheme:
                    return IDH.SetCurScheme();
                case DispatcherEnum.SetErrorBoard:
                    return IDH.SetErrorBoard();
                case DispatcherEnum.SetMeterChanged:
                    return IDH.SetMeterChanged();
                case DispatcherEnum.SetPressStatus:
                    return IDH.SetPressStatus();
                case DispatcherEnum.SetProgressBar:
                    return IDH.SetProgressBar();
                case DispatcherEnum.SetQuarantineStatus:
                    return IDH.SetQuarantineStatus();
                case DispatcherEnum.SetReverseStatus:
                    return IDH.SetReverseStatus();
                case DispatcherEnum.SetSchemeChanged:
                    return IDH.SetSchemeChanged();
                case DispatcherEnum.SetStdMeterData:
                    return IDH.SetStdMeterData();
                case DispatcherEnum.SetTaskFlagChecking:
                    return IDH.SetTaskFlagChecking();
                case DispatcherEnum.SetTaskFlagError:
                    return IDH.SetTaskFlagError();
                case DispatcherEnum.SetTaskFlagFinished:
                    return IDH.SetTaskFlagFinished();
                case DispatcherEnum.SetTaskFlagIdle:
                    return IDH.SetTaskFlagIdle();
                case DispatcherEnum.SetTaskFlagReady:
                    return IDH.SetTaskFlagReady();
                case DispatcherEnum.WriteRunningLog:
                    return IDH.WriteRunningLog();
                case DispatcherEnum.WriteRunningLogA:
                    return IDH.WriteRunningLogA();
                case DispatcherEnum.ZoomSetDicCheckID:
                    return IDH.ZoomSetDicCheckID();
                case DispatcherEnum.SetClientVersion:
                    return IDH.SetClientVersion();
                case DispatcherEnum.SetCurCheckState:
                    return IDH.SetCurCheckState();
                case DispatcherEnum.SetCurCheckStateStop:
                    return IDH.SetCurCheckStateStop();
                case DispatcherEnum.SetHighVoltage:
                    return IDH.SetHighVoltage();
                default:
                    return false;
            }
        }

    }
}

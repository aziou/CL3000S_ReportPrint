using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.Interfaces
{
    /// <summary>
    /// 报警灯控制接口
    /// </summary>
    public interface ICautionLight
    {
        /// <summary>
        /// 更新状态灯输出
        /// </summary>
        /// <param name="theoryPowerInfo">源理论输出参数</param>
        /// <param name="realPowerInfo">源实际输出参数</param>
        /// <param name="powerWorkFlow">当前源是否输出</param>
        /// <param name="lastPowerOnTime">最近一次源输出时间</param>
        /// <param name="taiID"></param>
        /// <returns>0,无报警,非0表示有报警。
        /// 具体报警描述请调用GetCautionDescription获取</returns>
        int UpdateLight(CLDC_DataCore.Struct.StPower theoryPowerInfo, CLDC_DataCore.Struct.StPower realPowerInfo, Cus_PowerWorkFlow powerWorkFlow, DateTime lastPowerOnTime, int taiID);

        /// <summary>
        /// 通过指定的报警号获取报警描述
        /// </summary>
        /// <param name="errorCode">报警号</param>
        /// <returns>报警描述</returns>
        string GetCautionDescription(int errorCode);

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        bool Init();
    }
}

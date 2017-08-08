using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Interfaces;
using System.Reflection;
using System.IO;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.CautionLights
{
    /// <summary>
    /// 报警灯管理类
    /// </summary>
    public class CautionLightManager : CLDC_Comm.BaseClass.SingletonBase<CautionLightManager>, ICautionLight
    {
        ICautionLight cautionLight = null;

        private bool iswork = true;

        /// <summary>
        /// 是否工作
        /// </summary>
        public bool Iswork
        {
            get { return iswork; }
            set
            {
                iswork = value;
                if (iswork == true)
                    CLDC_DataCore.Const.GlobalUnit.Logger.Debug("报警功能开启");
                else
                    CLDC_DataCore.Const.GlobalUnit.Logger.Debug("报警功能关闭");
            }
        }

        /// <summary>
        /// 已重写
        /// 通过在当前目录下搜索 CautionLight.dll 或者 Plugins\\CautionLight.dll
        /// 文件加载第一个 实现了 ICautionLight的类
        /// </summary>
        public CautionLightManager()
        {
            Assembly a = null;
            FileInfo fi = new FileInfo(this.GetType().Assembly.Location);
            string file = fi.DirectoryName + "\\CautionLight.dll";
            if (File.Exists(file))
            {
                a = Assembly.LoadFile(file);
            }
            else
            {
                file = fi.DirectoryName + "\\Plugins\\CautionLight.dll";
                if (File.Exists(file))
                {
                    a = Assembly.LoadFile(file);
                }
            }
            if (a == null)
            {
                return;
            }
            Type[] ts = a.GetTypes();
            foreach (Type t in ts)
            {
                if (t.GetInterface(typeof(ICautionLight).FullName, true) != null)
                {
                    cautionLight = (ICautionLight)(Activator.CreateInstance(t));
                    break;
                }
            }
        }

        /// <summary>
        /// 初始化报警组件
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            if (this.cautionLight == null)
                return false;
            return this.cautionLight.Init();
        }

        /// <summary>
        ///  更新电源信息
        /// </summary>
        /// <param name="theoryPowerInfo"></param>
        /// <param name="realPowerInfo"></param>
        /// <param name="powerWorkFlow"></param>
        /// <param name="lastPowerOnTime"></param>
        /// <param name="taiID"></param>
        /// <returns></returns>
        public int UpdateLight(CLDC_DataCore.Struct.StPower theoryPowerInfo, CLDC_DataCore.Struct.StPower realPowerInfo, Cus_PowerWorkFlow powerWorkFlow, DateTime lastPowerOnTime, int taiID)
        {
            if (powerWorkFlow == Cus_PowerWorkFlow.None || this.iswork == false)
                return 0;
            if (this.cautionLight != null)
                return this.cautionLight.UpdateLight(theoryPowerInfo, realPowerInfo, powerWorkFlow, lastPowerOnTime, taiID);
            return 0;
        }

        /// <summary>
        /// 获取相应的 错误描述
        /// </summary>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public string GetCautionDescription(int errorCode)
        {
            if (this.cautionLight != null)
                return cautionLight.GetCautionDescription(errorCode);
            return "没有加载相应的报警灯处理类";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace CLDC_DataManager
{
    public static class clsMain
    {
        [DllImport("kernel32")]
        private extern static int GetPrivateProfileStringA(string segName, string keyName, string sDefault, StringBuilder retValue, int nSize, string fileName);
        [DllImport("kernel32")]
        private extern static int WritePrivateProfileStringA(string segName, string keyName, string sValue, string fileName);

        #region------------获取配置文件数据---------------------------
        /// <summary>
        /// 获取ini配置文件数据，默认路径，运行目录下Manageinfo.ini，默认数据空
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public static string getIniString(string sSection, string skey)
        {
            return getIniString(sSection, skey, "");
        }
        /// <summary>
        /// 获取INI配置文件数据，默认路径，运行目录下Manageinfo.ini
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="skey"></param>
        /// <param name="sDefault"></param>
        /// <returns></returns>
        public static string getIniString(string sSection, string skey, string sDefault)
        {
            return getIniString(sSection, skey, sDefault, System.Windows.Forms.Application.StartupPath + "\\Plugins\\ManageInfo.ini");
        }
        /// <summary>
        /// 获取INIp配置文件数据
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="skey"></param>
        /// <param name="sDefault"></param>
        /// <param name="IniFilePath"></param>
        /// <returns></returns>
        public static string getIniString(string sSection, string skey, string sDefault, string IniFilePath)
        {
            StringBuilder Buff = new StringBuilder(255);

            int Int_Len = GetPrivateProfileStringA(sSection, skey, sDefault, Buff, 255, IniFilePath);

            return Buff.ToString();

        }
        #endregion 

        #region---------------写配置文件---------------------------
        /// <summary>
        /// 写配置文件数据,默认配置文件路径，运行目录下Manageinfo.ini
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="sKey"></param>
        /// <param name="sValue"></param>
        public static void WriteIni(string sSection, string sKey, string sValue)
        {
            WriteIni(sSection, sKey, sValue, System.Windows.Forms.Application.StartupPath + "\\Plugins\\ManageInfo.ini");
        }
        /// <summary>
        /// 写配置文件数据
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="sKey"></param>
        /// <param name="sValue"></param>
        /// <param name="IniPath"></param>
        public static void WriteIni(string sSection, string sKey, string sValue, string IniPath)
        {
            WritePrivateProfileStringA(sSection, sKey, sValue, IniPath);
        }

        #endregion

        /// <summary>
        /// 数据营销接口对象
        /// </summary>
        internal static CLDC_DataCore.Interfaces.IDatatomis dataToMis = null;
        /// <summary>
        /// 是否成功家在营销接口
        /// </summary>
        internal static bool isLoadMisInterface = false;


        internal static CLDC_DataCore.Interfaces.IReportInterface report = null;

        internal static bool isLoadReportInterface = false;

        /// <summary>
        /// 程序启动时的系统初始化
        /// </summary>
        public static void DefaultSystem()
        {

            isLoadReportInterface = loadReportInterface();

            isLoadMisInterface = loadToMisInterface();

        }


        /// <summary>
        /// 加载打印对象进行打印
        /// </summary>
        /// <param name="MeterInfo">电能表集合</param>
        /// <param name="PrintReportTao">模板套型</param>
        /// <param name="PrintType">打印类型（证书，结果通知书，。。。。。）</param>
        public static void LoadReportScript(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterInfo,int PrintReportTao,int PrintType,string jdyj,string zzbz)
        {
            if (report == null) return;

            report.PrintRpt(MeterInfo, PrintReportTao, PrintType, jdyj, zzbz);
        
        }

        /// <summary>
        /// 加载接口对象进行数据上传
        /// </summary>
        /// <param name="MeterInfo"></param>
        public static bool LoadMisScript(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, string FileName)
        {
            if (dataToMis == null) return false;
            return dataToMis.UpdateData(MeterInfo, FileName);
        }

        /// <summary>
        /// 加载营销接口组件
        /// </summary>
        /// <returns></returns>
        private static bool loadToMisInterface()
        {
            foreach (string Item in System.IO.Directory.GetFiles(CLDC_DataCore.Function.File.GetPhyPath("Plugins")))
            {
                if (CLDC_DataCore.Function.File.GetExtName(Item).ToLower() == ".dll")
                {
                    System.Reflection.Assembly asb;
                    try
                    {
                        asb = System.Reflection.Assembly.LoadFile(Item);
                    }
                    catch { continue; }
                    Type[] types = new Type[0];
                    try
                    {
                        types = asb.GetTypes();
                    }
                    catch { }

                    foreach (Type TypeItem in types)
                    {
                        if (TypeItem.GetInterface("IDatatomis") != null)
                        {
                            dataToMis = (CLDC_DataCore.Interfaces.IDatatomis)Activator.CreateInstance(TypeItem);       //创建实例
                            return true;
                        }
                    }

                }
            }

            return false;
        }

        /// <summary>
        /// 加载报表组件
        /// </summary>
        /// <returns></returns>
        private static bool loadReportInterface()
        {
            foreach (string Item in System.IO.Directory.GetFiles(CLDC_DataCore.Function.File.GetPhyPath("Plugins")))
            {
                if (CLDC_DataCore.Function.File.GetExtName(Item).ToLower() == ".dll")
                {
                    System.Reflection.Assembly asb;
                    try
                    {
                        asb = System.Reflection.Assembly.LoadFile(Item);
                    }
                    catch { continue; }
                    Type[] types = new Type[0];
                    try
                    {
                        types = asb.GetTypes();
                    }
                    catch { }

                    foreach (Type TypeItem in types)
                    {
                        if (TypeItem.GetInterface("IReportInterface") != null)
                        {
                            report = (CLDC_DataCore.Interfaces.IReportInterface)Activator.CreateInstance(TypeItem);       //创建实例
                            return true;
                        }
                    }

                }
            }

            return false;
        }

    }

}

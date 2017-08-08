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

        #region------------��ȡ�����ļ�����---------------------------
        /// <summary>
        /// ��ȡini�����ļ����ݣ�Ĭ��·��������Ŀ¼��Manageinfo.ini��Ĭ�����ݿ�
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public static string getIniString(string sSection, string skey)
        {
            return getIniString(sSection, skey, "");
        }
        /// <summary>
        /// ��ȡINI�����ļ����ݣ�Ĭ��·��������Ŀ¼��Manageinfo.ini
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
        /// ��ȡINIp�����ļ�����
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

        #region---------------д�����ļ�---------------------------
        /// <summary>
        /// д�����ļ�����,Ĭ�������ļ�·��������Ŀ¼��Manageinfo.ini
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="sKey"></param>
        /// <param name="sValue"></param>
        public static void WriteIni(string sSection, string sKey, string sValue)
        {
            WriteIni(sSection, sKey, sValue, System.Windows.Forms.Application.StartupPath + "\\Plugins\\ManageInfo.ini");
        }
        /// <summary>
        /// д�����ļ�����
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
        /// ����Ӫ���ӿڶ���
        /// </summary>
        internal static CLDC_DataCore.Interfaces.IDatatomis dataToMis = null;
        /// <summary>
        /// �Ƿ�ɹ�����Ӫ���ӿ�
        /// </summary>
        internal static bool isLoadMisInterface = false;


        internal static CLDC_DataCore.Interfaces.IReportInterface report = null;

        internal static bool isLoadReportInterface = false;

        /// <summary>
        /// ��������ʱ��ϵͳ��ʼ��
        /// </summary>
        public static void DefaultSystem()
        {

            isLoadReportInterface = loadReportInterface();

            isLoadMisInterface = loadToMisInterface();

        }


        /// <summary>
        /// ���ش�ӡ������д�ӡ
        /// </summary>
        /// <param name="MeterInfo">���ܱ���</param>
        /// <param name="PrintReportTao">ģ������</param>
        /// <param name="PrintType">��ӡ���ͣ�֤�飬���֪ͨ�飬������������</param>
        public static void LoadReportScript(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> MeterInfo,int PrintReportTao,int PrintType,string jdyj,string zzbz)
        {
            if (report == null) return;

            report.PrintRpt(MeterInfo, PrintReportTao, PrintType, jdyj, zzbz);
        
        }

        /// <summary>
        /// ���ؽӿڶ�����������ϴ�
        /// </summary>
        /// <param name="MeterInfo"></param>
        public static bool LoadMisScript(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, string FileName)
        {
            if (dataToMis == null) return false;
            return dataToMis.UpdateData(MeterInfo, FileName);
        }

        /// <summary>
        /// ����Ӫ���ӿ����
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
                            dataToMis = (CLDC_DataCore.Interfaces.IDatatomis)Activator.CreateInstance(TypeItem);       //����ʵ��
                            return true;
                        }
                    }

                }
            }

            return false;
        }

        /// <summary>
        /// ���ر������
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
                            report = (CLDC_DataCore.Interfaces.IReportInterface)Activator.CreateInstance(TypeItem);       //����ʵ��
                            return true;
                        }
                    }

                }
            }

            return false;
        }

    }

}

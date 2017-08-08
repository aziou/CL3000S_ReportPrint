using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// ����춥��������
    /// </summary>
    [Serializable()]
    public class Plan_Base:CLDC_CTNProtocol.CTNPCommand
    {
        /// <summary>
        /// ��������
        /// </summary>
        private string _Name;

        /// <summary>
        /// ��������ֻ��
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        /// <summary>
        /// ̨������
        /// </summary>
        protected int _TaiType=0;

        /// <summary>
        /// ��������
        /// </summary>
        protected string _PlanType = "";

        /// <summary>
        /// ����·��
        /// </summary>
        protected string _FAPath = "";
        /// <summary>
        /// ��������·�����������ļ���
        /// </summary>
        protected string _StartupPath = "";
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="TaiType">̨������0-����̨��1-����̨</param>
        /// <param name="vFAName">�������ƻ�id</param>
        public Plan_Base(string PlanType,int TaiType,string vFAName)
        {
            _StartupPath = Application.StartupPath;
            _PlanType = PlanType;
            this.SetPram(TaiType, vFAName);
        }

        public void SetPram(int TaiType, string vFAName)
        {
            _TaiType = TaiType;
            _Name = vFAName;
            if (CLDC_DataCore.Const.GlobalUnit.Plan_FromMDB == true)
            {
                _FAPath = string.Format(_StartupPath + "\\{0}", _PlanType);
            }
            else
            {
                _FAPath = string.Format(_StartupPath + "\\{0}\\{1}\\{2}.xml", _TaiType == 0 ? "SX" : "DX", _PlanType, vFAName);
            }
            

        }

        /// <summary>
        /// ��ȡָ���ļ����µ����з����ļ�����
        /// </summary>
        /// <param name="PlanType">�ļ�������</param>
        /// <param name="TaiType">̨������0-����̨��1-����̨</param>
        /// <returns></returns>
        public static List<string> getFileNames(string PlanType,int TaiType)
        {
            return CLDC_DataCore.Function.Folder.getFileNames(string.Format(Application.StartupPath + "\\{0}\\{1}", TaiType == 0 ? "SX" : "DX",PlanType),"*.Xml");
        }

        /// <summary>
        /// ��ȡָ���ļ����µ����з����ļ�
        /// </summary>
        /// <param name="PlanType">�ļ�������</param>
        /// <param name="TaiType">̨������0-����̨��1-����̨</param>
        /// <returns></returns>
        public static List<string> getFilePaths(string PlanType, int TaiType)
        {
            return CLDC_DataCore.Function.Folder.getFilePaths(string.Format(Application.StartupPath + "\\{0}\\{1}", TaiType == 0 ? "SX" : "DX", PlanType),"*.Xml");
        }
        /// <summary>
        /// ɾ��һ�������ļ�
        /// </summary>
        /// <param name="vFAName">��������</param>
        /// <param name="PlanType">����������һ���ļ��е����ƣ��ڳ���ģ�����ܹ��ҵ���Ӧ����</param>
        /// <param name="TaiType">̨������0-����̨��1-����̨</param>
        public static bool RemoveFA(string vFAName,string PlanType, int TaiType)
        {
            string _Path=string.Format(@"{0}\{1}\{2}\{3}.xml", Application.StartupPath, TaiType == 0 ? "SX" : "DX", PlanType, vFAName);
            if (!System.IO.File.Exists(_Path))
                return false;
            System.IO.File.Delete(_Path);
            return true;
        }
        #region ���ݿ�
        /// <summary>
        /// ��ȡָ���ļ����µ����з����ļ�����
        /// </summary>
        /// <param name="PlanType">�ļ�������</param>
        /// <param name="TaiType">̨������0-����̨��1-����̨</param>
        /// <returns></returns>
        public static List<PlanModel.Scheme_Check> getFileNamesFromMDB(string PlanType, int TaiType)
        {
            Plan_Scheme_Check plFANames = new Plan_Scheme_Check(TaiType, "All");
            string strErr = "";
            //List<PlanModel.Scheme_Check> modScCk = plFANames.GetList("", out strErr);
            return plFANames.GetList("", out strErr);
        }

        #endregion

    }
}

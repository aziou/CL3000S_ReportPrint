using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CLDC_DataCore.Model.Plan
{
    /// <summary>
    /// 单项检顶方案基类
    /// </summary>
    [Serializable()]
    public class Plan_Base:CLDC_CTNProtocol.CTNPCommand
    {
        /// <summary>
        /// 方案名称
        /// </summary>
        private string _Name;

        /// <summary>
        /// 方案名称只读
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        /// <summary>
        /// 台体类型
        /// </summary>
        protected int _TaiType=0;

        /// <summary>
        /// 方案类型
        /// </summary>
        protected string _PlanType = "";

        /// <summary>
        /// 方案路径
        /// </summary>
        protected string _FAPath = "";
        /// <summary>
        /// 程序启动路径，不包含文件名
        /// </summary>
        protected string _StartupPath = "";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="TaiType">台体类型0-三相台，1-单向台</param>
        /// <param name="vFAName">方案名称或id</param>
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
        /// 获取指定文件夹下的所有方案文件名称
        /// </summary>
        /// <param name="PlanType">文件夹名称</param>
        /// <param name="TaiType">台体类型0-三相台，1-单相台</param>
        /// <returns></returns>
        public static List<string> getFileNames(string PlanType,int TaiType)
        {
            return CLDC_DataCore.Function.Folder.getFileNames(string.Format(Application.StartupPath + "\\{0}\\{1}", TaiType == 0 ? "SX" : "DX",PlanType),"*.Xml");
        }

        /// <summary>
        /// 获取指定文件夹下的所有方案文件
        /// </summary>
        /// <param name="PlanType">文件夹名称</param>
        /// <param name="TaiType">台体类型0-三相台，1-单相台</param>
        /// <returns></returns>
        public static List<string> getFilePaths(string PlanType, int TaiType)
        {
            return CLDC_DataCore.Function.Folder.getFilePaths(string.Format(Application.StartupPath + "\\{0}\\{1}", TaiType == 0 ? "SX" : "DX", PlanType),"*.Xml");
        }
        /// <summary>
        /// 删除一个方案文件
        /// </summary>
        /// <param name="vFAName">方案名称</param>
        /// <param name="PlanType">方案所在上一级文件夹的名称，在常量模块中能够找到对应常量</param>
        /// <param name="TaiType">台体类型0-三相台，1-单相台</param>
        public static bool RemoveFA(string vFAName,string PlanType, int TaiType)
        {
            string _Path=string.Format(@"{0}\{1}\{2}\{3}.xml", Application.StartupPath, TaiType == 0 ? "SX" : "DX", PlanType, vFAName);
            if (!System.IO.File.Exists(_Path))
                return false;
            System.IO.File.Delete(_Path);
            return true;
        }
        #region 数据库
        /// <summary>
        /// 获取指定文件夹下的所有方案文件名称
        /// </summary>
        /// <param name="PlanType">文件夹名称</param>
        /// <param name="TaiType">台体类型0-三相台，1-单相台</param>
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

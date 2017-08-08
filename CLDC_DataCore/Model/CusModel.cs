using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Model.DnbModel;
//using System.Windows.Forms;
namespace CLDC_DataCore
{
    /// <summary>
    /// 系统总模型，包括电能表模型，系统配置信息。此模型下的所有成员必须标记可序列化"[Serializable()]"
    /// 通过本模型，可以直接从本地加载模型数据或是将现有模型序列化保存为本地文件。本地序列化文件保存路径
    /// 为CLDC_DataCore.Const.Variable.CONST_WAITUPDATE。
    /// </summary>
    [Serializable()]
    public class CusModel 
    {
        /// <summary>
        /// 电能表信息模型
        /// </summary>
        public DnbGroupInfo DnbData;
        /// <summary>
        /// 系统配置信息列表。用于系统模型携带系统必须配置信息。
        /// </summary>
        public Dictionary<string, string> SystemInfo;

        private int _Bws;

        private int _Taiid;

        #region sql相关字段，服务器地址，用户名，密码
        private string sqlIP = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SQL_SERVERIP, string.Empty);
        private string sqlUserName = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SQL_USERID, string.Empty);
        private string sqlPassWord = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SQL_PASSWORD, string.Empty);
        #endregion


        /// <summary>
        /// 构造函数，构造表信息List
        /// </summary>
        /// <param name="Bws">电能台表位数</param>
        public CusModel(int Bws,int TaiID)
        {
            _Bws = Bws;
            _Taiid = TaiID;
            DnbData = new DnbGroupInfo(Bws,TaiID);

            CLDC_DataCore.SystemModel.Item.SystemConfigure _TmpSystem = new CLDC_DataCore.SystemModel.Item.SystemConfigure();
            _TmpSystem.Load();

            this.Load(_TmpSystem);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~CusModel()
        {
            DnbData = null;
            SystemInfo=null;
        }

        /// <summary>
        /// 创建一个数据库操作链接对象
        /// </summary>
        /// <returns></returns>
        private DataBase.clsDataManage NewConnection()
        {
            DataBase.clsDataManage DataManage;

            if (CLDC_DataCore.Function.File.ReadInIString(CLDC_DataCore.Const.Variable.CONST_MANAGERINI, "Server", "Run", "0") == "1")           //服务器访问
            {
                DataManage = new DataBase.clsDataManage(CLDC_DataCore.Function.File.ReadInIString(CLDC_DataCore.Const.Variable.CONST_MANAGERINI, "Server", "Host", ""), CLDC_DataCore.Function.File.ReadInIString(CLDC_DataCore.Const.Variable.CONST_MANAGERINI, "Server", "Name", ""), CLDC_DataCore.Function.File.ReadInIString(CLDC_DataCore.Const.Variable.CONST_MANAGERINI, "Server", "Pwd", ""));
            }
            else   //本地访问
            {
                DataManage = new DataBase.clsDataManage();
            }
            if (!DataManage.Connection)
            {
                return null;
            }
            return DataManage;
        }
        
        /// <summary>
        /// 从本地序列化缓存中加载模型数据。如果加载失败，则返回一个全新的模型对象。
        /// </summary>
        public void Load()
        {
            //TODO:从临时库加载数据，现有模式保留优化。如果可以从临时库获取模型，则认为没有检完。反之创建新模型，当前方案也置空
            DnbData = DnbData.LoadTmpData();
            //DataBase.clsDataManage clsDM = new DataBase.clsDataManage(Const.GlobalUnit.DBPathOfTempAccess,false);
            //DnbData.MeterGroup = clsDM.GetMeterListFromTempDB();
            if (DnbData == null || DnbData.MeterGroup.Count != _Bws)
            {
                //clsDM.DeleteTmp_MeterInfo(-1);
                DnbData = new DnbGroupInfo(_Bws, _Taiid);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TmpSystemInfo"></param>
        public void Load(CLDC_DataCore.SystemModel.Item.SystemConfigure TmpSystemInfo)
        {
            SystemInfo = new Dictionary<string, string>();

            foreach (string _Key in TmpSystemInfo.getKeyNames())
            {
                SystemInfo.Add(_Key, TmpSystemInfo.getItem(_Key).Value);
            }
        }
        /// <summary>
        /// 保存电能表数据到临时文件中
        /// </summary>
        public void Save()
        {
            
            DnbData.Save();
        }
        public void SaveTempDB()
        {
            string strErr = "";
            DnbData.SaveToTempDB(out strErr,null, CLDC_Comm.Enum.Cus_DBTableName.METER_INFO);
            if (CLDC_DataCore.Const.GlobalUnit.NetState == CLDC_Comm.Enum.Cus_NetState.Connected)
            {
                DnbData.SaveToTempDB(sqlIP, sqlUserName, sqlPassWord, out strErr, null, CLDC_Comm.Enum.Cus_DBTableName.METER_INFO);
            }
        }

        public void SaveTempDB(CLDC_Comm.Enum.Cus_DBTableName SaveType, string[] key)
        {
            if (key == null || key.Length < 1) return;
            
            string strErr = "";
            DnbData.SaveToTempDB(out strErr,key, SaveType);
            if (CLDC_DataCore.Const.GlobalUnit.NetState == CLDC_Comm.Enum.Cus_NetState.Connected)
            {
                DnbData.SaveToTempDB(sqlIP, sqlUserName, sqlPassWord, out strErr, key, SaveType);
            }

        }
        /// <summary>
        /// 从序列化文件中获取电能表信息对象
        /// </summary>
        /// <param name="FileName">文件名称</param>
        /// <returns></returns>
        public static Model.DnbModel.DnbGroupInfo DnbInfo(string FileName)
        {
            string FilePath = string.Format("{0}\\{1}", CLDC_DataCore.Const.Variable.CONST_WAITUPDATE, FileName); 
            byte[] _TmpByte = CLDC_DataCore.Function.File.ReadFileData(FilePath);
            if (_TmpByte.Length == 1)
                return null;
            try
            {
                return (DnbGroupInfo)CLDC_CTNProtocol.CTNPCommand.GetObject(_TmpByte);
            }
            catch
            {
                return null;
            }
        }
        ///// <summary>
        ///// 序列化存储未上传的电能表数据到未上传文件夹
        ///// </summary>
        ///// <param name="_DnbInfo"></param>
        //public static void SaveWaitUptoServerFile(DnbGroupInfo _DnbInfo)
        //{
        //    Function.File.WriteFileData(string.Format("\\{0}\\{1}.dat", CLDC_DataCore.Const.Variable.CONST_WAITUPDATE,DateTime.Now.ToString()), _DnbInfo.GetBytes());
        //}
    }

}

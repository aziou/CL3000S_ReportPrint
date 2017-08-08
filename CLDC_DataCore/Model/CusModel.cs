using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Model.DnbModel;
//using System.Windows.Forms;
namespace CLDC_DataCore
{
    /// <summary>
    /// ϵͳ��ģ�ͣ��������ܱ�ģ�ͣ�ϵͳ������Ϣ����ģ���µ����г�Ա�����ǿ����л�"[Serializable()]"
    /// ͨ����ģ�ͣ�����ֱ�Ӵӱ��ؼ���ģ�����ݻ��ǽ�����ģ�����л�����Ϊ�����ļ����������л��ļ�����·��
    /// ΪCLDC_DataCore.Const.Variable.CONST_WAITUPDATE��
    /// </summary>
    [Serializable()]
    public class CusModel 
    {
        /// <summary>
        /// ���ܱ���Ϣģ��
        /// </summary>
        public DnbGroupInfo DnbData;
        /// <summary>
        /// ϵͳ������Ϣ�б�����ϵͳģ��Я��ϵͳ����������Ϣ��
        /// </summary>
        public Dictionary<string, string> SystemInfo;

        private int _Bws;

        private int _Taiid;

        #region sql����ֶΣ���������ַ���û���������
        private string sqlIP = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SQL_SERVERIP, string.Empty);
        private string sqlUserName = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SQL_USERID, string.Empty);
        private string sqlPassWord = CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_SQL_PASSWORD, string.Empty);
        #endregion


        /// <summary>
        /// ���캯�����������ϢList
        /// </summary>
        /// <param name="Bws">����̨��λ��</param>
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
        /// ��������
        /// </summary>
        ~CusModel()
        {
            DnbData = null;
            SystemInfo=null;
        }

        /// <summary>
        /// ����һ�����ݿ�������Ӷ���
        /// </summary>
        /// <returns></returns>
        private DataBase.clsDataManage NewConnection()
        {
            DataBase.clsDataManage DataManage;

            if (CLDC_DataCore.Function.File.ReadInIString(CLDC_DataCore.Const.Variable.CONST_MANAGERINI, "Server", "Run", "0") == "1")           //����������
            {
                DataManage = new DataBase.clsDataManage(CLDC_DataCore.Function.File.ReadInIString(CLDC_DataCore.Const.Variable.CONST_MANAGERINI, "Server", "Host", ""), CLDC_DataCore.Function.File.ReadInIString(CLDC_DataCore.Const.Variable.CONST_MANAGERINI, "Server", "Name", ""), CLDC_DataCore.Function.File.ReadInIString(CLDC_DataCore.Const.Variable.CONST_MANAGERINI, "Server", "Pwd", ""));
            }
            else   //���ط���
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
        /// �ӱ������л������м���ģ�����ݡ��������ʧ�ܣ��򷵻�һ��ȫ�µ�ģ�Ͷ���
        /// </summary>
        public void Load()
        {
            //TODO:����ʱ��������ݣ�����ģʽ�����Ż���������Դ���ʱ���ȡģ�ͣ�����Ϊû�м��ꡣ��֮������ģ�ͣ���ǰ����Ҳ�ÿ�
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
        /// ������ܱ����ݵ���ʱ�ļ���
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
        /// �����л��ļ��л�ȡ���ܱ���Ϣ����
        /// </summary>
        /// <param name="FileName">�ļ�����</param>
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
        ///// ���л��洢δ�ϴ��ĵ��ܱ����ݵ�δ�ϴ��ļ���
        ///// </summary>
        ///// <param name="_DnbInfo"></param>
        //public static void SaveWaitUptoServerFile(DnbGroupInfo _DnbInfo)
        //{
        //    Function.File.WriteFileData(string.Format("\\{0}\\{1}.dat", CLDC_DataCore.Const.Variable.CONST_WAITUPDATE,DateTime.Now.ToString()), _DnbInfo.GetBytes());
        //}
    }

}

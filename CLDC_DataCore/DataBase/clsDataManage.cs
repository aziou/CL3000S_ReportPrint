using System;
using System.Collections.Generic;
using System.Text;
using CLDC_Comm.Enum;

namespace CLDC_DataCore.DataBase
{
    /// <summary>
    /// ���ݿ�Ĳ��������ݹ�����
    /// �����ݿ�Ĳ������Ա������������������ݿ���
    /// fjk
    /// </summary>
    public class clsDataManage : CLDC_DataCore.DataBase.DataControl
    {
        #region ����
        /// <summary>
        /// �Ƿ���ʵ��Ƿ�����
        /// </summary>
        public bool _IsServer = false;

        #endregion

        #region ����
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsDataManage()
            : base()
        {

        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="DataPath">���ص����ݿ�·��</param>
        public clsDataManage(bool Tmpor)
            : base(Tmpor)
        {

        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="DataPath">���ص����ݿ�·��</param>
        public clsDataManage(string DataPath,bool Absor)
            : base(DataPath,Absor)
        {

        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="Ip">������IP</param>
        /// <param name="User">�û���</param>
        /// <param name="Pwd">����</param>
        public clsDataManage(string Ip, string User, string Pwd)
            : base(Ip, User, Pwd)
        {
            _IsServer = true;
        }
        /// <summary>
        /// �Ƿ���ʵ��Ƿ�����
        /// </summary>
        public bool IsServer
        {
            get
            {
                return _IsServer;
            }
        }
        #endregion

        #region ���ݹ����ѯ��
        /// <summary>
        /// ��ѯ���صĿɲ�ѯ���ֶ�ֵ
        /// </summary>
        /// <param name="sType">��ѯ������</param>
        /// <returns></returns>
        public List<string> GetDataFieldList(ScreenType sType)
        {
            return GetDataFieldList(sType, "false");
        }
        /// <summary>
        /// ��ѯ���صĿɲ�ѯ���ֶ�ֵ
        /// </summary>
        /// <param name="sType">��ѯ������</param>
        /// <param name="TaiID">̨ID��ֻ�з��ʷ�������ʱ����Ҫ�ò���������ֵΪ��false��Ϊ���ʵı������ݿ⣬����ֵ��True��Ϊ���ʷ��������ݿ������̨�����ݣ�����Ϊ����̨��</param>
        /// <returns></returns>
        public List<string> GetDataFieldList(ScreenType sType, string TaiID)
        {
            List<string> Items = new List<string>();

            if (!base.Connection)
            {
                return Items;
            }

            string SQLString = GetScreenFieldName(sType);

            SQLString = string.Format("SELECT DISTINCT {0} FROM METER_INFO ORDER BY {0} DESC", SQLString);

            if (TaiID != "" && TaiID.ToLower() != "false" && TaiID.ToLower() != "true")
            {
                SQLString = string.Format("{0} WHERE AVR_DEVICE_ID='{1}'", SQLString, TaiID);
            }

            System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);
            try
            {
                System.Data.OleDb.OleDbDataReader Reader = Cmd.ExecuteReader();

                while (Reader.Read())
                {
                    Items.Add(Reader[0].ToString());
                }

                Reader.Close();

                Reader.Dispose();

                Reader = null;

                Cmd = null;

                return Items;
            }
            catch (Exception e)
            {
                //System.Windows.Forms.MessageBox.Show(e.Message);
                return Items;
            }


        }

        /// <summary>
        /// ����ö�ٷ������ݿ��е���Ӧ�ֶ�
        /// </summary>
        /// <param name="sType"></param>
        /// <returns></returns>
        private string GetScreenFieldName(ScreenType sType)
        {
            switch (sType)
            {
                case ScreenType.�춨����:
                    return "DTM_TEST_DATE";
                case ScreenType.������:
                    return "AVR_BAR_CODE";
                case ScreenType.�������:
                    return "AVR_ASSET_NO";
                case ScreenType.�������:
                    return "AVR_MADE_NO";
                case ScreenType.���쳧��:
                    return "AVR_FACTORY";
                case ScreenType.������:
                    return "AVR_METER_TYPE";
                case ScreenType.���ͺ�:
                    return "AVR_METER_MODEL";
                case ScreenType.����:
                    return "AVR_IB";
                case ScreenType.��ѹ:
                    return "AVR_UB";
                case ScreenType.����Ա:
                    return "AVR_TEST_PERSON";
                case ScreenType.����Ա:
                    return "AVR_AUDIT_PERSON";
                case ScreenType.�Ƿ����ϴ�:
                    return "CHR_UPLOAD_FLAG";
                default:
                    return "";
            }

        }


        /// <summary>
        /// ��ѯ������������̨���б�
        /// </summary>
        /// <returns></returns>
        public List<string> getTaiIDList()
        {
            string SQLString = "SELECT DISTINCT AVR_DEVICE_ID From METER_INFO";

            List<string> IDList = new List<string>();

            if (!base.Connection)
            {
                return IDList;
            }

            try
            {
                System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

                System.Data.OleDb.OleDbDataReader Reader = Cmd.ExecuteReader();

                while (Reader.Read())
                {
                    IDList.Add(Reader[0].ToString());
                }

                Reader.Close();

                return IDList;

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);

                return IDList;
            }

        }

        /// <summary>
        /// ���ص��ܱ������Ϣ�б�
        /// </summary>
        /// <param name="Queryterm">��ѯ����(��ѯ�ֶ�ö������ID,ͨ���,����)</param>
        /// <param name="HeGe">�ϸ�ѡ��</param>
        /// <returns></returns>
        public List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> getScreenDnbInfo(List<string> Queryterm, ScreenHeGeType HeGe)
        {

            List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items = new List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo>();

            if (!base.Connection) return Items;
            string SQLString = "";
            for (int i = 0; i < Queryterm.Count; i++)
            {
                string[] Arr_Term = Queryterm[i].Split(',');

                if (Arr_Term.Length != 3) return Items;

                string Tmp_Name = this.GetScreenFieldName((ScreenType)int.Parse(Arr_Term[0]));
                if (Arr_Term[1].ToLower() == "like")
                {
                    SQLString = string.Format("{0} AND {1} {2} '%{3}%'", SQLString, Tmp_Name, Arr_Term[1], Arr_Term[2]);
                }
                else
                {
                    if ((ScreenType)int.Parse(Arr_Term[0]) == ScreenType.�춨���� && !IsServer)
                    {
                        SQLString = string.Format("{0} AND {1} {2} #{3}#", SQLString, Tmp_Name, Arr_Term[1], Arr_Term[2]);
                    }
                    else
                    {
                        SQLString = string.Format("{0} AND {1} {2} '{3}'", SQLString, Tmp_Name, Arr_Term[1], Arr_Term[2]);
                    }
                }
            }

            SQLString = "SELECT * FROM METER_INFO WHERE 1=1 " + SQLString;

            if (HeGe == ScreenHeGeType.�ϸ�)
            {
                SQLString = string.Format("{0} AND AVR_TOTAL_CONCLUSION='{1}'", SQLString, CLDC_DataCore.Const.Variable.CTG_HeGe);
            }
            else if (HeGe == ScreenHeGeType.���ϸ�)
            {
                SQLString = string.Format("{0} AND AVR_TOTAL_CONCLUSION='{1}'", SQLString, CLDC_DataCore.Const.Variable.CTG_BuHeGe);
            }
            return this.getBasicInfo(SQLString, true);

        }

        #endregion

        #region �����ݿ���������ݹ����춨����
        /// <summary>
        /// ��ȡһ��������Ϣ
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        private CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo getBasicInfo(string Sql)
        {
            List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> InfoList = getBasicInfo(Sql, true);
            if (InfoList.Count == 0) return null;
            return InfoList[0];
        }
        /// <summary>
        /// ��ȡ������Ϣ�б�
        /// </summary>
        /// <param name="Sql"></param>
        /// <param name="IsList"></param>
        /// <returns></returns>
        private List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> getBasicInfo(string Sql, bool IsList)
        {
            List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items = new List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo>();

            try
            {
                if (base._Con.State == System.Data.ConnectionState.Closed)
                    base._Con.Open();
                System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(Sql, base._Con);
                System.Data.OleDb.OleDbDataReader Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo();
                    Item.SetBno((int)Reader["LNG_BENCH_POINT_NO"]);          //��λ��
                    if (IsServer) Item._intTaiNo = Reader["AVR_DEVICE_ID"].ToString();        //��������ʱ����Ҫ����һ��̨���
                    Item._intTaiNo = Reader["AVR_DEVICE_ID"].ToString();        // �������ͻ��˶� �и�ֵ 
                    Item._intMyId = long.Parse(Reader["PK_LNG_METER_ID"].ToString());   //ΨһID
                    Item.Mb_ChrJlbh = Reader["AVR_ASSET_NO"].ToString().Trim();  //�������
                    Item.Mb_ChrCcbh = Reader["AVR_MADE_NO"].ToString().Trim();      //�������
                    Item.Mb_ChrTxm = Reader["AVR_BAR_CODE"].ToString().Trim();        //�����
                    Item.Mb_chrAddr = Reader["AVR_ADDRESS"].ToString().Trim();      //ͨ�ŵ�ַ
                    Item.Mb_chrzzcj = Reader["AVR_FACTORY"].ToString().Trim();      //���쳧��
                    Item.Mb_Bxh = Reader["AVR_METER_MODEL"].ToString().Trim();           //���ͺ�
                    Item.Mb_chrBcs = Reader["AVR_AR_CONSTANT"].ToString().Trim();                 //����
                    Item.Mb_chrBlx = Reader["AVR_METER_TYPE"].ToString().Trim();                 //������
                    Item.Mb_chrBdj = Reader["AVR_AR_CLASS"].ToString().Trim();                 //��ȼ�
                    try
                    {
                        Item.Mb_gygy = (Cus_GyGyType)(System.Enum.Parse(typeof(Cus_GyGyType), Reader["AVR_PULSE_TYPE"].ToString()));
                    }
                    catch
                    {
                    }
                    Item.Mb_chrCcrq = Reader["AVR_MADE_DATE"].ToString().Trim();                //��������
                    Item.Mb_chrSjdw = Reader["AVR_CUSTOMER"].ToString().Trim();                //�ͼ쵥λ
                    Item.Mb_chrZsbh = Reader["AVR_CERTIFICATE_NO"].ToString().Trim();                //֤����
                    Item.Mb_ChrBmc = Reader["AVR_METER_NAME"].ToString().Trim();                 //������
                    Item.Mb_intClfs = Convert.ToInt32(Reader["AVR_WIRING_MODE"]);           //������ʽ
                    Item.Mb_chrUb = Reader["AVR_UB"].ToString().Trim();                 //��ѹ
                    Item.Mb_chrIb = Reader["AVR_IB"].ToString().Trim();                  //����
                    Item.Mb_chrHz = Reader["AVR_FREQUENCY"].ToString().Trim();                  //Ƶ��
                    Item.Mb_BlnZnq = Reader["CHR_CC_PREVENT_FLAG"].ToString() == "1" || Reader["CHR_CC_PREVENT_FLAG"].ToString() == "1" ? true : false;//֮����
                    Item.Mb_BlnHgq = Reader["CHR_CT_CONNECTION_FLAG"].ToString() == "1" || Reader["CHR_CT_CONNECTION_FLAG"].ToString() == "1" ? true : false;//������
                    //Item.Mb_BlnHgq = true;//������
                    Item.Mb_chrTestType = Reader["AVR_TEST_TYPE"].ToString().Trim();                //��������
                    Item.Mb_DatJdrq = Reader["DTM_TEST_DATE"].ToString().Trim();      //�춨����
                    Item.Mb_Datjjrq = Reader["DTM_VALID_DATE"].ToString().Trim();      //�Ƽ�����
                    Item.Mb_chrWd = Reader["AVR_TEMPERATURE"].ToString().Trim();                      //�¶�
                    Item.Mb_chrSd = Reader["AVR_HUMIDITY"].ToString().Trim();                      //ʪ��
                    Item.Mb_chrResult = Reader["AVR_TOTAL_CONCLUSION"].ToString().Trim();                  //�ܽ���
                    Item.Mb_ChrJyy = Reader["AVR_TEST_PERSON"].ToString().Trim();                     //����Ա
                    Item.Mb_ChrHyy = Reader["AVR_AUDIT_PERSON"].ToString().Trim();                     //����Ա
                    Item.Mb_chrZhuGuan = Reader["AVR_SUPERVISOR"].ToString().Trim();                 //����
                    Item.Mb_BlnToServer = Reader["CHR_UPLOAD_FLAG"].ToString() == "1" || Reader["CHR_UPLOAD_FLAG"].ToString() == "1" ? true : false;        //�Ƿ��Ѿ��ϴ�
                    Item.Mb_BlnToMis = Reader["CHR_UPLOAD_FLAG"].ToString() == "1" || Reader["CHR_UPLOAD_FLAG"].ToString().ToLower() == "true" ? true : false;     //�Ƿ��Ѿ��ϴ���Ӫ��
                    Item.Mb_chrQianFeng1 = Reader["AVR_SEAL_1"].ToString().Trim();               //Ǧ��1
                    Item.Mb_chrQianFeng2 = Reader["AVR_SEAL_2"].ToString().Trim();               //Ǧ��2
                    Item.Mb_chrQianFeng3 = Reader["AVR_SEAL_3"].ToString().Trim();               //Ǧ��3
                    Item.AVR_SEAL_4 = Reader["AVR_SEAL_4"].ToString().Trim();               //Ǧ��3
                    Item.AVR_SEAL_5 = Reader["AVR_SEAL_5"].ToString().Trim();               //Ǧ��3
                    Item.Mb_chrOther1 = Reader["AVR_OTHER_1"].ToString().Trim();                  //��ע1
                    Item.Mb_chrOther2 = Reader["AVR_OTHER_2"].ToString().Trim();                  //��ע2
                    Item.Mb_chrOther3 = Reader["AVR_OTHER_3"].ToString().Trim();                  //��ע3
                    Item.Mb_chrOther4 = Reader["AVR_OTHER_4"].ToString().Trim();                  //��ע4
                    Item.Mb_chrOther5 = Reader["AVR_OTHER_5"].ToString().Trim();                  //��ע5
                    Item.YaoJianYn = Reader["CHR_CHECKED"].ToString() == "1" ? true : false;                  //Ҫ��˱�1Ҫ�죬0����
                    Item.AVR_TASK_NO = Reader["AVR_TASK_NO"].ToString().Trim(); 
                    Items.Add(Item);
                }
                Reader.Close();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "���ݿ��������", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return Items;

        }
        /// <summary>
        /// ��ȡ���ܱ���ϸ����
        /// </summary>
        /// <param name="InfoItem"></param>
        /// <param name="WcHzFh">�����Ƿ���Ҫ����</param>
        /// <returns></returns>
        public CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo getDnbInfo(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo InfoItem, bool WcHzFh)
        {
            if (!base.Connection) return InfoItem;

            //try
            //{
            //string SQLString;
            #region �����ݿ�
            /*#region ------------------------��ȡ��������---------------------------------------
            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM MeterResult WHERE Mr_LngMyID={0}", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM MeterResult WHERE Mr_LngMyID={0} AND Mr_TaiID='{1}'", InfoItem._intMyId, InfoItem.Mb_TaiID);
            }

            System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            System.Data.OleDb.OleDbDataReader Reader = Cmd.ExecuteReader();

            InfoItem.MeterResults.Clear();

            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult Result = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();
                Result.Mr_lngMyID = InfoItem._intMyId;
                Result.Mr_PrjID = Reader["Mr_PrjID"].ToString();
                Result.Mr_PrjName = Reader["Mr_PrjName"].ToString();
                Result.Mr_Time = Reader["Mr_Time"].ToString();
                Result.Mr_Current = Reader["Mr_Current"].ToString();
                Result.Mr_Result = Reader["Mr_Result"].ToString();
                InfoItem.MeterResults.Add(Result.Mr_PrjID, Result);
            }
            Reader.Close();

            #endregion

            #region--------------------------��ȡ�������------------------------------

            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM MeterError WHERE Me_LngMyID={0}", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM MeterError WHERE Me_LngMyID={0} AND Me_TaiID='{1}'", InfoItem._intMyId, InfoItem.Mb_TaiID);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterErrors.Clear();

            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError Error = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError(long.Parse(Reader["Me_AutoID"].ToString()));
                Error.Me_lngMyID = InfoItem._intMyId;
                Error.Me_PrjID = Reader["Me_PrjID"].ToString();
                Error.Me_PrjName = Reader["Me_PrjName"].ToString();
                Error.Me_Result = Reader["Me_Result"].ToString();
                Error.Me_Glys = Reader["Me_Glys"].ToString();
                Error.Me_xib = Reader["Me_xib"].ToString();
                Error.Me_xU = Reader["Me_xU"].ToString();
                Error.Me_WcLimit = Reader["mE_WcLimit"].ToString();
                Error.Me_Qs = int.Parse(Reader["Me_Qs"].ToString());
                Error.Me_PL = Reader["Me_Pl"].ToString();

                if (!WcHzFh)
                {
                    try
                    {
                        string[] WC_Arr = Reader["Me_Wc"].ToString().Split('|');
                        if (float.Parse(WC_Arr[WC_Arr.Length - 1].Replace("+", "")) == 0)
                        {
                            WC_Arr[WC_Arr.Length - 1] = WC_Arr[WC_Arr.Length - 1].Substring(1);
                        }
                        Error.Me_Wc = string.Join("|", WC_Arr);
                    }
                    catch
                    {
                        Error.Me_Wc = Reader["Me_Wc"].ToString();
                    }
                }
                else
                {
                    Error.Me_Wc = Reader["Me_Wc"].ToString();
                }
                InfoItem.MeterErrors.Add(Error.Me_PrjID, Error);
            }

            Reader.Close();

            #endregion



            #region --------------------��ȡ��������----------------------

            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM MeterZZData WHERE Mz_LngMyID={0}", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM MeterZZData WHERE Mz_LngMyID={0} AND Mz_TaiID='{1}'", InfoItem._intMyId, InfoItem.Mb_TaiID);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterZZErrors.Clear();

            int int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError ZZError = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError();
                ZZError.Mz_lngMyID = InfoItem._intMyId;
                ZZError.Mz_PrjID = Reader["Mz_PrjID"].ToString();
                ZZError.Mz_StartTime = Reader["Mz_StartTime"].ToString();
                ZZError.Mz_xIb = Reader["Mz_xIb"].ToString();
                ZZError.Mz_Glys = Reader["Mz_Glys"].ToString();
                ZZError.Mz_Start = float.Parse(Reader["Mz_Start"].ToString());
                ZZError.Mz_End = float.Parse(Reader["Mz_End"].ToString());
                ZZError.Mz_Diff = Reader["Mz_Diff"].ToString();
                ZZError.Mz_Wc = Reader["Mz_Wc"].ToString();
                ZZError.Mz_Result = Reader["Mz_Result"].ToString();
                ZZError.Mz_U = Reader["Mz_U"].ToString();
                ZZError.Mz_i = Reader["Mz_I"].ToString();
                int_ID++;
                InfoItem.MeterZZErrors.Add("P_" + int_ID.ToString(), ZZError);
            }

            Reader.Close();
            #endregion

            #region --------------------��ȡ�๦��----------------------

            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM MeterDgn WHERE Md_LngMyID={0}", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM MeterDgn WHERE Md_LngMyID={0} AND Md_TaiID='{1}'", InfoItem._intMyId, InfoItem.Mb_TaiID);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterDgns.Clear();

            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterDgn Dgn = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterDgn();
                Dgn.Md_lngMyID = InfoItem._intMyId;
                Dgn.Md_PrjID = Reader["Md_PrjID"].ToString();
                Dgn.Md_PrjName = Reader["Md_PrjName"].ToString();
                Dgn.Md_chrValue = Reader["Md_chrvalue"].ToString();
                InfoItem.MeterDgns.Add(Dgn.Md_PrjID, Dgn);
            }

            Reader.Close();
            #endregion

            #region --------------��ȡ����춨����----------------
            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM MeterSpecialErr WHERE Mse_LngMyID={0}", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM MeterSpecialErr WHERE Mse_LngMyID={0} AND Mse_TaiID='{1}'", InfoItem._intMyId, InfoItem.Mb_TaiID);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterSpecialErrs.Clear();

            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr();
                Mse.Mse_LngMyID = InfoItem._intMyId;
                Mse.Mse_PrjName = Reader["Mse_PrjName"].ToString();
                Mse.Mse_Result = Reader["Mse_Result"].ToString();
                Mse.Mse_Glfx = int.Parse(Reader["Mse_Glfx"].ToString());
                Mse.Mse_Ub = Reader["Mse_Ub"].ToString();
                Mse.Mse_Ib = Reader["Mse_Ib"].ToString();
                Mse.Mse_Phase = Reader["Mse_Phase"].ToString();
                Mse.Mse_Nxx = int.Parse(Reader["Mse_Nxx"].ToString());
                Mse.Mse_XieBo = int.Parse(Reader["Mse_XieBo"].ToString());
                Mse.Mse_WcLimit = Reader["Mse_WcLimit"].ToString();
                Mse.Mse_Qs = int.Parse(Reader["Mse_Qs"].ToString());
                Mse.Mse_Wc = Reader["Mse_Wc"].ToString();
                int_ID++;
                InfoItem.MeterSpecialErrs.Add("P_" + int_ID.ToString(), Mse);
            }

            Reader.Close();

            #endregion

            #region---------------��ȡ��չ���ݱ�����---------------

            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM MeterExtend WHERE Med_LngMyID={0}", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM MeterExtend WHERE Med_LngMyID={0} AND Med_TaiID='{1}'", InfoItem._intMyId, InfoItem.Mb_TaiID);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterExtend.Clear();

            while (Reader.Read())
            {
                InfoItem.MeterExtend.Add(Reader["Med_KeyID"].ToString(), Reader["Med_Value"].ToString());
            }

            Reader.Close();

            #endregion
            **/
            #endregion
            return InfoItem;

            //}
            //catch (Exception e)
            //{
            //    System.Windows.Forms.MessageBox.Show(e.ToString());
            //    return InfoItem;
            //}
        }
        /// <summary>
        /// ɾ�����ܱ���Ϣ
        /// </summary>
        /// <param name="MyId"></param>
        /// <param name="TaiId"></param>
        /// <returns></returns>
        public bool DeleteMeterInfo(long MyId, string TaiId)
        {
            if (!base.Connection) return false;

            string SqlString;

            if (!IsServer)
            {
                SqlString = string.Format("DELETE FROM METER_INFO WHERE PK_LNG_METER_ID='{0}'", MyId);
            }
            else
            {
                SqlString = string.Format("DELETE FROM METER_INFO WHERE PK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID", MyId, TaiId);
            }

            

            System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(SqlString, base._Con);  

            if (base._Con.State == System.Data.ConnectionState.Closed)
                base._Con.Open();
           
           int i= Cmd.ExecuteNonQuery();

            Cmd = null;

            if (i > 0)
            {
                return true;
            }
            return false;
        }

        #endregion

        #region----------------------------����������ʱ����----------------------2013-8-9

        #region----------------------------��ѯ������Ϣ����--------------------------
        /// <summary>
        /// ��ѯ������Ϣ��
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns></returns>
        public List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> getBasicInformation(string Sql)
        {
            List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items = new List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo>();
            try
            {
                if (base._Con.State == System.Data.ConnectionState.Closed)
                    base._Con.Open();
                System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(Sql, base._Con);
                System.Data.OleDb.OleDbDataReader Reader = Cmd.ExecuteReader();
                while (Reader.Read())
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo();
                    Item._intMyId = Convert.ToInt64(Reader["PK_LNG_METER_ID"].ToString());//���
                    Item._intTaiNo = Reader["AVR_DEVICE_ID"].ToString().Trim();//̨����
                    Item.SetBno(Convert.ToInt32(Reader["LNG_BENCH_POINT_NO"].ToString()));//��λ��
                    Item.Mb_ChrJlbh = Reader["AVR_ASSET_NO"].ToString().Trim();//�������
                    Item.Mb_ChrCcbh = Reader["AVR_MADE_NO"].ToString().Trim();//�������
                    Item.Mb_ChrTxm = Reader["AVR_BAR_CODE"].ToString().Trim();//������
                    Item.Mb_chrAddr = Reader["AVR_ADDRESS"].ToString().Trim();//��ͨ�ŵ�ַ
                    Item.Mb_chrzzcj = Reader["AVR_FACTORY"].ToString().Trim();//���쳧��
                    Item.Mb_Bxh = Reader["AVR_METER_MODEL"].ToString().Trim();//���ͺ�
                    Item.Mb_chrBcs = Reader["AVR_AR_CONSTANT"].ToString().Trim();//����
                    Item.Mb_chrBlx = Reader["AVR_METER_TYPE"].ToString().Trim();//������
                    Item.Mb_chrBdj = Reader["AVR_AR_CLASS"].ToString().Trim();//��ȼ�
                    Item.Mb_chrCcrq = Reader["AVR_MADE_DATE"].ToString();//��������
                    Item.Mb_chrSjdw = Reader["AVR_CUSTOMER"].ToString().Trim();//�ͼ쵥λ
                    Item.Mb_chrZsbh = Reader["AVR_CERTIFICATE_NO"].ToString();//֤����
                    Item.Mb_ChrBmc = Reader["AVR_METER_NAME"].ToString().Trim();//������
                    Item.Mb_intClfs = Convert.ToInt32(Reader["AVR_WIRING_MODE"].ToString());//������ʽ
                    Item.Mb_chrUb = Reader["AVR_UB"].ToString().Trim();//���ѹ
                    Item.Mb_chrIb = Reader["AVR_IB"].ToString().Trim();//�����
                    Item.Mb_chrHz = Reader["AVR_FREQUENCY"].ToString().Trim();//Ƶ��
                    int znq = Convert.ToInt32(Reader["CHR_CC_PREVENT_FLAG"].ToString());//0=����ֹ������1=��ֹ����
                    if (1 == znq)
                        Item.Mb_BlnZnq = true;
                    else
                        Item.Mb_BlnZnq = false;
                    int hgq = Convert.ToInt32(Reader["CHR_CT_CONNECTION_FLAG"].ToString());//0=������������1=��������
                    if (1 == hgq)
                        Item.Mb_BlnHgq = true;
                    else
                        Item.Mb_BlnHgq = false;
                    Item.Mb_chrTestType = Reader["AVR_TEST_TYPE"].ToString().Trim();//��������
                    Item.Mb_DatJdrq = Reader["DTM_TEST_DATE"].ToString();//�춨����
                    Item.Mb_Datjjrq = Reader["DTM_VALID_DATE"].ToString();//�Ƽ�����
                    Item.Mb_chrWd = Reader["AVR_TEMPERATURE"].ToString().Trim();//�¶�
                    Item.Mb_chrSd = Reader["AVR_HUMIDITY"].ToString().Trim();//ʪ��
                    Item.Mb_chrResult = Reader["AVR_TOTAL_CONCLUSION"].ToString().Trim();//����
                    Item.Mb_ChrJyy = Reader["AVR_TEST_PERSON"].ToString().Trim();//����Ա
                    Item.Mb_ChrHyy = Reader["AVR_AUDIT_PERSON"].ToString().Trim();//����Ա
                    Item.Mb_chrZhuGuan = Reader["AVR_SUPERVISOR"].ToString().Trim();//����
                    int yaojian = Convert.ToInt32(Reader["CHR_CHECKED"].ToString());//1=Ҫ�죬0=��Ҫ��
                    if (1 == yaojian)
                        Item.YaoJianYn = true;
                    else
                        Item.YaoJianYn = false;
                    string setNet = Reader["CHR_UPLOAD_FLAG"].ToString().Trim();//�Ƿ�������
                    if (setNet.ToUpper().Contains("Y"))
                        Item.Mb_BlnToServer = true;
                    else
                        Item.Mb_BlnToServer = false;
                    Item.Mb_chrQianFeng1 = Reader["AVR_SEAL_1"].ToString().Trim();//Ǧ��1
                    Item.Mb_chrQianFeng2 = Reader["AVR_SEAL_2"].ToString().Trim();//Ǧ��2
                    Item.Mb_chrQianFeng3 = Reader["AVR_SEAL_3"].ToString().Trim();//Ǧ��3
                    Item.AVR_SEAL_4 = Reader["AVR_SEAL_4"].ToString().Trim();//Ǧ��4
                    Item.AVR_SEAL_5 = Reader["AVR_SEAL_5"].ToString().Trim();//Ǧ��5
                    Item.Mb_chrSoftVer = Reader["AVR_SOFT_VER"].ToString().TrimEnd();//����汾��
                    Item.Mb_chrHardVer = Reader["AVR_HARD_VER"].ToString().TrimEnd();//Ӳ���汾��
                    Item.Mb_chrArriveBatchNo = Reader["AVR_ARRIVE_BATCH_NO"].ToString().Trim();//�������κ�
                    Item.Mb_intSchemeID = Convert.ToInt32(Reader["FK_LNG_SCHEME_ID"].ToString());//����Ψһ���
                    Item.Mb_intProtocolID = Convert.ToInt32(Reader["FK_PROTOCOL_ID"].ToString());//Э��Ψһ���
                    Item.Mb_intFKType = Convert.ToInt32(Reader["CHR_RATES_TYPE"].ToString());//�ѿ�����
                    Item.AVR_WORK_NO = Reader["AVR_WORK_NO"].ToString().Trim();//������
                    Item.Mb_chrOther1 = Reader["AVR_OTHER_1"].ToString().Trim();//����1
                    Item.Mb_chrOther2 = Reader["AVR_OTHER_2"].ToString().Trim();//����2
                    Item.Mb_chrOther3 = Reader["AVR_OTHER_3"].ToString().Trim();//����3
                    Item.Mb_chrOther4 = Reader["AVR_OTHER_4"].ToString().Trim();//����4
                    Item.Mb_chrOther5 = Reader["AVR_OTHER_5"].ToString().Trim();//����4
                    Items.Add(Item);
                }
                Reader.Close();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "���ݿ��������", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return Items;
        }
        #endregion      
       
        #region-----------------------------------ɾ����ʱ�����ݷ���--------------------
         /// <summary>
        /// ɾ����ʱ����
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns></returns>
        private bool DeleteTmpData(string sql)
        {
            if (!base.Connection) return false;
            System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(sql, base._Con);
            int i = Cmd.ExecuteNonQuery();
            Cmd = null;
            if (i > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// ɾ��������Ϣ��ʱ����
        /// </summary>
        /// <param name="MyId">ID�����Ϊ-1����Ϊ����ɾ��</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterInfo(long MyId)
        {
            string SqlString = "";
            if (MyId != -1)
                SqlString = string.Format("DELETE FROM TMP_METER_INFO WHERE PK_LNG_METER_ID={0}", MyId);
            else
                SqlString = string.Format("DELETE FROM TMP_METER_INFO");
            return DeleteTmpData(SqlString);
        }
        /// <summary>
        /// ɾ������ʱ�ι�����ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="GroupType">��𣬿ɿ�</param>
        /// <param name="ListNo">��Ŀ�ţ��ɿ�</param>
        /// <param name="ItemType">С��Ŀ�ţ�������˲�������-1</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterFLSDgn(long MyId, string GroupType, string ListNo, short ItemType)
        {
            string SqlString = "";
            SqlString = string.Format("Delete from TMP_METER_FUN_RATES_TIME_CONS where PK_LNG_METER_ID={0}", MyId);
            if (!string.IsNullOrEmpty(GroupType))
            {
                SqlString += " AND AVR_GROUP_TYPE='" + GroupType + "'";
            }
            if (!string.IsNullOrEmpty(ListNo))
            {
                SqlString += " AND AVR_LIST_NO='" + ListNo + "'";
            }
            if (-1 != ItemType)
            {
                SqlString += " AND AVR_ITEM_TYPE=" + ItemType;
            }
            return DeleteTmpData(SqlString);
        }
        
        /// <summary>
        /// ɾ������춨������ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="Memo">��������,�ɿ�</param>
        /// <param name="WcType">�������޴˲�����-1</param>
        /// <param name="WcLb">����޴˲�����-1</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterTsjd(long MyId, string Memo, int WcType, int WcLb)
        {
            string SqlString = "";
            SqlString = string.Format("Delete from TMP_METER_SPECIAL_DATA where FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(Memo))
            {
                SqlString += " AND FK_LNG_SCHEME_ID='" + Memo + "'";
            }
            if (-1 != WcType)
            {
                SqlString += " AND CHR_ERROR_TYPE=" + WcType;
            }
            if (-1 != WcLb)
            {
                SqlString += " AND CHR_POWER_TYPE=" + WcLb;
            }
            return DeleteTmpData(SqlString);
        }

        /// <summary>
        /// ɾ������������ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="GroupType">���,�ɿ�</param>
        /// <param name="ListNo">��Ŀ��,�ɿ�</param>
        /// <param name="ItemType">С��Ŀ��,����Ҫ�˲�����-1</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterXLgn(long MyId, string GroupType, string ListNo, short ItemType)
        {
            string SqlString = "";
            SqlString = string.Format("Delete from TMP_METER_FUN_MAX_DEMAND where FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(GroupType))
            {
                SqlString += " AND AVR_GRP_TYPE='" + GroupType + "'";
            }
            if (!string.IsNullOrEmpty(ListNo))
            {
                SqlString += " AND AVR_LIST_NO='" + ListNo + "'";
            }
            if (-1 != ItemType)
            {
                SqlString += " AND AVR_ITEM_TYPE=" + ItemType;
            }
            return DeleteTmpData(SqlString);
        }

        /// <summary>
        /// ɾ������������ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="GroupType">���,�ɿ�</param>
        /// <param name="ListNo">��Ŀ��,�ɿ�</param>
        /// <param name="ItemType">С��Ŀ��,����Ҫ�˲�����-1</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterJLgn(long MyId, string GroupType, string ListNo, short ItemType)
        {
            string SqlString = "";
            SqlString = string.Format("Delete from TMP_METER_FUN_ENERGY_MEASURE where FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(GroupType))
            {
                SqlString += " AND AVR_GROUP_TYPE='" + GroupType + "'";
            }
            if (!string.IsNullOrEmpty(ListNo))
            {
                SqlString += " AND AVR_LIST_NO='" + ListNo + "'";
            }
            if (-1 != ItemType)
            {
                SqlString += " AND AVR_ITEM_TYPE=" + ItemType;
            }
            return DeleteTmpData(SqlString);
        }

        /// <summary>
        /// ɾ��������ʾ������ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="GroupType">��𣬿ɿ�</param>
        /// <param name="ItemType">��Ŀ���ͣ��ɿ�</param>
        /// <param name="Type">ʵ����Ŀ���ͣ�����˲�����-1</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterShow(long MyId, string GroupType, string ItemType, int Type)
        {
            string SqlString = "";
            SqlString = string.Format("Delete from TMP_METER_FUN_SHOW where FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(GroupType))
            {
                SqlString += " AND AVR_GROUP_TYPE='" + GroupType + "'";
            }
            if (!string.IsNullOrEmpty(ItemType))
            {
                SqlString += " AND AVR_ITEM_TYPE='" + ItemType + "'";
            }
            if (-1 != Type)
            {
                SqlString += " AND AVR_CHILD_ITEM_TYPE=" + Type;
            }
            return DeleteTmpData(SqlString);
        }
        
        /// <summary>
        /// ɾ���¼���¼��ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="GrpType">��𣬿ɿ�</param>
        /// <param name="XmName">��Ŀ�����ɿ�</param>
        /// <param name="ListNo">��Ŀ�ţ��ɿ�</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterSjJLgn(long MyId,string GrpType,string XmName,string ListNo)
        {
            string SqlString = "";
            SqlString = string.Format("Delete from TMP_METER_FUN_EVENT_RECORD where FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(GrpType))
            {
                SqlString += " AND AVR_GROUP_TYPE='" + GrpType + "'";
            }
            if (!string.IsNullOrEmpty(XmName))
            {
                SqlString += " AND AVR_ITEM_NAME='" + XmName + "'";
            }
            if (!string.IsNullOrEmpty(ListNo))
            {
                SqlString += " AND AVR_LIST_NO='" + ListNo + "'";
            }
            return DeleteTmpData(SqlString);
        }

        /// <summary>
        /// ɾ����������ֵ��ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="ProjectNo">�춨��Ŀ�ţ��ɿ�</param>
        /// <param name="WcType">�����𣬲���˲�����-1</param>
        /// <param name="WcLb">��𣬲���˲�����-1</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterError(long MyId, string ProjectNo, int WcType, int WcLb)
        {
            string SqlString = "";
            SqlString = string.Format("Delete from TMP_METER_ERROR where FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(ProjectNo))
            {
                SqlString += " AND AVR_PROJECT_NO='" + ProjectNo + "'";
            }
            if (-1 != WcType)
            {
                SqlString += " AND CHR_ERROR_TYPE=" + WcType;
            }
            if (-1 != WcLb)
            {
                SqlString += " AND CHR_POWER_TYPE=" + WcLb;
            }
            return DeleteTmpData(SqlString);
        }

        /// <summary>
        /// ɾ������������ʱ����
        /// </summary>
        /// <param name="MyId">���</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterGhData(long MyId)
        {
            string SqlString = "";
            SqlString = string.Format("DELETE FROM TMP_METER_POWER_CONSUM_DATA WHERE FK_LNG_METER_ID='{0}'", MyId);
            return DeleteTmpData(SqlString);
        }

        /// <summary>
        /// ɾ����������������ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="Jdfx">�춨���򣬿ɿ�</param>
        /// <param name="Fl">���ʣ��ɿ�</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterZzData(long MyId, string Jdfx, string Fl)
        {
            string SqlString = "";
            SqlString = string.Format("DELETE FROM TMP_METER_ENERGY_TEST_DATA WHERE FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(Jdfx))
            {
                SqlString += " AND CHR_POWER_TYPE='" + Jdfx + "'";
            }
            if (!string.IsNullOrEmpty(Fl))
            {
                SqlString += " AND AVR_RATES='" + Fl + "'";
            } 
            return DeleteTmpData(SqlString);
        }

        /// <summary>
        /// ɾ��Ǳ������������ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="ProjectNo">�춨��Ŀ��,�ɿ�</param>
        /// <param name="Jdfx">�춨���򣬿ɿ�</param>
        /// <param name="ProjectName">��Ŀ���ƣ��ɿ�</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterQdQid(long MyId, string ProjectNo, string Jdfx, string ProjectName)
        {
            string SqlString = "";
            SqlString = string.Format("DELETE FROM TMP_METER_START_NO_LOAD WHERE FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(ProjectNo))
            {
                SqlString += " AND AVR_PROJECT_NO='" + ProjectNo + "'";
            }
            if (!string.IsNullOrEmpty(Jdfx))
            {
                SqlString += " AND CHR_POWER_TYPE='" + Jdfx + "'";
            }
            if (!string.IsNullOrEmpty(ProjectName))
            {
                SqlString += " AND AVR_PROJECT_NAME='" + ProjectName + "'";
            } 
            return DeleteTmpData(SqlString);
        }

        /// <summary>
        /// ɾ���ѿ�������ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="GrpType">��𣬿ɿ�</param>
        /// <param name="ItemType">��Ŀ���ͣ��ɿ�</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterFK(long MyId, string GrpType, string ItemType)
        {
            string SqlString = "";
            SqlString = string.Format("DELETE FROM TMP_METER_RATES_CONTROL WHERE FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(GrpType))
            {
                SqlString += " AND AVR_GROUP_TYPE='" + GrpType + "'";
            }
            if (!string.IsNullOrEmpty(ItemType))
            {
                SqlString += " AND AVR_ITEM_TYPE='" + ItemType + "'";
            }
            return DeleteTmpData(SqlString);
        }

        /// <summary>
        /// ɾ�������๦����Ϣ��ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="ProjectNo">�춨��Ŀ�ţ��ɿ�</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterDgn(long MyId, string ProjectNo)
        {
            string SqlString = "";
            SqlString = string.Format("DELETE FROM TMP_METER_COMMUNICATION WHERE FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(ProjectNo))
            {
                SqlString += " AND AVR_PROJECT_NO='" + ProjectNo + "'";
            }
            return DeleteTmpData(SqlString);
        }

        /// <summary>
        /// ɾ���ز�485������ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="Item">�춨��Ŀ���ɿ�</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterZb486(long MyId, string Item)
        {
            string SqlString = "";
            SqlString = string.Format("DELETE FROM TMP_METER_CARRIER_WAVE WHERE FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(Item))
            {
                SqlString += " AND AVR_ITEM_NAME='" + Item + "'";
            }
            return DeleteTmpData(SqlString);
        }
        /// <summary>
        /// ɾ����Լһ����������ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="ItemID">��ĿID������˲�����-1</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterDLTData(long MyId, int ItemID)
        {
            string SqlString = "";
            SqlString = string.Format("DELETE FROM TMP_METER_STANDARD_DLT_DATA WHERE FK_LNG_METER_ID='{0}'", MyId);
            if (-1 !=ItemID)
            {
                SqlString += " AND FK_LNG_ITEM_ID=" + ItemID;
            }
            return DeleteTmpData(SqlString);
        }

        /// <summary>
        /// ɾ��һ��������������ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="GrpType">��𣬿ɿ�</param>
        /// <param name="ItemType">��Ŀ���ͣ��ɿ�</param>
        /// <param name="ItemNo">��Ŀ���ص��ţ�����˲�����-1</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterConsistency(long MyId, string GrpType, string ItemType, int ItemNo)
        {
            string SqlString = "";
            SqlString = string.Format("DELETE FROM TMP_METER_CONSISTENCY_DATA WHERE FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(GrpType))
            {
                SqlString += " AND AVR_GRP_TYPE=" + GrpType;
            }
            if (!string.IsNullOrEmpty(ItemType))
            {
                SqlString += " AND AVR_ITEM_TYPE=" + ItemType;
            }
            if (-1 != ItemNo)
            {
                SqlString += " AND AVR_ITEM_NO=" + ItemNo;
            }
            return DeleteTmpData(SqlString);
        }
        /// <summary>
        /// ɾ�����۱���ʱ����
        /// </summary>
        /// <param name="MyId">Id��ţ�����</param>
        /// <param name="RstId">����ID���ɿ�</param>
        /// <returns></returns>
        public bool DeleteTmp_MeterResult(long MyId, string RstId)
        {
            string SqlString = "";
            SqlString = string.Format("DELETE FROM TMP_METER_RESULTS WHERE FK_LNG_METER_ID='{0}'", MyId);
            if (!string.IsNullOrEmpty(RstId))
            {
                SqlString += " AND AVR_RESULT_ID=" + RstId;
            }
            return DeleteTmpData(SqlString);
        }
        #endregion

        #region------------------------------����͸��»�����Ϣ��-----------------------------
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="Items">����</param>
        /// <param name="flag">��־��true��������ʽ�⣬false��������ʱ��</param>
        public bool Insert_MeterInfo(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Items,bool flag)
        {
            if (!base.Connection) return false ;
            string sqlString = "";
            string tableStr="";
            if(flag)
                tableStr = "METER_INFO";
            else
                tableStr = "TMP_METER_INFO";
                 
            sqlString = "Insert Into " + tableStr + " (PK_LNG_METER_ID,AVR_DEVICE_ID,LNG_BENCH_POINT_NO,"
                    + "AVR_ASSET_NO,AVR_MADE_NO,AVR_BAR_CODE,AVR_ADDRESS,AVR_FACTORY,AVR_METER_MODEL,AVR_AR_CONSTANT,AVR_METER_TYPE,AVR_AR_CLASS,AVR_MADE_DATE,AVR_CUSTOMER,AVR_CERTIFICATE_NO,"
                + "AVR_METER_NAME,AVR_WIRING_MODE,AVR_UB,AVR_IB,AVR_FREQUENCY,CHR_CC_PREVENT_FLAG,CHR_CT_CONNECTION_FLAG,AVR_TEST_TYPE,DTM_TEST_DATE,DTM_VALID_DATE,AVR_TEMPERATURE,AVR_HUMIDITY,"
                + "AVR_TOTAL_CONCLUSION,AVR_TEST_PERSON,AVR_AUDIT_PERSON,AVR_SUPERVISOR,CHR_CHECKED,CHR_UPLOAD_FLAG,AVR_SEAL_1,AVR_SEAL_2,AVR_SEAL_3,AVR_SEAL_4,AVR_SEAL_5,AVR_SOFT_VER,AVR_HARD_VER,"
                + "AVR_ARRIVE_BATCH_NO,FK_LNG_SCHEME_ID,FK_PROTOCOL_ID,CHR_RATES_TYPE,AVR_TASK_NO,AVR_WORK_NO,AVR_OTHER_1,AVR_OTHER_2,AVR_OTHER_3,AVR_OTHER_4,AVR_OTHER_5) values ("
                 + Items._intMyId + ",'" + Items._intTaiNo + "'," + Items.Mb_intBno + ",'"
                + Items.Mb_intBno + ",'"
                + Items.Mb_ChrJlbh + "','"
                + Items.Mb_ChrCcbh + "','"
                + Items.Mb_ChrTxm + "','"
                + Items.Mb_chrAddr + "','"
                + Items.Mb_chrzzcj + "','"
                + Items.Mb_Bxh + "','"
                + Items.Mb_chrBcs + "','"
                + Items.Mb_chrBlx + "','"
                + Items.Mb_chrBdj + "','"
                + Items.Mb_chrCcrq + "','"
                + Items.Mb_chrSjdw + "','"
                + Items.Mb_chrZsbh + "','"
                + Items.Mb_ChrBmc + "',"
                + Items.Mb_intClfs + ",'"
                + Items.Mb_chrUb + "','"
                + Items.Mb_chrIb + "','"
                + Items.Mb_chrHz + "','"
                + (Items.Mb_BlnZnq == true ? "1" : "0") + "','"
                + (Items.Mb_BlnHgq == true ? "1" : "0") + "','"
                + Items.Mb_chrTestType + "',#"
                    + DateTime.Now + "#,#"//_BasicInfo.Mb_DatJdrq
                    + DateTime.Now.AddMonths(12) + "#,'"//_BasicInfo.Mb_Datjjrq
                + Items.Mb_chrWd + "','"
                + Items.Mb_chrSd + "','"
                + Items.Mb_chrResult + "','"
                    + CLDC_DataCore.Const.GlobalUnit.User_Jyy.UserName + "','"//_BasicInfo.Mb_ChrJyy
                    + CLDC_DataCore.Const.GlobalUnit.User_Hyy.UserName + "','"//_BasicInfo.Mb_ChrHyy
                    + Items.Mb_chrZhuGuan + "','"
                    + (Items.YaoJianYn == true ? 1 : 0) + "','"
                + (Items.Mb_BlnToServer == true ? "1" : "0") + "','"
                + Items.Mb_chrQianFeng1 + "','"
                + Items.Mb_chrQianFeng2 + "','"
                + Items.Mb_chrQianFeng3 + "','"
                    + Items.AVR_SEAL_4 + "','"
                    + Items.AVR_SEAL_5 + "','"
                + Items.Mb_chrSoftVer + "','"
                + Items.Mb_chrHardVer + "','"
                + Items.Mb_chrArriveBatchNo + "',"
                + Items.Mb_intSchemeID + ","
                    + Items.Mb_intProtocolID + ",'"
                    + Items.Mb_intFKType + "','"
                    + Items.AVR_TASK_NO + "','"
                    + Items.AVR_WORK_NO + "','"
                + Items.Mb_chrOther1 + "','"
                + Items.Mb_chrOther2 + "','"
                + Items.Mb_chrOther3 + "','"
                    + Items.Mb_chrOther4 + "','"
                    + Items.Mb_chrOther5 + "')";

            int i = base.ExecuteSql(sqlString);
            if (i > 0)
                return true;
            else
                return false;                
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="Items">��������</param>
        /// <param name="flag">��־��true��������ʽ�⣬false��������ʱ��</param>
        /// <returns></returns>
        public bool Update_MeterInfo(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Items, bool flag)
        {
            if (!base.Connection) return false;
            string sqlString = "";
            string tableStr = "";
            if (flag)
                tableStr = "METER_INFO";
            else
                tableStr = "TMP_METER_INFO";

            sqlString = string.Format("Update " + tableStr + "Set PK_LNG_METER_ID={0},AVR_DEVICE_ID={1},LNG_BENCH_POINT_NO={2},AVR_ASSET_NO='{3}',AVR_MADE_NO='{4}',AVR_BAR_CODE='{5}',AVR_ADDRESS='{6}',AVR_FACTORY='{7}',AVR_METER_MODEL='{8}',AVR_AR_CONSTANT='{9}',AVR_METER_TYPE='{10}',"
                + "AVR_AR_CLASS='{11}',AVR_MADE_DATE='{12}',AVR_CUSTOMER='{13}',AVR_CERTIFICATE_NO='{14}',AVR_METER_NAME='{15}',AVR_WIRING_MODE={16},AVR_UB='{17}',AVR_IB='{18}',AVR_FREQUENCY='{19}',CHR_CC_PREVENT_FLAG='{20}',CHR_CT_CONNECTION_FLAG='{21}',AVR_TEST_TYPE='{22}',DTM_TEST_DATE= #{23}#,DTM_VALID_DATE=#{24}#,AVR_TEMPERATURE='{25}',"
                + "AVR_HUMIDITY='{26}',AVR_TOTAL_CONCLUSION='{27}',AVR_TEST_PERSON='{28}',AVR_AUDIT_PERSON='{29}',AVR_SUPERVISOR='{30}',CHR_CHECKED={31},CHR_UPLOAD_FLAG='{32}',AVR_SEAL_1='{33}',AVR_SEAL_2='{34}',AVR_SEAL_3='{35},AVR_SOFT_VER='{36}',"
                + "AVR_HARD_VER='{37}',AVR_ARRIVE_BATCH_NO='{38}',FK_LNG_SCHEME_ID={39},FK_PROTOCOL_ID={4},CHR_RATES_TYPE={41},AVR_OTHER_1='{42}',AVR_OTHER_2='{43}',AVR_OTHER_3='{44}',AVR_OTHER_4='{45}',AVR_OTHER_5='{46}',AVR_SEAL_4='{47}',AVR_SEAL_5='{48}',AVR_TASK_NO='{49}',AVR_WORK_NO='{50}' where PK_LNG_METER_ID={51}",
                Items._intMyId , Convert.ToInt32(Items._intTaiNo) , Items.Mb_intBno , Items.Mb_ChrJlbh , Items.Mb_ChrCcbh , Items.Mb_ChrTxm , Items.Mb_chrAddr ,
                Items.Mb_chrzzcj , Items.Mb_Bxh ,Items.Mb_chrBcs , Items.Mb_chrBlx , Items.Mb_chrBdj , Items.Mb_chrCcrq , Items.Mb_chrSjdw , Items.Mb_chrZsbh ,
                Items.Mb_ChrBmc , Items.Mb_intClfs , Items.Mb_chrUb , Items.Mb_chrIb , Items.Mb_chrHz , (Items.Mb_BlnZnq == true ? "1" : "0") , Items.Mb_BlnHgq == true ? "1" : "0" ,
                Items.Mb_chrTestType , Items.Mb_DatJdrq , Items.Mb_Datjjrq , Items.Mb_chrWd , Items.Mb_chrSd , Items.Mb_chrResult , Items.Mb_ChrJyy , Items.Mb_ChrHyy , 
                Items.Mb_chrZhuGuan , Items.YaoJianYn == true ? 1 : 0 , Items.Mb_BlnToServer == true ? "1" : "0" , Items.Mb_chrQianFeng1 , Items.Mb_chrQianFeng2 , 
                Items.Mb_chrQianFeng3 , Items.Mb_chrSoftVer ,Items.Mb_chrHardVer , Items.Mb_chrArriveBatchNo , Items.Mb_intSchemeID , Items.Mb_intProtocolID ,
                Items.Mb_intFKType , Items.Mb_chrOther1 , Items.Mb_chrOther2 , Items.Mb_chrOther3 , Items.Mb_chrOther4,Items._intMyId);

            int i = base.ExecuteSql(sqlString);
            if (i > 0)
                return true;
            else
                return false; 
        }
        #endregion
           
        #region---------------------------����ɾ����ʽ������-----------------------
        /// <summary>
        /// ����Id�жϱ����Ƿ���ڴ�������
        /// </summary>
        /// <param name="tableStr"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private bool SelectData(string tableStr, long Id)
        {
            string sql = string.Format("Select * from" + tableStr + "where PK_LNG_METER_ID={0}", Id);
            System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(sql, base._Con);
            System.Data.OleDb.OleDbDataReader dr = Cmd.ExecuteReader();
            if (dr.HasRows)
                return true;
            else
                return false;
        }
        /// <summary>
        /// ����IDɾ����ʽ�����б���������
        /// </summary>
        /// <param name="MyId"></param>
        /// <returns></returns>
        public bool DeleteAllData(long MyId)
        {
            List<string> SqlStr = new List<string>();
            foreach (Cus_DBTableName item in System.Enum.GetValues(typeof(Cus_DBTableName)))
            {
                string str = string.Format("DELETE FROM {1} WHERE PK_LNG_METER_ID={0}", MyId, System.Enum.GetName(typeof(Cus_DBTableName), item));
                SqlStr.Add(str);
            }
            #region �滻�����������
            //if (SelectData("MeterFLSDgn", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterFLSDgn WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterTsjd", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterTsjd WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterXLgn", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterXLgn WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterJLgn", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterJLgn WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterShow", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterShow WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterSjJLgn", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterSjJLgn WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterError", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterError WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterGhData", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterGhData WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterZzData", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterZzData WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterQdQid", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterQdQid WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //} 
            //if (SelectData("MeterFK", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterFK WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterDgn", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterDgn WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //} 
            //if (SelectData("MeterZb485", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterZb485 WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterDLTData", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterDLTData WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterConsistency", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterConsistency WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            //if (SelectData("MeterResult", MyId))
            //{
            //    string str = string.Format("DELETE FROM MeterResult WHERE intMyId={0}", MyId);
            //    SqlStr.Add(str);
            //}
            #endregion
            bool _Return = base.ExecuteSqlTran(SqlStr);
            return _Return;
        }
        #endregion       

        /// <summary>
        /// ��ȡ���ܱ���ϸ����
        /// </summary>
        /// <param name="InfoItem"></param>
        /// <param name="FlagOfTmp">true��ʽ�⣬false��ʱ��</param>
        /// <returns></returns>
        public Model.DnbModel.DnbInfo.MeterBasicInfo getDnbInfoNew(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo InfoItem, bool FlagOfTmp)
        {
            if (!base.Connection) return InfoItem;

            //try
            //{
            string SQLString;
            string TableName;
            string strKey;
            

            #region ------------------------��ȡ��������---------------------------------------
            if (true == FlagOfTmp)
            {
                TableName = "METER_RESULTS";
            }
            else
            {
                TableName = "TMP_METER_RESULTS";
            }
            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            
            System.Data.OleDb.OleDbDataReader Reader = Cmd.ExecuteReader();

            InfoItem.MeterResults.Clear();

            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult Result = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult();

                Result.Mr_chrRstId = Reader["AVR_RESULT_ID"].ToString().Trim();
                Result.Mr_chrRstName = Reader["AVR_RESULT_NAME"].ToString().Trim();
                Result.Mr_chrRstValue = Reader["AVR_RESULT_VALUE"].ToString().Trim();
                Result.Mr_chrNote = Reader["AVR_NOTE"].ToString().Trim();
                strKey = Result.Mr_chrRstId.Trim();
                if (strKey.Length > 0)
                {
                    if (InfoItem.MeterResults.ContainsKey(strKey))
                        InfoItem.MeterResults.Remove(strKey);
                    InfoItem.MeterResults.Add(Result.Mr_chrRstId, Result);
                }
            }
            Reader.Close();

            #endregion

            #region--------------------------��ȡ�������------------------------------
            if (true == FlagOfTmp)
            {
                TableName = "METER_ERROR";
            }
            else
            {
                TableName = "TMP_METER_ERROR";
            }
            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterErrors.Clear();

            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError Error = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError();

                Error.Me_chrProjectNo = Reader["AVR_PROJECT_NO"].ToString().Trim();

                Error.Me_chrWcJl = Reader["AVR_ERROR_CONCLUSION"].ToString().Trim();
                Error.Me_chrGlys = Reader["AVR_POWER_FACTOR"].ToString();
                Error.Me_dblxIb = Reader["AVR_IB_MULTIPLE"].ToString();
                Error.Me_chrMemo = Reader["FK_LNG_SCHEME_ID"].ToString();
                Error.Me_chrWcHz = Reader["AVR_ERROR_ROUNDING"].ToString();
                Error.Me_Glfx = Reader["CHR_POWER_TYPE"].ToString();
                Error.Me_intWcType = int.Parse(Reader["CHR_ERROR_TYPE"].ToString());
                Error.Me_intYj = int.Parse(Reader["CHR_COMPONENT"].ToString());
                Error.Me_chrWcMore = Reader["AVR_ERROR_MORE"].ToString();
                Error.Me_chrWc = Reader["AVR_ERROR_AVERAGE"].ToString();
                Error.AVR_CIRCLE_COUNT = Reader["AVR_CIRCLE_COUNT"].ToString();
                Error.AVR_DIF_ERR_AVG = Reader["AVR_DIF_ERR_AVG"].ToString();
                Error.AVR_DIF_ERR_ROUND = Reader["AVR_DIF_ERR_ROUND"].ToString();
                Error.AVR_DIF_H_ERR_AVG = Reader["AVR_DIF_H_ERR_AVG"].ToString();
                Error.AVR_DIF_H_ERR_ROUND = Reader["AVR_DIF_H_ERR_ROUND"].ToString();
                Error.AVR_DIF_H_ERRORS = Reader["AVR_DIF_H_ERRORS"].ToString();
                Error.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                Error.AVR_IB_MULTIPLE_STRING = Reader["AVR_IB_MULTIPLE_STRING"].ToString();
                Error.AVR_LOWER_LIMIT = Reader["AVR_LOWER_LIMIT"].ToString();
                Error.AVR_STANDARD_ERROR = Reader["AVR_STANDARD_ERROR"].ToString();
                Error.CHR_DIF_ERR_FLAG = Reader["CHR_DIF_ERR_FLAG"].ToString();
                strKey = Error.Me_chrProjectNo.Trim();
                if (InfoItem.MeterErrors.ContainsKey(strKey))
                    InfoItem.MeterErrors.Remove(strKey);
                InfoItem.MeterErrors.Add(strKey, Error);
            }

            Reader.Close();

            #endregion

            #region --------------------��ȡ��������----------------------
            if (true == FlagOfTmp)
            {
                TableName = "METER_ENERGY_TEST_DATA";
            }
            else
            {
                TableName = "TMP_METER_ENERGY_TEST_DATA";
            }
            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterZZErrors.Clear();

            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError ZZError = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError();

                ZZError.Me_chrProjectNo = Reader["AVR_PROJECT_NO"].ToString();
                ZZError.Mz_chrFl = Reader["AVR_RATES"].ToString();
                ZZError.Mz_chrGlys = Reader["AVR_POWER_FACTOR"].ToString();
                ZZError.Mz_chrxIbString = Reader["AVR_IB_MULTIPLE_STRING"].ToString();
                ZZError.Mz_chrJdfx = Reader["CHR_POWER_TYPE"].ToString();
                ZZError.Mz_chrJL = Reader["AVR_CONCLUSION"].ToString();
                ZZError.Mz_chrNeedTime = Reader["AVR_NEED_TIME"].ToString();
                ZZError.Mz_chrPules = Reader["AVR_NUMBER_PULSES"].ToString();
                ZZError.Mz_chrQiMa = float.Parse(Reader["AVR_START_ENERGY"].ToString());
                ZZError.Mz_chrQiMaZ = Reader["AVR_START_SUM_ENERGY"].ToString();
                ZZError.Mz_chrQiZiMaC = Reader["AVR_DIF_ENERGY"].ToString();
                ZZError.Mz_chrStartTime = Reader["AVR_START_TIME"].ToString();
                ZZError.Mz_chrWc = Reader["AVR_ERROR"].ToString();
                ZZError.Mz_chrZiMa = float.Parse(Reader["AVR_END_ENERGY"].ToString());
                ZZError.Mz_chrZiMaZ = Reader["AVR_END_SUM_ENERGY"].ToString();
                ZZError.AVR_NEED_ENERGY = Reader["AVR_NEED_ENERGY"].ToString();
                ZZError.AVR_STANDARD_METER_ENERGY = Reader["AVR_STANDARD_METER_ENERGY"].ToString();
                ZZError.AVR_TEST_WAY = Reader["AVR_TEST_WAY"].ToString();
                ZZError.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                if(InfoItem.MeterZZErrors.ContainsKey(ZZError.Me_chrProjectNo))
					InfoItem.MeterZZErrors.Remove(ZZError.Me_chrProjectNo);
            	InfoItem.MeterZZErrors.Add(ZZError.Me_chrProjectNo, ZZError);
            }

            Reader.Close();
            #endregion

            #region --------------------��ȡ�๦��----------------------
            if (true == FlagOfTmp)
            {
                TableName = "METER_COMMUNICATION";
            }
            else
            {
                TableName = "TMP_METER_COMMUNICATION";
            }
            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterDgns.Clear();

            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterDgn Dgn = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterDgn();

                Dgn.Md_PrjID = Reader["AVR_PROJECT_NO"].ToString();
                //Dgn.Md_PrjName = Reader["Md_PrjName"].ToString();
                Dgn.Md_chrValue = Reader["AVR_VALUE"].ToString().Trim();
                Dgn.AVR_CONCLUSION = Reader["AVR_CONCLUSION"].ToString().Trim();
                Dgn.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString().Trim();
                strKey = Dgn.Md_PrjID.Trim();
                if (InfoItem.MeterDgns.ContainsKey(strKey))
                    InfoItem.MeterDgns.Remove(strKey);
                InfoItem.MeterDgns.Add(strKey, Dgn);
            }

            Reader.Close();
            #endregion

            #region --------------��ȡ����춨����----------------
            if (true == FlagOfTmp)
            {
                TableName = "METER_SPECIAL_DATA";
            }
            else
            {
                TableName = "TMP_METER_SPECIAL_DATA";
            }
            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterSpecialErrs.Clear();

            int int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr();

                Mse.Mse_PrjName = Reader["AVR_ITEM_NAME"].ToString();
                Mse.Mse_Result = Reader["AVR_ERR_CONCLUSION"].ToString();
                Mse.Mse_PrjNumber = Reader["AVR_PROJECT_NO"].ToString();
                //Mse.Mse_Glfx = int.Parse(Reader["AVR_POWER_FACTOR"].ToString());
                //Mse.Mse_Ub = Reader["Mse_Ub"].ToString();
                //Mse.Mse_Ib = Reader["Mse_Ib"].ToString();
                //Mse.Mse_Phase = Reader["Mse_Phase"].ToString();
                Mse.Mse_Nxx = int.Parse(Reader["CHR_NEGATIVE_PHASE_FLAG"].ToString());
                Mse.Mse_XieBo = int.Parse(Reader["CHR_HARMONIC_WAVE_FLAG"].ToString());
                //Mse.Mse_WcLimit = Reader["Mse_WcLimit"].ToString();
                Mse.Mse_Qs = int.Parse(Reader["AVR_CIRCLE_COUNT"].ToString());
                Mse.Mse_Wc = Reader["AVR_ERROR_MORE"].ToString();
                Mse.AVR_BASE_LOAD_NO = Reader["AVR_BASE_LOAD_NO"].ToString();
                Mse.AVR_CIRCLE_COUNT = Reader["AVR_CIRCLE_COUNT"].ToString();
                Mse.AVR_CUR_A = Reader["AVR_CUR_A"].ToString();
                Mse.AVR_CUR_A_MULTIPLE_STRING = Reader["AVR_CUR_A_MULTIPLE_STRING"].ToString();
                Mse.AVR_CUR_B_MULTIPLE = Reader["AVR_CUR_B_MULTIPLE"].ToString();
                Mse.AVR_CUR_B_MULTIPLE_STRING = Reader["AVR_CUR_B_MULTIPLE_STRING"].ToString();
                Mse.AVR_CUR_C_MULTIPLE = Reader["AVR_CUR_C_MULTIPLE"].ToString();
                Mse.AVR_CUR_C_MULTIPLE_STRING = Reader["AVR_CUR_C_MULTIPLE_STRING"].ToString();
                //Mse.AVR_DIF_ERR_AVG = Reader["AVR_DIF_ERR_AVG"].ToString();
                //Mse.AVR_DIF_ERR_ROUND = Reader["AVR_DIF_ERR_ROUND"].ToString();
                //Mse.AVR_DIF_ERROR = Reader["AVR_DIF_ERROR"].ToString();
                //Mse.AVR_DIF_H_ERR_AVG = Reader["AVR_DIF_H_ERR_AVG"].ToString();
                //Mse.AVR_DIF_H_ERR_ROUND = Reader["AVR_DIF_H_ERR_ROUND"].ToString();
                //Mse.AVR_DIF_H_ERRORS = Reader["AVR_DIF_H_ERRORS"].ToString();
                //Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                //Mse.AVR_IB_MULTIPLE_STRING = Reader["AVR_IB_MULTIPLE_STRING"].ToString();
                Mse.AVR_LOWER_LIMIT = Reader["AVR_LOWER_LIMIT"].ToString();
                //Mse.AVR_STANDARD_ERROR = Reader["AVR_STANDARD_ERROR"].ToString();
                Mse.AVR_VOT_A = Reader["AVR_VOT_A"].ToString();
                Mse.AVR_VOT_B_MULTIPLE = Reader["AVR_VOT_B_MULTIPLE"].ToString();
                Mse.AVR_VOT_C_MULTIPLE = Reader["AVR_VOT_C_MULTIPLE"].ToString();
                Mse.CHR_BASE_LOAD_FLAG = Reader["CHR_BASE_LOAD_FLAG"].ToString();
                //Mse.CHR_DIF_ERR_FLAG = Reader["CHR_DIF_ERR_FLAG"].ToString();
                int_ID++;
                //add by zxr 20140813
                strKey = Mse.Mse_PrjNumber.Trim();
                if (strKey.Length > 0)
                {
                    strKey = strKey.Replace(" ", "");
                }
                else
                {
                    strKey = "P_" + int_ID.ToString();
                }

                if (InfoItem.MeterSpecialErrs.ContainsKey(strKey))
                    InfoItem.MeterSpecialErrs.Remove(strKey);
                InfoItem.MeterSpecialErrs.Add(strKey, Mse);
            }

            Reader.Close();

            #endregion

            #region ------------------��ȡ�����ѿ����������---------------------lees20161222
            /// <summary>
            /// ACCESS���ݿ������ַ�������
            /// </summary>
            const string CONST_ACCESS = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=True;Jet OLEDB:DataBase Password=;Data Source=";
            string NWDataPath = clsMain2.getIniString("OhterSoft", "othersoftpath", "");
            string OtherSoftId = "";
            if (System.IO.File.Exists(NWDataPath))
            {
                using (System.Data.OleDb.OleDbConnection OtherConn = new System.Data.OleDb.OleDbConnection(CONST_ACCESS + NWDataPath))
                {
                    if (OtherConn.State == System.Data.ConnectionState.Closed)
                        OtherConn.Open();

                    SQLString = string.Format("SELECT * FROM " + "METER_INFO" + " WHERE AVR_ASSET_NO='{0}'", InfoItem.Mb_ChrJlbh);

                    Cmd = new System.Data.OleDb.OleDbCommand(SQLString, OtherConn);

                    Reader = Cmd.ExecuteReader();

                    

                    InfoItem.MeterOtherSoftData.Clear();
                    while (Reader.Read())
                    {
                        OtherSoftId = Reader["PK_LNG_METER_ID"].ToString().Trim();
                    }
                    Reader.Close();

                    SQLString = string.Format("SELECT * FROM " + "METER_RATES_CONTROL" + " WHERE FK_LNG_METER_ID='{0}' AND (AVR_PROJECT_NAME = '{1}' OR AVR_PROJECT_NAME = '{2}' )", OtherSoftId,"���ݵ�۽���","��ʱ���ʵ�۽���");

                    Cmd = new System.Data.OleDb.OleDbCommand(SQLString, OtherConn);

                    Reader = Cmd.ExecuteReader();

                    while (Reader.Read())
                    {
                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterOtherSoftData OtherSoft = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterOtherSoftData();
                        OtherSoft.Mosd_chrValue = Reader["AVR_DATAS"].ToString().Trim();
                        OtherSoft.AVR_CONCLUSION = Reader["AVR_CONCLUSION"].ToString().Trim();
                        strKey = Reader["AVR_PROJECT_NAME"].ToString().Trim();
                        if (InfoItem.MeterOtherSoftData.ContainsKey(strKey))
                            InfoItem.MeterOtherSoftData.Remove(strKey);
                        InfoItem.MeterOtherSoftData.Add(strKey, OtherSoft);
                    }
                    Reader.Close();

                }
            }

           
            #endregion

            #region Ǳ��������
            if (true == FlagOfTmp)
            {
                TableName = "METER_START_NO_LOAD";
            }
            else
            {
                TableName = "TMP_METER_START_NO_LOAD";
            }

            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterQdQids.Clear();

            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterQdQid Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterQdQid();
                Mse.AVR_ACTUAL_TIME = Reader["AVR_ACTUAL_TIME"].ToString();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                Mse.AVR_VOLTAGE = Reader["AVR_VOLTAGE"].ToString();
                Mse.DTM_BEGIN_TIME = Reader["DTM_BEGIN_TIME"].ToString();
                Mse.DTM_OVER_TIME = Reader["DTM_OVER_TIME"].ToString();
                Mse.Mqd_chrDL = Reader["AVR_CURRENT"].ToString();
                Mse.Mqd_chrJdfx = Reader["CHR_POWER_TYPE"].ToString();
                Mse.Mqd_chrJL = Reader["AVR_CONCLUSION"].ToString();
                Mse.Mqd_chrProjectName = Reader["AVR_PROJECT_NAME"].ToString();
                Mse.Mqd_chrProjectNo = Reader["AVR_PROJECT_NO"].ToString();
                Mse.Mqd_chrTime = Reader["AVR_ACTUAL_TIME"].ToString();
                Mse.AVR_ACTUAL_TIME = Reader["AVR_ACTUAL_TIME"].ToString();
                Mse.AVR_STANDARD_TIME = Reader["AVR_STANDARD_TIME"].ToString();
                strKey = Mse.Mqd_chrProjectNo.Trim();
                if(InfoItem.MeterQdQids.ContainsKey(strKey))
                    InfoItem.MeterQdQids.Remove(strKey);
                InfoItem.MeterQdQids.Add(strKey, Mse);
            }

            Reader.Close();
            #endregion

            #region ��������
            if (true == FlagOfTmp)
            {
                TableName = "METER_POWER_CONSUM_DATA";
            }
            else
            {
                TableName = "TMP_METER_POWER_CONSUM_DATA";
            }


            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterPowers.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterPower Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterPower();
                Mse.AVR_CUR_CIR_A_CUR = Reader["AVR_CUR_CIR_A_CUR"].ToString();
                Mse.AVR_CUR_CIR_A_VOT = Reader["AVR_CUR_CIR_A_VOT"].ToString();
                Mse.AVR_CUR_CIR_B_CUR = Reader["AVR_CUR_CIR_B_CUR"].ToString();
                Mse.AVR_CUR_CIR_B_VOT = Reader["AVR_CUR_CIR_B_VOT"].ToString();
                Mse.AVR_CUR_CIR_C_CUR = Reader["AVR_CUR_CIR_C_CUR"].ToString();
                Mse.AVR_CUR_CIR_C_VOT = Reader["AVR_CUR_CIR_C_VOT"].ToString();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                Mse.Md_PrjID = Reader["AVR_LIST_NO"].ToString();
                Mse.AVR_VOT_CIR_A_ANGLE = Reader["AVR_VOT_CIR_A_ANGLE"].ToString();
                Mse.AVR_VOT_CIR_A_CUR = Reader["AVR_VOT_CIR_A_CUR"].ToString();
                Mse.AVR_VOT_CIR_A_VOT = Reader["AVR_VOT_CIR_A_VOT"].ToString();
                Mse.AVR_VOT_CIR_B_ANGLE = Reader["AVR_VOT_CIR_B_ANGLE"].ToString();
                Mse.AVR_VOT_CIR_B_CUR = Reader["AVR_VOT_CIR_B_CUR"].ToString();
                Mse.AVR_VOT_CIR_B_VOT = Reader["AVR_VOT_CIR_B_VOT"].ToString();
                Mse.AVR_VOT_CIR_C_ANGLE = Reader["AVR_VOT_CIR_C_ANGLE"].ToString();
                Mse.AVR_VOT_CIR_C_CUR = Reader["AVR_VOT_CIR_C_CUR"].ToString();
                
                Mse.Md_Ia_ReactiveS = Reader["AVR_CUR_A_APPARENT_POWER"].ToString();
                Mse.Md_Ib_ReactiveS = Reader["AVR_CUR_B_APPARENT_POWER"].ToString();
                Mse.AVR_CUR_CIR_S_LIMIT = Reader["AVR_CUR_APPARENT_LIMIT"].ToString();
                Mse.Md_Ic_ReactiveS = Reader["AVR_CUR_C_APPARENT_POWER"].ToString();
                Mse.Mgh_chrISJL = Reader["AVR_CUR_APPARENT_CONCLUSION"].ToString();
                
                Mse.Md_Ua_ReactiveP = Reader["AVR_VOT_A_ACTIVE_POWER"].ToString();
                Mse.Md_Ub_ReactiveP = Reader["AVR_VOT_B_ACTIVE_POWER"].ToString();
                Mse.AVR_VOT_CIR_P_LIMIT = Reader["AVR_VOT_ACTIVE_LIMIT"].ToString();
                Mse.Md_Uc_ReactiveP = Reader["AVR_VOT_C_ACTIVE_POWER"].ToString();
                Mse.Mgh_chrUPJL = Reader["AVR_VOT_ACTIVE_CONCLUSION"].ToString();
                
                Mse.Md_Ua_ReactiveS = Reader["AVR_VOT_A_APPARENT_POWER"].ToString();
                Mse.Md_Ub_ReactiveS = Reader["AVR_VOT_B_APPARENT_POWER"].ToString();
                Mse.AVR_VOT_CIR_S_LIMIT = Reader["AVR_VOT_APPARENT_LIMIT"].ToString();
                Mse.Md_Uc_ReactiveS = Reader["AVR_VOT_C_APPARENT_POWER"].ToString();
                Mse.Mgh_chrUSJL = Reader["AVR_VOT_APPARENT_CONCLUSION"].ToString();
                

                int_ID++;
                strKey = "P_" + int_ID.ToString();
                if (InfoItem.MeterPowers.ContainsKey(strKey))
                    InfoItem.MeterPowers.Remove(strKey);
                InfoItem.MeterPowers.Add(strKey, Mse);
            }

            Reader.Close();
            #endregion

            #region �ز�485����
            if (true == FlagOfTmp)
            {
                TableName = "METER_CARRIER_WAVE";
            }
            else
            {
                TableName = "TMP_METER_CARRIER_WAVE";
            }

            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterCarrierDatas.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterCarrierData Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterCarrierData();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                Mse.AVR_LIMIT = Reader["AVR_LIMIT"].ToString();
                Mse.AVR_NUMBER_FAIL = Reader["AVR_NUMBER_FAIL"].ToString();
                Mse.AVR_NUMBER_SUCCEED = Reader["AVR_NUMBER_SUCCEED"].ToString();
                Mse.AVR_NUMBER_TOTAL = Reader["AVR_NUMBER_TOTAL"].ToString();
                Mse.AVR_RATIO_SUCCEED = Reader["AVR_RATIO_SUCCEED"].ToString();
                Mse.AVR_RESERVE = Reader["AVR_RESERVE"].ToString();
                Mse.DTM_END_TIME = Reader["DTM_END_TIME"].ToString();
                Mse.DTM_START_TIME = Reader["DTM_START_TIME"].ToString();
                Mse.Mce_ItemResult = Reader["AVR_CONCLUSION"].ToString();
                Mse.Mce_PrjID = Reader["AVR_PROJECT_NO"].ToString();
                Mse.Mce_PrjName = Reader["AVR_ITEM_NAME"].ToString();
                Mse.Mce_PrjValue = Reader["AVR_VALUES"].ToString();
               

                int_ID++;
                strKey = "P_" + int_ID.ToString();
                if (InfoItem.MeterCarrierDatas.ContainsKey(strKey))
                    InfoItem.MeterCarrierDatas.Remove(strKey);
                InfoItem.MeterCarrierDatas.Remove(strKey);
                InfoItem.MeterCarrierDatas.Add(strKey, Mse);
            }
            Reader.Close();
            #endregion

            #region �ѿ�����
            if (true == FlagOfTmp)
            {
                TableName = "METER_RATES_CONTROL";
            }
            else
            {
                TableName = "TMP_METER_RATES_CONTROL";
            }


            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();
            InfoItem.MeterCostControls.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFK Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFK();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                Mse.Mfk_chrData = Reader["AVR_DATAS"].ToString();
                Mse.Mfk_chrGrpType = Reader["AVR_GROUP_TYPE"].ToString();
                Mse.Mfk_chrItemType = Reader["AVR_ITEM_TYPE"].ToString().Trim();
                switch (Mse.Mfk_chrItemType)
                {
                    case "001":
                        Mse.Mcc_PrjName = "�����֤";
                        break;
                    case "002":
                        Mse.Mcc_PrjName = "Զ�̿���";
                        break;
                    case "003":
                        Mse.Mcc_PrjName = "��������";
                        break;
                    case "004":
                        Mse.Mcc_PrjName = "���繦��";
                        break;
                    case "005":
                        Mse.Mcc_PrjName = "������";
                        break;
                    case "006":
                        Mse.Mcc_PrjName = "Զ�̱���";
                        break;
                    case "007":
                        Mse.Mcc_PrjName = "���ݻس�";
                        break;
                    case "008":
                        Mse.Mcc_PrjName = "��������";
                        break;
                    case "013":
                        Mse.Mcc_PrjName = "��Կ����";
                        break;
                    case "014":
                        Mse.Mcc_PrjName = "��Կ�ָ�";
                        break;
                    case "015":
                        Mse.Mcc_PrjName = "���ƹ���";
                        break;
                }
                Mse.Mfk_chrJL = Reader["AVR_CONCLUSION"].ToString();                
                int_ID++;
                strKey = Mse.Mfk_chrItemType;
                if (InfoItem.MeterCostControls.ContainsKey(strKey))
                    InfoItem.MeterCostControls.Remove(strKey);
                InfoItem.MeterCostControls.Add(strKey, Mse);
            }
            Reader.Close();
            #endregion

            #region ���ܱ�������
            if (true == FlagOfTmp)
            {
                TableName = "METER_FUNCTION";
            }
            else
            {
                TableName = "TMP_METER_FUNCTION";
            }
            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterFunctions.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFunction Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFunction();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString().Trim();
                Mse.Mf_lngMyID = InfoItem._intMyId;
                Mse.Mf_PrjID = Reader["AVR_PROJECT_NO"].ToString().Trim();
                Mse.Mf_PrjName = Reader["AVR_PROJECT_NAME"].ToString().Trim();
                Mse.Mf_chrValue = Reader["AVR_VALUE"].ToString().Trim();
                Mse.Mf_Result = Reader["AVR_CONCLUSION"].ToString().Trim();                

                int_ID++;
                strKey = Mse.Mf_PrjID.Trim();
                if (InfoItem.MeterFunctions.ContainsKey(strKey))
                    InfoItem.MeterFunctions.Remove(strKey);
                InfoItem.MeterFunctions.Add(strKey, Mse);
            }
            Reader.Close();
            #endregion

            #region ����ʱ�ι���
            if (true == FlagOfTmp)
            {
                TableName = "METER_FUN_RATES_TIME_CONS";
            }
            else
            {
                TableName = "TMP_METER_FUN_RATES_TIME_CONS";
            }
            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterFLSDgns.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFLSDgn Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFLSDgn();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                Mse.Mfl_chrFdlDat = Reader["AVR_TCC_PEAK_DATA"].ToString();
                Mse.Mfl_chrFmcDat = Reader["AVR_TCCP_PEAK_DATA"].ToString();
                Mse.Mfl_chrGdlDat = Reader["AVR_TCC_VALLEY_DATA"].ToString();
                Mse.Mfl_chrGmcDat = Reader["AVR_TCCP_VALLEY_DATA"].ToString();
                Mse.Mfl_chrGrpType = Reader["AVR_GROUP_TYPE"].ToString();
                Mse.Mfl_chrItemJL = Reader["AVR_ITEM_CONCLUSION"].ToString();
                Mse.Mfl_chrJdlDat = Reader["AVR_TCC_SHARP_DATA"].ToString();
                Mse.Mfl_chrJjrBcJL = Reader["AVR_FT_PROGRAMMING_CONC"].ToString();
                Mse.Mfl_chrJjrDat = Reader["AVR_FT_PROGRAMMING_RECORD"].ToString();
                Mse.Mfl_chrJmcDat = Reader["AVR_TCCP_SHARP_DATA"].ToString();
                Mse.Mfl_chrListNo = Reader["AVR_LIST_NO"].ToString();
                Mse.Mfl_chrOther1 = Reader["AVR_OTHER_1"].ToString();
                Mse.Mfl_chrOther2 = Reader["AVR_OTHER_2"].ToString();
                Mse.Mfl_chrOther3 = Reader["AVR_OTHER_3"].ToString();
                Mse.Mfl_chrOther4 = Reader["AVR_OTHER_4"].ToString();
                Mse.Mfl_chrOther5 = Reader["AVR_OTHER_5"].ToString();
                Mse.Mfl_chrPdlDat = Reader["AVR_TCC_ORDINARY_DATA"].ToString();
                Mse.Mfl_chrPmcDat = Reader["AVR_TCCP_ORDINARY_DATA"].ToString();
                Mse.Mfl_chrProjectName = Reader["AVR_PROJECT_NAME"].ToString();
                Mse.Mfl_chrSdBcDat = Reader["AVR_TC_PROGRAMMING_RECORD"].ToString();
                Mse.Mfl_chrSdBcJL = Reader["AVR_TC_PROGRAMMING_CONC"].ToString();
                Mse.Mfl_chrSdDatJL = Reader["AVR_TC_DATJL"].ToString();
                Mse.Mfl_chrSdDlQhJL = Reader["AVR_TCC_CHANGE_CONC"].ToString();
                Mse.Mfl_chrSdMCQhJL = Reader["AVR_TCCP_CHANGE_CONC"].ToString();
                Mse.Mfl_chrSdRdat = Reader["AVR_TC_READ_DATA"].ToString();
                Mse.Mfl_chrSdWdat = Reader["AVR_TC_WRITE_DATA"].ToString();
                Mse.Mfl_chrSdYddjDat = Reader["AVR_TC_CONVT_FREEZE_DATA"].ToString();
                Mse.Mfl_chrSdYddjJL = Reader["AVR_TC_CONCT_FREEZE_CONC"].ToString();
                Mse.Mfl_chrSdZtzDat = Reader["AVR_TC_STATE_DATA"].ToString();
                Mse.Mfl_chrSdZtzJL = Reader["AVR_TC_CHANGE_CONC"].ToString();
                Mse.Mfl_chrSqBcDat = Reader["AVR_TC_CHANGE_CONC"].ToString();
                Mse.Mfl_chrSqBcJL = Reader["AVR_TZ_PROGRAMMING_CONC"].ToString();
                Mse.Mfl_chrSqDatJL = Reader["AVR_TZ_DAT_CONC"].ToString();
                Mse.Mfl_chrSqRdat = Reader["AVR_TZ_READ_VALUE"].ToString();
                Mse.Mfl_chrSqWdat = Reader["AVR_TZ_WRITE_VALUE"].ToString();
                Mse.Mfl_chrSqYddjDat = Reader["AVR_TZ_CONVT_FREEZE_DATA"].ToString();
                Mse.Mfl_chrSqYddjJL = Reader["AVR_TZ_CONVT_FREEZE_CONC"].ToString();
                Mse.Mfl_chrSqZtzDat = Reader["AVR_TZ_STATE_DATA"].ToString();
                Mse.Mfl_chrSqZtzJL = Reader["AVR_TZ_CHANGE_CONC"].ToString();
                Mse.Mfl_chrZxrBcJL = Reader["AVR_HD_PROGRAMMING_CONC"].ToString();
                Mse.Mfl_chrZxrDat = Reader["AVR_HD_PROGRAMMING_RECORD"].ToString();
                Mse.Mfl_intItemType = Reader["AVR_ITEM_TYPE"].ToString();
                
                int_ID++;
                strKey = Mse.Mfl_intItemType;
                if (InfoItem.MeterFLSDgns.ContainsKey(strKey))
                    InfoItem.MeterFLSDgns.Remove(strKey);
                InfoItem.MeterFLSDgns.Add(strKey, Mse);
            }
            Reader.Close();
            #endregion

            #region ��������
            if (true == FlagOfTmp)
            {
                TableName = "METER_FUN_ENERGY_MEASURE";
            }
            else
            {
                TableName = "TMP_METER_FUN_ENERGY_MEASURE";
            }


            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' order by AVR_LIST_NO", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();
            InfoItem.MeterJLgns.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterJLgn Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterJLgn();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString().Trim();
                Mse.AVR_PROJECT_NAME = Reader["AVR_PROJECT_NAME"].ToString().Trim();
                Mse.AVR_LIST_NO = Reader["AVR_LIST_NO"].ToString().Trim();
                Mse.AVR_ITEM_CONC = Reader["AVR_ITEM_CONC"].ToString().Trim();
                Mse.AVR_GROUP_TYPE = Reader["AVR_GROUP_TYPE"].ToString().Trim();
                Mse.AVR_RECORD_OTHER = Reader["AVR_RECORD_OTHER"].ToString().Trim();
                //Mse.Mjl_chrJsrBcJL = Reader["AVR_TCCP_VALLEY_DATA"].ToString();
                //Mse.Mjl_chrListNo = Reader["AVR_LIST_NO"].ToString();
                //Mse.Mjl_chrOther1 = Reader["AVR_OTHER_1"].ToString();
                //Mse.Mjl_chrOther2 = Reader["AVR_OTHER_2"].ToString();
                //Mse.Mjl_chrOther3 = Reader["AVR_OTHER_3"].ToString();
                //Mse.Mjl_chrOther4 = Reader["AVR_OTHER_4"].ToString();
                //Mse.Mjl_chrOther5 = Reader["AVR_OTHER_5"].ToString();
                //Mse.Mjl_chrPf = Reader["AVR_LIST_NO"].ToString();
                Mse.AVR_PROJECT_NAME = Reader["AVR_PROJECT_NAME"].ToString().Trim();
                Mse.AVR_ACTIVE_FORWARD_DATA = Reader["AVR_ACTIVE_FORWARD_DATA"].ToString();
                Mse.AVR_ACTIVE_GROUP_DATA = Reader["AVR_ACTIVE_GROUP_DATA"].ToString().Trim();
                Mse.AVR_ACTIVE_REVERSE_DATA = Reader["AVR_ACTIVE_REVERSE_DATA"].ToString();
                ////Mse.Mjl_chrPz = Reader["AVR_OTHER_2"].ToString();
                ////Mse.Mjl_chrTdJL = Reader["AVR_OTHER_3"].ToString();
                //Mse.Mjl_chrWgZhZ1 = Reader["AVR_REACTIVE_STATE_1_DATA"].ToString();
                //Mse.Mjl_chrWgZhz110JL = Reader["AVR_OTHER_5"].ToString();
                //Mse.Mjl_chrWgZhZ1JL = Reader["AVR_TCC_ORDINARY_DATA"].ToString();
                //Mse.Mjl_chrWgZhZ2 = Reader["AVR_TCCP_ORDINARY_DATA"].ToString();
                //Mse.Mjl_chrWgZhz210JL = Reader["AVR_PROJECT_NAME"].ToString();
                //Mse.Mjl_chrWgZhZ2JL = Reader["AVR_TC_PROGRAMMING_RECORD"].ToString();
                //Mse.Mjl_chrXX1 = Reader["AVR_TC_PROGRAMMING_CONC"].ToString();
                //Mse.Mjl_chrXX2 = Reader["AVR_TC_DATJL"].ToString();
                //Mse.Mjl_chrXX3 = Reader["AVR_TCC_CHANGE_CONC"].ToString();
                //Mse.Mjl_chrXX4 = Reader["AVR_TCCP_CHANGE_CONC"].ToString();
                //Mse.Mjl_chrYgZhZ = Reader["AVR_TC_READ_DATA"].ToString();
                //Mse.Mjl_chrYgZhz10JL = Reader["AVR_TC_WRITE_DATA"].ToString();
                //Mse.Mjl_chrYgZhzJL = Reader["AVR_TC_CONVT_FREEZE_DATA"].ToString();
                //Mse.Mjl_chrZcJL = Reader["AVR_TC_CONCT_FREEZE_CONC"].ToString();
                //Mse.Mjl_chrZhP = Reader["AVR_TC_STATE_DATA"].ToString();
                //Mse.Mjl_chrZhQ1 = Reader["AVR_TC_CHANGE_CONC"].ToString();
                //Mse.Mjl_chrZhQ2 = Reader["AVR_TC_CHANGE_CONC"].ToString();
                //Mse.Mjl_intItemType = Reader["AVR_TZ_PROGRAMMING_CONC"].ToString();

                strKey = Mse.AVR_LIST_NO;
                if (InfoItem.MeterJLgns.ContainsKey(strKey))
                    InfoItem.MeterJLgns.Remove(strKey);
                InfoItem.MeterJLgns.Add(strKey, Mse);
            }
            Reader.Close();
            #endregion

            #region ������ʾ����
            if (true == FlagOfTmp)
            {
                TableName = "METER_FUN_SHOW";
            }
            else
            {
                TableName = "TMP_METER_FUN_SHOW";
            }

            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterShows.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterShow Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterShow();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                //Mse.Mjl_chrFxZjJL = Reader["AVR_TCC_PEAK_DATA"].ToString();
                //Mse.Mjl_chrGrpType = Reader["AVR_GROUP_TYPE"].ToString();
                //Mse.Mjl_chrItemJL = Reader["AVR_ITEM_CONC"].ToString();
                ////Mse.Mjl_chrJsrBcJL = Reader["AVR_TCCP_VALLEY_DATA"].ToString();
                //Mse.Mjl_chrListNo = Reader["AVR_LIST_NO"].ToString();
                //Mse.Mjl_chrOther1 = Reader["AVR_OTHER_1"].ToString();
                //Mse.Mjl_chrOther2 = Reader["AVR_OTHER_2"].ToString();
                //Mse.Mjl_chrOther3 = Reader["AVR_OTHER_3"].ToString();
                //Mse.Mjl_chrOther4 = Reader["AVR_OTHER_4"].ToString();
                //Mse.Mjl_chrOther5 = Reader["AVR_OTHER_5"].ToString();
                ////Mse.Mjl_chrPf = Reader["AVR_LIST_NO"].ToString();
                //Mse.Mjl_chrProjectName = Reader["AVR_PROJECT_NAME"].ToString();
                ////Mse.Mjl_chrPz = Reader["AVR_OTHER_2"].ToString();
                ////Mse.Mjl_chrTdJL = Reader["AVR_OTHER_3"].ToString();
                //Mse.Mjl_chrWgZhZ1 = Reader["AVR_REACTIVE_STATE_1_DATA"].ToString();
                //Mse.Mjl_chrWgZhz110JL = Reader["AVR_OTHER_5"].ToString();
                //Mse.Mjl_chrWgZhZ1JL = Reader["AVR_TCC_ORDINARY_DATA"].ToString();
                //Mse.Mjl_chrWgZhZ2 = Reader["AVR_TCCP_ORDINARY_DATA"].ToString();
                //Mse.Mjl_chrWgZhz210JL = Reader["AVR_PROJECT_NAME"].ToString();
                //Mse.Mjl_chrWgZhZ2JL = Reader["AVR_TC_PROGRAMMING_RECORD"].ToString();
                //Mse.Mjl_chrXX1 = Reader["AVR_TC_PROGRAMMING_CONC"].ToString();
                //Mse.Mjl_chrXX2 = Reader["AVR_TC_DATJL"].ToString();
                //Mse.Mjl_chrXX3 = Reader["AVR_TCC_CHANGE_CONC"].ToString();
                //Mse.Mjl_chrXX4 = Reader["AVR_TCCP_CHANGE_CONC"].ToString();
                //Mse.Mjl_chrYgZhZ = Reader["AVR_TC_READ_DATA"].ToString();
                //Mse.Mjl_chrYgZhz10JL = Reader["AVR_TC_WRITE_DATA"].ToString();
                //Mse.Mjl_chrYgZhzJL = Reader["AVR_TC_CONVT_FREEZE_DATA"].ToString();
                //Mse.Mjl_chrZcJL = Reader["AVR_TC_CONCT_FREEZE_CONC"].ToString();
                //Mse.Mjl_chrZhP = Reader["AVR_TC_STATE_DATA"].ToString();
                //Mse.Mjl_chrZhQ1 = Reader["AVR_TC_CHANGE_CONC"].ToString();
                //Mse.Mjl_chrZhQ2 = Reader["AVR_TC_CHANGE_CONC"].ToString();
                //Mse.Mjl_intItemType = Reader["AVR_TZ_PROGRAMMING_CONC"].ToString();


                int_ID++;
                strKey = "P_" + int_ID.ToString();
                if (InfoItem.MeterShows.ContainsKey(strKey))
                    InfoItem.MeterShows.Remove(strKey);
                InfoItem.MeterShows.Add(strKey, Mse);
            }
            Reader.Close();
            #endregion

            #region ��������
            if (true == FlagOfTmp)
            {
                TableName = "METER_FUN_MAX_DEMAND";
            }
            else
            {
                TableName = "TMP_METER_FUN_MAX_DEMAND";
            }

            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterXLgns.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterXLgn Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterXLgn();

                Mse.AVR_PROJECT_NAME = Reader["AVR_PROJECT_NAME"].ToString().Trim();
                Mse.AVR_GRP_TYPE = Reader["AVR_GRP_TYPE"].ToString().Trim();
                Mse.AVR_LIST_NO = Reader["AVR_LIST_NO"].ToString().Trim();
                Mse.AVR_RECORD_OTHER = Reader["AVR_RECORD_OTHER"].ToString().Trim();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString().Trim();
                //Mse.Mjl_chrFxZjJL = Reader["AVR_TCC_PEAK_DATA"].ToString();
                //Mse.Mjl_chrGrpType = Reader["AVR_GROUP_TYPE"].ToString();
                //Mse.Mjl_chrItemJL = Reader["AVR_ITEM_CONC"].ToString();
                ////Mse.Mjl_chrJsrBcJL = Reader["AVR_TCCP_VALLEY_DATA"].ToString();
                //Mse.Mjl_chrListNo = Reader["AVR_LIST_NO"].ToString();
                //Mse.Mjl_chrOther1 = Reader["AVR_OTHER_1"].ToString();
                //Mse.Mjl_chrOther2 = Reader["AVR_OTHER_2"].ToString();
                //Mse.Mjl_chrOther3 = Reader["AVR_OTHER_3"].ToString();
                //Mse.Mjl_chrOther4 = Reader["AVR_OTHER_4"].ToString();
                //Mse.Mjl_chrOther5 = Reader["AVR_OTHER_5"].ToString();
                ////Mse.Mjl_chrPf = Reader["AVR_LIST_NO"].ToString();
                //Mse.Mjl_chrProjectName = Reader["AVR_PROJECT_NAME"].ToString();
                ////Mse.Mjl_chrPz = Reader["AVR_OTHER_2"].ToString();
                ////Mse.Mjl_chrTdJL = Reader["AVR_OTHER_3"].ToString();
                //Mse.Mjl_chrWgZhZ1 = Reader["AVR_REACTIVE_STATE_1_DATA"].ToString();
                //Mse.Mjl_chrWgZhz110JL = Reader["AVR_OTHER_5"].ToString();
                //Mse.Mjl_chrWgZhZ1JL = Reader["AVR_TCC_ORDINARY_DATA"].ToString();
                //Mse.Mjl_chrWgZhZ2 = Reader["AVR_TCCP_ORDINARY_DATA"].ToString();
                //Mse.Mjl_chrWgZhz210JL = Reader["AVR_PROJECT_NAME"].ToString();
                //Mse.Mjl_chrWgZhZ2JL = Reader["AVR_TC_PROGRAMMING_RECORD"].ToString();
                //Mse.Mjl_chrXX1 = Reader["AVR_TC_PROGRAMMING_CONC"].ToString();
                //Mse.Mjl_chrXX2 = Reader["AVR_TC_DATJL"].ToString();
                //Mse.Mjl_chrXX3 = Reader["AVR_TCC_CHANGE_CONC"].ToString();
                //Mse.Mjl_chrXX4 = Reader["AVR_TCCP_CHANGE_CONC"].ToString();
                //Mse.Mjl_chrYgZhZ = Reader["AVR_TC_READ_DATA"].ToString();
                //Mse.Mjl_chrYgZhz10JL = Reader["AVR_TC_WRITE_DATA"].ToString();
                //Mse.Mjl_chrYgZhzJL = Reader["AVR_TC_CONVT_FREEZE_DATA"].ToString();
                //Mse.Mjl_chrZcJL = Reader["AVR_TC_CONCT_FREEZE_CONC"].ToString();
                //Mse.Mjl_chrZhP = Reader["AVR_TC_STATE_DATA"].ToString();
                //Mse.Mjl_chrZhQ1 = Reader["AVR_TC_CHANGE_CONC"].ToString();
                //Mse.Mjl_chrZhQ2 = Reader["AVR_TC_CHANGE_CONC"].ToString();
                //Mse.Mjl_intItemType = Reader["AVR_TZ_PROGRAMMING_CONC"].ToString();


                strKey = Mse.AVR_LIST_NO;
                if (InfoItem.MeterXLgns.ContainsKey(strKey))
                    InfoItem.MeterXLgns.Remove(strKey);
                InfoItem.MeterXLgns.Add(strKey, Mse);
            }
            Reader.Close();
            #endregion

            #region �¼���¼
            if (true == FlagOfTmp)
            {
                TableName = "METER_FUN_EVENT_RECORD";
            }
            else
            {
                TableName = "TMP_METER_FUN_EVENT_RECORD";
            }


            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterSjJLgns.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSjJLgn Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSjJLgn();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString().Trim();
                //Mse.Mjl_chrFxZjJL = Reader["AVR_TCC_PEAK_DATA"].ToString();
                //Mse.Mjl_chrGrpType = Reader["AVR_GROUP_TYPE"].ToString();
                Mse.ItemConc = Reader["AVR_ITEM_CONC"].ToString().Trim();
                Mse.ItemName = Reader["AVR_ITEM_NAME"].ToString().Trim();
                Mse.GroupType = Reader["AVR_GROUP_TYPE"].ToString().Trim();
                Mse.ListNo = Reader["AVR_LIST_NO"].ToString().Trim();
                Mse.StatusNo = Reader["AVR_STATUS_NO"].ToString().Trim();
                Mse.RecordOther = Reader["AVR_RECORD_OTHER"].ToString().Trim();
                Mse.UseTime = Reader["AVR_USE_TIME"].ToString().Trim();
                Mse.SumTimes = Reader["AVR_SUM_TIMES"].ToString().Trim();
                Mse.RecordStartTime = Reader["AVR_RECORD_START_TIME"].ToString().Trim();
                ////Mse.Mjl_chrJsrBcJL = Reader["AVR_TCCP_VALLEY_DATA"].ToString();
                //Mse.Mjl_chrListNo = Reader["AVR_LIST_NO"].ToString();
                //Mse.Mjl_chrOther1 = Reader["AVR_OTHER_1"].ToString();
                //Mse.Mjl_chrOther2 = Reader["AVR_OTHER_2"].ToString();
                //Mse.Mjl_chrOther3 = Reader["AVR_OTHER_3"].ToString();
                //Mse.Mjl_chrOther4 = Reader["AVR_OTHER_4"].ToString();
                //Mse.Mjl_chrOther5 = Reader["AVR_OTHER_5"].ToString();
                ////Mse.Mjl_chrPf = Reader["AVR_LIST_NO"].ToString();
                //Mse.Mjl_chrProjectName = Reader["AVR_PROJECT_NAME"].ToString();
                ////Mse.Mjl_chrPz = Reader["AVR_OTHER_2"].ToString();
                ////Mse.Mjl_chrTdJL = Reader["AVR_OTHER_3"].ToString();
                //Mse.Mjl_chrWgZhZ1 = Reader["AVR_REACTIVE_STATE_1_DATA"].ToString();
                //Mse.Mjl_chrWgZhz110JL = Reader["AVR_OTHER_5"].ToString();
                //Mse.Mjl_chrWgZhZ1JL = Reader["AVR_TCC_ORDINARY_DATA"].ToString();
                //Mse.Mjl_chrWgZhZ2 = Reader["AVR_TCCP_ORDINARY_DATA"].ToString();
                //Mse.Mjl_chrWgZhz210JL = Reader["AVR_PROJECT_NAME"].ToString();
                //Mse.Mjl_chrWgZhZ2JL = Reader["AVR_TC_PROGRAMMING_RECORD"].ToString();
                //Mse.Mjl_chrXX1 = Reader["AVR_TC_PROGRAMMING_CONC"].ToString();
                //Mse.Mjl_chrXX2 = Reader["AVR_TC_DATJL"].ToString();
                //Mse.Mjl_chrXX3 = Reader["AVR_TCC_CHANGE_CONC"].ToString();
                //Mse.Mjl_chrXX4 = Reader["AVR_TCCP_CHANGE_CONC"].ToString();
                //Mse.Mjl_chrYgZhZ = Reader["AVR_TC_READ_DATA"].ToString();
                //Mse.Mjl_chrYgZhz10JL = Reader["AVR_TC_WRITE_DATA"].ToString();
                //Mse.Mjl_chrYgZhzJL = Reader["AVR_TC_CONVT_FREEZE_DATA"].ToString();
                //Mse.Mjl_chrZcJL = Reader["AVR_TC_CONCT_FREEZE_CONC"].ToString();
                //Mse.Mjl_chrZhP = Reader["AVR_TC_STATE_DATA"].ToString();
                //Mse.Mjl_chrZhQ1 = Reader["AVR_TC_CHANGE_CONC"].ToString();
                //Mse.Mjl_chrZhQ2 = Reader["AVR_TC_CHANGE_CONC"].ToString();
                //Mse.Mjl_intItemType = Reader["AVR_TZ_PROGRAMMING_CONC"].ToString();


                int_ID++;
                strKey = Mse.StatusNo.Trim();
                if (InfoItem.MeterSjJLgns.ContainsKey(strKey))
                    InfoItem.MeterSjJLgns.Remove(strKey);
                InfoItem.MeterSjJLgns.Add(strKey, Mse);
            }
            Reader.Close();
            #endregion

            #region ����
            if (true == FlagOfTmp)
            {
                TableName = "METER_FREEZE";
            }
            else
            {
                TableName = "TMP_METER_FREEZE";
            }


            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterFreezes.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFreeze Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFreeze();
                //    Mse._intMyId = Reader["FK_LNG_METER_ID"].ToString();
                //  Mse._intTaiNo = Reader["AVR_DEVICE_ID"].ToString();
                //Mse.Mjl_chrGrpType = Reader["AVR_GROUP_TYPE"].ToString();
                //  Mse._intBno = Reader["LNG_BENCH_POINT_NO"].ToString();
                Mse.Md_chrValue = Reader["AVR_RESULT_VALUE"].ToString();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                Mse.Md_PrjID = Reader["AVR_PROJECT_NO"].ToString();
                ////Mse.Mjl_chrJsrBcJL = Reader["AVR_TCCP_VALLEY_DATA"].ToString();
                //Mse.Mjl_chrListNo = Reader["AVR_LIST_NO"].ToString();
                //Mse.Mjl_chrOther1 = Reader["AVR_OTHER_1"].ToString();

                strKey = Mse.Md_PrjID;
                if (InfoItem.MeterFreezes.ContainsKey(strKey))
                    InfoItem.MeterFreezes.Remove(strKey);
                InfoItem.MeterFreezes.Add(strKey, Mse);
            }
            Reader.Close();
            #endregion

            #region ��Լһ��������
            if (true == FlagOfTmp)
            {
                TableName = "METER_STANDARD_DLT_DATA";
            }
            else
            {
                TableName = "TMP_METER_STANDARD_DLT_DATA";
            }


            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterDLTDatas.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterDLTData Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterDLTData();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                Mse.AVR_COMPARISON_VALUE = Reader["AVR_COMPARISON_VALUE"].ToString();
                Mse.AVR_CONC = Reader["AVR_CONC"].ToString();
                Mse.AVR_CONDITION = Reader["AVR_CONDITION"].ToString();
                Mse.AVR_DI_FORMAT = Reader["AVR_DI_FORMAT"].ToString();
                Mse.AVR_DI_LEN = Reader["AVR_DI_LEN"].ToString();
                Mse.AVR_DI_MSG = Reader["AVR_DI_MSG"].ToString();
                Mse.AVR_DI0_DI3 = Reader["AVR_DI0_DI3"].ToString();
                Mse.Mdlt_chrValue = Reader["AVR_VALUE"].ToString();
                Mse.Mdlt_intItemID = Reader["FK_LNG_ITEM_ID"].ToString();
               

                int_ID++;

                strKey = "P_" + int_ID.ToString();
                if (InfoItem.MeterDLTDatas.ContainsKey(strKey))
                    InfoItem.MeterDLTDatas.Remove(strKey);
                InfoItem.MeterDLTDatas.Add(strKey, Mse);
            }
            Reader.Close();
            #endregion

            #region һ������������
            if (true == FlagOfTmp)
            {
                TableName = "METER_CONSISTENCY_DATA";
            }
            else
            {
                TableName = "TMP_METER_CONSISTENCY_DATA";
            }




            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }
            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();

            InfoItem.MeterConsistencys.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterConsistency Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterConsistency();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                Mse.AVR_DATA_1_AVG = Reader["AVR_DATA_1_AVG"].ToString();
                Mse.AVR_DATA_1_ROUNDING = Reader["AVR_DATA_1_ROUNDING"].ToString();
                Mse.AVR_DATA_2_AVG = Reader["AVR_DATA_2_AVG"].ToString();
                Mse.AVR_DATA_2_ROUNDING = Reader["AVR_DATA_2_ROUNDING"].ToString();
                Mse.AVR_DATAS_1 = Reader["AVR_DATAS_1"].ToString();
                Mse.AVR_DATAS_2 = Reader["AVR_DATAS_2"].ToString();
                Mse.AVR_DIF_DATA_AVG = Reader["AVR_DIF_DATA_AVG"].ToString();
                Mse.AVR_DIF_DATA_ROUNDING = Reader["AVR_DIF_DATA_ROUNDING"].ToString();
                Mse.AVR_DIF_ERROR_LIMIT = Reader["AVR_DIF_ERROR_LIMIT"].ToString();
                Mse.AVR_PARAMETER = Reader["AVR_PARAMETER"].ToString();
                //Mse.Mc_chrData = Reader["FK_LNG_ITEM_ID"].ToString();
                //Mse.Mc_chrDataAvg = Reader["FK_LNG_ITEM_ID"].ToString();
                //Mse.Mc_chrDataInt = Reader["FK_LNG_ITEM_ID"].ToString();
                Mse.Mc_chrGrpType = Reader["AVR_GRP_TYPE"].ToString();
                Mse.Mc_chrItemType = Reader["AVR_ITEM_TYPE"].ToString();
                Mse.Mc_chrJL = Reader["AVR_CONC"].ToString();
                //Mse.Mc_intItemNo = (int)Reader["AVR_ITEM_NO"];
                //Mse.Mc_intSamplingType = Reader["FK_LNG_ITEM_ID"].ToString();

                strKey = Reader["AVR_ITEM_NO"].ToString() ;
                if (strKey.Length > 0)
                {
                    strKey = strKey.Trim();
                    strKey = strKey.Replace(" ", "");
                }
                else
                {
                    int_ID++;
                    strKey = "P_" + int_ID.ToString();
                }
                if (InfoItem.MeterConsistencys.ContainsKey(strKey))
                    InfoItem.MeterConsistencys.Remove(strKey);
                InfoItem.MeterConsistencys.Add(strKey, Mse);
            }
            Reader.Close();
            //����Ҫת��һ��,�������ݲ����ϴ�,���治����ʾ
            ConvertErrorAccords(InfoItem);
            #endregion

            #region ���ɼ�¼
            if (true == FlagOfTmp)
            {
                TableName = "METER_FUN_LOAD_RECORD";
            }
            else
            {
                TableName = "TMP_METER_FUN_LOAD_RECORD";
            }


            if (!IsServer)
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}'", InfoItem._intMyId);
            }
            else
            {
                SQLString = string.Format("SELECT * FROM " + TableName + " WHERE FK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", InfoItem._intMyId, InfoItem._intTaiNo);
            }

            Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            Reader = Cmd.ExecuteReader();
            InfoItem.MeterLoadRecords.Clear();
            int_ID = 0;
            while (Reader.Read())
            {
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterLoadRecord Mse = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterLoadRecord();
                Mse.AVR_DIS_REASON = Reader["AVR_DIS_REASON"].ToString();
                Mse.Ml_PrjName = Reader["AVR_PROJECT_NAME"].ToString();
                Mse.Ml_SubItemName = Reader["AVR_PROJECT_SUB_NAME"].ToString();
                Mse.Ml_PrjID = Reader["AVR_LIST_NO"].ToString();
                Mse.Ml_chrValue = Reader["AVR_RECORD_OTHER"].ToString();
                Mse.Ml_Result = Reader["AVR_RESULT_VALUE"].ToString();


                strKey = Mse.Ml_PrjID;
                if (InfoItem.MeterLoadRecords.ContainsKey(strKey))
                    InfoItem.MeterLoadRecords.Remove(strKey);
                InfoItem.MeterLoadRecords.Add(strKey, Mse);
            }
            Reader.Close();
            #endregion

            return InfoItem;

            //}
            //catch (Exception e)
            //{
            //    System.Windows.Forms.MessageBox.Show(e.ToString());
            //    return InfoItem;
            //}
        }

        /// <summary>
        /// �����ݿ�ȡ�������һ��������ת��һ��,���ڽ�����ʾ���ϴ�
        /// </summary>
        /// <param name="meterInfo"></param>
        private void ConvertErrorAccords(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo)
        {
            foreach (KeyValuePair<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterConsistency> pair in meterInfo.MeterConsistencys)
            {
                #region ת����MeterErrAccordBase
                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrAccordBase baseItem = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrAccordBase();
                    baseItem._intMyId =pair.Value._intMyId;
                baseItem._intTaiNo=pair.Value._intTaiNo;
                baseItem._intBno =pair.Value._intBno;
                baseItem.Sub_Item_ID =pair.Value.Mc_chrItemType;
                baseItem.Mea_PrjID=pair.Key;
                string[] arrayTemp=pair.Value.AVR_PARAMETER.Split(',');
                if(arrayTemp.Length==5)
                {
                    baseItem.Mea_PrjName=arrayTemp[0];
                    baseItem.Mea_xU=arrayTemp[1];
                    baseItem.Mea_xib=arrayTemp[2];
                    baseItem.Mea_Glys=arrayTemp[3];
                    int intTemp = 0;
                    int.TryParse(arrayTemp[4],out intTemp);
                    baseItem.Mea_Qs = intTemp;
                }
                baseItem.Mea_ItemResult = pair.Value.Mc_chrJL;
                baseItem.Mea_Wc1 =pair.Value.AVR_DATAS_1;
                baseItem.Mea_WcAver =pair.Value.AVR_DATA_2_AVG;
                baseItem.Mea_Wc2 =pair.Value.AVR_DATAS_2;
                baseItem.Mea_Wc =pair.Value.AVR_DIF_DATA_AVG;
                baseItem.Mea_WcLimit = pair.Value.AVR_DIF_ERROR_LIMIT;
                #endregion
                string accordType = "1";
                #region ��������
                string keyItem = pair.Key;
                char charType = keyItem[0];
                switch (charType)
                {
                    case '4':
                    //���һ����
                        accordType = "1";
                        break;
                    case '5':
                        //�����
                        accordType = "2";
                        break;
                    case '6':
                        //�������
                        accordType = "3";
                        break;
                    case '7':
                        //��������
                        accordType = "4";
                        break;
                    default:
                        continue;
                }
                #endregion

                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrAccord accordItem;
                if (!meterInfo.MeterErrAccords.ContainsKey(accordType))
                {
                    accordItem = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterErrAccord();
                    accordItem.FK_LNG_SCHEME_ID = pair.Value._intMyId;
                    accordItem._intBno = baseItem._intBno;
                    accordItem._intTaiNo = baseItem._intTaiNo;
                    accordItem.Mea_Result = CLDC_DataCore.Const.Variable.CTG_HeGe;
                    meterInfo.MeterErrAccords.Add(accordType,accordItem);
                }
                else
                {
                    accordItem = meterInfo.MeterErrAccords[accordType];
                }
                if (!accordItem.lstTestPoint.ContainsKey(pair.Key))
                {
                    accordItem.lstTestPoint.Add(pair.Key, baseItem);
                }
                else
                {
                    accordItem.lstTestPoint[pair.Key] = baseItem;
                }
                if (baseItem.Mea_ItemResult != null)
                {
                    accordItem.Mea_Result = baseItem.Mea_ItemResult.Trim() == CLDC_DataCore.Const.Variable.CTG_HeGe ? CLDC_DataCore.Const.Variable.CTG_HeGe : CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                }
                else
                {
                    accordItem.Mea_Result = CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                }
            }
        }
        #endregion

        #region ==================�����ݿ���ʱ�����==================


        /// <summary>
        /// ɾ����ʱ���е��������Ϣ
        /// </summary>
        /// <param name="MyId"></param>
        /// <param name="TaiId"></param>
        /// <param name="Mb_chrTxmh"></param>
        /// <returns></returns>
        public bool DeleteMeterInfoTemp(string Mb_chrTxmh)
        {
            if (!base.Connection) return false;

            string SqlString;

            if (!IsServer)
            {
                SqlString = string.Format("DELETE FROM TMP_METER_INFO WHERE Mb_Txmh={0}", Mb_chrTxmh);
            }
            else
            {
                SqlString = string.Format("DELETE FROM TMP_METER_INFO WHERE Mb_Txmh={0}", Mb_chrTxmh);
            }

            System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(SqlString, base._Con);

            int i = Cmd.ExecuteNonQuery();

            Cmd = null;

            if (i == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// ����ʱ���в�������
        /// </summary>
        /// <param name="Items">Ҫ����ĵ�����ݼ���</param>
        public void InsertMeterInfoTemp(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items)
        {
            if (!base.Connection) return;

            string SqlString;
            if (IsServer)
            {
                int i = 0;
                for (i = 0; i < Items.Count; i++)
                {
                    SqlString = string.Format("select AVR_BAR_CODE from  TMP_METER_INFO where AVR_BAR_CODE={0}", Items[i].Mb_ChrTxm);

                    System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(SqlString, base._Con);
                    System.Data.OleDb.OleDbDataReader dr = Cmd.ExecuteReader();
                    if (!dr.HasRows)
                    {
                        SqlString = string.Format("insert into TMP_METER_INFO(AVR_BAR_CODE,AVR_MADE_NO,AVR_ASSET_NO,AVR_TEST_TYPE,AVR_METER_TYPE,AVR_METER_MODEL,AVR_FACTORY," +
                            "AVR_IB,AVR_UB,AVR_FREQUENCY,AVR_AR_CONSTANT,AVR_AR_CLASS,AVR_SEAL_1,AVR_SEAL_2,AVR_WIRING_MODE) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}'," +
                            "'{11}','{12}','{13}','{14}','{15}')", Items[i].Mb_ChrTxm, Items[i].Mb_ChrCcbh, Items[i].Mb_ChrJlbh, Items[i].Mb_chrTestType,
                             Items[i].Mb_chrBlx, Items[i].Mb_Bxh, Items[i].Mb_chrzzcj, Items[i].Mb_chrIb, Items[i].Mb_chrUb,
                             Items[i].Mb_chrHz, Items[i].Mb_chrBcs, Items[i].Mb_chrBdj, Items[i].Mb_chrQianFeng1, Items[i].Mb_chrQianFeng2, Items[i].Mb_intClfs.ToString());

                    }
                    else
                    {
                        SqlString = string.Format("update TMP_METER_INFO set AVR_MADE_NO='{1}',AVR_ASSET_NO='{2}',AVR_TEST_TYPE='{3}',AVR_METER_TYPE='{4}',AVR_METER_MODEL='{5}',AVR_FACTORY='{6}'," +
                                                   "AVR_IB='{7}',AVR_UB='{8}',AVR_FREQUENCY='{9}',AVR_AR_CONSTANT='{10}',AVR_AR_CLASS='{11}',AVR_SEAL_1='{12}',AVR_SEAL_2='{13}',AVR_WIRING_MODE='{14 }' where AVR_BAR_CODE='{0}'", Items[i].Mb_ChrTxm, Items[i].Mb_ChrCcbh, Items[i].Mb_ChrJlbh, Items[i].Mb_chrTestType,
                                                    Items[i].Mb_chrBlx, Items[i].Mb_Bxh, Items[i].Mb_chrzzcj, Items[i].Mb_chrIb, Items[i].Mb_chrUb,
                                                    Items[i].Mb_chrHz, Items[i].Mb_chrBcs, Items[i].Mb_chrBdj, Items[i].Mb_chrQianFeng1, Items[i].Mb_chrQianFeng2, Items[i].Mb_intClfs.ToString());
                    }
                    Cmd = new System.Data.OleDb.OleDbCommand(SqlString, base._Con);
                    i = Cmd.ExecuteNonQuery();
                    Cmd = null;
                }
            }
        }

        /// <summary>
        /// ����ʱ���л�ȡһ��������Ϣ
        /// </summary>
        /// <param name="mb_txmh">������</param>
        /// <returns></returns>
        public CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo getBasicInfoTemp(string mb_txmh)
        {
            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item = null;
            
            if (IsServer)
            {
                try
                {
                    if (base._Con.State == System.Data.ConnectionState.Closed)
                        base._Con.Open();
                    string Sql = string.Format("select * from METER_INFO WHERE AVR_BAR_CODE={0}", mb_txmh);
                    System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(Sql, base._Con);
                    System.Data.OleDb.OleDbDataReader Reader = Cmd.ExecuteReader();

                    while (Reader.Read())
                    {
                        Item = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo();

                        Item.Mb_ChrJlbh = Reader["AVR_ASSET_NO"].ToString();  //�������
                        Item.Mb_ChrCcbh = Reader["AVR_MADE_NO"].ToString();      //�������
                        Item.Mb_ChrTxm = Reader["AVR_BAR_CODE"].ToString();        //�����
                        Item.Mb_chrAddr = Reader["AVR_ADDRESS"].ToString();       //ͨ�ŵ�ַ
                        Item.Mb_chrzzcj = Reader["AVR_FACTORY"].ToString();      //���쳧��
                        Item.Mb_Bxh = Reader["AVR_METER_MODEL"].ToString();           //���ͺ�
                        Item.Mb_chrBcs = Reader["AVR_AR_CONSTANT"].ToString();                 //����
                        Item.Mb_chrBlx = Reader["AVR_METER_TYPE"].ToString();                 //װ������
                        Item.Mb_chrBdj = Reader["AVR_AR_CLASS"].ToString();                 //��ȼ�
                        Item.Mb_chrHz = Reader["AVR_FREQUENCY"].ToString();       //Ƶ��
                        Item.Mb_chrIb = Reader["AVR_IB"].ToString();     //����
                        Item.Mb_chrUb = Reader["AVR_UB"].ToString();     //��ѹ

                        Item.Mb_intClfs = int.Parse(Reader["AVR_WIRING_MODE"].ToString());  //������ʽ
                        Item.Mb_chrAddr = Item.Mb_ChrCcbh;                //��ͨѶ��ַ���������Գ��������ΪͨѶ��ַ
                    }
                    Reader.Close();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message, "���ݿ��������", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            return Item;

        }

        #endregion

        /// <summary>
        /// ��ȡһ�������б���ʱ����
        /// </summary>
        /// <returns></returns>
        public List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> GetMeterListFromTempDB()
        {

            List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items = new List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo>();

            string SQLString = "SELECT * FROM TMP_METER_INFO order by LNG_BENCH_POINT_NO asc";// WHERE Mb_datJdrq=#2012-11-24 19:45:19#

            Items = this.getBasicInformation(SQLString);

            for (int i = 0; i < Items.Count; i++)
            {
                this.getDnbInfoNew(Items[i], false);
            }

            return Items;
        }

        #region �ϴ����������ϴ����±�־
        /// <summary>
        /// �ϴ�MIS�ӿڳɹ�����±��ر�־
        /// </summary>
        /// <param name="MyId"></param>
        /// <param name="TaiID"></param>
        public void UpdateToMisOk(long MyId, string TaiID)
        {
            if (!base.Connection) return;


            string SqlString;

            if (!IsServer)
            {
                SqlString = string.Format("UPDATE METER_INFO SET CHR_UPLOAD_FLAG='1' WHERE PK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", MyId, TaiID);
            }
            else
            {
                SqlString = string.Format("UPDATE METER_INFO SET CHR_UPLOAD_FLAG='1' WHERE PK_LNG_METER_ID='{0}' AND AVR_DEVICE_ID='{1}'", MyId, TaiID);
            }

            System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(SqlString, base._Con);

            Cmd.ExecuteNonQuery();

            Cmd = null;
            return;

        }
        
        /// <summary>
        /// ��ȡû���ϴ����������������б�
        /// </summary>
        /// <returns></returns>
        public List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> GetWaitUpToServer()
        {

            List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items = new List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo>();

            if (IsServer)
            {
                return Items;
            }

            string SQLString = "SELECT * FROM METER_INFO WHERE CHR_UPLOAD_FLAG=0";

            Items = this.getBasicInfo(SQLString, true);

            for (int i = 0; i < Items.Count; i++)
            {
                this.getDnbInfoNew(Items[i], true);
            }

            return Items;
        }

        public void UpdateToServer(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items, string Ip, string User, string Pwd)
        {
            UpdateToServer(Items, Ip, User, Pwd, "");
        }

        /// <summary>
        /// �����ϴ������������ϴ��ɹ���ͬʱ��Ҫ�޸ı�־
        /// </summary>
        /// <param name="Items"></param>
        public void UpdateToServer(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items, string Ip, string User, string Pwd,string taiID)
        {
            if (Items.Count == 0) return;

            clsDataManage ToServer = new clsDataManage(Ip, User, Pwd);

            if (!ToServer.Connection) return;

            for (int i = 0; i < Items.Count; i++)
            {
                System.Threading.Thread.Sleep(1);
                if (taiID != "") Items[i]._intTaiNo = taiID;
                if (ToServer.UpdateServer(Items[i]))
                {
                    this.UpDateServerIsOk(Items[i]._intMyId);
                }
            }

            ToServer.CloseDB();

            ToServer = null;
        }

        private void UpDateServerIsOk(long Myid)
        {
            string SQLString = "UPDATE METER_INFO SET CHR_UPLOAD_FLAG=1 WHERE PK_LNG_METER_ID=" + Myid.ToString();

            System.Data.OleDb.OleDbCommand Cmd = new System.Data.OleDb.OleDbCommand(SQLString, base._Con);

            if (Cmd.ExecuteNonQuery() < 1)
            {
                CLDC_DataCore.Const.GlobalUnit.Logger.Debug("���±��� �ϴ�����������ʶʧ�ܣ�" + Myid.ToString());
            }
            
            Cmd = null;
        }

        /// <summary>
        /// �ϴ����ݵ�������
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        private bool UpdateServer(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item)
        {
            return true;
            //TODO:����ʱ�⵼����ʽ�⣬�������ݿ���д����
            #region
            /*
            if (!IsServer)
            {
                return false;
            }

            List<string> _InsertDataBaseSQL = new List<string>();

            #region-----------������ܱ��������------------------

            string _TmpSql = "";
            _TmpSql = "Insert Into MeterBasicInfo values(" + Item._intMyId.ToString() + ",'"
                                                             + Item.Mb_TaiID + "',"
                                                             + Item.Mb_intBno.ToString() + ",'"
                                                             + Item.Mb_ChrJlbh + "','"
                                                             + Item.Mb_ChrCcbh + "','"
                                                             + Item.Mb_ChrTxm + "','"
                                                             + Item.Mb_chrAddr + "','"
                                                             + Item.Mb_chrzzcj + "','"
                                                             + Item.Mb_Bxh + "','"
                                                             + Item.Mb_chrBcs + "','"
                                                             + Item.Mb_chrBlx + "','"
                                                             + Item.Mb_chrBdj + "','"
                                                             + Item.Mb_gygy + "','"
                                                             + Item.Mb_chrCcrq + "','"
                                                             + Item.Mb_CHRSjdw + "','"
                                                             + Item.Mb_chrZsbh + "','"
                                                             + Item.Mb_ChrBmc + "',"
                                                             + Item.Mb_intClfs.ToString() + ",'"
                                                             + Item.Mb_chrUb + "','"
                                                             + Item.Mb_chrIb + "','"
                                                             + Item.Mb_chrHz + "',"
                                                             + (Item.Mb_BlnZnq == true ? "1" : "0") + ","
                                                             + (Item.Mb_BlnHgq == true ? "1" : "0") + ",'"
                                                             + Item.Mb_chrTestType + "',#"
                                                             + Item.Mb_DatJdrq + "#,#"
                                                             + Item.Mb_Datjjrq + "#,'"
                                                             + Item.Mb_chrWd + "','"
                                                             + Item.Mb_chrSd + "','"
                                                             + Item.Mb_chrResult + "','"
                                                             + Item.Mb_ChrJyy + "','"
                                                             + Item.Mb_ChrHyy + "','"
                                                             + Item.Mb_chrZhuGuan + "',"
                                                             + (Item.Mb_BlnToServer == true ? "1" : "0") + ","
                                                             + (Item.Mb_BlnToMis == true ? "1" : "0") + ",'"
                                                             + Item.Mb_chrQianFeng1 + "','"
                                                             + Item.Mb_chrQianFeng2 + "','"
                                                             + Item.Mb_chrQianFeng3 + "','"
                                                             + Item.Mb_chrOther1 + "','"
                                                             + Item.Mb_chrOther2 + "','"
                                                             + Item.Mb_chrOther3 + "','"
                                                             + Item.Mb_chrOther4 + "','"
                                                             + Item.Mb_chrOther5 + "')";
            _InsertDataBaseSQL.Add(_TmpSql);

            #endregion

            #region-------------������ܱ����������չ��-------------

            if (Item.MeterExtend.Count != 0)
            {
                foreach (string _Key in Item.MeterExtend.Keys)
                {
                    _TmpSql = string.Format("INSERT INTO MeterExtend Values({0},'{1}','{2}','{3}')"
                                            , Item._intMyId.ToString()                         //ΨһIDֵ
                                            , Item.Mb_TaiID               //̨���
                                            , _Key                          //�����־
                                            , Item.MeterExtend[_Key]);         //�����־ֵ

                    _InsertDataBaseSQL.Add(_TmpSql);
                }
            }



            #endregion

            #region---------����������ݱ�SQL���----------------
            if (Item.MeterResults.Count != 0)
            {
                foreach (string _Key in Item.MeterResults.Keys)
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterResult _Result = Item.MeterResults[_Key];
                    _Result.Mr_lngMyID = Item._intMyId;
                    _TmpSql = "Insert into MeterResult Values(" + _Result.Mr_lngMyID.ToString() + ",'"
                                                            + Item.Mb_TaiID + "','"
                                                            + _Result.Mr_PrjID + "','"
                                                            + _Result.Mr_PrjName + "','"
                                                            + _Result.Mr_Result + "','"
                                                            + _Result.Mr_Time + "','"
                                                            + _Result.Mr_Current + "')";


                    _InsertDataBaseSQL.Add(_TmpSql);
                    _Result = null;
                }
            }
            #endregion

            #region---------����������ݱ�SQL���------------
            if (Item.MeterErrors.Count != 0)
            {
                foreach (string _Key in Item.MeterErrors.Keys)
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError _Error = Item.MeterErrors[_Key];
                    _Error.Me_lngMyID = Item._intMyId;
                    _TmpSql = "Insert into MeterError(Me_LngMyID,Me_TaiID,Me_PrjId,Me_PrjName,Me_Result,Me_Glys,Me_xib,Me_xU,Me_WcLimit,Me_Qs,Me_Pl,Me_Wc)"
                                                    + "Values(" + _Error.Me_lngMyID.ToString() + ",'" + Item.Mb_TaiID + "','" + _Error.Me_PrjID + "','"
                                                    + _Error.Me_PrjName + "','" + _Error.Me_Result + "','"
                                                    + _Error.Me_Glys + "','" + _Error.Me_xib + "','"
                                                    + _Error.Me_xU + "','" + _Error.Me_WcLimit + "',"
                                                    + _Error.Me_Qs.ToString() + ",'" + _Error.Me_PL + "','"
                                                    + _Error.Me_Wc + "')";
                    _InsertDataBaseSQL.Add(_TmpSql);
                    _Error = null;
                }
            }
            #endregion

            #region---------����๦���������ݱ�SQL���----------
            if (Item.MeterDgns.Count != 0)
            {
                foreach (string _Key in Item.MeterDgns.Keys)
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterDgn _Dgn = Item.MeterDgns[_Key];
                    _Dgn.Md_lngMyID = Item._intMyId;
                    _TmpSql = "Insert into MeterDgn Values(" + _Dgn.Md_lngMyID.ToString() + ",'"
                                                         + Item.Mb_TaiID + "','"
                                                         + _Dgn.Md_PrjID + "','"
                                                         + _Dgn.Md_PrjName + "','"
                                                         + _Dgn.Md_chrValue + "')";
                    _InsertDataBaseSQL.Add(_TmpSql);
                    _Dgn = null;
                }
            }
            #endregion

            #region-------��������������ݱ�SQL���----------------------
            if (Item.MeterZZErrors.Count != 0)
            {
                foreach (string _Key in Item.MeterZZErrors.Keys)
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError _ZZError = Item.MeterZZErrors[_Key];
                    _ZZError.Mz_lngMyID = Item._intMyId;

                    _TmpSql = "Insert into MeterZZData(Mz_lngMyID,Mz_TaiID,Mz_PrjID,Mz_StartTime" +
                                                  ",Mz_xIb,Mz_Glys,Mz_Start,Mz_End" +
                                                  ",Mz_Diff,Mz_Wc,Mz_Result,Mz_U,Mz_i) values("
                                                            + _ZZError.Mz_lngMyID.ToString() + ",'"
                                                            + Item.Mb_TaiID + "','"
                                                            + _ZZError.Mz_PrjID + "','"
                                                            + _ZZError.Mz_StartTime + "','"
                                                            + _ZZError.Mz_xIb + "','"
                                                            + _ZZError.Mz_Glys + "','"
                                                            + _ZZError.Mz_Start.ToString() + "','"
                                                            + _ZZError.Mz_End.ToString() + "','"
                                                            + _ZZError.Mz_Diff + "','"
                                                            + _ZZError.Mz_Wc + "','"
                                                            + _ZZError.Mz_Result + "','"
                                                            + _ZZError.Mz_U + "','"
                                                            + _ZZError.Mz_i + "')";

                    _InsertDataBaseSQL.Add(_TmpSql);
                    _ZZError = null;
                }
            }
            #endregion

            #region---------����춨����--------------------

            if (Item.MeterSpecialErrs != null && Item.MeterSpecialErrs.Count != 0)
            {
                foreach (string _Key in Item.MeterSpecialErrs.Keys)
                {
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr _SpErr = Item.MeterSpecialErrs[_Key];
                    _SpErr.Mse_LngMyID = Item._intMyId;
                    _TmpSql = string.Format("Insert into MeterSpecialErr(Mse_lngMyID,Mse_TaiID,Mse_PrjName,Mse_Result," +
                                                        "Mse_Glfx,Mse_Ub,Mse_Ib,Mse_Phase,Mse_Pl," +
                                                        "Mse_Nxx,Mse_XieBo,Mse_WcLimit,Mse_Qs,Mse_Wc)" +
                                                        "VALUES({0},'{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}',{9},{10},'{11}',{12},'{13}')"
                                                        , _SpErr.Mse_LngMyID.ToString()
                                                        , Item.Mb_TaiID
                                                        , _SpErr.Mse_PrjName
                                                        , _SpErr.Mse_Result
                                                        , _SpErr.Mse_Glfx.ToString()
                                                        , _SpErr.Mse_Ub
                                                        , _SpErr.Mse_Ib
                                                        , _SpErr.Mse_Phase
                                                        , _SpErr.Mse_Pl
                                                        , _SpErr.Mse_Nxx.ToString()
                                                        , _SpErr.Mse_XieBo.ToString()
                                                        , _SpErr.Mse_WcLimit
                                                        , _SpErr.Mse_Qs.ToString()
                                                        , _SpErr.Mse_Wc);

                    _InsertDataBaseSQL.Add(_TmpSql);
                    _SpErr = null;
                }

            }


            #endregion

            string TmpErr = "";

            return base.SaveData(_InsertDataBaseSQL, out TmpErr);
            **/
            #endregion
        }
        
        #endregion

        /// <summary>
        /// �ر����ݿ�
        /// </summary>
        public void DbClose()
        {
            if (base.Connection)
            {
                //base._Con.Close();
                base.CloseDB();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public enum ScreenHeGeType
        {
            ȫ�� = 0,
            �ϸ� = 1,
            ���ϸ� = 2
        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        public enum ScreenType
        {
            �ͻ��˱�� = 0,
            �춨���� = 1,
            ������ = 2,
            ������� = 3,
            ������� = 4,
            ���쳧�� = 5,
            ���ͺ� = 6,
            ������ = 7,
            ���� = 8,
            ��ѹ = 9,
            ����Ա = 10,
            ����Ա = 11,
            �Ƿ����ϴ� = 12
        }

    }
}

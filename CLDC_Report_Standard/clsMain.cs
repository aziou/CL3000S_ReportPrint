using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace CLReport_Standard
{
    internal static class clsMain
    {
        [DllImport("kernel32")]
        private extern static int GetPrivateProfileStringA(string segName, string keyName, string sDefault, StringBuilder retValue, int nSize, string fileName);
        [DllImport("kernel32")]
        private extern static int WritePrivateProfileStringA(string segName, string keyName, string sValue, string fileName);

        #region------------��ȡ�����ļ�����---------------------------
        /// <summary>
        /// ��ȡini�����ļ����ݣ�Ĭ��·��������Ŀ¼��Reportinfo.ini��Ĭ�����ݿ�
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public static string getIniString(string sSection, string skey)
        {
            return getIniString(sSection, skey, "");
        }
        /// <summary>
        /// ��ȡINI�����ļ����ݣ�Ĭ��·��������Ŀ¼��Reportinfo.ini
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="skey"></param>
        /// <param name="sDefault"></param>
        /// <returns></returns>
        public static string getIniString(string sSection, string skey, string sDefault)
        {
            return getIniString(sSection, skey, sDefault, getFilePath("Reportinfo.ini"));
        }
        /// <summary>
        /// ��ȡINI�����ļ�����
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
        /// д�����ļ�����,Ĭ�������ļ�·��������Ŀ¼��Reportinfo.ini
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="sKey"></param>
        /// <param name="sValue"></param>
        public static void WriteIni(string sSection, string sKey, string sValue)
        {
            WriteIni(sSection, sKey, sValue, System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
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
        /// �������·����ȡ����·��
        /// </summary>
        /// <param name="filepath">���·��</param>
        /// <returns></returns>
        public static string getFilePath(string filepath)
        {
            string tmp = System.Reflection.Assembly.GetExecutingAssembly().Location;

            tmp = tmp.Substring(0, tmp.LastIndexOf('\\'));
            if (filepath == "") return tmp;

            return string.Format(@"{0}\{1}", tmp, filepath);

        }
        /// <summary>
        /// ��ȡ��������������Ϣ
        /// </summary>
        /// <param name="keyValue">�����ĿID</param>
        /// <returns></returns>
        public static string getErrorName(string keyValue)
        {
            string yuanjian = ((CLDC_Comm.Enum.Cus_PowerYuanJian)int.Parse(keyValue[2].ToString())).ToString();

            if (yuanjian.ToLower() == "h")
            {
                yuanjian = "��Ԫ";
            }
            else
            {
                yuanjian += "Ԫ";
            }

            return string.Format("{0}{1}", (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(keyValue[1].ToString()), yuanjian); 

        }


        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="ErrorNum">�����</param>
        /// <param name="Description">��������</param>
        /// <param name="RowNum">������</param>
        public static void WriteReportErr(long ErrorNum, string Description)
        {
            if (!System.IO.Directory.Exists(getFilePath("ReportErr")))
            {
                System.IO.Directory.CreateDirectory(getFilePath("ReportErr"));
            }
            System.IO.StreamWriter Writer = new System.IO.StreamWriter(string.Format(@"{0}\Err_{1}.Log", getFilePath("ReportErr"), DateTime.Now.ToString("yyyyMMddhhmmhh")), true, System.Text.Encoding.Default);
            Writer.Write(string.Format("�������ڣ�{0}\r\n����ţ�{1}\r\n����������\r\n{2}\r\n"
                                                        , DateTime.Now.ToLongDateString()
                                                        , ErrorNum
                                                        , Description));
            Writer.Close();

        }

        public static void WriteRptConfigIni()
        {
            System.IO.StreamWriter FileItem = new System.IO.StreamWriter(getFilePath("ReportConfig.ini"), true, System.Text.Encoding.Default);
            #region ----------------������Ϣ-------------------------
            FileItem.WriteLine("[CL3000SH]");
            FileItem.WriteLine("Mb_lngMyID=MyID");
            FileItem.WriteLine(@"//��Ψһ���");
            FileItem.WriteLine("Mb_intBno=Position");
            FileItem.WriteLine(@"//��λ��");
            FileItem.WriteLine("Mb_ChrJlbh=MeterID");
            FileItem.WriteLine(@"//�������");
            FileItem.WriteLine("Mb_ChrCcbh=ProductID");
            FileItem.WriteLine(@"//�������");
            FileItem.WriteLine("Mb_ChrTxm=Txm");
            FileItem.WriteLine(@"//�����");
            FileItem.WriteLine("Mb_chrAddr=MeterAdr");
            FileItem.WriteLine(@"//��ͨ�ŵ�ַ");
            FileItem.WriteLine("Mb_chrzzcj=Factory");
            FileItem.WriteLine(@"//���쳧��");
            FileItem.WriteLine("Mb_chrBxh=Size");
            FileItem.WriteLine(@"//���ͺ�");
            FileItem.WriteLine("Mb_chrBcs=MeterConst");
            FileItem.WriteLine(@"//����");
            FileItem.WriteLine("Mb_chrBlx=MeterType");
            FileItem.WriteLine(@"//������");
            FileItem.WriteLine("Mb_chrBdj=MeterLevel");
            FileItem.WriteLine(@"//��ȼ�");
            FileItem.WriteLine("Mb_chrCcrq=ProductDate");
            FileItem.WriteLine(@"//��������");
            FileItem.WriteLine("Mb_chrSjdw=Owner");
            FileItem.WriteLine(@"//�ͼ쵥λ");
            FileItem.WriteLine("Mb_chrZsbh=CertificateNo");
            FileItem.WriteLine(@"//֤����");
            FileItem.WriteLine("Mb_ChrBmc=MeterName");
            FileItem.WriteLine(@"//������");
            FileItem.WriteLine("Mb_intClfs=CheckType");
            FileItem.WriteLine(@"//������ʽ");
            FileItem.WriteLine("Mb_chrUb=U");
            FileItem.WriteLine(@"//��ѹ");
            FileItem.WriteLine("Mb_chrIb=I");
            FileItem.WriteLine(@"//����");
            FileItem.WriteLine("Mb_chrHz=Pl");
            FileItem.WriteLine(@"//Ƶ��");
            FileItem.WriteLine("Mb_BlnZnq=ZNQ");
            FileItem.WriteLine(@"//ֹ����");
            FileItem.WriteLine("Mb_BlnHgq=HGQ");
            FileItem.WriteLine(@"//������");
            FileItem.WriteLine("Mb_chrTestType=TestType");
            FileItem.WriteLine(@"//��������");
            FileItem.WriteLine("Mb_DatJdrq=CheckDate");
            FileItem.WriteLine(@"//�춨����");
            FileItem.WriteLine("Mb_Datjjrq=JJDate");
            FileItem.WriteLine(@"//�Ƽ�����");
            FileItem.WriteLine("Mb_chrWd=Temperature");
            FileItem.WriteLine(@"//�¶�");
            FileItem.WriteLine("Mb_chrSd=Humidity");
            FileItem.WriteLine(@"//ʪ��");
            FileItem.WriteLine("Mb_chrResult=Result");
            FileItem.WriteLine(@"//����");
            FileItem.WriteLine("Mb_ChrJyy=Checker");
            FileItem.WriteLine(@"//����Ա");
            FileItem.WriteLine("Mb_ChrHyy=Verificationer");
            FileItem.WriteLine(@"//����Ա");
            FileItem.WriteLine("Mb_chrZhuGuan=Charge");
            FileItem.WriteLine(@"//����");
            FileItem.WriteLine("Mb_BlnToServer=ToServer");
            FileItem.WriteLine(@"//�Ƿ��ϴ���������");
            FileItem.WriteLine("Mb_BlnToMis=ToMis");
            FileItem.WriteLine(@"//�Ƿ����ϴ���Ӫ��");
            FileItem.WriteLine("Mb_CrQianFeng1=Qf1");
            FileItem.WriteLine(@"//Ǧ��һ");
            FileItem.WriteLine("Mb_CrQianFeng2=Qf2");
            FileItem.WriteLine(@"//Ǧ���");
            FileItem.WriteLine("Mb_CrQianFeng3=Qf3");
            FileItem.WriteLine(@"//Ǧ����");
            FileItem.WriteLine("Mb_chrOther1=other1");
            FileItem.WriteLine(@"//ͨ��Э������");
            FileItem.WriteLine("Mb_chrOther2=other2");
            FileItem.WriteLine(@"//�������");
            FileItem.WriteLine("Mb_chrOther4=other4");
            FileItem.WriteLine(@"//�����׼");
            FileItem.WriteLine("Mb_chrOther5=other5");
            FileItem.WriteLine(@"//�춨����");
            #endregion
            #region ---------------------������Ϣ-----------------------
            FileItem.WriteLine();
            FileItem.WriteLine("[CL3000SH_Result]");
            FileItem.WriteLine("R_100=ZGZJ");
            FileItem.WriteLine(@"//��ۼ������");
            FileItem.WriteLine("R_101=Tdjc");
            FileItem.WriteLine(@"//ͨ����");
            FileItem.WriteLine("R_102=GPNY");
            FileItem.WriteLine(@"//��Ƶ��ѹ����");
            FileItem.WriteLine("R_103=BasicError");
            FileItem.WriteLine(@"//�������");
            FileItem.WriteLine("R_1031=BP+");
            FileItem.WriteLine(@"//�����й��������");
            FileItem.WriteLine("R_1032=BP-");
            FileItem.WriteLine(@"//�����й��������");
            FileItem.WriteLine("R_1033=BQ+");
            FileItem.WriteLine(@"//�����޹��������");
            FileItem.WriteLine("R_1034=BQ-");
            FileItem.WriteLine(@"//�����޹��������");
            FileItem.WriteLine("R_104=Standard");
            FileItem.WriteLine(@"//��׼ƫ��");
            FileItem.WriteLine("R_1041=SP+");
            FileItem.WriteLine(@"//�����й���׼ƫ��");
            FileItem.WriteLine("R_1042=SP-");
            FileItem.WriteLine(@"//�����й���׼ƫ��");
            FileItem.WriteLine("R_1043=SQ+");
            FileItem.WriteLine(@"//�����޹���׼ƫ��");
            FileItem.WriteLine("R_1044=SQ-");
            FileItem.WriteLine(@"//�����޹���׼ƫ��");
            FileItem.WriteLine("R_105=MaxWarp");
            FileItem.WriteLine(@"//���ƫ��");
            FileItem.WriteLine("R_106=RunTest");
            FileItem.WriteLine(@"//��������");
            FileItem.WriteLine("R_1061=ZP+");
            FileItem.WriteLine(@"//�����й���������");
            FileItem.WriteLine("R_1062=ZP-");
            FileItem.WriteLine(@"//�����й���������");
            FileItem.WriteLine("R_1063=ZQ+");
            FileItem.WriteLine(@"//�����޹���������");
            FileItem.WriteLine("R_1064=ZQ-");
            FileItem.WriteLine(@"//�����޹���������");
            FileItem.WriteLine("R_107=Multifunction");
            FileItem.WriteLine(@"//�๦������");
            FileItem.WriteLine("R_108=SpecilCheck");
            FileItem.WriteLine(@"//����춨����");
            FileItem.WriteLine("R_109=Start:StartDate:StartI");
            FileItem.WriteLine(@"//������");
            FileItem.WriteLine("R_1091=Start_P+:StartDate_P+:StartI_P+");
            FileItem.WriteLine(@"//�����й���");
            FileItem.WriteLine("R_1092=Start_P-:StartDate_P-:StartI_P-");
            FileItem.WriteLine(@"//�����й���");
            FileItem.WriteLine("R_1093=Start_Q+:StartDate_Q+:StartI_Q+");
            FileItem.WriteLine(@"//�����޹���");
            FileItem.WriteLine("R_1094=Start_Q-:StartDate_Q-:StartI_Q-");
            FileItem.WriteLine(@"//�����޹���");
            FileItem.WriteLine("R_110=creeping");
            FileItem.WriteLine(@"//Ǳ������");
            FileItem.WriteLine("R_1101=creeping_P+");
            FileItem.WriteLine(@"//�����й�Ǳ��");
            FileItem.WriteLine("R_1102=creeping_P-");
            FileItem.WriteLine(@"//�����й�Ǳ��");
            FileItem.WriteLine("R_1103=creeping_Q+");
            FileItem.WriteLine(@"//�����޹�Ǳ��");
            FileItem.WriteLine("R_1104=creeping_Q-");
            FileItem.WriteLine(@"//�����޹�Ǳ��");
            #endregion
            #region --------------------�๦����Ϣ----------------------
            FileItem.WriteLine();
            FileItem.WriteLine();
            FileItem.WriteLine("[CL3000SH_DGN]");
            FileItem.WriteLine("D_001=ͨ�Ų���");
            FileItem.WriteLine("D_002=�ռ�ʱ���");
            FileItem.WriteLine("D_00201=�ռ�ʱ���ֵ");
            FileItem.WriteLine("D_00202=��ʱ���1,��ʱ���2,��ʱ���3,��ʱ���4,��ʱ���5");
            FileItem.WriteLine("D_00203=��ʱ���6,��ʱ���7,��ʱ���8,��ʱ���9,��ʱ���10");
            FileItem.WriteLine("D_003=ʱ��Ͷ�����");
            FileItem.WriteLine("D_00301=ʱ��1��׼,ʱ��1ʵ��,ʱ��1���,��1");
            FileItem.WriteLine("D_00302=ʱ��2��׼,ʱ��2ʵ��,ʱ��2���,��2");
            FileItem.WriteLine("D_00303=ʱ��3��׼,ʱ��3ʵ��,ʱ��3���,��3");
            FileItem.WriteLine("D_00304=ʱ��4��׼,ʱ��4ʵ��,ʱ��4���,��4");
            FileItem.WriteLine("D_00305=ʱ��5��׼,ʱ��5ʵ��,ʱ��5���,��5");
            FileItem.WriteLine("D_00306=ʱ��6��׼,ʱ��6ʵ��,ʱ��6���,��6");
            FileItem.WriteLine("D_00307=ʱ��7��׼,ʱ��7ʵ��,ʱ��7���,��7");
            FileItem.WriteLine("D_00308=ʱ��8��׼,ʱ��8ʵ��,ʱ��8���,��8");
            FileItem.WriteLine("D_004=GPS��ʱ");
            FileItem.WriteLine("D_005=�����л�");
            FileItem.WriteLine("D_006=�����Ĵ���");
            FileItem.WriteLine("D_007=�����Ĵ���");
            FileItem.WriteLine("D_008=˲ʱ�Ĵ���");
            FileItem.WriteLine("D_009=״̬�Ĵ������");
            FileItem.WriteLine("D_010=ʧѹ�Ĵ���");
            FileItem.WriteLine("D_011=�¼���¼");
            FileItem.WriteLine("D_012=��������");
            FileItem.WriteLine("D_013=��ѹ�仯");
            FileItem.WriteLine("D_014=��ѹ��������");
            FileItem.WriteLine("D_015=����ʱ��");
            FileItem.WriteLine("D_01501=��ǰʱ��,��׼ʱ��,GPS��ʱ��ֵ");
            FileItem.WriteLine("D_016=�������01Ib");
            FileItem.WriteLine("D_01602=��������,�������,�����������");
            FileItem.WriteLine("D_0161=01Ib�������P+");
            FileItem.WriteLine("D_01611=01IB�������P+,01Ibʵ������P+,01IB���P+");
            FileItem.WriteLine("D_0162=01Ib�������P-");
            FileItem.WriteLine("D_01621=01IB�������P-,01Ibʵ������P-,01IB���P-");
            FileItem.WriteLine("D_0163=01Ib�������Q+");
            FileItem.WriteLine("D_01631=01IB�������Q+,01Ibʵ������Q+,01IB���Q+");
            FileItem.WriteLine("D_0164=01Ib�������Q-");
            FileItem.WriteLine("D_01641=01IB�������Q-,01Ibʵ������Q-,01IB���Q-");
            FileItem.WriteLine("D_017=�������10Ib");
            FileItem.WriteLine("D_01702=��������,�������,�����������");
            FileItem.WriteLine("D_0171=10Ib�������P+");
            FileItem.WriteLine("D_01711=10IB�������P+,10Ibʵ������P+,10IB���P+");
            FileItem.WriteLine("D_0172=10Ib�������P-");
            FileItem.WriteLine("D_01721=10IB�������P-,10Ibʵ������P-,10IB���P-");
            FileItem.WriteLine("D_0173=10Ib�������Q+");
            FileItem.WriteLine("D_01731=10IB�������Q+,10Ibʵ������Q+,10IB���Q+");
            FileItem.WriteLine("D_0174=10Ib�������Q-");
            FileItem.WriteLine("D_01741=10IB�������Q-,10Ibʵ������Q-,10IB���Q-");
            FileItem.WriteLine("D_018=�������Imax");
            FileItem.WriteLine("D_01802=��������,�������,�����������");
            FileItem.WriteLine("D_0181=Imax�������P+");
            FileItem.WriteLine("D_01811=Imax�������P+,Imaxʵ������P+,Imax���P+");
            FileItem.WriteLine("D_0182=Imax�������P-");
            FileItem.WriteLine("D_01821=Imax�������P-,Imaxʵ������P-,Imax���P-");
            FileItem.WriteLine("D_0183=Imax�������Q+");
            FileItem.WriteLine("D_01831=Imax�������Q+,Imaxʵ������Q+,Imax���Q+");
            FileItem.WriteLine("D_0184=Imax�������Q-");
            FileItem.WriteLine("D_01841=Imax�������Q-,Imaxʵ������Q-,Imax���Q-");
            FileItem.WriteLine("D_019=��ȡ����");
            FileItem.WriteLine("D_01901=�׶�P+����,�׶�P+���,�׶�P+��ƽ,�׶�P+���,�׶�P+���");
            FileItem.WriteLine("D_01902=�׶�P-����,�׶�P-���,�׶�P-��ƽ,�׶�P-���,�׶�P-���");
            FileItem.WriteLine("D_01903=�׶�Q+����,�׶�Q+���,�׶�Q+��ƽ,�׶�Q+���,�׶�Q+���");
            FileItem.WriteLine("D_01904=�׶�Q-����,�׶�Q-���,�׶�Q-��ƽ,�׶�Q-���,�׶�Q-���");
            FileItem.WriteLine("D_01905=�׶�Q1����,�׶�Q1���,�׶�Q1��ƽ,�׶�Q1���,�׶�Q1���");
            FileItem.WriteLine("D_01906=�׶�Q2����,�׶�Q2���,�׶�Q2��ƽ,�׶�Q2���,�׶�Q2���");
            FileItem.WriteLine("D_01907=�׶�Q3����,�׶�Q3���,�׶�Q3��ƽ,�׶�Q3���,�׶�Q3���");
            FileItem.WriteLine("D_01908=�׶�Q4����,�׶�Q4���,�׶�Q4��ƽ,�׶�Q4���,�׶�Q4���");
            FileItem.WriteLine("D_01909=�޹���,һ�����޹���,�������޹���,�������޹���,�������޹���");
            FileItem.WriteLine("D_020=���ƹ���");
            FileItem.WriteLine("D_021=͸֧����");
            FileItem.WriteLine("D_022=��������");
            FileItem.WriteLine("D_023=���ӹ���");
            FileItem.WriteLine("D_024=�޹�����");
            FileItem.WriteLine("D_025=��α����");
            FileItem.WriteLine("D_026=��д����");
            FileItem.WriteLine("D_027=���Ź���");
            FileItem.WriteLine("D_028=���Ṧ��");
            FileItem.WriteLine("D_029=���ر���");
            FileItem.WriteLine("D_030=���书��");
            FileItem.WriteLine("D_031=��������");
            FileItem.WriteLine("D_032=У�Ե���");
            FileItem.WriteLine("D_033=У������");
            FileItem.WriteLine("D_034=����״̬");
            FileItem.WriteLine("D_035=Ԥ���Ѽ��");
            #endregion

            #region -------------------���ܱ�����-------------------------
            FileItem.WriteLine();
            FileItem.WriteLine();
            FileItem.WriteLine("[CL3000SH_GN]");
            FileItem.WriteLine("D_001=��������");
            FileItem.WriteLine("D_0010000=��ǰ����й�����");
            FileItem.WriteLine("D_0010100=��ǰ�����й�����");
            FileItem.WriteLine("D_0010200=��ǰ�����й�����");
            FileItem.WriteLine("D_0010300=��ǰ�����޹�����");
            FileItem.WriteLine("D_0010400=��ǰ�����޹�����");
            FileItem.WriteLine("D_0010500=��ǰ�޹�һ���޵���");
            FileItem.WriteLine("D_0010600=��ǰ�޹������޵���");
            FileItem.WriteLine("D_0010700=��ǰ�޹������޵���");
            FileItem.WriteLine("D_0010800=��ǰ�޹������޵���");
            FileItem.WriteLine("D_0010001=��1������й�����");
            FileItem.WriteLine("D_0010101=��1�������й�����");
            FileItem.WriteLine("D_0010201=��1�η����й�����");
            FileItem.WriteLine("D_0010301=��1�������޹�����");
            FileItem.WriteLine("D_0010401=��1�η����޹�����");
            FileItem.WriteLine("D_0010501=��1���޹�һ���޵���");
            FileItem.WriteLine("D_0010601=��1���޹������޵���");
            FileItem.WriteLine("D_0010701=��1���޹������޵���");
            FileItem.WriteLine("D_0010801=��1���޹������޵���");

            FileItem.WriteLine("D_002=��ʱ����");
            FileItem.WriteLine("D_00201=��ʱ����_23��55��ǰ�㲥Уʱ");
            FileItem.WriteLine("D_00202=��ʱ����_23��57�ֹ㲥Уʱ");
            FileItem.WriteLine("D_00203=��ʱ����_00��01�ֹ㲥Уʱ");
            FileItem.WriteLine("D_00204=��ʱ����_00��07�ֹ㲥Уʱ");
            FileItem.WriteLine("D_00205=��ʱ����_С��5�ֹ㲥Уʱ");
            FileItem.WriteLine("D_00206=��ʱ����_����5�ֹ㲥Уʱ");
            FileItem.WriteLine("D_00207=��ʱ����_һ��һ�ι㲥Уʱ");
            FileItem.WriteLine("D_00208=��ʱ����_һ���ظ��㲥Уʱ");


            FileItem.WriteLine("D_004=����ʱ�ι���");
            FileItem.WriteLine("D_00405=����ʱ�ι���_ת��ǰ��Ϣ");
            FileItem.WriteLine("D_00406=����ʱ�ι���_ת������Ϣ");
            FileItem.WriteLine("D_00407=����ʱ�ι���_�ָ�����Ϣ");

            FileItem.WriteLine("D_005=�����������");
            FileItem.WriteLine("D_00501=�����������_��������");
            FileItem.WriteLine("D_00502=�����������_������");
            FileItem.WriteLine("D_00503=�����������_Ͷ������");
            FileItem.WriteLine("D_00504=�����������_��������");

            FileItem.WriteLine("D_006=��������");
            FileItem.WriteLine("D_0060000=��ǰ����й�����");
            FileItem.WriteLine("D_0060100=��ǰ�����й�����");
            FileItem.WriteLine("D_0060200=��ǰ�����й�����");
            FileItem.WriteLine("D_0060300=��ǰ�����޹�����");
            FileItem.WriteLine("D_0060400=��ǰ�����޹�����");
            FileItem.WriteLine("D_0060500=��ǰ�޹�һ��������");
            FileItem.WriteLine("D_0060600=��ǰ�޹�����������");
            FileItem.WriteLine("D_0060700=��ǰ�޹�����������");
            FileItem.WriteLine("D_0060800=��ǰ�޹�����������");
            FileItem.WriteLine("D_0060001=��1������й�����");
            FileItem.WriteLine("D_0060101=��1�������й�����");
            FileItem.WriteLine("D_0060201=��1�η����й�����");
            FileItem.WriteLine("D_0060301=��1�������޹�����");
            FileItem.WriteLine("D_0060401=��1�η����޹�����");
            FileItem.WriteLine("D_0060501=��1���޹�һ��������");
            FileItem.WriteLine("D_0060601=��1���޹�����������");
            FileItem.WriteLine("D_0060701=��1���޹�����������");
            FileItem.WriteLine("D_0060801=��1���޹�����������");

            #endregion

            #region -------------------�¼���¼����-------------------------
            FileItem.WriteLine();
            FileItem.WriteLine();
            FileItem.WriteLine("[CL3000SH_SJ]");
            FileItem.WriteLine("D_001=ʧѹ��¼");
            FileItem.WriteLine("D_001010101=ʧѹ��¼_A��_��1��_����ǰ����");
            FileItem.WriteLine("D_001010102=ʧѹ��¼_A��_��1��_����������");

            FileItem.WriteLine("D_002=��ѹ��¼");
            FileItem.WriteLine("D_002010101=��ѹ��¼_A��_��1��_����ǰ����");
            FileItem.WriteLine("D_002010102=��ѹ��¼_A��_��1��_����������");

            FileItem.WriteLine("D_003=Ƿѹ��¼");
            FileItem.WriteLine("D_003010101=Ƿѹ��¼_A��_��1��_����ǰ����");
            FileItem.WriteLine("D_003010102=Ƿѹ��¼_A��_��1��_����������");

            FileItem.WriteLine("D_004=ʧ����¼");
            FileItem.WriteLine("D_004010101=ʧ����¼_A��_��1��_����ǰ����");
            FileItem.WriteLine("D_004010102=ʧ����¼_A��_��1��_����������");

            FileItem.WriteLine("D_005=������¼");
            FileItem.WriteLine("D_005010101=������¼_A��_��1��_����ǰ����");
            FileItem.WriteLine("D_005010102=������¼_A��_��1��_����������");

            FileItem.WriteLine("D_006=������¼");
            FileItem.WriteLine("D_006010101=������¼_A��_��1��_����ǰ����");
            FileItem.WriteLine("D_006010102=������¼_A��_��1��_����������");

            FileItem.WriteLine("D_007=���ؼ�¼");
            FileItem.WriteLine("D_007010101=���ؼ�¼_A��_��1��_����ǰ����");
            FileItem.WriteLine("D_007010102=���ؼ�¼_A��_��1��_����������");

            FileItem.WriteLine("D_008=�����¼");
            FileItem.WriteLine("D_008010101=�����¼_A��_��1��_����ǰ����");
            FileItem.WriteLine("D_008010102=�����¼_A��_��1��_����������");

            FileItem.WriteLine("D_009=�����¼");
            FileItem.WriteLine("D_009000101=�����¼_��1��_����ǰ����");
            FileItem.WriteLine("D_009000102=�����¼_��1��_����������");

            FileItem.WriteLine("D_010=ȫʧѹ��¼");
            FileItem.WriteLine("D_010000101=ȫʧѹ��¼_��1��_����ǰ����");
            FileItem.WriteLine("D_010000102=ȫʧѹ��¼_��1��_����������");

            FileItem.WriteLine("D_011=��ѹ��ƽ���¼");
            FileItem.WriteLine("D_011000101=ȫʧѹ��¼_��1��_����ǰ����");
            FileItem.WriteLine("D_011000102=ȫʧѹ��¼_��1��_����������");

            FileItem.WriteLine("D_012=������ƽ���¼");
            FileItem.WriteLine("D_012000101=ȫʧѹ��¼_��1��_����ǰ����");
            FileItem.WriteLine("D_012000102=ȫʧѹ��¼_��1��_����������");

            FileItem.WriteLine("D_013=��ѹ�������¼");
            FileItem.WriteLine("D_013000101=ȫʧѹ��¼_��1��_����ǰ����");
            FileItem.WriteLine("D_013000102=ȫʧѹ��¼_��1��_����������");

            FileItem.WriteLine("D_014=�����������¼");
            FileItem.WriteLine("D_014000101=�����������¼_��1��_����ǰ����");
            FileItem.WriteLine("D_014000102=�����������¼_��1��_����������");

            FileItem.WriteLine("D_015=����Ǽ�¼");
            FileItem.WriteLine("D_01501=����Ǽ�¼_��1��_����ǰ����");
            FileItem.WriteLine("D_01502=����Ǽ�¼_��1��_����������");

            FileItem.WriteLine("D_016=����ť�м�¼");
            FileItem.WriteLine("D_01601=����ť�м�¼_��1��_����ǰ����");
            FileItem.WriteLine("D_01602=����ť�м�¼_��1��_����������");

            FileItem.WriteLine("D_017=��̼�¼");
            FileItem.WriteLine("D_018=Уʱ��¼");
            FileItem.WriteLine("D_019=���������¼");
            FileItem.WriteLine("D_020=�¼������¼");
            FileItem.WriteLine("D_021=��������¼");
            FileItem.WriteLine("D_022=���������¼");
            FileItem.WriteLine("D_023=���ʷ����¼");
            FileItem.WriteLine("D_024=�������޼�¼");
            FileItem.WriteLine("D_025=�������������޼�¼");
            FileItem.WriteLine("D_026=����(�ز�)��¼");
            #endregion

            FileItem.Close();
        }


        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="WcShowStyle">�����ʾ��ʽ���ǻ���ֵ����ƽ��ֵ</param>
        /// <returns></returns>
        public static int GetWcNum(string KeyValue, int WcShowStyle, string PrintType)
        {
            int WcNum = 0;

            if (PrintType.IndexOf("֤��") >= 0)
            {
                if (KeyValue.Substring(2) == "12" )
                {
                    if (WcShowStyle != 0)
                    {
                        WcNum = -3;//��ƽ����ƽ�⸺�����֮�� ƽ��ֵ
                    }
                    else
                    {
                        WcNum = -4;//��ƽ����ƽ�⸺�����֮�� ����ֵ
                    }
                }

                else if (WcShowStyle == 0 || WcShowStyle == 1)
                {
                    WcNum = -2;
                }
                else
                {
                    WcNum = -1;
                }
            }
            else if (PrintType.IndexOf("ԭʼ��¼") >= 0)
            {
                #region ԭʼ��¼
                if (KeyValue.Substring(2) == "11")
                {
                       WcNum = -3;//��ƽ����ƽ�⸺�����֮��
                }
                else
                {
                    switch (KeyValue.ToLower())
                    {
                        case "wcpjz":
                            WcNum = -1;
                            break;
                        case "wchzz":
                            WcNum = -2;
                            break;
                        default:
                            try
                            {
                                WcNum = int.Parse(KeyValue.Substring(2));
                            }
                            catch
                            {
                                WcNum = -2;
                            }
                            break;
                    }
                }
                //}
                //else if (WcShowStyle == 0 || WcShowStyle == 1)
                //{
                //    WcNum = -2;
                //}
                //else
                //{
                //    WcNum = -1;
                //}
                #endregion
            }
            else if (PrintType.IndexOf("֪ͨ��") >= 0)
            {
                if (WcShowStyle == 1 || WcShowStyle == 3)
                {
                    WcNum = -2;
                }
                else
                {
                    WcNum = -1;
                }
            }
            else
            {
                switch (KeyValue.ToLower())
                {
                    case "wcpjz":
                        WcNum = -1;
                        break;
                    case "wchzz":
                        WcNum = -2;
                        break;
                    case "wcysz1":
                        WcNum = 0;
                        break;
                    case "wcysz2":
                        WcNum = 1;
                        break;
                    case "wcpc":
                        WcNum = -2;
                        break;
                    default:
                        try
                        {
                            WcNum = int.Parse(KeyValue.Substring(2));
                        }
                        catch
                        {
                            WcNum = -2;
                        }
                        break;
                }
            }
            return WcNum;
        }


        /// <summary>
        /// ��ȡ�����Ŀ����
        /// </summary>
        /// <param name="Items">�����Ŀ����</param>
        /// <param name="KeyValue">�����Ϣ"P+:H10_Imax:WcHzz"</param>
        /// <returns></returns>
        public static CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError getErrorItem(Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError> Items, string KeyValue)
        {
            string strCheckName = "";

            string[] arrTmp = KeyValue.Trim().Split(':');

            string[] arrTmp1 = arrTmp[1].Split('_');
            string key = "";
            string tempvalue = "";

            bool IsPc = KeyValue.IndexOf("PC") >= 0 ? true : false;         //ƫ��IDΪ2  

            if (IsPc)
            {
                key = "2";//�������=1,��׼ƫ��=2,
                strCheckName = arrTmp[0] + " ��Ԫ ";

                strCheckName += FormatGlysDot(arrTmp1[0], out tempvalue) + " ";

                strCheckName += FormatxIbDot(arrTmp1[1].ToLower(), out  tempvalue) + " ��׼ƫ��";



            }
            else
            {
                key = "1";//�������=1,��׼ƫ��=2,

                strCheckName = arrTmp[0] + " " + GetYuanJian(arrTmp[1].Substring(0, 1), out tempvalue) + " ";
                key += GetPower(arrTmp[0]) + tempvalue;
                strCheckName += FormatGlysDot(arrTmp1[0].Substring(1, arrTmp1[0].Length - 1), out tempvalue) + " ";
                key += tempvalue;
                strCheckName += FormatxIbDot(arrTmp1[1].ToLower(), out tempvalue) + " �������";
                key += tempvalue;

            }
            //Ĭ��г��������
            key += "00";

            if (Items.ContainsKey(key))
            {
                return Items[key];
            }
            return null;


        }
        /// <summary>
        /// ��ù��ʷ���ת����IDֵ��
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GetPower(string value)
        {

            switch (value.ToUpper())
            {
                case "P+":
                    {
                        return "1";
                    }
                case "Q+":
                    {
                        return "3";
                    }
                case "P-":
                    {
                        return "2";
                    }
                case "Q-":
                    {
                        return "4";
                    }

            }
            return "1";
        }
        private static string GetGLandIB(string value)
        {
            return "";
        }
        /// <summary>
        /// ��û��С����Ĺ�����������С����
        /// </summary>
        /// <param name="Glys"></param>
        /// <returns></returns>
        public static string FormatGlysDot(string Glys, out string value)
        {
            value = "01";
            switch (Glys.ToUpper())
            {
                case "10":
                    {
                        value = "01";
                        break;
                    }
                case "05L":
                    {
                        value = "02";
                        break;
                    }
                case "08C":
                    {
                        value = "03";
                        break;
                    }
                case "05C":
                    {
                        value = "04";
                        break;
                    }
                case "08L":
                    {
                        value = "05";
                        break;
                    }
                case "025L":
                    {
                        value = "06";
                        break;
                    }
                case "025C":
                    {
                        value = "07";
                        break;
                    }
            }
            return Glys.Insert(1, ".");
        }


        public static string FormatxIbDot(string xIb, out string value)
        {
            value = "07";
            if (xIb == "imax")
            {
                value = "01";
                return "Imax";
            }
            if (xIb == "imaxib")
            {
                value = "06";
                return "0.5(Imax-Ib)";
            }
            if (xIb == "ib")
            {
                value = "07";
                return "1.0Ib";
            }
            xIb = xIb.Replace("imax", "Imax");
            xIb = xIb.Replace("ib", "Ib");
            xIb = xIb.Insert(1, ".");
            switch (xIb)
            {
                case "0.5Imax":
                    {
                        value = "02";
                        break;
                    }
                case "1.0Ib":
                    {
                        value = "07";
                        break;
                    }
                case "3.0Ib":
                    {
                        value = "03";
                        break;
                    }
                case "2.0Ib":
                    {
                        value = "04";
                        break;
                    }
                case "1.5Ib":
                    {
                        value = "05";
                        break;
                    }
                case "0.8Ib":
                    {
                        value = "08";
                        break;
                    }
                case "0.5Ib":
                    {
                        value = "09";
                        break;
                    }
                case "0.1Ib":
                    {
                        value = "11";
                        break;
                    }
                case "0.2Ib":
                    {
                        value = "10";
                        break;
                    }
                case "0.05Ib":
                    {
                        value = "12";
                        break;
                    }
                case "0.02Ib":
                    {
                        value = "13";
                        break;
                    }
                case "0.01Ib":
                    {
                        value = "14";
                        break;
                    }
            }
            return xIb;
        }

        /// <summary>
        /// ��ȡ���ʷ���ID
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public static int GetGlfxNum(string KeyValue)
        {
            int Glfx = 0;
            KeyValue = KeyValue.ToLower();
            if (KeyValue == "p-")
            {
                Glfx = 2;
            }
            else if (KeyValue == "q+")
            {
                Glfx = 3;
            }
            else if (KeyValue == "q-")
            {
                Glfx = 4;
            }
            else
            {
                Glfx = 1;
            }
            return Glfx;
        }
        /// <summary>
        /// ��ȡԪ��ID
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public static string GetYuanJian(string KeyValue, out string value)
        {
            KeyValue = KeyValue.ToLower();
            string strYuanJian = "";
            value = "1";
            if (KeyValue == "a")
            {
                strYuanJian = "AԪ";
                value = "2";
            }
            else if (KeyValue == "b")
            {
                strYuanJian = "BԪ";
                value = "3";
            }
            else if (KeyValue == "c")
            {
                strYuanJian = "CԪ";
                value = "4";
            }
            else
            {
                strYuanJian = "��Ԫ";
                value = "1";
            }
            return strYuanJian;
        }

        /// <summary>
        /// ��ȡ���ʺ�
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public static int GetFeiLvNum(string KeyValue)
        {
            int FeiLv = 0;
            if (KeyValue == "��")
            {
                FeiLv = 2;
            }
            else if (KeyValue == "ƽ")
            {
                FeiLv = 3;
            }
            else if (KeyValue == "��")
            {
                FeiLv = 4;
            }
            else if (KeyValue == "��")
            {
                FeiLv = 1;
            }
            return FeiLv;
        }

    }
}

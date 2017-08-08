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

        #region------------获取配置文件数据---------------------------
        /// <summary>
        /// 获取ini配置文件数据，默认路径，运行目录下Reportinfo.ini，默认数据空
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public static string getIniString(string sSection, string skey)
        {
            return getIniString(sSection, skey, "");
        }
        /// <summary>
        /// 获取INI配置文件数据，默认路径，运行目录下Reportinfo.ini
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
        /// 获取INI配置文件数据
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
        /// 写配置文件数据,默认配置文件路径，运行目录下Reportinfo.ini
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="sKey"></param>
        /// <param name="sValue"></param>
        public static void WriteIni(string sSection, string sKey, string sValue)
        {
            WriteIni(sSection, sKey, sValue, System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
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
        /// 根据相对路径获取绝对路径
        /// </summary>
        /// <param name="filepath">相对路径</param>
        /// <returns></returns>
        public static string getFilePath(string filepath)
        {
            string tmp = System.Reflection.Assembly.GetExecutingAssembly().Location;

            tmp = tmp.Substring(0, tmp.LastIndexOf('\\'));
            if (filepath == "") return tmp;

            return string.Format(@"{0}\{1}", tmp, filepath);

        }
        /// <summary>
        /// 获取误差的中文描述信息
        /// </summary>
        /// <param name="keyValue">误差项目ID</param>
        /// <returns></returns>
        public static string getErrorName(string keyValue)
        {
            string yuanjian = ((CLDC_Comm.Enum.Cus_PowerYuanJian)int.Parse(keyValue[2].ToString())).ToString();

            if (yuanjian.ToLower() == "h")
            {
                yuanjian = "合元";
            }
            else
            {
                yuanjian += "元";
            }

            return string.Format("{0}{1}", (CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(keyValue[1].ToString()), yuanjian); 

        }


        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="ErrorNum">错误号</param>
        /// <param name="Description">错误描述</param>
        /// <param name="RowNum">错误行</param>
        public static void WriteReportErr(long ErrorNum, string Description)
        {
            if (!System.IO.Directory.Exists(getFilePath("ReportErr")))
            {
                System.IO.Directory.CreateDirectory(getFilePath("ReportErr"));
            }
            System.IO.StreamWriter Writer = new System.IO.StreamWriter(string.Format(@"{0}\Err_{1}.Log", getFilePath("ReportErr"), DateTime.Now.ToString("yyyyMMddhhmmhh")), true, System.Text.Encoding.Default);
            Writer.Write(string.Format("错误日期：{0}\r\n错误号：{1}\r\n错误描述：\r\n{2}\r\n"
                                                        , DateTime.Now.ToLongDateString()
                                                        , ErrorNum
                                                        , Description));
            Writer.Close();

        }

        public static void WriteRptConfigIni()
        {
            System.IO.StreamWriter FileItem = new System.IO.StreamWriter(getFilePath("ReportConfig.ini"), true, System.Text.Encoding.Default);
            #region ----------------基本信息-------------------------
            FileItem.WriteLine("[CL3000SH]");
            FileItem.WriteLine("Mb_lngMyID=MyID");
            FileItem.WriteLine(@"//表唯一编号");
            FileItem.WriteLine("Mb_intBno=Position");
            FileItem.WriteLine(@"//表位号");
            FileItem.WriteLine("Mb_ChrJlbh=MeterID");
            FileItem.WriteLine(@"//计量编号");
            FileItem.WriteLine("Mb_ChrCcbh=ProductID");
            FileItem.WriteLine(@"//出厂编号");
            FileItem.WriteLine("Mb_ChrTxm=Txm");
            FileItem.WriteLine(@"//条码号");
            FileItem.WriteLine("Mb_chrAddr=MeterAdr");
            FileItem.WriteLine(@"//表通信地址");
            FileItem.WriteLine("Mb_chrzzcj=Factory");
            FileItem.WriteLine(@"//制造厂家");
            FileItem.WriteLine("Mb_chrBxh=Size");
            FileItem.WriteLine(@"//表型号");
            FileItem.WriteLine("Mb_chrBcs=MeterConst");
            FileItem.WriteLine(@"//表常数");
            FileItem.WriteLine("Mb_chrBlx=MeterType");
            FileItem.WriteLine(@"//表类型");
            FileItem.WriteLine("Mb_chrBdj=MeterLevel");
            FileItem.WriteLine(@"//表等级");
            FileItem.WriteLine("Mb_chrCcrq=ProductDate");
            FileItem.WriteLine(@"//出厂日期");
            FileItem.WriteLine("Mb_chrSjdw=Owner");
            FileItem.WriteLine(@"//送检单位");
            FileItem.WriteLine("Mb_chrZsbh=CertificateNo");
            FileItem.WriteLine(@"//证书编号");
            FileItem.WriteLine("Mb_ChrBmc=MeterName");
            FileItem.WriteLine(@"//表名称");
            FileItem.WriteLine("Mb_intClfs=CheckType");
            FileItem.WriteLine(@"//测量方式");
            FileItem.WriteLine("Mb_chrUb=U");
            FileItem.WriteLine(@"//电压");
            FileItem.WriteLine("Mb_chrIb=I");
            FileItem.WriteLine(@"//电流");
            FileItem.WriteLine("Mb_chrHz=Pl");
            FileItem.WriteLine(@"//频率");
            FileItem.WriteLine("Mb_BlnZnq=ZNQ");
            FileItem.WriteLine(@"//止逆器");
            FileItem.WriteLine("Mb_BlnHgq=HGQ");
            FileItem.WriteLine(@"//互感器");
            FileItem.WriteLine("Mb_chrTestType=TestType");
            FileItem.WriteLine(@"//测试类型");
            FileItem.WriteLine("Mb_DatJdrq=CheckDate");
            FileItem.WriteLine(@"//检定日期");
            FileItem.WriteLine("Mb_Datjjrq=JJDate");
            FileItem.WriteLine(@"//计检日期");
            FileItem.WriteLine("Mb_chrWd=Temperature");
            FileItem.WriteLine(@"//温度");
            FileItem.WriteLine("Mb_chrSd=Humidity");
            FileItem.WriteLine(@"//湿度");
            FileItem.WriteLine("Mb_chrResult=Result");
            FileItem.WriteLine(@"//结论");
            FileItem.WriteLine("Mb_ChrJyy=Checker");
            FileItem.WriteLine(@"//检验员");
            FileItem.WriteLine("Mb_ChrHyy=Verificationer");
            FileItem.WriteLine(@"//核验员");
            FileItem.WriteLine("Mb_chrZhuGuan=Charge");
            FileItem.WriteLine(@"//主管");
            FileItem.WriteLine("Mb_BlnToServer=ToServer");
            FileItem.WriteLine(@"//是否上传到服务器");
            FileItem.WriteLine("Mb_BlnToMis=ToMis");
            FileItem.WriteLine(@"//是否已上传到营销");
            FileItem.WriteLine("Mb_CrQianFeng1=Qf1");
            FileItem.WriteLine(@"//铅封一");
            FileItem.WriteLine("Mb_CrQianFeng2=Qf2");
            FileItem.WriteLine(@"//铅封二");
            FileItem.WriteLine("Mb_CrQianFeng3=Qf3");
            FileItem.WriteLine(@"//铅封三");
            FileItem.WriteLine("Mb_chrOther1=other1");
            FileItem.WriteLine(@"//通信协议名称");
            FileItem.WriteLine("Mb_chrOther2=other2");
            FileItem.WriteLine(@"//检测类型");
            FileItem.WriteLine("Mb_chrOther4=other4");
            FileItem.WriteLine(@"//制造标准");
            FileItem.WriteLine("Mb_chrOther5=other5");
            FileItem.WriteLine(@"//检定依据");
            #endregion
            #region ---------------------结论信息-----------------------
            FileItem.WriteLine();
            FileItem.WriteLine("[CL3000SH_Result]");
            FileItem.WriteLine("R_100=ZGZJ");
            FileItem.WriteLine(@"//外观检查试验");
            FileItem.WriteLine("R_101=Tdjc");
            FileItem.WriteLine(@"//通电检查");
            FileItem.WriteLine("R_102=GPNY");
            FileItem.WriteLine(@"//工频耐压试验");
            FileItem.WriteLine("R_103=BasicError");
            FileItem.WriteLine(@"//基本误差");
            FileItem.WriteLine("R_1031=BP+");
            FileItem.WriteLine(@"//正向有功基本误差");
            FileItem.WriteLine("R_1032=BP-");
            FileItem.WriteLine(@"//反向有功基本误差");
            FileItem.WriteLine("R_1033=BQ+");
            FileItem.WriteLine(@"//正向无功基本误差");
            FileItem.WriteLine("R_1034=BQ-");
            FileItem.WriteLine(@"//反向无功基本误差");
            FileItem.WriteLine("R_104=Standard");
            FileItem.WriteLine(@"//标准偏差");
            FileItem.WriteLine("R_1041=SP+");
            FileItem.WriteLine(@"//正向有功标准偏差");
            FileItem.WriteLine("R_1042=SP-");
            FileItem.WriteLine(@"//反向有功标准偏差");
            FileItem.WriteLine("R_1043=SQ+");
            FileItem.WriteLine(@"//正向无功标准偏差");
            FileItem.WriteLine("R_1044=SQ-");
            FileItem.WriteLine(@"//反向无功标准偏差");
            FileItem.WriteLine("R_105=MaxWarp");
            FileItem.WriteLine(@"//最大偏差");
            FileItem.WriteLine("R_106=RunTest");
            FileItem.WriteLine(@"//走字试验");
            FileItem.WriteLine("R_1061=ZP+");
            FileItem.WriteLine(@"//正向有功走字试验");
            FileItem.WriteLine("R_1062=ZP-");
            FileItem.WriteLine(@"//反向有功走字试验");
            FileItem.WriteLine("R_1063=ZQ+");
            FileItem.WriteLine(@"//正向无功走字试验");
            FileItem.WriteLine("R_1064=ZQ-");
            FileItem.WriteLine(@"//反向无功走字试验");
            FileItem.WriteLine("R_107=Multifunction");
            FileItem.WriteLine(@"//多功能试验");
            FileItem.WriteLine("R_108=SpecilCheck");
            FileItem.WriteLine(@"//特殊检定试验");
            FileItem.WriteLine("R_109=Start:StartDate:StartI");
            FileItem.WriteLine(@"//起动试验");
            FileItem.WriteLine("R_1091=Start_P+:StartDate_P+:StartI_P+");
            FileItem.WriteLine(@"//正向有功起动");
            FileItem.WriteLine("R_1092=Start_P-:StartDate_P-:StartI_P-");
            FileItem.WriteLine(@"//反向有功起动");
            FileItem.WriteLine("R_1093=Start_Q+:StartDate_Q+:StartI_Q+");
            FileItem.WriteLine(@"//正向无功起动");
            FileItem.WriteLine("R_1094=Start_Q-:StartDate_Q-:StartI_Q-");
            FileItem.WriteLine(@"//反向无功起动");
            FileItem.WriteLine("R_110=creeping");
            FileItem.WriteLine(@"//潜动试验");
            FileItem.WriteLine("R_1101=creeping_P+");
            FileItem.WriteLine(@"//正向有功潜动");
            FileItem.WriteLine("R_1102=creeping_P-");
            FileItem.WriteLine(@"//反向有功潜动");
            FileItem.WriteLine("R_1103=creeping_Q+");
            FileItem.WriteLine(@"//正向无功潜动");
            FileItem.WriteLine("R_1104=creeping_Q-");
            FileItem.WriteLine(@"//反向无功潜动");
            #endregion
            #region --------------------多功能信息----------------------
            FileItem.WriteLine();
            FileItem.WriteLine();
            FileItem.WriteLine("[CL3000SH_DGN]");
            FileItem.WriteLine("D_001=通信测试");
            FileItem.WriteLine("D_002=日计时误差");
            FileItem.WriteLine("D_00201=日计时误差值");
            FileItem.WriteLine("D_00202=计时误差1,计时误差2,计时误差3,计时误差4,计时误差5");
            FileItem.WriteLine("D_00203=计时误差6,计时误差7,计时误差8,计时误差9,计时误差10");
            FileItem.WriteLine("D_003=时段投切误差");
            FileItem.WriteLine("D_00301=时段1标准,时段1实际,时段1误差,费1");
            FileItem.WriteLine("D_00302=时段2标准,时段2实际,时段2误差,费2");
            FileItem.WriteLine("D_00303=时段3标准,时段3实际,时段3误差,费3");
            FileItem.WriteLine("D_00304=时段4标准,时段4实际,时段4误差,费4");
            FileItem.WriteLine("D_00305=时段5标准,时段5实际,时段5误差,费5");
            FileItem.WriteLine("D_00306=时段6标准,时段6实际,时段6误差,费6");
            FileItem.WriteLine("D_00307=时段7标准,时段7实际,时段7误差,费7");
            FileItem.WriteLine("D_00308=时段8标准,时段8实际,时段8误差,费8");
            FileItem.WriteLine("D_004=GPS对时");
            FileItem.WriteLine("D_005=闰年切换");
            FileItem.WriteLine("D_006=电量寄存器");
            FileItem.WriteLine("D_007=需量寄存器");
            FileItem.WriteLine("D_008=瞬时寄存器");
            FileItem.WriteLine("D_009=状态寄存器检查");
            FileItem.WriteLine("D_010=失压寄存器");
            FileItem.WriteLine("D_011=事件记录");
            FileItem.WriteLine("D_012=需量复零");
            FileItem.WriteLine("D_013=电压变化");
            FileItem.WriteLine("D_014=电压跌落试验");
            FileItem.WriteLine("D_015=设置时间");
            FileItem.WriteLine("D_01501=当前时间,标准时间,GPS对时差值");
            FileItem.WriteLine("D_016=最大需量01Ib");
            FileItem.WriteLine("D_01602=需量周期,周期误差,需量周期误差");
            FileItem.WriteLine("D_0161=01Ib最大需量P+");
            FileItem.WriteLine("D_01611=01IB最大需量P+,01Ib实际需量P+,01IB误差P+");
            FileItem.WriteLine("D_0162=01Ib最大需量P-");
            FileItem.WriteLine("D_01621=01IB最大需量P-,01Ib实际需量P-,01IB误差P-");
            FileItem.WriteLine("D_0163=01Ib最大需量Q+");
            FileItem.WriteLine("D_01631=01IB最大需量Q+,01Ib实际需量Q+,01IB误差Q+");
            FileItem.WriteLine("D_0164=01Ib最大需量Q-");
            FileItem.WriteLine("D_01641=01IB最大需量Q-,01Ib实际需量Q-,01IB误差Q-");
            FileItem.WriteLine("D_017=最大需量10Ib");
            FileItem.WriteLine("D_01702=需量周期,周期误差,需量周期误差");
            FileItem.WriteLine("D_0171=10Ib最大需量P+");
            FileItem.WriteLine("D_01711=10IB最大需量P+,10Ib实际需量P+,10IB误差P+");
            FileItem.WriteLine("D_0172=10Ib最大需量P-");
            FileItem.WriteLine("D_01721=10IB最大需量P-,10Ib实际需量P-,10IB误差P-");
            FileItem.WriteLine("D_0173=10Ib最大需量Q+");
            FileItem.WriteLine("D_01731=10IB最大需量Q+,10Ib实际需量Q+,10IB误差Q+");
            FileItem.WriteLine("D_0174=10Ib最大需量Q-");
            FileItem.WriteLine("D_01741=10IB最大需量Q-,10Ib实际需量Q-,10IB误差Q-");
            FileItem.WriteLine("D_018=最大需量Imax");
            FileItem.WriteLine("D_01802=需量周期,周期误差,需量周期误差");
            FileItem.WriteLine("D_0181=Imax最大需量P+");
            FileItem.WriteLine("D_01811=Imax最大需量P+,Imax实际需量P+,Imax误差P+");
            FileItem.WriteLine("D_0182=Imax最大需量P-");
            FileItem.WriteLine("D_01821=Imax最大需量P-,Imax实际需量P-,Imax误差P-");
            FileItem.WriteLine("D_0183=Imax最大需量Q+");
            FileItem.WriteLine("D_01831=Imax最大需量Q+,Imax实际需量Q+,Imax误差Q+");
            FileItem.WriteLine("D_0184=Imax最大需量Q-");
            FileItem.WriteLine("D_01841=Imax最大需量Q-,Imax实际需量Q-,Imax误差Q-");
            FileItem.WriteLine("D_019=读取电量");
            FileItem.WriteLine("D_01901=底度P+后总,底度P+后峰,底度P+后平,底度P+后谷,底度P+后尖");
            FileItem.WriteLine("D_01902=底度P-后总,底度P-后峰,底度P-后平,底度P-后谷,底度P-后尖");
            FileItem.WriteLine("D_01903=底度Q+后总,底度Q+后峰,底度Q+后平,底度Q+后谷,底度Q+后尖");
            FileItem.WriteLine("D_01904=底度Q-后总,底度Q-后峰,底度Q-后平,底度Q-后谷,底度Q-后尖");
            FileItem.WriteLine("D_01905=底度Q1后总,底度Q1后峰,底度Q1后平,底度Q1后谷,底度Q1后尖");
            FileItem.WriteLine("D_01906=底度Q2后总,底度Q2后峰,底度Q2后平,底度Q2后谷,底度Q2后尖");
            FileItem.WriteLine("D_01907=底度Q3后总,底度Q3后峰,底度Q3后平,底度Q3后谷,底度Q3后尖");
            FileItem.WriteLine("D_01908=底度Q4后总,底度Q4后峰,底度Q4后平,底度Q4后谷,底度Q4后尖");
            FileItem.WriteLine("D_01909=无功总,一象限无功总,二象限无功总,三象限无功总,四象限无功总");
            FileItem.WriteLine("D_020=控制功能");
            FileItem.WriteLine("D_021=透支功能");
            FileItem.WriteLine("D_022=报警功能");
            FileItem.WriteLine("D_023=迭加功能");
            FileItem.WriteLine("D_024=限购功能");
            FileItem.WriteLine("D_025=辨伪功能");
            FileItem.WriteLine("D_026=返写功能");
            FileItem.WriteLine("D_027=补遗功能");
            FileItem.WriteLine("D_028=冻结功能");
            FileItem.WriteLine("D_029=过载保护");
            FileItem.WriteLine("D_030=记忆功能");
            FileItem.WriteLine("D_031=电量清零");
            FileItem.WriteLine("D_032=校对电量");
            FileItem.WriteLine("D_033=校对需量");
            FileItem.WriteLine("D_034=运行状态");
            FileItem.WriteLine("D_035=预付费检测");
            #endregion

            #region -------------------智能表试验-------------------------
            FileItem.WriteLine();
            FileItem.WriteLine();
            FileItem.WriteLine("[CL3000SH_GN]");
            FileItem.WriteLine("D_001=计量功能");
            FileItem.WriteLine("D_0010000=当前组合有功电量");
            FileItem.WriteLine("D_0010100=当前正向有功电量");
            FileItem.WriteLine("D_0010200=当前反向有功电量");
            FileItem.WriteLine("D_0010300=当前正向无功电量");
            FileItem.WriteLine("D_0010400=当前反向无功电量");
            FileItem.WriteLine("D_0010500=当前无功一象限电量");
            FileItem.WriteLine("D_0010600=当前无功二象限电量");
            FileItem.WriteLine("D_0010700=当前无功三象限电量");
            FileItem.WriteLine("D_0010800=当前无功四象限电量");
            FileItem.WriteLine("D_0010001=上1次组合有功电量");
            FileItem.WriteLine("D_0010101=上1次正向有功电量");
            FileItem.WriteLine("D_0010201=上1次反向有功电量");
            FileItem.WriteLine("D_0010301=上1次正向无功电量");
            FileItem.WriteLine("D_0010401=上1次反向无功电量");
            FileItem.WriteLine("D_0010501=上1次无功一象限电量");
            FileItem.WriteLine("D_0010601=上1次无功二象限电量");
            FileItem.WriteLine("D_0010701=上1次无功三象限电量");
            FileItem.WriteLine("D_0010801=上1次无功四象限电量");

            FileItem.WriteLine("D_002=计时功能");
            FileItem.WriteLine("D_00201=计时功能_23点55分前广播校时");
            FileItem.WriteLine("D_00202=计时功能_23点57分广播校时");
            FileItem.WriteLine("D_00203=计时功能_00点01分广播校时");
            FileItem.WriteLine("D_00204=计时功能_00点07分广播校时");
            FileItem.WriteLine("D_00205=计时功能_小于5分广播校时");
            FileItem.WriteLine("D_00206=计时功能_大于5分广播校时");
            FileItem.WriteLine("D_00207=计时功能_一天一次广播校时");
            FileItem.WriteLine("D_00208=计时功能_一天重复广播校时");


            FileItem.WriteLine("D_004=费率时段功能");
            FileItem.WriteLine("D_00405=费率时段功能_转换前信息");
            FileItem.WriteLine("D_00406=费率时段功能_转换后信息");
            FileItem.WriteLine("D_00407=费率时段功能_恢复后信息");

            FileItem.WriteLine("D_005=脉冲输出功能");
            FileItem.WriteLine("D_00501=脉冲输出功能_电能脉冲");
            FileItem.WriteLine("D_00502=脉冲输出功能_秒脉冲");
            FileItem.WriteLine("D_00503=脉冲输出功能_投切脉冲");
            FileItem.WriteLine("D_00504=脉冲输出功能_需量脉冲");

            FileItem.WriteLine("D_006=需量功能");
            FileItem.WriteLine("D_0060000=当前组合有功需量");
            FileItem.WriteLine("D_0060100=当前正向有功需量");
            FileItem.WriteLine("D_0060200=当前反向有功需量");
            FileItem.WriteLine("D_0060300=当前正向无功需量");
            FileItem.WriteLine("D_0060400=当前反向无功需量");
            FileItem.WriteLine("D_0060500=当前无功一象限需量");
            FileItem.WriteLine("D_0060600=当前无功二象限需量");
            FileItem.WriteLine("D_0060700=当前无功三象限需量");
            FileItem.WriteLine("D_0060800=当前无功四象限需量");
            FileItem.WriteLine("D_0060001=上1次组合有功需量");
            FileItem.WriteLine("D_0060101=上1次正向有功需量");
            FileItem.WriteLine("D_0060201=上1次反向有功需量");
            FileItem.WriteLine("D_0060301=上1次正向无功需量");
            FileItem.WriteLine("D_0060401=上1次反向无功需量");
            FileItem.WriteLine("D_0060501=上1次无功一象限需量");
            FileItem.WriteLine("D_0060601=上1次无功二象限需量");
            FileItem.WriteLine("D_0060701=上1次无功三象限需量");
            FileItem.WriteLine("D_0060801=上1次无功四象限需量");

            #endregion

            #region -------------------事件记录试验-------------------------
            FileItem.WriteLine();
            FileItem.WriteLine();
            FileItem.WriteLine("[CL3000SH_SJ]");
            FileItem.WriteLine("D_001=失压记录");
            FileItem.WriteLine("D_001010101=失压记录_A相_上1次_产生前数据");
            FileItem.WriteLine("D_001010102=失压记录_A相_上1次_产生后数据");

            FileItem.WriteLine("D_002=过压记录");
            FileItem.WriteLine("D_002010101=过压记录_A相_上1次_产生前数据");
            FileItem.WriteLine("D_002010102=过压记录_A相_上1次_产生后数据");

            FileItem.WriteLine("D_003=欠压记录");
            FileItem.WriteLine("D_003010101=欠压记录_A相_上1次_产生前数据");
            FileItem.WriteLine("D_003010102=欠压记录_A相_上1次_产生后数据");

            FileItem.WriteLine("D_004=失流记录");
            FileItem.WriteLine("D_004010101=失流记录_A相_上1次_产生前数据");
            FileItem.WriteLine("D_004010102=失流记录_A相_上1次_产生后数据");

            FileItem.WriteLine("D_005=断流记录");
            FileItem.WriteLine("D_005010101=断流记录_A相_上1次_产生前数据");
            FileItem.WriteLine("D_005010102=断流记录_A相_上1次_产生后数据");

            FileItem.WriteLine("D_006=过流记录");
            FileItem.WriteLine("D_006010101=过流记录_A相_上1次_产生前数据");
            FileItem.WriteLine("D_006010102=过流记录_A相_上1次_产生后数据");

            FileItem.WriteLine("D_007=过载记录");
            FileItem.WriteLine("D_007010101=过载记录_A相_上1次_产生前数据");
            FileItem.WriteLine("D_007010102=过载记录_A相_上1次_产生后数据");

            FileItem.WriteLine("D_008=断相记录");
            FileItem.WriteLine("D_008010101=断相记录_A相_上1次_产生前数据");
            FileItem.WriteLine("D_008010102=断相记录_A相_上1次_产生后数据");

            FileItem.WriteLine("D_009=掉电记录");
            FileItem.WriteLine("D_009000101=掉电记录_上1次_产生前数据");
            FileItem.WriteLine("D_009000102=掉电记录_上1次_产生后数据");

            FileItem.WriteLine("D_010=全失压记录");
            FileItem.WriteLine("D_010000101=全失压记录_上1次_产生前数据");
            FileItem.WriteLine("D_010000102=全失压记录_上1次_产生后数据");

            FileItem.WriteLine("D_011=电压不平衡记录");
            FileItem.WriteLine("D_011000101=全失压记录_上1次_产生前数据");
            FileItem.WriteLine("D_011000102=全失压记录_上1次_产生后数据");

            FileItem.WriteLine("D_012=电流不平衡记录");
            FileItem.WriteLine("D_012000101=全失压记录_上1次_产生前数据");
            FileItem.WriteLine("D_012000102=全失压记录_上1次_产生后数据");

            FileItem.WriteLine("D_013=电压逆相序记录");
            FileItem.WriteLine("D_013000101=全失压记录_上1次_产生前数据");
            FileItem.WriteLine("D_013000102=全失压记录_上1次_产生后数据");

            FileItem.WriteLine("D_014=电流逆相序记录");
            FileItem.WriteLine("D_014000101=电流逆相序记录_上1次_产生前数据");
            FileItem.WriteLine("D_014000102=电流逆相序记录_上1次_产生后数据");

            FileItem.WriteLine("D_015=开表盖记录");
            FileItem.WriteLine("D_01501=开表盖记录_上1次_产生前数据");
            FileItem.WriteLine("D_01502=开表盖记录_上1次_产生后数据");

            FileItem.WriteLine("D_016=开端钮盒记录");
            FileItem.WriteLine("D_01601=开端钮盒记录_上1次_产生前数据");
            FileItem.WriteLine("D_01602=开端钮盒记录_上1次_产生后数据");

            FileItem.WriteLine("D_017=编程记录");
            FileItem.WriteLine("D_018=校时记录");
            FileItem.WriteLine("D_019=需量清零记录");
            FileItem.WriteLine("D_020=事件清零记录");
            FileItem.WriteLine("D_021=电表清零记录");
            FileItem.WriteLine("D_022=潮流反向记录");
            FileItem.WriteLine("D_023=功率反向记录");
            FileItem.WriteLine("D_024=需量超限记录");
            FileItem.WriteLine("D_025=功率因数超下限记录");
            FileItem.WriteLine("D_026=过流(载波)记录");
            #endregion

            FileItem.Close();
        }


        /// <summary>
        /// 获取误差次数
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <param name="WcShowStyle">误差显示方式，是化整值还是平均值</param>
        /// <returns></returns>
        public static int GetWcNum(string KeyValue, int WcShowStyle, string PrintType)
        {
            int WcNum = 0;

            if (PrintType.IndexOf("证书") >= 0)
            {
                if (KeyValue.Substring(2) == "12" )
                {
                    if (WcShowStyle != 0)
                    {
                        WcNum = -3;//不平衡与平衡负载误差之差 平均值
                    }
                    else
                    {
                        WcNum = -4;//不平衡与平衡负载误差之差 化整值
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
            else if (PrintType.IndexOf("原始记录") >= 0)
            {
                #region 原始记录
                if (KeyValue.Substring(2) == "11")
                {
                       WcNum = -3;//不平衡与平衡负载误差之差
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
            else if (PrintType.IndexOf("通知书") >= 0)
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
        /// 获取误差项目对象
        /// </summary>
        /// <param name="Items">误差项目集合</param>
        /// <param name="KeyValue">误差信息"P+:H10_Imax:WcHzz"</param>
        /// <returns></returns>
        public static CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError getErrorItem(Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError> Items, string KeyValue)
        {
            string strCheckName = "";

            string[] arrTmp = KeyValue.Trim().Split(':');

            string[] arrTmp1 = arrTmp[1].Split('_');
            string key = "";
            string tempvalue = "";

            bool IsPc = KeyValue.IndexOf("PC") >= 0 ? true : false;         //偏差ID为2  

            if (IsPc)
            {
                key = "2";//基本误差=1,标准偏差=2,
                strCheckName = arrTmp[0] + " 合元 ";

                strCheckName += FormatGlysDot(arrTmp1[0], out tempvalue) + " ";

                strCheckName += FormatxIbDot(arrTmp1[1].ToLower(), out  tempvalue) + " 标准偏差";



            }
            else
            {
                key = "1";//基本误差=1,标准偏差=2,

                strCheckName = arrTmp[0] + " " + GetYuanJian(arrTmp[1].Substring(0, 1), out tempvalue) + " ";
                key += GetPower(arrTmp[0]) + tempvalue;
                strCheckName += FormatGlysDot(arrTmp1[0].Substring(1, arrTmp1[0].Length - 1), out tempvalue) + " ";
                key += tempvalue;
                strCheckName += FormatxIbDot(arrTmp1[1].ToLower(), out tempvalue) + " 基本误差";
                key += tempvalue;

            }
            //默认谐波和相序
            key += "00";

            if (Items.ContainsKey(key))
            {
                return Items[key];
            }
            return null;


        }
        /// <summary>
        /// 获得功率方向（转化成ID值）
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
        /// 将没有小数点的功率因数加上小数点
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
        /// 获取功率方向ID
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
        /// 获取元件ID
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
                strYuanJian = "A元";
                value = "2";
            }
            else if (KeyValue == "b")
            {
                strYuanJian = "B元";
                value = "3";
            }
            else if (KeyValue == "c")
            {
                strYuanJian = "C元";
                value = "4";
            }
            else
            {
                strYuanJian = "合元";
                value = "1";
            }
            return strYuanJian;
        }

        /// <summary>
        /// 获取费率号
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public static int GetFeiLvNum(string KeyValue)
        {
            int FeiLv = 0;
            if (KeyValue == "峰")
            {
                FeiLv = 2;
            }
            else if (KeyValue == "平")
            {
                FeiLv = 3;
            }
            else if (KeyValue == "谷")
            {
                FeiLv = 4;
            }
            else if (KeyValue == "尖")
            {
                FeiLv = 1;
            }
            return FeiLv;
        }

    }
}

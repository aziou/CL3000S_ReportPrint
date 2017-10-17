using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Office.Interop.Word;
namespace CLReport_Standard
{
    public class ClReport2007// : CLDC_DataCore.Interfaces.IReportInterface
    {
        /// <summary>
        /// 是否是一页多条
        /// </summary>
        private bool blnColIsAll = false;

        private string iniPath = clsMain.getFilePath(@"Res\Templet.ini");

        private UI.UI_ReportInfo uiReportInfo = null;

        private UI.UI_ReportSet uiReportSet = null;

        /// <summary>
        /// 检定依据
        /// </summary>
        private string chrJdyj = "";
        /// <summary>
        /// 制造标准
        /// </summary>
        private string chrZzbz = "";

        /// <summary>
        /// 打印配置
        /// </summary>
        private PrintOtherInfo PrintInfo;

        /// <summary>
        /// 打印类型名称
        /// </summary>
        private string PrintTypeName;
        /// <summary>
        /// 根目录
        /// </summary>
        private string ResRootPath = Path.GetDirectoryName(typeof(ClReport2007).Assembly.Location);

        /// <summary>
        /// 数据打印类型
        /// </summary>
        private enum PrintDataType
        {
            基本信息 = 1,
            耐压数据 = 2,
            功耗数据 = 3,
            误差数据 = 4,            
            走字数据 = 5,
            载波数据 = 6,            
            多功能数据 = 7,
            影响量数据 = 8,
            一致性数据 = 9,
            起动潜动数据 = 10,
            通讯协议数据 = 11,
            费控功能数据 = 12,
            冻结功能数据 = 13,
            事件记录数据 = 14,
            负荷记录数据 = 15,
            智能表功能数据 = 16,
        }

        public ClReport2007()
        {
            // WordApp = new clsWordControl();
            PrintInfo = new PrintOtherInfo();
            PrintInfo.BHGString = clsMain.getIniString("OtherInfo", "BHG", "");                                         //不合格表示
            PrintInfo.PrintHuman = clsMain.getIniString("OtherInfo", "PrintHuman", "0") == "0" ? false : true;          //是否打印检定员
            PrintInfo.Saving = clsMain.getIniString("OtherInfo", "Save", "0") == "0" ? false : true;                    //是否存盘
            PrintInfo.Preview = clsMain.getIniString("Otherinfo", "Preview", "0") == "0" ? false : true;                //是否预览
            PrintInfo.PrintStyle = int.Parse(clsMain.getIniString("OtherInfo", "PrintStyle", "0"));                     //打印样式
            PrintInfo.SaveOnly = clsMain.getIniString("OtherInfo", "SaveOnly", "0") == "0" ? false : true;              //是否仅存档
            PrintInfo.SaveBag = int.Parse(clsMain.getIniString("OtherInfo", "SaveBag", "0"));

            PrintInfo.NotCheck = clsMain.getIniString("ReportInfo", "NotCheck", "");//zxr修复，关键字写错了 20140812

            PrintInfo.SavePath = clsMain.getIniString("Otherinfo", "SavePath", System.Windows.Forms.Application.StartupPath);

            if (!System.IO.File.Exists(clsMain.getFilePath("ReportConfig.ini")))
            {
                clsMain.WriteRptConfigIni();
            }
        }


        #region IReportInterface 成员
        /// <summary>
        /// 打印报表
        /// </summary>
        /// <param name="Items">要打印的电能表数据</param>
        /// <param name="ReportTao">报表样式名称</param>
        /// <param name="ReportType">报表类型</param>
        /// <param name="Jdyj">检定依据</param>
        /// <param name="zzbz">制造标准</param>
        public void PrintRpt(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items
            , int ReportTao
            , int ReportType
            , string Jdyj
            , string zzbz)
        {
            //获取打印类型名称
            PrintTypeName = clsMain.getIniString("Type_" + ReportTao.ToString(), "TypeName", "", System.Windows.Forms.Application.StartupPath + @"\Res\Templet.ini").Split(',')[ReportType];
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在解析数据,请稍后.....");

            //整理数据
            List<Dictionary<PrintDataType, object>> DicItems = new List<Dictionary<PrintDataType, object>>();
            for (int i = 0; i < Items.Count; i++)
            {
                Dictionary<PrintDataType, object> DataItem = new Dictionary<PrintDataType, object>();
                DataItem.Add(PrintDataType.基本信息, DicBasicInfo(Items[i]));       //基本信息
                DataItem.Add(PrintDataType.误差数据, Items[i].MeterErrors);         //误差数据
                DataItem.Add(PrintDataType.多功能数据, DicDgnInfo(Items[i]));         //多功能数据
                DataItem.Add(PrintDataType.走字数据, Items[i].MeterZZErrors);       //走字数据
                DataItem.Add(PrintDataType.影响量数据, Items[i].MeterSpecialErrs);    //特殊检定数据
                

                this.AddDWSetting((Dictionary<string, string>)DataItem[PrintDataType.基本信息]);  //加入其它相关信息
                DicItems.Add(DataItem);
            }
            this.chrJdyj = Jdyj;
            this.chrZzbz = zzbz;
            CLDC_DataCore.Function.TopWaiting.HideWaiting();
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在生成报表,请稍后.....");
            WordTemplateParser.DocumentManager documentManager = null;

            for (int i = 0; i < Items.Count; i++)
            {

                if (blnColIsAll) break;     //如果是打印一页多条，就不再需要进行下面的过程，直接跳出

                string Report_List = this.getReportTemplet(Items[i].Mb_intClfs == (int)CLDC_Comm.Enum.Cus_Clfs.单相 ? true : false, Items[i], ReportTao, ReportType);

                if (Report_List == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("没有对应的报表模板，无法完成打印操作...", "打印失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }

                string[] Templet_Arr = Report_List.Split(',');

                if (Templet_Arr.Length < 2)
                {
                    System.Windows.Forms.MessageBox.Show("模板配置错误，无法完成打印操作...", "打印失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }

                if (!System.IO.File.Exists(string.Format(@"{0}\{1}", clsMain.getFilePath("Res"), Templet_Arr[0])))
                {
                    System.Windows.Forms.MessageBox.Show(string.Format(@"打开模板出现一个致命错误，没有找到对应 {0}\Res\{1}的模板文件...", ResRootPath, Templet_Arr[0]), "打印失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }

                //第一步：加载dot文件 
                documentManager = new WordTemplateParser.DocumentManager();
                string templatePata = string.Format(@"{0}\{1}", clsMain.getFilePath("Res"), Templet_Arr[0]);
                bool loadRet = documentManager.LoadTemplate(templatePata);
                if (!loadRet)
                {
                    System.Windows.Forms.MessageBox.Show("加载报表模板文件[Dotx]文件失败，请确认模板是07格式", "打印失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
                CLDC_DataCore.Function.TopWaiting.HideWaiting();
                CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在准备生成报表.....");

                for (int Int_Mb = 1; Int_Mb < Templet_Arr.Length; Int_Mb++)
                {

                    WordTemplateParser.WordProcess
                        templateProcess = ProcessOneDocument(string.Format("{0}\\{1}", clsMain.getFilePath("Res"), Templet_Arr[Int_Mb])
                                                                                        , DicItems
                                                                                        , i);
                    //把解析好的模板文档添加到文档管理器中
                    documentManager.AddContent(templateProcess);
                }
                //

                documentManager.SaveDocument();

                if (PrintInfo.Saving)           //如果需要存盘
                {
                    DateTime date = DateTime.Now;
                    DateTime.TryParse(Items[i].Mb_DatJdrq, out date);
                    string savePath = string.Format(@"{0}\{1}.docx", this.SavePath(date), Items[i].Mb_ChrTxm + "_" + PrintTypeName);
                    documentManager.SaveAs(savePath);
                }

                if (PrintInfo.Preview)
                {
                    documentManager.ShowFileContent();
                }
            }
            CLDC_DataCore.Function.TopWaiting.HideWaiting();
        }

        /// <summary>
        /// 解析一个文档
        /// </summary>
        /// <param name="templatePath">模板路径</param>
        /// <param name="Items">电能表数据</param>
        /// <param name="desc">当前序号</param>
        /// <returns>已经处理过的文档</returns>
        private WordTemplateParser.WordProcess ProcessOneDocument(string templatePath, List<Dictionary<PrintDataType, object>> Items, int desc)
        {
            //建立一个文档处理对象
            WordTemplateParser.WordProcess process = new WordTemplateParser.WordProcess();
            //加载模板文件
            process.Open(templatePath, true);
            //解析所有模板
            process.ParserBookMarks();
            //获取书签
            WordTemplateParser.Structs.BookMark bookMark = process.Get();
            while (bookMark != null)
            {
                //设置书签的值
                SetBookMarkValue(bookMark, Items, desc);
                //获取下一个书签
                bookMark = process.Get();
            }
            return process;
        }
        //解析一只表数据标签
        private WordTemplateParser.WordProcess ProcessOneDocument(string templatePath, Dictionary<PrintDataType, object> Item)
        {
            //建立一个文档处理对象
            WordTemplateParser.WordProcess process = new WordTemplateParser.WordProcess();
            //加载模板文件
            process.Open(templatePath, true);
            //解析所有模板
            process.ParserBookMarks();
            //获取书签
            WordTemplateParser.Structs.BookMark bookMark = process.Get();
            while (bookMark != null)
            {
                //设置书签的值
                SetBookMarkValue(bookMark, Item);
                //获取下一个书签
                bookMark = process.Get();
            }
            return process;
        }

        /// <summary>
        /// 设置书签的值
        /// </summary>
        /// <param name="bookMark">书签节点</param>
        private void SetBookMarkValue(WordTemplateParser.Structs.BookMark bookMark, List<Dictionary<PrintDataType, object>> Items, int desc)
        {
            Dictionary<PrintDataType, object> currentMeter = Items[desc];      //获取当前表对象
            if (bookMark == null || string.IsNullOrEmpty(bookMark.Text.ToLower())) return;
            string[] arrMarkValue = bookMark.Text.ToLower().Split(':');                           //标签每个元素间用:隔开
            if (arrMarkValue.Length == 6 && arrMarkValue[0] == "loop")
            {
                WordTemplateParser.WordProcess loopMarkParser = null;
                if (arrMarkValue[1] == "meter")
                    loopMarkParser = ParserLoopMarkMeter(Items, arrMarkValue);
                else
                    loopMarkParser = ParserLoopMarkMeterData(currentMeter, arrMarkValue);
                //回填解析后的文档
                if (loopMarkParser != null)
                    bookMark.SetContentValue(loopMarkParser);
                else
                    bookMark.SetValue("Invalid BookMark:" + bookMark.Text);
                return;
            }
            //TODO:处理非对象标签
            SetBookMarkValue(bookMark, currentMeter);
        }

        private void SetBookMarkValue(WordTemplateParser.Structs.BookMark bookMark, Dictionary<PrintDataType, object> Item)
        {
            if (bookMark == null || string.IsNullOrEmpty(bookMark.Text.ToLower())) return;
            string TmpValue = "";
            string Tmp_Label = bookMark.Text.ToLower();
            string[] Arr_Tmp = Tmp_Label.Split(':');                           //标签每个元素间用:隔开
            //TODO:解析标签
            switch (Arr_Tmp.Length)
            {
                case 1:             //书签文本格式只有一级表示为基本信息"meterid"

                    if (Arr_Tmp[0].ToLower() == "checker" || Arr_Tmp[0].ToLower() == "verificationer")
                    {
                        if (PrintInfo.PrintHuman)
                        {
                            TmpValue = this.GetColValue(PrintDataType.基本信息, Item[PrintDataType.基本信息], Arr_Tmp[0].ToLower());
                        }
                    }
                    else
                    {
                        TmpValue = this.GetColValue(PrintDataType.基本信息, Item[PrintDataType.基本信息], Arr_Tmp[0].ToLower());
                    }
                    break;
                case 2:             //如果有2级，表示为多功能信息“Dgn:通信测试”
                    TmpValue = this.GetColValue(PrintDataType.多功能数据, Item[PrintDataType.多功能数据], Arr_Tmp[1].ToLower());
                    break;
                case 3:             //如果有3级，表示为误差信息"P+:H10_Imax:WcHzz"
                    TmpValue = this.GetColValue(PrintDataType.误差数据, Item[PrintDataType.误差数据], Tmp_Label, clsMain.GetWcNum(Arr_Tmp[2], PrintInfo.PrintStyle, PrintTypeName));
                    break;
                case 4:         //走字数据"ZZDATA:P+:FL:FL(FL\Qm\Zm\Wc\Result\Mc\zhwc\ZhResult)"
                    if (Arr_Tmp[0].ToLower() == "zzdata")
                    {
                        TmpValue = this.GetColValue(PrintDataType.走字数据, Item[PrintDataType.走字数据], Tmp_Label);
                    }
                    break;

            }

            if (TmpValue == "")
            {
                TmpValue = PrintInfo.NotCheck;
            }
            bookMark.SetValue(TmpValue);
        }

        /// <summary>
        /// 解析循环标签[表循环]
        /// </summary>
        /// <param name="Items"></param>
        /// <param name="arrMarkValue"></param>
        /// <returns></returns>
        private WordTemplateParser.WordProcess ParserLoopMarkMeter(List<Dictionary<PrintDataType, object>> Items, string[] arrMarkValue)
        {
            //arrMarkValue.Length=6 loop:meter:num;header:body:footer
            if (arrMarkValue.Length != 6) return null;
            int loopNum = 0;                              //每页数量
            int parserNum = 0;                          //已经解析的数量
            int.TryParse(arrMarkValue[2], out loopNum);
            string bodyFilePath = string.Format("{0}\\Res\\{1}", ResRootPath, arrMarkValue[4]);
            if (!string.IsNullOrEmpty(arrMarkValue[4])) return null;      //没有主体，直接退出
            //开始循环处理每一块表
            WordTemplateParser.WordProcess meterLoopParser = null;      // new WordTemplateParser.WordProcess();
            WordTemplateParser.WordProcess headerParser = null;         //
            WordTemplateParser.WordProcess bodyParser = null;           //
            WordTemplateParser.WordProcess footerParser = null;         //
            if (!string.IsNullOrEmpty(arrMarkValue[3]))
            {
                //解析头
                headerParser = new WordTemplateParser.WordProcess();
                string headerFielPath = string.Format("{0}\\Res\\{1}", ResRootPath, arrMarkValue[3]);
                headerParser.Open(headerFielPath, true);//循环头部分不进行解析。确认循环头部分不应该包括标签 
                //
                meterLoopParser = new WordTemplateParser.WordProcess();
                meterLoopParser.Open(headerFielPath, true);
            }
            if (!string.IsNullOrEmpty(arrMarkValue[5]))
            {
                //解析脚
                headerParser = new WordTemplateParser.WordProcess();
                string footerFilePath = string.Format("{0}\\Res\\{1}", ResRootPath, arrMarkValue[5]);
                headerParser.Open(footerFilePath, true);//循环头部分不进行解析。确认循环头部分不应该包括标签 
            }
            //检测母板有没有加载
            if (meterLoopParser == null)
            {
                //无头,直接加载第一块表的循环数据
                meterLoopParser = ProcessOneDocument(bodyFilePath, Items, parserNum);
                parserNum++;
            }

            while (parserNum < Items.Count)
            {
                if (parserNum % loopNum == 0 && parserNum > 0)
                {
                    //处理分页
                    if (footerParser != null)
                        meterLoopParser.AddContent(footerParser);
                    //加第二页表头
                    if (headerParser != null)
                        meterLoopParser.AddContent(headerParser);
                }
                else
                {
                    //解析循环体
                    bodyParser = ProcessOneDocument(bodyFilePath, Items, parserNum);
                    meterLoopParser.AddContent(bodyParser);
                }
                parserNum++;
            }
            meterLoopParser.MegerDocument(string.Empty);
            return meterLoopParser;
        }

        /// <summary>
        /// 解析循环标签【单表数据】
        /// </summary>
        /// <param name="Item">当前电能表数据对象</param>
        /// <param name="arrMarkValue">标签文本</param>
        /// <returns>解析后的文档对象</returns>
        private WordTemplateParser.WordProcess ParserLoopMarkMeterData(Dictionary<PrintDataType, object> Item, string[] arrMarkValue)
        {
            if (arrMarkValue.Length != 6) return null;
            int loopNum = 0;                              //每页数量
            int parserNum = 0;                              //已经解析的数量
            int.TryParse(arrMarkValue[2], out loopNum);
            string bodyFilePath = string.Format("{0}\\Res\\{1}.docx", ResRootPath, arrMarkValue[4]);
            if (string.IsNullOrEmpty(arrMarkValue[4])) return null;      //没有主体，直接退出
            //开始循环处理
            WordTemplateParser.WordProcess meterLoopParser = new WordTemplateParser.WordProcess();      // new WordTemplateParser.WordProcess();
            if (!meterLoopParser.Create()) return null;
            WordTemplateParser.WordProcess headerParser = null;         //
            WordTemplateParser.WordProcess bodyParser = null;           //
            WordTemplateParser.WordProcess footerParser = null;         //
            if (!string.IsNullOrEmpty(arrMarkValue[3]))
            {
                //解析头
                headerParser = new WordTemplateParser.WordProcess();
                string headerFielPath = string.Format("{0}\\Res\\{1}.docx", ResRootPath, arrMarkValue[3]);
                headerParser.Open(headerFielPath, true);//循环头部分不进行解析。确认循环头部分不应该包括标签 
            }
            if (!string.IsNullOrEmpty(arrMarkValue[5]))
            {
                //解析脚
                footerParser = new WordTemplateParser.WordProcess();
                string footerFilePath = string.Format("{0}\\Res\\{1}.docx", ResRootPath, arrMarkValue[5]);
                footerParser.Open(footerFilePath, true);//循环头部分不进行解析。确认循环头部分不应该包括标签 
            }

            

            while (parserNum <1 )
            {
                if (parserNum % loopNum == 0)
                {
                    if(parserNum==0)
                    //处理分页
                    if (footerParser != null)
                        meterLoopParser.AddContent(footerParser);
                    //加第二页表头
                    if (headerParser != null)
                        meterLoopParser.AddContent(headerParser);
                }
                else
                {
                    //解析循环体
                    bodyParser = ProcessOneDocument(bodyFilePath, Item);
                    meterLoopParser.AddContent(bodyParser);
                }
                parserNum++;
            }
            if (footerParser != null)
                meterLoopParser.AddContent(footerParser);
            meterLoopParser.MegerDocument(string.Empty);
            return meterLoopParser;
        }

        /// <summary>
        /// 获取报表套型种类
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> ReportTaoXing()
        {
            int intSum = int.Parse(clsMain.getIniString("TaoName", "NameSum", "0", iniPath));

            for (int i = 0; i < intSum; i++)
            {
                yield return clsMain.getIniString("TaoName", string.Format("Name_{0}", i + 1), "", iniPath);
            }
        }
        /// <summary>
        /// 根据报表套型获取对应的模板配置
        /// </summary>
        /// <param name="taoxing"></param>
        /// <returns></returns>
        public string[] ReportType(int taoxing)
        {
            string typestring = clsMain.getIniString(string.Format("Type_{0}", taoxing), "TypeName", "", iniPath);

            if (typestring == "") return new string[0];

            return typestring.Split(',');
        }

        public void ShowPanel(CLDC_DataCore.Interfaces.IControlPanel panel)
        {
            panel.Save += new EventHandler(panel_Save);

            Dictionary<string, string> Items = new Dictionary<string, string>();

            Items.Add("Report_Info", "报表信息设置");

            Items.Add("Report_Set", "报表模板配置");

            System.Windows.Forms.TabPage[] tabs = panel.tabPages(Items);

            tabs[0].Controls.Clear();

            uiReportInfo = new CLReport_Standard.UI.UI_ReportInfo();

            tabs[0].Controls.Add(uiReportInfo);

            uiReportInfo.Dock = System.Windows.Forms.DockStyle.Fill;

            uiReportInfo.Margin = new System.Windows.Forms.Padding();

            tabs[1].Controls.Clear();

            uiReportSet = new CLReport_Standard.UI.UI_ReportSet();

            tabs[1].Controls.Add(uiReportSet);

            uiReportSet.Dock = System.Windows.Forms.DockStyle.Fill;

            uiReportSet.Margin = new System.Windows.Forms.Padding();
        }
        private void panel_Save(object sender, EventArgs e)
        {
            uiReportInfo.Save();
        }
        #endregion

        #region -----------------------------------数据解析-----------------------------
        /// <summary>
        /// 获取书签对应关键字数据
        /// </summary>
        /// <param name="DataType">数据类型</param>
        /// <param name="Items">数据对象</param>
        /// <param name="KeyValue">关键字</param>
        /// <returns></returns>
        private string GetColValue(PrintDataType DataType, object Items, string KeyValue)
        {
            return this.GetColValue(DataType, Items, KeyValue, 0);
        }


        /// <summary>
        /// 获取书签对应关键字数据
        /// </summary>
        /// <param name="DataType">数据类型</param>
        /// <param name="Items">数据对象</param>
        /// <param name="KeyValue">关键字</param>
        /// <param name="WcNum">误差次数（0=一次误差，1=二次误差。。。。。-1=误差平均值，-2=误差化整值）</param>
        /// <returns></returns>
        private string GetColValue(PrintDataType DataType, object Items, string KeyValue, int WcNum)
        {
            try
            {
                switch (DataType)
                {
                    case PrintDataType.基本信息:
                        if (!((Dictionary<string, string>)Items).ContainsKey(KeyValue))
                        {
                            clsMain.WriteReportErr(2, string.Format("错误的基本信息关键字：{0}", KeyValue));
                            return "";
                        }
                        return ((Dictionary<string, string>)Items)[KeyValue];
                    case PrintDataType.误差数据:


                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError errorItem = clsMain.getErrorItem((Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError>)Items, KeyValue);

                        if (errorItem == null)
                        {
                            clsMain.WriteReportErr(2, string.Format("错误的误差数据关键字：{0}", KeyValue));
                            return "";
                        }

                        string[] WcArr = errorItem.Me_chrWcMore.Split('|');
                        if (WcNum > WcArr.Length - 2)
                        {
                            clsMain.WriteReportErr(3, string.Format("错误的误差次数关键字：{0}", WcNum));
                            return "";
                        }
                        string WcString = "";
                        if (WcNum == -1)       //平均值
                        {
                            WcString = WcArr[WcArr.Length - 2];
                        }
                        else if (WcNum == -2)      //化整值
                        {
                            ///区分0.5s级 以及1级表以上的区别
                            
                            WcString = WcArr[WcArr.Length - 1];
                        }
                        else
                        {
                            WcString = WcArr[WcNum];
                        }
                        return WcString;
                    case PrintDataType.多功能数据:
                        if (((Dictionary<string, string>)Items).Count == 0) return "";
                        if (!((Dictionary<string, string>)Items).ContainsKey(KeyValue))
                        {
                            clsMain.WriteReportErr(2, string.Format("错误的多功能数据关键字：{0}", KeyValue));
                            return "";
                        }
                        return ((Dictionary<string, string>)Items)[KeyValue];
                    case PrintDataType.走字数据:               //"ZZDATA:P+:FL:FL(FL\Qm\Zm\Wc\Result\Mc\zhwc\ZhResult)"
                        string[] Arr_Key = KeyValue.Split(':');
                        Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError> ZzData = Items as Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError>;

                        int Glfx = clsMain.GetGlfxNum(Arr_Key[1]);

                        int FeiLv = clsMain.GetFeiLvNum(Arr_Key[2]);

                        foreach (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError ZZError in ZzData.Values)
                        {
                            if (ZZError.Mz_chrJdfx == Glfx.ToString() && ZZError.Mz_chrFl == FeiLv.ToString())
                            {
                                switch (Arr_Key[3].ToLower())
                                {
                                    case "fl": return Arr_Key[2];
                                    case "qm": return ZZError.Mz_chrQiMa.ToString();
                                    case "zm": return ZZError.Mz_chrZiMa.ToString();
                                    case "wc": return ZZError.Mz_chrWc;
                                    case "result": return ZZError.Mz_chrJL;
                                    case "mc": return ZZError.Mz_chrQiZiMaC;
                                    default:
                                        return "";
                                }
                            }
                        }
                        return "";
                    default:
                        return "";
                }
            }
            catch (Exception e)
            {
                clsMain.WriteReportErr(1, e.StackTrace);
                return "";
            }

        }
        #endregion

        #region------------------------字典制作-----------------------------
        /// <summary>
        /// 基本信息字典中添加其他相关信息
        /// </summary>
        /// <param name="BasicInfo"></param>
        private void AddDWSetting(Dictionary<string, string> BasicInfo)
        {
            BasicInfo.Add("jdyj", this.chrJdyj);        //检定依据
            BasicInfo.Add("zzbz", this.chrZzbz);        //制造标准
            BasicInfo.Add("rpthead", clsMain.getIniString("ReportInfo", "Head"));           //报表头名称
            BasicInfo.Add("checkadr", clsMain.getIniString("ReportInfo", "CheckAdr"));      //检测地点
            BasicInfo.Add("adr", clsMain.getIniString("ReportInfo", "adr"));                //单位地址
            BasicInfo.Add("tel", clsMain.getIniString("ReportInfo", "tel"));                //联系电话
            BasicInfo.Add("erpthead", clsMain.getIniString("ReportInfo", "ehead"));         //引文报表头
            BasicInfo.Add("fax", clsMain.getIniString("ReportInfo", "tex"));                //传真
            BasicInfo.Add("mail", clsMain.getIniString("ReportInfo", "email"));             //邮件
            BasicInfo.Add("post", clsMain.getIniString("ReportInfo", "zip"));               //邮编
            BasicInfo.Add("sqzs", clsMain.getIniString("ReportInfo", "num"));                //授权证书
            BasicInfo.Add("sqdw", clsMain.getIniString("ReportInfo", "dw"));                //授权单位
            BasicInfo.Add("pagehead", clsMain.getIniString("ReportInfo", "pagehead"));      //页眉
            if (CLDC_DataCore.Function.DateTimes.IsDate(BasicInfo["checkdate"].ToString()))
                BasicInfo.Add("formatcheckdate", DateTime.Parse(BasicInfo["checkdate"]).ToString("yyyy   年   MM   月    dd    日"));
            else
                BasicInfo.Add("formatcheckdate", DateTime.Now.ToString());
            if (CLDC_DataCore.Function.DateTimes.IsDate(BasicInfo["jjdate"]))
                BasicInfo.Add("formatjjdate", DateTime.Parse(BasicInfo["jjdate"]).ToString("yyyy   年   MM   月    dd    日"));
            else
                BasicInfo.Add("formatjjdate", DateTime.Now.ToString());
            if (CLDC_DataCore.Function.DateTimes.IsDate(BasicInfo["checkdate"]))
                BasicInfo.Add("duancheckdate", DateTime.Parse(BasicInfo["checkdate"]).ToString("yyyy年MM月dd日"));
            else
                BasicInfo.Add("duancheckdate", DateTime.Now.ToString());
            if (CLDC_DataCore.Function.DateTimes.IsDate(BasicInfo["jjdate"]))
                BasicInfo.Add("duanjjdate", DateTime.Parse(BasicInfo["jjdate"]).ToString("yyyy年MM月dd日"));
            else
                BasicInfo.Add("duanjjdate", DateTime.Now.ToString());
            string Tmp_Value = ((CLDC_Comm.Enum.Cus_Clfs)int.Parse(BasicInfo["checktype"])).ToString();

            BasicInfo.Add("p_xiangxian", Tmp_Value);

            Tmp_Value = BasicInfo["znq"];

            BasicInfo.Remove("znq");
            if (Tmp_Value == "1")
            {
                BasicInfo.Add("znq", "有止逆");
            }
            else
            {
                BasicInfo.Add("znq", "无止逆");
            }
            Tmp_Value = BasicInfo["hgq"];

            BasicInfo.Remove("hgq");
            if (Tmp_Value == "1")
            {
                BasicInfo.Add("hgq", "经互感器");
            }
            else
            {
                BasicInfo.Add("hgq", "直接接入");
            }

            BasicInfo.Add("yglevel", CLDC_DataCore.Function.Number.getDj(BasicInfo["meterlevel"])[0]);   //有功等级
            BasicInfo.Add("wglevel", CLDC_DataCore.Function.Number.getDj(BasicInfo["meterlevel"])[1]);   //无功等级
            string sTrConstDw = "";
            if (BasicInfo["metertype"].IndexOf("感应") >= 0 || BasicInfo["metertype"].IndexOf("机电") >= 0 || BasicInfo["metertype"].IndexOf("机械") >= 0)
            {
                sTrConstDw = "r";
            }
            else
            {
                sTrConstDw = "imp";
            }

            BasicInfo.Add("ygconst", string.Format("{0} {1}/kWh", CLDC_DataCore.Function.Number.GetBcs(BasicInfo["meterconst"], true), sTrConstDw));  //有功常数
            BasicInfo.Add("wgconst", string.Format("{0} {1}/kvarh", CLDC_DataCore.Function.Number.GetBcs(BasicInfo["meterconst"], false), sTrConstDw));      //无功常数

        }


        /// <summary>
        /// 获取基本信息和结论信息字典
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        private Dictionary<string, string> DicBasicInfo(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item)
        {
            System.Reflection.FieldInfo[] InfoTypes = Item.GetType().GetFields();
            Dictionary<string, string> DicInfo = new Dictionary<string, string>();
            for (int i = 0; i < InfoTypes.Length; i++)              //遍历基本信息表
            {
                string sTrKey = clsMain.getIniString("V90", InfoTypes[i].Name, "", clsMain.getFilePath("ReportConfig.ini"));

                if (sTrKey == string.Empty) continue;

                DicInfo.Add(sTrKey.ToLower(), InfoTypes[i].GetValue(Item).ToString());
            }

            foreach (string key in Item.MeterExtend.Keys)           //遍历扩展表
            {
                string sTrKey = clsMain.getIniString("V90", key, "", clsMain.getFilePath("ReportConfig.ini"));
                if (sTrKey == string.Empty) continue;
                DicInfo.Add(sTrKey.ToLower(), Item.MeterExtend[key]);
            }


            if (Item.MeterExtend.ContainsKey("StdMeterInfo"))
            {
                string[] arrtmp = Item.MeterExtend["StdMeterInfo"].Split('|');
                if (arrtmp.Length >= 8)
                {
                    DicInfo.Add("bzbname", arrtmp[0]);    //标准表名称
                    DicInfo.Add("bzbtype", arrtmp[1]);    //标准表类型
                    DicInfo.Add("bzbno", arrtmp[7]);    //标准表编号
                    DicInfo.Add("bzbclfw", string.Format("{0}—{1}", arrtmp[3], arrtmp[4]));    //标准表测量范围
                    DicInfo.Add("bzbzqd", arrtmp[5]);    //标准表等级
                    DicInfo.Add("bzbzsbh", arrtmp[2]);    //标准表证书
                    DicInfo.Add("bzbyxq", arrtmp[6]);    //标准表有效期
                }
            }

            if (Item.MeterExtend.ContainsKey("StdSetInfo"))
            {
                string[] arrtmp = Item.MeterExtend["StdSetInfo"].Split('|');
                if (arrtmp.Length >= 8)
                {
                    DicInfo.Add("setname", arrtmp[0]);    //装置名称
                    DicInfo.Add("settype", arrtmp[1]);    //装置类型
                    DicInfo.Add("setno", arrtmp[7]);    //装置编号
                    DicInfo.Add("setclfw", string.Format("{0}—{1}", arrtmp[3], arrtmp[4]));    //装置测量范围
                    DicInfo.Add("setzqd", arrtmp[5]);    //装置等级
                    DicInfo.Add("setzsbh", arrtmp[2]);    //装置证书
                    DicInfo.Add("setyxq", arrtmp[6]);    //装置有效期
                }
            }

            foreach (string Key in Item.MeterResults.Keys)
            {
                string sTrKey = clsMain.getIniString("V90Result", "R_" + Key, "", clsMain.getFilePath("ReportConfig.ini"));

                if (sTrKey == string.Empty) continue;
                string[] Arr_Tmp = sTrKey.Split(':');
                if (Arr_Tmp.Length > 1) //大于1就是启动信息
                {
                    DicInfo.Add(Arr_Tmp[0].ToLower(), Item.MeterResults[Key].Mr_chrRstValue);
                    //DicInfo.Add(Arr_Tmp[1].ToLower(), Item.MeterResults[Key].Mr_Time + "分");
                    //DicInfo.Add(Arr_Tmp[2].ToLower(), Item.MeterResults[Key].Mr_Current + "A");
                }
                else
                {
                    DicInfo.Add(sTrKey.ToLower(), Item.MeterResults[Key].Mr_chrRstValue);
                }
            }
            return DicInfo;
        }
        /// <summary>
        /// 获取多功能数据字典
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        private Dictionary<string, string> DicDgnInfo(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item)
        {
            Dictionary<string, string> DgnDic = new Dictionary<string, string>();

            foreach (string Key in Item.MeterDgns.Keys)
            {
                string sTrKey = clsMain.getIniString("V90Dgn", "D_" + Key, "", clsMain.getFilePath("ReportConfig.ini"));
                if (sTrKey == string.Empty) continue;
                string[] Arr_Tmp = sTrKey.Split(',');
                string[] Arr_Value = Item.MeterDgns[Key].Md_chrValue.Split('|');
                for (int i = 0; i < Arr_Tmp.Length; i++)
                {
                    try
                    {
                        DgnDic.Add(Arr_Tmp[i].ToLower(), Arr_Value[i]);
                    }
                    catch
                    { }
                }
            }
            return DgnDic;

        }

        #endregion

        #region-------------------模板获取----------------------
        /// <summary>
        /// 获取模板数组
        /// </summary>
        /// <param name="DxYn">是否是单相表</param>
        /// <param name="MeterInfo">表信息</param>
        /// <param name="ReportTao">模板套型</param>
        /// <param name="ReportType">模板类型（证书。原始记录。。。。）</param>
        /// <returns></returns>
        private string getReportTemplet(bool DxYn, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, int ReportTao, int ReportType)
        {
            int[] intType = new int[3];

            intType[2] = ReportType;

            bool blnPz = MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString() + ((int)CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功).ToString());

            bool blnPf = MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString() + ((int)CLDC_Comm.Enum.Cus_PowerFangXiang.反向有功).ToString());

            bool blnQz = MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString() + ((int)CLDC_Comm.Enum.Cus_PowerFangXiang.正向无功).ToString());

            bool blnQf = MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.基本误差试验).ToString() + ((int)CLDC_Comm.Enum.Cus_PowerFangXiang.反向无功).ToString());


            if (DxYn)
            {
                intType[1] = 6;
            }
            else if ((blnPz && blnPf && blnQz && blnQf) || MeterInfo.Mb_chrBlx.IndexOf("多功能") >= 0)   //多功能
            {
                intType[1] = 5;
            }
            else if (blnPz && blnQz)        //二合一
            {
                intType[1] = 4;
            }
            else if (blnQz && blnQf)        //双向无功
            {
                intType[1] = 3;
            }
            else if (blnPz && blnPf)        //双向有功
            {
                intType[1] = 2;
            }
            else if (blnQz)                 //普通有，无功
            {
                intType[1] = 1;     //无功
            }
            else
            {
                intType[1] = 0;     //有功
            }

            if (MeterInfo.Mb_chrBlx.IndexOf("机械") >= 0 || MeterInfo.Mb_chrBlx.IndexOf("机电") >= 0 || MeterInfo.Mb_chrBlx.IndexOf("感应") >= 0)
            {
                intType[0] = 1;
            }
            else
            {
                intType[0] = 0;
            }

            string sTrTemplet = clsMain.getIniString("PrintType_" + ReportTao.ToString(),
                                        string.Format("{0}_{1}_{2}", intType[0], intType[1], intType[2]),
                                        "",
                                        iniPath);
            return sTrTemplet;



        }
        #endregion

        #region------------存档路径------------------
        /// <summary>
        /// 获取存档路径
        /// </summary>
        /// <param name="JdDate"></param>
        /// <returns></returns>
        private string SavePath(DateTime JdDate)
        {
            string Path = PrintInfo.SavePath;
            if (Path == "") Path = "ReportData";
            if (!System.IO.Directory.Exists(Path))
            {
                System.IO.Directory.CreateDirectory(Path);
            }
            switch (PrintInfo.SaveBag)
            {
                case 0:         //天
                    Path += string.Format(@"\{0}", JdDate.ToString("yyyyMMdd"));
                    if (!System.IO.Directory.Exists(Path))
                    {
                        System.IO.Directory.CreateDirectory(Path);
                    }
                    break;
                case 1:         //月
                    Path += string.Format(@"\{0}", JdDate.ToString("yyyyMM"));
                    if (!System.IO.Directory.Exists(Path))
                    {
                        System.IO.Directory.CreateDirectory(Path);
                    }
                    break;
                case 2:         //年
                    Path += string.Format(@"\{0}", JdDate.ToString("yyyy"));
                    if (!System.IO.Directory.Exists(Path))
                    {
                        System.IO.Directory.CreateDirectory(Path);
                    }
                    break;
            }
            return Path;
        }

        #endregion

    }
}

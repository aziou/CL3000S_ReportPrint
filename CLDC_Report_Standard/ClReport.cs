using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Windows.Forms;

namespace CLReport_Standard
{
    /// <summary>
    /// 报表打印组件
    /// </summary>
    public class ClReport : CLDC_DataCore.Interfaces.IReportInterface
    {
        /// <summary>
        /// 是否是一页多条
        /// </summary>
        private bool blnColIsAll = false;

        private string iniPath = clsMain.getFilePath(@"Res\Templet.ini");
        
        private clsWordControl WordApp = null;

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
        /// 检验
        /// </summary>
        private string CheckerName;

        /// <summary>
        /// 核验
        /// </summary>
        private string TesterName;

        /// <summary>
        /// zhuguan
        /// </summary>
        private string chargeName;

        /// <summary>
        /// 数据打印类型
        /// </summary>
        private enum PrintDataType
        {
            基本信息 = 1,
            误差数据 = 2,
            多功能数据 = 3,
            走字数据 = 4,
            特殊检定数据 = 5,
            潜动起动数据 = 6,
            载波检定数据 = 7,
            误差一致性数据 = 8,
            功耗数据 = 9,
            费控功能数据 = 10,
            智能表功能数据 = 11,
            事件记录数据 = 12,
            冻结功能数据 = 13,
            南网费控软件数据=14,
        }

        /// <summary>
        /// 有功等级
        /// </summary>
        private int MeterYGlevel;

        public ClReport()
        {            
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
        ~ClReport()
        {
            if(WordApp != null)
                WordApp.Quit();
            WordApp = null;
        }

        #region IReportInterface 成员

        //public void PrintRpt(List<CLDC_Comm.Model.DnbModel.DnbInfo.MeterBasicInfo> Items, int ReportTao, int ReportType, string Jdyj, string zzbz)
        //{
        //    //throw new NotImplementedException();
        //}

        public string[] ReportType(int taoxing)
        {
            string typestring = clsMain.getIniString(string.Format("Type_{0}", taoxing), "TypeName", "", iniPath);

            if (typestring == "") return new string[0];

            return typestring.Split(',');

        }

        public IEnumerable<string> ReportTaoXing()
        {
            int intSum = int.Parse(clsMain.getIniString("TaoName", "NameSum", "0", iniPath));

            for (int i = 0; i < intSum; i++)
            {
                yield return clsMain.getIniString("TaoName", string.Format("Name_{0}", i + 1), "", iniPath);
            }

        }
        /// <summary>
        /// 在装载对象给出的panel上填充UI
        /// </summary>
        /// <param name="panel"></param>
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

        /// <summary>
        /// 报表打印函数(入口函数)
        /// </summary>
        /// <param name="Items">电能表集合</param>
        /// <param name="ReportTao">模板套型</param>
        /// <param name="ReportType">打印类型（证书，原始记录。。。。）</param>
        public void PrintRpt(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items, int ReportTao, int ReportType, string Jdyj, string zzbz)
        {
            //获取打印类型名称
            PrintTypeName = clsMain.getIniString(string.Format("Type_{0}", ReportTao), "TypeName", "", iniPath).Split(',')[ReportType];

            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在解析数据,请稍后.....");

            List<Dictionary<PrintDataType, object>> DicItems = new List<Dictionary<PrintDataType, object>>();
            this.chrJdyj = Jdyj; this.chrZzbz = zzbz;
            for (int i = 0; i < Items.Count; i++)
            {

                Dictionary<PrintDataType, object> DataItem = new Dictionary<PrintDataType, object>();

                DataItem.Add(PrintDataType.基本信息, DicBasicInfo(Items[i]));       //基本信息
                DataItem.Add(PrintDataType.误差数据, Items[i].MeterErrors);         //误差数据
                DataItem.Add(PrintDataType.多功能数据, GetDicDgnInfo(Items[i]));         //多功能数据
                DataItem.Add(PrintDataType.走字数据, Items[i].MeterZZErrors);       //走字数据
                DataItem.Add(PrintDataType.特殊检定数据, Items[i].MeterSpecialErrs);    //特殊检定数据--影响量
                DataItem.Add(PrintDataType.潜动起动数据,Items[i].MeterQdQids);    //起动潜动检定数据
                DataItem.Add(PrintDataType.功耗数据, Items[i].MeterPowers);    //功耗数据数据
                DataItem.Add(PrintDataType.误差一致性数据, Items[i].MeterConsistencys);    //功耗数据数据
                DataItem.Add(PrintDataType.冻结功能数据, Items[i].MeterFreezes);    //冻结功能数据
                DataItem.Add(PrintDataType.费控功能数据, Items[i].MeterCostControls);    //费控功能数据
                DataItem.Add(PrintDataType.事件记录数据, Items[i].MeterSjJLgns);    //事件记录数据
                DataItem.Add(PrintDataType.载波检定数据, Items[i].MeterCarrierDatas);    //载波检定数据
                DataItem.Add(PrintDataType.智能表功能数据, Items[i].MeterFunctions);    //智能表功能数据
                DataItem.Add(PrintDataType.南网费控软件数据,GetNwSoftInfo(Items[i]));//添加南网费控软件的数据
                this.AddDWSetting((Dictionary<string, string>)DataItem[PrintDataType.基本信息]);  //加入其它相关信息

                DicItems.Add(DataItem);

            }



            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在生成报表,请稍后.....");

            string saveTmpPath="";
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
                    string folderPrintReport = Application.StartupPath + @"\Plugins";

                    clsMain.WriteIni("Print", "METERID", Items[i]._intMyId.ToString(), folderPrintReport + "\\PrintConfig.ini");

                    string stdFile = Application.StartupPath + @"\Plugins\" + Templet_Arr[0];

                    string strfolderStdReport = Application.StartupPath + @"\Plugins\Res";
                    System.IO.DirectoryInfo TheFolder = new System.IO.DirectoryInfo(strfolderStdReport);
                    System.IO.FileInfo[] files = TheFolder.GetFiles();
                    for (int intInc = 0; intInc < files.Length; intInc++)
                    {
                        System.IO.FileInfo NextFile = files[intInc];
                        if (NextFile.Name == Templet_Arr[0])
                        {
                            NextFile.CopyTo(stdFile, true);
                            System.Diagnostics.Process.Start(stdFile);
                            break;
                        }
                    }

                    //Microsoft.Office.Interop.Word.Document Word_Tmp = WordApp.LoadDot(string.Format(@"{0}\{1}", clsMain.getFilePath("Res"), Templet_Arr[0]));

                    //WordApp.Add(Word_Tmp);

                    //WordApp.SaveDoc(Word_Tmp, clsMain.getFilePath("") + @"\TmpDoc.doc");

                    //WordApp.WordApplication.Visible = true;

                    //WordApp.ClearDocCol();                //清除文档集合

                    return;
                    //System.Windows.Forms.MessageBox.Show("模板配置错误，无法完成打印操作...", "打印失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //return;
                }
                string GenaraPath = "";

                if (!System.IO.File.Exists(string.Format(@"{0}\{1}", clsMain.getFilePath("Res"), Templet_Arr[0])))
                {
                    System.Windows.Forms.MessageBox.Show(string.Format(@"打开模板出现一个致命错误，没有找到对应 {0}\{1}的模板文件...", clsMain.getFilePath("Res"), Templet_Arr[0]), "打印失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
                WordApp = new clsWordControl();//创建word
                Microsoft.Office.Interop.Word.Document Word_TmpDoc = WordApp.LoadDot(string.Format(@"{0}\{1}", clsMain.getFilePath("Res"), Templet_Arr[0]));
                GenaraPath = string.Format(@"{0}\{1}", clsMain.getFilePath("Res"), Templet_Arr[0]);
                

                for (int Int_Mb = 1; Int_Mb < Templet_Arr.Length; Int_Mb++)
                {
                    CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在准备生成报表.....");

                    Microsoft.Office.Interop.Word.Document WordDoc = GetReportWord(Templet_Arr[Int_Mb], DicItems, i, Int_Mb, Templet_Arr.Length - 1);          //获取解析后的模板

                    if (WordDoc == null) return;

                    WordApp.PasteWord(Word_TmpDoc, WordDoc);             //组合报表模板
                }

                Word_TmpDoc.Paragraphs.Last.Range.Delete(ref clsWordControl.missing, ref clsWordControl.missing);

               // WordApp.InsertEditPwd(ref Word_TmpDoc, "myclou");         //Word报表加密


               


                if (PrintInfo.Saving)           //如果需要存盘
                {
                    string reportName = Items[i].Mb_ChrTxm == "" ? Items[i].Mb_ChrJlbh : Items[i].Mb_ChrTxm;
                    
                    if (!string.IsNullOrEmpty(Items[i].Mb_chrZsbh))
                    {
                        reportName = Items[i].Mb_chrZsbh;
                    }
                    saveTmpPath = string.Format(@"{0}\{1}.doc", this.SavePath(DateTime.Parse(Items[i].Mb_DatJdrq)), reportName + "_" + PrintTypeName);
                    WordApp.SaveDoc(Word_TmpDoc, string.Format(@"{0}\{1}.doc", this.SavePath(DateTime.Parse(Items[i].Mb_DatJdrq)), reportName + "_" + PrintTypeName));
                    
                    
                    if (PrintInfo.SaveOnly)     //如果仅存档
                    {
                       clsWordControl.CloseDoc(Word_TmpDoc, false);
                        continue;
                    }
                   
                }

                if (PrintInfo.Preview)
                {

                    WordApp.Add(Word_TmpDoc);

                }
                else
                {
                    //WordApp.PrintDoc(Word_TmpDoc);
                    //clsWordControl.CloseDoc(Word_TmpDoc, false);
                }
            }

            CLDC_DataCore.Function.TopWaiting.HideWaiting();

            InsertIntoSignPhoto(saveTmpPath, "Checker", TesterName);
            InsertIntoSignPhoto(saveTmpPath, "verificationer", CheckerName);
            InsertIntoSignPhoto(saveTmpPath, "Supervisor", chargeName);
            InsertIntoSignPhoto(saveTmpPath, "stamp", true);

            if (WordApp.Count > 0)      //大于零表示要预览
            {
                
                //Microsoft.Office.Interop.Word.Document Doc = WordApp.MergeFile();





              


                WordApp.Doc(saveTmpPath);

               // WordApp.SaveDoc(Word_TmpDoc, string.Format(@"{0}\{1}.doc", this.SavePath(DateTime.Parse(Items[i].Mb_DatJdrq)), reportName + "_" + PrintTypeName));
                   


               // WordApp.SaveDocTmp(Word_TmpDoc, clsMain.getFilePath("") + @"\TmpDoc.doc");

                WordApp.WordApplication.Visible = true;

               // WordApp.ClearDocCol();                //清除文档集合
            }

            return;
        }


        #region------------存档路径------------------
        /// <summary>
        /// 获取存档路径
        /// </summary>
        /// <param name="JdDate"></param>
        /// <returns></returns>
        private string SavePath(DateTime JdDate)
        {
            string Path = PrintInfo.SavePath;

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


        #region -----------------------数据替换-----------------------------------


        /// <summary>
        /// 获取报表模板，并进行报表模板的解析
        /// </summary>
        /// <param name="DocName">文档名称</param>
        /// <param name="Group">数据集合</param>
        /// <param name="Index">当前数据集合的下标</param>
        /// <param name="CurPage">当前页</param>
        /// <param name="MaxPage">最大页</param>
        /// <returns></returns>
        private Microsoft.Office.Interop.Word.Document GetReportWord(string DocName, List<Dictionary<PrintDataType, object>> Group, int Index, int CurPage, int MaxPage)
        {
            Microsoft.Office.Interop.Word.Document WordDoc = WordApp.Doc(string.Format(@"{0}\{1}", clsMain.getFilePath("Res"), DocName));

            if (WordDoc == null)
            {
                System.Windows.Forms.MessageBox.Show(string.Format(@"打开模板出现一个致命错误，没有找到对应 {0}\{1}的模板文件...", clsMain.getFilePath("Res"), DocName), "打印失败", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return null;
            }

            this.ReplaceReport(ref WordDoc, Group, Index, CurPage, MaxPage);
            return WordDoc;
        }



        /// <summary>
        /// 模板内容替换
        /// </summary>
        /// <param name="Word_DocTemplet"></param>
        /// <param name="Group"></param>
        /// <param name="Index"></param>
        /// <param name="CurPage"></param>
        /// <param name="MaxPage"></param>
        private void ReplaceReport(ref Microsoft.Office.Interop.Word.Document Word_DocTemplet, List<Dictionary<PrintDataType, object>> Group, int Index, int CurPage, int MaxPage)
        {
            this.ReplaceReport(ref Word_DocTemplet, Group, Index, CurPage, MaxPage, false);
        }
        /// <summary>
        /// 模板内容替换
        /// </summary>
        /// <param name="Word_DocTemplet"></param>
        /// <param name="Group"></param>
        /// <param name="Index"></param>
        /// <param name="CurPage"></param>
        /// <param name="MaxPage"></param>
        /// <param name="IsNull"></param>
        /// lees 插入数据20161102
        private void ReplaceReport(ref Microsoft.Office.Interop.Word.Document Word_DocTemplet, List<Dictionary<PrintDataType, object>> Group, int Index, int CurPage, int MaxPage, bool IsNull)
        {

            foreach (Microsoft.Office.Interop.Word.Bookmark Mark in Word_DocTemplet.Bookmarks)
            {
                if (IsNull)
                {
                    Mark.Range.Text = "";
                    continue;
                }
                string Tmp_Label = Mark.Range.Text;

                #region 签名
                if (Mark.Range.Text == "verificationer")
                {
                    //InserIntoSignPhoto(ref Word_DocTemplet, Mark);
                    continue;
                }
                if (Mark.Range.Text == "Supervisor")
                {
                    //InserIntoSignPhoto(ref Word_DocTemplet, Mark);
                    continue;
                }
                if (Mark.Range.Text == "Checker")
                {
                    //InserIntoSignPhoto(ref Word_DocTemplet, Mark);
                    continue;
                }
                if (Mark.Range.Text == "stamp")
                {
                    //InserIntoSignPhoto(ref Word_DocTemplet, Mark);
                    continue;
                }
                #endregion


                if ("\r\a" == Tmp_Label) 
                    continue;
                if (Tmp_Label == null) continue;
                
                Tmp_Label = Tmp_Label.Replace("\r\a", "");
                Tmp_Label = Tmp_Label.Replace("\r\n", "");

                int intFind = Tmp_Label.IndexOf('@');
                if (intFind > -1)
                {
                    string strTmp = Tmp_Label.Substring(0,intFind);
                    Index = int.Parse(strTmp);
                    if (Index > 0) Index = Index - 1;
                    Tmp_Label = Tmp_Label.Substring(intFind + 1, Tmp_Label.Length - intFind - 1);
                }
                if (Index >= Group.Count) continue;
                string[] Arr_Tmp = Tmp_Label.Split(':');
                string TmpValue = "";
                switch (Arr_Tmp.Length)
                {
                    case 1:             //书签文本格式只有一级表示为基本信息"meterid"

                        if (Arr_Tmp[0].ToLower() == "curpage")
                        {
                            TmpValue = CurPage.ToString();
                        }
                        else if (Arr_Tmp[0].ToLower() == "maxpage")
                        {
                            TmpValue = MaxPage.ToString();
                        }
                        else if (Arr_Tmp[0].ToLower() == "checker" || Arr_Tmp[0].ToLower() == "verificationer")
                        {
                            if (PrintInfo.PrintHuman)
                            {
                                TmpValue = this.GetColValue(PrintDataType.基本信息, Group[Index][PrintDataType.基本信息], Arr_Tmp[0].ToLower());
                            }
                        }
                        else if (Arr_Tmp[0].ToLower() == "creeping" || Arr_Tmp[0].ToLower() == "start")
                        {
                            TmpValue = this.GetColValue(PrintDataType.潜动起动数据, Group[Index][PrintDataType.潜动起动数据], Arr_Tmp[0].ToLower());
                        }
                        else
                        {
                            TmpValue = this.GetColValue(PrintDataType.基本信息, Group[Index][PrintDataType.基本信息], Arr_Tmp[0].ToLower());
                        }
                        break;
                    case 2:             //如果有2级，表示为多功能信息“Dgn:通信测试”
                        #region 冻结数据
                        if (Arr_Tmp[0].ToLower() == "frozen")
                        {
                            TmpValue = this.GetColValue(PrintDataType.冻结功能数据, Group[Index][PrintDataType.冻结功能数据], Arr_Tmp[1].ToLower());
                            break;
                        }
                        #endregion

                        #region 误差一致性数据
                        else if (Arr_Tmp[0].ToLower() == "erroraccords")
                        {
                            TmpValue = this.GetColValue(PrintDataType.误差一致性数据, Group[Index][PrintDataType.误差一致性数据], Arr_Tmp[1].ToLower());
                            break;
                        }
                        #endregion

                        #region 事件记录
                        if (Arr_Tmp[0].ToLower() == "eventlog")
                        {
                            TmpValue = this.GetColValue(PrintDataType.事件记录数据, Group[Index][PrintDataType.事件记录数据], Arr_Tmp[1].ToLower());
                            break;
                        }
                        #endregion

                        #region 费控数据
                        else if (Arr_Tmp[0].ToLower() == "costcontrol")
                        {
                            TmpValue = this.GetColValue(PrintDataType.费控功能数据, Group[Index][PrintDataType.费控功能数据], Arr_Tmp[1].ToLower());
                            break;
                        }
                        #endregion

                        #region 影响量 特殊 数据
                        else if (Arr_Tmp[0].ToLower() == "special")
                        {
                            TmpValue = this.GetColValue(PrintDataType.特殊检定数据 , Group[Index][PrintDataType.特殊检定数据 ], Arr_Tmp[1]);
                            break;
                        }
                        #endregion

                        #region 智能表功能数据
                        else if (Arr_Tmp[0].ToLower() == "function")
                        {
                            TmpValue = this.GetColValue(PrintDataType.智能表功能数据 , Group[Index][PrintDataType.智能表功能数据 ], Arr_Tmp[1].ToLower());
                            break;
                        }
                        #endregion

                        #region 南网费控软件数据
                        else if (Arr_Tmp[0].ToLower() == "nw")
                        {
                            TmpValue = this.GetColValue(PrintDataType.南网费控软件数据, Group[Index][PrintDataType.南网费控软件数据], Arr_Tmp[1].ToLower());
                        }
                        #endregion
                        #region 其他
                        else
                        {
                            TmpValue = this.GetColValue(PrintDataType.多功能数据, Group[Index][PrintDataType.多功能数据], Arr_Tmp[1].ToLower());

                        }
                        break;
                        #endregion
                    case 3:             //如果有3级，表示为误差信息"P+:H10_Imax:WcHzz"
                        if(Arr_Tmp[0].ToLower() == "fk")
                            TmpValue = this.GetColValue(PrintDataType.费控功能数据, Group[Index][PrintDataType.费控功能数据], Arr_Tmp[1].ToLower());
                        else if(Arr_Tmp[0].ToLower() == "jl")
                            TmpValue = this.GetColValue(PrintDataType.智能表功能数据, Group[Index][PrintDataType.智能表功能数据], Arr_Tmp[1].ToLower());
                        else
                            TmpValue = this.GetColValue(PrintDataType.误差数据, Group[Index][PrintDataType.误差数据], Tmp_Label, clsMain.GetWcNum(Arr_Tmp[2], PrintInfo.PrintStyle, PrintTypeName));
                        break;
                    case 4:         //走字数据"ZZDATA:P+:FL:FL(FL\Qm\Zm\Wc\Result\Mc\zhwc\ZhResult)"
                        if (Arr_Tmp[0].ToLower() == "zzdata")
                        {
                            TmpValue = this.GetColValue(PrintDataType.走字数据, Group[Index][PrintDataType.走字数据], Tmp_Label);
                        }
                        break;
                    case 5:         //特殊检定
                        //格式Loop:18:dnbhead:Dnb_Err:dnbfoot
                        if (Arr_Tmp[0].ToLower() == "loop")
                        {
                            InsertLoopTableForDoc(Tmp_Label, Mark, ref Word_DocTemplet, Group, Index);
                            continue;
                        }


                        break;
                    case 6:         //一页多条循环模式
                        /*
                           格式（Loop:P+:18:DnbHead:Dnb_Err:DnbFoot）Loop循环标识：:P+表示正向有功(该参数暂时无用):18表示一页18条记录:
                           DnbHead表示需要调用的模板头文件名称(不需要循环):Dnb_Err表示调用中间需要循环的模板:Dnb_Foot表示需要调用的尾部模板文件名称(不需要循环)
                           上面的头模板和尾模板都不能省略,如果没有头或尾,则需要将对应参数写为Null:Loop:P+:18:Null:Dnb_Err:Null
                         */
                        if (Arr_Tmp[0].ToLower() == "loop")
                        {
                            blnColIsAll = true;
                            this.InsertLoopDoc(Tmp_Label, Mark, ref  Word_DocTemplet, Group, CurPage, MaxPage);
                        }
                        continue;

                }
              

                if (TmpValue == "")
                {
                    TmpValue = PrintInfo.NotCheck;
                }
                Mark.Range.Text = TmpValue.Trim();
                //if (TmpValue.Trim() != "")
                //{
                //    Mark.Range.Text = TmpValue.Trim();
                //}
                //else
                //{
                //    Mark.Range.Text = @"/";
                //}
               
            }

        }
        private void InsertIntoSignPhoto(string FilePath,string bookMarkName)
        {
            Microsoft.Office.Interop.Word.ApplicationClass wordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            //定义该插入图片是否为外部链接
            object linkToFile = true;

            object filename = FilePath;
            //定义插入图片是否随word文档一起保存
            object saveWithDocument = true;
            object Nothing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref filename, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            try
            {
                //标签
                
                //图片
                string replacePic = string.Format(@"{0}\Sign\{1}.jpg", clsMain.getFilePath("Res"), CheckerName);
                string ed = "" ;
                foreach (Microsoft.Office.Interop.Word.Bookmark temp in doc.Bookmarks)
                {
                    if (temp.Range.Text == bookMarkName)
                    {
                        ed = temp.Name;
                        break;
                    }
                   
                }
                object bookMark = ed;
                if (doc.Bookmarks.Exists(ed) == true)
                {
                    //查找书签
                    doc.Bookmarks.get_Item(ref bookMark).Select();
                    //设置图片位置
                    wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                    //在书签的位置添加图片
                    //Microsoft.Office.Interop.Word.InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    Microsoft.Office.Interop.Word.InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    //设置图片大小
                    inlineShape.Width = 180;
                    inlineShape.Height = 50;
                    doc.Save();
                    doc.Close(ref Nothing, ref Nothing, ref Nothing);
                }
                else
                {
                    //word文档中不存在该书签，关闭文档
                    doc.Close(ref Nothing, ref Nothing, ref Nothing);
                }

            }
            catch
            {
                doc.Close(ref Nothing, ref Nothing, ref Nothing);
            }
        }

        private void InsertIntoSignPhoto(string FilePath, string bookMarkName,string Man)
        {
            Microsoft.Office.Interop.Word.ApplicationClass wordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            clsWordControl newApp = new clsWordControl();
            //定义该插入图片是否为外部链接
            object linkToFile = true;

            object filename = FilePath;
            //定义插入图片是否随word文档一起保存
            object saveWithDocument = true;
            object Nothing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref filename, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            try
            {
                //标签

                //图片
                string replacePic = string.Format(@"{0}\Sign\{1}.jpg", clsMain.getFilePath("Res"), Man);
                string ed = "";
                foreach (Microsoft.Office.Interop.Word.Bookmark temp in doc.Bookmarks)
                {
                    if (temp.Range.Text == bookMarkName)
                    {
                        ed = temp.Name;
                        break;
                    }

                }
                object bookMark = ed;
                if (doc.Bookmarks.Exists(ed) == true)
                {
                    //查找书签
                    doc.Bookmarks.get_Item(ref bookMark).Select();
                    //设置图片位置
                    wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    //在书签的位置添加图片
                    //Microsoft.Office.Interop.Word.InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    Microsoft.Office.Interop.Word.InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    //设置图片大小
                    inlineShape.Width = 100;
                    inlineShape.Height = 25;
                    doc.Save();
                   
                    doc.Close(ref Nothing, ref Nothing, ref Nothing);
                    string RealPath = filename.ToString();
                    newApp.Convert(RealPath, RealPath.Substring(0, RealPath.LastIndexOf(".")) + ".PDF", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
            
                    
                 
                   
                }
                else
                {
                    //word文档中不存在该书签，关闭文档
                    doc.Close(ref Nothing, ref Nothing, ref Nothing);
                }

            }
            catch
            {
                doc.Close(ref Nothing, ref Nothing, ref Nothing);
            }
        }

        private void InsertIntoSignPhoto(string FilePath, string bookMarkName, bool IsStamp)
        {
            Microsoft.Office.Interop.Word.ApplicationClass wordApp = new Microsoft.Office.Interop.Word.ApplicationClass();
            //定义该插入图片是否为外部链接
            clsWordControl newApp = new clsWordControl();
          
            object linkToFile = true;

            object filename = FilePath;
            //定义插入图片是否随word文档一起保存
            object saveWithDocument = true;
            object Nothing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref filename, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            try
            {
                //标签

                //图片
                string replacePic = string.Format(@"{0}\Sign\{1}.jpg", clsMain.getFilePath("Res"), "stamp");
                string ed = "";
                foreach (Microsoft.Office.Interop.Word.Bookmark temp in doc.Bookmarks)
                {
                    if (temp.Range.Text == bookMarkName)
                    {
                        ed = temp.Name;
                        break;
                    }

                }
                object bookMark = ed;
                if (doc.Bookmarks.Exists(ed) == true)
                {
                    //查找书签
                    doc.Bookmarks.get_Item(ref bookMark).Select();
                    //设置图片位置
                    wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                    //在书签的位置添加图片
                    //Microsoft.Office.Interop.Word.InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    Microsoft.Office.Interop.Word.InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    //设置图片大小
                    inlineShape.Width = 110;
                    inlineShape.Height = 110;
                    doc.Save();
                    doc.Close(ref Nothing, ref Nothing, ref Nothing);
                    string RealPath = filename.ToString();
                    newApp.Convert(RealPath, RealPath.Substring(0, RealPath.LastIndexOf(".")) + ".PDF", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
                }
                else
                {
                    //word文档中不存在该书签，关闭文档
                    doc.Close(ref Nothing, ref Nothing, ref Nothing);
                }

            }
            catch
            {
                doc.Close(ref Nothing, ref Nothing, ref Nothing);
            }
        }
        /// <summary>
        /// 表单内部循环误差，目前可适用于河南电科院和海南供电公司特殊样式
        /// </summary>
        /// <param name="LoopString"></param>
        /// <param name="mark"></param>
        /// <param name="templet"></param>
        /// <param name="Group"></param>
        /// <param name="groupIndex"></param>
        private void InsertLoopTableForDoc(string LoopString, Microsoft.Office.Interop.Word.Bookmark mark, ref Microsoft.Office.Interop.Word.Document templet
                                          , List<Dictionary<PrintDataType, object>> Group, int groupIndex)
        {
            string[] Arr_LoopInfo = LoopString.Split(':');
            object objStart = mark.Range.Start;
            object objEnd = mark.Range.End;

            Microsoft.Office.Interop.Word.Range word_ActiveRange = templet.Range(ref objStart, ref objEnd);

            int errorIndex = 0;

            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError> Items = (Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError>)Group[groupIndex][PrintDataType.误差数据];

            string xiangxian = ((Dictionary<string, string>)Group[groupIndex][PrintDataType.基本信息])["p_xiangxian"];

            string dl = CLDC_DataCore.Function.Number.GetCurrentByIb("ib", ((Dictionary<string, string>)Group[groupIndex][PrintDataType.基本信息])["i"]).ToString();

            string[] keyvales = new string[Items.Keys.Count];

            Items.Keys.CopyTo(keyvales, 0);

            int rowcount = 0;

            Console.WriteLine("Total:{0}", keyvales.Length);

            while (errorIndex < keyvales.Length)
            {

                if (Arr_LoopInfo[2].ToLower() != "null"
                        && Arr_LoopInfo[2].ToLower() != string.Empty
                        && System.IO.File.Exists(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[2])))
                {
                    Microsoft.Office.Interop.Word.Document headDoc = WordApp.Doc(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[2]));

                    this.ReplaceReport(ref headDoc, Group, groupIndex, 0, 0);

                    clsWordControl.CopyPasteTable(ref word_ActiveRange, ref headDoc, 1);
                }

                Microsoft.Office.Interop.Word.Table rangeTable = templet.Tables[templet.Tables.Count];

                for (; errorIndex < keyvales.Length; errorIndex++)
                {
                    if (keyvales[errorIndex][0] == '2')
                    {
                        Console.WriteLine("PC ,Continie");
                        continue;
                    }

                    clsWordControl.RangeMoveEnd(ref word_ActiveRange);

                    if (Arr_LoopInfo[3].ToLower() != "null"
                        && Arr_LoopInfo[3].ToLower() != string.Empty
                        && System.IO.File.Exists(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[3])))
                    {
                        Microsoft.Office.Interop.Word.Document loopDoc = WordApp.Doc(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[3]));

                        Console.WriteLine("Get Error Data:{0}", errorIndex);

                        this.ReplaceReport(ref loopDoc, Group[groupIndex], keyvales[errorIndex]);

                        clsWordControl.CopyPasteTable(ref word_ActiveRange, ref loopDoc, 1);

                        rowcount++;
                    }


                    if (rowcount % int.Parse(Arr_LoopInfo[1]) == 0)
                    {
                        if (Arr_LoopInfo[4].ToLower() != "null"
                            && Arr_LoopInfo[4].ToLower() != string.Empty
                            && System.IO.File.Exists(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[4])))
                        {
                            clsWordControl.RangeMoveEnd(ref word_ActiveRange);

                            Microsoft.Office.Interop.Word.Document headDoc = WordApp.Doc(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[4]));

                            this.ReplaceReport(ref headDoc, Group, groupIndex, 0, 0);

                            clsWordControl.CopyPasteTable(ref word_ActiveRange, ref headDoc, 1);


                        }
                        errorIndex++;

                        if (errorIndex != keyvales.Length)
                        {
                            clsWordControl.PageBreak(ref word_ActiveRange);
                        }
                        rowcount = 0;
                        break;
                    }
                }

            }

            if (keyvales.Length % int.Parse(Arr_LoopInfo[1]) != 0)
            {
                if (Arr_LoopInfo[4].ToLower() != "null"
                    && Arr_LoopInfo[4].ToLower() != string.Empty
                    && System.IO.File.Exists(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[4])))
                {
                    clsWordControl.RangeMoveEnd(ref word_ActiveRange);

                    Microsoft.Office.Interop.Word.Document headDoc = WordApp.Doc(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[4]));

                    this.ReplaceReport(ref headDoc, Group, groupIndex, 0, 0);

                    clsWordControl.CopyPasteTable(ref word_ActiveRange, ref headDoc, 1);

                }
            }
        }

        /// <summary>
        /// 仅供海南特殊报表模板的特殊标签替换
        /// </summary>
        /// <param name="Word_DocTemplet"></param>
        /// <param name="Item"></param>
        /// <param name="errKey"></param>
        private void ReplaceReport(ref Microsoft.Office.Interop.Word.Document Word_DocTemplet, Dictionary<PrintDataType, object> Item, string errKey)
        {


            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError ErrorItem = ((Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError>)Item[PrintDataType.误差数据])[errKey];

            foreach (Microsoft.Office.Interop.Word.Bookmark Mark in Word_DocTemplet.Bookmarks)
            {
                string Tmp_Label = Mark.Range.Text.Trim().ToLower();
                string TmpValue = "";

                switch (Tmp_Label)
                {
                    case "description":
                        TmpValue = clsMain.getErrorName(ErrorItem.Me_chrProjectNo);
                        break;
                    case "v":
                        //TmpValue = ErrorItem.Me_xU + "V";
                        break;
                    case "i":
                        TmpValue = string.Format("{0}A", CLDC_DataCore.Function.Number.GetCurrentByIb("ib", ((Dictionary<string, string>)Item[PrintDataType.基本信息])["i"]));
                        break;
                    case "xib":
                        TmpValue = ErrorItem.Me_dblxIb;
                        break;
                    case "glys":
                        TmpValue = ErrorItem.Me_chrGlys;
                        break;
                    case "wc":
                        {
                            string[] tmpwc = ErrorItem.Me_chrWcMore.Split('|');
                            if (tmpwc.Length < 2) break;
                            TmpValue = tmpwc[tmpwc.Length - 1];
                            break;
                        }
                    case "pc":
                        {
                            if (((Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError>)Item[PrintDataType.误差数据]).ContainsKey(string.Format("2{0}", errKey.Substring(1))))
                            {
                                string[] tmpwc = ((Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError>)Item[PrintDataType.误差数据])[string.Format("2{0}", errKey.Substring(1))].Me_chrWcMore.Split('|');
                                if (tmpwc.Length < 2) break;
                                TmpValue = tmpwc[tmpwc.Length - 1];
                            }
                            break;
                        }
                }

                if (TmpValue == "")
                {
                    TmpValue = PrintInfo.NotCheck;
                }
                Mark.Range.Text = TmpValue.Trim();
            }

        }


        /// <summary>
        /// 循环插入数据
        /// </summary>
        /// <param name="LoopString"></param>
        /// <param name="Mark"></param>
        /// <param name="Templet"></param>
        /// <param name="Group"></param>
        /// <param name="CurPage"></param>
        /// <param name="MaxPage"></param>
        private void InsertLoopDoc(string LoopString, Microsoft.Office.Interop.Word.Bookmark Mark
                                 , ref Microsoft.Office.Interop.Word.Document Templet
                                 , List<Dictionary<PrintDataType, object>> Group, int CurPage, int MaxPage)
        {
            string[] Arr_LoopInfo = LoopString.Split(':');
            object ObjStart = Mark.Range.Start;
            object ObjEnd = Mark.Range.End;

            Microsoft.Office.Interop.Word.Range Word_ActiveRange = Templet.Range(ref ObjStart, ref ObjEnd);
            /*
                *以下为循环的结构
                *
                *----->Dnbhead头部     End
                *-----> Dnb_Err        1
                *-----> .................
                *-----> Dnb_Err        16
                *-----> Dnb_Err        17
                *-----> Dnb_Err        18
                *----->DnbFoot底部    Start
                *
                *每次插入数据都是将存在的数据向下移动,并在当前位置插入一条新数据
             */
            for (int i = Group.Count - 1; i >= 0; i--)
            {
                System.Threading.Thread.Sleep(1);

                if ((i + 1) % int.Parse(Arr_LoopInfo[2]) == 0 || i == Group.Count - 1)       //如果是最后一条或者是每个分页条
                {
                    if (Arr_LoopInfo[5].ToLower() != "null")   //如果底部表单存在的话
                    {
                        Microsoft.Office.Interop.Word.Document LoopDoc = WordApp.Doc(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[5]));

                        this.ReplaceReport(ref LoopDoc, Group, i, CurPage, MaxPage);
                        clsWordControl.CopyPasteTable(ref Word_ActiveRange, ref LoopDoc, 1);
                    }

                    if (i == Group.Count - 1 && Group.Count % int.Parse(Arr_LoopInfo[2]) != 0)  //如果电能表条数不能刚好填充满一页，那么就需要加入一定数量的空行
                    {
                        int LoopNullRow = int.Parse(Arr_LoopInfo[2]) - (Group.Count % int.Parse(Arr_LoopInfo[2]));

                        for (int j = 0; j < LoopNullRow; j++)           //循环插入空行,最后一个插入当前有效行
                        {
                            Microsoft.Office.Interop.Word.Document LoopDoc = WordApp.Doc(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[4]));
                            this.ReplaceReport(ref LoopDoc, Group, i, CurPage, MaxPage, true);
                            clsWordControl.CopyPasteTable(ref Word_ActiveRange, ref LoopDoc, 1);
                        }
                    }
                    {
                        Microsoft.Office.Interop.Word.Document LoopDoc = WordApp.Doc(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[4]));
                        this.ReplaceReport(ref LoopDoc, Group, i, CurPage, MaxPage);
                        clsWordControl.CopyPasteTable(ref Word_ActiveRange, ref LoopDoc, 1);
                    }
                }
                else
                {
                    Microsoft.Office.Interop.Word.Document LoopDoc = WordApp.Doc(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[4]));
                    this.ReplaceReport(ref LoopDoc, Group, i, CurPage, MaxPage);
                    clsWordControl.CopyPasteTable(ref Word_ActiveRange, ref LoopDoc, 1);
                }

                if (i % int.Parse(Arr_LoopInfo[2]) == 0 && Arr_LoopInfo[3].ToLower() != "null")   //如果是集合循环结束或者刚好到达分页时,则需要加入一个头
                {
                    Microsoft.Office.Interop.Word.Document LoopDoc = WordApp.Doc(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[3]));
                    this.ReplaceReport(ref LoopDoc, Group, i, CurPage, MaxPage);
                    clsWordControl.CopyPasteTable(ref Word_ActiveRange, ref LoopDoc, 1);
                }
            }

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
                    #region 费控功能数据
                    case PrintDataType.费控功能数据:
                        {
                            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFK> meters = Items as Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFK>;
                            string[] key = KeyValue.Split('_');
                            if (key.Length == 1)
                            {

                                if (meters.ContainsKey(key[0]) && key[0].ToLower() != "result")
                                {//费控子检定项结论
                                    return meters[key[0]].Mfk_chrJL;
                                }
                                else if (key[0].ToLower() == "result")
                                {//费控总结论
                                    foreach (string _key in meters.Keys)
                                    {
                                        if (meters[_key].Mfk_chrJL == null) return CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                        if (meters[_key].Mfk_chrJL.Contains(CLDC_DataCore.Const.Variable.CTG_BuHeGe))
                                        {
                                            return CLDC_DataCore.Const.Variable.CTG_BuHeGe;
                                        }
                                    }
                                    return CLDC_DataCore.Const.Variable.CTG_HeGe;
                                }
                            }
                            return "";
                        }
                    #endregion

                    #region 误差一致性数据
                    case PrintDataType.误差一致性数据:
                        {
                            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterConsistency> meters = Items as Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterConsistency>;
                            string[] key = KeyValue.Split('_');
                            if (key.Length == 1)
                            {
                                return meters[key[0]].AVR_DIF_DATA_ROUNDING;
                                //if (meters.ContainsKey(key[0]) && key[0].ToLower() != "result")
                                //{
                                //    return meters[key[0]].Mea_Result;
                                //}
                            }
                            //else if (key.Length == 2)
                            //{
                            //    if (meters.ContainsKey(key[0]))
                            //    {

                            //        foreach (string _key in meters[key[0]].lstTestPoint.Keys)
                            //        {
                            //            if (_key.Trim() == key[1].Trim ())
                            //            {
                            //                return meters[key[0]].lstTestPoint[_key].Mea_Wc;
                            //            }
                            //        }
                   
                            //    }
                            //}
                            return "";
                        }
                    #endregion

                    #region 冻结数据
                    case PrintDataType.冻结功能数据:
                        {
                            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFreeze> meters = Items as Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFreeze>;
                            string[] key = KeyValue.Split('_');
                            if (key.Length == 1)
                            {
                                if (meters.ContainsKey(key[0]) && key[0].ToLower() != "result")
                                {
                                    return meters[key[0]].Md_chrValue;
                                }
                                else if (key[0].ToLower() == "result")
                                {
                                    foreach (string _key in meters.Keys)
                                    {
                                        if (_key.Length != 3) continue;//大ID 
                                        if (meters[_key].Md_chrValue == "不合格")
                                        {
                                            return meters[_key].Md_chrValue;
                                        }
                                        return CLDC_DataCore.Const.Variable.CTG_HeGe;
                                    }
                                }
                            }
                            return "";
                        }
                    #endregion

                    #region 事件记录数据
                    case PrintDataType.事件记录数据:
                        {//结论|总次数|总累计时间|最近一次发生时间
                            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSjJLgn> meters = Items as Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSjJLgn>;
                            string[] key = KeyValue.Split('_');
                            if (key.Length == 1 && key[0].ToLower() == "result")
                            {//总结论
                                    foreach (string _key in meters.Keys)
                                    {
                                        if (_key.Length != 3) continue;//大ID 
                                        if (meters[_key].ItemConc == "不合格")
                                        {
                                            return meters[_key].ItemConc;
                                        }
                                        return CLDC_DataCore.Const.Variable.CTG_HeGe;
                                    }
                            }
                            else if (key.Length == 2)
                            {//子事件记录数据
                                if (meters.ContainsKey(key[0]))
                                {
                                    int index;
                                    int.TryParse(key[1], out index);
                                    string[] values = meters[key[0]].PrintData.Split('|');
                                    if (index - 1 < values.Length)
                                    {
                                        return values[index - 1];
                                    }
                                }
 
                            }
                            return "";
                        }
                    #endregion

                    #region 智能表功能数据
                    case PrintDataType.智能表功能数据 :
                        {
                            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFunction> meters = Items as Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFunction>;
                            string[] key = KeyValue.Split('_');
                            if (key.Length == 1)
                            {
                                if (meters.ContainsKey(key[0]) && key[0].ToLower() != "result")
                                {
                                    return meters[key[0]].Mf_chrValue ;
                                }
                                else if (key[0].ToLower() == "result")
                                {
                                    foreach (string _key in meters.Keys)
                                    {
                                        if (_key.Length != 3) continue;//大ID 
                                        if (meters[_key].Mf_chrValue == "不合格")
                                        {
                                            return meters[_key].Mf_chrValue;
                                        }
                                        return CLDC_DataCore.Const.Variable.CTG_HeGe;
                                    }
                                }
                            }
                            return "";
                        }
                    #endregion

                    #region 特殊检定数据 影响量
                    case PrintDataType.特殊检定数据 :
                        {
                            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr> meters = Items as Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSpecialErr>;
                            string[] key = KeyValue.Split('_');
                            if (key.Length == 1)
                            {
                                //if (meters.ContainsKey(key[0]) && key[0].ToLower() != "result")
                                //{
                                //    return meters[key[0]].Mf_chrValue;
                                //}
                                //else if (key[0].ToLower() == "result")
                                //{
                                //    foreach (string _key in meters.Keys)
                                //    {
                                //      //  if (_key.Length != 3) continue;//大ID 
                                //        if (meters[_key].Mf_chrValue == "不合格")
                                //        {
                                //            return meters[_key].Mf_chrValue;
                                //        }
                                //        return CLDC_DataCore.Const.Variable.CTG_HeGe;
                                //    }
                                //}
                            }
                            else if (key.Length == 2)
                            {
                                //zxr xiu fu 20140813;
                                if(meters.ContainsKey(key[0]) ==false ) return "";
                                int Index = 2;  //结论|平均值|化整值|误差1|误差2
                                string skey = key[1].ToLower();
                                switch (skey)
                                {
                                    case "1":
                                        {
                                            Index = 1;
                                        }
                                        break;
                                    case "2":
                                    case "pjz":
                                        {
                                            Index = 2;
                                            
                                        }
                                        break;
                                    case "3":
                                    case "hzz":
                                        {
                                            Index = 3;
                                        }
                                        break;
                                    case "4":
                                    case "wc1":
                                        {
                                            Index = 4;
                                        }
                                        break;
                                    case "5":
                                    case "wc2":
                                        {
                                            Index = 5;
                                        }
                                        break;
                                    default:
                                        Index = 2;
                                        break;
                                }
                                string[] GetValues = meters[key[0]].PrintData.Split('|');
                                if (GetValues.Length > Index - 1)
                                    return GetValues[Index - 1];
                            }
                            return "";
                        }
                    #endregion

                    case PrintDataType.潜动起动数据:
                        {
                            string value = "合格";

                            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterQdQid> meters = Items as Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterQdQid>;
                            if (KeyValue == "creeping")
                            {
                                foreach (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterQdQid m in meters.Values)
                                {
                                    if (m.Mqd_chrProjectName.Contains("潜动"))
                                    {
                                        if (m.Mqd_chrJL.Contains("不合格"))
                                        {
                                            return m.Mqd_chrJL;
                                        }
                                    }
                                }

                            }
                            else
                            {
                                foreach (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterQdQid m in meters.Values)
                                {
                                    if (m.Mqd_chrProjectName.Contains("起动"))
                                    {
                                        if (m.Mqd_chrJL.Contains("不合格"))
                                        {
                                            return m.Mqd_chrJL;
                                        }
                                    }
                                }
                            }
                            return value;
                        }
                    case PrintDataType.基本信息:
                        KeyValue = KeyValue.Trim();
                        KeyValue = KeyValue.Replace(" ", "");
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
                            string[] arrTmp = KeyValue.Trim().Split(':');
                            bool IsYG = arrTmp[0].Contains("P");
                            if ((clsMain.getIniString("Certi", "MeterLevel").Contains("0.5") || clsMain.getIniString("Certi", "MeterLevel").Contains("0.1")) && IsYG)
                            {
                                WcString = Convert.ToDouble(WcArr[WcArr.Length - 1]).ToString("0.00");
                            }
                            else
                            {
                                WcString = WcArr[WcArr.Length - 1];
                            }
                          
                        }
                        else if (WcNum == -3)
                        {//平衡与不平衡负载误差之差 平均值
                            WcString = errorItem.AVR_DIF_ERR_AVG;
                        }
                        else if (WcNum == -4)
                        {//平衡与不平衡负载误差之差 化整值
                           // WcString = errorItem.AVR_DIF_ERR_ROUND;

                            if (clsMain.getIniString("Certi", "MeterLevel").Contains("0.5") || clsMain.getIniString("Certi", "MeterLevel").Contains("0.1"))
                            {
                                WcString = Convert.ToDouble(WcArr[WcArr.Length - 1]).ToString("0.00");
                            }
                            else
                            {
                                WcString = Convert.ToDouble(WcArr[WcArr.Length - 1]).ToString("0.0");
                            }

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
                            if (ZZError.Mz_chrGlys == Glfx.ToString() && ZZError.Mz_chrFl == Arr_Key[2])
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
                    case PrintDataType.南网费控软件数据:
                        if (((Dictionary<string, string>)Items).Count == 0) return "";
                        if (!((Dictionary<string, string>)Items).ContainsKey(KeyValue))
                        {
                            clsMain.WriteReportErr(2, string.Format("错误的南网软件数据关键字：{0}", KeyValue));
                            return "";
                        }
                        return ((Dictionary<string, string>)Items)[KeyValue];
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


        #region------------------------字典制作----------------------------

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
            CheckerName = BasicInfo["checker"].ToString();
            TesterName = BasicInfo["verificationer"].ToString();
            chargeName = BasicInfo["charge"].ToString();
            
            if (CLDC_DataCore.Function.DateTimes.IsDate(BasicInfo["checkdate"]))
            {
             BasicInfo.Add("meternowtime", DateTime.Parse(BasicInfo["jjdate"]).ToString("HH:mm:ss"));
             BasicInfo.Add("duancheckdate", DateTime.Parse(BasicInfo["checkdate"]).ToString("yyyy年MM月dd日"));
            }
                
            else
                BasicInfo.Add("duancheckdate", DateTime.Now.ToString());
            if (CLDC_DataCore.Function.DateTimes.IsDate(BasicInfo["jjdate"]))
            {
                BasicInfo.Add("duanjjdate", DateTime.Parse(BasicInfo["jjdate"]).ToString("yyyy年MM月dd日"));
               
            }
                
            else
                BasicInfo.Add("duanjjdate", DateTime.Now.ToString());

            string Tmp_Value = ((CLDC_Comm.Enum.Cus_Clfs)int.Parse(BasicInfo["checktype"])).ToString();
            double Meterlevel_yg = Convert.ToDouble(CLDC_DataCore.Function.Number.getDj(BasicInfo["meterlevel"])[0]);
            

            if (CLDC_DataCore.Function.DateTimes.IsDate(BasicInfo["checkdate"]))
            {
                if (Meterlevel_yg >=1)
                {
                    BasicInfo.Add("yxqforcheckdate", DateTime.Parse(BasicInfo["checkdate"].ToString()).AddYears(8).AddDays(-1).ToString("yyyy年MM月dd日"));
                }
                else
                {
                    BasicInfo.Add("yxqforcheckdate", DateTime.Parse(BasicInfo["checkdate"].ToString()).AddYears(6).AddDays(-1).ToString("yyyy年MM月dd日"));
                }
            }
            BasicInfo.Add("p_xiangxian", Tmp_Value);

            Tmp_Value = BasicInfo["znq"];

            BasicInfo.Remove("znq");
            if (Tmp_Value == "true")
            {
                BasicInfo.Add("znq", "有止逆");
            }
            else
            {
                BasicInfo.Add("znq", "无止逆");
            }
            Tmp_Value = BasicInfo["hgq"];

            BasicInfo.Remove("hgq");
            if (Tmp_Value == "True")
            {
                BasicInfo.Add("hgq", "经互感器");
            }
            else
            {
                BasicInfo.Add("hgq", "直接接入");
            }

           
            BasicInfo.Add("yglevel", Convert.ToDouble(CLDC_DataCore.Function.Number.getDj(BasicInfo["meterlevel"])[0]).ToString());   //有功等级
            //MeterYGlevel = Convert.ToDouble(CLDC_DataCore.Function.Number.getDj(BasicInfo["meterlevel"])[0]);
            BasicInfo.Add("wglevel", Convert.ToInt16(Convert.ToDouble(CLDC_DataCore.Function.Number.getDj(BasicInfo["meterlevel"])[1])).ToString());   //无功等级
            string sTrConstDw = "";
            if (BasicInfo["metertype"].IndexOf("感应") >= 0 || BasicInfo["metertype"].IndexOf("机电") >= 0 || BasicInfo["metertype"].IndexOf("机械") >= 0)
            {
                sTrConstDw = "r";
            }
            else
            {
                sTrConstDw = "imp";
            }

            //湛江要求
            BasicInfo.Add("ygconst", string.Format("{0} {1}/kWh（{1}/kvarh）", CLDC_DataCore.Function.Number.GetBcs(BasicInfo["meterconst"], true), sTrConstDw));  //有功常数
        
            //
            //BasicInfo.Add("ygconst", string.Format("{0} {1}/kWh", CLDC_DataCore.Function.Number.GetBcs(BasicInfo["meterconst"], true), sTrConstDw));  //有功常数
            //BasicInfo.Add("wgconst", string.Format("{0} {1}/kvarh", CLDC_DataCore.Function.Number.GetBcs(BasicInfo["meterconst"], false), sTrConstDw));      //无功常数

        }
        /// <summary>
        /// 获取基本信息和结论信息字典
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        private Dictionary<string, string> DicBasicInfo(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item)
        {
            System.Reflection.FieldInfo[] InfoTypes = Item.GetType().GetFields();
            string tempZsbh="";
            Dictionary<string, string> DicInfo = new Dictionary<string, string>();
            string EquiementNum = "", IndexFist = "1", Str_HGQ = "D", Str_Start = "",str_Zsbh_last="";
            if (Item.Mb_BlnHgq == true)
            {
                Str_HGQ = "D";
            }
            Str_Start = clsMain.getIniString("Certi", "Start");
            EquiementNum = clsMain.getIniString("OtherInfo", "Equiment", "");
            string TimeYear = Convert.ToDateTime(Item.Mb_DatJdrq).ToString("yyyy");
            str_Zsbh_last = clsMain.getIniString("OtherInfo", "ZSBH_" + TimeYear, "").PadLeft(5, '0');
            for (int i = 0; i < InfoTypes.Length; i++)              //遍历基本信息表
            {
                string sTrKey = clsMain.getIniString("CL3000SH", InfoTypes[i].Name, "", clsMain.getFilePath("ReportConfig.ini"));

                if (sTrKey == string.Empty) continue;
                #region 电压 电流
                if (sTrKey == "U"&&InfoTypes[16].GetValue(Item).ToString()=="0")
                {
                    switch (InfoTypes[i].GetValue(Item).ToString())
                    { 
                        case "220":
                            tempZsbh = "3×" + InfoTypes[i].GetValue(Item).ToString() + @"/380V";
                            break;
                        case "100":
                            tempZsbh = "3×" + InfoTypes[i].GetValue(Item).ToString()+"V";
                            break;
                        case "57.7":
                            tempZsbh = "3×" + InfoTypes[i].GetValue(Item).ToString() + @"/100V";
                            break;          
                        default:
                            break;
                    }
                    

                    DicInfo.Add(sTrKey.ToLower(), tempZsbh);
                    continue;
                }
                if (sTrKey == "I" && InfoTypes[16].GetValue(Item).ToString() == "0")
                {

                    tempZsbh = "3×" + InfoTypes[i].GetValue(Item).ToString();

                    DicInfo.Add(sTrKey.ToLower(), tempZsbh);
                    continue;
                }
                #endregion 
                
                if (sTrKey == "MeterConst")
                {
                    if (InfoTypes[8].GetValue(Item).ToString() == "感应式")
                    {
                        tempZsbh = InfoTypes[i].GetValue(Item).ToString()+@"r/kWh";
                        DicInfo.Add(sTrKey.ToLower(), InfoTypes[i].GetValue(Item).ToString());
                        continue;
                    }
                    else
                    {
                        tempZsbh = InfoTypes[i].GetValue(Item).ToString() + @"imp/kWh(imp/kvarh)";
                        DicInfo.Add(sTrKey.ToLower(), InfoTypes[i].GetValue(Item).ToString());
                        continue;
                    }
                    
                }
                if (sTrKey == "Txm")
                {
                    tempZsbh = InfoTypes[i].GetValue(Item).ToString().Trim();
                    if (tempZsbh== "")
                    {
                        tempZsbh = InfoTypes[0].GetValue(Item).ToString();
                    }

                    DicInfo.Add(sTrKey.ToLower(), tempZsbh);
                    continue;
                }
                if (sTrKey == "MeterID")
                {
                    tempZsbh = InfoTypes[i].GetValue(Item).ToString().Trim();
                    if (tempZsbh == "")
                    {
                        tempZsbh = InfoTypes[2].GetValue(Item).ToString();
                    }
                    DicInfo.Add(sTrKey.ToLower(), tempZsbh);
                    continue;
                }
                if (sTrKey == "ProductID")
                {
                    tempZsbh = InfoTypes[i].GetValue(Item).ToString().Trim();
                    if (tempZsbh == "")
                    {
                        tempZsbh = InfoTypes[2].GetValue(Item).ToString();
                        if (tempZsbh.Length > 12)
                        {
                           
                            tempZsbh = tempZsbh.Substring(tempZsbh.Length - 12, 12);
                        }
                    }
                    DicInfo.Add(sTrKey.ToLower(), InfoTypes[2].GetValue(Item).ToString());
                    continue;
                }
                if (sTrKey == "MeterLevel")
                {

                    clsMain.WriteIni("Certi", "MeterLevel", InfoTypes[i].GetValue(Item).ToString());
                    DicInfo.Add(sTrKey.ToLower(), InfoTypes[i].GetValue(Item).ToString());
                    continue;
                }
                
                if (sTrKey == "CertificateNo"&& tempZsbh.Length>0)
                {
                    
                   
                    
                    string This_Day = "", Last_Day = "";
                    int meter_ZSBH=0;
                    This_Day = Convert.ToDateTime(Item.Mb_DatJdrq).ToString("yyyy/MM/dd")+" 00:00:00";
                    Last_Day = Convert.ToDateTime(Item.Mb_DatJdrq).AddDays(1).ToString("yyyy/MM/dd") + " 00:00:00";
                    Dictionary<string,string> CheckMeter=  GetMeterZSBH(This_Day,Last_Day);
                    try 
                    {
                        foreach (string temp in CheckMeter.Keys)
                        {
                            meter_ZSBH++;
                            if (temp == Item.Mb_ChrJlbh.ToString().Trim())
                            {
                                break;
                            }
                        }

                        //DicInfo.Add(sTrKey.ToLower(), Convert.ToDateTime(Item.Mb_DatJdrq).ToString("yyyyMMdd") + meter_ZSBH.ToString().PadLeft(5, '0'));
                        //continue;
                         //DicInfo.Add(sTrKey.ToLower(), "J" + Convert.ToDateTime(Item.Mb_DatJdrq).ToString("YYYYMMDD") + tempZsbh.Substring(5, tempZsbh.Length - 5));
                        //广州局生成证书
                        if (Str_Start == "J")
                        {
                            DicInfo.Add(sTrKey.ToLower(), Str_Start + "Y" + Str_HGQ + EquiementNum + Convert.ToDateTime(Item.Mb_DatJdrq).ToString("yyyyMMdd") + str_Zsbh_last + @"(1/1)");
                            // 广州局生成证书
                            clsMain.WriteIni("OtherInfo", "ZSBH_" + TimeYear, (Convert.ToInt16(str_Zsbh_last) + 1).ToString().PadLeft(5, '0'));
                            continue;
                        }
                        else
                        {
                            DicInfo.Add(sTrKey.ToLower(), Str_Start + "Y" + Str_HGQ + EquiementNum + Convert.ToDateTime(Item.Mb_DatJdrq).ToString("yyyyMMdd") + str_Zsbh_last.PadLeft(6, '0') + @"(1/1)");
                            // 广州局生成证书
                            clsMain.WriteIni("OtherInfo", "ZSBH_" + TimeYear, (Convert.ToInt16(str_Zsbh_last) + 1).ToString().PadLeft(5, '0'));
                            continue;
                        }
                       
                    }
                    catch(Exception zcbh_E)
                    {
                    
                    }
                    
                }
              

                DicInfo.Add(sTrKey.ToLower(), InfoTypes[i].GetValue(Item).ToString());
                
                
            }

            foreach (string key in Item.MeterExtend.Keys)           //遍历扩展表
            {
                string sTrKey = clsMain.getIniString("CL3000SH", key, "", clsMain.getFilePath("ReportConfig.ini"));
                if (sTrKey == string.Empty) continue;
                DicInfo.Add(sTrKey.ToLower(), Item.MeterExtend[key]);
            }

            if (IndexFist == "1")
            {
                DicInfo.Add("bzbname", clsMain.getIniString("DeviceInfo", "txt_BzbName"));    //标准表名称
                DicInfo.Add("bzbtype", clsMain.getIniString("DeviceInfo", "txt_BzbType"));    //标准表类型
                DicInfo.Add("bzbno", clsMain.getIniString("DeviceInfo", "txt_BzbNum"));    //标准表编号
                DicInfo.Add("bzbclfw", clsMain.getIniString("DeviceInfo", "txt_BzbRange"));    //标准表测量范围
                DicInfo.Add("bzbzqd", clsMain.getIniString("DeviceInfo", "txt_BzbRank"));    //标准表等级
                DicInfo.Add("bzbzsbh", clsMain.getIniString("DeviceInfo", "txt_BzbCertificate"));    //标准表证书
                DicInfo.Add("bzbyxq", clsMain.getIniString("DeviceInfo", "txt_BzbValidity"));    //标准表有效期

                DicInfo.Add("uncertainty1", clsMain.getIniString("DeviceInfo", "txt_Unsure"));
                DicInfo.Add("uncertainty2", clsMain.getIniString("DeviceInfo", "txt_Unsure02"));
                DicInfo.Add("uncertainty3", clsMain.getIniString("DeviceInfo", "txt_Unsure03"));
                DicInfo.Add("uncertainty4", clsMain.getIniString("DeviceInfo", "txt_Unsure04"));
                DicInfo.Add("uncertainty5", clsMain.getIniString("DeviceInfo", "txt_Unsure05"));
                DicInfo.Add("uncertainty6", clsMain.getIniString("DeviceInfo", "txt_Unsure06"));
                DicInfo.Add("uncertainty7", clsMain.getIniString("DeviceInfo", "txt_Unsure07")); 

                DicInfo.Add("setname", clsMain.getIniString("DeviceInfo", "txt_ZzName"));    //装置名称
                DicInfo.Add("settype", clsMain.getIniString("DeviceInfo", "txt_ZzType"));    //装置类型
                DicInfo.Add("setno", clsMain.getIniString("DeviceInfo", "txt_ZzNum"));    //装置编号
                DicInfo.Add("setclfw", clsMain.getIniString("DeviceInfo", "txt_ZzRange"));    //装置测量范围
                DicInfo.Add("setzqd", clsMain.getIniString("DeviceInfo", "txt_ZzRank"));    //装置等级
                DicInfo.Add("setzsbh", clsMain.getIniString("DeviceInfo", "txt_ZzCertificate"));    //装置证书
                DicInfo.Add("setyxq", clsMain.getIniString("DeviceInfo", "txt_ZzValidity"));    //装置有效期

                IndexFist = "2";
            }
            
            

            foreach (string Key in Item.MeterResults.Keys)
            {
                string sTrKey = clsMain.getIniString("CL3000SH_Result", "R_" + Key, "", clsMain.getFilePath("ReportConfig.ini"));

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


            DicRunElectrcInfo(Item.MeterZZErrors, ref DicInfo);
            DicCreepingInfo(Item.MeterQdQids, ref DicInfo);


            return DicInfo;
        }


        /// <summary>
        /// 获取电能表走字详细信息
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        private void DicRunElectrcInfo(Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterZZError> Item ,ref Dictionary<string,string> OtherDict)
        {
            System.Reflection.FieldInfo[] InfoTypes = Item.GetType().GetFields();
         string RunElectric="";
            Dictionary<string, string> DicInfo = new Dictionary<string, string>();
            if (Item.Count > 0)
            {
              foreach (string temp_key in  Item.Keys)
              {
                RunElectric=temp_key;
                  break;
              }


              OtherDict.Add("走字前电量", Item[RunElectric].Mz_chrQiMa.ToString() + @"/kW·h");
              OtherDict.Add("走字后电量", Item[RunElectric].Mz_chrZiMa.ToString() + @"/kW·h");
              OtherDict.Add("走字电量差值", Convert.ToDouble(Item[RunElectric].Mz_chrQiZiMaC.ToString()).ToString("0.00") + @"/kW·h");
                
            }
            

            
        }

        /// <summary>
        /// 获取电能表潜动起动详细信息
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        private void DicCreepingInfo(Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterQdQid> Item, ref Dictionary<string, string> OtherDict)
        {
            System.Reflection.FieldInfo[] InfoTypes = Item.GetType().GetFields();
            string RunElectric = "";
            Dictionary<string, string> DicInfo = new Dictionary<string, string>();
            try
            {
                if (Item.Count > 0)
                {

                    OtherDict.Add("标准起动时间", (Math.Ceiling(Convert.ToDecimal(Item["1091"].AVR_STANDARD_TIME) * 60).ToString() + @"s"));
                    OtherDict.Add("实际起动时间", (Math.Ceiling(Convert.ToDecimal(Item["1091"].AVR_ACTUAL_TIME) * 60).ToString() + @"s"));
                    OtherDict.Add("标准潜动时间", (Math.Ceiling(Convert.ToDecimal(Item["1101115"].AVR_STANDARD_TIME) * 60).ToString() + @"s"));
                    OtherDict.Add("实际潜动时间", (Math.Ceiling(Convert.ToDecimal(Item["1101115"].AVR_ACTUAL_TIME) * 60).ToString() + @"s"));

                }
            }
            catch(Exception DicCreeping)
            { 
            
            }
            


        }


        private Dictionary<string, string> GetMeterZSBH(string Checkdate, string CheckLastDay)
        {
            Dictionary<string, string> MeterCol = new Dictionary<string, string>();
            try
            {
                string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
                string Sql_word_2 = ";Persist Security Info=False";
                string Datapath = clsMain.getIniString("Path", "DataPath", "", System.Windows.Forms.Application.StartupPath + @"\Plugins\ManageInfo.ini");
                OleDbConnection conn = new OleDbConnection(Sql_word_1 + Datapath + Sql_word_2);
                conn.Open();
                string sql = string.Format("Select * from meter_info where DTM_TEST_DATE >#{0}# and DTM_TEST_DATE <#{1}# order by DTM_TEST_DATE,LNG_BENCH_POINT_NO", Checkdate, CheckLastDay);
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataReader myreader = null;
                myreader = cmd.ExecuteReader();
                string temp = "";
                int MeterCount = 0;
                while (myreader.Read())
                {
                    MeterCount = MeterCount + 1;
                    if (myreader["AVR_ASSET_NO"].ToString().Trim() == "")
                    {
                        MeterCol.Add(myreader["AVR_BAR_CODE"].ToString().Trim(), MeterCount.ToString());
                    }
                    else
                    {
                        temp = myreader["AVR_ASSET_NO"].ToString().Trim();
                        MeterCol.Add(myreader["AVR_ASSET_NO"].ToString().Trim(), MeterCount.ToString());
                    }
                  
                }
                conn.Close();
                return MeterCol;

            }
            catch (Exception exFind)
            {
                return MeterCol;
            }

        }
    
        /// <summary>
        /// 获取多功能数据字典
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetDicDgnInfo(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item)
        {
            Dictionary<string, string> DgnDic = new Dictionary<string, string>();
            Dictionary<string, string> DgnJdqszzhwc = new Dictionary<string, string>();
            foreach (string Key in Item.MeterDgns.Keys)
            {
                if (Key.Substring(0,3) == "005")//计度器示值组合误差
                {
                    DgnJdqszzhwc.Add(Key, Item.MeterDgns[Key].Md_chrValue);
                }
                else if (Key == "00201")
                {
                    string sTrKey = clsMain.getIniString("CL3000SH_DGN", "D_" + Key, "", clsMain.getFilePath("ReportConfig.ini"));
                    if (sTrKey == string.Empty) continue;
                    string[] Arr_Tmp = sTrKey.Split(',');
                    string[] Arr_Value = Item.MeterDgns[Key].Md_chrValue.Split('|');
                    for (int i = 0; i < Arr_Tmp.Length; i++)
                    {
                        try
                        {
                            if (!DgnDic.ContainsKey(Arr_Tmp[i].ToLower()))
                            {
                                DgnDic.Add(Arr_Tmp[i].ToLower(), Convert.ToDouble(Arr_Value[1]) > 0 ? "+" + Convert.ToDouble(Arr_Value[1]).ToString() :  Convert.ToDouble(Arr_Value[1]).ToString());
                            }
                        }
                        catch
                        { }
                    }
                }
                else
                {
                    string sTrKey = clsMain.getIniString("CL3000SH_DGN", "D_" + Key, "", clsMain.getFilePath("ReportConfig.ini"));
                    if (sTrKey == string.Empty) continue;
                    string[] Arr_Tmp = sTrKey.Split(',');
                    string[] Arr_Value = Item.MeterDgns[Key].Md_chrValue.Split('|');
                    for (int i = 0; i < Arr_Tmp.Length; i++)
                    {
                        try
                        {
                            if (!DgnDic.ContainsKey(Arr_Tmp[i].ToLower()))
                            {
                                DgnDic.Add(Arr_Tmp[i].ToLower(), Arr_Value[i]);
                            }
                        }
                        catch
                        { }
                    }
                }

            }

            //add by zxr in the nanchang 20140814  增加处理计度器示值组合误差数据 方便打印输出
            #region 计度器
            if (DgnJdqszzhwc.Count > 4)
            {
                int dgncout = DgnJdqszzhwc.Count;
                //计度器示值组合误差总结论
                string sZongwc = "005" + (dgncout - 2).ToString().PadLeft(2, '0');
                string[] str_Value = null;
                string sfeilv = string.Empty;
                float ffeilvhe = 0.00f;

                foreach (string skey in DgnJdqszzhwc.Keys)
                {
                    str_Value = null;
                    sfeilv = string.Empty;
                    switch (skey)
                    {
                        case "005":
                            {
                                DgnDic.Add("组合误差结论", DgnJdqszzhwc[skey]);
                            }
                            break;
                        case "00500":
                        case "00501":
                        case "00502":
                        case "00503":
                        case "00504":
                        case "00505":
                        case "00506":
                            {
                                str_Value = DgnJdqszzhwc[skey].Split('|');
                                if (str_Value.Length > 2)
                                {
                                    sfeilv = str_Value[str_Value.Length - 1];
                                    if (!DgnDic.ContainsKey("组合误差" + sfeilv + "差值"))
                                    {
                                        DgnDic.Add("组合误差" + sfeilv + "差值", str_Value[2]);
                                        DgnDic.Add("组合误差" + sfeilv + "初始值", str_Value[0]);
                                        DgnDic.Add("组合误差" + sfeilv + "运行后值", str_Value[1]);
                                    }
                                        

                                    if (skey != "00500")
                                    {
                                        ffeilvhe += Convert.ToSingle(str_Value[2]);
                                    }
                                }
                                else
                                {
                                    if (str_Value.Length == 1 && sZongwc == skey)//组合总误差
                                    {
                                        if (!DgnDic.ContainsKey("组合误差"))
                                            DgnDic.Add("组合误差", str_Value[0]);
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }

                DgnDic.Add("组合误差分时和", ffeilvhe.ToString("0.0000"));

                //
            }
            #endregion
            return DgnDic;

        }


        private Dictionary<string, string> GetNwSoftInfo(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item)
        {
            try
            {
                Dictionary<string, string> NwInfo = new Dictionary<string, string>();
                Dictionary<string, string> NwInfoMake = new Dictionary<string, string>();
                string[] ValueList;
                string MoneyBefore = "1000", MoneyAfter = "838.72",MoneyChange="161.28";
                NwInfo.Add("电表金额使用前", MoneyBefore);
                NwInfo.Add("电表金额使用后", MoneyAfter);
                NwInfo.Add("电表金额变化值", MoneyChange);
                NwInfo.Add("剩余电量递减结论", "合格");
                foreach (string Key in Item.MeterOtherSoftData.Keys)
                {
                    if (Key == "阶梯电价结算")
                    {
                        NwInfoMake.Add(Key, Item.MeterOtherSoftData[Key].Mosd_chrValue);
                        ValueList = Item.MeterOtherSoftData[Key].Mosd_chrValue.Split(new string[] {@"^" }, StringSplitOptions.RemoveEmptyEntries);
                       // ValueList = System.Text.RegularExpressions.Regex.Split(Item.MeterOtherSoftData[Key].Mosd_chrValue, @"^", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        MoneyBefore = ValueList[ValueList.Length - 7];
                        MoneyAfter = ValueList[ValueList.Length - 6];
                        MoneyChange = ValueList[ValueList.Length - 4];
                        NwInfo.Remove("电表金额使用前");
                        NwInfo.Remove("电表金额使用后");
                        NwInfo.Remove("电表金额变化值");
                        NwInfo.Remove("剩余电量递减结论");
                        NwInfo.Add("电表金额使用前", MoneyBefore);
                        NwInfo.Add("电表金额使用后", MoneyAfter);
                        NwInfo.Add("电表金额变化值", MoneyChange);
                        NwInfo.Add("剩余电量递减结论", Item.MeterOtherSoftData[Key].AVR_CONCLUSION);
                    }
                    if (Key == "分时费率电价结算")
                    {
                        NwInfoMake.Add(Key, Item.MeterOtherSoftData[Key].Mosd_chrValue);
                        ValueList = System.Text.RegularExpressions.Regex.Split(Item.MeterOtherSoftData[Key].Mosd_chrValue, "^", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        MoneyBefore = ValueList[ValueList.Length - 7];
                        MoneyAfter = ValueList[ValueList.Length - 6];
                        MoneyChange = ValueList[ValueList.Length - 4];
                        if (NwInfo.ContainsKey("电表金额使用前") && NwInfo["电表金额使用前"] == "")
                        {
                            NwInfo.Remove("电表金额使用前");
                            NwInfo.Remove("电表金额使用后");
                            NwInfo.Remove("电表金额变化值");
                            NwInfo.Remove("剩余电量递减结论");
                            NwInfo.Add("电表金额使用前", MoneyBefore);
                            NwInfo.Add("电表金额使用后", MoneyAfter);
                            NwInfo.Add("电表金额变化值", MoneyChange);
                            NwInfo.Add("剩余电量递减结论", Item.MeterOtherSoftData[Key].AVR_CONCLUSION);
                        }
                        
                    }
                }
                 NwInfo.Add("剩余电能量预设报警","10");
                 NwInfo.Add("剩余电能量实际报警","10");
                 NwInfo.Add("剩余电能量预设断电","0");
                 NwInfo.Add("剩余电能量实际断电","0");
                return NwInfo;
            }
            catch
            {
                Dictionary<string, string> NwInfo = new Dictionary<string, string>();
                NwInfo.Add("电表金额使用前", @"0");
                NwInfo.Add("电表金额使用后", @"0");
                NwInfo.Add("电表金额变化值", @"0");
                NwInfo.Add("剩余电量递减结论", @"未检定");
                return NwInfo;
            }
          
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
            return sTrTemplet; //返回报表调用的模板名字

        }
        #endregion

    }
    


    
         struct PrintOtherInfo
    {
        /// <summary>
        /// 是否打印检验员
        /// </summary>
        public bool PrintHuman;
        /// <summary>
        /// 不合格标志
        /// </summary>
        public string BHGString;
        /// <summary>
        /// 是否需要存盘
        /// </summary>
        public bool Saving;
        /// <summary>
        /// 是否仅存档
        /// </summary>
        public bool SaveOnly;
        /// <summary>
        /// 存档方式
        /// </summary>
        public int SaveBag;
        /// <summary>
        /// 是否预览
        /// </summary>
        public bool Preview;
        /// <summary>
        /// 打印样式
        /// </summary>
        public int PrintStyle;
        /// <summary>
        /// 存档路径
        /// </summary>
        public string SavePath;
        /// <summary>
        /// 未检标志
        /// </summary>
        public string NotCheck;



    }



    }



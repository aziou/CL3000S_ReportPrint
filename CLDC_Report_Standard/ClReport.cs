using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Windows.Forms;

namespace CLReport_Standard
{
    /// <summary>
    /// �����ӡ���
    /// </summary>
    public class ClReport : CLDC_DataCore.Interfaces.IReportInterface
    {
        /// <summary>
        /// �Ƿ���һҳ����
        /// </summary>
        private bool blnColIsAll = false;

        private string iniPath = clsMain.getFilePath(@"Res\Templet.ini");
        
        private clsWordControl WordApp = null;

        private UI.UI_ReportInfo uiReportInfo = null;

        private UI.UI_ReportSet uiReportSet = null;

        /// <summary>
        /// �춨����
        /// </summary>
        private string chrJdyj = "";
        /// <summary>
        /// �����׼
        /// </summary>
        private string chrZzbz = "";

        /// <summary>
        /// ��ӡ����
        /// </summary>
        private PrintOtherInfo PrintInfo;

        /// <summary>
        /// ��ӡ��������
        /// </summary>
        private string PrintTypeName;

        /// <summary>
        /// ����
        /// </summary>
        private string CheckerName;

        /// <summary>
        /// ����
        /// </summary>
        private string TesterName;

        /// <summary>
        /// zhuguan
        /// </summary>
        private string chargeName;

        /// <summary>
        /// ���ݴ�ӡ����
        /// </summary>
        private enum PrintDataType
        {
            ������Ϣ = 1,
            ������� = 2,
            �๦������ = 3,
            �������� = 4,
            ����춨���� = 5,
            Ǳ�������� = 6,
            �ز��춨���� = 7,
            ���һ�������� = 8,
            �������� = 9,
            �ѿع������� = 10,
            ���ܱ������� = 11,
            �¼���¼���� = 12,
            ���Ṧ������ = 13,
            �����ѿ��������=14,
        }

        /// <summary>
        /// �й��ȼ�
        /// </summary>
        private int MeterYGlevel;

        public ClReport()
        {            
            PrintInfo = new PrintOtherInfo();
            PrintInfo.BHGString = clsMain.getIniString("OtherInfo", "BHG", "");                                         //���ϸ��ʾ
            PrintInfo.PrintHuman = clsMain.getIniString("OtherInfo", "PrintHuman", "0") == "0" ? false : true;          //�Ƿ��ӡ�춨Ա
            PrintInfo.Saving = clsMain.getIniString("OtherInfo", "Save", "0") == "0" ? false : true;                    //�Ƿ����
            PrintInfo.Preview = clsMain.getIniString("Otherinfo", "Preview", "0") == "0" ? false : true;                //�Ƿ�Ԥ��
            PrintInfo.PrintStyle = int.Parse(clsMain.getIniString("OtherInfo", "PrintStyle", "0"));                     //��ӡ��ʽ
            PrintInfo.SaveOnly = clsMain.getIniString("OtherInfo", "SaveOnly", "0") == "0" ? false : true;              //�Ƿ���浵
            PrintInfo.SaveBag = int.Parse(clsMain.getIniString("OtherInfo", "SaveBag", "0"));
            PrintInfo.NotCheck = clsMain.getIniString("ReportInfo", "NotCheck", "");//zxr�޸����ؼ���д���� 20140812

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

        #region IReportInterface ��Ա

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
        /// ��װ�ض��������panel�����UI
        /// </summary>
        /// <param name="panel"></param>
        public void ShowPanel(CLDC_DataCore.Interfaces.IControlPanel panel)
        {
            panel.Save += new EventHandler(panel_Save);

            Dictionary<string, string> Items = new Dictionary<string, string>();

            Items.Add("Report_Info", "������Ϣ����");

            Items.Add("Report_Set", "����ģ������");

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
        /// �����ӡ����(��ں���)
        /// </summary>
        /// <param name="Items">���ܱ���</param>
        /// <param name="ReportTao">ģ������</param>
        /// <param name="ReportType">��ӡ���ͣ�֤�飬ԭʼ��¼����������</param>
        public void PrintRpt(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items, int ReportTao, int ReportType, string Jdyj, string zzbz)
        {
            //��ȡ��ӡ��������
            PrintTypeName = clsMain.getIniString(string.Format("Type_{0}", ReportTao), "TypeName", "", iniPath).Split(',')[ReportType];

            CLDC_DataCore.Function.TopWaiting.ShowWaiting("���ڽ�������,���Ժ�.....");

            List<Dictionary<PrintDataType, object>> DicItems = new List<Dictionary<PrintDataType, object>>();
            this.chrJdyj = Jdyj; this.chrZzbz = zzbz;
            for (int i = 0; i < Items.Count; i++)
            {

                Dictionary<PrintDataType, object> DataItem = new Dictionary<PrintDataType, object>();

                DataItem.Add(PrintDataType.������Ϣ, DicBasicInfo(Items[i]));       //������Ϣ
                DataItem.Add(PrintDataType.�������, Items[i].MeterErrors);         //�������
                DataItem.Add(PrintDataType.�๦������, GetDicDgnInfo(Items[i]));         //�๦������
                DataItem.Add(PrintDataType.��������, Items[i].MeterZZErrors);       //��������
                DataItem.Add(PrintDataType.����춨����, Items[i].MeterSpecialErrs);    //����춨����--Ӱ����
                DataItem.Add(PrintDataType.Ǳ��������,Items[i].MeterQdQids);    //��Ǳ���춨����
                DataItem.Add(PrintDataType.��������, Items[i].MeterPowers);    //������������
                DataItem.Add(PrintDataType.���һ��������, Items[i].MeterConsistencys);    //������������
                DataItem.Add(PrintDataType.���Ṧ������, Items[i].MeterFreezes);    //���Ṧ������
                DataItem.Add(PrintDataType.�ѿع�������, Items[i].MeterCostControls);    //�ѿع�������
                DataItem.Add(PrintDataType.�¼���¼����, Items[i].MeterSjJLgns);    //�¼���¼����
                DataItem.Add(PrintDataType.�ز��춨����, Items[i].MeterCarrierDatas);    //�ز��춨����
                DataItem.Add(PrintDataType.���ܱ�������, Items[i].MeterFunctions);    //���ܱ�������
                DataItem.Add(PrintDataType.�����ѿ��������,GetNwSoftInfo(Items[i]));//��������ѿ����������
                this.AddDWSetting((Dictionary<string, string>)DataItem[PrintDataType.������Ϣ]);  //�������������Ϣ

                DicItems.Add(DataItem);

            }



            CLDC_DataCore.Function.TopWaiting.ShowWaiting("�������ɱ���,���Ժ�.....");

            string saveTmpPath="";
            for (int i = 0; i < Items.Count; i++)
            {

                if (blnColIsAll) break;     //����Ǵ�ӡһҳ�������Ͳ�����Ҫ��������Ĺ��̣�ֱ������

                string Report_List = this.getReportTemplet(Items[i].Mb_intClfs == (int)CLDC_Comm.Enum.Cus_Clfs.���� ? true : false, Items[i], ReportTao, ReportType);

                if (Report_List == string.Empty)
                {
                    System.Windows.Forms.MessageBox.Show("û�ж�Ӧ�ı���ģ�壬�޷���ɴ�ӡ����...", "��ӡʧ��", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
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

                    //WordApp.ClearDocCol();                //����ĵ�����

                    return;
                    //System.Windows.Forms.MessageBox.Show("ģ�����ô����޷���ɴ�ӡ����...", "��ӡʧ��", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    //return;
                }
                string GenaraPath = "";

                if (!System.IO.File.Exists(string.Format(@"{0}\{1}", clsMain.getFilePath("Res"), Templet_Arr[0])))
                {
                    System.Windows.Forms.MessageBox.Show(string.Format(@"��ģ�����һ����������û���ҵ���Ӧ {0}\{1}��ģ���ļ�...", clsMain.getFilePath("Res"), Templet_Arr[0]), "��ӡʧ��", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
                WordApp = new clsWordControl();//����word
                Microsoft.Office.Interop.Word.Document Word_TmpDoc = WordApp.LoadDot(string.Format(@"{0}\{1}", clsMain.getFilePath("Res"), Templet_Arr[0]));
                GenaraPath = string.Format(@"{0}\{1}", clsMain.getFilePath("Res"), Templet_Arr[0]);
                

                for (int Int_Mb = 1; Int_Mb < Templet_Arr.Length; Int_Mb++)
                {
                    CLDC_DataCore.Function.TopWaiting.ShowWaiting("����׼�����ɱ���.....");

                    Microsoft.Office.Interop.Word.Document WordDoc = GetReportWord(Templet_Arr[Int_Mb], DicItems, i, Int_Mb, Templet_Arr.Length - 1);          //��ȡ�������ģ��

                    if (WordDoc == null) return;

                    WordApp.PasteWord(Word_TmpDoc, WordDoc);             //��ϱ���ģ��
                }

                Word_TmpDoc.Paragraphs.Last.Range.Delete(ref clsWordControl.missing, ref clsWordControl.missing);

               // WordApp.InsertEditPwd(ref Word_TmpDoc, "myclou");         //Word�������


               


                if (PrintInfo.Saving)           //�����Ҫ����
                {
                    string reportName = Items[i].Mb_ChrTxm == "" ? Items[i].Mb_ChrJlbh : Items[i].Mb_ChrTxm;
                    
                    if (!string.IsNullOrEmpty(Items[i].Mb_chrZsbh))
                    {
                        reportName = Items[i].Mb_chrZsbh;
                    }
                    saveTmpPath = string.Format(@"{0}\{1}.doc", this.SavePath(DateTime.Parse(Items[i].Mb_DatJdrq)), reportName + "_" + PrintTypeName);
                    WordApp.SaveDoc(Word_TmpDoc, string.Format(@"{0}\{1}.doc", this.SavePath(DateTime.Parse(Items[i].Mb_DatJdrq)), reportName + "_" + PrintTypeName));
                    
                    
                    if (PrintInfo.SaveOnly)     //������浵
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

            if (WordApp.Count > 0)      //�������ʾҪԤ��
            {
                
                //Microsoft.Office.Interop.Word.Document Doc = WordApp.MergeFile();





              


                WordApp.Doc(saveTmpPath);

               // WordApp.SaveDoc(Word_TmpDoc, string.Format(@"{0}\{1}.doc", this.SavePath(DateTime.Parse(Items[i].Mb_DatJdrq)), reportName + "_" + PrintTypeName));
                   


               // WordApp.SaveDocTmp(Word_TmpDoc, clsMain.getFilePath("") + @"\TmpDoc.doc");

                WordApp.WordApplication.Visible = true;

               // WordApp.ClearDocCol();                //����ĵ�����
            }

            return;
        }


        #region------------�浵·��------------------
        /// <summary>
        /// ��ȡ�浵·��
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
                case 0:         //��
                    Path += string.Format(@"\{0}", JdDate.ToString("yyyyMMdd"));
                    if (!System.IO.Directory.Exists(Path))
                    {
                        System.IO.Directory.CreateDirectory(Path);
                    }
                    break;
                case 1:         //��
                    Path += string.Format(@"\{0}", JdDate.ToString("yyyyMM"));
                    if (!System.IO.Directory.Exists(Path))
                    {
                        System.IO.Directory.CreateDirectory(Path);
                    }
                    break;
                case 2:         //��
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


        #region -----------------------�����滻-----------------------------------


        /// <summary>
        /// ��ȡ����ģ�壬�����б���ģ��Ľ���
        /// </summary>
        /// <param name="DocName">�ĵ�����</param>
        /// <param name="Group">���ݼ���</param>
        /// <param name="Index">��ǰ���ݼ��ϵ��±�</param>
        /// <param name="CurPage">��ǰҳ</param>
        /// <param name="MaxPage">���ҳ</param>
        /// <returns></returns>
        private Microsoft.Office.Interop.Word.Document GetReportWord(string DocName, List<Dictionary<PrintDataType, object>> Group, int Index, int CurPage, int MaxPage)
        {
            Microsoft.Office.Interop.Word.Document WordDoc = WordApp.Doc(string.Format(@"{0}\{1}", clsMain.getFilePath("Res"), DocName));

            if (WordDoc == null)
            {
                System.Windows.Forms.MessageBox.Show(string.Format(@"��ģ�����һ����������û���ҵ���Ӧ {0}\{1}��ģ���ļ�...", clsMain.getFilePath("Res"), DocName), "��ӡʧ��", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return null;
            }

            this.ReplaceReport(ref WordDoc, Group, Index, CurPage, MaxPage);
            return WordDoc;
        }



        /// <summary>
        /// ģ�������滻
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
        /// ģ�������滻
        /// </summary>
        /// <param name="Word_DocTemplet"></param>
        /// <param name="Group"></param>
        /// <param name="Index"></param>
        /// <param name="CurPage"></param>
        /// <param name="MaxPage"></param>
        /// <param name="IsNull"></param>
        /// lees ��������20161102
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

                #region ǩ��
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
                    case 1:             //��ǩ�ı���ʽֻ��һ����ʾΪ������Ϣ"meterid"

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
                                TmpValue = this.GetColValue(PrintDataType.������Ϣ, Group[Index][PrintDataType.������Ϣ], Arr_Tmp[0].ToLower());
                            }
                        }
                        else if (Arr_Tmp[0].ToLower() == "creeping" || Arr_Tmp[0].ToLower() == "start")
                        {
                            TmpValue = this.GetColValue(PrintDataType.Ǳ��������, Group[Index][PrintDataType.Ǳ��������], Arr_Tmp[0].ToLower());
                        }
                        else
                        {
                            TmpValue = this.GetColValue(PrintDataType.������Ϣ, Group[Index][PrintDataType.������Ϣ], Arr_Tmp[0].ToLower());
                        }
                        break;
                    case 2:             //�����2������ʾΪ�๦����Ϣ��Dgn:ͨ�Ų��ԡ�
                        #region ��������
                        if (Arr_Tmp[0].ToLower() == "frozen")
                        {
                            TmpValue = this.GetColValue(PrintDataType.���Ṧ������, Group[Index][PrintDataType.���Ṧ������], Arr_Tmp[1].ToLower());
                            break;
                        }
                        #endregion

                        #region ���һ��������
                        else if (Arr_Tmp[0].ToLower() == "erroraccords")
                        {
                            TmpValue = this.GetColValue(PrintDataType.���һ��������, Group[Index][PrintDataType.���һ��������], Arr_Tmp[1].ToLower());
                            break;
                        }
                        #endregion

                        #region �¼���¼
                        if (Arr_Tmp[0].ToLower() == "eventlog")
                        {
                            TmpValue = this.GetColValue(PrintDataType.�¼���¼����, Group[Index][PrintDataType.�¼���¼����], Arr_Tmp[1].ToLower());
                            break;
                        }
                        #endregion

                        #region �ѿ�����
                        else if (Arr_Tmp[0].ToLower() == "costcontrol")
                        {
                            TmpValue = this.GetColValue(PrintDataType.�ѿع�������, Group[Index][PrintDataType.�ѿع�������], Arr_Tmp[1].ToLower());
                            break;
                        }
                        #endregion

                        #region Ӱ���� ���� ����
                        else if (Arr_Tmp[0].ToLower() == "special")
                        {
                            TmpValue = this.GetColValue(PrintDataType.����춨���� , Group[Index][PrintDataType.����춨���� ], Arr_Tmp[1]);
                            break;
                        }
                        #endregion

                        #region ���ܱ�������
                        else if (Arr_Tmp[0].ToLower() == "function")
                        {
                            TmpValue = this.GetColValue(PrintDataType.���ܱ������� , Group[Index][PrintDataType.���ܱ������� ], Arr_Tmp[1].ToLower());
                            break;
                        }
                        #endregion

                        #region �����ѿ��������
                        else if (Arr_Tmp[0].ToLower() == "nw")
                        {
                            TmpValue = this.GetColValue(PrintDataType.�����ѿ��������, Group[Index][PrintDataType.�����ѿ��������], Arr_Tmp[1].ToLower());
                        }
                        #endregion
                        #region ����
                        else
                        {
                            TmpValue = this.GetColValue(PrintDataType.�๦������, Group[Index][PrintDataType.�๦������], Arr_Tmp[1].ToLower());

                        }
                        break;
                        #endregion
                    case 3:             //�����3������ʾΪ�����Ϣ"P+:H10_Imax:WcHzz"
                        if(Arr_Tmp[0].ToLower() == "fk")
                            TmpValue = this.GetColValue(PrintDataType.�ѿع�������, Group[Index][PrintDataType.�ѿع�������], Arr_Tmp[1].ToLower());
                        else if(Arr_Tmp[0].ToLower() == "jl")
                            TmpValue = this.GetColValue(PrintDataType.���ܱ�������, Group[Index][PrintDataType.���ܱ�������], Arr_Tmp[1].ToLower());
                        else
                            TmpValue = this.GetColValue(PrintDataType.�������, Group[Index][PrintDataType.�������], Tmp_Label, clsMain.GetWcNum(Arr_Tmp[2], PrintInfo.PrintStyle, PrintTypeName));
                        break;
                    case 4:         //��������"ZZDATA:P+:FL:FL(FL\Qm\Zm\Wc\Result\Mc\zhwc\ZhResult)"
                        if (Arr_Tmp[0].ToLower() == "zzdata")
                        {
                            TmpValue = this.GetColValue(PrintDataType.��������, Group[Index][PrintDataType.��������], Tmp_Label);
                        }
                        break;
                    case 5:         //����춨
                        //��ʽLoop:18:dnbhead:Dnb_Err:dnbfoot
                        if (Arr_Tmp[0].ToLower() == "loop")
                        {
                            InsertLoopTableForDoc(Tmp_Label, Mark, ref Word_DocTemplet, Group, Index);
                            continue;
                        }


                        break;
                    case 6:         //һҳ����ѭ��ģʽ
                        /*
                           ��ʽ��Loop:P+:18:DnbHead:Dnb_Err:DnbFoot��Loopѭ����ʶ��:P+��ʾ�����й�(�ò�����ʱ����):18��ʾһҳ18����¼:
                           DnbHead��ʾ��Ҫ���õ�ģ��ͷ�ļ�����(����Ҫѭ��):Dnb_Err��ʾ�����м���Ҫѭ����ģ��:Dnb_Foot��ʾ��Ҫ���õ�β��ģ���ļ�����(����Ҫѭ��)
                           �����ͷģ���βģ�嶼����ʡ��,���û��ͷ��β,����Ҫ����Ӧ����дΪNull:Loop:P+:18:Null:Dnb_Err:Null
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
            //����ò���ͼƬ�Ƿ�Ϊ�ⲿ����
            object linkToFile = true;

            object filename = FilePath;
            //�������ͼƬ�Ƿ���word�ĵ�һ�𱣴�
            object saveWithDocument = true;
            object Nothing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref filename, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            try
            {
                //��ǩ
                
                //ͼƬ
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
                    //������ǩ
                    doc.Bookmarks.get_Item(ref bookMark).Select();
                    //����ͼƬλ��
                    wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                    //����ǩ��λ�����ͼƬ
                    //Microsoft.Office.Interop.Word.InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    Microsoft.Office.Interop.Word.InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    //����ͼƬ��С
                    inlineShape.Width = 180;
                    inlineShape.Height = 50;
                    doc.Save();
                    doc.Close(ref Nothing, ref Nothing, ref Nothing);
                }
                else
                {
                    //word�ĵ��в����ڸ���ǩ���ر��ĵ�
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
            //����ò���ͼƬ�Ƿ�Ϊ�ⲿ����
            object linkToFile = true;

            object filename = FilePath;
            //�������ͼƬ�Ƿ���word�ĵ�һ�𱣴�
            object saveWithDocument = true;
            object Nothing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref filename, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            try
            {
                //��ǩ

                //ͼƬ
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
                    //������ǩ
                    doc.Bookmarks.get_Item(ref bookMark).Select();
                    //����ͼƬλ��
                    wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    //����ǩ��λ�����ͼƬ
                    //Microsoft.Office.Interop.Word.InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    Microsoft.Office.Interop.Word.InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    //����ͼƬ��С
                    inlineShape.Width = 100;
                    inlineShape.Height = 25;
                    doc.Save();
                   
                    doc.Close(ref Nothing, ref Nothing, ref Nothing);
                    string RealPath = filename.ToString();
                    newApp.Convert(RealPath, RealPath.Substring(0, RealPath.LastIndexOf(".")) + ".PDF", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
            
                    
                 
                   
                }
                else
                {
                    //word�ĵ��в����ڸ���ǩ���ر��ĵ�
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
            //����ò���ͼƬ�Ƿ�Ϊ�ⲿ����
            clsWordControl newApp = new clsWordControl();
          
            object linkToFile = true;

            object filename = FilePath;
            //�������ͼƬ�Ƿ���word�ĵ�һ�𱣴�
            object saveWithDocument = true;
            object Nothing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Document doc = wordApp.Documents.Open(ref filename, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing,
          ref Nothing, ref Nothing, ref Nothing, ref Nothing);
            try
            {
                //��ǩ

                //ͼƬ
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
                    //������ǩ
                    doc.Bookmarks.get_Item(ref bookMark).Select();
                    //����ͼƬλ��
                    wordApp.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                    //����ǩ��λ�����ͼƬ
                    //Microsoft.Office.Interop.Word.InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    Microsoft.Office.Interop.Word.InlineShape inlineShape = wordApp.Selection.InlineShapes.AddPicture(replacePic, ref linkToFile, ref saveWithDocument, ref Nothing);
                    //����ͼƬ��С
                    inlineShape.Width = 110;
                    inlineShape.Height = 110;
                    doc.Save();
                    doc.Close(ref Nothing, ref Nothing, ref Nothing);
                    string RealPath = filename.ToString();
                    newApp.Convert(RealPath, RealPath.Substring(0, RealPath.LastIndexOf(".")) + ".PDF", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
                }
                else
                {
                    //word�ĵ��в����ڸ���ǩ���ر��ĵ�
                    doc.Close(ref Nothing, ref Nothing, ref Nothing);
                }

            }
            catch
            {
                doc.Close(ref Nothing, ref Nothing, ref Nothing);
            }
        }
        /// <summary>
        /// ���ڲ�ѭ����Ŀǰ�������ں��ϵ��Ժ�ͺ��Ϲ��繫˾������ʽ
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

            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError> Items = (Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError>)Group[groupIndex][PrintDataType.�������];

            string xiangxian = ((Dictionary<string, string>)Group[groupIndex][PrintDataType.������Ϣ])["p_xiangxian"];

            string dl = CLDC_DataCore.Function.Number.GetCurrentByIb("ib", ((Dictionary<string, string>)Group[groupIndex][PrintDataType.������Ϣ])["i"]).ToString();

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
        /// �����������ⱨ��ģ��������ǩ�滻
        /// </summary>
        /// <param name="Word_DocTemplet"></param>
        /// <param name="Item"></param>
        /// <param name="errKey"></param>
        private void ReplaceReport(ref Microsoft.Office.Interop.Word.Document Word_DocTemplet, Dictionary<PrintDataType, object> Item, string errKey)
        {


            CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError ErrorItem = ((Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError>)Item[PrintDataType.�������])[errKey];

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
                        TmpValue = string.Format("{0}A", CLDC_DataCore.Function.Number.GetCurrentByIb("ib", ((Dictionary<string, string>)Item[PrintDataType.������Ϣ])["i"]));
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
                            if (((Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError>)Item[PrintDataType.�������]).ContainsKey(string.Format("2{0}", errKey.Substring(1))))
                            {
                                string[] tmpwc = ((Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError>)Item[PrintDataType.�������])[string.Format("2{0}", errKey.Substring(1))].Me_chrWcMore.Split('|');
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
        /// ѭ����������
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
                *����Ϊѭ���Ľṹ
                *
                *----->Dnbheadͷ��     End
                *-----> Dnb_Err        1
                *-----> .................
                *-----> Dnb_Err        16
                *-----> Dnb_Err        17
                *-----> Dnb_Err        18
                *----->DnbFoot�ײ�    Start
                *
                *ÿ�β������ݶ��ǽ����ڵ����������ƶ�,���ڵ�ǰλ�ò���һ��������
             */
            for (int i = Group.Count - 1; i >= 0; i--)
            {
                System.Threading.Thread.Sleep(1);

                if ((i + 1) % int.Parse(Arr_LoopInfo[2]) == 0 || i == Group.Count - 1)       //��������һ��������ÿ����ҳ��
                {
                    if (Arr_LoopInfo[5].ToLower() != "null")   //����ײ������ڵĻ�
                    {
                        Microsoft.Office.Interop.Word.Document LoopDoc = WordApp.Doc(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[5]));

                        this.ReplaceReport(ref LoopDoc, Group, i, CurPage, MaxPage);
                        clsWordControl.CopyPasteTable(ref Word_ActiveRange, ref LoopDoc, 1);
                    }

                    if (i == Group.Count - 1 && Group.Count % int.Parse(Arr_LoopInfo[2]) != 0)  //������ܱ��������ܸպ������һҳ����ô����Ҫ����һ�������Ŀ���
                    {
                        int LoopNullRow = int.Parse(Arr_LoopInfo[2]) - (Group.Count % int.Parse(Arr_LoopInfo[2]));

                        for (int j = 0; j < LoopNullRow; j++)           //ѭ���������,���һ�����뵱ǰ��Ч��
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

                if (i % int.Parse(Arr_LoopInfo[2]) == 0 && Arr_LoopInfo[3].ToLower() != "null")   //����Ǽ���ѭ���������߸պõ����ҳʱ,����Ҫ����һ��ͷ
                {
                    Microsoft.Office.Interop.Word.Document LoopDoc = WordApp.Doc(string.Format(@"{0}\{1}.doc", clsMain.getFilePath("res"), Arr_LoopInfo[3]));
                    this.ReplaceReport(ref LoopDoc, Group, i, CurPage, MaxPage);
                    clsWordControl.CopyPasteTable(ref Word_ActiveRange, ref LoopDoc, 1);
                }
            }

        }

        #endregion


        #region -----------------------------------���ݽ���-----------------------------
        /// <summary>
        /// ��ȡ��ǩ��Ӧ�ؼ�������
        /// </summary>
        /// <param name="DataType">��������</param>
        /// <param name="Items">���ݶ���</param>
        /// <param name="KeyValue">�ؼ���</param>
        /// <returns></returns>
        private string GetColValue(PrintDataType DataType, object Items, string KeyValue)
        {
            return this.GetColValue(DataType, Items, KeyValue, 0);
        }


        /// <summary>
        /// ��ȡ��ǩ��Ӧ�ؼ�������
        /// </summary>
        /// <param name="DataType">��������</param>
        /// <param name="Items">���ݶ���</param>
        /// <param name="KeyValue">�ؼ���</param>
        /// <param name="WcNum">��������0=һ����1=��������������-1=���ƽ��ֵ��-2=����ֵ��</param>
        /// <returns></returns>
        private string GetColValue(PrintDataType DataType, object Items, string KeyValue, int WcNum)
        {
            try
            {
                switch (DataType)
                {
                    #region �ѿع�������
                    case PrintDataType.�ѿع�������:
                        {
                            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFK> meters = Items as Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterFK>;
                            string[] key = KeyValue.Split('_');
                            if (key.Length == 1)
                            {

                                if (meters.ContainsKey(key[0]) && key[0].ToLower() != "result")
                                {//�ѿ��Ӽ춨�����
                                    return meters[key[0]].Mfk_chrJL;
                                }
                                else if (key[0].ToLower() == "result")
                                {//�ѿ��ܽ���
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

                    #region ���һ��������
                    case PrintDataType.���һ��������:
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

                    #region ��������
                    case PrintDataType.���Ṧ������:
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
                                        if (_key.Length != 3) continue;//��ID 
                                        if (meters[_key].Md_chrValue == "���ϸ�")
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

                    #region �¼���¼����
                    case PrintDataType.�¼���¼����:
                        {//����|�ܴ���|���ۼ�ʱ��|���һ�η���ʱ��
                            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSjJLgn> meters = Items as Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterSjJLgn>;
                            string[] key = KeyValue.Split('_');
                            if (key.Length == 1 && key[0].ToLower() == "result")
                            {//�ܽ���
                                    foreach (string _key in meters.Keys)
                                    {
                                        if (_key.Length != 3) continue;//��ID 
                                        if (meters[_key].ItemConc == "���ϸ�")
                                        {
                                            return meters[_key].ItemConc;
                                        }
                                        return CLDC_DataCore.Const.Variable.CTG_HeGe;
                                    }
                            }
                            else if (key.Length == 2)
                            {//���¼���¼����
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

                    #region ���ܱ�������
                    case PrintDataType.���ܱ������� :
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
                                        if (_key.Length != 3) continue;//��ID 
                                        if (meters[_key].Mf_chrValue == "���ϸ�")
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

                    #region ����춨���� Ӱ����
                    case PrintDataType.����춨���� :
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
                                //      //  if (_key.Length != 3) continue;//��ID 
                                //        if (meters[_key].Mf_chrValue == "���ϸ�")
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
                                int Index = 2;  //����|ƽ��ֵ|����ֵ|���1|���2
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

                    case PrintDataType.Ǳ��������:
                        {
                            string value = "�ϸ�";

                            Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterQdQid> meters = Items as Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterQdQid>;
                            if (KeyValue == "creeping")
                            {
                                foreach (CLDC_DataCore.Model.DnbModel.DnbInfo.MeterQdQid m in meters.Values)
                                {
                                    if (m.Mqd_chrProjectName.Contains("Ǳ��"))
                                    {
                                        if (m.Mqd_chrJL.Contains("���ϸ�"))
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
                                    if (m.Mqd_chrProjectName.Contains("��"))
                                    {
                                        if (m.Mqd_chrJL.Contains("���ϸ�"))
                                        {
                                            return m.Mqd_chrJL;
                                        }
                                    }
                                }
                            }
                            return value;
                        }
                    case PrintDataType.������Ϣ:
                        KeyValue = KeyValue.Trim();
                        KeyValue = KeyValue.Replace(" ", "");
                        if (!((Dictionary<string, string>)Items).ContainsKey(KeyValue))
                        {
                            clsMain.WriteReportErr(2, string.Format("����Ļ�����Ϣ�ؼ��֣�{0}", KeyValue));
                            return "";
                        }
                        return ((Dictionary<string, string>)Items)[KeyValue];
                    case PrintDataType.�������:

                        
                        CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError errorItem = clsMain.getErrorItem((Dictionary<string, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError>)Items, KeyValue);

                        if (errorItem == null)
                        {
                            clsMain.WriteReportErr(2, string.Format("�����������ݹؼ��֣�{0}", KeyValue));
                            return "";
                        }

                        string[] WcArr = errorItem.Me_chrWcMore.Split('|');
                        if (WcNum > WcArr.Length - 2)
                        {
                            clsMain.WriteReportErr(3, string.Format("������������ؼ��֣�{0}", WcNum));
                            return "";
                        }
                        string WcString = "";
                        if (WcNum == -1)       //ƽ��ֵ
                        {
                            WcString = WcArr[WcArr.Length - 2];
                        }
                        else if (WcNum == -2)      //����ֵ
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
                        {//ƽ���벻ƽ�⸺�����֮�� ƽ��ֵ
                            WcString = errorItem.AVR_DIF_ERR_AVG;
                        }
                        else if (WcNum == -4)
                        {//ƽ���벻ƽ�⸺�����֮�� ����ֵ
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
                    case PrintDataType.�๦������:
                        if (((Dictionary<string, string>)Items).Count == 0) return "";
                        if (!((Dictionary<string, string>)Items).ContainsKey(KeyValue))
                        {
                            clsMain.WriteReportErr(2, string.Format("����Ķ๦�����ݹؼ��֣�{0}", KeyValue));
                            return "";
                        }
                        return ((Dictionary<string, string>)Items)[KeyValue];
                    case PrintDataType.��������:               //"ZZDATA:P+:FL:FL(FL\Qm\Zm\Wc\Result\Mc\zhwc\ZhResult)"
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
                    case PrintDataType.�����ѿ��������:
                        if (((Dictionary<string, string>)Items).Count == 0) return "";
                        if (!((Dictionary<string, string>)Items).ContainsKey(KeyValue))
                        {
                            clsMain.WriteReportErr(2, string.Format("���������������ݹؼ��֣�{0}", KeyValue));
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


        #region------------------------�ֵ�����----------------------------

        /// <summary>
        /// ������Ϣ�ֵ���������������Ϣ
        /// </summary>
        /// <param name="BasicInfo"></param>
        private void AddDWSetting(Dictionary<string, string> BasicInfo)
        {
            BasicInfo.Add("jdyj", this.chrJdyj);        //�춨����
            BasicInfo.Add("zzbz", this.chrZzbz);        //�����׼
            BasicInfo.Add("rpthead", clsMain.getIniString("ReportInfo", "Head"));           //����ͷ����
            BasicInfo.Add("checkadr", clsMain.getIniString("ReportInfo", "CheckAdr"));      //���ص�
            BasicInfo.Add("adr", clsMain.getIniString("ReportInfo", "adr"));                //��λ��ַ
            BasicInfo.Add("tel", clsMain.getIniString("ReportInfo", "tel"));                //��ϵ�绰
            BasicInfo.Add("erpthead", clsMain.getIniString("ReportInfo", "ehead"));         //���ı���ͷ
            BasicInfo.Add("fax", clsMain.getIniString("ReportInfo", "tex"));                //����
            BasicInfo.Add("mail", clsMain.getIniString("ReportInfo", "email"));             //�ʼ�
            BasicInfo.Add("post", clsMain.getIniString("ReportInfo", "zip"));               //�ʱ�
            BasicInfo.Add("sqzs", clsMain.getIniString("ReportInfo", "num"));                //��Ȩ֤��
            BasicInfo.Add("sqdw", clsMain.getIniString("ReportInfo", "dw"));                //��Ȩ��λ
            BasicInfo.Add("pagehead", clsMain.getIniString("ReportInfo", "pagehead"));      //ҳü
            if (CLDC_DataCore.Function.DateTimes.IsDate(BasicInfo["checkdate"].ToString()))
                BasicInfo.Add("formatcheckdate", DateTime.Parse(BasicInfo["checkdate"]).ToString("yyyy   ��   MM   ��    dd    ��"));
            else
                BasicInfo.Add("formatcheckdate", DateTime.Now.ToString());
            if (CLDC_DataCore.Function.DateTimes.IsDate(BasicInfo["jjdate"]))
                BasicInfo.Add("formatjjdate", DateTime.Parse(BasicInfo["jjdate"]).ToString("yyyy   ��   MM   ��    dd    ��"));
            else
                BasicInfo.Add("formatjjdate", DateTime.Now.ToString());
            CheckerName = BasicInfo["checker"].ToString();
            TesterName = BasicInfo["verificationer"].ToString();
            chargeName = BasicInfo["charge"].ToString();
            
            if (CLDC_DataCore.Function.DateTimes.IsDate(BasicInfo["checkdate"]))
            {
             BasicInfo.Add("meternowtime", DateTime.Parse(BasicInfo["jjdate"]).ToString("HH:mm:ss"));
             BasicInfo.Add("duancheckdate", DateTime.Parse(BasicInfo["checkdate"]).ToString("yyyy��MM��dd��"));
            }
                
            else
                BasicInfo.Add("duancheckdate", DateTime.Now.ToString());
            if (CLDC_DataCore.Function.DateTimes.IsDate(BasicInfo["jjdate"]))
            {
                BasicInfo.Add("duanjjdate", DateTime.Parse(BasicInfo["jjdate"]).ToString("yyyy��MM��dd��"));
               
            }
                
            else
                BasicInfo.Add("duanjjdate", DateTime.Now.ToString());

            string Tmp_Value = ((CLDC_Comm.Enum.Cus_Clfs)int.Parse(BasicInfo["checktype"])).ToString();
            double Meterlevel_yg = Convert.ToDouble(CLDC_DataCore.Function.Number.getDj(BasicInfo["meterlevel"])[0]);
            

            if (CLDC_DataCore.Function.DateTimes.IsDate(BasicInfo["checkdate"]))
            {
                if (Meterlevel_yg >=1)
                {
                    BasicInfo.Add("yxqforcheckdate", DateTime.Parse(BasicInfo["checkdate"].ToString()).AddYears(8).AddDays(-1).ToString("yyyy��MM��dd��"));
                }
                else
                {
                    BasicInfo.Add("yxqforcheckdate", DateTime.Parse(BasicInfo["checkdate"].ToString()).AddYears(6).AddDays(-1).ToString("yyyy��MM��dd��"));
                }
            }
            BasicInfo.Add("p_xiangxian", Tmp_Value);

            Tmp_Value = BasicInfo["znq"];

            BasicInfo.Remove("znq");
            if (Tmp_Value == "true")
            {
                BasicInfo.Add("znq", "��ֹ��");
            }
            else
            {
                BasicInfo.Add("znq", "��ֹ��");
            }
            Tmp_Value = BasicInfo["hgq"];

            BasicInfo.Remove("hgq");
            if (Tmp_Value == "True")
            {
                BasicInfo.Add("hgq", "��������");
            }
            else
            {
                BasicInfo.Add("hgq", "ֱ�ӽ���");
            }

           
            BasicInfo.Add("yglevel", Convert.ToDouble(CLDC_DataCore.Function.Number.getDj(BasicInfo["meterlevel"])[0]).ToString());   //�й��ȼ�
            //MeterYGlevel = Convert.ToDouble(CLDC_DataCore.Function.Number.getDj(BasicInfo["meterlevel"])[0]);
            BasicInfo.Add("wglevel", Convert.ToInt16(Convert.ToDouble(CLDC_DataCore.Function.Number.getDj(BasicInfo["meterlevel"])[1])).ToString());   //�޹��ȼ�
            string sTrConstDw = "";
            if (BasicInfo["metertype"].IndexOf("��Ӧ") >= 0 || BasicInfo["metertype"].IndexOf("����") >= 0 || BasicInfo["metertype"].IndexOf("��е") >= 0)
            {
                sTrConstDw = "r";
            }
            else
            {
                sTrConstDw = "imp";
            }

            //տ��Ҫ��
            BasicInfo.Add("ygconst", string.Format("{0} {1}/kWh��{1}/kvarh��", CLDC_DataCore.Function.Number.GetBcs(BasicInfo["meterconst"], true), sTrConstDw));  //�й�����
        
            //
            //BasicInfo.Add("ygconst", string.Format("{0} {1}/kWh", CLDC_DataCore.Function.Number.GetBcs(BasicInfo["meterconst"], true), sTrConstDw));  //�й�����
            //BasicInfo.Add("wgconst", string.Format("{0} {1}/kvarh", CLDC_DataCore.Function.Number.GetBcs(BasicInfo["meterconst"], false), sTrConstDw));      //�޹�����

        }
        /// <summary>
        /// ��ȡ������Ϣ�ͽ�����Ϣ�ֵ�
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
            for (int i = 0; i < InfoTypes.Length; i++)              //����������Ϣ��
            {
                string sTrKey = clsMain.getIniString("CL3000SH", InfoTypes[i].Name, "", clsMain.getFilePath("ReportConfig.ini"));

                if (sTrKey == string.Empty) continue;
                #region ��ѹ ����
                if (sTrKey == "U"&&InfoTypes[16].GetValue(Item).ToString()=="0")
                {
                    switch (InfoTypes[i].GetValue(Item).ToString())
                    { 
                        case "220":
                            tempZsbh = "3��" + InfoTypes[i].GetValue(Item).ToString() + @"/380V";
                            break;
                        case "100":
                            tempZsbh = "3��" + InfoTypes[i].GetValue(Item).ToString()+"V";
                            break;
                        case "57.7":
                            tempZsbh = "3��" + InfoTypes[i].GetValue(Item).ToString() + @"/100V";
                            break;          
                        default:
                            break;
                    }
                    

                    DicInfo.Add(sTrKey.ToLower(), tempZsbh);
                    continue;
                }
                if (sTrKey == "I" && InfoTypes[16].GetValue(Item).ToString() == "0")
                {

                    tempZsbh = "3��" + InfoTypes[i].GetValue(Item).ToString();

                    DicInfo.Add(sTrKey.ToLower(), tempZsbh);
                    continue;
                }
                #endregion 
                
                if (sTrKey == "MeterConst")
                {
                    if (InfoTypes[8].GetValue(Item).ToString() == "��Ӧʽ")
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
                        //���ݾ�����֤��
                        if (Str_Start == "J")
                        {
                            DicInfo.Add(sTrKey.ToLower(), Str_Start + "Y" + Str_HGQ + EquiementNum + Convert.ToDateTime(Item.Mb_DatJdrq).ToString("yyyyMMdd") + str_Zsbh_last + @"(1/1)");
                            // ���ݾ�����֤��
                            clsMain.WriteIni("OtherInfo", "ZSBH_" + TimeYear, (Convert.ToInt16(str_Zsbh_last) + 1).ToString().PadLeft(5, '0'));
                            continue;
                        }
                        else
                        {
                            DicInfo.Add(sTrKey.ToLower(), Str_Start + "Y" + Str_HGQ + EquiementNum + Convert.ToDateTime(Item.Mb_DatJdrq).ToString("yyyyMMdd") + str_Zsbh_last.PadLeft(6, '0') + @"(1/1)");
                            // ���ݾ�����֤��
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

            foreach (string key in Item.MeterExtend.Keys)           //������չ��
            {
                string sTrKey = clsMain.getIniString("CL3000SH", key, "", clsMain.getFilePath("ReportConfig.ini"));
                if (sTrKey == string.Empty) continue;
                DicInfo.Add(sTrKey.ToLower(), Item.MeterExtend[key]);
            }

            if (IndexFist == "1")
            {
                DicInfo.Add("bzbname", clsMain.getIniString("DeviceInfo", "txt_BzbName"));    //��׼������
                DicInfo.Add("bzbtype", clsMain.getIniString("DeviceInfo", "txt_BzbType"));    //��׼������
                DicInfo.Add("bzbno", clsMain.getIniString("DeviceInfo", "txt_BzbNum"));    //��׼����
                DicInfo.Add("bzbclfw", clsMain.getIniString("DeviceInfo", "txt_BzbRange"));    //��׼�������Χ
                DicInfo.Add("bzbzqd", clsMain.getIniString("DeviceInfo", "txt_BzbRank"));    //��׼��ȼ�
                DicInfo.Add("bzbzsbh", clsMain.getIniString("DeviceInfo", "txt_BzbCertificate"));    //��׼��֤��
                DicInfo.Add("bzbyxq", clsMain.getIniString("DeviceInfo", "txt_BzbValidity"));    //��׼����Ч��

                DicInfo.Add("uncertainty1", clsMain.getIniString("DeviceInfo", "txt_Unsure"));
                DicInfo.Add("uncertainty2", clsMain.getIniString("DeviceInfo", "txt_Unsure02"));
                DicInfo.Add("uncertainty3", clsMain.getIniString("DeviceInfo", "txt_Unsure03"));
                DicInfo.Add("uncertainty4", clsMain.getIniString("DeviceInfo", "txt_Unsure04"));
                DicInfo.Add("uncertainty5", clsMain.getIniString("DeviceInfo", "txt_Unsure05"));
                DicInfo.Add("uncertainty6", clsMain.getIniString("DeviceInfo", "txt_Unsure06"));
                DicInfo.Add("uncertainty7", clsMain.getIniString("DeviceInfo", "txt_Unsure07")); 

                DicInfo.Add("setname", clsMain.getIniString("DeviceInfo", "txt_ZzName"));    //װ������
                DicInfo.Add("settype", clsMain.getIniString("DeviceInfo", "txt_ZzType"));    //װ������
                DicInfo.Add("setno", clsMain.getIniString("DeviceInfo", "txt_ZzNum"));    //װ�ñ��
                DicInfo.Add("setclfw", clsMain.getIniString("DeviceInfo", "txt_ZzRange"));    //װ�ò�����Χ
                DicInfo.Add("setzqd", clsMain.getIniString("DeviceInfo", "txt_ZzRank"));    //װ�õȼ�
                DicInfo.Add("setzsbh", clsMain.getIniString("DeviceInfo", "txt_ZzCertificate"));    //װ��֤��
                DicInfo.Add("setyxq", clsMain.getIniString("DeviceInfo", "txt_ZzValidity"));    //װ����Ч��

                IndexFist = "2";
            }
            
            

            foreach (string Key in Item.MeterResults.Keys)
            {
                string sTrKey = clsMain.getIniString("CL3000SH_Result", "R_" + Key, "", clsMain.getFilePath("ReportConfig.ini"));

                if (sTrKey == string.Empty) continue;
                string[] Arr_Tmp = sTrKey.Split(':');
                if (Arr_Tmp.Length > 1) //����1����������Ϣ
                {
                    DicInfo.Add(Arr_Tmp[0].ToLower(), Item.MeterResults[Key].Mr_chrRstValue);
                    //DicInfo.Add(Arr_Tmp[1].ToLower(), Item.MeterResults[Key].Mr_Time + "��");
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
        /// ��ȡ���ܱ�������ϸ��Ϣ
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


              OtherDict.Add("����ǰ����", Item[RunElectric].Mz_chrQiMa.ToString() + @"/kW��h");
              OtherDict.Add("���ֺ����", Item[RunElectric].Mz_chrZiMa.ToString() + @"/kW��h");
              OtherDict.Add("���ֵ�����ֵ", Convert.ToDouble(Item[RunElectric].Mz_chrQiZiMaC.ToString()).ToString("0.00") + @"/kW��h");
                
            }
            

            
        }

        /// <summary>
        /// ��ȡ���ܱ�Ǳ������ϸ��Ϣ
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

                    OtherDict.Add("��׼��ʱ��", (Math.Ceiling(Convert.ToDecimal(Item["1091"].AVR_STANDARD_TIME) * 60).ToString() + @"s"));
                    OtherDict.Add("ʵ����ʱ��", (Math.Ceiling(Convert.ToDecimal(Item["1091"].AVR_ACTUAL_TIME) * 60).ToString() + @"s"));
                    OtherDict.Add("��׼Ǳ��ʱ��", (Math.Ceiling(Convert.ToDecimal(Item["1101115"].AVR_STANDARD_TIME) * 60).ToString() + @"s"));
                    OtherDict.Add("ʵ��Ǳ��ʱ��", (Math.Ceiling(Convert.ToDecimal(Item["1101115"].AVR_ACTUAL_TIME) * 60).ToString() + @"s"));

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
        /// ��ȡ�๦�������ֵ�
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetDicDgnInfo(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item)
        {
            Dictionary<string, string> DgnDic = new Dictionary<string, string>();
            Dictionary<string, string> DgnJdqszzhwc = new Dictionary<string, string>();
            foreach (string Key in Item.MeterDgns.Keys)
            {
                if (Key.Substring(0,3) == "005")//�ƶ���ʾֵ������
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

            //add by zxr in the nanchang 20140814  ���Ӵ���ƶ���ʾֵ���������� �����ӡ���
            #region �ƶ���
            if (DgnJdqszzhwc.Count > 4)
            {
                int dgncout = DgnJdqszzhwc.Count;
                //�ƶ���ʾֵ�������ܽ���
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
                                DgnDic.Add("���������", DgnJdqszzhwc[skey]);
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
                                    if (!DgnDic.ContainsKey("������" + sfeilv + "��ֵ"))
                                    {
                                        DgnDic.Add("������" + sfeilv + "��ֵ", str_Value[2]);
                                        DgnDic.Add("������" + sfeilv + "��ʼֵ", str_Value[0]);
                                        DgnDic.Add("������" + sfeilv + "���к�ֵ", str_Value[1]);
                                    }
                                        

                                    if (skey != "00500")
                                    {
                                        ffeilvhe += Convert.ToSingle(str_Value[2]);
                                    }
                                }
                                else
                                {
                                    if (str_Value.Length == 1 && sZongwc == skey)//��������
                                    {
                                        if (!DgnDic.ContainsKey("������"))
                                            DgnDic.Add("������", str_Value[0]);
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }

                DgnDic.Add("�������ʱ��", ffeilvhe.ToString("0.0000"));

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
                NwInfo.Add("�����ʹ��ǰ", MoneyBefore);
                NwInfo.Add("�����ʹ�ú�", MoneyAfter);
                NwInfo.Add("�����仯ֵ", MoneyChange);
                NwInfo.Add("ʣ������ݼ�����", "�ϸ�");
                foreach (string Key in Item.MeterOtherSoftData.Keys)
                {
                    if (Key == "���ݵ�۽���")
                    {
                        NwInfoMake.Add(Key, Item.MeterOtherSoftData[Key].Mosd_chrValue);
                        ValueList = Item.MeterOtherSoftData[Key].Mosd_chrValue.Split(new string[] {@"^" }, StringSplitOptions.RemoveEmptyEntries);
                       // ValueList = System.Text.RegularExpressions.Regex.Split(Item.MeterOtherSoftData[Key].Mosd_chrValue, @"^", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        MoneyBefore = ValueList[ValueList.Length - 7];
                        MoneyAfter = ValueList[ValueList.Length - 6];
                        MoneyChange = ValueList[ValueList.Length - 4];
                        NwInfo.Remove("�����ʹ��ǰ");
                        NwInfo.Remove("�����ʹ�ú�");
                        NwInfo.Remove("�����仯ֵ");
                        NwInfo.Remove("ʣ������ݼ�����");
                        NwInfo.Add("�����ʹ��ǰ", MoneyBefore);
                        NwInfo.Add("�����ʹ�ú�", MoneyAfter);
                        NwInfo.Add("�����仯ֵ", MoneyChange);
                        NwInfo.Add("ʣ������ݼ�����", Item.MeterOtherSoftData[Key].AVR_CONCLUSION);
                    }
                    if (Key == "��ʱ���ʵ�۽���")
                    {
                        NwInfoMake.Add(Key, Item.MeterOtherSoftData[Key].Mosd_chrValue);
                        ValueList = System.Text.RegularExpressions.Regex.Split(Item.MeterOtherSoftData[Key].Mosd_chrValue, "^", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        MoneyBefore = ValueList[ValueList.Length - 7];
                        MoneyAfter = ValueList[ValueList.Length - 6];
                        MoneyChange = ValueList[ValueList.Length - 4];
                        if (NwInfo.ContainsKey("�����ʹ��ǰ") && NwInfo["�����ʹ��ǰ"] == "")
                        {
                            NwInfo.Remove("�����ʹ��ǰ");
                            NwInfo.Remove("�����ʹ�ú�");
                            NwInfo.Remove("�����仯ֵ");
                            NwInfo.Remove("ʣ������ݼ�����");
                            NwInfo.Add("�����ʹ��ǰ", MoneyBefore);
                            NwInfo.Add("�����ʹ�ú�", MoneyAfter);
                            NwInfo.Add("�����仯ֵ", MoneyChange);
                            NwInfo.Add("ʣ������ݼ�����", Item.MeterOtherSoftData[Key].AVR_CONCLUSION);
                        }
                        
                    }
                }
                 NwInfo.Add("ʣ�������Ԥ�豨��","10");
                 NwInfo.Add("ʣ�������ʵ�ʱ���","10");
                 NwInfo.Add("ʣ�������Ԥ��ϵ�","0");
                 NwInfo.Add("ʣ�������ʵ�ʶϵ�","0");
                return NwInfo;
            }
            catch
            {
                Dictionary<string, string> NwInfo = new Dictionary<string, string>();
                NwInfo.Add("�����ʹ��ǰ", @"0");
                NwInfo.Add("�����ʹ�ú�", @"0");
                NwInfo.Add("�����仯ֵ", @"0");
                NwInfo.Add("ʣ������ݼ�����", @"δ�춨");
                return NwInfo;
            }
          
        }
        #endregion

        #region-------------------ģ���ȡ----------------------
        /// <summary>
        /// ��ȡģ������
        /// </summary>
        /// <param name="DxYn">�Ƿ��ǵ����</param>
        /// <param name="MeterInfo">����Ϣ</param>
        /// <param name="ReportTao">ģ������</param>
        /// <param name="ReportType">ģ�����ͣ�֤�顣ԭʼ��¼����������</param>
        /// <returns></returns>
        private string getReportTemplet(bool DxYn, CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo MeterInfo, int ReportTao, int ReportType)
        {
            int[] intType = new int[3];

            intType[2] = ReportType;

            bool blnPz = MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.�����������).ToString() + ((int)CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�).ToString());

            bool blnPf = MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.�����������).ToString() + ((int)CLDC_Comm.Enum.Cus_PowerFangXiang.�����й�).ToString());

            bool blnQz = MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.�����������).ToString() + ((int)CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�).ToString());

            bool blnQf = MeterInfo.MeterResults.ContainsKey(((int)CLDC_Comm.Enum.Cus_MeterResultPrjID.�����������).ToString() + ((int)CLDC_Comm.Enum.Cus_PowerFangXiang.�����޹�).ToString());


            if (DxYn)
            {
                intType[1] = 6;
            }
            else if ((blnPz && blnPf && blnQz && blnQf) || MeterInfo.Mb_chrBlx.IndexOf("�๦��") >= 0)   //�๦��
            {
                intType[1] = 5;
            }
            else if (blnPz && blnQz)        //����һ
            {
                intType[1] = 4;
            }
            else if (blnQz && blnQf)        //˫���޹�
            {
                intType[1] = 3;
            }
            else if (blnPz && blnPf)        //˫���й�
            {
                intType[1] = 2;
            }
            else if (blnQz)                 //��ͨ�У��޹�
            {
                intType[1] = 1;     //�޹�
            }
            else
            {
                intType[1] = 0;     //�й�
            }

            if (MeterInfo.Mb_chrBlx.IndexOf("��е") >= 0 || MeterInfo.Mb_chrBlx.IndexOf("����") >= 0 || MeterInfo.Mb_chrBlx.IndexOf("��Ӧ") >= 0)
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
            return sTrTemplet; //���ر�����õ�ģ������

        }
        #endregion

    }
    


    
         struct PrintOtherInfo
    {
        /// <summary>
        /// �Ƿ��ӡ����Ա
        /// </summary>
        public bool PrintHuman;
        /// <summary>
        /// ���ϸ��־
        /// </summary>
        public string BHGString;
        /// <summary>
        /// �Ƿ���Ҫ����
        /// </summary>
        public bool Saving;
        /// <summary>
        /// �Ƿ���浵
        /// </summary>
        public bool SaveOnly;
        /// <summary>
        /// �浵��ʽ
        /// </summary>
        public int SaveBag;
        /// <summary>
        /// �Ƿ�Ԥ��
        /// </summary>
        public bool Preview;
        /// <summary>
        /// ��ӡ��ʽ
        /// </summary>
        public int PrintStyle;
        /// <summary>
        /// �浵·��
        /// </summary>
        public string SavePath;
        /// <summary>
        /// δ���־
        /// </summary>
        public string NotCheck;



    }



    }



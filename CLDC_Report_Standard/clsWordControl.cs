using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop;
using System.IO;
namespace CLReport_Standard
{
    /// <summary>
    /// word�ĵ�����������
    /// </summary>
    internal class clsWordControl
    {
        private Microsoft.Office.Interop.Word.ApplicationClass WordApplic;


        public static object missing = System.Reflection.Missing.Value;

        private bool IsMyCreate = false;


        private List<Microsoft.Office.Interop.Word.Document> WordDocArr;


        /// <summary>
        /// ���캯��������һ��WORDȫ�ֶ���
        /// </summary>
        public clsWordControl()
        {
            try
            {
                WordApplic = (Microsoft.Office.Interop.Word.ApplicationClass)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
                IsMyCreate = false;
            }
            catch
            { }
			try
            {
				if (WordApplic == null)
				{
					WordApplic = new Microsoft.Office.Interop.Word.ApplicationClass();
					IsMyCreate = true;
				}
			}
            catch
            { }
        }

        /// <summary>
        /// �ĵ�����
        /// </summary>
        /// <param name="Doc">�ĵ�����</param>
        /// <param name="DocPassword">����</param>
        public void InsertEditPwd(ref Microsoft.Office.Interop.Word.Document Doc, string DocPassword)
        {
            object NoReset = false;
            object Pwd = DocPassword;
            object enforce = false;
            Doc.Protect(Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyReading, ref NoReset, ref Pwd, ref missing, ref enforce);

        }

        public void ClosePwd(Microsoft.Office.Interop.Word.Document Doc, string DocPassword)
        {
            try
            {
                object Pwd = DocPassword;
                Doc.Unprotect(ref Pwd);
            }
            catch(Exception closeException)
            { 
            
            }
           
        }


        /// <summary>
        /// ��ȡWORDȫ�ֶ���
        /// </summary>
        public Microsoft.Office.Interop.Word.ApplicationClass WordApplication
        {
            get { return WordApplic; }
        }

        /// <summary>
        /// ��һ��WORDģ��
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public Microsoft.Office.Interop.Word.Document Doc(string strPath)
        {
            object FileName = strPath;
            object readOnly = false;
            object isVisible = false;
            try
            {
                
                return WordApplic.Documents.Open(ref FileName, ref missing, ref readOnly,
                    ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref isVisible, ref missing, ref missing, ref missing, ref missing);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// ����һ��ģ��WORD����
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public Microsoft.Office.Interop.Word.Document LoadDot(string strPath)
        {
            object FileName = strPath;
            object blnNewTemplet = false;
            object isVisible = false;
            try
            {
                int count = WordApplic.Documents.Count;

            }
            catch 
            {
                WordApplic = new Microsoft.Office.Interop.Word.ApplicationClass();
            }
            return WordApplic.Documents.Add(ref FileName, ref blnNewTemplet, ref missing, ref isVisible);
        }

        /// <summary>
        /// ճ���ĵ�
        /// </summary>
        /// <param name="WordTemplet">��Ҫճ�����ĵ�</param>
        /// <param name="CopyDoc">�������ĵ�</param>
        public void PasteWord(Microsoft.Office.Interop.Word.Document WordTemplet, Microsoft.Office.Interop.Word.Document CopyDoc)
        {

            CopyDoc.Select();
            WordApplic.Selection.Copy();
            Microsoft.Office.Interop.Word.Paragraph Word_Para;
                
           if(WordTemplet.Paragraphs.Count==0)      //�������������0��������һ������
           {
            WordTemplet.Paragraphs.Add(ref missing);
           }
                
            Word_Para = WordTemplet.Paragraphs.Last;        //��λ�����һ������
            if (WordTemplet.Paragraphs.Count > 1)           //�������������1������Ҫ���ӻ�ҳ��
            {

                object pagebreak = (int)Microsoft.Office.Interop.Word.WdBreakType.wdSectionBreakNextPage;
                Word_Para.Range.InsertBreak(ref pagebreak);     //�����һ����������һ����ҳ��
            }

            Word_Para.Range.Paste();        //ճ�����Ƶ�ģ��

            Word_Para = null;
            System.Windows.Forms.Clipboard.Clear();     //��ռ��а�

            CloseDoc(CopyDoc, false);       //�رձ����Ƶ��ĵ�
        }

        /// <summary>
        /// ����ճ�����
        /// </summary>
        /// <param name="WordRange"></param>
        /// <param name="CopyDoc"></param>
        /// <param name="TableIndex"></param>
        public static void CopyPasteTable(ref Microsoft.Office.Interop.Word.Range WordRange, ref Microsoft.Office.Interop.Word.Document CopyDoc, int TableIndex)
        {
            CopyDoc.Tables[TableIndex].Range.Copy();
            WordRange.Paste();
            System.Windows.Forms.Clipboard.Clear();
            clsWordControl.CloseDoc(CopyDoc, false);
        }
        /// <summary>
        /// ����WORD
        /// </summary>
        public void Quit()
        {
            try
            {
                if (IsMyCreate)
                {
                    WordApplic.Application.Quit(ref missing, ref missing, ref missing);
                }
            }
            catch
            { }
        }
        /// <summary>
        /// �ر�һ���ĵ�
        /// </summary>
        /// <param name="Doc">�ĵ�����</param>
        /// <param name="IsSave">�Ƿ񱣴�ر�</param>
        public static void CloseDoc(Microsoft.Office.Interop.Word.Document Doc, bool IsSave)
        {
            Object Saveed = IsSave;
            Doc.Close(ref Saveed, ref missing, ref missing);
            
        }

        /// <summary>
        /// �ƶ����ĵ�ĩβ
        /// </summary>
        /// <param name="range"></param>
        public static void RangeMoveEnd(ref Microsoft.Office.Interop.Word.Range range)
        {
            object unit = Microsoft.Office.Interop.Word.WdUnits.wdStory;
            object extend = 1;
            range.Move(ref unit, ref extend);
        }

        public static void PageBreak(ref Microsoft.Office.Interop.Word.Range range)
        {
            RangeMoveEnd(ref range);

            object pagebreak = Microsoft.Office.Interop.Word.WdBreakType.wdPageBreak;
            range.InsertBreak(ref pagebreak);
        }

        /// <summary>
        /// �ĵ���ӡ
        /// </summary>
        /// <param name="Doc"></param>
        public void PrintDoc(Microsoft.Office.Interop.Word.Document Doc)
        {
            Doc.PrintOut(ref missing, ref missing, ref missing, ref missing
                        , ref missing, ref missing, ref missing, ref missing
                        , ref missing, ref missing, ref missing, ref missing
                        , ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        }

        #region  ����ͼƬlees


        #endregion


        /// <summary>
        /// docתpdf
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        /// <param name="exportFormat"></param>
        /// <returns></returns>
        public  bool Convert(string sourcePath, string targetPath, Microsoft.Office.Interop.Word.WdExportFormat exportFormat)
        {
            bool result;
            object paramMissing = Type.Missing;

            Microsoft.Office.Interop.Word.ApplicationClass wordApplication = new Microsoft.Office.Interop.Word.ApplicationClass();
            Microsoft.Office.Interop.Word.Document wordDocument = null;
            try
            {
                object paramSourceDocPath = sourcePath;
                string paramExportFilePath = targetPath;

                Microsoft.Office.Interop.Word.WdExportFormat paramExportFormat = exportFormat;
                bool paramOpenAfterExport = false;
                Microsoft.Office.Interop.Word.WdExportOptimizeFor paramExportOptimizeFor =
                        Microsoft.Office.Interop.Word.WdExportOptimizeFor.wdExportOptimizeForPrint;
                Microsoft.Office.Interop.Word.WdExportRange paramExportRange = Microsoft.Office.Interop.Word.WdExportRange.wdExportAllDocument;
                int paramStartPage = 0;
                int paramEndPage = 0;
                Microsoft.Office.Interop.Word.WdExportItem paramExportItem = Microsoft.Office.Interop.Word.WdExportItem.wdExportDocumentContent;
                bool paramIncludeDocProps = true;
                bool paramKeepIRM = true;
                Microsoft.Office.Interop.Word.WdExportCreateBookmarks paramCreateBookmarks =
                        Microsoft.Office.Interop.Word.WdExportCreateBookmarks.wdExportCreateWordBookmarks;
                bool paramDocStructureTags = true;
                bool paramBitmapMissingFonts = true;
                bool paramUseISO19005_1 = false;

                wordDocument = wordApplication.Documents.Open(
                        ref paramSourceDocPath, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing, ref paramMissing, ref paramMissing,
                        ref paramMissing);

                //ClosePwd(WordDocArr[0], "myclou");
                //���Ӳ���ͼƬ�Ĺ���
                //object bookmark = "sherolee";

                //wordDocument.Bookmarks.get_Item(ref bookmark).Select();

                //if (wordDocument.Bookmarks.Exists(bookmark.ToString()) == true)
                //{
                //    try
                //    {

                //        wordApplication.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;

                //        object Nothing = System.Reflection.Missing.Value;
                //        //����ò���ͼƬ�Ƿ�Ϊ�ⲿ����
                //        object linkToFile = true;
                //        //�������ͼƬ�Ƿ���word�ĵ�һ�𱣴�
                //        object saveWithDocument = true;
                //        InlineShape inlineShape = wordApplication.Selection.InlineShapes.AddPicture(@"C:\Users\screw\Desktop\ReportSave\sign.jpg", ref linkToFile, ref saveWithDocument, ref Nothing);

                //        inlineShape.Width = 20;

                //        inlineShape.Height = 16;
                //    }
                //    catch (Exception e)
                //    {

                //    }


                //}

                //InsertEditPwd(ref wordDocument, "myclou");

                //
                if (wordDocument != null)
                    wordDocument.ExportAsFixedFormat(paramExportFilePath,
                            paramExportFormat, paramOpenAfterExport,
                            paramExportOptimizeFor, paramExportRange, paramStartPage,
                            paramEndPage, paramExportItem, paramIncludeDocProps,
                            paramKeepIRM, paramCreateBookmarks, paramDocStructureTags,
                            paramBitmapMissingFonts, paramUseISO19005_1,
                            ref paramMissing);
                result = true;
            }
            finally
            {
                if (wordDocument != null)
                {
                   wordDocument.Close(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordDocument = null;
                }
                if (wordApplication != null)
                {
                    wordApplication.Quit(ref paramMissing, ref paramMissing, ref paramMissing);
                    wordApplication = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return result;
        }

        /// <summary>
        /// �ĵ��浵
        /// </summary>
        /// <param name="?"></param>
        /// <param name="SavePath"></param>
        public void SaveDoc(Microsoft.Office.Interop.Word.Document Doc, string SavePath)
        {
            object path = SavePath;

            int intoDel = 0;
            //20161027 lees
            if (File.Exists((string)SavePath) && intoDel==0)
            {
                File.Delete((string)SavePath);
                intoDel = intoDel + 1;
            }

            Doc.SaveAs(ref path, ref missing, ref missing, ref missing, ref missing
                        , ref missing, ref missing, ref missing, ref missing, ref missing
                        , ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            clsWordControl.CloseDoc(Doc, false);
            
            string path_start = SavePath.Substring(0, SavePath.LastIndexOf("\\"))+DateTime.Now.ToShortDateString();

            string path_end = (SavePath.Substring(SavePath.LastIndexOf("\\"), SavePath.Length  - SavePath.LastIndexOf("\\")));

            string RealPath = path_end.Substring(0, path_end.LastIndexOf(".")) + ".PDF";
            Convert(SavePath, SavePath.Substring(0, SavePath.LastIndexOf(".")) + ".PDF", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
            
        } /// <summary>
        /// �ĵ��浵
        /// </summary>
        /// <param name="?"></param>
        /// <param name="SavePath"></param>
        public void SaveDocTmp(Microsoft.Office.Interop.Word.Document Doc, string SavePath)
        {
            object path = SavePath;

            int intoDel = 0;
            //20161027 lees
            if (File.Exists((string)SavePath) && intoDel == 0)
            {
                File.Delete((string)SavePath);
                intoDel = intoDel + 1;
            }

            Doc.SaveAs(ref path, ref missing, ref missing, ref missing, ref missing
                        , ref missing, ref missing, ref missing, ref missing, ref missing
                        , ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
            

        }

        /// <summary>
        /// ���ĵ������б��м���һ���ĵ�
        /// </summary>
        /// <param name="Doc"></param>
        public void Add(Microsoft.Office.Interop.Word.Document Doc)
        {
            if (WordDocArr == null)
            {
                WordDocArr = new List<Microsoft.Office.Interop.Word.Document>();
            }
            WordDocArr.Add(Doc);
        }

        /// <summary>
        /// ��ȡ�ĵ������б�����
        /// </summary>
        public int Count
        {
            get
            {
                try
                {
                    return WordDocArr.Count;
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// ����ĵ������б�
        /// </summary>
        public void ClearDocCol()
        {
            try
            {
                WordDocArr.Clear();
            }
            catch
            { }
        }


        /// <summary>
        /// ����ĵ�
        /// </summary>
        /// <returns></returns>
        public Microsoft.Office.Interop.Word.Document MergeFile()
        {

            if (WordDocArr.Count == 0) return null;

            //ClosePwd(WordDocArr[0], "myclou");          //����

            for (int i = 1; i < WordDocArr.Count; i++)
            {
                this.PasteWord(WordDocArr[0], WordDocArr[i]);           //���ĵ��ϲ�
            }
            Microsoft.Office.Interop.Word.Document Doc = WordDocArr[0];

            //InsertEditPwd(ref Doc, "myclou");
            return WordDocArr[0];
        }

    }
}

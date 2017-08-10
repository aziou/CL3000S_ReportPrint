using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop;
using System.IO;
namespace CLReport_Standard
{
    /// <summary>
    /// word文档操作控制类
    /// </summary>
    internal class clsWordControl
    {
        private Microsoft.Office.Interop.Word.ApplicationClass WordApplic;


        public static object missing = System.Reflection.Missing.Value;

        private bool IsMyCreate = false;


        private List<Microsoft.Office.Interop.Word.Document> WordDocArr;


        /// <summary>
        /// 构造函数，创建一个WORD全局对象
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
        /// 文档加密
        /// </summary>
        /// <param name="Doc">文档对象</param>
        /// <param name="DocPassword">密码</param>
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
        /// 获取WORD全局对象
        /// </summary>
        public Microsoft.Office.Interop.Word.ApplicationClass WordApplication
        {
            get { return WordApplic; }
        }

        /// <summary>
        /// 打开一个WORD模板
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
        /// 加载一个模板WORD对象
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
        /// 粘贴文档
        /// </summary>
        /// <param name="WordTemplet">需要粘贴的文档</param>
        /// <param name="CopyDoc">拷贝的文档</param>
        public void PasteWord(Microsoft.Office.Interop.Word.Document WordTemplet, Microsoft.Office.Interop.Word.Document CopyDoc)
        {

            CopyDoc.Select();
            WordApplic.Selection.Copy();
            Microsoft.Office.Interop.Word.Paragraph Word_Para;
                
           if(WordTemplet.Paragraphs.Count==0)      //如果段落数等于0，则新增一个段落
           {
            WordTemplet.Paragraphs.Add(ref missing);
           }
                
            Word_Para = WordTemplet.Paragraphs.Last;        //定位到最后一个段落
            if (WordTemplet.Paragraphs.Count > 1)           //如果段落数大于1，就需要增加换页符
            {

                object pagebreak = (int)Microsoft.Office.Interop.Word.WdBreakType.wdSectionBreakNextPage;
                Word_Para.Range.InsertBreak(ref pagebreak);     //在最后一个段落后插入一个换页符
            }

            Word_Para.Range.Paste();        //粘贴复制的模板

            Word_Para = null;
            System.Windows.Forms.Clipboard.Clear();     //清空剪切板

            CloseDoc(CopyDoc, false);       //关闭被复制的文档
        }

        /// <summary>
        /// 拷贝粘贴表格
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
        /// 销毁WORD
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
        /// 关闭一个文档
        /// </summary>
        /// <param name="Doc">文档对象</param>
        /// <param name="IsSave">是否保存关闭</param>
        public static void CloseDoc(Microsoft.Office.Interop.Word.Document Doc, bool IsSave)
        {
            Object Saveed = IsSave;
            Doc.Close(ref Saveed, ref missing, ref missing);
            
        }

        /// <summary>
        /// 移动到文档末尾
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
        /// 文档打印
        /// </summary>
        /// <param name="Doc"></param>
        public void PrintDoc(Microsoft.Office.Interop.Word.Document Doc)
        {
            Doc.PrintOut(ref missing, ref missing, ref missing, ref missing
                        , ref missing, ref missing, ref missing, ref missing
                        , ref missing, ref missing, ref missing, ref missing
                        , ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        }

        #region  插入图片lees


        #endregion


        /// <summary>
        /// doc转pdf
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
                //增加插入图片的功能
                //object bookmark = "sherolee";

                //wordDocument.Bookmarks.get_Item(ref bookmark).Select();

                //if (wordDocument.Bookmarks.Exists(bookmark.ToString()) == true)
                //{
                //    try
                //    {

                //        wordApplication.Selection.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;

                //        object Nothing = System.Reflection.Missing.Value;
                //        //定义该插入图片是否为外部链接
                //        object linkToFile = true;
                //        //定义插入图片是否随word文档一起保存
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
        /// 文档存档
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
        /// 文档存档
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
        /// 在文档对象列表中加入一个文档
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
        /// 获取文档对象列表数量
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
        /// 清除文档对象列表
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
        /// 组合文档
        /// </summary>
        /// <returns></returns>
        public Microsoft.Office.Interop.Word.Document MergeFile()
        {

            if (WordDocArr.Count == 0) return null;

            //ClosePwd(WordDocArr[0], "myclou");          //解密

            for (int i = 1; i < WordDocArr.Count; i++)
            {
                this.PasteWord(WordDocArr[0], WordDocArr[i]);           //将文档合并
            }
            Microsoft.Office.Interop.Word.Document Doc = WordDocArr[0];

            //InsertEditPwd(ref Doc, "myclou");
            return WordDocArr[0];
        }

    }
}

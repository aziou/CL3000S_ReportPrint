using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Data.OleDb;
using System.Data;
using System.Collections.ObjectModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
namespace CLDC_DataManager
{
    public class clsExcelControl
    {
        Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp;

        string _Path = "";

        public const string str_DetailFileName = @"\电能表检定模板.xls";
        public const string str_FinishFileName = @"\电能表检定模板"+""+"(完成).xls";
        public delegate void AccomplishTask();//声明一个在完成任务时通知主线程的委托
        public AccomplishTask TaskCallBack;
        public clsExcelControl()
        {
            this.CreateApp();
        }
        ~clsExcelControl()
        {
            try
            {
                this.ExcelApp.Quit();
                this.ExcelApp = null;
            }
            catch
            { }
        }

        public clsExcelControl(string path)
        {
            _Path = path;
            this.CreateApp();
        }

        private void CreateApp()
        {
            try
            {

                ExcelApp = (Microsoft.Office.Interop.Excel.ApplicationClass)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");

            }

            catch

            { }

            if (ExcelApp == null)
            {

                ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            }
        }

        /// <summary>
        /// 创建Excel文件导出详细数据
        /// </summary>
        /// <returns></returns>
        public string CreateExcelForDetailed(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items)
        {
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\Wc"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Wc");
            }
            string Path = System.Windows.Forms.Application.StartupPath + @"\Wc\" + DateTime.Now.ToString("yyyyMMddHHmmss");
            if (CreateExcelForDetailed(Items, Path))
            {
                return Path;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 创建Excel文件导出详细数据
        /// </summary>
        /// <param name="ExcelSavePath"></param>
        /// <returns></returns>
        public bool CreateExcelForDetailed(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items, string ExcelSavePath)
        {

            Microsoft.Office.Interop.Excel.Worksheet Sheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Workbooks.Add(true).ActiveSheet;

            Sheet.Name = "误差数据导出页";

            Sheet.Cells[1, 1] = "资产编号";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["A:A",System.Type.Missing]).ColumnWidth = 25;
            Sheet.Cells[1, 2] = "电表型号";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["B:B", System.Type.Missing]).ColumnWidth = 35;
            Sheet.Cells[1, 3] = "生产厂家";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["C:C", System.Type.Missing]).ColumnWidth = 25;
            Sheet.Cells[1, 4] = "常数";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["D:D", System.Type.Missing]).ColumnWidth = 10;
            Sheet.Cells[1, 5] = "等级";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["E:E", System.Type.Missing]).ColumnWidth = 10;
            Sheet.Cells[1, 6] = "送检单位";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["F:F", System.Type.Missing]).ColumnWidth = 30;
            Sheet.Cells[1, 7] = "起动";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).ColumnWidth = 8;
            Sheet.Cells[1, 8] = "潜动";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).ColumnWidth = 8;
            Sheet.Cells[1, 9] = "走字试验";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).ColumnWidth = 10;
            Sheet.Cells[1, 10] = "日计时误差";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, 11] = "日计时结论";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, 12] = "总结论";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).ColumnWidth = 8;
            Sheet.Cells[1, 13] = "功率方向";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["H:H", System.Type.Missing]).ColumnWidth = 8;
            Sheet.Cells[1, 14] = "元件";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["I:I", System.Type.Missing]).ColumnWidth = 4;
            Sheet.Cells[1, 15] = "功率因素";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["J:J", System.Type.Missing]).ColumnWidth = 8;
            Sheet.Cells[1, 16] = "负载电流";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["K:K", System.Type.Missing]).ColumnWidth = 8;
            Sheet.Cells[1, 17] = "误差化整值";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["L:L", System.Type.Missing]).ColumnWidth =10;
            Sheet.Cells[1, 18] = "误差平均值";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["M:M", System.Type.Missing]).ColumnWidth = 10;
            Sheet.Cells[1, 19] = "误差值(1,2,3,...,n)";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["N:N", System.Type.Missing]).ColumnWidth = 10;


            int RowIndex = 2;

            for (int i = 0; i < Items.Count; i++)
            {

                RowIndex = this.CreateOneMeterExcelRowForDetailed(Items[i], Sheet, RowIndex);
            
            }

            Sheet.PageSetup.LeftMargin = ExcelApp.InchesToPoints(0.12D);
            Sheet.PageSetup.RightMargin = ExcelApp.InchesToPoints(0.12D);
            ExcelApp.ActiveWorkbook.Close(true, ExcelSavePath, null);
            return true;
        }
        /// <summary>
        /// 创建一只表的Excel表格
        /// </summary>
        /// <param name="Item">电能表数据模型</param>
        /// <param name="Sheet">表单对象</param>
        /// <param name="RowIndex">开始行号</param>
        /// <returns>结束行号</returns>
        private int CreateOneMeterExcelRowForDetailed(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item, Microsoft.Office.Interop.Excel.Worksheet Sheet, int RowIndex)
        {
            if (Item.MeterErrors.Count == 0)
            {
                return RowIndex;
            }

            List<string> Keys = new List<string>();
            foreach (string _Key in Item.MeterErrors.Keys)
            {
                if (_Key.Substring(0, 1) == ((int)CLDC_Comm.Enum.Cus_WcType.标准偏差).ToString()) continue;            //只获取基本误差ID

                Keys.Add(_Key);
            }

            Keys.Sort();

            string Glfx = "";
            string Yuan = "";
            string Glys = "";
            string Fzd = "";

            int intGlfx = 0;
            int intYuan = 0;
            int intGlys = 0;
            int LastColIndex = 0;
            int intTxm = RowIndex;

            Microsoft.Office.Interop.Excel.Range myrange = Sheet.get_Range("A1", "A1000");
            myrange.NumberFormatLocal = "@";
            Sheet.Cells[RowIndex, 1] = string.IsNullOrEmpty(Item.Mb_ChrJlbh.Trim()) ? Item.Mb_ChrTxm.Trim() : Item.Mb_ChrJlbh.Trim();

            Sheet.Cells[RowIndex, 2] = Item.Mb_chrBlx .Trim ();
            Sheet.Cells[RowIndex, 3] = Item.Mb_chrzzcj.Trim();
            Sheet.Cells[RowIndex, 4] = Item.Mb_chrBcs.Trim();
            Sheet.Cells[RowIndex, 5] = Item.Mb_chrBdj.Trim();
            Sheet.Cells[RowIndex, 6] = Item.Mb_chrSjdw.Trim();

            string ItemKey;
            #region lsx 启动 潜动 走字结果
            //启动
            ItemKey = string.Format("{0}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验);
            Sheet.Cells[RowIndex, 7] = "合格";
            foreach (string tempKey in Item.MeterQdQids.Keys )
            {
                if (tempKey.Contains(ItemKey))
                {
                    if (Item.MeterQdQids[tempKey].Mqd_chrJL.Contains ("不合格"))
                    {
                        Sheet.Cells[RowIndex, 7] = Item.MeterQdQids[tempKey].Mqd_chrJL;
                        break;
                    }
                }
            }
            //潜动
            ItemKey = string.Format("{0}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验 );
            Sheet.Cells[RowIndex, 8] = "合格";
            foreach (string tempKey in Item.MeterQdQids.Keys)
            {
                if (tempKey.Contains(ItemKey))
                {
                    if (Item.MeterQdQids[tempKey].Mqd_chrJL.Contains ("不合格"))
                    {
                        Sheet.Cells[RowIndex, 8] = Item.MeterQdQids[tempKey].Mqd_chrJL;
                        break;
                    }
                }
            }
            // 走字
            ItemKey = string.Format("{0}", (int)CLDC_Comm .Enum . Cus_MeterResultPrjID.走字试验);
            if (Item.MeterResults.ContainsKey(ItemKey))
            {
                Sheet.Cells[RowIndex, 9] = Item.MeterResults[ItemKey].Mr_chrRstValue;
            }
            #endregion 启动 潜动 走字结果
            ItemKey = ((int)CLDC_Comm.Enum.Cus_DgnItem.日计时误差).ToString().PadLeft(3, '0');
            //平均值
            string key = ItemKey + "01";
            if (Item.MeterDgns.ContainsKey(key))
            {
                string[] strValue = Item.MeterDgns[key].Md_chrValue.Split('|');
                if (strValue.Length > 1)
                {
                    Sheet.Cells[RowIndex, 10] = strValue[1];
                }
            }

            ItemKey = ((int)(CLDC_Comm.Enum.Cus_DgnItem.日计时误差)).ToString("D3");
            if (Item.MeterDgns.ContainsKey(ItemKey))
            {
                Sheet.Cells[RowIndex, 11] = Item.MeterDgns[ItemKey].Md_chrValue;
            }

            Sheet.Cells[RowIndex, 12] = Item.Mb_chrResult;

            for (int i = 0; i < Keys.Count; i++)
            {
                if (Fzd != Keys[i].Substring(5, 2))
                {
                    Fzd = Keys[i].Substring(5, 2);
                    
                    ((Microsoft.Office.Interop.Excel.Range)Sheet.Rows[RowIndex.ToString(), System.Type.Missing]).NumberFormatLocal = "@";
                    ((Microsoft.Office.Interop.Excel.Range)Sheet.Rows[RowIndex.ToString(), System.Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    ((Microsoft.Office.Interop.Excel.Range)Sheet.Rows[RowIndex.ToString(), System.Type.Missing]).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    Sheet.Cells[RowIndex, 16] = Item.MeterErrors[Keys[i]].Me_dblxIb;
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError Error = Item.MeterErrors[Keys[i]];
                    string[] Arr_Wc = Error.Me_chrWcMore.Split('|');
                    if (Arr_Wc.Length > 2)
                    {
                        if (LastColIndex < Arr_Wc.Length + 16)
                        {
                            LastColIndex = Arr_Wc.Length + 16;
                        }
                        Sheet.Cells[RowIndex, 17] = Arr_Wc[Arr_Wc.Length - 1];
                        Sheet.Cells[RowIndex, 18] = Arr_Wc[Arr_Wc.Length - 2];
                        for (int j = 0; j < Arr_Wc.Length - 2; j++)
                        {
                            Sheet.Cells[RowIndex, 19 + j] = Arr_Wc[j];
                        }
                    }
                }

                if (Glys != Keys[i].Substring(3, 2))
                {
                    if (intGlys != 0)
                    {
                        Microsoft.Office.Interop.Excel.Range range = Sheet.get_Range(Sheet.Cells[intGlys, 15], Sheet.Cells[RowIndex-1, 15]);
                        range.Merge(System.Reflection.Missing.Value);
                    }
                    intGlys = RowIndex;
                    Glys = Keys[i].Substring(3, 2);
                    Sheet.Cells[RowIndex, 15] = Item.MeterErrors[Keys[i]].Me_chrGlys;
                }

                if (Yuan != Keys[i].Substring(2, 1))
                {
                    if (intYuan != 0)
                    {
                        Microsoft.Office.Interop.Excel.Range range = Sheet.get_Range(Sheet.Cells[intYuan, 14], Sheet.Cells[RowIndex-1, 14]);
                        range.Merge(System.Reflection.Missing.Value);
                    }
                    intYuan =RowIndex;
                    Yuan = Keys[i].Substring(2, 1);
                    Sheet.Cells[RowIndex, 14] = ((CLDC_Comm.Enum.Cus_PowerYuanJian)int.Parse(Yuan)).ToString();
                }

                if (Glfx != Keys[i].Substring(1, 1))
                {
                    if (intGlfx != 0)
                    {
                        Microsoft.Office.Interop.Excel.Range range = Sheet.get_Range(Sheet.Cells[intGlfx, 13], Sheet.Cells[RowIndex-1, 13]);
                        range.Merge(System.Reflection.Missing.Value);
                    }
                    intGlfx =RowIndex;
                    Glfx = Keys[i].Substring(1, 1);
                    Sheet.Cells[RowIndex, 13] = ((CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(Glfx)).ToString();
                }
                RowIndex++;
            }

            Microsoft.Office.Interop.Excel.Range Lastrange;

            for (int j = 1; j <= 12; j++)
            {
                Lastrange = Sheet.get_Range(Sheet.Cells[intTxm, j], Sheet.Cells[RowIndex - 1, j]);
                Lastrange.Merge(System.Reflection.Missing.Value);
            }

            Lastrange = Sheet.get_Range(Sheet.Cells[intGlfx, 13], Sheet.Cells[RowIndex - 1, 13]);
            Lastrange.Merge(System.Reflection.Missing.Value);

            Lastrange = Sheet.get_Range(Sheet.Cells[intYuan, 14], Sheet.Cells[RowIndex - 1, 14]);
            Lastrange.Merge(System.Reflection.Missing.Value);

            Lastrange = Sheet.get_Range(Sheet.Cells[intGlys, 15], Sheet.Cells[RowIndex - 1, 15]);
            Lastrange.Merge(System.Reflection.Missing.Value);


            Microsoft.Office.Interop.Excel.Range RangeAll = Sheet.get_Range(Sheet.Cells[intTxm, 1], Sheet.Cells[RowIndex - 1, LastColIndex]);
            RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = 1;
            RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = 1;
            RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = 1;
            RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle=1;
            return RowIndex;
        }

        /// <summary>
        /// 创建Excel文件导出详细数据
        /// </summary>
        /// <returns></returns>
        public string CreateExcelForThanDataThree(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items)
        {
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\Wc"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Wc");
            }
            string Path = System.Windows.Forms.Application.StartupPath + @"\Wc\" + DateTime.Now.ToString("yyyyMMddHHmmss");
            if (CreateExcelForThanDataThree(Items, Path))
            {
                return Path;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 创建Excel文件导出详细数据
        /// </summary>
        /// <param name="ExcelSavePath"></param>
        /// <returns></returns>
        public bool CreateExcelForThanDataThree(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items, string ExcelSavePath)
        {

            Microsoft.Office.Interop.Excel.Worksheet Sheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Workbooks.Add(true).ActiveSheet;

            Sheet.Name = "误差数据导出页";

            int intRowNo = 1;

            Sheet.Cells[intRowNo++, 1] = "资产编号";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["A:A", System.Type.Missing]).ColumnWidth = 10;
            Sheet.Cells[intRowNo++, 1] = "电表型号";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["B:B", System.Type.Missing]).ColumnWidth = 10;
            Sheet.Cells[intRowNo++, 1] = "生产厂家";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["C:C", System.Type.Missing]).ColumnWidth = 10;
            Sheet.Cells[intRowNo++, 1] = "常数";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["D:D", System.Type.Missing]).ColumnWidth = 10;
            Sheet.Cells[intRowNo++, 1] = "等级";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["E:E", System.Type.Missing]).RowHeight = 10;
            Sheet.Cells[intRowNo++, 1] = "送检单位";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["F:F", System.Type.Missing]).RowHeight = 10;
            Sheet.Cells[intRowNo++, 1] = "起动";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).RowHeight = 8;
            Sheet.Cells[intRowNo++, 1] = "潜动";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).RowHeight = 8;
            Sheet.Cells[intRowNo++, 1] = "走字试验";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).RowHeight = 10;
            Sheet.Cells[intRowNo++, 1] = "日计时误差";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).RowHeight = 15;
            Sheet.Cells[intRowNo++, 1] = "日计时结论";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).RowHeight = 15;
            Sheet.Cells[intRowNo++, 1] = "总结论";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).RowHeight = 8;
            //Sheet.Cells[intRowNo++, 1] = "功率方向";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["H:H", System.Type.Missing]).ColumnWidth = 8;
            //Sheet.Cells[intRowNo++, 1] = "元件";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["I:I", System.Type.Missing]).ColumnWidth = 4;
            //Sheet.Cells[intRowNo++, 1] = "功率因素";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["J:J", System.Type.Missing]).ColumnWidth = 8;
            //Sheet.Cells[intRowNo++, 1] = "负载电流";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["K:K", System.Type.Missing]).ColumnWidth = 8;
            //Sheet.Cells[intRowNo++, 1] = "误差化整值";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["L:L", System.Type.Missing]).ColumnWidth = 10;
            //Sheet.Cells[intRowNo++, 1] = "误差平均值";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["M:M", System.Type.Missing]).ColumnWidth = 10;
            //Sheet.Cells[intRowNo++, 1] = "误差值(1,2,3,...,n)";
            //((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["N:N", System.Type.Missing]).ColumnWidth = 10;


            int ColIndex = 5;

            for (int i = 0; i < Items.Count; i++)
            {

                ColIndex = this.CreateOneMeterExcelRowForThanDataThree(Items[i], Sheet, ColIndex);

            }

            Sheet.PageSetup.LeftMargin = ExcelApp.InchesToPoints(0.12D);
            Sheet.PageSetup.RightMargin = ExcelApp.InchesToPoints(0.12D);
            ExcelApp.ActiveWorkbook.Close(true, ExcelSavePath, null);
            return true;
        }
        /// <summary>
        /// 创建一只表的Excel表格
        /// </summary>
        /// <param name="Item">电能表数据模型</param>
        /// <param name="Sheet">表单对象</param>
        /// <param name="RowIndex">开始行号</param>
        /// <returns>结束行号</returns>
        private int CreateOneMeterExcelRowForThanDataThree(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item, Microsoft.Office.Interop.Excel.Worksheet Sheet, int ColIndex)
        {
            if (Item.MeterErrors.Count == 0)
            {
                return ColIndex;
            }

            List<string> Keys = new List<string>();
            foreach (string _Key in Item.MeterErrors.Keys)
            {
                if (_Key.Substring(0, 1) == ((int)CLDC_Comm.Enum.Cus_WcType.标准偏差).ToString()) continue;            //只获取基本误差ID

                Keys.Add(_Key);
            }

            Keys.Sort();

            string Glfx = "";
            string Yuan = "";
            string Glys = "";
            string Fzd = "";

            int intGlfx = 0;
            int intYuan = 0;
            int intGlys = 0;
            int LastColIndex = 0;
            int intTxm = ColIndex;

            Microsoft.Office.Interop.Excel.Range myrange = Sheet.get_Range("A1", "A1000");
            myrange.NumberFormatLocal = "@";
            Sheet.Cells[1, ColIndex] = string.IsNullOrEmpty(Item.Mb_ChrJlbh.Trim()) ? Item.Mb_ChrTxm.Trim() : Item.Mb_ChrJlbh.Trim();

            Sheet.Cells[2, ColIndex] = Item.Mb_chrBlx.Trim();
            Sheet.Cells[3, ColIndex] = Item.Mb_chrzzcj.Trim();
            Sheet.Cells[4, ColIndex] = Item.Mb_chrBcs.Trim();
            Sheet.Cells[5, ColIndex] = Item.Mb_chrBdj.Trim();
            Sheet.Cells[6, ColIndex] = Item.Mb_chrSjdw.Trim();

            string ItemKey;
            #region lsx 启动 潜动 走字结果
            //启动
            ItemKey = string.Format("{0}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验);
            Sheet.Cells[7, ColIndex] = "合格";
            foreach (string tempKey in Item.MeterQdQids.Keys)
            {
                if (tempKey.Contains(ItemKey))
                {
                    if (Item.MeterQdQids[tempKey].Mqd_chrJL.Contains("不合格"))
                    {
                        Sheet.Cells[7, ColIndex] = Item.MeterQdQids[tempKey].Mqd_chrJL;
                        break;
                    }
                }
            }
            //潜动
            ItemKey = string.Format("{0}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验);
            Sheet.Cells[8, ColIndex] = "合格";
            foreach (string tempKey in Item.MeterQdQids.Keys)
            {
                if (tempKey.Contains(ItemKey))
                {
                    if (Item.MeterQdQids[tempKey].Mqd_chrJL.Contains("不合格"))
                    {
                        Sheet.Cells[8, ColIndex] = Item.MeterQdQids[tempKey].Mqd_chrJL;
                        break;
                    }
                }
            }
            // 走字
            ItemKey = string.Format("{0}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验);
            if (Item.MeterResults.ContainsKey(ItemKey))
            {
                Sheet.Cells[9, ColIndex] = Item.MeterResults[ItemKey].Mr_chrRstValue;
            }
            #endregion 启动 潜动 走字结果
            ItemKey = ((int)CLDC_Comm.Enum.Cus_DgnItem.日计时误差).ToString().PadLeft(3, '0');
            //平均值
            string key = ItemKey + "01";
            if (Item.MeterDgns.ContainsKey(key))
            {
                string[] strValue = Item.MeterDgns[key].Md_chrValue.Split('|');
                if (strValue.Length > 1)
                {
                    Sheet.Cells[10, ColIndex] = strValue[1];
                }
            }

            ItemKey = ((int)(CLDC_Comm.Enum.Cus_DgnItem.日计时误差)).ToString("D3");
            if (Item.MeterDgns.ContainsKey(ItemKey))
            {
                Sheet.Cells[11, ColIndex] = Item.MeterDgns[ItemKey].Md_chrValue;
            }

            Sheet.Cells[12, ColIndex] = Item.Mb_chrResult;

            for (int i = 0; i < 30; i++)
            {
                ((Microsoft.Office.Interop.Excel.Range)Sheet.Rows[i + 1 , System.Type.Missing]).NumberFormatLocal = "@";
                ((Microsoft.Office.Interop.Excel.Range)Sheet.Rows[i + 1, System.Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                ((Microsoft.Office.Interop.Excel.Range)Sheet.Rows[i + 1, System.Type.Missing]).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            }

            for (int i = 0; i < Keys.Count; i++)
            {
                if (Fzd != Keys[i].Substring(5, 2))
                {
                    Fzd = Keys[i].Substring(5, 2);


                    Sheet.Cells[13 + i, 4] = Item.MeterErrors[Keys[i]].Me_dblxIb;
                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError Error = Item.MeterErrors[Keys[i]];
                    string[] Arr_Wc = Error.Me_chrWcMore.Split('|');
                    if (Arr_Wc.Length > 2)
                    {                        
                        Sheet.Cells[13+i, ColIndex] = Arr_Wc[Arr_Wc.Length - 2];
                    }
                }

                if (Glys != Keys[i].Substring(3, 2))
                {
                    if (intGlys != 0)
                    {
                        Microsoft.Office.Interop.Excel.Range range = Sheet.get_Range(Sheet.Cells[intGlys, 3], Sheet.Cells[13 + i - 1, 3]);
                        range.Merge(System.Reflection.Missing.Value);
                    }
                    intGlys = 13 + i;
                    Glys = Keys[i].Substring(3, 2);
                    Sheet.Cells[13 + i, 3] = Item.MeterErrors[Keys[i]].Me_chrGlys;
                }

                if (Yuan != Keys[i].Substring(2, 1))
                {
                    if (intYuan != 0)
                    {
                        Microsoft.Office.Interop.Excel.Range range = Sheet.get_Range(Sheet.Cells[intYuan, 2], Sheet.Cells[13 + i - 1, 2]);
                        range.Merge(System.Reflection.Missing.Value);
                    }
                    intYuan = 13 + i;
                    Yuan = Keys[i].Substring(2, 1);
                    Sheet.Cells[13 + i, 2] = ((CLDC_Comm.Enum.Cus_PowerYuanJian)int.Parse(Yuan)).ToString();
                }

                if (Glfx != Keys[i].Substring(1, 1))
                {
                    if (intGlfx != 0)
                    {
                        Microsoft.Office.Interop.Excel.Range range = Sheet.get_Range(Sheet.Cells[intGlfx, 1], Sheet.Cells[13 + i - 1, 1]);
                        range.Merge(System.Reflection.Missing.Value);
                    }
                    intGlfx = 13 + i;
                    Glfx = Keys[i].Substring(1, 1);
                    Sheet.Cells[13 + i, 1] = ((CLDC_Comm.Enum.Cus_PowerFangXiang)int.Parse(Glfx)).ToString();
                }
                //ColIndex++;
            }

            Microsoft.Office.Interop.Excel.Range Lastrange;

            for (int j = 1; j <= 12; j++)
            {
                Lastrange = Sheet.get_Range(Sheet.Cells[j, 1], Sheet.Cells[j, 4]);
                Lastrange.Merge(System.Reflection.Missing.Value);
            }

            Lastrange = Sheet.get_Range(Sheet.Cells[intGlfx,1], Sheet.Cells[13+Keys.Count - 1,1]);
            Lastrange.Merge(System.Reflection.Missing.Value);

            Lastrange = Sheet.get_Range(Sheet.Cells[intYuan,2], Sheet.Cells[13+Keys.Count - 1,2]);
            Lastrange.Merge(System.Reflection.Missing.Value);

            Lastrange = Sheet.get_Range(Sheet.Cells[intGlys,3], Sheet.Cells[13+Keys.Count - 1,3]);
            Lastrange.Merge(System.Reflection.Missing.Value);


            //Microsoft.Office.Interop.Excel.Range RangeAll = Sheet.get_Range(Sheet.Cells[intTxm, 1], Sheet.Cells[LastColIndex, ColIndex - 1]);
            //RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = 1;
            //RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            //RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            //RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = 1;
            //RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = 1;
            //RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = 1;
            ColIndex++;
            return ColIndex;
        }

        /// <summary>
        /// 创建Excel文件导出比对数据
        /// </summary>
        /// <returns></returns>
        public string CreateExcelForThanData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items)
        {
            if (!System.IO.Directory.Exists(System.Windows.Forms.Application.StartupPath + @"\Wc"))
            {
                System.IO.Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + @"\Wc");
            }
            string Path = System.Windows.Forms.Application.StartupPath + @"\Wc\" + DateTime.Now.ToString("yyyyMMddHHmmss");
            if (CreateExcelForThanData(Items, Path))
            {
                return Path;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 创建Excel文件导出比对数据
        /// </summary>
        /// <param name="ExcelSavePath"></param>
        /// <returns></returns>
        public bool CreateExcelForThanData(List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items, string ExcelSavePath)
        {
            int int_ColNo = 1;

            Microsoft.Office.Interop.Excel.Worksheet Sheet = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Workbooks.Add(true).ActiveSheet;

            Sheet.Name = "误差数据导出页";

            Sheet.Cells[1, int_ColNo++] = "资产编号";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["A:A", System.Type.Missing]).ColumnWidth = 25;
            Sheet.Cells[1, int_ColNo++] = "电表型号";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["B:B", System.Type.Missing]).ColumnWidth = 35;
            Sheet.Cells[1, int_ColNo++] = "生产厂家";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["C:C", System.Type.Missing]).ColumnWidth = 25;
            Sheet.Cells[1, int_ColNo++] = "常数";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["D:D", System.Type.Missing]).ColumnWidth = 10;
            Sheet.Cells[1, int_ColNo++] = "等级";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["E:E", System.Type.Missing]).ColumnWidth = 10;
            Sheet.Cells[1, int_ColNo++] = "送检单位";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["F:F", System.Type.Missing]).ColumnWidth = 30;
            Sheet.Cells[1, int_ColNo++] = "起动";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).ColumnWidth = 8;
            Sheet.Cells[1, int_ColNo++] = "潜动";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).ColumnWidth = 8;
            Sheet.Cells[1, int_ColNo++] = "走字试验";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).ColumnWidth = 10;
            Sheet.Cells[1, int_ColNo++] = "日计时误差";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, int_ColNo++] = "日计时结论";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, int_ColNo++] = "总结论";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["G:G", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, int_ColNo++] = "1.0/Imax";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["H:H", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, int_ColNo++] = "0.5L/Imax";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["I:I", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, int_ColNo++] = "1.0/0.5Imax";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["J:J", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, int_ColNo++] = "0.5L/0.5Imax";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["K:K", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, int_ColNo++] = "1.0/Ib";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["L:L", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, int_ColNo++] = "0.5L/Ib";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["M:M", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, int_ColNo++] = "0.5L/0.2Ib";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["N:N", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, int_ColNo++] = "1.0/0.1Ib";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["N:N", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, int_ColNo++] = "0.5L/0.1Ib";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["N:N", System.Type.Missing]).ColumnWidth = 15;
            Sheet.Cells[1, int_ColNo++] = "1.0/0.05Ib";
            ((Microsoft.Office.Interop.Excel.Range)Sheet.Columns["N:N", System.Type.Missing]).ColumnWidth = 15;

            int RowIndex = 2;

            for (int i = 0; i < Items.Count; i++)
            {

                RowIndex = this.CreateOneMeterExcelRowForThanData(Items[i], Sheet, RowIndex);

            }

            Sheet.PageSetup.LeftMargin = ExcelApp.InchesToPoints(0.12D);
            Sheet.PageSetup.RightMargin = ExcelApp.InchesToPoints(0.12D);
            ExcelApp.ActiveWorkbook.Close(true, ExcelSavePath, null);
            return true;
        }
        /// <summary>
        /// 创建一只表的Excel表格比对数据
        /// </summary>
        /// <param name="Item">电能表数据模型</param>
        /// <param name="Sheet">表单对象</param>
        /// <param name="RowIndex">开始行号</param>
        /// <returns>结束行号</returns>
        private int CreateOneMeterExcelRowForThanData(CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item, Microsoft.Office.Interop.Excel.Worksheet Sheet, int RowIndex)
        {
            if (Item.MeterErrors.Count == 0)
            {
                return RowIndex;
            }

            List<string> Keys = new List<string>();
            foreach (string _Key in Item.MeterErrors.Keys)
            {
                if (_Key.Substring(0, 1) == ((int)CLDC_Comm.Enum.Cus_WcType.标准偏差).ToString()) continue;            //只获取基本误差ID

                Keys.Add(_Key);
            }

            Keys.Sort();
            
            string Fzd = "";
            int LastColIndex = 0;
            int intTxm = RowIndex;

            Microsoft.Office.Interop.Excel.Range myrange = Sheet.get_Range("A1", "A1000");
            myrange.NumberFormatLocal = "@";
            Sheet.Cells[RowIndex, 1] = string.IsNullOrEmpty(Item.Mb_ChrJlbh.Trim()) ? Item.Mb_ChrTxm.Trim() : Item.Mb_ChrJlbh.Trim();

            Sheet.Cells[RowIndex, 2] = Item.Mb_chrBlx.Trim();
            Sheet.Cells[RowIndex, 3] = Item.Mb_chrzzcj.Trim();
            Sheet.Cells[RowIndex, 4] = Item.Mb_chrBcs.Trim();
            Sheet.Cells[RowIndex, 5] = Item.Mb_chrBdj.Trim();
            Sheet.Cells[RowIndex, 6] = Item.Mb_chrSjdw.Trim();

            string ItemKey;
            #region lsx 启动 潜动 走字结果
            //启动
            ItemKey = string.Format("{0}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.起动试验);
            Sheet.Cells[RowIndex, 7] = "合格";
            foreach (string tempKey in Item.MeterQdQids.Keys)
            {
                if (tempKey.Contains(ItemKey))
                {
                    if (Item.MeterQdQids[tempKey].Mqd_chrJL.Contains("不合格"))
                    {
                        Sheet.Cells[RowIndex, 7] = Item.MeterQdQids[tempKey].Mqd_chrJL;
                        break;
                    }
                }
            }
            //潜动
            ItemKey = string.Format("{0}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.潜动试验);
            Sheet.Cells[RowIndex, 8] = "合格";
            foreach (string tempKey in Item.MeterQdQids.Keys)
            {
                if (tempKey.Contains(ItemKey))
                {
                    if (Item.MeterQdQids[tempKey].Mqd_chrJL.Contains("不合格"))
                    {
                        Sheet.Cells[RowIndex, 8] = Item.MeterQdQids[tempKey].Mqd_chrJL;
                        break;
                    }
                }
            }
            // 走字
            ItemKey = string.Format("{0}", (int)CLDC_Comm.Enum.Cus_MeterResultPrjID.走字试验);
            if (Item.MeterResults.ContainsKey(ItemKey))
            {
                Sheet.Cells[RowIndex, 9] = Item.MeterResults[ItemKey].Mr_chrRstValue;
            }
            #endregion 启动 潜动 走字结果
            ItemKey = ((int)CLDC_Comm.Enum.Cus_DgnItem.日计时误差).ToString().PadLeft(3, '0');
            //平均值
            string key = ItemKey + "01";
            if (Item.MeterDgns.ContainsKey(key))
            {
                string[] strValue = Item.MeterDgns[key].Md_chrValue.Split('|');
                if (strValue.Length > 1)
                {
                    Sheet.Cells[RowIndex, 10] = strValue[1];
                }
            }

            ItemKey = ((int)(CLDC_Comm.Enum.Cus_DgnItem.日计时误差)).ToString("D3");
            if (Item.MeterDgns.ContainsKey(ItemKey))
            {
                Sheet.Cells[RowIndex, 11] = Item.MeterDgns[ItemKey].Md_chrValue;
            }

            Sheet.Cells[RowIndex, 12] = Item.Mb_chrResult;

            for (int i = 0; i < 10; i++)
            {
                if (Fzd != Keys[i].Substring(5, 2))
                {
                    Fzd = Keys[i].Substring(5, 2);

                    ((Microsoft.Office.Interop.Excel.Range)Sheet.Rows[RowIndex.ToString(), System.Type.Missing]).NumberFormatLocal = "@";
                    ((Microsoft.Office.Interop.Excel.Range)Sheet.Rows[RowIndex.ToString(), System.Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    ((Microsoft.Office.Interop.Excel.Range)Sheet.Rows[RowIndex.ToString(), System.Type.Missing]).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                    CLDC_DataCore.Model.DnbModel.DnbInfo.MeterError Error = Item.MeterErrors[Keys[i]];
                    string[] Arr_Wc = Error.Me_chrWcMore.Split('|');
                    if (Arr_Wc.Length > 2)
                    {                       
                        Sheet.Cells[RowIndex, 13 + i] = Arr_Wc[Arr_Wc.Length - 2];                        
                    }
                }                
            }

            LastColIndex = Keys.Count + 12;           

            Microsoft.Office.Interop.Excel.Range RangeAll = Sheet.get_Range(Sheet.Cells[intTxm, 1], Sheet.Cells[RowIndex - 1, LastColIndex]);
            RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = 1;
            RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].LineStyle = 1;
            RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = 1;
            RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = 1;
            RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].LineStyle = 1;
            RangeAll.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = 1;
            RowIndex++;
            return RowIndex;
        }


        public static void OutputExcel(List<string> Meter_zcbh, List<string> Meter_seal_1, List<string> Meter_seal_2, List<string> Meter_seal_3,string Time)
        {
            string SavePath = clsMain.getIniString("OtherInfo", "SavePath", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\ReportInfo.ini");
            MemoryStream ms = new MemoryStream();
            HSSFWorkbook wk = new HSSFWorkbook();
            ISheet TableEx = wk.CreateSheet("电表铅封信息");
            TableEx.SetColumnWidth(0, 30 * 256);
            TableEx.SetColumnWidth(1, 30 * 256);
            TableEx.SetColumnWidth(2, 30 * 256);
            TableEx.SetColumnWidth(3, 30 * 256);
            IRow tbRow_0 = TableEx.CreateRow(0);
            TableEx.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 3));
            ICell cell = tbRow_0.CreateCell(0);
            cell.SetCellValue("电表铅封信息 检定时间："+Time);
            ICellStyle styleCenter = wk.CreateCellStyle();
            styleCenter.Alignment = HorizontalAlignment.Center;
           
            cell.CellStyle = styleCenter;
            IRow tbRow = TableEx.CreateRow(1);
            #region 列头
            List<string> ColName = new List<string>();
            ColName.Add("资产编号");
            ColName.Add("铅封一");
            ColName.Add("铅封二");
            ColName.Add("铅封三");
            for (int col = 0; col < 4; col++)
            {
                ICell CellCol = tbRow.CreateCell(col);
                CellCol.SetCellValue(ColName[col]);
            }
            #endregion

            for (int row = 0; row < Meter_zcbh.Count; row++)
            {
                IRow MeterInfoRow = TableEx.CreateRow(2 + row);
                ICell InfoCell = MeterInfoRow.CreateCell(0);
                InfoCell.SetCellValue(Meter_zcbh[row]);

                if (Meter_seal_1.Count > 0 && Meter_seal_1.Count > row)
                {
                    InfoCell = MeterInfoRow.CreateCell(1);
                    InfoCell.SetCellValue(Meter_seal_1[row]);
                }
                if (Meter_seal_2.Count > 0 && Meter_seal_2.Count > row)
                {
                    InfoCell = MeterInfoRow.CreateCell(2);
                    InfoCell.SetCellValue(Meter_seal_2[row]);
                }

                if (Meter_seal_3.Count > 0 && Meter_seal_3.Count>row)
                {
                    InfoCell = MeterInfoRow.CreateCell(3);
                    InfoCell.SetCellValue(Meter_seal_3[row]);
                }
            }
            Time=Convert.ToDateTime(Time).ToString("yyyyMMdd");
            using (FileStream fs = File.OpenWrite(SavePath + @"\"+Time+"铅封数据.xls")) //打开一个xls文件，如果没有则自行创建，如果存在myxls.xls文件则在创建是不要打开该文件！
            {
                wk.Write(fs);   //向打开的这个xls文件中写入mySheet表并保存。
                //MessageBox.Show("提示：创建成功！");
            }
        }

        #region LEE 导出所有误差

        public static void InsertErrorExcel(ObservableCollection<CLDC_DataManager.Class.ClouMeterErrorType> Error_obser,List<int> EType_Count,List<int> EGLFX_Count)
        {
            string SavePath = clsMain.getIniString("OtherInfo", "SavePath", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\ReportInfo.ini");
            int TheFirstRow = 3;
            string tempPath = SavePath + str_DetailFileName;
            
            MemoryStream ms = new MemoryStream();
            HSSFWorkbook wk = new HSSFWorkbook();
           
            #region style
            ICellStyle styleTitle = wk.CreateCellStyle();
            styleTitle.Alignment = HorizontalAlignment.Center;
            styleTitle.VerticalAlignment = VerticalAlignment.Center;
       
            #endregion
            ISheet Sheet = wk.CreateSheet("电能表检定");
            int ThisCol = 3, ColTo = 0, colNum = 0;
            #region ROW2
            IRow r_Glfx = Sheet.CreateRow(TheFirstRow);
            foreach (int temp in EGLFX_Count)
            {
                if (temp == 0)
                    continue;
                Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(TheFirstRow, TheFirstRow, ThisCol, temp + ThisCol - 1));
                ICell cell = r_Glfx.CreateCell(3 + colNum);
                colNum = colNum + temp;
                // cell.SetCellValue("123456");
                cell.SetCellValue(Error_obser[ColTo].Error_FY.ToString());
                cell.CellStyle = styleTitle;
                ColTo = ColTo + temp;
                ThisCol = temp + ThisCol;
            }
            #endregion

            #region Row3
            ThisCol = 3; ColTo = 0; colNum = 0;
            IRow tbRow_0 = Sheet.CreateRow(TheFirstRow+1);
            foreach (int temp in EType_Count)
            {
                if (temp == 0)
                    continue;
                Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(TheFirstRow + 1, TheFirstRow + 1, ThisCol, temp + ThisCol - 1));
                ICell cell = tbRow_0.CreateCell(3 + colNum);
                colNum = colNum + temp;
               // cell.SetCellValue("123456");
                cell.SetCellValue(Error_obser[ColTo].Error_GLYS.ToString());
                cell.CellStyle = styleTitle;
                ColTo = ColTo + temp ;
                ThisCol = temp + ThisCol;
            }
            #endregion
            IRow row = Sheet.CreateRow(TheFirstRow + 3);
            IRow row_DL = Sheet.CreateRow(TheFirstRow + 2);
            for (int i = 0; i < Error_obser.Count; i++)
            {
                ICell cell_fzdl = row_DL.CreateCell(3 + i);
                ICell cell_value = row.CreateCell(3 + i);
                if (Const.Gb_Attribute.CHR_CT_CONNECTION_FLAG == "False")
                {
                    cell_fzdl.SetCellValue(Error_obser[i].Error_FZDL.ToString().Trim());
                }
                else
                {
                    cell_fzdl.SetCellValue(Error_obser[i].Error_FZDL.ToString().Trim().Replace("b", "n"));
                    
                }
                
                cell_value.SetCellValue(Error_obser[i].Error_HZZ.ToString().Trim());
            }


            using (FileStream fs = File.OpenWrite(SavePath + str_FinishFileName)) //打开一个xls文件，如果没有则自行创建，如果存在myxls.xls文件则在创建是不要打开该文件！
            {
                wk.Write(fs);   //向打开的这个xls文件中写入mySheet表并保存。
                //MessageBox.Show("提示：创建成功！");
            }
        }

        public static void InsertErrorExcel(ObservableCollection<CLDC_DataManager.Class.ClouMeterErrorType> Error_obser, List<int> EType_Count, List<int> EGLFX_Count,int RowNum)
        {
            string SavePath = clsMain.getIniString("OtherInfo", "SavePath", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\ReportInfo.ini");
            int TheFirstRow = 3;
            string tempPath = SavePath + str_FinishFileName;
            HSSFWorkbook wk = null;
            using (FileStream fs = File.Open(tempPath, FileMode.Open,
 FileAccess.Read, FileShare.ReadWrite))
            {
                //把xls文件读入workbook变量里，之后就可以关闭了  
                wk = new HSSFWorkbook(fs);
                fs.Close();
            }
            ICellStyle styleTitle = wk.CreateCellStyle();
            styleTitle.Alignment = HorizontalAlignment.Center;
            styleTitle.VerticalAlignment = VerticalAlignment.Center;
            styleTitle.BorderBottom = BorderStyle.Thin;
            styleTitle.BorderLeft = BorderStyle.Thin;
            styleTitle.BorderRight = BorderStyle.Thin;
            styleTitle.BorderTop = BorderStyle.Thin;

            ISheet Sheet = wk.GetSheet("电能表检定");
            //int ThisCol = 2, ColTo = 0, colNum = 0;
            #region ROW2
            
            //foreach (int temp in EGLFX_Count)
            //{

            //    Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, ThisCol, temp + ThisCol - 1));
            //    ICell cell = r_Glfx.CreateCell(2 + colNum);
            //    colNum = colNum + temp;
            //    // cell.SetCellValue("123456");
            //    cell.SetCellValue(Error_obser[ColTo].Error_FY.ToString());
            //    ColTo = ColTo + temp;
            //    ThisCol = temp + ThisCol;
            //}
            #endregion

            #region Row3
            //ThisCol = 2; ColTo = 0; colNum = 0;
            //IRow tbRow_0 = Sheet.CreateRow(3);
            //foreach (int temp in EType_Count)
            //{

            //    Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, ThisCol, temp + ThisCol - 1));
            //    ICell cell = tbRow_0.CreateCell(2 + colNum);
            //    colNum = colNum + temp;
            //    // cell.SetCellValue("123456");
            //    cell.SetCellValue(Error_obser[ColTo].Error_GLYS.ToString());
            //    ColTo = ColTo + temp;
            //    ThisCol = temp + ThisCol;
            //}
            #endregion
            IRow row = Sheet.CreateRow(TheFirstRow+3 + RowNum);
            //IRow row_DL = Sheet.CreateRow(4);
            for (int i = 0; i < Error_obser.Count; i++)
            {
                //ICell cell_fzdl = row_DL.CreateCell(2 + i);
                ICell cell_value = row.CreateCell(3 + i);
                //cell_fzdl.SetCellValue(Error_obser[i].Error_FZDL.ToString().Trim());
                cell_value.SetCellValue(Error_obser[i].Error_HZZ.ToString().Trim());
                cell_value.CellStyle = styleTitle;
            }


            using (FileStream fs = File.OpenWrite(SavePath + str_FinishFileName)) //打开一个xls文件，如果没有则自行创建，如果存在myxls.xls文件则在创建是不要打开该文件！
            {
                wk.Write(fs);   //向打开的这个xls文件中写入mySheet表并保存。
                //MessageBox.Show("提示：创建成功！");
            }
        }

        public void OutPutAllError(object ob_List)
        {
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在生成所有误差报表.....");
            UpdateEle Object_List = (UpdateEle)ob_List;
            OutPutAllError(Object_List.AddItemLsit);
           
           
        }
        public  void OutPutAllError(List<string> Meter_Id)
        {
            #region 获取误差
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在获取所有表的误差信息.....");
            int StartFrom = 0;
            ObservableCollection<MeterBaseInfo_OutPut> MeterBaseOut=new ObservableCollection<MeterBaseInfo_OutPut> ();
            for (int i = 0; i < Meter_Id.Count; i++)
            {
                ObservableCollection<CLDC_DataManager.Class.ClouMeterErrorType> Error_1 = new ObservableCollection<Class.ClouMeterErrorType>();
                List<int> One_GLYS_Count = new List<int>();
                List<int> One_Type_FY_Count = new List<int>();
                int ColCount = 0, Type_FY = 0;
                #region p+
                //p+
                GetErrorInfo("1.0", "0", "1",  Meter_Id[i], ref Error_1);
                One_GLYS_Count.Add(Error_1.Count); ColCount = Error_1.Count;
                GetErrorInfo("0.5L", "0", "1",  Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "0", "1",  Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count);
                Type_FY = Error_1.Count;
                //p+ A
                GetErrorInfo("1.0", "0", "2",  Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "0", "2",  Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "0", "2",  Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;

                //p+ B
                GetErrorInfo("1.0", "0", "3",  Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "0", "3",  Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "0", "3",  Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;

                //p+ C
                GetErrorInfo("1.0", "0", "4",  Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "0", "4",  Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "0", "4",  Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;
                #endregion

                #region Q+
                //Q+
                GetErrorInfo("1.0", "1", "1", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "1", "1", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "1", "1", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;
                //Q+ A
                GetErrorInfo("1.0", "1", "2", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "1", "2", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "1", "2", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;

                //Q+ B
                GetErrorInfo("1.0", "1", "3", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "1", "3", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "1", "3", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;

                //Q+ C
                GetErrorInfo("1.0", "1", "4", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "1", "4", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "1", "4", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;

#endregion

                #region P-
                //p-
                GetErrorInfo("1.0", "2", "1",  Meter_Id[i], ref Error_1);
                One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                GetErrorInfo("0.5L", "2", "1",  Meter_Id[i], ref Error_1);
                One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                GetErrorInfo("0.8C", "2", "1",  Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;
                //p- A
                GetErrorInfo("1.0", "2", "2", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "2", "2", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "2", "2", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;
                //p- B
                GetErrorInfo("1.0", "2", "3", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "2", "3", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "2", "3", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;
                //p- c
                GetErrorInfo("1.0", "2", "4", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "2", "4", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "2", "4", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;
                #endregion

                #region Q-
                //Q-
                GetErrorInfo("1.0", "3", "1", Meter_Id[i], ref Error_1);
                One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                GetErrorInfo("0.5L", "3", "1", Meter_Id[i], ref Error_1);
                One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                GetErrorInfo("0.8C", "3", "1", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;
                //Q- A
                GetErrorInfo("1.0", "3", "2", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "3", "2", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "3", "2", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;
                //Q- B
                GetErrorInfo("1.0", "3", "3", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "3", "3", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "3", "3", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;
                //Q- c
                GetErrorInfo("1.0", "3", "4", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.5L", "3", "4", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                GetErrorInfo("0.8C", "3", "4", Meter_Id[i], ref Error_1);
                if (Error_1.Count - ColCount != 0)
                {
                    One_GLYS_Count.Add(Error_1.Count - ColCount); ColCount = Error_1.Count;
                }
                One_Type_FY_Count.Add(Error_1.Count - Type_FY);
                Type_FY = Error_1.Count;
                #endregion
                if (i == 0)
                {
                    InsertErrorExcel(Error_1, One_GLYS_Count, One_Type_FY_Count);
                }
                else
                {
                    InsertErrorExcel(Error_1, One_GLYS_Count, One_Type_FY_Count,i);
                }
                StartFrom=Error_1.Count;
                
            }
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在获取所有表的基本信息.....");
            Get_METER_BaseInfo(Meter_Id,ref MeterBaseOut);
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在获取所有表的多功能信息.....");
            Get_METER_COMMUNICATION("002", Meter_Id, ref MeterBaseOut);
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在获取所有表的潜动启动信息.....");
            Get_METER_START_NO_LOAD(Meter_Id, ref MeterBaseOut);
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在获取所有表的结论信息.....");

            Get_METER_RESULTS("103", Meter_Id, ref MeterBaseOut);

            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在获取所有表的费控结论信息.....");

            Get_METER_RATES_CONTROL(Meter_Id, ref MeterBaseOut);
            CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在插入表信息到EXCEL表格.....");

            InsertMeterBaseInfo(StartFrom, MeterBaseOut);
            TaskCallBack();
            #endregion

        }
        public static void InsertMeterBaseInfo(int startFromCol, ObservableCollection<MeterBaseInfo_OutPut> meterOutPut)
        {
            string SavePath = clsMain.getIniString("OtherInfo", "SavePath", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\ReportInfo.ini");
            int TheFirstRow = 3,TheEndRow=0;
            List<string> ColTitle = new List<string>();
            #region 列头
            ColTitle.Add("基本误差");
            ColTitle.Add("日计时误差");
            ColTitle.Add("起动");
            ColTitle.Add("潜动");
            ColTitle.Add("误差一致性");
            ColTitle.Add("多功能");
            ColTitle.Add("通讯");
            ColTitle.Add("身份认证");
            ColTitle.Add("保电功能");
            ColTitle.Add("密钥更新");
            ColTitle.Add("总结论");

            #endregion
            string tempPath = SavePath + str_FinishFileName;
            HSSFWorkbook wk = null;
            using (FileStream fs = File.Open(tempPath, FileMode.Open,
            FileAccess.Read, FileShare.ReadWrite))
            {
                //把xls文件读入workbook变量里，之后就可以关闭了  
                wk = new HSSFWorkbook(fs);
                fs.Close();
            }
            #region style
            ICellStyle styleTitle = wk.CreateCellStyle();
            styleTitle.Alignment = HorizontalAlignment.Center;
            styleTitle.VerticalAlignment = VerticalAlignment.Center;
            
            styleTitle.BorderBottom = BorderStyle.Thin;
            styleTitle.BorderLeft = BorderStyle.Thin;
            styleTitle.BorderRight = BorderStyle.Thin;
            styleTitle.BorderTop = BorderStyle.Thin;
            #endregion
            #region 执行插入
            HSSFSheet Sheet = wk.GetSheet("电能表检定") as HSSFSheet;
            Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(TheFirstRow, TheFirstRow+2, 1, 2));
            Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(TheFirstRow, TheFirstRow + 2, 0, 0));
            IRow row_zcbh = Sheet.GetRow(TheFirstRow);
            ICell cell_zcbh = row_zcbh.CreateCell(1);
            cell_zcbh.SetCellValue("资产编号");
            cell_zcbh.CellStyle = styleTitle;
            ICell cell_Num = row_zcbh.CreateCell(0);

            cell_Num.SetCellValue("序号");
            cell_Num.CellStyle = styleTitle;
            string InsertValue = "";
            for (int col = 0; col < ColTitle.Count; col++)
            {
                if (col >5&&col<10)
                {
                    ICell Cell_error_R;
                    if (col == 6)
                    {
                        Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(TheFirstRow, TheFirstRow, startFromCol + 3 + col, startFromCol + 6 + col));
                        Cell_error_R = row_zcbh.CreateCell(startFromCol + 3 + col);
                        Cell_error_R.CellStyle = styleTitle;
                        Cell_error_R.SetCellValue("费控功能");
                       
                        
                    }
                    Sheet.SetColumnWidth(startFromCol + 3 + col, 11 * 256);
                    Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(TheFirstRow+1, TheFirstRow+2, startFromCol + 3 + col, startFromCol + 3 + col));
                    IRow row_Fk = Sheet.GetRow(TheFirstRow+1);
                    Cell_error_R = row_Fk.CreateCell(startFromCol + 3 + col);
                    Cell_error_R.CellStyle = styleTitle;
                    Cell_error_R.SetCellValue(ColTitle[col]);
                    
                }
                else
                {
                    Sheet.SetColumnWidth(startFromCol + 3 + col, 11 * 256);
                    Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(TheFirstRow, TheFirstRow + 2, startFromCol + 3 + col, startFromCol + 3 + col));
                    ICell Cell_error_R = row_zcbh.CreateCell(startFromCol + 3 + col);
                    Cell_error_R.SetCellValue(ColTitle[col]);
                    Cell_error_R.CellStyle = styleTitle;
                }
                
                for (int i = 0; i < meterOutPut.Count; i++)
                {
                    IRow row_list = Sheet.GetRow(TheFirstRow + 3 + i);
                    #region Denifity Data
                    switch (col.ToString())
                    { 
                        case "0":
                            InsertValue = meterOutPut[i].Meter_Error_R;
                            break;
                        case "1":
                            InsertValue = meterOutPut[i].Meter_DayTime_R;
                            break;
                        case "2":
                            InsertValue = meterOutPut[i].Meter_QiDong;
                            break;
                        case "3":
                            InsertValue = meterOutPut[i].Meter_QianDong;
                            break;
                        case "4":
                            InsertValue = meterOutPut[i].Meter_Consistanct;
                            break;
                        case "5":
                            InsertValue = meterOutPut[i].Meter_MultiResult;
                            break;
                        case "6":
                            InsertValue = meterOutPut[i].Meter_Fk_TX;
                            break;
                        case "7":
                            InsertValue = meterOutPut[i].Meter_Fk_IdCheck;
                            break;
                        case "8":
                            InsertValue = meterOutPut[i].Meter_Fk_SaveElectorn;
                            break;
                        case "9":
                            InsertValue = meterOutPut[i].Meter_Fk_Code;
                            break;
                        case "10":
                            InsertValue = meterOutPut[i].Meter_Result;
                            break;
                    }
                    #endregion
                    ICell cell_lis = row_list.CreateCell(startFromCol + 3 + col);
                    cell_lis.CellStyle=styleTitle;
                    cell_lis.SetCellValue(InsertValue);
                    
                    TheEndRow = startFromCol + 3 + col;
                }
                
            }
                
            for (int i = 0; i < meterOutPut.Count; i++)
            {
                IRow row_list = Sheet.GetRow(TheFirstRow+3 + i);
                Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6 + i, 6 + i, 1, 2));
                ICell cell_lis = row_list.CreateCell(1);
                cell_lis.SetCellValue(meterOutPut[i].Meter_zcbh.Trim());
                ICell cell_num = row_list.CreateCell(0);
                cell_num.SetCellValue((i + 1).ToString());
            }

            Sheet.AutoSizeColumn(1);
            

            #region 结论信息
            Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, TheEndRow));
            IRow row_Title = Sheet.CreateRow(0);
            ICell cell_Title = row_Title.CreateCell(0);
            ICellStyle styleCenter = wk.CreateCellStyle();
            styleCenter.Alignment = HorizontalAlignment.Center;
            IFont fontLeft = wk.CreateFont();
            fontLeft.FontHeightInPoints =16;
            fontLeft.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            styleCenter.SetFont(fontLeft);
            cell_Title.CellStyle = styleCenter;
            cell_Title.SetCellValue("电能表检定记录表");
            #endregion


            #endregion

            CellRangeAddress region = new CellRangeAddress(3, meterOutPut.Count+3+2, 0, TheEndRow);
            ICellStyle style = wk.CreateCellStyle();
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.Alignment = HorizontalAlignment.Center;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.TopBorderColor = HSSFColor.Black.Index;
            for (int i = region.FirstRow; i <= region.LastRow; i++)
            {
                IRow row = HSSFCellUtil.GetRow(i, Sheet);
                for (int j = region.FirstColumn; j <= region.LastColumn; j++)
                {
                    ICell singleCell = HSSFCellUtil.GetCell(row, (short)j);
                    singleCell.CellStyle = style;
                }
            }  
            
            #region 插入文档头的信息
            string str_ZZname = clsMain.getIniString("DeviceInfo", "txt_ZzName", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
            string str_ZZType = clsMain.getIniString("DeviceInfo", "txt_ZzType", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
            string str_ZZNum = clsMain.getIniString("DeviceInfo", "txt_ZzNum", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\Reportinfo.ini");
            string str_guicheng = CLDC_DataManager.Const.Gb_Attribute.chr_CheckYJ;
            #region 组合被插入值
            List<string> FristKey = new List<string>();
            List<string> FristValue = new List<string>();
            FristKey.Add("装置名称：");
            FristKey.Add("装置类型：");
            FristKey.Add("装置编号：");
            FristKey.Add("温度：");
            FristKey.Add("湿度：");
            FristKey.Add("检定依据");
            FristValue.Add(str_ZZname);
            FristValue.Add(str_ZZType);
            FristValue.Add(str_ZZNum);
            FristValue.Add(meterOutPut[0].Meter_WD);
            FristValue.Add(meterOutPut[0].Meter_SD);
            FristValue.Add(str_guicheng);
            #endregion
            IRow First = Sheet.CreateRow(1);
            for(int First_int=0;First_int<6;First_int++)
            { 
                Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 1+First_int*5, 4+First_int*5));
                ICell Cell_Tile_key = First.CreateCell(0 + First_int * 5);
                ICell Cell_Tile_Value = First.CreateCell(1 + First_int * 5);
                Cell_Tile_key.SetCellValue(FristKey[First_int]);
                Cell_Tile_Value.SetCellValue(FristValue[First_int]);
            }
            FristKey.Clear();
            FristValue.Clear();
            FristKey.Add("电表名称：");
            FristKey.Add("厂家：");
            FristKey.Add("型号：");
            FristKey.Add("常数：");
            FristKey.Add("等级：");
            FristKey.Add("规格：");
            FristKey.Add("检定时间：");
            FristKey.Add("铅封一：");
            FristKey.Add("铅封二：");
            FristKey.Add("铅封三：");
            FristValue.Add(meterOutPut[0].Meter_Name);
            FristValue.Add(meterOutPut[0].Meter_factory);
            FristValue.Add(meterOutPut[0].Meter_Type);
            FristValue.Add(meterOutPut[0].Meter_Const);
            FristValue.Add(meterOutPut[0].Meter_rank);
            FristValue.Add(meterOutPut[0].Meter_Size);
            FristValue.Add(meterOutPut[0].Meter_Checktime);
            FristValue.Add(meterOutPut[0].Seal001);
            FristValue.Add(meterOutPut[0].Seal002);
            FristValue.Add(meterOutPut[0].Seal003);
            IRow Second = Sheet.CreateRow(2);
            for (int First_int = 0; First_int < 10; First_int++)
            {
                Sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 1 + First_int * 5, 4 + First_int * 5));
                ICell Cell_Tile_key = Second.CreateCell(0 + First_int * 5);
                ICell Cell_Tile_Value = Second.CreateCell(1 + First_int * 5);
                Cell_Tile_key.SetCellValue(FristKey[First_int]);
                Cell_Tile_Value.SetCellValue(FristValue[First_int]);
            }
            #endregion 

        
            using (FileStream fs = File.OpenWrite(SavePath + str_FinishFileName)) //打开一个xls文件，如果没有则自行创建，如果存在myxls.xls文件则在创建是不要打开该文件！
                {
                    wk.Write(fs);   //向打开的这个xls文件中写入mySheet表并保存。
                    //MessageBox.Show("提示：创建成功！");
                }
            if (File.Exists(CLDC_DataManager.Const.Gb_Attribute.str_ExcelSavePath))
            {
                File.Delete(CLDC_DataManager.Const.Gb_Attribute.str_ExcelSavePath);
            }
            File.Move(SavePath + str_FinishFileName, CLDC_DataManager.Const.Gb_Attribute.str_ExcelSavePath);
        }

        private static void Get_METER_COMMUNICATION(string RESULT_ID, List<string> MeterId, ref ObservableCollection<MeterBaseInfo_OutPut> METERBASE)
        {
            
            string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
            string Sql_word_2 = ";Persist Security Info=False";
            string DataPath = clsMain.getIniString("Path", "DataPath", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\ManageInfo.ini");
            ObservableCollection<CLDC_DataManager.Class.ClouMeterErrorType> ErrorCol = new ObservableCollection<Class.ClouMeterErrorType>();
            OleDbConnection conn = new OleDbConnection(Sql_word_1 + DataPath + Sql_word_2);
            for (int i = 0; i < MeterId.Count; i++)
            {
                if (conn.State == ConnectionState.Closed)
                { conn.Open(); }
                string strSQL = string.Format(" SELECT AVR_VALUE FROM METER_COMMUNICATION where AVR_PROJECT_NO='{0}' and FK_LNG_METER_ID='{1}'", RESULT_ID, MeterId[i]);

                OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                OleDbDataReader Myreader = null;
                try
                {
                    Myreader = cmd.ExecuteReader();
                    while (Myreader.Read())
                    {
                        METERBASE[i].Meter_DayTime_R = Myreader["AVR_VALUE"].ToString().Trim();
                    }
                    conn.Close();


                }
                catch (Exception EX_MUl)
                {
                    conn.Close();

                }
            }
               
           
        }
       
        private static void Get_METER_START_NO_LOAD(List<string> MeterId, ref ObservableCollection<MeterBaseInfo_OutPut> METERBASE)
        {
            
            string DataPath = clsMain.getIniString("Path", "DataPath", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\ManageInfo.ini");
            string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
            string Sql_word_2 = ";Persist Security Info=False";
            ObservableCollection<CLDC_DataManager.Class.ClouMeterErrorType> ErrorCol = new ObservableCollection<Class.ClouMeterErrorType>();
            OleDbConnection conn = new OleDbConnection(Sql_word_1 + DataPath + Sql_word_2);
            
            for (int i = 0; i < MeterId.Count; i++)
            {
                string strSQL = string.Format(" SELECT * FROM METER_START_NO_LOAD where FK_LNG_METER_ID='{0}'", MeterId[i]);
                if (conn.State == ConnectionState.Closed)
                { conn.Open(); }
                OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                OleDbDataReader Myreader = null;
                try
                {
                    Myreader = cmd.ExecuteReader();
                    while (Myreader.Read())
                    {
                        if (Myreader["AVR_PROJECT_NO"].ToString().Trim() == "1091")
                        {
                            METERBASE[i].Meter_QianDong = Myreader["AVR_CONCLUSION"].ToString().Trim();
                        }
                        if (Myreader["AVR_PROJECT_NO"].ToString().Trim() == "1101115")
                        {
                            METERBASE[i].Meter_QiDong = Myreader["AVR_CONCLUSION"].ToString().Trim();
                        }
                    }
                    conn.Close();

                }
                catch (Exception EX_QD)
                {
                    conn.Close();


                }     
            }
               
        }

        private static void Get_METER_RATES_CONTROL(List<string> MeterId, ref ObservableCollection<MeterBaseInfo_OutPut> METERBASE)
        {
            
            string DataPath = clsMain.getIniString("Path", "DataPath", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\ManageInfo.ini");
            string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
            string Sql_word_2 = ";Persist Security Info=False";
            ObservableCollection<CLDC_DataManager.Class.ClouMeterErrorType> ErrorCol = new ObservableCollection<Class.ClouMeterErrorType>();
            OleDbConnection conn = new OleDbConnection(Sql_word_1 + DataPath + Sql_word_2);

            for (int i = 0; i < MeterId.Count; i++)
            {
                string strSQL = string.Format(" SELECT * FROM METER_RATES_CONTROL where FK_LNG_METER_ID='{0}'", MeterId[i]);
                if (conn.State == ConnectionState.Closed)
                { conn.Open(); }
                OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                OleDbDataReader Myreader = null;
                try
                {
                    Myreader = cmd.ExecuteReader();
                    while (Myreader.Read())
                    {
                        if (Myreader["AVR_ITEM_TYPE"].ToString().Trim() == "013")
                        {
                            METERBASE[i].Meter_Fk_Code = Myreader["AVR_CONCLUSION"].ToString().Trim();
                            METERBASE[i].Meter_Fk_IdCheck = Myreader["AVR_CONCLUSION"].ToString().Trim();
                            METERBASE[i].Meter_Fk_TX = Myreader["AVR_CONCLUSION"].ToString().Trim();
                            METERBASE[i].Meter_Fk_SaveElectorn = Myreader["AVR_CONCLUSION"].ToString().Trim();
                        }
                        
                    }
                    conn.Close();

                }
                catch (Exception EX_QD)
                {
                    conn.Close();


                }
            }

        }

        private static void Get_METER_BaseInfo(List<string> MeterId, ref ObservableCollection<MeterBaseInfo_OutPut> METERBASE)
        {
           
            string DataPath = clsMain.getIniString("Path", "DataPath", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\ManageInfo.ini");
          
            string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
            string Sql_word_2 = ";Persist Security Info=False";
            ObservableCollection<CLDC_DataManager.Class.ClouMeterErrorType> ErrorCol = new ObservableCollection<Class.ClouMeterErrorType>();
            OleDbConnection conn = new OleDbConnection(Sql_word_1 + DataPath + Sql_word_2);
            string votage = "";
            string str_zcbh="";
            foreach (string tempId in MeterId)
            {
                string strSQL = string.Format(" SELECT * FROM METER_INFO where PK_LNG_METER_ID='{0}' ", tempId);

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    { conn.Open(); }
                    OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                    OleDbDataReader Myreader = null;
                    Myreader = cmd.ExecuteReader();
                    while (Myreader.Read())
                    {
                        str_zcbh = Myreader["AVR_ASSET_NO"].ToString().Trim() != "" ? Myreader["AVR_ASSET_NO"].ToString().Trim() : Myreader["AVR_BAR_CODE"].ToString().Trim();
                        switch (Myreader["AVR_UB"].ToString().Trim())
                        { 
                            case "220":
                                votage = "380";
                                break;
                            case "57.7":
                                votage = "100";
                                break;
                        }

                        METERBASE.Add(new MeterBaseInfo_OutPut()
                        {
                            Meter_zcbh = str_zcbh,
                            Meter_Result = Myreader["AVR_TOTAL_CONCLUSION"].ToString().Trim(),
                            Meter_WD = Myreader["AVR_TEMPERATURE"].ToString().Trim() + @"°C",
                            Meter_SD = Myreader["AVR_HUMIDITY"].ToString().Trim() + @"%",
                            Meter_Name = Myreader["AVR_METER_NAME"].ToString().Trim(),
                            Meter_Type = Myreader["AVR_METER_MODEL"].ToString().Trim(),
                            Meter_Const = Myreader["AVR_AR_CONSTANT"].ToString().Trim() + @"imp/kwh(imp/kvarh)",
                            Meter_rank = Myreader["AVR_AR_CLASS"].ToString().Trim(),
                            Meter_Size = "3×" + Myreader["AVR_UB"].ToString().Trim()+@"/"+votage + "V  " + "3×" + Myreader["AVR_IB"].ToString().Trim() + "A",
                            Meter_Checktime = Myreader["DTM_TEST_DATE"].ToString().Trim(),
                            Meter_factory = Myreader["AVR_FACTORY"].ToString().Trim(),
                            Seal001 = Myreader["AVR_SEAL_1"].ToString().Trim(),
                            Seal002 = Myreader["AVR_SEAL_1"].ToString().Trim(),
                            Seal003 = Myreader["AVR_SEAL_2"].ToString().Trim(),

                        });
                    }
                    conn.Close();
                
                    
                }
                catch (Exception EX_base)
                {
                    conn.Close();
                 
                }
            }
            
           
        }
        private static void Get_METER_RESULTS(string RESULT_ID, List<string> MeterId, ref ObservableCollection<MeterBaseInfo_OutPut> METERBASE)
        {
        
            string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
            string Sql_word_2 = ";Persist Security Info=False";
            string DataPath = clsMain.getIniString("Path", "DataPath", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\ManageInfo.ini");
          
            ObservableCollection<CLDC_DataManager.Class.ClouMeterErrorType> ErrorCol = new ObservableCollection<Class.ClouMeterErrorType>();
            OleDbConnection conn = new OleDbConnection(Sql_word_1 + DataPath + Sql_word_2);
            for (int i = 0; i < MeterId.Count; i++)
            {
                if (conn.State == ConnectionState.Closed)
                { conn.Open(); }
                string strSQL = " SELECT * FROM METER_RESULTS where  FK_LNG_METER_ID='" + MeterId[i] + "'";
                OleDbCommand cmd = new OleDbCommand(strSQL, conn);
                OleDbDataReader Myreader = null;
                try
                {
                    Myreader = cmd.ExecuteReader();
                    while (Myreader.Read())
                    {
                        if (Myreader["AVR_RESULT_ID"].ToString().Trim() == RESULT_ID)
                        {
                            METERBASE[i].Meter_Error_R = Myreader["AVR_RESULT_VALUE"].ToString().Trim();

                        }
                        if (Myreader["AVR_RESULT_ID"].ToString().Trim() == "1151")
                        {
                            METERBASE[i].Meter_Consistanct = Myreader["AVR_RESULT_VALUE"].ToString().Trim();

                        }
                        if (Myreader["AVR_RESULT_ID"].ToString().Trim() == "107")
                        {
                            METERBASE[i].Meter_MultiResult = Myreader["AVR_RESULT_VALUE"].ToString().Trim();

                        }
                        
                    }
                    conn.Close();
   
                }
                catch (Exception EX_R)
                {
                    conn.Close();

                  
                }
            }
              
        
        }
        public static void GetErrorInfo(string GLYS, string str_GLFX, string str_FY, string MeterID, ref ObservableCollection<CLDC_DataManager.Class.ClouMeterErrorType> Error_ALL)
        {
            
            try
            {
                 string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
                 string Sql_word_2 = ";Persist Security Info=False";
                 string DataPath = clsMain.getIniString("Path", "DataPath", "", System.Windows.Forms.Application.StartupPath + "\\Plugins\\ManageInfo.ini");
        
                ObservableCollection<CLDC_DataManager.Class.ClouMeterErrorType> ErrorCol = new ObservableCollection<Class.ClouMeterErrorType>();
                OleDbConnection conn = new OleDbConnection(Sql_word_1 + DataPath + Sql_word_2);
                if (conn.State == ConnectionState.Closed)
                { conn.Open(); }
                string sql = string.Format("SELECT * FROM METER_ERROR WHERE 1=1 AND AVR_POWER_FACTOR ='{0}' AND CHR_POWER_TYPE='{1}' AND CHR_COMPONENT='{2}'and FK_LNG_METER_ID='{3}' order by AVR_IB_MULTIPLE desc", GLYS, str_GLFX, str_FY, MeterID);
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataReader Myreader = null;
                Myreader = cmd.ExecuteReader();
                string Error_Value = "",FY="",GLFX="",Glys_done="";
                char[] csplit = { '|' };
                string[] Error_list = null;
                while (Myreader.Read())
                {
                    Error_Value = Myreader["AVR_ERROR_MORE"].ToString().Trim();
                    Error_list = Error_Value.Split(csplit);
                    Error_Value=Error_list[Error_list.Length-2];
                    #region FYDM
                    switch (Myreader["CHR_COMPONENT"].ToString().Trim())
                    { 
                        case "1":
                            FY = "平衡负载";
                            break;
                        case "2":
                            FY = "不平衡负载A相";
                            break;
                        case "3":
                            FY = "不平衡负载B相";
                            break;
                        case "4":
                            FY = "不平衡负载C相";
                            break;

                    }
                    #endregion
                    #region GLFX
                    switch (Myreader["CHR_POWER_TYPE"].ToString().Trim())
                    {
                        case "1":
                            GLFX = "反向有功";
                            break;
                        case "2":
                            GLFX = "正向无功";
                            break;
                        case "3":
                            GLFX = "反向无功";
                            break;
                        case "0":
                            GLFX = "正向有功";
                            break;

                    }
                    #endregion
                    if (FY.Contains("不平衡"))
                    {
                        Glys_done = @"cosθ";
                    }
                    else {
                        Glys_done = @"cosφ"; 
                    }
                    Error_ALL.Add(new CLDC_DataManager.Class.ClouMeterErrorType()
                    {
                        Error_GLYS = Glys_done+"="+Myreader["AVR_POWER_FACTOR"].ToString(),
                        Error_HZZ=Error_Value,
                        Error_FZDL = Myreader["AVR_IB_MULTIPLE"].ToString(),
                        Error_FY = GLFX+"   "+FY,
                    });
                }

                conn.Close();
            }
            catch (Exception e)
            {
                
            }
        }
        #endregion



        public Microsoft.Office.Interop.Excel.Worksheet getSheet(string path, bool isReadOnly)
        {
            if (ExcelApp == null) return null;

            return (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Workbooks.Open(path, 0, isReadOnly, 1, System.Type.Missing, System.Type.Missing, false, System.Type.Missing, System.Type.Missing, System.Type.Missing, false, System.Type.Missing, false, false, System.Type.Missing).Sheets[1];

        }

        public void CloseExcel()
        {
            try
            {
                ExcelApp.Workbooks.Close();
            }
            catch { }
        }

        public void Quit()
        {
            try
            {
                ExcelApp.Quit();
                ExcelApp = null;

            }
            catch { }
        }
    }

    public class UpdateEle
    {
        public List<string> AddItemLsit { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace CLDC_DataManager
{
    public partial class Frm_DataImport : Form
    {
        const string CONST_MISINTERFACE_PATH = @"C:\PMSJYT\CURRENT";

        public Frm_DataImport()
        {
            InitializeComponent();
        }

        private void Cmd_Import_Click(object sender, EventArgs e)
        {
            if (Cmb_filename.SelectedIndex < 0)
            {
                MessageBox.Show("请选择文件名...", "导入失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cmb_filename.Focus();
                return;
            }
            CLDC_DataCore.DataBase.clsDataManage ItemDB;

            if (clsMain.getIniString("Server", "Run") == "1")           //服务器访问
            {

                ItemDB = new CLDC_DataCore.DataBase.clsDataManage(clsMain.getIniString("Server", "Host"), clsMain.getIniString("Server", "Name"), clsMain.getIniString("Server", "Pwd"));

                if (!ItemDB.Connection)
                {
                    MessageBox.Show("数据库访问失败，请检查设置...\n【如果是服务器端，请选择信息设置中的服务器访问】", "数据库连接出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                CLDC_DataCore.Function.TopWaiting.ShowWaiting("正在导入数据，请稍候...");
                List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items = getMeterItems(Cmb_filename.Text);

                ItemDB.InsertMeterInfoTemp(Items);
                
                ItemDB.DbClose();
                ItemDB = null;
                CLDC_DataCore.Function.TopWaiting.HideWaiting();
                MessageBox.Show("导入完成","消息",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void Frm_DataImport_Load(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(CONST_MISINTERFACE_PATH)) return;

            Cmb_filename.Items.Clear();
            foreach (System.IO.FileInfo finfo in new System.IO.DirectoryInfo(CONST_MISINTERFACE_PATH).GetFiles())
            {
                Cmb_filename.Items.Add(finfo.Name);
            }
        }

        private void Cmd_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 获取EXCEL中数据
        /// </summary>
        /// <param name="Items">需要获取数据的文件名称</param>
        /// <returns>电能表数据集合对象</returns>
        public List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> getMeterItems(string filename)
        {
            string strPath = "";
            List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo> Items = new List<CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo>(); ;
            if (!System.IO.Directory.Exists(CONST_MISINTERFACE_PATH)) return null;

            foreach (System.IO.FileInfo finfo in new System.IO.DirectoryInfo(CONST_MISINTERFACE_PATH).GetFiles())
            {
                if (finfo.Name == filename)
                {
                    strPath = finfo.FullName;
                    break;
                }
            }

            if (strPath == "") return null;

            clsExcelControl excelControl = new clsExcelControl();

            Microsoft.Office.Interop.Excel.Worksheet sheet = excelControl.getSheet(strPath, false);

            for (int rowindex = 2; rowindex < sheet.Cells.Count; rowindex++)
            {
                Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 3];

                if (range == null || range.Text.ToString() == "") break;

                CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo Item = new CLDC_DataCore.Model.DnbModel.DnbInfo.MeterBasicInfo();

                Item.Mb_ChrTxm = range.Text.ToString();
                Item.Mb_chrBlx = ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 2]).Text.ToString();   //装置类型
                Item.Mb_ChrCcbh = ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 4]).Text.ToString();
                Item.Mb_ChrJlbh = ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 5]).Text.ToString();  //装置编号
                Item.Mb_chrzzcj = ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 6]).Text.ToString();  //生产产家
                Item.Mb_Bxh = ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 7]).Text.ToString();      //表型号
                Item.Mb_chrIb = string.Format("{0}({1})", ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 8]).Text,
                                                            ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 9]).Text);
                Item.Mb_chrUb = (float.Parse(((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 10]).Text.ToString()) * 1000d).ToString("F0");
                Item.Mb_chrHz = ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 11]).Text.ToString();
                Item.Mb_chrBcs = ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 12]).Text.ToString();
                Item.Mb_chrQianFeng1 = ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 14]).Text.ToString();
                Item.Mb_chrQianFeng2 = ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 15]).Text.ToString();
                Item.Mb_chrBdj = ((Microsoft.Office.Interop.Excel.Range)sheet.Cells[rowindex, 64]).Text.ToString();
                Items.Add(Item);

            }

            excelControl.CloseExcel();
            excelControl.Quit();

            return Items;
        }

    }
}

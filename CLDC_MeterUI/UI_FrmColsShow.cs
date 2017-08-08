using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CLDC_DataCore.SystemModel;
using CLDC_DataCore.SystemModel.Item;
using CLDC_DataCore.Struct;

namespace CLDC_MeterUI
{
    public partial class UI_FrmColsShow : Office2007Form
    {
        private static UI_FrmColsShow instance;
        private static object syncRoot = new Object();
        public static UI_FrmColsShow Instance(SystemInfo Item)
        {

                instance = null;
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new UI_FrmColsShow(Item);
                    }
                }
                return instance;
            
        }
        public void ShowD()
        {
            if (instance != null)
            {
                if (instance.Visible != true)
                {
                    instance.ShowDialog();
                }

            }
        }

        private SystemInfo _SystemCol;

        public UI_FrmColsShow(SystemInfo Item)
        {
            InitializeComponent();
            this.TopMost = true;
            InitControl();
            this.SystemCol = Item;
        }

        private void InitControl()
        {
            DataGridViewColumn GridCol = null;
            GridCol = new DataGridViewTextBoxColumn();
            GridCol.Name = "ID";
            GridCol.HeaderText = "原始列名";
            GridCol.ReadOnly = true;
            GridCol.DefaultCellStyle = new DataGridViewCellStyle() { BackColor=Color.Gray};
            dgv_CXLRColsVisiable.Columns.Add(GridCol);
            GridCol = new DataGridViewTextBoxColumn();
            GridCol.Name = "Name";
            GridCol.HeaderText = "显示列明";
            dgv_CXLRColsVisiable.Columns.Add(GridCol);
            GridCol = new DataGridViewCheckBoxColumn();
            GridCol.Name = "ShowType";
            GridCol.HeaderText = "是否显示";
            dgv_CXLRColsVisiable.Columns.Add(GridCol);
            dgv_CXLRColsVisiable.AllowUserToAddRows = false;
            dgv_CXLRColsVisiable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        /// <summary>
        /// 系统信息模型赋值
        /// </summary>
        private SystemInfo SystemCol
        {
            get
            {
                return _SystemCol;
            }
            set
            {
                _SystemCol = value;
                this.DefaultColsShowGrid(_SystemCol.ColsVisiable);          //初始化系统信息列表
                
            }
        }

        private void DefaultColsShowGrid(csColsVisiable csColsVisiable)
        {
            List<StColsVisiable> lstCols = csColsVisiable.getColPrj();
            foreach (StColsVisiable item in lstCols)
            {
                dgv_CXLRColsVisiable.Rows.Add(item.ColName, item.ColShowName, (item.ColShowType == 0 ? false : true));
            }
        }

        

        private void btn_OK_Click(object sender, EventArgs e)
        {
            //TODO：处理界面更改
            Dictionary<string, StColsVisiable> _ColsVisiable = new Dictionary<string, StColsVisiable>();

            for (int i = 0; i < dgv_CXLRColsVisiable.Rows.Count; i++)
            {
                StColsVisiable _Col = new CLDC_DataCore.Struct.StColsVisiable();
                _Col.ColName = dgv_CXLRColsVisiable.Rows[i].Cells[0].Value.ToString();
                _Col.ColShowName = dgv_CXLRColsVisiable.Rows[i].Cells[1].Value.ToString();
                _Col.ColShowType = (bool)dgv_CXLRColsVisiable.Rows[i].Cells[2].Value == true ? 1 : 0;
                _ColsVisiable.Add(_Col.ColName, _Col);
            }
            
            _SystemCol.ColsVisiable._ColsVisiable = _ColsVisiable; 
            _SystemCol.ColsVisiable.Save();
            this.Close();
            
        }

        private void btn_Cansel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

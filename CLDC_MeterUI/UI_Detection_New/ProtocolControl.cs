using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace CLDC_MeterUI.UI_Detection_New
{
    public partial class ProtocolControl : UserControl
    {
        
        public ProtocolControl()
        {
            InitializeComponent();
            
        }
        string strPath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Environment.CurrentDirectory + @"\DATABASE\ClouProtocol.mdb";
        
        int rowindex = 0;  //datagrid选择行的Id
        int colindex = 0;  //datagrid选择列的Id
        string parentName = "DLT645-2007"; //当前选中树形控件第一级节点名
        string parent_nextName = "电能量"; //当前选中树形控件第二级节点名
        int parentIndex_next = -1;         //当前选中树形控件选中的第二级节点的索引
        int Index_tree = -1;               //当前选中树形控件选中的第三级节点的索引
        int Node_Level = 0;                //当前选中树形控件选中的节点的等级

        private void ProtocolControl_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 加载树形控件
        /// </summary>
        public void Load_TV()
        {
            TV_Protocol.Nodes.Clear();
            CLDC_DataCore.DataBase.DbHelperOleDb.connectionString = strPath;
            List<string> protocolNameList = new List<string>();
         

            #region 读取标准名称的列表
            string sql = "select AVR_PROTOCOL_NAME from PROTOCOL_INFO";
            //读取标准名
            using (OleDbDataReader dr = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteReader(sql))
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        protocolNameList.Add(dr["AVR_PROTOCOL_NAME"].ToString());
                    }
                }
            }
            #endregion 读取标准名称的列表

            #region 加载标准名称所对应的子项
            //加载标准名称所对应的子项
            foreach (string protocolName in protocolNameList)
            {
                CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode(protocolName);
                sql = string.Format("select * from PROTOCOL_TYPE where AVR_PROTOCOL_NAME = '{0}'", protocolName);
                using (OleDbDataReader dr = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteReader(sql))
                {
                    string nodeProtocolName = string.Empty;
                    if (dr.HasRows)
                    {   
                        while (dr.Read())
                        {
                          
                            //加载子项描述
                            _Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode(dr["DESCRIPTION"].ToString(), 1, 2));
                        }

                        for (int i = 0; i < _Node.Nodes.Count; i++)
                        {
                       
                             string AVR_IDENT_TYPE= GetAVR_IDENT_TYPE(_Node.Nodes[i].Text, protocolName);
                            string AVR_PROTOCOL_NO =GetAVR_PROTOCOL_NO (protocolName);
                            if (AVR_PROTOCOL_NO=="" || AVR_IDENT_TYPE=="") return ;

                            //加载子项对应的记录
                            List<CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo> prolist = GetAVR_ITEM_NAMEList(int.Parse(AVR_IDENT_TYPE), int.Parse(AVR_PROTOCOL_NO));
                            for (int j = 0; j < prolist.Count; j++)
                            {
                                _Node.Nodes[i].Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode(prolist[j].AVR_ITEM_NAME, 1, 3));
                            }
                        }

                        TV_Protocol.Nodes.Add(_Node);
                    }
                }
                
            }
            #endregion 

            //CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("DLT645-2007");

            //string sqlString = "select * fr";
            
            //_Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("电能量", 1, 2));
            //_Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("最大需量", 1, 2));
            //_Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("变量", 1, 2));
            //_Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("事件记录", 1, 2));
            //_Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("参变量", 1, 2));
            //_Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("冻结", 1, 2));
            //_Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("负荷记录", 1, 2));
            //_Node.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("安全认证", 1, 2));

            //for (int i = 0; i < _Node.Nodes.Count; i++)
            //{
            //    List<CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo> prolist = GetAVR_ITEM_NAMEList(i + 1, 0);
            //    for (int j = 0; j < prolist.Count; j++)
            //    {
            //        _Node.Nodes[i].Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode(prolist[j].AVR_ITEM_NAME, 1, 3));
            //    }
            //}

            //TV_Protocol.Nodes.Add(_Node);

            //CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode _Node1 = new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("DLT645-1997");

            //_Node1.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("电能量", 1, 2));
            //_Node1.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("最大需量", 1, 2));
            //_Node1.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("变量", 1, 2));
            //_Node1.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("事件记录", 1, 2));
            //_Node1.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("参变量", 1, 2));
            //_Node1.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("冻结", 1, 2));
            //_Node1.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("负荷记录", 1, 2));
            //_Node1.Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode("安全认证", 1, 2));

            //for (int i = 0; i < _Node1.Nodes.Count; i++)
            //{
            //    List<CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo> prolist = GetAVR_ITEM_NAMEList(i + 1, 1);
            //    for (int j = 0; j < prolist.Count; j++)
            //    {
            //        _Node1.Nodes[i].Nodes.Add(new CLDC_MeterUI.TreeViewCheckStyel.ThreeStateTreeNode(prolist[j].AVR_ITEM_NAME, 1, 3));
            //    }
            //}

            //TV_Protocol.Nodes.Add(_Node1);
        }

        /// <summary>
        /// 根据编码类型得到编码号
        /// </summary>
        /// <param name="AVR_IDENT_TYPE"></param>
        /// <returns></returns>
        public string GetAVR_IDENT_TYPE(string AVR_IDENT_TYPE)
        {
            string code = "1";
            switch (AVR_IDENT_TYPE)
            {
                case "电能量":
                    code = "1";
                    break;
                case "最大需量":
                    code = "2";
                    break;
                case "变量":
                    code = "3";
                    break;
                case "事件记录":
                    code = "4";
                    break;
                case "参变量":
                    code = "5";
                    break;
                case "冻结":
                    code = "6";
                    break;
                case "负荷记录":
                    code = "7";
                    break;
                case "安全认证":
                    code = "8";
                    break;
            }
            return code;
        }
        /// <summary>
        /// 根据编码类型（编码类型的描述）和协议名称得到编码号
        /// </summary>
        /// <param name="AVR_IDENT_TYPE"></param>
        /// <param name="AVR_PROTOCOL_NAME"></param>
        /// <returns></returns>
        public string GetAVR_IDENT_TYPE(string DESCRIPTION, string AVR_PROTOCOL_NAME)
        {
            string GetTypr = "";
            string sql = string.Format("select * from PROTOCOL_TYPE where AVR_PROTOCOL_NAME = '{0}' and DESCRIPTION = '{1}'", AVR_PROTOCOL_NAME, DESCRIPTION);
            CLDC_DataCore.DataBase.DbHelperOleDb.connectionString = strPath;
            OleDbDataReader dr = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteReader(sql);
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo proinfo = new CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo();
                    GetTypr = dr["AVR_IDENT_TYPE"].ToString();

                }
                dr.Close();
            }
            return GetTypr;
        }  
        
        /// <summary>
        ///根据编码号得到编码类型
        /// </summary>
        /// <param name="AVR_IDENT_TYPE_Code"></param>
        /// <returns></returns>
        public string Ret_AVR_IDENT_TYPE(string AVR_IDENT_TYPE_Code)
        {
            string code = "电能量";
            switch (AVR_IDENT_TYPE_Code)
            {
                case "1":
                    code = "电能量";
                    break;
                case "2":
                    code = "最大需量";
                    break;
                case "3":
                    code = "变量";
                    break;
                case "4":
                    code = "事件记录";
                    break;
                case "5":
                    code = "参变量";
                    break;
                case "6":
                    code = "冻结";
                    break;
                case "7":
                    code = "负荷记录";
                    break;
                case "8":
                    code = "安全认证";
                    break;
            }
            return code;
        }
    /// <summary>
        /// 根据编码号和协议名称得到编码类型
    /// </summary>
    /// <param name="AVR_IDENT_TYPE_Code"></param>
    /// <param name="AVR_PROTOCOL_NAME"></param>
    /// <returns></returns>
        public string Ret_AVR_IDENT_TYPE(string AVR_IDENT_TYPE_Code, string AVR_PROTOCOL_NAME)
        {
            string GetTypr="";
            string sql = string.Format("select * from PROTOCOL_TYPE where AVR_PROTOCOL_NAME = '{0}' and AVR_IDENT_TYPE = '{1}'", AVR_PROTOCOL_NAME, AVR_IDENT_TYPE_Code);
            CLDC_DataCore.DataBase.DbHelperOleDb.connectionString = strPath;
           object  obj = CLDC_DataCore.DataBase.DbHelperOleDb.GetSingle (sql);
           if (obj != null)
               GetTypr = obj.ToString();
            return GetTypr;
        }
        /// <summary>
        /// 根据规约名称得到规约号
        /// </summary>
        /// <param name="AVR_PROTOCOL_NO"></param>
        /// <returns></returns>
        public string GetAVR_PROTOCOL_NO(string AVR_PROTOCOL_NO)
        {
            string ReAVR_PROTOCOL_NO ="";
            string sql = string.Format("select AVR_PROTOCOL_NO from PROTOCOL_INFO where AVR_PROTOCOL_NAME = '{0}'", AVR_PROTOCOL_NO);
            CLDC_DataCore.DataBase.DbHelperOleDb.connectionString = strPath;
            object obj = CLDC_DataCore.DataBase.DbHelperOleDb.GetSingle (sql );
            if (obj != null)
                ReAVR_PROTOCOL_NO =obj .ToString ();    
            //if (AVR_PROTOCOL_NO == "DLT645-2007")
            //{
            //    ReAVR_PROTOCOL_NO = "0";
            //}
            //if (AVR_PROTOCOL_NO == "DLT645-1997")
            //{
            //    ReAVR_PROTOCOL_NO = "1";
            //}
            return ReAVR_PROTOCOL_NO;
        }

        /// <summary>
        /// 根据权限号获取权限值
        /// </summary>
        /// <param name="CLASS_Code"></param>
        /// <returns></returns>
        public string GetCLASSByCode(string CLASS_Code)
        {
            string Code = "普通";
            if (CLASS_Code == "0")
            {
                Code = "禁止写";
            }
            else if (CLASS_Code == "1")
            {
                Code = "普通";
            }
            else if (CLASS_Code == "2")
            {
                Code = "特殊";
            }
            return Code;
        }

        /// <summary>
        /// 根据权限值获取权限号
        /// </summary>
        /// <param name="CLASS"></param>
        /// <returns></returns>
        public string GetCodeByCLASS(string CLASS)
        {
            string Code = "1";
            if (CLASS == "禁止写")
            {
                Code = "0";
            }
            else if (CLASS == "普通")
            {
                Code = "1";
            }
            else if (CLASS == "特殊")
            {
                Code = "2";
            }
            return Code;
        }

        /// <summary>
        /// 根据操作号获取操作值
        /// </summary>
        /// <param name="Type_Code"></param>
        /// <returns></returns>
        public string GetTYPEByCode(string Type_Code)
        {
            string Code = "只读";
            if (Type_Code == "0")
            {
                Code = "只读";
            }
            else if (Type_Code == "1")
            {
                Code = "只写";
            }
            else if (Type_Code == "2")
            {
                Code = "读写";
            }
            return Code;
        }

        /// <summary>
        /// 根据操作值获取操作号
        /// </summary>
        /// <param name="Type_Code"></param>
        /// <returns></returns>
        public string GetCodeByTYPE(string Type_Code)
        {
            string Code = "0";
            if (Type_Code == "只读")
            {
                Code = "0";
            }
            else if (Type_Code == "只写")
            {
                Code = "1";
            }
            else if (Type_Code == "读写")
            {
                Code = "2";
            }
            return Code;
        }

        /// <summary>
        /// 获取项名称集合
        /// </summary>
        /// <param name="AVR_IDENT_TYPE"></param>
        /// <param name="AVR_PROTOCOL_NO"></param>
        /// <returns></returns>
        public List<CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo> GetAVR_ITEM_NAMEList(int AVR_IDENT_TYPE, int AVR_PROTOCOL_NO)
        {
            try
            {
                CLDC_DataCore.DataBase.DbHelperOleDb.connectionString = strPath;

                List<CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo> list = new List<CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo>();

                string sql = "select AVR_ITEM_NAME from PROTOCOL_DLT645DICT where AVR_PROTOCOL_NO = '" + AVR_PROTOCOL_NO + "' and AVR_IDENT_TYPE = '" + AVR_IDENT_TYPE + "'";
                OleDbDataReader dr = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteReader(sql);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo proinfo = new CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo();
                        proinfo.AVR_ITEM_NAME = dr["AVR_ITEM_NAME"].ToString();
                        list.Add(proinfo);
                    }
                    dr.Close();
                }
                return list;
            }
            catch { return null; }
        }

        /// <summary>
        /// 获取规约信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo> GetPROTOCOLinfoList(string sql)
        {
            try
            {
                CLDC_DataCore.DataBase.DbHelperOleDb.connectionString = strPath;
                List<CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo> list = new List<CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo>();
                OleDbDataReader dr = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteReader(sql);

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo proinfo = new CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo();
                        proinfo.PK_LNG_DLT_ID = int.Parse(dr["PK_LNG_DLT_ID"].ToString());
                        proinfo.AVR_PROTOCOL_NO = dr["AVR_PROTOCOL_NO"].ToString();
                        proinfo.AVR_IDENT_TYPE = dr["AVR_IDENT_TYPE"].ToString();
                        proinfo.AVR_ITEM_NAME = dr["AVR_ITEM_NAME"].ToString();
                        proinfo.AVR_ID = dr["AVR_ID"].ToString();
                        proinfo.CHR_CLASS = dr["CHR_CLASS"].ToString();
                        proinfo.LNG_LENGTH = dr["LNG_LENGTH"].ToString();
                        proinfo.LNG_DOT = dr["LNG_DOT"].ToString();
                        proinfo.CHR_TYPE = dr["CHR_TYPE"].ToString();
                        proinfo.AVR_FORMAT = dr["AVR_FORMAT"].ToString();
                        proinfo.AVR_DEF_VALUE = dr["AVR_DEF_VALUE"].ToString();
                        list.Add(proinfo);
                    }
                    dr.Close();
                }
                return list;
            }
            catch { return null; }
        }

        private void TV_Protocol_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (TV_Protocol.SelectedNode.Level == 2)
            {
                Node_Level = TV_Protocol.SelectedNode.Level;
                Index_tree = TV_Protocol.SelectedNode.Index;
                parentIndex_next = TV_Protocol.SelectedNode.Parent.Index;
                parentName = TV_Protocol.SelectedNode.Parent.Parent.Text;
                parent_nextName = TV_Protocol.SelectedNode.Parent.Text;
                
            }
            else if (TV_Protocol.SelectedNode.Level == 1)
            {
                Node_Level = TV_Protocol.SelectedNode.Level;
                parentIndex_next = TV_Protocol.SelectedNode.Index;
                parentName = TV_Protocol.SelectedNode.Parent.Text;
                parent_nextName = TV_Protocol.SelectedNode.Text;
            }

            if (e.Action == TreeViewAction.Unknown)
            {
                return;
            }
            if (e.Node.Level == 0)
            {
                btn_Add.Enabled = false;
                btn_delete.Enabled = false;
                btn_Save.Enabled = false;
                btn_Save.Enabled = false;
                return;
            }
           
            btn_Add.Enabled = true;
            btn_delete.Enabled = true;
            btn_Save.Enabled = true;
            btn_Save.Enabled = true;

            DGV_source();
        }

        /// <summary>
        /// 绑定刷新Datagrid
        /// </summary>
        void DGV_source()
        {
            cmb_text.Visible = false;
            Dgv_Data.AutoGenerateColumns = false;
            Dgv_Data.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            //Dgv_Data.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            Dgv_Data.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            Dgv_Data.GridColor = SystemColors.ActiveBorder;
            Dgv_Data.RowHeadersVisible = false;

            string sql = "";
            if (Node_Level == 1)
            {
                string AVR_PROTOCOL_NO = GetAVR_PROTOCOL_NO(parentName);
                string AVR_IDENT_TYPE = GetAVR_IDENT_TYPE(parent_nextName);
                sql = "select * from PROTOCOL_DLT645DICT where AVR_PROTOCOL_NO = '" + AVR_PROTOCOL_NO + "' and AVR_IDENT_TYPE = '" + AVR_IDENT_TYPE + "'";
            }
            else if (Node_Level == 2)
            {
                if (TV_Protocol.SelectedNode == null) return;
                
                string AVR_PROTOCOL_NO = GetAVR_PROTOCOL_NO(parentName);
                string AVR_IDENT_TYPE = GetAVR_IDENT_TYPE(parent_nextName);
                string AVR_ITEM_NAME = TV_Protocol.SelectedNode.Text;
                sql = "select * from PROTOCOL_DLT645DICT where AVR_PROTOCOL_NO = '" + AVR_PROTOCOL_NO + "' and AVR_IDENT_TYPE = '" + AVR_IDENT_TYPE + "' and AVR_ITEM_NAME ='" + AVR_ITEM_NAME + "'";
            }

            List<CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo> list = GetPROTOCOLinfoList(sql);
            Dgv_Data.Rows.Clear();
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Dgv_Data.Rows.Add();
                }
            }
            else
            {
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Dgv_Data.Rows[i].Cells["编号"].Value = list[i].PK_LNG_DLT_ID;
                Dgv_Data.Rows[i].Cells["规约名称"].Value = GETAVR_PROTOCOLByAVR_PROTOCOL_NO(list[i].AVR_PROTOCOL_NO);
                Dgv_Data.Rows[i].Cells["编码类型"].Value = Ret_AVR_IDENT_TYPE(list[i].AVR_IDENT_TYPE);
                Dgv_Data.Rows[i].Cells["项名称"].Value = list[i].AVR_ITEM_NAME;
                Dgv_Data.Rows[i].Cells["数据标识"].Value = list[i].AVR_ID;
                Dgv_Data.Rows[i].Cells["权限"].Value = GetCLASSByCode(list[i].CHR_CLASS);
                Dgv_Data.Rows[i].Cells["长度"].Value = list[i].LNG_LENGTH;
                Dgv_Data.Rows[i].Cells["小数位"].Value = list[i].LNG_DOT;
                Dgv_Data.Rows[i].Cells["操作方式"].Value = GetTYPEByCode(list[i].CHR_TYPE);
                Dgv_Data.Rows[i].Cells["格式串"].Value = list[i].AVR_FORMAT;
                Dgv_Data.Rows[i].Cells["默认值"].Value = list[i].AVR_DEF_VALUE;
            }
        }

        /// <summary>
        /// 获取规约名称
        /// </summary>
        /// <param name="No"></param>
        /// <returns></returns>
        string GETAVR_PROTOCOLByAVR_PROTOCOL_NO(string No)
        {
            if (No == "0")
            {
                return "DLT645-2007";
            }
            if (No == "1")
            {
                return "DLT645-1997";
            }
            return "";
        }

        
        private void Dgv_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            cmb_text.Visible = false;
            if (e.ColumnIndex == 1)
            {
                cmb_text.Items.Clear();
                string sql = "select AVR_PROTOCOL_NAME from PROTOCOL_INFO";
                //读取标准名
                using (OleDbDataReader dr = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteReader(sql))
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cmb_text.Items.Add(dr["AVR_PROTOCOL_NAME"].ToString());
                        }
                    }
                }
                DrCOMBOX(e);
            }
            else if (e.ColumnIndex == 2)
            {
                object obj = Dgv_Data.Rows[e.RowIndex].Cells["规约名称"].Value;
                cmb_text.Items.Clear();
                if (obj!= null)
                {
                    string sql = string.Format("select DESCRIPTION from PROTOCOL_TYPE where AVR_PROTOCOL_NAME='{0}'", obj.ToString());
                    //读取标准名
                    using (OleDbDataReader dr = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteReader(sql))
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                cmb_text.Items.Add(dr["DESCRIPTION"].ToString());
                            }
                        }
                    }
                }
                //else
                //{
                //    cmb_text.Items.Add("电能量");
                //    cmb_text.Items.Add("最大需量");
                //    cmb_text.Items.Add("变量");
                //    cmb_text.Items.Add("事件记录");
                //    cmb_text.Items.Add("参变量");
                //    cmb_text.Items.Add("冻结");
                //    cmb_text.Items.Add("负荷记录");
                //    cmb_text.Items.Add("参变量");
                //    cmb_text.Items.Add("安全认证");
                //}
                DrCOMBOX(e);
            }
            else if (e.ColumnIndex == 5)
            {
                cmb_text.Items.Clear();
                cmb_text.Items.Add("禁止写");
                cmb_text.Items.Add("普通");
                cmb_text.Items.Add("特殊");
                DrCOMBOX(e);
            }
            else if (e.ColumnIndex == 8)
            {
                cmb_text.Items.Clear();
                cmb_text.Items.Add("只读");
                cmb_text.Items.Add("只写");
                cmb_text.Items.Add("读写");
                DrCOMBOX(e);
            }
            rowindex = e.RowIndex;
            colindex = e.ColumnIndex;
        }

        /// <summary>
        /// 绘制下拉框位置
        /// </summary>
        /// <param name="e"></param>
        private void DrCOMBOX(DataGridViewCellEventArgs e)
        {
            Rectangle rt = Dgv_Data.GetCellDisplayRectangle(e.ColumnIndex,e.RowIndex,false);
            cmb_text.Visible = true;
            cmb_text.Left = rt.Left - 1;// 261;
            cmb_text.Height = rt.Height;//18;
            cmb_text.Width = Dgv_Data.Columns[e.ColumnIndex].Width;
            cmb_text.Top = rt.Top - 2;
        }

        private void cmb_text_SelectedIndexChanged(object sender, EventArgs e)
        {
                Dgv_Data.Rows[rowindex].Cells[colindex].Value = cmb_text.Text;
                cmb_text.Visible = false;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Dgv_Data.Rows.Add();
            Dgv_Data.CurrentCell = Dgv_Data.Rows[Dgv_Data.Rows.Count - 1].Cells[1];
            
        }

        /// <summary>
        /// 控制下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_Data_Scroll(object sender, ScrollEventArgs e)
        {
            //Rectangle rt = Dgv_Data.GetCellDisplayRectangle(colindex,rowindex,false);
            //if (rt.Top <= 0)
            //{
            //    cmb_text.Visible = false;
            //}
            //else
            //{
            //    if (colindex == 1 || colindex == 2 || colindex == 5 || colindex == 8)
            //    {
            //        cmb_text.Visible = true;
            //        cmb_text.Left = rt.Left;
            //        cmb_text.Top = rt.Top;
            //    }
            //}
            cmb_text.Visible = false;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                for (int j = 1; j < 8; j++)
                {
                    if (Dgv_Data.Rows[i].Cells[j].Value == null)
                    {

                        MessageBox.Show(this, "请填满数据后再保存", "提示");
                        return;
                    }
                }
            }
            

            ArrayList strList = new ArrayList();
             
            for (int i = 0; i < Dgv_Data.Rows.Count; i++)
            {
                string sql = "";
                CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo protocolinfo = new CLDC_DataCore.Model.PROTOCOLModel.PROTOCOLinfo();
                protocolinfo.AVR_PROTOCOL_NO = GetAVR_PROTOCOL_NO(Dgv_Data.Rows[i].Cells[1].Value.ToString());
                protocolinfo.AVR_IDENT_TYPE = GetAVR_IDENT_TYPE(Dgv_Data.Rows[i].Cells[2].Value.ToString());
                protocolinfo.AVR_ITEM_NAME = Dgv_Data.Rows[i].Cells[3].Value.ToString();
                protocolinfo.AVR_ID = Dgv_Data.Rows[i].Cells[4].Value.ToString();
                protocolinfo.CHR_CLASS = GetCodeByTYPE(Dgv_Data.Rows[i].Cells[5].Value.ToString());
                protocolinfo.LNG_LENGTH = Dgv_Data.Rows[i].Cells[6].Value.ToString();
                protocolinfo.LNG_DOT = Dgv_Data.Rows[i].Cells[7].Value.ToString();
                protocolinfo.CHR_TYPE = GetCodeByTYPE(Dgv_Data.Rows[i].Cells[8].Value.ToString());
                protocolinfo.AVR_FORMAT = Dgv_Data.Rows[i].Cells[9].Value == null ? "" : Dgv_Data.Rows[i].Cells[9].Value.ToString();
                protocolinfo.AVR_DEF_VALUE = Dgv_Data.Rows[i].Cells[10].Value == null ? "" : Dgv_Data.Rows[i].Cells[10].Value.ToString();

                if (Dgv_Data.Rows[i].Cells[0].Value == null)
                {
                    protocolinfo.PK_LNG_DLT_ID = int.Parse(System.DateTime.Now.ToString("hhmmssff")) + i;
                    sql = string.Format(@"insert into PROTOCOL_DLT645DICT(PK_LNG_DLT_ID,AVR_PROTOCOL_NO,AVR_IDENT_TYPE,AVR_ITEM_NAME,"+
                                        "AVR_ID,CHR_CLASS,LNG_LENGTH,LNG_DOT,CHR_TYPE,AVR_FORMAT,AVR_DEF_VALUE) values ("+
                                         "{0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')",+
                                         protocolinfo.PK_LNG_DLT_ID,
                                         protocolinfo.AVR_PROTOCOL_NO,
                                         protocolinfo.AVR_IDENT_TYPE,
                                         protocolinfo.AVR_ITEM_NAME,
                                         protocolinfo.AVR_ID,
                                         protocolinfo.CHR_CLASS,
                                         protocolinfo.LNG_LENGTH,
                                         protocolinfo.LNG_DOT,
                                         protocolinfo.CHR_TYPE,
                                         protocolinfo.AVR_FORMAT,
                                         protocolinfo.AVR_DEF_VALUE,
                                         protocolinfo.PK_LNG_DLT_ID);

                }
                else
                {
                    protocolinfo.PK_LNG_DLT_ID = int.Parse(Dgv_Data.Rows[i].Cells[0].Value.ToString());
                    sql = string.Format(@"update PROTOCOL_DLT645DICT set AVR_PROTOCOL_NO ='{0}',AVR_IDENT_TYPE='{1}',"+
                                          "AVR_ITEM_NAME='{2}',AVR_ID='{3}',CHR_CLASS='{4}',LNG_LENGTH='{5}',"+
                                          "LNG_DOT='{6}',CHR_TYPE='{7}',AVR_FORMAT='{8}',AVR_DEF_VALUE='{9}' where PK_LNG_DLT_ID={10}",
                                          protocolinfo.AVR_PROTOCOL_NO,
                                          protocolinfo.AVR_IDENT_TYPE,
                                          protocolinfo.AVR_ITEM_NAME,
                                          protocolinfo.AVR_ID,
                                          protocolinfo.CHR_CLASS,
                                          protocolinfo.LNG_LENGTH,
                                          protocolinfo.LNG_DOT,
                                          protocolinfo.CHR_TYPE,
                                          protocolinfo.AVR_FORMAT,
                                          protocolinfo.AVR_DEF_VALUE,
                                          protocolinfo.PK_LNG_DLT_ID);
                }
                strList.Add(sql);
            }
            bool blnSuccess = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteSqlTran(strList);
            if (blnSuccess)
            {
                MessageBox.Show(this, "保存成功", "提示");
                DGV_source();
                show_TV_Protocol();
            }
            else
            {
                MessageBox.Show(this, "保存失败", "提示");
            }
        }

        /// <summary>
        /// 刷新树形
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="parent"></param>
        void show_TV_Protocol()
        {
            int parentIndex = int.Parse(GetAVR_PROTOCOL_NO(parentName));

            TV_Protocol.Nodes.Clear();
            Load_TV();
            if (Index_tree != -1)
            {
                TV_Protocol.Nodes[parentIndex].Expand();
                TV_Protocol.Nodes[parentIndex].Nodes[parentIndex_next].Expand();
            }
            else if (parentIndex_next != -1)
            {
                TV_Protocol.Nodes[parentIndex].Expand();
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除选中的数据？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            string sql = "";
            string id = Dgv_Data.CurrentRow.Cells[0].Value == null ? "" : Dgv_Data.CurrentRow.Cells[0].Value.ToString();
            if (id != "")
            {
                sql = "delete from PROTOCOL_DLT645DICT where PK_LNG_DLT_ID = " + int.Parse(id);
                bool blnSuccess = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteSql(sql) > 0;
                if (blnSuccess)
                {
                    MessageBox.Show(this, "删除成功", "提示");
                    DGV_source();
                    show_TV_Protocol();
                }
                else
                {
                    MessageBox.Show(this, "删除失败", "提示");
                }
            }
            else
            {
                Dgv_Data.Rows.Remove(Dgv_Data.CurrentRow);
            }
        }

    }
}

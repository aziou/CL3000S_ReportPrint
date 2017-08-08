using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CLDC_DataCore.DataBase;
using System.Xml;

namespace CLDC_MeterUI.UI_Detection_New
{
    ///<summary>
    ///FileName:FrameYouSee.cs
    ///CLRVersion:2.0.50727.5472
    ///Author:kaury
    ///Corporation:
    ///Description:
    ///DateTime:12/20/2013 3:28:32 PM
    ///</summary>
    public partial class FrameYouSee : Office2007Form
    {
        
        private static FrameYouSee instance;
        private static object syncRoot = new Object();

        string Strsql = "";
        OleDbDataReader dr = null;

        static string _ErrorString = "";
        XmlNode _XmlNode = null;

        string strPath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Environment.CurrentDirectory + @"\DATABASE\ClouLog.mdb";
       
        public static FrameYouSee Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new FrameYouSee();
                    }
                }
                return instance;
            }
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

        public FrameYouSee()
        {
            InitializeComponent();
        }
        private void FrameYouSee_Load(object sender, EventArgs e)
        {
            cmb_Type.Items.Clear();
            cmbTiaojian.Items.Clear();
            cmbTiaojian.Enabled = false;

            Grid_seebaowen.AutoGenerateColumns =false;
            Grid_seebaowen.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            Grid_seebaowen.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            Grid_seebaowen.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            Grid_seebaowen.GridColor = SystemColors.ActiveBorder;
            tableLayoutPanel1.Top = 5;
            

            txt_time1.Text = "例如：2008-08-08";
            txt_time2.Text = "例如：2008-09-08";

            cmb_Type.Items.Add("全部");
            cmb_Type.Items.Add("控制");
            cmb_Type.Items.Add("端口号");
            cmb_Type.Items.Add("排除端口");

            CLDC_DataCore.DataBase.DbHelperOleDb.connectionString = strPath;

            Strsql = "select top 25 * from FrameLog order by chrSTime desc";
            dr = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteReader(Strsql);
            List<CLDC_DataCore.Model.LogModel.LogFrameInfo> BaowenList = new List<CLDC_DataCore.Model.LogModel.LogFrameInfo>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CLDC_DataCore.Model.LogModel.LogFrameInfo see = new CLDC_DataCore.Model.LogModel.LogFrameInfo();
                    see.chrPortNo = dr["chrPortNo"].ToString();
                    see.chrEquipName = dr["chrEquipName"].ToString();
                    see.chrItemName = dr["chrItemName"].ToString();
                    see.chrMessage = dr["chrMessage"].ToString();
                    see.chrSFrame = dr["chrSFrame"].ToString();
                    see.chrSMeaning = dr["chrSMeaning"].ToString();
                    see.chrSTime = dr["chrSTime"].ToString();
                    see.chrRFrame = dr["chrRFrame"].ToString();
                    see.chrRMeaning = dr["chrRMeaning"].ToString();
                    see.chrRTime = dr["chrRTime"].ToString();
                    see.chrOther = dr["chrOther"].ToString();
                    BaowenList.Add(see);
                }
                dr.Close();
            }

            Grid_seebaowen.DataSource = BaowenList;

            string colName = "";
            _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_SHOW_BWCOL, out _ErrorString);
            if (_XmlNode == null || _ErrorString != "")
            {
                _XmlNode = clsXmlControl.CreateXmlNode("ColsConfig");
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "端口号", "Name", "端口号", "ShowType", "1"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "设备名称", "Name", "设备名称", "ShowType", "1"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "项目名称", "Name", "项目名称", "ShowType", "1"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "操作消息", "Name", "操作消息", "ShowType", "1"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "发帧字符串", "Name", "发帧字符串", "ShowType", "1"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "发帧解析", "Name", "发帧解析", "ShowType", "1"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "发帧时间", "Name", "发帧时间", "ShowType", "1"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "收帧字符串", "Name", "收帧字符串", "ShowType", "1"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "收帧解析", "Name", "收帧解析", "ShowType", "1"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "收帧时间", "Name", "收帧时间", "ShowType", "1"));
                _XmlNode.AppendChild(clsXmlControl.CreateXmlNode("R", "ID", "备用", "Name", "备用", "ShowType", "1"));

                clsXmlControl.SaveXml(_XmlNode, Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_SHOW_BWCOL);
            }
            else
            {
                for (int _j = 0; _j < _XmlNode.ChildNodes.Count; _j++)
                {
                    if (_XmlNode.ChildNodes[_j].Attributes[2].Value != "1")
                    {
                        colName = _XmlNode.ChildNodes[_j].Attributes[1].Value;
                        Grid_seebaowen.Columns[colName].Visible = false;
                    }
                }
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string Strsql = "";
            if (txt_time1.Text.Trim() == "例如：2008-08-08")
            {
                txt_time1.Text = "";
            }
            if (txt_time2.Text.Trim() == "例如：2008-09-08")
            {
                txt_time2.Text = "";
            }

            if (cmb_Type.Text.Trim() == "全部")
            {
                if (txt_time1.Text.Trim() != "")
                {
                    if (txt_No.Text.Trim() != "" && txt_time2.Text.Trim() == "")
                    {
                        Strsql = "select top " + txt_No.Text.Trim() + " * from FrameLog where chrSTime like '%" + txt_time1.Text.Trim() + "%' order by chrSTime desc";
                    }
                    else if (txt_time2.Text.Trim() != "" && txt_No.Text.Trim() != "")
                    {
                        Strsql = "select top " + txt_No.Text.Trim() + " * from FrameLog where chrSTime >= '" + txt_time1.Text.Trim() + "' and chrSTime <= '" + txt_time2.Text.Trim() + "' order by chrSTime desc";
                    }
                    else if (txt_No.Text.Trim() == "" && txt_time2.Text.Trim() != "")
                    {
                        Strsql = "select  * from FrameLog where chrSTime >= '" + txt_time1.Text.Trim() + "' and chrSTime <= '" + txt_time2.Text.Trim() + "' order by chrSTime desc";
                    }
                    else
                    {
                        Strsql = "select * from FrameLog where chrSTime like '%" + txt_time1.Text.Trim() + "%' order by chrSTime desc";
                    }
                }
                else if (txt_time1.Text.Trim() == "" && txt_No.Text.Trim() != "")
                {
                    Strsql = "select top " + txt_No.Text.Trim() + " * from FrameLog order by chrSTime desc ";
                }
                else
                {
                    Strsql = " select * from FrameLog order by chrSTime desc";
                }
            }
            else if (cmb_Type.Text.Trim() == "排除端口")
            {
                if (txt_time1.Text.Trim() != "")
                {
                    if (txt_No.Text.Trim() != "" && txt_time2.Text.Trim() == "")
                    {
                        Strsql = "select top " + txt_No.Text.Trim() + " * from FrameLog where chrPortNo <> '" + cmbTiaojian.Text.Trim() + "' and chrSTime like '%" + txt_time1.Text.Trim() + "%' order by chrSTime desc";
                    }
                    else if (txt_time2.Text.Trim() != "" && txt_No.Text.Trim() != "")
                    {
                        Strsql = "select top " + txt_No.Text.Trim() + " * from FrameLog where chrPortNo <>'" + cmbTiaojian.Text.Trim() + "' and chrSTime >= '" + txt_time1.Text.Trim() + "' and chrSTime <= '" + txt_time2.Text.Trim() + "' order by chrSTime desc";
                    }
                    else if (txt_No.Text.Trim() == "" && txt_time2.Text.Trim() != "")
                    {
                        Strsql = "select  * from FrameLog where chrPortNo <> '" + cmbTiaojian.Text.Trim() + "' and chrSTime >= '" + txt_time1.Text.Trim() + "' and chrSTime <= '" + txt_time2.Text.Trim() + "' order by chrSTime desc";
                    }
                    else
                    {
                        Strsql = "select * from FrameLog where chrPortNo <> '" + cmbTiaojian.Text.Trim() + "' and chrSTime like '%" + txt_time1.Text.Trim() + "%' order by chrSTime desc";
                    }
                }
                else if (txt_time1.Text.Trim() == "" && txt_No.Text.Trim() != "")
                {
                    Strsql = "select top " + txt_No.Text.Trim() + " * from FrameLog where chrPortNo <> '" + cmbTiaojian.Text.Trim() + "' order by chrSTime desc ";
                }
                else
                {
                    Strsql = " select * from FrameLog where chrPortNo <> '" + cmbTiaojian.Text.Trim() + "' order by chrSTime desc";
                }
            }
            else if (cmb_Type.Text.Trim() == "端口号")
            {
                if (txt_time1.Text.Trim() != "")
                {
                    if (txt_No.Text.Trim() != "" && txt_time2.Text.Trim() == "")
                    {
                        Strsql = "select top " + txt_No.Text.Trim() + " * from FrameLog where chrPortNo = '" + cmbTiaojian.Text.Trim() + "' and chrSTime like '%" + txt_time1.Text.Trim() + "%' order by chrSTime desc";
                    }
                    else if (txt_time2.Text.Trim() != "" && txt_No.Text.Trim() != "")
                    {
                        Strsql = "select top " + txt_No.Text.Trim() + " * from FrameLog where chrPortNo = '" + cmbTiaojian.Text.Trim() + "' and chrSTime >= '" + txt_time1.Text.Trim() + "' and chrSTime <= '" + txt_time2.Text.Trim() + "' order by chrSTime desc";
                    }
                    else if (txt_No.Text.Trim() == "" && txt_time2.Text.Trim() != "")
                    {
                        Strsql = "select  * from FrameLog where chrPortNo = '" + cmbTiaojian.Text.Trim() + "' and chrSTime >= '" + txt_time1.Text.Trim() + "' and chrSTime <= '" + txt_time2.Text.Trim() + "' order by chrSTime desc";
                    }
                    else
                    {
                        Strsql = "select * from FrameLog where chrPortNo = '" + cmbTiaojian.Text.Trim() + "' and chrSTime like '%" + txt_time1.Text.Trim() + "%' order by chrSTime desc";
                    }
                }
                else if (txt_time1.Text.Trim() == "" && txt_No.Text.Trim() != "")
                {
                    Strsql = "select top " + txt_No.Text.Trim() + " * from FrameLog where chrPortNo = '" + cmbTiaojian.Text.Trim() + "' order by chrSTime desc ";
                }
                else
                {
                    Strsql = " select * from FrameLog where chrPortNo = '" + cmbTiaojian.Text.Trim() + "' order by chrSTime desc";
                }
            }
            else if (cmb_Type.Text.Trim() == "控制")
            {
                if (txt_time1.Text.Trim() != "")
                {
                    if (txt_No.Text.Trim() != "" && txt_time2.Text.Trim() == "")
                    {
                        Strsql = "select top " + txt_No.Text.Trim() + " * from FrameLog where chrItemName = '" + cmbTiaojian.Text.Trim() + "' and chrSTime like '%" + txt_time1.Text.Trim() + "%' order by chrSTime desc";
                    }
                    else if (txt_time2.Text.Trim() != "" && txt_No.Text.Trim() != "")
                    {
                        Strsql = "select top " + txt_No.Text.Trim() + " * from FrameLog where chrItemName = '" + cmbTiaojian.Text.Trim() + "' and chrSTime >= '" + txt_time1.Text.Trim() + "' and chrSTime <= '" + txt_time2.Text.Trim() + "' order by chrSTime desc";
                    }
                    else if (txt_No.Text.Trim() == "" && txt_time2.Text.Trim() != "")
                    {
                        Strsql = "select  * from FrameLog where chrItemName = '" + cmbTiaojian.Text.Trim() + "' and chrSTime >= '" + txt_time1.Text.Trim() + "' and chrSTime <= '" + txt_time2.Text.Trim() + "' order by chrSTime desc";
                    }
                    else
                    {
                        Strsql = "select * from FrameLog where chrItemName = '" + cmbTiaojian.Text.Trim() + "' and chrSTime like '%" + txt_time1.Text.Trim() + "%' order by chrSTime desc";
                    }
                }
                else if (txt_time1.Text.Trim() == "" && txt_No.Text.Trim() != "")
                {
                    Strsql = "select top " + txt_No.Text.Trim() + " * from FrameLog where chrItemName = '" + cmbTiaojian.Text.Trim() + "' order by chrSTime desc ";
                }
                else
                {
                    Strsql = " select * from FrameLog where chrItemName = '" + cmbTiaojian.Text.Trim() + "' order by chrSTime desc";
                }
            }
            else
            {
                Strsql = "select top 25 * from FrameLog order by chrSTime desc";
            }

            OleDbDataReader dr = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteReader(Strsql);
            List<CLDC_DataCore.Model.LogModel.LogFrameInfo> BaowenList = new List<CLDC_DataCore.Model.LogModel.LogFrameInfo>();
            
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    CLDC_DataCore.Model.LogModel.LogFrameInfo see = new CLDC_DataCore.Model.LogModel.LogFrameInfo();
                    see.chrPortNo = dr["chrPortNo"].ToString();
                    see.chrEquipName = dr["chrEquipName"].ToString();
                    see.chrItemName = dr["chrItemName"].ToString();
                    see.chrMessage = dr["chrMessage"].ToString();
                    see.chrSFrame = dr["chrSFrame"].ToString();
                    see.chrSMeaning = dr["chrSMeaning"].ToString();
                    see.chrSTime = dr["chrSTime"].ToString();
                    see.chrRFrame = dr["chrRFrame"].ToString();
                    see.chrRMeaning = dr["chrRMeaning"].ToString();
                    see.chrRTime = dr["chrRTime"].ToString();
                    see.chrOther = dr["chrOther"].ToString();
                    BaowenList.Add(see);
                }
                dr.Close();
            }

            Grid_seebaowen.DataSource = BaowenList;
        }

        private void cmbTiaojian_Click(object sender, EventArgs e)
        {
            string cmbTiaojian_Value1 = "";
            for (int i = 0; i < cmbTiaojian.Items.Count - 1; i++)
            {
                cmbTiaojian_Value1 = cmbTiaojian.Items[i].ToString();

                for (int j = i + 1; j < cmbTiaojian.Items.Count; j++)
                {
                    string cmbTiaojian_Value2 = cmbTiaojian.Items[j].ToString();
                    if (cmbTiaojian_Value1 == cmbTiaojian_Value2)
                    {
                        cmbTiaojian.Items.RemoveAt(j);
                        j--;
                    }
                }
            }
        }

        private void cmb_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            cmbTiaojian.Enabled = true;
            cmbTiaojian.Items.Clear();
            string Strsql = "";

            if (cmb_Type.Text == "全部")
            {
                cmbTiaojian.Items.Add("发送时间");
                cmbTiaojian.Text = cmbTiaojian.Items[0].ToString();
            }
            else if (cmb_Type.Text == "端口号" || cmb_Type.Text == "排除端口")
            {
                Strsql = "select distinct chrPortNo from FrameLog";
                dr = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteReader(Strsql);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbTiaojian.Items.Add(dr["chrPortNo"].ToString());
                    }
                    cmbTiaojian.Text = cmbTiaojian.Items[0].ToString();
                }
            }
            else if (cmb_Type.Text == "控制")
            {
                Strsql = "select distinct chrItemName from FrameLog";
                dr = CLDC_DataCore.DataBase.DbHelperOleDb.ExecuteReader(Strsql);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cmbTiaojian.Items.Add(dr["chrItemName"].ToString());
                    }
                    cmbTiaojian.Text = cmbTiaojian.Items[0].ToString();
                }
            }

        
        }

        private void txt_time1_Click(object sender, EventArgs e)
        {
            if (txt_time1.Text == "例如：2008-08-08")
            txt_time1.Text="";
        }

        private void txt_time2_Click(object sender, EventArgs e)
        {
            if (txt_time2.Text == "例如：2008-09-08")
            txt_time2.Text = "";
        }

        //右键
        private void Grid_seebaowen_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex < 0)
                {
                    Add_ctMS_Header_Data();
                }
            }
        }

        void Add_ctMS_Header_Data()
        {
            
            string ColName = "";
            int ColType = 0;
            ctMS_Header.Items.Clear();
            _XmlNode = clsXmlControl.LoadXml(Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_SHOW_BWCOL, out _ErrorString);
            if (_XmlNode == null) return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                ColName = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                ColType = int.Parse(_XmlNode.ChildNodes[_i].Attributes[2].Value);
                ctMS_Header.Items.Add(ColName, imglst_IsChecked.Images[ColType], tsbX_Click);
            }

            ctMS_Header.Show(MousePosition.X, MousePosition.Y);
        }

        void tsbX_Click(object sender, EventArgs e)
        {
            string[] ColName = new string[11];
            string[] ColType = new string[11];
            if (_XmlNode == null) return;
            for (int _i = 0; _i < _XmlNode.ChildNodes.Count; _i++)
            {
                ColName[_i] = _XmlNode.ChildNodes[_i].Attributes[1].Value;
                ColType[_i] = _XmlNode.ChildNodes[_i].Attributes[2].Value;
                if (ColName[_i] == sender.ToString())
                {
                    if (ColType[_i] == "1")
                    {
                        ColType[_i] = "0";
                        Grid_seebaowen.Columns[ColName[_i]].Visible = false;
                    }
                    else if (ColType[_i] == "0" || ColType[_i] == "")
                    {
                        ColType[_i] = "1";
                        Grid_seebaowen.Columns[ColName[_i]].Visible = true;
                    }
                }
            }

            clsXmlControl _Xml = new clsXmlControl();
            _Xml.appendchild("", "ColsConfig");
            for (int i = 0; i < ColName.Length; i++)
            {
                
                _Xml.appendchild("", "R", "ID", ColName[i], "Name", ColName[i], "ShowType", ColType[i]);

            }

            _Xml.SaveXml(Application.StartupPath + CLDC_DataCore.Const.Variable.CONST_SHOW_BWCOL);
        }

        private void Grid_seebaowen_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X,
            e.RowBounds.Location.Y,
            Grid_seebaowen.RowHeadersWidth - 4,
            e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                                Grid_seebaowen.RowHeadersDefaultCellStyle.Font,
                            rectangle,
                Grid_seebaowen.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

    }
}

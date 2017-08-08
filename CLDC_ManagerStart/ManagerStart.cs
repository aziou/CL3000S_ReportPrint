using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Diagnostics;
using System.Threading;

namespace CLDC_ManagerStart
{
    
    public partial class ManagerStart : Form
    {
        public ManagerStart()
        {
            InitializeComponent();
            Dictionary<string, string> UserPass = new System.Collections.Generic.Dictionary<string, string>();

            LoadTheMan();



        }

     

        public void LoadTheMan() 
        { 
        string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
        string Sql_word_2 = ";Persist Security Info=False";
        string path = AppDomain.CurrentDomain.BaseDirectory + @"DataBase\ClouConfig.mdb";
        List<string> temp = new List<string>();
        Dictionary<string, string> temp_User = new System.Collections.Generic.Dictionary<string, string>();
        using (OleDbConnection conn = new OleDbConnection(Sql_word_1 + path + Sql_word_2))
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            string sql = "SELECT * FROM Info_User ";

            OleDbCommand cmd = new OleDbCommand(sql, conn);

            
            OleDbDataReader myReader = null;
            myReader = cmd.ExecuteReader();

            while (myReader.Read())
            {
                temp_User.Add(myReader["chrUserName"].ToString(), myReader["chrPassword"].ToString());
                temp.Add(myReader["chrQx"].ToString());
            }
        }
        
        if (temp.Count != 0)
        {
            foreach (string name in temp_User.Keys)
            {
                cmbManagerName.Items.Add(name);
            }
        }



        

        }

        #region 数据库操作

        public void SqlUpdataMessage(string sql)
        {

            string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
            string Sql_word_2 = ";Persist Security Info=False";
            string path = AppDomain.CurrentDomain.BaseDirectory + @"DataBase\ClouConfig.mdb";
            using (OleDbConnection conn = new OleDbConnection(Sql_word_1 + path + Sql_word_2))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
               

                OleDbCommand cmd = new OleDbCommand(sql, conn);

                cmd.ExecuteNonQuery();

                conn.Close();
            }

        
        }


        #endregion



        public void GettheMan(out Dictionary<string, string> dicTemp)
        {
            string Sql_word_1 = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=";
            string Sql_word_2 = ";Persist Security Info=False";
            string path = AppDomain.CurrentDomain.BaseDirectory + @"UserBase\ClouConfig.mdb";
            List<string> temp = new List<string>();
            Dictionary<string, string> temp_User = new System.Collections.Generic.Dictionary<string, string>();
            using (OleDbConnection conn = new OleDbConnection(Sql_word_1 + path + Sql_word_2))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                string sql = "SELECT * FROM Info_User ";

                OleDbCommand cmd = new OleDbCommand(sql, conn);


                OleDbDataReader myReader = null;
                myReader = cmd.ExecuteReader();

                while (myReader.Read())
                {
                    temp_User.Add(myReader["chrUserName"].ToString(), myReader["chrPassword"].ToString());
                    temp.Add(myReader["chrQx"].ToString());
                }
            }
            dicTemp = temp_User;
   




        }
        public int CheckIdAndPass(string UserName, string Password,Dictionary<string,string> TheRight)
        {
            int result = 0;
            try
            {
                int count=0;
                foreach (string name in TheRight.Keys)
                {
                    
                    if (UserName == name && Password == TheRight[UserName])
                    {
                        result = 1;
                        break;
                    }
                    else
                    {
                        result = -1;
                    }
                    count+=1;
                }
                return result;
            }
            catch (Exception e)
            {
                return -1;
            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string,string > DicUserPass=new System.Collections.Generic.Dictionary<string,string> ();
            GettheMan(out DicUserPass);
            if (CheckIdAndPass(cmbManagerName.Text, txtManagerPass.Text, DicUserPass) == 1)
            {
               // CLDC_DataCore.Function.File.RunOtherExe(CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_OTHER_DATAMANAGEEXEPATH, "/CLDC_DataManager.exe"), CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_OTHER_DATAMANAGEPROCESSNAME, "ClDataManager"));
                
               
                //CLDC_DataCore.Function.File.RunOtherExe(CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_OTHER_DATAMANAGEEXEPATH, "/CLDC_DataManager.exe"), CLDC_DataCore.Const.GlobalUnit.GetConfig(CLDC_DataCore.Const.Variable.CTC_OTHER_DATAMANAGEPROCESSNAME, "ClDataManager"));

                string sql = "UPDATE Info_User SET chrGrpCode = 'loging' WHERE chrUserName = '" + cmbManagerName.Text .Trim()+ "'";
                SqlUpdataMessage(sql);
                this.Close();
                Process.Start(System.Windows.Forms.Application.StartupPath + "\\CLDC_DataManager.exe");
                    
            }
            else
            {
                MessageBox.Show("The Wrong UserName or Password!");
            }

        }

        //#region core
        ///// <summary>
        ///// 根据相对路径获取文件、文件夹绝对路径
        ///// </summary>
        ///// <param name="FileName">相对路径</param>   
        ///// <returns></returns>
        //public static string GetPhyPath(string FileName)
        //{
        //    FileName = FileName.Replace('/', '\\');             //规范路径写法
        //    if (FileName.IndexOf(':') != -1) return FileName;   //已经是绝对路径了
        //    if (FileName.Length > 0 && FileName[0] == '\\') FileName = FileName.Substring(1);
        //    return string.Format("{0}\\{1}", System.Windows.Forms.Application.StartupPath, FileName);
        //}
        //public static void RunOtherExe(string strFilePath, string processName)
        //{
        //    string[] _strFilePath = strFilePath.Split(' ');
        //    if (_strFilePath != null && _strFilePath.Length > 0)
        //    {
        //        strFilePath = GetPhyPath(_strFilePath[0]);
        //    }
        //    else
        //    {
        //        strFilePath = GetPhyPath(strFilePath);
        //    }
        //    Process[] processes = Process.GetProcessesByName(processName);
        //    if (processes.Length > 0)
        //    {
        //        Win32Api.ShowWindow((int)processes[0].MainWindowHandle, 1);
        //        Win32Api.SetForegroundWindow((int)processes[0].MainWindowHandle);
        //        return;
        //    }
        //    System.Diagnostics.Process pHand = null;
        //    pHand = new System.Diagnostics.Process();
        //    pHand.StartInfo.FileName = strFilePath;
        //    pHand.StartInfo.UseShellExecute = false;
        //    if (_strFilePath != null && _strFilePath.Length > 1)
        //    {
        //        pHand.StartInfo.Arguments = _strFilePath[1];
        //    }
        //    //pHand.StartInfo.UserName = processName;
        //    try
        //    {
        //        pHand.Start();
        //    }
        //    catch (Exception e)
        //    {
        //        //GlobalUnit.g_MsgControl.OutMessage(e.Message,false, CLDC_Comm.Enum.Cus_MessageType.提示消息);
        //        MessageBox.Show(e.Message, "提示消息");
        //    }
        //}
        //#endregion
    }
}

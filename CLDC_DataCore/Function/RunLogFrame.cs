using System;
using System.Collections.Generic;
using System.Text;
using CLDC_DataCore.Model.LogModel;
using System.Data.OleDb;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// 写帧日志队列
    /// 数据库连接对象，调试模式一直处于连接状态，停止检定或停止调试，释放连接
    /// 1、默认日志库，Access
    /// 2、自定义数据库
    /// </summary>
    public class RunLogFrame
    {
        /// <summary>
        /// 
        /// </summary>
        public int SleepTime = 1;//ms
        /// <summary>
        /// 
        /// </summary>
        public int CmdTimeOut = 5000;//ms

        private string m_strLogPath;
        const string CONST_ACCESS = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=True;Jet OLEDB:DataBase Password=;Data Source=";
        private OleDbConnection _Con;
        private object objLock = new object();
        private Queue<LogFrameInfo> lstRunLog = new Queue<LogFrameInfo>();

        /// <summary>
        /// 
        /// </summary>
        public RunLogFrame()
        {
            //默认连接
            m_strLogPath = System.Windows.Forms.Application.StartupPath + "\\DataBase\\ClouLog.mdb";
            _Con = new OleDbConnection(CONST_ACCESS + m_strLogPath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strPath"></param>
        public RunLogFrame(string strPath)
        {
            //传入连接,
            if (System.IO.File.Exists(strPath))
            {
                m_strLogPath = strPath;
                _Con = new OleDbConnection(CONST_ACCESS + m_strLogPath);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void CloseCon()
        {
            if (System.Data.ConnectionState.Closed != _Con.State)
            {
                _Con.Close();
            }
        }
        /// <summary>
        /// 压缩报文日志数据库
        /// </summary>
        /// <returns></returns>
        public bool CompressAccess()
        {
            return CLDC_DataCore.DataBase.DbHelperOleDb.CompressAccess(m_strLogPath, "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool DeleteAll()
        {
            try
            {
                if (_Con.State != System.Data.ConnectionState.Open)
                {
                    _Con.Open();
                }
                OleDbCommand _Cmd = _Con.CreateCommand();
                _Cmd.CommandTimeout = CmdTimeOut;
                _Cmd.CommandText = "Delete from FrameLog";//换数据库换表
                _Cmd.ExecuteNonQuery();
                _Cmd.Dispose();
                _Con.Close();
                return true;
            }
            catch 
            {
                return false;
            }
        }
        /// <summary>
        /// 日志入口
        /// </summary>
        /// <param name="LogMsg"></param>
        public void WriteFrameLog(LogFrameInfo LogMsg)
        {

            WriteFrameLog(null, "", LogMsg);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="FunName"></param>
        /// <param name="LogMsg"></param>
        public void WriteFrameLog(object sender, string FunName, LogFrameInfo LogMsg)
        {
            
            lock (objLock)
            {

                System.Diagnostics.Debug.Assert(LogMsg != null);

                lstRunLog.Enqueue(LogMsg);
            }

        }


        /// <summary>
        /// 线程函数,必须保证只有一个线程写日志
        /// </summary>
        public void DoWork()
        {
            while (true)
            {
                if (CLDC_DataCore.Const.GlobalUnit.ApplicationIsOver)
                {
                    break;
                }
                //lstRunLog.TrimExcess();
                if (lstRunLog.Count > 0)
                {
                    try
                    {
                        if (_Con.State != System.Data.ConnectionState.Open)
                        {
                            _Con.Open();
                        }
                        LogFrameInfo _tagLog = lstRunLog.Peek();
                        if (_tagLog != null)
                        {
                            //这里写数据库
                            OleDbCommand _Cmd = _Con.CreateCommand();
                            _Cmd.CommandTimeout = CmdTimeOut;
                            _Cmd.CommandText = _tagLog.getStrSQL();
                            
                            if (1 == _Cmd.ExecuteNonQuery())
                            {
                                lstRunLog.Dequeue();
                            }
                            _Cmd.Dispose();
                        }
                    }
                    catch (Exception EX)
                    {
                        EX.ToString();
                        string logPath = "/Log/Thread/LogThread-" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";
                        //WriteLog(logPath, this, "DoWork", EX.Message + "\r\n" + EX.StackTrace);
                    }
                }
                System.Threading.Thread.Sleep(SleepTime);
            }
        }

    }
}

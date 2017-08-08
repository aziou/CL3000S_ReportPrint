using System;

namespace CLDC_DataCore.DataBase
{
    /// <summary>
    /// 获取数据库连接字符串
    /// </summary>
    public class PubConstant
    {
        /// <summary>
        /// oracle获取连接字符串
        /// <param name="source">数据源</param>
        /// <param name="pwd">密码</param>
        /// </summary>
        public static string ConnectionString(string source, string pwd)
        {

            string _connectionString = "data source=" + source + ";persist security info=True;user id=mcp_pb;password=" + pwd;

            return _connectionString;

        }

        /// <summary>
        /// oledb数据库连接字符串。
        /// </summary>
        /// <param name="mdbPath">数据库路径，包含完整文件名</param>
        /// <param name="pwd">数据库路径，包含完整文件名</param>
        /// <returns></returns>
        public static string GetConnectionString(string mdbPath,string pwd)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Persist Security Info=True;Jet OLEDB:DataBase Password=" + pwd + ";Data Source=" + mdbPath;
            
            return connectionString;
        }


    }

}
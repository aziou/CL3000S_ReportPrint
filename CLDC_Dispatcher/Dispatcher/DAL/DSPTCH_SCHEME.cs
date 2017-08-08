using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using CLDC_DataCore.DataBase;
using System.Configuration;

namespace CLDC_Dispatcher.DAL  
{
	 	//DSPTCH_MESSAGE
    public partial class DSPTCH_SCHEME
	{
            public DSPTCH_SCHEME()
   		{
            DbHelperSQL.connectionString = ConfigurationManager.ConnectionStrings["CLDC_StartUpDispatcher.Properties.Settings.CLOUMETERDATASERVER"].ConnectionString;
   		}

            public bool Exists(string AVR_SCHEME_NAME, string AVR_CHECK_NO)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select count(1) from DSPTCH_SCHEME");
                strSql.Append(" where ");
                strSql.Append(" AVR_SCHEME_NAME = @AVR_SCHEME_NAME and AVR_CHECK_NO=@AVR_CHECK_NO");
                SqlParameter[] parameters = {
					new SqlParameter("@AVR_SCHEME_NAME", SqlDbType.Char,200),
                    new SqlParameter("@AVR_CHECK_NO", SqlDbType.Char,5000)};
                parameters[0].Value = AVR_SCHEME_NAME;
                parameters[1].Value = AVR_CHECK_NO;

                return DbHelperSQL.Exists(strSql.ToString(), parameters);
            }
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
            public int Add(CLDC_Dispatcher.Model.DSPTCH_SCHEME model)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into DSPTCH_SCHEME(");
                strSql.Append("AVR_SCHEME_NAME,AVR_CHECK_NO");
                strSql.Append(") values (");
                strSql.Append("@AVR_SCHEME_NAME,@AVR_CHECK_NO");
                strSql.Append(") ");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
			            new SqlParameter("@AVR_SCHEME_NAME", SqlDbType.Char,200) ,            
                        new SqlParameter("@AVR_CHECK_NO", SqlDbType.Char,5000)             
              
                };

                parameters[0].Value = model.AVR_SCHEME_NAME;
                parameters[1].Value = model.AVR_CHECK_NO;
                object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);

                }
            }
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(CLDC_Dispatcher.Model.DSPTCH_SCHEME model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update DSPTCH_SCHEME set ");
			                        
            //strSql.Append(" AVR_SCHEME_ID = @AVR_SCHEME_ID , ");                                    
            strSql.Append(" AVR_SCHEME_NAME = @AVR_SCHEME_NAME , ");                                    
            strSql.Append(" AVR_CHECK_NO = @AVR_CHECK_NO  ");            			
			strSql.Append(" where AVR_SCHEME_ID=@AVR_SCHEME_ID  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@AVR_SCHEME_ID", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_SCHEME_NAME", SqlDbType.Char,200) ,            
                        new SqlParameter("@AVR_CHECK_NO", SqlDbType.Char,5000)             
              
            };
						            
            parameters[0].Value = model.AVR_SCHEME_ID;                        
            parameters[1].Value = model.AVR_SCHEME_NAME;                        
            parameters[2].Value = model.AVR_CHECK_NO;                        
            int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string AVR_SCHEME_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from DSPTCH_SCHEME ");
			strSql.Append(" where AVR_SCHEME_ID=@AVR_SCHEME_ID ");
						SqlParameter[] parameters = {
					new SqlParameter("@AVR_SCHEME_ID", SqlDbType.Char,32)			};
			parameters[0].Value = AVR_SCHEME_ID;


			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
				
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public CLDC_Dispatcher.Model.DSPTCH_SCHEME GetModel(string AVR_SCHEME_ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select AVR_SCHEME_ID, AVR_SCHEME_NAME, AVR_CHECK_NO  ");
            strSql.Append("  from DSPTCH_SCHEME ");
			strSql.Append(" where AVR_SCHEME_ID=@AVR_SCHEME_ID ");
						SqlParameter[] parameters = {
					new SqlParameter("@AVR_SCHEME_ID", SqlDbType.Char,32)			};
			parameters[0].Value = AVR_SCHEME_ID;


            CLDC_Dispatcher.Model.DSPTCH_SCHEME model = new CLDC_Dispatcher.Model.DSPTCH_SCHEME();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.AVR_SCHEME_ID= ds.Tables[0].Rows[0]["AVR_SCHEME_ID"].ToString();
																																model.AVR_SCHEME_NAME= ds.Tables[0].Rows[0]["AVR_SCHEME_NAME"].ToString();
																																model.AVR_CHECK_NO= ds.Tables[0].Rows[0]["AVR_CHECK_NO"].ToString();
																										
				return model;
			}
			else
			{
				return null;
			}
		}
		
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetDataSet(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM DSPTCH_SCHEME ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetDataSet(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
            strSql.Append(" FROM DSPTCH_SCHEME ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<CLDC_Dispatcher.Model.DSPTCH_SCHEME> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
            strSql.Append(" FROM DSPTCH_SCHEME ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			if(ds == null )
			{
				return null;
			}
            List<CLDC_Dispatcher.Model.DSPTCH_SCHEME> lst_M = new List<CLDC_Dispatcher.Model.DSPTCH_SCHEME>();
			foreach(DataRow row in ds.Tables[0].Rows)
			{
                CLDC_Dispatcher.Model.DSPTCH_SCHEME model = new CLDC_Dispatcher.Model.DSPTCH_SCHEME();
			
																model.AVR_SCHEME_ID= row["AVR_SCHEME_ID"].ToString();
																																model.AVR_SCHEME_NAME= row["AVR_SCHEME_NAME"].ToString();
																																model.AVR_CHECK_NO= row["AVR_CHECK_NO"].ToString();
																										
				lst_M.Add(model);
			
			}
			return lst_M;
		}

   
	}
}


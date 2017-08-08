using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using CLDC_DataCore.DataBase;
using System.Configuration;

namespace CLDC_Dispatcher.DAL  
{
	 	//DSPTCH_CUR_SCHEME
		public partial class DSPTCH_CUR_SCHEME
	{
   		public DSPTCH_CUR_SCHEME()
   		{
            DbHelperSQL.connectionString = ConfigurationManager.ConnectionStrings["CLDC_StartUpDispatcher.Properties.Settings.CLOUMETERDATASERVER"].ConnectionString;
   		}
   		
		public bool Exists(string FK_DEVICE_MADE_NO)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DSPTCH_CUR_SCHEME");
			strSql.Append(" where ");
			                                       strSql.Append(" FK_DEVICE_MADE_NO = @FK_DEVICE_MADE_NO  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@FK_DEVICE_MADE_NO", SqlDbType.Char,32)			};
			parameters[0].Value = FK_DEVICE_MADE_NO;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DSPTCH_CUR_SCHEME(");			
            strSql.Append("FK_DEVICE_MADE_NO,AVR_SCHEME_ID");
			strSql.Append(") values (");
            strSql.Append("@FK_DEVICE_MADE_NO,@AVR_SCHEME_ID");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@FK_DEVICE_MADE_NO", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_SCHEME_ID", SqlDbType.Char,32)             
              
            };
			            
            parameters[0].Value = model.FK_DEVICE_MADE_NO;                        
            parameters[1].Value = model.AVR_SCHEME_ID;                        
			            DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DSPTCH_CUR_SCHEME set ");
			                        
            //strSql.Append(" FK_DEVICE_MADE_NO = @FK_DEVICE_MADE_NO , ");                                    
            strSql.Append(" AVR_SCHEME_ID = @AVR_SCHEME_ID  ");            			
			strSql.Append(" where FK_DEVICE_MADE_NO=@FK_DEVICE_MADE_NO  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@FK_DEVICE_MADE_NO", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_SCHEME_ID", SqlDbType.Char,32)             
              
            };
						            
            parameters[0].Value = model.FK_DEVICE_MADE_NO;                        
            parameters[1].Value = model.AVR_SCHEME_ID;                        
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
		public bool Delete(string FK_DEVICE_MADE_NO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from DSPTCH_CUR_SCHEME ");
			strSql.Append(" where FK_DEVICE_MADE_NO=@FK_DEVICE_MADE_NO ");
						SqlParameter[] parameters = {
					new SqlParameter("@FK_DEVICE_MADE_NO", SqlDbType.Char,32)			};
			parameters[0].Value = FK_DEVICE_MADE_NO;


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
		public CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME GetModel(string FK_DEVICE_MADE_NO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select FK_DEVICE_MADE_NO, AVR_SCHEME_ID  ");			
			strSql.Append("  from DSPTCH_CUR_SCHEME ");
			strSql.Append(" where FK_DEVICE_MADE_NO=@FK_DEVICE_MADE_NO ");
						SqlParameter[] parameters = {
					new SqlParameter("@FK_DEVICE_MADE_NO", SqlDbType.Char,32)			};
			parameters[0].Value = FK_DEVICE_MADE_NO;

			
			CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME model=new CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.FK_DEVICE_MADE_NO= ds.Tables[0].Rows[0]["FK_DEVICE_MADE_NO"].ToString();
																																model.AVR_SCHEME_ID= ds.Tables[0].Rows[0]["AVR_SCHEME_ID"].ToString();
																										
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
			strSql.Append(" FROM DSPTCH_CUR_SCHEME ");
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
			strSql.Append(" FROM DSPTCH_CUR_SCHEME ");
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
		public List<CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM DSPTCH_CUR_SCHEME ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			if(ds == null )
			{
				return null;
			}
			List<CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME> lst_M = new List<CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME>();
			foreach(DataRow row in ds.Tables[0].Rows)
			{
				CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME model=new CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME();
			
																model.FK_DEVICE_MADE_NO= row["FK_DEVICE_MADE_NO"].ToString();
																																model.AVR_SCHEME_ID= row["AVR_SCHEME_ID"].ToString();
																										
				lst_M.Add(model);
			
			}
			return lst_M;
		}

   
	}
}


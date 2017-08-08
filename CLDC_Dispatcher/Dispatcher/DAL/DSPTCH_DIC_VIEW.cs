using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using CLDC_DataCore.DataBase;
using System.Configuration;

namespace CLDC_StartUpDispatcher.DAL  
{
	 	//DSPTCH_DIC_VIEW
		public partial class DSPTCH_DIC_VIEW
	{
   		public DSPTCH_DIC_VIEW()
   		{
   			DbHelperSQL.connectionString = ConfigurationManager.ConnectionStrings["CLOUMETERDATASERVER"].ConnectionString;
   		}
   		
		public bool Exists(string PK_VIEW_NO)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DSPTCH_DIC_VIEW");
			strSql.Append(" where ");
			                                       strSql.Append(" PK_VIEW_NO = @PK_VIEW_NO  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@PK_VIEW_NO", SqlDbType.Char,32)			};
			parameters[0].Value = PK_VIEW_NO;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DSPTCH_DIC_VIEW(");			
            strSql.Append("PK_VIEW_NO,AVR_CHECK_NAME,AVR_TABLE_NAME,AVR_COL_NAME,AVR_COL_SHOW_NAME");
			strSql.Append(") values (");
            strSql.Append("@PK_VIEW_NO,@AVR_CHECK_NAME,@AVR_TABLE_NAME,@AVR_COL_NAME,@AVR_COL_SHOW_NAME");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@PK_VIEW_NO", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_CHECK_NAME", SqlDbType.Char,200) ,            
                        new SqlParameter("@AVR_TABLE_NAME", SqlDbType.Char,50) ,            
                        new SqlParameter("@AVR_COL_NAME", SqlDbType.Char,500) ,            
                        new SqlParameter("@AVR_COL_SHOW_NAME", SqlDbType.Char,1000)             
              
            };
			            
            parameters[0].Value = model.PK_VIEW_NO;                        
            parameters[1].Value = model.AVR_CHECK_NAME;                        
            parameters[2].Value = model.AVR_TABLE_NAME;                        
            parameters[3].Value = model.AVR_COL_NAME;                        
            parameters[4].Value = model.AVR_COL_SHOW_NAME;                        
			            DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DSPTCH_DIC_VIEW set ");
			                        
            strSql.Append(" PK_VIEW_NO = @PK_VIEW_NO , ");                                    
            strSql.Append(" AVR_CHECK_NAME = @AVR_CHECK_NAME , ");                                    
            strSql.Append(" AVR_TABLE_NAME = @AVR_TABLE_NAME , ");                                    
            strSql.Append(" AVR_COL_NAME = @AVR_COL_NAME , ");                                    
            strSql.Append(" AVR_COL_SHOW_NAME = @AVR_COL_SHOW_NAME  ");            			
			strSql.Append(" where PK_VIEW_NO=@PK_VIEW_NO  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@PK_VIEW_NO", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_CHECK_NAME", SqlDbType.Char,200) ,            
                        new SqlParameter("@AVR_TABLE_NAME", SqlDbType.Char,50) ,            
                        new SqlParameter("@AVR_COL_NAME", SqlDbType.Char,500) ,            
                        new SqlParameter("@AVR_COL_SHOW_NAME", SqlDbType.Char,1000)             
              
            };
						            
            parameters[0].Value = model.PK_VIEW_NO;                        
            parameters[1].Value = model.AVR_CHECK_NAME;                        
            parameters[2].Value = model.AVR_TABLE_NAME;                        
            parameters[3].Value = model.AVR_COL_NAME;                        
            parameters[4].Value = model.AVR_COL_SHOW_NAME;                        
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
		public bool Delete(string PK_VIEW_NO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from DSPTCH_DIC_VIEW ");
			strSql.Append(" where PK_VIEW_NO=@PK_VIEW_NO ");
						SqlParameter[] parameters = {
					new SqlParameter("@PK_VIEW_NO", SqlDbType.Char,32)			};
			parameters[0].Value = PK_VIEW_NO;


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
		public CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW GetModel(string PK_VIEW_NO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PK_VIEW_NO, AVR_CHECK_NAME, AVR_TABLE_NAME, AVR_COL_NAME, AVR_COL_SHOW_NAME  ");			
			strSql.Append("  from DSPTCH_DIC_VIEW ");
			strSql.Append(" where PK_VIEW_NO=@PK_VIEW_NO ");
						SqlParameter[] parameters = {
					new SqlParameter("@PK_VIEW_NO", SqlDbType.Char,32)			};
			parameters[0].Value = PK_VIEW_NO;

			
			CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW model=new CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.PK_VIEW_NO= ds.Tables[0].Rows[0]["PK_VIEW_NO"].ToString();
																																model.AVR_CHECK_NAME= ds.Tables[0].Rows[0]["AVR_CHECK_NAME"].ToString();
																																model.AVR_TABLE_NAME= ds.Tables[0].Rows[0]["AVR_TABLE_NAME"].ToString();
																																model.AVR_COL_NAME= ds.Tables[0].Rows[0]["AVR_COL_NAME"].ToString();
																																model.AVR_COL_SHOW_NAME= ds.Tables[0].Rows[0]["AVR_COL_SHOW_NAME"].ToString();
																										
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
			strSql.Append(" FROM DSPTCH_DIC_VIEW ");
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
			strSql.Append(" FROM DSPTCH_DIC_VIEW ");
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
		public List<CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM DSPTCH_DIC_VIEW ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			if(ds == null )
			{
				return null;
			}
			List<CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW> lst_M = new List<CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW>();
			foreach(DataRow row in ds.Tables[0].Rows)
			{
				CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW model=new CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW();
			
																model.PK_VIEW_NO= row["PK_VIEW_NO"].ToString();
																																model.AVR_CHECK_NAME= row["AVR_CHECK_NAME"].ToString();
																																model.AVR_TABLE_NAME= row["AVR_TABLE_NAME"].ToString();
																																model.AVR_COL_NAME= row["AVR_COL_NAME"].ToString();
																																model.AVR_COL_SHOW_NAME= row["AVR_COL_SHOW_NAME"].ToString();
																										
				lst_M.Add(model);
			
			}
			return lst_M;
		}

   
	}
}


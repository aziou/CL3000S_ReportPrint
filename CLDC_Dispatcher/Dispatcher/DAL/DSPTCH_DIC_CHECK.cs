using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using CLDC_DataCore.DataBase;
using System.Configuration;

namespace CLDC_Dispatcher.DAL  
{
	 	//DSPTCH_DIC_CHECK
		public partial class DSPTCH_DIC_CHECK
	{
   		public DSPTCH_DIC_CHECK()
   		{
            DbHelperSQL.connectionString = ConfigurationManager.ConnectionStrings["CLDC_StartUpDispatcher.Properties.Settings.CLOUMETERDATASERVER"].ConnectionString;
   		}
   		
		public bool Exists(string AVR_CHECK_NO)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DSPTCH_DIC_CHECK");
			strSql.Append(" where ");
			                                       strSql.Append(" AVR_CHECK_NO = @AVR_CHECK_NO  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@AVR_CHECK_NO", SqlDbType.Char,32)			};
			parameters[0].Value = AVR_CHECK_NO;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DSPTCH_DIC_CHECK(");			
            strSql.Append("AVR_CHECK_NO,AVR_CHECK_NAME,AVR_CHECK_TYPE,AVR_NEED_TIME,FK_VIEW_NO");//
			strSql.Append(") values (");
            strSql.Append("@AVR_CHECK_NO,@AVR_CHECK_NAME,@AVR_CHECK_TYPE,@AVR_NEED_TIME,@FK_VIEW_NO");//            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@AVR_CHECK_NO", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_CHECK_NAME", SqlDbType.Char,200) ,            
                        new SqlParameter("@AVR_CHECK_TYPE", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_NEED_TIME", SqlDbType.Char,8) ,            
                        new SqlParameter("@FK_VIEW_NO", SqlDbType.Char,32)             
              
            };
			            
            parameters[0].Value = model.AVR_CHECK_NO;                        
            parameters[1].Value = model.AVR_CHECK_NAME;                        
            parameters[2].Value = model.AVR_CHECK_TYPE;                        
            parameters[3].Value = model.AVR_NEED_TIME;                        
            parameters[4].Value = model.FK_VIEW_NO;                        
			            DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DSPTCH_DIC_CHECK set ");
			                        
            //strSql.Append(" AVR_CHECK_NO = @AVR_CHECK_NO , ");                                    
            strSql.Append(" AVR_CHECK_NAME = @AVR_CHECK_NAME , ");                                    
            strSql.Append(" AVR_CHECK_TYPE = @AVR_CHECK_TYPE , ");                                    
            strSql.Append(" AVR_NEED_TIME = @AVR_NEED_TIME  ");                                    
            //strSql.Append(" FK_VIEW_NO = @FK_VIEW_NO  ");            			
			strSql.Append(" where AVR_CHECK_NO = @AVR_CHECK_NO  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@AVR_CHECK_NO", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_CHECK_NAME", SqlDbType.Char,200) ,            
                        new SqlParameter("@AVR_CHECK_TYPE", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_NEED_TIME", SqlDbType.Char,8) ,            
                        //new SqlParameter("@FK_VIEW_NO", SqlDbType.Char,32)             
              
            };
						            
            parameters[0].Value = model.AVR_CHECK_NO;                        
            parameters[1].Value = model.AVR_CHECK_NAME;                        
            parameters[2].Value = model.AVR_CHECK_TYPE;                        
            parameters[3].Value = model.AVR_NEED_TIME;                        
            //parameters[4].Value = model.FK_VIEW_NO;                        
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
		public bool Delete(string AVR_CHECK_NO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from DSPTCH_DIC_CHECK ");
			strSql.Append(" where AVR_CHECK_NO=@AVR_CHECK_NO ");
						SqlParameter[] parameters = {
					new SqlParameter("@AVR_CHECK_NO", SqlDbType.Char,32)			};
			parameters[0].Value = AVR_CHECK_NO;


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
		public CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK GetModel(string AVR_CHECK_NO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select AVR_CHECK_NO, AVR_CHECK_NAME, AVR_CHECK_TYPE, AVR_NEED_TIME, FK_VIEW_NO  ");			
			strSql.Append("  from DSPTCH_DIC_CHECK ");
			strSql.Append(" where AVR_CHECK_NO=@AVR_CHECK_NO ");
						SqlParameter[] parameters = {
					new SqlParameter("@AVR_CHECK_NO", SqlDbType.Char,32)			};
			parameters[0].Value = AVR_CHECK_NO;

			
			CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK model=new CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.AVR_CHECK_NO= ds.Tables[0].Rows[0]["AVR_CHECK_NO"].ToString();
																																model.AVR_CHECK_NAME= ds.Tables[0].Rows[0]["AVR_CHECK_NAME"].ToString();
																																model.AVR_CHECK_TYPE= ds.Tables[0].Rows[0]["AVR_CHECK_TYPE"].ToString();
																																model.AVR_NEED_TIME= ds.Tables[0].Rows[0]["AVR_NEED_TIME"].ToString();
																																model.FK_VIEW_NO= ds.Tables[0].Rows[0]["FK_VIEW_NO"].ToString();
																										
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
			strSql.Append(" FROM DSPTCH_DIC_CHECK ");
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
			strSql.Append(" FROM DSPTCH_DIC_CHECK ");
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
		public List<CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM DSPTCH_DIC_CHECK ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			if(ds == null )
			{
				return null;
			}
			List<CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK> lst_M = new List<CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK>();
			foreach(DataRow row in ds.Tables[0].Rows)
			{
				CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK model=new CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK();
			
																model.AVR_CHECK_NO= row["AVR_CHECK_NO"].ToString();
																																model.AVR_CHECK_NAME= row["AVR_CHECK_NAME"].ToString();
																																model.AVR_CHECK_TYPE= row["AVR_CHECK_TYPE"].ToString();
																																model.AVR_NEED_TIME= row["AVR_NEED_TIME"].ToString();
																																model.FK_VIEW_NO= row["FK_VIEW_NO"].ToString();
																										
				lst_M.Add(model);
			
			}
			return lst_M;
		}

   
	}
}


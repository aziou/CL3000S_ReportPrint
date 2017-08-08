using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using CLDC_DataCore.DataBase;
using System.Configuration;

namespace CLDC_Dispatcher.DAL  
{
	 	//DSPTCH_CUR_MESSAGE
		public partial class DSPTCH_CUR_MESSAGE
	{
   		public DSPTCH_CUR_MESSAGE()
   		{
            DbHelperSQL.connectionString = ConfigurationManager.ConnectionStrings["CLDC_StartUpDispatcher.Properties.Settings.CLOUMETERDATASERVER"].ConnectionString;
   		}
   		
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DSPTCH_CUR_MESSAGE");
			strSql.Append(" where ");
			                                       strSql.Append(" ID = @ID  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DSPTCH_CUR_MESSAGE(");			
            strSql.Append("FK_DEVICE_MADE_NO,AVR_MSG_TYPE,AVR_DATA,AVR_WRITE_TIME,AVR_HANDLE_FLAG");
			strSql.Append(") values (");
            strSql.Append("@FK_DEVICE_MADE_NO,@AVR_MSG_TYPE,@AVR_DATA,@AVR_WRITE_TIME,@AVR_HANDLE_FLAG");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@FK_DEVICE_MADE_NO", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_MSG_TYPE", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_DATA", SqlDbType.Char,1000) ,            
                        new SqlParameter("@AVR_WRITE_TIME", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_HANDLE_FLAG", SqlDbType.Char,8)             
              
            };
			            
            parameters[0].Value = model.FK_DEVICE_MADE_NO;                        
            parameters[1].Value = model.AVR_MSG_TYPE;                        
            parameters[2].Value = model.AVR_DATA;                        
            parameters[3].Value = model.AVR_WRITE_TIME;                        
            parameters[4].Value = model.AVR_HANDLE_FLAG;                        
			   
			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);			
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
		public bool Update(CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DSPTCH_CUR_MESSAGE set ");
			                                                
            //strSql.Append(" FK_DEVICE_MADE_NO = @FK_DEVICE_MADE_NO , ");                                    
            //strSql.Append(" AVR_MSG_TYPE = @AVR_MSG_TYPE , ");                                    
            strSql.Append(" AVR_DATA = @AVR_DATA , ");                                    
            strSql.Append(" AVR_WRITE_TIME = @AVR_WRITE_TIME , ");                                    
            strSql.Append(" AVR_HANDLE_FLAG = @AVR_HANDLE_FLAG  ");
            strSql.Append(" where FK_DEVICE_MADE_NO=@FK_DEVICE_MADE_NO and AVR_MSG_TYPE=@AVR_MSG_TYPE");
						
SqlParameter[] parameters = {
			            
                        new SqlParameter("@FK_DEVICE_MADE_NO", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_MSG_TYPE", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_DATA", SqlDbType.Char,1000) ,            
                        new SqlParameter("@AVR_WRITE_TIME", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_HANDLE_FLAG", SqlDbType.Char,8)             
              
            };
						            
                       
            parameters[0].Value = model.FK_DEVICE_MADE_NO;                        
            parameters[1].Value = model.AVR_MSG_TYPE;                        
            parameters[2].Value = model.AVR_DATA;                        
            parameters[3].Value = model.AVR_WRITE_TIME;                        
            parameters[4].Value = model.AVR_HANDLE_FLAG;                        
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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from DSPTCH_CUR_MESSAGE ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;


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
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from DSPTCH_CUR_MESSAGE ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, FK_DEVICE_MADE_NO, AVR_MSG_TYPE, AVR_DATA, AVR_WRITE_TIME, AVR_HANDLE_FLAG  ");			
			strSql.Append("  from DSPTCH_CUR_MESSAGE ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE model=new CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																				model.FK_DEVICE_MADE_NO= ds.Tables[0].Rows[0]["FK_DEVICE_MADE_NO"].ToString();
																																model.AVR_MSG_TYPE= ds.Tables[0].Rows[0]["AVR_MSG_TYPE"].ToString();
																																model.AVR_DATA= ds.Tables[0].Rows[0]["AVR_DATA"].ToString();
																																model.AVR_WRITE_TIME= ds.Tables[0].Rows[0]["AVR_WRITE_TIME"].ToString();
																																model.AVR_HANDLE_FLAG= ds.Tables[0].Rows[0]["AVR_HANDLE_FLAG"].ToString();
																										
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
			strSql.Append(" FROM DSPTCH_CUR_MESSAGE ");
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
			strSql.Append(" FROM DSPTCH_CUR_MESSAGE ");
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
		public List<CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM DSPTCH_CUR_MESSAGE ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			if(ds == null )
			{
				return null;
			}
			List<CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE> lst_M = new List<CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE>();
			foreach(DataRow row in ds.Tables[0].Rows)
			{
				CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE model=new CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE();
			
												if(row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
																																				model.FK_DEVICE_MADE_NO= row["FK_DEVICE_MADE_NO"].ToString();
																																model.AVR_MSG_TYPE= row["AVR_MSG_TYPE"].ToString();
																																model.AVR_DATA= row["AVR_DATA"].ToString();
																																model.AVR_WRITE_TIME= row["AVR_WRITE_TIME"].ToString();
																																model.AVR_HANDLE_FLAG= row["AVR_HANDLE_FLAG"].ToString();
																										
				lst_M.Add(model);
			
			}
			return lst_M;
		}

   
	}
}


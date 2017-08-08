﻿using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using CLDC_DataCore.DataBase;
using System.Configuration;

namespace CLDC_Dispatcher.DAL  
{
	 	//DSPTCH_TASKS
		public partial class DSPTCH_TASKS
	{
   		public DSPTCH_TASKS()
   		{
            DbHelperSQL.connectionString = ConfigurationManager.ConnectionStrings["CLDC_StartUpDispatcher.Properties.Settings.CLOUMETERDATASERVER"].ConnectionString;
            
   		}

        public bool Exists(string DEVICE_ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DSPTCH_TASKS");
			strSql.Append(" where ");
            strSql.Append(" AVR_DEVICE_ID = @AVR_DEVICE_ID and AVR_END_FLAG='0'  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@AVR_DEVICE_ID", SqlDbType.Char,32)
			};
			parameters[0].Value = DEVICE_ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CLDC_Dispatcher.Model.DSPTCH_TASKS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DSPTCH_TASKS(");			
            strSql.Append("TASK_NO,AVR_DEVICE_ID,AVR_END_FLAG,AVR_START_TIME,AVR_END_TIME,AVR_HANDLE_FLAG,AVR_SCHEME_ID,AVR_WRITE_TIME");
			strSql.Append(") values (");
            strSql.Append("@TASK_NO,@AVR_DEVICE_ID,@AVR_END_FLAG,@AVR_START_TIME,@AVR_END_TIME,@AVR_HANDLE_FLAG,@AVR_SCHEME_ID,@AVR_WRITE_TIME");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			SqlParameter[] parameters = {
			            new SqlParameter("@TASK_NO", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_DEVICE_ID", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_END_FLAG", SqlDbType.Char,8) ,            
                        new SqlParameter("@AVR_START_TIME", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_END_TIME", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_HANDLE_FLAG", SqlDbType.Char,8) ,            
                        new SqlParameter("@AVR_SCHEME_ID", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_WRITE_TIME", SqlDbType.Char,32)             
              
            };
			            
            parameters[0].Value = model.TASK_NO;                        
            parameters[1].Value = model.AVR_DEVICE_ID;                        
            parameters[2].Value = model.AVR_END_FLAG;                        
            parameters[3].Value = model.AVR_START_TIME;                        
            parameters[4].Value = model.AVR_END_TIME;                        
            parameters[5].Value = model.AVR_HANDLE_FLAG;                        
            parameters[6].Value = model.AVR_SCHEME_ID;                        
            parameters[7].Value = model.AVR_WRITE_TIME;                        
			   
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
		public bool Update(CLDC_Dispatcher.Model.DSPTCH_TASKS model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DSPTCH_TASKS set ");
			                                                
            //strSql.Append(" TASK_NO = @TASK_NO , ");                                    
            //strSql.Append(" AVR_DEVICE_ID = @AVR_DEVICE_ID , ");                                    
            //strSql.Append(" AVR_END_FLAG = @AVR_END_FLAG , ");                                    
            //strSql.Append(" AVR_START_TIME = @AVR_START_TIME , ");                                    
            //strSql.Append(" AVR_END_TIME = @AVR_END_TIME , ");                                    
            strSql.Append(" AVR_HANDLE_FLAG = @AVR_HANDLE_FLAG , ");                                    
            //strSql.Append(" AVR_SCHEME_ID = @AVR_SCHEME_ID , ");                                    
            strSql.Append(" AVR_WRITE_TIME = @AVR_WRITE_TIME  ");
            strSql.Append(" where AVR_DEVICE_ID = @AVR_DEVICE_ID and AVR_END_FLAG='0'");
						
SqlParameter[] parameters = {
                        //new SqlParameter("@ID", SqlDbType.Int,4) ,            
                        //new SqlParameter("@TASK_NO", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_DEVICE_ID", SqlDbType.Char,32) ,            
                        //new SqlParameter("@AVR_END_FLAG", SqlDbType.Char,8) ,            
                        //new SqlParameter("@AVR_START_TIME", SqlDbType.Char,32) ,            
                        //new SqlParameter("@AVR_END_TIME", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_HANDLE_FLAG", SqlDbType.Char,8) ,            
                        //new SqlParameter("@AVR_SCHEME_ID", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_WRITE_TIME", SqlDbType.Char,32)             
              
            };

            //parameters[0].Value = model.ID;
            //parameters[1].Value = model.TASK_NO;                        
            //parameters[2].Value = model.AVR_DEVICE_ID;
            //parameters[3].Value = model.AVR_END_FLAG;
            //parameters[4].Value = model.AVR_START_TIME;
            //parameters[5].Value = model.AVR_END_TIME;                        
            //parameters[6].Value = model.AVR_HANDLE_FLAG;
            //parameters[7].Value = model.AVR_SCHEME_ID;                        
            //parameters[8].Value = model.AVR_WRITE_TIME;

            parameters[0].Value = model.AVR_DEVICE_ID;
            parameters[1].Value = model.AVR_HANDLE_FLAG;
            parameters[2].Value = model.AVR_WRITE_TIME;
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
			strSql.Append("delete from DSPTCH_TASKS ");
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
			strSql.Append("delete from DSPTCH_TASKS ");
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
		public CLDC_Dispatcher.Model.DSPTCH_TASKS GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID, TASK_NO, AVR_DEVICE_ID, AVR_END_FLAG, AVR_START_TIME, AVR_END_TIME, AVR_HANDLE_FLAG, AVR_SCHEME_ID, AVR_WRITE_TIME  ");			
			strSql.Append("  from DSPTCH_TASKS ");
			strSql.Append(" where ID=@ID");
						SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			
			CLDC_Dispatcher.Model.DSPTCH_TASKS model=new CLDC_Dispatcher.Model.DSPTCH_TASKS();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
																																				model.TASK_NO= ds.Tables[0].Rows[0]["TASK_NO"].ToString();
																																model.AVR_DEVICE_ID= ds.Tables[0].Rows[0]["AVR_DEVICE_ID"].ToString();
																																model.AVR_END_FLAG= ds.Tables[0].Rows[0]["AVR_END_FLAG"].ToString();
																																model.AVR_START_TIME= ds.Tables[0].Rows[0]["AVR_START_TIME"].ToString();
																																model.AVR_END_TIME= ds.Tables[0].Rows[0]["AVR_END_TIME"].ToString();
																																model.AVR_HANDLE_FLAG= ds.Tables[0].Rows[0]["AVR_HANDLE_FLAG"].ToString();
																																model.AVR_SCHEME_ID= ds.Tables[0].Rows[0]["AVR_SCHEME_ID"].ToString();
																																model.AVR_WRITE_TIME= ds.Tables[0].Rows[0]["AVR_WRITE_TIME"].ToString();
																										
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
			strSql.Append(" FROM DSPTCH_TASKS ");
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
			strSql.Append(" FROM DSPTCH_TASKS ");
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
		public List<CLDC_Dispatcher.Model.DSPTCH_TASKS> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM DSPTCH_TASKS ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			if(ds == null )
			{
				return null;
			}
			List<CLDC_Dispatcher.Model.DSPTCH_TASKS> lst_M = new List<CLDC_Dispatcher.Model.DSPTCH_TASKS>();
			foreach(DataRow row in ds.Tables[0].Rows)
			{
				CLDC_Dispatcher.Model.DSPTCH_TASKS model=new CLDC_Dispatcher.Model.DSPTCH_TASKS();
			
												if(row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
																																				model.TASK_NO= row["TASK_NO"].ToString();
																																model.AVR_DEVICE_ID= row["AVR_DEVICE_ID"].ToString();
																																model.AVR_END_FLAG= row["AVR_END_FLAG"].ToString();
																																model.AVR_START_TIME= row["AVR_START_TIME"].ToString();
																																model.AVR_END_TIME= row["AVR_END_TIME"].ToString();
																																model.AVR_HANDLE_FLAG= row["AVR_HANDLE_FLAG"].ToString();
																																model.AVR_SCHEME_ID= row["AVR_SCHEME_ID"].ToString();
																																model.AVR_WRITE_TIME= row["AVR_WRITE_TIME"].ToString();
																										
				lst_M.Add(model);
			
			}
			return lst_M;
		}

   
	}
}


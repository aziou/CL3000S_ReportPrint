using System; 
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic; 
using System.Data;
using CLDC_DataCore.DataBase;
using System.Configuration;

namespace CLDC_Dispatcher.DAL  
{
	 	//DSPTCH_EQUIP_INFO
		public partial class DSPTCH_EQUIP_INFO
	{
   		public DSPTCH_EQUIP_INFO()
   		{
            DbHelperSQL.connectionString = ConfigurationManager.ConnectionStrings["CLDC_StartUpDispatcher.Properties.Settings.CLOUMETERDATASERVER"].ConnectionString;
   		}
   		
		public bool Exists(string PK_DEVICE_MADE_NO)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from DSPTCH_EQUIP_INFO");
			strSql.Append(" where ");
			                                       strSql.Append(" PK_DEVICE_MADE_NO = @PK_DEVICE_MADE_NO  ");
                            			SqlParameter[] parameters = {
					new SqlParameter("@PK_DEVICE_MADE_NO", SqlDbType.Char,32)			};
			parameters[0].Value = PK_DEVICE_MADE_NO;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		
				
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into DSPTCH_EQUIP_INFO(");			
            strSql.Append("PK_DEVICE_MADE_NO,AVR_DEVICE_ID,AVR_EQUIP_NAME,AVR_EQUIP_MODEL,AVR_EQUIP_PARA,AVR_EQUIP_TYPE,AVR_FACTORY,AVR_EQUIP_CLASS,AVR_EQUIP_STANDARD,LNG_BENCH_NUM");
			strSql.Append(") values (");
            strSql.Append("@PK_DEVICE_MADE_NO,@AVR_DEVICE_ID,@AVR_EQUIP_NAME,@AVR_EQUIP_MODEL,@AVR_EQUIP_PARA,@AVR_EQUIP_TYPE,@AVR_FACTORY,@AVR_EQUIP_CLASS,@AVR_EQUIP_STANDARD,@LNG_BENCH_NUM");            
            strSql.Append(") ");            
            		
			SqlParameter[] parameters = {
			            new SqlParameter("@PK_DEVICE_MADE_NO", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_DEVICE_ID", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_EQUIP_NAME", SqlDbType.Char,100) ,            
                        new SqlParameter("@AVR_EQUIP_MODEL", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_EQUIP_PARA", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_EQUIP_TYPE", SqlDbType.Char,8) ,            
                        new SqlParameter("@AVR_FACTORY", SqlDbType.Char,100) ,            
                        new SqlParameter("@AVR_EQUIP_CLASS", SqlDbType.Char,8) ,            
                        new SqlParameter("@AVR_EQUIP_STANDARD", SqlDbType.Char,32) ,            
                        new SqlParameter("@LNG_BENCH_NUM", SqlDbType.Char,8)             
              
            };
			            
            parameters[0].Value = model.PK_DEVICE_MADE_NO;                        
            parameters[1].Value = model.AVR_DEVICE_ID;                        
            parameters[2].Value = model.AVR_EQUIP_NAME;                        
            parameters[3].Value = model.AVR_EQUIP_MODEL;                        
            parameters[4].Value = model.AVR_EQUIP_PARA;                        
            parameters[5].Value = model.AVR_EQUIP_TYPE;                        
            parameters[6].Value = model.AVR_FACTORY;                        
            parameters[7].Value = model.AVR_EQUIP_CLASS;                        
            parameters[8].Value = model.AVR_EQUIP_STANDARD;                        
            parameters[9].Value = model.LNG_BENCH_NUM;                        
			            DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update DSPTCH_EQUIP_INFO set ");
			                        
            strSql.Append(" PK_DEVICE_MADE_NO = @PK_DEVICE_MADE_NO , ");                                    
            strSql.Append(" AVR_DEVICE_ID = @AVR_DEVICE_ID , ");                                    
            strSql.Append(" AVR_EQUIP_NAME = @AVR_EQUIP_NAME , ");                                    
            strSql.Append(" AVR_EQUIP_MODEL = @AVR_EQUIP_MODEL , ");                                    
            strSql.Append(" AVR_EQUIP_PARA = @AVR_EQUIP_PARA , ");                                    
            strSql.Append(" AVR_EQUIP_TYPE = @AVR_EQUIP_TYPE , ");                                    
            strSql.Append(" AVR_FACTORY = @AVR_FACTORY , ");                                    
            strSql.Append(" AVR_EQUIP_CLASS = @AVR_EQUIP_CLASS , ");                                    
            strSql.Append(" AVR_EQUIP_STANDARD = @AVR_EQUIP_STANDARD , ");                                    
            strSql.Append(" LNG_BENCH_NUM = @LNG_BENCH_NUM  ");            			
			strSql.Append(" where PK_DEVICE_MADE_NO=@PK_DEVICE_MADE_NO  ");
						
SqlParameter[] parameters = {
			            new SqlParameter("@PK_DEVICE_MADE_NO", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_DEVICE_ID", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_EQUIP_NAME", SqlDbType.Char,100) ,            
                        new SqlParameter("@AVR_EQUIP_MODEL", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_EQUIP_PARA", SqlDbType.Char,32) ,            
                        new SqlParameter("@AVR_EQUIP_TYPE", SqlDbType.Char,8) ,            
                        new SqlParameter("@AVR_FACTORY", SqlDbType.Char,100) ,            
                        new SqlParameter("@AVR_EQUIP_CLASS", SqlDbType.Char,8) ,            
                        new SqlParameter("@AVR_EQUIP_STANDARD", SqlDbType.Char,32) ,            
                        new SqlParameter("@LNG_BENCH_NUM", SqlDbType.Char,8)             
              
            };
						            
            parameters[0].Value = model.PK_DEVICE_MADE_NO;                        
            parameters[1].Value = model.AVR_DEVICE_ID;                        
            parameters[2].Value = model.AVR_EQUIP_NAME;                        
            parameters[3].Value = model.AVR_EQUIP_MODEL;                        
            parameters[4].Value = model.AVR_EQUIP_PARA;                        
            parameters[5].Value = model.AVR_EQUIP_TYPE;                        
            parameters[6].Value = model.AVR_FACTORY;                        
            parameters[7].Value = model.AVR_EQUIP_CLASS;                        
            parameters[8].Value = model.AVR_EQUIP_STANDARD;                        
            parameters[9].Value = model.LNG_BENCH_NUM;                        
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
		public bool Delete(string PK_DEVICE_MADE_NO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from DSPTCH_EQUIP_INFO ");
			strSql.Append(" where PK_DEVICE_MADE_NO=@PK_DEVICE_MADE_NO ");
						SqlParameter[] parameters = {
					new SqlParameter("@PK_DEVICE_MADE_NO", SqlDbType.Char,32)			};
			parameters[0].Value = PK_DEVICE_MADE_NO;


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
		public CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO GetModel(string PK_DEVICE_MADE_NO)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PK_DEVICE_MADE_NO, AVR_DEVICE_ID, AVR_EQUIP_NAME, AVR_EQUIP_MODEL, AVR_EQUIP_PARA, AVR_EQUIP_TYPE, AVR_FACTORY, AVR_EQUIP_CLASS, AVR_EQUIP_STANDARD, LNG_BENCH_NUM  ");			
			strSql.Append("  from DSPTCH_EQUIP_INFO ");
			strSql.Append(" where PK_DEVICE_MADE_NO=@PK_DEVICE_MADE_NO ");
						SqlParameter[] parameters = {
					new SqlParameter("@PK_DEVICE_MADE_NO", SqlDbType.Char,32)			};
			parameters[0].Value = PK_DEVICE_MADE_NO;

			
			CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO model=new CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.PK_DEVICE_MADE_NO= ds.Tables[0].Rows[0]["PK_DEVICE_MADE_NO"].ToString();
																																model.AVR_DEVICE_ID= ds.Tables[0].Rows[0]["AVR_DEVICE_ID"].ToString();
																																model.AVR_EQUIP_NAME= ds.Tables[0].Rows[0]["AVR_EQUIP_NAME"].ToString();
																																model.AVR_EQUIP_MODEL= ds.Tables[0].Rows[0]["AVR_EQUIP_MODEL"].ToString();
																																model.AVR_EQUIP_PARA= ds.Tables[0].Rows[0]["AVR_EQUIP_PARA"].ToString();
																																model.AVR_EQUIP_TYPE= ds.Tables[0].Rows[0]["AVR_EQUIP_TYPE"].ToString();
																																model.AVR_FACTORY= ds.Tables[0].Rows[0]["AVR_FACTORY"].ToString();
																																model.AVR_EQUIP_CLASS= ds.Tables[0].Rows[0]["AVR_EQUIP_CLASS"].ToString();
																																model.AVR_EQUIP_STANDARD= ds.Tables[0].Rows[0]["AVR_EQUIP_STANDARD"].ToString();
																																model.LNG_BENCH_NUM= ds.Tables[0].Rows[0]["LNG_BENCH_NUM"].ToString();
																										
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
			strSql.Append(" FROM DSPTCH_EQUIP_INFO ");
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
			strSql.Append(" FROM DSPTCH_EQUIP_INFO ");
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
		public List<CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO> GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM DSPTCH_EQUIP_INFO ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			DataSet ds = DbHelperSQL.Query(strSql.ToString());
			if(ds == null )
			{
				return null;
			}
			List<CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO> lst_M = new List<CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO>();
			foreach(DataRow row in ds.Tables[0].Rows)
			{
				CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO model=new CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO();
			
																model.PK_DEVICE_MADE_NO= row["PK_DEVICE_MADE_NO"].ToString();
																																model.AVR_DEVICE_ID= row["AVR_DEVICE_ID"].ToString();
																																model.AVR_EQUIP_NAME= row["AVR_EQUIP_NAME"].ToString();
																																model.AVR_EQUIP_MODEL= row["AVR_EQUIP_MODEL"].ToString();
																																model.AVR_EQUIP_PARA= row["AVR_EQUIP_PARA"].ToString();
																																model.AVR_EQUIP_TYPE= row["AVR_EQUIP_TYPE"].ToString();
																																model.AVR_FACTORY= row["AVR_FACTORY"].ToString();
																																model.AVR_EQUIP_CLASS= row["AVR_EQUIP_CLASS"].ToString();
																																model.AVR_EQUIP_STANDARD= row["AVR_EQUIP_STANDARD"].ToString();
																																model.LNG_BENCH_NUM= row["LNG_BENCH_NUM"].ToString();
																										
				lst_M.Add(model);
			
			}
			return lst_M;
		}

   
	}
}


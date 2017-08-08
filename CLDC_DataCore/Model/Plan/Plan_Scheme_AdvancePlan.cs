using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_AdvancePlan
		public partial class Plan_Scheme_AdvancePlan:Plan_Base
	{
		public Plan_Scheme_AdvancePlan(int TaiType, string vFAName)
                : base(CLDC_DataCore.Const.Variable.CONST_ACCESS_FANAME, TaiType, vFAName)
		{
			DbHelperOleDb.connectionString=PubConstant.GetConnectionString(_FAPath,"");
		}
		
		
		/// <summary>
		///  判断是否存在
		/// </summary>
		public bool Exists(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Scheme_AdvancePlan");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.Exists(strSql.ToString());
		}
				
		/// <summary>
		///  统计数据条目
		/// </summary>
		public object Stat(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Scheme_AdvancePlan");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(Scheme_AdvancePlan model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_AdvancePlan(");			
            strSql.Append("chrProjectName,intListNo,chrProjectNo,chrParameter,chrChecked");
			strSql.Append(") values (");
            strSql.Append("@chrProjectName,@intListNo,@chrProjectNo,@chrParameter,@chrChecked");            
            strSql.Append(") ");            
            		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@chrProjectName", OleDbType.VarChar,30) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrProjectNo", OleDbType.VarChar,2) ,            
                        new OleDbParameter("@chrParameter", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrChecked", OleDbType.VarChar,1)             
              
            };
			            
            parameters[0].Value = model.chrProjectName;                        
            parameters[1].Value = model.intListNo;                        
            parameters[2].Value = model.chrProjectNo;                        
            parameters[3].Value = model.chrParameter;                        
            parameters[4].Value = model.chrChecked;                        
			            DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
            			
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Scheme_AdvancePlan model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_AdvancePlan set ");
			                        
            strSql.Append(" chrProjectName = @chrProjectName , ");                                    
            strSql.Append(" intListNo = @intListNo , ");                                    
            strSql.Append(" chrProjectNo = @chrProjectNo , ");                                    
            strSql.Append(" chrParameter = @chrParameter , ");                                    
            strSql.Append(" chrChecked = @chrChecked  ");            			
			strSql.Append(" where  ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@chrProjectName", OleDbType.VarChar,30) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrProjectNo", OleDbType.VarChar,2) ,            
                        new OleDbParameter("@chrParameter", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrChecked", OleDbType.VarChar,1)             
              
            };
			            
            parameters[5].Value = model.chrProjectName;                        
            parameters[6].Value = model.intListNo;                        
            parameters[7].Value = model.chrProjectNo;                        
            parameters[8].Value = model.chrParameter;                        
            parameters[9].Value = model.chrChecked;                        
            int rows=DbHelperOleDb.ExecuteSql(strSql.ToString(),parameters);
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
		/// 删除数据
		/// </summary>
		public bool Delete(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Scheme_AdvancePlan ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			int rows=DbHelperOleDb.ExecuteSql(strSql.ToString());
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
		public Scheme_AdvancePlan GetModel()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select chrProjectName, intListNo, chrProjectNo, chrParameter, chrChecked  ");			
			strSql.Append("  from Scheme_AdvancePlan ");
			strSql.Append(" where ");
						OleDbParameter[] parameters = {
			};

			
			Scheme_AdvancePlan model=new Scheme_AdvancePlan();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
																model.chrProjectName= ds.Tables[0].Rows[0]["chrProjectName"].ToString();
																												if(ds.Tables[0].Rows[0]["intListNo"].ToString()!="")
				{
					model.intListNo=int.Parse(ds.Tables[0].Rows[0]["intListNo"].ToString());
				}
																																				model.chrProjectNo= ds.Tables[0].Rows[0]["chrProjectNo"].ToString();
																																model.chrParameter= ds.Tables[0].Rows[0]["chrParameter"].ToString();
																																model.chrChecked= ds.Tables[0].Rows[0]["chrChecked"].ToString();
																										
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
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM Scheme_AdvancePlan ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.Query(strSql.ToString());
		}
		
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" * ");
			strSql.Append(" FROM Scheme_AdvancePlan ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_AdvancePlan 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_AdvancePlan  Scheme_AdvancePlan)
		{
			    string strColumns = "";
				string strValues = "";
									if (Scheme_AdvancePlan.chrProjectName!=null && Scheme_AdvancePlan.chrProjectName.ToString().Trim() != "" )
					{
						strColumns += "chrProjectName,";
						strValues += "'"+Scheme_AdvancePlan.chrProjectName + "',";
					}
									if ( Scheme_AdvancePlan.intListNo.ToString().Trim() != "" )
					{
						strColumns += "intListNo,";
						strValues += "'"+Scheme_AdvancePlan.intListNo + "',";
					}
									if (Scheme_AdvancePlan.chrProjectNo!=null && Scheme_AdvancePlan.chrProjectNo.ToString().Trim() != "" )
					{
						strColumns += "chrProjectNo,";
						strValues += "'"+Scheme_AdvancePlan.chrProjectNo + "',";
					}
									if (Scheme_AdvancePlan.chrParameter!=null && Scheme_AdvancePlan.chrParameter.ToString().Trim() != "" )
					{
						strColumns += "chrParameter,";
						strValues += "'"+Scheme_AdvancePlan.chrParameter + "',";
					}
									if (Scheme_AdvancePlan.chrChecked!=null && Scheme_AdvancePlan.chrChecked.ToString().Trim() != "" )
					{
						strColumns += "chrChecked,";
						strValues += "'"+Scheme_AdvancePlan.chrChecked + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_AdvancePlan",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_AdvancePlan 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_AdvancePlan  Scheme_AdvancePlan,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if (Scheme_AdvancePlan.chrProjectName!=null && Scheme_AdvancePlan.chrProjectName.ToString().Trim() != "" )
						setValues += "chrProjectName='"+ Scheme_AdvancePlan .chrProjectName+"',";
								  	if ( Scheme_AdvancePlan.intListNo.ToString().Trim() != "" )
						setValues += "intListNo='"+ Scheme_AdvancePlan .intListNo+"',";
								  	if (Scheme_AdvancePlan.chrProjectNo!=null && Scheme_AdvancePlan.chrProjectNo.ToString().Trim() != "" )
						setValues += "chrProjectNo='"+ Scheme_AdvancePlan .chrProjectNo+"',";
								  	if (Scheme_AdvancePlan.chrParameter!=null && Scheme_AdvancePlan.chrParameter.ToString().Trim() != "" )
						setValues += "chrParameter='"+ Scheme_AdvancePlan .chrParameter+"',";
								  	if (Scheme_AdvancePlan.chrChecked!=null && Scheme_AdvancePlan.chrChecked.ToString().Trim() != "" )
						setValues += "chrChecked='"+ Scheme_AdvancePlan .chrChecked+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_AdvancePlan",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_AdvancePlan  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_AdvancePlan",strWhere);
				return _SqlText;
			}
	}
}


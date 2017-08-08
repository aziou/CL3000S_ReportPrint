using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_Dgn
		public partial class Plan_Scheme_Dgn:Plan_Base
	{
		public Plan_Scheme_Dgn(int TaiType, string vFAName)
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
			strSql.Append("select count(1) from Scheme_Dgn");
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
			strSql.Append("select count(1) from Scheme_Dgn");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Scheme_Dgn model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_Dgn(");			
            strSql.Append("chrProjectName,intListNo,chrProjectNo,chrCParameter,chrTParameter,chrChecked");
			strSql.Append(") values (");
            strSql.Append("@chrProjectName,@intListNo,@chrProjectNo,@chrCParameter,@chrTParameter,@chrChecked");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@chrProjectName", OleDbType.VarChar,30) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrProjectNo", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrCParameter", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrTParameter", OleDbType.VarChar,100) ,            
                        new OleDbParameter("@chrChecked", OleDbType.VarChar,1)             
              
            };
			            
            parameters[0].Value = model.chrProjectName;                        
            parameters[1].Value = model.intListNo;                        
            parameters[2].Value = model.chrProjectNo;                        
            parameters[3].Value = model.chrCParameter;                        
            parameters[4].Value = model.chrTParameter;                        
            parameters[5].Value = model.chrChecked;                        
			   
			object obj = DbHelperOleDb.GetSingle(strSql.ToString(),parameters);			
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
		public bool Update(Scheme_Dgn model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_Dgn set ");
			                                                
            strSql.Append(" chrProjectName = @chrProjectName , ");                                    
            strSql.Append(" intListNo = @intListNo , ");                                    
            strSql.Append(" chrProjectNo = @chrProjectNo , ");                                    
            strSql.Append(" chrCParameter = @chrCParameter , ");                                    
            strSql.Append(" chrTParameter = @chrTParameter , ");                                    
            strSql.Append(" chrChecked = @chrChecked  ");            			
			strSql.Append(" where schemeID=@schemeID ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@schemeID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrProjectName", OleDbType.VarChar,30) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrProjectNo", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrCParameter", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrTParameter", OleDbType.VarChar,100) ,            
                        new OleDbParameter("@chrChecked", OleDbType.VarChar,1)             
              
            };
			            
            parameters[6].Value = model.schemeID;                        
            parameters[7].Value = model.chrProjectName;                        
            parameters[8].Value = model.intListNo;                        
            parameters[9].Value = model.chrProjectNo;                        
            parameters[10].Value = model.chrCParameter;                        
            parameters[11].Value = model.chrTParameter;                        
            parameters[12].Value = model.chrChecked;                        
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
			strSql.Append("delete from Scheme_Dgn ");
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
		public Scheme_Dgn GetModel(int schemeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select schemeID, chrProjectName, intListNo, chrProjectNo, chrCParameter, chrTParameter, chrChecked  ");			
			strSql.Append("  from Scheme_Dgn ");
			strSql.Append(" where schemeID=@schemeID");
						OleDbParameter[] parameters = {
					new OleDbParameter("@schemeID", OleDbType.Integer,4)
			};
			parameters[0].Value = schemeID;

			
			Scheme_Dgn model=new Scheme_Dgn();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["schemeID"].ToString()!="")
				{
					model.schemeID=int.Parse(ds.Tables[0].Rows[0]["schemeID"].ToString());
				}
																																				model.chrProjectName= ds.Tables[0].Rows[0]["chrProjectName"].ToString();
																												if(ds.Tables[0].Rows[0]["intListNo"].ToString()!="")
				{
					model.intListNo=int.Parse(ds.Tables[0].Rows[0]["intListNo"].ToString());
				}
																																				model.chrProjectNo= ds.Tables[0].Rows[0]["chrProjectNo"].ToString();
																																model.chrCParameter= ds.Tables[0].Rows[0]["chrCParameter"].ToString();
																																model.chrTParameter= ds.Tables[0].Rows[0]["chrTParameter"].ToString();
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
			strSql.Append(" FROM Scheme_Dgn ");
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
			strSql.Append(" FROM Scheme_Dgn ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_Dgn 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_Dgn  Scheme_Dgn)
		{
			    string strColumns = "";
				string strValues = "";
									if ( Scheme_Dgn.schemeID.ToString().Trim() != "" )
					{
						strColumns += "schemeID,";
						strValues += "'"+Scheme_Dgn.schemeID + "',";
					}
									if (Scheme_Dgn.chrProjectName!=null && Scheme_Dgn.chrProjectName.ToString().Trim() != "" )
					{
						strColumns += "chrProjectName,";
						strValues += "'"+Scheme_Dgn.chrProjectName + "',";
					}
									if ( Scheme_Dgn.intListNo.ToString().Trim() != "" )
					{
						strColumns += "intListNo,";
						strValues += "'"+Scheme_Dgn.intListNo + "',";
					}
									if (Scheme_Dgn.chrProjectNo!=null && Scheme_Dgn.chrProjectNo.ToString().Trim() != "" )
					{
						strColumns += "chrProjectNo,";
						strValues += "'"+Scheme_Dgn.chrProjectNo + "',";
					}
									if (Scheme_Dgn.chrCParameter!=null && Scheme_Dgn.chrCParameter.ToString().Trim() != "" )
					{
						strColumns += "chrCParameter,";
						strValues += "'"+Scheme_Dgn.chrCParameter + "',";
					}
									if (Scheme_Dgn.chrTParameter!=null && Scheme_Dgn.chrTParameter.ToString().Trim() != "" )
					{
						strColumns += "chrTParameter,";
						strValues += "'"+Scheme_Dgn.chrTParameter + "',";
					}
									if (Scheme_Dgn.chrChecked!=null && Scheme_Dgn.chrChecked.ToString().Trim() != "" )
					{
						strColumns += "chrChecked,";
						strValues += "'"+Scheme_Dgn.chrChecked + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_Dgn",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_Dgn 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_Dgn  Scheme_Dgn,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if ( Scheme_Dgn.schemeID.ToString().Trim() != "" )
						setValues += "schemeID='"+ Scheme_Dgn .schemeID+"',";
								  	if (Scheme_Dgn.chrProjectName!=null && Scheme_Dgn.chrProjectName.ToString().Trim() != "" )
						setValues += "chrProjectName='"+ Scheme_Dgn .chrProjectName+"',";
								  	if ( Scheme_Dgn.intListNo.ToString().Trim() != "" )
						setValues += "intListNo='"+ Scheme_Dgn .intListNo+"',";
								  	if (Scheme_Dgn.chrProjectNo!=null && Scheme_Dgn.chrProjectNo.ToString().Trim() != "" )
						setValues += "chrProjectNo='"+ Scheme_Dgn .chrProjectNo+"',";
								  	if (Scheme_Dgn.chrCParameter!=null && Scheme_Dgn.chrCParameter.ToString().Trim() != "" )
						setValues += "chrCParameter='"+ Scheme_Dgn .chrCParameter+"',";
								  	if (Scheme_Dgn.chrTParameter!=null && Scheme_Dgn.chrTParameter.ToString().Trim() != "" )
						setValues += "chrTParameter='"+ Scheme_Dgn .chrTParameter+"',";
								  	if (Scheme_Dgn.chrChecked!=null && Scheme_Dgn.chrChecked.ToString().Trim() != "" )
						setValues += "chrChecked='"+ Scheme_Dgn .chrChecked+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_Dgn",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_Dgn  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_Dgn",strWhere);
				return _SqlText;
			}
	}
}


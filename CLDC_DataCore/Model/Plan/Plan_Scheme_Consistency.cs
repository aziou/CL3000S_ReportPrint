using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_Consistency
		public partial class Plan_Scheme_Consistency:Plan_Base
	{
		public Plan_Scheme_Consistency(int TaiType, string vFAName)
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
			strSql.Append("select count(1) from Scheme_Consistency");
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
			strSql.Append("select count(1) from Scheme_Consistency");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Scheme_Consistency model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_Consistency(");			
            strSql.Append("intListNo,chrGrpType,intItemType,intItemNo,chrTParameter");
			strSql.Append(") values (");
            strSql.Append("@intListNo,@chrGrpType,@intItemType,@intItemNo,@chrTParameter");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrGrpType", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@intItemType", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intItemNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrTParameter", OleDbType.VarChar,255)             
              
            };
			            
            parameters[0].Value = model.intListNo;                        
            parameters[1].Value = model.chrGrpType;                        
            parameters[2].Value = model.intItemType;                        
            parameters[3].Value = model.intItemNo;                        
            parameters[4].Value = model.chrTParameter;                        
			   
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
		public bool Update(Scheme_Consistency model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_Consistency set ");
			                                                
            strSql.Append(" intListNo = @intListNo , ");                                    
            strSql.Append(" chrGrpType = @chrGrpType , ");                                    
            strSql.Append(" intItemType = @intItemType , ");                                    
            strSql.Append(" intItemNo = @intItemNo , ");                                    
            strSql.Append(" chrTParameter = @chrTParameter  ");            			
			strSql.Append(" where schemeID=@schemeID ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@schemeID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrGrpType", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@intItemType", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intItemNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrTParameter", OleDbType.VarChar,255)             
              
            };
			            
            parameters[5].Value = model.schemeID;                        
            parameters[6].Value = model.intListNo;                        
            parameters[7].Value = model.chrGrpType;                        
            parameters[8].Value = model.intItemType;                        
            parameters[9].Value = model.intItemNo;                        
            parameters[10].Value = model.chrTParameter;                        
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
			strSql.Append("delete from Scheme_Consistency ");
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
		public Scheme_Consistency GetModel(int schemeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select schemeID, intListNo, chrGrpType, intItemType, intItemNo, chrTParameter  ");			
			strSql.Append("  from Scheme_Consistency ");
			strSql.Append(" where schemeID=@schemeID");
						OleDbParameter[] parameters = {
					new OleDbParameter("@schemeID", OleDbType.Integer,4)
			};
			parameters[0].Value = schemeID;

			
			Scheme_Consistency model=new Scheme_Consistency();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["schemeID"].ToString()!="")
				{
					model.schemeID=int.Parse(ds.Tables[0].Rows[0]["schemeID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["intListNo"].ToString()!="")
				{
					model.intListNo=int.Parse(ds.Tables[0].Rows[0]["intListNo"].ToString());
				}
																																				model.chrGrpType= ds.Tables[0].Rows[0]["chrGrpType"].ToString();
																												if(ds.Tables[0].Rows[0]["intItemType"].ToString()!="")
				{
					model.intItemType=int.Parse(ds.Tables[0].Rows[0]["intItemType"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["intItemNo"].ToString()!="")
				{
					model.intItemNo=int.Parse(ds.Tables[0].Rows[0]["intItemNo"].ToString());
				}
																																				model.chrTParameter= ds.Tables[0].Rows[0]["chrTParameter"].ToString();
																										
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
			strSql.Append(" FROM Scheme_Consistency ");
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
			strSql.Append(" FROM Scheme_Consistency ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_Consistency 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_Consistency  Scheme_Consistency)
		{
			    string strColumns = "";
				string strValues = "";
									if ( Scheme_Consistency.schemeID.ToString().Trim() != "" )
					{
						strColumns += "schemeID,";
						strValues += "'"+Scheme_Consistency.schemeID + "',";
					}
									if ( Scheme_Consistency.intListNo.ToString().Trim() != "" )
					{
						strColumns += "intListNo,";
						strValues += "'"+Scheme_Consistency.intListNo + "',";
					}
									if (Scheme_Consistency.chrGrpType!=null && Scheme_Consistency.chrGrpType.ToString().Trim() != "" )
					{
						strColumns += "chrGrpType,";
						strValues += "'"+Scheme_Consistency.chrGrpType + "',";
					}
									if ( Scheme_Consistency.intItemType.ToString().Trim() != "" )
					{
						strColumns += "intItemType,";
						strValues += "'"+Scheme_Consistency.intItemType + "',";
					}
									if ( Scheme_Consistency.intItemNo.ToString().Trim() != "" )
					{
						strColumns += "intItemNo,";
						strValues += "'"+Scheme_Consistency.intItemNo + "',";
					}
									if (Scheme_Consistency.chrTParameter!=null && Scheme_Consistency.chrTParameter.ToString().Trim() != "" )
					{
						strColumns += "chrTParameter,";
						strValues += "'"+Scheme_Consistency.chrTParameter + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_Consistency",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_Consistency 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_Consistency  Scheme_Consistency,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if ( Scheme_Consistency.schemeID.ToString().Trim() != "" )
						setValues += "schemeID='"+ Scheme_Consistency .schemeID+"',";
								  	if ( Scheme_Consistency.intListNo.ToString().Trim() != "" )
						setValues += "intListNo='"+ Scheme_Consistency .intListNo+"',";
								  	if (Scheme_Consistency.chrGrpType!=null && Scheme_Consistency.chrGrpType.ToString().Trim() != "" )
						setValues += "chrGrpType='"+ Scheme_Consistency .chrGrpType+"',";
								  	if ( Scheme_Consistency.intItemType.ToString().Trim() != "" )
						setValues += "intItemType='"+ Scheme_Consistency .intItemType+"',";
								  	if ( Scheme_Consistency.intItemNo.ToString().Trim() != "" )
						setValues += "intItemNo='"+ Scheme_Consistency .intItemNo+"',";
								  	if (Scheme_Consistency.chrTParameter!=null && Scheme_Consistency.chrTParameter.ToString().Trim() != "" )
						setValues += "chrTParameter='"+ Scheme_Consistency .chrTParameter+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_Consistency",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_Consistency  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_Consistency",strWhere);
				return _SqlText;
			}
	}
}


using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_FK
		public partial class Plan_Scheme_FK:Plan_Base
	{
		public Plan_Scheme_FK(int TaiType, string vFAName)
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
			strSql.Append("select count(1) from Scheme_FK");
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
			strSql.Append("select count(1) from Scheme_FK");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Scheme_FK model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_FK(");			
            strSql.Append("chrProjectName,intListNo,chrGrpType,intItemType,chrCParameter,chrTParameter,chrChecked");
			strSql.Append(") values (");
            strSql.Append("@chrProjectName,@intListNo,@chrGrpType,@intItemType,@chrCParameter,@chrTParameter,@chrChecked");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@chrProjectName", OleDbType.VarChar,30) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrGrpType", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@intItemType", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrCParameter", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrTParameter", OleDbType.VarChar,200) ,            
                        new OleDbParameter("@chrChecked", OleDbType.VarChar,1)             
              
            };
			            
            parameters[0].Value = model.chrProjectName;                        
            parameters[1].Value = model.intListNo;                        
            parameters[2].Value = model.chrGrpType;                        
            parameters[3].Value = model.intItemType;                        
            parameters[4].Value = model.chrCParameter;                        
            parameters[5].Value = model.chrTParameter;                        
            parameters[6].Value = model.chrChecked;                        
			   
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
		public bool Update(Scheme_FK model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_FK set ");
			                                                
            strSql.Append(" chrProjectName = @chrProjectName , ");                                    
            strSql.Append(" intListNo = @intListNo , ");                                    
            strSql.Append(" chrGrpType = @chrGrpType , ");                                    
            strSql.Append(" intItemType = @intItemType , ");                                    
            strSql.Append(" chrCParameter = @chrCParameter , ");                                    
            strSql.Append(" chrTParameter = @chrTParameter , ");                                    
            strSql.Append(" chrChecked = @chrChecked  ");            			
			strSql.Append(" where schemeID=@schemeID ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@schemeID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrProjectName", OleDbType.VarChar,30) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrGrpType", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@intItemType", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrCParameter", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrTParameter", OleDbType.VarChar,200) ,            
                        new OleDbParameter("@chrChecked", OleDbType.VarChar,1)             
              
            };
			            
            parameters[7].Value = model.schemeID;                        
            parameters[8].Value = model.chrProjectName;                        
            parameters[9].Value = model.intListNo;                        
            parameters[10].Value = model.chrGrpType;                        
            parameters[11].Value = model.intItemType;                        
            parameters[12].Value = model.chrCParameter;                        
            parameters[13].Value = model.chrTParameter;                        
            parameters[14].Value = model.chrChecked;                        
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
			strSql.Append("delete from Scheme_FK ");
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
		public Scheme_FK GetModel(int schemeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select schemeID, chrProjectName, intListNo, chrGrpType, intItemType, chrCParameter, chrTParameter, chrChecked  ");			
			strSql.Append("  from Scheme_FK ");
			strSql.Append(" where schemeID=@schemeID");
						OleDbParameter[] parameters = {
					new OleDbParameter("@schemeID", OleDbType.Integer,4)
			};
			parameters[0].Value = schemeID;

			
			Scheme_FK model=new Scheme_FK();
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
																																				model.chrGrpType= ds.Tables[0].Rows[0]["chrGrpType"].ToString();
																												if(ds.Tables[0].Rows[0]["intItemType"].ToString()!="")
				{
					model.intItemType=int.Parse(ds.Tables[0].Rows[0]["intItemType"].ToString());
				}
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
			strSql.Append(" FROM Scheme_FK ");
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
			strSql.Append(" FROM Scheme_FK ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_FK 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_FK  Scheme_FK)
		{
			    string strColumns = "";
				string strValues = "";
									if ( Scheme_FK.schemeID.ToString().Trim() != "" )
					{
						strColumns += "schemeID,";
						strValues += "'"+Scheme_FK.schemeID + "',";
					}
									if (Scheme_FK.chrProjectName!=null && Scheme_FK.chrProjectName.ToString().Trim() != "" )
					{
						strColumns += "chrProjectName,";
						strValues += "'"+Scheme_FK.chrProjectName + "',";
					}
									if ( Scheme_FK.intListNo.ToString().Trim() != "" )
					{
						strColumns += "intListNo,";
						strValues += "'"+Scheme_FK.intListNo + "',";
					}
									if (Scheme_FK.chrGrpType!=null && Scheme_FK.chrGrpType.ToString().Trim() != "" )
					{
						strColumns += "chrGrpType,";
						strValues += "'"+Scheme_FK.chrGrpType + "',";
					}
									if ( Scheme_FK.intItemType.ToString().Trim() != "" )
					{
						strColumns += "intItemType,";
						strValues += "'"+Scheme_FK.intItemType + "',";
					}
									if (Scheme_FK.chrCParameter!=null && Scheme_FK.chrCParameter.ToString().Trim() != "" )
					{
						strColumns += "chrCParameter,";
						strValues += "'"+Scheme_FK.chrCParameter + "',";
					}
									if (Scheme_FK.chrTParameter!=null && Scheme_FK.chrTParameter.ToString().Trim() != "" )
					{
						strColumns += "chrTParameter,";
						strValues += "'"+Scheme_FK.chrTParameter + "',";
					}
									if (Scheme_FK.chrChecked!=null && Scheme_FK.chrChecked.ToString().Trim() != "" )
					{
						strColumns += "chrChecked,";
						strValues += "'"+Scheme_FK.chrChecked + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_FK",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_FK 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_FK  Scheme_FK,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if ( Scheme_FK.schemeID.ToString().Trim() != "" )
						setValues += "schemeID='"+ Scheme_FK .schemeID+"',";
								  	if (Scheme_FK.chrProjectName!=null && Scheme_FK.chrProjectName.ToString().Trim() != "" )
						setValues += "chrProjectName='"+ Scheme_FK .chrProjectName+"',";
								  	if ( Scheme_FK.intListNo.ToString().Trim() != "" )
						setValues += "intListNo='"+ Scheme_FK .intListNo+"',";
								  	if (Scheme_FK.chrGrpType!=null && Scheme_FK.chrGrpType.ToString().Trim() != "" )
						setValues += "chrGrpType='"+ Scheme_FK .chrGrpType+"',";
								  	if ( Scheme_FK.intItemType.ToString().Trim() != "" )
						setValues += "intItemType='"+ Scheme_FK .intItemType+"',";
								  	if (Scheme_FK.chrCParameter!=null && Scheme_FK.chrCParameter.ToString().Trim() != "" )
						setValues += "chrCParameter='"+ Scheme_FK .chrCParameter+"',";
								  	if (Scheme_FK.chrTParameter!=null && Scheme_FK.chrTParameter.ToString().Trim() != "" )
						setValues += "chrTParameter='"+ Scheme_FK .chrTParameter+"',";
								  	if (Scheme_FK.chrChecked!=null && Scheme_FK.chrChecked.ToString().Trim() != "" )
						setValues += "chrChecked='"+ Scheme_FK .chrChecked+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_FK",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_FK  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_FK",strWhere);
				return _SqlText;
			}
	}
}


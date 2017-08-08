using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_Grp
		public partial class Plan_Scheme_Grp:Plan_Base
	{
		public Plan_Scheme_Grp(int TaiType, string vFAName)
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
			strSql.Append("select count(1) from Scheme_Grp");
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
			strSql.Append("select count(1) from Scheme_Grp");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Scheme_Grp model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_Grp(");			
            strSql.Append("chrGrpType,iListNo,chrGrpName,chrCheck");
			strSql.Append(") values (");
            strSql.Append("@chrGrpType,@iListNo,@chrGrpName,@chrCheck");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@chrGrpType", OleDbType.VarChar,8) ,            
                        new OleDbParameter("@iListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrGrpName", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrCheck", OleDbType.VarChar,1)             
              
            };
			            
            parameters[0].Value = model.chrGrpType;                        
            parameters[1].Value = model.iListNo;                        
            parameters[2].Value = model.chrGrpName;                        
            parameters[3].Value = model.chrCheck;                        
			   
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
		public bool Update(Scheme_Grp model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_Grp set ");
			                                                
            strSql.Append(" chrGrpType = @chrGrpType , ");                                    
            strSql.Append(" iListNo = @iListNo , ");                                    
            strSql.Append(" chrGrpName = @chrGrpName , ");                                    
            strSql.Append(" chrCheck = @chrCheck  ");            			
			strSql.Append(" where schemeID=@schemeID ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@schemeID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrGrpType", OleDbType.VarChar,8) ,            
                        new OleDbParameter("@iListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrGrpName", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrCheck", OleDbType.VarChar,1)             
              
            };
			            
            parameters[4].Value = model.schemeID;                        
            parameters[5].Value = model.chrGrpType;                        
            parameters[6].Value = model.iListNo;                        
            parameters[7].Value = model.chrGrpName;                        
            parameters[8].Value = model.chrCheck;                        
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
			strSql.Append("delete from Scheme_Grp ");
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
		public Scheme_Grp GetModel(int schemeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select schemeID, chrGrpType, iListNo, chrGrpName, chrCheck  ");			
			strSql.Append("  from Scheme_Grp ");
			strSql.Append(" where schemeID=@schemeID");
						OleDbParameter[] parameters = {
					new OleDbParameter("@schemeID", OleDbType.Integer,4)
			};
			parameters[0].Value = schemeID;

			
			Scheme_Grp model=new Scheme_Grp();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["schemeID"].ToString()!="")
				{
					model.schemeID=int.Parse(ds.Tables[0].Rows[0]["schemeID"].ToString());
				}
																																				model.chrGrpType= ds.Tables[0].Rows[0]["chrGrpType"].ToString();
																												if(ds.Tables[0].Rows[0]["iListNo"].ToString()!="")
				{
					model.iListNo=int.Parse(ds.Tables[0].Rows[0]["iListNo"].ToString());
				}
																																				model.chrGrpName= ds.Tables[0].Rows[0]["chrGrpName"].ToString();
																																model.chrCheck= ds.Tables[0].Rows[0]["chrCheck"].ToString();
																										
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
			strSql.Append(" FROM Scheme_Grp ");
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
			strSql.Append(" FROM Scheme_Grp ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_Grp 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_Grp  Scheme_Grp)
		{
			    string strColumns = "";
				string strValues = "";
									if ( Scheme_Grp.schemeID.ToString().Trim() != "" )
					{
						strColumns += "schemeID,";
						strValues += "'"+Scheme_Grp.schemeID + "',";
					}
									if (Scheme_Grp.chrGrpType!=null && Scheme_Grp.chrGrpType.ToString().Trim() != "" )
					{
						strColumns += "chrGrpType,";
						strValues += "'"+Scheme_Grp.chrGrpType + "',";
					}
									if ( Scheme_Grp.iListNo.ToString().Trim() != "" )
					{
						strColumns += "iListNo,";
						strValues += "'"+Scheme_Grp.iListNo + "',";
					}
									if (Scheme_Grp.chrGrpName!=null && Scheme_Grp.chrGrpName.ToString().Trim() != "" )
					{
						strColumns += "chrGrpName,";
						strValues += "'"+Scheme_Grp.chrGrpName + "',";
					}
									if (Scheme_Grp.chrCheck!=null && Scheme_Grp.chrCheck.ToString().Trim() != "" )
					{
						strColumns += "chrCheck,";
						strValues += "'"+Scheme_Grp.chrCheck + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_Grp",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_Grp 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_Grp  Scheme_Grp,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if ( Scheme_Grp.schemeID.ToString().Trim() != "" )
						setValues += "schemeID='"+ Scheme_Grp .schemeID+"',";
								  	if (Scheme_Grp.chrGrpType!=null && Scheme_Grp.chrGrpType.ToString().Trim() != "" )
						setValues += "chrGrpType='"+ Scheme_Grp .chrGrpType+"',";
								  	if ( Scheme_Grp.iListNo.ToString().Trim() != "" )
						setValues += "iListNo='"+ Scheme_Grp .iListNo+"',";
								  	if (Scheme_Grp.chrGrpName!=null && Scheme_Grp.chrGrpName.ToString().Trim() != "" )
						setValues += "chrGrpName='"+ Scheme_Grp .chrGrpName+"',";
								  	if (Scheme_Grp.chrCheck!=null && Scheme_Grp.chrCheck.ToString().Trim() != "" )
						setValues += "chrCheck='"+ Scheme_Grp .chrCheck+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_Grp",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_Grp  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_Grp",strWhere);
				return _SqlText;
			}
	}
}


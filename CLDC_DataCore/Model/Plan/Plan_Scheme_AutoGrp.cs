using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_AutoGrp
		public partial class Plan_Scheme_AutoGrp:Plan_Base
	{
		public Plan_Scheme_AutoGrp(int TaiType, string vFAName)
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
			strSql.Append("select count(1) from Scheme_AutoGrp");
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
			strSql.Append("select count(1) from Scheme_AutoGrp");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Scheme_AutoGrp model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_AutoGrp(");			
            strSql.Append("intListNo,chrGrpType,intItemTypeOrPrjNo,chrSpecOrDataName");
			strSql.Append(") values (");
            strSql.Append("@intListNo,@chrGrpType,@intItemTypeOrPrjNo,@chrSpecOrDataName");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrGrpType", OleDbType.VarChar,8) ,            
                        new OleDbParameter("@intItemTypeOrPrjNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrSpecOrDataName", OleDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.intListNo;                        
            parameters[1].Value = model.chrGrpType;                        
            parameters[2].Value = model.intItemTypeOrPrjNo;                        
            parameters[3].Value = model.chrSpecOrDataName;                        
			   
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
		public bool Update(Scheme_AutoGrp model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_AutoGrp set ");
			                                                
            strSql.Append(" intListNo = @intListNo , ");                                    
            strSql.Append(" chrGrpType = @chrGrpType , ");                                    
            strSql.Append(" intItemTypeOrPrjNo = @intItemTypeOrPrjNo , ");                                    
            strSql.Append(" chrSpecOrDataName = @chrSpecOrDataName  ");            			
			strSql.Append(" where schemeID=@schemeID ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@schemeID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrGrpType", OleDbType.VarChar,8) ,            
                        new OleDbParameter("@intItemTypeOrPrjNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrSpecOrDataName", OleDbType.VarChar,50)             
              
            };
			            
            parameters[4].Value = model.schemeID;                        
            parameters[5].Value = model.intListNo;                        
            parameters[6].Value = model.chrGrpType;                        
            parameters[7].Value = model.intItemTypeOrPrjNo;                        
            parameters[8].Value = model.chrSpecOrDataName;                        
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
			strSql.Append("delete from Scheme_AutoGrp ");
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
		public Scheme_AutoGrp GetModel(int schemeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select schemeID, intListNo, chrGrpType, intItemTypeOrPrjNo, chrSpecOrDataName  ");			
			strSql.Append("  from Scheme_AutoGrp ");
			strSql.Append(" where schemeID=@schemeID");
						OleDbParameter[] parameters = {
					new OleDbParameter("@schemeID", OleDbType.Integer,4)
			};
			parameters[0].Value = schemeID;

			
			Scheme_AutoGrp model=new Scheme_AutoGrp();
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
																												if(ds.Tables[0].Rows[0]["intItemTypeOrPrjNo"].ToString()!="")
				{
					model.intItemTypeOrPrjNo=int.Parse(ds.Tables[0].Rows[0]["intItemTypeOrPrjNo"].ToString());
				}
																																				model.chrSpecOrDataName= ds.Tables[0].Rows[0]["chrSpecOrDataName"].ToString();
																										
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
			strSql.Append(" FROM Scheme_AutoGrp ");
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
			strSql.Append(" FROM Scheme_AutoGrp ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_AutoGrp 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_AutoGrp  Scheme_AutoGrp)
		{
			    string strColumns = "";
				string strValues = "";
									if ( Scheme_AutoGrp.schemeID.ToString().Trim() != "" )
					{
						strColumns += "schemeID,";
						strValues += "'"+Scheme_AutoGrp.schemeID + "',";
					}
									if ( Scheme_AutoGrp.intListNo.ToString().Trim() != "" )
					{
						strColumns += "intListNo,";
						strValues += "'"+Scheme_AutoGrp.intListNo + "',";
					}
									if (Scheme_AutoGrp.chrGrpType!=null && Scheme_AutoGrp.chrGrpType.ToString().Trim() != "" )
					{
						strColumns += "chrGrpType,";
						strValues += "'"+Scheme_AutoGrp.chrGrpType + "',";
					}
									if ( Scheme_AutoGrp.intItemTypeOrPrjNo.ToString().Trim() != "" )
					{
						strColumns += "intItemTypeOrPrjNo,";
						strValues += "'"+Scheme_AutoGrp.intItemTypeOrPrjNo + "',";
					}
									if (Scheme_AutoGrp.chrSpecOrDataName!=null && Scheme_AutoGrp.chrSpecOrDataName.ToString().Trim() != "" )
					{
						strColumns += "chrSpecOrDataName,";
						strValues += "'"+Scheme_AutoGrp.chrSpecOrDataName + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_AutoGrp",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_AutoGrp 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_AutoGrp  Scheme_AutoGrp,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if ( Scheme_AutoGrp.schemeID.ToString().Trim() != "" )
						setValues += "schemeID='"+ Scheme_AutoGrp .schemeID+"',";
								  	if ( Scheme_AutoGrp.intListNo.ToString().Trim() != "" )
						setValues += "intListNo='"+ Scheme_AutoGrp .intListNo+"',";
								  	if (Scheme_AutoGrp.chrGrpType!=null && Scheme_AutoGrp.chrGrpType.ToString().Trim() != "" )
						setValues += "chrGrpType='"+ Scheme_AutoGrp .chrGrpType+"',";
								  	if ( Scheme_AutoGrp.intItemTypeOrPrjNo.ToString().Trim() != "" )
						setValues += "intItemTypeOrPrjNo='"+ Scheme_AutoGrp .intItemTypeOrPrjNo+"',";
								  	if (Scheme_AutoGrp.chrSpecOrDataName!=null && Scheme_AutoGrp.chrSpecOrDataName.ToString().Trim() != "" )
						setValues += "chrSpecOrDataName='"+ Scheme_AutoGrp .chrSpecOrDataName+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_AutoGrp",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_AutoGrp  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_AutoGrp",strWhere);
				return _SqlText;
			}
	}
}


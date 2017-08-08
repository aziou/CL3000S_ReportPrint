using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_DLTData
		public partial class Plan_Scheme_DLTData:Plan_Base
	{
		public Plan_Scheme_DLTData(int TaiType, string vFAName)
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
			strSql.Append("select count(1) from Scheme_DLTData");
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
			strSql.Append("select count(1) from Scheme_DLTData");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Scheme_DLTData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_DLTData(");			
            strSql.Append("intItemID,dltID,intListNo,chrItemName,chrID,intLength,intDot,chrFormat,intType,chrValue");
			strSql.Append(") values (");
            strSql.Append("@intItemID,@dltID,@intListNo,@chrItemName,@chrID,@intLength,@intDot,@chrFormat,@intType,@chrValue");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@intItemID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@dltID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrItemName", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrID", OleDbType.VarChar,8) ,            
                        new OleDbParameter("@intLength", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intDot", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrFormat", OleDbType.VarChar,100) ,            
                        new OleDbParameter("@intType", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrValue", OleDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.intItemID;                        
            parameters[1].Value = model.dltID;                        
            parameters[2].Value = model.intListNo;                        
            parameters[3].Value = model.chrItemName;                        
            parameters[4].Value = model.chrID;                        
            parameters[5].Value = model.intLength;                        
            parameters[6].Value = model.intDot;                        
            parameters[7].Value = model.chrFormat;                        
            parameters[8].Value = model.intType;                        
            parameters[9].Value = model.chrValue;                        
			   
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
		public bool Update(Scheme_DLTData model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_DLTData set ");
			                                                
            strSql.Append(" intItemID = @intItemID , ");                                    
            strSql.Append(" dltID = @dltID , ");                                    
            strSql.Append(" intListNo = @intListNo , ");                                    
            strSql.Append(" chrItemName = @chrItemName , ");                                    
            strSql.Append(" chrID = @chrID , ");                                    
            strSql.Append(" intLength = @intLength , ");                                    
            strSql.Append(" intDot = @intDot , ");                                    
            strSql.Append(" chrFormat = @chrFormat , ");                                    
            strSql.Append(" intType = @intType , ");                                    
            strSql.Append(" chrValue = @chrValue  ");            			
			strSql.Append(" where schemeID=@schemeID ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@schemeID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intItemID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@dltID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrItemName", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrID", OleDbType.VarChar,8) ,            
                        new OleDbParameter("@intLength", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intDot", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrFormat", OleDbType.VarChar,100) ,            
                        new OleDbParameter("@intType", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrValue", OleDbType.VarChar,50)             
              
            };
			            
            parameters[10].Value = model.schemeID;                        
            parameters[11].Value = model.intItemID;                        
            parameters[12].Value = model.dltID;                        
            parameters[13].Value = model.intListNo;                        
            parameters[14].Value = model.chrItemName;                        
            parameters[15].Value = model.chrID;                        
            parameters[16].Value = model.intLength;                        
            parameters[17].Value = model.intDot;                        
            parameters[18].Value = model.chrFormat;                        
            parameters[19].Value = model.intType;                        
            parameters[20].Value = model.chrValue;                        
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
			strSql.Append("delete from Scheme_DLTData ");
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
		public Scheme_DLTData GetModel(int schemeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select schemeID, intItemID, dltID, intListNo, chrItemName, chrID, intLength, intDot, chrFormat, intType, chrValue  ");			
			strSql.Append("  from Scheme_DLTData ");
			strSql.Append(" where schemeID=@schemeID");
						OleDbParameter[] parameters = {
					new OleDbParameter("@schemeID", OleDbType.Integer,4)
			};
			parameters[0].Value = schemeID;

			
			Scheme_DLTData model=new Scheme_DLTData();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["schemeID"].ToString()!="")
				{
					model.schemeID=int.Parse(ds.Tables[0].Rows[0]["schemeID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["intItemID"].ToString()!="")
				{
					model.intItemID=int.Parse(ds.Tables[0].Rows[0]["intItemID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["dltID"].ToString()!="")
				{
					model.dltID=int.Parse(ds.Tables[0].Rows[0]["dltID"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["intListNo"].ToString()!="")
				{
					model.intListNo=int.Parse(ds.Tables[0].Rows[0]["intListNo"].ToString());
				}
																																				model.chrItemName= ds.Tables[0].Rows[0]["chrItemName"].ToString();
																																model.chrID= ds.Tables[0].Rows[0]["chrID"].ToString();
																												if(ds.Tables[0].Rows[0]["intLength"].ToString()!="")
				{
					model.intLength=int.Parse(ds.Tables[0].Rows[0]["intLength"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["intDot"].ToString()!="")
				{
					model.intDot=int.Parse(ds.Tables[0].Rows[0]["intDot"].ToString());
				}
																																				model.chrFormat= ds.Tables[0].Rows[0]["chrFormat"].ToString();
																												if(ds.Tables[0].Rows[0]["intType"].ToString()!="")
				{
					model.intType=int.Parse(ds.Tables[0].Rows[0]["intType"].ToString());
				}
																																				model.chrValue= ds.Tables[0].Rows[0]["chrValue"].ToString();
																										
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
			strSql.Append(" FROM Scheme_DLTData ");
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
			strSql.Append(" FROM Scheme_DLTData ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_DLTData 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_DLTData  Scheme_DLTData)
		{
			    string strColumns = "";
				string strValues = "";
									if ( Scheme_DLTData.schemeID.ToString().Trim() != "" )
					{
						strColumns += "schemeID,";
						strValues += "'"+Scheme_DLTData.schemeID + "',";
					}
									if ( Scheme_DLTData.intItemID.ToString().Trim() != "" )
					{
						strColumns += "intItemID,";
						strValues += "'"+Scheme_DLTData.intItemID + "',";
					}
									if ( Scheme_DLTData.dltID.ToString().Trim() != "" )
					{
						strColumns += "dltID,";
						strValues += "'"+Scheme_DLTData.dltID + "',";
					}
									if ( Scheme_DLTData.intListNo.ToString().Trim() != "" )
					{
						strColumns += "intListNo,";
						strValues += "'"+Scheme_DLTData.intListNo + "',";
					}
									if (Scheme_DLTData.chrItemName!=null && Scheme_DLTData.chrItemName.ToString().Trim() != "" )
					{
						strColumns += "chrItemName,";
						strValues += "'"+Scheme_DLTData.chrItemName + "',";
					}
									if (Scheme_DLTData.chrID!=null && Scheme_DLTData.chrID.ToString().Trim() != "" )
					{
						strColumns += "chrID,";
						strValues += "'"+Scheme_DLTData.chrID + "',";
					}
									if ( Scheme_DLTData.intLength.ToString().Trim() != "" )
					{
						strColumns += "intLength,";
						strValues += "'"+Scheme_DLTData.intLength + "',";
					}
									if ( Scheme_DLTData.intDot.ToString().Trim() != "" )
					{
						strColumns += "intDot,";
						strValues += "'"+Scheme_DLTData.intDot + "',";
					}
									if (Scheme_DLTData.chrFormat!=null && Scheme_DLTData.chrFormat.ToString().Trim() != "" )
					{
						strColumns += "chrFormat,";
						strValues += "'"+Scheme_DLTData.chrFormat + "',";
					}
									if ( Scheme_DLTData.intType.ToString().Trim() != "" )
					{
						strColumns += "intType,";
						strValues += "'"+Scheme_DLTData.intType + "',";
					}
									if (Scheme_DLTData.chrValue!=null && Scheme_DLTData.chrValue.ToString().Trim() != "" )
					{
						strColumns += "chrValue,";
						strValues += "'"+Scheme_DLTData.chrValue + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_DLTData",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_DLTData 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_DLTData  Scheme_DLTData,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if ( Scheme_DLTData.schemeID.ToString().Trim() != "" )
						setValues += "schemeID='"+ Scheme_DLTData .schemeID+"',";
								  	if ( Scheme_DLTData.intItemID.ToString().Trim() != "" )
						setValues += "intItemID='"+ Scheme_DLTData .intItemID+"',";
								  	if ( Scheme_DLTData.dltID.ToString().Trim() != "" )
						setValues += "dltID='"+ Scheme_DLTData .dltID+"',";
								  	if ( Scheme_DLTData.intListNo.ToString().Trim() != "" )
						setValues += "intListNo='"+ Scheme_DLTData .intListNo+"',";
								  	if (Scheme_DLTData.chrItemName!=null && Scheme_DLTData.chrItemName.ToString().Trim() != "" )
						setValues += "chrItemName='"+ Scheme_DLTData .chrItemName+"',";
								  	if (Scheme_DLTData.chrID!=null && Scheme_DLTData.chrID.ToString().Trim() != "" )
						setValues += "chrID='"+ Scheme_DLTData .chrID+"',";
								  	if ( Scheme_DLTData.intLength.ToString().Trim() != "" )
						setValues += "intLength='"+ Scheme_DLTData .intLength+"',";
								  	if ( Scheme_DLTData.intDot.ToString().Trim() != "" )
						setValues += "intDot='"+ Scheme_DLTData .intDot+"',";
								  	if (Scheme_DLTData.chrFormat!=null && Scheme_DLTData.chrFormat.ToString().Trim() != "" )
						setValues += "chrFormat='"+ Scheme_DLTData .chrFormat+"',";
								  	if ( Scheme_DLTData.intType.ToString().Trim() != "" )
						setValues += "intType='"+ Scheme_DLTData .intType+"',";
								  	if (Scheme_DLTData.chrValue!=null && Scheme_DLTData.chrValue.ToString().Trim() != "" )
						setValues += "chrValue='"+ Scheme_DLTData .chrValue+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_DLTData",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_DLTData  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_DLTData",strWhere);
				return _SqlText;
			}
	}
}


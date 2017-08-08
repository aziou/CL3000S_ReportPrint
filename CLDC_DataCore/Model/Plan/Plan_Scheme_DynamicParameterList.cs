using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_DynamicParameterList
		public partial class Plan_Scheme_DynamicParameterList:Plan_Base
	{
		public Plan_Scheme_DynamicParameterList(int TaiType, string vFAName)
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
			strSql.Append("select count(1) from Scheme_DynamicParameterList");
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
			strSql.Append("select count(1) from Scheme_DynamicParameterList");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Scheme_DynamicParameterList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_DynamicParameterList(");			
            strSql.Append("chrParaName,chrParaDetail,chrGrpType,intItemTypeOrPrjNo,chrControlType,chrDefValue,chrVisiable,chrStatus,chrGlobalPara,ChangeDate,chrEditor");
			strSql.Append(") values (");
            strSql.Append("@chrParaName,@chrParaDetail,@chrGrpType,@intItemTypeOrPrjNo,@chrControlType,@chrDefValue,@chrVisiable,@chrStatus,@chrGlobalPara,@ChangeDate,@chrEditor");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@chrParaName", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrParaDetail", OleDbType.VarChar,200) ,            
                        new OleDbParameter("@chrGrpType", OleDbType.VarChar,8) ,            
                        new OleDbParameter("@intItemTypeOrPrjNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrControlType", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@chrDefValue", OleDbType.VarChar,255) ,            
                        new OleDbParameter("@chrVisiable", OleDbType.VarChar,20) ,            
                        new OleDbParameter("@chrStatus", OleDbType.VarChar,20) ,            
                        new OleDbParameter("@chrGlobalPara", OleDbType.VarChar,20) ,            
                        new OleDbParameter("@ChangeDate", OleDbType.Date) ,            
                        new OleDbParameter("@chrEditor", OleDbType.VarChar,10)             
              
            };
			            
            parameters[0].Value = model.chrParaName;                        
            parameters[1].Value = model.chrParaDetail;                        
            parameters[2].Value = model.chrGrpType;                        
            parameters[3].Value = model.intItemTypeOrPrjNo;                        
            parameters[4].Value = model.chrControlType;                        
            parameters[5].Value = model.chrDefValue;                        
            parameters[6].Value = model.chrVisiable;                        
            parameters[7].Value = model.chrStatus;                        
            parameters[8].Value = model.chrGlobalPara;                        
            parameters[9].Value = model.ChangeDate;                        
            parameters[10].Value = model.chrEditor;                        
			   
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
		public bool Update(Scheme_DynamicParameterList model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_DynamicParameterList set ");
			                                                
            strSql.Append(" chrParaName = @chrParaName , ");                                    
            strSql.Append(" chrParaDetail = @chrParaDetail , ");                                    
            strSql.Append(" chrGrpType = @chrGrpType , ");                                    
            strSql.Append(" intItemTypeOrPrjNo = @intItemTypeOrPrjNo , ");                                    
            strSql.Append(" chrControlType = @chrControlType , ");                                    
            strSql.Append(" chrDefValue = @chrDefValue , ");                                    
            strSql.Append(" chrVisiable = @chrVisiable , ");                                    
            strSql.Append(" chrStatus = @chrStatus , ");                                    
            strSql.Append(" chrGlobalPara = @chrGlobalPara , ");                                    
            strSql.Append(" ChangeDate = @ChangeDate , ");                                    
            strSql.Append(" chrEditor = @chrEditor  ");            			
			strSql.Append(" where schemeID=@schemeID ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@schemeID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrParaName", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrParaDetail", OleDbType.VarChar,200) ,            
                        new OleDbParameter("@chrGrpType", OleDbType.VarChar,8) ,            
                        new OleDbParameter("@intItemTypeOrPrjNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrControlType", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@chrDefValue", OleDbType.VarChar,255) ,            
                        new OleDbParameter("@chrVisiable", OleDbType.VarChar,20) ,            
                        new OleDbParameter("@chrStatus", OleDbType.VarChar,20) ,            
                        new OleDbParameter("@chrGlobalPara", OleDbType.VarChar,20) ,            
                        new OleDbParameter("@ChangeDate", OleDbType.Date) ,            
                        new OleDbParameter("@chrEditor", OleDbType.VarChar,10)             
              
            };
			            
            parameters[11].Value = model.schemeID;                        
            parameters[12].Value = model.chrParaName;                        
            parameters[13].Value = model.chrParaDetail;                        
            parameters[14].Value = model.chrGrpType;                        
            parameters[15].Value = model.intItemTypeOrPrjNo;                        
            parameters[16].Value = model.chrControlType;                        
            parameters[17].Value = model.chrDefValue;                        
            parameters[18].Value = model.chrVisiable;                        
            parameters[19].Value = model.chrStatus;                        
            parameters[20].Value = model.chrGlobalPara;                        
            parameters[21].Value = model.ChangeDate;                        
            parameters[22].Value = model.chrEditor;                        
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
			strSql.Append("delete from Scheme_DynamicParameterList ");
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
		public Scheme_DynamicParameterList GetModel(int schemeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select schemeID, chrParaName, chrParaDetail, chrGrpType, intItemTypeOrPrjNo, chrControlType, chrDefValue, chrVisiable, chrStatus, chrGlobalPara, ChangeDate, chrEditor  ");			
			strSql.Append("  from Scheme_DynamicParameterList ");
			strSql.Append(" where schemeID=@schemeID");
						OleDbParameter[] parameters = {
					new OleDbParameter("@schemeID", OleDbType.Integer,4)
			};
			parameters[0].Value = schemeID;

			
			Scheme_DynamicParameterList model=new Scheme_DynamicParameterList();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["schemeID"].ToString()!="")
				{
					model.schemeID=int.Parse(ds.Tables[0].Rows[0]["schemeID"].ToString());
				}
																																				model.chrParaName= ds.Tables[0].Rows[0]["chrParaName"].ToString();
																																model.chrParaDetail= ds.Tables[0].Rows[0]["chrParaDetail"].ToString();
																																model.chrGrpType= ds.Tables[0].Rows[0]["chrGrpType"].ToString();
																												if(ds.Tables[0].Rows[0]["intItemTypeOrPrjNo"].ToString()!="")
				{
					model.intItemTypeOrPrjNo=int.Parse(ds.Tables[0].Rows[0]["intItemTypeOrPrjNo"].ToString());
				}
																																				model.chrControlType= ds.Tables[0].Rows[0]["chrControlType"].ToString();
																																model.chrDefValue= ds.Tables[0].Rows[0]["chrDefValue"].ToString();
																																model.chrVisiable= ds.Tables[0].Rows[0]["chrVisiable"].ToString();
																																model.chrStatus= ds.Tables[0].Rows[0]["chrStatus"].ToString();
																																model.chrGlobalPara= ds.Tables[0].Rows[0]["chrGlobalPara"].ToString();
																												if(ds.Tables[0].Rows[0]["ChangeDate"].ToString()!="")
				{
					model.ChangeDate=DateTime.Parse(ds.Tables[0].Rows[0]["ChangeDate"].ToString());
				}
																																				model.chrEditor= ds.Tables[0].Rows[0]["chrEditor"].ToString();
																										
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
			strSql.Append(" FROM Scheme_DynamicParameterList ");
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
			strSql.Append(" FROM Scheme_DynamicParameterList ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_DynamicParameterList 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_DynamicParameterList  Scheme_DynamicParameterList)
		{
			    string strColumns = "";
				string strValues = "";
									if ( Scheme_DynamicParameterList.schemeID.ToString().Trim() != "" )
					{
						strColumns += "schemeID,";
						strValues += "'"+Scheme_DynamicParameterList.schemeID + "',";
					}
									if (Scheme_DynamicParameterList.chrParaName!=null && Scheme_DynamicParameterList.chrParaName.ToString().Trim() != "" )
					{
						strColumns += "chrParaName,";
						strValues += "'"+Scheme_DynamicParameterList.chrParaName + "',";
					}
									if (Scheme_DynamicParameterList.chrParaDetail!=null && Scheme_DynamicParameterList.chrParaDetail.ToString().Trim() != "" )
					{
						strColumns += "chrParaDetail,";
						strValues += "'"+Scheme_DynamicParameterList.chrParaDetail + "',";
					}
									if (Scheme_DynamicParameterList.chrGrpType!=null && Scheme_DynamicParameterList.chrGrpType.ToString().Trim() != "" )
					{
						strColumns += "chrGrpType,";
						strValues += "'"+Scheme_DynamicParameterList.chrGrpType + "',";
					}
									if ( Scheme_DynamicParameterList.intItemTypeOrPrjNo.ToString().Trim() != "" )
					{
						strColumns += "intItemTypeOrPrjNo,";
						strValues += "'"+Scheme_DynamicParameterList.intItemTypeOrPrjNo + "',";
					}
									if (Scheme_DynamicParameterList.chrControlType!=null && Scheme_DynamicParameterList.chrControlType.ToString().Trim() != "" )
					{
						strColumns += "chrControlType,";
						strValues += "'"+Scheme_DynamicParameterList.chrControlType + "',";
					}
									if (Scheme_DynamicParameterList.chrDefValue!=null && Scheme_DynamicParameterList.chrDefValue.ToString().Trim() != "" )
					{
						strColumns += "chrDefValue,";
						strValues += "'"+Scheme_DynamicParameterList.chrDefValue + "',";
					}
									if (Scheme_DynamicParameterList.chrVisiable!=null && Scheme_DynamicParameterList.chrVisiable.ToString().Trim() != "" )
					{
						strColumns += "chrVisiable,";
						strValues += "'"+Scheme_DynamicParameterList.chrVisiable + "',";
					}
									if (Scheme_DynamicParameterList.chrStatus!=null && Scheme_DynamicParameterList.chrStatus.ToString().Trim() != "" )
					{
						strColumns += "chrStatus,";
						strValues += "'"+Scheme_DynamicParameterList.chrStatus + "',";
					}
									if (Scheme_DynamicParameterList.chrGlobalPara!=null && Scheme_DynamicParameterList.chrGlobalPara.ToString().Trim() != "" )
					{
						strColumns += "chrGlobalPara,";
						strValues += "'"+Scheme_DynamicParameterList.chrGlobalPara + "',";
					}
									if (Scheme_DynamicParameterList.ChangeDate!=null && Scheme_DynamicParameterList.ChangeDate.ToString().Trim() != "" )
					{
						strColumns += "ChangeDate,";
						strValues += "'"+Scheme_DynamicParameterList.ChangeDate + "',";
					}
									if (Scheme_DynamicParameterList.chrEditor!=null && Scheme_DynamicParameterList.chrEditor.ToString().Trim() != "" )
					{
						strColumns += "chrEditor,";
						strValues += "'"+Scheme_DynamicParameterList.chrEditor + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_DynamicParameterList",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_DynamicParameterList 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_DynamicParameterList  Scheme_DynamicParameterList,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if ( Scheme_DynamicParameterList.schemeID.ToString().Trim() != "" )
						setValues += "schemeID='"+ Scheme_DynamicParameterList .schemeID+"',";
								  	if (Scheme_DynamicParameterList.chrParaName!=null && Scheme_DynamicParameterList.chrParaName.ToString().Trim() != "" )
						setValues += "chrParaName='"+ Scheme_DynamicParameterList .chrParaName+"',";
								  	if (Scheme_DynamicParameterList.chrParaDetail!=null && Scheme_DynamicParameterList.chrParaDetail.ToString().Trim() != "" )
						setValues += "chrParaDetail='"+ Scheme_DynamicParameterList .chrParaDetail+"',";
								  	if (Scheme_DynamicParameterList.chrGrpType!=null && Scheme_DynamicParameterList.chrGrpType.ToString().Trim() != "" )
						setValues += "chrGrpType='"+ Scheme_DynamicParameterList .chrGrpType+"',";
								  	if ( Scheme_DynamicParameterList.intItemTypeOrPrjNo.ToString().Trim() != "" )
						setValues += "intItemTypeOrPrjNo='"+ Scheme_DynamicParameterList .intItemTypeOrPrjNo+"',";
								  	if (Scheme_DynamicParameterList.chrControlType!=null && Scheme_DynamicParameterList.chrControlType.ToString().Trim() != "" )
						setValues += "chrControlType='"+ Scheme_DynamicParameterList .chrControlType+"',";
								  	if (Scheme_DynamicParameterList.chrDefValue!=null && Scheme_DynamicParameterList.chrDefValue.ToString().Trim() != "" )
						setValues += "chrDefValue='"+ Scheme_DynamicParameterList .chrDefValue+"',";
								  	if (Scheme_DynamicParameterList.chrVisiable!=null && Scheme_DynamicParameterList.chrVisiable.ToString().Trim() != "" )
						setValues += "chrVisiable='"+ Scheme_DynamicParameterList .chrVisiable+"',";
								  	if (Scheme_DynamicParameterList.chrStatus!=null && Scheme_DynamicParameterList.chrStatus.ToString().Trim() != "" )
						setValues += "chrStatus='"+ Scheme_DynamicParameterList .chrStatus+"',";
								  	if (Scheme_DynamicParameterList.chrGlobalPara!=null && Scheme_DynamicParameterList.chrGlobalPara.ToString().Trim() != "" )
						setValues += "chrGlobalPara='"+ Scheme_DynamicParameterList .chrGlobalPara+"',";
								  	if (Scheme_DynamicParameterList.ChangeDate!=null && Scheme_DynamicParameterList.ChangeDate.ToString().Trim() != "" )
						setValues += "ChangeDate='"+ Scheme_DynamicParameterList .ChangeDate+"',";
								  	if (Scheme_DynamicParameterList.chrEditor!=null && Scheme_DynamicParameterList.chrEditor.ToString().Trim() != "" )
						setValues += "chrEditor='"+ Scheme_DynamicParameterList .chrEditor+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_DynamicParameterList",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_DynamicParameterList  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_DynamicParameterList",strWhere);
				return _SqlText;
			}
	}
}


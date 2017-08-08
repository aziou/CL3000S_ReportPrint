using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_Wc
		public partial class Plan_Scheme_Wc:Plan_Base
	{
		public Plan_Scheme_Wc(int TaiType, string vFAName)
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
			strSql.Append("select count(1) from Scheme_Wc");
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
			strSql.Append("select count(1) from Scheme_Wc");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Scheme_Wc model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_Wc(");			
            strSql.Append("intListNo,chrProjectNo,chrXIb,chrParameter,chrTime,chrULimitBL,chrLLimitBL,chrQCount");
			strSql.Append(") values (");
            strSql.Append("@intListNo,@chrProjectNo,@chrXIb,@chrParameter,@chrTime,@chrULimitBL,@chrLLimitBL,@chrQCount");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrProjectNo", OleDbType.VarChar,5) ,            
                        new OleDbParameter("@chrXIb", OleDbType.VarChar,30) ,            
                        new OleDbParameter("@chrParameter", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrTime", OleDbType.VarChar,30) ,            
                        new OleDbParameter("@chrULimitBL", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrLLimitBL", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrQCount", OleDbType.VarChar,10)             
              
            };
			            
            parameters[0].Value = model.intListNo;                        
            parameters[1].Value = model.chrProjectNo;                        
            parameters[2].Value = model.chrXIb;                        
            parameters[3].Value = model.chrParameter;                        
            parameters[4].Value = model.chrTime;                        
            parameters[5].Value = model.chrULimitBL;                        
            parameters[6].Value = model.chrLLimitBL;                        
            parameters[7].Value = model.chrQCount;                        
			   
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
		public bool Update(Scheme_Wc model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_Wc set ");
			                                                
            strSql.Append(" intListNo = @intListNo , ");                                    
            strSql.Append(" chrProjectNo = @chrProjectNo , ");                                    
            strSql.Append(" chrXIb = @chrXIb , ");                                    
            strSql.Append(" chrParameter = @chrParameter , ");                                    
            strSql.Append(" chrTime = @chrTime , ");                                    
            strSql.Append(" chrULimitBL = @chrULimitBL , ");                                    
            strSql.Append(" chrLLimitBL = @chrLLimitBL , ");                                    
            strSql.Append(" chrQCount = @chrQCount  ");            			
			strSql.Append(" where schemeID=@schemeID ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@schemeID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrProjectNo", OleDbType.VarChar,5) ,            
                        new OleDbParameter("@chrXIb", OleDbType.VarChar,30) ,            
                        new OleDbParameter("@chrParameter", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrTime", OleDbType.VarChar,30) ,            
                        new OleDbParameter("@chrULimitBL", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrLLimitBL", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrQCount", OleDbType.VarChar,10)             
              
            };
			            
            parameters[8].Value = model.schemeID;                        
            parameters[9].Value = model.intListNo;                        
            parameters[10].Value = model.chrProjectNo;                        
            parameters[11].Value = model.chrXIb;                        
            parameters[12].Value = model.chrParameter;                        
            parameters[13].Value = model.chrTime;                        
            parameters[14].Value = model.chrULimitBL;                        
            parameters[15].Value = model.chrLLimitBL;                        
            parameters[16].Value = model.chrQCount;                        
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
			strSql.Append("delete from Scheme_Wc ");
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
		public Scheme_Wc GetModel(int schemeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select schemeID, intListNo, chrProjectNo, chrXIb, chrParameter, chrTime, chrULimitBL, chrLLimitBL, chrQCount  ");			
			strSql.Append("  from Scheme_Wc ");
			strSql.Append(" where schemeID=@schemeID");
						OleDbParameter[] parameters = {
					new OleDbParameter("@schemeID", OleDbType.Integer,4)
			};
			parameters[0].Value = schemeID;

			
			Scheme_Wc model=new Scheme_Wc();
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
																																				model.chrProjectNo= ds.Tables[0].Rows[0]["chrProjectNo"].ToString();
																																model.chrXIb= ds.Tables[0].Rows[0]["chrXIb"].ToString();
																																model.chrParameter= ds.Tables[0].Rows[0]["chrParameter"].ToString();
																																model.chrTime= ds.Tables[0].Rows[0]["chrTime"].ToString();
																																model.chrULimitBL= ds.Tables[0].Rows[0]["chrULimitBL"].ToString();
																																model.chrLLimitBL= ds.Tables[0].Rows[0]["chrLLimitBL"].ToString();
																																model.chrQCount= ds.Tables[0].Rows[0]["chrQCount"].ToString();
																										
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
			strSql.Append(" FROM Scheme_Wc ");
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
			strSql.Append(" FROM Scheme_Wc ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_Wc 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_Wc  Scheme_Wc)
		{
			    string strColumns = "";
				string strValues = "";
									if ( Scheme_Wc.schemeID.ToString().Trim() != "" )
					{
						strColumns += "schemeID,";
						strValues += "'"+Scheme_Wc.schemeID + "',";
					}
									if ( Scheme_Wc.intListNo.ToString().Trim() != "" )
					{
						strColumns += "intListNo,";
						strValues += "'"+Scheme_Wc.intListNo + "',";
					}
									if (Scheme_Wc.chrProjectNo!=null && Scheme_Wc.chrProjectNo.ToString().Trim() != "" )
					{
						strColumns += "chrProjectNo,";
						strValues += "'"+Scheme_Wc.chrProjectNo + "',";
					}
									if (Scheme_Wc.chrXIb!=null && Scheme_Wc.chrXIb.ToString().Trim() != "" )
					{
						strColumns += "chrXIb,";
						strValues += "'"+Scheme_Wc.chrXIb + "',";
					}
									if (Scheme_Wc.chrParameter!=null && Scheme_Wc.chrParameter.ToString().Trim() != "" )
					{
						strColumns += "chrParameter,";
						strValues += "'"+Scheme_Wc.chrParameter + "',";
					}
									if (Scheme_Wc.chrTime!=null && Scheme_Wc.chrTime.ToString().Trim() != "" )
					{
						strColumns += "chrTime,";
						strValues += "'"+Scheme_Wc.chrTime + "',";
					}
									if (Scheme_Wc.chrULimitBL!=null && Scheme_Wc.chrULimitBL.ToString().Trim() != "" )
					{
						strColumns += "chrULimitBL,";
						strValues += "'"+Scheme_Wc.chrULimitBL + "',";
					}
									if (Scheme_Wc.chrLLimitBL!=null && Scheme_Wc.chrLLimitBL.ToString().Trim() != "" )
					{
						strColumns += "chrLLimitBL,";
						strValues += "'"+Scheme_Wc.chrLLimitBL + "',";
					}
									if (Scheme_Wc.chrQCount!=null && Scheme_Wc.chrQCount.ToString().Trim() != "" )
					{
						strColumns += "chrQCount,";
						strValues += "'"+Scheme_Wc.chrQCount + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_Wc",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_Wc 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_Wc  Scheme_Wc,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if ( Scheme_Wc.schemeID.ToString().Trim() != "" )
						setValues += "schemeID='"+ Scheme_Wc .schemeID+"',";
								  	if ( Scheme_Wc.intListNo.ToString().Trim() != "" )
						setValues += "intListNo='"+ Scheme_Wc .intListNo+"',";
								  	if (Scheme_Wc.chrProjectNo!=null && Scheme_Wc.chrProjectNo.ToString().Trim() != "" )
						setValues += "chrProjectNo='"+ Scheme_Wc .chrProjectNo+"',";
								  	if (Scheme_Wc.chrXIb!=null && Scheme_Wc.chrXIb.ToString().Trim() != "" )
						setValues += "chrXIb='"+ Scheme_Wc .chrXIb+"',";
								  	if (Scheme_Wc.chrParameter!=null && Scheme_Wc.chrParameter.ToString().Trim() != "" )
						setValues += "chrParameter='"+ Scheme_Wc .chrParameter+"',";
								  	if (Scheme_Wc.chrTime!=null && Scheme_Wc.chrTime.ToString().Trim() != "" )
						setValues += "chrTime='"+ Scheme_Wc .chrTime+"',";
								  	if (Scheme_Wc.chrULimitBL!=null && Scheme_Wc.chrULimitBL.ToString().Trim() != "" )
						setValues += "chrULimitBL='"+ Scheme_Wc .chrULimitBL+"',";
								  	if (Scheme_Wc.chrLLimitBL!=null && Scheme_Wc.chrLLimitBL.ToString().Trim() != "" )
						setValues += "chrLLimitBL='"+ Scheme_Wc .chrLLimitBL+"',";
								  	if (Scheme_Wc.chrQCount!=null && Scheme_Wc.chrQCount.ToString().Trim() != "" )
						setValues += "chrQCount='"+ Scheme_Wc .chrQCount+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_Wc",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_Wc  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_Wc",strWhere);
				return _SqlText;
			}
	}
}


using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_Zz
		public partial class Plan_Scheme_Zz:Plan_Base
	{
		public Plan_Scheme_Zz(int TaiType, string vFAName)
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
			strSql.Append("select count(1) from Scheme_Zz");
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
			strSql.Append("select count(1) from Scheme_Zz");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Scheme_Zz model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_Zz(");			
            strSql.Append("intListNo,chrJdfx,chrFl,chrYj,sngXUb,sngXIb,chrGlys,chrStartTime,chrNeedTime,chrzzlx");
			strSql.Append(") values (");
            strSql.Append("@intListNo,@chrJdfx,@chrFl,@chrYj,@sngXUb,@sngXIb,@chrGlys,@chrStartTime,@chrNeedTime,@chrzzlx");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrJdfx", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrFl", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrYj", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@sngXUb", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngXIb", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrGlys", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrStartTime", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrNeedTime", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrzzlx", OleDbType.VarChar,1)             
              
            };
			            
            parameters[0].Value = model.intListNo;                        
            parameters[1].Value = model.chrJdfx;                        
            parameters[2].Value = model.chrFl;                        
            parameters[3].Value = model.chrYj;                        
            parameters[4].Value = model.sngXUb;                        
            parameters[5].Value = model.sngXIb;                        
            parameters[6].Value = model.chrGlys;                        
            parameters[7].Value = model.chrStartTime;                        
            parameters[8].Value = model.chrNeedTime;                        
            parameters[9].Value = model.chrzzlx;                        
			   
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
		public bool Update(Scheme_Zz model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_Zz set ");
			                                                
            strSql.Append(" intListNo = @intListNo , ");                                    
            strSql.Append(" chrJdfx = @chrJdfx , ");                                    
            strSql.Append(" chrFl = @chrFl , ");                                    
            strSql.Append(" chrYj = @chrYj , ");                                    
            strSql.Append(" sngXUb = @sngXUb , ");                                    
            strSql.Append(" sngXIb = @sngXIb , ");                                    
            strSql.Append(" chrGlys = @chrGlys , ");                                    
            strSql.Append(" chrStartTime = @chrStartTime , ");                                    
            strSql.Append(" chrNeedTime = @chrNeedTime , ");                                    
            strSql.Append(" chrzzlx = @chrzzlx  ");            			
			strSql.Append(" where schemeID=@schemeID ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@schemeID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrJdfx", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrFl", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrYj", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@sngXUb", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngXIb", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrGlys", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrStartTime", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrNeedTime", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrzzlx", OleDbType.VarChar,1)             
              
            };
			            
            parameters[10].Value = model.schemeID;                        
            parameters[11].Value = model.intListNo;                        
            parameters[12].Value = model.chrJdfx;                        
            parameters[13].Value = model.chrFl;                        
            parameters[14].Value = model.chrYj;                        
            parameters[15].Value = model.sngXUb;                        
            parameters[16].Value = model.sngXIb;                        
            parameters[17].Value = model.chrGlys;                        
            parameters[18].Value = model.chrStartTime;                        
            parameters[19].Value = model.chrNeedTime;                        
            parameters[20].Value = model.chrzzlx;                        
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
			strSql.Append("delete from Scheme_Zz ");
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
		public Scheme_Zz GetModel(int schemeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select schemeID, intListNo, chrJdfx, chrFl, chrYj, sngXUb, sngXIb, chrGlys, chrStartTime, chrNeedTime, chrzzlx  ");			
			strSql.Append("  from Scheme_Zz ");
			strSql.Append(" where schemeID=@schemeID");
						OleDbParameter[] parameters = {
					new OleDbParameter("@schemeID", OleDbType.Integer,4)
			};
			parameters[0].Value = schemeID;

			
			Scheme_Zz model=new Scheme_Zz();
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
																																				model.chrJdfx= ds.Tables[0].Rows[0]["chrJdfx"].ToString();
																																model.chrFl= ds.Tables[0].Rows[0]["chrFl"].ToString();
																																model.chrYj= ds.Tables[0].Rows[0]["chrYj"].ToString();
																												if(ds.Tables[0].Rows[0]["sngXUb"].ToString()!="")
				{
					model.sngXUb=int.Parse(ds.Tables[0].Rows[0]["sngXUb"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["sngXIb"].ToString()!="")
				{
					model.sngXIb=int.Parse(ds.Tables[0].Rows[0]["sngXIb"].ToString());
				}
																																				model.chrGlys= ds.Tables[0].Rows[0]["chrGlys"].ToString();
																																model.chrStartTime= ds.Tables[0].Rows[0]["chrStartTime"].ToString();
																																model.chrNeedTime= ds.Tables[0].Rows[0]["chrNeedTime"].ToString();
																																model.chrzzlx= ds.Tables[0].Rows[0]["chrzzlx"].ToString();
																										
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
			strSql.Append(" FROM Scheme_Zz ");
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
			strSql.Append(" FROM Scheme_Zz ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_Zz 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_Zz  Scheme_Zz)
		{
			    string strColumns = "";
				string strValues = "";
									if ( Scheme_Zz.schemeID.ToString().Trim() != "" )
					{
						strColumns += "schemeID,";
						strValues += "'"+Scheme_Zz.schemeID + "',";
					}
									if ( Scheme_Zz.intListNo.ToString().Trim() != "" )
					{
						strColumns += "intListNo,";
						strValues += "'"+Scheme_Zz.intListNo + "',";
					}
									if (Scheme_Zz.chrJdfx!=null && Scheme_Zz.chrJdfx.ToString().Trim() != "" )
					{
						strColumns += "chrJdfx,";
						strValues += "'"+Scheme_Zz.chrJdfx + "',";
					}
									if (Scheme_Zz.chrFl!=null && Scheme_Zz.chrFl.ToString().Trim() != "" )
					{
						strColumns += "chrFl,";
						strValues += "'"+Scheme_Zz.chrFl + "',";
					}
									if (Scheme_Zz.chrYj!=null && Scheme_Zz.chrYj.ToString().Trim() != "" )
					{
						strColumns += "chrYj,";
						strValues += "'"+Scheme_Zz.chrYj + "',";
					}
									if ( Scheme_Zz.sngXUb.ToString().Trim() != "" )
					{
						strColumns += "sngXUb,";
						strValues += "'"+Scheme_Zz.sngXUb + "',";
					}
									if ( Scheme_Zz.sngXIb.ToString().Trim() != "" )
					{
						strColumns += "sngXIb,";
						strValues += "'"+Scheme_Zz.sngXIb + "',";
					}
									if (Scheme_Zz.chrGlys!=null && Scheme_Zz.chrGlys.ToString().Trim() != "" )
					{
						strColumns += "chrGlys,";
						strValues += "'"+Scheme_Zz.chrGlys + "',";
					}
									if (Scheme_Zz.chrStartTime!=null && Scheme_Zz.chrStartTime.ToString().Trim() != "" )
					{
						strColumns += "chrStartTime,";
						strValues += "'"+Scheme_Zz.chrStartTime + "',";
					}
									if (Scheme_Zz.chrNeedTime!=null && Scheme_Zz.chrNeedTime.ToString().Trim() != "" )
					{
						strColumns += "chrNeedTime,";
						strValues += "'"+Scheme_Zz.chrNeedTime + "',";
					}
									if (Scheme_Zz.chrzzlx!=null && Scheme_Zz.chrzzlx.ToString().Trim() != "" )
					{
						strColumns += "chrzzlx,";
						strValues += "'"+Scheme_Zz.chrzzlx + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_Zz",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_Zz 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_Zz  Scheme_Zz,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if ( Scheme_Zz.schemeID.ToString().Trim() != "" )
						setValues += "schemeID='"+ Scheme_Zz .schemeID+"',";
								  	if ( Scheme_Zz.intListNo.ToString().Trim() != "" )
						setValues += "intListNo='"+ Scheme_Zz .intListNo+"',";
								  	if (Scheme_Zz.chrJdfx!=null && Scheme_Zz.chrJdfx.ToString().Trim() != "" )
						setValues += "chrJdfx='"+ Scheme_Zz .chrJdfx+"',";
								  	if (Scheme_Zz.chrFl!=null && Scheme_Zz.chrFl.ToString().Trim() != "" )
						setValues += "chrFl='"+ Scheme_Zz .chrFl+"',";
								  	if (Scheme_Zz.chrYj!=null && Scheme_Zz.chrYj.ToString().Trim() != "" )
						setValues += "chrYj='"+ Scheme_Zz .chrYj+"',";
								  	if ( Scheme_Zz.sngXUb.ToString().Trim() != "" )
						setValues += "sngXUb='"+ Scheme_Zz .sngXUb+"',";
								  	if ( Scheme_Zz.sngXIb.ToString().Trim() != "" )
						setValues += "sngXIb='"+ Scheme_Zz .sngXIb+"',";
								  	if (Scheme_Zz.chrGlys!=null && Scheme_Zz.chrGlys.ToString().Trim() != "" )
						setValues += "chrGlys='"+ Scheme_Zz .chrGlys+"',";
								  	if (Scheme_Zz.chrStartTime!=null && Scheme_Zz.chrStartTime.ToString().Trim() != "" )
						setValues += "chrStartTime='"+ Scheme_Zz .chrStartTime+"',";
								  	if (Scheme_Zz.chrNeedTime!=null && Scheme_Zz.chrNeedTime.ToString().Trim() != "" )
						setValues += "chrNeedTime='"+ Scheme_Zz .chrNeedTime+"',";
								  	if (Scheme_Zz.chrzzlx!=null && Scheme_Zz.chrzzlx.ToString().Trim() != "" )
						setValues += "chrzzlx='"+ Scheme_Zz .chrzzlx+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_Zz",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_Zz  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_Zz",strWhere);
				return _SqlText;
			}
	}
}


using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_Show
		public partial class Plan_Scheme_Show:Plan_Base
	{
		public Plan_Scheme_Show(int TaiType, string vFAName)
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
			strSql.Append("select count(1) from Scheme_Show");
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
			strSql.Append("select count(1) from Scheme_Show");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Scheme_Show model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_Show(");			
            strSql.Append("intFaName,intListNo,intShowType,intType,chrType,intSubItem,chrSubItem,chrID,intClass,intLength,intDot,intReadWrite,chrFormat,chrContent,intCheck,c_Other1,c_Other2,c_Other3,c_Other4,c_Other5,c_Other6,c_Other7,c_Other8,c_Other9,c_Other10");
			strSql.Append(") values (");
            strSql.Append("@intFaName,@intListNo,@intShowType,@intType,@chrType,@intSubItem,@chrSubItem,@chrID,@intClass,@intLength,@intDot,@intReadWrite,@chrFormat,@chrContent,@intCheck,@c_Other1,@c_Other2,@c_Other3,@c_Other4,@c_Other5,@c_Other6,@c_Other7,@c_Other8,@c_Other9,@c_Other10");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@intFaName", OleDbType.VarChar,255) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intShowType", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intType", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrType", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@intSubItem", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrSubItem", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrID", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@intClass", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intLength", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intDot", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intReadWrite", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrFormat", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrContent", OleDbType.VarChar,200) ,            
                        new OleDbParameter("@intCheck", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@c_Other1", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other2", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other3", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other4", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other5", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other6", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other7", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other8", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other9", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other10", OleDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.intFaName;                        
            parameters[1].Value = model.intListNo;                        
            parameters[2].Value = model.intShowType;                        
            parameters[3].Value = model.intType;                        
            parameters[4].Value = model.chrType;                        
            parameters[5].Value = model.intSubItem;                        
            parameters[6].Value = model.chrSubItem;                        
            parameters[7].Value = model.chrID;                        
            parameters[8].Value = model.intClass;                        
            parameters[9].Value = model.intLength;                        
            parameters[10].Value = model.intDot;                        
            parameters[11].Value = model.intReadWrite;                        
            parameters[12].Value = model.chrFormat;                        
            parameters[13].Value = model.chrContent;                        
            parameters[14].Value = model.intCheck;                        
            parameters[15].Value = model.c_Other1;                        
            parameters[16].Value = model.c_Other2;                        
            parameters[17].Value = model.c_Other3;                        
            parameters[18].Value = model.c_Other4;                        
            parameters[19].Value = model.c_Other5;                        
            parameters[20].Value = model.c_Other6;                        
            parameters[21].Value = model.c_Other7;                        
            parameters[22].Value = model.c_Other8;                        
            parameters[23].Value = model.c_Other9;                        
            parameters[24].Value = model.c_Other10;                        
			   
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
		public bool Update(Scheme_Show model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_Show set ");
			                                                
            strSql.Append(" intFaName = @intFaName , ");                                    
            strSql.Append(" intListNo = @intListNo , ");                                    
            strSql.Append(" intShowType = @intShowType , ");                                    
            strSql.Append(" intType = @intType , ");                                    
            strSql.Append(" chrType = @chrType , ");                                    
            strSql.Append(" intSubItem = @intSubItem , ");                                    
            strSql.Append(" chrSubItem = @chrSubItem , ");                                    
            strSql.Append(" chrID = @chrID , ");                                    
            strSql.Append(" intClass = @intClass , ");                                    
            strSql.Append(" intLength = @intLength , ");                                    
            strSql.Append(" intDot = @intDot , ");                                    
            strSql.Append(" intReadWrite = @intReadWrite , ");                                    
            strSql.Append(" chrFormat = @chrFormat , ");                                    
            strSql.Append(" chrContent = @chrContent , ");                                    
            strSql.Append(" intCheck = @intCheck , ");                                    
            strSql.Append(" c_Other1 = @c_Other1 , ");                                    
            strSql.Append(" c_Other2 = @c_Other2 , ");                                    
            strSql.Append(" c_Other3 = @c_Other3 , ");                                    
            strSql.Append(" c_Other4 = @c_Other4 , ");                                    
            strSql.Append(" c_Other5 = @c_Other5 , ");                                    
            strSql.Append(" c_Other6 = @c_Other6 , ");                                    
            strSql.Append(" c_Other7 = @c_Other7 , ");                                    
            strSql.Append(" c_Other8 = @c_Other8 , ");                                    
            strSql.Append(" c_Other9 = @c_Other9 , ");                                    
            strSql.Append(" c_Other10 = @c_Other10  ");            			
			strSql.Append(" where schemeID=@schemeID ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@schemeID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intFaName", OleDbType.VarChar,255) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intShowType", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intType", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrType", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@intSubItem", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrSubItem", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrID", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@intClass", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intLength", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intDot", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intReadWrite", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrFormat", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrContent", OleDbType.VarChar,200) ,            
                        new OleDbParameter("@intCheck", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@c_Other1", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other2", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other3", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other4", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other5", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other6", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other7", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other8", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other9", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@c_Other10", OleDbType.VarChar,50)             
              
            };
			            
            parameters[25].Value = model.schemeID;                        
            parameters[26].Value = model.intFaName;                        
            parameters[27].Value = model.intListNo;                        
            parameters[28].Value = model.intShowType;                        
            parameters[29].Value = model.intType;                        
            parameters[30].Value = model.chrType;                        
            parameters[31].Value = model.intSubItem;                        
            parameters[32].Value = model.chrSubItem;                        
            parameters[33].Value = model.chrID;                        
            parameters[34].Value = model.intClass;                        
            parameters[35].Value = model.intLength;                        
            parameters[36].Value = model.intDot;                        
            parameters[37].Value = model.intReadWrite;                        
            parameters[38].Value = model.chrFormat;                        
            parameters[39].Value = model.chrContent;                        
            parameters[40].Value = model.intCheck;                        
            parameters[41].Value = model.c_Other1;                        
            parameters[42].Value = model.c_Other2;                        
            parameters[43].Value = model.c_Other3;                        
            parameters[44].Value = model.c_Other4;                        
            parameters[45].Value = model.c_Other5;                        
            parameters[46].Value = model.c_Other6;                        
            parameters[47].Value = model.c_Other7;                        
            parameters[48].Value = model.c_Other8;                        
            parameters[49].Value = model.c_Other9;                        
            parameters[50].Value = model.c_Other10;                        
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
			strSql.Append("delete from Scheme_Show ");
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
		public Scheme_Show GetModel(int schemeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select schemeID, intFaName, intListNo, intShowType, intType, chrType, intSubItem, chrSubItem, chrID, intClass, intLength, intDot, intReadWrite, chrFormat, chrContent, intCheck, c_Other1, c_Other2, c_Other3, c_Other4, c_Other5, c_Other6, c_Other7, c_Other8, c_Other9, c_Other10  ");			
			strSql.Append("  from Scheme_Show ");
			strSql.Append(" where schemeID=@schemeID");
						OleDbParameter[] parameters = {
					new OleDbParameter("@schemeID", OleDbType.Integer,4)
			};
			parameters[0].Value = schemeID;

			
			Scheme_Show model=new Scheme_Show();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["schemeID"].ToString()!="")
				{
					model.schemeID=int.Parse(ds.Tables[0].Rows[0]["schemeID"].ToString());
				}
																																				model.intFaName= ds.Tables[0].Rows[0]["intFaName"].ToString();
																												if(ds.Tables[0].Rows[0]["intListNo"].ToString()!="")
				{
					model.intListNo=int.Parse(ds.Tables[0].Rows[0]["intListNo"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["intShowType"].ToString()!="")
				{
					model.intShowType=int.Parse(ds.Tables[0].Rows[0]["intShowType"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["intType"].ToString()!="")
				{
					model.intType=int.Parse(ds.Tables[0].Rows[0]["intType"].ToString());
				}
																																				model.chrType= ds.Tables[0].Rows[0]["chrType"].ToString();
																												if(ds.Tables[0].Rows[0]["intSubItem"].ToString()!="")
				{
					model.intSubItem=int.Parse(ds.Tables[0].Rows[0]["intSubItem"].ToString());
				}
																																				model.chrSubItem= ds.Tables[0].Rows[0]["chrSubItem"].ToString();
																																model.chrID= ds.Tables[0].Rows[0]["chrID"].ToString();
																												if(ds.Tables[0].Rows[0]["intClass"].ToString()!="")
				{
					model.intClass=int.Parse(ds.Tables[0].Rows[0]["intClass"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["intLength"].ToString()!="")
				{
					model.intLength=int.Parse(ds.Tables[0].Rows[0]["intLength"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["intDot"].ToString()!="")
				{
					model.intDot=int.Parse(ds.Tables[0].Rows[0]["intDot"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["intReadWrite"].ToString()!="")
				{
					model.intReadWrite=int.Parse(ds.Tables[0].Rows[0]["intReadWrite"].ToString());
				}
																																				model.chrFormat= ds.Tables[0].Rows[0]["chrFormat"].ToString();
																																model.chrContent= ds.Tables[0].Rows[0]["chrContent"].ToString();
																																model.intCheck= ds.Tables[0].Rows[0]["intCheck"].ToString();
																																model.c_Other1= ds.Tables[0].Rows[0]["c_Other1"].ToString();
																																model.c_Other2= ds.Tables[0].Rows[0]["c_Other2"].ToString();
																																model.c_Other3= ds.Tables[0].Rows[0]["c_Other3"].ToString();
																																model.c_Other4= ds.Tables[0].Rows[0]["c_Other4"].ToString();
																																model.c_Other5= ds.Tables[0].Rows[0]["c_Other5"].ToString();
																																model.c_Other6= ds.Tables[0].Rows[0]["c_Other6"].ToString();
																																model.c_Other7= ds.Tables[0].Rows[0]["c_Other7"].ToString();
																																model.c_Other8= ds.Tables[0].Rows[0]["c_Other8"].ToString();
																																model.c_Other9= ds.Tables[0].Rows[0]["c_Other9"].ToString();
																																model.c_Other10= ds.Tables[0].Rows[0]["c_Other10"].ToString();
																										
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
			strSql.Append(" FROM Scheme_Show ");
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
			strSql.Append(" FROM Scheme_Show ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_Show 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_Show  Scheme_Show)
		{
			    string strColumns = "";
				string strValues = "";
									if ( Scheme_Show.schemeID.ToString().Trim() != "" )
					{
						strColumns += "schemeID,";
						strValues += "'"+Scheme_Show.schemeID + "',";
					}
									if (Scheme_Show.intFaName!=null && Scheme_Show.intFaName.ToString().Trim() != "" )
					{
						strColumns += "intFaName,";
						strValues += "'"+Scheme_Show.intFaName + "',";
					}
									if ( Scheme_Show.intListNo.ToString().Trim() != "" )
					{
						strColumns += "intListNo,";
						strValues += "'"+Scheme_Show.intListNo + "',";
					}
									if ( Scheme_Show.intShowType.ToString().Trim() != "" )
					{
						strColumns += "intShowType,";
						strValues += "'"+Scheme_Show.intShowType + "',";
					}
									if ( Scheme_Show.intType.ToString().Trim() != "" )
					{
						strColumns += "intType,";
						strValues += "'"+Scheme_Show.intType + "',";
					}
									if (Scheme_Show.chrType!=null && Scheme_Show.chrType.ToString().Trim() != "" )
					{
						strColumns += "chrType,";
						strValues += "'"+Scheme_Show.chrType + "',";
					}
									if ( Scheme_Show.intSubItem.ToString().Trim() != "" )
					{
						strColumns += "intSubItem,";
						strValues += "'"+Scheme_Show.intSubItem + "',";
					}
									if (Scheme_Show.chrSubItem!=null && Scheme_Show.chrSubItem.ToString().Trim() != "" )
					{
						strColumns += "chrSubItem,";
						strValues += "'"+Scheme_Show.chrSubItem + "',";
					}
									if (Scheme_Show.chrID!=null && Scheme_Show.chrID.ToString().Trim() != "" )
					{
						strColumns += "chrID,";
						strValues += "'"+Scheme_Show.chrID + "',";
					}
									if ( Scheme_Show.intClass.ToString().Trim() != "" )
					{
						strColumns += "intClass,";
						strValues += "'"+Scheme_Show.intClass + "',";
					}
									if ( Scheme_Show.intLength.ToString().Trim() != "" )
					{
						strColumns += "intLength,";
						strValues += "'"+Scheme_Show.intLength + "',";
					}
									if ( Scheme_Show.intDot.ToString().Trim() != "" )
					{
						strColumns += "intDot,";
						strValues += "'"+Scheme_Show.intDot + "',";
					}
									if ( Scheme_Show.intReadWrite.ToString().Trim() != "" )
					{
						strColumns += "intReadWrite,";
						strValues += "'"+Scheme_Show.intReadWrite + "',";
					}
									if (Scheme_Show.chrFormat!=null && Scheme_Show.chrFormat.ToString().Trim() != "" )
					{
						strColumns += "chrFormat,";
						strValues += "'"+Scheme_Show.chrFormat + "',";
					}
									if (Scheme_Show.chrContent!=null && Scheme_Show.chrContent.ToString().Trim() != "" )
					{
						strColumns += "chrContent,";
						strValues += "'"+Scheme_Show.chrContent + "',";
					}
									if (Scheme_Show.intCheck!=null && Scheme_Show.intCheck.ToString().Trim() != "" )
					{
						strColumns += "intCheck,";
						strValues += "'"+Scheme_Show.intCheck + "',";
					}
									if (Scheme_Show.c_Other1!=null && Scheme_Show.c_Other1.ToString().Trim() != "" )
					{
						strColumns += "c_Other1,";
						strValues += "'"+Scheme_Show.c_Other1 + "',";
					}
									if (Scheme_Show.c_Other2!=null && Scheme_Show.c_Other2.ToString().Trim() != "" )
					{
						strColumns += "c_Other2,";
						strValues += "'"+Scheme_Show.c_Other2 + "',";
					}
									if (Scheme_Show.c_Other3!=null && Scheme_Show.c_Other3.ToString().Trim() != "" )
					{
						strColumns += "c_Other3,";
						strValues += "'"+Scheme_Show.c_Other3 + "',";
					}
									if (Scheme_Show.c_Other4!=null && Scheme_Show.c_Other4.ToString().Trim() != "" )
					{
						strColumns += "c_Other4,";
						strValues += "'"+Scheme_Show.c_Other4 + "',";
					}
									if (Scheme_Show.c_Other5!=null && Scheme_Show.c_Other5.ToString().Trim() != "" )
					{
						strColumns += "c_Other5,";
						strValues += "'"+Scheme_Show.c_Other5 + "',";
					}
									if (Scheme_Show.c_Other6!=null && Scheme_Show.c_Other6.ToString().Trim() != "" )
					{
						strColumns += "c_Other6,";
						strValues += "'"+Scheme_Show.c_Other6 + "',";
					}
									if (Scheme_Show.c_Other7!=null && Scheme_Show.c_Other7.ToString().Trim() != "" )
					{
						strColumns += "c_Other7,";
						strValues += "'"+Scheme_Show.c_Other7 + "',";
					}
									if (Scheme_Show.c_Other8!=null && Scheme_Show.c_Other8.ToString().Trim() != "" )
					{
						strColumns += "c_Other8,";
						strValues += "'"+Scheme_Show.c_Other8 + "',";
					}
									if (Scheme_Show.c_Other9!=null && Scheme_Show.c_Other9.ToString().Trim() != "" )
					{
						strColumns += "c_Other9,";
						strValues += "'"+Scheme_Show.c_Other9 + "',";
					}
									if (Scheme_Show.c_Other10!=null && Scheme_Show.c_Other10.ToString().Trim() != "" )
					{
						strColumns += "c_Other10,";
						strValues += "'"+Scheme_Show.c_Other10 + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_Show",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_Show 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_Show  Scheme_Show,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if ( Scheme_Show.schemeID.ToString().Trim() != "" )
						setValues += "schemeID='"+ Scheme_Show .schemeID+"',";
								  	if (Scheme_Show.intFaName!=null && Scheme_Show.intFaName.ToString().Trim() != "" )
						setValues += "intFaName='"+ Scheme_Show .intFaName+"',";
								  	if ( Scheme_Show.intListNo.ToString().Trim() != "" )
						setValues += "intListNo='"+ Scheme_Show .intListNo+"',";
								  	if ( Scheme_Show.intShowType.ToString().Trim() != "" )
						setValues += "intShowType='"+ Scheme_Show .intShowType+"',";
								  	if ( Scheme_Show.intType.ToString().Trim() != "" )
						setValues += "intType='"+ Scheme_Show .intType+"',";
								  	if (Scheme_Show.chrType!=null && Scheme_Show.chrType.ToString().Trim() != "" )
						setValues += "chrType='"+ Scheme_Show .chrType+"',";
								  	if ( Scheme_Show.intSubItem.ToString().Trim() != "" )
						setValues += "intSubItem='"+ Scheme_Show .intSubItem+"',";
								  	if (Scheme_Show.chrSubItem!=null && Scheme_Show.chrSubItem.ToString().Trim() != "" )
						setValues += "chrSubItem='"+ Scheme_Show .chrSubItem+"',";
								  	if (Scheme_Show.chrID!=null && Scheme_Show.chrID.ToString().Trim() != "" )
						setValues += "chrID='"+ Scheme_Show .chrID+"',";
								  	if ( Scheme_Show.intClass.ToString().Trim() != "" )
						setValues += "intClass='"+ Scheme_Show .intClass+"',";
								  	if ( Scheme_Show.intLength.ToString().Trim() != "" )
						setValues += "intLength='"+ Scheme_Show .intLength+"',";
								  	if ( Scheme_Show.intDot.ToString().Trim() != "" )
						setValues += "intDot='"+ Scheme_Show .intDot+"',";
								  	if ( Scheme_Show.intReadWrite.ToString().Trim() != "" )
						setValues += "intReadWrite='"+ Scheme_Show .intReadWrite+"',";
								  	if (Scheme_Show.chrFormat!=null && Scheme_Show.chrFormat.ToString().Trim() != "" )
						setValues += "chrFormat='"+ Scheme_Show .chrFormat+"',";
								  	if (Scheme_Show.chrContent!=null && Scheme_Show.chrContent.ToString().Trim() != "" )
						setValues += "chrContent='"+ Scheme_Show .chrContent+"',";
								  	if (Scheme_Show.intCheck!=null && Scheme_Show.intCheck.ToString().Trim() != "" )
						setValues += "intCheck='"+ Scheme_Show .intCheck+"',";
								  	if (Scheme_Show.c_Other1!=null && Scheme_Show.c_Other1.ToString().Trim() != "" )
						setValues += "c_Other1='"+ Scheme_Show .c_Other1+"',";
								  	if (Scheme_Show.c_Other2!=null && Scheme_Show.c_Other2.ToString().Trim() != "" )
						setValues += "c_Other2='"+ Scheme_Show .c_Other2+"',";
								  	if (Scheme_Show.c_Other3!=null && Scheme_Show.c_Other3.ToString().Trim() != "" )
						setValues += "c_Other3='"+ Scheme_Show .c_Other3+"',";
								  	if (Scheme_Show.c_Other4!=null && Scheme_Show.c_Other4.ToString().Trim() != "" )
						setValues += "c_Other4='"+ Scheme_Show .c_Other4+"',";
								  	if (Scheme_Show.c_Other5!=null && Scheme_Show.c_Other5.ToString().Trim() != "" )
						setValues += "c_Other5='"+ Scheme_Show .c_Other5+"',";
								  	if (Scheme_Show.c_Other6!=null && Scheme_Show.c_Other6.ToString().Trim() != "" )
						setValues += "c_Other6='"+ Scheme_Show .c_Other6+"',";
								  	if (Scheme_Show.c_Other7!=null && Scheme_Show.c_Other7.ToString().Trim() != "" )
						setValues += "c_Other7='"+ Scheme_Show .c_Other7+"',";
								  	if (Scheme_Show.c_Other8!=null && Scheme_Show.c_Other8.ToString().Trim() != "" )
						setValues += "c_Other8='"+ Scheme_Show .c_Other8+"',";
								  	if (Scheme_Show.c_Other9!=null && Scheme_Show.c_Other9.ToString().Trim() != "" )
						setValues += "c_Other9='"+ Scheme_Show .c_Other9+"',";
								  	if (Scheme_Show.c_Other10!=null && Scheme_Show.c_Other10.ToString().Trim() != "" )
						setValues += "c_Other10='"+ Scheme_Show .c_Other10+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_Show",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_Show  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_Show",strWhere);
				return _SqlText;
			}
	}
}


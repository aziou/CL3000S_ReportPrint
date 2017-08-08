using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;
using System.Collections.Generic;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_Check
		public partial class Plan_Scheme_Check:Plan_Base
	{
		public Plan_Scheme_Check(int TaiType, string vFAName)
                : base(CLDC_DataCore.Const.Variable.CONST_ACCESS_FANAME, TaiType, vFAName)
		{
			DbHelperOleDb.connectionString=PubConstant.GetConnectionString(_FAPath,"");
		}
		
		/// <summary>
        /// 获取最大ID值
        /// </summary>
        /// <returns></returns>
        public int GetMaxId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select max(schemeID) from Scheme_Check");
            string result = DbHelperOleDb.GetSingle(strSql.ToString()).ToString();
            if(string.IsNullOrEmpty(result))
                result = "0";
            return Convert.ToInt32(result);
        }
		
		/// <summary>
		///  判断是否存在
		/// </summary>
		public bool Exists(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Scheme_Check");
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
			strSql.Append("select count(1) from Scheme_Check");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Scheme_Check model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_Check(");			
            strSql.Append("chrPlanName,chrEquipCateg,chrWiringMode,chrSchemeStatus,startDate,DisabledDate");
			strSql.Append(") values (");
            strSql.Append("@chrPlanName,@chrEquipCateg,@chrWiringMode,@chrSchemeStatus,@startDate,@DisabledDate");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@chrPlanName", OleDbType.VarChar,40) ,            
                        new OleDbParameter("@chrEquipCateg", OleDbType.VarChar,8) ,            
                        new OleDbParameter("@chrWiringMode", OleDbType.VarChar,8) ,            
                        new OleDbParameter("@chrSchemeStatus", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@startDate", OleDbType.Date) ,            
                        new OleDbParameter("@DisabledDate", OleDbType.Date)             
              
            };
			            
            parameters[0].Value = model.chrPlanName;                        
            parameters[1].Value = model.chrEquipCateg;                        
            parameters[2].Value = model.chrWiringMode;                        
            parameters[3].Value = model.chrSchemeStatus;                        
            parameters[4].Value = model.startDate;                        
            parameters[5].Value = model.DisabledDate;                        
			   
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
		public bool Update(Scheme_Check model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_Check set ");
			                                                
            strSql.Append(" chrPlanName = @chrPlanName , ");                                    
            strSql.Append(" chrEquipCateg = @chrEquipCateg , ");                                    
            strSql.Append(" chrWiringMode = @chrWiringMode , ");                                    
            strSql.Append(" chrSchemeStatus = @chrSchemeStatus , ");                                    
            strSql.Append(" startDate = @startDate , ");                                    
            strSql.Append(" DisabledDate = @DisabledDate  ");            			
			strSql.Append(" where schemeID=@schemeID ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@schemeID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrPlanName", OleDbType.VarChar,40) ,            
                        new OleDbParameter("@chrEquipCateg", OleDbType.VarChar,8) ,            
                        new OleDbParameter("@chrWiringMode", OleDbType.VarChar,8) ,            
                        new OleDbParameter("@chrSchemeStatus", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@startDate", OleDbType.Date) ,            
                        new OleDbParameter("@DisabledDate", OleDbType.Date)             
              
            };
			            
            parameters[6].Value = model.schemeID;                        
            parameters[7].Value = model.chrPlanName;                        
            parameters[8].Value = model.chrEquipCateg;                        
            parameters[9].Value = model.chrWiringMode;                        
            parameters[10].Value = model.chrSchemeStatus;                        
            parameters[11].Value = model.startDate;                        
            parameters[12].Value = model.DisabledDate;                        
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
			strSql.Append("delete from Scheme_Check ");
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
		public Scheme_Check GetModel(int schemeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select schemeID, chrPlanName, chrEquipCateg, chrWiringMode, chrSchemeStatus, startDate, DisabledDate  ");			
			strSql.Append("  from Scheme_Check ");
			strSql.Append(" where schemeID=@schemeID");
						OleDbParameter[] parameters = {
					new OleDbParameter("@schemeID", OleDbType.Integer,4)
			};
			parameters[0].Value = schemeID;

			
			Scheme_Check model=new Scheme_Check();
			DataSet ds=DbHelperOleDb.Query(strSql.ToString(),parameters);
			
			if(ds.Tables[0].Rows.Count>0)
			{
												if(ds.Tables[0].Rows[0]["schemeID"].ToString()!="")
				{
					model.schemeID=int.Parse(ds.Tables[0].Rows[0]["schemeID"].ToString());
				}
																																				model.chrPlanName= ds.Tables[0].Rows[0]["chrPlanName"].ToString();
																																model.chrEquipCateg= ds.Tables[0].Rows[0]["chrEquipCateg"].ToString();
																																model.chrWiringMode= ds.Tables[0].Rows[0]["chrWiringMode"].ToString();
																																model.chrSchemeStatus= ds.Tables[0].Rows[0]["chrSchemeStatus"].ToString();
																												if(ds.Tables[0].Rows[0]["startDate"].ToString()!="")
				{
					model.startDate=DateTime.Parse(ds.Tables[0].Rows[0]["startDate"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["DisabledDate"].ToString()!="")
				{
					model.DisabledDate=DateTime.Parse(ds.Tables[0].Rows[0]["DisabledDate"].ToString());
				}
																														
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
		public List<PlanModel.Scheme_Check> GetList(string strWhere,out string outErrString)
        {
            List<PlanModel.Scheme_Check> lstCheck=new List<Scheme_Check>();
            //TODO:fjk
            DataSet ds = GetList("");
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                Scheme_Check model = new Scheme_Check();
                if (dr["schemeID"].ToString() != "")
                {
                    model.schemeID = int.Parse(dr["schemeID"].ToString());
                }
                model.chrPlanName = dr["chrPlanName"].ToString();
                model.chrEquipCateg = dr["chrEquipCateg"].ToString();
                model.chrWiringMode = dr["chrWiringMode"].ToString();
                model.chrSchemeStatus = dr["chrSchemeStatus"].ToString();
                if (dr["startDate"].ToString() != "")
                {
                    model.startDate = DateTime.Parse(dr["startDate"].ToString());
                }
                if (dr["DisabledDate"].ToString() != "")
                {
                    model.DisabledDate = DateTime.Parse(dr["DisabledDate"].ToString());
                }

                lstCheck.Add(model);
            }
            outErrString="";
            return lstCheck;
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM Scheme_Check ");
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
			strSql.Append(" FROM Scheme_Check ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_Check 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_Check  Scheme_Check)
		{
			    string strColumns = "";
				string strValues = "";
									if ( Scheme_Check.schemeID.ToString().Trim() != "" )
					{
						strColumns += "schemeID,";
						strValues += "'"+Scheme_Check.schemeID + "',";
					}
									if (Scheme_Check.chrPlanName!=null && Scheme_Check.chrPlanName.ToString().Trim() != "" )
					{
						strColumns += "chrPlanName,";
						strValues += "'"+Scheme_Check.chrPlanName + "',";
					}
									if (Scheme_Check.chrEquipCateg!=null && Scheme_Check.chrEquipCateg.ToString().Trim() != "" )
					{
						strColumns += "chrEquipCateg,";
						strValues += "'"+Scheme_Check.chrEquipCateg + "',";
					}
									if (Scheme_Check.chrWiringMode!=null && Scheme_Check.chrWiringMode.ToString().Trim() != "" )
					{
						strColumns += "chrWiringMode,";
						strValues += "'"+Scheme_Check.chrWiringMode + "',";
					}
									if (Scheme_Check.chrSchemeStatus!=null && Scheme_Check.chrSchemeStatus.ToString().Trim() != "" )
					{
						strColumns += "chrSchemeStatus,";
						strValues += "'"+Scheme_Check.chrSchemeStatus + "',";
					}
									if (Scheme_Check.startDate!=null && Scheme_Check.startDate.ToString().Trim() != "" )
					{
						strColumns += "startDate,";
						strValues += "'"+Scheme_Check.startDate + "',";
					}
									if (Scheme_Check.DisabledDate!=null && Scheme_Check.DisabledDate.ToString().Trim() != "" )
					{
						strColumns += "DisabledDate,";
						strValues += "'"+Scheme_Check.DisabledDate + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_Check",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_Check 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_Check  Scheme_Check,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if ( Scheme_Check.schemeID.ToString().Trim() != "" )
						setValues += "schemeID='"+ Scheme_Check .schemeID+"',";
								  	if (Scheme_Check.chrPlanName!=null && Scheme_Check.chrPlanName.ToString().Trim() != "" )
						setValues += "chrPlanName='"+ Scheme_Check .chrPlanName+"',";
								  	if (Scheme_Check.chrEquipCateg!=null && Scheme_Check.chrEquipCateg.ToString().Trim() != "" )
						setValues += "chrEquipCateg='"+ Scheme_Check .chrEquipCateg+"',";
								  	if (Scheme_Check.chrWiringMode!=null && Scheme_Check.chrWiringMode.ToString().Trim() != "" )
						setValues += "chrWiringMode='"+ Scheme_Check .chrWiringMode+"',";
								  	if (Scheme_Check.chrSchemeStatus!=null && Scheme_Check.chrSchemeStatus.ToString().Trim() != "" )
						setValues += "chrSchemeStatus='"+ Scheme_Check .chrSchemeStatus+"',";
								  	if (Scheme_Check.startDate!=null && Scheme_Check.startDate.ToString().Trim() != "" )
						setValues += "startDate='"+ Scheme_Check .startDate+"',";
								  	if (Scheme_Check.DisabledDate!=null && Scheme_Check.DisabledDate.ToString().Trim() != "" )
						setValues += "DisabledDate='"+ Scheme_Check .DisabledDate+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_Check",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_Check  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_Check",strWhere);
				return _SqlText;
			}
	}
}


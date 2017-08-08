using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using CLDC_DataCore.DataBase;
using CLDC_DataCore.Model.Plan.PlanModel;

namespace CLDC_DataCore.Model.Plan
{
	 	//Scheme_Spec
		public partial class Plan_Scheme_Spec:Plan_Base
	{
		public Plan_Scheme_Spec(int TaiType, string vFAName)
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
			strSql.Append("select count(1) from Scheme_Spec");
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
			strSql.Append("select count(1) from Scheme_Spec");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperOleDb.GetSingle(strSql.ToString());
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Scheme_Spec model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Scheme_Spec(");			
            strSql.Append("intListNo,chrProjectName,chrJdfx,chrYj,chrGlys,sngULimitBL,sngLLimitBL,sngQCount,chrXieBo,chrNXX,sngXHz,sngXUa,sngXUb,sngXUc,sngXIa,sngXIb,sngXIc,sngPhi_Ua,sngPhi_Ub,sngPhi_Uc,sngPhi_Ia,sngPhi_Ib,sngPhi_Ic,chrXieBoChkUa,chrXieBoChkUb,chrXieBoChkUc,chrXieBoChkIa,chrXieBoChkIb,chrXieBoChkIc,chrXieBoFDUa,chrXieBoFDUb,chrXieBoFDUc,chrXieBoFDIa,chrXieBoFDIb,chrXieBoFDIc,chrXieBoXWUa,chrXieBoXWUb,chrXieBoXWUc,chrXieBoXWIa,chrXieBoXWIb,chrXieBoXWIc");
			strSql.Append(") values (");
            strSql.Append("@intListNo,@chrProjectName,@chrJdfx,@chrYj,@chrGlys,@sngULimitBL,@sngLLimitBL,@sngQCount,@chrXieBo,@chrNXX,@sngXHz,@sngXUa,@sngXUb,@sngXUc,@sngXIa,@sngXIb,@sngXIc,@sngPhi_Ua,@sngPhi_Ub,@sngPhi_Uc,@sngPhi_Ia,@sngPhi_Ib,@sngPhi_Ic,@chrXieBoChkUa,@chrXieBoChkUb,@chrXieBoChkUc,@chrXieBoChkIa,@chrXieBoChkIb,@chrXieBoChkIc,@chrXieBoFDUa,@chrXieBoFDUb,@chrXieBoFDUc,@chrXieBoFDIa,@chrXieBoFDIb,@chrXieBoFDIc,@chrXieBoXWUa,@chrXieBoXWUb,@chrXieBoXWUc,@chrXieBoXWIa,@chrXieBoXWIb,@chrXieBoXWIc");            
            strSql.Append(") ");            
            strSql.Append(";select @@IDENTITY");		
			OleDbParameter[] parameters = {
			            new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrProjectName", OleDbType.VarChar,20) ,            
                        new OleDbParameter("@chrJdfx", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrYj", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@chrGlys", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@sngULimitBL", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@sngLLimitBL", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@sngQCount", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrXieBo", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@chrNXX", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@sngXHz", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngXUa", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngXUb", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngXUc", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngXIa", OleDbType.Double) ,            
                        new OleDbParameter("@sngXIb", OleDbType.Double) ,            
                        new OleDbParameter("@sngXIc", OleDbType.Double) ,            
                        new OleDbParameter("@sngPhi_Ua", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngPhi_Ub", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngPhi_Uc", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngPhi_Ia", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngPhi_Ib", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngPhi_Ic", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrXieBoChkUa", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoChkUb", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoChkUc", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoChkIa", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoChkIb", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoChkIc", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoFDUa", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoFDUb", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoFDUc", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoFDIa", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoFDIb", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoFDIc", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoXWUa", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoXWUb", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoXWUc", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoXWIa", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoXWIb", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoXWIc", OleDbType.VarChar,50)             
              
            };
			            
            parameters[0].Value = model.intListNo;                        
            parameters[1].Value = model.chrProjectName;                        
            parameters[2].Value = model.chrJdfx;                        
            parameters[3].Value = model.chrYj;                        
            parameters[4].Value = model.chrGlys;                        
            parameters[5].Value = model.sngULimitBL;                        
            parameters[6].Value = model.sngLLimitBL;                        
            parameters[7].Value = model.sngQCount;                        
            parameters[8].Value = model.chrXieBo;                        
            parameters[9].Value = model.chrNXX;                        
            parameters[10].Value = model.sngXHz;                        
            parameters[11].Value = model.sngXUa;                        
            parameters[12].Value = model.sngXUb;                        
            parameters[13].Value = model.sngXUc;                        
            parameters[14].Value = model.sngXIa;                        
            parameters[15].Value = model.sngXIb;                        
            parameters[16].Value = model.sngXIc;                        
            parameters[17].Value = model.sngPhi_Ua;                        
            parameters[18].Value = model.sngPhi_Ub;                        
            parameters[19].Value = model.sngPhi_Uc;                        
            parameters[20].Value = model.sngPhi_Ia;                        
            parameters[21].Value = model.sngPhi_Ib;                        
            parameters[22].Value = model.sngPhi_Ic;                        
            parameters[23].Value = model.chrXieBoChkUa;                        
            parameters[24].Value = model.chrXieBoChkUb;                        
            parameters[25].Value = model.chrXieBoChkUc;                        
            parameters[26].Value = model.chrXieBoChkIa;                        
            parameters[27].Value = model.chrXieBoChkIb;                        
            parameters[28].Value = model.chrXieBoChkIc;                        
            parameters[29].Value = model.chrXieBoFDUa;                        
            parameters[30].Value = model.chrXieBoFDUb;                        
            parameters[31].Value = model.chrXieBoFDUc;                        
            parameters[32].Value = model.chrXieBoFDIa;                        
            parameters[33].Value = model.chrXieBoFDIb;                        
            parameters[34].Value = model.chrXieBoFDIc;                        
            parameters[35].Value = model.chrXieBoXWUa;                        
            parameters[36].Value = model.chrXieBoXWUb;                        
            parameters[37].Value = model.chrXieBoXWUc;                        
            parameters[38].Value = model.chrXieBoXWIa;                        
            parameters[39].Value = model.chrXieBoXWIb;                        
            parameters[40].Value = model.chrXieBoXWIc;                        
			   
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
		public bool Update(Scheme_Spec model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Scheme_Spec set ");
			                                                
            strSql.Append(" intListNo = @intListNo , ");                                    
            strSql.Append(" chrProjectName = @chrProjectName , ");                                    
            strSql.Append(" chrJdfx = @chrJdfx , ");                                    
            strSql.Append(" chrYj = @chrYj , ");                                    
            strSql.Append(" chrGlys = @chrGlys , ");                                    
            strSql.Append(" sngULimitBL = @sngULimitBL , ");                                    
            strSql.Append(" sngLLimitBL = @sngLLimitBL , ");                                    
            strSql.Append(" sngQCount = @sngQCount , ");                                    
            strSql.Append(" chrXieBo = @chrXieBo , ");                                    
            strSql.Append(" chrNXX = @chrNXX , ");                                    
            strSql.Append(" sngXHz = @sngXHz , ");                                    
            strSql.Append(" sngXUa = @sngXUa , ");                                    
            strSql.Append(" sngXUb = @sngXUb , ");                                    
            strSql.Append(" sngXUc = @sngXUc , ");                                    
            strSql.Append(" sngXIa = @sngXIa , ");                                    
            strSql.Append(" sngXIb = @sngXIb , ");                                    
            strSql.Append(" sngXIc = @sngXIc , ");                                    
            strSql.Append(" sngPhi_Ua = @sngPhi_Ua , ");                                    
            strSql.Append(" sngPhi_Ub = @sngPhi_Ub , ");                                    
            strSql.Append(" sngPhi_Uc = @sngPhi_Uc , ");                                    
            strSql.Append(" sngPhi_Ia = @sngPhi_Ia , ");                                    
            strSql.Append(" sngPhi_Ib = @sngPhi_Ib , ");                                    
            strSql.Append(" sngPhi_Ic = @sngPhi_Ic , ");                                    
            strSql.Append(" chrXieBoChkUa = @chrXieBoChkUa , ");                                    
            strSql.Append(" chrXieBoChkUb = @chrXieBoChkUb , ");                                    
            strSql.Append(" chrXieBoChkUc = @chrXieBoChkUc , ");                                    
            strSql.Append(" chrXieBoChkIa = @chrXieBoChkIa , ");                                    
            strSql.Append(" chrXieBoChkIb = @chrXieBoChkIb , ");                                    
            strSql.Append(" chrXieBoChkIc = @chrXieBoChkIc , ");                                    
            strSql.Append(" chrXieBoFDUa = @chrXieBoFDUa , ");                                    
            strSql.Append(" chrXieBoFDUb = @chrXieBoFDUb , ");                                    
            strSql.Append(" chrXieBoFDUc = @chrXieBoFDUc , ");                                    
            strSql.Append(" chrXieBoFDIa = @chrXieBoFDIa , ");                                    
            strSql.Append(" chrXieBoFDIb = @chrXieBoFDIb , ");                                    
            strSql.Append(" chrXieBoFDIc = @chrXieBoFDIc , ");                                    
            strSql.Append(" chrXieBoXWUa = @chrXieBoXWUa , ");                                    
            strSql.Append(" chrXieBoXWUb = @chrXieBoXWUb , ");                                    
            strSql.Append(" chrXieBoXWUc = @chrXieBoXWUc , ");                                    
            strSql.Append(" chrXieBoXWIa = @chrXieBoXWIa , ");                                    
            strSql.Append(" chrXieBoXWIb = @chrXieBoXWIb , ");                                    
            strSql.Append(" chrXieBoXWIc = @chrXieBoXWIc  ");            			
			strSql.Append(" where schemeID=@schemeID ");
						
OleDbParameter[] parameters = {
			            new OleDbParameter("@schemeID", OleDbType.Integer,4) ,            
                        new OleDbParameter("@intListNo", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrProjectName", OleDbType.VarChar,20) ,            
                        new OleDbParameter("@chrJdfx", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrYj", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@chrGlys", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@sngULimitBL", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@sngLLimitBL", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@sngQCount", OleDbType.VarChar,10) ,            
                        new OleDbParameter("@chrXieBo", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@chrNXX", OleDbType.VarChar,1) ,            
                        new OleDbParameter("@sngXHz", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngXUa", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngXUb", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngXUc", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngXIa", OleDbType.Double) ,            
                        new OleDbParameter("@sngXIb", OleDbType.Double) ,            
                        new OleDbParameter("@sngXIc", OleDbType.Double) ,            
                        new OleDbParameter("@sngPhi_Ua", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngPhi_Ub", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngPhi_Uc", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngPhi_Ia", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngPhi_Ib", OleDbType.Integer,4) ,            
                        new OleDbParameter("@sngPhi_Ic", OleDbType.Integer,4) ,            
                        new OleDbParameter("@chrXieBoChkUa", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoChkUb", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoChkUc", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoChkIa", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoChkIb", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoChkIc", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoFDUa", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoFDUb", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoFDUc", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoFDIa", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoFDIb", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoFDIc", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoXWUa", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoXWUb", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoXWUc", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoXWIa", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoXWIb", OleDbType.VarChar,50) ,            
                        new OleDbParameter("@chrXieBoXWIc", OleDbType.VarChar,50)             
              
            };
			            
            parameters[41].Value = model.schemeID;                        
            parameters[42].Value = model.intListNo;                        
            parameters[43].Value = model.chrProjectName;                        
            parameters[44].Value = model.chrJdfx;                        
            parameters[45].Value = model.chrYj;                        
            parameters[46].Value = model.chrGlys;                        
            parameters[47].Value = model.sngULimitBL;                        
            parameters[48].Value = model.sngLLimitBL;                        
            parameters[49].Value = model.sngQCount;                        
            parameters[50].Value = model.chrXieBo;                        
            parameters[51].Value = model.chrNXX;                        
            parameters[52].Value = model.sngXHz;                        
            parameters[53].Value = model.sngXUa;                        
            parameters[54].Value = model.sngXUb;                        
            parameters[55].Value = model.sngXUc;                        
            parameters[56].Value = model.sngXIa;                        
            parameters[57].Value = model.sngXIb;                        
            parameters[58].Value = model.sngXIc;                        
            parameters[59].Value = model.sngPhi_Ua;                        
            parameters[60].Value = model.sngPhi_Ub;                        
            parameters[61].Value = model.sngPhi_Uc;                        
            parameters[62].Value = model.sngPhi_Ia;                        
            parameters[63].Value = model.sngPhi_Ib;                        
            parameters[64].Value = model.sngPhi_Ic;                        
            parameters[65].Value = model.chrXieBoChkUa;                        
            parameters[66].Value = model.chrXieBoChkUb;                        
            parameters[67].Value = model.chrXieBoChkUc;                        
            parameters[68].Value = model.chrXieBoChkIa;                        
            parameters[69].Value = model.chrXieBoChkIb;                        
            parameters[70].Value = model.chrXieBoChkIc;                        
            parameters[71].Value = model.chrXieBoFDUa;                        
            parameters[72].Value = model.chrXieBoFDUb;                        
            parameters[73].Value = model.chrXieBoFDUc;                        
            parameters[74].Value = model.chrXieBoFDIa;                        
            parameters[75].Value = model.chrXieBoFDIb;                        
            parameters[76].Value = model.chrXieBoFDIc;                        
            parameters[77].Value = model.chrXieBoXWUa;                        
            parameters[78].Value = model.chrXieBoXWUb;                        
            parameters[79].Value = model.chrXieBoXWUc;                        
            parameters[80].Value = model.chrXieBoXWIa;                        
            parameters[81].Value = model.chrXieBoXWIb;                        
            parameters[82].Value = model.chrXieBoXWIc;                        
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
			strSql.Append("delete from Scheme_Spec ");
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
		public Scheme_Spec GetModel(int schemeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select schemeID, intListNo, chrProjectName, chrJdfx, chrYj, chrGlys, sngULimitBL, sngLLimitBL, sngQCount, chrXieBo, chrNXX, sngXHz, sngXUa, sngXUb, sngXUc, sngXIa, sngXIb, sngXIc, sngPhi_Ua, sngPhi_Ub, sngPhi_Uc, sngPhi_Ia, sngPhi_Ib, sngPhi_Ic, chrXieBoChkUa, chrXieBoChkUb, chrXieBoChkUc, chrXieBoChkIa, chrXieBoChkIb, chrXieBoChkIc, chrXieBoFDUa, chrXieBoFDUb, chrXieBoFDUc, chrXieBoFDIa, chrXieBoFDIb, chrXieBoFDIc, chrXieBoXWUa, chrXieBoXWUb, chrXieBoXWUc, chrXieBoXWIa, chrXieBoXWIb, chrXieBoXWIc  ");			
			strSql.Append("  from Scheme_Spec ");
			strSql.Append(" where schemeID=@schemeID");
						OleDbParameter[] parameters = {
					new OleDbParameter("@schemeID", OleDbType.Integer,4)
			};
			parameters[0].Value = schemeID;

			
			Scheme_Spec model=new Scheme_Spec();
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
																																				model.chrProjectName= ds.Tables[0].Rows[0]["chrProjectName"].ToString();
																																model.chrJdfx= ds.Tables[0].Rows[0]["chrJdfx"].ToString();
																																model.chrYj= ds.Tables[0].Rows[0]["chrYj"].ToString();
																																model.chrGlys= ds.Tables[0].Rows[0]["chrGlys"].ToString();
																																model.sngULimitBL= ds.Tables[0].Rows[0]["sngULimitBL"].ToString();
																																model.sngLLimitBL= ds.Tables[0].Rows[0]["sngLLimitBL"].ToString();
																																model.sngQCount= ds.Tables[0].Rows[0]["sngQCount"].ToString();
																																model.chrXieBo= ds.Tables[0].Rows[0]["chrXieBo"].ToString();
																																model.chrNXX= ds.Tables[0].Rows[0]["chrNXX"].ToString();
																												if(ds.Tables[0].Rows[0]["sngXHz"].ToString()!="")
				{
					model.sngXHz=int.Parse(ds.Tables[0].Rows[0]["sngXHz"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["sngXUa"].ToString()!="")
				{
					model.sngXUa=int.Parse(ds.Tables[0].Rows[0]["sngXUa"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["sngXUb"].ToString()!="")
				{
					model.sngXUb=int.Parse(ds.Tables[0].Rows[0]["sngXUb"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["sngXUc"].ToString()!="")
				{
					model.sngXUc=int.Parse(ds.Tables[0].Rows[0]["sngXUc"].ToString());
				}
																																																																																																								if(ds.Tables[0].Rows[0]["sngPhi_Ua"].ToString()!="")
				{
					model.sngPhi_Ua=int.Parse(ds.Tables[0].Rows[0]["sngPhi_Ua"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["sngPhi_Ub"].ToString()!="")
				{
					model.sngPhi_Ub=int.Parse(ds.Tables[0].Rows[0]["sngPhi_Ub"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["sngPhi_Uc"].ToString()!="")
				{
					model.sngPhi_Uc=int.Parse(ds.Tables[0].Rows[0]["sngPhi_Uc"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["sngPhi_Ia"].ToString()!="")
				{
					model.sngPhi_Ia=int.Parse(ds.Tables[0].Rows[0]["sngPhi_Ia"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["sngPhi_Ib"].ToString()!="")
				{
					model.sngPhi_Ib=int.Parse(ds.Tables[0].Rows[0]["sngPhi_Ib"].ToString());
				}
																																if(ds.Tables[0].Rows[0]["sngPhi_Ic"].ToString()!="")
				{
					model.sngPhi_Ic=int.Parse(ds.Tables[0].Rows[0]["sngPhi_Ic"].ToString());
				}
																																				model.chrXieBoChkUa= ds.Tables[0].Rows[0]["chrXieBoChkUa"].ToString();
																																model.chrXieBoChkUb= ds.Tables[0].Rows[0]["chrXieBoChkUb"].ToString();
																																model.chrXieBoChkUc= ds.Tables[0].Rows[0]["chrXieBoChkUc"].ToString();
																																model.chrXieBoChkIa= ds.Tables[0].Rows[0]["chrXieBoChkIa"].ToString();
																																model.chrXieBoChkIb= ds.Tables[0].Rows[0]["chrXieBoChkIb"].ToString();
																																model.chrXieBoChkIc= ds.Tables[0].Rows[0]["chrXieBoChkIc"].ToString();
																																model.chrXieBoFDUa= ds.Tables[0].Rows[0]["chrXieBoFDUa"].ToString();
																																model.chrXieBoFDUb= ds.Tables[0].Rows[0]["chrXieBoFDUb"].ToString();
																																model.chrXieBoFDUc= ds.Tables[0].Rows[0]["chrXieBoFDUc"].ToString();
																																model.chrXieBoFDIa= ds.Tables[0].Rows[0]["chrXieBoFDIa"].ToString();
																																model.chrXieBoFDIb= ds.Tables[0].Rows[0]["chrXieBoFDIb"].ToString();
																																model.chrXieBoFDIc= ds.Tables[0].Rows[0]["chrXieBoFDIc"].ToString();
																																model.chrXieBoXWUa= ds.Tables[0].Rows[0]["chrXieBoXWUa"].ToString();
																																model.chrXieBoXWUb= ds.Tables[0].Rows[0]["chrXieBoXWUb"].ToString();
																																model.chrXieBoXWUc= ds.Tables[0].Rows[0]["chrXieBoXWUc"].ToString();
																																model.chrXieBoXWIa= ds.Tables[0].Rows[0]["chrXieBoXWIa"].ToString();
																																model.chrXieBoXWIb= ds.Tables[0].Rows[0]["chrXieBoXWIb"].ToString();
																																model.chrXieBoXWIc= ds.Tables[0].Rows[0]["chrXieBoXWIc"].ToString();
																										
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
			strSql.Append(" FROM Scheme_Spec ");
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
			strSql.Append(" FROM Scheme_Spec ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperOleDb.Query(strSql.ToString());
		}   
		
		/// <summary>
		/// Scheme_Spec 插入SQL语句
		/// </summary>
		public  string InsertSQL(Scheme_Spec  Scheme_Spec)
		{
			    string strColumns = "";
				string strValues = "";
									if ( Scheme_Spec.schemeID.ToString().Trim() != "" )
					{
						strColumns += "schemeID,";
						strValues += "'"+Scheme_Spec.schemeID + "',";
					}
									if ( Scheme_Spec.intListNo.ToString().Trim() != "" )
					{
						strColumns += "intListNo,";
						strValues += "'"+Scheme_Spec.intListNo + "',";
					}
									if (Scheme_Spec.chrProjectName!=null && Scheme_Spec.chrProjectName.ToString().Trim() != "" )
					{
						strColumns += "chrProjectName,";
						strValues += "'"+Scheme_Spec.chrProjectName + "',";
					}
									if (Scheme_Spec.chrJdfx!=null && Scheme_Spec.chrJdfx.ToString().Trim() != "" )
					{
						strColumns += "chrJdfx,";
						strValues += "'"+Scheme_Spec.chrJdfx + "',";
					}
									if (Scheme_Spec.chrYj!=null && Scheme_Spec.chrYj.ToString().Trim() != "" )
					{
						strColumns += "chrYj,";
						strValues += "'"+Scheme_Spec.chrYj + "',";
					}
									if (Scheme_Spec.chrGlys!=null && Scheme_Spec.chrGlys.ToString().Trim() != "" )
					{
						strColumns += "chrGlys,";
						strValues += "'"+Scheme_Spec.chrGlys + "',";
					}
									if (Scheme_Spec.sngULimitBL!=null && Scheme_Spec.sngULimitBL.ToString().Trim() != "" )
					{
						strColumns += "sngULimitBL,";
						strValues += "'"+Scheme_Spec.sngULimitBL + "',";
					}
									if (Scheme_Spec.sngLLimitBL!=null && Scheme_Spec.sngLLimitBL.ToString().Trim() != "" )
					{
						strColumns += "sngLLimitBL,";
						strValues += "'"+Scheme_Spec.sngLLimitBL + "',";
					}
									if (Scheme_Spec.sngQCount!=null && Scheme_Spec.sngQCount.ToString().Trim() != "" )
					{
						strColumns += "sngQCount,";
						strValues += "'"+Scheme_Spec.sngQCount + "',";
					}
									if (Scheme_Spec.chrXieBo!=null && Scheme_Spec.chrXieBo.ToString().Trim() != "" )
					{
						strColumns += "chrXieBo,";
						strValues += "'"+Scheme_Spec.chrXieBo + "',";
					}
									if (Scheme_Spec.chrNXX!=null && Scheme_Spec.chrNXX.ToString().Trim() != "" )
					{
						strColumns += "chrNXX,";
						strValues += "'"+Scheme_Spec.chrNXX + "',";
					}
									if ( Scheme_Spec.sngXHz.ToString().Trim() != "" )
					{
						strColumns += "sngXHz,";
						strValues += "'"+Scheme_Spec.sngXHz + "',";
					}
									if ( Scheme_Spec.sngXUa.ToString().Trim() != "" )
					{
						strColumns += "sngXUa,";
						strValues += "'"+Scheme_Spec.sngXUa + "',";
					}
									if ( Scheme_Spec.sngXUb.ToString().Trim() != "" )
					{
						strColumns += "sngXUb,";
						strValues += "'"+Scheme_Spec.sngXUb + "',";
					}
									if ( Scheme_Spec.sngXUc.ToString().Trim() != "" )
					{
						strColumns += "sngXUc,";
						strValues += "'"+Scheme_Spec.sngXUc + "',";
					}
									if ( Scheme_Spec.sngXIa.ToString().Trim() != "" )
					{
						strColumns += "sngXIa,";
						strValues += "'"+Scheme_Spec.sngXIa + "',";
					}
									if ( Scheme_Spec.sngXIb.ToString().Trim() != "" )
					{
						strColumns += "sngXIb,";
						strValues += "'"+Scheme_Spec.sngXIb + "',";
					}
									if ( Scheme_Spec.sngXIc.ToString().Trim() != "" )
					{
						strColumns += "sngXIc,";
						strValues += "'"+Scheme_Spec.sngXIc + "',";
					}
									if ( Scheme_Spec.sngPhi_Ua.ToString().Trim() != "" )
					{
						strColumns += "sngPhi_Ua,";
						strValues += "'"+Scheme_Spec.sngPhi_Ua + "',";
					}
									if ( Scheme_Spec.sngPhi_Ub.ToString().Trim() != "" )
					{
						strColumns += "sngPhi_Ub,";
						strValues += "'"+Scheme_Spec.sngPhi_Ub + "',";
					}
									if ( Scheme_Spec.sngPhi_Uc.ToString().Trim() != "" )
					{
						strColumns += "sngPhi_Uc,";
						strValues += "'"+Scheme_Spec.sngPhi_Uc + "',";
					}
									if ( Scheme_Spec.sngPhi_Ia.ToString().Trim() != "" )
					{
						strColumns += "sngPhi_Ia,";
						strValues += "'"+Scheme_Spec.sngPhi_Ia + "',";
					}
									if ( Scheme_Spec.sngPhi_Ib.ToString().Trim() != "" )
					{
						strColumns += "sngPhi_Ib,";
						strValues += "'"+Scheme_Spec.sngPhi_Ib + "',";
					}
									if ( Scheme_Spec.sngPhi_Ic.ToString().Trim() != "" )
					{
						strColumns += "sngPhi_Ic,";
						strValues += "'"+Scheme_Spec.sngPhi_Ic + "',";
					}
									if (Scheme_Spec.chrXieBoChkUa!=null && Scheme_Spec.chrXieBoChkUa.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoChkUa,";
						strValues += "'"+Scheme_Spec.chrXieBoChkUa + "',";
					}
									if (Scheme_Spec.chrXieBoChkUb!=null && Scheme_Spec.chrXieBoChkUb.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoChkUb,";
						strValues += "'"+Scheme_Spec.chrXieBoChkUb + "',";
					}
									if (Scheme_Spec.chrXieBoChkUc!=null && Scheme_Spec.chrXieBoChkUc.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoChkUc,";
						strValues += "'"+Scheme_Spec.chrXieBoChkUc + "',";
					}
									if (Scheme_Spec.chrXieBoChkIa!=null && Scheme_Spec.chrXieBoChkIa.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoChkIa,";
						strValues += "'"+Scheme_Spec.chrXieBoChkIa + "',";
					}
									if (Scheme_Spec.chrXieBoChkIb!=null && Scheme_Spec.chrXieBoChkIb.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoChkIb,";
						strValues += "'"+Scheme_Spec.chrXieBoChkIb + "',";
					}
									if (Scheme_Spec.chrXieBoChkIc!=null && Scheme_Spec.chrXieBoChkIc.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoChkIc,";
						strValues += "'"+Scheme_Spec.chrXieBoChkIc + "',";
					}
									if (Scheme_Spec.chrXieBoFDUa!=null && Scheme_Spec.chrXieBoFDUa.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoFDUa,";
						strValues += "'"+Scheme_Spec.chrXieBoFDUa + "',";
					}
									if (Scheme_Spec.chrXieBoFDUb!=null && Scheme_Spec.chrXieBoFDUb.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoFDUb,";
						strValues += "'"+Scheme_Spec.chrXieBoFDUb + "',";
					}
									if (Scheme_Spec.chrXieBoFDUc!=null && Scheme_Spec.chrXieBoFDUc.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoFDUc,";
						strValues += "'"+Scheme_Spec.chrXieBoFDUc + "',";
					}
									if (Scheme_Spec.chrXieBoFDIa!=null && Scheme_Spec.chrXieBoFDIa.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoFDIa,";
						strValues += "'"+Scheme_Spec.chrXieBoFDIa + "',";
					}
									if (Scheme_Spec.chrXieBoFDIb!=null && Scheme_Spec.chrXieBoFDIb.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoFDIb,";
						strValues += "'"+Scheme_Spec.chrXieBoFDIb + "',";
					}
									if (Scheme_Spec.chrXieBoFDIc!=null && Scheme_Spec.chrXieBoFDIc.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoFDIc,";
						strValues += "'"+Scheme_Spec.chrXieBoFDIc + "',";
					}
									if (Scheme_Spec.chrXieBoXWUa!=null && Scheme_Spec.chrXieBoXWUa.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoXWUa,";
						strValues += "'"+Scheme_Spec.chrXieBoXWUa + "',";
					}
									if (Scheme_Spec.chrXieBoXWUb!=null && Scheme_Spec.chrXieBoXWUb.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoXWUb,";
						strValues += "'"+Scheme_Spec.chrXieBoXWUb + "',";
					}
									if (Scheme_Spec.chrXieBoXWUc!=null && Scheme_Spec.chrXieBoXWUc.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoXWUc,";
						strValues += "'"+Scheme_Spec.chrXieBoXWUc + "',";
					}
									if (Scheme_Spec.chrXieBoXWIa!=null && Scheme_Spec.chrXieBoXWIa.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoXWIa,";
						strValues += "'"+Scheme_Spec.chrXieBoXWIa + "',";
					}
									if (Scheme_Spec.chrXieBoXWIb!=null && Scheme_Spec.chrXieBoXWIb.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoXWIb,";
						strValues += "'"+Scheme_Spec.chrXieBoXWIb + "',";
					}
									if (Scheme_Spec.chrXieBoXWIc!=null && Scheme_Spec.chrXieBoXWIc.ToString().Trim() != "" )
					{
						strColumns += "chrXieBoXWIc,";
						strValues += "'"+Scheme_Spec.chrXieBoXWIc + "',";
					}
								
				strColumns = strColumns.Substring(0, strColumns.Length - 1);
				strValues = strValues.Substring(0, strValues.Length - 1);
				string _SqlText = string.Format("insert  into {0}({1})  values({2})","Scheme_Spec",strColumns,strValues);
				return _SqlText;
			}
			
			/// <summary>
		    /// Scheme_Spec 修改SQL语句,不支持并发修改使用
		    /// </summary>
			public string UpdateSQL(Scheme_Spec  Scheme_Spec,string strWhere)
			{
				if (string.IsNullOrEmpty(strWhere))
                {
                    strWhere = "1=1";
                }
				string setValues = "";
								  	if ( Scheme_Spec.schemeID.ToString().Trim() != "" )
						setValues += "schemeID='"+ Scheme_Spec .schemeID+"',";
								  	if ( Scheme_Spec.intListNo.ToString().Trim() != "" )
						setValues += "intListNo='"+ Scheme_Spec .intListNo+"',";
								  	if (Scheme_Spec.chrProjectName!=null && Scheme_Spec.chrProjectName.ToString().Trim() != "" )
						setValues += "chrProjectName='"+ Scheme_Spec .chrProjectName+"',";
								  	if (Scheme_Spec.chrJdfx!=null && Scheme_Spec.chrJdfx.ToString().Trim() != "" )
						setValues += "chrJdfx='"+ Scheme_Spec .chrJdfx+"',";
								  	if (Scheme_Spec.chrYj!=null && Scheme_Spec.chrYj.ToString().Trim() != "" )
						setValues += "chrYj='"+ Scheme_Spec .chrYj+"',";
								  	if (Scheme_Spec.chrGlys!=null && Scheme_Spec.chrGlys.ToString().Trim() != "" )
						setValues += "chrGlys='"+ Scheme_Spec .chrGlys+"',";
								  	if (Scheme_Spec.sngULimitBL!=null && Scheme_Spec.sngULimitBL.ToString().Trim() != "" )
						setValues += "sngULimitBL='"+ Scheme_Spec .sngULimitBL+"',";
								  	if (Scheme_Spec.sngLLimitBL!=null && Scheme_Spec.sngLLimitBL.ToString().Trim() != "" )
						setValues += "sngLLimitBL='"+ Scheme_Spec .sngLLimitBL+"',";
								  	if (Scheme_Spec.sngQCount!=null && Scheme_Spec.sngQCount.ToString().Trim() != "" )
						setValues += "sngQCount='"+ Scheme_Spec .sngQCount+"',";
								  	if (Scheme_Spec.chrXieBo!=null && Scheme_Spec.chrXieBo.ToString().Trim() != "" )
						setValues += "chrXieBo='"+ Scheme_Spec .chrXieBo+"',";
								  	if (Scheme_Spec.chrNXX!=null && Scheme_Spec.chrNXX.ToString().Trim() != "" )
						setValues += "chrNXX='"+ Scheme_Spec .chrNXX+"',";
								  	if ( Scheme_Spec.sngXHz.ToString().Trim() != "" )
						setValues += "sngXHz='"+ Scheme_Spec .sngXHz+"',";
								  	if ( Scheme_Spec.sngXUa.ToString().Trim() != "" )
						setValues += "sngXUa='"+ Scheme_Spec .sngXUa+"',";
								  	if ( Scheme_Spec.sngXUb.ToString().Trim() != "" )
						setValues += "sngXUb='"+ Scheme_Spec .sngXUb+"',";
								  	if ( Scheme_Spec.sngXUc.ToString().Trim() != "" )
						setValues += "sngXUc='"+ Scheme_Spec .sngXUc+"',";
								  	if ( Scheme_Spec.sngXIa.ToString().Trim() != "" )
						setValues += "sngXIa='"+ Scheme_Spec .sngXIa+"',";
								  	if ( Scheme_Spec.sngXIb.ToString().Trim() != "" )
						setValues += "sngXIb='"+ Scheme_Spec .sngXIb+"',";
								  	if ( Scheme_Spec.sngXIc.ToString().Trim() != "" )
						setValues += "sngXIc='"+ Scheme_Spec .sngXIc+"',";
								  	if ( Scheme_Spec.sngPhi_Ua.ToString().Trim() != "" )
						setValues += "sngPhi_Ua='"+ Scheme_Spec .sngPhi_Ua+"',";
								  	if ( Scheme_Spec.sngPhi_Ub.ToString().Trim() != "" )
						setValues += "sngPhi_Ub='"+ Scheme_Spec .sngPhi_Ub+"',";
								  	if ( Scheme_Spec.sngPhi_Uc.ToString().Trim() != "" )
						setValues += "sngPhi_Uc='"+ Scheme_Spec .sngPhi_Uc+"',";
								  	if ( Scheme_Spec.sngPhi_Ia.ToString().Trim() != "" )
						setValues += "sngPhi_Ia='"+ Scheme_Spec .sngPhi_Ia+"',";
								  	if ( Scheme_Spec.sngPhi_Ib.ToString().Trim() != "" )
						setValues += "sngPhi_Ib='"+ Scheme_Spec .sngPhi_Ib+"',";
								  	if ( Scheme_Spec.sngPhi_Ic.ToString().Trim() != "" )
						setValues += "sngPhi_Ic='"+ Scheme_Spec .sngPhi_Ic+"',";
								  	if (Scheme_Spec.chrXieBoChkUa!=null && Scheme_Spec.chrXieBoChkUa.ToString().Trim() != "" )
						setValues += "chrXieBoChkUa='"+ Scheme_Spec .chrXieBoChkUa+"',";
								  	if (Scheme_Spec.chrXieBoChkUb!=null && Scheme_Spec.chrXieBoChkUb.ToString().Trim() != "" )
						setValues += "chrXieBoChkUb='"+ Scheme_Spec .chrXieBoChkUb+"',";
								  	if (Scheme_Spec.chrXieBoChkUc!=null && Scheme_Spec.chrXieBoChkUc.ToString().Trim() != "" )
						setValues += "chrXieBoChkUc='"+ Scheme_Spec .chrXieBoChkUc+"',";
								  	if (Scheme_Spec.chrXieBoChkIa!=null && Scheme_Spec.chrXieBoChkIa.ToString().Trim() != "" )
						setValues += "chrXieBoChkIa='"+ Scheme_Spec .chrXieBoChkIa+"',";
								  	if (Scheme_Spec.chrXieBoChkIb!=null && Scheme_Spec.chrXieBoChkIb.ToString().Trim() != "" )
						setValues += "chrXieBoChkIb='"+ Scheme_Spec .chrXieBoChkIb+"',";
								  	if (Scheme_Spec.chrXieBoChkIc!=null && Scheme_Spec.chrXieBoChkIc.ToString().Trim() != "" )
						setValues += "chrXieBoChkIc='"+ Scheme_Spec .chrXieBoChkIc+"',";
								  	if (Scheme_Spec.chrXieBoFDUa!=null && Scheme_Spec.chrXieBoFDUa.ToString().Trim() != "" )
						setValues += "chrXieBoFDUa='"+ Scheme_Spec .chrXieBoFDUa+"',";
								  	if (Scheme_Spec.chrXieBoFDUb!=null && Scheme_Spec.chrXieBoFDUb.ToString().Trim() != "" )
						setValues += "chrXieBoFDUb='"+ Scheme_Spec .chrXieBoFDUb+"',";
								  	if (Scheme_Spec.chrXieBoFDUc!=null && Scheme_Spec.chrXieBoFDUc.ToString().Trim() != "" )
						setValues += "chrXieBoFDUc='"+ Scheme_Spec .chrXieBoFDUc+"',";
								  	if (Scheme_Spec.chrXieBoFDIa!=null && Scheme_Spec.chrXieBoFDIa.ToString().Trim() != "" )
						setValues += "chrXieBoFDIa='"+ Scheme_Spec .chrXieBoFDIa+"',";
								  	if (Scheme_Spec.chrXieBoFDIb!=null && Scheme_Spec.chrXieBoFDIb.ToString().Trim() != "" )
						setValues += "chrXieBoFDIb='"+ Scheme_Spec .chrXieBoFDIb+"',";
								  	if (Scheme_Spec.chrXieBoFDIc!=null && Scheme_Spec.chrXieBoFDIc.ToString().Trim() != "" )
						setValues += "chrXieBoFDIc='"+ Scheme_Spec .chrXieBoFDIc+"',";
								  	if (Scheme_Spec.chrXieBoXWUa!=null && Scheme_Spec.chrXieBoXWUa.ToString().Trim() != "" )
						setValues += "chrXieBoXWUa='"+ Scheme_Spec .chrXieBoXWUa+"',";
								  	if (Scheme_Spec.chrXieBoXWUb!=null && Scheme_Spec.chrXieBoXWUb.ToString().Trim() != "" )
						setValues += "chrXieBoXWUb='"+ Scheme_Spec .chrXieBoXWUb+"',";
								  	if (Scheme_Spec.chrXieBoXWUc!=null && Scheme_Spec.chrXieBoXWUc.ToString().Trim() != "" )
						setValues += "chrXieBoXWUc='"+ Scheme_Spec .chrXieBoXWUc+"',";
								  	if (Scheme_Spec.chrXieBoXWIa!=null && Scheme_Spec.chrXieBoXWIa.ToString().Trim() != "" )
						setValues += "chrXieBoXWIa='"+ Scheme_Spec .chrXieBoXWIa+"',";
								  	if (Scheme_Spec.chrXieBoXWIb!=null && Scheme_Spec.chrXieBoXWIb.ToString().Trim() != "" )
						setValues += "chrXieBoXWIb='"+ Scheme_Spec .chrXieBoXWIb+"',";
								  	if (Scheme_Spec.chrXieBoXWIc!=null && Scheme_Spec.chrXieBoXWIc.ToString().Trim() != "" )
						setValues += "chrXieBoXWIc='"+ Scheme_Spec .chrXieBoXWIc+"',";
								setValues = setValues.Substring(0, setValues.Length - 1);
				string _SqlText = "";
				 if(!string.IsNullOrEmpty(setValues))
					_SqlText = string.Format("update  {0}  set  {1}  where {2} ","Scheme_Spec",setValues, strWhere);
				return _SqlText;
		    }
			
			/// <summary>
		    /// Scheme_Spec  删除SQL语句
		    /// </summary>
			public string DeleteSQL(string strWhere)
			{
				string _SqlText = "";
				_SqlText = string.Format("delete  from  {0}  where  {1}","Scheme_Spec",strWhere);
				return _SqlText;
			}
	}
}


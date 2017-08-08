using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using CLDC_StartUpDispatcher.Model;
namespace CLDC_StartUpDispatcher.BLL {
	 	//DSPTCH_DIC_VIEW
		public partial class DSPTCH_DIC_VIEW
	{
   		     
		private readonly CLDC_StartUpDispatcher.DAL.DSPTCH_DIC_VIEW dal=new CLDC_StartUpDispatcher.DAL.DSPTCH_DIC_VIEW();
		public DSPTCH_DIC_VIEW()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string PK_VIEW_NO)
		{
			return dal.Exists(PK_VIEW_NO);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string PK_VIEW_NO)
		{
			
			return dal.Delete(PK_VIEW_NO);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW GetModel(string PK_VIEW_NO)
		{
			
			return dal.GetModel(PK_VIEW_NO);
		}

		

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetDataSet(string strWhere)
		{
			return dal.GetDataSet(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetDataSet(int Top,string strWhere,string filedOrder)
		{
			return dal.GetDataSet(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetDataSet(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW> DataTableToList(DataTable dt)
		{
			List<CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW> modelList = new List<CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CLDC_StartUpDispatcher.Model.DSPTCH_DIC_VIEW();					
																	model.PK_VIEW_NO= dt.Rows[n]["PK_VIEW_NO"].ToString();
																																model.AVR_CHECK_NAME= dt.Rows[n]["AVR_CHECK_NAME"].ToString();
																																model.AVR_TABLE_NAME= dt.Rows[n]["AVR_TABLE_NAME"].ToString();
																																model.AVR_COL_NAME= dt.Rows[n]["AVR_COL_NAME"].ToString();
																																model.AVR_COL_SHOW_NAME= dt.Rows[n]["AVR_COL_SHOW_NAME"].ToString();
																						
				
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetDataSet("");
		}
#endregion
   
	}
}
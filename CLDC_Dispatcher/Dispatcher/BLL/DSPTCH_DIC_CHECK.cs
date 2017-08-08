using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using CLDC_Dispatcher.Model;
namespace CLDC_Dispatcher.BLL {
	 	//DSPTCH_DIC_CHECK
		public partial class DSPTCH_DIC_CHECK
	{
   		     
		private readonly CLDC_Dispatcher.DAL.DSPTCH_DIC_CHECK dal=new CLDC_Dispatcher.DAL.DSPTCH_DIC_CHECK();
		public DSPTCH_DIC_CHECK()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string AVR_CHECK_NO)
		{
			return dal.Exists(AVR_CHECK_NO);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string AVR_CHECK_NO)
		{
			
			return dal.Delete(AVR_CHECK_NO);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK GetModel(string AVR_CHECK_NO)
		{
			
			return dal.GetModel(AVR_CHECK_NO);
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
		public List<CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetDataSet(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK> DataTableToList(DataTable dt)
		{
			List<CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK> modelList = new List<CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CLDC_Dispatcher.Model.DSPTCH_DIC_CHECK();					
																	model.AVR_CHECK_NO= dt.Rows[n]["AVR_CHECK_NO"].ToString();
																																model.AVR_CHECK_NAME= dt.Rows[n]["AVR_CHECK_NAME"].ToString();
																																model.AVR_CHECK_TYPE= dt.Rows[n]["AVR_CHECK_TYPE"].ToString();
																																model.AVR_NEED_TIME= dt.Rows[n]["AVR_NEED_TIME"].ToString();
																																model.FK_VIEW_NO= dt.Rows[n]["FK_VIEW_NO"].ToString();
																						
				
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
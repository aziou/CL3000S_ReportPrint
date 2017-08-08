using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using CLDC_Dispatcher.Model;
namespace CLDC_Dispatcher.BLL {
	 	//DSPTCH_CUR_SCHEME
		public partial class DSPTCH_CUR_SCHEME
	{
   		     
		private readonly CLDC_Dispatcher.DAL.DSPTCH_CUR_SCHEME dal=new CLDC_Dispatcher.DAL.DSPTCH_CUR_SCHEME();
		public DSPTCH_CUR_SCHEME()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string FK_DEVICE_MADE_NO)
		{
			return dal.Exists(FK_DEVICE_MADE_NO);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string FK_DEVICE_MADE_NO)
		{
			
			return dal.Delete(FK_DEVICE_MADE_NO);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME GetModel(string FK_DEVICE_MADE_NO)
		{
			
			return dal.GetModel(FK_DEVICE_MADE_NO);
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
		public List<CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetDataSet(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME> DataTableToList(DataTable dt)
		{
			List<CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME> modelList = new List<CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CLDC_Dispatcher.Model.DSPTCH_CUR_SCHEME();					
																	model.FK_DEVICE_MADE_NO= dt.Rows[n]["FK_DEVICE_MADE_NO"].ToString();
																																model.AVR_SCHEME_ID= dt.Rows[n]["AVR_SCHEME_ID"].ToString();
																						
				
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
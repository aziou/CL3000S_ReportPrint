using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using CLDC_Dispatcher.Model;
namespace CLDC_Dispatcher.BLL {
	 	//DSPTCH_EQUIP_INFO
		public partial class DSPTCH_EQUIP_INFO
	{
   		     
		private readonly CLDC_Dispatcher.DAL.DSPTCH_EQUIP_INFO dal=new CLDC_Dispatcher.DAL.DSPTCH_EQUIP_INFO();
		public DSPTCH_EQUIP_INFO()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string PK_DEVICE_MADE_NO)
		{
			return dal.Exists(PK_DEVICE_MADE_NO);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void  Add(CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO model)
		{
						dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string PK_DEVICE_MADE_NO)
		{
			
			return dal.Delete(PK_DEVICE_MADE_NO);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO GetModel(string PK_DEVICE_MADE_NO)
		{
			
			return dal.GetModel(PK_DEVICE_MADE_NO);
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
		public List<CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetDataSet(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO> DataTableToList(DataTable dt)
		{
			List<CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO> modelList = new List<CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CLDC_Dispatcher.Model.DSPTCH_EQUIP_INFO();					
																	model.PK_DEVICE_MADE_NO= dt.Rows[n]["PK_DEVICE_MADE_NO"].ToString();
																																model.AVR_DEVICE_ID= dt.Rows[n]["AVR_DEVICE_ID"].ToString();
																																model.AVR_EQUIP_NAME= dt.Rows[n]["AVR_EQUIP_NAME"].ToString();
																																model.AVR_EQUIP_MODEL= dt.Rows[n]["AVR_EQUIP_MODEL"].ToString();
																																model.AVR_EQUIP_PARA= dt.Rows[n]["AVR_EQUIP_PARA"].ToString();
																																model.AVR_EQUIP_TYPE= dt.Rows[n]["AVR_EQUIP_TYPE"].ToString();
																																model.AVR_FACTORY= dt.Rows[n]["AVR_FACTORY"].ToString();
																																model.AVR_EQUIP_CLASS= dt.Rows[n]["AVR_EQUIP_CLASS"].ToString();
																																model.AVR_EQUIP_STANDARD= dt.Rows[n]["AVR_EQUIP_STANDARD"].ToString();
																																model.LNG_BENCH_NUM= dt.Rows[n]["LNG_BENCH_NUM"].ToString();
																						
				
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
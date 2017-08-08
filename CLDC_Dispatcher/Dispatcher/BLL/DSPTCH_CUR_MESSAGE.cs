using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using CLDC_Dispatcher.Model;
namespace CLDC_Dispatcher.BLL {
	 	//DSPTCH_CUR_MESSAGE
		public partial class DSPTCH_CUR_MESSAGE
	{
   		     
		private readonly CLDC_Dispatcher.DAL.DSPTCH_CUR_MESSAGE dal=new CLDC_Dispatcher.DAL.DSPTCH_CUR_MESSAGE();
		public DSPTCH_CUR_MESSAGE()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE model)
		{
						return dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
				/// <summary>
		/// 批量删除一批数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE GetModel(int ID)
		{
			
			return dal.GetModel(ID);
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
		public List<CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetDataSet(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE> DataTableToList(DataTable dt)
		{
			List<CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE> modelList = new List<CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new CLDC_Dispatcher.Model.DSPTCH_CUR_MESSAGE();					
													if(dt.Rows[n]["ID"].ToString()!="")
				{
					model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
				}
																																				model.FK_DEVICE_MADE_NO= dt.Rows[n]["FK_DEVICE_MADE_NO"].ToString();
																																model.AVR_MSG_TYPE= dt.Rows[n]["AVR_MSG_TYPE"].ToString();
																																model.AVR_DATA= dt.Rows[n]["AVR_DATA"].ToString();
																																model.AVR_WRITE_TIME= dt.Rows[n]["AVR_WRITE_TIME"].ToString();
																																model.AVR_HANDLE_FLAG= dt.Rows[n]["AVR_HANDLE_FLAG"].ToString();
																						
				
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
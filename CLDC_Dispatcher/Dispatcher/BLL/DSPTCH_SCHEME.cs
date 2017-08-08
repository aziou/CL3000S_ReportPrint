using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
using CLDC_Dispatcher.Model;
namespace CLDC_Dispatcher.BLL {
	 	//DSPTCH_MESSAGE
		public partial class DSPTCH_SCHEME
	{
   		     
		private readonly CLDC_Dispatcher.DAL.DSPTCH_SCHEME dal=new CLDC_Dispatcher.DAL.DSPTCH_SCHEME();
        public DSPTCH_SCHEME()
		{}
		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string AVR_SCHEME_NAME, string AVR_CHECK_NO)
		{
            return dal.Exists(AVR_SCHEME_NAME, AVR_CHECK_NO);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(CLDC_Dispatcher.Model.DSPTCH_SCHEME model)
		{
					return	dal.Add(model);
						
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(CLDC_Dispatcher.Model.DSPTCH_SCHEME model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string AVR_SCHEME_ID)
		{
			
			return dal.Delete(AVR_SCHEME_ID);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public CLDC_Dispatcher.Model.DSPTCH_SCHEME GetModel(string AVR_SCHEME_ID)
		{
			
			return dal.GetModel(AVR_SCHEME_ID);
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
        public List<CLDC_Dispatcher.Model.DSPTCH_SCHEME> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetDataSet(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<CLDC_Dispatcher.Model.DSPTCH_SCHEME> DataTableToList(DataTable dt)
		{
            List<CLDC_Dispatcher.Model.DSPTCH_SCHEME> modelList = new List<CLDC_Dispatcher.Model.DSPTCH_SCHEME>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                CLDC_Dispatcher.Model.DSPTCH_SCHEME model;
				for (int n = 0; n < rowsCount; n++)
				{
                    model = new CLDC_Dispatcher.Model.DSPTCH_SCHEME();					
																	model.AVR_SCHEME_ID= dt.Rows[n]["AVR_SCHEME_ID"].ToString();
																																model.AVR_SCHEME_NAME= dt.Rows[n]["AVR_SCHEME_NAME"].ToString();
																																model.AVR_CHECK_NO= dt.Rows[n]["AVR_CHECK_NO"].ToString();
																						
				
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
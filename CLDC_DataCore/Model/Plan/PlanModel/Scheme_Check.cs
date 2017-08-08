using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_Check
		public class Scheme_Check
	{
      			private int _schemeid;
		/// <summary>
		/// 方案唯一编号
        /// </summary>		
        public int schemeID
        {
            get{ return _schemeid; }
            set{ _schemeid = value; }
        }        
				private string _chrplanname;
		/// <summary>
		/// 方案名称，可以重复，删除相同名称方案时根据方案状态控制
        /// </summary>		
        public string chrPlanName
        {
            get{ return _chrplanname; }
            set{ _chrplanname = value; }
        }        
				private string _chrequipcateg;
		/// <summary>
		/// 设备类别，01：电能表，09：采集终端???引用国家电网公司营销管理代码类集5110.64: 营销技术装备类别分类与代码
        /// </summary>		
        public string chrEquipCateg
        {
            get{ return _chrequipcateg; }
            set{ _chrequipcateg = value; }
        }        
				private string _chrwiringmode;
		/// <summary>
		/// 接线方式，1：单相，2：三相三线，3：三相四线
        /// </summary>		
        public string chrWiringMode
        {
            get{ return _chrwiringmode; }
            set{ _chrwiringmode = value; }
        }        
				private string _chrschemestatus;
		/// <summary>
		/// 方案状态，1：过时失效，但有外关联；默认0：正常状态
        /// </summary>		
        public string chrSchemeStatus
        {
            get{ return _chrschemestatus; }
            set{ _chrschemestatus = value; }
        }        
				private DateTime _startdate;
		/// <summary>
		/// 方案建立时间
        /// </summary>		
        public DateTime startDate
        {
            get{ return _startdate; }
            set{ _startdate = value; }
        }        
				private DateTime _disableddate;
		/// <summary>
		/// 弃用时间
        /// </summary>		
        public DateTime DisabledDate
        {
            get{ return _disableddate; }
            set{ _disableddate = value; }
        }        
			}
}
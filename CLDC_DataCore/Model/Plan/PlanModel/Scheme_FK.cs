using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_FK
		public class Scheme_FK
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
				private string _chrprojectname;
		/// <summary>
		/// 项目名称
        /// </summary>		
        public string chrProjectName
        {
            get{ return _chrprojectname; }
            set{ _chrprojectname = value; }
        }        
				private int _intlistno;
		/// <summary>
		/// 排序号
        /// </summary>		
        public int intListNo
        {
            get{ return _intlistno; }
            set{ _intlistno = value; }
        }        
				private string _chrgrptype;
		/// <summary>
		/// 项目组类型
        /// </summary>		
        public string chrGrpType
        {
            get{ return _chrgrptype; }
            set{ _chrgrptype = value; }
        }        
				private int _intitemtype;
		/// <summary>
		/// 子项目类型
        /// </summary>		
        public int intItemType
        {
            get{ return _intitemtype; }
            set{ _intitemtype = value; }
        }        
				private string _chrcparameter;
		/// <summary>
		/// 控制源输出参数(方向,元件,电压,电流,功率因素)
        /// </summary>		
        public string chrCParameter
        {
            get{ return _chrcparameter; }
            set{ _chrcparameter = value; }
        }        
				private string _chrtparameter;
		/// <summary>
		/// 功能检定参数
        /// </summary>		
        public string chrTParameter
        {
            get{ return _chrtparameter; }
            set{ _chrtparameter = value; }
        }        
				private string _chrchecked;
		/// <summary>
		/// 1：要检，0：不检
        /// </summary>		
        public string chrChecked
        {
            get{ return _chrchecked; }
            set{ _chrchecked = value; }
        }        
			}
}
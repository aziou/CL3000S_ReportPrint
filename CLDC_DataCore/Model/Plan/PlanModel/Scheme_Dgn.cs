using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_Dgn
		public class Scheme_Dgn
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
		/// 功能项目名称
        /// </summary>		
        public string chrProjectName
        {
            get{ return _chrprojectname; }
            set{ _chrprojectname = value; }
        }        
				private int _intlistno;
		/// <summary>
		/// 功能检定序号
        /// </summary>		
        public int intListNo
        {
            get{ return _intlistno; }
            set{ _intlistno = value; }
        }        
				private string _chrprojectno;
		/// <summary>
		/// 功能项目号,参见方案功能项目编号表
        /// </summary>		
        public string chrProjectNo
        {
            get{ return _chrprojectno; }
            set{ _chrprojectno = value; }
        }        
				private string _chrcparameter;
		/// <summary>
		/// 控制源输出参数,参数串格式：方向、元件，电压，电流，功率因数
        /// </summary>		
        public string chrCParameter
        {
            get{ return _chrcparameter; }
            set{ _chrcparameter = value; }
        }        
				private string _chrtparameter;
		/// <summary>
		/// 功能检定参数,参见参数值格式表
        /// </summary>		
        public string chrTParameter
        {
            get{ return _chrtparameter; }
            set{ _chrtparameter = value; }
        }        
				private string _chrchecked;
		/// <summary>
		/// 是否参加调试	1=调试；0=不调试
        /// </summary>		
        public string chrChecked
        {
            get{ return _chrchecked; }
            set{ _chrchecked = value; }
        }        
			}
}
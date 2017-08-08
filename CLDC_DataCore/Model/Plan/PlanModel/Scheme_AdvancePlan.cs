using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_AdvancePlan
		public class Scheme_AdvancePlan
	{
      			private string _chrprojectname;
		/// <summary>
		/// 调试项目名称
        /// </summary>		
        public string chrProjectName
        {
            get{ return _chrprojectname; }
            set{ _chrprojectname = value; }
        }        
				private int _intlistno;
		/// <summary>
		/// 调试检定序号
        /// </summary>		
        public int intListNo
        {
            get{ return _intlistno; }
            set{ _intlistno = value; }
        }        
				private string _chrprojectno;
		/// <summary>
		/// 调试项目号
        /// </summary>		
        public string chrProjectNo
        {
            get{ return _chrprojectno; }
            set{ _chrprojectno = value; }
        }        
				private string _chrparameter;
		/// <summary>
		/// 参数串。	参数串格式：方向、元件，电压，电流，功率因数，圈数
        /// </summary>		
        public string chrParameter
        {
            get{ return _chrparameter; }
            set{ _chrparameter = value; }
        }        
				private string _chrchecked;
		/// <summary>
		/// 是否参加调试，1=调试；0=不调试
        /// </summary>		
        public string chrChecked
        {
            get{ return _chrchecked; }
            set{ _chrchecked = value; }
        }        
			}
}
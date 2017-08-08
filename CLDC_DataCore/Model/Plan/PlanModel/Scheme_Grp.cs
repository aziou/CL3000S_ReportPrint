using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_Grp
		public class Scheme_Grp
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
				private string _chrgrptype;
		/// <summary>
		/// 组类型
        /// </summary>		
        public string chrGrpType
        {
            get{ return _chrgrptype; }
            set{ _chrgrptype = value; }
        }        
				private int _ilistno;
		/// <summary>
		/// 排序号
        /// </summary>		
        public int iListNo
        {
            get{ return _ilistno; }
            set{ _ilistno = value; }
        }        
				private string _chrgrpname;
		/// <summary>
		/// 组项目名
        /// </summary>		
        public string chrGrpName
        {
            get{ return _chrgrpname; }
            set{ _chrgrpname = value; }
        }        
				private string _chrcheck;
		/// <summary>
		/// 1：要检，0：不检
        /// </summary>		
        public string chrCheck
        {
            get{ return _chrcheck; }
            set{ _chrcheck = value; }
        }        
			}
}
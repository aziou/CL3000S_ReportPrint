using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_AutoGrp
		public class Scheme_AutoGrp
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
		/// 组类型，必填
        /// </summary>		
        public string chrGrpType
        {
            get{ return _chrgrptype; }
            set{ _chrgrptype = value; }
        }        
				private int _intitemtypeorprjno;
		/// <summary>
		/// 子项目类型或项目编号，无编号填-99
        /// </summary>		
        public int intItemTypeOrPrjNo
        {
            get{ return _intitemtypeorprjno; }
            set{ _intitemtypeorprjno = value; }
        }        
				private string _chrspecordataname;
		/// <summary>
		/// 特殊或协议一致性试验时填名称，其它有编号项目不填
        /// </summary>		
        public string chrSpecOrDataName
        {
            get{ return _chrspecordataname; }
            set{ _chrspecordataname = value; }
        }        
			}
}
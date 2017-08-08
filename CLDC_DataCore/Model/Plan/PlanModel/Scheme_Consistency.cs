using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_Consistency
		public class Scheme_Consistency
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
				private int _intitemno;
		/// <summary>
		/// 负载点编号，大于0：负载点参数；0：要检标志；-1：预热时间、自热时间；-2：间隔时间；-3：过载时间参数；
        /// </summary>		
        public int intItemNo
        {
            get{ return _intitemno; }
            set{ _intitemno = value; }
        }        
				private string _chrtparameter;
		/// <summary>
		/// 功能检定参数，多值以/分隔。格式：负载点参数(方向/元件/电压/电流/功率因数/圈数)；要检标志：Y要检，N不检；时间：min；过载时间参数：过载电流工作时间/功能检定参数，多值以/分隔。格式：负载点参数(方向/元件/电压/电流/功率因数/圈数)；要检标志：Y要检，N不检；时间：min；过载时间参数：过载电流工作时间/恢复正常运行时间,
        /// </summary>		
        public string chrTParameter
        {
            get{ return _chrtparameter; }
            set{ _chrtparameter = value; }
        }        
			}
}
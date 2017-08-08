using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_DLTData
		public class Scheme_DLTData
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
				private int _intitemid;
		/// <summary>
		/// 索引，不重复
        /// </summary>		
        public int intItemID
        {
            get{ return _intitemid; }
            set{ _intitemid = value; }
        }        
				private int _dltid;
		/// <summary>
		/// 数据标识ID。关联表ProDLT645Dict。等于 -1时，采用自定义标识
        /// </summary>		
        public int dltID
        {
            get{ return _dltid; }
            set{ _dltid = value; }
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
				private string _chritemname;
		/// <summary>
		/// 自定义项名称
        /// </summary>		
        public string chrItemName
        {
            get{ return _chritemname; }
            set{ _chritemname = value; }
        }        
				private string _chrid;
		/// <summary>
		/// 自定义数据标识
        /// </summary>		
        public string chrID
        {
            get{ return _chrid; }
            set{ _chrid = value; }
        }        
				private int _intlength;
		/// <summary>
		/// 自定义长度
        /// </summary>		
        public int intLength
        {
            get{ return _intlength; }
            set{ _intlength = value; }
        }        
				private int _intdot;
		/// <summary>
		/// 自定义小数位
        /// </summary>		
        public int intDot
        {
            get{ return _intdot; }
            set{ _intdot = value; }
        }        
				private string _chrformat;
		/// <summary>
		/// 自定义格式串
        /// </summary>		
        public string chrFormat
        {
            get{ return _chrformat; }
            set{ _chrformat = value; }
        }        
				private int _inttype;
		/// <summary>
		/// 操作方式：0=读，1=写
        /// </summary>		
        public int intType
        {
            get{ return _inttype; }
            set{ _inttype = value; }
        }        
				private string _chrvalue;
		/// <summary>
		/// 当写时采用此值
        /// </summary>		
        public string chrValue
        {
            get{ return _chrvalue; }
            set{ _chrvalue = value; }
        }        
			}
}
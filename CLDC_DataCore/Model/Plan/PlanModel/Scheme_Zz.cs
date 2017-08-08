using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_Zz
		public class Scheme_Zz
	{
      			private int _schemeid;
		/// <summary>
		/// 方案名称
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
				private string _chrjdfx;
		/// <summary>
		/// 检定方向
        /// </summary>		
        public string chrJdfx
        {
            get{ return _chrjdfx; }
            set{ _chrjdfx = value; }
        }        
				private string _chrfl;
		/// <summary>
		/// 费率
        /// </summary>		
        public string chrFl
        {
            get{ return _chrfl; }
            set{ _chrfl = value; }
        }        
				private string _chryj;
		/// <summary>
		/// 元件
        /// </summary>		
        public string chrYj
        {
            get{ return _chryj; }
            set{ _chryj = value; }
        }        
				private int _sngxub;
		/// <summary>
		/// 电压倍数
        /// </summary>		
        public int sngXUb
        {
            get{ return _sngxub; }
            set{ _sngxub = value; }
        }        
				private int _sngxib;
		/// <summary>
		/// 电流倍数
        /// </summary>		
        public int sngXIb
        {
            get{ return _sngxib; }
            set{ _sngxib = value; }
        }        
				private string _chrglys;
		/// <summary>
		/// 功率因数
        /// </summary>		
        public string chrGlys
        {
            get{ return _chrglys; }
            set{ _chrglys = value; }
        }        
				private string _chrstarttime;
		/// <summary>
		/// 走字起始时间
        /// </summary>		
        public string chrStartTime
        {
            get{ return _chrstarttime; }
            set{ _chrstarttime = value; }
        }        
				private string _chrneedtime;
		/// <summary>
		/// 走字需要的时间
        /// </summary>		
        public string chrNeedTime
        {
            get{ return _chrneedtime; }
            set{ _chrneedtime = value; }
        }        
				private string _chrzzlx;
		/// <summary>
		/// 走字方法：1走字实验法0标准表法
        /// </summary>		
        public string chrzzlx
        {
            get{ return _chrzzlx; }
            set{ _chrzzlx = value; }
        }        
			}
}
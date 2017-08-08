using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_Show
		public class Scheme_Show
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
				private string _intfaname;
		/// <summary>
		/// ?
        /// </summary>		
        public string intFaName
        {
            get{ return _intfaname; }
            set{ _intfaname = value; }
        }        
				private int _intlistno;
		/// <summary>
		/// 检定序号
        /// </summary>		
        public int intListNo
        {
            get{ return _intlistno; }
            set{ _intlistno = value; }
        }        
				private int _intshowtype;
		/// <summary>
		/// 显示类型 0：循环显示  1：按键显示  2：屏显
        /// </summary>		
        public int intShowType
        {
            get{ return _intshowtype; }
            set{ _intshowtype = value; }
        }        
				private int _inttype;
		/// <summary>
		/// 实验项目类型
        /// </summary>		
        public int intType
        {
            get{ return _inttype; }
            set{ _inttype = value; }
        }        
				private string _chrtype;
		/// <summary>
		/// 实验项目类型名称
        /// </summary>		
        public string chrType
        {
            get{ return _chrtype; }
            set{ _chrtype = value; }
        }        
				private int _intsubitem;
		/// <summary>
		/// 分项实验项目主键（序号）
        /// </summary>		
        public int intSubItem
        {
            get{ return _intsubitem; }
            set{ _intsubitem = value; }
        }        
				private string _chrsubitem;
		/// <summary>
		/// 分项实验项目名称
        /// </summary>		
        public string chrSubItem
        {
            get{ return _chrsubitem; }
            set{ _chrsubitem = value; }
        }        
				private string _chrid;
		/// <summary>
		/// 标识符
        /// </summary>		
        public string chrID
        {
            get{ return _chrid; }
            set{ _chrid = value; }
        }        
				private int _intclass;
		/// <summary>
		/// 表权限0 ， 1 ， 2 防止特殊命令使用特殊权限
        /// </summary>		
        public int intClass
        {
            get{ return _intclass; }
            set{ _intclass = value; }
        }        
				private int _intlength;
		/// <summary>
		/// 数据长度
        /// </summary>		
        public int intLength
        {
            get{ return _intlength; }
            set{ _intlength = value; }
        }        
				private int _intdot;
		/// <summary>
		/// 小数位
        /// </summary>		
        public int intDot
        {
            get{ return _intdot; }
            set{ _intdot = value; }
        }        
				private int _intreadwrite;
		/// <summary>
		/// 读写标志 0:读 1:写 2: 产生动作
        /// </summary>		
        public int intReadWrite
        {
            get{ return _intreadwrite; }
            set{ _intreadwrite = value; }
        }        
				private string _chrformat;
		/// <summary>
		/// 数据格式
        /// </summary>		
        public string chrFormat
        {
            get{ return _chrformat; }
            set{ _chrformat = value; }
        }        
				private string _chrcontent;
		/// <summary>
		/// 对比内容
        /// </summary>		
        public string chrContent
        {
            get{ return _chrcontent; }
            set{ _chrcontent = value; }
        }        
				private string _intcheck;
		/// <summary>
		/// 选择做此项目实验0:不做 1:做
        /// </summary>		
        public string intCheck
        {
            get{ return _intcheck; }
            set{ _intcheck = value; }
        }        
				private string _c_other1;
		/// <summary>
		/// 备用1
        /// </summary>		
        public string c_Other1
        {
            get{ return _c_other1; }
            set{ _c_other1 = value; }
        }        
				private string _c_other2;
		/// <summary>
		/// 备用2
        /// </summary>		
        public string c_Other2
        {
            get{ return _c_other2; }
            set{ _c_other2 = value; }
        }        
				private string _c_other3;
		/// <summary>
		/// 备用3
        /// </summary>		
        public string c_Other3
        {
            get{ return _c_other3; }
            set{ _c_other3 = value; }
        }        
				private string _c_other4;
		/// <summary>
		/// 备用4
        /// </summary>		
        public string c_Other4
        {
            get{ return _c_other4; }
            set{ _c_other4 = value; }
        }        
				private string _c_other5;
		/// <summary>
		/// 备用5
        /// </summary>		
        public string c_Other5
        {
            get{ return _c_other5; }
            set{ _c_other5 = value; }
        }        
				private string _c_other6;
		/// <summary>
		/// 备用6
        /// </summary>		
        public string c_Other6
        {
            get{ return _c_other6; }
            set{ _c_other6 = value; }
        }        
				private string _c_other7;
		/// <summary>
		/// 备用7
        /// </summary>		
        public string c_Other7
        {
            get{ return _c_other7; }
            set{ _c_other7 = value; }
        }        
				private string _c_other8;
		/// <summary>
		/// 备用8
        /// </summary>		
        public string c_Other8
        {
            get{ return _c_other8; }
            set{ _c_other8 = value; }
        }        
				private string _c_other9;
		/// <summary>
		/// 备用9
        /// </summary>		
        public string c_Other9
        {
            get{ return _c_other9; }
            set{ _c_other9 = value; }
        }        
				private string _c_other10;
		/// <summary>
		/// 备用10
        /// </summary>		
        public string c_Other10
        {
            get{ return _c_other10; }
            set{ _c_other10 = value; }
        }        
			}
}
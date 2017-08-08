using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_DynamicParameterList
		public class Scheme_DynamicParameterList
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
				private string _chrparaname;
		/// <summary>
		/// 参数名称
        /// </summary>		
        public string chrParaName
        {
            get{ return _chrparaname; }
            set{ _chrparaname = value; }
        }        
				private string _chrparadetail;
		/// <summary>
		/// 参数作用详细描述
        /// </summary>		
        public string chrParaDetail
        {
            get{ return _chrparadetail; }
            set{ _chrparadetail = value; }
        }        
				private string _chrgrptype;
		/// <summary>
		/// 所属组类型
        /// </summary>		
        public string chrGrpType
        {
            get{ return _chrgrptype; }
            set{ _chrgrptype = value; }
        }        
				private int _intitemtypeorprjno;
		/// <summary>
		/// 所属子项目类型或项目编号
        /// </summary>		
        public int intItemTypeOrPrjNo
        {
            get{ return _intitemtypeorprjno; }
            set{ _intitemtypeorprjno = value; }
        }        
				private string _chrcontroltype;
		/// <summary>
		/// 控件类型，0：文本框，1：多选按钮，2：列表框???
        /// </summary>		
        public string chrControlType
        {
            get{ return _chrcontroltype; }
            set{ _chrcontroltype = value; }
        }        
				private string _chrdefvalue;
		/// <summary>
		/// 预定义值；根据控件类型不同：1：选中，0:不选；多值格式：值1|值2|???|???，用“|”分割
        /// </summary>		
        public string chrDefValue
        {
            get{ return _chrdefvalue; }
            set{ _chrdefvalue = value; }
        }        
				private string _chrvisiable;
		/// <summary>
		/// 是否显示；T：显示，F：隐藏；隐藏不一定失效，通过chrStatus控制
        /// </summary>		
        public string chrVisiable
        {
            get{ return _chrvisiable; }
            set{ _chrvisiable = value; }
        }        
				private string _chrstatus;
		/// <summary>
		/// 是否有效；1：失效，0：正常；默认0
        /// </summary>		
        public string chrStatus
        {
            get{ return _chrstatus; }
            set{ _chrstatus = value; }
        }        
				private string _chrglobalpara;
		/// <summary>
		/// G：global所有方案通用参数，L：Local仅本方案
        /// </summary>		
        public string chrGlobalPara
        {
            get{ return _chrglobalpara; }
            set{ _chrglobalpara = value; }
        }        
				private DateTime _changedate;
		/// <summary>
		/// 修改时间
        /// </summary>		
        public DateTime ChangeDate
        {
            get{ return _changedate; }
            set{ _changedate = value; }
        }        
				private string _chreditor;
		/// <summary>
		/// 修改人
        /// </summary>		
        public string chrEditor
        {
            get{ return _chreditor; }
            set{ _chreditor = value; }
        }        
			}
}
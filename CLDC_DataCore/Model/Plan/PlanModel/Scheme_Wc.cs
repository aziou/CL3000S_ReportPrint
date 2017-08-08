using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_Wc
		public class Scheme_Wc
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
		/// 检定序号
        /// </summary>		
        public int intListNo
        {
            get{ return _intlistno; }
            set{ _intlistno = value; }
        }        
				private string _chrprojectno;
		/// <summary>
		/// 检定项目号(方向+项目编号) 参见误差点项目编号排列表，方向:0:正向有功1:正向无功2:反向有功3:反向有功。项目编号:001:预热002:起动试验003:潜动试验80%004:潜动试验100%005:潜动试验110%006:潜动试验115%007至099预号100至399误差点400至499偏差点500到699预留号700至999为特殊增加误点
        /// </summary>		
        public string chrProjectNo
        {
            get{ return _chrprojectno; }
            set{ _chrprojectno = value; }
        }        
				private string _chrxib;
		/// <summary>
		/// 电流倍数
        /// </summary>		
        public string chrXIb
        {
            get{ return _chrxib; }
            set{ _chrxib = value; }
        }        
				private string _chrparameter;
		/// <summary>
		/// 特殊点的参数，参数串格式：功率因数类型(0=功率因数，1=指定角度)，方向，元件，功率因数，电流
        /// </summary>		
        public string chrParameter
        {
            get{ return _chrparameter; }
            set{ _chrparameter = value; }
        }        
				private string _chrtime;
		/// <summary>
		/// 时间
        /// </summary>		
        public string chrTime
        {
            get{ return _chrtime; }
            set{ _chrtime = value; }
        }        
				private string _chrulimitbl;
		/// <summary>
		/// 上限比例
        /// </summary>		
        public string chrULimitBL
        {
            get{ return _chrulimitbl; }
            set{ _chrulimitbl = value; }
        }        
				private string _chrllimitbl;
		/// <summary>
		/// 下限比例
        /// </summary>		
        public string chrLLimitBL
        {
            get{ return _chrllimitbl; }
            set{ _chrllimitbl = value; }
        }        
				private string _chrqcount;
		/// <summary>
		/// 圈数
        /// </summary>		
        public string chrQCount
        {
            get{ return _chrqcount; }
            set{ _chrqcount = value; }
        }        
			}
}
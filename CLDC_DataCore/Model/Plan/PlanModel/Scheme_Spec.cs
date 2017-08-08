using System; 

namespace CLDC_DataCore.Model.Plan.PlanModel
{
	 	//Scheme_Spec
		public class Scheme_Spec
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
				private string _chrprojectname;
		/// <summary>
		/// 此项的名称
        /// </summary>		
        public string chrProjectName
        {
            get{ return _chrprojectname; }
            set{ _chrprojectname = value; }
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
				private string _chryj;
		/// <summary>
		/// 元件
        /// </summary>		
        public string chrYj
        {
            get{ return _chryj; }
            set{ _chryj = value; }
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
				private string _sngulimitbl;
		/// <summary>
		/// 上限比例
        /// </summary>		
        public string sngULimitBL
        {
            get{ return _sngulimitbl; }
            set{ _sngulimitbl = value; }
        }        
				private string _sngllimitbl;
		/// <summary>
		/// 下限比例
        /// </summary>		
        public string sngLLimitBL
        {
            get{ return _sngllimitbl; }
            set{ _sngllimitbl = value; }
        }        
				private string _sngqcount;
		/// <summary>
		/// 圈数
        /// </summary>		
        public string sngQCount
        {
            get{ return _sngqcount; }
            set{ _sngqcount = value; }
        }        
				private string _chrxiebo;
		/// <summary>
		/// 加谐波1=加，0=不加
        /// </summary>		
        public string chrXieBo
        {
            get{ return _chrxiebo; }
            set{ _chrxiebo = value; }
        }        
				private string _chrnxx;
		/// <summary>
		/// 逆相序 1=逆，0=正
        /// </summary>		
        public string chrNXX
        {
            get{ return _chrnxx; }
            set{ _chrnxx = value; }
        }        
				private int _sngxhz;
		/// <summary>
		/// 频率倍数
        /// </summary>		
        public int sngXHz
        {
            get{ return _sngxhz; }
            set{ _sngxhz = value; }
        }        
				private int _sngxua;
		/// <summary>
		/// A电压倍数
        /// </summary>		
        public int sngXUa
        {
            get{ return _sngxua; }
            set{ _sngxua = value; }
        }        
				private int _sngxub;
		/// <summary>
		/// B电压倍数
        /// </summary>		
        public int sngXUb
        {
            get{ return _sngxub; }
            set{ _sngxub = value; }
        }        
				private int _sngxuc;
		/// <summary>
		/// C电压倍数
        /// </summary>		
        public int sngXUc
        {
            get{ return _sngxuc; }
            set{ _sngxuc = value; }
        }        
				private double _sngxia;
		/// <summary>
		/// A电流倍数
        /// </summary>		
        public double sngXIa
        {
            get{ return _sngxia; }
            set{ _sngxia = value; }
        }        
				private double _sngxib;
		/// <summary>
		/// B电流倍数
        /// </summary>		
        public double sngXIb
        {
            get{ return _sngxib; }
            set{ _sngxib = value; }
        }        
				private double _sngxic;
		/// <summary>
		/// C电流倍数
        /// </summary>		
        public double sngXIc
        {
            get{ return _sngxic; }
            set{ _sngxic = value; }
        }        
				private int _sngphi_ua;
		/// <summary>
		/// A相电压角度
        /// </summary>		
        public int sngPhi_Ua
        {
            get{ return _sngphi_ua; }
            set{ _sngphi_ua = value; }
        }        
				private int _sngphi_ub;
		/// <summary>
		/// B相电压角度
        /// </summary>		
        public int sngPhi_Ub
        {
            get{ return _sngphi_ub; }
            set{ _sngphi_ub = value; }
        }        
				private int _sngphi_uc;
		/// <summary>
		/// C相电压角度
        /// </summary>		
        public int sngPhi_Uc
        {
            get{ return _sngphi_uc; }
            set{ _sngphi_uc = value; }
        }        
				private int _sngphi_ia;
		/// <summary>
		/// A相电流角度
        /// </summary>		
        public int sngPhi_Ia
        {
            get{ return _sngphi_ia; }
            set{ _sngphi_ia = value; }
        }        
				private int _sngphi_ib;
		/// <summary>
		/// B相电流角度
        /// </summary>		
        public int sngPhi_Ib
        {
            get{ return _sngphi_ib; }
            set{ _sngphi_ib = value; }
        }        
				private int _sngphi_ic;
		/// <summary>
		/// C相电流角度
        /// </summary>		
        public int sngPhi_Ic
        {
            get{ return _sngphi_ic; }
            set{ _sngphi_ic = value; }
        }        
				private string _chrxiebochkua;
		/// <summary>
		/// A相电压谐波次数
        /// </summary>		
        public string chrXieBoChkUa
        {
            get{ return _chrxiebochkua; }
            set{ _chrxiebochkua = value; }
        }        
				private string _chrxiebochkub;
		/// <summary>
		/// B相电压谐波次数
        /// </summary>		
        public string chrXieBoChkUb
        {
            get{ return _chrxiebochkub; }
            set{ _chrxiebochkub = value; }
        }        
				private string _chrxiebochkuc;
		/// <summary>
		/// C相电压谐波次数
        /// </summary>		
        public string chrXieBoChkUc
        {
            get{ return _chrxiebochkuc; }
            set{ _chrxiebochkuc = value; }
        }        
				private string _chrxiebochkia;
		/// <summary>
		/// A相电流谐波次数
        /// </summary>		
        public string chrXieBoChkIa
        {
            get{ return _chrxiebochkia; }
            set{ _chrxiebochkia = value; }
        }        
				private string _chrxiebochkib;
		/// <summary>
		/// B相电流谐波次数
        /// </summary>		
        public string chrXieBoChkIb
        {
            get{ return _chrxiebochkib; }
            set{ _chrxiebochkib = value; }
        }        
				private string _chrxiebochkic;
		/// <summary>
		/// C相电流谐波次数
        /// </summary>		
        public string chrXieBoChkIc
        {
            get{ return _chrxiebochkic; }
            set{ _chrxiebochkic = value; }
        }        
				private string _chrxiebofdua;
		/// <summary>
		/// A相电压谐波幅度
        /// </summary>		
        public string chrXieBoFDUa
        {
            get{ return _chrxiebofdua; }
            set{ _chrxiebofdua = value; }
        }        
				private string _chrxiebofdub;
		/// <summary>
		/// B相电压谐波幅度
        /// </summary>		
        public string chrXieBoFDUb
        {
            get{ return _chrxiebofdub; }
            set{ _chrxiebofdub = value; }
        }        
				private string _chrxiebofduc;
		/// <summary>
		/// C相电压谐波幅度
        /// </summary>		
        public string chrXieBoFDUc
        {
            get{ return _chrxiebofduc; }
            set{ _chrxiebofduc = value; }
        }        
				private string _chrxiebofdia;
		/// <summary>
		/// A相电流谐波幅度
        /// </summary>		
        public string chrXieBoFDIa
        {
            get{ return _chrxiebofdia; }
            set{ _chrxiebofdia = value; }
        }        
				private string _chrxiebofdib;
		/// <summary>
		/// B相电流谐波幅度
        /// </summary>		
        public string chrXieBoFDIb
        {
            get{ return _chrxiebofdib; }
            set{ _chrxiebofdib = value; }
        }        
				private string _chrxiebofdic;
		/// <summary>
		/// C相电流谐波幅度
        /// </summary>		
        public string chrXieBoFDIc
        {
            get{ return _chrxiebofdic; }
            set{ _chrxiebofdic = value; }
        }        
				private string _chrxieboxwua;
		/// <summary>
		/// A相电压谐波相位
        /// </summary>		
        public string chrXieBoXWUa
        {
            get{ return _chrxieboxwua; }
            set{ _chrxieboxwua = value; }
        }        
				private string _chrxieboxwub;
		/// <summary>
		/// B相电压谐波相位
        /// </summary>		
        public string chrXieBoXWUb
        {
            get{ return _chrxieboxwub; }
            set{ _chrxieboxwub = value; }
        }        
				private string _chrxieboxwuc;
		/// <summary>
		/// C相电压谐波相位
        /// </summary>		
        public string chrXieBoXWUc
        {
            get{ return _chrxieboxwuc; }
            set{ _chrxieboxwuc = value; }
        }        
				private string _chrxieboxwia;
		/// <summary>
		/// A相电流谐波相位
        /// </summary>		
        public string chrXieBoXWIa
        {
            get{ return _chrxieboxwia; }
            set{ _chrxieboxwia = value; }
        }        
				private string _chrxieboxwib;
		/// <summary>
		/// B相电流谐波相位
        /// </summary>		
        public string chrXieBoXWIb
        {
            get{ return _chrxieboxwib; }
            set{ _chrxieboxwib = value; }
        }        
				private string _chrxieboxwic;
		/// <summary>
		/// C相电流谐波相位
        /// </summary>		
        public string chrXieBoXWIc
        {
            get{ return _chrxieboxwic; }
            set{ _chrxieboxwic = value; }
        }        
			}
}
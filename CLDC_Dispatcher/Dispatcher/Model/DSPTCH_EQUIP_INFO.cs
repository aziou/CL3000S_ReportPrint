using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace CLDC_Dispatcher.Model{
	 	//DSPTCH_EQUIP_INFO
		public class DSPTCH_EQUIP_INFO
	{
   		     
      	private string _pk_device_made_no;
		/// <summary>
		/// PK_DEVICE_MADE_NO
        /// </summary>		
        public string PK_DEVICE_MADE_NO
        {
            get{ return _pk_device_made_no; }
            set{ _pk_device_made_no = value; }
        }        
		private string _avr_device_id;
		/// <summary>
		/// AVR_DEVICE_ID
        /// </summary>		
        public string AVR_DEVICE_ID
        {
            get{ return _avr_device_id; }
            set{ _avr_device_id = value; }
        }        
		private string _avr_equip_name;
		/// <summary>
		/// AVR_EQUIP_NAME
        /// </summary>		
        public string AVR_EQUIP_NAME
        {
            get{ return _avr_equip_name; }
            set{ _avr_equip_name = value; }
        }        
		private string _avr_equip_model;
		/// <summary>
		/// AVR_EQUIP_MODEL
        /// </summary>		
        public string AVR_EQUIP_MODEL
        {
            get{ return _avr_equip_model; }
            set{ _avr_equip_model = value; }
        }        
		private string _avr_equip_para;
		/// <summary>
		/// AVR_EQUIP_PARA
        /// </summary>		
        public string AVR_EQUIP_PARA
        {
            get{ return _avr_equip_para; }
            set{ _avr_equip_para = value; }
        }        
		private string _avr_equip_type;
		/// <summary>
		/// AVR_EQUIP_TYPE
        /// </summary>		
        public string AVR_EQUIP_TYPE
        {
            get{ return _avr_equip_type; }
            set{ _avr_equip_type = value; }
        }        
		private string _avr_factory;
		/// <summary>
		/// AVR_FACTORY
        /// </summary>		
        public string AVR_FACTORY
        {
            get{ return _avr_factory; }
            set{ _avr_factory = value; }
        }        
		private string _avr_equip_class;
		/// <summary>
		/// AVR_EQUIP_CLASS
        /// </summary>		
        public string AVR_EQUIP_CLASS
        {
            get{ return _avr_equip_class; }
            set{ _avr_equip_class = value; }
        }        
		private string _avr_equip_standard;
		/// <summary>
		/// AVR_EQUIP_STANDARD
        /// </summary>		
        public string AVR_EQUIP_STANDARD
        {
            get{ return _avr_equip_standard; }
            set{ _avr_equip_standard = value; }
        }        
		private string _lng_bench_num;
		/// <summary>
		/// LNG_BENCH_NUM
        /// </summary>		
        public string LNG_BENCH_NUM
        {
            get{ return _lng_bench_num; }
            set{ _lng_bench_num = value; }
        }        
		   
	}
}
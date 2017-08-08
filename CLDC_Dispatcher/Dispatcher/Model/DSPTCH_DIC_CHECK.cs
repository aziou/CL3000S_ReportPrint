using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace CLDC_Dispatcher.Model{
	 	//DSPTCH_DIC_CHECK
		public class DSPTCH_DIC_CHECK
	{
   		     
      	private string _avr_check_no;
		/// <summary>
		/// AVR_CHECK_NO
        /// </summary>		
        public string AVR_CHECK_NO
        {
            get{ return _avr_check_no; }
            set{ _avr_check_no = value; }
        }        
		private string _avr_check_name;
		/// <summary>
		/// AVR_CHECK_NAME
        /// </summary>		
        public string AVR_CHECK_NAME
        {
            get{ return _avr_check_name; }
            set{ _avr_check_name = value; }
        }        
		private string _avr_check_type;
		/// <summary>
		/// AVR_CHECK_TYPE
        /// </summary>		
        public string AVR_CHECK_TYPE
        {
            get{ return _avr_check_type; }
            set{ _avr_check_type = value; }
        }        
		private string _avr_need_time;
		/// <summary>
		/// AVR_NEED_TIME
        /// </summary>		
        public string AVR_NEED_TIME
        {
            get{ return _avr_need_time; }
            set{ _avr_need_time = value; }
        }        
		private string _fk_view_no;
		/// <summary>
		/// FK_VIEW_NO
        /// </summary>		
        public string FK_VIEW_NO
        {
            get{ return _fk_view_no; }
            set{ _fk_view_no = value; }
        }        
		   
	}
}
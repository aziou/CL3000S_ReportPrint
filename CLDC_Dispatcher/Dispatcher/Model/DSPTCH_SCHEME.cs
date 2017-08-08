using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace CLDC_Dispatcher.Model{
	 	//DSPTCH_MESSAGE
    public class DSPTCH_SCHEME
	{
   		     
      	private string _avr_scheme_id;
		/// <summary>
		/// AVR_SCHEME_ID
        /// </summary>		
        public string AVR_SCHEME_ID
        {
            get{ return _avr_scheme_id; }
            set{ _avr_scheme_id = value; }
        }        
		private string _avr_scheme_name;
		/// <summary>
		/// AVR_SCHEME_NAME
        /// </summary>		
        public string AVR_SCHEME_NAME
        {
            get{ return _avr_scheme_name; }
            set{ _avr_scheme_name = value; }
        }        
		private string _avr_check_no;
		/// <summary>
		/// AVR_CHECK_NO
        /// </summary>		
        public string AVR_CHECK_NO
        {
            get{ return _avr_check_no; }
            set{ _avr_check_no = value; }
        }
        private string _avr_dspt_scheme_id;
		/// <summary>
        /// AVR_DSPT_SCHEME_ID
        /// </summary>		
        public string AVR_DSPT_SCHEME_ID
        {
            get { return _avr_dspt_scheme_id; }
            set { _avr_dspt_scheme_id = value; }
        }
        
		   
	}
}
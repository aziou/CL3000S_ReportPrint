using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace CLDC_Dispatcher.Model{
	 	//DSPTCH_CUR_SCHEME
		public class DSPTCH_CUR_SCHEME
	{
   		     
      	private string _fk_device_made_no;
		/// <summary>
		/// FK_DEVICE_MADE_NO
        /// </summary>		
        public string FK_DEVICE_MADE_NO
        {
            get{ return _fk_device_made_no; }
            set{ _fk_device_made_no = value; }
        }        
		private string _avr_scheme_id;
		/// <summary>
		/// AVR_SCHEME_ID
        /// </summary>		
        public string AVR_SCHEME_ID
        {
            get{ return _avr_scheme_id; }
            set{ _avr_scheme_id = value; }
        }        
		   
	}
}
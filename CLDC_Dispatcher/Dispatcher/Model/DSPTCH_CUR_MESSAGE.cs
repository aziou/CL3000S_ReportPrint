using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace CLDC_Dispatcher.Model{
	 	//DSPTCH_CUR_MESSAGE
		public class DSPTCH_CUR_MESSAGE
	{
   		     
      	private int _id;
		/// <summary>
		/// ID
        /// </summary>		
        public int ID
        {
            get{ return _id; }
            set{ _id = value; }
        }        
		private string _fk_device_made_no;
		/// <summary>
		/// FK_DEVICE_MADE_NO
        /// </summary>		
        public string FK_DEVICE_MADE_NO
        {
            get{ return _fk_device_made_no; }
            set{ _fk_device_made_no = value; }
        }        
		private string _avr_msg_type;
		/// <summary>
		/// AVR_MSG_TYPE
        /// </summary>		
        public string AVR_MSG_TYPE
        {
            get{ return _avr_msg_type; }
            set{ _avr_msg_type = value; }
        }        
		private string _avr_data;
		/// <summary>
		/// AVR_DATA
        /// </summary>		
        public string AVR_DATA
        {
            get{ return _avr_data; }
            set{ _avr_data = value; }
        }        
		private string _avr_write_time;
		/// <summary>
		/// AVR_WRITE_TIME
        /// </summary>		
        public string AVR_WRITE_TIME
        {
            get{ return _avr_write_time; }
            set{ _avr_write_time = value; }
        }        
		private string _avr_handle_flag;
		/// <summary>
		/// AVR_HANDLE_FLAG
        /// </summary>		
        public string AVR_HANDLE_FLAG
        {
            get{ return _avr_handle_flag; }
            set{ _avr_handle_flag = value; }
        }        
		   
	}
}
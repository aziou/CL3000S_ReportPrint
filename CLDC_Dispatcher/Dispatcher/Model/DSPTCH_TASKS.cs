using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace CLDC_Dispatcher.Model{
	 	//DSPTCH_TASKS
		public class DSPTCH_TASKS
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
		private string _task_no;
		/// <summary>
		/// TASK_NO
        /// </summary>		
        public string TASK_NO
        {
            get{ return _task_no; }
            set{ _task_no = value; }
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
		private string _avr_end_flag;
		/// <summary>
		/// AVR_END_FLAG
        /// </summary>		
        public string AVR_END_FLAG
        {
            get{ return _avr_end_flag; }
            set{ _avr_end_flag = value; }
        }        
		private string _avr_start_time;
		/// <summary>
		/// AVR_START_TIME
        /// </summary>		
        public string AVR_START_TIME
        {
            get{ return _avr_start_time; }
            set{ _avr_start_time = value; }
        }        
		private string _avr_end_time;
		/// <summary>
		/// AVR_END_TIME
        /// </summary>		
        public string AVR_END_TIME
        {
            get{ return _avr_end_time; }
            set{ _avr_end_time = value; }
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
		private string _avr_scheme_id;
		/// <summary>
		/// AVR_SCHEME_ID
        /// </summary>		
        public string AVR_SCHEME_ID
        {
            get{ return _avr_scheme_id; }
            set{ _avr_scheme_id = value; }
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
		   
	}
}
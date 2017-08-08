using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace CLDC_Dispatcher.Model{
	 	//DSPTCH_LOG
		public class DSPTCH_LOG
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
		private string _avr_type;
		/// <summary>
		/// AVR_TYPE
        /// </summary>		
        public string AVR_TYPE
        {
            get{ return _avr_type; }
            set{ _avr_type = value; }
        }        
		private string _avr_log;
		/// <summary>
		/// AVR_LOG
        /// </summary>		
        public string AVR_LOG
        {
            get{ return _avr_log; }
            set{ _avr_log = value; }
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
        private string _avr_source;
        /// <summary>
        /// AVR_SOURCE
        /// </summary>	
		public string AVR_SOURCE
        {
            get { return _avr_source; }
            set { _avr_source = value; }
        }        
	}
}
using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace CLDC_StartUpDispatcher.Model{
	 	//DSPTCH_DIC_VIEW
		public class DSPTCH_DIC_VIEW
	{
   		     
      	private string _pk_view_no;
		/// <summary>
		/// PK_VIEW_NO
        /// </summary>		
        public string PK_VIEW_NO
        {
            get{ return _pk_view_no; }
            set{ _pk_view_no = value; }
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
		private string _avr_table_name;
		/// <summary>
		/// AVR_TABLE_NAME
        /// </summary>		
        public string AVR_TABLE_NAME
        {
            get{ return _avr_table_name; }
            set{ _avr_table_name = value; }
        }        
		private string _avr_col_name;
		/// <summary>
		/// AVR_COL_NAME
        /// </summary>		
        public string AVR_COL_NAME
        {
            get{ return _avr_col_name; }
            set{ _avr_col_name = value; }
        }        
		private string _avr_col_show_name;
		/// <summary>
		/// AVR_COL_SHOW_NAME
        /// </summary>		
        public string AVR_COL_SHOW_NAME
        {
            get{ return _avr_col_show_name; }
            set{ _avr_col_show_name = value; }
        }        
		   
	}
}
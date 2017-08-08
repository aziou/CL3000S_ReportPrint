using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Threading;


namespace CLDC_MeterUI.UI_Detection_New
{
    public partial class Frm_PROTOCOL : Office2007Form
    {
        private static Frm_PROTOCOL instance;
        private static object syncRoot = new Object();

        public static Frm_PROTOCOL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Frm_PROTOCOL();
                    }
                }
                return instance;
            }
        }

        
        public void ShowD()
        {
            if (instance != null)
            {
                if (instance.Visible != true)
                {
                    TV_Protocol.Load_TV();
                    instance.ShowDialog();
                }

            }
        }

        public Frm_PROTOCOL()
        {
            InitializeComponent();
            
        }
    }
}

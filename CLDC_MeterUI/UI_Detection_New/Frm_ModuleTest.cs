using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace CLDC_MeterUI.UI_Detection_New
{
    public partial class Frm_ModuleTest : Office2007Form
    {
        #region
        private static Frm_ModuleTest instance;
        private static object syncRoot = new Object();

        public static Frm_ModuleTest Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Frm_ModuleTest();
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
                    instance.ShowDialog();
                }

            }
        }
        #endregion

        public Frm_ModuleTest()
        {
            InitializeComponent();
        }
    }
}

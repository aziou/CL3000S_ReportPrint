using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI.UI_FA.FAPrj
{
    public partial class UI_WGJC : UI_TableBase
    {
        public UI_WGJC()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 拷贝方案
        /// </summary>
        public CLDC_DataCore.Model.Plan.Plan_WGJC Copy()
        {
            CLDC_DataCore.Model.Plan.Plan_WGJC _Obj = new CLDC_DataCore.Model.Plan.Plan_WGJC((int)TaiType, "");
            _Obj.Add(0);
            
            return _Obj;
        }
    }
}

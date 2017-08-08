using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace UI.DisplayInfo
{
    /// <summary>
    /// 传递一个Comm.Model.DnbModel.DnbInfo.MeterBasicInfo对象进来、显示里面所有的检定数据
    /// </summary>
    public partial class MeterInfo : Base 
    {
        public MeterInfo()
        {
            InitializeComponent();
        }

        public MeterInfo(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
            : base(meterInfo, allowedit)
        {
            InitializeComponent();
            if (Comm.Function.Common.IsVSDevenv()) return;
            this.Load += new EventHandler(MeterInfo_Load);
        }

        void MeterInfo_Load(object sender, EventArgs e)
        {
            SetData(MeterInfo, AllowEdit);
        }

        #region SetData(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        public override void SetData(Comm.Model.DnbModel.DnbInfo.MeterBasicInfo meterInfo, bool allowedit)
        {
            base.SetData(meterInfo, allowedit);
            TabMain.TabPages.Clear();

            if (MeterInfo.MeterResults.ContainsKey(((int)Comm.Enum.Cus_MeterResultPriID.启动试验).ToString())
                    || MeterInfo.MeterResults.ContainsKey(((int)Comm.Enum.Cus_MeterResultPriID.潜动试验).ToString()))
            {
                TabMain.TabPages.Add("启动、潜动");
                Comm.Function.Common.DoCover(TabMain.TabPages["启动、潜动"], true);
                CheckQiQianDong WC_Normal = null;
                ThreadPool.QueueUserWorkItem(new WaitCallback(thLoadChild), new object[] { TabMain.TabPages["启动、潜动"], 5, WC_Normal });
            }

            if (MeterInfo.MeterErrors.Count > 0)
            {
                TabMain.TabPages.Add("基本误差");
                Comm.Function.Common.DoCover(TabMain.TabPages["基本误差"], true);
                CheckWC_Normal WC_Normal = null ;
                ThreadPool.QueueUserWorkItem(new WaitCallback(thLoadChild), new object[] { TabMain.TabPages["基本误差"], 1, WC_Normal });

                TabMain.TabPages.Add("标准偏差");
                Comm.Function.Common.DoCover(TabMain.TabPages["标准偏差"], true);
                CheckWC_PianCha WC_Normal2 = null;
                ThreadPool.QueueUserWorkItem(new WaitCallback(thLoadChild), new object[] { TabMain.TabPages["标准偏差"], 2, WC_Normal2 });
            }
            if (MeterInfo.MeterZZErrors.Count > 0)
            {
                TabMain.TabPages.Add("走字误差");
                Comm.Function.Common.DoCover(TabMain.TabPages["走字误差"], true);
                CheckZZ WC_Normal = null;
                ThreadPool.QueueUserWorkItem(new WaitCallback(thLoadChild), new object[] { TabMain.TabPages["走字误差"], 3, WC_Normal });
            }
            if (MeterInfo.MeterDgns.Count > 0)
            {
                TabMain.TabPages.Add("多功能");
                Comm.Function.Common.DoCover(TabMain.TabPages["多功能"], true);
                CheckDgn WC_Normal = null;
                ThreadPool.QueueUserWorkItem(new WaitCallback(thLoadChild), new object[] { TabMain.TabPages["多功能"], 4, WC_Normal });
            }
            
        }

        private void thLoadChild(object obj)
        {
            TabPage Tab = (TabPage)((object[])obj)[0];
            int IType   = (int)((object[])obj)[1];
            UserControl objChild = (UserControl)((object[])obj)[2];
            switch (IType)
            {
                case 1: //基本误差
                    objChild = new CheckWC_Normal(MeterInfo, AllowEdit);
                    break;

                case 2: //标准偏差
                    objChild = new CheckWC_PianCha(MeterInfo, AllowEdit);
                    break;

                case 3: //走字误差
                    objChild = new CheckZZ(MeterInfo, AllowEdit);
                    break;

                case 4: //多功能
                    objChild = new CheckDgn(MeterInfo, AllowEdit);
                    break;

                case 5://启动、潜动
                    objChild = new CheckQiQianDong(MeterInfo, AllowEdit);
                    break;

                case 6: //特殊检定
                    break;

                default:
                    break;
            }
            this.BeginInvoke(new EventAddToTabPage(AddToTabPage),Tab ,objChild );
        }

        private delegate void EventAddToTabPage(TabPage Tab, UserControl objChild);
        private void AddToTabPage(TabPage  Tab, UserControl objChild)
        {
            Tab.Controls.Clear();
            Tab.Controls.Add(objChild);
            objChild.Margin = new Padding(1);
            objChild.Dock = DockStyle.Fill;
            Comm.Function.Common.DoCover(Tab, false);
        }
        #endregion


    }
}

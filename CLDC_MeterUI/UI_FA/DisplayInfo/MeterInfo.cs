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
    /// ����һ��Comm.Model.DnbModel.DnbInfo.MeterBasicInfo�����������ʾ�������еļ춨����
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

            if (MeterInfo.MeterResults.ContainsKey(((int)Comm.Enum.Cus_MeterResultPriID.��������).ToString())
                    || MeterInfo.MeterResults.ContainsKey(((int)Comm.Enum.Cus_MeterResultPriID.Ǳ������).ToString()))
            {
                TabMain.TabPages.Add("������Ǳ��");
                Comm.Function.Common.DoCover(TabMain.TabPages["������Ǳ��"], true);
                CheckQiQianDong WC_Normal = null;
                ThreadPool.QueueUserWorkItem(new WaitCallback(thLoadChild), new object[] { TabMain.TabPages["������Ǳ��"], 5, WC_Normal });
            }

            if (MeterInfo.MeterErrors.Count > 0)
            {
                TabMain.TabPages.Add("�������");
                Comm.Function.Common.DoCover(TabMain.TabPages["�������"], true);
                CheckWC_Normal WC_Normal = null ;
                ThreadPool.QueueUserWorkItem(new WaitCallback(thLoadChild), new object[] { TabMain.TabPages["�������"], 1, WC_Normal });

                TabMain.TabPages.Add("��׼ƫ��");
                Comm.Function.Common.DoCover(TabMain.TabPages["��׼ƫ��"], true);
                CheckWC_PianCha WC_Normal2 = null;
                ThreadPool.QueueUserWorkItem(new WaitCallback(thLoadChild), new object[] { TabMain.TabPages["��׼ƫ��"], 2, WC_Normal2 });
            }
            if (MeterInfo.MeterZZErrors.Count > 0)
            {
                TabMain.TabPages.Add("�������");
                Comm.Function.Common.DoCover(TabMain.TabPages["�������"], true);
                CheckZZ WC_Normal = null;
                ThreadPool.QueueUserWorkItem(new WaitCallback(thLoadChild), new object[] { TabMain.TabPages["�������"], 3, WC_Normal });
            }
            if (MeterInfo.MeterDgns.Count > 0)
            {
                TabMain.TabPages.Add("�๦��");
                Comm.Function.Common.DoCover(TabMain.TabPages["�๦��"], true);
                CheckDgn WC_Normal = null;
                ThreadPool.QueueUserWorkItem(new WaitCallback(thLoadChild), new object[] { TabMain.TabPages["�๦��"], 4, WC_Normal });
            }
            
        }

        private void thLoadChild(object obj)
        {
            TabPage Tab = (TabPage)((object[])obj)[0];
            int IType   = (int)((object[])obj)[1];
            UserControl objChild = (UserControl)((object[])obj)[2];
            switch (IType)
            {
                case 1: //�������
                    objChild = new CheckWC_Normal(MeterInfo, AllowEdit);
                    break;

                case 2: //��׼ƫ��
                    objChild = new CheckWC_PianCha(MeterInfo, AllowEdit);
                    break;

                case 3: //�������
                    objChild = new CheckZZ(MeterInfo, AllowEdit);
                    break;

                case 4: //�๦��
                    objChild = new CheckDgn(MeterInfo, AllowEdit);
                    break;

                case 5://������Ǳ��
                    objChild = new CheckQiQianDong(MeterInfo, AllowEdit);
                    break;

                case 6: //����춨
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CLDC_MeterUI
{
    public partial class UI_AutoPowerControl : Form
    {
        private CLDC_Comm.Enum.Cus_Clfs clfsTmp;
        private static UI_AutoPowerControl instance;
        private static object syncRoot = new Object();
        public static UI_AutoPowerControl Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new UI_AutoPowerControl();
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

        public UI_AutoPowerControl()
        {
            InitializeComponent();
            this.TopMost = true;
            InitDicPram();
            RegeditEvent();

            dbi_IA.Value = 0;
            dbi_IB.Value = 0;
            dbi_IC.Value = 0;
            dbi_CUA.Value = 0;
            dbi_CIA.Value = 0;

            clfsTmp = CLDC_DataCore.Const.GlobalUnit.Clfs;
        }

        float LcaxI = 1;//电流基数，根据In选择
        float LcaU = 220;//电压基数，根据Un选择
        float LcaHz = 50;//频率基数，不变
        List<CLDC_DataCore.Struct.StXieBo> XieBoItem;
        static bool isXieBo = false;
        static string XieBoFaName = "";
        private void RegeditEvent()
        {
            #region Click -=
            pic_xI_Add1.Click -= new EventHandler(Set_Click);
            pic_xI_Add10.Click -= new EventHandler(Set_Click);
            pic_xI_Plus1.Click -= new EventHandler(Set_Click);
            pic_xI_Plus10.Click -= new EventHandler(Set_Click);
            pic_xU_Add5.Click -= new EventHandler(Set_Click);
            pic_xU_Plus5.Click -= new EventHandler(Set_Click);
            pic_Hz_Add1.Click -= new EventHandler(Set_Click);
            pic_Hz_Plus1.Click -= new EventHandler(Set_Click);

            pic_GLYS_0p25L.Click -= new EventHandler(Set_Click);
            pic_JXFS_P.Click -= new EventHandler(Set_Click);
            pic_JXFS_P34.Click -= new EventHandler(Set_Click);
            pic_JXFS_P32.Click -= new EventHandler(Set_Click);
            pic_YJ_A.Click -= new EventHandler(Set_Click);
            pic_YJ_B.Click -= new EventHandler(Set_Click);
            pic_YJ_C.Click -= new EventHandler(Set_Click);
            pic_YJ_H.Click -= new EventHandler(Set_Click);
            pic_U_57p7.Click -= new EventHandler(Set_Click);
            pic_U_100.Click -= new EventHandler(Set_Click);
            pic_U_220.Click -= new EventHandler(Set_Click);
            pic_U_0.Click -= new EventHandler(Set_Click);
            pic_I_0p3.Click -= new EventHandler(Set_Click);
            pic_I_1p5.Click -= new EventHandler(Set_Click);
            pic_I_5.Click -= new EventHandler(Set_Click);
            pic_I_10.Click -= new EventHandler(Set_Click);
            pic_I_20.Click -= new EventHandler(Set_Click);
            pic_I_40.Click -= new EventHandler(Set_Click);
            pic_xI_0p01.Click -= new EventHandler(Set_Click);
            pic_xI_0p02.Click -= new EventHandler(Set_Click);
            pic_xI_0p1.Click -= new EventHandler(Set_Click);
            pic_xI_0p2.Click -= new EventHandler(Set_Click);
            pic_xI_1.Click -= new EventHandler(Set_Click);
            pic_xI_2.Click -= new EventHandler(Set_Click);
            pic_GLYS_0p25L.Click -= new EventHandler(Set_Click);
            pic_GLYS_1p0.Click -= new EventHandler(Set_Click);
            pic_GLYS_0p5L.Click -= new EventHandler(Set_Click);
            pic_GLYS_0p5C.Click -= new EventHandler(Set_Click);
            pic_GLYS_0p8L.Click -= new EventHandler(Set_Click);
            pic_GLYS_0p8C.Click -= new EventHandler(Set_Click);
            pic_Hz_50.Click -= new EventHandler(Set_Click);
            #endregion

            #region Click +=
            pic_xI_Add1.Click += new EventHandler(Set_Click);
            pic_xI_Add10.Click += new EventHandler(Set_Click);
            pic_xI_Plus1.Click += new EventHandler(Set_Click);
            pic_xI_Plus10.Click += new EventHandler(Set_Click);
            pic_xU_Add5.Click += new EventHandler(Set_Click);
            pic_xU_Plus5.Click += new EventHandler(Set_Click);
            pic_Hz_Add1.Click += new EventHandler(Set_Click);
            pic_Hz_Plus1.Click += new EventHandler(Set_Click);

            pic_GLYS_0p25L.Click += new EventHandler(Set_Click);
            pic_JXFS_P.Click += new EventHandler(Set_Click);
            pic_JXFS_P34.Click += new EventHandler(Set_Click);
            pic_JXFS_P32.Click += new EventHandler(Set_Click);
            pic_YJ_A.Click += new EventHandler(Set_Click);
            pic_YJ_B.Click += new EventHandler(Set_Click);
            pic_YJ_C.Click += new EventHandler(Set_Click);
            pic_YJ_H.Click += new EventHandler(Set_Click);
            pic_U_57p7.Click += new EventHandler(Set_Click);
            pic_U_100.Click += new EventHandler(Set_Click);
            pic_U_220.Click += new EventHandler(Set_Click);
            pic_U_0.Click += new EventHandler(Set_Click);
            pic_I_0p3.Click += new EventHandler(Set_Click);
            pic_I_1p5.Click += new EventHandler(Set_Click);
            pic_I_5.Click += new EventHandler(Set_Click);
            pic_I_10.Click += new EventHandler(Set_Click);
            pic_I_20.Click += new EventHandler(Set_Click);
            pic_I_40.Click += new EventHandler(Set_Click);
            pic_xI_0p01.Click += new EventHandler(Set_Click);
            pic_xI_0p02.Click += new EventHandler(Set_Click);
            pic_xI_0p1.Click += new EventHandler(Set_Click);
            pic_xI_0p2.Click += new EventHandler(Set_Click);
            pic_xI_1.Click += new EventHandler(Set_Click);
            pic_xI_2.Click += new EventHandler(Set_Click);
            pic_GLYS_0p25L.Click += new EventHandler(Set_Click);
            pic_GLYS_1p0.Click += new EventHandler(Set_Click);
            pic_GLYS_0p5L.Click += new EventHandler(Set_Click);
            pic_GLYS_0p5C.Click += new EventHandler(Set_Click);
            pic_GLYS_0p8L.Click += new EventHandler(Set_Click);
            pic_GLYS_0p8C.Click += new EventHandler(Set_Click);
            pic_Hz_50.Click += new EventHandler(Set_Click);
            #endregion

            #region MouseEnter -=
            ptn_ON.MouseEnter -= new EventHandler(pic_MouseEnter);
            ptn_OFF.MouseEnter -= new EventHandler(pic_MouseEnter);

            pic_xI_Add1.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_xI_Add10.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_xI_Plus1.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_xI_Plus10.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_xU_Add5.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_xU_Plus5.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_Hz_Add1.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_Hz_Plus1.MouseEnter -= new EventHandler(pic_MouseEnter);

            pic_GLYS_0p25L.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_JXFS_P.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_JXFS_P34.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_JXFS_P32.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_YJ_A.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_YJ_B.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_YJ_C.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_YJ_H.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_U_57p7.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_U_100.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_U_220.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_U_0.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_I_0p3.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_I_1p5.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_I_5.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_I_10.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_I_20.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_I_40.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_xI_0p01.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_xI_0p02.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_xI_0p1.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_xI_0p2.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_xI_1.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_xI_2.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_GLYS_0p25L.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_GLYS_1p0.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_GLYS_0p5L.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_GLYS_0p5C.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_GLYS_0p8L.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_GLYS_0p8C.MouseEnter -= new EventHandler(pic_MouseEnter);
            pic_Hz_50.MouseEnter -= new EventHandler(pic_MouseEnter);
            #endregion

            #region MouseEnter +=
            ptn_ON.MouseEnter += new EventHandler(pic_MouseEnter);
            ptn_OFF.MouseEnter += new EventHandler(pic_MouseEnter);

            pic_xI_Add1.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_xI_Add10.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_xI_Plus1.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_xI_Plus10.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_xU_Add5.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_xU_Plus5.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_Hz_Add1.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_Hz_Plus1.MouseEnter += new EventHandler(pic_MouseEnter);

            pic_GLYS_0p25L.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_JXFS_P.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_JXFS_P34.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_JXFS_P32.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_YJ_A.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_YJ_B.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_YJ_C.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_YJ_H.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_U_57p7.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_U_100.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_U_220.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_U_0.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_I_0p3.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_I_1p5.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_I_5.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_I_10.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_I_20.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_I_40.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_xI_0p01.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_xI_0p02.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_xI_0p1.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_xI_0p2.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_xI_1.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_xI_2.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_GLYS_0p25L.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_GLYS_1p0.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_GLYS_0p5L.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_GLYS_0p5C.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_GLYS_0p8L.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_GLYS_0p8C.MouseEnter += new EventHandler(pic_MouseEnter);
            pic_Hz_50.MouseEnter += new EventHandler(pic_MouseEnter);
            #endregion

            #region MouseLeave -=
            ptn_ON.MouseLeave -= new EventHandler(pic_MouseLeave);
            ptn_OFF.MouseLeave -= new EventHandler(pic_MouseLeave);

            pic_xI_Add1.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_xI_Add10.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_xI_Plus1.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_xI_Plus10.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_xU_Add5.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_xU_Plus5.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_Hz_Add1.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_Hz_Plus1.MouseLeave -= new EventHandler(pic_MouseLeave);

            pic_GLYS_0p25L.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_JXFS_P.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_JXFS_P34.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_JXFS_P32.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_YJ_A.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_YJ_B.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_YJ_C.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_YJ_H.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_U_57p7.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_U_100.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_U_220.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_U_0.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_I_0p3.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_I_1p5.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_I_5.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_I_10.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_I_20.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_I_40.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_xI_0p01.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_xI_0p02.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_xI_0p1.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_xI_0p2.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_xI_1.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_xI_2.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_GLYS_0p25L.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_GLYS_1p0.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_GLYS_0p5L.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_GLYS_0p5C.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_GLYS_0p8L.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_GLYS_0p8C.MouseLeave -= new EventHandler(pic_MouseLeave);
            pic_Hz_50.MouseLeave -= new EventHandler(pic_MouseLeave);
            #endregion

            #region MouseLeave +=
            ptn_ON.MouseLeave += new EventHandler(pic_MouseLeave);
            ptn_OFF.MouseLeave += new EventHandler(pic_MouseLeave);

            pic_xI_Add1.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_xI_Add10.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_xI_Plus1.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_xI_Plus10.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_xU_Add5.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_xU_Plus5.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_Hz_Add1.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_Hz_Plus1.MouseLeave += new EventHandler(pic_MouseLeave);

            pic_GLYS_0p25L.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_JXFS_P.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_JXFS_P34.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_JXFS_P32.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_YJ_A.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_YJ_B.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_YJ_C.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_YJ_H.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_U_57p7.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_U_100.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_U_220.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_U_0.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_I_0p3.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_I_1p5.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_I_5.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_I_10.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_I_20.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_I_40.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_xI_0p01.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_xI_0p02.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_xI_0p1.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_xI_0p2.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_xI_1.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_xI_2.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_GLYS_0p25L.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_GLYS_1p0.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_GLYS_0p5L.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_GLYS_0p5C.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_GLYS_0p8L.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_GLYS_0p8C.MouseLeave += new EventHandler(pic_MouseLeave);
            pic_Hz_50.MouseLeave += new EventHandler(pic_MouseLeave);
            #endregion

            ptn_ON.Click -= new EventHandler(ptn_ON_Click);
            ptn_OFF.Click -= new EventHandler(ptn_OFF_Click);

            ptn_ON.Click+=new EventHandler(ptn_ON_Click);
            ptn_OFF.Click+=new EventHandler(ptn_OFF_Click);

            btn_PowerOn.Click -= new EventHandler(btn_PowerOn_Click);
            btn_PowerOff.Click -= new EventHandler(btn_PowerOff_Click);

            btn_PowerOn.Click += new EventHandler(btn_PowerOn_Click);
            btn_PowerOff.Click += new EventHandler(btn_PowerOff_Click);
        }

        void btn_PowerOff_Click(object sender, EventArgs e)
        {
            ptn_OFF_Click(sender, e);
            CLDC_DataCore.Const.GlobalUnit.Clfs = clfsTmp;
        }

        void btn_PowerOn_Click(object sender, EventArgs e)
        {
            lock (objLockthPn)
            {
                CLDC_DataCore.Const.GlobalUnit.OnlyToPower = true;
                float Ua = 0, Ub = 0, Uc = 0;
                float Ia = 0, Ib = 0, Ic = 0;
                double PhiUa = 0; double PhiUb = 0; double PhiUc = 0;
                double PhiIa = 0; double PhiIb = 0; double PhiIc = 0;
                float feq = 50;

                if (chk_UA.Checked == true)
                {
                    Ua = (float)dbi_UA.Value;
                    PhiUa = dbi_CUA.Value;
                }
                if (chk_UB.Checked == true)
                {
                    Ub = (float)dbi_UB.Value;
                    PhiUb = dbi_CUB.Value;
                }
                if (chk_UC.Checked == true)
                {
                    Uc = (float)dbi_UC.Value;
                    PhiUc = dbi_CUC.Value;
                }
                if (chk_IA.Checked == true)
                {
                    Ia = (float)dbi_IA.Value;
                    PhiIa = dbi_CIA.Value;
                }
                if (chk_IB.Checked == true)
                {
                    Ib = (float)dbi_IB.Value;
                    PhiIb = dbi_CIB.Value;
                }
                if (chk_IC.Checked == true)
                {
                    Ic = (float)dbi_IC.Value;
                    PhiIc = dbi_CIC.Value;
                }
                feq = (float)dbi_Hz.Value;

                isXieBo = chk_XieBo.Checked;
                XieBoFaName = Cmb_XieBo.Text;
                
                System.Threading.Thread thPn = new System.Threading.Thread(() =>
                {
                    if (isXieBo)
                    {
                        LoadXieBo(XieBoFaName);
                        setXieBo(XieBoItem);
                    }
                    CLDC_VerifyAdapter.Helper.EquipHelper.Instance.PowerOnFree(Ua, Ub, Uc, Ia, Ib, Ic, PhiUa, PhiUb, PhiUc, PhiIa, PhiIb, PhiIc, feq);
                    CLDC_DataCore.Const.GlobalUnit.OnlyToPower = false;
                });
                thPn.IsBackground = true;
                thPn.Start();
            }
        }

        #region 控源参数字典
        Dictionary<string, string> DicPram = new Dictionary<string, string>();
        void InitDicPram()
        {
            DicPram.Clear();
            DicPram.Add(pic_JXFS_P.Name, "单相");
            DicPram.Add(pic_JXFS_P34.Name, "三相四线");
            DicPram.Add(pic_JXFS_P32.Name, "三相三线");
            DicPram.Add(pic_YJ_A.Name, "A相");
            DicPram.Add(pic_YJ_B.Name, "B相");
            DicPram.Add(pic_YJ_C.Name, "C相");
            DicPram.Add(pic_YJ_H.Name, "合元");
            DicPram.Add(pic_U_57p7.Name, "57.7V");
            DicPram.Add(pic_U_100.Name, "100V");
            DicPram.Add(pic_U_220.Name, "220V");
            DicPram.Add(pic_U_0.Name, "0V");
            DicPram.Add(pic_I_0p3.Name, "0.3A");
            DicPram.Add(pic_I_1p5.Name, "1.5A");
            DicPram.Add(pic_I_5.Name, "5A");
            DicPram.Add(pic_I_10.Name, "10A");
            DicPram.Add(pic_I_20.Name, "20A");
            DicPram.Add(pic_I_40.Name, "40A");
            DicPram.Add(pic_xI_0p01.Name, "0.01Ib");
            DicPram.Add(pic_xI_0p02.Name, "0.02Ib");
            DicPram.Add(pic_xI_0p1.Name, "0.1Ib");
            DicPram.Add(pic_xI_0p2.Name, "0.2Ib");
            DicPram.Add(pic_xI_1.Name, "1Ib");
            DicPram.Add(pic_xI_2.Name, "2Ib");
            DicPram.Add(pic_GLYS_0p25L.Name, "0.25L");
            DicPram.Add(pic_GLYS_1p0.Name, "1.0");
            DicPram.Add(pic_GLYS_0p5L.Name, "0.5L");
            DicPram.Add(pic_GLYS_0p5C.Name, "0.5C");
            DicPram.Add(pic_GLYS_0p8L.Name, "0.8L");
            DicPram.Add(pic_GLYS_0p8C.Name, "0.8C");
            DicPram.Add(pic_Hz_50.Name, "50Hz");
            //pic_Hz_Add1
            //pic_Hz_Plus1
            //pic_xI_Add1
            //pic_xI_Add10
            //pic_xI_Plus1
            //pic_xI_Plus10
            //pic_xU_Add5
            //pic_xU_Plus5
            
            
        }
        string getDicValue(string Key)
        {
            string rdv = "";
            if (DicPram.ContainsKey(Key))
            {
                rdv = DicPram[Key];
            }
            else
            {
                rdv = "Error";
            }
            return rdv;
        }
        #endregion

        private void pic_MouseEnter(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                PictureBox picBox = sender as PictureBox;
                picBox.SizeMode = PictureBoxSizeMode.CenterImage;
                picBox.Image = Image.FromFile(Application.StartupPath + @"\Pic\images\" + picBox.Name + ".gif");
            }
        }

        private void pic_MouseLeave(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                PictureBox picBox = sender as PictureBox;
                picBox.Image = null;
            }
        }
        #region 命名规则
        //pic_JXFS_P
        //pic_YJ_B
        //pic_U_100
        //pic_xU_Add5
        //pic_I_0p3
        //pic_xI_0p01
        //pic_xI_Add1
        //pic_GLYS_0p25L
        //pic_Hz_Add1
        //pic_Hz_50
        //pic_Hz_Plus1
        #endregion
        private void Set_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                PictureBox pcb = sender as PictureBox;
                string CrtName = pcb.Name;
                string[] strTp = CrtName.Split('_');
                if (strTp.Length == 3)
                {
                    switch (strTp[1])
                    {
                        case "JXFS":
                            {
                                txt_JXFS.Text = getDicValue(CrtName);

                                break;
                            }
                        case "YJ":
                            {
                                txt_YJ.Text = getDicValue(CrtName);

                                break;
                            }
                        case "U":
                            {
                                txt_U.Text = getDicValue(CrtName);
                                LcaU = getU();
                                break;
                            }
                        case "xU":
                            {
                                if (strTp[2].IndexOf("Plus") != -1)
                                {
                                    float dU = getU();
                                    float LsU = (float)(dU - LcaU * 0.05);
                                    if (LsU < 0)
                                    {
                                        txt_U.Text = "0V";
                                    }
                                    else
                                    {
                                        txt_U.Text = LsU.ToString("F4") + "V";
                                    }
                                }
                                else if (strTp[2].IndexOf("Add") != -1)
                                {
                                    float dU = getU();
                                    txt_U.Text = (dU + LcaU * 0.05).ToString("F4") + "V";
                                }
                                break;
                            }
                        case "I":
                            {
                                txt_I.Text = getDicValue(CrtName);
                                //txt_xIb.Text = "1Ib";//重新选择基本电流时，把倍数归1
                                
                                break;
                            }
                        case "xI":
                            {
                                string strxI = "Error";
                                if (strTp[2].IndexOf("Plus") != -1)
                                {
                                    float dI = getxI();
                                    float pS = float.Parse(strTp[2].Substring(4).Replace("p", "")) / 100;
                                    strxI = (dI - pS).ToString("F2") + "Ib";
                                    if (dI - pS < 0)
                                    {
                                        strxI = "0Ib";
                                    }
                                }
                                else if (strTp[2].IndexOf("Add") != -1)
                                {
                                    float dI = getxI();
                                    float pS = float.Parse(strTp[2].Substring(3).Replace("p", "")) / 100;
                                    strxI = (dI + pS).ToString("F2") + "Ib";
                                }
                                else
                                {
                                    strxI = getDicValue(CrtName);
                                    LcaxI = getxI();
                                }
                                txt_xIb.Text = strxI;
                                break;
                            }
                        case "GLYS":
                            {
                                txt_GLYS.Text = getDicValue(CrtName);
                                break;
                            }
                        case "Hz":
                            {
                                string strHz = "Error";
                                if (strTp[2].IndexOf("Plus") != -1)
                                {
                                    float dHz = getHz();
                                    float pH = (float)(dHz - LcaHz * 0.01);
                                    strHz = pH.ToString("F4") + "Hz";
                                }
                                else if (strTp[2].IndexOf("Add") != -1)
                                {
                                    float dHz = getHz();
                                    float pH = (float)(dHz + LcaHz * 0.01);
                                    strHz = pH.ToString("F4") + "Hz";
                                }
                                else
                                {
                                    strHz = getDicValue(CrtName);
                                }
                                txt_Hz.Text = strHz;
                                break;
                            }

                        default:
                            break;
                    }
                }
            }
        }
        object objLockthPn = new object();
        /// <summary>
        /// 升源单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ptn_ON_Click(object sender, EventArgs e)
        {
            lock (objLockthPn)
            {
                try
                {
                    CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetPowerSupplyType(3, false);
                }
                catch { }
                CLDC_DataCore.Const.GlobalUnit.OnlyToPower = true;
                
                CLDC_DataCore.Const.GlobalUnit.Clfs = getCLFS();//接线方式
                float U = 0;
                float I = 0;
                float feq = 50;
                CLDC_Comm.Enum.Cus_PowerYuanJian ele = CLDC_Comm.Enum.Cus_PowerYuanJian.H;
                CLDC_Comm.Enum.Cus_PowerFangXiang glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功;
                
                string glys = "1.0";
                bool bYouGong = true;
                bYouGong = rdobtn_A.Checked;//有功无功
                bool isDuiBiao = false;
                CLDC_Comm.Enum.Cus_PowerPhase isNXX = CLDC_Comm.Enum.Cus_PowerPhase.正相序;

                U = getU();
                I = getI();
                feq = getHz();
                ele = getYJ();
                glfx = getGLFX();
                glys = txt_GLYS.Text;//getGLYS();
                isNXX = chk_IsNXX.Checked ? CLDC_Comm.Enum.Cus_PowerPhase.电压逆相序 : CLDC_Comm.Enum.Cus_PowerPhase.正相序;
                isXieBo = chk_XieBo.Checked;
                XieBoFaName = Cmb_XieBo.Text;
                //CLDC_VerifyAdapter.Helper.EquipHelper.Instance.PowerOn(U, I, ele, glfx, glys, bYouGong, isDuiBiao);
                System.Threading.Thread thPn = new System.Threading.Thread(() =>
                {
                    if (isXieBo)
                    {
                        LoadXieBo(XieBoFaName);
                        setXieBo(XieBoItem);
                    }
                    CLDC_VerifyAdapter.Helper.EquipHelper.Instance.PowerOn(U, U, U, I, I, I, ele, feq, glys, bYouGong, isDuiBiao, isNXX, glfx);
                    CLDC_DataCore.Const.GlobalUnit.OnlyToPower = false;
                });
                thPn.IsBackground = true;
                thPn.Start();
            }
        }

        private CLDC_Comm.Enum.Cus_Clfs getCLFS()
        {
            CLDC_Comm.Enum.Cus_Clfs Rc = CLDC_Comm.Enum.Cus_Clfs.单相;
            if ("三相三线" == txt_JXFS.Text)
            {
                Rc = CLDC_Comm.Enum.Cus_Clfs.三相三线;
            }
            else if("三相四线"==txt_JXFS.Text)
            {
                Rc = CLDC_Comm.Enum.Cus_Clfs.三相四线;
            }
            return Rc;
        }

        private float getHz()
        {
            return float.Parse(txt_Hz.Text.Replace("Hz", ""));
        }

        private float getI()
        {
            return float.Parse(txt_I.Text.Replace("A", "")) * getxI();
        }

        private float getxI()
        {
            return float.Parse(txt_xIb.Text.Replace("Ib", ""));
        }

        private float getU()
        {
            return float.Parse(txt_U.Text.Replace("V", ""));
        }
        #region 私有函数
        private string getGLYS()
        {
            string glys = "1.0";
            switch (txt_GLYS.Text)
            {
                case "1.0":
                    {
                        glys = "1.0";
                        break;
                    }
                case "0.25L":
                    {
                        glys = "0.25L";
                        break;
                    }
                case "0.5L":
                    {
                        glys = "0.5L";
                        break;
                    }
                case "0.5C":
                    {
                        glys = "0.5C";
                        break;
                    }
                case "0.8L":
                    {
                        glys = "0.8L";
                        break;
                    }
                case "0.8C":
                    {
                        glys = "0.8C";
                        break;
                    }
                default:
                    break;
            }
            return glys;
        }

        private CLDC_Comm.Enum.Cus_PowerFangXiang getGLFX()
        {
            CLDC_Comm.Enum.Cus_PowerFangXiang glfx=CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功;
            if (rdobtn_A.Checked && rdobtn_P.Checked)
            {
                glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.正向有功;
            }
            else if (rdobtn_A.Checked && rdobtn_Q.Checked)
            {
                glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.正向无功;
            }
            else if (rdobtn_RA.Checked && rdobtn_P.Checked)
            {
                glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.反向有功;
            }
            else if (rdobtn_RA.Checked && rdobtn_Q.Checked)
            {
                glfx = CLDC_Comm.Enum.Cus_PowerFangXiang.反向无功;
            }
            return glfx;
        }

        private CLDC_Comm.Enum.Cus_PowerYuanJian getYJ()
        {
            CLDC_Comm.Enum.Cus_PowerYuanJian ele = CLDC_Comm.Enum.Cus_PowerYuanJian.Error;
            if ("A相" == txt_YJ.Text)
            {
                ele = CLDC_Comm.Enum.Cus_PowerYuanJian.A;
            }
            else if ("B相" == txt_YJ.Text)
            {
                ele = CLDC_Comm.Enum.Cus_PowerYuanJian.B;
            }
            else if ("C相" == txt_YJ.Text)
            {
                ele = CLDC_Comm.Enum.Cus_PowerYuanJian.C;
            }
            else
            {
                ele = CLDC_Comm.Enum.Cus_PowerYuanJian.H;
            }
            return ele;
        }
        #endregion
        private void ptn_OFF_Click(object sender, EventArgs e)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.PowerOff();
            CLDC_DataCore.Const.GlobalUnit.OnlyToPower = false;
        }

        void InitXieBoList()
        {
            #region ---------------获取谐波方案集合-----------------
            CLDC_DataCore.SystemModel.Item.csXieBo XieBoMode = new CLDC_DataCore.SystemModel.Item.csXieBo();

            Cmb_XieBo.Items.Clear();

            Cmb_XieBo.Items.Add("不加谐波");

            for (int i = 0; i < XieBoMode.FaNameList.Count; i++)
            {
                Cmb_XieBo.Items.Add(XieBoMode.FaNameList[i]);
            }

            Cmb_XieBo.SelectedIndex = 0;

            #endregion
        }
        public void LoadXieBo(string XieBoFa)
        {
            XieBoItem = new List<CLDC_DataCore.Struct.StXieBo>();
            
            CLDC_DataCore.SystemModel.Item.csXieBo _XieBoItem = new CLDC_DataCore.SystemModel.Item.csXieBo();

            XieBoItem = _XieBoItem.getXieBoFa(XieBoFa);

        }
        void setXieBo(List<CLDC_DataCore.Struct.StXieBo> xieBoItem)
        {
            CLDC_VerifyAdapter.Helper.EquipHelper.HarmonicPhasePara[] arrPara = new CLDC_VerifyAdapter.Helper.EquipHelper.HarmonicPhasePara[6];
            for (int i = 0; i < arrPara.Length; i++)
            {
                arrPara[i] = new CLDC_VerifyAdapter.Helper.EquipHelper.HarmonicPhasePara();
                arrPara[i].Initialize();
            }
            if (xieBoItem.Count > 0)
            {
                CLDC_DataCore.Struct.StXieBo item;
                int inum = 0;
                int cnum = 0;
                for (int i = 0; i < xieBoItem.Count; i++)
                {
                    item = xieBoItem[i];
                    inum = (int)item.YuanJian - 2;
                    if (!item.IsUb)
                    {
                        inum += 3;              //电流偏移3
                    }
                    inum %= 6;
                    cnum = item.Num - 1;
                    cnum %= 64;                 //最大64次
                    if (cnum == 0) cnum++;      //0为基波位置
                    
                    arrPara[inum].IsOpen = true;                        //本相开关
                    arrPara[SwitchXieBo(inum)].TimeSwitch[cnum] = true; //本次开关
                    arrPara[inum].Content[0] = 1F;                      //基波含量
                    arrPara[inum].Content[cnum] = item.Extent / 100F;   //含量
                    arrPara[inum].Phase[cnum] = item.Xw;                //相角
                }
                CLDC_VerifyAdapter.Helper.EquipHelper.Instance.SetHarmonic(arrPara[0], arrPara[1], arrPara[2],
                    arrPara[3], arrPara[4], arrPara[5]);
            }
        }
        private int SwitchXieBo(int source)
        {
            if (source == 1 || source == 4)
                return source;
            if (source % 2 == 0)
            {
                return Math.Abs(source - 2);
            }
            else
            {
                if (source == 3)
                    return 5;
                else
                    return 3;
            }
        }

        private void chk_XieBo_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_XieBo.Checked)
            {

                new CLDC_MeterUI.UI_FA.Frm_XieBoSetup().ShowDialog();
                
                InitXieBoList();
            }
            else
            {
                
            }
        }

        //TODO：源控界面，按照平板样式

    }
}

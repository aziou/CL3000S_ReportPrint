//上下位，挂表状态测试
//单三相通用
//fjk，2013-03-20
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CLDC_Comm;
using CLDC_VerifyAdapter;
using System.Threading;
using System.Drawing.Drawing2D;
using CLDC_VerifyAdapter.Multi;
using DevComponents.DotNetBar;

namespace CLDC_MeterUI.UI_Detection_New
{
    //fjk
    public enum StatusType
    {
        上限位 = 1,
        表位 = 16,
        下限位 = 31
    }
    public partial class UI_ElectShow : Office2007Form
    {
        
        public UI_ElectShow()
        {
            InitializeComponent();
            this.button2.Enabled = false;
            
        }
        Thread invokeThread = null;
        /// <summary>
        /// 读表位状态、可视化，fjk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                ////上限位
                setStatus(i, StatusType.上限位, Color.Gray);
                ////挂表
                setStatus(i, StatusType.表位, Color.Gray);
                ////下限位
                setStatus(i, StatusType.下限位, Color.Gray);
            }
            invokeThread = new Thread(new ThreadStart(ReadElcStatus));
            invokeThread.Name = "ThrReadElcStatus";
            invokeThread.Start();
            if (!invokeThread.IsAlive)
            {
                invokeThread.Abort();
            }
        }
        /// <summary>
        /// 异步委托
        /// </summary>
        /// <param name="index"></param>
        /// <param name="st"></param>
        /// <param name="cr"></param>
        private delegate void dlgBeginInvoke(int index,StatusType st, Color cr);
        private delegate void dlgIBeginInvoke(int index, StatusType st, Image cr);
        /// <summary>
        /// 异步设置状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="st"></param>
        /// <param name="cr"></param>
        private void setStatus(int index,StatusType st, Color cr)
        {
            this.BeginInvoke(new dlgBeginInvoke((int i, StatusType bst, Color bcr) => { this.Controls.Find("pictureBox" + (i - i / 15 * 15 + i / 15 * 45 + (int)bst), true)[0].BackColor = bcr; }), new object[] { index, st, cr });
        }
        /// <summary>
        /// 异步设置 值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="st"></param>
        /// <param name="cr"></param>
        private void setStatus(int index, StatusType st, Image img)
        {
            this.BeginInvoke(new dlgIBeginInvoke((int i, StatusType bst, Image bimg) => { ((PictureBox)(this.Controls.Find("pictureBox" + (i - i / 15 * 15 + i / 15 * 45 + (int)bst), true)[0])).Image = bimg; }), new object[] { index, st, img });
        }
        /// <summary>
        /// 状态
        /// </summary>
        private void ReadElcStatus()
        {
            string[] str_Status;
            
            
            string strData = "";

            str_Status = new string[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];

            CLDC_VerifyAdapter.Helper.EquipHelper.Instance.GetBWStatus(out str_Status);

            //if (!bResult) bResult = Adapter.ComAdpater.ReadTaskData(ref arrData, ref arrWcCount, ref str_Status);
            //判断电表压接电机是否在x限位
            
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                if (str_Status[i] == null || str_Status[i] == "")
                {

                    ////上限位
                    setStatus(i, StatusType.上限位, Color.Black);
                    ////挂表
                    setStatus(i, StatusType.表位, Color.Black);
                    ////下限位
                    setStatus(i, StatusType.下限位, Color.Black);

                    continue;
                }
                strData = str_Status[i].Substring(0, 1);

                if (strData != "1")
                {
                    ////下限没到位
                    setStatus(i, StatusType.下限位, Color.Red);
                }
                else if (strData == "1")
                {
                    ////下限到位
                    setStatus(i, StatusType.下限位, Color.Green);
                }
                strData = str_Status[i].Substring(1, 1);
                if (strData != "1")
                {
                    ////上限没到位
                    setStatus(i, StatusType.上限位, Color.Red);
                }
                else if (strData == "1")
                {
                    ////上限到位
                    setStatus(i, StatusType.上限位, Color.Green);
                }
                strData = str_Status[i].Substring(2, 1);
                if (strData != "1")
                {
                    ////没挂表
                    setStatus(i, StatusType.表位, Color.Red);
                }
                else if (strData == "1")
                {
                    ////挂表
                    setStatus(i, StatusType.表位, Color.Green);
                }

            }
        }
        /// <summary>
        /// 读电能表表号和UI,fjk
        /// </summary>
        private void ReadAddrAndUI()
        {
            //            02	01	01	00	XXX.X	2	V	*	A相电压
            //                      02							B相电压
            //                      03							C相电压
            //                      FF							电压数据块



            //02	02	01	00	XXX.XXX	3	A	*	A相电流
            //          02							B相电流
            //          03							C相电流
            //          FF							电流数据块
            string[] strU = new string[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            string[] strI = new string[CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws];
            //if (Adapter.Adpater485 == null)
            //{
            //    Adapter.Ini485Adpater();
            //}
            //Adapter.Adpater485.Reset();
            //bool blnRst = Adapter.Adpater485.ReadData("02010100", 2, 1);
            //for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            //{
            //    if (Control485.CurReturnString[i] != null)
            //    {
            //        strU[i] = Control485.CurReturnString[i];
            //        Bitmap img = ConvertToImage(strU[i]);
            //        setStatus(i, StatusType.上限位, img);
            //    }
            //    else
            //    {
            //        setStatus(i, StatusType.上限位, Color.Black);
            //    }
            //}
            //blnRst = Adapter.Adpater485.ReadData("02020100", 3, 3);
            //for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            //{
            //    if (Control485.CurReturnString[i] != null)
            //    {
            //        strI[i] = Control485.CurReturnString[i];
            //        Bitmap img = ConvertToImage(strI[i]);
            //        setStatus(i, StatusType.下限位, img);
            //    }
            //    else
            //    {
            //        setStatus(i, StatusType.下限位, Color.Black);
            //    }
            //}
            ////Bitmap img = ConvertToImage("220.02");
            ////setStatus(0, StatusType.上限位, img);
            ////this.pictureBox1.Refresh();
            return;
        }

        private Bitmap ConvertToImage(string strFormat)
        {
            Bitmap img = new Bitmap(40, 13);
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(new LinearGradientBrush(new Point(10, 10), new Point(500, 100), pictureBox1.BackColor, pictureBox1.BackColor), new Rectangle(new Point(0, 0), img.Size));
            g.DrawString(strFormat, new Font(FontFamily.GenericSerif, 9f), new LinearGradientBrush(new Point(10, 10), new Point(500, 500), Color.Blue, Color.Blue), new PointF(0f, 0f));
            return img;
        }
        /// <summary>
        /// 结束线程，fjk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_ElectShow_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (invokeThread != null)
            {
                invokeThread.Abort();
            }
        }
        /// <summary>
        /// 读电表UI click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CLDC_DataCore.Const.GlobalUnit.g_CUS.DnbData._Bws; i++)
            {
                ////上限位
                setStatus(i, StatusType.上限位, Color.Gray);
                ////挂表
                setStatus(i, StatusType.表位, Color.Gray);
                ////下限位
                setStatus(i, StatusType.下限位, Color.Gray);
            }
            invokeThread = new Thread(new ThreadStart(ReadAddrAndUI));
            invokeThread.Name = "ThrReadAddrAndUI";
            invokeThread.IsBackground = true;
            invokeThread.Start();
            //if (!invokeThread.IsAlive)
            //{
            //    invokeThread.Abort();
            //}

        }







    }
}

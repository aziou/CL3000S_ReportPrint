using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// ί��ִ��һ������
    /// </summary>
    /// <param name="Params"></param>
    public delegate void EventInvoke(params object[] Params);

    /// <summary>
    /// ί��ִ��һ������
    /// </summary>
    public delegate void EventInvokeWithNoParams();

    /// <summary>
    /// ���Ƽ����ò����ࡢ����̼߳�����ؼ�������
    /// </summary>
    public class SetControl
    {

        #region ���ÿؼ��Ŀ���״̬ ����/������SetEnabled(Control Contrl, bool bEnable)
        /// <summary>
        /// ���ÿؼ��Ŀ���״̬ ����/������
        /// </summary>
        /// <param name="Contrl"></param>
        /// <param name="bEnable"></param>
        public static void SetEnabled(Control Contrl, bool bEnable)
        {
            if (Contrl.InvokeRequired)
            {
                Contrl.Invoke(new EventSetEnabled(InvokeSetEnabled), Contrl, bEnable); 
            }
            else
            {
                InvokeSetEnabled(Contrl, bEnable);
            }
        }
        private delegate void EventSetEnabled(Control Contrl, bool bEnable);
        private static void InvokeSetEnabled(Control Contrl, bool bEnable)
        {
            if (Contrl == null || !Contrl.IsHandleCreated || Contrl.IsDisposed) return;
            Contrl.Enabled = bEnable;
        }



        /// <summary>
        /// ���ÿؼ��Ŀ���״̬ ����/������
        /// </summary>
        /// <param name="Contrl"></param>
        /// <param name="bEnable"></param>
        public static void SetEnabled( Form  Contrl, bool bEnable)
        {
            if (Contrl.InvokeRequired)
            {
                try
                {
                    Contrl.Invoke(new EventSetEnabled2(InvokeSetEnabled2), Contrl, bEnable);
                }
                catch
                {
                    try
                    {
                        InvokeSetEnabled2( Contrl, bEnable);
                    }
                    catch {}
                }
            }
            else
            {
                InvokeSetEnabled2(  Contrl, bEnable);
            }
        }
        private delegate void EventSetEnabled2( Form Contrl, bool bEnable);
        private static void InvokeSetEnabled2( Form Contrl, bool bEnable)
        {
            if (Contrl == null || !Contrl.IsHandleCreated || Contrl.IsDisposed) return;
            Contrl.Enabled = bEnable;
        }
        #endregion

        #region ���ÿؼ�����״̬ ��ʾ/����SetVisible(Control Contrl, bool bVisible)
        /// <summary>
        /// ���ÿؼ�����״̬ ��ʾ/����
        /// </summary>
        /// <param name="Contrl"></param>
        /// <param name="bVisible"></param>
        public static void SetVisible(Control Contrl, bool bVisible)
        {
            if (Contrl.InvokeRequired)
            {
                Contrl.Invoke(new EventSetVisible(InvokeSetVisible), Contrl, bVisible);
            }
            else
            {
                InvokeSetVisible(Contrl, bVisible);
            }
        }
        private delegate void EventSetVisible(Control Contrl, bool bVisible);
        private static void InvokeSetVisible(Control Contrl, bool bVisible)
        {
            if (Contrl == null || !Contrl.IsHandleCreated || Contrl.IsDisposed) return;
            Contrl.Visible = bVisible;
        }
        #endregion

        #region ���ÿؼ����ı�SetText(Control Contrl, string text)
        /// <summary>
        /// ���ÿؼ����ı�
        /// </summary>
        /// <param name="Contrl"></param>
        /// <param name="text"></param>
        public static void SetText(Control Contrl, string text)
        {
            if (Contrl.InvokeRequired)
            {
                Contrl.Invoke(new EventSetText(InvokeSetText), Contrl, text);
            }
            else
            {
                InvokeSetText(Contrl, text);
            }
        }
        private delegate void EventSetText(Control Contrl, string text);
        private static void InvokeSetText(Control Contrl, string text)
        {
            if (Contrl == null || !Contrl.IsHandleCreated || Contrl.IsDisposed) return;
            Contrl.Text = text;
        }
        /// <summary>
        /// ���ÿؼ����ı�
        /// </summary>
        /// <param name="InvokeControl"></param>
        /// <param name="Contrl"></param>
        /// <param name="text"></param>
        public static void SetText(Control InvokeControl,ToolStripItem Contrl, string text)
        {
            if (InvokeControl.InvokeRequired)
            {
                InvokeControl.Invoke(new EventSetText_ToolStripItem(InvokeSetText_ToolStripItem), Contrl, text);
            }
            else
            {
                InvokeSetText_ToolStripItem(Contrl, text);
            }
        }
        private delegate void EventSetText_ToolStripItem(ToolStripItem Contrl, string text);
        private static void InvokeSetText_ToolStripItem(ToolStripItem Contrl, string text)
        {
            if (Contrl == null ||  Contrl.IsDisposed) return;
            Contrl.Text = text;
        }
        #endregion

        #region ���ÿؼ��� TopMost ����SetTopmost(Form Contrl, bool btopmost)
        /// <summary>
        /// ���ÿؼ��� TopMost ����
        /// </summary>
        /// <param name="Contrl"></param>
        /// <param name="btopmost"></param>
        public static void SetTopmost(Form Contrl, bool btopmost)
        {
            if (Contrl.InvokeRequired)
            {
                Contrl.Invoke(new EventTopmost(InvokeTopmost), Contrl, btopmost);
            }
            else
            {
                InvokeTopmost(Contrl, btopmost);
            }
        }
        private delegate void EventTopmost(Form Contrl, bool btopmost);
        private static void InvokeTopmost(Form Contrl, bool btopmost)
        {
            if (Contrl == null || !Contrl.IsHandleCreated || Contrl.IsDisposed) return;
            Contrl.TopMost = btopmost;
        }
        #endregion

        #region ���ÿؼ�������λ��SetLocation(Control Contrl, Point Loca)
        /// <summary>
        /// ���ÿؼ�������λ��
        /// </summary>
        /// <param name="Contrl"></param>
        /// <param name="Loca"></param>
        public static void SetLocation(Control Contrl, Point Loca)
        {
            if (Contrl.InvokeRequired)
            {
                Contrl.Invoke(new EventSetLocation(InvokeSetLocation), Contrl, Loca);
            }
            else
            {
                InvokeSetLocation(Contrl, Loca);
            }
        }
        private delegate void EventSetLocation(Control Contrl, Point Loca);
        private static void InvokeSetLocation(Control Contrl, Point Loca)
        {
            if (Contrl == null || !Contrl.IsHandleCreated || Contrl.IsDisposed) return;
            Contrl.Location = Loca;
        }
        #endregion

        #region ���ÿؼ��Ŀ��SetWidth(Control Contrl, int width)
        /// <summary>
        /// ���ÿؼ��Ŀ�� 
        /// </summary>
        /// <param name="Contrl"></param>
        /// <param name="width"></param>
        public static void SetWidth(Control Contrl, int width)
        {
            if (Contrl.InvokeRequired)
            {
                Contrl.Invoke(new EventSetWidth(InvokeSetWidth), Contrl, width);
            }
            else
            {
                InvokeSetWidth(Contrl, width);
            }
        }
        private delegate void EventSetWidth(Control Contrl, int width);
        private static void InvokeSetWidth(Control Contrl, int width)
        {
            if (Contrl == null || !Contrl.IsHandleCreated || Contrl.IsDisposed) return;
            Contrl.Width = width;
        }
        #endregion

        #region ���ÿؼ��ĸ߶�SetHeight(Control Contrl, int height)
        /// <summary>
        /// ���ÿؼ��ĸ߶�
        /// </summary>
        /// <param name="Contrl"></param>
        /// <param name="height"></param>
        public static void SetHeight(Control Contrl, int height)
        {
            if (Contrl.InvokeRequired)
            {
                Contrl.Invoke(new EventSetHeight(InvokeSetHeight), Contrl, height);
            }
            else
            {
                InvokeSetHeight(Contrl, height);
            }
        }
        private delegate void EventSetHeight(Control Contrl, int height);
        private static void InvokeSetHeight(Control Contrl, int height)
        {
            if (Contrl == null || !Contrl.IsHandleCreated || Contrl.IsDisposed) return;
            Contrl.Height = height;
        }
        #endregion

        #region ���ñ�����ɫSetBackColor(Control Contrl, Color color)
        /// <summary>
        /// ���ñ�����ɫ
        /// </summary>
        /// <param name="Contrl"></param>
        /// <param name="color"></param>
        public static void SetBackColor(Control Contrl, Color color)
        {
            if (Contrl.InvokeRequired)
            {
                Contrl.Invoke(new EventSetBackColor(InvokeSetBackColor), Contrl, color);
            }
            else
            {
                InvokeSetBackColor(Contrl, color);
            }
        }
        private delegate void EventSetBackColor(Control Contrl, Color color);
        private static void InvokeSetBackColor(Control Contrl, Color color)
        {
            if (Contrl == null || !Contrl.IsHandleCreated || Contrl.IsDisposed) return;
            Contrl.BackColor = color;
        }
        #endregion

        #region ����ǰ����ɫSetForceColor(Control Contrl, Color color)
        /// <summary>
        /// ����ǰ����ɫ
        /// </summary>
        /// <param name="Contrl"></param>
        /// <param name="color"></param>
        public static void SetForceColor(Control Contrl, Color color)
        {
            if (Contrl.InvokeRequired)
            {
                Contrl.Invoke(new EventSetForceColor(InvokeSetForceColor), Contrl, color);
            }
            else
            {
                InvokeSetForceColor(Contrl, color);
            }
        }
        private delegate void EventSetForceColor(Control Contrl, Color color);
        private static void InvokeSetForceColor(Control Contrl, Color color)
        {
            if (Contrl == null || !Contrl.IsHandleCreated || Contrl.IsDisposed) return;
            Contrl.ForeColor = color;
        }
        #endregion

        #region ���ý�������ǰֵ�����ֵ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TControl"></param>
        /// <param name="curPorcess"></param>
        /// <param name="maxValue"></param>
        public static void SetProcessbar(ToolStripProgressBar TControl, int curPorcess, int maxValue)
        {
            
            InvokeSetProcessbar(TControl, curPorcess,maxValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TControl"></param>
        /// <param name="curProcess"></param>
        /// <param name="maxProcess"></param>
        public delegate void EventSetProcessbar(ToolStripProgressBar TControl, int curProcess, int maxProcess);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TControl"></param>
        /// <param name="curPorcess"></param>
        /// <param name="maxValue"></param>
        public static void InvokeSetProcessbar(ToolStripProgressBar TControl, int curPorcess, int maxValue)
        {
            if (TControl == null  || TControl.IsDisposed) return;
            if (curPorcess > maxValue) curPorcess = maxValue;
            TControl.Maximum = maxValue;
            TControl.Value = curPorcess;

            #region ���½�������Ϣ
            Command.Update.UpdateProgressBar_Ask progressBarAsk=new Command.Update.UpdateProgressBar_Ask();
            progressBarAsk.CurrentValue=curPorcess;
            progressBarAsk.MaxValue=maxValue;
            CLDC_DataCore.Const.GlobalUnit.g_DataControl.AddMsg(CLDC_DataCore.Const.GlobalUnit.g_DataControl, progressBarAsk);
            #endregion
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="TControl"></param>
        /// <param name="Value"></param>
        public static void SetProcessbar(ProgressBar TControl, int Value)
        {
            TControl.Invoke(new EventSetProcessbar2(InvokeSetProcessbar2),Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TControl"></param>
        /// <param name="Value"></param>
        public delegate void EventSetProcessbar2(ProgressBar TControl, int Value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TControl"></param>
        /// <param name="Value"></param>
        public static void InvokeSetProcessbar2(ProgressBar TControl, int Value)
        {
            if (TControl == null || TControl.IsDisposed) return;
            TControl.Value = Value ;
        }


        #endregion

        #region ί��ִ��һ������Invoke(EventInvoke invoke,params object[] Params)
        /// <summary>
        /// ί��ִ��һ������
        /// </summary>
        /// <param name="Contrl">ί��������ؼ����ڵ����߳���</param>
        /// <param name="invoke">Comm.Function.EventInvoke</param>
        /// <param name="Params"></param>
        public static void Invoke(Control Contrl, EventInvoke invoke, params object[] Params)
        {
            if (Contrl.IsHandleCreated && !Contrl.IsDisposed)
            {
                if (Contrl.InvokeRequired)
                {
                    Contrl.Invoke(invoke, Params);
                }
                else
                {
                    invoke(invoke, Params);
                }
            }
        }

        /// <summary>
        /// ί��ִ��һ������������������
        /// </summary>
        /// <param name="Contrl">ί��������ؼ����ڵ����߳���</param>
        /// <param name="invoke"></param>
        public static void InvokeWithNoParams(Control Contrl, EventInvokeWithNoParams invoke)
        {
            if (Contrl.IsHandleCreated && !Contrl.IsDisposed)
            {
                if (Contrl.InvokeRequired)
                {
                    Contrl.Invoke(invoke);
                }
                else
                {
                    invoke();
                }
            }
        }
        #endregion

        #region ����PictureBox��ͼ��
        /// <summary>
        /// ����PictureBox��ͼ��
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="Url">��Ի����·��</param>
        public static void SetPictureImg(PictureBox Ctrl, string Url)
        {
            if (Ctrl.InvokeRequired)
            {
                Ctrl.Invoke(new EventInvokeSetPictureImg(InvokeSetPictureImg), Ctrl, Url);
            }
            else
            {
                InvokeSetPictureImg(Ctrl, Url);
            }
        }
        private delegate void EventInvokeSetPictureImg(PictureBox Ctrl, string Url);
        private  static void InvokeSetPictureImg(PictureBox Ctrl, string Url)
        {
            Url = CLDC_DataCore.Function.File.GetPhyPath(Url);
            if (!System.IO.File.Exists(Url))
            {
                MessageBox.Show(string.Format("��Դ�ļ�{0}��δ�ҵ�", Url), "ȱ���ļ�!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (CLDC_DataCore.Function.File.GetExtName(Url).ToLower() == ".ico")
                {
                    Ctrl.Image = (Image)new Icon(Url).ToBitmap();
                }
                else
                {
                    Ctrl.Load(Url);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(string.Format("{0}\r\n{1}",Url,ex.Message ) , "����ͼƬ����",MessageBoxButtons.OK , MessageBoxIcon.Error );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="img"></param>
        public static void SetPictureImg(PictureBox Ctrl, Image  img)
        {
            if (Ctrl.InvokeRequired)
            {
                Ctrl.Invoke(new EventSetPictureImg2(SetPictureImg2),Ctrl ,img );
            }
            else
            {
                SetPictureImg2( Ctrl,  img);
            }
        }
        private delegate  void EventSetPictureImg2(PictureBox Ctrl, Image img);

        private static  void SetPictureImg2(PictureBox Ctrl, Image img)
        {
            Ctrl.Image = img;
        }

        #endregion

    }
}

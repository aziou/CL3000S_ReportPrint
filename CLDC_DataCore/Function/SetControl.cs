using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// 委托执行一个函数
    /// </summary>
    /// <param name="Params"></param>
    public delegate void EventInvoke(params object[] Params);

    /// <summary>
    /// 委托执行一个函数
    /// </summary>
    public delegate void EventInvokeWithNoParams();

    /// <summary>
    /// 控制件常用操作类、解决线程间操作控件的问题
    /// </summary>
    public class SetControl
    {

        #region 设置控件的可用状态 可用/不可用SetEnabled(Control Contrl, bool bEnable)
        /// <summary>
        /// 设置控件的可用状态 可用/不可用
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
        /// 设置控件的可用状态 可用/不可用
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

        #region 设置控件可显状态 显示/隐藏SetVisible(Control Contrl, bool bVisible)
        /// <summary>
        /// 设置控件可显状态 显示/隐藏
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

        #region 设置控件的文本SetText(Control Contrl, string text)
        /// <summary>
        /// 设置控件的文本
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
        /// 设置控件的文本
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

        #region 设置控件的 TopMost 属性SetTopmost(Form Contrl, bool btopmost)
        /// <summary>
        /// 设置控件的 TopMost 属性
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

        #region 设置控件的坐标位置SetLocation(Control Contrl, Point Loca)
        /// <summary>
        /// 设置控件的坐标位置
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

        #region 设置控件的宽度SetWidth(Control Contrl, int width)
        /// <summary>
        /// 设置控件的宽度 
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

        #region 设置控件的高度SetHeight(Control Contrl, int height)
        /// <summary>
        /// 设置控件的高度
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

        #region 设置背景颜色SetBackColor(Control Contrl, Color color)
        /// <summary>
        /// 设置背景颜色
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

        #region 设置前景颜色SetForceColor(Control Contrl, Color color)
        /// <summary>
        /// 设置前景颜色
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

        #region 设置进度条当前值，最大值
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

            #region 更新进度条信息
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

        #region 委托执行一个函数Invoke(EventInvoke invoke,params object[] Params)
        /// <summary>
        /// 委托执行一个函数
        /// </summary>
        /// <param name="Contrl">委托在这个控件所在的线线程中</param>
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
        /// 委托执行一个函数、不带参数的
        /// </summary>
        /// <param name="Contrl">委托在这个控件所在的线线程中</param>
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

        #region 设置PictureBox的图象、
        /// <summary>
        /// 设置PictureBox的图象、
        /// </summary>
        /// <param name="Ctrl"></param>
        /// <param name="Url">相对或绝对路径</param>
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
                MessageBox.Show(string.Format("资源文件{0}、未找到", Url), "缺少文件!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show(string.Format("{0}\r\n{1}",Url,ex.Message ) , "加载图片出错！",MessageBoxButtons.OK , MessageBoxIcon.Error );
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

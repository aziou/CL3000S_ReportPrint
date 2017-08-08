using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CLDC_DataCore.Function
{

    /// <summary>
    /// 
    /// </summary>
    public delegate void CallBack();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    public delegate void CallBack_Inc_Para1(object obj);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    public delegate void CallBack_Inc_Para2(object obj1, object obj2);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    public delegate void CallBack_Params(params object[] obj);

    /// <summary>
    /// 
    /// </summary>
    public class ThreadCallBack
    {

        /// <summary>
        /// 延迟调用
        /// </summary>
        /// <param name="callBack"></param>
        /// <param name="DelayMSecond"></param>
        public static void Call(CallBack callBack, int DelayMSecond)
        {
            CLDC_CTNProtocol.ThreadPool.QueueUserWorkItem(new WaitCallback(thCall), new object[] { callBack, DelayMSecond });
        }

        private static void thCall(object obj)
        {
            CallBack callBack = (CallBack)((object[])obj)[0];
            int DelayMSecond = (int)((object[])obj)[1];
            Thread.Sleep(DelayMSecond);
            callBack();
        }

        /// <summary>
        /// 延迟调用、委托
        /// </summary>
        /// <param name="InvokeParent">委托所依附的对象</param>
        /// <param name="callBack">委托对象</param>
        /// <param name="DelayMSecond">延迟时间(ms)</param>
        public static void BeginInvoke(Control InvokeParent, CallBack callBack, int DelayMSecond)
        {
            //ThreadPool.QueueUserWorkItem( new WaitCallback(thBeginInvoke),new object[] {InvokeParent,callBack,DelayMSecond });
            CLDC_CTNProtocol.ThreadPool.QueueUserWorkItem(new WaitCallback(thBeginInvoke), new object[] { InvokeParent, callBack, DelayMSecond });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InvokeParent"></param>
        /// <param name="callBack"></param>
        /// <param name="DelayMSecond"></param>
        /// <param name="objParams"></param>
        public static void BeginInvoke(Control InvokeParent, CallBack_Params callBack, int DelayMSecond, params object[] objParams)
        {
            //ThreadPool.QueueUserWorkItem(new WaitCallback(thBeginInvoke2), new object[] { InvokeParent, callBack, DelayMSecond,objParams });
            CLDC_CTNProtocol.ThreadPool.QueueUserWorkItem(new WaitCallback(thBeginInvoke2), new object[] { InvokeParent, callBack, DelayMSecond, objParams });
        }

        private static void thBeginInvoke(object obj)
        {
            Control InvokeParent = (Control)((object[])obj)[0];
            CallBack callBack = (CallBack)((object[])obj)[1];
            int DelayMSecond = (int)((object[])obj)[2];
            System.Threading.Thread.Sleep(DelayMSecond);
            try
            {
                InvokeParent.Invoke(callBack);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + InvokeParent.GetType() + "  " + InvokeParent.Name);
            }
        }
        private static void thBeginInvoke2(object obj)
        {
            Control InvokeParent = (Control)((object[])obj)[0];
            CallBack_Params callBack = (CallBack_Params)((object[])obj)[1];
            int DelayMSecond = (int)((object[])obj)[2];

            object[] objParams = (object[])((object[])obj)[3];
            System.Threading.Thread.Sleep(DelayMSecond);
            try
            {
                InvokeParent.Invoke(callBack, objParams);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + InvokeParent.GetType() + "  " + InvokeParent.Name);
            }
        }


    }



}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CLDC_DataCore.Function
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorLog
    {

        /// <summary>
        /// д������־�ļ�(ÿСʱһ���ļ�)
        /// </summary>
        /// <param name="ex"></param>
        public static  void Write(Exception ex)
        {
            try
            {
                string LogPath = string.Format(@"ErrLog\{0}.txt", DateTime.Now.ToString("yyyy-MM-dd hh"));
                LogPath = CLDC_DataCore.Function.File.GetPhyPath(LogPath);
                FileStream Fs = CLDC_DataCore.Function.File.Create(LogPath);
                if (Fs == null)
                {
                   // MessageBox.Show(string.Format("��־�ļ�{0}����ʧ��!", LogPath));
                    return;
                }
                Fs.Close();
                Fs.Dispose();

                string ErrTxt = string.Format(@"
{0}:{1}
    {2}
    {3}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), ex.Message, ex.StackTrace, ex.InnerException);

                System.IO.File.AppendAllText(LogPath, ErrTxt);
            }
            catch { }
        }


    }
}

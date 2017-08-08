using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using DeviceDriver.Drivers.Geny;

namespace MethodCaller
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            //CtrComm.YCIVCtrClassClass yc;
            //CtrComm_StdMeter.ClsStdCommClass cl;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Byte[] buf = DataFormart.Formart(1.0, 3, false);

            List<Type> types = DeviceDriver.Drivers.Geny.Packets.PacketSeracher.GetAllOutPacketType();

            Thread t = new Thread(new ThreadStart(GenyThreadCaller));
            //t.Start();

            //t = new Thread(new ThreadStart(StdCommMethodCallerMethod));
            //t.Start();

            t = new Thread(new ThreadStart(VBDllThreadCaller));
            t.Start();

            t = new Thread(new ThreadStart(RequestThreadCaller));
            t.Start();

            Comm.GlobalUnit.g_SystemConfig = new Comm.SystemModel.SystemInfo();
            Comm.GlobalUnit.g_SystemConfig.Load();
            Application.Run(new DataAnalysis.DataAnalysiser());
        }


        static void GenyThreadCaller()
        {
            Application.Run(new GenyDriverMethodCaller());
        }

        static void VBDllThreadCaller()
        {
            Application.Run(new VBDLLMethodCaller());
        }

        static void RequestThreadCaller()
        {
            Application.Run(new RequestPacketTest());
        }

        static void StdCommMethodCallerMethod()
        {
            Application.Run(new StdCommMethodCaller());
        }
    }
}

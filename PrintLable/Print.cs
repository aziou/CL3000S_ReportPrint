using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.InteropServices;

namespace PrintLable
{
    public class Print
    {
        [DllImport("kernel32")]
        private extern static int GetPrivateProfileStringA(string segName, string keyName, string sDefault, StringBuilder retValue, int nSize, string fileName);
        [DllImport("kernel32")]
        private extern static int WritePrivateProfileStringA(string segName, string keyName, string sValue, string fileName);

        #region------------获取配置文件数据---------------------------
        /// <summary>
        /// 获取ini配置文件数据，默认路径，运行目录下Manageinfo.ini，默认数据空
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="skey"></param>
        /// <returns></returns>
        public static string getIniString(string sSection, string skey)
        {
            return getIniString(sSection, skey, "");
        }
        /// <summary>
        /// 获取INI配置文件数据，默认路径，运行目录下Manageinfo.ini
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="skey"></param>
        /// <param name="sDefault"></param>
        /// <returns></returns>
        public static string getIniString(string sSection, string skey, string sDefault)
        {
            return getIniString(sSection, skey, sDefault, System.Windows.Forms.Application.StartupPath + "\\Plugins\\ManageInfo.ini");
        }
        /// <summary>
        /// 获取INIp配置文件数据
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="skey"></param>
        /// <param name="sDefault"></param>
        /// <param name="IniFilePath"></param>
        /// <returns></returns>
        public static string getIniString(string sSection, string skey, string sDefault, string IniFilePath)
        {
            StringBuilder Buff = new StringBuilder(255);

            int Int_Len = GetPrivateProfileStringA(sSection, skey, sDefault, Buff, 255, IniFilePath);

            return Buff.ToString();

        }
        #endregion

        #region---------------写配置文件---------------------------
        /// <summary>
        /// 写配置文件数据,默认配置文件路径，运行目录下Manageinfo.ini
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="sKey"></param>
        /// <param name="sValue"></param>
        public static void WriteIni(string sSection, string sKey, string sValue)
        {
            WriteIni(sSection, sKey, sValue, System.Windows.Forms.Application.StartupPath + "\\Plugins\\ManageInfo.ini");
        }
        /// <summary>
        /// 写配置文件数据
        /// </summary>
        /// <param name="sSection"></param>
        /// <param name="sKey"></param>
        /// <param name="sValue"></param>
        /// <param name="IniPath"></param>
        public static void WriteIni(string sSection, string sKey, string sValue, string IniPath)
        {
            WritePrivateProfileStringA(sSection, sKey, sValue, IniPath);
        }

        #endregion
        public static int  PrintTheLable(string lis_zcbh, string man, string timeForCheck)
        {
            uint lab_width, lab_height, lab_X, lab_Y, lab_Fix,lab_Diriction;
            lab_width = Convert.ToUInt16( getIniString("OtherInfo", "txt_lableWidth"));

            lab_height = Convert.ToUInt16(getIniString("OtherInfo", "txt_lableHeight"));

            lab_X = Convert.ToUInt16(getIniString("OtherInfo", "txt_lableX"));

            lab_Y = Convert.ToUInt16(getIniString("OtherInfo", "txt_lableY"));

            lab_Fix = Convert.ToUInt16(getIniString("OtherInfo", "txt_labFix"));
            lab_Diriction = Convert.ToUInt16(getIniString("OtherInfo", "cmb_labDirection"));
            int result = 1;
            try
            {
                PrintLab.OpenPort("POSTEK C168/300s");//打开打印机端口
                PrintLab.PTK_ClearBuffer();           //清空缓冲区
                PrintLab.PTK_SetPrintSpeed(4);        //设置打印速度
                PrintLab.PTK_SetDarkness(20);         //设置打印黑度
                PrintLab.PTK_SetLabelHeight(lab_height, 15); //设置标签的高度和定位间隙\黑线\穿孔的高度
                PrintLab.PTK_SetLabelWidth(lab_width);

                for (int i = 1; i <= 1; i++)
                {
                    #region 注释
                    //PrintLab.PTK_DrawTextTrueTypeW(200, 300, 40, 40, "宋体", 1, 400, false, true, true, "1", "12456789");//打印一行 TrueType Font文字
                    //PrintLab.PTK_DrawBarcode(100, 20, 0, "1", 3, 3, 80, 'N', "12345");//打印一个条码
                    //PrintLab.PTK_SetPagePrintCount(1, 1);//命令打印机执行打印工作

                    //// 画矩形
                    //PrintLab.PTK_DrawRectangle(58, 15, 3, 558, 312);


                    //// 打印PCX图片 方式一
                    //PrintLab.PTK_PcxGraphicsDel("PCX");
                    //PrintLab.PTK_PcxGraphicsDownload("PCX", "logo.pcx");
                    //PrintLab.PTK_DrawPcxGraphics(80, 20, "PCX");

                    //// 打印PCX图片 方式二
                    //// PTK_PrintPCX(80,30,pchar('logo.pcx'));

                    //// 打印一个条码;
                    //PrintLab.PTK_DrawBarcode(300, 23, 0, "1", 2, 2, 50, 'B', "123456789");              

                    //// 画表格分割线
                    //PrintLab.PTK_DrawLineOr(58, 100, 500, 3);               

                    //// 打印一行TrueTypeFont文字;
                    //PrintLab.PTK_DrawTextTrueTypeW(80, 120, 40, 0, "Arial", 1, 400, false, false, false, "A1", "TrueTypeFont");                

                    // 打印一行文本文字(内置字体或软字体);
                    //PrintLab.PTK_DrawText(380, 180, 2, 2, 1, 1, 'N', "09001FE00000000007963031");
                    //PrintLab.PTK_DrawText(380, 150, 2, 2, 1, 1, 'N', "09001FE00000000007963031");
                    #endregion
                    

                    string zcbh_patr1 = "", zcbh_part2 = "";
                    zcbh_patr1 = lis_zcbh.Substring(1, (lis_zcbh.Length / 2));
                    zcbh_part2 = lis_zcbh.Substring((lis_zcbh.Length / 2), (lis_zcbh.Length / 2));


                    PrintLab.PTK_DrawText(lab_X, lab_Y - lab_Fix, lab_Diriction, 3, 1, 1, 'N', zcbh_part2);
                    //PrintLab.PTK_DrawText(322, 190 - 22, 0, 2, 1, 1, 'N', zcbh_part2);
                    //Console.WriteLine(i);
                    PrintLab.PTK_DrawText(lab_X, lab_Y - 2 * lab_Fix, lab_Diriction, 3, 1, 1, 'N', timeForCheck);
                    PrintLab.PTK_DrawText(lab_X, lab_Y - 3 * lab_Fix - 3, lab_Diriction, 3, 1, 1, 'N', man);




                    //// 打印PDF417码
                    //PrintLab.PTK_DrawBar2D_Pdf417(80, 210, 400, 300, 0, 0, 3, 7, 10, 2, 0, 0, "123456789");//PDF417码

                    //// 打印QR码
                    //PrintLab.PTK_DrawBar2D_QR(420, 120, 180, 180, 0, 3, 2, 0, 0, "Postek Electronics Co., Ltd.");


                    //// 打印一行TrueTypeFont文字旋转;
                    //PrintLab.PTK_DrawTextTrueTypeW(520, 102, 22, 0, "Arial", 2, 400, false, false, false, "A2", "www.postek.com.cn");
                    //PrintLab.PTK_DrawTextTrueTypeW(80, 260, 19, 0, "Arial", 1, 700, false, false, false, "A3", "Use different ID_NAME for different Truetype font objects");


                    // 命令打印机执行打印工作
                    PrintLab.PTK_PrintLabel(1, 1);
                    PrintLab.ClosePort();//关闭打印机端口
                    result = 0;
                }
            }
            catch (Exception print_EX)
            { 
            
            }
            return result;
             
        }
        
    }
    public static class PrintLab
    {
        [DllImport("WINPSK.dll")]
        public static extern int OpenPort(string printname);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_SetPrintSpeed(uint px);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_SetDarkness(uint id);
        [DllImport("WINPSK.dll")]
        public static extern int ClosePort();
        [DllImport("WINPSK.dll")]
        public static extern int PTK_PrintLabel(uint number, uint cpnumber);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawTextTrueTypeW
                                            (int x, int y, int FHeight,
                                            int FWidth, string FType,
                                            int Fspin, int FWeight,
                                            bool FItalic, bool FUnline,
                                            bool FStrikeOut,
                                            string id_name,
                                            string data);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawBarcode(uint px,
                                        uint py,
                                        uint pdirec,
                                        string pCode,
                                        uint pHorizontal,
                                        uint pVertical,
                                        uint pbright,
                                        char ptext,
                                        string pstr);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_SetLabelHeight(uint lheight, uint gapH);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_SetLabelWidth(uint lwidth);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_ClearBuffer();
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawRectangle(uint px, uint py, uint thickness, uint pEx, uint pEy);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawLineOr(uint px, uint py, uint pLength, uint pH);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawBar2D_QR(uint x, uint y, uint w, uint v, uint o, uint r, uint m, uint g, uint s, string pstr);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawBar2D_Pdf417(uint x, uint y, uint w, uint v, uint s, uint c, uint px, uint py, uint r, uint l, uint t, uint o, string pstr);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_PcxGraphicsDel(string pid);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_PcxGraphicsDownload(string pcxname, string pcxpath);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawPcxGraphics(uint px, uint py, string gname);
        [DllImport("WINPSK.dll")]
        public static extern int PTK_DrawText(uint px, uint py, uint pdirec, uint pFont, uint pHorizontal, uint pVertical, char ptext, string pstr);


    }
    
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CLDC_Comm
{
    /// <summary>
    /// 
    /// </summary>
    public class Win32Api
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate bool WNDENUMPROC(int hwnd, int lParam);



        /// <summary>
        /// 
        /// </summary>
        public const int WM_PAINT = 0x000F;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_SETFOCUS = 0x0007;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_KILLFOCUS = 0x0008;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_ENABLE = 0x000A;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_SETTEXT = 0x000C;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_GETTEXT = 0x000D;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_GETTEXTLENGTH = 0x000E;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_CLOSE = 0x0010;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_QUIT = 0x0012;

        /// <summary>
        /// 获取和设置图标
        /// </summary>
        public const int WM_GETICON = 0x007F;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_SETICON = 0x0080;

        /// <summary>
        /// 
        /// </summary>
        public const int ICON_SMALL = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int ICON_BIG = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int ICON_SMALL2 = 2;	//XP系统中使用
        /// <summary>
        /// 获取和设置图标
        /// </summary>
        public const int BM_GETCHECK = 0x00F0;
        /// <summary>
        /// 
        /// </summary>
        public const int BM_SETCHECK = 0x00F1;
        /// <summary>
        /// 
        /// </summary>
        public const int BM_GETSTATE = 0x00F2;
        /// <summary>
        /// 
        /// </summary>
        public const int BM_SETSTATE = 0x00F3;
        /// <summary>
        /// 
        /// </summary>
        public const int BM_SETSTYLE = 0x00F4;
        /// <summary>
        /// 
        /// </summary>
        public const int BM_CLICK = 0x00F5;	//单击按钮
        /// <summary>
        /// 
        /// </summary>
        public const int BM_GETIMAGE = 0x00F6;
        /// <summary>
        /// 
        /// </summary>
        public const int BM_SETIMAGE = 0x00F7;
        /// <summary>
        /// 
        /// </summary>
        public const int BST_UNCHECKED = 0x0000;
        /// <summary>
        /// 
        /// </summary>
        public const int BST_CHECKED = 0x0001;
        /// <summary>
        /// 
        /// </summary>
        public const int BST_INDETERMINATE = 0x0002;
        /// <summary>
        /// 
        /// </summary>
        public const int BST_PUSHED = 0x0004;
        /// <summary>
        /// 
        /// </summary>
        public const int BST_FOCUS = 0x0008;

        /// <summary>
        /// 
        /// </summary>
        public const int WM_KEYFIRST = 0x0100;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_KEYDOWN = 0x0100;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_KEYUP = 0x0101;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_CHAR = 0x0102;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_DEADCHAR = 0x0103;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_SYSKEYDOWN = 0x0104;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_SYSKEYUP = 0x0105;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_SYSCHAR = 0x0106;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_SYSDEADCHAR = 0x0107;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_UNICHAR = 0x0109;			//XP系统
        /// <summary>
        /// 
        /// </summary>
        public const int WM_KEYLAST = 0x0109;
        /// <summary>
        /// 
        /// </summary>
        public const int UNICODE_NOCHAR = 0xFFFF;

        /// <summary>
        /// 鼠标消息ID
        /// </summary>
        public const int WM_MOUSEFIRST = 0x0200;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_MOUSEMOVE = 0x0200;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_LBUTTONDOWN = 0x0201;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_LBUTTONUP = 0x0202;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_LBUTTONDBLCLK = 0x0203;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_RBUTTONDOWN = 0x0204;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_RBUTTONUP = 0x0205;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_RBUTTONDBLCLK = 0x0206;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_MBUTTONDOWN = 0x0207;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_MBUTTONUP = 0x0208;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_MBUTTONDBLCLK = 0x0209;
        /// <summary>
        /// 滚轮消息
        /// </summary>
        public const int WM_MOUSEWHEEL = 0x020A;	
        /// <summary>
        /// 
        /// </summary>
        public const int WM_XBUTTONDOWN = 0x020B;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_XBUTTONUP = 0x020C;
        /// <summary>
        /// 
        /// </summary>
        public const int WM_XBUTTONDBLCLK = 0x020D;

        /// <summary>
        /// Key State Masks for Mouse Messages
        /// </summary>
        public const int MK_LBUTTON = 0x0001;
        /// <summary>
        /// 
        /// </summary>
        public const int MK_RBUTTON = 0x0002;
        /// <summary>
        /// 
        /// </summary>
        public const int MK_SHIFT = 0x0004;
        /// <summary>
        /// 
        /// </summary>
        public const int MK_CONTROL = 0x0008;
        /// <summary>
        /// 
        /// </summary>
        public const int MK_MBUTTON = 0x0010;
        /// <summary>
        /// 
        /// </summary>
        public const int MK_XBUTTON1 = 0x0020;
        /// <summary>
        /// 
        /// </summary>
        public const int MK_XBUTTON2 = 0x0040;
        /// <summary>
        /// ShowWindow() Constants
        /// </summary>
        public const int SW_HIDE = 0;
        /// <summary>
        /// 
        /// </summary>
        public const int SW_SHOWNORMAL = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int SW_NORMAL = 1;
        /// <summary>
        /// 
        /// </summary>
        public const int SW_SHOWMINIMIZED = 2;
        /// <summary>
        /// 
        /// </summary>
        public const int SW_SHOWMAXIMIZED = 3;
        /// <summary>
        /// 
        /// </summary>
        public const int SW_MAXIMIZE = 3;
        /// <summary>
        /// 显示但不激活窗口，一个关键参数
        /// </summary>
        public const int SW_SHOWNOACTIVATE = 4;		
        /// <summary>
        /// 
        /// </summary>
        public const int SW_SHOW = 5;
        /// <summary>
        /// 
        /// </summary>
        public const int SW_MINIMIZE = 6;
        /// <summary>
        /// 
        /// </summary>
        public const int SW_SHOWMINNOACTIVE = 7;
        /// <summary>
        /// 
        /// </summary>
        public const int SW_SHOWNA = 8;
        /// <summary>
        /// 
        /// </summary>
        public const int SW_RESTORE = 9;
        /// <summary>
        /// 
        /// </summary>
        public const int SW_SHOWDEFAULT = 10;
        /// <summary>
        /// 
        /// </summary>
        public const int SW_FORCEMINIMIZE = 11;
        /// <summary>
        /// 
        /// </summary>
        public const int SW_MAX = 11;

        /// <summary>
        /// GetWindow() Constants
        /// </summary>
        public const uint GW_HWNDFIRST = 0;
        /// <summary>
        /// 
        /// </summary>
        public const uint GW_HWNDLAST = 1;
        /// <summary>
        /// 
        /// </summary>
        public const uint GW_HWNDNEXT = 2;
        /// <summary>
        /// 
        /// </summary>
        public const uint GW_HWNDPREV = 3;
        /// <summary>
        /// 
        /// </summary>
        public const uint GW_OWNER = 4;
        /// <summary>
        /// 
        /// </summary>
        public const uint GW_CHILD = 5;
        /// <summary>
        /// 
        /// </summary>
        public const uint GW_ENABLEDPOPUP = 6;
        /// <summary>
        /// (低版本系统中定义为5)
        /// </summary>
        public const uint GW_MAX = 6;	//

        /// <summary>
        ///  Binary raster ops 
        /// </summary>
        public const int R2_BLACK = 1;   
        /// <summary>
        /// 0
        /// </summary>
        public const int R2_NOTMERGEPEN = 2; 
        /// <summary>
        /// DPon 
        /// </summary>
        public const int R2_MASKNOTPEN = 3;  
        /// <summary>
        ///  DPna
        /// </summary>
        public const int R2_NOTCOPYPEN = 4;   
        /// <summary>
        /// PN
        /// </summary>
        public const int R2_MASKPENNOT = 5;   
        /// <summary>
        /// PDna 
        /// </summary>
        public const int R2_NOT = 6;  
        /// <summary>
        ///  Dn
        /// </summary>
        public const int R2_XORPEN = 7;  
        /// <summary>
        ///  DPx
        /// </summary>
        public const int R2_NOTMASKPEN = 8;   
        /// <summary>
        /// DPan
        /// </summary>
        public const int R2_MASKPEN = 9;   
        /// <summary>
        /// DPa
        /// </summary>
        public const int R2_NOTXORPEN = 10; 
        /// <summary>
        /// DPxn
        /// </summary>
        public const int R2_NOP = 11;
        /// <summary>
        /// D
        /// </summary>
        public const int R2_MERGENOTPEN = 12; 
        /// <summary>
        /// DPno
        /// </summary>
        public const int R2_COPYPEN = 13;  
        /// <summary>
        ///  P
        /// </summary>
        public const int R2_MERGEPENNOT = 14; 
        /// <summary>
        /// PDno
        /// </summary>
        public const int R2_MERGEPEN = 15; 
        /// <summary>
        /// DPo
        /// </summary>
        public const int R2_WHITE = 16; 
        /// <summary>
        ///  1
        /// </summary>
        public const int R2_LAST = 16;

        /// <summary>
        /// Ternary raster operations 光栅操作码，BitBlt函数的参数
        /// </summary>
        public const int SRCCOPY = 0x00CC0020; 
        /// <summary>
        /// dest = source   
        /// </summary>
        public const int SRCPAINT = 0x00EE0086;
        /// <summary>
        /// dest = source OR dest
        /// </summary>
        public const int SRCAND = 0x008800C6;
        /// <summary>
        /// dest = source AND dest 
        /// </summary>
        public const int SRCINVERT = 0x00660046;
        /// <summary>
        /// dest = source XOR dest 
        /// </summary>
        public const int SRCERASE = 0x00440328; 
        /// <summary>
        /// dest = source AND (NOT dest )
        /// </summary>
        public const int NOTSRCCOPY = 0x00330008; 
        /// <summary>
        /// dest = (NOT source) 
        /// </summary>
        public const int NOTSRCERASE = 0x001100A6;
        /// <summary>
        /// dest = (NOT src) AND (NOT dest)
        /// </summary>
        public const int MERGECOPY = 0x00C000CA;
        /// <summary>
        ///  dest = (source AND pattern)
        /// </summary>
        public const int MERGEPAINT = 0x00BB0226;
        /// <summary>
        /// dest = (NOT source) OR dest 
        /// </summary>
        public const int PATCOPY = 0x00F00021;
        /// <summary>
        /// pattern
        /// </summary>
        public const int PATPAINT = 0x00FB0A09;
        /// <summary>
        /// dest = DPSnoo 
        /// </summary>
        public const int PATINVERT = 0x005A0049;
        /// <summary>
        /// dest = pattern XOR dest  dest = (NOT dest)
        /// </summary>
        public const int DSTINVERT = 0x00550009;
        /// <summary>
        /// dest = BLACK 
        /// </summary>
        public const int BLACKNESS = 0x00000042;
        /// <summary>
        /// dest = WHITE 
        /// </summary>
        public const int WHITENESS = 0x00FF0062; 

        /* Pen Styles */
        /// <summary>
        /// 
        /// </summary>
        public const int PS_SOLID = 0;
        /// <summary>
        ///     /* -------  */
        /// </summary>
        public const int PS_DASH = 1;   
        /// <summary>
        ///  /* .......  */
        /// </summary>
        public const int PS_DOT = 2;      
        /// <summary>
        /// /* _._._._  */
        /// </summary>
        public const int PS_DASHDOT = 3;   
        /// <summary>
        ///  /* _.._.._  */
        /// </summary>
        public const int PS_DASHDOTDOT = 4;     
        /// <summary>
        /// 
        /// </summary>
        public const int PS_NULL = 5;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_INSIDEFRAME = 6;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_USERSTYLE = 7;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_ALTERNATE = 8;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_STYLE_MASK = 0x0000000F;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_ENDCAP_ROUND = 0x00000000;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_ENDCAP_SQUARE = 0x00000100;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_ENDCAP_FLAT = 0x00000200;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_ENDCAP_MASK = 0x00000F00;

        /// <summary>
        /// 
        /// </summary>
        public const int PS_JOIN_ROUND = 0x00000000;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_JOIN_BEVEL = 0x00001000;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_JOIN_MITER = 0x00002000;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_JOIN_MASK = 0x0000F000;

        /// <summary>
        /// 
        /// </summary>
        public const int PS_COSMETIC = 0x00000000;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_GEOMETRIC = 0x00010000;
        /// <summary>
        /// 
        /// </summary>
        public const int PS_TYPE_MASK = 0x000F0000;



        
        /// <summary>
        /// 
        /// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT 
		{
            /// <summary>
            /// 
            /// </summary>
            /// <param name="_left"></param>
            /// <param name="_top"></param>
            /// <param name="_right"></param>
            /// <param name="_bottom"></param>
			public RECT(int _left,int _top,int _right,int _bottom)
			{
				Left=_left;
				Top=_top;
				Right=_right;
				Bottom=_bottom;
			}
            /// <summary>
            /// 
            /// </summary>
			public int Left;
            /// <summary>
            /// 
            /// </summary>
			public int Top;
            /// <summary>
            /// 
            /// </summary>
			public int Right;
            /// <summary>
            /// 
            /// </summary>
			public int Bottom;
		}

		/// <summary>
        /// Declare wrapper managed POINT class.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct POINT 
		{
            /// <summary>
            /// 
            /// </summary>
            /// <param name="_x"></param>
            /// <param name="_y"></param>
			public POINT(int _x,int _y)
			{
				X=_x;
				Y=_y;
			}
            /// <summary>
            /// 
            /// </summary>
			public int X;
            /// <summary>
            /// 
            /// </summary>
			public int Y;
		}

        /// <summary>
        /// 
        /// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct PAINTSTRUCT
		{ 
            /// <summary>
            /// 
            /// </summary>
			public int  hdc; 
            /// <summary>
            /// 
            /// </summary>
			public bool fErase; 
            /// <summary>
            /// 
            /// </summary>
			public RECT rcPaint; 
            /// <summary>
            /// 
            /// </summary>
			public bool fRestore; 
            /// <summary>
            /// 
            /// </summary>
			public bool fIncUpdate; 
            /// <summary>
            /// 
            /// </summary>
			public byte[] rgbReserved;
		}

        /// <summary>
        /// 
        /// </summary>
		[StructLayout(LayoutKind.Sequential)]
		public struct LOGPEN
		{
            /// <summary>
            /// 
            /// </summary>
			public uint lopnStyle;
			POINT lopnWidth;
			int lopnColor;
		}

		/// <summary>
		/// 判断一个点是否位于矩形内
		/// </summary>
		/// <param name="lprc"></param>
		/// <param name="pt"></param>
		/// <returns></returns>
		[DllImport("user32")]
		public static extern bool PtInRect(
			ref RECT lprc,
			POINT pt
			);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwndChild"></param>
        /// <param name="hwndNewParent"></param>
        /// <returns></returns>
		[DllImport("user32", EntryPoint="SetParent")]
		public static extern int SetParent (
			IntPtr hwndChild,
			int hwndNewParent
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpClassName"></param>
        /// <param name="lpWindowName"></param>
        /// <returns></returns>
		[DllImport("user32", EntryPoint="FindWindowA")]
		public static extern int FindWindow(
			string lpClassName,
			string lpWindowName
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwndParent"></param>
        /// <param name="hwndChildAfter"></param>
        /// <param name="lpszClass"></param>
        /// <param name="lpszWindow"></param>
        /// <returns></returns>
		[DllImport("user32", EntryPoint="FindWindowExA")]
		public static extern int FindWindowEx (
			int hwndParent,
			int hwndChildAfter,
			string lpszClass,		//窗口类
			string lpszWindow		//窗口标题
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern int SendMessage(
			int hWnd, 
			int wMsg, 
			int wParam, 
			IntPtr lParam
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
		[DllImport("user32.dll", EntryPoint = "SendMessage")]
		public static extern int SendMessage(
			int hWnd, 
			int wMsg, 
			int wParam, 
			int lParam
			);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
		[DllImport("user32.dll", EntryPoint = "SendMessageA")]
		public static extern int SendMessage(
			int hWnd, 
			int wMsg, 
			int wParam, 
			string lParam
			);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="wMsg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
		[DllImport("user32.dll", EntryPoint = "SendMessageA")]
		public static extern int SendMessage(
			int hWnd, 
			int wMsg, 
			int wParam, 
			StringBuilder lParam
			);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpdwProcessId"></param>
        /// <returns></returns>
		[DllImport("user32.dll")]
		public static extern int GetWindowThreadProcessId(
			int hWnd,
			ref int lpdwProcessId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dwMilliseconds"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern int Sleep(
			int dwMilliseconds
			);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(
			int hWnd,
			ref RECT lpRect
			);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpString"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern int GetWindowText(
			int hWnd,
			StringBuilder lpString,
			int nMaxCount
			);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(
			int hWnd,
			int nCmdShow
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns></returns>
		[DllImport("user32", EntryPoint="SetWindowLong")]
		public static extern uint SetWindowLong (
			IntPtr hwnd,
			int nIndex,
			uint dwNewLong
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
		[DllImport("user32", EntryPoint="GetWindowLong")]
		public static extern uint GetWindowLong (
			IntPtr hwnd,
			int nIndex
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="ColorRefKey"></param>
        /// <param name="bAlpha"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
		[DllImport("user32", EntryPoint="SetLayeredWindowAttributes")]
		public static extern int SetLayeredWindowAttributes (
			IntPtr hwnd,			//目标窗口句柄
			int ColorRefKey,		//透明色
			int bAlpha,				//不透明度
			int dwFlags
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="bRepaint"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern bool MoveWindow(
			int hWnd,
			int X,
			int Y,
			int nWidth,
			int nHeight,
			bool bRepaint
			);

		/// <summary>
        /// 获得窗口类名称，返回值为字符串的字符数量
		/// </summary>
		/// <param name="hWnd"></param>
		/// <param name="pszType"></param>
		/// <param name="cchType"></param>
		/// <returns></returns>
		[DllImport("user32")]
		public static extern uint RealGetWindowClass(
			int hWnd,
			StringBuilder pszType,		//缓冲区
			uint cchType);				//缓冲区长度

		/// <summary>
        /// 枚举屏幕上所有顶级窗口（不会枚举子窗口，除了一些有WS_CHILD的顶级窗口）
		/// </summary>
		/// <param name="lpEnumFunc"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		[DllImport("user32")]
		public static extern bool EnumWindows(
			WNDENUMPROC lpEnumFunc,
			int lParam
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWndParent"></param>
        /// <param name="lpEnumFunc"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern bool EnumChildWindows(
			int hWndParent,
			WNDENUMPROC lpEnumFunc,
			int lParam
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dwThreadId"></param>
        /// <param name="lpEnumFunc"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern bool EnumThreadWindows(		//枚举线程窗口
			int dwThreadId,
			WNDENUMPROC lpEnumFunc,
			int lParam
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern int GetParent(
			int hWnd
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="uCmd"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern int GetWindow(
			int hWnd,	//基础窗口
			uint uCmd	//关系
			);

        /// <summary>
        /// 获取鼠标的屏幕坐标，填充到Point
        /// * WINUSERAPI
         ///   BOOL
         ///   WINAPI
         ///   GetCursorPos(
        ///          __out LPPOINT lpPoint);
        /// </summary>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern bool GetCursorPos(
            out POINT lpPoint
            );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32")]
		public static extern int GetDC(
			int hWnd
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern int GetWindowDC(
			int hWnd
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hDC"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern int ReleaseDC(
			int hWnd,
			int hDC
			);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDC"></param>
        /// <param name="lprc"></param>
        /// <param name="hBrush"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern int FillRect(
			int hDC,
			RECT lprc,
			int hBrush
			);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpRect"></param>
        /// <param name="bErase"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern bool InvalidateRect(
			int hwnd,
			ref RECT lpRect,
			bool bErase
			);

		/// <summary>
        /// 判断一个窗口是否是可见的
		/// </summary>
		/// <param name="hwnd"></param>
		/// <returns></returns>
		[DllImport("user32")]
		public static extern bool IsWindowVisible(
			int hwnd
			);

		/// <summary>
        /// 绘制焦点举行
		/// </summary>
		/// <param name="hDC"></param>
		/// <param name="lprc"></param>
		/// <returns></returns>
		[DllImport("user32")]
		public static extern bool DrawFocusRect(
			int hDC,
			ref RECT lprc
			);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern bool UpdateWindow(
			int hwnd
			);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="bEnable"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern bool EnableWindow(
			int hwnd,
			bool bEnable
			);

		/// <summary>
        /// 设置前景窗口，强制其线程成为前台，并激活窗口
		/// </summary>
		/// <param name="hwnd"></param>
		/// <returns></returns>
		[DllImport("user32")]
		public static extern bool SetForegroundWindow(
			int hwnd
			);

		/// <summary>
        /// 设置前景窗口，强制其线程成为前台，并激活窗口
		/// </summary>
		/// <returns></returns>
		[DllImport("user32")]
		public static extern bool GetForegroundWindow(
			);

		/// <summary>
        /// 获取拥有焦点窗口（唯一拥有键盘输入的窗口）
		/// </summary>
		/// <returns></returns>
		[DllImport("user32")]
		public static extern int GetFocus(
			);

		/// <summary>
        /// 设置焦点窗口（返回值是前一个焦点窗口）
		/// </summary>
		/// <param name="hwnd"></param>
		/// <returns></returns>
		[DllImport("user32")]
		public static extern int SetFocus(
			int hwnd
			);

		/// <summary>
        /// 根据点查找窗口
		/// </summary>
		/// <param name="Point"></param>
		/// <returns></returns>
		[DllImport("user32")]
		public static extern int WindowFromPoint(
			POINT Point
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWndParent"></param>
        /// <param name="Point"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern int ChildWindowFromPoint(
			int hWndParent,
			POINT Point
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hIcon"></param>
        /// <returns></returns>
		[DllImport("user32")]
		public static extern bool DestroyIcon(
			int hIcon
			);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDC"></param>
        /// <param name="fnDrawMode"></param>
        /// <returns></returns>
		[DllImport("Gdi32")]
		public static extern int SetROP2(
			int hDC,
			int fnDrawMode
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDC"></param>
        /// <returns></returns>
		[DllImport("Gdi32")]
		public static extern int GetROP2(
			int hDC
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("Gdi32")]
		public static extern bool ValidateRect(
			int hWnd,
			ref RECT lpRect
			);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crColor"></param>
        /// <returns></returns>
		[DllImport("Gdi32")]
		public static extern int CreateSolidBrush(
			int crColor
			);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpszDriver"></param>
        /// <param name="lpszDevice"></param>
        /// <param name="lpszOutput"></param>
        /// <param name="lpInitData"></param>
        /// <returns></returns>
		[DllImport("Gdi32")]
		public static extern int CreateDC(
			string lpszDriver,
			string lpszDevice,
			string lpszOutput,
			int lpInitData		//这个参数实际是一个 DEVMODE 结构的指针
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDC"></param>
        /// <param name="hGdiObj"></param>
        /// <returns></returns>
		[DllImport("Gdi32")]
		public static extern int SelectObject(
			int hDC,
			int hGdiObj
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
		[DllImport("Gdi32")]
		public static extern int DeleteObject(
			int hObject
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fnPenStyle"></param>
        /// <param name="nWidth"></param>
        /// <param name="crColor"></param>
        /// <returns></returns>
		[DllImport("Gdi32")]
		public static extern int CreatePen(
			int fnPenStyle,
			int nWidth,
			int crColor
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lplogpen"></param>
        /// <returns></returns>
		[DllImport("Gdi32")]
		public static extern int CreatePenIndirect(
			ref LOGPEN lplogpen
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDC"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="lpPoint"></param>
        /// <returns></returns>
		[DllImport("Gdi32")]
		public static extern bool MoveToEx(
			int hDC,
			int X,
			int Y,
			ref POINT lpPoint
			);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hDC"></param>
        /// <param name="nXEnd"></param>
        /// <param name="nYEnd"></param>
        /// <returns></returns>
		[DllImport("Gdi32")]
		public static extern bool LineTo(
			int hDC,
			int nXEnd,
			int nYEnd
			);

		/// <summary>
        /// 无伸展位图传送
		/// </summary>
		/// <param name="hdcDest"></param>
		/// <param name="nXDest"></param>
		/// <param name="nYDest"></param>
		/// <param name="nWidth"></param>
		/// <param name="nHeight"></param>
		/// <param name="hdcSrc"></param>
		/// <param name="nXsrc"></param>
		/// <param name="nYsrc"></param>
		/// <param name="dwRop"></param>
		/// <returns></returns>
		[DllImport("Gdi32")]
		public static extern bool BitBlt(
			int hdcDest,
			int nXDest,
			int nYDest,
			int nWidth,
			int nHeight,
			int hdcSrc,
			int nXsrc,
			int nYsrc,
			int dwRop		//光栅操作码
			);

		/// <summary>
        /// 获取特定设备的特定信息，例如屏幕象素高度，宽度
		/// </summary>
		/// <param name="hdc"></param>
		/// <param name="nIndex"></param>
		/// <returns></returns>
		[DllImport("Gdi32")]
		public static extern bool GetDeviceCaps(
			int hdc,
			int nIndex
			);

		/// <summary>
        /// 创建一个匹配的内存DC，返回其句柄
		/// </summary>
		/// <param name="hDC"></param>
		/// <returns></returns>
		[DllImport("Gdi32")]
		public static extern int CreateCompatibleDC(
			int hDC		//如果此参数null，则创建于屏幕匹配的内存dc
			);

		/// <summary>
        /// 创建一个与某dc匹配的内存位图，返回句柄
		/// </summary>
		/// <param name="hDC"></param>
		/// <param name="nWidth"></param>
		/// <param name="nHeight"></param>
		/// <returns></returns>
		[DllImport("Gdi32")]
		public static extern int CreateCompatibleBitmap(
			int hDC,
			int nWidth,
			int nHeight
			);




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public extern static IntPtr GetWindow();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="crKey"></param>
        /// <param name="bAlpha"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public extern static bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
        /// <summary>
        /// 
        /// </summary>
        public static uint LWA_COLORKEY = 0x00000001;
        /// <summary>
        /// 
        /// </summary>
        public static uint LWA_ALPHA = 0x00000002;

        #region 阴影效果变量
        /// <summary>
        /// 声明Win32 API
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nIndex"></param>
        /// <param name="dwNewLong"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nIndex"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);
        //[DllImport("user32.dll")]
        //public extern static uint SetWindowLong(IntPtr hwnd, int nIndex, uint dwNewLong);
        //[DllImport("user32.dll")]
        //public extern static uint GetWindowLong(IntPtr hwnd, int nIndex);
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public enum WindowStyle : int
        {
            /// <summary>
            /// 
            /// </summary>
            GWL_EXSTYLE = -20
        }
        /// <summary>
        /// 
        /// </summary>
        public enum ExWindowStyle : uint
        {
            /// <summary>
            /// 
            /// </summary>
            WS_EX_LAYERED = 0x00080000
        }



    }
}

using System;
using System.Runtime.InteropServices;

namespace Import_License
{
	public class WinAPI
	{
		public const int SWP_NOOWNERZORDER = 512;

		public const int SWP_NOREDRAW = 8;

		public const int SWP_NOZORDER = 4;

		public const int SWP_SHOWWINDOW = 64;

		public const int WS_EX_MDICHILD = 64;

		public const int SWP_FRAMECHANGED = 32;

		public const int SWP_NOACTIVATE = 16;

		public const int SWP_ASYNCWINDOWPOS = 16384;

		public const int SWP_NOMOVE = 2;

		public const int SWP_NOSIZE = 1;

		public const int GWL_STYLE = -16;

		public const int WS_VISIBLE = 268435456;

		public const int WM_CLOSE = 16;

		public const int WS_CHILD = 1073741824;

		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		private static extern long GetWindowThreadProcessId(long hWnd, long lpdwProcessId);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
		public static extern long GetWindowLong(IntPtr hwnd, int nIndex);

		[DllImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true)]
		public static extern long SetWindowLong(IntPtr hwnd, int nIndex, long dwNewLong);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

		[DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
		public static extern bool PostMessage(IntPtr hwnd, uint Msg, long wParam, long lParam);
	}
}

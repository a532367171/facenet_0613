using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
namespace FaceCompareThread
{
	public static class INIClass
	{
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
		public static void IniWriteValue(string INIPath, string Section, string Key, string Value)
		{
			INIClass.WritePrivateProfileString(Section, Key, Value, INIPath);
		}
		public static string IniReadValue(string INIPath, string Section, string Key)
		{
			StringBuilder stringBuilder = new StringBuilder(500);
			INIClass.GetPrivateProfileString(Section, Key, "", stringBuilder, 500, INIPath);
			return stringBuilder.ToString();
		}
		public static bool ExistINIFile(string INIPath)
		{
			return File.Exists(INIPath);
		}
	}
}

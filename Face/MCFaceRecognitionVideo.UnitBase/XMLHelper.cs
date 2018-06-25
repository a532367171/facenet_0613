using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
namespace MCFaceRecognitionVideo.UnitBase
{
	public static class XMLHelper
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class Class_c
		{
			public static readonly XMLHelper.Class_c class_c_9 = new XMLHelper.Class_c();
			public static Func<XElement, XElement> class_c_9__1_0;
			internal XElement _getXmlValue_b__1_0(XElement c)
			{
				return c;
			}
		}
		private static string xmlName = "SystemSetting.xml";
		public static string getXmlValue(string xmlElement, string xmlAttribute)
		{
			IEnumerable<XElement> arg_34_0 = XDocument.Load(XMLHelper.xmlName).Descendants(xmlElement);
			Func<XElement, XElement> arg_34_1;
			if ((arg_34_1 = XMLHelper.Class_c.class_c_9__1_0) == null)
			{
				arg_34_1 = (XMLHelper.Class_c.class_c_9__1_0 = new Func<XElement, XElement>(XMLHelper.Class_c.class_c_9._getXmlValue_b__1_0));
			}
			IEnumerable<XElement> arg_3F_0 = arg_34_0.Select(arg_34_1);
			string result = "";
			using (IEnumerator<XElement> enumerator = arg_3F_0.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					result = enumerator.Current.Attribute(xmlAttribute).Value.ToString();
				}
			}
			return result;
		}
		public static void setXmlValue(string xmlElement, string xmlAttribute, string xmlValue)
		{
			XDocument expr_0A = XDocument.Load(XMLHelper.xmlName);
			XElement expr_1A = expr_0A.Element("SystemSetting");
			XElement expr_2C = (expr_1A != null) ? expr_1A.Element(xmlElement) : null;
			if (expr_2C != null)
			{
				expr_2C.Attribute(xmlAttribute).SetValue(xmlValue);
			}
			expr_0A.Save(XMLHelper.xmlName);
		}
	}
}

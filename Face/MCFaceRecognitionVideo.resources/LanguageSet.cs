using System;
using System.Resources;
namespace MCFaceRecognitionVideo.resources
{
	public class LanguageSet
	{
		public static ResourceManager Resource
		{
			get;
			set;
		}
		public LanguageSet(string languageName)
		{
			if (languageName.Equals("EN_US"))
			{
				LanguageSet.Resource = new ResourceManager(typeof(EN_US));
				UnitField.LoadLanguage(LanguageSet.Resource);
				return;
			}
			LanguageSet.Resource = null;
		}
	}
}

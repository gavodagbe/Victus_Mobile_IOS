using System;
using Foundation;

namespace Victus_Mobile_IOS
{
	public class Settings
	{
		public Settings ()
		{
			//Initialisation
		}

		public string getSettings(string key){
			if (NSUserDefaults.StandardUserDefaults[key] == null)
				return "";
			return NSUserDefaults.StandardUserDefaults [key].ToString();
		}

		public void setSettings(string key, string value){
			NSUserDefaults.StandardUserDefaults.SetString (value, key);
		}
	}
}


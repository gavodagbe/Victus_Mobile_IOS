using System;
using System.Net;
using System.IO;
using System.Text;
using Foundation;

namespace Victus_Mobile_IOS
{
	public static class networkManager
	{
	
		//Appel une url
		public static string callURL(string sUrl, string sEncoding){

			//var webClient = new WebClient();
			Console.WriteLine ("netWorkMAnager callURL : " + sUrl);

			//var url = new Uri(sUrl);

			var MyClient = WebRequest.Create(sUrl) as HttpWebRequest;
			MyClient.Method = WebRequestMethods.Http.Get;

			//Récupère la réponse du serveur
			var response = MyClient.GetResponse() as HttpWebResponse;

			//Récup du CFID et CFTOKEN
			if (NSUserDefaults.StandardUserDefaults["CFID"].ToString() == "" ){

				//Parcours les cookies
				for (int i = 0; i < response.Headers.GetValues("Set-Cookie").Length; i++) {

					//Stocke la valeur du cookie
					string val = response.Headers.GetValues ("Set-Cookie") [i];

					//Console.WriteLine (response.Headers.GetValues("Set-Cookie")[i]);

					//CFID
					if (val.StartsWith("CFID=")) {
						//Extrait la valeur du cookie
						val = val.Remove(val.IndexOf(";")).Replace("CFID=","");
						//Stocke la variable dans le global
						NSUserDefaults.StandardUserDefaults.SetString(val,"CFID");
						Console.WriteLine ("CFID = "+val);
					}
					else if (val.StartsWith("CFTOKEN=")) {
						//Extrait la valeur du cookie
						val = val.Remove(val.IndexOf(";")).Replace("CFTOKEN=","");
						//Stocke la variable dans le global
						NSUserDefaults.StandardUserDefaults.SetString(val,"CFTOKEN");
						Console.WriteLine ("CFTOKEN = "+val);
					}
				}
			} //Fin recup CFID et CFTOKEN

			//Récupère le contenu
			StringBuilder sb = new StringBuilder();
			Byte[] buf = new byte[8192];
			int count;
			do
			{
				count = response.GetResponseStream().Read(buf, 0, buf.Length);
				if(count != 0)
				{
					sb.Append(Encoding.UTF8.GetString(buf,0,count)); // just hardcoding UTF8 here
				}
			}while (count > 0);
			String content = sb.ToString();

			Console.WriteLine (content);
			// ************


			return content;

		}
	}
}
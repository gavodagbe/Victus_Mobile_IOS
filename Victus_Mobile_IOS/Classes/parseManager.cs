using System;
using System.Xml;
using System.Json;

namespace Victus_Mobile_IOS
{
	public static class parseManager
	{
		public static XmlReader parseXML (string sXML)
		{
			XmlReader reader = XmlReader.Create(sXML);

			return reader;

		}

		public static JsonValue parseJSON (string sJSON)
		{
			var oJson = JsonValue.Parse(sJSON);

			return oJson;

		}
	}
}


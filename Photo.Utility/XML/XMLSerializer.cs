using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Photo.Utility.XML
{
	/// <summary>
	/// XML Serializer class for the XML access 
	/// </summary>
	public static class XMLSerializer
	{
		#region Public Methods

		/// <summary>
		/// Gets XML generated for the object passed
		/// </summary>
		/// <param name="obj">Object contains data whose parameters to be generated</param>
		/// <returns>String</returns>
		public static string GetXML<T>(T obj) where T : class, new()
		{
			return GetXML<T>(obj, false);
		}

		/// <summary>
		/// Gets XML generated for the object passed
		/// </summary>
		/// <param name="obj">Object contains data whose parameters to be generated</param>
		/// <param name="omitXmlDeclaration">Whether to omit or iunclde the Xml declaration</param>
		/// <returns>String</returns>
		public static string GetXML<T>(T obj, bool omitXmlDeclaration) where T : class, new()
		{
			return GetXML<T>(obj, omitXmlDeclaration, false);
		}

		/// <summary>
		/// Gets XML generated for the object passed
		/// </summary>
		/// <param name="obj">Object contains data whose parameters to be generated</param>
		/// <param name="omitXmlDeclaration">Whether to omit or iunclde the Xml declaration</param>
		/// <param name="omitXmlSerializerNamespaces">Whether to omit or include namespace values</param>
		/// <returns>String</returns>
		public static string GetXML<T>(T obj, bool omitXmlDeclaration, bool omitXmlSerializerNamespaces) where T : class, new()
		{
			return GetXML<T>(obj, omitXmlDeclaration, omitXmlSerializerNamespaces, false);
		}

		/// <summary>
		/// Gets XML generated for the object passed
		/// </summary>
		/// <param name="obj">Object contains data whose parameters to be generated</param>
		/// <param name="omitXmlDeclaration">Whether to omit or iunclde the Xml declaration</param>
		/// <param name="omitXmlSerializerNamespaces">Whether to omit or include namespace values</param>
		/// <param name="useSingleLine">Completely disable indentation so that the result string will consist of one line only</param>
		/// <returns>String</returns>
		public static string GetXML<T>(T obj, bool omitXmlDeclaration, bool omitXmlSerializerNamespaces, bool useSingleLine) where T : class, new()
		{
			XmlSerializer xs = new XmlSerializer(typeof(T));
			XmlWriterSettings writerSettings = new XmlWriterSettings();

			string finalXml = string.Empty;

			if (omitXmlDeclaration)
				writerSettings.OmitXmlDeclaration = true;

			writerSettings.NewLineHandling = useSingleLine ? NewLineHandling.None : NewLineHandling.Replace;
			//writerSettings.NewLineOnAttributes = !useSingleLine;
			writerSettings.Indent = !useSingleLine;

			if(writerSettings.Indent)
				writerSettings.IndentChars = "\t";

			using (StringWriter stringWriter = new StringWriter())
			{
				XmlWriter xmlWriter = XmlWriter.Create(stringWriter, writerSettings);

				if (!omitXmlSerializerNamespaces)
				{
					xs.Serialize(xmlWriter, obj);
				}
				else
				{
					XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
					ns.Add("", "");
					xs.Serialize(xmlWriter, obj, ns);
				}

				finalXml = stringWriter.ToString();
				stringWriter.Close();
			}

			return finalXml;
		}

		/// <summary>
		/// Returns object from response xml 
		/// </summary>
		/// <param name="xml">XML string</param>
		/// <returns>T</returns>
		public static T GetObject<T>(string xml) where T : class
		{
			if (string.IsNullOrEmpty(xml))
				return null;

			StringReader stringReader = new StringReader(xml);
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			Object obj = xmlSerializer.Deserialize(stringReader);
			stringReader.Close();
			stringReader.Dispose();
			return (T)obj;
		}

		#endregion
	}
}
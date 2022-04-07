using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace AGDataUITest.Repository
{
	public class CareersPage
	{
		static XmlDocument xmlDocument;

		public CareersPage()
		{
			try
			{
				xmlDocument = new XmlDocument();
				string xmlDocPath = Path.Combine(Config.XPathDirPath, "CareersPage.xml");
				xmlDocument.Load(xmlDocPath);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public string OpenPositionsFrame
		{
			get { return RepositoryCommon.FetchXpathValue("OpenPositionsFrame", xmlDocument); }
		}

		public string OpenPositionItem
		{
			get { return RepositoryCommon.FetchXpathValue("OpenPositionItem", xmlDocument); }
		}

		/// <summary>
		/// %1 shall be replaced with required value before using XPath
		/// </summary>
		public string OpenPositionWithRequiredTitle
		{
			get { return RepositoryCommon.FetchXpathValue("OpenPositionWithRequiredTitle", xmlDocument); }
		}
		
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace AGDataUITest.Repository
{
	public class HomePage
	{
		static XmlDocument xmlDocument;

		public HomePage()
		{
			try
			{
				xmlDocument = new XmlDocument();
				string xmlDocPath = Path.Combine(Config.XPathDirPath, "HomePage.xml");
				xmlDocument.Load(xmlDocPath);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public string AGDataHome
		{
			get { return RepositoryCommon.FetchXpathValue("AGDataHome", xmlDocument); }
		}

		public string PageContent
		{
			get { return RepositoryCommon.FetchXpathValue("PageContent", xmlDocument); }
		}

		/// <summary>
		/// %1 shall be replaced with required value before using XPath
		/// </summary>
		public string MainMenu
		{
			get { return RepositoryCommon.FetchXpathValue("MainMenu", xmlDocument); }
		}

		/// <summary>
		/// %1 shall be replaced with required value before using XPath
		/// </summary>
		public string SubMenu
		{
			get { return RepositoryCommon.FetchXpathValue("SubMenu", xmlDocument); }
		}
	}
}

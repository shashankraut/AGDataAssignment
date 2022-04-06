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

		public string MenuCompany
		{
			get { return RepositoryCommon.FetchXpathValue("MenuCompany", xmlDocument); }
		}

		public string SubmenuCareers
		{
			get { return RepositoryCommon.FetchXpathValue("SubmenuCareers", xmlDocument); }
		}
	}
}

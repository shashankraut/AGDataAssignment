using System;
using System.Xml;

namespace AGDataUITest.Repository
{
	public class RepositoryCommon
	{
		/// <summary>
		/// Method to fetch xpath value from xml document on basis of key specified
		/// </summary>
		/// <param name="xpathKey">Element key for which Xpath needs to be fetched</param>
		/// <param name="xmlDoc">XML document</param>
		/// <returns>Returns XPath value for a specified key</returns>
		public static string FetchXpathValue(string xpathKey, XmlDocument xmlDoc)
		{
			string xpathValue = string.Empty;
			try
			{

				if (xmlDoc == null)
					throw new System.ArgumentNullException("xmlDoc", "Invalid xml document passed for fetching xpath");

				XmlNode oSelectedNode = xmlDoc.SelectSingleNode("/ElementXPaths/Elements");
				XmlNodeList oSelectedNodeList = oSelectedNode.SelectNodes("Element");
				foreach (XmlNode oNode in oSelectedNodeList)
				{
					if (string.Compare(oNode.Attributes.GetNamedItem("key").Value, xpathKey) == 0)
					{
						xpathValue = oNode.InnerText;
						break;
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return xpathValue;
		}
	}
}

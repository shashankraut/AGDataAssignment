using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace AGDataUITest
{
	public class Config
	{
		private static string screenshotPath = string.Empty;
		private static Uri applicationUri;
		private static string xpathDirPath = string.Empty;

		public static string ScreenshotPath
		{
			get
			{
				if (string.IsNullOrEmpty(screenshotPath))
				{
					string tempStringval = ConfigurationManager.AppSettings["ScreenshotPath"];
					if (!string.IsNullOrEmpty(tempStringval))
						screenshotPath = tempStringval;
					else
						screenshotPath = @"C:\UItest_Screenshot";
				}

				return screenshotPath;
			}
		}

		public static Uri ApplicationUri
		{
			get
			{
				if (applicationUri == null)
				{
					string tempStringVal = ConfigurationManager.AppSettings["ApplicationURL"];
					if (!string.IsNullOrEmpty(tempStringVal))
						applicationUri = new Uri(tempStringVal);
					else
						applicationUri = new Uri("https://www.agdata.com/");
				}
				return applicationUri;
			}
		}

		public static string XPathDirPath
		{
			get
			{
				if (string.IsNullOrEmpty(xpathDirPath))
				{
					string tempStringval = Path.Combine(Environment.CurrentDirectory, "XPaths");
					xpathDirPath = tempStringval;
				}
				return xpathDirPath;
			}
		}
	}
}

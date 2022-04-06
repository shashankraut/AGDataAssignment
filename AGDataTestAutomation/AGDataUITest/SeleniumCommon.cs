using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.IO;
using OpenQA.Selenium.Interactions;
using AventStack.ExtentReports;
using TestReporting;

namespace AGDataUITest
{
	public class SeleniumCommon
	{

		public IWebDriver driver;
		private WebDriverWait explicitWait;

		public SeleniumCommon(IWebDriver driver)
		{
			this.driver = driver;

			explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
		}

		/// <summary>
		/// Method to take screenshots on failure
		/// </summary>
		/// <param name="fileName">File name to store</param>
		public void TakeScreenshot(string fileName)
		{
			try
			{
				if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(fileName, "Invalid param filename");
				string m_sScreenshotFileLocation = Config.ScreenshotPath;
				if (!Directory.Exists(m_sScreenshotFileLocation)) Directory.CreateDirectory(m_sScreenshotFileLocation);

				ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
				Screenshot screenshot = screenshotDriver.GetScreenshot();
				fileName = DateTime.Now.ToShortDateString() + DateTime.Now.ToString("-HH-mm-") + " " + fileName;
				fileName = fileName.Replace(":", "").Replace("\\", "").Replace("/", "").Replace("|", "").Replace("\\\\", "").Replace("*", "").Replace("<", "").Replace(">", "").Replace("\"", "").Replace("?", "");
				screenshot.SaveAsFile(m_sScreenshotFileLocation + fileName + ".png", ScreenshotImageFormat.Png);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// To throw assestion failure in case of failure and taking screenshot 
		/// </summary>
		/// <param name="failureMesage">Failure message</param>
		/// <param name="screenshotFileName">File name for screenshot [Can be ignored if dont want to take screenshot for failure]</param>
		public void LogFailure(string failureMesage, string screenshotFileName = "")
		{
			if (!string.IsNullOrEmpty(screenshotFileName)) TakeScreenshot(screenshotFileName);
			Assert.Fail(failureMesage);
			TestReporting.Reporting.Log(Status.Fail, failureMesage);
		}

		/// <summary>
		/// To get control using XPath with explicit wait 
		/// </summary>
		/// <param name="xpath">XPath of the control</param>
		/// <param name="errorMessage">Error message in case control is not found with specified xpath, can also be empty if dont want to raise excepton if control is not available</param>
		/// <returns>Instance of WebElement</returns>
		public IWebElement WaitForElementToPresent(string xpath, string errorMessage = "")
		{
			IWebElement element = null;
			try
			{
				element = explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(xpath)));
			}
			catch { }
			if (!string.IsNullOrEmpty(errorMessage))
				if (element == null) LogFailure(errorMessage, xpath);

			return element;
		}

		/// <summary>
		/// To get control using specified xpath with required explicit wait
		/// </summary>
		/// <param name="xpath">Xpath of the control</param>
		/// <param name="waitTime">Wait time in second to be used for explicit wait</param>
		/// <param name="errorMessage">Error message in case control is not found, can also be empty if dont want to raise excepton if control is not available</param>
		/// <returns>Instance of WebElement</returns>
		public IWebElement WaitForElementToPresent(string xpath, int waitTime, string errorMessage = "")
		{
			IWebElement element = null;

			explicitWait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
			try
			{
				element = explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(xpath)));
			}
			catch { }
			if (!string.IsNullOrEmpty(errorMessage))
				if (element == null) LogFailure(errorMessage, xpath);

			return element;
		}


		/// <summary>
		/// To get collection of controls maching with specified xpath
		/// </summary>
		/// <param name="xpath">Xpath to get collection of controls</param>
		/// <param name="errorMessage">Error message in case control is not found with specified xpath</param>
		/// <returns>Instance of readonly collection of elements</returns>
		public IReadOnlyCollection<IWebElement> WaitForElementsToPresent(string xpath, string errorMessage = "")
		{
			IReadOnlyCollection<IWebElement> elementsColl = null;
			try
			{
				elementsColl = explicitWait.Until(d => driver.FindElements(By.XPath(xpath)));
			}
			catch { }
			if (!string.IsNullOrEmpty(errorMessage))
				if (elementsColl.Count == 0) LogFailure(errorMessage, xpath);

			return elementsColl;
		}

		/// <summary>
		/// To navigate to specific URI for already launched browser instance
		/// </summary>
		/// <param name="navigateUri">URI to navigate</param>
		public void NavigateTo(Uri navigateUri)
		{
			driver.Navigate().GoToUrl(navigateUri);
		}

		public void MouseHover(IWebElement element)
		{
			Actions mouseAction = new Actions(driver);
			mouseAction.MoveToElement(element, 1, 1).Build().Perform();
		}

		/// <summary>
		/// To switch control to specific frame
		/// </summary>
		/// <param name="frameElement">Frame web element</param>
		public void SwitchToFrame(IWebElement frameElement)
		{
			driver.SwitchTo().Frame(frameElement);
		}

		/// <summary>
		/// To get the control in viewport
		/// </summary>
		/// <param name="webElement">Element</param>
		public void ScrollToViewElement(IWebElement webElement)
		{
			try
			{
				string elementId = webElement.GetAttribute("id");
				IJavaScriptExecutor js = driver as IJavaScriptExecutor;
				js.ExecuteScript("arguments[0].scrollIntoView(true);", webElement);
				Reporting.Log(Status.Info, $"Scrolled to element {elementId}");
			}
			catch (Exception ex)
			{
				LogFailure("Exception occurred in ScrollToViewElement in : " + ex.Message, "ScrollToViewElementError");
			}
		}
	}
}

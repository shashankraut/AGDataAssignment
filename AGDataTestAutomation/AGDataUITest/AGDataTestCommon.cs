using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using AGDataUITest.Repository;
using TestReporting;
using AventStack.ExtentReports;

namespace AGDataUITest
{
	public class AGDataTestCommon
	{
		private IWebDriver driver;
		private SeleniumCommon seleniumCommon;
		private HomePage homePage;

		public AGDataTestCommon(IWebDriver webDriver)
		{
			driver = webDriver;
			seleniumCommon = new SeleniumCommon(driver);
			homePage = new HomePage();
		}

		/// <summary>
		/// To navigate to specific sub menu from a main menu
		/// </summary>
		/// <param name="mainMenu">MainMenu</param>
		/// <param name="subMenu">SubMenu</param>
		public void NavigateToMenu(MainMenu mainMenu, SubMenu subMenu)
		{
			try
			{
				IWebElement mainMenuEle = seleniumCommon.WaitForElementToPresent(homePage.MainMenu.Replace("%1", mainMenu.ToString()), $"Main menu {mainMenu} not found");
				seleniumCommon.MouseHover(mainMenuEle);
				IWebElement subMenuEle = seleniumCommon.WaitForElementToPresent(homePage.SubMenu.Replace("%1", subMenu.ToString()), $"Sub menu {subMenu} not found");
				subMenuEle.Click();
				Reporting.Log(Status.Info, $"Control switched to {mainMenu} -> {subMenu} menu");
			}
			catch (Exception ex)
			{
				seleniumCommon.LogFailure($"Exception occurred in Navigating to menu : {ex.Message}");
			}
		}
	}
}

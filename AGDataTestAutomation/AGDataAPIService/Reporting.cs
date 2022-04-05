using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;

namespace AGDataAPIService
{
	public static class Reporting
	{
		#region Member Declarations
		public static ExtentReports extentReports;
		public static ExtentTest testCase;
		public static ExtentHtmlReporter htmlReporter;
		#endregion

		/// <summary>
		/// To configure and setup extent report
		/// </summary>
		/// <param name="reportName">Report name</param>
		/// <param name="reportTitle">Report title</param>
		/// <param name="reportPath">Path to store report</param>
		public static void SetupExtentReport(string reportName, string reportTitle, dynamic reportPath)
		{
			htmlReporter = new ExtentHtmlReporter(reportPath);
			htmlReporter.Config.Theme = Theme.Standard;
			htmlReporter.Config.DocumentTitle = reportTitle;
			htmlReporter.Config.ReportName = reportName;

			extentReports = new ExtentReports();
			extentReports.AttachReporter(htmlReporter);
		}

		/// <summary>
		/// To create test case with extent report instance
		/// </summary>
		/// <param name="testName">Test case name</param>
		public static void CreateTest(string testName)
		{
			testCase = extentReports.CreateTest(testName);
		}

		/// <summary>
		/// To log messages to extent report for a specific test case
		/// </summary>
		/// <param name="status">Status instance</param>
		/// <param name="logMessage">Message to log</param>
		public static void Log(Status status, string logMessage)
		{
			testCase.Log(status, logMessage);
		}

		/// <summary>
		/// To update test case status
		/// </summary>
		/// <param name="status">Test case status</param>
		public static void TestStatus(string status)
		{
			if (status.ToUpper().Equals("PASS"))
				testCase.Pass("Test case is passed!");
			else
				testCase.Fail("Test case is failed!");
		}

		/// <summary>
		/// To flush the extent report and finalize
		/// </summary>
		public static void Flush()
		{
			extentReports.Flush();
		}
	}
}

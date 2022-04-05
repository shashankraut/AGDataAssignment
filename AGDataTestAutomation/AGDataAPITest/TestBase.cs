using AGDataAPIService;
using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AGDataAPITest
{
	public class TestBase
	{
		public static TestContext TestContext { get; set; }

		[OneTimeSetUp]
		public static void FixtureSetup()
		{
			var directory = TestContext.CurrentContext.TestDirectory;
			Reporting.SetupExtentReport("AGData API test", "API Test report", directory);
		}

		[SetUp]
		public void TestSetupTest()
		{
			Reporting.CreateTest(TestContext.CurrentContext.Test.Name);
		}

		[TearDown]
		public static void TestTearDown()
		{
			var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
			Status logStatus = Status.Info;

			switch (testStatus)
			{
				case TestStatus.Passed:
					logStatus = Status.Pass;
					
					break;
				case TestStatus.Failed:
					logStatus = Status.Fail;
					break;
			}
			Reporting.TestStatus(logStatus.ToString());
		}

		[OneTimeTearDown]
		public static void FixtureTearDown()
		{
			Reporting.Flush();
		}
	}
}

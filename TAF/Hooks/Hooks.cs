using EpamAutomationTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using TechTalk.SpecFlow;

namespace EpamAutomationTests.Hooks
{
    [Binding]
    public class Hooks
    {
        private static IWebDriver _driver;
        private static WebDriverWait _wait;
        public TestContext TestContext { get; set; }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Logger.Info("Starting test run");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Logger.Info("Test run completed");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Logger.Info($"Starting scenario: {ScenarioContext.Current.ScenarioInfo.Title}");
            _driver = BrowserFactory.GetDriver("chrome");
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            ScenarioContext.Current["Driver"] = _driver;
            ScenarioContext.Current["Wait"] = _wait;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Logger.Info($"Scenario completed: {ScenarioContext.Current.ScenarioInfo.Title}");

            // Take screenshot if the scenario failed
            if (ScenarioContext.Current.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
            {
                TakeScreenshot();
            }

            _driver?.Quit();
        }

        private void TakeScreenshot()
        {
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            var screenshotsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");

            if (!Directory.Exists(screenshotsFolder))
            {
                Directory.CreateDirectory(screenshotsFolder);
            }

            var fileName = $"{ScenarioContext.Current.ScenarioInfo.Title}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            var fullPath = Path.Combine(screenshotsFolder, fileName);

            screenshot.SaveAsFile(fullPath);
        }
    }
}
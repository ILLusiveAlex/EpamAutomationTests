using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace EpamAutomationTests.Core
{
    public abstract class BaseTest
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        [TestInitialize]
        public void Setup()
        {
            Driver = BrowserFactory.GetDriver("chrome");
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        [TestCleanup]
        public void TearDown()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                TakeScreenshot();
            }
            Driver.Quit();
        }

        private void TakeScreenshot()
        {
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            var fileName = $"{TestContext.TestName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            screenshot.SaveAsFile(fileName);
        }

        public TestContext TestContext { get; set; }
    }
}
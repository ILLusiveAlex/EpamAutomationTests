using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using EpamAutomationTests.Pages;

namespace EpamAutomationTests.Core
{
    public abstract class BaseTest
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        protected void WaitClick(By locator)
        {
            Wait.Until(driver => driver.FindElement(locator)).Click();
        }

        protected void Click(By locator)
        {
            Driver.FindElement(locator).Click();
        }

        protected void ClickApplyButtonOfLatestJob()
        {
            var latestJob = Driver.FindElements(By.ClassName("search-result__item")).Last();
            latestJob.FindElement(By.CssSelector("div.search-result__item-controls a.search-result__item-apply-23")).Click();
        }


        protected string BaseUrl => Constants.BaseUrl;
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

            var screenshotsFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");

            if (!Directory.Exists(screenshotsFolder))
            {
                Directory.CreateDirectory(screenshotsFolder);
            }

            var fileName = $"{TestContext.TestName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            var fullPath = Path.Combine(screenshotsFolder, fileName);

            screenshot.SaveAsFile(fullPath);
        }

        public TestContext TestContext { get; set; }
    }
}
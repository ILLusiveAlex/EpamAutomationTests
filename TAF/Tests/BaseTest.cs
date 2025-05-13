using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Configuration;
using EpamAutomationTests.Pages;

namespace EpamAutomationTests.Core
{
    public abstract class BaseTest
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;
        
        public TestContext TestContext { get; set; }

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
            var browser = TestContext.Properties["Browser"]?.ToString() ?? "chrome";
            var implicitWait = int.Parse(TestContext.Properties["ImplicitWait"]?.ToString() ?? "30");
            var pageLoadTimeout = int.Parse(TestContext.Properties["PageLoadTimeout"]?.ToString() ?? "60");
            var scriptTimeout = int.Parse(TestContext.Properties["ScriptTimeout"]?.ToString() ?? "60");
            var acceptCookies = bool.Parse(TestContext.Properties["AcceptCookies"]?.ToString() ?? "true");

            Driver = BrowserFactory.GetDriver(browser);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWait);
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadTimeout);
            Driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(scriptTimeout);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(implicitWait));

            // Navigate to base URL
            Driver.Navigate().GoToUrl(Constants.BaseUrl);
            
            // Wait for page to be fully loaded
            Wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            
            // Handle cookie consent if needed
            if (acceptCookies)
            {
                try
                {
                    var cookieConsent = Wait.Until(d => d.FindElement(By.Id("onetrust-accept-btn-handler")));
                    cookieConsent.Click();
                    // Wait for cookie banner to disappear
                    Wait.Until(d => !d.FindElements(By.Id("onetrust-accept-btn-handler")).Any());
                }
                catch (WebDriverTimeoutException)
                {
                    // Cookie consent not found, continue
                }
            }
            
            // Wait for any loading overlays to disappear
            try
            {
                Wait.Until(d => !d.FindElements(By.CssSelector(".loading-overlay")).Any());
            }
            catch (WebDriverTimeoutException)
            {
                // No loading overlay found, continue
            }
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
            try
            {
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
                var fileName = $"{TestContext.FullyQualifiedTestClassName}_{TestContext.TestName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                screenshot.SaveAsFile($"{TestContext.TestResultsDirectory}\\{fileName}");
                TestContext.AddResultFile($"{TestContext.TestResultsDirectory}\\{fileName}");
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to take screenshot: {ex.Message}");
            }
        }
    }
}
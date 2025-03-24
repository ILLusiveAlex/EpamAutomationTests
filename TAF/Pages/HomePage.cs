using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using EpamAutomationTests.Core;

namespace EpamAutomationTests.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public HomePage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        public void NavigateToAbout()
        {
            Logger.Info("Navigating to About page.");
            _driver.FindElement(By.LinkText("About")).Click();
        }

        public void NavigateToInsights()
        {
            Logger.Info("Navigating to Insights page.");
            _driver.FindElement(By.LinkText("Insights")).Click();
        }

        public void ScrollToEPAMAtAGlance()
        {
            Logger.Info("Scrolling to EPAM At A Glance section.");
            var element = _driver.FindElement(By.CssSelector(".button__content--desktop"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public IWebElement DownloadButton => _driver.FindElement(By.XPath("//span[contains(@class, 'button__content') and contains(text(), 'DOWNLOAD')]"));
    }
}
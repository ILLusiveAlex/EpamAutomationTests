using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
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

        public void NavigateToHomePage()
        {
            Logger.Info("Navigating to EPAM homepage");
            _driver.Navigate().GoToUrl(Constants.BaseUrl);
        }

        // Navigate to "About" section
        public void NavigateToAbout()
        {
            Logger.Info("Navigating to About page.");
            _driver.FindElement(By.LinkText("About")).Click();
        }

        // Navigate to "Insights" section
        public void NavigateToInsights()
        {
            Logger.Info("Navigating to Insights page.");
            _driver.FindElement(By.LinkText("Insights")).Click();
        }

        // Scroll to "EPAM at a Glance" section
        public void ScrollToEPAMAtAGlance()
        {
            Logger.Info("Scrolling to 'EPAM at a Glance' section.");
            var element = _wait.Until(d => d.FindElement(By.CssSelector(".button__content--desktop")));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        // Click the Download button
        public void ClickDownloadButton()
        {
            Logger.Info("Clicking Download button.");
            _wait.Until(d => d.FindElement(By.XPath("//span[contains(@class, 'button__content') and contains(text(), 'DOWNLOAD')]"))).Click();
        }

        // Swipe carousel (for Test Case #4)
        public void SwipeCarousel(int times)
        {
            Logger.Info($"Swiping carousel {times} times.");
            var nextButton = _wait.Until(d => d.FindElement(By.CssSelector("button.slider__right-arrow.slider-navigation-arrow")));
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollBy(0, 160);");
            for (int i = 0; i < times; i++)
            {
                nextButton.Click();
                System.Threading.Thread.Sleep(1000); // Pause between swipes
            }
        }

        // Get article title from carousel
        public string GetArticleTitleFromCarousel()
        {
            Logger.Info("Getting article title from carousel.");
            // First scroll to make sure the carousel is in view
            ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollBy(0, 160);");
            
            // Wait for the carousel to be visible and get the title
            var titleElement = _wait.Until(d => {
                var element = d.FindElement(By.CssSelector("span.font-size-60 > span.museo-sans-light:first-child + span.rte-text-gradient > span.museo-sans-700.gradient-text + span.museo-sans-light\r\n"));
                return element.Displayed ? element : null;
            });
            
            return titleElement.Text;
        }

        // Click "Read More" button
        public void ClickReadMore()
        {
            Logger.Info("Clicking 'Read More' button.");
            var readMoreButton = _wait.Until(d => d.FindElement(By.CssSelector("a.slider-cta-link")));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", readMoreButton);
        }
    }
}
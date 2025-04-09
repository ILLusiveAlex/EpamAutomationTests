using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using EpamAutomationTests.Core;
using OpenQA.Selenium.Interactions;

namespace EpamAutomationTests.Pages
{
    public class ServicesPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // Locators
        private readonly By _servicesLink = By.LinkText("Services");
        private readonly By _serviceCategoryDropdown = By.CssSelector(".top-navigation__item-link[href*='services']");
        private readonly By _pageTitle = By.CssSelector("h1, .text-ui-23 .museo-sans-500.gradient-text, .text-ui-23 .museo-sans-light");
        private readonly By _relatedExpertiseSection = By.XPath("//div[contains(@class, 'text-ui-23')]//span[contains(text(), 'Our Related Expertise')]");

        public ServicesPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
        }

        public void NavigateToServices()
        {
            Logger.Info("Hovering over Services link");
            var servicesElement = _wait.Until(d => d.FindElement(_servicesLink));
            var actions = new Actions(_driver);
            actions.MoveToElement(servicesElement).Perform();
           
        }

        public void SelectServiceCategory(string category)
        {
            Logger.Info($"Selecting service category: {category}");         
               
                var categoryLocator = By.XPath($"//a[contains(@href, 'services') and contains(text(), '{category}')]");
                var categoryElement = _wait.Until(d => d.FindElement(categoryLocator));

                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", categoryElement);             
        }

        public string GetPageTitle()
        {
            Logger.Info("Getting page title");
            return _wait.Until(d => d.FindElement(_pageTitle)).Text;
        }

        public void ScrollToRelatedExpertiseSection()
        {
            Logger.Info("Scrolling to 'Our Related Expertise' section");
            
                // First try to find the section by its text
                var expertiseSection = _wait.Until(d => d.FindElement(_relatedExpertiseSection));
                
                // Scroll to the parent section element
                var sectionElement = expertiseSection.FindElement(By.XPath("./ancestor::section[contains(@class, 'section-ui')]"));
                
                // Scroll the section into view
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", sectionElement);
                System.Threading.Thread.Sleep(1000); // Give time for scrolling
            
        }

        public bool IsRelatedExpertiseSectionDisplayed()
        {
            Logger.Info("Checking if 'Our Related Expertise' section is displayed");
           
                // First scroll to the section
                ScrollToRelatedExpertiseSection();
                
                // Then check if it's displayed
                return _wait.Until(d => d.FindElement(_relatedExpertiseSection)).Displayed;
            
        }
    }
} 
using EpamAutomationTests.Core;
using EpamAutomationTests.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;

namespace EpamAutomationTests.StepDefinitions
{
    [Binding]
    public class ServicesNavigationSteps
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly HomePage _homePage;
        private readonly ServicesPage _servicesPage;
        private string _pageTitle;

        public ServicesNavigationSteps(ScenarioContext scenarioContext)
        {
            _driver = (IWebDriver)scenarioContext["Driver"];
            _wait = (WebDriverWait)scenarioContext["Wait"];
            _homePage = new HomePage(_driver, _wait);
            _servicesPage = new ServicesPage(_driver, _wait);
        }

        [Given(@"I am on the EPAM homepage")]
        public void GivenIAmOnTheEPAMHomepage()
        {
            Logger.Info("Navigating to EPAM homepage");
            _homePage.NavigateToHomePage();
        }

        [When(@"I hover over the ""(.*)"" link in the main navigation menu")]
        public void WhenIHoverOverTheLinkInTheMainNavigationMenu(string linkText)
        {
            Logger.Info($"Hovering over {linkText} link in the main navigation menu");
            _servicesPage.NavigateToServices();
        }

        [When(@"I select ""(.*)"" from the dropdown")]
        public void WhenISelectFromTheDropdown(string category)
        {
            Logger.Info($"Selecting {category} from the dropdown");
            _servicesPage.SelectServiceCategory(category);
            _pageTitle = _servicesPage.GetPageTitle();
        }

        [Then(@"I should see the correct page title")]
        public void ThenIShouldSeeTheCorrectPageTitle()
        {
            Logger.Info($"Verifying page title: {_pageTitle}");
            Assert.IsNotNull(_pageTitle, "Page title should not be null");
            Assert.IsTrue(_pageTitle.Length > 0, "Page title should not be empty");
            Assert.IsTrue(_pageTitle != "Unknown", "Page title should not be 'Unknown'");
        }

        [Then(@"I should see the ""(.*)"" section on the page")]
        public void ThenIShouldSeeTheSectionOnThePage(string sectionName)
        {
            Logger.Info($"Verifying that {sectionName} section is displayed on the page");
            
            // First scroll to the section
            _servicesPage.ScrollToRelatedExpertiseSection();
            
            // Then check if it's displayed
            Assert.IsTrue(_servicesPage.IsRelatedExpertiseSectionDisplayed(), 
                $"The {sectionName} section should be displayed on the page");
        }
    }
} 
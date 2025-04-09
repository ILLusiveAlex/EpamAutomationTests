using EpamAutomationTests.Core;
using EpamAutomationTests.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.IO;
using System.Linq;


namespace EpamAutomationTests.Tests.Core
{
    [TestClass]
    public class EpamTests : BaseTest
    {
        [TestMethod]
        [DataRow("Java")]
        [DataRow("C#")]
        public void ValidateJobSearch(string keyword)
        {
            var careersPage = new CareersPage(Driver, Wait);

            Logger.Info($"Starting job search test for keyword: {keyword}");
            Driver.Navigate().GoToUrl(Constants.BaseUrl);

            WaitClick(By.LinkText("Careers"));

            var keywordField = Wait.Until(driver => driver.FindElement(careersPage.KeywordField));
            keywordField.Clear();
            keywordField.SendKeys(keyword);

            Click(careersPage.LocationFilter);
            Click(careersPage.AllLocations);
            Click(careersPage.FilterLabel);
            Click(careersPage.SearchButton);

            var latestJob = Wait.Until(d => d.FindElements(careersPage.SearchResults)).Last();
            latestJob.FindElement(careersPage.ApplyButton).Click();

            Assert.IsTrue(Driver.PageSource.Contains(keyword), "The job description does not contain the expected keyword.");
            Logger.Info("Job search test completed successfully.");
        }

        [TestMethod]
        [DataRow("BLOCKCHAIN")]
        [DataRow("Cloud")]
        [DataRow("Automation")]
        public void ValidateGlobalSearch(string searchTerm)
        {
            var globalSearchPage = new GlobalSearchPage(Driver, Wait);

            Logger.Info($"Starting global search test for term: {searchTerm}");
            Driver.Navigate().GoToUrl(Constants.BaseUrl);

            WaitClick(By.ClassName("header-search__button"));

            var searchInput = Wait.Until(driver => driver.FindElement(globalSearchPage.SearchInput));
            searchInput.Clear();
            searchInput.SendKeys(searchTerm);

            Click(globalSearchPage.SearchButton);

            var searchResults = Wait.Until(driver => driver.FindElements(globalSearchPage.SearchResults))
                                    .Select(result => result.Text.ToLower())
                                    .ToList();

            Assert.IsTrue(searchResults.All(text => text.Contains(searchTerm.ToLower())), "Not all results contain the expected term.");
            Logger.Info("Global search test completed successfully.");
        }


        [TestMethod]
        [DataRow("EPAM_Corporate_Overview_Q4FY-2024.pdf")]
        public void ValidateFileDownload(string expectedFileName)
        {
            Logger.Info($"Starting file download test for: {expectedFileName}");
            var homePage = new HomePage(Driver, Wait);

            Driver.Navigate().GoToUrl(Constants.BaseUrl);
            homePage.NavigateToAbout();
            homePage.ScrollToEPAMAtAGlance();
            homePage.ClickDownloadButton();

            var downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", expectedFileName);
            var timeout = DateTime.Now.AddSeconds(30);
            while (!File.Exists(downloadPath) && DateTime.Now < timeout)
            {
                Thread.Sleep(1000);
            }

            Assert.IsTrue(File.Exists(downloadPath), $"File {expectedFileName} was not downloaded.");
            Logger.Info("File download test completed successfully.");
        }

        [TestMethod]
        public void ValidateArticleTitleInCarousel()
        {
            Logger.Info("Starting carousel article title validation test.");
            var homePage = new HomePage(Driver, Wait);

            Driver.Navigate().GoToUrl(Constants.BaseUrl);
            homePage.NavigateToInsights();
            homePage.SwipeCarousel(2); // Swipe twice
            var expectedTitle = homePage.GetArticleTitleFromCarousel();
            homePage.ClickReadMore();

            var actualTitle = Wait.Until(d => d.FindElement(By.CssSelector("span.museo-sans-light"))).Text;
            Assert.AreEqual(expectedTitle, actualTitle, "Article title mismatch.");
            Logger.Info("Carousel test completed successfully.");
        }
    }
}
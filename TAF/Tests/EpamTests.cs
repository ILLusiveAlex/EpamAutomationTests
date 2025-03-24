using EpamAutomationTests.Core;
using EpamAutomationTests.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.IO;
using System.Linq;

namespace EpamAutomationTests.Tests
{
    [TestClass]
    public class EpamTests : BaseTest
    {
        [TestMethod]
        [DataRow("Java")]
        [DataRow("C#")]
        public void ValidateJobSearch(string keyword)
        {
            Logger.Info($"Starting job search test for keyword: {keyword}");
            Driver.Navigate().GoToUrl("https://www.epam.com/");

            Wait.Until(d => d.FindElement(By.LinkText("Careers"))).Click();

            var keywordField = Wait.Until(driver => driver.FindElement(By.Id("new_form_job_search-keyword")));
            keywordField.Clear();
            keywordField.SendKeys(keyword);

            Driver.FindElement(By.CssSelector("div.recruiting-search__location")).Click();
            Driver.FindElement(By.XPath("//li[contains(text(), 'All Locations')]")).Click();

            Driver.FindElement(By.CssSelector("label.recruiting-search__filter-label-23")).Click();

            Driver.FindElement(By.ClassName("job-search-button-transparent-23"))?.Click();

            var latestJob = Wait.Until(d => d.FindElements(By.ClassName("search-result__item"))).Last();
            latestJob.FindElement(By.CssSelector("div.search-result__item-controls a.search-result__item-apply-23")).Click();

            Assert.IsTrue(Driver.PageSource.Contains(keyword), "The job description does not contain the expected keyword.");
            Logger.Info("Job search test completed successfully.");
        }

        [TestMethod]
        [DataRow("BLOCKCHAIN")]
        [DataRow("Cloud")]
        [DataRow("Automation")]
        public void ValidateGlobalSearch(string searchTerm)
        {
            Logger.Info($"Starting global search test for term: {searchTerm}");
            Driver.Navigate().GoToUrl("https://www.epam.com/");

            Driver.FindElement(By.ClassName("header-search__button"))?.Click();

            var searchInput = Driver.FindElement(By.TagName("input"));
            searchInput.Clear();
            searchInput.SendKeys(searchTerm);

            Driver.FindElement(By.CssSelector("span.bth-text-layer"))?.Click();

            var results = Wait.Until(d => d.FindElements(By.XPath("//li[@class='search-results__item']/a")))
                            .Select(e => e.Text.ToLower())
                            .ToList();

            Assert.IsTrue(results.All(text => text.Contains(searchTerm.ToLower())), "Not all results contain the expected term.");
            Logger.Info("Global search test completed successfully.");
        }
    }
}
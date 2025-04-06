using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace EpamAutomationTests.Pages
{
    public class CareersPage : HomePage
    {
        public CareersPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public By KeywordField => By.Id("new_form_job_search-keyword");
        public By LocationFilter => By.CssSelector("div.recruiting-search__location");
        public By AllLocations => By.XPath("//li[contains(text(), 'All Locations')]");
        public By FilterLabel => By.CssSelector("label.recruiting-search__filter-label-23");
        public By SearchButton => By.ClassName("job-search-button-transparent-23");
        public By SearchResults => By.ClassName("search-result__item");
        public By ApplyButton => By.CssSelector("div.search-result__item-controls a.search-result__item-apply-23");
    }
}

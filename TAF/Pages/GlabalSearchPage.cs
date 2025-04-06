using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using EpamAutomationTests.Core;

namespace EpamAutomationTests.Pages
{
    public class GlobalSearchPage : HomePage
    {
        public GlobalSearchPage(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }

        public By SearchButton => By.ClassName("header-search__button");
        public By SearchInput => By.TagName("input");
        public By SearchSubmitButton => By.CssSelector("span.bth-text-layer");
        public By SearchResults => By.XPath("//li[@class='search-results__item']/a");

    }
}

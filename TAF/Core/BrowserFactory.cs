using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EpamAutomationTests.Core
{
    public static class BrowserFactory
    {
        public static IWebDriver GetDriver(string browserType)
        {
            IWebDriver driver;
            switch (browserType.ToLower())
            {
                case "chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArgument("start-maximized");
                    //options.AddArgument("headless");
                    driver = new ChromeDriver(options);
                    break;
                default:
                    throw new ArgumentException("Unsupported browser type.");
            }
            return driver;
        }
    }
}
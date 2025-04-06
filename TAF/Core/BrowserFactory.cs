using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;

namespace EpamAutomationTests.Core
{
    public interface IBrowser
    {
        IWebDriver CreateDriver();
    }

    public class ChromeBrowser : IBrowser
    {
        public IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("headless");
            return new ChromeDriver(options);
        }
    }

    public class EdgeBrowser : IBrowser
    {
        public IWebDriver CreateDriver()
        {
            var options = new EdgeOptions();
            options.AddArgument("start-maximized");
            return new EdgeDriver(options);
        }
    }

    public static class BrowserFactory
    {
        public static IWebDriver GetDriver(string browserType)
        {
            IBrowser browser = browserType.ToLower() switch
            {
                "chrome" => new ChromeBrowser(),
                "edge" => new EdgeBrowser(),
                _ => throw new ArgumentException($"Unsupported browser type: {browserType}")
            };

            return browser.CreateDriver();
        }
    }
}

using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;

namespace WebDriverFactory
{
    public class WebDriverFactory
    {
        private static string[] browser_types = { "chrome", "firefox", "edge", "IE", "opera" };
        public IWebDriver getDriver(string browser)
        {
            if (browser == browser_types[0]) return new ChromeDriver();
            if (browser == browser_types[1]) return new FirefoxDriver();
            if (browser == browser_types[2]) return new EdgeDriver();
            if (browser == browser_types[3]) return new InternetExplorerDriver();
            if (browser == browser_types[4]) return new OperaDriver();
            return null;
        }
        public List<IWebDriver> getDrivers(List<string> browsers)
        {
            List<IWebDriver> drivers = new List<IWebDriver>();
            foreach(string browser in browsers)
            {
                IWebDriver driver;
                if (browser == browser_types[0]) driver = new ChromeDriver();
                else if (browser == browser_types[1]) driver = new FirefoxDriver();
                else if (browser == browser_types[2]) driver = new EdgeDriver();
                else if (browser == browser_types[3]) driver = new InternetExplorerDriver();
                else if (browser == browser_types[4]) driver = new OperaDriver();
                else driver = null;
                drivers.Add(driver);
            }         
            return drivers;
        }
    }
}

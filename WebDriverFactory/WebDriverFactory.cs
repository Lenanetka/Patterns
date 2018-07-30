using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public IWebDriver getDriver(string browser)
        {
            if (browser == "chrome") return new ChromeDriver();
            if (browser == "firefox") return new FirefoxDriver();
            if (browser == "edge") return new EdgeDriver();
            if (browser == "IE") return new InternetExplorerDriver();
            if (browser == "opera") return new OperaDriver();
            return null;
        }
        public List<IWebDriver> getDrivers(List<string> browsers)
        {
            List<IWebDriver> drivers = new List<IWebDriver>();
            foreach(string browser in browsers)
            {
                IWebDriver driver;
                if (browser == "chrome") driver = new ChromeDriver();
                else if (browser == "firefox") driver = new FirefoxDriver();
                else if (browser == "edge") driver = new EdgeDriver();
                else if (browser == "IE") driver = new InternetExplorerDriver();
                else if (browser == "opera") driver = new OperaDriver();
                else driver = null;
                drivers.Add(driver);
            }         
            return drivers;
        }
    }
}

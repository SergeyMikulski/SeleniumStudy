using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.IE;

namespace SeleniumTests
{
    class Program
    {
        static void Main(string[] args)
        {
            //IWebDriver driver = new ChromeDriver();
            IWebDriver driver = new RemoteWebDriver(new Uri("http://127.0.0.1:5555/wd/hub"), new ChromeOptions());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            
            //IWebDriver driver1 = new RemoteWebDriver(new Uri("http://10.165.72.152:4444/wd/hub"), new ChromeOptions());
            IWebDriver driver1 = new RemoteWebDriver(new Uri("http://127.0.0.1:5555/wd/hub"), new ChromeOptions());
            driver1.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);

            driver1.Navigate().GoToUrl("http://www.google.com/");

            driver.Navigate().GoToUrl("http://www.tut.by/");

            //driver = new RemoteWebDriver(new Uri("http://127.0.0.1:5555/wd/hub"), new ChromeOptions());

            //driver.Navigate().GoToUrl("http://www.google.com/");

            IWebDriver _driver = new RemoteWebDriver(new Uri("http://127.0.0.1:5555/wd/hub"), new InternetExplorerOptions());
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            _driver.Navigate().GoToUrl("http://www.google.com/");

            IWebDriver _driver1 = new RemoteWebDriver(new Uri("http://127.0.0.1:5555/wd/hub"), new InternetExplorerOptions());
            _driver1.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
            _driver1.Navigate().GoToUrl("http://www.google.com/");

            driver.Quit();
            driver1.Quit();
            _driver.Quit();
            _driver1.Quit();

            //IWebDriver driver;
            //DesiredCapabilities capability = new DesiredCapabilities();
            //capability.SetCapability(CapabilityType.BrowserName, "Chrome");
            //capability.SetCapability("browser", "Chrome");
            //capability.SetCapability("browser_version", "62.0");
            //capability.SetCapability("os", "Windows");
            //capability.SetCapability("os_version", "10");
            //capability.SetCapability("resolution", "1024x768");
            //capability.SetCapability("browserstack.user", "sergeymikulski1");
            //capability.SetCapability("browserstack.key", "4n211Yf51xsNtHTpYnaK");
            //capability.SetCapability("name", "Bstack-[C_sharp] Sample Test");

            //driver = new RemoteWebDriver(
            //  new Uri("http://hub-cloud.browserstack.com/wd/hub/"), capability
            //);
            //driver.Navigate().GoToUrl("http://www.google.com");
            //Console.WriteLine(driver.Title);

            //IWebElement query = driver.FindElement(By.Name("q"));
            //query.SendKeys("Browserstack");
            //query.Submit();
            //Console.WriteLine(driver.Title);

            //driver.Quit();
        }
    }
}

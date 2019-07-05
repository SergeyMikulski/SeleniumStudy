using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class LiteCartLoginTest
    {
        [Test]
        public void LiteCardLoginTest()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("http://localhost:8080/litecart/admin/");

            var userNameField = driver.FindElement(By.Name("username"));
            userNameField.SendKeys("admin");
            var passwordField = driver.FindElement(By.Name("password"));
            passwordField.SendKeys("admin");
            var loginButton = driver.FindElement(By.TagName("button"));
            loginButton.Click();

            driver.Quit();
        }

        [Test]
        public void LiteCardLoginTestFireFox()
        {
            //IWebDriver driver = new FirefoxDriver();
            FirefoxOptions options = new FirefoxOptions();
            List<string> fireFoxTypes = new List<string>() { @"c:\Program Files\Firefox Nightly\firefox.exe", @"C:\Program Files\Mozilla Firefox\firefox.exe" };
            foreach (var type in fireFoxTypes)
            {
                options.BrowserExecutableLocation = type;
                IWebDriver driver = new FirefoxDriver(options);

                driver.Navigate().GoToUrl("http://localhost:8080/litecart/admin/");

                var userNameField = driver.FindElement(By.Name("username"));
                userNameField.SendKeys("admin");
                var passwordField = driver.FindElement(By.Name("password"));
                passwordField.SendKeys("admin");
                var loginButton = driver.FindElement(By.TagName("button"));
                loginButton.Click();

                driver.Quit();
            }
        }

        [Test]
        public void LiteCardLoginTestIE()
        {
            IWebDriver driver = new InternetExplorerDriver();

            driver.Navigate().GoToUrl("http://localhost:8080/litecart/admin/");

            var userNameField = driver.FindElement(By.Name("username"));
            userNameField.SendKeys("admin");
            var passwordField = driver.FindElement(By.Name("password"));
            passwordField.SendKeys("admin");
            var loginButton = driver.FindElement(By.TagName("button"));
            loginButton.Click();

            driver.Quit();
        }
    }
}

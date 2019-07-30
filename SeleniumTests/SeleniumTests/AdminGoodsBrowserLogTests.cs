using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class AdminGoodsBrowserLogTests
    {
        EventFiringWebDriver _driver;
        List<string> browserLogs;

        [Test]
        public void AdminGoodsBrowserLogTest()
        {
            Start();

            GetBrowserLogs();

            GoodsClick();


            _driver.Quit();
        }

        private void GetBrowserLogs()
        {
            _driver.Navigated += (sender, e) => Console.WriteLine(e.Url);
            _driver.Navigated += (sender, e) => browserLogs.Add(e.Url.ToString());
            _driver.ExceptionThrown += (sender, e) => Console.WriteLine(e.ThrownException);
            _driver.ExceptionThrown += (sender, e) => browserLogs.Add(e.ThrownException.ToString());
            _driver.ElementClicked += (sender, e) => Console.WriteLine(e.Element);
            _driver.ElementClicked += (sender, e) => browserLogs.Add(e.Element.ToString());
        }

        private void OpenClosedFolders()
        {
            var closedFolders = _driver.FindElements(By.ClassName("fa-folder"));
            if (closedFolders.Count > 0)
            {
                var closedFolderLink = closedFolders[0].FindElement(By.XPath("./..//a"));
                closedFolderLink.Click();
                OpenClosedFolders();
            }
        }

        private List<IWebElement> FindGoods()
        {
            var goodsList = _driver.FindElements(By.XPath("//table//img/../a")).ToList();
            return goodsList;
        }

        private void GoodsClick()
        {
            OpenClosedFolders();
            var goodsList = FindGoods();
            for (int i = 0; i < goodsList.Count; i++)
            {
                var browserLogsCount = browserLogs.Count;
                goodsList[i].Click();

                GoToCatalog(_driver);
                OpenClosedFolders();
                goodsList = FindGoods();

                Assert.IsTrue(browserLogsCount < browserLogs.Count);
            }
        }

        private void Start()
        {
            EventFiringWebDriver driver = new EventFiringWebDriver(new ChromeDriver());
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            GoToCatalog(driver);
            _driver = driver;

            var userNameField = _driver.FindElement(By.Name("username"));
            userNameField.SendKeys("admin");
            var passwordField = _driver.FindElement(By.Name("password"));
            passwordField.SendKeys("admin");
            var loginButton = _driver.FindElement(By.TagName("button"));
            loginButton.Click();

            browserLogs = new List<string>();
        }

        private void GoToCatalog(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1");
        }
    }
}

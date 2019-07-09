using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class LiteCartAdminLeftMenuTest
    {
        IWebDriver _driver;
        List<IWebElement> _leftMenuMainItemsList;


        [Test]
        public void LiteCartAdminPageLeftMenuTest()
        {
            Start();

            _leftMenuMainItemsList = LeftMenuMainItemsList();
            var leftMenuMainItemsListCount = _leftMenuMainItemsList.Count();

            for (var i = 0; i < leftMenuMainItemsListCount; i++)
            {
                _leftMenuMainItemsList[i].Click();
                _leftMenuMainItemsList = LeftMenuMainItemsList();
                CheckPageHeaderPresent();

                var leftMenuSubItemsList = leftMenuSubMenuItemsList(i);
                leftMenuSubMenuItemsListClick(leftMenuSubItemsList, i);
            }

            _driver.Quit();
        }

        private List<IWebElement> LeftMenuMainItemsList()
        {
            var menuItems = _driver.FindElements(By.CssSelector("li#app-")).ToList();
            return menuItems;
        }

        private List<IWebElement> leftMenuSubMenuItemsList(int iterator)
        {
            var menuSubItems = _leftMenuMainItemsList[iterator].FindElements(By.TagName("li")).ToList();
            return menuSubItems;
        }

        private void leftMenuSubMenuItemsListClick(List<IWebElement> leftMenuSubItemsList, int leftMenuMainItemNumber)
        {
            var leftMenuSubItemsListCount = leftMenuSubItemsList.Count();

            if (leftMenuSubItemsListCount > 1)
            {
                for (var i = 1; i < leftMenuSubItemsListCount; i++)
                {
                    leftMenuSubItemsList[i].Click();

                    _leftMenuMainItemsList = LeftMenuMainItemsList();
                    leftMenuSubItemsList = leftMenuSubMenuItemsList(leftMenuMainItemNumber);
                    CheckPageHeaderPresent();
                }
            }
        }

        private void CheckPageHeaderPresent()
        {
            Assert.IsTrue(_driver.FindElement(By.CssSelector("td#content h1")) != null);
        }

        private void Start()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:8080/litecart/admin/");
            _driver = driver;

            var userNameField = _driver.FindElement(By.Name("username"));
            userNameField.SendKeys("admin");
            var passwordField = _driver.FindElement(By.Name("password"));
            passwordField.SendKeys("admin");
            var loginButton = _driver.FindElement(By.TagName("button"));
            loginButton.Click();
        }
    }
}

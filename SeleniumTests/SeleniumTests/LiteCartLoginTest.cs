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
    }
}

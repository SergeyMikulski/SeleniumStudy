using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.PageObjects
{
    public class TestsBase
    {
        public IWebDriver _driver;
        public WebDriverWait _wait;

        public TestsBase()
        {
            var driver = new ChromeDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            _driver = driver;
            _wait = wait;
        }

        [SetUp]
        public void Start()
        {
            _driver.Navigate().GoToUrl("http://litecart.stqa.ru/en/");
        }

        [TearDown]
        public void Stop()
        {
            _driver.Quit();
        }
    }
}

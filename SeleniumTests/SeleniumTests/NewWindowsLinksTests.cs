using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class NewWindowsLinksTests
    {
        IWebDriver _driver;
        
        [Test]
        public void CheckHelpLinksOpenNewWindow()
        {
            Start();

            AddNewCountryButtonClick();

            HelpButtonsClick();

            _driver.Quit();
        }

        private void HelpButtonsClick()
        {
            var helpButtons = _driver.FindElements(By.CssSelector("form a:not(#address-format-hint)"));

            foreach (var button in helpButtons)
            {
                NewWindowsOpenAndClose(button);
            }
        }

        private void NewWindowsOpenAndClose(IWebElement buttonToClick)
        {
            var mainWindow = _driver.CurrentWindowHandle;
            var oldWindows = _driver.WindowHandles;
            buttonToClick.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(x => oldWindows.Count() < _driver.WindowHandles.Count());

            var newWindow = _driver.WindowHandles.Except(oldWindows).ToList();
            _driver.SwitchTo().Window(newWindow[0]);
            _driver.Close();
            _driver.SwitchTo().Window(mainWindow);
        }

        private void AddNewCountryButtonClick()
        {
            var addNewCountryButton = _driver.FindElement(By.CssSelector("a.button"));
            addNewCountryButton.Click();
        }

        private void Start()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:8080/litecart/admin/?app=countries&doc=countries");
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

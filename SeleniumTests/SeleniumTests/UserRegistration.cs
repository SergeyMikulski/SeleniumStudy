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
    public class UserRegistration
    {
        IWebDriver _driver;
        List<IWebElement> userRegistrationInputs;
        string email;
        string initialInput = "11111";

        [Test]
        public void UserRegistrationTest()
        {
            Start();

            NewCustomerLinkClick();

            Random random = new Random();
            UserRegistrationProcess(random);

            LogOut();

            LogInWithCredentials();

            LogOut();

            _driver.Quit();
        }

        private void LogOut()
        {
            var logOutButton = _driver.FindElement(By.LinkText("Logout"));
            logOutButton.Click();
        }

        private void UserRegistrationProcess(Random random)
        {
            InitialFieldsFilling();
            EmailFilling(random);

            if (!IsNoticeErrorsShown())
            {
                RegistrationButtonClick();
            }

            while(IsNoticeErrorsShown())
            {
                EmailFilling(random);
                RegistrationButtonClick();
            }
        }

        private bool IsNoticeErrorsShown()
        {
            try
            {
                var error = _driver.FindElement(By.CssSelector(".notice.errors"));
                return error.Displayed;
            }
            catch
            {
                return false;
            }
        }

        private void LogInWithCredentials()
        {
            var emailField = _driver.FindElement(By.Name("email"));
            var passwordField = _driver.FindElement(By.Name("password"));
            var logInButton = _driver.FindElement(By.Name("login"));

            emailField.Clear();
            emailField.SendKeys(email);
            passwordField.Clear();
            passwordField.SendKeys(initialInput);

            logInButton.Click();
        }

        private void RegistrationButtonClick()
        {
            int numberOfTrials = 1;
            if (IsHiddenInputPresent())
            {
                numberOfTrials = 2;
            }

            for(int i = 0; i < numberOfTrials; i++)
            {
                var registrationButton = _driver.FindElement(By.CssSelector("table button"));

                userRegistrationInputsFind();
                userRegistrationInputs[10].Clear();
                userRegistrationInputs[11].Clear();
                userRegistrationInputs[10].SendKeys(initialInput);
                userRegistrationInputs[11].SendKeys(initialInput);

                registrationButton.Click();
            }
        }

        private void EmailFilling(Random random)
        {
            string emailStart;
            userRegistrationInputsFind();
            if (userRegistrationInputs[8].GetAttribute("value").Contains("@"))
            {
                emailStart = userRegistrationInputs[8].GetAttribute("value").Substring(0, userRegistrationInputs[8].GetAttribute("value").IndexOf('@'));
            }
            else
            {
                emailStart = userRegistrationInputs[8].GetAttribute("value");
            }
            var emailEnd = "@mail.ru";

            userRegistrationInputs[8].Clear();

            email = emailStart + random.Next(10).ToString();
            userRegistrationInputs[8].SendKeys(email + emailEnd);
            email += emailEnd;
        }

        private void InitialFieldsFilling()
        {
            var countrySelecionField = _driver.FindElement(By.CssSelector("table select:not([disabled])"));
            var selectElement = new SelectElement(countrySelecionField);
            selectElement.SelectByText("United States");

            userRegistrationInputsFind();
            foreach (var input in userRegistrationInputs)
            {
                input.SendKeys(initialInput);
            }
        }

        private void userRegistrationInputsFind()
        {
            userRegistrationInputs = _driver.FindElements(By.CssSelector("table input:not([type=hidden]):not([type=checkbox])")).ToList();
        }

        private bool IsHiddenInputPresent()
        {
            var userRegistrationHiddenInputs = _driver.FindElements(By.CssSelector("table input[type=hidden]")).ToList();
            return userRegistrationHiddenInputs.Count() > 0;
        }

        private void NewCustomerLinkClick()
        {
            var newCustomerLink = _driver.FindElement(By.CssSelector("table a"));
            newCustomerLink.Click();
        }

        private void Start()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:8080/litecart/");
            _driver = driver;
        }
    }
}

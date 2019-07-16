using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class GoodAddToAdmin
    {
        IWebDriver _driver;
        private const string productName = "Siutcase";

        [Test]
        public void GoodAddToAdminTest()
        {
            Start();

            LeftMenuMainCatalogItemClick();
            AddNewProductButtonClick();

            FillGeneralTab();
            FillInformationTab();
            FillPricesTab();
            SaveChangesButtonClick();

            var newProductItem = _driver.FindElement(By.LinkText(productName));
            Assert.IsTrue(newProductItem.Displayed);

            _driver.Quit();
        }

        private void FillGeneralTab()
        {
            ChooseProductTabs("General");
            SetStatusEnabled();
            FillInputFields();
            SetProductGroups();
            SetNumberFields();
            UploadFile();
            SetDates();
        }

        private void FillInformationTab()
        {
            ChooseProductTabs("Information");
            FillInputFields();
            SelectDropDowns();
            FillDescriptionTextBox();
        }

        private void FillPricesTab()
        {
            ChooseProductTabs("Prices");
            FillInputFields();
            SetNumberFields();
            SelectDropDowns();
        }

        private void SaveChangesButtonClick()
        {
            var saveButton = _driver.FindElement(By.Name("save"));
            saveButton.Click();
        }

        private void SelectDropDowns()
        {
            var dropdownsSelectionFields = _driver.FindElements(By.CssSelector("table select"));
            var validDropdownsSelectionFields = dropdownsSelectionFields.Where(x => x.Displayed);
            foreach (var selection in validDropdownsSelectionFields)
            {
                var selectElement = new SelectElement(selection);
                try
                {
                    selectElement.SelectByIndex(1);
                }
                catch
                {
                    selectElement.SelectByIndex(0);
                }
            }
        }

        private void FillDescriptionTextBox()
        {
            var textBox = _driver.FindElement(By.ClassName("trumbowyg-editor"));
            textBox.SendKeys(productName);
        }

        private void UploadFile()
        {
            var inputFileField = _driver.FindElement(By.CssSelector("[type=file]"));

            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var iconPath = Path.Combine(outPutDirectory, "../../../Suitcase.jpg");
            string icon_path = new Uri(iconPath).LocalPath;
            inputFileField.SendKeys(icon_path);
        }

        private void FillInputFields()
        {
            var inputFields = _driver.FindElements(By.CssSelector("[type=text][data-type=text]"));
            var validInputFields = inputFields.Where(x => x.Displayed);
            foreach(var field in validInputFields)
            {
                field.SendKeys(productName);
            }
        }

        private void SetProductGroups()
        {
            var femaleCheckBox = _driver.FindElement(By.XPath("//td[contains(text(),'Female')]/..//input"));
            femaleCheckBox.Click();
        }

        private void SetNumberFields()
        {
            var numbersInputFields = _driver.FindElements(By.CssSelector("[type=number]"));
            var validNumbersInputFields = numbersInputFields.Where(x => x.Displayed);
            foreach (var field in validNumbersInputFields)
            {
                field.SendKeys("5");
            }
        }

        private void SetDates()
        {
            var datesFields = _driver.FindElements(By.CssSelector("[type=date]"));
            datesFields[0].SendKeys("01Jan" + Keys.ArrowRight + "2001");
            datesFields[1].SendKeys("01Jan" + Keys.ArrowRight + "2025");
        }

        private void SetStatusEnabled()
        {
            var enabledStatusRadioButton = _driver.FindElement(By.XPath("//label[contains(text(),'Enabled')]/input"));
            enabledStatusRadioButton.Click();
        }

        private void ChooseProductTabs(string requiredTabName)
        {
            var tabsList = _driver.FindElements(By.CssSelector(".index li"));
            var tabToClick = tabsList.Single(x => x.Text == requiredTabName);
            tabToClick.Click();
        }

        private void AddNewProductButtonClick()
        {
            var addNewProductButton = _driver.FindElements(By.CssSelector("div a.button"))[1];
            addNewProductButton.Click();
        }

        private void LeftMenuMainCatalogItemClick()
        {
            var menuCatalogItem = _driver.FindElements(By.CssSelector("li#app-"))[1];
            var menuCatalogItemToClick = menuCatalogItem.FindElement(By.TagName("a"));
            menuCatalogItemToClick.Click();
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

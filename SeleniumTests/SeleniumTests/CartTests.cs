using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;

namespace SeleniumTests
{
    public class CartTests
    {
        IWebDriver _driver;
        WebDriverWait _wait;

        [Test]
        public void CartAddRemoveGoodsTest()
        {
            Start();

            for(int i = 0; i < 3; i++)
            {
                LatestProductsClick(i);
                AddGoogToCart();
                CloseAlert();
                GoHome();
            }

            OpenCart();
            RemoveProductsFromCart();

            _driver.Quit();
        }

        private void RemoveProductsFromCart()
        {
            var removeButtonsCount = _driver.FindElements(By.Name("remove_cart_item")).Count();

            for (int i = 0; i < removeButtonsCount; i++)
            {
                var productTableRowsCount = _driver.FindElements(By.CssSelector(".dataTable tr")).Count();

                var removeButton = _driver.FindElement(By.Name("remove_cart_item"));
                FirstShortCutClick();
                removeButton.Click();

                WaitProductListDecresedOrDisappeared(productTableRowsCount);
            }
        }

        private void FirstShortCutClick()
        {
            var shortCutsList = _driver.FindElements(By.ClassName("shortcut"));
            if(shortCutsList.Count > 1)
                shortCutsList[0].Click();
        }

        private void WaitProductListDecresedOrDisappeared(int initialProductsNumber)
        {
            var productTableCount = _driver.FindElements(By.CssSelector(".dataTable tr")).Count();
            _wait.Until(x => _driver.FindElements(By.CssSelector(".dataTable tr")).Count() < initialProductsNumber);
        }

        private void OpenCart()
        {
            var cartLink = _driver.FindElement(By.CssSelector("#cart-wrapper .link"));
            cartLink.Click();
        }
        private void LatestProductsClick(int productNumber)
        {
            var latestProductsList = _driver.FindElements(By.CssSelector("#box-latest-products a.link")).ToList();
            latestProductsList[productNumber].Click();
        }

        private void AddGoogToCart()
        {
            if(IsGoodContainsOptions())
            {
                SelectGoodOption();
            }

            var addToCartButton = _driver.FindElement(By.Name("add_cart_product"));
            addToCartButton.Click();
        }

        private void CloseAlert()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
            _driver.SwitchTo().Alert().Accept();
        }

        private void GoHome()
        {
            var homeButton = _driver.FindElement(By.ClassName("general-0"));
            homeButton.Click();
        }

        private void SelectGoodOption()
        {
            var goodSelecionField = _driver.FindElement(By.TagName("select"));
            var selectElement = new SelectElement(goodSelecionField);
            selectElement.SelectByIndex(1);
        }

        private bool IsGoodContainsOptions()
        {
            return _driver.FindElements(By.ClassName("options")).Any();
        }

        private void Start()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl("http://localhost:8080/litecart/");
            _driver = driver;

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            _wait = wait;
        }
    }
}

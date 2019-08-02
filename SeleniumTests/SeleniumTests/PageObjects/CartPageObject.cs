using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.PageObjects
{
    public class CartPageObject : PageBase
    {
        public CartPageObject(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        private List<IWebElement> RemoveButtons => _driver.FindElements(By.Name("remove_cart_item")).ToList();

        private List<IWebElement> ProductTableRows => _driver.FindElements(By.CssSelector(".dataTable tr")).ToList();

        private int ProductTableRowsCount => ProductTableRows.Count();

        private List<IWebElement> ShortCutsList => _driver.FindElements(By.ClassName("shortcut")).ToList();

        public void RemoveProductsFromCart()
        {
            var removeButtonsCount = RemoveButtons.Count();

            for (int i = 0; i < removeButtonsCount; i++)
            {
                var productTableRowsCount = ProductTableRows.Count();

                FirstShortCutClick();
                var removeButton = RemoveButtons.First();
                removeButton.Click();

                WaitProductListDecresedOrDisappeared(productTableRowsCount);
            }
        }

        private void FirstShortCutClick()
        {
            if (ShortCutsList.Count > 1)
                ShortCutsList[0].Click();
        }

        private void WaitProductListDecresedOrDisappeared(int initialProductsNumber) => _wait.Until(x => ProductTableRowsCount < initialProductsNumber);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.PageObjects
{
    public class CartWrapperPageBlock : PageBase
    {
        public CartWrapperPageBlock(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        private IWebElement CartWrapper => _driver.FindElement(By.Id("cart-wrapper"));

        private IWebElement CartCheckOutLink => CartWrapper.FindElement(By.ClassName("link"));

        private IWebElement CartNumberOfItems => CartWrapper.FindElement(By.ClassName("quantity"));

        public string CartNumberOfItemsText => CartNumberOfItems.Text;

        public CartPageObject OpenCart()
        {
            CartCheckOutLink.Click();

            return new CartPageObject(_driver, _wait);
        }



        public void WaitCartItemsNumberChanged(string initialValue) => _wait.Until(x => CartNumberOfItemsText != initialValue);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.PageObjects
{
    public class GoodPageObject : PageBase
    {
        public GoodPageObject(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        private IWebElement AddToCartButton => _driver.FindElement(By.Name("add_cart_product"));

        private IWebElement GoodSelecionField => _driver.FindElement(By.TagName("select"));

        private List<IWebElement> OptionsFields => _driver.FindElements(By.ClassName("options")).ToList();

        public  void AddGoodToCart()
        {
            if (IsGoodContainsOptions())
            {
                SelectGoodOption();
            }

            var cartWrapper = new CartWrapperPageBlock(_driver, _wait);
            var cartNumberOfItems = cartWrapper.CartNumberOfItemsText;

            AddToCartButton.Click();

            
            cartWrapper.WaitCartItemsNumberChanged(cartNumberOfItems);
        }

        private void SelectGoodOption()
        {
            var selectElement = new SelectElement(GoodSelecionField);
            selectElement.SelectByIndex(1);
        }

        private bool IsGoodContainsOptions()
        {
            return OptionsFields.Any();
        }
    }
}

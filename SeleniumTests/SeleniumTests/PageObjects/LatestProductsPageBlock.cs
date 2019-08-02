using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.PageObjects
{
    public class LatestProductsPageBlock : PageBase
    {
        public LatestProductsPageBlock(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        private IWebElement LatestProductsBox => _driver.FindElement(By.CssSelector("#box-latest-products"));

        private List<IWebElement> LatestProductsList => LatestProductsBox.FindElements(By.CssSelector("a.link")).ToList();

        public GoodPageObject LatestProductsClick(int productNumber)
        {
            LatestProductsList[productNumber].Click();
            return new GoodPageObject(_driver, _wait);
        }
    }
}

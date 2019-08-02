using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests.PageObjects
{
    public class PageHeaderMenuPageBlock : PageBase
    {
        public PageHeaderMenuPageBlock(IWebDriver driver, WebDriverWait wait) : base(driver, wait)
        {
        }

        private IWebElement HomeButton => _driver.FindElement(By.ClassName("general-0"));

        public void GoHome()
        {
            HomeButton.Click();
        }
    }
}

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class LiteCartMainPageTests
    {
        IWebDriver _driver;

        [Test]
        public void LiteCartMainPageStickersCheck()
        {
            Start();

            var mainBoxes = MainBoxes();

            foreach(var box in mainBoxes)
            {
                var products = box.FindElements(By.ClassName("product")).ToList();
                CheckStickers(products);
            }

            _driver.Quit();
        }

        private List<IWebElement> MainBoxes()
        {
            var boxMostPopular = _driver.FindElement(By.Id("box-most-popular"));
            var boxCampaigns = _driver.FindElement(By.Id("box-campaigns"));
            var boxLatestProducts = _driver.FindElement(By.Id("box-latest-products"));

            List<IWebElement> boxes = new List<IWebElement>() { boxMostPopular, boxCampaigns, boxLatestProducts };

            return boxes;
        }

        private void CheckStickers(List<IWebElement> products)
        {
            foreach (var product in products)
            {
                var stickers = product.FindElements(By.ClassName("sticker"));
                Assert.IsTrue(stickers.Count() == 1);
            }
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

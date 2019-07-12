using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class GoodsTests
    {
        IWebDriver _driver;
        private string _regularPriceMainPage;
        private string _campainPriceMainPage;
        private string _regularPriceProductPage;
        private string _campainPriceProductPage;
        private string _productNameMainPage;
        private string _productNameProductPage;

        [Test]
        [TestCase(DriverName.ChromeDriver)]
        [TestCase(DriverName.IeDriver)]
        [TestCase(DriverName.FireFoxDriver)]
        public void GoodAttibutesTest(DriverName drivername)
        {
            Start(drivername);

            MainPageActions();
            ProductPageActions();

            Assert.AreEqual(_regularPriceMainPage, _regularPriceProductPage);
            Assert.AreEqual(_campainPriceMainPage, _campainPriceProductPage);
            Assert.AreEqual(_productNameMainPage, _productNameProductPage);

            _driver.Quit();
        }

        private void MainPageActions()
        {
            var campainsBox = _driver.FindElement(By.Id("box-campaigns"));
            var product = campainsBox.FindElement(By.ClassName("product"));
            var productLink = product.FindElement(By.TagName("a"));
            var productName = product.FindElement(By.ClassName("name")).Text;

            var regularPrice = product.FindElement(By.ClassName("regular-price"));
            var campainPrice = product.FindElement(By.ClassName("campaign-price"));

            PricesActions(regularPrice, campainPrice);

            _regularPriceMainPage = regularPrice.Text;
            _campainPriceMainPage = campainPrice.Text;
            _productNameMainPage = productName;

            productLink.Click();
        }

        private void ProductPageActions()
        {
            var resultPageProductBox = _driver.FindElement(By.Id("box-product"));
            var resultPageProductName = resultPageProductBox.FindElement(By.TagName("h1")).Text;

            var regularPrice = resultPageProductBox.FindElement(By.ClassName("regular-price"));
            var campainPrice = resultPageProductBox.FindElement(By.ClassName("campaign-price"));

            PricesActions(regularPrice, campainPrice);

            _regularPriceProductPage = regularPrice.Text;
            _campainPriceProductPage = campainPrice.Text;
            _productNameProductPage = resultPageProductName;
        }

        private void PricesActions(IWebElement regularPrice, IWebElement campainPrice)
        {
            var regularPriceLineThrough = regularPrice.GetAttribute("tagName");
            Assert.AreEqual("S", regularPriceLineThrough);
            var regularPriceColor = regularPrice.GetCssValue("color");
            var rgb = regularPriceColor.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            rgb[0] = rgb[0].Substring(rgb[0].IndexOf('(') + 1);
            if (rgb.Count() == 3)
            {
                rgb[2] = rgb[2].Substring(0, rgb[2].IndexOf(')'));
            }
            Assert.AreEqual(rgb[0], rgb[1]);
            Assert.AreEqual(rgb[1], rgb[2]);
            var regularPriceFontSize = int.Parse(string.Concat(regularPrice.GetCssValue("font-size").Take(2)));

            var campainPriceBold = campainPrice.GetAttribute("tagName");
            Assert.AreEqual("STRONG", campainPriceBold);
            var campainPriceColor = campainPrice.GetCssValue("color");
            rgb = campainPriceColor.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            rgb[0] = rgb[0].Substring(rgb[0].IndexOf('(') + 1);
            if (rgb.Count() == 3)
            {            
                rgb[2] = rgb[2].Substring(0, rgb[2].IndexOf(')'));
            }
            Assert.IsTrue(int.Parse(rgb[0]) != 0);
            Assert.IsTrue(int.Parse(rgb[1]) == 0);
            Assert.IsTrue(int.Parse(rgb[2]) == 0);
            var campainPriceFontSize = int.Parse(string.Concat(campainPrice.GetCssValue("font-size").Take(2)));

            Assert.IsTrue(campainPriceFontSize > regularPriceFontSize);
        }

        private void Start(DriverName driverName)
        {
            switch (driverName)
            {
                case DriverName.ChromeDriver:
                    {
                        _driver = new ChromeDriver();
                        break;
                    }
                case DriverName.FireFoxDriver:
                    {
                        _driver = new FirefoxDriver();
                        break;
                    }
                case DriverName.IeDriver:
                    {
                        _driver = new InternetExplorerDriver();
                        break;
                    }
            }

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl("http://localhost:8080/litecart/");
        }
    }

    public enum DriverName
    {
        ChromeDriver,
        IeDriver,
        FireFoxDriver
    }
}

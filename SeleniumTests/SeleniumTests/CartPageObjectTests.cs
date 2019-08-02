using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class CartPageObjectTests : TestsBase
    {

        [Test]
        public void CartAddRemoveGoodsTest()
        {
            var latestProductsPageBlock = new LatestProductsPageBlock(_driver, _wait);
            var pageHeaderMenu = new PageHeaderMenuPageBlock(_driver, _wait);

            for (int i = 0; i < 3; i++)
            {
                var good = latestProductsPageBlock.LatestProductsClick(i);
                good.AddGoodToCart();
                pageHeaderMenu.GoHome();
            }

            var cartWrapper = new CartWrapperPageBlock(_driver, _wait);
            var cartPage = cartWrapper.OpenCart();

            cartPage.RemoveProductsFromCart();
        }
    }
}

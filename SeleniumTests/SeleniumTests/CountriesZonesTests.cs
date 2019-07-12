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
    public class CountriesZonesTests
    {
        IWebDriver _driver;
        IWebElement _countiesTable;
        private string countiesPageURL = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
        private string geoPageURL = "http://localhost:8080/litecart/admin/?app=geo_zones&doc=geo_zones";

        [Test]
        public void CountiesZonesCountriesPageCheckAlphaOrder()
        {
            Start(countiesPageURL);

            CountriesTableFind();

            var countryColumns = _countiesTable.FindElements(By.CssSelector("tr td a")).ToList();
            var countryNameColumn = countryColumns.Where(x => x.Text.Length > 0);
            var countryNames = countryNameColumn.Select(x => x.Text).ToList();
            AlphaOrderCheck(countryNames);


            var cellsWithZones = GetCellsWithZones(_countiesTable);
            var rowsIDsWithZones = GetRowsWithZones(cellsWithZones);

            for (int i = 0; i < rowsIDsWithZones.Count; i++)
            {
                CountryClickByID(rowsIDsWithZones[i]);
                ZonesAlphaOrderCheck();
                _driver.Navigate().GoToUrl(countiesPageURL);
                CountriesTableFind();
            }

            _driver.Quit();
        }

        [Test]
        public void CountiesZonesGeoPageCheckAlphaOrder()
        {
            Start(geoPageURL);

            CountriesTableFind();

            var rowsAmount = _countiesTable.FindElements(By.CssSelector("tr.row")).Count;

            for (int i = 0; i < rowsAmount; i++)
            {
                CountryClick(i);
                GeoZonesAlphaOrderCheck();
                _driver.Navigate().GoToUrl(geoPageURL);
                CountriesTableFind();
            }

            _driver.Quit();
        }

        private void CountryClick(int i)
        {
            var rows = _countiesTable.FindElements(By.CssSelector("tr.row"));
            var elementToClick = rows[i].FindElement(By.TagName("a"));
            elementToClick.Click();
        }

        private void GeoZonesAlphaOrderCheck()
        {
            var zonesTable = _driver.FindElement(By.ClassName("dataTable"));
            var countryColumnsCells = zonesTable.FindElements(By.CssSelector("tr td")).ToList();

            List<IWebElement> cellsWithNames = new List<IWebElement>();
            for (var i = 2; i < (countryColumnsCells.Count); i = i + 4)
            {
                cellsWithNames.Add(countryColumnsCells[i]);
            }

            var cellNames = cellsWithNames.Select(x => x.FindElement(By.CssSelector("option[selected]")).Text).ToList();

            AlphaOrderCheck(cellNames);
        }

        private List<string> DropDownSelectedValueRead(List<IWebElement> cellsWithNames)
        {
            var cellNames = cellsWithNames.Select(x => x.FindElement(By.CssSelector("option[selected]")).Text).ToList();
            return cellNames;
        }

        private void CountriesTableFind()
        {
            _countiesTable = _driver.FindElement(By.ClassName("dataTable"));
        }

        private IEnumerable<IWebElement> GetCellsWithZones(IWebElement countiesTable)
        {
            var cells = countiesTable.FindElements(By.CssSelector("tr.row td"));
            List<IWebElement> cellsZones = new List<IWebElement>();
            for (var i = 5; i < cells.Count; i = i + 7)
            {
                cellsZones.Add(cells[i]);
            }

            var cellsWithZones = cellsZones.Where(x => int.Parse(x.Text) > 0);

            return cellsWithZones;            
        }

        private List<string> GetRowsWithZones(IEnumerable<IWebElement> cellsWithZones)
        {
            var rowsWithZones = cellsWithZones.Select(x => x.FindElement(By.XPath("./.."))).ToList();

            var countryWithZoneRowID = rowsWithZones.Select(x => (x.FindElements(By.TagName("td")).ToList())[2].Text).ToList();

            return countryWithZoneRowID;
        }

        private void CountryClickByID(string rowIDWithZones)
        {
            var rows = _countiesTable.FindElements(By.CssSelector("tr.row"));
            var rowToClick = rows.Single(x => (x.FindElements(By.TagName("td")).ToList())[2].Text == rowIDWithZones);
            var elementToClick = rowToClick.FindElement(By.TagName("a"));
            elementToClick.Click();
        }

        private void ZonesAlphaOrderCheck()
        {
            var zonesTable = _driver.FindElement(By.ClassName("dataTable"));
            var countryColumnsCells = zonesTable.FindElements(By.CssSelector("tr td")).ToList();

            List<IWebElement> cellsWithNames = new List<IWebElement>();
            for (var i = 2; i < (countryColumnsCells.Count-4); i = i + 4)
            {
                cellsWithNames.Add(countryColumnsCells[i]);
            }

            var cellNames = cellsWithNames.Select(x => x.Text).ToList();

            AlphaOrderCheck(cellNames);
        }

        private void AlphaOrderCheck(List<string> countryNames)
        {
            var sorted = new List<string>();
            sorted.AddRange(countryNames.OrderBy(o => o));
            Assert.IsTrue(countryNames.SequenceEqual(sorted));
        }

        private void Start(string expectedPageURL)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Navigate().GoToUrl(expectedPageURL);
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

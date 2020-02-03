using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace csharttest
{
    [TestFixture]
    public class AdminTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            ChromeOptions options = new ChromeOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            
        }

        [Test]
        [Obsolete]
        public void Login()
        {
            LoginInternal(this.driver);
        }


        [Test]
        [Obsolete]
        public void Task7_CheckMenu()
        {
            LoginInternal(this.driver);
            var selector = "div#body table td#sidebar div#box-apps-menu-wrapper ul#box-apps-menu li#app-";
            var headerSelector = "div#body table td#content h1";
            var liElements = driver.FindElements(By.CssSelector(selector));

            for(var i = 0; i < liElements.Count; i++)
            {
                liElements[i].Click();
                liElements = driver.FindElements(By.CssSelector(selector));
                var innerLiElements = liElements[i].FindElements(By.CssSelector("ul.docs li"));

                for (var j = 0; j < innerLiElements.Count; j++)
                {
                    if(j > 0)
                        innerLiElements[j].Click();
                    liElements = driver.FindElements(By.CssSelector(selector));
                    innerLiElements = liElements[i].FindElements(By.CssSelector("ul.docs li"));
                    wait.Until(ExpectedConditions.ElementExists(By.CssSelector(headerSelector)));
                    Thread.Sleep(250);
                }

                Thread.Sleep(250);
            }
        }   


        [Test]
        [Obsolete]
        public void Task9_CheckCountriesSorting()
        {
            LoginInternal(this.driver);
            driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
            var trElementsSelector = "tr.row";
            var trElements = driver.FindElements(By.CssSelector(trElementsSelector));
            string cache = null; 
            for (var i = 0; i < trElements.Count - 1; i++)
            {
                string currentCountry = cache == null ? GetCountryName(trElements, i) : cache;
                string nextCountry = GetCountryName(trElements, i + 1);
                cache = nextCountry;
                var textCompare = string.Compare(currentCountry, nextCountry);
                Assert.AreEqual(textCompare, -1);
            }
        }

        [Test]
        [Obsolete]
        public void Task9_CheckGeoZones()
        {
            LoginInternal(this.driver);
            driver.Url = "http://localhost:8080/litecart/admin/?app=geo_zones&doc=geo_zones";
            var trElementsSelector = "tr.row";
            var trElements = driver.FindElements(By.CssSelector(trElementsSelector));
            for (var i = 0; i < trElements.Count; i++)
            {
                var tdElements = trElements[i].FindElements(By.CssSelector("td"));
                var tdCountry = tdElements[2];
                var aElement = tdCountry.FindElement(By.CssSelector("a"));
                aElement.Click();
                wait.Until(ExpectedConditions.TitleIs("Edit Geo Zone | My Store"));
                var tableElementsSelector = "table#table-zones tr:not(.header)";
                var trZoneElements = driver.FindElements(By.CssSelector(tableElementsSelector));
                string cache = null;
                for (var j = 0; j < trZoneElements.Count - 2; j++)
                {
                    string currentZone = cache == null ? GetZoneName(trZoneElements, j) : cache;
                    string nextZone = GetZoneName(trZoneElements, j + 1);
                    cache = nextZone;
                    var textCompare = string.Compare(currentZone, nextZone);
                    Assert.AreEqual(textCompare, -1);
                }
                driver.Navigate().Back();
                trElements = driver.FindElements(By.CssSelector(trElementsSelector));
            }
        }

        [Test]
        [Obsolete]
        public void Test12()
        {
            LoginInternal(this.driver);
            driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog";

            driver.FindElement(By.CssSelector("td#content div :nth-child(2)")).Click();
            var name = Guid.NewGuid().ToString();
            GeneralInfo(name);
            driver.FindElement(By.CssSelector("div.tabs ul :nth-child(2)")).Click();
            InformationInfo();

            driver.FindElement(By.CssSelector("div.tabs ul :nth-child(4)")).Click();
            PricesInfo();

            driver.FindElement(By.CssSelector("span.button-set button[name='save']")).Click();

            Thread.Sleep(1000);

            var trRow = driver.FindElements(By.CssSelector("tr.row"));
            var productsCount = trRow.Count(tr =>
            {
                var tdName = tr.FindElements(By.CssSelector("td"))[2];
                var a = tdName.FindElement(By.CssSelector("a"));
                return a.Text == name;
            });

            Assert.AreEqual(1, productsCount);
        }

        [Test]
        [Obsolete]
        public void Test14()
        {
            LoginInternal(this.driver);
            driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
            WaitForLoad(driver, wait);
            driver.FindElement(By.CssSelector("form[name='countries_form'] table.dataTable tr.row"))
                .FindElements(By.CssSelector("td"))[4].FindElement(By.CssSelector("a")).Click();
            WaitForLoad(driver, wait);
            var externalLinks = driver.FindElements(By.CssSelector("form table a[target='_blank']"));
            foreach(var externalLink in externalLinks)
            {
                externalLink.Click();
                driver.SwitchTo().Window(driver.WindowHandles[1]);
                WaitForLoad(driver, wait);
                driver.SwitchTo().Window(driver.WindowHandles[1]).Close();
                driver.SwitchTo().Window(driver.WindowHandles[0]);
            }
        }

        [Test]
        [Obsolete]
        public void Test17()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.SetLoggingPreference(LogType.Browser, LogLevel.Warning);

            driver = new FirefoxDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            LoginInternal(driver);
            driver.Url = "http://localhost:8080/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            WaitForLoad(driver, wait);

            var rows = driver.FindElements(By.CssSelector("form[name='catalog_form'] table.dataTable tr.row"))
                .Where(row => !row.FindElements(By.CssSelector("i.fa-folder-open,i.fa-folder")).Any()).ToList();
            var i = 0;
            for(i = 0; i < rows.Count(); i++)
            {
                rows[i].FindElements(By.CssSelector("td"))[2].FindElement(By.CssSelector("a")).Click();
                Thread.Sleep(400);

                // fails with NullReferenceException
                var entries = driver.Manage().Logs.GetLog(LogType.Browser);
                Assert.IsEmpty(entries, "logs!!!");
                
                driver.Navigate().Back();
                Thread.Sleep(400);

                // refresh rows
                rows = driver.FindElements(By.CssSelector("form[name='catalog_form'] table.dataTable tr.row"))
                    .Where(row => !row.FindElements(By.CssSelector("i.fa-folder-open,i.fa-folder")).Any()).ToList();
            }

            Thread.Sleep(2000);
        }

        private static void WaitForLoad(IWebDriver driver, WebDriverWait wait)
        {
            wait.Until(wd => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString() == "complete");
        }

        private void GeneralInfo(string name)
        {
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div#tab-general input[name='name[en]']")).SendKeys(name);

            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div#tab-general input[name='code']")).SendKeys("12345");

            Thread.Sleep(500);
            driver.FindElements(By.CssSelector("div#tab-general input[name='categories[]']"))[1].Click();

            Thread.Sleep(500);
            var category = driver.FindElement(By.Name("default_category_id"));
            var selectElement = new SelectElement(category);
            selectElement.SelectByValue("1");

            Thread.Sleep(500);
            driver.FindElements(By.CssSelector("div#tab-general input[name='product_groups[]']"))[1].Click();

            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div#tab-general input[name='quantity']")).SendKeys("5");

            Thread.Sleep(500);
            var upload = driver.FindElement(By.CssSelector("div#tab-general input[name='new_images[]"));
            upload.SendKeys(Path.GetFullPath("2020-02-01 20.07.42.jpg"));

            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div#tab-general input[name='date_valid_from']")).SendKeys("01012020");

            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div#tab-general input[name='date_valid_to']")).SendKeys("01012021");
        }

        private void InformationInfo()
        {

            Thread.Sleep(500);
            var manufacturer = driver.FindElement(By.Name("manufacturer_id"));
            var selectElement = new SelectElement(manufacturer);
            selectElement.SelectByValue("1");

            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div#tab-information input[name='keywords']")).SendKeys("keywords");

            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div#tab-information input[name='short_description[en]']")).SendKeys("short_description");

            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div.trumbowyg-editor")).SendKeys("editor");

            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div#tab-information input[name='head_title[en]']")).SendKeys("head_title");

            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div#tab-information input[name='meta_description[en]']")).SendKeys("meta_description");
        }

        private void PricesInfo()
        {
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div#tab-prices input[name='purchase_price']")).SendKeys("1000");

            Thread.Sleep(500);
            var purchasePrice = driver.FindElement(By.Name("purchase_price_currency_code"));
            var selectElement = new SelectElement(purchasePrice);
            selectElement.SelectByValue("EUR");

            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div#tab-prices input[name='prices[USD]']")).SendKeys("1000");

            Thread.Sleep(500);
            driver.FindElement(By.CssSelector("div#tab-prices input[name='prices[EUR]']")).SendKeys("1000");
        }

        private string GetZoneName(ReadOnlyCollection<IWebElement> trZoneElements, int j)
        {
            var tdZoneElements = trZoneElements[j].FindElements(By.CssSelector("td"));
            var tdCountryZone = tdZoneElements[2];
            var selectNameZones = tdCountryZone.FindElements(By.CssSelector("select option"));
            for (var i = 0; i < selectNameZones.Count; i++)
            {
                var zoneSelect = selectNameZones[i].GetAttribute("selected");
                if(zoneSelect == "true")
                {
                    return selectNameZones[i].Text;
                }
            }
            Assert.Fail("Country Zone is not selected");
            return null;
        }



        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

        private string GetCountryName(ReadOnlyCollection<IWebElement> trElements, int i)
        {
            var tdElements = trElements[i].FindElements(By.CssSelector("td"));
            var tdCountry = tdElements[4];
            var aElement = tdCountry.FindElement(By.CssSelector("a"));
            var aText = aElement.Text;
            return aText;
        }

        private void LoginInternal(IWebDriver driver)
        {
            driver.Url = "http://localhost:8080/litecart/admin/";
            Thread.Sleep(1000);
            //IWebElement element = wait.Until(d => d.FindElement(By.Name("username")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            //IWebElement elementpswd = wait.Until(d => d.FindElement(By.Name("password")));
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("remember_me")).Click();
            driver.FindElement(By.Name("login")).Click();
            Thread.Sleep(1000);
            //wait.Until(ExpectedConditions.TitleIs("My Store"));
        }
    }
}

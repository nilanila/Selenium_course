using System;
using System.Threading;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharttest
{
    [TestFixture]
    public class SortingTests
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        }

        [Test]
        [Obsolete]
        public void Task9_CheckSorting()
        {
            driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
            var trElementsSelector = "tr.row";
            var trElements = driver.FindElements(By.CssSelector(trElementsSelector));

            int i = 5;
            i++;
            //for (var i = 0; i < liElements.Count; i++)
            //{
             //   var stickers = liElements[i].FindElements(By.CssSelector(stickerSelector));
               // Assert.AreEqual(stickers.Count, 1);
            //}
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

    }
}

using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharttest
{
    [TestFixture]
    public class ProductTests
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
        public void Task8_CheckStickers()
        {
            driver.Url = "http://localhost:8080/litecart/";
            var liElementsSelector = "li.product";
            var stickerSelector = "div.sticker";
            var liElements = driver.FindElements(By.CssSelector(liElementsSelector)); 
            for (var i = 0; i < liElements.Count; i++)
            {
                var stickers = liElements[i].FindElements(By.CssSelector(stickerSelector));
                //Assert.IsTrue(stickers.Count == 1);
                Assert.AreEqual(stickers.Count, 1);
            }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

    }
}

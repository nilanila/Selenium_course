using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

namespace csharttest
{
    [TestFixture]
    public class ProductTests
    {
        private IWebDriver driverChrome;
        private IWebDriver driverFF;

        private WebDriverWait waitChrome;
        private WebDriverWait waitFF;


        [Test]
        [Obsolete]
        public void Task8_CheckStickers()
        {
            driverChrome = new ChromeDriver();
            waitChrome = new WebDriverWait(driverChrome, TimeSpan.FromSeconds(2));

            driverChrome.Url = "http://localhost:8080/litecart/";
            var liElementsSelector = "li.product";
            var stickerSelector = "div.sticker";
            var liElements = driverChrome.FindElements(By.CssSelector(liElementsSelector));

            for (var i = 0; i < liElements.Count; i++)
            {
                var stickers = liElements[i].FindElements(By.CssSelector(stickerSelector));
                Assert.AreEqual(stickers.Count, 1);
            }

            driverChrome.Quit();
            driverChrome = null;
        }

        [Test]
        [Obsolete]
        public void Task10_Chrome()
        {
            driverChrome = new ChromeDriver();
            waitChrome = new WebDriverWait(driverChrome, TimeSpan.FromSeconds(2));
            Test10(driverChrome, waitChrome, "rgba({0}, {1}, {2}, 1)", "700");
            driverChrome.Quit();
            driverChrome = null;
        }

        [Test]
        [Obsolete]
        public void Task10_FF()
        {
            driverFF = new FirefoxDriver();
            waitFF = new WebDriverWait(driverFF, TimeSpan.FromSeconds(2));
            Test10(driverFF, waitFF, "rgb({0}, {1}, {2})", "900");
            driverFF.Quit();
            driverFF = null;
        }


        [Test]
        [Obsolete]
        private void Test10(IWebDriver driver, WebDriverWait wait, string rgbTemplate, string fontWeight)
        {
            driver.Url = "http://localhost:8080/litecart/";

            var productName = driver.FindElements(By.CssSelector("div#box-campaigns div.content div.name"));
            var productNameText = productName[0].Text;

            var RegularPrice = driver.FindElement(By.CssSelector("div#box-campaigns div.content div.price-wrapper s.regular-price"));
            var RegularPriceText = RegularPrice.Text;
            var RegularPriceColor = RegularPrice.GetCssValue("color");
            var RegularPriceDecoration = RegularPrice.GetCssValue("text-decoration").Contains("line-through");
            var RegularPriceFontSize = RegularPrice.GetCssValue("font-size");

            var CampaignPrice = driver.FindElement(By.CssSelector("div#box-campaigns div.content div.price-wrapper strong.campaign-price"));
            var CampaignPriceText = CampaignPrice.Text;
            var CampaignPriceColor = CampaignPrice.GetCssValue("color");
            var CampaignPriceDecoration = CampaignPrice.GetCssValue("font-weight").Contains(fontWeight);
            var CampaignPriceFontSize = CampaignPrice.GetCssValue("font-size");

            var divElements = driver.FindElements(By.CssSelector("div#box-campaigns div.image-wrapper"));
            divElements[0].Click();
            wait.Until(ExpectedConditions.TitleIs("Yellow Duck | Subcategory | Rubber Ducks | My Store"));
            var productNameUpd = driver.FindElement(By.CssSelector("div#box-product h1.title"));
            var productNameUpdText = productNameUpd.Text;

            var RegularPriceUpd = driver.FindElement(By.CssSelector("div#box-product div.content div.information div.price-wrapper s.regular-price"));
            var RegularPriceUpdText = RegularPriceUpd.Text;
            var RegularPriceUpdColor = RegularPriceUpd.GetCssValue("color");
            var RegularPriceUpdDecoration = RegularPriceUpd.GetCssValue("text-decoration").Contains("line-through");
            var RegularPriceFontSizeUpd = RegularPriceUpd.GetCssValue("font-size");

            var CampaignPriceUpd = driver.FindElement(By.CssSelector("div#box-product div.content div.information div.price-wrapper strong.campaign-price"));
            var CampaignPriceUpdText = CampaignPriceUpd.Text;
            var CampaignPriceUpdColor = CampaignPriceUpd.GetCssValue("color");
            var CampaignPriceUpdDecoration = CampaignPriceUpd.GetCssValue("font-weight").Contains("700");
            var CampaignPriceFontSizeUpd = CampaignPriceUpd.GetCssValue("font-size");

            Assert.AreEqual(productNameText, productNameUpdText);
            Assert.AreEqual(RegularPriceText, RegularPriceUpdText);
            Assert.AreEqual(CampaignPriceText, CampaignPriceUpdText);
            Assert.AreEqual(string.Format(rgbTemplate, 119, 119, 119), RegularPriceColor);
            Assert.AreEqual(string.Format(rgbTemplate, 102, 102, 102), RegularPriceUpdColor);
            Assert.AreEqual(string.Format(rgbTemplate, 204, 0, 0), CampaignPriceColor);
            Assert.AreEqual(string.Format(rgbTemplate, 204, 0, 0), CampaignPriceUpdColor);
            Assert.IsTrue(RegularPriceDecoration);
            Assert.IsTrue(RegularPriceUpdDecoration);
            Assert.IsTrue(CampaignPriceDecoration);
            Assert.IsTrue(CampaignPriceUpdDecoration);
            Assert.Greater(CampaignPriceFontSize, RegularPriceFontSize);
            Assert.Greater(CampaignPriceFontSizeUpd, RegularPriceFontSizeUpd);
        }
    }
}

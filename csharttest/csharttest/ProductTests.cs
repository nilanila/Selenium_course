using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;
using System.Threading;
using System.Drawing;
using System.Linq;
using csharttest.Pages;

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
        public void Test8()
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
        public void Test10_Chrome()
        {
            driverChrome = new ChromeDriver();
            waitChrome = new WebDriverWait(driverChrome, TimeSpan.FromSeconds(2));
            Test10(driverChrome, waitChrome, "700");
            driverChrome.Quit();
            driverChrome = null;
        }

        [Test]
        [Obsolete]
        public void Test10_FF()
        {
            driverFF = new FirefoxDriver();
            waitFF = new WebDriverWait(driverFF, TimeSpan.FromSeconds(2));
            Test10(driverFF, waitFF, "900");
            driverFF.Quit();
            driverFF = null;
        }


        [Obsolete]
        private void Test10(IWebDriver driver, WebDriverWait wait, string fontWeight)
        {
            driver.Url = "http://localhost:8080/litecart/";

            var productName = driver.FindElements(By.CssSelector("div#box-campaigns div.content div.name"));
            var productNameText = productName[0].Text;

            var RegularPrice = driver.FindElement(By.CssSelector("div#box-campaigns div.content div.price-wrapper s.regular-price"));
            var RegularPriceText = RegularPrice.Text;
            var RegularPriceColorString = RegularPrice.GetCssValue("color");
            var RegularPriceColor = GetColorFromStringRgb(RegularPriceColorString);
            var RegularPriceDecoration = RegularPrice.GetCssValue("text-decoration").Contains("line-through");
            var RegularPriceFontSize = RegularPrice.GetCssValue("font-size");

            var CampaignPrice = driver.FindElement(By.CssSelector("div#box-campaigns div.content div.price-wrapper strong.campaign-price"));
            var CampaignPriceText = CampaignPrice.Text;
            var CampaignPriceColorString = CampaignPrice.GetCssValue("color");
            var CampaignPriceColor = GetColorFromStringRgb(CampaignPriceColorString);
            var CampaignPriceDecoration = CampaignPrice.GetCssValue("font-weight").Contains(fontWeight);
            var CampaignPriceFontSize = CampaignPrice.GetCssValue("font-size");

            var divElements = driver.FindElements(By.CssSelector("div#box-campaigns div.image-wrapper"));
            divElements[0].Click();
            wait.Until(ExpectedConditions.TitleIs("Yellow Duck | Subcategory | Rubber Ducks | My Store"));
            var productNameUpd = driver.FindElement(By.CssSelector("div#box-product h1.title"));
            var productNameUpdText = productNameUpd.Text;

            var RegularPriceUpd = driver.FindElement(By.CssSelector("div#box-product div.content div.information div.price-wrapper s.regular-price"));
            var RegularPriceUpdText = RegularPriceUpd.Text;
            var RegularPriceUpdColorString = RegularPriceUpd.GetCssValue("color");
            var RegularPriceUpdColor = GetColorFromStringRgb(RegularPriceUpdColorString);
            var RegularPriceUpdDecoration = RegularPriceUpd.GetCssValue("text-decoration").Contains("line-through");
            var RegularPriceFontSizeUpd = RegularPriceUpd.GetCssValue("font-size");

            var CampaignPriceUpd = driver.FindElement(By.CssSelector("div#box-product div.content div.information div.price-wrapper strong.campaign-price"));
            var CampaignPriceUpdText = CampaignPriceUpd.Text;
            var CampaignPriceUpdColorString = CampaignPriceUpd.GetCssValue("color");
            var CampaignPriceUpdColor = GetColorFromStringRgb(CampaignPriceUpdColorString);
            var CampaignPriceUpdDecoration = CampaignPriceUpd.GetCssValue("font-weight").Contains("700");
            var CampaignPriceFontSizeUpd = CampaignPriceUpd.GetCssValue("font-size");

            Assert.AreEqual(productNameText, productNameUpdText);
            Assert.AreEqual(RegularPriceText, RegularPriceUpdText);
            Assert.AreEqual(CampaignPriceText, CampaignPriceUpdText);
            Assert.IsTrue(RegularPriceColor.R == RegularPriceColor.G &&
                          RegularPriceColor.R == RegularPriceColor.B);
            Assert.IsTrue(RegularPriceUpdColor.R == RegularPriceUpdColor.G &&
                          RegularPriceUpdColor.R == RegularPriceUpdColor.B);
            Assert.IsTrue(CampaignPriceColor.G == CampaignPriceColor.B);
            Assert.IsTrue(CampaignPriceUpdColor.G == CampaignPriceUpdColor.B);
            Assert.IsTrue(RegularPriceDecoration);
            Assert.IsTrue(RegularPriceUpdDecoration);
            Assert.IsTrue(CampaignPriceDecoration);
            Assert.IsTrue(CampaignPriceUpdDecoration);
            Assert.Greater(CampaignPriceFontSize, RegularPriceFontSize);
            Assert.Greater(CampaignPriceFontSizeUpd, RegularPriceFontSizeUpd);
        }


        [Test]
        [Obsolete]
        public void Test11()
        {
            driverChrome = new ChromeDriver();
            driverChrome.Url = "http://litecart.stqa.ru/en/";
            waitChrome = new WebDriverWait(driverChrome, TimeSpan.FromSeconds(2));
            var email = GenerateEmail();
            var password = "password_test_111";
            var trElements = driverChrome.FindElements(By.CssSelector("div.content form table tr"));
            var tdElements = trElements[4];
            var newCustButton = tdElements.FindElement(By.CssSelector("a"));
            newCustButton.Click();
            
            EnterTextToInput(driverChrome, "firstname", "Nila");
            EnterTextToInput(driverChrome, "lastname", "Tikhonova");
            EnterTextToInput(driverChrome, "address1", "ulitsa");
            EnterTextToInput(driverChrome, "postcode", "10010");
            EnterTextToInput(driverChrome, "city", "Voronezh");

            var country = driverChrome.FindElement(By.Name("country_code"));
            var selectElement = new SelectElement(country);
            selectElement.SelectByValue("US");

            EnterTextToInput(driverChrome, "email", email);

            EnterTextToInput(driverChrome, "phone", "+7123123123");
            EnterTextToInput(driverChrome, "password", password);
            EnterTextToInput(driverChrome, "confirmed_password", password);

            driverChrome.FindElement(By.CssSelector("button[name='create_account']")).Click();

            waitChrome.Until(ExpectedConditions.TitleIs("Online Store | My store"));
            Thread.Sleep(1000);

            LogOut(driverChrome);

            EnterTextToInput(driverChrome, "email", email);
            EnterTextToInput(driverChrome, "password", password);

            var loginButton = driverChrome.FindElement(By.CssSelector("button[name='login']"));
            loginButton.Click();

            LogOut(driverChrome);
        }

        [Test]
        [Obsolete]
        public void Test13()
        {
            driverChrome = new ChromeDriver();
            driverChrome.Url = "http://litecart.stqa.ru/en/";
            waitChrome = new WebDriverWait(driverChrome, TimeSpan.FromSeconds(10));
            
            WaitForLoad(driverChrome, waitChrome);

            for (int i = 0; i <= 2; i++)
            {
                AddMostPopularToBasket(driverChrome, waitChrome, i);
            }

            driverChrome.FindElement(By.CssSelector("div#cart a.link")).Click();

            WaitForLoad(driverChrome, waitChrome);

            Thread.Sleep(1000);

            for (int i = 0; i <= 2; i++)
            {
                var removedElement = waitChrome.Until(ExpectedConditions.ElementExists(By.Name("remove_cart_item")));
                removedElement.Click();
                waitChrome.Until(ExpectedConditions.StalenessOf(removedElement));
            }
        }

        [Test]
        [Obsolete]
        public void Test13_OOP()
        {
            driverChrome = new ChromeDriver();
            driverChrome.Url = "http://litecart.stqa.ru/en/";
            waitChrome = new WebDriverWait(driverChrome, TimeSpan.FromSeconds(5));

            var mainPage = new MainPage(driverChrome, waitChrome);
            for (var i = 0; i <= 2; i++)
            {
                var productPage = mainPage.GoToNthMostPopularProduct(i);
                productPage.AddProductToCart();
                mainPage = productPage.GoToMainPage();
            }

            var cartPage = mainPage.GoToCart();
            cartPage.RemoveProducts();
        }

        [Test]
        public void TestGettingColor()
        {
            var color = GetColorFromStringRgb("rgb(123, 123, 123)");
            Assert.IsTrue(color.R == 123);
            Assert.IsTrue(color.G == 123);
            Assert.IsTrue(color.B == 123);

            var color2 = GetColorFromStringRgb("rgba(123, 123, 123, 123)");
            Assert.IsTrue(color2.A == 123);
            Assert.IsTrue(color2.R == 123);
            Assert.IsTrue(color2.G == 123);
            Assert.IsTrue(color2.B == 123);
        }

        [TearDown]
        public void Stop()
        {
            if (driverChrome != null)
            {
                driverChrome.Quit();
                driverChrome = null;
            }

            if (driverFF != null)
            {
                driverFF.Quit();
                driverFF = null;
            }
        }

        private static Color GetColorFromStringRgb(string rgb)
        {
            var openIndex = rgb.IndexOf('(');
            var closeIndex = rgb.IndexOf(')');
            var colorsString = rgb.Substring(openIndex + 1, closeIndex - openIndex - 1); // "123, 123, 123"
            string[] colorNums = colorsString.Split(','); // ["123"," 123"," 123"]

            for(var i = 0; i < colorNums.Length; i++) // ["123","123","123"]
            {
                colorNums[i] = colorNums[i].Trim();
            }

            int[] nums = new int[colorNums.Length]; // [,,]
            for(var i = 0; i < colorNums.Length; i++) // [123,123,123]
            {
                int num = int.Parse(colorNums[i]);
                nums[i] = num;
            }

            //var nums = colorsString.Split(',').Select(s => int.Parse(s.Trim())).ToList();

            if(nums.Count() == 3)
            {
                return Color.FromArgb(nums[0], nums[1], nums[2]);
            }
            else
            {
                return Color.FromArgb(nums[3], nums[0], nums[1], nums[2]);
            }

            //return nums.Count() == 3
            //    ? Color.FromArgb(nums[0], nums[1], nums[2])
            //    : Color.FromArgb(nums[3], nums[0], nums[1], nums[2]);
        }

        [Obsolete]
        private static void AddMostPopularToBasketMultyLayer(IWebDriver driver, WebDriverWait wait, int index)
        {
            var mainPage = new MainPage(driver, wait);
            var productPage = mainPage.GoToNthMostPopularProduct(index);
            productPage.GoToMainPage();
        }

        [Obsolete]
        private static void AddMostPopularToBasket(IWebDriver driver, WebDriverWait wait, int index)
        {
            driver.FindElements(By.CssSelector("div#box-most-popular div.content ul li"))[index].FindElement(By.CssSelector("a.link")).Click();
            WaitForLoad(driver, wait);
            Thread.Sleep(1000);

            var size = driver.FindElements(By.Name("options[Size]"));
            if (size.Count > 0)
            {
                var selectElement = new SelectElement(size[0]);
                selectElement.SelectByValue("Large");
            }

            wait.Until(ExpectedConditions.ElementExists(By.Name("add_cart_product"))).Click();
            //driver.FindElement(By.Name("add_cart_product")).Click();
            var quantityElement = driver.FindElement(By.CssSelector("div#cart a.content span.quantity"));
            wait.Until(ExpectedConditions.TextToBePresentInElement(quantityElement, (index + 1).ToString()));
            driver.Navigate().Back();
            WaitForLoad(driver, wait);
        }

        private static void WaitForLoad(IWebDriver driver, WebDriverWait wait)
        {
            wait.Until(wd => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").ToString() == "complete");
        }

        private static void LogOut(IWebDriver driver)
        {
            var trElementsLogout = driver.FindElements(By.CssSelector("div#box-account div.content ul.list-vertical li"));
            var aLogout = trElementsLogout[3].FindElement(By.CssSelector("a"));
            aLogout.Click();
        }


        private static string GenerateEmail()
        {
            var guid = Guid.NewGuid();
            return $"{guid}@nila.com";
        }

        private static void EnterTextToInput(IWebDriver driver, string inputName, string text)
        {
            Thread.Sleep(500);
            driver.FindElement(By.CssSelector($"div.content form table tbody input[name='{inputName}']"))
                .SendKeys(text);
        }
    }

}
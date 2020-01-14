using System;
using System.Collections.ObjectModel;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        }

        [Test]
        [Obsolete]
        public void Login()
        {
            LoginInternal();

        }

        private void LoginInternal()
        {
            driver.Url = "http://localhost:8080/litecart/admin/";
            IWebElement element = wait.Until(d => d.FindElement(By.Name("username")));
            driver.FindElement(By.Name("username")).SendKeys("admin");
            IWebElement elementpswd = wait.Until(d => d.FindElement(By.Name("password")));
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("remember_me")).Click();
            driver.FindElement(By.Name("login")).Click();
            wait.Until(ExpectedConditions.TitleIs("My Store"));
        }

        [Test]
        [Obsolete]
        public void Task7_CheckMenu()
        {
            LoginInternal();

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
        public void Task9_CheckSorting()
        {
            LoginInternal();
            driver.Url = "http://localhost:8080/litecart/admin/?app=countries&doc=countries";
            var trElementsSelector = "tr.row";
            var trElements = driver.FindElements(By.CssSelector(trElementsSelector));

            for (var i = 0; i < trElements.Count - 1; i++)
            {
                var currentCountry = GetCountryName(trElements, i);
                var nextCountry = GetCountryName(trElements, i+1);
                var textCompare = string.Compare(currentCountry, nextCountry);
                Assert.AreEqual(textCompare, -1);
            }
        }

        private string GetCountryName(ReadOnlyCollection<IWebElement> trElements, int i) 
        { 
            var tdElements = trElements[i].FindElements(By.CssSelector("td"));
            var tdCountry = tdElements[4];
            var aElement = tdCountry.FindElement(By.CssSelector("a"));
            var aText = aElement.Text;
            return aText;
        }





        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }


   

    }
}

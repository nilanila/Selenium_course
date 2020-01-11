using System;
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

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }


        private void LoginInternal ()
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

    }
}

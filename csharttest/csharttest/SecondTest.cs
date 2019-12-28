using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;


namespace csharttest
{
    [TestFixture]
    public class SecondTest
    {
        private IWebDriver Driver;
        private WebDriverWait Wait;

        [SetUp]
        public void start2()
        {
            Driver = new FirefoxDriver();
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void StartFF()
        {
            Driver.Url = "http://www.google.com/";
            IWebElement element = Wait.Until(d => d.FindElement(By.Name("q")));
            Driver.FindElement(By.Name("q")).SendKeys("webdriver");
            Thread.Sleep(300);
            Driver.FindElement(By.Name("btnK")).Click();
            Wait.Until(ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }


        [TearDown]
        public void FFstop()
        {
            Driver.Quit();
            Driver = null;
        }
    }
}
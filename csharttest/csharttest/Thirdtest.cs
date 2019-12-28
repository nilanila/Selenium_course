﻿using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;


//FirefoxOptions options = new FirefoxOptions();
//options.BrowserExecutableLocation = @"c:\Program Files (x86)\Nightly\firefox.exe";
//IWebDriver driver = new FirefoxDriver(options);
//Applications/Firefox Nightly


namespace csharttest
{
    [TestFixture]
    public class Thirdtest
    {
        private IWebDriver Driver;
        private WebDriverWait Wait;

        [SetUp]
        public void start2()
        {

            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = @"/Applications/Firefox Nightly.app";
            Driver = new FirefoxDriver(options);
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
            Wait.Until(ExpectedConditions.TitleIs("webdriver - Google-Suche"));
        }


        [TearDown]
        public void FFstop()
        {
            Driver.Quit();
            Driver = null;
        }
    }
}
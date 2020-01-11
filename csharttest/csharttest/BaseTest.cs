using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharttest
{
    public class BaseTest
    {
        public static void DoTest(IWebDriver driver, WebDriverWait wait, string finalTitle)
        {
            driver.Url = "http://www.google.com/";
            IWebElement element = wait.Until(d => d.FindElement(By.Name("q")));
            driver.FindElement(By.Name("q")).SendKeys("webdriver");
            Thread.Sleep(300);
            driver.FindElement(By.Name("btnK")).Click();
            wait.Until(ExpectedConditions.TitleIs(finalTitle));
        }
    }
}

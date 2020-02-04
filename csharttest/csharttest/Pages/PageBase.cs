using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharttest.Pages
{
    public abstract class PageBase
    {
        protected IWebDriver _driver;
        protected WebDriverWait _wait;

        protected PageBase(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;

            WaitForLoad();
        }

        public MainPage GoToMainPage()
        {
            // TODO: take from config or environment
            _driver.Navigate().GoToUrl("http://litecart.stqa.ru/en/");
            return CreateMainPage();
        }

        protected ReadOnlyCollection<IWebElement> GetElementsByCssSelector(string selector)
        {
            return _driver.FindElements(By.CssSelector(selector));
        }

        protected IWebElement GetElementByCssSelector(string selector)
        {
            return _driver.FindElement(By.CssSelector(selector));
        }

        protected IWebElement GetNthElementByCssSelector(string selector, int n)
        {
            return _driver.FindElements(By.CssSelector(selector))[n];
        }

        protected IWebElement GetNthElementLookup(params SelectionStep[] steps)
        {
            ISearchContext current = _driver;

            foreach(var step in steps)
            {
                current = step.N.HasValue
                    ? current.FindElements(By.CssSelector(step.Selector))[step.N.Value]
                    : current.FindElement(By.CssSelector(step.Selector));
            }

            return current as IWebElement;
        }

        protected IWebElement GetElementByName(string name)
        {
            var elements = _driver.FindElements(By.Name(name));
            return elements.Count > 0 ? elements[0] : null;
        }

        protected ReadOnlyCollection<IWebElement> GetElementsByName(string name)
        {
            return _driver.FindElements(By.Name(name));
        }

        protected IWebElement WainUntilExistsByName(string name)
        {
            return _wait.Until(ExpectedConditions.ElementExists(By.Name(name)));
        }

        protected void WaitForLoad()
        {
            _wait.Until(wd => ((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState")
                .ToString() == "complete");

            Thread.Sleep(500);
        }

        protected MainPage CreateMainPage()
        {
            return new MainPage(_driver, _wait);
        }

        protected ProductPage CreateProductPage()
        {
            return new ProductPage(_driver, _wait);
        }

        protected CartPage CreateCartPage()
        {
            return new CartPage(_driver, _wait);
        }
    }
}

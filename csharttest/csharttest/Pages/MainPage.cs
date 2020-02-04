using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharttest.Pages
{
    public class MainPage : ProductBasePage
    {
        public MainPage(IWebDriver driver, WebDriverWait wait)
            : base(driver, wait)
        {
            var test = GetElementsByName("huy_sobachiy");
        }

        public ProductPage GoToNthMostPopularProduct(int n)
        {
            GetNthElementLookup(new []{ new SelectionStep("div#box-most-popular div.content ul li", n),
                                        new SelectionStep("a.link")}).Click();
            return CreateProductPage();
        }
    }
}

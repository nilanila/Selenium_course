using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharttest.Pages
{
    public abstract class ProductBasePage : PageBase
    {
        private IWebElement _quantityElement;

        protected ProductBasePage(IWebDriver driver, WebDriverWait wait)
            : base(driver, wait)
        {
            _quantityElement = GetElementByCssSelector("div#cart a.content span.quantity");
        }

        protected int CartQuantity => int.Parse(_quantityElement.Text);

        protected void WaitUntilCartQuantityIs(int quantity)
        {
            _wait.Until(ExpectedConditions.TextToBePresentInElement(_quantityElement, quantity.ToString()));
        }

        public CartPage GoToCart()
        {
            GetElementByCssSelector("div#cart a.link").Click();
            return CreateCartPage();
        }
    }
}

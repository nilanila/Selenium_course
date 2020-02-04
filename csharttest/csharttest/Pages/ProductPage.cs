using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharttest.Pages
{
    public class ProductPage : ProductBasePage
    {
        private IWebElement _addCartProduct;

        public ProductPage(IWebDriver driver, WebDriverWait wait)
            : base(driver, wait)
        {
            var size = GetElementsByName("options[Size]");
            if (size.Count > 0)
            {
                var selectElement = new SelectElement(size[0]);
                selectElement.SelectByValue("Large");
            }

            _addCartProduct = WainUntilExistsByName("add_cart_product");
        }

        public void AddProductToCart()
        {
            var quantity = CartQuantity;
            _addCartProduct.Click();
            WaitUntilCartQuantityIs(quantity + 1);
        }
    }
}

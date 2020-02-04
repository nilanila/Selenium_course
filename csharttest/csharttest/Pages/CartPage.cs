using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace csharttest.Pages
{
    public class CartPage : PageBase
    {
        public CartPage(IWebDriver driver, WebDriverWait wait)
            : base(driver, wait)
        {
        }

        public void RemoveProducts()
        {
            IWebElement currentRemoveButton = WainUntilExistsByName("remove_cart_item");
            while (currentRemoveButton != null)
            {
                currentRemoveButton.Click();
                _wait.Until(ExpectedConditions.StalenessOf(currentRemoveButton));
                
                Thread.Sleep(1000);

                currentRemoveButton = GetElementByName("remove_cart_item");
            }
        }
    }
}

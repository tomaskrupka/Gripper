using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser.Selenium
{
    public class SeleniumChromeBrowserWindow : IBrowserWindow
    {
        private ChromeDriver _driver;
        private INavigation _navigation;
        public SeleniumChromeBrowserWindow(ChromeDriver driver)
        {
            _driver = driver;
            _navigation = _driver.Navigate();
        }
        public void Dispose()
        {
            _driver.Dispose();
        }

        public Task EnterFullScreenAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<string> ExecuteScriptAsync(string script)
        {
            var result = _driver.ExecuteScript(script);
            return result?.ToString() ?? "No result.";
        }

        public async Task<IElement> FindElementByCssSelectorAsync(string cssSelector)
        {
            var element = _driver.FindElement(By.CssSelector(cssSelector));
            return new SeleniumWebElement(element);
        }

        public async Task GoToUrlAsync(string address)
        {
            _navigation.GoToUrl(address);
        }

        public Task ReloadAsync()
        {

            throw new NotImplementedException();
        }
    }
}

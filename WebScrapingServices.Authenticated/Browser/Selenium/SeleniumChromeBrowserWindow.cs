using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser.Selenium
{
    public class SeleniumChromeBrowserWindow : IBrowserWindow
    {
        private ILogger _logger;
        private ChromeDriver _driver;
        private INavigation _navigation;
        public SeleniumChromeBrowserWindow(ILogger<SeleniumChromeBrowserWindow> logger, ChromeDriver driver)
        {
            _logger = logger;
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
            try
            {
                var result = _driver.ExecuteScript(script);
                return result?.ToString() ?? "No result.";
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.ToString());
                throw;
            }
        }

        public async Task<IElement?> FindElementByCssSelectorAsync(string cssSelector)
        {
            var element = _driver.FindElement(By.CssSelector(cssSelector));

            if (element == null)
            {
                return null;
            }
            else
            {
                return new SeleniumWebElement(element);
            }
        }

        public async Task GoToUrlAsync(string address)
        {
            _navigation.GoToUrl(address);
        }

        public async Task ReloadAsync()
        {
            _navigation.Refresh();
        }
    }
}

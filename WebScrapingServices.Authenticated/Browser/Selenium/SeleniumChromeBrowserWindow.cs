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

        public Task<string> ExecuteScriptAsync(string script)
        {
            throw new NotImplementedException();
        }

        public Task<IElement> FindElementByCssSelectorAsync(string cssSelector)
        {
            throw new NotImplementedException();
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

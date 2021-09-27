using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Threading;
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

        public string Url => _driver.Url;

        public void Dispose()
        {
            _driver.Dispose();
        }

        public Task EnterFullScreenAsync()
        {
            throw new NotImplementedException();
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

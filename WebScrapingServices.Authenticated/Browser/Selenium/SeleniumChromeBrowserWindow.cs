using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser.Selenium
{
    public class SeleniumChromeBrowserWindow : IBrowserWindow
    {
        private ChromeDriver _driver;
        public SeleniumChromeBrowserWindow(ChromeDriver driver)
        {
            _driver = driver;
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

        public Task GoToUrlAsync(string address)
        {
            throw new NotImplementedException();
        }

        public Task ReloadAsync()
        {
            throw new NotImplementedException();
        }
    }
}

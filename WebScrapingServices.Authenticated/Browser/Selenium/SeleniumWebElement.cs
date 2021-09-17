﻿using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser.Selenium
{
    public class SeleniumWebElement : IElement
    {
        private IWebElement _webElement;
        public SeleniumWebElement(IWebElement webElement)
        {
            _webElement = webElement;
        }
        public async Task ClickAsync()
        {
            _webElement.Click();
        }

        public async Task SendKeysAsync(string keys)
        {
            _webElement.SendKeys(keys);
        }

        public async Task SendKeysAsync(string keys, int delayEachMs)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                await Task.Delay(delayEachMs);
                _webElement.SendKeys(keys[i].ToString());
            }
        }

        public async Task SendSpecialKeyAsync(SpecialKey key)
        {
            _webElement.SendKeys(key.AsSeleniumKeys());
        }
    }
}

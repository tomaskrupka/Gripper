using OpenQA.Selenium;
using System;

namespace Gripper.Authenticated.Browser.Selenium
{
    public static class SeleniumExtensions
    {
        public static string AsSeleniumKeys(this SpecialKey key)
        {
            return key switch
            {
                SpecialKey.Enter => Keys.Enter,
                SpecialKey.Backspace => Keys.Backspace,
                SpecialKey.Escape => Keys.Escape,
                SpecialKey.Tab => Keys.Tab,
                _ => throw new NotImplementedException()
            };
        }
    }
}

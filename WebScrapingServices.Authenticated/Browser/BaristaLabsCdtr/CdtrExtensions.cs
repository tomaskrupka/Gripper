using System;
using BaristaLabs.ChromeDevTools.Runtime.Input;

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCdtr
{
    internal static class CdtrExtensions
    {
        // For complete map see https://pkg.go.dev/github.com/unixpickle/muniverse/chrome
        internal static DispatchKeyEventCommand ToDispatchKeyEventCommand(this SpecialKey specialKey)
        {
            return specialKey switch
            {
                SpecialKey.Backspace => new DispatchKeyEventCommand { Code = "Backspace", Type = "char" },
                SpecialKey.Enter => new DispatchKeyEventCommand { Code = "Enter", Type = "char" },
                SpecialKey.Escape => new DispatchKeyEventCommand { NativeVirtualKeyCode = 27, WindowsVirtualKeyCode = 27, Type = "char" },
                SpecialKey.Tab => new DispatchKeyEventCommand { Code = "Tab", Type = "char" },
                _ => throw new NotImplementedException()
            };

        }
    }

}

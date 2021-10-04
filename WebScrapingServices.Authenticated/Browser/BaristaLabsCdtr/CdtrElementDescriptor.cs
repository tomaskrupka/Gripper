﻿using System;
using System.Threading.Tasks;
using BaristaLabs.ChromeDevTools.Runtime;
using Runtime = BaristaLabs.ChromeDevTools.Runtime.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.Input;

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCdtr
{
    /// <summary>
    /// This is a description of the element when the DOM was last seen, not a reference to it.
    /// Every call the selector is evaluated against the current state of the DOM.
    /// Two subsequent calls to the same <see cref="CdtrElementDescriptor"/> instance may therefore result in interactions with two different objects in the DOM.
    /// </summary>
    public class CdtrElementDescriptor : IElement
    {
        private string _cssSelector;
        private ChromeSession _chromeSession;
        private IJsBuilder _jsBuilder;
        public CdtrElementDescriptor(string cssSelector, ChromeSession chromeSession, IJsBuilder jsBuilder)
        {
            _cssSelector = cssSelector;
            _chromeSession = chromeSession;
            _jsBuilder = jsBuilder;
        }
        public async Task ClickAsync()
        {
            await _chromeSession.Runtime.Evaluate(new Runtime.EvaluateCommand
            {
                Expression = _jsBuilder.ClickFirstByCssSelector(_cssSelector)
            });
        }

        public async Task SendKeysAsync(string keys)
        {
            await _chromeSession.Input.InsertText(new InsertTextCommand
            {
                Text = keys
            });
            //await _chromeSession.Input.DispatchKeyEvent(new BaristaLabs.ChromeDevTools.Runtime.Input.DispatchKeyEventCommand
            //{
            //    Text = keys
            //});
        }

        public async Task SendKeysAsync(string keys, TimeSpan delayAfterStroke)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                var dispatchKeyEventResponse = await _chromeSession.Input.DispatchKeyEvent(new DispatchKeyEventCommand
                {
                    Type = "char",
                    Text = key.ToString()
                });
                await Task.Delay(delayAfterStroke);
            }
        }

        public async Task SendSpecialKeyAsync(SpecialKey key)
        {
            await _chromeSession.Input.DispatchKeyEvent(key.ToDispatchKeyEventCommand());
        }
    }

}

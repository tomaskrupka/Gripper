using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BaristaLabs.ChromeDevTools.Runtime;
using System.Net.Http;
using Newtonsoft.Json;
using Page = BaristaLabs.ChromeDevTools.Runtime.Page;
using Runtime = BaristaLabs.ChromeDevTools.Runtime.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.Browser;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using BaristaLabs.ChromeDevTools.Runtime.Input;

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCdtr
{
    public class CdtrChromeClient : IWebClient
    {
        private CdtrRdpSession _rdpSession;
        private CdtrChromeBrowserWindow _browserWindow;
        private ChromeSession _chromeSession;

        private ILogger _logger;

        private ILoggerFactory _loggerFactory;
        private IJsBuilder _jsBuilder;

        public IRdpSession RdpClient => _rdpSession;

        public IBrowserWindow BrowserWindow => _browserWindow;

        public CookieContainer Cookies => throw new NotImplementedException();

        public event EventHandler WebClientEvent;

        public CdtrChromeClient(ILoggerFactory loggerFactory, IJsBuilder jsBuilder, WebClientSettings settings)
        {
            _loggerFactory = loggerFactory;
            _jsBuilder = jsBuilder;
            _logger = loggerFactory.CreateLogger<CdtrChromeClient>();
            (_chromeSession, _browserWindow, _rdpSession) = LaunchAndConnectAsync(settings).Result;
        }

        private async Task<(ChromeSession, CdtrChromeBrowserWindow, CdtrRdpSession)> LaunchAndConnectAsync(WebClientSettings settings)
        {
            // see https://github.com/BaristaLabs/chrome-dev-tools-runtime/blob/master/ChromeDevToolsCLI/Program.cs

            // "user-data-dir=C:\SeleniumProfiles\" + config.UserName + "\\"

            var browserArgs = new StringBuilder()
                .Append("--remote-debugging-port=").Append(settings.RemoteDebuggingPort)
                .Append(@" --user-data-dir=C:\CdtrProfiles\").Append(settings.UserProfileName).Append('\\');

            Process.Start(settings.BrowserLocation, browserArgs.ToString());

            using var httpClient = new HttpClient();
            var remoteSessions = await httpClient.GetStringAsync("http://localhost:9223/json");
            var sessionInfos = JsonConvert.DeserializeObject<List<ChromeSessionInfo>>(remoteSessions);

            var chromeSession = new ChromeSession(sessionInfos.First(x => x.Type == "page").WebSocketDebuggerUrl);

            long executionContextId = -1;
            var s = new SemaphoreSlim(0, 1);

            //Navigate to homepage.
            var navigateResult = await chromeSession.Page.Navigate(new Page.NavigateCommand
            {
                Url = "https://www.google.com"
            });

            //Find execution context id.
            chromeSession.Runtime.SubscribeToExecutionContextCreatedEvent((e) =>
            {
                var auxData = e.Context.AuxData as JObject;
                var frameId = auxData?["frameId"]?.Value<string>();

                if (frameId == navigateResult.FrameId)
                {
                    executionContextId = e.Context.Id;
                    s.Release();
                }
            });

            var enableRuntimeResult = await chromeSession.Runtime.Enable(new Runtime.EnableCommand());

            await s.WaitAsync();

            var rdpSession = new CdtrRdpSession(chromeSession);

            var browserLogger = _loggerFactory.CreateLogger<CdtrChromeBrowserWindow>();
            var browserWindow = new CdtrChromeBrowserWindow(browserLogger, chromeSession);

            return (chromeSession, browserWindow, rdpSession);
        }

        public void Dispose()
        {
            try
            {
                _rdpSession.Dispose();
            }
            catch (ObjectDisposedException)
            {
                _logger.LogDebug("ObjectDisposedException when disposing {object} at {this}.", nameof(_rdpSession), nameof(CdtrChromeClient));
            }

            try
            {
                _browserWindow.Dispose();
            }
            catch (ObjectDisposedException)
            {
                _logger.LogDebug("ObjectDisposedException when disposing {object} at {this}.", nameof(_browserWindow), nameof(CdtrChromeClient));
            }

            try
            {
                _chromeSession.Dispose();
            }
            catch (ObjectDisposedException)
            {
                _logger.LogDebug("ObjectDisposedException when disposing {object} at {this}.", nameof(_chromeSession), nameof(CdtrChromeClient));
            }
        }

        public async Task<string> ExecuteScriptAsync(string script)
        {
            var result = await _chromeSession.Runtime.Evaluate(new Runtime.EvaluateCommand
            {
                Expression = script
            });

            return result.Result.Description;
        }

        public async Task<IElement?> FindElementByCssSelectorAsync(string cssSelector)
        {

            try
            {
                var result = await _chromeSession.Runtime.Evaluate(new Runtime.EvaluateCommand
                {
                    Expression = _jsBuilder.DocumentQuerySelector(cssSelector)
                });

                if (result.ExceptionDetails != null)
                {
                    return null;
                }

                var objectId = result.Result.ObjectId;

                return new CdtrElement(objectId, _chromeSession, _jsBuilder);

            }
            catch (Exception e)
            {
                ;
                throw new NotImplementedException();
            }

        }

        public Task<IElement?> WaitUntilElementPresentAsync(string cssSelector, CancellationToken cancellationToken, PollSettings settings)
        {
            throw new NotImplementedException();
        }
    }

    public class CdtrElement : IElement
    {
        public CdtrElement(string objectId, ChromeSession chromeSession, IJsBuilder jsBuilder)
        {
            throw new NotImplementedException();
        }
        public Task ClickAsync()
        {
            throw new NotImplementedException();
        }

        public Task SendKeysAsync(string keys)
        {
            throw new NotImplementedException();
        }

        public Task SendKeysAsync(string keys, int delayEachMs)
        {
            throw new NotImplementedException();
        }

        public Task SendSpecialKeyAsync(SpecialKey key)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Unlike the Selenium element, this is a description of the element when the DOM was last seen rather than a reference to it.
    /// Every call the selector is evaluated against the current state of the DOM.
    /// Two subsequent calls to the same <see cref="CdtrElementDescriptor"/> may therefore result in interactions with two different objects in the DOM.
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
            await _chromeSession.Input.InsertText(new BaristaLabs.ChromeDevTools.Runtime.Input.InsertTextCommand
            {
                Text = keys
            });
            //await _chromeSession.Input.DispatchKeyEvent(new BaristaLabs.ChromeDevTools.Runtime.Input.DispatchKeyEventCommand
            //{
            //    Text = keys
            //});
        }

        public async Task SendKeysAsync(string keys, int delayEachMs)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                await _chromeSession.Input.DispatchKeyEvent(new BaristaLabs.ChromeDevTools.Runtime.Input.DispatchKeyEventCommand
                {
                    Key = key.ToString()
                });
                await Task.Delay(delayEachMs);
            }
        }

        public async Task SendSpecialKeyAsync(SpecialKey key)
        {
            await _chromeSession.Input.DispatchKeyEvent(key.ToDispatchKeyEventCommand());
        }
    }
    internal static class CdtrExtensions
    {
        // For complete map see https://pkg.go.dev/github.com/unixpickle/muniverse/chrome
        internal static DispatchKeyEventCommand ToDispatchKeyEventCommand(this SpecialKey specialKey)
        {
            return specialKey switch
            {
                SpecialKey.Backspace => new DispatchKeyEventCommand { Code = "Backspace" },
                SpecialKey.Enter => new DispatchKeyEventCommand { Code = "Enter" },
                SpecialKey.Escape => new DispatchKeyEventCommand { NativeVirtualKeyCode = 27, WindowsVirtualKeyCode = 27 },
                SpecialKey.Tab => new DispatchKeyEventCommand { Code = "Tab" }
            };
        }
    }

}

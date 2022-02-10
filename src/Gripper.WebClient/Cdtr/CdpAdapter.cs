using BaristaLabs.ChromeDevTools.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.Page;
using BaristaLabs.ChromeDevTools.Runtime.Runtime;
using Gripper.WebClient.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient.Cdtr
{
    internal class CdpAdapter : ICdpAdapter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private IBrowserManager? _browserManager;
        private readonly WebClientSettings _webClientSettings;
        private ChromeSession? _chromeSession;

        private async Task SubscribeToRdpEventsAsync(CancellationToken cancellationToken)
        {
            await ChromeSession.Network.Enable(
                new BaristaLabs.ChromeDevTools.Runtime.Network.EnableCommand { },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: cancellationToken);

            ChromeSession.Network.SubscribeToRequestWillBeSentEvent(x =>
            {
                WebClientEvent?.Invoke(this, new Network_RequestWillBeSentEventArgs(x.RequestId, x.Request.Headers, x.Request.Method, x.Request.Url));
            });

            await ChromeSession.Page.Enable(
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: cancellationToken);

            ChromeSession.Page.SubscribeToFrameStoppedLoadingEvent(x =>
            {
                _logger.LogDebug("Frame stopped loading: {id}", x.FrameId);
                WebClientEvent?.Invoke(this, new Page_FrameStoppedLoadingEventArgs(x.FrameId));
            });

            await ChromeSession.Runtime.Enable(throwExceptionIfResponseNotReceived: false, cancellationToken: cancellationToken);

            ChromeSession.Runtime.SubscribeToExecutionContextCreatedEvent(x =>
            {
                _logger.LogDebug("execution context created: {id}, {name}, {origin}.", x.Context.Id, x.Context.Name, x.Context.Origin);
                WebClientEvent?.Invoke(this, new Runtime_ExecutionContextCreatedEventArgs(x.Context));
            });

            ChromeSession.Runtime.SubscribeToExecutionContextDestroyedEvent(x =>
            {
                _logger.LogDebug("execution context destroyed: {id}.", x.ExecutionContextId);
                WebClientEvent?.Invoke(this, new Runtime_ExecutionContextDestroyedEventArgs(x.ExecutionContextId));
            });
        }

        /// <summary>
        /// Continuously enforce that events triggered on children iFrames are captured.
        /// Make sure to <see cref="SubscribeToRdpEventsAsync"/>
        /// </summary>
        /// <param name="targetAttachment">What strategy shall be used.</param>
        /// <returns></returns>
        private void SetupTargetAttachment(TargetAttachmentMode targetAttachment)
        {
            switch (targetAttachment)
            {
                case TargetAttachmentMode.Default:
                case TargetAttachmentMode.Auto:
                    WebClientEvent += async (s, e) =>
                    {
                        if (e is Page_FrameStoppedLoadingEventArgs)
                        {
                            await ChromeSession.Target.SetAutoAttach(new BaristaLabs.ChromeDevTools.Runtime.Target.SetAutoAttachCommand
                            {
                                AutoAttach = true,
                                // Setting WaitForDebuggerOnStart = true requires releasing stalled frames by calling Runtime.RunIfWaitingForDebugger
                                WaitForDebuggerOnStart = false,
                                Flatten = true
                            },
                            throwExceptionIfResponseNotReceived: false,

                            // Do not set the cancellationToken to the startup token -- this lambda will be called throughout the app lifetime.
                            // Only set if there is a token managing the app lifecycle.
                            cancellationToken: CancellationToken.None);
                        }
                    };

                    break;

                case TargetAttachmentMode.SeekAndAttach:

                default:
                    throw new NotImplementedException();
            }
        }

        public CdpAdapter(ILoggerFactory loggerFactory, IOptions<WebClientSettings> options)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<CdpAdapter>();
            _webClientSettings = options.Value;
        }

        public event EventHandler<RdpEventArgs>? WebClientEvent;

        public ChromeSession ChromeSession
        {
            get => _chromeSession ??
                throw new ApplicationException(
                    string.Format(
                        "You must first run {0} before accessing the {1}.",
                        nameof(BindAsync),
                        nameof(ChromeSession)));
        }

        public async Task BindAsync(IBrowserManager browserManager)
        {
            var startupCts = new CancellationTokenSource(_webClientSettings.BrowserLaunchTimeoutMs);

            _browserManager = browserManager;
            _logger.LogInformation("{this} ctor binding {chromeSession}...", nameof(CdpAdapter), nameof(ChromeSession));

            _chromeSession = new ChromeSession(
                _loggerFactory.CreateLogger<ChromeSession>(),
                browserManager.DebuggerUrl);

            _logger.LogDebug("{this} ctor subscribing to rdp events...", nameof(CdpAdapter));

            await SubscribeToRdpEventsAsync(startupCts.Token);
            SetupTargetAttachment(_webClientSettings.TargetAttachment);
        }
    }
}

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
    public class CdpAdapter : ICdpAdapter
    {
        private readonly ILogger _logger;
        private readonly IBrowserManager _browserManager;
        private readonly WebClientSettings _webClientSettings;
        private readonly ChromeSession _chromeSession;

        private ConcurrentDictionary<int, ExecutionContextDescription> _executionContexts;
        private Action<FrameStoppedLoadingEvent>? _frameStoppedLoading;

        private async Task SubscribeToRdpEventsAsync(CancellationToken cancellationToken)
        {
            await _chromeSession.Network.Enable(
                new BaristaLabs.ChromeDevTools.Runtime.Network.EnableCommand { },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: cancellationToken);

            _chromeSession.Network.SubscribeToRequestWillBeSentEvent(x =>
            {
                WebClientEvent?.Invoke(this, new Network_RequestWillBeSentEventArgs(x.RequestId, x.Request.Headers, x.Request.Method, x.Request.Url));
            });

            await _chromeSession.Page.Enable(throwExceptionIfResponseNotReceived: false, cancellationToken: cancellationToken);

            _chromeSession.Page.SubscribeToFrameStoppedLoadingEvent(x =>
            {
                _frameStoppedLoading?.Invoke(x);
                WebClientEvent?.Invoke(this, new Page_FrameStoppedLoadingEventArgs(x.FrameId));
            });
            _frameStoppedLoading += x => _logger.LogDebug("Frame stopped loading: {id}", x.FrameId);

            await _chromeSession.Runtime.Enable(throwExceptionIfResponseNotReceived: false, cancellationToken: cancellationToken);

            _chromeSession.Runtime.SubscribeToExecutionContextCreatedEvent(x =>
            {
                _logger.LogDebug("execution context created: {id}, {name}, {origin}.", x.Context.Id, x.Context.Name, x.Context.Origin);
                _executionContexts[(int)x.Context.Id] = x.Context;
            });

            _chromeSession.Runtime.SubscribeToExecutionContextDestroyedEvent(x =>
            {
                _logger.LogDebug("execution context destroyed: {id}.", x.ExecutionContextId);
                var contextId = x.ExecutionContextId;
                _executionContexts.TryRemove((int)contextId, out _);
            });

            await _chromeSession.DOM.Enable(throwExceptionIfResponseNotReceived: false);

            _chromeSession.DOM.SubscribeToSetChildNodesEvent(x =>
            {
                _logger.LogDebug("set child node: Parent: {nodeParent}, nodes: {nodes}", x.ParentId, string.Join(' ', x.Nodes.Select(x => x.NodeId + " " + x.NodeName + " " + x.NodeValue)));
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
                    _frameStoppedLoading += async x =>
                    {
                        await _chromeSession.Target.SetAutoAttach(new BaristaLabs.ChromeDevTools.Runtime.Target.SetAutoAttachCommand
                        {
                            AutoAttach = true,
                            // Setting WaitForDebuggerOnStart = true requires releasing stalled frames by calling Runtime.RunIfWaitingForDebugger
                            WaitForDebuggerOnStart = false,
                            Flatten = true
                        },
                        throwExceptionIfResponseNotReceived: false,

                        // Do not set the cancellationToken to the startup token -- this lambda will be called throughout the app lifetime.
                        // Only change it if there is a cancellationToken managing the app lifecycle.
                        cancellationToken: CancellationToken.None);
                    };

                    break;

                case TargetAttachmentMode.SeekAndAttach:
                default:
                    throw new NotImplementedException();
            }
        }

        private async Task LoopSeekAndAttachTargetsAsync(TimeSpan loopPeriod, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(loopPeriod);
                var targets = await _chromeSession.Target.GetTargets(throwExceptionIfResponseNotReceived: false, cancellationToken: cancellationToken);

                _logger.LogDebug("Found targets: {targetsCount} of which attached: {attachedTargetsCount}", targets.TargetInfos.Length, targets.TargetInfos.Count(x => x.Attached));

                foreach (var target in targets.TargetInfos.Where(x => !x.Attached))
                {
                    _logger.LogDebug("Attaching to target: {targetId}", target.TargetId);
                    await _chromeSession.Target.AttachToTarget(new BaristaLabs.ChromeDevTools.Runtime.Target.AttachToTargetCommand
                    {
                        TargetId = target.TargetId
                    },
                    throwExceptionIfResponseNotReceived: false,
                    cancellationToken: cancellationToken);
                }
            }
        }


        public event EventHandler<RdpEventArgs>? WebClientEvent;

        public CdpAdapter(ILoggerFactory loggerFactory, IOptions<WebClientSettings> options, IBrowserManager browserManager)
        {
            _logger = loggerFactory.CreateLogger<CdpAdapter>();
            _browserManager = browserManager;
            _webClientSettings = options.Value;
            _executionContexts = new ConcurrentDictionary<int, ExecutionContextDescription>();

            var startupCts = new CancellationTokenSource(_webClientSettings.BrowserLaunchTimeoutMs);

            if (_webClientSettings.LaunchBrowser)
            {
                _logger.LogInformation("{this} ctor launching {browserManager}", nameof(CdpAdapter), nameof(browserManager));

                browserManager.LaunchAsync(startupCts.Token).Wait();
            }

            _logger.LogInformation("{this} ctor binding {chromeSession}", nameof(CdpAdapter), nameof(ChromeSession));

            _chromeSession = new ChromeSession(loggerFactory.CreateLogger<ChromeSession>(), browserManager.DebuggerUrl);

            _logger.LogDebug("Subscribing to rdp events.");

            SubscribeToRdpEventsAsync(startupCts.Token).Wait();
            SetupTargetAttachment(_webClientSettings.TargetAttachment);
        }
        public async Task<ChromeSession> GetChromeSessionAsync()
        {
            return _chromeSession;
        }

        public async Task<ICollection<ExecutionContextDescription>> GetContextDescriptionsAsync()
        {
            return _executionContexts.Values;
        }
    }
}

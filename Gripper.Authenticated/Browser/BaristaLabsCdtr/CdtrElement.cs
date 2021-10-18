using System;
using System.Threading.Tasks;
using BaristaLabs.ChromeDevTools.Runtime;
using Runtime = BaristaLabs.ChromeDevTools.Runtime.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.Input;
using BaristaLabs.ChromeDevTools.Runtime.DOM;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace Gripper.Authenticated.Browser.BaristaLabsCdtr
{
    public class CdtrElement : IElement
    {
        private ILogger _logger;
        private ChromeSession _chromeSession;
        private long _nodeId;
        private CancellationToken _cancellationToken;

        private async Task FocusAsync()
        {
            try
            {
                await _chromeSession.DOM.Focus(new FocusCommand
                {
                    NodeId = _nodeId
                },
                throwExceptionIfResponseNotReceived: false);
            }
            catch (Exception e)
            {
                _logger.LogError("Focus error. _nodeId: {_nodeId}. Exception: {e}", _nodeId, e);
                throw;
            }
        }

        public CdtrElement(ILogger<CdtrElement> logger, ChromeSession chromeSession, long nodeId, CancellationToken cancellationToken)
        {
            _logger = logger;
            _chromeSession = chromeSession;
            _nodeId = nodeId;
            _cancellationToken = cancellationToken;
        }

        public async Task ClickAsync()
        {
            try
            {
                var boxModel = await _chromeSession.DOM.GetBoxModel(new GetBoxModelCommand
                {
                    NodeId = _nodeId
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: _cancellationToken);

                var contentQuad = boxModel.Model.Content;

                var contentX = 0.5 * (contentQuad[0] + contentQuad[1]);
                var contentY = 0.5 * (contentQuad[0] + contentQuad[2]);

                // TODO: randomize click position.

                var mousePressedEventResult = await _chromeSession.Input.DispatchMouseEvent(new DispatchMouseEventCommand
                {
                    Type = "mousePressed",
                    ClickCount = 1,
                    Button = MouseButton.Left,
                    X = contentX,
                    Y = contentY
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: _cancellationToken);

                // TODO: randomize delay.

                await Task.Delay(10);

                var mouseReleasedEventResult = await _chromeSession.Input.DispatchMouseEvent(new DispatchMouseEventCommand
                {
                    Type = "mouseReleased",
                    ClickCount = 1,
                    Button = MouseButton.Left,
                    X = contentX,
                    Y = contentY
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: _cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError("Error clicking. Node id: {_nodeId}, Exception: {e}", _nodeId, e);
            }
        }

        private async Task SendKeysAsync(DispatchKeyEventCommand command)
        {
            await FocusAsync();

            var commandResponse = await _chromeSession.Input.DispatchKeyEvent(command, throwExceptionIfResponseNotReceived: false, cancellationToken: _cancellationToken);
        }

        public async Task SendKeysAsync(string keys)
        {
            await SendKeysAsync(new DispatchKeyEventCommand
            {
                Type = "char",
                Text = keys
            });
        }

        public async Task SendKeysAsync(string keys, TimeSpan delayAfterStroke)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                var key = keys[i];

                await SendKeysAsync(new DispatchKeyEventCommand
                {
                    Type = "char",
                    Text = key.ToString()
                });

                await Task.Delay(delayAfterStroke);
            }
        }
        public async Task SendSpecialKeyAsync(SpecialKey key)
        {
            await FocusAsync();

            var commands = key.ToDispatchKeyEventCommands();
            try
            {
                foreach (var command in commands)
                {

                    await _chromeSession.Input.DispatchKeyEvent(command, throwExceptionIfResponseNotReceived: false, cancellationToken: _cancellationToken);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to send special keys: {e}", e);
                throw;
            }
        }
    }
}

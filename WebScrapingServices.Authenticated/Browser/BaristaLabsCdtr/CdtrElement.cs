using System;
using System.Threading.Tasks;
using BaristaLabs.ChromeDevTools.Runtime;
using Runtime = BaristaLabs.ChromeDevTools.Runtime.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.Input;
using BaristaLabs.ChromeDevTools.Runtime.DOM;
using Microsoft.Extensions.Logging;

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCdtr
{
    public class CdtrElement : ElementBase
    {
        private ILogger _logger;
        private long _nodeId;
        private ChromeSession _chromeSession;

        private async Task FocusAsync()
        {
            try
            {
                await ChromeSession.DOM.Focus(new FocusCommand
                {
                    NodeId = _nodeId
                });
            }
            catch (Exception e)
            {
                _logger.LogError("Focus error. _nodeId: {_nodeId}. Exception: {e}", _nodeId, e);
                throw;
            }
        }

        public CdtrElement(ILogger<CdtrElement> logger, long nodeId, ChromeSession chromeSession) : base(chromeSession)
        {
            _logger = logger;
            _nodeId = nodeId;
            _chromeSession = base.ChromeSession;
        }
        public override async Task ClickAsync()
        {
            try
            {

                var boxModel = await ChromeSession.DOM.GetBoxModel(new GetBoxModelCommand
                {
                    NodeId = _nodeId
                });

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
                });

                // TODO: randomize delay.

                await Task.Delay(10);

                var mouseReleasedEventResult = await _chromeSession.Input.DispatchMouseEvent(new DispatchMouseEventCommand
                {
                    Type = "mouseReleased",
                    ClickCount = 1,
                    Button = MouseButton.Left,
                    X = contentX,
                    Y = contentY
                });
            }
            catch (Exception e)
            {
                ;
            }

        }

        private async Task SendKeysAsync(DispatchKeyEventCommand command)
        {
            await FocusAsync();

            var commandResponse = await _chromeSession.Input.DispatchKeyEvent(command);
        }

        public override async Task SendKeysAsync(string keys)
        {
            await SendKeysAsync(new DispatchKeyEventCommand
            {
                Type = "char",
                Text = keys
            });
        }


        public override async Task SendKeysAsync(string keys, TimeSpan delayAfterStroke)
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
    }
}

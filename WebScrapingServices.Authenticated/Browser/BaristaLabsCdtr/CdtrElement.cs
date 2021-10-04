using System;
using System.Threading.Tasks;
using BaristaLabs.ChromeDevTools.Runtime;
using Runtime = BaristaLabs.ChromeDevTools.Runtime.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.Input;
using BaristaLabs.ChromeDevTools.Runtime.DOM;

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCdtr
{
    public class CdtrElement : IElement
    {
        private long _nodeId;
        private ChromeSession _chromeSession;

        private async Task FocusAsync()
        {
            await _chromeSession.DOM.Focus(new FocusCommand
            {
                NodeId = _nodeId
            });
        }

        public CdtrElement(long nodeId, ChromeSession chromeSession)
        {
            _nodeId = nodeId;
            _chromeSession = chromeSession;
        }
        public async Task ClickAsync()
        {
            try
            {

                var boxModel = await _chromeSession.DOM.GetBoxModel(new GetBoxModelCommand
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

        public async Task SendKeysAsync(string keys)
        {
            await FocusAsync();

            var response = await _chromeSession.Input.InsertText(new InsertTextCommand
            {
                Text = keys
            });
            ;
        }


        public async Task SendKeysAsync(string keys, TimeSpan delayAfterStroke)
        {
            await FocusAsync();

            for (int i = 0; i < keys.Length; i++)
            {
                var key = keys[i];
                var dispatchKeyEventResponse = await _chromeSession.Input.DispatchKeyEvent(new DispatchKeyEventCommand
                {
                    Type = "char",
                    Key = key.ToString()
                });
                await Task.Delay(delayAfterStroke);
            }
            ;
        }

        public async Task SendSpecialKeyAsync(SpecialKey key)
        {
            await FocusAsync();

            await _chromeSession.Input.DispatchKeyEvent(key.ToDispatchKeyEventCommand());
            ;
        }
    }

}

﻿using System;
using System.Threading.Tasks;
using BaristaLabs.ChromeDevTools.Runtime;
using Runtime = BaristaLabs.ChromeDevTools.Runtime.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.Input;
using BaristaLabs.ChromeDevTools.Runtime.DOM;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace Gripper.WebClient.Browser.BaristaLabsCdtr
{
    public class CdtrElement : IElement
    {
        private ILogger _logger;
        private ChromeSession _chromeSession;
        private long _backendNodeId;
        private CancellationToken _cancellationToken;

        private async Task LogAttributesAsync(string when)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                var nodeDescription = await _chromeSession.DOM.DescribeNode(new DescribeNodeCommand { BackendNodeId = _backendNodeId }, throwExceptionIfResponseNotReceived: false);

                if (nodeDescription?.Node?.Attributes != null)
                {
                    var classIndex = Array.IndexOf(nodeDescription.Node.Attributes, "class");

                    if (classIndex > -1)
                    {
                        _logger.LogDebug("Element attributes {when}: {attributes}", when, nodeDescription.Node.Attributes[classIndex + 1]);
                    }
                }
            }
        }

        public async Task FocusAsync()
        {
            try
            {
                var focusResponse = await _chromeSession.DOM.Focus(new FocusCommand
                {
                    BackendNodeId = _backendNodeId
                },
                throwExceptionIfResponseNotReceived: false);
            }
            catch (CommandResponseException e)
            {
                _logger.LogWarning("{name} error: {message}. Doing nothing.", nameof(FocusAsync), e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError("Focus error. _nodeId: {_backendNodeId}. Exception: {e}", _backendNodeId, e);
                throw;
            }
        }

        private async Task SendKeysAsync(IEnumerable<DispatchKeyEventCommand> commands, TimeSpan delayBetweenStrokes)
        {
            await FocusAsync();

            foreach (var command in commands)
            {
                await Task.Delay(delayBetweenStrokes);
                var dispatchKeyResponse = await _chromeSession.Input.DispatchKeyEvent(command, throwExceptionIfResponseNotReceived: false, cancellationToken: _cancellationToken);
            }
        }


        public CdtrElement(ILogger<CdtrElement> logger, ChromeSession chromeSession, long backendNodeId, CancellationToken cancellationToken)
        {
            _logger = logger;
            _chromeSession = chromeSession;
            _backendNodeId = backendNodeId;
            _cancellationToken = cancellationToken;
        }

        public async Task ClickAsync()
        {
            try
            {
                var boxModel = await _chromeSession.DOM.GetBoxModel(new GetBoxModelCommand
                {
                    BackendNodeId = _backendNodeId
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: _cancellationToken);

                var contentQuad = boxModel.Model.Content;

                var contentX = 0.5 * (contentQuad[0] + contentQuad[2]);
                var contentY = 0.5 * (contentQuad[1] + contentQuad[5]);

                // TODO: randomize click position.

                var mousePressedEventResult = await _chromeSession.Input.DispatchMouseEvent(new DispatchMouseEventCommand
                {
                    Type = "mousePressed",
                    ClickCount = 1,
                    Button = "left",
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
                    Button = "left",
                    X = contentX,
                    Y = contentY
                },
                throwExceptionIfResponseNotReceived: false,
                cancellationToken: _cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError("Error clicking. Backend node id: {_backendNodeId}, Exception: {e}", _backendNodeId, e);
            }
        }

        public async Task SendKeysAsync(string keys, TimeSpan delayBetweenStrokes)
        {
            await LogAttributesAsync("before " + nameof(SendKeysAsync));

            for (int i = 0; i < keys.Length; i++)
            {
                var key = keys[i];

                await SendKeysAsync(new DispatchKeyEventCommand[]
                {
                    //new()
                    //{
                    //    Type = "keyDown",
                    //    Text = key.ToString()
                    //},
                    new()
                    {
                        Type = "char",
                        Text = key.ToString()
                    },
                    //new()
                    //{
                    //    Type = "keyUp",
                    //    Text = key.ToString()
                    //}
                }, delayBetweenStrokes);
            }

            await LogAttributesAsync("after " + nameof(SendKeysAsync));
        }
        public async Task SendSpecialKeyAsync(SpecialKey key)
        {
            await FocusAsync();

            var commands = key.ToDispatchKeyEventCommands();
            try
            {
                foreach (var command in commands)
                {
                    var dispatchResponse = await _chromeSession.Input.DispatchKeyEvent(command, throwExceptionIfResponseNotReceived: false, cancellationToken: _cancellationToken);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to send special keys: {e}", e);
                throw;
            }
        }

        public async Task<string> GetInnerTextAsync()
        {
            var outerHtml = await _chromeSession.DOM.GetOuterHTML(new GetOuterHTMLCommand
            {
                BackendNodeId = _backendNodeId
            },
            throwExceptionIfResponseNotReceived: false);

            throw new NotImplementedException();
        }

    }
}
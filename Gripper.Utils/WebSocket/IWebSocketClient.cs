using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Utils.WebSocket
{
    public interface IWebSocketClient
    {
        public event EventHandler<MessageReceivedEventArgs>? MessageReceived;
        public Task<bool> TryConnectAsync(Uri uri, CancellationToken cancellationToken);
        public Task SendAsync(string message);
    }

    public class WebSocketClient : IWebSocketClient
    {
        public event EventHandler<MessageReceivedEventArgs>? MessageReceived;

        public Task SendAsync(string message)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryConnectAsync(Uri uri, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public class MessageReceivedEventArgs : EventArgs
    {

    }

}

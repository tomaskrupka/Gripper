using System.Collections.Generic;

namespace Gripper.WebClient.Events
{
    /// <summary>
    /// Event args for the Network.responseReceived event.
    /// </summary>
    public class Network_ResponseReceivedEventArgs : RdpEventArgs
    {
        /// <summary>
        /// Represents the Http response.
        /// </summary>
        public HttpResponse Response { get; set; }


        internal Network_ResponseReceivedEventArgs(string requestId, long status, IReadOnlyDictionary<string, string> responseHeaders) : base("Network", "responseReceived")
        {
            Response = new HttpResponse(requestId, status, responseHeaders);
        }
    }

}

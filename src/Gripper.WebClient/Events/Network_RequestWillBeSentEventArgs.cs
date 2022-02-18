using System.Collections.Generic;

namespace Gripper.WebClient.Events
{
    /// <summary>
    /// Event args for the Network.requestWillBeSent event.
    /// </summary>
    public class Network_RequestWillBeSentEventArgs : RdpEventArgs
    {
        /// <summary>
        /// Represents the http request about to be sent.
        /// </summary>
        public HttpRequest Request { get; set; }


        internal Network_RequestWillBeSentEventArgs(string requestId, IReadOnlyDictionary<string, string> requestHeaders, string requestMethod, string requestUrl) : base("Network", "requestWillBeSent")
        {
            Request = new HttpRequest(requestId, requestHeaders, requestMethod, requestUrl);
        }
        internal Network_RequestWillBeSentEventArgs(HttpRequest request) : base("Network", "requestWillBeSent")
        {
            Request = request;
        }
    }

}

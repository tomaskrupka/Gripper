using System.Collections.Generic;

namespace Gripper.WebClient.Browser
{
    public class Network_RequestWillBeSentEventArgs : RdpEventArgs
    {
        public HttpRequest Request { get; set; }
        public Network_RequestWillBeSentEventArgs(string requestId, IReadOnlyDictionary<string, string> requestHeaders, string requestMethod, string requestUrl) : base("Network", "requestWillBeSent")
        {
            Request = new HttpRequest(requestId, requestHeaders, requestMethod, requestUrl);
        }
        public Network_RequestWillBeSentEventArgs(HttpRequest request) : base("Network", "requestWillBeSent")
        {
            Request = request;
        }
    }

}

using System.Collections.Generic;

namespace WebScrapingServices.Authenticated.Browser
{
    public class Network_RequestWillBeSentEventArgs : RdpEventArgs
    {
        public Request Request { get; set; }
        public Network_RequestWillBeSentEventArgs(string id, IReadOnlyDictionary<string, string> headers, string method, string url) : base("Network", "requestWillBeSent")
        {
            Request = new Request(id, headers, method, url);
        }
        public Network_RequestWillBeSentEventArgs(Request request) : base("Network", "requestWillBeSent")
        {
            Request = request;
        }
    }

}

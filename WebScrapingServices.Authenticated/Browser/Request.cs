using System.Collections.Generic;

namespace WebScrapingServices.Authenticated.Browser
{
    public class Request
    {
        public Request(string id, IReadOnlyDictionary<string, string> headers, string method, string url)
        {
            Id = id;
            Headers = headers;
            Method = method;
            Url = url;
        }

        public string Id { get; private set; }
        public IReadOnlyDictionary<string, string> Headers { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }

    }

}

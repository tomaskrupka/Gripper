using System.Collections.Generic;

namespace Gripper.WebClient
{
    /// <summary>
    /// Contains Http Request metadata.
    /// </summary>
    public class HttpRequest
    {
        internal HttpRequest(string id, IReadOnlyDictionary<string, string> headers, string method, string url)
        {
            Id = id;
            Headers = headers;
            Method = method;
            Url = url;
        }

        /// <summary>
        /// Id of the http request as defined by the browser backend.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Request headers.
        /// </summary>
        public IReadOnlyDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Raw representation of the request Http method (POST, GET, PUT, OPTIONS, DELETE...)
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Raw representation of the request destination Url
        /// </summary>
        public string Url { get; set; }

    }

}

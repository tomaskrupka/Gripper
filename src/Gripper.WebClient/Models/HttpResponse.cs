using System.Collections.Generic;

namespace Gripper.WebClient
{
    /// <summary>
    /// Contains Http Response metadata.
    /// </summary>
    public class HttpResponse
    {
        internal HttpResponse(string requestId, long status, IReadOnlyDictionary<string, string> headers)
        {
            RequestId = requestId;
            Status = status;
            Headers = headers;
        }

        /// <summary>
        /// ID of the request this response is for.
        /// </summary>
        
        public string RequestId { get; private set; }
        /// <summary>
        /// Response Http Status.
        /// </summary>
        /// 
        public long Status { get; private set; }

        /// <summary>
        /// Request headers.
        /// </summary>
        public IReadOnlyDictionary<string, string> Headers { get; private set; }
    }

}

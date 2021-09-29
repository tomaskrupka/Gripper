using Newtonsoft.Json.Linq;
using System;

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCdtr
{
    public class CdtrRdpCommandResult : IRdpCommandResult
    {
        public CdtrRdpCommandResult(JToken resultToken)
        {
            throw new NotImplementedException();
        }
        public string Message => throw new NotImplementedException();
    }
}

using Newtonsoft.Json.Linq;
using System;

namespace WebScrapingServices.Authenticated.Browser
{
    public class RdpEventArgs : EventArgs
    {
        private JToken? _eventData;

        public string DomainName { get; private set; }
        public string EventName { get; private set; }
        public bool HasEventData { get; private set; }
        public JToken EventData
        {
            get
            {
                if (_eventData == null)
                {
                    throw new InvalidOperationException($"This event has no data. You can check this using the {nameof(HasEventData)} property beforehand.");
                }
                return _eventData;
            }
            private set => _eventData = value;
        }

        public RdpEventArgs(string domainName, string eventName)
        {
            DomainName = domainName;
            EventName = eventName;
            HasEventData = false;
            _eventData = null;
        }

        public RdpEventArgs(string domainName, string eventName, JToken eventData)
        {
            DomainName = domainName;
            EventName = eventName;

            if (eventData == null)
            {
                HasEventData = false;
                _eventData = null;
            }
            else
            {
                HasEventData = true;
                _eventData = eventData;
            }
        }
    }
}

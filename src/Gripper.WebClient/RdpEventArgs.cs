using Newtonsoft.Json.Linq;
using System;

namespace Gripper.WebClient
{
    /// <summary>
    /// The base event args container for any event from all CDP domains.
    /// </summary>
    public class RdpEventArgs : EventArgs
    {
        private JToken? _eventData;

        /// <summary>
        /// The namespace of the domain where the event originated, e.g. "Network". The first character is always uppercase.
        /// </summary>
        /// <remarks>
        /// See the <see cref="https://chromedevtools.github.io/devtools-protocol">API reference</see> for all domains.
        /// </remarks>
        public string DomainName { get; private set; }

        /// <summary>
        /// The name of the event, e.g. "requestWillBeSent". The first character is always lowercase.
        /// </summary>
        /// <remarks>
        /// See the <see cref="https://chromedevtools.github.io/devtools-protocol">API reference</see> for all domains.
        /// </remarks>
        public string EventName { get; private set; }

        /// <summary>
        /// Defines whether this event carries some additional data.
        /// </summary>
        public bool HasEventData { get; private set; }

        /// <summary>
        /// Getter for the event data container.
        /// </summary>
        /// <exception cref="InvalidOperationException"> The event had no data.</exception>
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

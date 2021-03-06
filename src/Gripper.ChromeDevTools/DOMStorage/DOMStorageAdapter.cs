namespace Gripper.ChromeDevTools.DOMStorage
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an adapter for the DOMStorage domain to simplify the command interface.
    /// </summary>
    public class DOMStorageAdapter
    {
        private readonly ChromeSession m_session;
        
        public DOMStorageAdapter(ChromeSession session)
        {
            m_session = session ?? throw new ArgumentNullException(nameof(session));
        }

        /// <summary>
        /// Gets the ChromeSession associated with the adapter.
        /// </summary>
        public ChromeSession Session
        {
            get { return m_session; }
        }

        /// <summary>
        /// clear
        /// </summary>
        public async Task<ClearCommandResponse> Clear(ClearCommand command, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<ClearCommand, ClearCommandResponse>(command, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// Disables storage tracking, prevents storage events from being sent to the client.
        /// </summary>
        public async Task<DisableCommandResponse> Disable(DisableCommand command = null, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<DisableCommand, DisableCommandResponse>(command ?? new DisableCommand(), cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// Enables storage tracking, storage events will now be delivered to the client.
        /// </summary>
        public async Task<EnableCommandResponse> Enable(EnableCommand command = null, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<EnableCommand, EnableCommandResponse>(command ?? new EnableCommand(), cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// getDOMStorageItems
        /// </summary>
        public async Task<GetDOMStorageItemsCommandResponse> GetDOMStorageItems(GetDOMStorageItemsCommand command, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<GetDOMStorageItemsCommand, GetDOMStorageItemsCommandResponse>(command, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// removeDOMStorageItem
        /// </summary>
        public async Task<RemoveDOMStorageItemCommandResponse> RemoveDOMStorageItem(RemoveDOMStorageItemCommand command, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<RemoveDOMStorageItemCommand, RemoveDOMStorageItemCommandResponse>(command, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// setDOMStorageItem
        /// </summary>
        public async Task<SetDOMStorageItemCommandResponse> SetDOMStorageItem(SetDOMStorageItemCommand command, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<SetDOMStorageItemCommand, SetDOMStorageItemCommandResponse>(command, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }

        /// <summary>
        /// domStorageItemAdded
        /// </summary>
        public void SubscribeToDomStorageItemAddedEvent(Action<DomStorageItemAddedEvent> eventCallback)
        {
            m_session.Subscribe(eventCallback);
        }
        /// <summary>
        /// domStorageItemRemoved
        /// </summary>
        public void SubscribeToDomStorageItemRemovedEvent(Action<DomStorageItemRemovedEvent> eventCallback)
        {
            m_session.Subscribe(eventCallback);
        }
        /// <summary>
        /// domStorageItemUpdated
        /// </summary>
        public void SubscribeToDomStorageItemUpdatedEvent(Action<DomStorageItemUpdatedEvent> eventCallback)
        {
            m_session.Subscribe(eventCallback);
        }
        /// <summary>
        /// domStorageItemsCleared
        /// </summary>
        public void SubscribeToDomStorageItemsClearedEvent(Action<DomStorageItemsClearedEvent> eventCallback)
        {
            m_session.Subscribe(eventCallback);
        }
    }
}
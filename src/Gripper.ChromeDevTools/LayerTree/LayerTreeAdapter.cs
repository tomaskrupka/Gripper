namespace Gripper.ChromeDevTools.LayerTree
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents an adapter for the LayerTree domain to simplify the command interface.
    /// </summary>
    public class LayerTreeAdapter
    {
        private readonly ChromeSession m_session;
        
        public LayerTreeAdapter(ChromeSession session)
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
        /// Provides the reasons why the given layer was composited.
        /// </summary>
        public async Task<CompositingReasonsCommandResponse> CompositingReasons(CompositingReasonsCommand command, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<CompositingReasonsCommand, CompositingReasonsCommandResponse>(command, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// Disables compositing tree inspection.
        /// </summary>
        public async Task<DisableCommandResponse> Disable(DisableCommand command = null, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<DisableCommand, DisableCommandResponse>(command ?? new DisableCommand(), cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// Enables compositing tree inspection.
        /// </summary>
        public async Task<EnableCommandResponse> Enable(EnableCommand command = null, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<EnableCommand, EnableCommandResponse>(command ?? new EnableCommand(), cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// Returns the snapshot identifier.
        /// </summary>
        public async Task<LoadSnapshotCommandResponse> LoadSnapshot(LoadSnapshotCommand command, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<LoadSnapshotCommand, LoadSnapshotCommandResponse>(command, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// Returns the layer snapshot identifier.
        /// </summary>
        public async Task<MakeSnapshotCommandResponse> MakeSnapshot(MakeSnapshotCommand command, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<MakeSnapshotCommand, MakeSnapshotCommandResponse>(command, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// profileSnapshot
        /// </summary>
        public async Task<ProfileSnapshotCommandResponse> ProfileSnapshot(ProfileSnapshotCommand command, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<ProfileSnapshotCommand, ProfileSnapshotCommandResponse>(command, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// Releases layer snapshot captured by the back-end.
        /// </summary>
        public async Task<ReleaseSnapshotCommandResponse> ReleaseSnapshot(ReleaseSnapshotCommand command, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<ReleaseSnapshotCommand, ReleaseSnapshotCommandResponse>(command, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// Replays the layer snapshot and returns the resulting bitmap.
        /// </summary>
        public async Task<ReplaySnapshotCommandResponse> ReplaySnapshot(ReplaySnapshotCommand command, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<ReplaySnapshotCommand, ReplaySnapshotCommandResponse>(command, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }
        /// <summary>
        /// Replays the layer snapshot and returns canvas log.
        /// </summary>
        public async Task<SnapshotCommandLogCommandResponse> SnapshotCommandLog(SnapshotCommandLogCommand command, CancellationToken cancellationToken = default(CancellationToken), int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            return await m_session.SendCommand<SnapshotCommandLogCommand, SnapshotCommandLogCommandResponse>(command, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
        }

        /// <summary>
        /// layerPainted
        /// </summary>
        public void SubscribeToLayerPaintedEvent(Action<LayerPaintedEvent> eventCallback)
        {
            m_session.Subscribe(eventCallback);
        }
        /// <summary>
        /// layerTreeDidChange
        /// </summary>
        public void SubscribeToLayerTreeDidChangeEvent(Action<LayerTreeDidChangeEvent> eventCallback)
        {
            m_session.Subscribe(eventCallback);
        }
    }
}
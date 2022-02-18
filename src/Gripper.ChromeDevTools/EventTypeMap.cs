namespace Gripper.ChromeDevTools
{
    using System;
    using System.Collections.Generic;

    public static class EventTypeMap
    {
        private readonly static IDictionary<string, Type> s_methodNameEventTypeDictionary;
        private readonly static IDictionary<Type, string> s_eventTypeMethodNameDictionary;

        static EventTypeMap()
        {
            s_methodNameEventTypeDictionary = new Dictionary<string, Type>()
            {
                { "Accessibility.loadComplete", typeof(Accessibility.LoadCompleteEvent) },
                { "Accessibility.nodesUpdated", typeof(Accessibility.NodesUpdatedEvent) },
                { "Animation.animationCanceled", typeof(Animation.AnimationCanceledEvent) },
                { "Animation.animationCreated", typeof(Animation.AnimationCreatedEvent) },
                { "Animation.animationStarted", typeof(Animation.AnimationStartedEvent) },
                { "Audits.issueAdded", typeof(Audits.IssueAddedEvent) },
                { "BackgroundService.recordingStateChanged", typeof(BackgroundService.RecordingStateChangedEvent) },
                { "BackgroundService.backgroundServiceEventReceived", typeof(BackgroundService.BackgroundServiceEventReceivedEvent) },
                { "Browser.downloadWillBegin", typeof(Browser.DownloadWillBeginEvent) },
                { "Browser.downloadProgress", typeof(Browser.DownloadProgressEvent) },
                { "CSS.fontsUpdated", typeof(CSS.FontsUpdatedEvent) },
                { "CSS.mediaQueryResultChanged", typeof(CSS.MediaQueryResultChangedEvent) },
                { "CSS.styleSheetAdded", typeof(CSS.StyleSheetAddedEvent) },
                { "CSS.styleSheetChanged", typeof(CSS.StyleSheetChangedEvent) },
                { "CSS.styleSheetRemoved", typeof(CSS.StyleSheetRemovedEvent) },
                { "Cast.sinksUpdated", typeof(Cast.SinksUpdatedEvent) },
                { "Cast.issueUpdated", typeof(Cast.IssueUpdatedEvent) },
                { "DOM.attributeModified", typeof(DOM.AttributeModifiedEvent) },
                { "DOM.attributeRemoved", typeof(DOM.AttributeRemovedEvent) },
                { "DOM.characterDataModified", typeof(DOM.CharacterDataModifiedEvent) },
                { "DOM.childNodeCountUpdated", typeof(DOM.ChildNodeCountUpdatedEvent) },
                { "DOM.childNodeInserted", typeof(DOM.ChildNodeInsertedEvent) },
                { "DOM.childNodeRemoved", typeof(DOM.ChildNodeRemovedEvent) },
                { "DOM.distributedNodesUpdated", typeof(DOM.DistributedNodesUpdatedEvent) },
                { "DOM.documentUpdated", typeof(DOM.DocumentUpdatedEvent) },
                { "DOM.inlineStyleInvalidated", typeof(DOM.InlineStyleInvalidatedEvent) },
                { "DOM.pseudoElementAdded", typeof(DOM.PseudoElementAddedEvent) },
                { "DOM.pseudoElementRemoved", typeof(DOM.PseudoElementRemovedEvent) },
                { "DOM.setChildNodes", typeof(DOM.SetChildNodesEvent) },
                { "DOM.shadowRootPopped", typeof(DOM.ShadowRootPoppedEvent) },
                { "DOM.shadowRootPushed", typeof(DOM.ShadowRootPushedEvent) },
                { "DOMStorage.domStorageItemAdded", typeof(DOMStorage.DomStorageItemAddedEvent) },
                { "DOMStorage.domStorageItemRemoved", typeof(DOMStorage.DomStorageItemRemovedEvent) },
                { "DOMStorage.domStorageItemUpdated", typeof(DOMStorage.DomStorageItemUpdatedEvent) },
                { "DOMStorage.domStorageItemsCleared", typeof(DOMStorage.DomStorageItemsClearedEvent) },
                { "Database.addDatabase", typeof(Database.AddDatabaseEvent) },
                { "Emulation.virtualTimeBudgetExpired", typeof(Emulation.VirtualTimeBudgetExpiredEvent) },
                { "HeadlessExperimental.needsBeginFramesChanged", typeof(HeadlessExperimental.NeedsBeginFramesChangedEvent) },
                { "Input.dragIntercepted", typeof(Input.DragInterceptedEvent) },
                { "Inspector.detached", typeof(Inspector.DetachedEvent) },
                { "Inspector.targetCrashed", typeof(Inspector.TargetCrashedEvent) },
                { "Inspector.targetReloadedAfterCrash", typeof(Inspector.TargetReloadedAfterCrashEvent) },
                { "LayerTree.layerPainted", typeof(LayerTree.LayerPaintedEvent) },
                { "LayerTree.layerTreeDidChange", typeof(LayerTree.LayerTreeDidChangeEvent) },
                { "Log.entryAdded", typeof(Log.EntryAddedEvent) },
                { "Network.dataReceived", typeof(Network.DataReceivedEvent) },
                { "Network.eventSourceMessageReceived", typeof(Network.EventSourceMessageReceivedEvent) },
                { "Network.loadingFailed", typeof(Network.LoadingFailedEvent) },
                { "Network.loadingFinished", typeof(Network.LoadingFinishedEvent) },
                { "Network.requestIntercepted", typeof(Network.RequestInterceptedEvent) },
                { "Network.requestServedFromCache", typeof(Network.RequestServedFromCacheEvent) },
                { "Network.requestWillBeSent", typeof(Network.RequestWillBeSentEvent) },
                { "Network.resourceChangedPriority", typeof(Network.ResourceChangedPriorityEvent) },
                { "Network.signedExchangeReceived", typeof(Network.SignedExchangeReceivedEvent) },
                { "Network.responseReceived", typeof(Network.ResponseReceivedEvent) },
                { "Network.webSocketClosed", typeof(Network.WebSocketClosedEvent) },
                { "Network.webSocketCreated", typeof(Network.WebSocketCreatedEvent) },
                { "Network.webSocketFrameError", typeof(Network.WebSocketFrameErrorEvent) },
                { "Network.webSocketFrameReceived", typeof(Network.WebSocketFrameReceivedEvent) },
                { "Network.webSocketFrameSent", typeof(Network.WebSocketFrameSentEvent) },
                { "Network.webSocketHandshakeResponseReceived", typeof(Network.WebSocketHandshakeResponseReceivedEvent) },
                { "Network.webSocketWillSendHandshakeRequest", typeof(Network.WebSocketWillSendHandshakeRequestEvent) },
                { "Network.webTransportCreated", typeof(Network.WebTransportCreatedEvent) },
                { "Network.webTransportConnectionEstablished", typeof(Network.WebTransportConnectionEstablishedEvent) },
                { "Network.webTransportClosed", typeof(Network.WebTransportClosedEvent) },
                { "Network.requestWillBeSentExtraInfo", typeof(Network.RequestWillBeSentExtraInfoEvent) },
                { "Network.responseReceivedExtraInfo", typeof(Network.ResponseReceivedExtraInfoEvent) },
                { "Network.trustTokenOperationDone", typeof(Network.TrustTokenOperationDoneEvent) },
                { "Network.subresourceWebBundleMetadataReceived", typeof(Network.SubresourceWebBundleMetadataReceivedEvent) },
                { "Network.subresourceWebBundleMetadataError", typeof(Network.SubresourceWebBundleMetadataErrorEvent) },
                { "Network.subresourceWebBundleInnerResponseParsed", typeof(Network.SubresourceWebBundleInnerResponseParsedEvent) },
                { "Network.subresourceWebBundleInnerResponseError", typeof(Network.SubresourceWebBundleInnerResponseErrorEvent) },
                { "Network.reportingApiReportAdded", typeof(Network.ReportingApiReportAddedEvent) },
                { "Network.reportingApiReportUpdated", typeof(Network.ReportingApiReportUpdatedEvent) },
                { "Network.reportingApiEndpointsChangedForOrigin", typeof(Network.ReportingApiEndpointsChangedForOriginEvent) },
                { "Overlay.inspectNodeRequested", typeof(Overlay.InspectNodeRequestedEvent) },
                { "Overlay.nodeHighlightRequested", typeof(Overlay.NodeHighlightRequestedEvent) },
                { "Overlay.screenshotRequested", typeof(Overlay.ScreenshotRequestedEvent) },
                { "Overlay.inspectModeCanceled", typeof(Overlay.InspectModeCanceledEvent) },
                { "Page.domContentEventFired", typeof(Page.DomContentEventFiredEvent) },
                { "Page.fileChooserOpened", typeof(Page.FileChooserOpenedEvent) },
                { "Page.frameAttached", typeof(Page.FrameAttachedEvent) },
                { "Page.frameClearedScheduledNavigation", typeof(Page.FrameClearedScheduledNavigationEvent) },
                { "Page.frameDetached", typeof(Page.FrameDetachedEvent) },
                { "Page.frameNavigated", typeof(Page.FrameNavigatedEvent) },
                { "Page.documentOpened", typeof(Page.DocumentOpenedEvent) },
                { "Page.frameResized", typeof(Page.FrameResizedEvent) },
                { "Page.frameRequestedNavigation", typeof(Page.FrameRequestedNavigationEvent) },
                { "Page.frameScheduledNavigation", typeof(Page.FrameScheduledNavigationEvent) },
                { "Page.frameStartedLoading", typeof(Page.FrameStartedLoadingEvent) },
                { "Page.frameStoppedLoading", typeof(Page.FrameStoppedLoadingEvent) },
                { "Page.downloadWillBegin", typeof(Page.DownloadWillBeginEvent) },
                { "Page.downloadProgress", typeof(Page.DownloadProgressEvent) },
                { "Page.interstitialHidden", typeof(Page.InterstitialHiddenEvent) },
                { "Page.interstitialShown", typeof(Page.InterstitialShownEvent) },
                { "Page.javascriptDialogClosed", typeof(Page.JavascriptDialogClosedEvent) },
                { "Page.javascriptDialogOpening", typeof(Page.JavascriptDialogOpeningEvent) },
                { "Page.lifecycleEvent", typeof(Page.LifecycleEventEvent) },
                { "Page.backForwardCacheNotUsed", typeof(Page.BackForwardCacheNotUsedEvent) },
                { "Page.loadEventFired", typeof(Page.LoadEventFiredEvent) },
                { "Page.navigatedWithinDocument", typeof(Page.NavigatedWithinDocumentEvent) },
                { "Page.screencastFrame", typeof(Page.ScreencastFrameEvent) },
                { "Page.screencastVisibilityChanged", typeof(Page.ScreencastVisibilityChangedEvent) },
                { "Page.windowOpen", typeof(Page.WindowOpenEvent) },
                { "Page.compilationCacheProduced", typeof(Page.CompilationCacheProducedEvent) },
                { "Performance.metrics", typeof(Performance.MetricsEvent) },
                { "PerformanceTimeline.timelineEventAdded", typeof(PerformanceTimeline.TimelineEventAddedEvent) },
                { "Security.certificateError", typeof(Security.CertificateErrorEvent) },
                { "Security.visibleSecurityStateChanged", typeof(Security.VisibleSecurityStateChangedEvent) },
                { "Security.securityStateChanged", typeof(Security.SecurityStateChangedEvent) },
                { "ServiceWorker.workerErrorReported", typeof(ServiceWorker.WorkerErrorReportedEvent) },
                { "ServiceWorker.workerRegistrationUpdated", typeof(ServiceWorker.WorkerRegistrationUpdatedEvent) },
                { "ServiceWorker.workerVersionUpdated", typeof(ServiceWorker.WorkerVersionUpdatedEvent) },
                { "Storage.cacheStorageContentUpdated", typeof(Storage.CacheStorageContentUpdatedEvent) },
                { "Storage.cacheStorageListUpdated", typeof(Storage.CacheStorageListUpdatedEvent) },
                { "Storage.indexedDBContentUpdated", typeof(Storage.IndexedDBContentUpdatedEvent) },
                { "Storage.indexedDBListUpdated", typeof(Storage.IndexedDBListUpdatedEvent) },
                { "Storage.interestGroupAccessed", typeof(Storage.InterestGroupAccessedEvent) },
                { "Target.attachedToTarget", typeof(Target.AttachedToTargetEvent) },
                { "Target.detachedFromTarget", typeof(Target.DetachedFromTargetEvent) },
                { "Target.receivedMessageFromTarget", typeof(Target.ReceivedMessageFromTargetEvent) },
                { "Target.targetCreated", typeof(Target.TargetCreatedEvent) },
                { "Target.targetDestroyed", typeof(Target.TargetDestroyedEvent) },
                { "Target.targetCrashed", typeof(Target.TargetCrashedEvent) },
                { "Target.targetInfoChanged", typeof(Target.TargetInfoChangedEvent) },
                { "Tethering.accepted", typeof(Tethering.AcceptedEvent) },
                { "Tracing.bufferUsage", typeof(Tracing.BufferUsageEvent) },
                { "Tracing.dataCollected", typeof(Tracing.DataCollectedEvent) },
                { "Tracing.tracingComplete", typeof(Tracing.TracingCompleteEvent) },
                { "Fetch.requestPaused", typeof(Fetch.RequestPausedEvent) },
                { "Fetch.authRequired", typeof(Fetch.AuthRequiredEvent) },
                { "WebAudio.contextCreated", typeof(WebAudio.ContextCreatedEvent) },
                { "WebAudio.contextWillBeDestroyed", typeof(WebAudio.ContextWillBeDestroyedEvent) },
                { "WebAudio.contextChanged", typeof(WebAudio.ContextChangedEvent) },
                { "WebAudio.audioListenerCreated", typeof(WebAudio.AudioListenerCreatedEvent) },
                { "WebAudio.audioListenerWillBeDestroyed", typeof(WebAudio.AudioListenerWillBeDestroyedEvent) },
                { "WebAudio.audioNodeCreated", typeof(WebAudio.AudioNodeCreatedEvent) },
                { "WebAudio.audioNodeWillBeDestroyed", typeof(WebAudio.AudioNodeWillBeDestroyedEvent) },
                { "WebAudio.audioParamCreated", typeof(WebAudio.AudioParamCreatedEvent) },
                { "WebAudio.audioParamWillBeDestroyed", typeof(WebAudio.AudioParamWillBeDestroyedEvent) },
                { "WebAudio.nodesConnected", typeof(WebAudio.NodesConnectedEvent) },
                { "WebAudio.nodesDisconnected", typeof(WebAudio.NodesDisconnectedEvent) },
                { "WebAudio.nodeParamConnected", typeof(WebAudio.NodeParamConnectedEvent) },
                { "WebAudio.nodeParamDisconnected", typeof(WebAudio.NodeParamDisconnectedEvent) },
                { "Media.playerPropertiesChanged", typeof(Media.PlayerPropertiesChangedEvent) },
                { "Media.playerEventsAdded", typeof(Media.PlayerEventsAddedEvent) },
                { "Media.playerMessagesLogged", typeof(Media.PlayerMessagesLoggedEvent) },
                { "Media.playerErrorsRaised", typeof(Media.PlayerErrorsRaisedEvent) },
                { "Media.playersCreated", typeof(Media.PlayersCreatedEvent) },
                { "Console.messageAdded", typeof(Console.MessageAddedEvent) },
                { "Debugger.breakpointResolved", typeof(Debugger.BreakpointResolvedEvent) },
                { "Debugger.paused", typeof(Debugger.PausedEvent) },
                { "Debugger.resumed", typeof(Debugger.ResumedEvent) },
                { "Debugger.scriptFailedToParse", typeof(Debugger.ScriptFailedToParseEvent) },
                { "Debugger.scriptParsed", typeof(Debugger.ScriptParsedEvent) },
                { "HeapProfiler.addHeapSnapshotChunk", typeof(HeapProfiler.AddHeapSnapshotChunkEvent) },
                { "HeapProfiler.heapStatsUpdate", typeof(HeapProfiler.HeapStatsUpdateEvent) },
                { "HeapProfiler.lastSeenObjectId", typeof(HeapProfiler.LastSeenObjectIdEvent) },
                { "HeapProfiler.reportHeapSnapshotProgress", typeof(HeapProfiler.ReportHeapSnapshotProgressEvent) },
                { "HeapProfiler.resetProfiles", typeof(HeapProfiler.ResetProfilesEvent) },
                { "Profiler.consoleProfileFinished", typeof(Profiler.ConsoleProfileFinishedEvent) },
                { "Profiler.consoleProfileStarted", typeof(Profiler.ConsoleProfileStartedEvent) },
                { "Profiler.preciseCoverageDeltaUpdate", typeof(Profiler.PreciseCoverageDeltaUpdateEvent) },
                { "Runtime.bindingCalled", typeof(Runtime.BindingCalledEvent) },
                { "Runtime.consoleAPICalled", typeof(Runtime.ConsoleAPICalledEvent) },
                { "Runtime.exceptionRevoked", typeof(Runtime.ExceptionRevokedEvent) },
                { "Runtime.exceptionThrown", typeof(Runtime.ExceptionThrownEvent) },
                { "Runtime.executionContextCreated", typeof(Runtime.ExecutionContextCreatedEvent) },
                { "Runtime.executionContextDestroyed", typeof(Runtime.ExecutionContextDestroyedEvent) },
                { "Runtime.executionContextsCleared", typeof(Runtime.ExecutionContextsClearedEvent) },
                { "Runtime.inspectRequested", typeof(Runtime.InspectRequestedEvent) },
            };

            s_eventTypeMethodNameDictionary = new Dictionary<Type, string>()
            {
                { typeof(Accessibility.LoadCompleteEvent), "Accessibility.loadComplete" },
                { typeof(Accessibility.NodesUpdatedEvent), "Accessibility.nodesUpdated" },
                { typeof(Animation.AnimationCanceledEvent), "Animation.animationCanceled" },
                { typeof(Animation.AnimationCreatedEvent), "Animation.animationCreated" },
                { typeof(Animation.AnimationStartedEvent), "Animation.animationStarted" },
                { typeof(Audits.IssueAddedEvent), "Audits.issueAdded" },
                { typeof(BackgroundService.RecordingStateChangedEvent), "BackgroundService.recordingStateChanged" },
                { typeof(BackgroundService.BackgroundServiceEventReceivedEvent), "BackgroundService.backgroundServiceEventReceived" },
                { typeof(Browser.DownloadWillBeginEvent), "Browser.downloadWillBegin" },
                { typeof(Browser.DownloadProgressEvent), "Browser.downloadProgress" },
                { typeof(CSS.FontsUpdatedEvent), "CSS.fontsUpdated" },
                { typeof(CSS.MediaQueryResultChangedEvent), "CSS.mediaQueryResultChanged" },
                { typeof(CSS.StyleSheetAddedEvent), "CSS.styleSheetAdded" },
                { typeof(CSS.StyleSheetChangedEvent), "CSS.styleSheetChanged" },
                { typeof(CSS.StyleSheetRemovedEvent), "CSS.styleSheetRemoved" },
                { typeof(Cast.SinksUpdatedEvent), "Cast.sinksUpdated" },
                { typeof(Cast.IssueUpdatedEvent), "Cast.issueUpdated" },
                { typeof(DOM.AttributeModifiedEvent), "DOM.attributeModified" },
                { typeof(DOM.AttributeRemovedEvent), "DOM.attributeRemoved" },
                { typeof(DOM.CharacterDataModifiedEvent), "DOM.characterDataModified" },
                { typeof(DOM.ChildNodeCountUpdatedEvent), "DOM.childNodeCountUpdated" },
                { typeof(DOM.ChildNodeInsertedEvent), "DOM.childNodeInserted" },
                { typeof(DOM.ChildNodeRemovedEvent), "DOM.childNodeRemoved" },
                { typeof(DOM.DistributedNodesUpdatedEvent), "DOM.distributedNodesUpdated" },
                { typeof(DOM.DocumentUpdatedEvent), "DOM.documentUpdated" },
                { typeof(DOM.InlineStyleInvalidatedEvent), "DOM.inlineStyleInvalidated" },
                { typeof(DOM.PseudoElementAddedEvent), "DOM.pseudoElementAdded" },
                { typeof(DOM.PseudoElementRemovedEvent), "DOM.pseudoElementRemoved" },
                { typeof(DOM.SetChildNodesEvent), "DOM.setChildNodes" },
                { typeof(DOM.ShadowRootPoppedEvent), "DOM.shadowRootPopped" },
                { typeof(DOM.ShadowRootPushedEvent), "DOM.shadowRootPushed" },
                { typeof(DOMStorage.DomStorageItemAddedEvent), "DOMStorage.domStorageItemAdded" },
                { typeof(DOMStorage.DomStorageItemRemovedEvent), "DOMStorage.domStorageItemRemoved" },
                { typeof(DOMStorage.DomStorageItemUpdatedEvent), "DOMStorage.domStorageItemUpdated" },
                { typeof(DOMStorage.DomStorageItemsClearedEvent), "DOMStorage.domStorageItemsCleared" },
                { typeof(Database.AddDatabaseEvent), "Database.addDatabase" },
                { typeof(Emulation.VirtualTimeBudgetExpiredEvent), "Emulation.virtualTimeBudgetExpired" },
                { typeof(HeadlessExperimental.NeedsBeginFramesChangedEvent), "HeadlessExperimental.needsBeginFramesChanged" },
                { typeof(Input.DragInterceptedEvent), "Input.dragIntercepted" },
                { typeof(Inspector.DetachedEvent), "Inspector.detached" },
                { typeof(Inspector.TargetCrashedEvent), "Inspector.targetCrashed" },
                { typeof(Inspector.TargetReloadedAfterCrashEvent), "Inspector.targetReloadedAfterCrash" },
                { typeof(LayerTree.LayerPaintedEvent), "LayerTree.layerPainted" },
                { typeof(LayerTree.LayerTreeDidChangeEvent), "LayerTree.layerTreeDidChange" },
                { typeof(Log.EntryAddedEvent), "Log.entryAdded" },
                { typeof(Network.DataReceivedEvent), "Network.dataReceived" },
                { typeof(Network.EventSourceMessageReceivedEvent), "Network.eventSourceMessageReceived" },
                { typeof(Network.LoadingFailedEvent), "Network.loadingFailed" },
                { typeof(Network.LoadingFinishedEvent), "Network.loadingFinished" },
                { typeof(Network.RequestInterceptedEvent), "Network.requestIntercepted" },
                { typeof(Network.RequestServedFromCacheEvent), "Network.requestServedFromCache" },
                { typeof(Network.RequestWillBeSentEvent), "Network.requestWillBeSent" },
                { typeof(Network.ResourceChangedPriorityEvent), "Network.resourceChangedPriority" },
                { typeof(Network.SignedExchangeReceivedEvent), "Network.signedExchangeReceived" },
                { typeof(Network.ResponseReceivedEvent), "Network.responseReceived" },
                { typeof(Network.WebSocketClosedEvent), "Network.webSocketClosed" },
                { typeof(Network.WebSocketCreatedEvent), "Network.webSocketCreated" },
                { typeof(Network.WebSocketFrameErrorEvent), "Network.webSocketFrameError" },
                { typeof(Network.WebSocketFrameReceivedEvent), "Network.webSocketFrameReceived" },
                { typeof(Network.WebSocketFrameSentEvent), "Network.webSocketFrameSent" },
                { typeof(Network.WebSocketHandshakeResponseReceivedEvent), "Network.webSocketHandshakeResponseReceived" },
                { typeof(Network.WebSocketWillSendHandshakeRequestEvent), "Network.webSocketWillSendHandshakeRequest" },
                { typeof(Network.WebTransportCreatedEvent), "Network.webTransportCreated" },
                { typeof(Network.WebTransportConnectionEstablishedEvent), "Network.webTransportConnectionEstablished" },
                { typeof(Network.WebTransportClosedEvent), "Network.webTransportClosed" },
                { typeof(Network.RequestWillBeSentExtraInfoEvent), "Network.requestWillBeSentExtraInfo" },
                { typeof(Network.ResponseReceivedExtraInfoEvent), "Network.responseReceivedExtraInfo" },
                { typeof(Network.TrustTokenOperationDoneEvent), "Network.trustTokenOperationDone" },
                { typeof(Network.SubresourceWebBundleMetadataReceivedEvent), "Network.subresourceWebBundleMetadataReceived" },
                { typeof(Network.SubresourceWebBundleMetadataErrorEvent), "Network.subresourceWebBundleMetadataError" },
                { typeof(Network.SubresourceWebBundleInnerResponseParsedEvent), "Network.subresourceWebBundleInnerResponseParsed" },
                { typeof(Network.SubresourceWebBundleInnerResponseErrorEvent), "Network.subresourceWebBundleInnerResponseError" },
                { typeof(Network.ReportingApiReportAddedEvent), "Network.reportingApiReportAdded" },
                { typeof(Network.ReportingApiReportUpdatedEvent), "Network.reportingApiReportUpdated" },
                { typeof(Network.ReportingApiEndpointsChangedForOriginEvent), "Network.reportingApiEndpointsChangedForOrigin" },
                { typeof(Overlay.InspectNodeRequestedEvent), "Overlay.inspectNodeRequested" },
                { typeof(Overlay.NodeHighlightRequestedEvent), "Overlay.nodeHighlightRequested" },
                { typeof(Overlay.ScreenshotRequestedEvent), "Overlay.screenshotRequested" },
                { typeof(Overlay.InspectModeCanceledEvent), "Overlay.inspectModeCanceled" },
                { typeof(Page.DomContentEventFiredEvent), "Page.domContentEventFired" },
                { typeof(Page.FileChooserOpenedEvent), "Page.fileChooserOpened" },
                { typeof(Page.FrameAttachedEvent), "Page.frameAttached" },
                { typeof(Page.FrameClearedScheduledNavigationEvent), "Page.frameClearedScheduledNavigation" },
                { typeof(Page.FrameDetachedEvent), "Page.frameDetached" },
                { typeof(Page.FrameNavigatedEvent), "Page.frameNavigated" },
                { typeof(Page.DocumentOpenedEvent), "Page.documentOpened" },
                { typeof(Page.FrameResizedEvent), "Page.frameResized" },
                { typeof(Page.FrameRequestedNavigationEvent), "Page.frameRequestedNavigation" },
                { typeof(Page.FrameScheduledNavigationEvent), "Page.frameScheduledNavigation" },
                { typeof(Page.FrameStartedLoadingEvent), "Page.frameStartedLoading" },
                { typeof(Page.FrameStoppedLoadingEvent), "Page.frameStoppedLoading" },
                { typeof(Page.DownloadWillBeginEvent), "Page.downloadWillBegin" },
                { typeof(Page.DownloadProgressEvent), "Page.downloadProgress" },
                { typeof(Page.InterstitialHiddenEvent), "Page.interstitialHidden" },
                { typeof(Page.InterstitialShownEvent), "Page.interstitialShown" },
                { typeof(Page.JavascriptDialogClosedEvent), "Page.javascriptDialogClosed" },
                { typeof(Page.JavascriptDialogOpeningEvent), "Page.javascriptDialogOpening" },
                { typeof(Page.LifecycleEventEvent), "Page.lifecycleEvent" },
                { typeof(Page.BackForwardCacheNotUsedEvent), "Page.backForwardCacheNotUsed" },
                { typeof(Page.LoadEventFiredEvent), "Page.loadEventFired" },
                { typeof(Page.NavigatedWithinDocumentEvent), "Page.navigatedWithinDocument" },
                { typeof(Page.ScreencastFrameEvent), "Page.screencastFrame" },
                { typeof(Page.ScreencastVisibilityChangedEvent), "Page.screencastVisibilityChanged" },
                { typeof(Page.WindowOpenEvent), "Page.windowOpen" },
                { typeof(Page.CompilationCacheProducedEvent), "Page.compilationCacheProduced" },
                { typeof(Performance.MetricsEvent), "Performance.metrics" },
                { typeof(PerformanceTimeline.TimelineEventAddedEvent), "PerformanceTimeline.timelineEventAdded" },
                { typeof(Security.CertificateErrorEvent), "Security.certificateError" },
                { typeof(Security.VisibleSecurityStateChangedEvent), "Security.visibleSecurityStateChanged" },
                { typeof(Security.SecurityStateChangedEvent), "Security.securityStateChanged" },
                { typeof(ServiceWorker.WorkerErrorReportedEvent), "ServiceWorker.workerErrorReported" },
                { typeof(ServiceWorker.WorkerRegistrationUpdatedEvent), "ServiceWorker.workerRegistrationUpdated" },
                { typeof(ServiceWorker.WorkerVersionUpdatedEvent), "ServiceWorker.workerVersionUpdated" },
                { typeof(Storage.CacheStorageContentUpdatedEvent), "Storage.cacheStorageContentUpdated" },
                { typeof(Storage.CacheStorageListUpdatedEvent), "Storage.cacheStorageListUpdated" },
                { typeof(Storage.IndexedDBContentUpdatedEvent), "Storage.indexedDBContentUpdated" },
                { typeof(Storage.IndexedDBListUpdatedEvent), "Storage.indexedDBListUpdated" },
                { typeof(Storage.InterestGroupAccessedEvent), "Storage.interestGroupAccessed" },
                { typeof(Target.AttachedToTargetEvent), "Target.attachedToTarget" },
                { typeof(Target.DetachedFromTargetEvent), "Target.detachedFromTarget" },
                { typeof(Target.ReceivedMessageFromTargetEvent), "Target.receivedMessageFromTarget" },
                { typeof(Target.TargetCreatedEvent), "Target.targetCreated" },
                { typeof(Target.TargetDestroyedEvent), "Target.targetDestroyed" },
                { typeof(Target.TargetCrashedEvent), "Target.targetCrashed" },
                { typeof(Target.TargetInfoChangedEvent), "Target.targetInfoChanged" },
                { typeof(Tethering.AcceptedEvent), "Tethering.accepted" },
                { typeof(Tracing.BufferUsageEvent), "Tracing.bufferUsage" },
                { typeof(Tracing.DataCollectedEvent), "Tracing.dataCollected" },
                { typeof(Tracing.TracingCompleteEvent), "Tracing.tracingComplete" },
                { typeof(Fetch.RequestPausedEvent), "Fetch.requestPaused" },
                { typeof(Fetch.AuthRequiredEvent), "Fetch.authRequired" },
                { typeof(WebAudio.ContextCreatedEvent), "WebAudio.contextCreated" },
                { typeof(WebAudio.ContextWillBeDestroyedEvent), "WebAudio.contextWillBeDestroyed" },
                { typeof(WebAudio.ContextChangedEvent), "WebAudio.contextChanged" },
                { typeof(WebAudio.AudioListenerCreatedEvent), "WebAudio.audioListenerCreated" },
                { typeof(WebAudio.AudioListenerWillBeDestroyedEvent), "WebAudio.audioListenerWillBeDestroyed" },
                { typeof(WebAudio.AudioNodeCreatedEvent), "WebAudio.audioNodeCreated" },
                { typeof(WebAudio.AudioNodeWillBeDestroyedEvent), "WebAudio.audioNodeWillBeDestroyed" },
                { typeof(WebAudio.AudioParamCreatedEvent), "WebAudio.audioParamCreated" },
                { typeof(WebAudio.AudioParamWillBeDestroyedEvent), "WebAudio.audioParamWillBeDestroyed" },
                { typeof(WebAudio.NodesConnectedEvent), "WebAudio.nodesConnected" },
                { typeof(WebAudio.NodesDisconnectedEvent), "WebAudio.nodesDisconnected" },
                { typeof(WebAudio.NodeParamConnectedEvent), "WebAudio.nodeParamConnected" },
                { typeof(WebAudio.NodeParamDisconnectedEvent), "WebAudio.nodeParamDisconnected" },
                { typeof(Media.PlayerPropertiesChangedEvent), "Media.playerPropertiesChanged" },
                { typeof(Media.PlayerEventsAddedEvent), "Media.playerEventsAdded" },
                { typeof(Media.PlayerMessagesLoggedEvent), "Media.playerMessagesLogged" },
                { typeof(Media.PlayerErrorsRaisedEvent), "Media.playerErrorsRaised" },
                { typeof(Media.PlayersCreatedEvent), "Media.playersCreated" },
                { typeof(Console.MessageAddedEvent), "Console.messageAdded" },
                { typeof(Debugger.BreakpointResolvedEvent), "Debugger.breakpointResolved" },
                { typeof(Debugger.PausedEvent), "Debugger.paused" },
                { typeof(Debugger.ResumedEvent), "Debugger.resumed" },
                { typeof(Debugger.ScriptFailedToParseEvent), "Debugger.scriptFailedToParse" },
                { typeof(Debugger.ScriptParsedEvent), "Debugger.scriptParsed" },
                { typeof(HeapProfiler.AddHeapSnapshotChunkEvent), "HeapProfiler.addHeapSnapshotChunk" },
                { typeof(HeapProfiler.HeapStatsUpdateEvent), "HeapProfiler.heapStatsUpdate" },
                { typeof(HeapProfiler.LastSeenObjectIdEvent), "HeapProfiler.lastSeenObjectId" },
                { typeof(HeapProfiler.ReportHeapSnapshotProgressEvent), "HeapProfiler.reportHeapSnapshotProgress" },
                { typeof(HeapProfiler.ResetProfilesEvent), "HeapProfiler.resetProfiles" },
                { typeof(Profiler.ConsoleProfileFinishedEvent), "Profiler.consoleProfileFinished" },
                { typeof(Profiler.ConsoleProfileStartedEvent), "Profiler.consoleProfileStarted" },
                { typeof(Profiler.PreciseCoverageDeltaUpdateEvent), "Profiler.preciseCoverageDeltaUpdate" },
                { typeof(Runtime.BindingCalledEvent), "Runtime.bindingCalled" },
                { typeof(Runtime.ConsoleAPICalledEvent), "Runtime.consoleAPICalled" },
                { typeof(Runtime.ExceptionRevokedEvent), "Runtime.exceptionRevoked" },
                { typeof(Runtime.ExceptionThrownEvent), "Runtime.exceptionThrown" },
                { typeof(Runtime.ExecutionContextCreatedEvent), "Runtime.executionContextCreated" },
                { typeof(Runtime.ExecutionContextDestroyedEvent), "Runtime.executionContextDestroyed" },
                { typeof(Runtime.ExecutionContextsClearedEvent), "Runtime.executionContextsCleared" },
                { typeof(Runtime.InspectRequestedEvent), "Runtime.inspectRequested" },
            };
        }

        /// <summary>
        /// Gets the event type corresponding to the specified method name.
        /// </summary>
        public static bool TryGetTypeForMethodName(string methodName, out Type eventType)
        {
            return s_methodNameEventTypeDictionary.TryGetValue(methodName, out eventType);
        }

        /// <summary>
        /// Gets the method name corresponding to the specified event type.
        /// </summary>
        public static bool TryGetMethodNameForType<TEvent>(out string methodName)
            where TEvent : IEvent
        {
            return s_eventTypeMethodNameDictionary.TryGetValue(typeof(TEvent), out methodName);
        }
    }
}
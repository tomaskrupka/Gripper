[← Home](index.md) [Quickstart →](quickstart.md) [API →](api_reference.md)

# Features

Gripper is a browser automation toolbox that operates in a browser window under its control,
using the Chrome Devtools Protocol (CDP).

While retaining full access to the CDP, Gripper offers a set of more advanced features built on top.

These can be separated into two groups:
1. The features related to web-scraping of Single Page Applications.
1. The features related to deployment of the resulting web-scraping agent.

## Scraping SPAs

The following features are focused on enabling the following requirements:
- Scraping SPAs with service workers actively rebuilding the DOM.
- Reliable JavaScript evaluation in iFrames.

### Full CDP API Access

Gripper includes its own build of the [chrome-dev-tools-runtime](https://github.com/BaristaLabs/chrome-dev-tools-runtime), exposing a strongly-typed 1-1 mapping of the CDP types, commands and events.

This enables a type-safe development of custom routines that take advantage of all CDP features.

### Target Discovery and Attachment

If the browser is launched with site isolation disabled (as it is by default), Gripper automatically discovers and attaches to new targets as they are opened by Gripper itself, by a background Service Worker or even by manual interaction. 

Therefore, on enabled domains, events from new targets start streaming automatically to the existing subscribers. Similarly, thanks to automatic attachment, enabling a domain works on all existing targets.

### Browsing Context to Execution Context mapping

In theory, accessing a Browsing Context through the Runtime domain [doesn't make sense](https://github.com/ChromeDevTools/devtools-protocol/issues/72#issuecomment-347977976), so the CDP doesn't have a feature of script evaluation in an iFrame.

In practice, there is usually exactly one Execution Context that can be used to affect an iFrame's DOM by script evaluation, and when it is not (Browsing Context not having a DOM, Execution Context not having a Browsing context), such Browsing context does not present an interesting web-scraping target and can be safely ignored.

The `IContext` interface represents the mapping of a Browsing Context to the Execution Context with access to its DOM, and provides access to both.

This enables handy abstractions such as evaluating a script in an iFrame or accessing an HTML Node both with JavaScript and through the [browser domains](https://github.com/ChromeDevTools/devtools-protocol/blob/master/json/browser_protocol.json) such as `DOM` and `Network`.

Under the hood, Gripper tracks the lifetime of execution contexts and their associated iFrames and thanks to this knowledge, when the `IWebClient.GetContextsAsync()` is called, it can bind each of the present browsing contexts to the proper execution context.

### Smart waiting

The way an SPA bootstraps often depends on deployment-related factors like zone (varying round-trips to iFrames sourced from distant origins) or hardware.

Gripper provides a configurable `IWebClient.WaitUntilFramesLoadedAsync()` method that polls for changes in the frame tree and waits until all frames have been loaded and no new iFrames have started loading for a period of time.

Blocking until the DOM is stable is a reliable way to avoid errors like clicking stale elements or dereferencing destroyed execution contexts.

Also, compared to polling for a specific element or even blind hardcoded waiting, it is less prone to variance across different deployments.

## Deployment

These features aim at solving:
- Reliable, long-running and low-upkeep remote deployment.
- Running parallel web-scraping tasks in independent browser windows.

### Service oriented design

Gripper provides extensions for adding itself into an existing `IServiceCollection`.

Each time the client code resolves the transient `IWebClient` service, a fresh browser window is launched and bound.

This enables performing paralellizable tasks (connecting to multiple sibling WebSocket endpoints, obtaining multiple valid session tokens, polling multiple sibling pages...) from a single C&C thread.

### Automated browser launch and disposal (Windows only)

The `WebClientSettings` configuration class facilitates declarative management of browser instances lifetime.

Under the hood, the `Runtime.IChildProcessTracker` service prevents stale browser windows and memory leaks, enabling a long-running, no-downtime deployment.

### Fingerprint management

Gripper includes features that enable introducing some variance into new instances, making them harder to link. This is configurable, including:

- Specifying an HTTP proxy for each browser window.
- Passing custom startup parameters to new browser windows.
- Purging selected data from existing user data directory before launching (Windows only).
- Specifying the user data directory for each browser window.
- Binding to an existing browser window, as opposed to launching a new one.

### CDP forward compatibility

In addition to the strongly-typed API, Gripper provides overrides for literal invocation of CDP commands, returning the response as raw `JToken`.

This fallback makes it possible to use an older build of Gripper against Chrome with newer CDP API version.

### Chrome forward compatibility

Gripper aims to work reliably with out-of-the-box [Stable](https://chromereleases.googleblog.com/) Chrome on Windows/Windows Server, including robust handling of automatic Chrome updates that take place about once a month.

The built-in CDP wrapper build targets a version of the protocol, not the browser. This makes any breaking impact of automatic Chrome update on a running Gripper very unlikely,

The only time a running Gripper deployment needs a rebuild is when the browser updates during runtime and the new version's CDP API introduces breaking changes to a signature that the application *actively uses*.

## Soon™

Although Gripper is under active development, it won't be getting any new killer features anytime soon. The current focus is on distributed deployment, performance, test and documentation coverage and general stability and reliability.

The following features are half-baked or under development and will be getting some attention soon™:

- Discovery and attachment of isolated targets (when site-per-process, IsolateOrigins is not disabled).
- Forward compatibility for CDP events (relaying unknown events in a raw form).
- Passing custom parameters to new browser windows programmatically, adding to the ability of each instance to alter its fingerprint.
- Programmatically configuring new browser windows.
- Automated browser launch and disposal outside Windows.
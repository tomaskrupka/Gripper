[←Home](index.md) [Quickstart→](quickstart.md) [API→](api_reference.md)

# Features

While providing a full, type-safe access to the CDP, Gripper offers two sets of features built on top:

1. Web-scraping of Single Page Applications.
1. Reliable, distributed deployment of the resulting web-scraping agent.

- [Features](#features)
  - [1. Web-scraping of SPAs](#1-web-scraping-of-spas)
    - [Full CDP API Access](#full-cdp-api-access)
    - [Target Discovery and Attachment](#target-discovery-and-attachment)
    - [Browsing Context to Execution Context mapping](#browsing-context-to-execution-context-mapping)
    - [Smart waiting](#smart-waiting)
  - [2. Deployment](#2-deployment)
    - [Service oriented design](#service-oriented-design)
    - [Automated browser launch and disposal (Windows only)](#automated-browser-launch-and-disposal-windows-only)
    - [Fingerprint management](#fingerprint-management)
    - [CDP forward compatibility](#cdp-forward-compatibility)
    - [Chrome forward compatibility](#chrome-forward-compatibility)
- [Soon™](#soon)

## 1. Web-scraping of SPAs

These features are designed around the following requirements:
- Scraping SPAs with Service Workers actively rebuilding the DOM.
- Reliable JavaScript evaluation in iFrames.

### Full CDP API Access

Gripper includes its own build of the [chrome-dev-tools-runtime](https://github.com/BaristaLabs/chrome-dev-tools-runtime), exposing a strongly-typed 1-1 mapping of every CDP type, command and event.

This enables a type-safe development of custom routines that take advantage of all CDP features.

### Target Discovery and Attachment

If the browser is launched with site isolation disabled (as it is by default), Gripper automatically discovers and attaches to new targets as they are opened by Gripper itself, by a background Service Worker or even by manual interaction. 

Therefore, on enabled domains, events from new targets start streaming automatically to the existing subscribers. Similarly, thanks to automatic attachment, enabling a domain works on all existing targets.

### Browsing Context to Execution Context mapping

In theory, accessing a Browsing Context through the Runtime domain [doesn't make sense](https://github.com/ChromeDevTools/devtools-protocol/issues/72#issuecomment-347977976), so the CDP doesn't have a feature of script evaluation in an iFrame.

In practice, there is usually exactly one Execution Context that can be used to affect an iFrame's DOM by script evaluation. When it is not (Browsing Context not having a DOM, Execution Context not having a Browsing Context), such Browsing context does not present an interesting web-scraping target and can be safely ignored.

The `IContext` interface represents the mapping of a Browsing Context to the Execution Context with access to its DOM, and provides access to both.

This enables handy abstractions such as evaluating a script in an iFrame or accessing an HTML Node both with JavaScript and through the [browser domains](https://github.com/ChromeDevTools/devtools-protocol/blob/master/json/browser_protocol.json) such as `DOM` and `Network`.

Under the hood, Gripper tracks the lifetime of execution contexts and their associated iFrames. When the `IWebClient.GetContextsAsync()` is called, Gripper resolves the current Frame Tree and binds each of the present Browsing Contexts to the proper Execution Context.

### Smart waiting

The way an SPA bootstraps often depends on deployment-related factors like zone (varying round-trips to iFrames sourced from distant origins), hardware or headless/headed setup.

Gripper provides a configurable `IWebClient.WaitUntilFramesLoadedAsync()` method that polls for changes in the Frame Tree and waits until all frames have been loaded and no new iFrames have started loading for a period of time.

Blocking until the DOM is stable is a reliable way to avoid errors like clicking stale elements or dereferencing destroyed execution contexts.

Also, compared to polling for a specific element or even blind hardcoded waiting, it is less prone to variance across different deployments.

## 2. Deployment

These features aim at solving:
- Reliable, long-running and low-upkeep remote deployment.
- Running parallel web-scraping tasks in independent browser windows.

### Service oriented design

Gripper provides extensions for adding itself into an existing `IServiceCollection`.

Each time the client code resolves the transient `IWebClient` service, a fresh browser window is launched and bound.

This enables performing paralellizable tasks (negotiating multiple WebSockets, obtaining multiple session tokens, polling multiple sibling pages etc.) from a single C&C thread.

### Automated browser launch and disposal (Windows only)

The `WebClientSettings` configuration class facilitates declarative management of browser instances lifetime.

In the background, the `Runtime.IChildProcessTracker` service then prevents stale browser windows and memory leaks, enabling a long-running, no-downtime deployment.

### Fingerprint management

Gripper includes features that enable introducing some variance into new instances, making them harder to link. This is configurable, including:

- Specifying an HTTP proxy for each browser window.
- Passing custom startup parameters to new browser windows.
- Purging selected data from existing user data directory before launching (Windows only).
- Specifying the user data directory for each browser window.
- Binding to an existing browser window, as opposed to launching a new one.

### CDP forward compatibility

In addition to the strongly-typed API, Gripper provides overrides for literal invocation of CDP commands, returning the response as a raw `JToken`.

This fallback makes it possible to use an older build of Gripper against Chrome with a newer CDP API version.

### Chrome forward compatibility

Gripper aims to work reliably with out-of-the-box [Stable](https://chromereleases.googleblog.com/) Chrome on Windows/Windows Server, including robust handling of automatic Chrome updates that take place about once a month.

The built-in CDP wrapper targets a version of the protocol, not the browser. This makes any breaking impact of an automatic Chrome update on a running Gripper very unlikely.

The only time a Chrome update breaks a running Gripper deployment is when the new version's CDP API introduces breaking changes to a signature that the application *actively uses*.

# Soon™

Although Gripper is under active development, it won't be getting any new killer features anytime soon. The current focus is on polishing existing features, distributed deployment, performance, test and documentation coverage and general stability and reliability.

That includes:

- Discovery and attachment of isolated targets (when site-per-process, IsolateOrigins is not disabled).
- Forward compatibility for CDP events (relaying unknown events in a raw form).
- Passing custom parameters to new browser windows programmatically, adding to the ability of each instance to alter its fingerprint.
- Programmatically configuring new browser windows.
- Automated browser launch and disposal outside Windows.



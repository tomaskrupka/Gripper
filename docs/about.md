[←Home](index.md) [Quickstart→](quickstart.md) [Features→](features.md) [API→](api_reference.md)

# About Gripper

Gripper is `.NET` toolbox for creating powerful browser automation agents.
Gripper operates in a browser window under its control, using the Chrome Devtools Protocol (CDP).
Gripper provides a full, type-safe access to the CDP and a set of own features built on top.

- [About Gripper](#about-gripper)
- [Automating the Browser with Gripper](#automating-the-browser-with-gripper)
  - [Automating SPAs](#automating-spas)
  - [Deployment](#deployment)
- [Principles](#principles)
  - [Transparency](#transparency)
  - [Low Level Access](#low-level-access)
  - [Easy to keep up-to-date](#easy-to-keep-up-to-date)

# Automating the Browser with Gripper

Gripper enables straightforward implementation of any CDP command sequence by providing a full, type-safe access to the CDP endpoint of the hooked browser.

Furthermore, Gripper provides own APIs targetting the most common challenges:

1. Automation of Single Page Applications. [Features→](features.md/#1-automation-of-spas)
2. Reliable, long-running deployment of the resulting agent. [Features→](features.md/#2-deployment)

## Automating SPAs

For common tasks that can't be expressed as a simple CDP routine, Gripper abstracts the complexity away with own APIs:

| Problem | Solution |
| :--- | :--- |
| Intercepting HTTP, WebSocket traffic | Subscribe to [`WebClientEvent`](api/Gripper_WebClient_IWebClient_WebClientEvent)s. No missed events as targets are [automatically attached](features#target-discovery-and-attachment). |
| Emulating inputs | The [`IElement`](api/Gripper_WebClient_IElement) interface automatically handles focusing and presents a unified abstraction for mouse and keyboard actions. |
| JS Evaluation: CORS compliance | Gripper [tracks](features#browsing-context-to-execution-context-mapping) the execution contexts of iFrames. The [`IContext`](api/Gripper_WebClient_IContext) interface then abstracts script evaluation in an iFrame, complying with the rule that the DOM can only be touched from its own Execution Context. |
| JS Evaluation: CSRF compliance | Thanks to the [`IContext`](api/Gripper_WebClient_IContext) interface, any iFrame can be used to evaluate a Fetch request against its origin, within the browser CSRF policy. |
| Unexpected overlays/popups | |

## Deployment

Gripper enables implementing custom solutions of specific deployment-related challenges by [passing ownership](#transparency) of its resources.

For common painpoints, Gripper is equipped with services that either solve the problem or provide useful tooling.

| Problem | Solution |
| :--- | :--- |
| Zombie artifacts on crash or restart | The [`IChildProcessTracker`](api/Gripper_WebClient_Runtime_IChildProcessTracker) service automatically registers and kills on exit Gripper child processes. This prevents zombie artifacts like hanging browser windows from accummulating. |
| Horizontal scaling | The root [`IWebClient`](api/Gripper_WebClient_IWebClient) interface that represents a browser window is designed as a [transient service](features#service-oriented-design). Any number of instances can be resolved in parallel and configured independently. |
| Authorized session timeout | Gripper enables purging selected data from the user profile before launch, or launching from a fresh profile directory. This makes it easy to implement reliable login renewal by just ditching the old instance and re-running the login routine using a fresh [`IWebClient`](api/Gripper_WebClient_IWebClient) instance. |
| Automatic browser update | The built-in CDP wrapper targets a version of the protocol, not the browser, making Gripper [forward compatible](features/#chrome-forward-compatibility) with Chrome versions. It is [unlikely](features/#cdp-forward-compatibility) that Chrome update will break the agent. |
| Responsive resizing | |
| Memory leaks | Gripper prevents two major sources of memory leaks. First, Gripper prevents hanging pointers to destroyed Execution Contexts by tracking the [executionContextDestroyed](https://chromedevtools.github.io/devtools-protocol/tot/Runtime/#event-executionContextDestroyed) event. Second, by exposing the [`WebClientEvent`](api/Gripper_WebClient_IWebClient_WebClientEvent) delegate, Gripper enables proactive unsubscribing from events. |

# Principles

## Transparency

Gripper aims to be a completely transparent layer. The rule of thumb is that it should be possible to build Gripper on top of itself.

Gripper passes ownership to the user by exposing the resources it creates or binds.
This includes the browser OS `Process` handle, the root `ChromeSession` instance or full raw access to the browser CDP API endpoint. 

## Low Level Access

Compared to other popular automation frameworks, Gripper makes the CDP layer completely visible and accessible rather than masking its presence behind own adapters.

While Gripper does wrap the CDP with own code, the Types, Commands and Events are always a 1-1 mapping to the [CDP definition](https://github.com/ChromeDevTools/devtools-protocol/tree/master/json). In fact, they're [autogenerated](https://github.com/tomaskrupka/chrome-dev-tools-generator) from there.

## Easy to keep up-to-date

The main [`Gripper.WebClient`](https://www.nuget.org/packages/Gripper.WebClient/) package depends on [`Gripper.ChromeDevTools`](https://www.nuget.org/packages/Gripper.ChromeDevTools/) for the CDP wrapper. Both packages are being kept up-to-date with the latest CDP API.

Furthermore, the `Gripper.ChromeDevTools` library can be [autogenerated](https://github.com/tomaskrupka/chrome-dev-tools-generator) against any version of the CDP API (defaults to [latest tip-of-tree](https://github.com/ChromeDevTools/devtools-protocol/tree/master/json)).

This means that creating updated Gripper builds as the protocol evolves can be fully automated.

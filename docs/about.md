# What is Gripper

Gripper is a web scraping toolbox that operates in a browser window under its control,
using the chrome devtools protocol (CDP).

Compared to the many popular integration-testing frameworks,
Gripper is not an abstraction layer designed to mask the presence of the CDP—it is a library for management of the CDP runtime,
paired with a toolbox of web-scraping routines on top.

The main focus is on modern SPAs that are hard to get a grip of.

# When to consider Gripper

Consider whether your web-scraping project has the following needs:
1. **You need an open browser window** at some point of the routine.
1. **The target is an SPA** with a DOM that's difficult to navigate.
Gripper will break the page into a navigable interface.
1. **The target is a PWA** with lots of background activity and a DOM that's always changing.
Gripper will let you interact with the element before you lose the grip.
2. **You tried common testing frameworks** and failed.
Gripper is focused on web-scraping, so it has features that testing frameworks don't.
3. **It will run for a long time**.
Gripper aspires to be a deploy-and-forget library.
4. **You need access to the Chrome Remote Devtools API**.
Gripper won't stand in your way.

# When to use something else

As tradeoffs for staying true to its principles, Gripper has some limitations:
1. Gripper nas no networking abilities like subscribing to broadcasts or automated polling of REST APIs.
These are off the scope of this project.
2. Gripper has no JavaScript, CSS Selectors, XPath and similar builders.
3. Gripper covers just a fraction of the rich CDP API and its functionality is almost never a 1-1 mapping.
You'll often have to call the directly.
4. Gripper may take some time to set up if none of the default configs suits you.

# Principles

When prioritizing features and evaluating tradeoffs, the development of Gripper is governed by the following principles:

## The user knows best

- Gripper will pass the ownership.
In the dilemma of saving the user from themselves vs. hiding something useful,
Gripper will err on the side of the user. That includes websockets, OS process handles etc.
- Gripper won't try to get out of trouble by itself, just relay the error and let the user figure it out.
- Gripper aims to eliminate hardcoded behaviour. Every time there's a decision, there's a config attribute.

## Environment neutrality

- Gripper aims to be deployable to as many targets as possible.
- Gripper aims to enable applications that produce the same outcome across conditions and environments.
- Gripper is designed as a service, easy to configure and plug into an existing host.

# Features

Please see the [tests](https://github.com/tomaskrupka/Gripper/tree/main/test/Gripper.Test) for the current state.

## Implemented features

- CDP tunnelling (method calls and events).
- Automatic target (iFrame) binding.
- 1-1 mapping of a Gripper interface and a DOM element.
- 1-1-1 mapping of a Gripper interface, an iFrame and its corresponding JavaScript execution context.
- Target detection after the `Page.frameNavigated`, `Page.frameAttached` and `Page.frameStoppedLoading` events.

## Soon™

- `fetch()` injection.
- Pausing script execution in specific contexts.
- API to wait for an SPA that's building up to settle.

# Deployment
Gripper aims to support as many (OS, Browser) combinations as possible.
The basic dependencies are a .NET 6 Runtime and an installation of a browser that supports remote control over the CDP.

Below is a list of tested configurations:

Soon™
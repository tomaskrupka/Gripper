# WebScrapingServices

WebScrapingServices (WSS) is a web scraping toolbox that operates in a browser window under its control.

It relies on the [chrome devtools protocol](https://chromedevtools.github.io/devtools-protocol/) (CDP)
and is configurable to use either [Selenium](https://github.com/SeleniumHQ)
or [chrome-dev-tools-runtime](https://github.com/BaristaLabs/chrome-dev-tools-runtime) as the CDP client.

## Interface

The CDP has a rich interface and countless use cases.

In cases of directly adopting the CDP interface,
the WSS reduces its depth and width greatly with a facade that is solely focused on the web-scraping use case.

# Gripper

Gripper is a web scraping toolbox.

### <a name="WebClient"></a> Gripper.WebClient 
`Gripper.WebClient` launches its own browser window and uses the [chrome devtools protocol](https://chromedevtools.github.io/devtools-protocol/) (CDP) to control it.
`Gripper.WebClient` provides a slim, web-scraping-focused facade over the rich CDP API while retaining the full access to it for the rare cases. Uses [chrome-dev-tools-runtime](https://github.com/BaristaLabs/chrome-dev-tools-runtime) as the CDP client.

[It's on nuget](https://www.nuget.org/packages/Gripper.WebClient).


### Gripper.Utils
`Gripper.Utils` is a toolbox for easy automation of common web-scraping routines. Easy to integrate into an existing DI as a singleton service.


### Gripper.Authenticated
`Gripper.Authenticated` is dead, [`Gripper.WebClient`](#WebClient) killed him.

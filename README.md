# YTMDesktop CSharp Companion

This is a library for the YTMDesktop Companion Server which lets you easier communicate with the server and handle
authorization and so on.

## Table of contents

<!-- toc -->

- [Badges](#badges)
- [Installation](#installation)
- [Usage](#usage)
- [How to contribute?](#how-to-contribute)

<!-- tocstop -->

## Badges

[![Forks](https://img.shields.io/github/forks/XeroxDev/ytmdesktop-cs-companion?color=blue&style=for-the-badge)](https://github.com/XeroxDev/ytmdesktop-cs-companion/network/members)  [![Stars](https://img.shields.io/github/stars/XeroxDev/ytmdesktop-cs-companion?color=yellow&style=for-the-badge)](https://github.com/XeroxDev/ytmdesktop-cs-companion/stargazers) [![Watchers](https://img.shields.io/github/watchers/XeroxDev/ytmdesktop-cs-companion?color=lightgray&style=for-the-badge)](https://github.com/XeroxDev/ytmdesktop-cs-companion/watchers) [![Contributors](https://img.shields.io/github/contributors/XeroxDev/ytmdesktop-cs-companion?color=green&style=for-the-badge)](https://github.com/XeroxDev/ytmdesktop-cs-companion/graphs/contributors)

[![Issues](https://img.shields.io/github/issues/XeroxDev/ytmdesktop-cs-companion?color=yellow&style=for-the-badge)](https://github.com/XeroxDev/ytmdesktop-cs-companion/issues) [![Issues closed](https://img.shields.io/github/issues-closed/XeroxDev/ytmdesktop-cs-companion?color=yellow&style=for-the-badge)](https://github.com/XeroxDev/ytmdesktop-cs-companion/issues?q=is%3Aissue+is%3Aclosed)

[![Issues-pr](https://img.shields.io/github/issues-pr/XeroxDev/ytmdesktop-cs-companion?color=yellow&style=for-the-badge)](https://github.com/XeroxDev/ytmdesktop-cs-companion/pulls) [![Issues-pr closed](https://img.shields.io/github/issues-pr-closed/XeroxDev/ytmdesktop-cs-companion?color=yellow&style=for-the-badge)](https://github.com/XeroxDev/ytmdesktop-cs-companion/pulls?q=is%3Apr+is%3Aclosed) [![PRs welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=for-the-badge)](https://github.com/XeroxDev/ytmdesktop-cs-companion/compare)

![Version](https://img.shields.io/nuget/v/XeroxDev.YTMDesktop.Companion?style=for-the-badge) ![Downloads](https://img.shields.io/nuget/dt/XeroxDev.YTMDesktop.Companion?style=for-the-badge)

[![Awesome Badges](https://img.shields.io/badge/badges-awesome-green?style=for-the-badge)](https://shields.io)

## Installation

TODO: Add installation instructions

## Usage

Can be easier seen in the [example file](https://github.com/XeroxDev/ytmdesktop-cs-companion/blob/main/YTMDesktopCompanion.Example/Program.cs)
Also you can look through the [documentation](https://xeroxdev.github.io/ytmdesktop-cs-companion/) but here's a quick rundown:

```csharp
// import everything
using System.Text.Json;
using NuGet.Versioning;
using XeroxDev.YTMDesktop.Companion;
using XeroxDev.YTMDesktop.Companion.Settings;

// Set YOUR version (or fetch them from anywhere) I will use a static version for this example 
var version = SemanticVersion.Parse("1.0.0");

// Define settings (add token if you have one, see bigger example for how this could be done)
var settings = new ConnectorSettings(
    "127.0.0.1",
    9863,
    "ytmdesktop-cs-companion-example",
    "YTMDesktop C# Companion Example",
    version
);

// Create a new connector
var connector = new CompanionConnector(settings);

// extract clients for easier access
var restClient = connector.RestClient;
var socketClient = connector.SocketClient;

// Get metadata
var metadata = await restClient.GetMetadata();
if (metadata is null)
{
    Console.WriteLine("Failed to get metadata. Probably the server is not running or the settings are wrong.");
    return;
}

Console.WriteLine(JsonSerializer.Serialize(metadata));

// Get token
var code = await restClient.GetAuthCode();
if (code is null)
{
    Console.WriteLine("Failed to get auth code. Probably the server is not running or the settings are wrong.");
    return;
}

Console.WriteLine($"Got new code, please compare it with the code from YTMDesktop: {code}");

var token = await restClient.GetAuthToken(code);
if (string.IsNullOrWhiteSpace(token))
{
    Console.WriteLine("Something went wrong...");
    return;
}
// Get the current state and print it
var state = await restClient.GetState();
Console.WriteLine(JsonSerializer.Serialize(state));

// Pause current track
await restClient.Pause();

// wait 2 seconds
await Task.Delay(2000);

// Resume current track
await restClient.Play();
```

## How to contribute?

Just fork the repository and create PR's.

> [!NOTE]
> We're using [release-please](https://github.com/googleapis/release-please) to optimal release the library.
> release-please is following the [conventionalcommits](https://www.conventionalcommits.org) specification.
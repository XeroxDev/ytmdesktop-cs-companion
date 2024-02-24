using System.Text.Json;
using NuGet.Versioning;
using XeroxDev.YTMDesktop.Companion;
using XeroxDev.YTMDesktop.Companion.Settings;

#region Setup

// Set YOUR version (or fetch them from anywhere) I will use a static version for this example 
var version = SemanticVersion.Parse("1.0.0");

// Check if file ".token" exists
const string tokenPath = ".token";
if (!File.Exists(tokenPath))
{
    // Create file (with empty content)
    File.Create(tokenPath).Close();
}

// Read token from file
var token = File.ReadAllText(tokenPath);

// Define settings
var settings = new ConnectorSettings(
    "127.0.0.1",
    9863,
    "ytmdesktop-cs-companion-example",
    "YTMDesktop C# Companion Example",
    version
);

// Check if token is set
if (!string.IsNullOrWhiteSpace(token)) settings.Token = token;

var connector = new CompanionConnector(settings);

// extract clients for easier access
var restClient = connector.RestClient;
var socketClient = connector.SocketClient;

#endregion

#region RestClient

// Get metadata
var metadata = await restClient.GetMetadata();
if (metadata is null)
{
    Console.WriteLine("Failed to get metadata. Probably the server is not running or the settings are wrong.");
    return;
}

Console.WriteLine(JsonSerializer.Serialize(metadata));

// Example Rest Client usage

// Check if token is set
if (string.IsNullOrWhiteSpace(token))
{
    // If not, try to request one and show it to the user
    var code = await restClient.GetAuthCode();
    if (code is null)
    {
        Console.WriteLine("Failed to get auth code. Probably the server is not running or the settings are wrong.");
        return;
    }

    Console.WriteLine($"Got new code, please compare it with the code from YTMDesktop: {code}");

    token = await restClient.GetAuthToken(code);
    if (string.IsNullOrWhiteSpace(token))
    {
        Console.WriteLine("Something went wrong...");
        return;
    }

    // Save token to file
    File.WriteAllText(tokenPath, token);

    // Show full path
    var fullPath = Path.GetFullPath(tokenPath);
    Console.WriteLine($"Authorization successful! Token saved to: {fullPath}");

    connector.SetAuthToken(token);
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

#endregion

#region SocketClient

// Example Socket Client usage

// Register events
socketClient.OnError += (sender, args) => Console.WriteLine($"Error: {args.Message}");
socketClient.OnConnectionChange += (sender, args) => Console.WriteLine($"Connection changed: {args}");
socketClient.OnStateChange += (sender, args) => Console.WriteLine(JsonSerializer.Serialize(args));
socketClient.OnPlaylistCreated += (sender, args) => Console.WriteLine(JsonSerializer.Serialize(args));
socketClient.OnPlaylistDeleted += (sender, args) => Console.WriteLine($"Playlist deleted: {args}");

// Connect to the server
await socketClient.Connect();

#endregion

// Wait for user input to close the application
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
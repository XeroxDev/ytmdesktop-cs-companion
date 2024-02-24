using System.Text.Json.Serialization;
using XeroxDev.YTMDesktop.Companion.Enums;

namespace XeroxDev.YTMDesktop.Companion.Models.Input;

/// <summary>
/// The input for the command endpoint.
/// </summary>
[JsonSerializable(typeof(CommandInput))]
public class CommandInput
{
    [JsonPropertyName("command"), JsonRequired]
    public string Command { get; set; }

    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Data { get; set; }

    /// <summary>
    /// The input for the command endpoint.
    /// </summary>
    /// <param name="command">The command to execute</param>
    /// <param name="data">The data to send with</param>
    public CommandInput(string command, object? data = null)
    {
        Command = command;
        Data = data;
    }

    /// <summary>
    /// The input for the command endpoint.
    /// </summary>
    /// <param name="command">The command to execute</param>
    /// <param name="data">The data to send with</param>
    public CommandInput(ECommand command, object? data = null) : this(command.ToCommandString(), data)
    {
    }
}
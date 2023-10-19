using System;
using System.Text.Json.Serialization;

namespace WasabiCli.Models.Info;

public class PeerInfo
{
    [JsonPropertyName("isConnected")]
    public bool IsConnected { get; set; }

    [JsonPropertyName("lastSeen")]
    public DateTimeOffset LastSeen { get; set; }

    [JsonPropertyName("endpoint")]
    public string? Endpoint { get; set; }

    [JsonPropertyName("userAgent")]
    public string? UserAgent { get; set; }
}

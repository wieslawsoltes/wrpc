using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Info;

public class KeyInfo
{
    [JsonPropertyName("fullKeyPath")]
    public string? FullKeyPath { get; set; }

    [JsonPropertyName("internal")]
    public bool? Internal { get; set; }

    [JsonPropertyName("keyState")]
    public int KeyState { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("scriptPubKey")]
    public string? ScriptPubKey { get; set; }

    [JsonPropertyName("pubkey")]
    public string? PubKey { get; set; }

    [JsonPropertyName("pubKeyHash")]
    public string? PubKeyHash { get; set; }

    [JsonPropertyName("address")]
    public string? Address { get; set; }
}


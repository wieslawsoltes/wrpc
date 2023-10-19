using System.Text.Json.Serialization;

namespace WasabiCli.Models.Info;

public class AddressInfo
{
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    [JsonPropertyName("keyPath")]
    public string? KeyPath { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("publicKey")]
    public string? PublicKey { get; set; }

    [JsonPropertyName("scriptPubKey")]
    public string? ScriptPubKey { get; set; }
}

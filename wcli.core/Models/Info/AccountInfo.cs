using System.Text.Json.Serialization;

namespace WasabiCli.Models.Info;

public class AccountInfo
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("publicKey")]
    public string? PublicKey { get; set; }

    [JsonPropertyName("keyPath")]
    public string? KeyPath { get; set; }
}

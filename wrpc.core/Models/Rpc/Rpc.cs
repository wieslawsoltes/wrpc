using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Rpc;

public abstract class Rpc
{
    [JsonPropertyName("jsonrpc")]
    public string? JsonRpc { get; set; } = "2.0";

    [JsonPropertyName("id")]
    public string? Id { get; set; } = "1";
}

using System.Text.Json.Serialization;

namespace wcli.Models;

public class RpcMethod : Rpc
{
    [JsonPropertyName("method")]
    public string? Method { get; set; }

    [JsonPropertyName("params")]
    public string? Params { get; set; }
}

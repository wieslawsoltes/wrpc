using System.Text.Json.Serialization;

namespace WasabiCli.Models.RpcJson;

public class RpcMethod : Rpc
{
    [JsonPropertyName("method")]
    public string? Method { get; set; }

    [JsonPropertyName("params")]
    public string? Params { get; set; }
}

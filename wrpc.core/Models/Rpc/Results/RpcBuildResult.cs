using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Rpc.Results;

public class RpcBuildResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public string? Result { get; set; }
}

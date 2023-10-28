using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Results;

public class RpcBuildResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public string? Result { get; set; }
}

using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Rpc.Results;

public class RpcPayInCoinjoinResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public string? Result { get; set; }
}

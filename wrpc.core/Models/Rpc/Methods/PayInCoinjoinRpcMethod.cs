using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Rpc.Methods;

public class PayInCoinjoinRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public string?[]? Params { get; set; }
}
using System.Text.Json.Serialization;

namespace WasabiRpc.Models.Rpc.Methods;

public class GetNewAddressRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public string?[]? Params { get; set; }
}
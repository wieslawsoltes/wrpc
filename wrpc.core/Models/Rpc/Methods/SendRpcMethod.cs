using System.Text.Json.Serialization;
using WasabiRpc.Models.Params.Send;

namespace WasabiRpc.Models.Rpc.Methods;

public class SendRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public Send? Params { get; set; }
}
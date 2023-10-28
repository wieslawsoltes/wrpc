using System.Text.Json.Serialization;
using WasabiRpc.Models.Params.Send;

namespace WasabiRpc.Models.Results;

public class SendRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public Send? Params { get; set; }
}
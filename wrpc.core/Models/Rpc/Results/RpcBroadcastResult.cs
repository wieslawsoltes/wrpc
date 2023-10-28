using System.Text.Json.Serialization;
using WasabiRpc.Models.Info;

namespace WasabiRpc.Models.Results;

public class RpcBroadcastResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public BroadcastInfo? Result { get; set; }
}

using System.Text.Json.Serialization;
using WasabiRpc.Models.Info;

namespace WasabiRpc.Models.Rpc.Results;

public class RpcSendResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public SendInfo? Result { get; set; }
}

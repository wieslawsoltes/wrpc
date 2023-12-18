using System.Text.Json.Serialization;
using WasabiRpc.Models.Info;

namespace WasabiRpc.Models.Rpc.Results;

public class RpcGetStatusResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public StatusInfo? Result { get; set; }
}

using System.Text.Json.Serialization;
using WasabiCli.Models.Info;

namespace WasabiCli.Models.Results;

public class RpcBroadcastResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public BroadcastInfo? Result { get; set; }
}

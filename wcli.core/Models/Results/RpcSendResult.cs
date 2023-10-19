using System.Text.Json.Serialization;
using WasabiCli.Models.Info;

namespace WasabiCli.Models.Results;

public class RpcSendResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public SendInfo? Result { get; set; }
}

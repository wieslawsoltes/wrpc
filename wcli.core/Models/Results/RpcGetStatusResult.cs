using System.Text.Json.Serialization;
using WasabiCli.Models.Info;

namespace WasabiCli.Models.Results;

public class RpcGetStatusResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public StatusInfo? Result { get; set; }
}

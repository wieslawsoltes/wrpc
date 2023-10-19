using System.Text.Json.Serialization;
using WasabiCli.Models.Info;

namespace WasabiCli.Models.Results;

public class RpcErrorResult : Rpc
{
    [JsonPropertyName("error")]
    [JsonRequired]
    public ErrorInfo? Error { get; set; }
}

using System.Text.Json.Serialization;
using WasabiRpc.Models.Info;

namespace WasabiRpc.Models.Results;

public class RpcErrorResult : Rpc
{
    [JsonPropertyName("error")]
    [JsonRequired]
    public ErrorInfo? Error { get; set; }
}

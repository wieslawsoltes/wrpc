using System.Text.Json.Serialization;

namespace WasabiCli.Models.RpcJson;

public class RpcStopResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public object? Result { get; set; }
}

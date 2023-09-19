using System.Text.Json.Serialization;

namespace wcli.Models;

public class RpcGetStatusResult : Rpc
{
    [JsonPropertyName("result")]
    public GetStatusResult? Result { get; set; }
}

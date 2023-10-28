using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiRpc.Models.Info;

namespace WasabiRpc.Models.Rpc.Results;

public class RpcListKeysResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public List<KeyInfo>? Result { get; set; }
}

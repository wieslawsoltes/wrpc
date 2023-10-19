using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiCli.Models.Info;

namespace WasabiCli.Models.Results;

public class RpcListKeysResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public List<KeyInfo>? Result { get; set; }
}

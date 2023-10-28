using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiRpc.Models.Info;

namespace WasabiRpc.Models.Rpc.Results;

public class RpcListUnspentCoinsResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public List<CoinInfo>? Result { get; set; }
}

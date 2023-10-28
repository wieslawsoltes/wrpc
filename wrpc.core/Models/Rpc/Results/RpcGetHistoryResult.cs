using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiRpc.Models.Info;

namespace WasabiRpc.Models.Rpc.Results;

public class RpcGetHistoryResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public List<TransactionInfo>? Result { get; set; }
}

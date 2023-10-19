using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiCli.Models.Info;

namespace WasabiCli.Models.Results;

public class RpcGetHistoryResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public List<TransactionInfo>? Result { get; set; }
}

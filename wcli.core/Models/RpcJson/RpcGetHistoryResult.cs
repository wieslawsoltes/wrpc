using System.Collections.Generic;
using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.Models.RpcJson;

public class RpcGetHistoryResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public List<TransactionInfo>? Result { get; set; }
}

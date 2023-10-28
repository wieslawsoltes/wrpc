using System.Text.Json.Serialization;
using WasabiRpc.Models.Params.Transactions;

namespace WasabiRpc.Models.Results;

public class CancelTransactionRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public CancelTransaction? Params { get; set; }
}
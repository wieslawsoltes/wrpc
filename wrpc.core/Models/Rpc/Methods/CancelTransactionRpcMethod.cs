using System.Text.Json.Serialization;
using WasabiRpc.Models.Params.Transactions;

namespace WasabiRpc.Models.Rpc.Methods;

public class CancelTransactionRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public CancelTransaction? Params { get; set; }
}
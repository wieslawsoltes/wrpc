using System.Text.Json.Serialization;
using WasabiRpc.Models.Params.Transactions;

namespace WasabiRpc.Models.Results;

public class ExcludeFromCoinJoinRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public ExcludeFromCoinJoin? Params { get; set; }
}
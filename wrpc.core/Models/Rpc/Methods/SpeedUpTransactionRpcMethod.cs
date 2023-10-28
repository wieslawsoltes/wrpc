using System.Text.Json.Serialization;
using WasabiRpc.Models.Params.Transactions;

namespace WasabiRpc.Models.Results;

public class SpeedUpTransactionRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public SpeedUpTransaction? Params { get; set; }
}
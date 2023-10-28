using System.Text.Json.Serialization;
using WasabiRpc.Models.Params.Transactions;

namespace WasabiRpc.Models.Rpc.Methods;

public class SpeedUpTransactionRpcMethod : RpcMethod
{
    [JsonPropertyName("params")]
    public SpeedUpTransaction? Params { get; set; }
}
using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.Models.RpcJson;

public class RpcBroadcastResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public BroadcastInfo? Result { get; set; }
}

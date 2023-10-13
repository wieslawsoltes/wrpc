using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.Models.RpcJson;

public class RpcSendResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public SendInfo? Result { get; set; }
}

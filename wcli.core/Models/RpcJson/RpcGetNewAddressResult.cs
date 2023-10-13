using System.Text.Json.Serialization;
using WasabiCli.Models.WalletWasabi;

namespace WasabiCli.Models.RpcJson;

public class RpcGetNewAddressResult : Rpc
{
    [JsonPropertyName("result")]
    [JsonRequired]
    public AddressInfo? Result { get; set; }
}
